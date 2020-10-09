import { Component, OnInit } from '@angular/core';
import Swal, { SweetAlertResult } from 'sweetalert2';
import { TiposDocumentoService } from '../../services/tipos-documento/tipos-documento.service';
import { TipoDocumento } from '../../models/TipoDocumento.model';
import { SwalUtil } from '../../utils/swal.util';

@Component({
  selector: 'app-tipos-documento',
  templateUrl: './tipos-documento.component.html',
  styles: [
  ]
})
export class TiposDocumentoComponent implements OnInit {

  tiposDocumento: TipoDocumento[] = [];
  private swal: SwalUtil = new SwalUtil();

  constructor(
    public _tds: TiposDocumentoService
  ) { }

  ngOnInit(): void {
    
    this.cargarTiposDocumento();

  }

  cargarTiposDocumento() {

    this._tds.obtener()
    .subscribe((tipos: TipoDocumento[]) => {
      this.tiposDocumento = tipos
    });

  }

  crearTipoDocumento() {

    this.swal.InputText('Crear Tipo Documento', 'Ingrese el nombre del tipo de documento')
    .then((sar: SweetAlertResult) => {

      if ( sar && sar.dismiss && sar.dismiss === Swal.DismissReason.cancel ) {
        return;
      }

      if( !sar || sar === undefined || !sar.value || (<string>sar.value).length === 0 ){
        
        this.swal.Alerta('Parametro no ingresado', 'Debe ingresar el nombre del tipo de documento');
        return;

      }

      this._tds.actualizar(new TipoDocumento(-1, sar.value, 1)).subscribe(() => this.cargarTiposDocumento());

    });

  }

  guardarTipoDocumento(tipo: TipoDocumento) {
    this._tds.actualizar(tipo).subscribe(() => this.cargarTiposDocumento());
  }

  eliminarTipoDocumento(tipo: TipoDocumento) {
    this._tds.eliminar(tipo.idTipoDocumento).subscribe(() => this.cargarTiposDocumento());
  }

}
