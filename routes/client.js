var database = require ('../data/database');
var Joi = require('joi');
var auth = require('./auth');
var token = require('../config/token');

function loggedIn(request){
    var session = request.session.get("currentUser");
    return { 
        isLoggedIn: session != null,
        isAdmin: session == null ? false : session.isAdmin
      };
}

function login(request, reply){
    auth(request.payload.email, request.payload.password, 
        function(isValid, auth){
            var jwt = '';
            if(isValid){
                jwt = token.createToken(auth);
                request.session.set("currentUser", auth);
            }

            jwt == '' ? reply.redirect('/').header('Authorization', 'Bearer ' + jwt) :
                reply.redirect('/');
        });
}

function addAuth(request){
    var auth = 'Bearer ' + request.auth.credentials;
    return auth;
}

module.exports= [{
    method: 'GET',
    path: '/',
    config: {
        handler: function(request, reply){
            
        	reply.view("index", loggedIn(request))
                .header('Authorization', addAuth(request));
        }
    }
},
{
    method: 'GET',
    path: '/officers',
    config: {
        handler: function(request, reply){
            reply.view("officers", loggedIn(request));
        }
    }
},
{
    method: 'POST',
    path: '/logout',
    config: {
        handler: function(request, reply){
            request.session.clear("currentUser");
            request.session.reset();
            reply.redirect('/');
        }
    }
},
{
    method: 'POST',
    path: '/login',
    config: {
        handler: login,
        validate: {
            payload: {
                email: Joi.string().min(4).required(),
                password: Joi.string().min(8).required()
            }
        }
    }
}];