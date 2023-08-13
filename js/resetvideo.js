$(document).on('show.bs.modal', function(event) { // modelId is dynamic
  var button = $(event.relatedTarget);
  var productid = button.data('target');
  console.log(productid)
});

$(document).on('hidden.bs.modal', function () {
    $('footage').each(function() {
      this.player.pause();
    });
});
