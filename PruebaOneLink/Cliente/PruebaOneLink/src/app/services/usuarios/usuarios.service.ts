import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IUserInfo } from '../../interfaces/IUserInfo';
import { URL_SERVICIOS } from '../../config/appConfig';
import { map, catchError } from 'rxjs/operators';
import { IResultado } from '../../interfaces/IResultado';
import { IUserToken } from '../../interfaces/IUserToken';
import { IUsuario } from '../../interfaces/IUsuario';
import { Service } from '../service.base';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UsuariosService extends Service {

  userToken: IUserToken;

  constructor(
    private http: HttpClient
  ) { 
    super();
  }

  login(user: IUserInfo, recordar: boolean = false) {

    if ( recordar ) {
      localStorage.setItem( 'nombreUsuario', user.nombreUsuario );
    } else {
      localStorage.removeItem( 'nombreUsuario' );
    }

    return this.http.post(`${ URL_SERVICIOS }/Usuario/Login`, user)
    .pipe(
      map( (resp: IResultado) => {
        if (resp.exitoso) {
          this.userToken = <IUserToken>resp.datos;
          localStorage.setItem('token', this.userToken.token);
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

  registrar(user: IUsuario) {
    
    return this.http.post(`${ URL_SERVICIOS }/Usuario/Registrar`, user)
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
