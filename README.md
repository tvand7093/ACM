# ACM Hub

This is the main ACM website. It also provides the API used by the official ACM mobile application.

## Setup Instructions

- Install [Node.js](https://nodejs.org/)
	- Install [Npm](https://www.npmjs.com/)
	- NOTE: On some machines you need to create a symlink to the node application. 
		You will know if this is the case because running the node command will not start
		the server. If you have this issue, running the following command:
		`sudo ln -s /usr/bin/nodejs /usr/bin/node`
		
- Install [Mongodb](https://www.mongodb.org/)
- Install the website using: 
```sh 
	curl https://raw.githubusercontent.com/tvand7093/ACM/Hub/config/install.sh | sh
```
- Update the /config/config.js file to use your own private keys prior to running for the first time. 

- Run the website using: `node .`
