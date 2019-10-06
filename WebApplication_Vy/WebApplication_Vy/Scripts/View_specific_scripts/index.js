﻿$(function () {
    var time = ["00:00", "01:00", "02:00", "03:00", "04:00", "05:00", "06:00", "07:00", "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00", "22:00", "23:00"];

    var dropdown = "";
    for (var i of time) {
        dropdown += "<option value='" + i + "'>" + i + "</option>";
    }
    $("#time").append(dropdown);
    $("#Return_Time").append(dropdown);

    var stations = [];
    fetch("https://itinerary.cloud.nsb.no/api/stops", { "credentials": "omit", "headers": { "accept": "application/json", "sec-fetch-mode": "cors", "x-language": "no" }, "referrer": "https://www.vy.no/bestill/velg-togavgang?from=Oslo%20S&fromDisplayName=Oslo%20S&fromType=train-station-name&to=Bergen&toDisplayName=Bergen&toType=train-station-name&departureDatetime=2019-09-24%2010%3A28&petFree=false&pasCats=1&numPasCats=1", "referrerPolicy": "no-referrer-when-downgrade", "body": null, "method": "GET", "mode": "cors" })
        .then(data => data.json()).then(data => {
            $.each(data, function (index, value) {
                if (value.active && value.type === "TRAIN") {
                    stations.push(value.name)
                }
            });
            stations.sort();
        });

    $('.stations').autocomplete({
        source: stations
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

    $("#date").on("focus", function () {
        var btn = $(this).next().children("button")[0]
        console.log(btn)
        btn.click()
    })

    $("#ReturnDiv").on("focus", "#ReturnDate", function () {
        var btn = $(this).next().children("button")[0]
        console.log(btn)
        btn.click()
    })

    $("#date").on("change", function () {
        date = new Date($("#date").val())
        date.setDate(date.getDate() - 1)
        $("#ReturnDate").datepicker("destroy");
        $("#ReturnDate").datepicker({
            minDate: date,
            uiLibrary: 'bootstrap4',
            maxDate: dateMax,
            format: "yyyy-mm-dd",
            weekStartDay: 1,
        });
        $("#ReturnDate").addClass("bg-white");
        $("#ReturnDate").next().hide()
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
});