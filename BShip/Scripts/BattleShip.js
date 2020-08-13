
$(document).ready(function () {
    constructGameBoard();
});

function constructGameBoard() {
    var size = 10; var arrayOfArrays = [];
    for (var i = 0; i < gameBoardData.length; i += size) {
        arrayOfArrays.push(gameBoardData.slice(i, i + size));
    }

    $('.board-body').html("");

    $.each(arrayOfArrays, function (i, o) {
        var tr = $('<tr></tr>');
        $.each(o, function (i, o) {
            var color = o.HasShip ? o.Color : "";
            tr.append('<td class="battle-cell animate__animated animate__fadeInUpBig" style="background-color:' + color + '" data-id=' + o.Id + ' data-uid=' + o.UniqueId + '></td>');
        });
        $('.board-body').append(tr);
    });
    //add first column
    $('.board-table tbody').find('tr').each(function (i) {
        $(this).find('td').eq(0).before('<td>' + i + '</td>');
    });

}

$(document).on('click', '.battle-cell', function () {
    var uid = $(this).attr("data-uid");
    //check if there was any ship
    if (uid && uid != "null")
    {
        var cellid = $(this).attr("data-id");
        var hit = $.grep(gameBoardData, function (o) {
            return o.Id == cellid;
        });
        hit[0].Sinked = true;
        checkSinkStatus(uid);
        $(this).css("background-color", "red");
       
    }
    else {
        $(this).css("background-color", "blue");
        Swal.fire({
            position: 'top-end',
            icon: 'error',
            title: 'Missed',
            showConfirmButton: false,
            timer: 800
        })
    }

});

function checkSinkStatus(uid) {
    var hits = $.grep(gameBoardData, function (v) {
        return v.UniqueId === uid;
    });
    var sinked = true;
    $.each(hits, function (i, o) {
        if (!o.Sinked) {
            sinked = false;
            return false;
        }

    });
    if (sinked)
    {

        Swal.fire({
            icon: 'success',
            title: hits[0].ShipName + ' ship had sunk',
            showConfirmButton: false,
            timer: 2500
        });
    }
    else
        Swal.fire({
            position: 'top-end',
            icon: 'success',
            title: 'Good Shot!',
            showConfirmButton: false,
            timer: 800
        })
}


$(document).on('click', '.new-game', function () {

    Swal.fire({
        title: 'Are you sure?',
        text: "You want to start new game",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, start it!'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                type: "POST",
                url: "/Home/LoadNewGame",
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    gameBoardData = data.GameCells
                    constructGameBoard();
                },
                error: function () {
                    alert("Error occured!!")
                }
            }); 
        }
    })
   
 
});


