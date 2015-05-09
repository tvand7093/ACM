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

module.exports= [{
    method: 'GET',
    path: '/admin',
    config: {
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
}];