
$(document).ready(function() {
    // Maneja el clic en el botón "Enviar código"
    $('.login100-form-btn').on('click', function(event) {
        event.preventDefault();

        // Obtiene el número de cédula del campo de entrada
        var cedula = $('input[name="card"]').val();

        // Realiza la solicitud para enviar el código de verificación
        $.post(`/Home/EnviarCodigo?cedula=${cedula}`, function(data) {
            // Agrega el contenido del modal al cuerpo del documento
            $('body').append(data);

            // Muestra el modal
            var modal = $('#myModal');
            modal.show();

            // Maneja el clic en el botón "Ingresar"
            $('#btnIngresarCodigo').on('click', function() {
                var codigo = $('#codigo').val();

                // Realiza la solicitud para validar el código
                $.post(`/Home/ValidarCodigo?codigo=${codigo}`, function(resultado) {
                    // Actualiza el contenido del modal con el resultado de la validación
                    modal.find('.modal-content').html(resultado);

                    // Si la validación fue exitosa, redirige al usuario a la aplicación
                    if (resultado.includes('éxito')) {
                        window.location.href = '/Home/App';
                    }
                });
            });

            // Maneja el clic en el botón "Cerrar" del modal
            $('.close').on('click', function() {
                modal.hide();
                modal.remove();
            });
        });
    });
});