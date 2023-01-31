jQuery(function ($) {
    "use strict";
    function initialize() {
        //map options
        //add map, the type of map
        var mapOptions = {
            zoom: 14,
            draggable: true,
            scrollwheel: false,
            animation: google.maps.Animation.DROP,
            mapTypeId: google.maps.MapTypeId.ROADMAP,
            center: new google.maps.LatLng(29.986722,31.516735), // area location
            styles: [{ "stylers": [{ "saturation": -100 }, { "gamma": 1 }] }, { "elementType": "labels.text.stroke", "stylers": [{ "visibility": "off" }] }, { "featureType": "poi.business", "elementType": "labels.text", "stylers": [{ "visibility": "off" }] }, { "featureType": "poi.business", "elementType": "labels.icon", "stylers": [{ "visibility": "off" }] }, { "featureType": "poi.place_of_worship", "elementType": "labels.text", "stylers": [{ "visibility": "off" }] }, { "featureType": "poi.place_of_worship", "elementType": "labels.icon", "stylers": [{ "visibility": "off" }] }, { "featureType": "road", "elementType": "geometry", "stylers": [{ "visibility": "simplified" }] }, { "featureType": "water", "stylers": [{ "visibility": "on" }, { "saturation": 50 }, { "gamma": 0 }, { "hue": "#50a5d1" }] }, { "featureType": "administrative.neighborhood", "elementType": "labels.text.fill", "stylers": [{ "color": "#c5c5c5" }] }, { "featureType": "road.local", "elementType": "labels.text", "stylers": [{ "weight": 0.5 }, { "color": "#ff0000" }] }, { "featureType": "transit.station", "elementType": "labels.icon", "stylers": [{ "gamma": 1 }, { "saturation": 50 }] }]
        };
        //map canvas
        var mapElement = document.getElementById('map_canvas');
        var map = new google.maps.Map(mapElement, mapOptions);

        //add locations boxes
        var locations = [
            ['<div class"info-window"><div class="image-label"><img class="img-responsive" src="/images/projects/NextLife/map_image-240-120.jpg" alt="NextLife"><label>Available</label></div><div class="map-detail"><a href="#"><h4>NextLife</h4></a><p>North Mokattam</p><span>Units:435</span><span> Available:54</span><span> Sq Ft:1200</span></div></div>', 29.986722,31.516735, '../../images/map_marker.png']
        ];

        //declare marker call it 'i'
        var marker, i;
        //declare infowindow
        var infowindow = new google.maps.InfoWindow();
        //add marker to each locations
        //project nextlife
        for (i = 0; i < locations.length; i++) {
            marker = new google.maps.Marker({
                position: new google.maps.LatLng(locations[i][1], locations[i][2]),
                map: map,
                icon: locations[i][3]
            });
            //click function to marker, pops up infowindow
            google.maps.event.addListener(marker, 'click', (function (marker, i) {
                return function () {
                    infowindow.setContent(locations[i][0]);
                    infowindow.open(map, marker);
                }
            })(marker, i));
        }

    }
    google.maps.event.addDomListener(window, 'load', initialize);
});