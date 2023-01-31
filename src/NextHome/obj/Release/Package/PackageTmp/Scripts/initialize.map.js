function initializeMap(mapElemntId, lat, lng, info) {
	//add map, the type of map

	
		var mapOptions = {
			zoom: 12,
			draggable: true,
			scrollwheel: false,
			animation: google.maps.Animation.DROP,
			mapTypeId: google.maps.MapTypeId.ROADMAP,
			center: new google.maps.LatLng(lat, lng), // area location
			styles: [{ "stylers": [{ "saturation": -100 }, { "gamma": 1 }] }, { "elementType": "labels.text.stroke", "stylers": [{ "visibility": "off" }] }, { "featureType": "poi.business", "elementType": "labels.text", "stylers": [{ "visibility": "off" }] }, { "featureType": "poi.business", "elementType": "labels.icon", "stylers": [{ "visibility": "off" }] }, { "featureType": "poi.place_of_worship", "elementType": "labels.text", "stylers": [{ "visibility": "off" }] }, { "featureType": "poi.place_of_worship", "elementType": "labels.icon", "stylers": [{ "visibility": "off" }] }, { "featureType": "road", "elementType": "geometry", "stylers": [{ "visibility": "simplified" }] }, { "featureType": "water", "stylers": [{ "visibility": "on" }, { "saturation": 50 }, { "gamma": 0 }, { "hue": "#50a5d1" }] }, { "featureType": "administrative.neighborhood", "elementType": "labels.text.fill", "stylers": [{ "color": "#c5c5c5" }] }, { "featureType": "road.local", "elementType": "labels.text", "stylers": [{ "weight": 0.5 }, { "color": "#ff0000" }] }, { "featureType": "transit.station", "elementType": "labels.icon", "stylers": [{ "gamma": 1 }, { "saturation": 50 }] }]
		};

		var mapElement = document.getElementById(mapElemntId);
	    var map = new google.maps.Map(mapElement, mapOptions);

	var marker, i;

	//declare infowindow

	var infowindow = new google.maps.InfoWindow();

	//add marker to each locations

		marker = new google.maps.Marker({
			position: new google.maps.LatLng(lat, lng),
			map: map,
			icon: '../../images/map_marker.png'
		});
		//click function to marker, pops up infowindow
		google.maps.event.addListener(marker, 'click', (function (marker, i)
		{
			return function () {
                infowindow.setContent('<table><tr><td><div class"info-window"><div class="image-label"><img class="img-responsive" src="' + info.imgUrl + '" alt="' + info.name + '"></div></td><td style="vertical-align: top;"><div class="map-detail"><a href="#"><h4>' + info.title + '</h4></a><p style="float">' + info.subtitle +'</p></div></div></td></tr></table>');
				infowindow.open(map, marker);
			}
		})
		(marker, i));
}
