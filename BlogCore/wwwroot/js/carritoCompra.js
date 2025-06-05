$(document).ready(function () {

    $('#button_place_page_xR3hhYUZKVk5XOVZiVEExVGxaUmQxcEhkRnBWWmgKYTBwb1ZGVmFhMk14YkRaU2F6VlNWMFZL').on('click', function () {
        $.ajax({
            url: '/Client/GenerarOrdenCompra/GenerarOrden',
            method: 'POST'
        }).done((resultado) => {
            window.location.href = resultado.data;
        }).error((error) => {
            Swal.fire({
                icon: "error",
                title: "Proceso Compra",
                confirmButtonText: "Entendido",
                text: error.mensaje
            });
        });
    });

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

                $("#precio_bW9zdHJhcl9wcmVjaW9fZmluYWwK").get()[0].innerText =
                    "$" + Number(resultado.precioTotal).toLocaleString('es-ES', { minimumFractionDigits: 2, maximumFractionDigits: 2 });

                let HTML = ``;
                for (let indice = 0; indice < resultado.preciosProductos.length; indice++) {
                    HTML += `
                    <div class="d-flex justify-content-between">
                        <h5 class="text-uppercase">Producto ${indice + 1}</h5>
                        <h5>$${Number(resultado.preciosProductos[indice]).toLocaleString('es-ES', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</h5>
                    </div>
                    `;
                }

                $("#1WamFXOXpJR1JsSUdOaFpHRWdUJ3").get()[0].innerHTML = HTML;
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

                $("#precio_bW9zdHJhcl9wcmVjaW9fZmluYWwK").get()[0].innerText =
                    "$" + Number(resultado.precioTotal).toLocaleString('es-ES', { minimumFractionDigits: 2, maximumFractionDigits: 2 });

                let HTML = ``;
                for (let indice = 0; indice < resultado.preciosProductos.length; indice++) {
                    HTML += `
                    <div class="d-flex justify-content-between">
                        <h5 class="text-uppercase">Producto ${indice + 1}</h5>
                        <h5>$${ Number(resultado.preciosProductos[indice]).toLocaleString('es-ES', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</h5>
                    </div>
                    `;
                }

                $("#1WamFXOXpJR1JsSUdOaFpHRWdUJ3").get()[0].innerHTML = HTML;
            })
            .fail((resultado) => {
                toastr.error(resultado.data);
            });

        });

    });

});
