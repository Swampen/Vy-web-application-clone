$(function () {

    $("#table").on("click", "tr", function () {
        let id = $(this)[0].dataset["id"]
        $("#tickets" + id).modal("show")
    })

    $("#table").find("tr").hover().css("cursor", "pointer")

    $('#table').DataTable({
        scrollY: 475
    });
    $('.tickets-table').DataTable({
        "columns": [
            null,
            null,
            null,
            null,
            null,
            null,
            null
        ]
    });
    $(".dataTables_filter").parent().addClass("row")
    $(".dataTables_filter").find("input").addClass("form-control");
    $(".dataTables_filter").addClass("col-12 col-md-7 pt-2")
    $(".dataTables_length").find("select").addClass("form-control");
    $(".dataTables_length").addClass("col-12 col-md-5 pt-2")
   
    //$('.ticket-modal').on('show.bs.modal', function (event) {
    //    let table = $(this).find(".tickets-table").eq(0).DataTable()
    //    table.columns.adjust().draw();
    //})

    $('#delete').on('show.bs.modal', function (event) {
        const button = $(event.relatedTarget) // Button that triggered the modal
        const id = button[0].id;
        const cid = button[0].dataset["cid"]
        $("#tickets" + cid).modal("hide")

        const url = `/admin/deleteTicket?ticketId=${id}`;
        const modal = $(this);
        modal.find("#confirmBtn").attr("href", url);
        modal.attr("data-cid", cid);

    });

    $('#delete').on('hidden.bs.modal', function (event) {
        const cid = $(this)[0].dataset["cid"];
        $("#tickets" + cid).modal("show")
    })

    

});