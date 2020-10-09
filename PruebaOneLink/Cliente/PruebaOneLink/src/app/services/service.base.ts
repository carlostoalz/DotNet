import { SwalUtil } from '../utils/swal.util';

export class Service {
    
    private swal: SwalUtil = new SwalUtil();

    protected MostrarError(ex?: any, mensaje?: string, pila?: string, code?: number) {
        this.swal.Errorservicio( ex, mensaje, pila, code );
    }

    protected MostrarErrors(err: any) {
        this.swal.Errors(err);
    }

}