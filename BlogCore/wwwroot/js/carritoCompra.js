$(document).ready(function () {

    //section for increment value of product in shopping cart
    $.each($('input[type=hidden].EZoclZqRktkRlZyYkZkV00yUk1RMmM'), function (indice, valor) {

        $(`#${valor.id}`).on('click', (evento) => {
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

                $("#precio_bW9zdHJhcl9wcmVjaW9fZmluYWwK").text = resultado.precioTotal;
            })
            .fail((resultado) => {
                toastr.error(resultado.data);
            });

        });

    });

    $.each($('input[type=hidden].lFhrVjFKdFVrbFdWM2RMQ2c9PQo'), function (indice, valor) {

        $(`#${valor.id}`).on('click', (evento) => {
            let id = evento.target.value;
            let formdata = new FormData();
            formdata.append("guid_Z3VpZCBwcm9kdWN0bwo", id);

            $.ajax({
                url: `/Client/CarritoCompras/DecrementAmountProduct`,
                method: 'POST',
                contentType: false,
                processData: false,
                data: formdata
            }).done((resultado) => {
                toastr.success(resultado.data);

                $("#precio_bW9zdHJhcl9wcmVjaW9fZmluYWwK").text = resultado.precioTotal;
            })
            .fail((resultado) => {
                toastr.error(resultado.data);
            });

        });

    });

});
