var lat, lng;
map.addEventListener('mousemove', function (ev) {
    lat = ev.latlng.lat;
    lng = ev.latlng.lng;
    console.log("lat", lat);
    console.log("lng", lng);
});

console.log("id", markerId);

fetch("/Map/GetMarkers", {
    method: "get",
    headers: {
        'Content-Type': "application/json"
    },
}).then(r => r.json()).then(markers => {
    markers.forEach(m => {
        if (m.id == markerId) {
            map.setView([m.latitude, m.longitude], 10);
            var redIcon = new L.Icon({
                iconUrl: 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-red.png',
                shadowUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/0.7.7/images/marker-shadow.png',
                iconSize: [25, 41],
                iconAnchor: [12, 41],
                popupAnchor: [1, -34],
                shadowSize: [41, 41]
            });
            var marker = L.marker([m.latitude, m.longitude], { title: m.title, draggable: 'true', icon: redIcon }).addTo(map).on('click', function (e) {
                console.log("drag marker #", m.id);
                //console.log("marker", e.target.options);
               
            }).bindPopup(m.title)
                .on('dragend', function (event) {
                var position = marker.getLatLng();
                marker.setLatLng(position, {
                    draggable: 'true'
                }).bindPopup(position).update();
                var latFixed = position.lat.toString().replace(".", ",");
                var lonFixed = position.lng.toString().replace(".", ",");
                $("#Latitude").val(latFixed);
                $("#Longitude").val(lonFixed).keyup();
            });
            $("#Latitude, #Longitude").change(function () {
                var position = [parseInt($("#Latitude").val()), parseInt($("#Longitude").val())];
                marker.setLatLng(position, {
                    draggable: 'true'
                }).bindPopup(position).update();
                map.panTo(position);
            });

        }
        else {
            L.marker([m.latitude, m.longitude], { title: m.title }).addTo(map).on('click', function (e) {
                console.log("notdrag marker #", m.id);
            }).bindPopup(m.title);
        }
    })

    map.addLayer(marker);
})