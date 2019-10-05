$(function () {
    $("#trips").hide();
    console.log(trip);

    var people = { Adult: trip.Adult, Child: trip.Child, Student: trip.Student, Senior: trip.Senior }
    console.log(people)
    //Querry to vy api
    var itineraries = "";
    fetch("https://booking.cloud.nsb.no/api/itineraries/search", {
        "credentials": "omit", "headers": { "accept": "application/json", "content-type": "application/json", "sec-fetch-mode": "cors", "x-language": "no" },
        "referrer": "https://www.vy.no/bestill/velg-togavgang?from=" + trip.Departure_Station + "%20S&fromDisplayName=" + trip.Departure_Station + "%20S&fromType=train-station-name&to=" + trip.Arrival_Station + "&toDisplayName=" + trip.Arrival_Station + "&toType=train-station-name&departureDatetime=" + trip.Date + "%20" + trip.Time.split(":")[0] + "%3A" + trip.Time.split(":")[1] + "&petFree=false&pasCats=1&numPasCats=1",
        "referrerPolicy": "no-referrer-when-downgrade", "body": "{\"to\":\"" + trip.Arrival_Station + "\",\"from\":\"" + trip.Departure_Station + "\",\"time\":\"" + trip.Date + "T" + trip.Time + "\",\"limitResultsToSameDay\":true,\"language\":\"no\",\"passengers\":[{\"type\":\"ADULT\",\"customerNumber\":null,\"discount\":\"NONE\",\"extras\":[]}],\"priceNecessity\":\"REQUIRED\"}", "method": "POST", "mode": "cors"
    }).then(data => data.json()).then(data => {
        //Loads the result
        itineraries = data.itineraries;
        if (itineraries.length === 0) {
            $("#alert").show();
        }
        //For each departure
        $.each(itineraries, function (i, value) {
            //Finds how many changes it has
            var steps = value.legs;
            var changes = -1;
            for (var train of steps) {
                if (train.transportType == "TRAIN") {
                    changes++;
                }
            }

            //Price section
            var countZero = 0;
            var originalPrice = value.priceOptions[value.priceOptions.length - 1].amount
            if (originalPrice == null) {
                countZero++;
                if (countZero == itineraries.length) {
                    $("#alert").show();
                }
                return true;
            }
            var price = 0;
            var priceDetails = "<div class='col m-3'>\
                                        <div class='row'>\
                                            <div class='w-100 text-center'><button type='button' class='price-btn p-2 btn btn-primary'>Show price details</button></div >\
                                        </div>\
                                        <div style='display: none;'>"
            $.each(people, function (p, ammount) {
                if (ammount > 0) {
                    if (p === "Adult") {
                        priceDetails += "<div class='row'>\
                                                <div class='col text-center ml-2 mr-2'>" + ammount + " " + p + "  -  " + originalPrice + ",-</div>\
                                            </div>"
                        price += originalPrice;
                    }
                } else if (p === "Child" || p === "Senior") {
                    if (ammount > 0) {
                        priceDetails += "<div class='row'>\
                                                <div class='col text-center ml-2 mr-2'>" + p + "  -  " + originalPrice * 0.5 + ",-</div>\
                                            </div>"
                        price += originalPrice * 0.5;
                    }
                } else if (p === "Student") {
                    if (ammount > 0) {
                        priceDetails += "<div class='row'>\
                                                <div class='col text-center ml-2 mr-2'>" + p + "  -  " + originalPrice * 0.75 + ",-</div>\
                                            </div>"
                        price += originalPrice * 0.75;
                    }
                }

            });
            priceDetails += "</div></div>"//end price section


            //Sets suitable text to the ammount of changes
            var changesText;
            if (changes == 0) {
                changesText = "Direct";
            } else if (changes == 1) {
                changesText = "1 change";
            } else {
                changesText = changes + " changes";
            }

            const temp = value.duration;
            const duration = (temp.days !== 0 ? temp.days + "d " : "") + (temp.hours !== 0 ? temp.hours + "h " : "") + (temp.minutes !== 0 ? temp.minutes + "m " : "")

            //Displays the result
            $("#trips").append(`<div class='container'><form action='/home/trips' method='POST'>
                        <div id='${i}' class='border-top border-bottom rounded form-group'>
                            <div class='row text-center button-row p-3 '>
                                <div class='col font-weight-bold'>
                                    <input type='text' hidden name='Departure_Time' value='${value.departureScheduled.match("[0-9]{2}:[0-9]{2}")}'>${value.departureScheduled.match("[0-9]{2}:[0-9]{2}")}<span> - </span>
                                    <input type='text' hidden name='Arrival_Time' value='${value.arrivalScheduled.match("[0-9]{2}:[0-9]{2}")}'>${value.arrivalScheduled.match("[0-9]{2}:[0-9]{2}")}
                                </div>
                                <div class='col' ><input type='text' hidden name=Duration value='${duration}'>${duration}</div>
                                <div class='col' ><input type='text' hidden name=Train_Changes value='${changesText}'>${changesText}</div>
                                <div class='col' ><input type='number' hidden name=Price value='${price}'>${price} kr</div>
                                <div><input type='text' hidden name=Date value='${value.departureScheduled.split("T")[0]}'></div>
                                <div><input type='text' hidden name=Departure_Station value='${value.from}'></div>
                                <div><input type='text' hidden name=Arrival_Station value='${value.to}'></div>
                                <div><input type='checkbox' hidden name=Round_Trip value='${trip.Round_Trip ? true : false}' checked></div>
                            </div>\
                            <div id='hidden_${i}' class='row mt-4' style='display: none;'>
                            </div>\
                        </div>\
                    </form></div>`);

            //Appends more info, but hidden
            var hidden_content = "<div class='col ml-5'>"
            $.each(value.legs, function (j, leg) {
                if (leg.transportType == "TRAIN") {
                    $.each(this.stops, function (k, stop) {
                        if (k == 0) {
                            hidden_content += "<div class='row font-weight-bold'>" + stop.name + "</div>"
                            hidden_content += "<div class='border-left border-success pl-4'>\<div class='row'><a href=''>" + (leg.stops.length - 1) + " stops</a></div>"
                            hidden_content += "<div class='col ml-0' style='display: none;'><div class='pl-3'>"
                        } else if (k == leg.stops.length - 1) {
                            hidden_content += "</div></div></div>"
                            hidden_content += "<div class='row mb-3 font-weight-bold'>" + stop.name + "</div>"
                        } else {
                            hidden_content += "<div class='row'>" + stop.name + "</div>"
                        }
                    })
                }
            });
            hidden_content += "</div>";
            hidden_content += priceDetails;
            hidden_content += "<div class='col text-right m-3' > <button type='submit' class='btn btn-success'>Select</button></div>";

            $("#hidden_" + i).append(hidden_content);

        });//end foreach departure

        //Hides the loading spinner and shows the result
        $("#loading").remove();
        $("#trips").show();
    });

    $("#trips").on("mouseenter", ".button-row", function () {
        $(this).css('cursor', 'pointer')
        $(this).css('background-color', '#e6e6e6')
    });

    $("#trips").on("mouseleave", ".button-row", function () {
        $(this).css('background-color', '#ffffff')
    });

    $("#trips").on("click", ".button-row", function () {
        $(this).next().slideToggle()
    })

    $("#trips").on("click", "a", function (e) {
        e.preventDefault();
        $(this).parent().next().slideToggle()
    })

    $("#trips").on("click", ".price-btn", function () {
        $(this).parent().parent().next().slideToggle()
    });
});