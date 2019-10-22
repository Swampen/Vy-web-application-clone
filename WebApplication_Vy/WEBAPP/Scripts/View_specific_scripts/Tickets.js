$(function () {

    //$("#table").on("mouseenter", ".button-row", function () {
    //    var row = $(this)
    //    row.css('cursor', 'pointer')
    //    if (!row.hasClass("clicked")) {
    //        row.css('background-color', '#e6e6e6')
    //    }
    //});

    //$("#table").on("mouseleave", ".button-row", function () {
    //    var row = $(this)
    //    if (!row.hasClass("clicked")) {
    //        row.css('background-color', '#DBF0E0')
    //    }
    //});

    $("#table").on("click", "tr", function () {
        let id = $(this)[0].dataset["id"]
        $("#tickets" + id).modal("show")
    })

    $("#table").find("tr").hover().css("cursor", "pointer")


    //    row.next().slideToggle()
    //});

    $('#table').DataTable({
        scrollY: 475
    });
    $('.tickets-table').DataTable({
        scrollY: 475
    });
    $(".dataTables_filter").parent().addClass("row")
    $(".dataTables_filter").find("input").addClass("form-control");
    $(".dataTables_filter").addClass("col-12 col-md-7 pt-2")
    $(".dataTables_length").find("select").addClass("form-control");
    $(".dataTables_length").addClass("col-12 col-md-5 pt-2")
   

    $('#delete').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Button that triggered the modal
        var id = button[0].id;
        console.log(id);


        var url = `/admin/deleteTicket?ticketId=${id}`;
        var modal = $(this);
        modal.find("#confirmBtn").attr("href", url);
    });


    

});