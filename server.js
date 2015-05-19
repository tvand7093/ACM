/// <reference path="typings/node/node.d.ts"/>
var Hapi = require('hapi');
var Joi = require('joi');
var Routes = require('./routes/routes');
var database = require ('./data/database');
var server = new Hapi.Server();
var token = require('./config/token');
var config = require('./config/config');

server.connection({ port: config.port });

// Load up the swagger plugin for documentation
server.register({
        register: require('hapi-swagger'),
    }, function (err) {
        if (err) {
            server.log(['error'], 'hapi-swagger load error: ' + err);
        }else{
            server.log(['start'], 'hapi-swagger interface loaded');
        }
    });
    
//configure yar
server.register({
    register: require('yar'),
    options: {
        cookieOptions: {
            password: config.yarPrivateKey,
            isSecure: false
        }
    },

}, function (err) { });

server.route(Routes);

server.views({
    engines: {
        jade: require("jade")
    },
    path: "./public/views",
    compileOptions: {
        pretty: true,
        compileDebug: true
    }
});

//ensures that the admin account is created prior to running the website.
function ensureAdmin(){
    database.startDB(function(db){
        db.User.findOne({isAdmin: true, email: config.defaultAdmin.email}, function(err, result){
          //look for admin, if not found, create!
          if(result == null){
            var admin = new db.User({
              username: config.defaultAdmin.username,
              isAdmin: true,
              imageUrl: "<no image>",
              email: config.defaultAdmin.email,
              password: config.defaultAdmin.password
            });
            admin.save(function(error){
              if(error)
                console.log("Error creating Admin account on server.");
              else
                console.log("FIRST RUN: Sucessfully added Admin account!");
            });
          }
        });
    });
}

server.start(function () {
    ensureAdmin();
    console.log('Server running at:', server.info.uri);
});
