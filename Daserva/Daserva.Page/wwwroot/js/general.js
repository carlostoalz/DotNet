'use strict';

function AlertaSuccess() {
    swal({
        position: 'top-end',
        type: 'success',
        title: 'Cambios guardados exitosamente',
        showConfirmButton: false,
        timer: 1500
    });
}

function AlertaError(e) {
    swal({
        position: 'top-end',
        type: 'error',
        title: e,
        showConfirmButton: false,
        timer: 2500
    });
}

function AlertaErrorGuardar(e) {

    var esCalmelCase = false;

    if (e !== null
        && e.Error !== null) {
        esCalmelCase = true;

        if (e.Error.MensajeInnerException !== null
            && e.Error.MensajeInnerException.length > 0) {

            e.Error.MensajeUsuarios = e.MensajeInnerException
        };

        swal({
            position: 'top-end',
            type: 'error',
            title: 'Error ',
            text: e.Error.MensajeUsuario,
            showConfirmButton: false,
            timer: 15000
        });
    } else {
        AlertaErrorMensajeGeneral();
    }

    if (!esCalmelCase) {
        if (e !== null
            && e.error !== null) {
            if (e.MensajeInnerException !== null
                && e.MensajeInnerException.length > 0) {

                e.error.mensajeUsuario = e.MensajeInnerException;
            };

            swal({
                position: 'top-end',
                type: 'error',
                title: 'Error ',
                text: e.error.mensajeUsuario,
                showConfirmButton: false,
                timer: 15000
            });
        } else {
            AlertaErrorMensajeGeneral();
        }
    }



    console.log("Mensaje completo de error");
    console.log(e);

}