/*
* @Author: Marte
* @Date:   2016-12-12 10:41:49
* @Last Modified by:   Marte
* @Last Modified time: 2016-12-24 17:10:10
*/
(function() {
    'use strict';

    var doT = {
		version: '1.0.0',
		templateSettings: { /*...*/ },
		template: undefined, //fn, compile template
		compile:  undefined  //fn, for express
	};

	if (typeof module !== 'undefined' && module.exports) {
		module.exports = doT;
	} else if (typeof define === 'function' && define.amd) {
		define(function(){return doT;});
	} else {
		(function(){ return this || (0,eval)('this'); }()).doT = doT;
	}

})()
