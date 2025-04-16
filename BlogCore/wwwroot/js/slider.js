document.addEventListener("DOMContentLoaded", cargarTabla);

function cargarTabla() {
    new DataTable('#tbl-articulos', {
        "columns": [
            { "data": "slider_id", "width": "2%" },
            { "data": "slider_nombre", "width": "15%" },
            {
                "data": "slider_estado",
                "render": function (data) {

                    if (!data) {
                        resultado = `<div>
                            <i
                                class="fa-solid
                                fa-eye-slash fa-x2"
                                style="color: #e70d2e;font-size:25px;"></i>
                        </div>`;
                    } else {
                        resultado = `<div>
                           <i class="fa-solid fa-eye fa-x2" style="color: #1dd110;font-size:25px"></i>
                        </div>`;
                    }

                    return resultado;
                },
                "width": "2%"
            },
            {
                "data": "slider_rutaImagen",
                "render": function (data) {
                    return `<div>
                        <img src="${data}" width=200 height=150/>
                    </div>`;
                },
                "width": "10%"
            },
            {
                "data": "slider_id",
                "render": function (data) {
                    return `<div class="text-center d-flex flex-column">
                                <a href="/Admin/Sliders/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer; width:140px;">
                                <i class="far fa-edit"></i> Editar
                                </a>
                                &nbsp;
                                <a onclick="Delete('/Admin/Sliders/Delete/${data}')" class="btn btn-danger text-white" style="cursor:pointer; width:140px;">
                                <i class="far fa-trash-alt"></i> Borrar
                                </a>
                            </div>
                            `;
                }, "width": "10%"
            }
        ],
        ajax: {
            "url": "/admin/sliders/getAll",
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

async function Actualizando(url, estado) {
    
}

async function Delete(url) {
    let resultado = await Swal.fire({
        icon: 'warning',
        title: 'Eliminar Articulo',
        text: 'Deseas eliminar el articulo permanentemente?',
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