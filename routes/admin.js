var database = require ('../data/database');
var Joi = require('joi');
var auth = require('./auth');

function loggedIn(request){
    var session = request.session.get("currentUser");
    return { 
        isLoggedIn: session != null,
        isAdmin: session == null ? false : session.isAdmin
      };
}

function getUserProfile(db, userId){
    db.User.findOne({id: userId}, function(err, user){
       if(!err && user != null){
           db.Officer.findOne({userId: user.id}, function(error, officer){
               db.close();
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
               return profile;
           });
       }
       else {
            return null;
       }
   });
}

function getUsers(db) {
    return db.User.find({isSysAdmin: false}, { password: 0 });
}

function getOfficers(db){
    return db.Officer.find();
}

function getSettings(session, callback){
    database(function(db){
       var userProfile = getUserProfile(db, session.id);
       var users = getUsers(db);
       var officers = getOfficers(db);
       db.close();
       callback({
           profile: userProfile,
           users: users,
           officers: officers
       });
    });
}

function createNewUser(objToAdd){
    database(function(db){
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
                   db.close();
                   return toAdd;
               });
               //error. So return null.
               return null;
           } 
           else db.close();
        });
    });
    return null;
}

module.exports= [{
    method: 'GET',
    path: '/admin',
    config: {
        auth: 'token',
        handler: function(request, reply){
            var session = request.session.get("currentUser");
            if(session == null){
                reply.redirect('/NotAuthorized');
            }
            else{
                getSettings(session, function(settings){
                    if(settings == null){
                        reply.redirect('/PageNotFound');
                    }
                    else {
                        var toSend = loggedIn(request);
                        toSend.settings = settings;
                        reply.view('admin', toSend);
                    }
                });
            }
        }
    }
},
{
    method: 'POST',
    path: '/admin/user/create',
    config: {
        auth: 'token',
        validate: {
            payload: {
                email: Joi.string().min(4).required(),
                password: Joi.string().min(8).required(),
                username: Joi.string().required(),
                isAdmin: Joi.boolean().required()
            }
        },
        handler: function(request, reply){
            var session = request.session.get("currentUser");
            if(session == null){
                reply.redirect('/NotAuthorized');
            }
            else{
                reply(createNewUser(request.payload));
            }
        }
    }
}];