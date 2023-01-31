
//var map;
// map = new GMaps({
//     el: '#map',
//     lat: 29.9960477,
//     lng: 31.2897642,
//     scrollwheel: false
// });

// map.addMarker({
//     lat: 21.2334329,
//     lng: 72.86372,
//     title: 'Marker with InfoWindow',
//     infoWindow: {
//         content: '<p>Idea Homes Melbourne, Merrick Way, <br>FL 12345 Australia<a target="_blank">Themeforest</a></p>'
//     }
// });


$(document).ready(function () {
    "use strict";

    $(".language").click(function (e) {
        e.preventDefault();

        var pathname = window.location.href; // Returns path only
        var newLanguagePath = window.location.href;
        //console.log("Original path => " + pathname);
        if (pathname.includes("/ar")) {
            console.log("Changing to English...");
            newLanguagePath = pathname.replace("/ar", "/en");
        } else {
            console.log("Changing to Arabic...");
            newLanguagePath = pathname.replace("/en", "/ar");
        }
        //console.log("New path => " + newLanguagePath);
        window.location.href = newLanguagePath;
    });

});