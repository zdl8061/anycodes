webpackJsonp([0],[
/* 0 */
/***/ function(module, exports, __webpack_require__) {

	__webpack_require__(!(function webpackMissingModule() { var e = new Error("Cannot find module \"./main.css\""); e.code = 'MODULE_NOT_FOUND'; throw e; }()));
	var $ = __webpack_require__(1);
	var sub = __webpack_require__(2);
	var app  = document.createElement('div');
	app.innerHTML = '<h1>Hello World</h1>';
	app.appendChild(sub());
	document.body.appendChild(app);
	$("body").append('jQuery');



/***/ },
/* 1 */,
/* 2 */
/***/ function(module, exports) {

	function generateText() {
	  var element = document.createElement('h2');
	  element.innerHTML = "Hello h2 world";
	  return element;
	}

	module.exports = generateText;


/***/ }
]);