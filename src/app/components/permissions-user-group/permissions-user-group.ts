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
        this.adminService.GetAllGroups().subscribe((res: any) => {
            res.sort((a: { _name: string; }, b: { _name: any; }) =>
                a._name.localeCompare(b._name, 'en', { sensitivity: 'base' })
            );
            console.log(res);
            this.groups.set(res);
            this.filteredData = res;

            this.otherGroups = res;
            this.filteredOtherGroups = res;
            this.filteredOtherGroupsForUserMainTab = res;
            this.cdr.detectChanges();
        });
        this.adminService.GetAllUsers().subscribe((res: any) => {
            console.log("getAllUsers", res);
            res.sort((a: { _apellidos: string; }, b: { _apellidos: any; }) =>
                a._apellidos.localeCompare(b._apellidos, 'en', { sensitivity: 'base' })
            );
            this.filteredUsersSidebar = res;

            this.allUsers = res;
            this.otherUsers = res;
            this.filteredOtherUsers = res;
            this.cdr.detectChanges();
        });
    }

    filterGroups(): void {
        this.filteredData = this.groups().filter(item =>
            item._name.toLowerCase().includes(this.searchText.toLowerCase())
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
        this.filteredOtherGroups = this.otherGroups.filter(g =>
            !this.inheritedGroups.some((ig: any) => ig._id === g._id) &&
            g._id !== this.selectedItem._id
        );
        this.filteredOtherGroups = this.filteredOtherGroups.filter(item =>
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
        console.log(this.selectedItem);

        //obtener los usuarios de ese grupo
        this.adminService.GetAllUsersForAGroup(this.selectedItem._id).subscribe((res: any) => {
            console.log(res);
            this.usersForAGroup = res;
            this.filteredGroupUsers = res;
            this.filteredOtherUsers = this.otherUsers.filter(u => !this.usersForAGroup.some(g => g._id === u._id));
            this.cdr.detectChanges();
        });

        //obtener los grupos heredados de ese grupo
        this.adminService.GetInheritedGroups(this.selectedItem._id).subscribe((res: any) => {
            console.log("inherited groups", res);
            this.inheritedGroups = res;
            this.filteredInheritedGroups = res;
            this.filterOtherGroups();
            this.cdr.detectChanges();
        });

    }

    selectSidebarUserItem(item: any): void {
        this.selectedUserSidebarItem = item;
        console.log(this.selectedUserSidebarItem);

        //obtener los grupos de ese usuario
        this.adminService.GetGroupsForAUser(this.selectedUserSidebarItem._id).subscribe((res: any) => {
            console.log(res);
            this.groupsBelongUser = res;
            this.filteredGroupsBelongUser = res;
            this.filterOtherGroupsForUserMainTab();
            this.cdr.detectChanges();
        });

    }
    removeUserFromGroup(item: any): void {
        this.adminService.RemoveUserFromGroup(item._id, this.selectedItem._id).subscribe({
            next: (res: any) => {
                this.usersForAGroup = this.usersForAGroup.filter(p => p !== item);
                this.filterGroupUsers();
                this.filteredOtherUsers = this.otherUsers.filter(u => !this.usersForAGroup.some(g => g._id === u._id));
                this.cdr.detectChanges();
            },
            error: (err: any) => {
                console.error(err);
            }
        });
    }

    removeUserFromGroup2(item: any): void {
        this.adminService.RemoveUserFromGroup(this.selectedUserSidebarItem._id, item._id).subscribe({
            next: (res: any) => {
                this.groupsBelongUser = this.groupsBelongUser.filter(p => p !== item);
                this.filterGroupsBelongUser();
                this.filterOtherGroupsForUserMainTab();
                this.cdr.detectChanges();
            },
            error: (err: any) => {
                console.error(err);
            }
        });
    }

    addUserIntoGroup(item: any): void {
        this.adminService.AddUserIntoGroup(item._id, this.selectedItem._id).subscribe({
            next: (res: any) => {
                this.usersForAGroup.push(item);
                this.filterGroupUsers();
                this.filteredOtherUsers = this.otherUsers.filter(u => !this.usersForAGroup.some(g => g._id === u._id));
                this.cdr.detectChanges();
            },
            error: (err: any) => {
                console.error(err);
            }
        });
    }

    addUserIntoGroup2(item: any): void {
        this.adminService.AddUserIntoGroup(this.selectedUserSidebarItem._id, item._id).subscribe({
            next: (res: any) => {
                this.groupsBelongUser.push(item);
                this.filterGroupsBelongUser();
                this.filterOtherGroupsForUserMainTab();
                this.cdr.detectChanges();
            },
            error: (err: any) => {
                console.error(err);
            }
        });
    }

    addInheritedGroup(item: any): void {
        this.adminService.AddInheritedGroup(this.selectedItem._id, item._id).subscribe({
            next: (res: any) => {
                this.inheritedGroups.push(item);
                this.filterInheritedGroups();
                this.filterOtherGroups();
                this.cdr.detectChanges();
            },
            error: (err: any) => {
                console.error(err);
            }
        });
    }
    deleteInheritedGroup(item: any): void {
        this.adminService.DeleteInheritedGroup(this.selectedItem._id, item._id).subscribe({
            next: (res: any) => {
                this.inheritedGroups = this.inheritedGroups.filter(p => p !== item);
                this.filterInheritedGroups();
                this.filterOtherGroups();
                this.cdr.detectChanges();
            },
            error: (err: any) => {
                console.error(err);
            }
        });
    }

}
