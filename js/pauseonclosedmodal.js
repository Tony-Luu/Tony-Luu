$(document).ready(function(){
    $('.modal').on('hidden.bs.modal', function() { //show modal event for an element which has class 'modal'
		var id = $(this).attr('id');//saves in the var the ID value of the closed modal
		var video = document.getElementById(id).querySelectorAll("footage");//Find the element 'video' inside of the modal defined by the ID previosly saved

        $(video)[0].pause(); //pauses the video
        $(video)[0].currentTime = 0; //rests the video to 0 for have it from the beging when the user opens the modal again
	});
});
