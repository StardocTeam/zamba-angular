import { AfterViewInit, Component, inject, OnInit, ViewChild, TemplateRef, ChangeDetectionStrategy, ViewEncapsulation, Renderer2, ChangeDetectorRef, Inject, WritableSignal, signal, Input, Output, EventEmitter } from '@angular/core';
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
import { NzDescriptionsModule } from 'ng-zorro-antd/descriptions';
import { NzAvatarModule } from 'ng-zorro-antd/avatar';
import { NzTypographyModule } from 'ng-zorro-antd/typography';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { RuleExecutorComponent } from '../rule-executor/rule-executor.component';
import { AdminService } from 'src/app/services/admin.service';
import { toSignal } from '@angular/core/rxjs-interop';
import { forkJoin } from 'rxjs';


@Component({
    selector: 'app-group-data',
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
        NzLayoutModule,
        NzDescriptionsModule,
        NzAvatarModule,
        NzTypographyModule,
        NzDividerModule
    ],
    templateUrl: './group-data.html',
    styleUrls: ['./group-data.css'],
    providers: [],
    changeDetection: ChangeDetectionStrategy.OnPush,
    encapsulation: ViewEncapsulation.Emulated
})
export class GroupDataComponent implements OnInit {

    @Input()
    groupId: number = 0;
    @Input()
    name: string = '';
    @Input()
    description: string = '';

    @Output()
    onChangesSaved = new EventEmitter<void>();

    isLoading: boolean = false;
    isEditing: boolean = false;

    @Input()
    canEdit: boolean = false;

    private route = inject(ActivatedRoute);

    constructor(
        @Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
        private cdr: ChangeDetectorRef,
        private modal: NzModalService,
        private sanitizer: DomSanitizer,

        private adminService: AdminService
    ) {
    }

    toggleEdit(): void {
        this.isEditing = !this.isEditing;
    }
    ngOnInit(): void {
        this.route.queryParamMap.subscribe(params => {

            const tokenParam = params.get('t');

            if (tokenParam) {
                this.tokenService.set({ token: tokenParam });
            }

        });
    }

    saveChanges(): void {
        this.isLoading = true;
        this.adminService.updateGroup(this.groupId, {
            name: this.name,
            description: this.description
        }).subscribe({
            next: () => {
                this.isLoading = false;
                this.isEditing = false;
                this.cdr.markForCheck();
                this.onChangesSaved.emit();
            },
            error: () => {
                this.isLoading = false;
                this.cdr.markForCheck();
            }
        });
    }




}
