module.exports = {
	//THE KEY NAME TO STORE THE SIGNED IN USER'S COOKIE.
	sessionKey: 'currentUser',
	
	//CREDENTIALS FOR THE DEFAULT ADMIN ACCOUNT
	defaultAdmin: {
		username: 'Admin',
		email: 'admin@acmcoug.com',
        password: '<Your admin password here>'
	},
	
	//YAR PRIVATE KEY FOR SESSION STORAGE.
	yarPrivateKey: '<Your session encryption private key here>',
	
	//THE PORT THE SERVER RUNS ON
	port: 8080,
	
	//THE KEY USED TO SIGN JWT'S
	jwtPrivateKey: '<Your JWT signing private key here>'
};