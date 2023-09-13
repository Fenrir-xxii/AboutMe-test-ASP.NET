

$('#datepicker').datepicker({
    format: 'dd/mm/yyyy',
});

let selectedDate = $('#datepicker').datepicker('getFormattedDate');
let table = document.getElementById("tableBody");

$('#datepicker').on('changeDate', function () {
    $('#my_hidden_input').val(
        selectedDate = $('#datepicker').datepicker('getFormattedDate')
    );
    fetch("/Task/GetTasks", {
        method: "POST",
        headers: {
            'Content-Type': "application/json"
        },
        body: JSON.stringify(selectedDate)
    }).then(r => r.json()).then(tasks => {
        table.innerHTML = ""; 
        tasks.forEach(t => {
            console.log(t);
            let row = table.insertRow();
            let checkbox = row.insertCell(0);
            let title = row.insertCell(1);
            title.innerHTML = t.title;
            let date = row.insertCell(2);
            date.innerHTML = t.date;
            let del = row.insertCell(3);
        })
    })
    //fetch("/Task/GetTasks", {
    //    method: "POST",
    //    headers: {
    //        'Content-Type': "application/json"
    //    },
    //    body: JSON.stringify(selectedDate)
    //})
    
});



//$('.datepicker').datepicker()
//    .on('changeDate', getTasks);

//function getTasks() {
//    fetch("/Task/GetTasks", {
//        method: "POST",
//        headers: {
//            'Content-Type': "application/json"
//        },
//        body: JSON.stringify(selectedDate)
//    }).then(r => r.json()).then(tasks => {
//        tasks.forEach(t => {
//            console.log(t);
//        })
//    })
//}


//function addListeners() {
//    var checkboxes = document.querySelectorAll("input[type=checkbox][name=settings]");
//    var delButtons = document.querySelectorAll("button[name=delete]");
//    var editableCol = document.getElementsByClassName("editable")

//    checkboxes.forEach(function (checkbox, index) {
//        checkbox.addEventListener('change', function () {
//            var rowIndex = $('#table tr').index($(this).closest('tr'));
//            if (this.checked) {
//                completeTask(rowIndex - 1)
//            }
//            else {
//                restoreTask(rowIndex - 1)
//            }
//        })
//    });
//    delButtons.forEach(function (button, index) {
//        if (button.getAttribute('listener') !== 'true') {
//            console.log(`setting event @ #${index}`)
//            button.addEventListener('click', function () {
//                var rowIndex = $('#table tr').index($(this).closest('tr'));
//                console.log(rowIndex)
//                deleteTask(rowIndex - 1)
//            })
//        }
//        else {
//            console.log(`event exist @ #${index}`)
//        }
//    })
//    delButtons.forEach(function (button) {
//        button.setAttribute('listener', 'true')
//    })
//    // console.log(editableCol)
//    for (let title of editableCol) {
//        title.addEventListener('click', function (e) {
//            let el = e.target
//            let content = el.innerText
//            el.innerHTML = `<input type="text" class="editable-input" value="${content}">`

//            el.querySelector("input").focus()
//            //updateTasks()
//            // console.log(title)

//            Array.from(document.querySelectorAll("input.editable-input"))
//                .forEach(inp => {
//                    inp.addEventListener('blur', (ev) => {
//                        let editable = ev.target.closest(".editable")
//                        //console.log("blur")
//                        if (editable !== null) {
//                            ev.target.closest(".editable").innerHTML = ev.target.value
//                            // console.log("editable if")
//                            updateTasks()
//                        }
//                    })
//                })
//        })
//    }

//}

function AddTask() {

    let taskTitle = document.getElementById('taskTitle').value;
    /* let taskDate = datePicker.getAttribute('data-date');*/
    //let taskDate = $('#datepicker').datepicker('getFormattedDate');
    let taskDate = $('#datepicker').datepicker('getUTCDate');

    console.log("datepicker", datePicker);
    console.log("taskTitle", taskTitle);
    console.log("taskDate", taskDate);

    fetch("/Task/AddTask", {
        method: "post",
        headers: {
            'Content-Type': "application/json"
        },
        body: JSON.stringify({
            Title: taskTitle,
            Date: taskDate,
            IsCompleted: false
        })
    })
}
