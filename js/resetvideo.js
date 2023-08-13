(function ($) {
  $(document).on('hidden.bs.modal', function (e) {
    alert(e.target.id);
    var video = document.getElementById("footage");
    alert(document.getElementById("footage"));
    video.pause();
    video.currentTime = 0;
  });
})(jQuery);
