$(function () {

    $("#table").on("mouseenter", ".button-row", function () {
        var row = $(this)
        row.css('cursor', 'pointer')
        if (!row.hasClass("clicked")) {
            row.css('background-color', '#e6e6e6')
        }
    });

    $("#table").on("mouseleave", ".button-row", function () {
        var row = $(this)
        if (!row.hasClass("clicked")) {
            row.css('background-color', '#DBF0E0')
        }
    });

    $("#table").on("click", ".button-row", function () {
        var row = $(this)
        if (row.hasClass("clicked")) {
            row.removeClass("clicked");
            row.css('background-color', '#e6e6e6')
        } else {
            row.addClass("clicked");
        }


        row.next().slideToggle()
    });

    $('#delete').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Button that triggered the modal
        var id = button[0].id;
        console.log(id);
       

        var url = `/admin/deleteTicket?ticketId=${id}`;
        var modal = $(this);
        modal.find("#confirmBtn").attr("href", url);
    });
});