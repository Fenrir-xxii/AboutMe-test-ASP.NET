

fetch("/Map/GetMarkers", {
    method: "get",
    headers: {
        'Content-Type': "application/json"
    },
}).then(r => r.json()).then(markers => {
    markers.forEach(m => {
        var mark = L.marker([m.latitude, m.longitude], { title: m.title, id: m.id}).addTo(map).on('click', function (e) {
            mark.marker_id = m.id;
            console.log("marker", e.target.options);
            Swal.fire({
                title: e.target.options.title,
                text: this.getLatLng(),
                icon: 'info',
                showCancelButton: false,
                
                showCancelButton: true,
                confirmButtonText: 'Edit',
                confirmButtonColor: '#7a0707'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        type: "GET",
                        url: "/Map/EditMarker/" + m.id,
                        success: function () {
                            window.location = "/Map/EditMarker/" + m.id;
                        }

                    });
                    //Swal.fire({
                    //    title: 'Are you sure?',
                    //    text: "You won't be able to revert this!",
                    //    icon: 'warning',
                    //    showCancelButton: true,
                    //    confirmButtonColor: '#3085d6',
                    //    cancelButtonColor: '#d33',
                    //    confirmButtonText: 'Yes, delete it!'
                    //}).then((result) => {
                    //    if (result.isConfirmed) {
                    //        //delete marker
                    //        map.removeLayer(e.target);
                    //        //let lightMarker = { title: marker.options.title, lat: marker._latlng.lat, lng: marker._latlng.lng };
                    //        //const index = markers.findIndex(object => {
                    //        //    return object.title === lightMarker.title && object.lat === lightMarker.lat && object.lng === lightMarker.lng;
                    //        //})
                    //        //if (index > -1) { // only splice array when item is found
                    //        //    markers.splice(index, 1); // 2nd parameter means remove one item only
                    //        //}
                    //        //saveData();
                    //        Swal.fire(
                    //            'Deleted!',
                    //            'Your marker has been deleted.',
                    //            'success'
                    //        )
                    //    }
                    //})
                } else if (result.isDenied) {
                    //fetch("/Map/EditMarker/" + marker.id, {
                    //    method: "get",
                    //    headers: {
                    //        'Content-Type': "application/json"
                    //    },

                    //})
                    //console.log("cancel del");
                    //$.ajax({
                    //    type: "GET",
                    //    url: "/Map/EditMarker/" + marker.id,
                    //    success: function () {
                    //        window.location = "/";
                    //    }

                    //});
                }
            })
        }).openPopup();
    })
})



//var buttons = $('<div>')
//    .append(createButton('Edit', function () {
//        swal.close();
//        console.log('edit');
//    })).append(createButton('Delete', function () {
//        swal.close();
//        console.log('delete');
//    })).append(createButton('Cancel', function () {
//        swal.close();
//        console.log('Cancel');
//    }));