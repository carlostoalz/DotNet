import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html'
})
export class SidebarComponent implements OnInit {

  constructor() { }

  menu: any[] = [
    {
        titulo: 'Principal',
        icono: 'mdi mdi-gauge',
        submenu: [
            { titulo: 'Dashboard', url: '/dashboard' }
        ]
    },
    {
        titulo: 'Mantenimientos',
        icono: 'mdi mdi-folder-lock-open',
        submenu: [
            { titulo: 'Tipos Documento', url: '/tiposdocumento' },
            { titulo: 'Areas', url: '/areas' },
            { titulo: 'Sub Areas', url: '/subareas' },
            { titulo: 'Empleados', url: '/empleados' }
        ]
    }
];

  ngOnInit(): void {
  }

}
