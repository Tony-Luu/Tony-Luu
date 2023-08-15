(function ($) {
  $(document).on('hide.bs.modal', function (e) {    
    var videoList = document.getElementsByTagName("video");
    for(var i = 0; i < videoList.length; i++)
    {
      videoList[i].pause();
      videoList[i].currentTime = 0;
    }
  });
})(jQuery);
