const results = $('ul#search-result')

//function updateSkills(query) {
//    console.log("update skills");
//    console.log("query", query);
//    fetch("/AboutMe/Skills", {
//        method: "post",
//        headers: {
//            'Content-Type': "application/json"
//        },
//        body: JSON.stringify({
//            Query: query
//        })
//    }).then(r => r.json()).then(skills => {
//        results.empty()
//        skills.forEach(s => {
//            results.append(`<li>${s}</li>`)
//        })
//    })
//}

function updateSkills(query) {
    console.log("update skills");
    console.log("query", query);
    fetch("/AboutMe/Skills", {
        method: "post",
        headers: {
            'Content-Type': "application/json"
        },
        body: JSON.stringify({
            Query: query
        })
    }).then(r => r.json()).then(skills => {
        results.empty();
        skills.forEach(s => {
            let imgFolderPath = "/uploads/images/" + `${s.image}`;
            results.append(`<li><img class="img-thumbnail" src='${imgFolderPath}' width="25px"> &#160; ${s.title} &#160; <b><i>${s.level}<i></b> <div><progress value=${s.level} max="100"></progress></div></li>`)
        })
    })
}


$("input#search").on('input', e => {
    updateSkills($(e.target).val())
})
updateSkills("")