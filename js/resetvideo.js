(function ($) {
  $(document).on('hidden.bs.modal', function (e) {
    
    var videoList = document.getElementsByTagName("video");
    
    jQuery.each(videoList, function(index, value)
    {
      alert("index", index, "value", value);
    });
    alert(object[type^="video/"]);
    var video7 = document.getElementById('footage7');
    video7.pause();
    video7.currentTime = 0;

    var video6 = document.getElementById('footage6');
    video6.pause();
    video6.currentTime = 0;
  });
})(jQuery);
