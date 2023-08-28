const fullPath = $('#fullPath');
function getLogoPath() {
	let input = document.getElementById("logo").files[0].name;
	let folderPath = document.getElementById("pathToFolder").innerText;
	let fullPathOfImage = folderPath + input;
	console.log("input", input);
	console.log("full path", fullPath);

    fetch("/PersonInfo/SkillsLogo", {  
        method: "post",
        headers: {
            'Content-Type': "application/json"
        },
        body: JSON.stringify({
            Query: fullPathOfImage
        })
    }).then(r => r.json()).then(
        //fullPath.textContent = fullPathOfImage;
        //$('#fullPath').update("hello");
        fullPath.append(`${fullPathOfImage}`) 
    )

}

$("input#logo").on('change', e => {
	getLogoPath();
})
