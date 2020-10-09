import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DashboardComponent } from './dashboard/dashboard.component';
import { TiposDocumentoComponent } from './tipos-documento/tipos-documento.component';
import { AreasComponent } from './areas/areas.component';
import { SubAreasComponent } from './sub-areas/sub-areas.component';
import { EmpleadosComponent } from './empleados/empleados.component';
import { VerificaTokenGuard } from '../services/service.index';

const routes: Routes = [
  {
    path: 'dashboard',
    component: DashboardComponent,
    // canActivate: [VerificaTokenGuard],
    data: {
      tutulo: 'Dashboard'
    }
  },
  { path: 'tiposdocumento', component: TiposDocumentoComponent },
  { path: 'areas', component: AreasComponent },
  { path: 'subareas', component: SubAreasComponent },
  { path: 'empleados', component: EmpleadosComponent },
  { path: '', pathMatch: 'full', redirectTo: 'dashboard' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PagesRoutingModule { }
