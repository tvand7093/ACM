
function index(request, reply){
	reply.view("index");
}

function officers(request, reply){
	reply.view("officers");
}

var Types = require('hapi').types;
module.exports= [{
    method: 'GET',
    path: '/',
    config: {
        handler: index
    }
},
{
    method: 'GET',
    path: '/officers',
    config: {
        handler: officers
    }
}];