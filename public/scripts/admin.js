/// <reference path="../../typings/jquery/jquery.d.ts"/>
$(document).ready(function () {
	function createUser(){
		var data = {
			isAdmin: $("input[name=isAdmin]").val(),
			username: $("input[name=username]").val(),
			password: $("input[name=password]").val(),
			email: $('input[name=email]').val()	
		};
		$.post("/user/create", data,
			function(result){
				//add new user to view.
				if(result != null){
					//check if it is good or not.
					$('#userErrors').html('');
					if(result.details){
						//means there was a Joi validation issue.
						$.each(result.details, function(errorMsg){
							//now add all errors to list.
							$('#userErrors').append('<li>' + errorMsg + '</li>');
						});
					}
					else{
						//validation success, so add user to view
						
					}
				}
			});
	}
});