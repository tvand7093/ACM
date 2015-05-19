/// <reference path="../../typings/jquery/jquery.d.ts"/>
$(document).ready(function () {
	var url = window.location.href;
	
	if(url.endsWith('/')){
	    $('#main-navbar > li').removeClass('active');
	    $('#main-navbar > li > a[href="/"]').parent().addClass('active');
	}
	else if(url.endsWith('/officers')){
	    $('#main-navbar > li').removeClass('active');
	    $('#main-navbar > li > a[href="/officers"]').parent().addClass('active');
	}
	else if(url.endsWith('/documentation')){
	    $('#main-navbar > li').removeClass('active');
	    $('#main-navbar > li > a[href="/documentation"]').parent().addClass('active');
	}
	else if(url.endsWith('/admin')){
	    $('#main-navbar > li').removeClass('active');
	    $('#main-navbar > li > a[href="/admin"]').parent().addClass('active');
	}
	
	$('#main-navbar > li').click(function (e) {
	    $('#main-navbar > li').removeClass('active');
	    $(this).addClass('active');                
	});            
});