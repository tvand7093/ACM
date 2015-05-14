var jwt = require('jsonwebtoken'),
    database = require('../data/database');
    
var privateKey = 'BbZJjyoXAdr8BUZuiKKARWimKfrSmQ6fv8kZ7OFfc';

// Use this token to build your request with the 'Authorization' header.  
// Ex:
//     Authorization: Bearer <token>
function createToken(user){
    return jwt.sign(user, privateKey);
}

var validate = function (decodedToken, callback) {
    database(function(db){
        db.User.findOne({id: decodedToken.id}, function(err, user){
            db.close();
            if(!err && user != null){
                //user found, so they are authorized.
                return callback(err, true, user);
            }
            return callback(err, false, user);
        });
    });
};

module.exports = {
    validateFunc: validate,
    createToken: createToken,
    key: privateKey
};