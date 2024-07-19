import { Component, ElementRef, HostListener, OnInit, ViewChild } from '@angular/core';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { SignatureService } from '../signature/signature.service';
import { catchError, finalize } from 'rxjs';
import { TokenService } from '@delon/auth';
import { SFStringWidgetSchema } from '@delon/form';

@Component({
  selector: 'app-signature2',
  templateUrl: './signature-v2.component.html',
  styleUrls: ['./signature-v2.component.less'],
})
export class SignatureV2Component {
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
  constructor(private modalRef: NzModalRef, private signatureService: SignatureService, private tokenService: TokenService) {

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

  SignOk(): void {
    this.pdfResult = '';
    this.showSpinner = true;
    let dataUrl = this.canvas.toDataURL();
    var genericRequest = {
      UserId: 0,
      token: 0,
      Params: { sign: dataUrl }
    };
    this.signatureService.SignPDF(genericRequest).pipe(
      finalize(() => {
        this.showSpinner = false;
      }),
    ).subscribe({
      next: (result) => {
        this.pdfResult = result;
        this.signatureSuccess = true;
        this.signatureError = false;
      },
      error: (error) => {
        this.signatureError = true;
        this.signatureSuccess = false;
      }
    });
  }
  SingnatureWithKeyboard(): void {
    this.pdfResult = '';
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
      Params: { sign: dataUrl }
    };
    this.signatureService.SignPDF(genericRequest).pipe(
      finalize(() => {
        this.showSpinner = false;
      }),
    ).subscribe({
      next: (result) => {
        this.pdfResult = result;
        this.signatureSuccess = true;
        this.signatureError = false;
      },
      error: (error) => {
        this.signatureError = true;
        this.signatureSuccess = false;
      }
    });
  }

  clearSignInput(): void {
    this.keyboardSignature = '';

  }
}