import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subject } from 'rxjs';
import { ActivatedRoute } from '@angular/router'
import { MatDialog } from '@angular/material/dialog';
import { V } from '@angular/cdk/keycodes';
import { FormsModule } from "@angular/forms";
import { Imap } from "../../../entities/Imap";
import { EditService } from "./edit.service";
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { Mail } from "../../../entities/Mail";
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';

@Component({
    selector: 'app-edit',
    templateUrl: './edit.component.html',
    styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {
  componentName: string = "Nuevo Proceso";
  Field: string = "";
  ValFilter: string = "";
  New: Boolean = false;
  Recent: boolean = false;
  ExportAttachment: boolean = false;
  EntitiesValue;
  IndicesValue;
  ExportationType;
  FilterFieldOptions;
  selectedEntity:string;
  isUpdate:Boolean = false; 
  hidePassword = true;
  waitingResponse:Boolean = false;
  currentProcessId:Number = -1;      
  horizontalPosition: MatSnackBarHorizontalPosition = 'center';
  verticalPosition: MatSnackBarVerticalPosition = 'top';
  FilterProtocolOptions;
  dataSourceMails: MatTableDataSource<Mail>;
  displayedColumns: string[] = ['UniqueId', 'Date', 'Sender'];
  ShowMailsGrid:Boolean = false;
  NgFilter:boolean = false;

    @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
    @ViewChild(MatSort, { static: true }) sort: MatSort;

    form: FormGroup;

    // Horizontal Stepper
    horizontalStepperStep1: FormGroup;
    horizontalStepperStep2: FormGroup;
    horizontalStepperStep3: FormGroup;
    horizontalStepperStep4: FormGroup;
    horizontalStepperStep5: FormGroup;

    // Vertical Stepper
    verticalStepperStep1: FormGroup;
    verticalStepperStep2: FormGroup;
    verticalStepperStep3: FormGroup;

    // Private
    private _unsubscribeAll: Subject<any>;

    /**
     * Constructor
     *
     * @param {FormBuilder} _formBuilder
     */
    constructor(
        private _formBuilder: FormBuilder,
        private _routeActivated: ActivatedRoute,
        public dialog: MatDialog,
        private _EditService: EditService,
        private _snackBar: MatSnackBar
    ) {
        // Set the private defaults
        this._unsubscribeAll = new Subject();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    ngOnInit(): void {
        let imapProcessData = new Imap();
        this.currentProcessId = -1;
        this.FilterFieldOptions = ['Ninguno','Para','De','CC','CCO','Body'];
        this.FilterProtocolOptions = ['None','Ssl','StartTls'];

        var value = this._routeActivated.queryParamMap.subscribe(params => {
            var paramData = JSON.parse(params.get('process'));
            if (paramData != null) {
                this.isUpdate = true;
                this.componentName = "Editar Proceso";
                imapProcessData = paramData;
                this.NgFilter = parseInt(imapProcessData.HAS_FILTERS) == 0 ? false : true;
                console.log(imapProcessData);
                this.currentProcessId = imapProcessData.PROCESS_ID;
            }
        });

        // Horizontal Stepper form steps
        this.horizontalStepperStep1 = this._formBuilder.group({
            processsname: [(this.isUpdate ? imapProcessData.PROCESS_NAME : ''), [Validators.required]],
            username: [(this.isUpdate ? imapProcessData.USER_NAME : ''), [Validators.required]],
            email: [(this.isUpdate ? imapProcessData.EMAIL : ''), [Validators.required, Validators.email]],
            password: [(this.isUpdate ? imapProcessData.USER_PASSWORD : ''), [Validators.required]],
        });

        this.horizontalStepperStep2 = this._formBuilder.group({

            ipAddress: [(this.isUpdate ? imapProcessData.IP_ADDRESS : ''), Validators.required],
            port: [(this.isUpdate ? imapProcessData.FIELD_PORT : ''), Validators.required],
            protocol: [(this.isUpdate ? imapProcessData.FIELD_PROTOCOL : ''), Validators.required]
        });

        this.horizontalStepperStep3 = this._formBuilder.group({
            ExportAttachment: [(this.isUpdate ? imapProcessData.EXPORT_ATTACHMENTS_SEPARATELY : '')],
            Folder_Name: [(this.isUpdate ? imapProcessData.FOLDER_NAME : ''), Validators.required],
            Filter_Field: [(this.isUpdate ? imapProcessData.FILTER_FIELD : '')],
            Filter_Value: [(this.isUpdate ? imapProcessData.FILTER_VALUE : '')],
            Filter: [(this.isUpdate ? imapProcessData.HAS_FILTERS : '0')],
            Filter_Recents: [(this.isUpdate ? imapProcessData.FILTER_RECENTS : '')],
            Filter_NotReads: [(this.isUpdate ? imapProcessData.FILTER_NOT_READS : '')]
        });

        this.horizontalStepperStep4 = this._formBuilder.group({
            AutoIncrement   : [(this.isUpdate ? imapProcessData.AUT_INCREMENT : '')],
            SentBy   : [(this.isUpdate ? imapProcessData.SENT_BY : ''), Validators.required],
            To   : [(this.isUpdate ? imapProcessData.FIELD_TO : ''), Validators.required],
            CC   : [(this.isUpdate ? imapProcessData.CC : '')],
            BCC   : [(this.isUpdate ? imapProcessData.CCO : '')],
            Subject   : [(this.isUpdate ? imapProcessData.SUBJECT : ''), Validators.required],
            Body   : [(this.isUpdate ? imapProcessData.FIELD_BODY : '')],
            Date   : [(this.isUpdate ? imapProcessData.FIELD_DATE : ''), Validators.required],
            UserZamba   : [(this.isUpdate ? imapProcessData.Z_USER : '')],
            CodMail   : [(this.isUpdate ? imapProcessData.CODE_MAIL : '')],
            //TipoExportacion   : [(this.isUpdate ? imapProcessData.TYPE_OF_EXPORT : '')],
            TipoExportacion: ['1'],
            Entity: [(this.isUpdate ? imapProcessData.ENTITY_ID : ''), Validators.required],
        });

        if (this.isUpdate) {
            this.selectedEntity = imapProcessData.ENTITY_ID;
            this.SelectIndices(this.selectedEntity);
        }


        try {
            var data = {}
            this._EditService.getEntities(data)
                .subscribe(x => {
                    this.ConvertEntities(x)
                },
                    error => {
                        console.log("Error", error);
                    });

        } catch (error) {
            console.log("hola");
            console.log(error);
        }
    }

    ConvertEntities(entities) {
        this.EntitiesValue = JSON.parse(entities);
    }

    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }
        //Crea un objeto IMAP
    Confirm_ObjectImap(){


        if(this.horizontalStepperStep3.value.Filter == "filtrado")
        {
            this.horizontalStepperStep3.value.Filter_Field = "";
            this.horizontalStepperStep3.value.Filter_Value = "";
        }

        var ObjImap = {
                "UserId": parseInt(localStorage.getItem("UserID")),
                "Params": {
                    "IS_ACTIVE":1,
                    "PROCESS_ID": this.currentProcessId,
                    "USER_NAME": this.horizontalStepperStep1.value.username,
                    "PROCESS_NAME": this.horizontalStepperStep1.value.processsname,
                    "EMAIL": this.horizontalStepperStep1.value.email,
                    "USER_ID": parseInt(localStorage.getItem("UserID")),
                    "USER_PASSWORD": this.horizontalStepperStep1.value.password,

                    "IP_ADDRESS": this.horizontalStepperStep2.value.ipAddress,
                    "FIELD_PORT": this.horizontalStepperStep2.value.port,
                    "FIELD_PROTOCOL": this.horizontalStepperStep2.value.protocol,
                    "HAS_FILTERS": this.horizontalStepperStep3.value.Filter == "1"? 1: 0,
                    "FILTER_FIELD": this.horizontalStepperStep3.value.Filter_Field == null? "":this.horizontalStepperStep3.value.Filter_Field,
                    "FILTER_VALUE": this.horizontalStepperStep3.value.Filter_Value == null?"":this.horizontalStepperStep3.value.Filter_Value,
                    "FILTER_RECENTS": this.horizontalStepperStep3.value.Filter_Recents == true ? 1: 0,
                    "FILTER_NOT_READS": this.horizontalStepperStep3.value.Filter_NotReads == true ? 1: 0,
                    "EXPORT_ATTACHMENTS_SEPARATELY": this.horizontalStepperStep3.value.ExportAttachment == true ? 1: 0,

                    "FOLDER_NAME": this.horizontalStepperStep3.value.Folder_Name,
                    "ENTITY_ID": this.selectedEntity,
                    "SENT_BY": this.horizontalStepperStep4.value.SentBy,
                    "FIELD_TO": this.horizontalStepperStep4.value.To,
                    "CC": this.horizontalStepperStep4.value.CC,
                    "CCO": this.horizontalStepperStep4.value.BCC,
                    "SUBJECT": this.horizontalStepperStep4.value.Subject,
                    "FIELD_BODY": this.horizontalStepperStep4.value.Body,
                    "FIELD_DATE": this.horizontalStepperStep4.value.Date,
                    "Z_USER": this.horizontalStepperStep4.value.UserZamba,
                    "CODE_MAIL": this.horizontalStepperStep4.value.CodMail,
                    "TYPE_OF_EXPORT": this.horizontalStepperStep4.value.TipoExportacion,
                    "AUT_INCREMENT": this.horizontalStepperStep4.value.AutoIncrement
                }
            };
            console.log(ObjImap);
            this.waitingResponse = true;
            this._EditService.SaveImapProcess(ObjImap).subscribe(
                OkResponse  => { this.InsertObject(OkResponse) },
                error  => {
                    this.showInsertProcessMessageError(error);
                    
                }
            );
            
        }

        InsertObject(OkResponse){

            let message = "";
            if(this.isUpdate)   
                message = "El proceso se ha actualizado exitosamente";
            else
            message = "El proceso se ha creado exitosamente";
            this._snackBar.open(message, 'Ok', {
                duration: 2000,
                horizontalPosition: this.horizontalPosition,
                verticalPosition: this.verticalPosition,
            });
            console.log(OkResponse);
            this.waitingResponse = false;
        }

        showInsertProcessMessageError(error){
            this._snackBar.open("Ha ocurrido un error al intentar crear el nuevo proceso", 'Cerrar', {
                duration: 3000,
                horizontalPosition: this.horizontalPosition,
                verticalPosition: this.verticalPosition,
            });
            console.log("Error", error);
            this.waitingResponse = false;
        }

        verFormulario()
        {
            console.log(this.horizontalStepperStep3.value);
            console.log(this.horizontalStepperStep4.value);
        }
        SelectIndices(option){
            try {
                var data = {
                    UserId: parseInt(localStorage.getItem("UserID")),
                    Params:
                    {
                        "IndexID": option
                    }                   
                }
               this._EditService.getIndexForEntities(data)
               .subscribe(
                   
                 x  => { this.setComboIndexValues(x) },
                error  => {console.log("Error", error);}
               );
                
                
            } catch (error) {
                console.log(error);
            }
        }

    setComboIndexValues(Indices) {
        this.IndicesValue = JSON.parse(Indices);
        this.ExportationType = [{text: "Automática", id:"1"}]
    }

        GetConection(){
            if(this.horizontalStepperStep2.value.ipAddress != "" && this.horizontalStepperStep2.value.port != "" && this.horizontalStepperStep2.value.protocol != "")
            {
                var ConnectObj = { 
                    UserId: parseInt(localStorage.getItem("UserID")), //necesito usuario id
                    Params: {
                        "Host": this.horizontalStepperStep2.value.ipAddress,
                        "Port": this.horizontalStepperStep2.value.port,
                        "ProtCon": this.horizontalStepperStep2.value.protocol,
                        "UserName": this.horizontalStepperStep1.value.username,
                        "UserPass": this.horizontalStepperStep1.value.password,
                    }
                }
    
    
                this._EditService.getConection(ConnectObj)
                .subscribe(
                    data  => {
                        this._snackBar.open("Conexion exitosa", 'Cerrar', {
                            duration: 3000,
                            horizontalPosition: this.horizontalPosition,
                            verticalPosition: this.verticalPosition
                        });
                        console.log("Data:", data);
                    },
                    error  => {
                        this._snackBar.open("Fallo la conexión con los parámetros que se han establecido", 'Cerrar', {
                            horizontalPosition: this.horizontalPosition,
                            verticalPosition: this.verticalPosition,
                        });
                        console.log("Error", error);
                    });
            }
            else
            {
                this._snackBar.open("Debe completar los datos de la conexión", 'Cerrar', {
                horizontalPosition: this.horizontalPosition,
                verticalPosition: this.verticalPosition,
                });
            }
            
    }

    GetEMails() {
        var ParamsObj = {
            UserId: parseInt(localStorage.getItem("UserID")),
            RuleId: "0",
            Params: {
                "Host": this.horizontalStepperStep2.value.ipAddress,
                "Port": this.horizontalStepperStep2.value.port,
                "ProtCon": this.horizontalStepperStep2.value.protocol,
                "UserName": this.horizontalStepperStep1.value.username,
                "UserPass": this.horizontalStepperStep1.value.password,
                "Folder": this.horizontalStepperStep3.value.Folder_Name,
                "Filter_Field": this.horizontalStepperStep3.value.Filter_Field,
                "Filter_Value": this.horizontalStepperStep3.value.Filter_Value,
                "NewEmails": this.horizontalStepperStep3.value.Filter_NotReads == 1 ? "true" : "false",
                "RecentEmails": this.horizontalStepperStep3.value.Filter_Recents == 1 ? "true" : "false",
                "Todos": this.horizontalStepperStep3.value.Filter == "0" ? "true" : "false",
                "Filtrado": this.horizontalStepperStep3.value.Filter == "1" ? "true" : "false",
                "resultIds": "0",
                "userId": parseInt(localStorage.getItem("UserID"))
            }
        }

        this._EditService.getEMails(ParamsObj)
            .subscribe(
                data => {
                    this.ListEmailsTable(data);
                    console.log("Data", data);
                    this.ShowMailsGrid = true;
                },
                error => {
                    this._snackBar.open("No se ha podido obtener los correos.", 'Cerrar', {
                        horizontalPosition: this.horizontalPosition,
                        verticalPosition: this.verticalPosition,
                    });
                    console.log("Error", error);
                });

    }



    private ListEmailsTable(data) {
        var dataParced: Mail[] = JSON.parse(data);
        var dataSource = new MatTableDataSource(dataParced);
        console.log(dataParced);
        this.dataSourceMails = dataSource;
        this.dataSourceMails.paginator = this.paginator;
        this.dataSourceMails.sort = this.sort;
    }

    InsertMails() {
        var MailObj = {
            UserId: parseInt(localStorage.getItem("UserID")), //necesito usuario id
            RuleId: "0",
            Params: {
                "Host": this.horizontalStepperStep2.value.ipAddress,
                "Port": this.horizontalStepperStep2.value.port,
                "ProtCon": this.horizontalStepperStep2.value.protocol,
                "UserName": this.horizontalStepperStep1.value.username,
                "UserPass": this.horizontalStepperStep1.value.password,
                "Folder": this.horizontalStepperStep3.value.Folder_Name,
                "Filter_Field": this.horizontalStepperStep3.value.Filter_Field,
                "Filter_Value": this.horizontalStepperStep3.value.Filter_Value,
                "Entity": this.horizontalStepperStep4.value.Entity,
                "EXPORT_ATTACHMENTS_SEPARATELY": this.horizontalStepperStep3.value.ExportAttachment == true ? 1: 0,
                "NewEmails": this.horizontalStepperStep3.value.Filter_NotReads == 1 ? "true" : "false",
                "RecentEmails": this.horizontalStepperStep3.value.Filter_Recents == 1 ? "true" : "false",
                "Todos": this.horizontalStepperStep3.value.Filter == "0" ? "true" : "false",
                "Filtrado": this.horizontalStepperStep3.value.Filter == "1" ? "true" : "false",
                "resultIds": "0",
                "userId": parseInt(localStorage.getItem("UserID"))
            }
        }

        this._EditService.insertMail(MailObj)
            .subscribe(
                data => {
                    
                    //this.InsertObject(data); //??
                    console.log("data", data);
                    this.ShowMailsGrid = true;
                },
                error => {
                    this._snackBar.open("No se ha podido insertar los correos.", 'Cerrar', {
                        horizontalPosition: this.horizontalPosition,
                        verticalPosition: this.verticalPosition,
                    });
                    console.log("Error", error);
                });

    }


    //#endregion
}
