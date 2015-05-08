/// <reference path="typings/node/node.d.ts"/>
var Hapi = require('hapi');
var Joi = require('joi');
var Routes = require('./routes/routes');
var database = require ('./data/database');
var server = new Hapi.Server();
var Yar =
server.connection({ port: 8080 });

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
            password: 'NJbk$CQgyBLqCt*BdEBiZXghPqAR49v',
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
        pretty: true
    }
});

//ensures that the admin account is created prior to running the website.
function ensureAdmin(){
    database(function(db){
        db.User.findOne({enabled: true, isSysAdmin: true, isAdmin: true, username: "Admin"}, function(err, result){
          //look for admin, if not found, create!
          if(result == null){
            var admin = new db.User({
              username: "Admin",
              enabled: true,
              isSysAdmin: true,
              isAdmin: true,
              email: "admin@acmcoug.com",
              password: "nyxCu2Kjn%wirTwuVEiVG&8JpqjTmeg"
            });
            admin.save(function(error){
              if(error)
                console.log("Error creating Admin account on server.");
              else
                console.log("FIRST RUN: Sucessfully added Admin account!");
              db.close();
            });
          }
          else{
              db.close();
          }
        });
    });
}

server.start(function () {
    ensureAdmin();
    console.log('Server running at:', server.info.uri);
});
