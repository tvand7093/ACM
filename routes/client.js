var database = require ('../data/database');

function index(request, reply){
	reply.view("index");
}

function officers(request, reply){
    database(function(db){
        var newOfficer = new db.Officer({
            name: 'Tyler Vanderhoef',
            position: 'President',
            bio: 'Tyler is a cool person!'
        });
        newOfficer.save();
        var list = db.Officer.find();
    	reply.view("officers", newOfficer);
    });

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