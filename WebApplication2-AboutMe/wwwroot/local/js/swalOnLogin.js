document.getElementById('showAlert0').addEventListener("click", function () {

    fetch("/Account/Login/", {
        method: "post",
    }).then(() => {
        Swal.fire({
            position: 'top-end',
            icon: 'success',
            title: 'Welcome',
            showConfirmButton: false,
            timer: 1500
        })
    })

});