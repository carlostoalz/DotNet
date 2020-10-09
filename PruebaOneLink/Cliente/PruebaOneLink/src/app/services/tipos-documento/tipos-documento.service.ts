import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { IServiceCRUD } from '../../interfaces/IServiceCRUD';
import { TipoDocumento } from '../../models/TipoDocumento.model';
import { URL_SERVICIOS } from '../../config/appConfig';
import { IResultado } from '../../interfaces/IResultado';
import { Service } from '../service.base';

@Injectable({
  providedIn: 'root'
})
export class TiposDocumentoService extends Service implements IServiceCRUD<TipoDocumento> {

  constructor(
    public http: HttpClient
  ) { 
    super();
  }

  obtener(): Observable<any> {
    
    return this.http.get(`${ URL_SERVICIOS }/TipoDocumento/Obtener`)
    .pipe(
      map( (resp: IResultado) => {
        
        if (resp.exitoso) {
          return resp.datos;
        }
        
        this.MostrarError(null, resp.error.mensaje, resp.error.pila, resp.codigoRespuesta);
      }),
      catchError( (err: any) => {

        this.MostrarErrors(err);
        return Observable.throw( err );

      })
    );

  }

  seleccionar(id: number): Observable<any> {
    throw new Error("Method not implemented.");
  }

  actualizar(entity: TipoDocumento): Observable<any> {
    return this.http.post(`${URL_SERVICIOS}/TipoDocumento/Actualizar`, entity)
    .pipe(
      map( (resp: IResultado) => {
        
        if (resp.exitoso) {
          return resp.datos;
        }
        
        this.MostrarError(null, resp.error.mensaje, resp.error.pila, resp.codigoRespuesta);
      }),
      catchError( (err: any) => {

        this.MostrarErrors(err);
        return Observable.throw( err );

      })
    );
  }

  eliminar(id: number): Observable<any> {
    return this.http.delete(`${URL_SERVICIOS}/TipoDocumento/Eliminar?id=${id}`)
    .pipe(
      map( (resp: IResultado) => {
        
        if (resp.exitoso) {
          return resp.datos;
        }
        
        this.MostrarError(null, resp.error.mensaje, resp.error.pila, resp.codigoRespuesta);
      }),
      catchError( (err: any) => {

        this.MostrarErrors(err);
        return Observable.throw( err );

      })
    );
  }
}
