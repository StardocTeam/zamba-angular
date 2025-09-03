import { AfterViewInit, Component, inject, OnInit, ViewChild, ChangeDetectionStrategy, ViewEncapsulation, Renderer2, ChangeDetectorRef, Inject } from '@angular/core';
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

@Component({
  selector: 'app-quick-actions',
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
    NzSpinModule
  ],
  templateUrl: './quick-actions.component.html',
  styleUrls: ['./quick-actions.component.css'],
  providers: [TaskHistoryService],
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.Emulated
})
export class QuickActionsComponent implements OnInit {
  showAllCategoriesPanel: boolean = false;
  searchText: string = '';
  appliedSearchText: string = '';
  searchMatchedCategories: string[] = [];
  isLoadingAction = false;

  categories: any[] = [];
  greeting: string = 'Hola, bienvenido a Quick Actions';

  selectedCategories: string[] = [];

  favouriteActions: any[] = [];

  private route = inject(ActivatedRoute);
  constructor(
    private router: Router,
    @Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
    private taskService: TaskService
  ) {
  }
  ngOnInit(): void {

    this.route.queryParamMap.subscribe(params => {

      const tokenParam = params.get('t');

      if (tokenParam) {
        this.tokenService.set({ token: tokenParam });
      }

    });

    this.categories = [
      {
        name: 'Favoritos',
        icon: 'star',
        actions: [
        ]
      },
      {
        name: 'Emision',
        icon: 'dashboard',
        actions: [
          {
            name: 'Ingreso Designacion Beneficiarios',
            description: 'Descripción de Ingreso Designacion Beneficiarios, pero es demasiado larga entonces la voy a mostrar en este tooltip',
            isFavorite: false,
            ruleid: 0
          },
          {
            name: 'Ingreso Carta Documento',
            description: 'Descripción de Ingreso Carta Documento',
            isFavorite: false,
            ruleid: 0
          }
        ]
      },
      {
        name: 'Siniestros',
        icon: 'user',
        actions: [
          {
            name: 'Buscar RAJ',
            description: 'Descripción de Buscar RAJ',
            isFavorite: false,
            ruleid: 1013372
          },
          {
            name: 'Reporte Doc Siniestros Ingresados X Dia',
            description: 'Descripción de Reporte Doc Siniestros Ingresados',
            isFavorite: false,
            ruleid: 11544483
          },
          {
            name: 'Ing Doc Siniestros',
            description: 'Descripción de Ing Doc Siniestros',
            isFavorite: false,
            ruleid: 1282
          },
          {
            name: 'Ing Documentacion y/o Notificacion',
            description: 'Descripción Ing Documentacion y/o Notificacion',
            isFavorite: false,
            ruleid: 1012891
          }
        ]
      },
      {
        name: 'Reporte Pagos',
        icon: 'bar-chart',
        actions: [
          {
            name: 'Facturas y pagos pendientes',
            description: 'Descripción de Facturas y pagos pendientes',
            isFavorite: false,
            ruleid: 0
          },
          {
            name: 'Lotes aprobados entre fechas',
            description: 'Descripción de Lotes aprobados entre fechas',
            isFavorite: false,
            ruleid: 0
          },
          {
            name: 'Lotes',
            description: 'Descripción de Lotes',
            isFavorite: false,
            ruleid: 0
          }
        ]
      },
      {
        name: 'Pruebas Apro y Conf X Mail',
        icon: 'setting',
        actions: [
          {
            name: 'Envio de mail para Aprobar/Conformar pagos y facturas',
            description: 'Descripción de Envio de mail para Aprobar/Conformar pagos y facturas',
            isFavorite: false,
            ruleid: 0
          }
        ]
      },
      {
        name: 'Designaciones', icon: 'mail',
        actions: [
          {
            name: 'Reporte Designaciones',
            description: 'Descripción de Reporte Designaciones',
            isFavorite: false,
            ruleid: 0
          }
        ]
      },
      {
        name: 'Reporte',
        icon: 'calendar',
        actions: [
          {
            name: 'Prueba Emiliano',
            description: 'Descripción de Prueba Emiliano',
            isFavorite: false,
            ruleid: 0
          },
          {
            name: 'Casos sin fecha acuse',
            description: 'Descripción de Casos sin fecha acuse',
            isFavorite: false,
            ruleid: 0
          },
          {
            name: 'Reclamos sinrivar',
            description: 'Descripción de Reclamos sinrivar',
            isFavorite: false,
            ruleid: 0
          },
          {
            name: 'Notificacion Mediacion (AK) sin RAJ',
            description: 'Descripción de Notificacion Mediacion (AK) sin RAJ',
            isFavorite: false,
            ruleid: 0
          },
          {
            name: 'Reporte de ARJ activos en zamba',
            description: 'Descripción de Reporte de ARJ activos en zamba',
            isFavorite: false,
            ruleid: 0
          },
          {
            name: 'Notificacion Mediacion (AJ) sin RAJ',
            description: 'Descripción de Notificacion Mediacion (AJ) sin RAJ',
            isFavorite: false,
            ruleid: 0
          },
          {
            name: 'Notificacion Mediacion(AJ) sin informe asociado(AK)',
            description: 'Descripción de Notificacion Mediacion(AJ) sin informe asociado(AK)',
            isFavorite: false,
            ruleid: 0
          }
        ]
      },
      {
        name: 'Facturas y Pagos',
        icon: 'cloud',
        actions: [
          {
            name: 'Ingreso Solicitud de Pago',
            description: 'Descripción de Ingreso Solicitud de Pago',
            isFavorite: false,
            ruleid: 0
          }
        ]
      },
      {
        name: 'Pruebas',
        icon: 'team',
        actions: [
          {
            name: 'Ingresar Datos',
            description: 'Descripción de Ingreso de Datos',
            isFavorite: false,
            ruleid: 0
          }
        ]
      },
      {
        name: 'Carta Documento',
        icon: 'file',
        actions: [
          {
            name: 'Ingreso Cartas Documento Siniestros',
            description: 'Descripción de Ingreso Cartas Documento Siniestros',
            isFavorite: false,
            ruleid: 0
          },
          {
            name: 'Reporte Ingreso Carta Documento Siniestros',
            description: 'Descripción de Reporte Ingreso Carta Documento Siniestros',
            isFavorite: false,
            ruleid: 0
          }
        ]
      },
      {
        name: 'Acciones',
        icon: 'star',
        actions: [
          {
            name: 'Ver Formulario',
            description: 'Descripción de Ver Formulario',
            isFavorite: false,
            ruleid: 0
          },
          {
            name: 'Informe mediacion y Documentacion a portada por terceros',
            description: 'Descripción de Informe mediacion y Documentacion a portada por terceros',
            isFavorite: false,
            ruleid: 0
          }
        ]
      },
      {
        name: 'Facturas _(T-T)_ Pagos',
        icon: 'star',
        actions: [
          {
            name: 'Ingreso Factura',
            description: 'Descripción de Ingreso Factura',
            isFavorite: true,
            ruleid: 1431
          }
        ]
      }
    ];
    this.updateFavouriteCategory();
    this.selectedCategories = ['Favoritos'];
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
                break;
            }
          }
          this.isLoadingAction = false;
        },
        error: () => {
          this.isLoadingAction = false;
        }
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
      .filter(cat =>
        // Coincide el nombre de la categoría
        (cat.name && cat.name.toLowerCase().includes(search)) ||
        // O alguna acción coincide
        (cat.actions && cat.actions.some((action: any) =>
          action.name.toLowerCase().includes(search)
        ))
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
          cat.actions.forEach((action: { isFavorite: any; }) => {
            if (action.isFavorite) {
              favCategory.actions.push(action);
            }
          });
        }
      });
    }
  }

  isSelected(cat: any): boolean {
    return this.selectedCategories.includes(cat.name);
  }
  toggleFavorite(action: any) {
    action.isFavorite = !action.isFavorite;
    this.updateFavouriteCategory();
  }

}
