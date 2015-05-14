var db = require ('../data/database').db;
var Joi = require('joi');
var auth = require('./auth');
var jwt = require('../config/token');
var config = require('../config/config');
var Admin = require('../controllers/admin_controller');
var Users = require('../controllers/users_controller');

function loggedIn(token){
    return { 
        isLoggedIn: token != null
    };
}

module.exports= [{
    method: 'GET',
    path: '/admin',
    config: {
        handler: function(request, reply){
            jwt.processRequest(request, function(profile){
                if(!profile){
                    reply.redirect('/NotAuthorized');
                }
                else{
                    console.log(profile);
                    Admin.fetch(profile.id, function(settings){
                        if(settings == null){
                            reply.redirect('/PageNotFound');
                        }
                        else {
                            var toSend = loggedIn(profile);
                            toSend.settings = settings;
                            reply.view('./admin/admin', toSend);
                        }
                    });
                }
            });
        }
    }
},
{
    method: 'POST',
    path: '/admin/user/create',
    config: {
        validate: {
            payload: {
                email: Joi.string().min(4).required(),
                password: Joi.string().min(8).required(),
                username: Joi.string().required(),
                isAdmin: Joi.boolean().required(),
                position: Joi.string().required()
            }
        },
        handler: function(request, reply){
            jwt.processRequest(request, function(token){
                if(token == null){
                    reply.redirect('/NotAuthorized');
                }
                else{
                    Users.create(request.payload, function(added){
                        reply(added);
                    });
                }
            });
        }
    }
}];