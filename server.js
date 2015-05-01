/// <reference path="typings/node/node.d.ts"/>
var Hapi = require('hapi');
var Joi = require('joi');
var Routes = require('./routes/routes');

var server = new Hapi.Server();

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


server.start(function () {
    console.log('Server running at:', server.info.uri);
});
