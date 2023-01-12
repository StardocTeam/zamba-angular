import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoginService } from '../../../../services/login.service';
import { FuseConfigService } from '@fuse/services/config.service';
import { fuseAnimations } from '@fuse/animations';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import {
    MatSnackBar,
    MatSnackBarHorizontalPosition,
    MatSnackBarVerticalPosition,
} from '@angular/material/snack-bar';
import { JsonpClientBackend } from '@angular/common/http';

@Component({
    selector: 'login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class LoginComponent implements OnInit {
    public static USER_MESSAGE_ERROR = 'El usuario ingresado es incorrecto';
    public static PASS_MESSAGE_ERROR = 'La clave ingresada es incorrecta'
    horizontalPosition: MatSnackBarHorizontalPosition = 'center';
    verticalPosition: MatSnackBarVerticalPosition = 'top';
    loginForm: FormGroup;
    validating: Boolean = false;

    /**
     * Constructor
     *
     * @param {FuseConfigService} _fuseConfigService
     * @param {FormBuilder} _formBuilder
     */
    constructor(
        private _fuseConfigService: FuseConfigService,
        private _formBuilder: FormBuilder,
        private loginService: LoginService,
        private router: Router,
        private _snackBar: MatSnackBar
    ) {
        // Configure the layout
        this._fuseConfigService.config = {
            layout: {
                navbar: {
                    hidden: true
                },
                toolbar: {
                    hidden: true
                },
                footer: {
                    hidden: true
                },
                sidepanel: {
                    hidden: true
                }
            }
        };
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void {
        this.loginForm = this._formBuilder.group({
            username: ['', [Validators.required]],
            password: ['', Validators.required]
        });

        localStorage.clear();
    }


    LoginOnClick() {
        try {
            this.validating = true;
            var data = {
                UserName: this.loginForm.value.username,
                Password: this.loginForm.value.password,
                ComputerNameOrIp: "127.1.1.5",
                name: "",
                lastName: "",
                eMail: "",
                type: ""
            }
            this.loginService.Login(data).subscribe(x => {

                this.ValidateLogin(x);

            }, e => {
                var showMessage = '';
                try {
                    if (e.error.Message.includes('clave'))
                        showMessage = LoginComponent.PASS_MESSAGE_ERROR;
                    else
                        showMessage = LoginComponent.USER_MESSAGE_ERROR;

                    this._snackBar.open(showMessage, 'Cerrar', {
                        duration: 3000,
                        horizontalPosition: this.horizontalPosition,
                        verticalPosition: this.verticalPosition,
                    });
                    this.validating = false;
                } catch (genericError) {
                    this._snackBar.open("No se pudo establecer la conexíon", 'Cerrar', {
                        horizontalPosition: this.horizontalPosition,
                        verticalPosition: this.verticalPosition,
                    });
                    this.validating = false;
                }

            }
            );
        } catch (error) {
        }

    }

    ValidateLogin(LoginObject) {
        var tokenUser = LoginObject.token;
        if (tokenUser != null && tokenUser !== undefined && tokenUser != "") {



            try {
                var data = {
                    Params:
                    {
                        "userIdEncrip": LoginObject.userID
                    }                   
                }
               this.loginService.DecryptUserName(data)
               .subscribe(
                   
                 x  => { this.setNameValues(x,LoginObject) },
                error  => {console.log("Error", error);}
               );
                
                
            } catch (error) {
                console.log(error);
            }
            
            
        } else {
            //this.toastr.error(message, title)

        }
        this.validating = false;

    }

    setNameValues(name, LoginObject){


        var tokenUser = LoginObject.token;
        localStorage.setItem("token", tokenUser);
        localStorage.setItem("username", this.loginForm.value.username);
        localStorage.setItem("UserID", JSON.parse(name).UserIdDecryp);



        this._snackBar.dismiss();
        //this.router.navigate(['apps/dashboards/analytics']);
        this.router.navigate(['/apps/IMAP/client']);

       

    }


}
