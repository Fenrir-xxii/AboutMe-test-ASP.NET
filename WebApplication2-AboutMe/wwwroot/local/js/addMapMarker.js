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
        if (!result.value) {
            Swal.fire('You need to write something!', '', 'error');
            return;
        }
        console.log("result", result);
        if (result.isConfirmed) {
            var marker = L.marker([lat, lng], { title: result.value }).addTo(map).on('click', function (e) {
                console.log("marker", e.target.options);
                Swal.fire({
                    title: e.target.options.title,
                    text: this.getLatLng(),
                    icon: 'info',
                    showCancelButton: true,
                    confirmButtonText: 'Delete',
                    confirmButtonColor: '#7a0707'
                    
                }).then((result) => {
                    if (result.isConfirmed) {
                        Swal.fire({
                            title: 'Are you sure?',
                            text: "You won't be able to revert this!",
                            icon: 'warning',
                            showCancelButton: true,
                            confirmButtonColor: '#3085d6',
                            cancelButtonColor: '#d33',
                            confirmButtonText: 'Yes, delete it!'
                        }).then((result) => {
                            if (result.isConfirmed) {
                                //delete marker
                                map.removeLayer(e.target);
                                //let lightMarker = { title: marker.options.title, lat: marker._latlng.lat, lng: marker._latlng.lng };
                                //const index = markers.findIndex(object => {
                                //    return object.title === lightMarker.title && object.lat === lightMarker.lat && object.lng === lightMarker.lng;
                                //})
                                //if (index > -1) { // only splice array when item is found
                                //    markers.splice(index, 1); // 2nd parameter means remove one item only
                                //}
                                //saveData();
                                Swal.fire(
                                    'Deleted!',
                                    'Your marker has been deleted.',
                                    'success'
                                )
                            }
                        })
                    } else if (result.isDenied) {
                        // Swal.fire('Changes are not saved', '', 'info')
                    }
                })
            }).openPopup();
           /* let lightMarker = { title: marker.options.title, lat: marker._latlng.lat, lng: marker._latlng.lng };*/
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
            })

            Swal.fire('Saved!', '', 'success')
        } else if (result.isDenied) {
            Swal.fire('Changes are not saved', '', 'info')
        }
    })

});