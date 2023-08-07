$(document).ready(function(){
	/* Get iframe src attribute value i.e. YouTube video url
	    and store it in a variable */
	    var url = $("#footage").attr('src');
	    
	    /* Assign empty url value to the iframe src attribute when
	    modal hide, which stop the video playing */
	    $("#container").on('hide.bs.modal', function(){
	        $("#footage").attr('src', '');
	    });
	    
	    /* Assign the initially stored url back to the iframe src
	    attribute when modal is displayed again */
	    $("#container").on('show.bs.modal', function(){
	        $("#footage").attr('src', url);
	    });
	});
});
