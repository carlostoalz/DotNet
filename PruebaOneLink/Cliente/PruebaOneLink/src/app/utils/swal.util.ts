import Swal ,{ SweetAlertResult } from 'sweetalert2'

export class SwalUtil {
    Espere(){
        Swal.fire({
            title: 'Espere.',
            text: 'Guardando información.',
            icon: "info",
            allowOutsideClick: false
        });
        Swal.showLoading();
    }

    Alerta( titulo: string, text: string ){
        Swal.fire({
            title: titulo,
            text: text,
            icon: "warning"
        });
    }

    Error(texto: string){
        Swal.fire({
            title: 'Error',
            text: texto,
            icon: "error"
        });
    }

    Errors( ex: any ) {
        
        let err: string = "";

        if ( ex.error ) {            
            if ( ex.error.mensaje )  {
                err += ex.error.mensaje + '\n';
                if ( ex.error.errors.message ) {
                    err += ex.error.errors.message + '\n';
                }
            } else {
                err = ex.message;
            }        
        } else {
            err = ex.message;
        }


        Swal.fire({
            title: 'Error',
            text: err,
            icon: "error"
        });
    }

    Pregunta(pregunta: string, texto: string) : Promise<SweetAlertResult>{
        return Swal.fire({
            title: pregunta,
            text: texto,
            icon: 'question',
            showConfirmButton: true,
            showCancelButton: true
        });
    }

    Exitoso( titulo: string, text: string ){
        return Swal.fire({
            title: titulo,
            text: text,
            icon: 'success'
        });
    }

    InputText( titulo: string, text: string ) {
        
        return Swal.fire({
            title: titulo,
            text: text,
            icon: 'info',
            input: 'text',
            showCancelButton: true,
        });

    }

    Errorservicio(ex?: any, mensaje?: string, pila?: string, code?: number) {
    
        let titulo: string = "Error";

        if (ex) {
            mensaje = ex.message;
        }else {

            if (code) {
                titulo = code.toString();
            }
    
            if (pila) {
                mensaje = `${mensaje}\n${pila}`;
            }
        }

        return Swal.fire({
            title: titulo,
            text: mensaje,
            icon: "error"
        });
    }
}