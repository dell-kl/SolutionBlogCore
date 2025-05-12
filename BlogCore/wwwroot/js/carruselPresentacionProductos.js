document.addEventListener("DOMContentLoaded", async () => {
    await CargarProducto();
    MostrarProductosCarrusel();
});

function MostrarProductosCarrusel() {
    let longitud = 4;
    let indice = 0;
    let sessionData = JSON.parse(sessionStorage.getItem("productos"));
    while (longitud <= sessionData.length) {
        datos = sessionData.slice(indice, longitud);
        inyectarComponenteProducto(datos, longitud <= 4 ? "active" : "");
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
        inyectarComponenteProducto(sessionData, "active");
    }
}

async function CargarProducto() {
    let url = "Client/Home/ConsultarProductos";
    let consulta = await fetch(url, {
        method: "GET"
    });
    let salida = await consulta.json();
    sessionStorage.setItem("productos", JSON.stringify(salida.data));
}

const inyectarComponenteProducto = (datos, active) => {
    let htmlProducto = '';
    datos.forEach(dato => {
        htmlProducto += `
                <div class="col-3 p-0 card rounded-2 border border-warning-subtle shadow-sm"> 
                    <div> 
                        <img height="250" 
                            class="card-img-top rounded-top-2" 
                            src="${dato.imagenesProducto[0].imagenesProducto_ruta}" 
                            alt="Imagen de un articulo especifico" /> 
                    </div> 
                    <div class="card-body d-flex flex-column gap-2 justify-content-around"> 
                        <div>
                            <h5 class="fw-bold fs-5">${dato.producto_nombre}</h5> 
                        </div> 
                        <div style="height: 5.5rem;" class="overflow-hidden fs-5"> 
                            ${dato.producto_descripcion}
                        </div> 
                        <div class="mt-2"> 
                            <a
                            href="Client/Home/DetailsProduct/${dato.producto_id}"
                            class="btn btn-primary"><i class="fa-solid fa-newspaper"></i> Visitar Articulo</a> 
                        </div> 
                    </div> 
                </div> `;
    })
    let divGeneral = `
        <div class="carousel-item ${active} gap-1">
            <div class="d-flex gap-1">
                ${htmlProducto}
            </div>
        </div>
        `;
    document.getElementById("carrusel-productos").innerHTML += divGeneral;
};