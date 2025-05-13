/**
 * Este script de aqui nos permitira que las imagenes que se encuentran en la seccion
 * de detalles del producto se puedan ir cambiando de manera dinamica cuando se pase
 * el mouse por encia de las imagenes que son pequenas y se muestren en el recurado 
 * grande.
 */

document.addEventListener("DOMContentLoaded", () => {
    cargando();
});

function cargando() {
    let imagenes = document.querySelectorAll("#imagen_responsive");
    imagenes.forEach(imagen => {
        imagen.addEventListener("mouseover", (e) => {
            let enlaceImagen = e.target.src;
            document.getElementById("tarima_imagen_responsive").src = enlaceImagen;
        });
    });
}