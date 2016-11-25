$('#checkIn').on('click', function () {
    var $btn = $(this).button('loading')
    // business logic...
    $btn.button('reset')
})

//document.getElementById("checkIn").onclick= function() {habilitar()};

//function habilitar() {
//    document.getElementById("checkOut").disabled = false;
//}

//window.onload = function () {
//    document.getElementById("#checkIn").onclick = function () {
//        document.getElementById("#checkOut").disabled = false;
//    }
//}

//$('#checkIn').on('click', function () {
//    $('#checkOut').prop("disabled", true);
//});