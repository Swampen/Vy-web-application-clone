$(function () {

    $("#table").on("mouseenter", ".button-row", function () {
        var row = $(this)
        row.css('cursor', 'pointer')
        if (!row.hasClass("clicked")){
            row.css('background-color', '#e6e6e6')
        }
    });

    $("#table").on("mouseleave", ".button-row", function () {
        var row = $(this)
        if (!row.hasClass("clicked")){
            row.css('background-color', 'rgba(40, 167, 69, 0.50)')
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

    $("#table").on("click", "#paymentDetails", function () {
        id = $(this).val();
        console.log("this" + id);
        $.ajax({
            url: "/home/GetPaymentDetails",
            type: "POST",
            data: JSON.stringify(id),
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                console.log(data);
            },
            error: function (x, y, z) {
                alert(x + '\n' + y + '\n' + z);
            }
        });
    });
});