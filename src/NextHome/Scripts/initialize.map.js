function initializeMap(mapElemntId, locations) {
	//add map, the type of map

    var domainUrl = 'http://nexthome-egy.com';
    var bounds = new google.maps.LatLngBounds();
		var mapOptions = {
			zoom: 12,
			draggable: true,
			scrollwheel: false,
			animation: google.maps.Animation.DROP,
			mapTypeId: google.maps.MapTypeId.ROADMAP,
			//center: new google.maps.LatLng(locations[0].lat, locations[0].lng), // area location
			//styles: [{ "stylers": [{ "saturation": -100 }, { "gamma": 1 }] }, { "elementType": "labels.text.stroke", "stylers": [{ "visibility": "off" }] }, { "featureType": "poi.business", "elementType": "labels.text", "stylers": [{ "visibility": "off" }] }, { "featureType": "poi.business", "elementType": "labels.icon", "stylers": [{ "visibility": "off" }] }, { "featureType": "poi.place_of_worship", "elementType": "labels.text", "stylers": [{ "visibility": "off" }] }, { "featureType": "poi.place_of_worship", "elementType": "labels.icon", "stylers": [{ "visibility": "off" }] }, { "featureType": "road", "elementType": "geometry", "stylers": [{ "visibility": "simplified" }] }, { "featureType": "water", "stylers": [{ "visibility": "on" }, { "saturation": 50 }, { "gamma": 0 }, { "hue": "#50a5d1" }] }, { "featureType": "administrative.neighborhood", "elementType": "labels.text.fill", "stylers": [{ "color": "#c5c5c5" }] }, { "featureType": "road.local", "elementType": "labels.text", "stylers": [{ "weight": 0.5 }, { "color": "#ff0000" }] }, { "featureType": "transit.station", "elementType": "labels.icon", "stylers": [{ "gamma": 1 }, { "saturation": 50 }] }]
		};

		var mapElement = document.getElementById(mapElemntId);
	    var map = new google.maps.Map(mapElement, mapOptions);
            map.setTilt(45);
	
         var marker;

	//declare infowindow

	var infowindow = new google.maps.InfoWindow();

    //add marker to each locations
    for (var i = 0; i < locations.length; i++) {
        var position = new google.maps.LatLng(locations[i].lat, locations[i].lng);
        bounds.extend(position);
        marker = new google.maps.Marker({
            position: position,
            map: map,
            icon: (locations[i].markerUrl != null && locations[i].markerUrl != '') ? locations[i].markerUrl : domainUrl + '/images/map_marker.png'
        });
        //click function to marker, pops up infowindow
        google.maps.event.addListener(marker, 'click', (function (marker, i) {
            return function () {
                infowindow.setContent('<table><tr><td><div class="info-window"><div class="image-label"><img class="img-responsive" src="' + locations[i].imgUrl + '" alt="' + locations[i].title + '"></div></td><td style="vertical-align: top;"><div class="map-detail"><a href="#"><h4>' + locations[i].title + '</h4></a><p style="float">' + locations[i].subtitle + '</p></div></div></td></tr></table>');
                infowindow.open(map, marker);
            }
        })(marker, i));

    }

        // Automatically center the map fitting all markers on the screen
        map.fitBounds(bounds);

    // Override our map zoom level once our fitBounds function runs (Make sure it only runs once)
    var boundsListener = google.maps.event.addListener((map), 'bounds_changed', function (event) {
        this.setZoom(12);
        google.maps.event.removeListener(boundsListener);
    });
}
