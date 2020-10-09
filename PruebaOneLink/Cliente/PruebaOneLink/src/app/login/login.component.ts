import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { IUserInfo } from '../interfaces/IUserInfo';
import { UsuariosService } from '../services/usuarios/usuarios.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: [ './login.component.css' ]
})
export class LoginComponent implements OnInit {

  recuerdame: boolean = false;
  nombreUsuario: string = "";

  constructor(
    public _us: UsuariosService,
    public router: Router
  ) { }

  ngOnInit(): void {
  }

  ingresar( forma: NgForm ) {
    
    if ( forma.invalid ) {
      return;
    }

    let usuario: IUserInfo = { 
      nombreUsuario: forma.value.nombreUsuario, 
      contrasena: forma.value.contrasena 
    }

    this._us.login(usuario, this.recuerdame)
    .subscribe(
      correcto => this.router.navigate(['/dashboard'])
    );

  }

}
