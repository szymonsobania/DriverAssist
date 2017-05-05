$(function () {

    function initMap() {


        var routeCoordinates = [
          { lat: 50.26489189999999, lng: 19.02378150000004 },
          { lat: 50.48774299999999, lng: 19.416625899999985 },
		 
        ];

        var map = new google.maps.Map(document.getElementById('map'), {
            zoom: 7,
            center: routeCoordinates[3],
            mapTypeId: 'terrain'
        });
        var measurmentsPath = new google.maps.Polyline({
            path: routeCoordinates,
            geodesic: true,
            strokeColor: '#FFFFFF',
            strokeOpacity: 1.0,
            strokeWeight: 2
        });

        measurmentsPath.setMap(map);
    }

    google.maps.event.addDomListener(window, 'load', initMap);


});