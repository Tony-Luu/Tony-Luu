$('.modal').on('hidden.bs.modal', function () {
    $('footage').each(function() {
      this.player.pause();
    });
});
