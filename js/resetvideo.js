(function ($) {
  $(document).on('hidden.bs.modal', function (e) {
    alert(e.target.id);
    var video = document.getElementById('footage');
    var videolist = document.getElementsByTagName('footage');
    for(var i = 0; i < videolist.length; i++)
    {
      alert(document.getElementsByTagName("footage")[i]);
        document.getElementsByTagName("footage")[i].pause();
        document.getElementsByTagName("footage")[i].currentTime = 0;
    }    
  });
})(jQuery);
