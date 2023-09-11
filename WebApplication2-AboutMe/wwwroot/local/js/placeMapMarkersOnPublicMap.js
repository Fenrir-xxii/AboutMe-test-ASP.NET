fetch("/Map/GetMarkers", {
    method: "get",
    headers: {
        'Content-Type': "application/json"
    },
}).then(r => r.json()).then(markers => {
    markers.forEach(m => {
        L.marker([m.latitude, m.longitude], { title: m.title, id: m.id }).addTo(map).on('click', function (e) {
            
           
        }).bindPopup(m.title)
    })
})
