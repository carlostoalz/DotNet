import { Component, OnInit } from '@angular/core';
import { ModalEmpleadoService } from '../../services/empleados/modal-empleado.service';
import { TiposDocumentoService } from '../../services/tipos-documento/tipos-documento.service';
import { AreasService } from '../../services/areas/areas.service';
import { EmpleadosService } from '../../services/empleados/empleados.service';
import { SubAreasService } from '../../services/subAreas/sub-areas.service';
import { Empleado } from '../../models/Empleado.model';
import { TipoDocumento } from '../../models/TipoDocumento.model';
import { Area } from '../../models/Area.model';
import { SubArea } from '../../models/SubArea.model';
import { SwalUtil } from '../../utils/swal.util';

@Component({
  selector: 'app-modal-update-empleado',
  templateUrl: './modal-update-empleado.component.html',
  styles: [
  ]
})
export class ModalUpdateEmpleadoComponent implements OnInit {

  tiposDomumento: TipoDocumento[] = [];

  private swal: SwalUtil = new SwalUtil();

  constructor(
    public _mu: ModalEmpleadoService,
    public _tds: TiposDocumentoService,
    public _as: AreasService,
    public _sas: SubAreasService,
    public _es: EmpleadosService
  ) { }

  ngOnInit(): void {
    this._tds.obtener().subscribe((tiposDomumento: TipoDocumento[]) => this.tiposDomumento = tiposDomumento);
    this._as.obtener().subscribe((areas: Area[]) => this._mu.areas = areas);
  }

  onChangeArea() {
    if (this._mu.empleado.idArea > 0) {
      this._sas.buscar(this._mu.empleado.idArea).subscribe((subareas: SubArea[]) => {
        if (subareas.length === 0) {
          this.cerrarModal();
          this.swal.Alerta("No hay registros", "No hay sub areas asignadas para esta area si desea dirijase a la pantalla de sub areas y asigne una al area indicada");
          return;
        }
        this._mu.subareas = subareas
      });
    }
  }

  onClickGuardar() {
    if (this._mu.empleado.idTipoDocumento === 0) {
      this.swal.Alerta("Completar campos", "Debe seleccionar un tipo de documento");
      return;
    }

    if (this._mu.empleado.numeroDocumento.trim() === "") {
      this.swal.Alerta("Completar campos", "Debe ingresar un número de documento");
      return;
    }

    if (this._mu.empleado.nombres.trim() === "") {
      this.swal.Alerta("Completar campos", "Debe completar el campo Nmbres");
      return;
    }

    if (this._mu.empleado.apellidos.trim() === "") {
      this.swal.Alerta("Completar campos", "Debe completar el campo Apellidos");
      return;
    }

    if (this._mu.empleado.idArea === 0) {
      this.swal.Alerta("Completar campos", "Debe seleccionar un area");
      return;
    }

    if (this._mu.empleado.idSubArea === 0) {
      this.swal.Alerta("Completar campos", "Debe seleccionar una sub area");
      return;
    }

    this._es.actualizar(this._mu.empleado).subscribe((empleado: Empleado) => {
      this._mu.notification.emit( empleado );
      this.cerrarModal();
    });
  }
  
  cerrarModal() {
    this._mu.ocultarModal();
    this._mu.empleado = new Empleado(0, 0, "", "", "", 0, 0, 1);
    this._mu.subareas = [];
    this._mu.areas = [];
  }
}
