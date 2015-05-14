var db = require ('../data/database').db;
var Joi = require('joi');
var auth = require('./auth');
var jwt = require('../config/token');
var config = require('../config/config');

function loggedIn(token){
    return { 
        isLoggedIn: token != null
      };
}

function getUserProfile(userId, callback){
    db.User.findOne({id: userId}, function(err, user){
       if(!err && user != null){
           db.Officer.findOne({userId: user.id}, function(error, officer){
               var profile = {
                   isLoggedIn: true,
                   isAdmin: user.isAdmin,
                   profile: {
                       username: user.username,
                       email: user.email,
                       bio: officer.bio,
                       position: officer.position
                   }
               };
               callback(profile);
           });
       }
       else {
            callback(null);
       }
   });
}

function getUsers(callback) {
    db.User.find({isSysAdmin: false}, { password: 0 }, function(err, user){
        if(!err)
            callback(user);
        else
            callback(null);
    });
}

function getOfficers(callback){
    db.Officer.find(function(err, officers){
        if(!err)
            callback(officers);
        else
            callback(null);
    });
}

function getSettings(profile, callback){
   getUserProfile(profile.id, function(profile){
       getUsers(function(users){
          getOfficers(function(officers){
               callback({
                   profile: profile,
                   users: users,
                   officers: officers
               });
          }) ;
       });
   });
}

function createNewUser(objToAdd, callback){
    db.User.findOne({email: objToAdd.email}, function(error, user){
       if(user == null){
           //no user with that email, so add them.
           var toAdd = new db.User({
               username: objToAdd.username,
               email: objToAdd.email,
               isAdmin: objToAdd.isAdmin,
               password: objToAdd.password,
               isSysAdmin: false,
               enabled: true
           });
           toAdd.save(function(error, success){
               callback(toAdd);
           });
           //error. So return null.
           callback();
       } 
    });
}

module.exports= [{
    method: 'GET',
    path: '/admin',
    config: {
        handler: function(request, reply){
            jwt.processRequest(request, function(profile){
                if(!profile){
                    reply.redirect('/NotAuthorized');
                }
                else{
                    getSettings(profile, function(settings){
                        if(settings == null){
                            reply.redirect('/PageNotFound');
                        }
                        else {
                            var toSend = loggedIn(profile);
                            toSend.settings = settings;
                            reply.view('./admin/admin', toSend);
                        }
                    });
                }
            });
        }
    }
},
{
    method: 'POST',
    path: '/admin/user/create',
    config: {
        validate: {
            payload: {
                email: Joi.string().min(4).required(),
                password: Joi.string().min(8).required(),
                username: Joi.string().required(),
                isAdmin: Joi.boolean().required()
            }
        },
        handler: function(request, reply){
            jwt.processRequest(request, reply, function(token){
                if(token == null){
                    reply.redirect('/NotAuthorized');
                }
                else{
                    createNewUser(request.payload, function(added){
                        reply(added);
                    });
                }
            });
        }
    }
}];