document.addEventListener("DOMContentLoaded", cargarTabla);

function cargarTabla() {
    new DataTable('#tbl-categoriasProductos', {
        "columns": [
            { "data": "categoriaProducto_id", "width": "10%" },
            { "data": "categoriaProducto_nombre", "width": "40%" },
            {
                "data": "categoriaProducto_guid",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/CategoriasProducto/Update?guid=${data}" class="btn btn-success text-white" style="cursor:pointer; width:140px;">
                                <i class="far fa-edit"></i> Editar
                                </a>
                                &nbsp;
                                <a onclick="Delete('/Admin/CategoriasProducto/Delete?guid=${data}')" class="btn btn-danger text-white" style="cursor:pointer; width:140px;">
                                <i class="far fa-trash-alt"></i> Borrar
                                </a>
                            </div>
                            `;
                }, "width": "25%"
            }
        ],
        ajax: {
            "url": "/admin/categoriasproducto/obtenercategoriasproducto",
            "type": "GET",
            "datatype": "json"
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
        title: 'Eliminar Catalogo',
        text: 'Deseas eliminar el catalogo permanentemente?',
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
            toastr.error("No se pudo eliminar la categoria");
        }

    }
}