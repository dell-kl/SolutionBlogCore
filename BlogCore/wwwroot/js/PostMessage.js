(() => {
    document.addEventListener("DOMContentLoaded", () => {
        const btnsPostMessage = document.querySelectorAll(".btn_new_post_message");
        btnsPostMessage.forEach(btnMessage => {
            btnMessage.onclick = (evento) => {
                let identificador = evento.target.id;
                console.log(identificador);
            };
        });
    });
})();