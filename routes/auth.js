var db = require('../data/database').db;
var jwt = require('../config/token');
var config = require('../config/config');

module.exports = function(request, email, password, callback){
    db.User.findOne({ email: email },
        function(err, user){
            if(!err && user != null){
                //found user, now check password!
                user.comparePassword(password, function(error, match){
                    if(match){
                        var profile = { 
                            id: user.id,
                            name: user.username,
                            isAdmin: user.isAdmin
                        };
                        var token = jwt.encode(profile);
                        request.session.set(config.sessionKey, token);
                        callback(token);
                    }
                    else callback(match);
                });
            }   
            else{
                callback(false, null);
            } 
        }); 
};