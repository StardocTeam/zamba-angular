import { ChangeDetectorRef, Component, Inject, OnInit } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';
import { NzModalService } from 'ng-zorro-antd/modal';
import { environment } from '../../../../environments/environment';
import { SharedService } from '../../../services/zamba/shared.service';
import { ZambaService } from '../../../services/zamba/zamba.service';

@Component({
  selector: 'app-rule',
  templateUrl: './rule.component.html',
  styles: [
    `
      #ruleComponentIframe {
        width: 100%;
        height: 100%;
      }

      #main-spinner {
        position: fixed;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
      }
    `
  ]
})
export class RuleComponent implements OnInit {
  WebUrl = environment['zambaWeb'];
  result: boolean;

  navigateUrl!: SafeResourceUrl;
  safeZambaUrl: SafeResourceUrl = '';
  nextStepUrl: SafeResourceUrl = '';

  constructor(
    private sanitizer: DomSanitizer,
    private zambaService: ZambaService,
    private route: ActivatedRoute,
    public sharedService: SharedService,
    @Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
    private cdr: ChangeDetectorRef,

    private modalSrv: NzModalService,
    private router: Router,
  ) {
    this.result = false;
  }

  ngOnInit(): void {
    window.addEventListener('message', event => {
      if (event.data === 'login-rrhh-ok') {
        this.safeZambaUrl = this.sanitizer.bypassSecurityTrustResourceUrl('');
        this.nextStep();
      } else {
this.router.navigate(['passport','login'])     
        // ver que hacer cuando falla la auth en zamba
      }
    });

    this.result = false;

    this.route.queryParams.subscribe(params => {
      const tokenData: any = this.tokenService.get();
      let genericRequest = {};
      if (tokenData != null) {
        console.log('Imprimo los valores en tokenService en el service', tokenData);

        genericRequest = {
          UserId: tokenData['userID'],
          token: tokenData['jwt'],
          Params: params
        };
      }

      this.zambaService.executeRule(genericRequest).subscribe({
        next: data => {
          switch (params['typeRule']) {
            case 'executeViewTask':
              console.log('Datos recibidos:', data);

              let result = JSON.parse(data);
              let urlTask = result.Vars.taskurl;

              let newUrl = `${this.WebUrl}${urlTask}`;
              // Encode string to Base64
              //const encodedString = this.encodeStringToBase64(JSON.stringify(tokenData.token));
              const encodedString = tokenData.token;

              newUrl = `${newUrl}&modalmode=true&t=${encodedString}`;
              this.nextStepUrl = this.sanitizer.bypassSecurityTrustResourceUrl(newUrl);

              // Abre una nueva ventana o pestaña con la URL especificada
              this.safeZambaUrl = this.zambaService.preFlightLogin();
              this.cdr.detectChanges();

              //this.navigateUrl = this.sanitizer.bypassSecurityTrustResourceUrl(newUrl);
              // this.result = true;
              // this.cdr.detectChanges();

              // const newtab = window.open(newUrl, '_blank');
              // newtab?.postMessage({ authToken: JSON.stringify(tokenData) }, '*');

              // this.navigateUrl = this.sanitizer.bypassSecurityTrustResourceUrl(newUrl);
              //// Abre una nueva ventana o pestaña con la URL especificada
              // window.open(newUrl, '_blank');

              break;
          }
        },
        error: error => {
          console.error('Error al obtener datos:', error);
          this.result = true;
          this.cdr.detectChanges();

          this.modalSrv.confirm({
            nzTitle: `Ups! Tuvimos un problema`,
            nzContent: environment.production
              ? `No pudimos procesar tu solicitud, en caso de continuar el problema contacta al area de soporte.`
              : `Ver：${JSON.stringify(error)}`,
            nzCancelDisabled: true,
            nzOkText: 'OK',
            nzOnOk: () => this.cdr.detectChanges()
          });
        }
      });
    });
  }

  nextStep() {
    this.navigateUrl = this.nextStepUrl;
    this.result = true;
    this.cdr.detectChanges();
  }
  encodeStringToBase64(str: string): string {
    return btoa(str);
  }

  // Function to decode base64 to string
  decodeBase64ToString(base64: string): string {
    return atob(base64);
  }
}
