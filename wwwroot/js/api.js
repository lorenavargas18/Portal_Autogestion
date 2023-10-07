 document.getElementById("enviarCodigoBtn").addEventListener("click", function() {
        var numeroCedula = document.getElementById("numeroCedula").value;

        // Hacer una solicitud AJAX al endpoint del controlador
        fetch(`/Home/ConsultarApi?numeroCedula=${numeroCedula}`, {
            method: "GET",
            headers: {
                "Content-Type": "application/json"
            }
        })
        .then(response => response.json())
        .then(data => {
            console.log(data); // Muestra la respuesta en la consola del navegador
            // Hacer algo con la respuesta si es necesario
        })
        .catch(error => {
            console.error(error); // Muestra cualquier error en la consola del navegador
        });
    });