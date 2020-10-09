import { Injectable } from '@angular/core';
import { Service } from '../service.base';
import { IServiceCRUD } from '../../interfaces/IServiceCRUD';
import { SubArea } from '../../models/SubArea.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { URL_SERVICIOS } from '../../config/appConfig';
import { map, catchError } from 'rxjs/operators';
import { IResultado } from '../../interfaces/IResultado';

@Injectable({
  providedIn: 'root'
})
export class SubAreasService extends Service implements IServiceCRUD<SubArea> {

  constructor(
    public http: HttpClient
  ) { 
    super();
  }

  obtener(): Observable<any> {
    return this.http.get(`${ URL_SERVICIOS }/SubArea/Obtener`)
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

  buscar(idArea: number): Observable<any> {
    return this.http.get(`${ URL_SERVICIOS }/SubArea/Buscar?idArea=${idArea}`)
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

  actualizar(entity: SubArea): Observable<any> {
    return this.http.post(`${ URL_SERVICIOS }/SubArea/Actualizar`, entity)
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
    return this.http.delete(`${ URL_SERVICIOS }/SubArea/Eliminar?id=${id}`)
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
