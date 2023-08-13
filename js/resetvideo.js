(function ($) {
  $(document).on('hidden.bs.modal', function (e) {
    alert(e.target.id);
    var video = document.getElementById('footage' + e.target.id);
    alert(document.getElementById('footage' + e.target.id));
    video.pause();
    video.currentTime = 0;
  });
})(jQuery);
