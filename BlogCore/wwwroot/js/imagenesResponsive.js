/**
 * Este script de aqui nos permitira que las imagenes que se encuentran en la seccion
 * de detalles del producto se puedan ir cambiando de manera dinamica cuando se pase
 * el mouse por encia de las imagenes que son pequenas y se muestren en el recurado 
 * grande.
 */

document.addEventListener("DOMContentLoaded", () => {
    cargando();
    escuchandoImagenesModal();
    escucnandoImagnesVideo();
});

function cargando() {
    let imagenes = document.querySelectorAll("#imagen_responsive");
    imagenes.forEach(imagen => {
        if (imagen.tagName == "DIV") {
            imagen.onclick = (e) => {
                document.getElementById("Ym90b24gbW9kYWwgZGV0YWxsZXMgdmlkZW9zIGUgaW1hZ2VuZXMK").click();
            }
        }
        imagen.addEventListener("mouseover", (e) => {
            let enlaceImagen = e.target.src == undefined ? e.target.parentNode.firstElementChild.src : e.target.src;
            document.getElementById("tarima_imagen_responsive").src = enlaceImagen;
        });
    });
}

function escuchandoImagenesModal() {

    document.querySelectorAll(".modal-proyeccion-imagen").forEach(imagen => {
        imagen.onclick = (e) => {
            let rutaImagen = e.target.src;
            document.getElementById("imagen-presentacion-lZ3RVCk1lRUVVJWZVZkWE1IaFpWMDVIWWpOd1").src = rutaImagen;
        };
    });

}

function escucnandoImagnesVideo() {
    document.querySelectorAll(".modal-proyeccion-video").forEach(imagen => {
        imagen.onclick = (e) => {
            let rutaImagen = e.target.id;
            console.log(rutaImagen);
            document.getElementById("imagen-presentacion-aW1hZ2VuIHZpZGVvIHByZXNlbnRhY2lvbiB0YXJpbWEK").src = rutaImagen;
        };
    });
}