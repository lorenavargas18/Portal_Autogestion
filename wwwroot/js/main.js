
(function ($) {
    "use strict";

    
    /*==================================================================
    [ Validate ]*/
    var input = $('.validate-input .input100');

    $('.validate-form').on('submit',function(){
        var check = true;

        for(var i=0; i<input.length; i++) {
            if(validate(input[i]) == false){
                showValidate(input[i]);
                check=false;
            }
        }

        return check;
    });


    $('.validate-form .input100').each(function(){
        $(this).focus(function(){
           hideValidate(this);
        });
    });

    function validate (input) {
        if($(input).attr('type') == 'email' || $(input).attr('name') == 'email') {
            if($(input).val().trim().match(/^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{1,5}|[0-9]{1,3})(\]?)$/) == null) {
                return false;
            }
        }
        else {
            if($(input).val().trim() == ''){
                return false;
            }
        }
    }

    function showValidate(input) {
        var thisAlert = $(input).parent();

        $(thisAlert).addClass('alert-validate');
    }

    function hideValidate(input) {
        var thisAlert = $(input).parent();

        $(thisAlert).removeClass('alert-validate');
    }
    
    
var modal = document.getElementById('myModal');
var btnIngresar = document.querySelector('.login100-form-btn');
var closeBtn = document.querySelector('.close');
var btnIngresarCodigo = document.getElementById('btnIngresarCodigo');
var codigoInput = document.getElementById('codigo');


btnIngresar.addEventListener('click', function() {
  modal.style.display = 'block';
});


closeBtn.addEventListener('click', function() {
  modal.style.display = 'none';
});


window.addEventListener('click', function(event) {
  if (event.target === modal) {
    modal.style.display = 'none';
  }
});

// Manejar el ingreso con el código
btnIngresarCodigo.addEventListener('click', function() {
  var codigoIngresado = codigoInput.value;
  var codigoCorrecto = "12345"; // Reemplaza esto con el código correcto

  if (codigoIngresado === codigoCorrecto) {
      alert("¡Ingreso exitoso!"); 
      window.location.href = "Bienvenida.cshtml"; 
  } else {
      alert("Código incorrecto. Inténtalo de nuevo.");
  }
});


})(jQuery);