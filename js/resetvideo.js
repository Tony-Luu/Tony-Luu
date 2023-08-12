$(document).ready(function() {
           $(document).on('hide.bs.modal', function (event) {
                 document.getElementById('footage').pause();
		 document.getElementById('footage').currentTime = 0;
           });
      });