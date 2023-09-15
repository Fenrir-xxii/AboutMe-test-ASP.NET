

$('#datepicker').datepicker({
    format: 'dd/mm/yyyy',
    weekStart: '1',
    todayBtn: 'linked',
    todayHighlight: 'true',
    //daysOfWeekHighlighted: '[0,6]'
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
                    }).then(() => {
                        showTasks()
                    })

                }
                else {
                    fetch("/Task/RestoreTask", {
                        method: "POST",
                        headers: {
                            'Content-Type': "application/json"
                        },
                        body: JSON.stringify(taskId)
                    }).then(() => {
                        showTasks()
                    })
                }
            })
            let title = row.insertCell(1);
            title.innerHTML = t.title;
            if (t.isCompleted) {
                title.classList.add("completed");
            }
            title.addEventListener('dblclick', function () {
                console.log("dblclick title");
                Swal.fire({
                    input: 'textarea',
                    inputLabel: 'New Title of Task',
                    inputValue: t.title,
                    showCancelButton: true
                }).then((inputValue) => {
                    console.log("inputVal", inputValue);
                    console.log("taskId", taskId);
                    fetch(`/Task/EditTitle/${taskId}`, {
                        method: "POST",
                        headers: {
                            'Content-Type': "application/json"
                        },
                        body: JSON.stringify(inputValue.value)
                    }).then(() => {
                        showTasks();
                    })
                })

            })

            let date = row.insertCell(2);
            //date.innerHTML = t.date.toString();
            date.innerHTML = moment(t.date).format('DD.MM.YYYY');
            if (t.isCompleted) {
                date.classList.add("completed");
            }
            date.addEventListener('dblclick', function (event) {
                console.log("dblclick date");
                event.target.innerHTML = `<div class='input-group date' id='datetimepickerEdit'>
                                        <input type='text' class="form-control" />
                                        <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span>
                                        </span>
                                    </div>`

                $('#datetimepickerEdit').datepicker({
                    format: 'dd/mm/yyyy',
                    weekStart: '1',
                    todayBtn: 'linked',
                    todayHighlight: 'true',
                });
                const d = new Date(`${t.date}`);
                console.log("d", d);
                $("#datetimepickerEdit").datepicker("setDate", d);
                $('#datetimepickerEdit').on('changeDate', function () {
                    let editDate = $('#datetimepickerEdit').datepicker('getFormattedDate');
                    fetch(`/Task/EditDate/${taskId}`, {
                        method: "POST",
                        headers: {
                            'Content-Type': "application/json"
                        },
                        body: JSON.stringify(editDate)
                    }).then(() => {
                        $('#datetimepickerEdit').datepicker('hide');
                        showTasks();
                    })
                });


            })


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
        document.getElementById('taskTitle').value = "";
        showTasks();
        Swal.fire({
            position: 'top-end',
            icon: 'success',
            title: 'Task added successfully',
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
