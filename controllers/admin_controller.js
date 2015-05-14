var Users = require('./users_controller');
var Officers = require('./officers_controller');
var Profiles = require('./user_profiles_controller');

function getProfile(id, callback){
   Profiles.fetch(id, function(profile){
       Users.fetch(function(users){
          Officers.fetch(function(officers){
               callback({
                   profile: profile,
                   users: users,
                   officers: officers
               });
          }) ;
       });
   });
}

module.exports = {
	fetch: getProfile
};