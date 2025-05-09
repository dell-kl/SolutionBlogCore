document.addEventListener("DOMContentLoaded", async () => {
    await Cargar();
    MostrarArticulosCarrusel();
});

function MostrarArticulosCarrusel() {
    let longitud = 4;
    let indice = 0;
    let sessionData = JSON.parse(sessionStorage.getItem("data"));

    while (longitud <= sessionData.length) {
        datos = sessionData.slice(indice, longitud);
        inyectarComponentes(datos, longitud <= 4 ? "active" : "");
        indice = longitud;
        if (longitud + 4 > sessionData.length) {
            //hacer comprobacion.
            let restaComprobacion = sessionData.length - longitud;
            if (restaComprobacion == 0)
                break;
            longitud = longitud + restaComprobacion;
        }
        else 
            longitud += 4;   
    }    

    if (sessionData.length < longitud) {
        inyectarComponentes(sessionData, "active");
    }
}

async function Cargar() {
    let url = "Client/Home/ConsultarArticulos";
    let consulta = await fetch(url, {
        method: "GET"
    });
    let salida = await consulta.json();
    sessionStorage.setItem("data", JSON.stringify(salida.data));
}


const inyectarComponentes = (datos, active) => {
    let htmlArticulo = '';
    datos.forEach(dato => {
        htmlArticulo += `
                <div class="col-3 p-0 card rounded-2 border border-warning-subtle shadow-sm"> 
                    <div> 
                        <img height="250" 
                            class="card-img-top rounded-top-2" 
                            src="${dato.articulo_rutaImagen}" 
                            alt="Imagen de un articulo especifico" /> 
                    </div> 
                    <div class="card-body d-flex flex-column gap-2 justify-content-around"> 
                        <div>
                            <h5 class="fw-bold fs-5">${dato.articulo_nombre}</h5> 
                        </div> 
                        <div style="height: 5.5rem;" class="overflow-hidden fs-5"> 
                            ${dato.articulo_descripcion}
                        </div> 
                        <div class="mt-2"> 
                            <a
                            href="Client/Home/Details/${dato.articulo_id}"
                            class="btn btn-primary"><i class="fa-solid fa-newspaper"></i> Visitar Articulo</a> 
                        </div> 
                    </div> 
                </div> `;
    })
    let divGeneral = `
        <div class="carousel-item ${active} gap-1">
            <div class="d-flex gap-1">
                ${htmlArticulo}
            </div>
        </div>
        `;

    document.getElementById("carrusel-articulos").innerHTML += divGeneral;
};