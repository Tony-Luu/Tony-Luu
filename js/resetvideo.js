$('.modal').on('hidden.bs.modal', function () {
    $('video').each(function() {
      this.player.pause();
    });
});
