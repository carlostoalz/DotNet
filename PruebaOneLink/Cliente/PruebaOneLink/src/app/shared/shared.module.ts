import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { HeaderComponent } from './header/header.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { ModalUpdateEmpleadoComponent } from './modals/modal-update-empleado.component';
import { NopagefoundComponent } from './nopagefound/nopagefound.component';

@NgModule({
    declarations: [
        HeaderComponent,
        SidebarComponent,
        ModalUpdateEmpleadoComponent,
        NopagefoundComponent
    ],
    exports: [
        HeaderComponent,
        SidebarComponent,
        ModalUpdateEmpleadoComponent,
        NopagefoundComponent
    ],
    imports: [
        RouterModule,
        CommonModule,
        FormsModule
    ]
})
export class SharedModule {}