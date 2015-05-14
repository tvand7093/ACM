var mongoose = require('mongoose');

module.exports = mongoose.Schema({
	position: { type: String, required: true },
	bio: { type: String },
	userId: { type: String, required: true }
});

