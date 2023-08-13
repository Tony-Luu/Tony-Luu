(function ($) {
  $(document).on('hidden.bs.modal', function (e) {
    var video = document.getElementById('footage' + e.target.id);
    video.pause();
    video.currentTime = 0;
  });
})(jQuery);
