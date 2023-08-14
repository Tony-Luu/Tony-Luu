(function ($) {
  $(document).on('hidden.bs.modal', function (e) {
    var video = document.getElementById('footage');
    video.pause();
    video.currentTime = 0;
  });
})(jQuery);
