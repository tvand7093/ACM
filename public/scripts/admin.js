/// <reference path="../../typings/jquery/jquery.d.ts"/>
$(document).ready(function () {
	function createUser(){
		var data = {
			isAdmin: $('input[name="isAdmin"]').val(),
			username: $('input[name="username"]').val(),
			password: $('input[name="password"]').val(),
			email: $('input[name="email"]').val(),
			position: $('.position').val()
		};
		$.post("/admin/user/create", data,
			function(result){
				//add new user to view.
				if(result){
					//check if it is good or not.
					$('#addUserErrors').html('');
					//validation success, so add user to view
					$('#addUserErrors').addClass('hidden');
					$('#usersList').prepend(
						'<li>' +
							'<div class="col-md-4">' + data.username + '</div>'+
							'<div class="col-md-4">' + data.email + '</div>' +
							'<div class="col-md-4">' + data.isAdmin + '</div>' +
						'</li>');
				}
			}).fail(function(error){
				if(error){
					$('#addUserErrors').html('');
					$('#addUserErrors').removeClass('hidden');
					var matches = error.responseJSON.message.match(/\[(.*?)\]/);

					if (matches) {
					    $('#addUserErrors').append('<li style="color: red">*' + matches[1] + '</li>');
					}
				}
			});
	}
	$('.add-user').click(function(e){
		e.preventDefault();
		createUser();
	});
});