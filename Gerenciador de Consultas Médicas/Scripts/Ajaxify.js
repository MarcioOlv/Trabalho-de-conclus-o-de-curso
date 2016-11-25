$('#checkIn').on('click', function () {
    var $btn = $(this).button('loading')
    alert("teste");
    $btn.button('reset')
})