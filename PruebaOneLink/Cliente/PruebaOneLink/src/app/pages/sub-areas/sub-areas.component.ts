import { Component, OnInit } from '@angular/core';
import { SubAreasService } from '../../services/subAreas/sub-areas.service';
import { AreasService } from '../../services/areas/areas.service';
import { Area } from '../../models/Area.model';
import { SubArea } from 'src/app/models/SubArea.model';
import { SwalUtil } from '../../utils/swal.util';
import Swal, { SweetAlertResult } from 'sweetalert2';

@Component({
  selector: 'app-sub-areas',
  templateUrl: './sub-areas.component.html',
  styles: [
  ]
})
export class SubAreasComponent implements OnInit {

  areas: Area[] = [];
  subAreas: SubArea[] = [];
  idArea: string = ""; 

  private swal: SwalUtil = new SwalUtil();

  constructor(
    public _sas: SubAreasService,
    public _as: AreasService
  ) { }

  ngOnInit(): void {
    this.cargarAreas();
  }

  cargarAreas() {
    this._as.obtener().subscribe( areas => this.areas = areas );
  }

  buscarAreas() {
    if (!this.idArea || this.idArea == "") {
      return;
    }

    this._sas.buscar(Number.parseInt(this.idArea)).subscribe((subareas: SubArea[]) => this.subAreas = subareas);
  }

  onChangeAreas() {
    this.buscarAreas();
  }

  crearSubArea() {
    this.swal.InputText('Crear Sub Area', 'Ingrese el nombre de la sub area')
    .then((sar: SweetAlertResult) => {

      if ( sar && sar.dismiss && sar.dismiss === Swal.DismissReason.cancel ) {
        return;
      }

      if( !sar || sar === undefined || !sar.value || (<string>sar.value).length === 0 ){
        
        this.swal.Alerta('Parametro no ingresado', 'Debe ingresar el nombre de la sub area');
        return;

      }

      if (!this.idArea || this.idArea == "") {
        this.swal.Alerta('Parametro no ingresado', 'Debe seleccionar el area');
        return;
      }

      this._sas.actualizar(new SubArea(-1, sar.value, Number.parseInt(this.idArea), 1)).subscribe(() => this.buscarAreas());

    });
  }

  guardarSubArea(subarea: SubArea) {
    this._sas.actualizar(subarea).subscribe(() => this.buscarAreas());
  }

  eliminarSubArea(subarea: SubArea) {
    this._sas.eliminar(subarea.idSubArea).subscribe(() => this.buscarAreas());
  }

}
