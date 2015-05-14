var database = require ('../data/database');
var Joi = require('joi');
var auth = require('./auth');
var jwt = require('../config/token');

function loggedIn(token){
    return { 
        isLoggedIn: token != null
      };
}

function login(request, reply){
    auth(request, request.payload.email, request.payload.password, 
        function(token){
            reply.redirect('/');
        });
}

module.exports= [{
    method: 'GET',
    path: '/',
    config: {
        handler: function(request, reply){
            jwt.processRequest(request, function(token){
                reply.view("index", loggedIn(token));
            });
        }
    }
},
{
    method: 'GET',
    path: '/officers',
    config: {
        handler: function(request, reply){
            jwt.processRequest(request, function(token){
                reply.view("officers", loggedIn(token));
            });
            
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