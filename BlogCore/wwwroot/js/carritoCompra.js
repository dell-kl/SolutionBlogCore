$(document).ready(function () {

    $("#input_4gc3VtYXIgbWFzIGNhbnRpZGFkIGRlbCBwcm9kdWN0bwo").on('click', (evento) => {
        let id = evento.target.value;
        let formdata = new FormData();
        formdata.append("guid_Z3VpZCBwcm9kdWN0bwo", id);

        $.ajax({
            url: `/Client/CarritoCompras/IncrementAmountProduct`,
            method: 'POST',
            contentType: false,
            processData: false,
            data: formdata
        }).done((resultado) => {
            toastr.success(resultado.data);
        })
        .fail((resultado) => {
            toastr.error(resultado.data);
        });
    });

    //$("#input_4gc3VtYXIgbWFzIGNhbnRpZGFkIGRlbCBwcm9kdWN0bwo").on('click', (evento) => {
    //    let id = evento.target.value;
    //    $.ajax({
    //        url: `/Client/CarritoCompras/DecrementAmountProduct`,
    //        method: 'PUT',
    //        contentType: 'application/json; charset=utf-8',
    //        data: {
    //            "guid_Z3VpZCBwcm9kdWN0bwo": id
    //        }
    //    }).done((resultado) => {
    //        console.log(resultado);
    //    })
    //    .fail((resultado) => {
    //        console.log(resultado);
    //    });
    //});
});
