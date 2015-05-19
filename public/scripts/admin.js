/// <reference path="../../typings/jquery/jquery.d.ts"/>
$(document).ready(function () {
	function resetInputs(){
		$('input[name="username"]').val('');
		$('input[name="email"]').val('');
		$('textarea').val('');
		$("select option:selected").prop("selected", false);
		$("select option:first").prop("selected", "selected");
		editingId = null;
		$('tr.highlight').removeClass('highlight');
	}
	
	function createOrEditUser(){
		var data = {
			username: $('input[name="username"]').val(),
			bio: $('textarea').val(),
			email: $('input[name="email"]').val(),
			position: $('.position').val()
		};
		
		if(editingId){
			data.id = editingId;
			sendData('/admin/officer/update', 'PUT', data, editSuccess, fail);
		}
		else{
			sendData('/admin/officer/create', 'POST', data, addSuccess, fail);
		}
	}
	
	function fail(error){
		if(error){
			$('#addUserErrors').html('');
			$('#addUserErrors').removeClass('hidden');
			var matches = error.responseJSON.message.match(/\[(.*?)\]/);

			if (matches) {
			    $('#addUserErrors').append('<li style="color: red">*' + matches[1] + '</li>');
			}
		}
	}
	
	function success(result){
		if(result){
			//check if it is good or not.
			$('#addUserErrors').html('');
			//validation success, so add user to view
			$('#addUserErrors').addClass('hidden');
			resetInputs();
		}
	}
	
	function editSuccess(result){
		var cols = $('#' + result.officer._id).find('td');

		cols.eq(0).html(result.user.username);
		cols.eq(1).html(result.officer.position);
		cols.eq(2).html(result.user.email);
		cols.eq(3).html(result.officer.bio);
		success(result);
	}
	
	function beginEdit(id){
		var row = $('#' + id);
		var cols = row.find('td');
		
		$('input[name="username"]').val(cols.eq(0).text());
		$("select option:selected").prop("selected", false);
		$("select").val(cols.eq(1).text());
		$('input[name="email"]').val(cols.eq(2).text());
		$('textarea').val(cols.eq(3).text());
	}
	
	function addSuccess(result){
		//add new user to view.
		if(result){
			$('tbody').append(
				'<tr class="hover" id="' + result.officer._id + '">' +
					'<td role="row">' + result.user.username + '</td>'+
					'<td>' + result.officer.position + '</td>' +
					'<td>' + result.user.email + '</td>' +
					'<td>' + result.officer.bio + '</td>' +
					'<td><a class="delete-user" href="#"><span class="glyphicon glyphicon-trash"></span></a></td>' +
				'</tr>');
		}
		success(result);
	}
	
	function sendData(url, method, data, success, failure){
		$.ajax({
	        url: url,
	        type: method,
			data: data,
	        success: success,
	        error: failure
	    });
	}
	
	function deleteUser(userId){
		$.ajax({
	        url: '/admin/officer/delete/' + userId,
	        type: 'DELETE',
	        success: function(success){
				//remove item from view if success.
				$('#' + userId).remove();
				deleteInProgress = false;
			},
	        error: function(error){
				$('#addUserErrors').html('');
				$('#addUserErrors').removeClass('hidden');
				var matches = error.responseJSON.message.match(/\[(.*?)\]/);

				if (matches) {
				    $('#addUserErrors').append('<li style="color: red">*' + matches[1] + '</li>');
				}
				deleteInProgress = false;
			}
	    });
	}
	
	var editingId = null;
	$('tbody').on('click', 'tr', function(e){
		if(!deleteInProgress){
			resetInputs(true);
			$(this).addClass('highlight');
			editingId = $(this).prop('id');
			beginEdit(editingId);
		}
	});
	
	var deleteInProgress = false;
	$('tbody').on('click', 'a', function(e){
		e.preventDefault();
		var officerId = $(this).closest('tr').prop('id');
		deleteInProgress = true;
		deleteUser(officerId);
	});
	
	$('.add-user').click(function(e){
		e.preventDefault();
		createOrEditUser();
	});
	
	$('.clear-input').click(function(e){
		e.preventDefault();
		resetInputs();
	});
});