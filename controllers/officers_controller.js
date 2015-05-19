var db = require('../data/database').db;

function getOfficersList(callback){
    db.Officer.find({ }).populate('user', '-password').exec(function(err, officers){
        if(!err){ 
            callback(officers);
        }
        else
            callback(null);
    });
}

function getPosition(position, callback){
    db.Officer.findOne({position: position}, { __v: 0 }).populate('user', '-password -isAdmin -__v').exec(function(err, pos){
        callback(pos);
    });
}

function getId(id, callback){
    db.Officer.findOne({_id: id}, { __v: 0 }).populate('user', '-password -isAdmin -__v').exec(function(err, pos){
        callback(pos);
    });
}

function create(objToAdd, callback){
    db.User.findOne({email: objToAdd.email}, function(error, user){
       if(user == null){
           //no user with that email, so add them.
           var toAdd = new db.User({
               username: objToAdd.username,
               email: objToAdd.email,
               imageUrl: "gets set in pre save.",
               isAdmin: false
           });
           toAdd.save(function(error, success){
               if(!error){
                   var newOfficer = new db.Officer({
                       position: objToAdd.position,
                       user: toAdd._id,
                       bio: objToAdd.bio
                   });
                   newOfficer.save(function(saveErr, success){
                       if(saveErr){
                           toAdd.remove(function(result){
                               callback({
                                   error: '[This position is already filled.]'
                               });
                           });
                           return;
                       }
                       var data = {
                           user: toAdd,
                           officer: newOfficer
                       };
                       callback(data);
                   });
                   return;
               }
               else{
                   toAdd.remove(function(result){
                       callback({
                           error: '[An officer with this email already exists.]'
                       });
                       return;
                   });
                   
               }
           });
       } 
       else{
           callback({
               error: "[An officer with this email already exists.]"
           });
       }

    });
}

function update(toUpdate, callback){
    getId(toUpdate.id, function(officer){
       if(officer){
           officer.user.username = toUpdate.username;
           officer.user.email = toUpdate.email;
           officer.user.save(function(error, result){
              if(error){
                  callback({
                      error: '[Updating the officer failed because the email is already in use.]'
                  });
                  return;
              } 
              else{
                  officer.bio = toUpdate.bio;
                  officer.position = toUpdate.position;
                  officer.save(function(err, saved){
                     if(err){
                         callback({
                            error: "[Updating the officer failed because the position is already filled.]" 
                         });
                     } 
                     else{
                         callback({
                             officer: saved,
                             user: result
                         });
                     }
                  });
              }
           });
       } 
       else{
           callback({
               error: "[An error occurred while getting the record.]"
           });
           return;
       }
    });
}

function remove(officerId, callback){
    db.Officer.findByIdAndRemove(officerId, function(error, result){
        if(error || (error == null && result == null)){
            callback({
                error: '[There was a problem deleting the record.]'
            });
            return;
        }
        else{
            db.User.findByIdAndRemove(result.user, function(err, user){
                if(err){
                    callback({
                        error: '[There was a problem deleting th user.]'
                    });
                    return;
                }
                else{
                    //success
                    callback({result: true});
                }
            });
        }
    });
}

function getCurrentHolders(callback){
    getPosition('President', function(pres){
        getPosition('Vice President', function(vp){
           getPosition('Treasurer', function(tres){
                getPosition('Secretary', function(sec){
                   getPosition('Secretary (Outreach)', function(out){
                      var valid = pres != null && vp != null && tres != null && sec != null && out != null;
                       
                      callback({
                        president: pres,
                        vicePresident: vp,
                        treasurer: tres,
                        secretary: sec,
                        outreach: out,
                        valid: valid == true
                      });
                   });
                });
           });
        });
    });
}

module.exports = {
	fetch: getOfficersList,
    remove: remove,
    create: create,
    update: update,
    fetchCabinet: getCurrentHolders
};