function getOfficers(request, reply){
	reply([
        {name: "Tyler Vanderhoef", position: "President"},
        {name: "Kyle Nosar", position: "Vice President"},
        {name: "Sunnya Sisler", position: "Treasurer"},
        {name: "Jonathan Wambach", position: "Secretary"},
        {name: "Matt Burris", position: "Secretary (Outreach)"}
    ]);
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