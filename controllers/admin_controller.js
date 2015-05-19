var Officers = require('./officers_controller');
var Profiles = require('./user_profiles_controller');

function getProfile(id, callback){
   Profiles.fetch(id, function(profile){
      Officers.fetch(function(officers){
         callback({
             profile: profile,
             officers: officers
         });
       });    
   });
}

module.exports = {
	fetch: getProfile
};