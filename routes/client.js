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

function login(request, reply){
    auth(request.payload.email, request.payload.password, 
        function(isValid, auth){
            if(isValid)
                request.session.set("currentUser", auth);
            reply.redirect('/');
        });
}

module.exports= [{
    method: 'GET',
    path: '/',
    config: {
        handler: function(request, reply){
        	reply.view("index", loggedIn(request));
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