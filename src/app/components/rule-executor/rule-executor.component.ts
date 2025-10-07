import { AfterViewInit, Component, inject, OnInit, ViewChild, TemplateRef, ChangeDetectionStrategy, ViewEncapsulation, Renderer2, ChangeDetectorRef, Inject, Input, Output } from '@angular/core';
import { TaskHistoryService } from '../../services/task-history-service.service';
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
import { EventEmitter } from '@angular/core';

@Component({
  selector: 'app-rule-executor',
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
    NzListModule
  ],
  templateUrl: './rule-executor.component.html',
  styleUrls: ['./rule-executor.component.css'],
  providers: [TaskHistoryService],
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.Emulated
})
export class RuleExecutorComponent implements OnInit {

  @ViewChild('iframeModal', { static: true }) iframeModal!: TemplateRef<any>;
  iframeUrl: string = '';
  safeIframeUrl: SafeResourceUrl = '';

  @Input() ruleId!: number;

  isLoadingAction = false;

  @Output() ruleExecuted = new EventEmitter<any>();
  //@Output() ruleCompleted = new EventEmitter();
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
    this.isLoadingAction = true;
    if (this.ruleId && this.ruleId > 0) {
      this.executeRule(this.ruleId);
    }

  }

  executeRule(ruleid: number) {
    this.isLoadingAction = true;
    this.taskService.executeTaskRule(ruleid, null, null)
      .subscribe({
        next: (response: any) => {
          const responseObject = JSON.parse(response);
          const accion: string = this.taskService.checkAccion(responseObject);
          console.log(responseObject);
          if (accion != '') {
            switch (accion) {
              case 'doshowtable':
                this.router.navigate(['/tools/doshowtable'], { state: { Params: responseObject.Params, PendingChildRules: responseObject.PendingChildRules } });
                //this.SendExecutedEvent(responseObject, true);

                break;
              case 'executescript':
                if (responseObject.Params.RuleClass.toLowerCase().includes("doopentask")) {
                  this.DoOpenTaskHandler(responseObject.Vars, responseObject.Params);
                  this.SendExecutedEvent(responseObject, true);
                  break;
                }

                if (responseObject.Params.RuleClass.toLowerCase().includes("doopenurl")) {
                  this.DoOpenUrlHandler(responseObject.Vars, responseObject.Params);
                  this.SendExecutedEvent(responseObject, true);
                  break;

                }
                if (responseObject.Vars.scripttoexecute.toLowerCase().includes("opendoc")) {
                  this.OpenTask(responseObject.Vars, responseObject.Params);
                  this.SendExecutedEvent(responseObject, true);
                  break;
                }
                this.SendExecutedEvent(responseObject, true);
                break;

            }
          }
          this.isLoadingAction = false;
          this.cdr.markForCheck();
        },
        error: () => {
          this.SendExecutedEvent({}, false);
        }
      });
  }

  SendExecutedEvent(responseObject: any, ExecutedSuccessfully: boolean) {
    responseObject.ExecutedSuccessfully = ExecutedSuccessfully;
    this.isLoadingAction = false;
    this.cdr.markForCheck();
    this.ruleExecuted.emit(responseObject);
  }
  OpenTask(Vars: any, Params: any) {
    try {
      const taskId = Vars["nuevatarea.taskid"];
      const generateddocid = Vars["generateddocid"];
      const entityId = Vars["nuevatarea.entityid"];
      const asDoc = false;
      const name = Vars["nuevatarea.name"];
      const userid = Vars["nuevatarea.currentuserid"];
      const taskurl = "../WF/TaskViewer.aspx?doctype=" + entityId + "&docid=" + generateddocid + "&taskid=" + taskId + "&userid=" + userid;
      const idnotificacionaasociar = Vars["idnotificacionaasociar"];
      const openMode = Params?.openMode || '0';
      const tareaId = Vars["nuevatarea.id"];
      const wfstepid = Vars["nuevatarea.stepid"];
      const scriptToExecute = Vars["scripttoexecute"];

      console.log(`${environment['zambaWeb']}`.toLocaleLowerCase());

      let Url = (
        `${environment['zambaWeb']}/views/WF/TaskViewer.aspx?` +
        `DocTypeId=${entityId}` +
        `&docid=${generateddocid}` +
        `&taskid=${taskId}` +
        `&wfstepid=${wfstepid}` +
        `&user=${userid}`
      );
      window.open(Url, '_blank');


    } catch (error) {
      console.error('Error opening task:', error);
    }

  }
  DoOpenTaskHandler(Vars: any, Params: any) {
    const resultId = Params["DocID"] || 0;
    const docTyopeId = Params["DocTypeId"] || 0;
    const openMode = Params["OpenMode"] || 0;
    const userid = Params["CurrentUser"] || 0;
    let Url = (
      `${environment['zambaWeb']}/views/WF/TaskViewer.aspx?` +
      `DocTypeId=${docTyopeId}` +
      `&docid=${resultId}` +
      `&user=${userid}`
    );
    window.open(Url, '_blank');
  }

  DoOpenUrlHandler(Vars: any, Params: any) {
    const urlToOpen = Params["url"] || '';
    const openMode = Params["OpenMode"] || 0;
    switch (openMode) {
      case 0: // New Tab/Window
        window.open(urlToOpen, '_blank');
        break;
      case 1: // Modal
        this.showIframeModal(urlToOpen);
        break;
      default:
        window.open(urlToOpen, '_blank');
        break;
    }
  }

  showIframeModal(url: string): void {
    this.iframeUrl = url;
    this.safeIframeUrl = this.sanitizer.bypassSecurityTrustResourceUrl(url);
    this.modal.create({
      nzTitle: '',
      nzContent: this.iframeModal,
      nzWidth: 800,
      nzFooter: null
    });
  }


}
