var mongoose = require('mongoose'),
	Schema = mongoose.Schema;

module.exports = mongoose.Schema({
	position: { type: String, required: true, index: { unique: true } },
	bio: { type: String },
	user: { type: Schema.Types.ObjectId, ref: 'Users' }
});

