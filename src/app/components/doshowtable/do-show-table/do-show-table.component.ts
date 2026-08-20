import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, Input, OnInit, Output, OnChanges, SimpleChanges, EventEmitter } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { PageHeaderModule } from '@delon/abc/page-header';
import { de } from 'date-fns/locale';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzMessageModule, NzMessageService } from 'ng-zorro-antd/message';
import { NzModalService } from 'ng-zorro-antd/modal';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { NzTableModule } from 'ng-zorro-antd/table';
import { TaskService } from 'src/app/services/task.service';

interface ItemData {
  [key: string]: any;
}
@Component({
  selector: 'app-do-show-table',
  standalone: true,
  imports: [CommonModule, NzTableModule, FormsModule, NzInputModule, PageHeaderModule, NzButtonModule, NzMessageModule, NzSpinModule],
  templateUrl: './do-show-table.component.html',
  styleUrls: ['./do-show-table.component.less'],
})
export class DoShowTableComponent implements OnInit, OnChanges {
  isLoading = false;
  searchText: string = '';
  originalListOfData: readonly ItemData[] = [];
  listOfSelection = [];
  checked = false;
  indeterminate = false;
  listOfCurrentPageData: readonly ItemData[] = [];
  listOfData: Array<{ [key: string]: any }> = [];
  setOfCheckedId = new Set<number>();
  title: string = '';
  columns: string[] = [];
  tableToView: any[] = [];
  @Input() Params: any;

  @Input() PendingChildRules: number[] = [];

  @Output() doShowTableHasFinished = new EventEmitter<any>();

  constructor(
    private router: Router,
    private taskService: TaskService,
    private message: NzMessageService,
    private modal: NzModalService,
    private cdr: ChangeDetectorRef,
  ) {}
  onItemChecked(id: number, checked: boolean): void {
    this.setOfCheckedId.clear(); // Limpia todas las selecciones previas
    if (checked) {
      this.setOfCheckedId.add(id);
    }
    this.refreshCheckedStatus();
  }

  onCurrentPageDataChange($event: readonly ItemData[]): void {
    this.listOfCurrentPageData = $event;
    this.refreshCheckedStatus();
  }
  refreshCheckedStatus(): void {}

  ngOnInit(): void {
    this.initializeTable();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['Params']) {
      this.initializeTable();
    }
  }

  initializeTable(): void {
    if (this.Params != null && this.Params != undefined) {
      this.listOfData = this.Params.tableToView || [];
      this.originalListOfData = this.Params.tableToView || [];

      if (this.listOfData.length > 0) {
        this.columns = Object.keys(this.listOfData[0]);
      } else {
        this.columns = [];
      }
      this.title = this.Params.tableTitle || '';

      // Limpia los estados por si se está reutilizando el componente
      this.searchText = '';
      this.setOfCheckedId.clear();
      this.isLoading = false;
    }
  }

  applyFilter(value?: string): void {
    const search = (value || '').toLowerCase();
    this.listOfData = this.originalListOfData.filter(row => this.columns.some(col => String(row[col]).toLowerCase().includes(search)));
    this.setOfCheckedId.clear();
  }

  cancel(): void {
    this.doShowTableHasFinished.emit({ success: false, message: 'Operación cancelada por el usuario' });
  }

  logSelected(): void {
    this.isLoading = true;
    // Obtiene los elementos seleccionados usando los índices guardados en setOfCheckedId
    const seleccionados = Array.from(this.setOfCheckedId).map(i => this.listOfData[i]);
    if (this.Params?.VarDestiny) {
      const varDestiny = this.Params.VarDestiny;
    }
    // Convierte saveColumns a array de números si es string
    let saveColumnsArr: number[] = [];
    if (typeof this.Params?.saveColumns === 'string') {
      saveColumnsArr = (this.Params.saveColumns as string)
        .split(',')
        .map((x: string) => parseInt(x.trim(), 10))
        .filter((x: number) => !isNaN(x));
    } else if (Array.isArray(this.Params?.saveColumns)) {
      saveColumnsArr = this.Params.saveColumns.map((x: any) => Number(x)).filter((x: number) => !isNaN(x));
    }

    // Si saveColumns tiene exactamente 1 elemento
    if (saveColumnsArr.length === 1) {
      const colIndex = saveColumnsArr[0] - 1; // base 1 a base 0
      const colName = this.columns[colIndex];
      let valorSeleccionado: any = null;

      if (seleccionados.length === 1) {
        valorSeleccionado = seleccionados[0][colName];
      } else {
        valorSeleccionado = seleccionados.map(item => item[colName]);
      }

      const VarDestiny = this.Params.VarDestiny;
      let ResultValues: Array<{ name: string; value: any }> = [];
      ResultValues.push({ name: VarDestiny, value: valorSeleccionado });
      const ResultValuesJson = JSON.stringify(ResultValues);

      if (valorSeleccionado != null) {
        if (this.PendingChildRules != null && this.PendingChildRules.length > 0) {
          this.taskService.executeTaskRule(this.PendingChildRules[0], null, ResultValuesJson).subscribe({
            next: response => {
              this.isLoading = false;
              this.cdr.detectChanges();
              // Parsea el response si es string
              let respObj: any = response;
              if (typeof response === 'string') {
                try {
                  respObj = JSON.parse(response);
                } catch (e) {
                  console.error('No se pudo parsear el response:', e);
                  return;
                }
              }

              // Emitimos la respuesta directamente para que el rule-executor decida qué acción tomar
              this.doShowTableHasFinished.emit({ success: true, response: respObj });
            },
            error: err => {
              this.isLoading = false;
              this.message.error('Ocurrió un error al procesar la acción');
              console.error(err);
              this.doShowTableHasFinished.emit({ success: false, message: err });
            },
          });
        } else {
          this.isLoading = false;
          this.doShowTableHasFinished.emit({ success: true, response: ResultValuesJson });
        }
      }
    } else {
      this.isLoading = false;
      this.message.error('Error en la regla: Se debe especificar la columna que se desea guardar.');
      this.doShowTableHasFinished.emit({
        success: false,
        message: 'Error en la regla: Se debe especificar la columna que se desea guardar.',
      });
    }
  }
}
