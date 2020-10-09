import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from "@angular/forms";

import { PagesRoutingModule } from './pages-routing.module';
import { SharedModule } from '../shared/shared.module';

import { DashboardComponent } from './dashboard/dashboard.component';
import { TiposDocumentoComponent } from './tipos-documento/tipos-documento.component';
import { AreasComponent } from './areas/areas.component';
import { SubAreasComponent } from './sub-areas/sub-areas.component';
import { EmpleadosComponent } from './empleados/empleados.component';


@NgModule({
  declarations: [
    DashboardComponent,
    TiposDocumentoComponent,
    AreasComponent,
    SubAreasComponent,
    EmpleadosComponent
  ],
  exports: [
    DashboardComponent,
    TiposDocumentoComponent,
    AreasComponent,
    SubAreasComponent,
    EmpleadosComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    PagesRoutingModule,
    SharedModule
  ]
})
export class PagesModule { }
