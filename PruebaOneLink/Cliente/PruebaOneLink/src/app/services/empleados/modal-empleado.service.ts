import { Injectable, EventEmitter } from '@angular/core';
import { Empleado } from '../../models/Empleado.model';
import { SubArea } from '../../models/SubArea.model';
import { Area } from '../../models/Area.model';
import { SubAreasService } from '../subAreas/sub-areas.service';
import { AreasService } from '../areas/areas.service';

@Injectable({
  providedIn: 'root'
})
export class ModalEmpleadoService {

  oculto: string = 'oculto'; 
  titulo: string = "";
  empleado: Empleado = new Empleado(0, 0, "", "", "", 0, 0, 1);
  subareas: SubArea[] = [];
  areas: Area[] = [];

  notification: EventEmitter<any> = new EventEmitter<any>();

  constructor(
    public _sas: SubAreasService,
    public _as: AreasService
  ) { }

  ocultarModal() {
    this.oculto = 'oculto';
  }

  mostrarModal( titulo: string, empleado:Empleado) {
    
    this.empleado = empleado;
    this.oculto = '';
    this.titulo = titulo;

    this._as.obtener().subscribe( (areas: Area[]) => this.areas = areas);

    if(this.empleado.idEmmpleado > 0) {
      this._sas.buscar(this.empleado.idArea).subscribe((subareas: SubArea[]) => this.subareas = subareas);
    }

  }
}
