var mongoose = require('mongoose');

module.exports = mongoose.Schema({
	position: { type: String, required: true },
	bio: { type: String, required: true },
	userId: { type: String, required: true }
});

