module.exports = {
	//THE KEY NAME TO STORE THE SIGNED IN USER'S COOKIE.
	sessionKey: 'currentUser',
	
	//CREDENTIALS FOR THE DEFAULT ADMIN ACCOUNT
	defaultAdmin: {
		username: 'Admin',
		email: 'admin@acmcoug.com',
        password: 'nyxCu2Kjn%wirTwuVEiVG&8JpqjTmeg'
	},
	
	//YAR PRIVATE KEY FOR SESSION STORAGE.
	yarPrivateKey: 'NJbk$CQgyBLqCt*BdEBiZXghPqAR49v',
	
	//THE PORT THE SERVER RUNS ON
	port: 8080,
	
	//THE KEY USED TO SIGN JWT'S
	jwtPrivateKey: 'BbZJjyoXAdr8BUZuiKKARWimKfrSmQ6fv8kZ7OFfc'
};