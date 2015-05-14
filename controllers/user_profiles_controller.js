var db = require('../data/database').db;

function getUserProfile(userId, callback){
    db.User.findOne({_id: userId}, function(err, user){
       if(!err && user != null){
           db.Officer.findOne({userId: user._id}, function(error, officer){
               if(officer){
                   console.log(officer);
                   var profile = {
                       isLoggedIn: true,
                       isAdmin: user.isAdmin,
                       profile: {
                           username: user.username,
                           email: user.email,
                           bio: officer.bio == "" ? "" : officer.bio,
                           position: officer.position
                       }
                   };
                   callback(profile);
                   return;
               }
               callback(null);
               return;
           });
       }
       else {
            callback(null);
       }
   });
}

module.exports = {
	fetch: getUserProfile
};