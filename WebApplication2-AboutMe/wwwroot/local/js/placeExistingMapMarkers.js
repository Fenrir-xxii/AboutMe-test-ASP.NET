

fetch("/Map/GetMarkers", {
    method: "get",
    headers: {
        'Content-Type': "application/json"
    },
}).then(r => r.json()).then(markers => {
    markers.forEach(m => {
        L.marker([m.latitude, m.longitude], { title: m.title, id: m.id}).addTo(map).on('click', function (e) {
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
})
