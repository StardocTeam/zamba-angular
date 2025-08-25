import { AfterViewInit, Component, inject, OnInit, ViewChild, ChangeDetectionStrategy, ViewEncapsulation, Renderer2, ChangeDetectorRef, Inject } from '@angular/core';
import { MatPaginator, MatPaginatorModule, MatPaginatorIntl } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { TaskHistoryService } from '../../services/task-history-service.service';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { catchError, of, tap } from 'rxjs';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatCardModule } from '@angular/material/card';
import { ActivatedRoute, Router } from '@angular/router';
import { ITokenService, DA_SERVICE_TOKEN } from '@delon/auth';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { NzMenuModule } from 'ng-zorro-antd/menu';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzInputModule } from 'ng-zorro-antd/input';
import { MatSort, Sort, MatSortModule } from '@angular/material/sort';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { NzBreadCrumbModule } from 'ng-zorro-antd/breadcrumb';
import { FormsModule } from '@angular/forms';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';
import { is } from 'date-fns/locale';
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
    NzToolTipModule
  ],
  templateUrl: './quick-actions.component.html',
  styleUrls: ['./quick-actions.component.css'],
  providers: [TaskHistoryService],
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.Emulated
})
export class QuickActionsComponent implements OnInit {
  searchText: string = '';
  appliedSearchText: string = '';
  searchMatchedCategories: string[] = [];

  categories: any[] = [];
  greeting: string = 'Hola, bienvenido a Quick Actions';

  selectedCategories: string[] = [];

  favouriteActions: any[] = [];
  ngOnInit(): void {

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
            isFavorite: false
          },
          {
            name: 'Ingreso Carta Documento',
            description: 'Descripción de Ingreso Carta Documento',
            isFavorite: false
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
            isFavorite: false
          },
          {
            name: 'Reporte Doc Siniestros Ingresados',
            description: 'Descripción de Reporte Doc Siniestros Ingresados',
            isFavorite: false
          },
          {
            name: 'Ing Doc Siniestros',
            description: 'Descripción de Ing Doc Siniestros',
            isFavorite: false
          }
        ]
      },
      { name: 'Category 3', icon: 'bar-chart' },
      { name: 'Category 4', icon: 'setting' },
      { name: 'Category 5', icon: 'mail' },
      { name: 'Category 6', icon: 'calendar' },
      { name: 'Category 7', icon: 'cloud' },
      { name: 'Category 8', icon: 'team' },
      { name: 'Category 9', icon: 'file' },
      { name: 'Category 10', icon: 'star' },
    ];
    this.updateFavouriteCategory();
    this.selectedCategories = ['Favoritos'];
  }


  onSearch() {
    this.appliedSearchText = this.searchText;
    this.selectCategoriesBySearch();
  }
  selectCategoriesBySearch() {
    if (!this.appliedSearchText.trim()) {
      // No hacer nada si la búsqueda está vacía
      return;
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
    const idx = this.selectedCategories.indexOf(cat.name);
    if (idx > -1) {
      this.selectedCategories.splice(idx, 1);
    } else {
      this.selectedCategories.push(cat.name);
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
