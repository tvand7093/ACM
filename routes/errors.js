var auth = require('./auth');

function loggedIn(request){
    var session = request.session.get("currentUser");
    return { 
        isLoggedIn: session != null,
        isAdmin: session == null ? false : session.isAdmin
      };
}

module.exports= [{
    method: 'GET',
    path: '/PageNotFound',
    config: {
        handler: function(request, reply){
        	reply.view("page_not_found", loggedIn(request));
        }
    }
},
{
    method: 'GET',
    path: '/NotAuthorized',
    config: {
        handler: function(request, reply){
        	reply.view("not_authorized", loggedIn(request));
        }
    }
},
{
    method: 'GET',
    path: '/ServerError',
    config: {
        handler: function(request, reply){
        	reply.view("server_error", loggedIn(request));
        }
    }
}];