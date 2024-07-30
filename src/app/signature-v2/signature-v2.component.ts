import { Component, ElementRef, HostListener, OnInit, ViewChild } from '@angular/core';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { SignatureService } from '../signature/signature.service';
import { catchError, finalize } from 'rxjs';
import { TokenService } from '@delon/auth';
import { SFStringWidgetSchema } from '@delon/form';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-signature2',
  templateUrl: './signature-v2.component.html',
  styleUrls: ['./signature-v2.component.less'],
})
export class SignatureV2Component implements OnInit {
  signatureColor: string = 'black';
  signatureWidth: number = 3;
  backgroundColor: string = 'white';
  xPrev: number = 0;
  yPrev: number = 0;
  xCurr: number = 0;
  yCurr: number = 0;
  drawing: boolean = false;

  confirm: boolean = false;

  showSpinner: boolean = false;
  signatureSuccess = false;
  signatureError = false;
  canvas: any;

  pdfResult: string = '';
  selectedTabIndex: number = 0;

  keyboardSignature: string = '';
  keyboardSignatureMaxLength: number = 20;

  docType: string = '';
  docId: string = '';

  hasSignature: boolean = false;

  saveSign = true;

  constructor(private router: Router, private route: ActivatedRoute, private modalRef: NzModalRef, private signatureService: SignatureService, private tokenService: TokenService, private sanitizer: DomSanitizer) {

  }
  ngOnInit(): void {
    if (this.modalRef.getContentComponent().record) {
      this.docType = this.modalRef.getContentComponent().record.docType;
      this.docId = this.modalRef.getContentComponent().record.docId;
    }
    this.signatureService.UserHasSignature({
      UserId: 0,
      token: 0,
      Params: {
      }
    }).pipe(
      finalize(() => {
      }),
    ).subscribe({
      next: (result) => {
        this.hasSignature = result.hasSignature;
      },
      error: (error) => {
      }
    });
  }


  previousStep(): void {
    this.confirm = false;
    this.clearCanvas();
  }

  getCoordinates(event: MouseEvent | TouchEvent) {
    if (this.canvas == null) {
      this.canvas = document.querySelector('#signatureCanvas');
      this.canvas.width = this.canvas.offsetWidth;
      this.canvas.height = this.canvas.offsetHeight;
    }
    let x, y;
    if (event instanceof MouseEvent) {
      x = event.clientX - this.canvas.getBoundingClientRect().left;
      y = event.clientY - this.canvas.getBoundingClientRect().top;
    } else {
      x = event.touches[0].clientX - this.canvas.getBoundingClientRect().left;
      y = event.touches[0].clientY - this.canvas.getBoundingClientRect().top;
    }
    return { x, y };
  }

  clearCanvas() {
    if (this.canvas == null) {
      this.canvas = document.querySelector('#signatureCanvas');
      this.canvas.width = this.canvas.offsetWidth;
      this.canvas.height = this.canvas.offsetHeight;
    }
    const context = this.canvas.getContext('2d');
    context.fillStyle = this.backgroundColor;
    context.fillRect(0, 0, this.canvas.width, this.canvas.height);

  }
  @HostListener('mousedown', ['$event'])
  @HostListener('touchstart', ['$event'])
  onStart(event: MouseEvent | TouchEvent) {
    if (this.confirm) {
      return;
    }
    this.drawing = true;
    const coordinates = this.getCoordinates(event);
    this.xCurr = coordinates.x;
    this.yCurr = coordinates.y;
    if (this.canvas == null) {
      this.canvas = document.querySelector('#signatureCanvas');
      this.canvas.width = this.canvas.offsetWidth;
      this.canvas.height = this.canvas.offsetHeight;
    }
    this.xPrev = this.xCurr;
    this.yPrev = this.yCurr;
  }

  @HostListener('mousemove', ['$event'])
  @HostListener('touchmove', ['$event'])
  onMove(event: MouseEvent | TouchEvent) {
    if (this.confirm || !this.drawing) {
      return;
    }
    const coordinates = this.getCoordinates(event);
    this.xCurr = coordinates.x;
    this.yCurr = coordinates.y;
    const context = this.canvas.getContext('2d');
    context.beginPath();
    context.moveTo(this.xPrev, this.yPrev);
    context.lineTo(this.xCurr, this.yCurr);
    context.strokeStyle = this.signatureColor;
    context.lineWidth = this.signatureWidth;
    context.stroke();
    context.closePath();
    this.xPrev = this.xCurr;
    this.yPrev = this.yCurr;
  }

  @HostListener('mouseup', ['$event'])
  @HostListener('touchend', ['$event'])
  onEnd(event: MouseEvent | TouchEvent) {
    this.drawing = false;
  }

  @HostListener('mouseout', ['$event'])
  onMouseOut(event: MouseEvent) {
    this.drawing = false;
  }

  closeModal(): void {
    this.modalRef.close(this.pdfResult);
  }
  confirmSign(): void {
    this.confirm = true;
    const context = this.canvas.getContext('2d');
    context.font = '23px Arial';
    context.fillStyle = 'black';

    const currentDate = new Date();
    const formattedDate = currentDate.toLocaleDateString('es-ES', {
      year: 'numeric',
      month: '2-digit',
      day: '2-digit',
    });

    const tokenData: any = this.tokenService.get();
    const fistName = tokenData['firstname'];
    const lastName = tokenData['lastname'];
    const text = `${fistName} ${lastName} - ${formattedDate}`;
    const textWidth = context.measureText(text).width;

    const xPosition = (this.canvas.width - textWidth) / 2;
    const yPosition = this.canvas.height - 5;

    context.fillText(text, xPosition, yPosition);
  }

  SignTask(): void {
    this.showSpinner = true;
    let dataUrl = this.canvas.toDataURL();
    var genericRequest = {
      UserId: 0,
      token: 0,
      Params: {
        sign: dataUrl,
        DocType: this.docType,
        DocId: this.docId,
        document: this.pdfResult,
        useLastSign: "false",
        saveSign: this.saveSign
      }
    };
    this.signatureService.SignTask(genericRequest).pipe(
      finalize(() => {
        this.showSpinner = false;
      }),
    ).subscribe({
      next: (result) => {
        this.signatureSuccess = true;
        this.signatureError = false;
      },
      error: (error) => {
        this.signatureError = true;
        this.signatureSuccess = false;
      }
    });
  }

  DoSign(wayToSign: string): void {
    this.pdfResult = '';
    this.showSpinner = true;
    var genericRequest = {
      UserId: 0,
      token: 0,
      Params: {
        doctypeId: this.docType,
        docid: this.docId,
        userId: this.tokenService.get()['userID']
      }
    };
    this.signatureService.getDocument(genericRequest).pipe(
      finalize(() => {
      }),
    ).subscribe({
      next: (result) => {
        this.pdfResult = JSON.parse(result).data;
        switch (wayToSign) {
          case 'keyboard':
            this.SingnatureWithKeyboard();
            break;
          case 'mouse':
            this.SignTask();
            break;
          case 'previous':
            this.UseLastSignature();
            break;
          default:
            break;
        }
      },
      error: (error) => {
        this.signatureError = true;
        this.signatureSuccess = false;
      }
    });

  }
  SingnatureWithKeyboard(): void {
    this.showSpinner = true;
    let tempCanvas = document.createElement('canvas');

    tempCanvas.width = 1400;
    tempCanvas.height = 250;
    let ctx = tempCanvas.getContext('2d');
    // Configurar estilo de texto
    if (ctx) {
      ctx.font = '68px PlaywriteIN';
      ctx.fillStyle = 'black';

      // fondo sea transparente
      ctx.clearRect(0, 0, tempCanvas.width, tempCanvas.height);

      let textWidth = ctx.measureText(this.keyboardSignature).width;

      let x = (tempCanvas.width - textWidth) / 2;

      let y = (tempCanvas.height / 2) + (30 / 2); // 30px es el tamaño de la fuente

      ctx.fillText(this.keyboardSignature, x, y); // Usa las posiciones x e y calculadas
    }
    let dataUrl = tempCanvas.toDataURL();
    var genericRequest = {
      UserId: 0,
      token: 0,
      Params: {
        sign: dataUrl,
        DocType: this.docType,
        DocId: this.docId,
        document: this.pdfResult,
        useLastSign: "false",
        saveSign: this.saveSign
      }
    };
    this.signatureService.SignTask(genericRequest).pipe(
      finalize(() => {
        this.showSpinner = false;
      }),
    ).subscribe({
      next: (result) => {
        this.signatureSuccess = true;
        this.signatureError = false;
      },
      error: (error) => {
        this.signatureError = true;
        this.signatureSuccess = false;
      }
    });
  }

  UseLastSignature(): void {
    this.showSpinner = true;
    let dataUrl = ""
    var genericRequest = {
      UserId: 0,
      token: 0,
      Params: {
        sign: dataUrl,
        DocType: this.docType,
        DocId: this.docId,
        document: this.pdfResult,
        useLastSign: "true",
        saveSign: this.saveSign
      }
    };
    this.signatureService.SignTask(genericRequest).pipe(
      finalize(() => {
        this.showSpinner = false;
      }),
    ).subscribe({
      next: (result) => {
        this.signatureSuccess = true;
        this.signatureError = false;
      },
      error: (error) => {
        this.signatureError = true;
        this.signatureSuccess = false;
      }
    });
  }

  reload(): void {
    console.log('reload');
    this.route.queryParams.subscribe(params => {
      // Navega a la misma ruta con los mismos parámetros de consulta
      this.router.navigate([], {
        relativeTo: this.route,
        queryParams: params,
        replaceUrl: true // Opcional: reemplaza la URL actual en el historial para no crear una entrada adicional
      });
    });
  }
  clearSignInput(): void {
    this.keyboardSignature = '';

  }
}