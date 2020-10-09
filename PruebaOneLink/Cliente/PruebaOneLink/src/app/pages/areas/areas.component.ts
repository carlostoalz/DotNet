import { Component, OnInit } from '@angular/core';
import { AreasService } from '../../services/areas/areas.service';
import { Area } from '../../models/Area.model';
import { SwalUtil } from '../../utils/swal.util';
import Swal, { SweetAlertResult } from 'sweetalert2';

@Component({
  selector: 'app-areas',
  templateUrl: './areas.component.html',
  styles: [
  ]
})
export class AreasComponent implements OnInit {

  areas: Area[] = [];
  private swal: SwalUtil = new SwalUtil();

  constructor(
    public _as: AreasService
  ) { }

  ngOnInit(): void {
    this.cargarAreas();
  }
  
  cargarAreas() {
    this._as.obtener().subscribe( areas => this.areas = areas );
  }

  crearArea() {
    this.swal.InputText('Crear Area', 'Ingrese el nombre del area')
    .then((sar: SweetAlertResult) => {

      if ( sar && sar.dismiss && sar.dismiss === Swal.DismissReason.cancel ) {
        return;
      }

      if( !sar || sar === undefined || !sar.value || (<string>sar.value).length === 0 ){
        
        this.swal.Alerta('Parametro no ingresado', 'Debe ingresar el nombre del area');
        return;

      }

      this._as.actualizar(new Area(-1, sar.value, 1)).subscribe(() => this.cargarAreas());

    });
  }

  guardarArea(area: Area) {
    this._as.actualizar(area).subscribe(() => this.cargarAreas());
  }

  eliminarArea(area: Area) {
    this._as.eliminar(area.idArea).subscribe(() => this.cargarAreas());
  }
}
