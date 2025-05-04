document.addEventListener('DOMContentLoaded', cargarTabla);
function cargarTabla() {

    new DataTable('#tbl-productos', {
        "columns": [
            { "data": "producto_id", "width": "2%" },
            { "data": "producto_nombre", "width": "10%" },
            { "data": "producto_precio", "width": "10%" },
            { "data": "producto_stock", "width": "10%" },
            {
                "data": "producto_disponiblidad",
                "render": function (data) {
                    return `<span>${data ? '<i class="fa-solid fa-check-to-slot fs-1" title="Disponible y Visible" style="color: #9BEC00;"></i>' : '<i class="fa-solid fa-eye-slash fs-1" title="No Visible" style="color: #ed1217;"></i>'}</span>`;
                },
                "width": "10%"
            },
            {
                "data": null,
                "render": function (data, type, row) {

                    let valor = 0;
                    if (data.producto_Esdescuento && data.producto_descuento > 0) {
                        valor = (data.producto_precio * data.producto_descuento) / 100;
                        valor = data.producto_precio - Math.round(valor);
                    }
                    else {
                        valor = data.producto_precio;
                    }

                    return `<span>${valor}</span>`;
                },
                "width": "15%"
            },
            {
                "data": "producto_descuento",
                "render": function (data) {
                    return `<strong>${data}%</strong>`;
                },
                "width": "10%"
            },
            {
                "data": "producto_id",
                "render": function (data) {
                    return `<div>
                        <a class="btn btn-danger" onclick=Delete('/admin/producto/deleteproduct/${data}')><i class="fa-solid fa-trash"></i> Eliminar</a>
                        <a 
                            href="/admin/producto/update/${data}"
                            class="btn btn-warning"><i class="fa-solid fa-pen"></i> Editar</a>
                    </div>`;
                },
                "width": "30%"
            }
        ],
        ajax: {
            "url": "/admin/producto/getproductos",
            "type": "GET",
            "datatype": "json",
            "data": {
                "tipo" : "parcial"
            }
        },
        "language": {
            "decimal": "",
            "emptyTable": "No hay registros",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
            "infoFiltered": "(Filtrado de _MAX_ total entradas)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "width": "100%"
    });

}

async function Delete(url) {
    let resultado = await Swal.fire({
        icon: 'warning',
        title: 'Eliminar Producto',
        text: 'Deseas eliminar el producto permanentemente?',
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si, borrar!",
        closeOnconfirm: true
    });

    if (resultado.value) {
        let solicitudEliminar = await fetch(url, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        let contenido = await solicitudEliminar.json();
        let mensaje = contenido['data'];

        if (solicitudEliminar.ok) {
            toastr.success(mensaje);

            setTimeout(() => {
                window.location.reload();
            }, 3000);
        }
        else {
            toastr.error(mensaje);
        }

    }
}