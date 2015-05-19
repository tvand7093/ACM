var mongoose = require('mongoose');
var schemas = require('./schemas/schemas');

var db = {
  Officer: mongoose.model('Officers', schemas.officer),
  User: mongoose.model('Users', schemas.user)
};

function startDb(init){
    mongoose.connect('mongodb://localhost/acm');

    var connection = mongoose.connection;
    connection.on('error', console.error.bind(console, 'connection error:'));
    connection.once('open', function (callback) {
      // db opened
      init(db);
    });
};

module.exports.startDB = startDb;
module.exports.db = db;