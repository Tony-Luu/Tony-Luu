$(document).ready(function(){
	$('#body').on('hidden.bs.modal', '.modal', function () {
		$('#footage').trigger('pause');
	});
});
