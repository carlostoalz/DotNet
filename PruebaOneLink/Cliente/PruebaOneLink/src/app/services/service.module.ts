import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from "@angular/common/http";

import { 
  TiposDocumentoService,
  AreasService,
  SubAreasService,
  EmpleadosService,
  ModalEmpleadoService,
  UsuariosService,
  VerificaTokenGuard
} from './service.index'

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    HttpClientModule
  ],
  providers: [
    TiposDocumentoService,
    AreasService,
    SubAreasService,
    EmpleadosService,
    ModalEmpleadoService,
    UsuariosService,
    VerificaTokenGuard
  ]
})
export class ServiceModule { }
