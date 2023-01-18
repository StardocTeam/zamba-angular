import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router, ActivatedRoute, ParamMap, NavigationExtras } from '@angular/router';
import { ClientImapService } from "./client-imap.service";
import { Imap } from "../../../entities/Imap";
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { EditService } from '../edit/edit.service';
import { runInThisContext } from 'vm';


export interface UserData {
  id: string;
  name: string;
  mail: string;
  //progress: string;
  color: string;

}

@Component({
  selector: 'app-client-imap',
  templateUrl: './client-imap.component.html',
  styleUrls: ['./client-imap.component.css']
})
export class ClientIMAPComponent implements OnInit {
  componentName: string = "Cliente IMAP";
  //displayedColumns es un array de strings para determinar el orden de la tabla.
  displayedColumns: string[] = ['IS_ACTIVE','PROCESS_ID','PROCESS_NAME', 'EMAIL', 'actions'];
  dataSourceProcess: MatTableDataSource<TableMap>;
  waitingResponse: Boolean = false;
  waitingProcessAllResponse:Boolean = false;
  horizontalPosition: MatSnackBarHorizontalPosition = 'center';
  verticalPosition: MatSnackBarVerticalPosition = 'top';
  refreshing: Boolean = false;

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  constructor(
    private router: Router,
    private _ClientImapService: ClientImapService,
    private _snackBar: MatSnackBar
  ) {

    this.GetRegister();

  }

  ModifyOnClick(process) {
    try {
      let jsonValue = JSON.stringify(process);
      this.router.navigate(['/apps/IMAP/edit'], { queryParams: { process: jsonValue } });
    } catch (error) {
      console.log(error);
    }
  }

  refrshOnClick() {
    try {
      this.refreshing = true;
      this.GetRegister();

    } catch (error) {
      console.log(error);
    }
  }

  DeleteProcess(process) {
    try {
      var genericRequest = {
        "UserId": parseInt(localStorage.getItem("UserID")),
        "Params": {
          "processId": process.PROCESS_ID
        }
      }
      console.log("PROCESS_ID", process.PROCESS_ID);
      this.waitingResponse = true;
      this._ClientImapService.DeleteProcess(genericRequest).subscribe(
        x => {
          console.log(x);
          this.waitingResponse = false;
          this._snackBar.open("El proceso se ha eliminado correctamente", 'Ok', {
            duration: 2000,
            horizontalPosition: this.horizontalPosition,
            verticalPosition: this.verticalPosition,
          });
          this.GetRegister();
        },
        error => {
          console.log(error);
          this.waitingResponse = false;
          this.GetRegister();
        }
      );
    } catch (error) {
      console.log(error);
    }
  }

  ngOnInit() {


  }

  applyFilter(filterValue: string) {
    this.dataSourceProcess.filter = filterValue.trim().toLowerCase();

    if (this.dataSourceProcess.paginator) {
      this.dataSourceProcess.paginator.firstPage();
    }
  }
  isActiveToggleOnChange(process){
    process.IS_ACTIVE = process.IS_ACTIVE == true || process.IS_ACTIVE == 1?1:0;
    var genericRequest = {
      "UserId": parseInt(localStorage.getItem("UserID")),
      "Params": {
        "PROCESS_ID":process.PROCESS_ID,
        "IS_ACTIVE":process.IS_ACTIVE
      }
    };
    this._ClientImapService.SetProcessActiveState(genericRequest).subscribe(
      OkResponse  => { this.showIsActiveResponseMessage(OkResponse,process.IS_ACTIVE) },
      error  => {
          this.showIsActiveErrorResponseMessage(error);
          this.refrshOnClick()
      }
    );
  }

  showIsActiveResponseMessage(OkResponse,isActive){

    let message = "El proceso ha quedado "+ (isActive?"Activo":"Inactivo");
    this._snackBar.open(message, 'Ok', {
        duration: 2000,
        horizontalPosition: this.horizontalPosition,
        verticalPosition: this.verticalPosition,
    });
    console.log(OkResponse);
}

showInsertOkMessage(OkResponse){

  let message = "Los procesos se han ejecutado satisfactoriamente";
  this._snackBar.open(message, 'Ok', {
      duration: 2000,
      horizontalPosition: this.horizontalPosition,
      verticalPosition: this.verticalPosition,
  });
  console.log(OkResponse);
}



showIsActiveErrorResponseMessage(ErrorResponse){

  let message = "Ha ocurido un error, intente de nuevo más tarde";
  this._snackBar.open(message, 'Cerrar', {
      horizontalPosition: this.horizontalPosition,
      verticalPosition: this.verticalPosition,
  });
  console.log(ErrorResponse);
}

  GetRegister() {

    this._ClientImapService.getListTable()
      .subscribe(
        x => {
          this.ListObjectTable(x);
          this.refreshing = false;
          
        },
        error => { console.log("Error", error); this.refreshing = false; }
      );
  }

  ListObjectTable(TableList) {
    try {
      var dataParced: TableMap[] = JSON.parse(TableList);
      var dataSource = new MatTableDataSource(dataParced)
      console.log(dataParced);
      this.dataSourceProcess = dataSource;
      this.dataSourceProcess.paginator = this.paginator;
      this.dataSourceProcess.sort = this.sort;
    } catch (error) {
      console.log(error)
    }

    
  }
  
  InsertMails() {
    var MailObj = {
        UserId: parseInt(localStorage.getItem("UserID")),
        RuleId: "0",
        Params: {
        }
    };
    this.waitingProcessAllResponse = true;
    this._ClientImapService.insertMail(MailObj)
        .subscribe(
          OkResponse  => {
            this.waitingProcessAllResponse = false; 
            this.showInsertOkMessage(OkResponse);
           },
            error => {
                this.waitingProcessAllResponse = false;
                this._snackBar.open("No se ha podido insertar los correos.", 'Cerrar', {
                    horizontalPosition: this.horizontalPosition,
                    verticalPosition: this.verticalPosition,
                });
                console.log("Error", error);
            });

}


}
export class TableMap {
  CORREO_ELECTRONICO: string;
  NOMBRE_USUARIO: string;
  ID_PROCESO: string;
}
