var lat, lng;
map.addEventListener('mousemove', function (ev) {
    lat = ev.latlng.lat;
    lng = ev.latlng.lng;
    console.log("lat", lat);
    console.log("lng", lng);
});
$("#map").on('dblclick', function (e) {
    Swal.fire({
        title: 'Add marker here?',
        text: `lat: ${lat} lng ${lng}`,
        input: 'text',
        inputAttributes: {
            autocapitalize: 'off'
        },
        icon: 'info',
        confirmButtonText: 'Confirm',
        showCancelButton: true
    }).then((result) => {
        console.log("result", result);
        if (result.isConfirmed) {
            if (!result.value) {
                Swal.fire('You need to write something!', '', 'error');
                return;
            }
            var marker = L.marker([lat, lng], { title: result.value });
           
            console.log("result confirmed");
            fetch("/Map/AddMarker", {
                method: "post",
                headers: {
                    'Content-Type': "application/json"
                },
                body: JSON.stringify({
                    Title: marker.options.title,
                    Latitude: marker._latlng.lat,
                    Longitude: marker._latlng.lng
                })
            }).then(() => {
                // read data from db and get this marker.Id
                //location.reload();

                fetch("/Map/GetLastMarker", {
                    method: "get",
                    headers: {
                        'Content-Type': "application/json"
                    },
                }).then(r => r.json()).then(m => {
                    L.marker([m.latitude, m.longitude], { title: m.title, id: m.id }).addTo(map).on('click', function (e) {
                        /*mark.marker_id = m.id;*/
                        console.log("marker", e.target.options);
                        Swal.fire({
                            title: e.target.options.title,
                            text: this.getLatLng(),
                            icon: 'info',
                            showDenyButton: true,
                            showCancelButton: true,
                            confirmButtonText: 'Edit',
                            confirmButtonColor: '#3c4cc8',
                            denyButtonText: 'Delete',
                            denyButtonColor: '#7a0707'
                        }).then((result) => {
                            if (result.isConfirmed) {
                                $.ajax({
                                    type: "GET",
                                    url: "/Map/EditMarker/" + m.id,
                                    success: function () {
                                        window.location = "/Map/EditMarker/" + m.id;
                                    }

                                });
                            } else if (result.isDenied) {
                                fetch("/Map/DeleteMarker/" + m.id, {
                                    method: "delete",
                                }).then(() => {
                                    map.removeLayer(e.target);
                                })

                            }
                        })
                    }).openPopup();
                })
            }) //close then

            Swal.fire('Saved!', '', 'success')
        } else if (result.isDenied) {
            Swal.fire('Changes are not saved', '', 'info')
        }
    })

});
