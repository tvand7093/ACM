var database = require('../data/database');

module.exports = function(email, password, callback){
    database(function(db){
       db.User.findOne({ email: email },
            function(err, user){
                if(!err && user != null){
                    //found user, now check password!
                    user.comparePassword(password, function(error, match){
                        db.close();
                        callback(match, { id: user.id, name: user.username, isAdmin: user.isAdmin });
                    });
                }   
                else{
                    db.close();
                    callback(false, null);
                } 
            }); 
    });
};