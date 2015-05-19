var db = require ('../data/database').db;
var Joi = require('joi');
var auth = require('./auth');
var jwt = require('../config/token');
var config = require('../config/config');
var Admin = require('../controllers/admin_controller');
var Officers = require('../controllers/officers_controller');
var boom = require('boom');

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
    path: '/admin/officer/create',
    config: {
        validate: {
            payload: {
                email: Joi.string().min(4).required(),
                bio: Joi.string(),
                username: Joi.string().required(),
                position: Joi.string().required()
            }
        },
        handler: function(request, reply){
            jwt.processRequest(request, function(token){
                if(token == null){
                    reply.redirect('/NotAuthorized');
                }
                else{
                    Officers.create(request.payload, function(added){
                        if(added.error){
                            //error, so 404
                            return reply(boom.badRequest(added.error));
                        }
                        else{
                            reply(added);
                        }
                    });
                }
            });
        }
    }
},
{
    method: 'PUT',
    path: '/admin/officer/update',
    config: {
        validate: {
            payload: {
                id: Joi.string().length(24).required(),
                email: Joi.string().min(4).required(),
                bio: Joi.string(),
                username: Joi.string().required(),
                position: Joi.string().required()
            }
        },
        handler: function(request, reply){
            jwt.processRequest(request, function(token){
                if(token == null){
                    reply.redirect('/NotAuthorized');
                }
                else{
                    Officers.update(request.payload, function(updated){
                        if(updated.error){
                            //error, so 404
                            return reply(boom.badRequest(updated.error));
                        }
                        else{
                            reply(updated);
                        }
                    });
                }
            });
        }
    }
},
{
    method: 'DELETE',
    path: '/admin/officer/delete/{id}',
    config: {
        validate: {
            params: {
                id: Joi.string().length(24).required()
            }
        },
        handler: function(request, reply){
            jwt.processRequest(request, function(token){
                if(token == null){
                    reply.redirect('/NotAuthorized');
                }
                else{
                    Officers.remove(request.params.id, function(result){
                        if(result.error){
                            //error, so 404
                            return reply(boom.badRequest(result.error));
                        }
                        else{
                            reply(result);
                        }
                    });
                }
            });
        }
    }
}
];