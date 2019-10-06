$(function () {
    let TripTickets = { Trip: null, ReturnTrip: null };
    ticketData.forEach(ticket => {
        if (ticket.Round_Trip === true) {
            TripTickets.Trip = ticket;
            populateTripForm(ticket);
        } else if (ticketData.length > 1) {
            TripTickets.ReturnTrip = ticket;
            populateReturnTripForm(ticket);
            showHiddenCard(ticket);
        } else {
            TripTickets.Trip = ticket;
            populateTripForm(ticket);
        }
    });


    function showHiddenCard(ticket) {
        const hiddenCard = ` <div class="card bg-light mb-3 col-ml4" id="ticketCard" style="max-width: 18rem;">
          <div class="card-header">Your chosen return trip</div>
          <div class="card-body">
              <h5 class="card-title">
                  <span>
                      <img id="tripIcon" src='/Content/Assets/train.svg' alt="Train icon"/>
                  </span>
                  ${ticket.Departure_Station} - ${ticket.Arrival_Station}
              </h5>
              <p class="card-text">
                  <span class="font-weight-bold">Date:</span>
                  <span>${ticket.Date}</span>
              </p>
              <p class="card-text">
                  <span class="font-weight-bold">Departure:</span>
                  <span>${ticket.Departure_Time}</span>
              </p>
              <p class="card-text">
                  <span class="font-weight-bold">Arrival:</span>
                  <span>${ticket.Arrival_Time}</span>
              </p>
              <p class="card-text">
                  <span class="font-weight-bold">Travel time:</span>
                  <span>${ticket.Duration}</span>

              </p>
              <p class="card-text">
                  <span class="font-weight-bold">Price:</span>
                  <span>${ticket.Price},-</span>
              </p>
          </div>
      </div>
      `;
        $('.cards').append(hiddenCard);
    }

    function populateTripForm(ticket) {
        $('#hfDepStation').val(ticket.Departure_Station);
        $('#hfArrStation').val(ticket.Arrival_Station);
        $('#hfDepTime').val(ticket.Departure_Time);
        $('#hfArrTime').val(ticket.Arrival_Time);
        $('#hfDuration').val(ticket.Duration);
        $('#hfTrainChanges').val(ticket.Train_Changes);
        $('#hfPrice').val(ticket.Price);
        $('#hfPriceReturn').val(0);
    }

    function populateReturnTripForm(ticket) {
        $('#hfDepStationReturn').val(ticket.Departure_Station);
        $('#hfArrStationReturn').val(ticket.Arrival_Station);
        $('#hfDepTimeReturn').val(ticket.Departure_Time);
        $('#hfArrTimeReturn').val(ticket.Arrival_Time);
        $('#hfDurationReturn').val(ticket.Duration);
        $('#hfTrainChangesReturn').val(ticket.Train_Changes);
        $('#hfPriceReturn').val(ticket.Price);
    }

});



$(function () {

    $('.cards')
        .on('mouseenter', '#ticketCard', function () {
            $(this).addClass("shadow");
        })
        .on('mouseleave', '#ticketCard', function () {
            $(this).removeClass("shadow");
        });

    $("#postalcode").on('input', function () {
        const postalcode = $("#postalcode").val();
        const jsonin = {
            Postalcode: postalcode
        };

        if (postalcode.match("[0-9]{4}")) {
            $.ajax({
                url: "/home/searchzip",
                type: 'POST',
                data: JSON.stringify(jsonin),
                contentType: "application/json;charset=utf-8",
                success: function (response) {
                    console.log('setting postaltown');
                    $('#postaltown').val(response);
                }
            });
        } else {
            $('#postaltown').val("Undefined");
        }
    });

    $("#changeTrip").click(function () {
        alert("Not yet implemented");
    });

});

$(function () {
    new Cleave('#card', {
        creditCard: true,
    });

    new Cleave('#expiry', {
        date: true,
        datePattern: ['m', 'y']
    });
});


function getDuration(duration) {
    const { days, hours, minutes } = duration;
    let durationString = (days !== "0") ? days + " d " : "";
    durationString += (hours !== "0") ? hours + " h " : "";
    durationString += minutes + " m ";
    return durationString;
}