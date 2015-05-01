/// <reference path="../../typings/jquery/jquery.d.ts"/>
$(document).ready(function () {
	var url = window.location.href;
	
	if(url.endsWith('/')){
	    $('ul.nav > li').removeClass('active');
	    $('ul.nav > li > a[href="/"]').parent().addClass('active');
	}
	else if(url.endsWith('/officers')){
	    $('ul.nav > li').removeClass('active');
	    $('ul.nav > li > a[href="/officers"]').parent().addClass('active');
	}
	else if(url.endsWith('/documentation')){
	    $('ul.nav > li').removeClass('active');
	    $('ul.nav > li > a[href="/documentation"]').parent().addClass('active');
	}
	
	$('ul.nav > li').click(function (e) {
	    $('ul.nav > li').removeClass('active');
	    $(this).addClass('active');                
	});            
});