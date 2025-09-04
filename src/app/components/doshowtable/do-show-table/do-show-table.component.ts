import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NzTableModule } from 'ng-zorro-antd/table';
import { FormsModule } from '@angular/forms';
import { NzInputModule } from 'ng-zorro-antd/input';
import { PageHeaderModule } from '@delon/abc/page-header';
import { Router } from '@angular/router';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { TaskService } from 'src/app/services/task.service';
import { NzMessageModule, NzMessageService } from 'ng-zorro-antd/message';
import { NzModalService } from 'ng-zorro-antd/modal';
import { NzSpinModule } from 'ng-zorro-antd/spin';


interface ItemData {
  [key: string]: any;
}
@Component({
  selector: 'app-do-show-table',
  standalone: true,
  imports: [CommonModule, NzTableModule, FormsModule, NzInputModule, PageHeaderModule, NzButtonModule, NzMessageModule, NzSpinModule],
  templateUrl: './do-show-table.component.html',
  styleUrls: ['./do-show-table.component.less']
})
export class DoShowTableComponent implements OnInit {

  isLoading = false;
  searchText: string = '';
  originalListOfData: readonly ItemData[] = [];
  listOfSelection = [

  ];
  checked = false;
  indeterminate = false;
  listOfCurrentPageData: readonly ItemData[] = [];
  listOfData: { [key: string]: any }[] = [];
  setOfCheckedId = new Set<number>();
  title: string = '';
  columns: string[] = [];
  tableToView: any[] = [];
  Params: any;

  PendingChildRules: number[] = [];

  constructor(private router: Router, private taskService: TaskService, private message: NzMessageService, private modal: NzModalService) {
    const navigation = this.router.getCurrentNavigation();
    const state = navigation?.extras.state as { Params?: any, PendingChildRules?: number[] };

    if (state && state.Params?.tableToView && state.Params.tableToView.length > 0) {
      this.listOfData = state.Params.tableToView;
      this.originalListOfData = state.Params.tableToView; // <--- guarda la original
      this.Params = state.Params;
      this.PendingChildRules = state.PendingChildRules || [];
      this.columns = Object.keys(this.listOfData[0]);
      this.title = state.Params?.tableTitle || '';
    }
  }
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
  refreshCheckedStatus(): void {

  }

  ngOnInit(): void {

  }


  applyFilter(value?: string): void {
    const search = (value || '').toLowerCase();
    this.listOfData = this.originalListOfData.filter(row =>
      this.columns.some(col =>
        String(row[col]).toLowerCase().includes(search)
      )
    );
    this.setOfCheckedId.clear();
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
      let ResultValues: { name: string; value: any }[] = [];
      ResultValues.push({ name: VarDestiny, value: valorSeleccionado });
      const ResultValuesJson = JSON.stringify(ResultValues);

      if (valorSeleccionado != null) {
        this.taskService.executeTaskRule(this.PendingChildRules[0], null, ResultValuesJson)
          .subscribe({
            next: (response) => {
              this.isLoading = false;
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
              const action = this.taskService.checkAccion(respObj);
              switch (action.toLowerCase()) {
                case 'executescript':
                  if (respObj?.Params?.RuleTypeName.toLowerCase() === 'doscreenmessage') {
                    const nuevoMensaje = respObj.Params["Nuevo Mensaje"]?.replace(/\n/g, '<br>');
                    console.log(nuevoMensaje);
                    this.modal.info({
                      nzTitle: nuevoMensaje,
                      nzWidth: 500,
                      nzOnOk: () => {
                        if (respObj.PendingChildRules && respObj.PendingChildRules.length === 0) {
                          window.history.back();
                        }
                      }
                    });
                  }
                  break;
              }
            },
            error: (err) => {
              this.isLoading = false;
              this.message.error('Ocurrió un error al procesar la acción');
              console.error(err);
            }
          });
      }
    }

  }
}
