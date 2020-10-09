import { Component, OnInit } from '@angular/core';
import { Empleado } from '../../models/Empleado.model';
import { ModalEmpleadoService } from '../../services/empleados/modal-empleado.service';
import { EmpleadosService } from '../../services/empleados/empleados.service';
import { IResultado } from '../../interfaces/IResultado';

@Component({
  selector: 'app-empleados',
  templateUrl: './empleados.component.html',
  styles: [
  ]
})
export class EmpleadosComponent implements OnInit {

  empleados: Empleado[] = [];
  cantPaginas: number = 0;
  paginaActual: number = 1;
  paginas: number[] = [];
  termino: string = "";

  constructor(
    public _mu: ModalEmpleadoService,
    public _es: EmpleadosService
  ) { }

  ngOnInit(): void {
    this.obtenerEmpleados();
    this._mu.notification.subscribe( () => this.obtenerEmpleados() );
  }

  obtenerEmpleados(paginaActual?: number, tamanoPagina?: number, termino = null) {

    this.paginaActual = paginaActual ? paginaActual : 1;

    this._es.obtenerEmpleados(paginaActual, tamanoPagina, termino).subscribe((resp: IResultado) => {
      this.empleados = resp.datos;
      this.cantPaginas = resp.catidadPaginas;
      this.paginador();
    });

  }
  
  crearEmpleado() {
    this._mu.mostrarModal("Crear Empleado", new Empleado(0, 0, "", "", "", 0, 0, 1));
  }

  guardarEmpleado(empleado: Empleado) {
    this._mu.mostrarModal("Actualizar Empleado", empleado);
  }

  eliminarEmpleado(empleado: Empleado) {
    this._es.eliminar(empleado.idEmmpleado).subscribe(() => this.obtenerEmpleados());
  }

  onChangeBuscar() {
    this.obtenerEmpleados(1, 10, this.termino);
  }

  private paginador() {

    let wpag: number[] = Array(this.cantPaginas).fill(this.cantPaginas).map((x,i)=> i + 1);

    if (this.cantPaginas <= 10) {      
      this.paginas = wpag;
    } else {
      if (this.paginaActual <= 10) {
        this.paginas = wpag.splice(0, 10);
      } else {
        this.paginas = wpag.splice( this.paginaActual - 1, 10);
      }
    }
  }

}
