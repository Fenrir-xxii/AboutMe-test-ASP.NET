const results = $('ul#search-result')

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
        results.empty()
        skills.forEach(s => {
            results.append(`<li>${s}</li>`)
        })
    })
}

$("input#search").on('input', e => {
    updateSkills($(e.target).val())
})
updateSkills("")