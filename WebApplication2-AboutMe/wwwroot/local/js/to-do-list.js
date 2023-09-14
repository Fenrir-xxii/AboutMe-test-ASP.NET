

$('#datepicker').datepicker({
    format: 'dd/mm/yyyy',
});
$("#datepicker").datepicker("setDate", new Date());

let selectedDate = $('#datepicker').datepicker('getFormattedDate');
let table = document.getElementById("tableBody");
showTasks();

$('#datepicker').on('changeDate', showTasks);
function showTasks() {
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
            let taskId = t.id;
            console.log("taskId", taskId);
            console.log("checkbox", checkbox);

            if (t.isCompleted) {
                checkbox.innerHTML = `<input class="form-check-input" type = "checkbox" value = "" id = "flexCheckChecked" checked>`
            } else {
                checkbox.innerHTML = `<input class="form-check-input" type = "checkbox" value = "" id = "flexCheckDefault">`
            }
            checkbox.firstChild.addEventListener('change', function () {
                console.log("inside checkbox", this);
                if (this.checked) {
                    fetch("/Task/CompleteTask", {
                        method: "POST",
                        headers: {
                            'Content-Type': "application/json"
                        },
                        body: JSON.stringify(taskId)
                    })

                }
                else {
                    fetch("/Task/RestoreTask", {
                        method: "POST",
                        headers: {
                            'Content-Type': "application/json"
                        },
                        body: JSON.stringify(taskId)
                    })
                }
            })
            let title = row.insertCell(1);
            title.innerHTML = t.title;
            title.addEventListener('dblclick', function () {
                console.log("dblclick");
                Swal.fire({
                    input: 'textarea',
                    inputLabel: 'New Title of Task',
                    //inputPlaceholder: 'Type new title of this task...',
                    //inputAttributes: {
                    //    'aria-label': 'Type new title of this task'
                    //},
                    inputValue: t.title,
                    showCancelButton: true
                })

            })

            let date = row.insertCell(2);
            //date.innerHTML = t.date.toString();
            date.innerHTML = moment(t.date).format('DD.MM.YYYY')
            let del = row.insertCell(3);
            del.innerHTML = `<button type="button" class="btn btn-danger">X</button>`
            del.firstChild.addEventListener('click', function () {
                fetch("/Task/DeleteTask/", {
                    method: "POST",
                    headers: {
                        'Content-Type': "application/json"
                    },
                    body: JSON.stringify(taskId)
                }).then(() => {
                    showTasks();
                })

            })
            let edit = row.insertCell(4);
            edit.innerHTML = `<button type="button" class="btn btn-warning">E</button>`
            edit.firstChild.addEventListener('click', function () {
                //fetch("/Task/EditTask/", {
                //    method: "POST",
                //    headers: {
                //        'Content-Type': "application/json"
                //    },
                //    body: JSON.stringify({
                //        Title: taskTitle,
                //        Date: taskDate,
                //        IsCompleted: false
                //    })
                //}).then(() => {
                //    showTasks();
                //})

            })
        })
    })
}
function AddTask() {

    let taskTitle = document.getElementById('taskTitle').value;
    let taskDate = $('#datepicker').datepicker('getUTCDate');

    //console.log("datepicker", datePicker);
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
    }).then(() => {
        //location.reload();
        showTasks();
        Swal.fire({
            position: 'top-end',
            icon: 'success',
            title: 'Task removed successfully',
            showConfirmButton: false,
            timer: 1500
        })
    })
}


function removeAllCompleted() {
    Swal.fire({
        title: 'Are you sure?',
        html: "deleting all completed tasks",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#910a27',
        cancelButtonColor: '#545253',
        confirmButtonText: 'Yes, delete them',
        cancelButtonText: 'No, keep tasks',
        allowOutsideClick: false,
    }).then((result) => {
        if (result.isConfirmed) {
            fetch("/Task/DeleteAllCompleted", {
                method: "post",
                headers: {
                    'Content-Type': "application/json"
                },
                body: JSON.stringify(selectedDate)
            }).then(() => {
                //location.reload();
                showTasks();
                Swal.fire({
                    position: 'top-end',
                    icon: 'success',
                    title: 'Completed tasks was removed',
                    showConfirmButton: false,
                    timer: 1500
                })
            })
        }
    });

}


