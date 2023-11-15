$(document).ready(function () {
    $('.js-delete').on("click", function () {
        var btn = $(this);
        Swal.fire({
            title: "Are you sure?",
            text: "",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#d33682",
            cancelButtonColor: "#268bd2",
            confirmButtonText: "Yes, delete it!"
        }).then((result) => {
            if (result.isConfirmed) {
               $.ajax({
                  url: `/Games/Delete/${btn.data('id')}`,
                  method: 'Delete',
                  success: function () {
                      btn.parents('tr').fadeOut(300);

                  },
                  error: function () {
                      Swal.fire({
                          
                          title:  "Ooops ",
                          text: "Somethings went wromg ",
                          icon: "warning",
                      });
                  }
              }); 
            }
        });

       
    });
});