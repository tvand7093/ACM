var Client = require('./client');
var Api = require('./api');
var Resources = require('./resources');
var Admin = require('./admin');
var Errors = require('./errors');

module.exports = [].concat(Client, Api, Admin, Errors, Resources);