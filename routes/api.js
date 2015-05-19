var Officers = require('../controllers/officers_controller');

function getOfficers(request, reply){
    Officers.fetchCabinet(function(cabinet){
        reply(cabinet);
    });
}

module.exports= [{
    method: 'GET',
    path: '/api/officers',
    config: {
        handler: getOfficers,
        description: 'Get all current officers',
        notes: 'Returns a list of all current officers.',
        tags: ['api']
    }
}];