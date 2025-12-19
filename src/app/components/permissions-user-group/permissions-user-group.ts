import { AfterViewInit, Component, inject, OnInit, ViewChild, TemplateRef, ChangeDetectionStrategy, ViewEncapsulation, Renderer2, ChangeDetectorRef, Inject } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { ActivatedRoute, Router } from '@angular/router';
import { ITokenService, DA_SERVICE_TOKEN } from '@delon/auth';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { NzMenuModule } from 'ng-zorro-antd/menu';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzInputModule } from 'ng-zorro-antd/input';
import { FormsModule } from '@angular/forms';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';
import { TaskService } from '../../services/task.service';
import { NzSkeletonModule } from 'ng-zorro-antd/skeleton';
import { NzSpaceModule } from 'ng-zorro-antd/space';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzModalService } from 'ng-zorro-antd/modal';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { environment } from '@env/environment';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzTabsModule } from 'ng-zorro-antd/tabs';
import { RuleExecutorComponent } from '../rule-executor/rule-executor.component';

@Component({
    selector: 'app-permissions-user-group',
    standalone: true,
    imports: [NzMenuModule,
        HttpClientModule,
        CommonModule,
        MatCardModule,
        MatButtonModule,
        MatIconModule,
        NzIconModule,
        FormsModule,
        NzButtonModule,
        NzInputModule,
        NzCardModule,
        NzToolTipModule,
        NzSpinModule,
        NzSkeletonModule,
        NzSpaceModule,
        NzListModule,
        RuleExecutorComponent,
        NzTabsModule,
        NzLayoutModule
    ],
    templateUrl: './permissions-user-group.html',
    styleUrls: ['./permissions-user-group.css'],
    providers: [],
    changeDetection: ChangeDetectionStrategy.OnPush,
    encapsulation: ViewEncapsulation.Emulated
})
export class PermissionsUserGroupComponent implements OnInit {

    isLoading: boolean = true;
    searchText: string = '';
    searchTextUsers: string = '';
    isLoadingAction = false;
    selectedTabIndex = 0;
    data = [
        { title: 'Administrador_ZambaHR (178546)' },
        { title: 'Area_RRHH_Administracion (171)' },
        { title: 'Area_RRHH_Capacitacion (102)' },
        { title: 'Area_RRHH_Compensacion_y_Beneficios (191)' },
        { title: 'Area_RRHH_Evaluacion_de_Desempeño (192)' },
        { title: 'Area_RRHH_Gestion_de_Licencias (408)' },
        { title: 'Area_RRHH_Gestion_de_Vacaciones (454)' },
        { title: 'Area_RRHH_Gestion_del_Personal (169)' },
        { title: 'Area_RRHH_Liquidacion_de_Sueldos (170)' },
        { title: 'Area_RRHH_Recruitment (206)' },
        { title: 'Area_RRHH_Seguridad_y_Ambiente_Laboral (190)' },
        { title: 'Empleado (101)' },
        { title: 'LX_Analistas_a_Cargo_de_Clientes (511)' },
        { title: 'Postulantes (186)' },
        { title: 'Stardoc Argentina SA (168)' },
        { title: 'Stardoc_Area_Administracion (167)' },
        { title: 'test marsh event log user (505)' },
        { title: 'Z_CC_Licencias_Permiso_Consultar (336)' },
        { title: 'Z_CC_Licencias_Permiso_Editar (337)' },
        { title: 'Z_CC_Licencias_Permiso_Eliminar (339)' },
        { title: 'Z_CC_Licencias_Permiso_Insertar (338)' },
        { title: 'Z_CC_Vacaciones_Permiso_Consultar (331)' },
        { title: 'Z_CC_Vacaciones_Permiso_Editar (332)' },
        { title: 'Z_CC_Vacaciones_Permiso_Eliminar (333)' },
        { title: 'Z_CC_Vacaciones_Permiso_Insertar (334)' },
        { title: 'Z_Config_Firmas_Documentos_Permiso_Consultar (341)' },
        { title: 'Z_Config_Firmas_Documentos_Permiso_Editar (342)' },
        { title: 'Z_Config_Firmas_Documentos_Permiso_Eliminar (344)' },
        { title: 'Z_Config_Firmas_Documentos_Permiso_Insertar (343)' },
        { title: 'Z_Config_Grupos_Permiso_Consultar (492)' }
    ];
    filteredData: { title: string; }[] = [];
    filteredUsers: { username: string; }[] = [];
    selectedItem: any = { title: '-' };

    users = [{ username: 'Alvarez Emiliano (emiliano.alvarez@stardoc.com.ar 433)' },
    { username: 'Alvarez Emiliano (EmilianoManuAlvarez@gmail.com 189)' },
    { username: 'Bruñé Nicolas (bruñen 530)' },
    { username: 'Cabrera Nicolas (nicolas.cabrera@stardoc.com.ar 183)' },
    { username: 'Cardoso Mauro (mauro.cardoso@stardoc.com.ar 295)' },
    { username: 'Coria Paola (coriap 531)' },
    { username: 'Cruz Juan (cruzj 529)' },
    { username: 'Doe John (gusrollan@gmail.com 248)' },
    { username: 'Figueroa Susana (figueroas 532)' },
    { username: 'Gio Alejandro (empleado1@stardoc.com.ar 440)' },
    { username: 'Gonzalez Marcos (marcos.gonzalez2@stardoc.com.ar 271)' },
    { username: 'Legnani Martin (legnani@gmail.com 229)' },
    { username: 'Molina Huilen (molinah 528)' },
    { username: 'Montoto Jose (rrhhadmin@stardoc.com.ar 202)' },
    { username: 'Parma Francisco (empleadorhrh@stardoc.com.ar 434)' },
    { username: 'Parma Francisco (franciscoparma@stardoc.com.ar 503)' },
    { username: 'Rollan Gustavo (gusrollan1@gmail.com 245)' },
    { username: 'Rollan Gustavo Marcelo (gustavo.rollan@stardoc.com.ar 403)' },
    { username: 'Rollan Marcelo (gustavomarcelo.rollan@gmail.com 453)' },
    { username: 'stardoc zamba (zamba 22242)' },
    { username: 'test cambio de pass cambio de pass (test 504)' }];

    perros = [
        {
            username: 'Fido',
        }
    ]

    private route = inject(ActivatedRoute);
    constructor(
        private router: Router,
        @Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
        private taskService: TaskService,
        private cdr: ChangeDetectorRef,
        private modal: NzModalService,
        private sanitizer: DomSanitizer,
    ) {
    }
    ngOnInit(): void {
        this.filteredData = this.data;
        this.filteredUsers = this.users;
    }

    filterGroups(): void {
        this.filteredData = this.data.filter(item =>
            item.title.toLowerCase().includes(this.searchText.toLowerCase())
        );
    }

    filterUsers(): void {
        this.filteredUsers = this.users.filter(item =>
            item.username.toLowerCase().includes(this.searchTextUsers.toLowerCase())
        );
    }

    selectItem(item: any): void {
        this.selectedItem = item;
    }

    moveToGroup(item: any): void {
        this.users = this.users.filter(u => u !== item);
        this.perros.push(item);
        this.filterUsers();
    }

    removeFromGroup(item: any): void {
        this.perros = this.perros.filter(p => p !== item);
        this.users.push(item);
        this.filterUsers();
    }
}
