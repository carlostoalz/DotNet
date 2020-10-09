import { Injectable } from '@angular/core';
import { Service } from '../service.base';
import { IServiceCRUD } from '../../interfaces/IServiceCRUD';
import { Empleado } from '../../models/Empleado.model';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { URL_SERVICIOS } from '../../config/appConfig';
import { catchError, map } from 'rxjs/operators';
import { IResultado } from '../../interfaces/IResultado';

@Injectable({
  providedIn: 'root'
})
export class EmpleadosService extends Service implements IServiceCRUD<Empleado> {

  constructor(
    public http: HttpClient
  ) { 
    super();
  }

  obtener(): Observable<any> {
    throw new Error("Method not implemented.");
  }

  obtenerEmpleados(paginaActual: number = 1, tamanoPagina: number = 10, termino = null): Observable<any> {

    let url: string = `${URL_SERVICIOS}/Empleado/Obtener?pPaginaActual=${paginaActual}&pTamanoPagina=${tamanoPagina}`;
    url = termino ? `${url}&ptermino=${termino}` : url;

    return this.http.get(url)
    .pipe(
      map( (resp: IResultado) => {
        
        if (resp.exitoso) {
          return resp;
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

  actualizar(entity: Empleado): Observable<any> {
    return this.http.post(`${URL_SERVICIOS}/Empleado/Actualizar`, entity)
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
    return this.http.delete(`${URL_SERVICIOS}/Empleado/Eliminar?id=${id}`)
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
