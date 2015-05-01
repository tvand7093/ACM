var mongoose = require('mongoose');
var schemas = require('./schemas/schemas');

module.exports = function(success){
  mongoose.connect('mongodb://localhost/acm');

  var db = mongoose.connection;
  db.on('error', console.error.bind(console, 'connection error:'));
  db.once('open', function (callback) {
    // db opened
  
    var Officer = mongoose.model('Officers', schemas.officer);
    success({
      Officer: Officer
    });
  });
};