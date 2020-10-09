import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { SwalUtil } from '../utils/swal.util';
import { Router } from '@angular/router';
import { IUsuario } from '../interfaces/IUsuario';
import { UsuariosService } from '../services/service.index';

declare function init_plugins();

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./login.component.css']
})
export class RegisterComponent implements OnInit {

  swal: SwalUtil = new SwalUtil();
  forma: FormGroup;

  constructor(
    public router: Router,
    public _us: UsuariosService
  ) { }

  ngOnInit(): void {
    
    init_plugins();

    this.forma = new FormGroup({
      nombreUsuario: new FormControl( null, Validators.required ),
      nombre: new FormControl( null, Validators.required ),
      email: new FormControl( null, [Validators.required, Validators.email] ),
      contrasena: new FormControl( null, Validators.required ),
      contrasena2: new FormControl( null, Validators.required ),      
      condiciones: new FormControl( false )  
    }, 
    {
      validators: this.sonIguales( 'contrasena', 'contrasena2' )
    });
  }

  sonIguales( campo1: string, campo2: string ) {
    return ( group: FormGroup ) => {

      let pass1: string = group.controls[campo1].value;
      let pass2: string = group.controls[campo2].value;

      if ( pass1 === pass2 ) {
        return null
      } 
      
      return {
        sonIguales: true
      };

    };
  }

  registrarUsuario() {

    if ( !this.forma.value.condiciones ) {

      this.swal.Alerta( 'Importante', 'Debe aceptar las condiciones' );      
      return;

    }

    if ( this.forma.invalid ) {
      return;
    }

    let usuario: IUsuario = {
      nombreUsuario: this.forma.value.nombre,
      contrasena: this.forma.value.contrasena, 
      nombre: this.forma.value.nombre,
      email: this.forma.value.email
    };
    

    this._us.registrar(usuario).subscribe( () => this.router.navigate(['/login']) );
  }

}
