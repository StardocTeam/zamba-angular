import { AfterViewInit, Component, inject, OnInit, ViewChild, TemplateRef, ChangeDetectionStrategy, ViewEncapsulation, Renderer2, ChangeDetectorRef, Inject, WritableSignal, signal } from '@angular/core';
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
import { AdminService } from 'src/app/services/admin.service';
import { toSignal } from '@angular/core/rxjs-interop';
import { forkJoin } from 'rxjs';

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
    isLoadingRightPanel: boolean = false;
    searchText: string = '';
    searchTextUsers: string = '';
    searchTextGroupUsers: string = '';
    searchTextOtherUsers: any; string = '';
    searchInheritedGroupUsers: string = '';
    searchTextOtherGroups: string = '';

    searchTextOtherGroupsForUserMainTab: string = '';

    searchTextGroupsBelongUser: string = '';
    isLoadingAction = false;
    selectedTabIndex = 0;

    groups = signal<any[]>([]);

    filteredData: any[] = [];
    filteredUsersSidebar: any[] = [];
    filteredOtherUsers: any[] = [];
    filteredGroupUsers: any[] = [];
    selectedItem: any = { _name: '-' };

    selectedUserSidebarItem: any = { _nombres: '-', _apellidos: '', _name: '', };

    allUsers: any[] = [];

    otherUsers: any[] = [];

    usersForAGroup: any[] = [];

    //grupos heredados del grupo seleccionado
    inheritedGroups: any[] = [];
    filteredInheritedGroups: any[] = [];

    otherGroups: any[] = [];
    filteredOtherGroups: any[] = [];

    filteredOtherGroupsForUserMainTab: any[] = [];

    //Asignar grupos al usuario
    groupsBelongUser: any[] = [];
    filteredGroupsBelongUser: any[] = [];

    unassignedRoles: any[] = [];
    filteredUnassignedRoles: any[] = [];

    private route = inject(ActivatedRoute);

    constructor(
        private router: Router,
        @Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
        private taskService: TaskService,
        private cdr: ChangeDetectorRef,
        private modal: NzModalService,
        private sanitizer: DomSanitizer,

        private adminService: AdminService
    ) {
    }
    ngOnInit(): void {
        this.isLoading = true;
        forkJoin({
            groups: this.adminService.GetAllGroups(),
            users: this.adminService.GetAllUsers()
        }).subscribe({
            next: (res: any) => {
                // Groups
                let groups = res.groups;


                groups.sort((a: { _name: string; }, b: { _name: any; }) =>
                    a._name.localeCompare(b._name, 'en', { sensitivity: 'base' })
                );
                console.log(groups);
                this.groups.set(groups);
                //solo grupos, sin roles
                let groupsWithoutRoles = groups.filter((g: any) => !g._name.toLowerCase().startsWith('rol_'));
                this.filteredData = groupsWithoutRoles;
                this.otherGroups = groups;

                //filteredOtherGroups = roles no asignados. Asi que vamos a filtrar y dejar solo la lista de roles
                let rolesOnly = groups.filter((g: any) => g._name.toLowerCase().startsWith('rol_'));

                this.unassignedRoles = rolesOnly;
                this.filteredUnassignedRoles = rolesOnly;

                this.filteredOtherGroupsForUserMainTab = groups;

                // Users
                const users = res.users;
                console.log("getAllUsers", users);
                users.sort((a: { _apellidos: string; }, b: { _apellidos: any; }) =>
                    a._apellidos.localeCompare(b._apellidos, 'en', { sensitivity: 'base' })
                );
                this.filteredUsersSidebar = users;
                this.allUsers = users;
                this.otherUsers = users;
                this.filteredOtherUsers = users;

                this.isLoading = false;
                this.cdr.detectChanges();
            },
            error: (err) => {
                console.error(err);
                this.isLoading = false;
                this.cdr.detectChanges();
            }
        });
    }

    filterGroups(): void {
        this.filteredData = this.groups().filter(item =>
            item._name.toLowerCase().includes(this.searchText.toLowerCase()) ||
            item._id.toString().toLowerCase().includes(this.searchText.toLowerCase())
        );
    }
    filterUsers(): void {
        this.filteredUsersSidebar = this.allUsers.filter(item =>
            (item.username || (item._apellidos + ' ' + item._nombres + ' (' + item._name + ' ' + item._id + ')'))
                .toLowerCase().includes(this.searchTextUsers.toLowerCase())
        );
    }

    filterInheritedGroups(): void {
        this.filteredInheritedGroups = this.inheritedGroups.filter(item =>
            item._name.toLowerCase().includes(this.searchInheritedGroupUsers.toLowerCase())
        );
    }

    filterOtherGroups(): void {
        this.filteredUnassignedRoles = this.unassignedRoles.filter(g =>
            !this.inheritedGroups.some((ig: any) => ig._id === g._id) &&
            g._id !== this.selectedItem._id
        );
        this.filteredUnassignedRoles = this.filteredUnassignedRoles.filter(item =>
            (item._name || '').toLowerCase().includes((this.searchTextOtherGroups || '').toLowerCase())
        );
    }

    filterOtherGroupsForUserMainTab(): void {
        this.filteredOtherGroupsForUserMainTab = this.otherGroups.filter(g =>
            !this.groupsBelongUser.some((ig: any) => ig._id === g._id)
        );
        this.filteredOtherGroupsForUserMainTab = this.filteredOtherGroupsForUserMainTab.filter(item =>
            (item._name || '').toLowerCase().includes((this.searchTextOtherGroupsForUserMainTab || '').toLowerCase())
        );
    }


    filterOtherUsers(): void {
        const availableUsers = this.otherUsers.filter(u => !this.usersForAGroup.some(g => g._id === u._id));
        this.filteredOtherUsers = availableUsers.filter(item => {
            const val = item.username || (item._apellidos + ' ' + item._nombres + ' (' + item._name + ' ' + item._id + ')');
            return val.toLowerCase().includes(this.searchTextOtherUsers.toLowerCase());
        });
    }

    filterGroupUsers(): void {
        this.filteredGroupUsers = this.usersForAGroup.filter(item => {
            const val = item.username || (item._apellidos + ' ' + item._nombres + ' (' + item._name + ' ' + item._id + ')');
            return val.toLowerCase().includes(this.searchTextGroupUsers.toLowerCase());
        });
    }

    filterGroupsBelongUser(): void {
        this.filteredGroupsBelongUser = this.groupsBelongUser.filter(item => {
            const val = item.username || (item._apellidos + ' ' + item._nombres + ' (' + item._name + ' ' + item._id + ')');
            return val.toLowerCase().includes(this.searchTextGroupsBelongUser.toLowerCase());
        });
    }

    //cuando se selecciona un GRUPO de la sidebar
    selectSidebarGroupItem(item: any): void {
        this.selectedItem = item;
        this.isLoadingRightPanel = true;
        console.log(this.selectedItem);

        forkJoin({
            users: this.adminService.GetAllUsersForAGroup(this.selectedItem._id),
            inheritedGroups: this.adminService.GetInheritedGroups(this.selectedItem._id)
        }).subscribe({
            next: (res: any) => {
                // Users
                console.log(res.users);
                this.usersForAGroup = res.users;
                this.usersForAGroup.sort((a: any, b: any) =>
                    (a._apellidos || '').localeCompare((b._apellidos || ''), 'en', { sensitivity: 'base' })
                );
                this.filteredGroupUsers = this.usersForAGroup;
                this.filteredOtherUsers = this.otherUsers.filter(u => !this.usersForAGroup.some(g => g._id === u._id));

                // Inherited Groups
                console.log("inherited groups", res.inheritedGroups);

                //ahora no quieren que se vean los grupos, sino solo los roles heredados. Asi que filtramos solo los que empiezan por "rol_"
                let inheritedGroupsFiltered = res.inheritedGroups.filter((g: any) => g._name.toLowerCase().startsWith('rol_'));
                this.inheritedGroups = inheritedGroupsFiltered;
                this.filteredInheritedGroups = inheritedGroupsFiltered;
                this.filterOtherGroups();

                this.isLoadingRightPanel = false;
                this.cdr.detectChanges();
            },
            error: (err) => {
                console.error(err);
                this.isLoadingRightPanel = false;
                this.cdr.detectChanges();
            }
        });

    }

    selectSidebarUserItem(item: any): void {
        this.selectedUserSidebarItem = item;
        this.isLoadingRightPanel = true;
        console.log(this.selectedUserSidebarItem);

        //obtener los grupos de ese usuario
        this.adminService.GetGroupsForAUser(this.selectedUserSidebarItem._id).subscribe({
            next: (res: any) => {
                console.log(res);
                this.groupsBelongUser = res;
                this.filteredGroupsBelongUser = res;
                this.filterOtherGroupsForUserMainTab();
                this.isLoadingRightPanel = false;
                this.cdr.detectChanges();
            },
            error: (err) => {
                console.error(err);
                this.isLoadingRightPanel = false;
                this.cdr.detectChanges();
            }
        });

    }
    removeUserFromGroup(item: any): void {
        this.isLoadingRightPanel = true;
        this.adminService.RemoveUserFromGroup(item._id, this.selectedItem._id).subscribe({
            next: (res: any) => {
                this.usersForAGroup = this.usersForAGroup.filter(p => p !== item);
                this.filterGroupUsers();
                this.filteredOtherUsers = this.otherUsers.filter(u => !this.usersForAGroup.some(g => g._id === u._id));
                this.isLoadingRightPanel = false;
                this.cdr.detectChanges();
            },
            error: (err: any) => {
                console.error(err);
                this.isLoadingRightPanel = false;
                this.cdr.detectChanges();
            }
        });
    }

    removeUserFromGroup2(item: any): void {
        this.isLoadingRightPanel = true;
        this.adminService.RemoveUserFromGroup(this.selectedUserSidebarItem._id, item._id).subscribe({
            next: (res: any) => {
                this.groupsBelongUser = this.groupsBelongUser.filter(p => p !== item);
                this.filterGroupsBelongUser();
                this.filterOtherGroupsForUserMainTab();
                this.isLoadingRightPanel = false;
                this.cdr.detectChanges();
            },
            error: (err: any) => {
                console.error(err);
                this.isLoadingRightPanel = false;
            }
        });
    }

    addUserIntoGroup(item: any): void {
        this.isLoadingRightPanel = true;
        this.adminService.AddUserIntoGroup(item._id, this.selectedItem._id).subscribe({
            next: (res: any) => {
                this.usersForAGroup.push(item);
                this.usersForAGroup.sort((a: any, b: any) =>
                    (a._apellidos || '').localeCompare((b._apellidos || ''), 'en', { sensitivity: 'base' })
                );
                this.filterGroupUsers();
                this.filteredOtherUsers = this.otherUsers.filter(u => !this.usersForAGroup.some(g => g._id === u._id));
                this.isLoadingRightPanel = false;
                this.cdr.detectChanges();
            },
            error: (err: any) => {
                console.error(err);
                this.isLoadingRightPanel = false;
                this.cdr.detectChanges();
            }
        });
    }

    addUserIntoGroup2(item: any): void {
        this.isLoadingRightPanel = true;
        this.adminService.AddUserIntoGroup(this.selectedUserSidebarItem._id, item._id).subscribe({
            next: (res: any) => {
                this.groupsBelongUser.push(item);
                this.groupsBelongUser.sort((a: any, b: any) =>
                    (a._name || '').localeCompare((b._name || ''), 'en', { sensitivity: 'base' })
                );
                this.filterGroupsBelongUser();
                this.filterOtherGroupsForUserMainTab();
                this.isLoadingRightPanel = false;
                this.cdr.detectChanges();
            },
            error: (err: any) => {
                console.error(err);
                this.isLoadingRightPanel = false;
            }
        });
    }

    addInheritedGroup(item: any): void {
        this.isLoadingRightPanel = true;
        this.adminService.AddInheritedGroup(this.selectedItem._id, item._id).subscribe({
            next: (res: any) => {
                this.inheritedGroups.push(item);
                this.inheritedGroups.sort((a: any, b: any) =>
                    (a._name || '').localeCompare((b._name || ''), 'en', { sensitivity: 'base' })
                );
                this.filterInheritedGroups();
                this.filterOtherGroups();
                this.isLoadingRightPanel = false;
                this.cdr.detectChanges();
            },
            error: (err: any) => {
                console.error(err);
                this.isLoadingRightPanel = false;
            }
        });
    }
    deleteInheritedGroup(item: any): void {
        this.isLoadingRightPanel = true;
        this.adminService.DeleteInheritedGroup(this.selectedItem._id, item._id).subscribe({
            next: (res: any) => {
                this.inheritedGroups = this.inheritedGroups.filter(p => p !== item);
                this.filterInheritedGroups();
                this.filterOtherGroups();
                this.isLoadingRightPanel = false;
                this.cdr.detectChanges();
            },
            error: (err: any) => {
                console.error(err);
                this.isLoadingRightPanel = false;
            }
        });
    }

}
