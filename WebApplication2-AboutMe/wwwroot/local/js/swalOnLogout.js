//document.getElementById('showAlert1').addEventListener("click", function () {
//    const swalWithBootstrapButtons = Swal.mixin({
//        customClass: {
//            confirmButton: 'btn btn-danger',
//            cancelButton: 'btn btn-primary'
//        },
//        buttonsStyling: false
//    });


//    swalWithBootstrapButtons.fire({
//        title: 'Are you sure?',
//        html: "Stay for a while",
//        //icon: 'warning',
//        showCancelButton: true,
//        //confirmButtonColor: '#3085d6',
//        //cancelButtonColor: '#d33',
//        cancelButtonText: 'No, cancel!',
//        confirmButtonText: 'Yes, i`m out',
//        allowOutsideClick: false,
//    }).then((result) => {
//        if (result.isConfirmed) {
//            $.ajax({
//                type: "POST",
//                url: "/Account/Logout",
//                success: function () {
//                    window.location = "/";
//                }

//            });
//        }
//    });
//});

document.getElementById('showAlert1').addEventListener("click", function () {
   
    Swal.fire({
        title: 'Are you sure?',
        html: "Stay for a while",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#0d5b0d',
        cancelButtonColor: '#d0d2b5',
        cancelButtonText: 'No, I`m Staying',
        confirmButtonText: 'Yes, I`m Out',
        allowOutsideClick: false,
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                url: "/Account/Logout",
                success: function () {
                    window.location = "/";
                }

            });
        }
    });
});