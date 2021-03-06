﻿$(function () {
    var time = ["00:00", "01:00", "02:00", "03:00", "04:00", "05:00", "06:00", "07:00", "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00", "22:00", "23:00"];

    var dropdown = "";
    for (var i of time) {
        dropdown += "<option value='" + i + "'>" + i + "</option>";
    }
    $("#time").append(dropdown);
    $("#Return_Time").append(dropdown);

    let stations = [];
    let names = []
    $.ajax({
        url: "/home/GetAllStations",
        type: 'GET',
        contentType: "application/json;charset=utf-8",
        success: function (response) {
            stations = JSON.parse(response);
            for (var key in stations) {
                names.push(key)
            }
        }
    });

    $('.stations').autocomplete({
        source: function (request, response) {
            //let results = $.ui.autocomplete.filter(names, request.term);
            let results = []
            const regex = new RegExp("^" + request.term)
            results = names.filter(s => regex.test(s))
            if (results.length == 0) {
                results = $.ui.autocomplete.filter(names, request.term);
            }
            response(results.slice(0, 10));
        },
        autoFocus: true,
    });

    $("#SwitchButton").on("click", function (e) {

        var destinationTemp = $("#Arrival").val();
        var departureTemp = $("#Departure").val();

        $("#Arrival").val(departureTemp);
        $("#Departure").val(destinationTemp);
    });

    $("#SwitchButton").on("hover").css("cursor", "pointer");

    //Start round trips select 
    $('#Round_Trip').bootstrapToggle({
        off: 'One way',
        on: 'Round trip',
        onstyle: "success",
        offstyle: "secondary",
    });

    $("#Round_Trip").prop("checked", false);
    $('#Round_Trip').bootstrapToggle('off')

    $("#Round_Trip").change(function () {
        if (this.checked) {
            $("#ReturnDiv").slideDown();
            $("#ReturnDate").prop('required', true)
            $("#Return_Time").prop('required', true)
        } else {
            $("#ReturnDiv").slideUp();
            $("#ReturnDate").prop('required', false)
            $("#Return_Time").prop('required', false)
        }
    });//End round trip select

    var date = new Date();
    date.setDate(date.getDate() - 1);
    var dateMax = new Date();
    dateMax.setDate(dateMax.getDate() + 80);

    //Start date picker
    $('#date').datepicker({
        minDate: date,
        uiLibrary: 'bootstrap4',
        maxDate: dateMax,
        format: "yyyy-mm-dd",
        weekStartDay: 1,
    });


    $('#ReturnDate').datepicker({
        minDate: date,
        uiLibrary: 'bootstrap4',
        maxDate: dateMax,
        format: "yyyy-mm-dd",
        weekStartDay: 1,
    });

    $(".date-picker").eq(0).next().hide()
    $(".date-picker").eq(1).next().hide()

    var dateFocus = false;
    $("#date").on("focus", function () {
        if (!dateFocus) {
            var btn = $(this).next().children("button")[0]
            btn.click()
            dateFocus = true;
        }
    })

    $("#date").on("focusout", function () {
        dateFocus = false;
    })

    var returnDateFocus = false;
    $("#ReturnDiv").on("focus", "#ReturnDate", function () {
        if (!returnDateFocus) {
            var btn = $(this).next().children("button")[0]
            btn.click()
            returnDateFocus = true;
        }
    })

    $("#ReturnDiv").on("focusout", "#ReturnDate", function () {
        returnDateFocus = false;
    })

    $("#date").on("change", function () {
        date = new Date($("#date").val())
        date.setDate(date.getDate() - 1);
        $("#ReturnDate").datepicker("destroy");
        $("#ReturnDate").datepicker({
            minDate: date,
            uiLibrary: 'bootstrap4',
            maxDate: dateMax,
            format: "yyyy-mm-dd",
            weekStartDay: 1,
        });
        $("#ReturnDate").addClass("bg-white").addClass("date-picker");
        $("#ReturnDate").next().hide();
    });//End date picker


    //Start people select
    $("#adult").children("input").val(1);
    $("#child").children("input").val(0);
    $("#student").children("input").val(0);
    $("#senior").children("input").val(0);

    var totalTickets = 1;
    var maxTickets = 10

    $(".add-sub-ticket").on("click", ".pluss", function () {
        var value = $(this).siblings("input").val();
        if (value == 0) {
            $(this).siblings(".minus").attr("disabled", false);
        }
        ++value;
        ++totalTickets;
        $(this).siblings("input").val(value);
        $(this).siblings("b").text(value);

        if (totalTickets >= maxTickets) {
            $(".pluss").attr("disabled", true)
        }
    });

    $(".add-sub-ticket").on("click", ".minus", function () {
        if (totalTickets <= 1) return
        var value = $(this).siblings("input").val();
        --value;
        --totalTickets;
        $(this).siblings("input").val(value);
        $(this).siblings("b").text(value);
        if (value <= 0) {
            $(this).attr("disabled", true);
        }

        if (totalTickets < maxTickets) {
            $(".pluss").attr("disabled", false)
        }
    });//End people select

    $("#form").on("submit", e => {
        $("#Departure_stationId").val(stations[$("#Departure").val()]);
        $("#Arrival_stationId").val(stations[$("#Arrival").val()]);
    });
});