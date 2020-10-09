import { Injectable } from '@angular/core';
import { Service } from '../service.base';
import { IServiceCRUD } from '../../interfaces/IServiceCRUD';
import { Area } from '../../models/Area.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { URL_SERVICIOS } from 'src/app/config/appConfig';
import { IResultado } from '../../interfaces/IResultado';
import { map, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AreasService extends Service implements IServiceCRUD<Area>{

  constructor(
    public http: HttpClient
  ) { 
    super()
  }

  obtener(): Observable<any> {
    return this.http.get(`${ URL_SERVICIOS }/Area/Obtener`)
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

  actualizar(entity: Area): Observable<any> {
    return this.http.post(`${URL_SERVICIOS}/Area/Actualizar`, entity)
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
    return this.http.delete(`${URL_SERVICIOS}/Area/Eliminar?id=${id}`)
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
