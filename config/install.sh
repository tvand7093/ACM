#!/bin/bash

CURRENT=`pwd`
DESTINATION=$CURRENT/ACM
SAMPLE=$DESTINATION/config/config-sample.js
CONFIG=$DESTINATION/config/config.js
SOURCE=http://github.com/tvand7093/ACM.git
BRANCH=Hub

#clone repo first.
git clone $SOURCE

#move into repo and swtich to correct branch
cd $DESTINATION
git checkout $BRANCH

#make the config file.
cp $SAMPLE $CONFIG

#run install command.
echo '\n INFO =====> Installing project dependencies... \n'
npm install .

echo "\n NOTE =====> Before running, please update your $CONFIG file to use your own passwords/keys. \n"
