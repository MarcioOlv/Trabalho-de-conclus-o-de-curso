function initialize() {
    directionsDisplay = new google.maps.DirectionsRenderer();
    var latlng = new google.maps.LatLng(-18.8800397, -47.05878999999999);

    var options = {
        zoom: 15,
        center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    map = new google.maps.Map(document.getElementById("mapa"), options);
    directionsDisplay.setMap(map);

    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {

            pontoPadrao = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
            map.setCenter(pontoPadrao);

            var geocoder = new google.maps.Geocoder();

            geocoder.geocode({
                "location": new google.maps.LatLng(position.coords.latitude, position.coords.longitude)
            },
            function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    $("mapa").val(results[0].formatted_address);
                }
            });
        });
    }
}

function abreLink() {
    window.open('http://www.google.com.br');
}


function chamaMarcacaoEndereco(endereco, bairro, cidade) {
    //colocando o endereco no padrao correto

    var end = endereco + ", " + bairro + " - " + cidade;
    //var end = "Av. Humberto de Alencar Castelo Branco, 3972, Assuncao - Sao bernardo do campo";
    //window.alert(endereco);
    //var endereco = "Rua Itaperuna, 9, Jardim Thelma - São Bernardo do Campo"
    //MUDANDO O ZOOM DO MAPA
    map.setZoom(19);
    //Buscando lat e log por endereco (no formato: nome rua, numero, bairro - cidade)
    var geocoder = new google.maps.Geocoder();
    geocoder.geocode({ 'address': end }, function (results, status) {
        //se o retorno de status for ok
        if (status = google.maps.GeocoderStatus.OK) {
            //pega o retorno de result, que sao a latitude e longitude
            var latlng = results[0].geometry.location;
            //faz marcacao no mapa
            var marker = new google.maps.Marker({ position: latlng, map: map });
            map.setCenter(latlng);//leva o mapa para a posicao da marcacao
        }
    });
}

