var jwt = require('jsonwebtoken'),
    database = require('../data/database'),
    config = require('./config');

function createToken(user){
    return jwt.sign(user, config.jwtPrivateKey);
}

function decode(token, callback){
    jwt.verify(token, config.jwtPrivateKey, function(err, decoded) {
        if(err) callback(null);
        else callback(decoded);
    });
}

function ensureLoggedIn(request, callback){
    var session = request.session.get(config.sessionKey);
    
    if(!session) {
        callback(null);
        return;
    } 
    
    decode(session, function(token){
        callback(token);
    });
}

module.exports = {
    encode: createToken,
    decode: decode,
    processRequest: ensureLoggedIn
};