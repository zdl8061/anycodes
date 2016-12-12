var tx = (function() {
  var readyCallback=function(){
    console.log("readyCallback");
  };
  var errorCallback = function(msg){
    console.log(msg);
  };

  function config(cfg){
      if(cfg.appid=="1234"){
        readyCallback();
      }
      else
      {
          errorCallback("asdf");

      }
  };

  function ready(fn){
      readyCallback = fn;
  };

 return {
   config:config,
   ready:ready

 }
})();

tx.config({
  appid:"12342"
});

tx.ready(function(){
    console.log("readyCallback2");
})
