import { Observable } from 'rxjs';

export interface IServiceCRUD<T> {    
    obtener() : Observable<any>;
    seleccionar( id:number ) : Observable<any>;
    actualizar(entity:T) : Observable<any>;
    eliminar(id:number) : Observable<any>;
}