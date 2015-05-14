var db = require ('../data/database').db;
var Joi = require('joi');

function getUsers(callback) {
    db.User.find({isSysAdmin: false}, { password: 0 }, function(err, user){
        if(!err)
            callback(user);
        else
            callback(null);
    });
}

function create(objToAdd, callback){
    db.User.findOne({email: objToAdd.email}, function(error, user){
       if(user == null){
           //no user with that email, so add them.
           var toAdd = new db.User({
               username: objToAdd.username,
               email: objToAdd.email,
               isAdmin: objToAdd.isAdmin,
               password: objToAdd.password,
               isSysAdmin: false,
               enabled: true
           });
           toAdd.save(function(error, success){
               var newOfficer = new db.Officer({
                   position: objToAdd.position,
                   userId: toAdd._id
               });
               newOfficer.save(function(saveErr, success){
                   var data = {
                       user: toAdd,
                       officer: newOfficer
                   };
                   callback(data);
               });
           });
       } 
       else{
           callback({
               message: "[User already exists.]"
           });
       }

    });
}

module.exports = {
    create: create,
    fetch: getUsers
};