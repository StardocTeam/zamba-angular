import { TaskHistoryService } from '../../services/task-history-service.service';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, inject, OnInit, ViewChild, TemplateRef, ChangeDetectionStrategy, ViewEncapsulation, Renderer2, ChangeDetectorRef, Inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { ActivatedRoute, Router } from '@angular/router';
import { ITokenService, DA_SERVICE_TOKEN } from '@delon/auth';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { NzMenuModule } from 'ng-zorro-antd/menu';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzInputModule } from 'ng-zorro-antd/input';
import { FormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { NzCardModule } from 'ng-zorro-antd/card';
import { TaskService } from '../../services/task.service';
import { NzSkeletonModule } from 'ng-zorro-antd/skeleton';
import { NzSpaceModule } from 'ng-zorro-antd/space';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzModalService } from 'ng-zorro-antd/modal';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { environment } from '@env/environment';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';
import { RuleExecutorComponent } from '../rule-executor/rule-executor.component';

@Component({
  selector: 'app-quick-actions',
  standalone: true,
  imports: [
    NzMenuModule,
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
  ],
  templateUrl: './quick-actions.component.html',
  styleUrls: ['./quick-actions.component.css'],
  providers: [TaskHistoryService],
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.Emulated,
})
export class QuickActionsComponent implements OnInit {

  @ViewChild(RuleExecutorComponent, { static: false }) ruleExecutor!: RuleExecutorComponent;

  @ViewChild('iframeModal', { static: true }) iframeModal!: TemplateRef<any>;
  iframeUrl: string = '';
  safeIframeUrl: SafeResourceUrl = '';
  isLoading: boolean = true;
  showAllCategoriesPanel: boolean = false;
  searchText: string = '';
  appliedSearchText: string = '';
  searchMatchedCategories: string[] = [];
  isLoadingAction = false;

  categories: any[] = [];
  greeting: string = 'Hola, bienvenido a Quick Actions';

  selectedCategories: string[] = [];

  favouriteActions: any[] = [];
  ruleExecutorWorking: boolean = false;
  pendingRuleId: number = 0;

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

    this.route.queryParamMap.subscribe(params => {

      const tokenParam = params.get('t');

      if (tokenParam) {
        this.tokenService.set({ token: tokenParam });
      }

    });
    this.taskService.getDynamicButtons().subscribe({
      next: (response: any) => {
        const responseObject = JSON.parse(response);
        console.log(responseObject);
        let categories = responseObject || [];
        categories = [
          {
            name: 'Favoritos',
            icon: 'star',
            actions: [],
          },
          ...categories,
        ];
        this.categories = categories;
        this.updateFavouriteCategory();
        this.clearSelectedCategories();
        this.isLoading = false;
        this.cdr.markForCheck();
      },
      error: error => {
        console.error('Error fetching dynamic buttons:', error);
        this.isLoading = false;
        this.cdr.markForCheck();
      },
    });
  }


  executeRule(ruleid: number) {
    this.pendingRuleId = ruleid;
    this.ruleExecutorWorking = true;
    this.cdr.markForCheck();
  }
  onActionCardClick(ruleid: number) {
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
                this.isLoadingAction = false;
                this.cdr.markForCheck();

                break;
              case 'executescript':
                if (responseObject.Params.RuleClass.toLowerCase().includes('doopentask')) {
                  this.DoOpenTaskHandler(responseObject.Vars, responseObject.Params);
                  this.isLoadingAction = false;
                  this.cdr.markForCheck();
                  break;
                }

                if (responseObject.Params.RuleClass.toLowerCase().includes('doopenurl')) {
                  this.DoOpenUrlHandler(responseObject.Vars, responseObject.Params);
                  this.isLoadingAction = false;
                  this.cdr.markForCheck();
                  break;
                }
                if (responseObject.Vars.scripttoexecute.toLowerCase().includes('opendoc')) {
                  this.OpenTask(responseObject.Vars, responseObject.Params);
                  this.isLoadingAction = false;
                  this.cdr.markForCheck();
                  break;
                }
                this.isLoadingAction = false;
                this.cdr.markForCheck();
                break;
            }
          }
          this.isLoadingAction = false;
          this.cdr.markForCheck();
        },
        error: () => {
          this.isLoadingAction = false;
          this.cdr.markForCheck();
        },
      });
  }

  onSearch() {
    this.appliedSearchText = this.searchText;
    this.selectCategoriesBySearch();
  }

  clearSelectedCategories() {
    this.selectedCategories = [];
    this.showAllCategoriesPanel = this.selectedCategories.length === 0;
    if (this.showAllCategoriesPanel) {
      this.selectedCategories = [...this.categories.map(cat => cat.name)];
    }
  }
  clearOnSearch() {
    this.searchText = '';
    this.onSearch();
  }
  selectCategoriesBySearch() {
    if (!this.appliedSearchText.trim()) {
      // No hacer nada si la búsqueda está vacía
      return;
    }
    if (this.showAllCategoriesPanel) {
      this.selectedCategories = [];
    }

    const search = this.appliedSearchText.trim().toLowerCase();
    this.searchMatchedCategories = this.categories
      .filter(
        cat =>
          // Coincide el nombre de la categoría
          (cat.name && cat.name.toLowerCase().includes(search)) ||
          // O alguna acción coincide
          (cat.actions && cat.actions.some((action: any) => action.name.toLowerCase().includes(search))),
      )
      .map(cat => cat.name);

    // Selecciona solo las categorías que matchean la búsqueda
    this.selectedCategories = [...this.searchMatchedCategories];
    if (this.selectedCategories.length > 0) {
      this.showAllCategoriesPanel = false;
    }
  }

  getVisibleActions(cat: any) {

    if (!this.appliedSearchText.trim()) {
      return cat.actions || [];
    }
    const search = this.appliedSearchText.trim().toLowerCase();
    // Si la categoría fue seleccionada manualmente (no por búsqueda), muestra todas sus acciones
    if (!this.searchMatchedCategories.includes(cat.name)) {
      return cat.actions || [];
    }
    // Si el nombre de la categoría coincide, muestra todas sus acciones
    if (cat.name && cat.name.toLowerCase().includes(search)) {
      return cat.actions || [];
    }
    // Si fue seleccionada por búsqueda, filtra las acciones
    return (cat.actions || []).filter((action: any) =>
      action.name.toLowerCase().includes(search)
    );
  }
  toggleCategory(cat: any) {

    if (this.showAllCategoriesPanel) {
      this.selectedCategories = [];
    }
    const idx = this.selectedCategories.indexOf(cat.name);


    if (idx > -1) {
      this.selectedCategories.splice(idx, 1);

    } else {
      this.selectedCategories.push(cat.name);
    }
    // Si ya no hay ninguna categoría seleccionada, resetea la búsqueda y muestra todo
    this.showAllCategoriesPanel = this.selectedCategories.length === 0;
    if (this.showAllCategoriesPanel) {
      this.selectedCategories = [...this.categories.map(cat => cat.name)];
    }
  }

  updateFavouriteCategory() {
    const favCategory = this.categories.find(cat => cat.name === 'Favoritos');
    if (favCategory) {
      favCategory.actions = [];
      this.categories.forEach(cat => {
        if (cat.name !== 'Favoritos' && cat.actions) {
          cat.actions.forEach((action: { isFavorite: any }) => {
            if (action.isFavorite) {
              favCategory.actions.push(action);
            }
          });
        }
      });
    }
    // Supón que tienes un array de acciones seleccionadas
    const selectedActions = favCategory.actions.filter((action: any) => action.isFavorite);

    // Construye el string con los ruleid
    const ruleIdsString = `[${selectedActions.map((a: any) => a.ruleid).join(',')}]`;
    console.log(ruleIdsString);
    this.taskService.updateUserFavoriteActions(ruleIdsString).subscribe({
      next: (response: any) => {
        console.log('Favorite actions updated successfully');
      },
    });
  }

  isSelected(cat: any): boolean {
    return this.selectedCategories.includes(cat.name);
  }
  toggleFavorite(action: any) {
    action.isFavorite = !action.isFavorite;
    this.updateFavouriteCategory();
  }


  onRuleCompleted(event: any) {

    console.log('Rule completed event received:', event);
    this.ruleExecutorWorking = false;
    this.pendingRuleId = 0;
    this.isLoadingAction = false;
    this.cdr.markForCheck();
  }
  OpenTask(Vars: any, Params: any) {
    try {
      const taskId = Vars['nuevatarea.taskid'];
      const generateddocid = Vars['generateddocid'];
      const entityId = Vars['nuevatarea.entityid'];
      const asDoc = false;
      const name = Vars['nuevatarea.name'];
      const userid = Vars['nuevatarea.currentuserid'];
      const taskurl = `../WF/TaskViewer.aspx?doctype=${entityId}&docid=${generateddocid}&taskid=${taskId}&userid=${userid}`;
      const idnotificacionaasociar = Vars['idnotificacionaasociar'];
      const openMode = Params?.openMode || '0';
      const tareaId = Vars['nuevatarea.id'];
      const wfstepid = Vars['nuevatarea.stepid'];
      const scriptToExecute = Vars['scripttoexecute'];

      console.log(`${environment['zambaWeb']}`.toLocaleLowerCase());

      let Url =
        `${environment['zambaWeb']}/views/WF/TaskViewer.aspx?` +
        `DocTypeId=${entityId}` +
        `&docid=${generateddocid}` +
        `&taskid=${taskId}` +
        `&wfstepid=${wfstepid}` +
        `&user=${userid}`;
      window.open(Url, '_blank');


    } catch (error) {
      console.error('Error opening task:', error);
    }

  }
  DoOpenTaskHandler(Vars: any, Params: any) {
    const resultId = Params['DocID'] || 0;
    const docTyopeId = Params['DocTypeId'] || 0;
    const openMode = Params['OpenMode'] || 0;
    const userid = Params['CurrentUser'] || 0;
    let Url = `${environment['zambaWeb']}/views/WF/TaskViewer.aspx?` + `DocTypeId=${docTyopeId}` + `&docid=${resultId}` + `&user=${userid}`;
    window.open(Url, '_blank');
  }

  DoOpenUrlHandler(Vars: any, Params: any) {
    const urlToOpen = Params['url'] || '';
    const openMode = Params['OpenMode'] || 0;
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
      nzFooter: null,
    });
  }
}
