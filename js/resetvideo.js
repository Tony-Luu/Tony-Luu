$(function(){
    $('#portfolioModal7').modal({
        show: false
    }).on('hidden.bs.modal', function(){
        $(this).find('footage')[0].pause();
    });
});
