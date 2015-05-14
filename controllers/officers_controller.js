var db = require('../data/database').db;

function getOfficers(callback){
    db.Officer.find(function(err, officers){
        if(!err)
            callback(officers);
        else
            callback(null);
    });
}

module.exports = {
	fetch: getOfficers
};