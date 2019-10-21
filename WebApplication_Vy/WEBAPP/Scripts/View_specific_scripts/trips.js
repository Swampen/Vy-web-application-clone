$(function () {
    $("#trips").hide();
    console.log(trip)

    const people = { Adult: trip.Adult, Child: trip.Child, Student: trip.Student, Senior: trip.Senior }
    const variables = {
        numTripPatterns: 10,
        from: {
            place: trip.Departure_StationId
        },
        to: {
            place: trip.Arrival_StationId
        },
        dateTime: trip.Date + "T" + trip.Time,
        arriveBy: false,
        modes: [
            "rail",
            "foot",
            "bus"
        ],
        transportSubmodes: [
            {
                transportMode: "rail",
                transportSubmodes: [
                    "international",
                    "interregionalRail",
                    "local",
                    "longDistance",
                    "nightRail",
                    "regionalRail",
                    "touristRailway"
                ]
            },
            {
                transportMode: "bus",
                transportSubmodes: [
                    "railReplacementBus"
                ]
            }
        ],
        maxPreTransitWalkDistance: 2000,
        walkSpeed: 1.3,
        minimumTransferTime: 120,
        banned: {
            authorities: "FLT:Authority:FLT"
        }
    }


    //Querry to entur api
    let itineraries = "";
    fetch("https://api.entur.io/sales/v1/offers/search/graphql", {
        "credentials": "omit", "headers": { "accept-language": "nob", "content-type": "application/json", "entur-pos": "Vy lookalike", "et-client-id": "OsloMet - Webapplication course group 41", "et-client-name": "OsloMet - Webapplication course group 41", "sec-fetch-mode": "cors", "x-correlation-id": "146537f8-5beb-47d6-a50f-02b6825909d3" }, "referrerPolicy": "same-origin",
        "body": `{"query": 
            "query tripPatterns( $numTripPatterns:Int!, $from:Location!, $to:Location!, $dateTime:DateTime!, $arriveBy:Boolean!, $modes:[Mode]!, $transportSubmodes:[TransportSubmodeFilter], $maxPreTransitWalkDistance:Float, $walkSpeed:Float, $minimumTransferTime:Int, $useFlex:Boolean, $banned:InputBanned, $whiteListed:InputWhiteListed ){ trip( numTripPatterns: $numTripPatterns wheelchair: false from: $from to: $to dateTime: $dateTime arriveBy: $arriveBy modes: $modes transportSubmodes: $transportSubmodes maxPreTransitWalkDistance: $maxPreTransitWalkDistance walkSpeed: $walkSpeed minimumTransferTime: $minimumTransferTime useFlex: $useFlex banned: $banned whiteListed: $whiteListed ) { tripPatterns { startTime endTime duration distance legs { ...legFields } } } } fragment legFields on Leg { mode aimedStartTime aimedEndTime transportSubmode expectedStartTime expectedEndTime realtime distance duration interchangeFrom { ...interchangeFields } interchangeTo { ...interchangeFields } toEstimatedCall { ...toEstimatedCallFields } fromEstimatedCall { ...fromEstimatedCallFields } pointsOnLink { points length } fromPlace { ...placeFields } toPlace { ...placeFields } intermediateQuays { id name stopPlace { ...stopPlaceFields } } authority { id name url } operator { id name url } line { ...lineFields } transportSubmode serviceJourney { ...serviceJourneyFields } fromEstimatedCall { date } intermediateEstimatedCalls { ...intermediateEstimatedCallFields } situations { ...situationFields } ride } fragment lineFields on Line { publicCode name transportSubmode id flexibleLineType bookingArrangements { bookingMethods bookingNote minimumBookingPeriod bookingContact { phone url } } } fragment interchangeFields on Interchange { staySeated guaranteed } fragment toEstimatedCallFields on EstimatedCall { forBoarding requestStop forAlighting destinationDisplay { frontText } notices { text } } fragment fromEstimatedCallFields on EstimatedCall { forBoarding requestStop forAlighting destinationDisplay { frontText } notices { text } } fragment placeFields on Place { name latitude longitude quay { id name stopPlace { ...stopPlaceFields } publicCode } } fragment serviceJourneyFields on ServiceJourney { id publicCode journeyPattern { line { transportSubmode notices { text } } notices { text } } notices { text } } fragment intermediateEstimatedCallFields on EstimatedCall { quay { id name stopPlace { id } } forAlighting forBoarding requestStop cancellation aimedArrivalTime expectedArrivalTime actualArrivalTime aimedDepartureTime expectedDepartureTime actualDepartureTime } fragment stopPlaceFields on StopPlace { id name description tariffZones { id } parent { name id } } fragment situationFields on PtSituationElement { situationNumber summary { value } description { language value } detail { value } validityPeriod { startTime endTime } reportType infoLinks { uri label } }",
	        "variables":${JSON.stringify(variables)}}`,
        "method": "POST", "mode": "cors"
    }).then(data => data.json()).then(data => {
        //Loads the result  
        itineraries = data.data.trip.tripPatterns;

        if (itineraries.length === 0) {
            $("#alert").show();
        }
        console.log(itineraries);

        //For each departure
        $.each(itineraries, function (i, value) {
            //Finds how many changes it has
            let steps = value.legs;
            let lastTrain = 0;
            let changes = -1;
            for (let i in steps) {
                if (steps[i].mode == "rail") {
                    lastTrain = i;
                    changes++;
                }
            }

            //Alternative price
            //$.ajax({
            //    url: "https://api.entur.io/sales/v1/offers/search/trip/trip-pattern/" + value.id,
            //    type: 'GET',
            //   'entur-pos': "Vy lookalike",
            //    'et-client-id': "OsloMet - Webapplication course group 41",
            //    'et-client-name': "OsloMet - Webapplication course group 41",
            //    contentType: "application/json;charset=utf-8",
            //    success: function (response) {
            //        console.log(response.offers[0].salesPackageConfig.prices[0].amount)
            //    }
            //});
            console.log(value.id)
            //Price section
            let price = 0;
            let originalPrice = 0;
            fetch("https://europe-west1-entur-prod.cloudfunctions.net/tripDetail/v1/trip-patterns", {
                "credentials": "omit", "headers": { "accept-language": "nob", "content-type": "application/json", "entur-pos": "Jurney Planner", "et-client-id": "OsloMet - Webapplication course group 39", "et-client-name": "entur-client-web", "sec-fetch-mode": "cors", "x-correlation-id": "c8711541-ef79-4091-b20b-53c66252c4f8" }, "referrerPolicy": "same-origin", "body":
                    "{\"tripPatternIds\":[\"" + value.id + "\"" + "],\
                    \"travellers\":[{\"count\":1,\"userTypes\":[\"ADULT\"],\"name\":\"Voksen\"}]}", "method": "PATCH", "mode": "cors"
            }).then(data => data.json()).then(data => {



                originalPrice = data.price;

                const priceText = ['OFFERS_SOLD_OUT', 'SEATING_INFO_UNAVAILABLE'];
                if (priceText.includes(originalPrice)) {
                    $("#totPrice" + data.tripPatternId).text("No seates available");
                    $("#totPrice" + data.tripPatternId).append(`<input type='text' hidden name=Price value='0'>`);

                } else {
                    let priceDetails = `<div class='row'>
                                        <div class='w-100 text-center'>
                                            <button type='button' class='price-btn p-2 btn btn-primary'>Show price details</button>
                                        </div >
                                    </div>
                                    <div style='display: none;'>`

                    let calculated = 0;
                    $.each(people, function (p, ammount) {
                        if (ammount > 0) {
                            if (p === "Adult") {
                                calculated = originalPrice;
                                priceDetails += "<div class='row'>\
                                                    <div class='col text-center ml-2 mr-2'>" + ammount + " " + p + "  -  " + originalPrice + ",-</div>\
                                                </div>";
                                price += calculated * ammount;
                            } else if (p === "Child" || p === "Senior") {
                                calculated = parseInt(originalPrice * 0.5, 10);
                                priceDetails += "<div class='row'>\
                                                    <div class='col text-center ml-2 mr-2'>" + ammount + " " + p + "  -  " + calculated + ",-</div>\
                                                </div>"
                                price += calculated * ammount;
                            } else if (p === "Student") {
                                calculated = parseInt(originalPrice * 0.75, 10);
                                priceDetails += "<div class='row'>\
                                                    <div class='col text-center ml-2 mr-2'>" + ammount + " " + p + "  -  " + calculated + ",-</div>\
                                                </div>"
                                price += calculated * ammount;
                            }
                        }

                    });
                    priceDetails += "</div>"//end price section
                    $("#priceDetails" + data.tripPatternId).append(priceDetails);

                    $("#totPrice" + data.tripPatternId).text(price + " kr");
                    $("#totPrice" + data.tripPatternId).append(`<input type='text' hidden name=Price value='${price}'>`);
                    $("#" + data.tripPatternId).children().eq(1).children(".btn-select").children("button").attr("disabled", false);
                }

            });

            //Sets suitable text to the ammount of changes
            let changesText;
            if (changes == 0) {
                changesText = "Direct";
            } else if (changes == 1) {
                changesText = "1 change";
            } else {
                changesText = changes + " changes";
            }

            const startTime = new Date(value.startTime);
            const endTime = new Date(value.endTime);
            let seconds = (endTime.getTime() - startTime.getTime()) / 1000;

            let days = Math.floor(seconds / (3600 * 24));
            seconds -= days * 3600 * 24;
            let hours = Math.floor(seconds / 3600);
            seconds -= hours * 3600;
            let minutes = Math.floor(seconds / 60);

            const duration = (days !== 0 ? days + "d " : "") + (hours !== 0 ? hours + "h " : "") + (minutes !== 0 ? minutes + "m " : "")

            //Displays the result
            $("#trips").append(`<div class='container'><form action='/home/trips' method='POST'>
                        <div id='${value.id}' class='border-top border-bottom rounded form-group'>
                            <div class='row text-center button-row p-3 '>
                                <div class='col font-weight-bold'>
                                    <input type='text' hidden name='Departure_Time' value='${value.startTime.match("[0-9]{2}:[0-9]{2}")}'>${value.startTime.match("[0-9]{2}:[0-9]{2}")}<span> - </span>
                                    <input type='text' hidden name='Arrival_Time' value='${value.endTime.match("[0-9]{2}:[0-9]{2}")}'>${value.endTime.match("[0-9]{2}:[0-9]{2}")}
                                </div>
                                <div class='col' ><input type='text' hidden name=Duration value='${duration}'>${duration}</div>
                                <div class='col' ><input type='text' hidden name=Train_Changes value='${changesText}'>${changesText}</div>
                                <div class='col' id=totPrice${value.id}>
                                    
                                    <div class="spinner-grow text-success" role="status">
                                      <span class="sr-only">Loading...</span>
                                    </div>
                                </div>
                                <div><input type='text' hidden name=Date value='${value.startTime.split("T")[0]}'></div>
                                <div><input type='text' hidden name=Departure_Station value='${value.legs[0].fromPlace.name}'></div>
                                <div><input type='text' hidden name=Arrival_Station value='${value.legs[lastTrain].toPlace.name}'></div>
                                <div><input type='checkbox' hidden name=Round_Trip value='${trip.Round_Trip ? true : false}' checked></div>
                            </div>\
                            <div id='hidden_${i}' class='row mt-4' style='display: none;'>
                            </div>\
                        </div>\
                    </form></div>`);

            //Appends more info, but hidden
            var hidden_content = "<div class='col ml-5'>"
            console.log(value.legs)
            if (value.legs == 0) {
            }
            $.each(value.legs, function (j, leg) {
                if (leg.mode == "rail") {
                    if (leg.intermediateQuays.length == 0) {
                        hidden_content += "<div class='row font-weight-bold'>" + leg.fromPlace.name + "</div>"
                        hidden_content += "<div class='border-left border-success pl-4'>\<div class='row'><p>1 stop</p></div>"
                        hidden_content += "</div></div>"
                        hidden_content += "<div class='row mb-3 font-weight-bold'>" + leg.toPlace.name + "</div>"
                    }
                    $.each(this.intermediateQuays, function (k, stop) {
                        if (leg.intermediateQuays.length == 1) {
                            hidden_content += "<div class='row font-weight-bold'>" + value.legs[j].fromPlace.name + "</div>"
                            hidden_content += "<div class='border-left border-success pl-4'>\<div class='row'><a href=''>" + (leg.intermediateQuays.length + 1) + " stops</a></div>"
                            hidden_content += "<div class='col ml-0' style='display: none;'><div class='pl-3'>"
                            hidden_content += "<div class='row'>" + /([\s\S]*?)(stasjon)/g.exec(stop.name)[1] + "</div>"
                            hidden_content += "</div></div></div>"
                            hidden_content += "<div class='row mb-3 font-weight-bold'>" + value.legs[j].toPlace.name + "</div>"
                        }
                        else if (k == 0) {
                            hidden_content += "<div class='row font-weight-bold'>" + value.legs[j].fromPlace.name + "</div>"
                            hidden_content += "<div class='border-left border-success pl-4'>\<div class='row'><a href=''>" + (leg.intermediateQuays.length + 1) + " stops</a></div>"
                            hidden_content += "<div class='col ml-0' style='display: none;'><div class='pl-3'>"
                        } else if (k == leg.intermediateQuays.length - 1) {
                            hidden_content += "</div></div></div>"
                            hidden_content += "<div class='row mb-3 font-weight-bold'>" + value.legs[j].toPlace.name + "</div>"
                        } else {
                            hidden_content += "<div class='row'>" + /([\s\S]*?)(stasjon)/g.exec(stop.name)[1] + "</div>"
                        }
                    })
                }
            });
            hidden_content += "</div>";
            hidden_content += `<div id="priceDetails${value.id}" class="col m-3"></div>`;
            hidden_content += `<div class='col text-right m-3 btn-select'> <button type='submit' disabled class='btn btn-success'>Select</button></div>`;

            $("#hidden_" + i).append(hidden_content);

        });//end foreach departure

        //Hides the loading spinner and shows the result
        $("#loading").remove();
        $("#trips").show();
    });

    $("#trips").on("mouseenter", ".button-row", function () {
        var row = $(this)
        row.css('cursor', 'pointer')
        if (!row.hasClass("clicked")) {
            row.css('background-color', '#e6e6e6')
        }
    });

    $("#trips").on("mouseleave", ".button-row", function () {
        var row = $(this)
        if (!row.hasClass("clicked")) {
            row.css('background-color', '#ffffff')
        }
    });

    $("#trips").on("click", ".button-row", function () {
        var row = $(this)
        if (row.hasClass("clicked")) {
            row.removeClass("clicked");
            row.css('background-color', '#e6e6e6')
        } else {
            row.addClass("clicked");
        }
        row.next().slideToggle()
    })

    $("#trips").on("click", "a", function (e) {
        e.preventDefault();
        $(this).parent().next().slideToggle()
    })

    $("#trips").on("click", ".price-btn", function () {
        $(this).parent().parent().next().slideToggle()
    });
});