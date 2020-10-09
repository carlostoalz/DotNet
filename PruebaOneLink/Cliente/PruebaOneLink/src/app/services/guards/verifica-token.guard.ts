import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { UsuariosService } from '../usuarios/usuarios.service';
import { SwalUtil } from '../../utils/swal.util';

@Injectable({
  providedIn: 'root'
})
export class VerificaTokenGuard implements CanActivate {

  private swal: SwalUtil = new SwalUtil();

  constructor(
    public _us: UsuariosService,
    public router: Router
  ) {}

  canActivate(): Promise<boolean> | boolean {

    let expirado: boolean = this._us.userToken.expiration >= new Date();
    if (expirado) {
      this.swal.Alerta( 'Token expiró', 'Debe loguearse nuevamente' );
      this.router.navigate(['/login']);
      return false;
    }

    return true;
  }
  
}
