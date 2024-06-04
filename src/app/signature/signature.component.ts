import { Component, ElementRef, HostListener, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-signature',
  templateUrl: './signature.component.html',
  styleUrls: ['./signature.component.less'],
})
export class SignatureComponent implements OnInit {
  signatureColor: string = 'black';
  signatureWidth: number = 2;
  backgroundColor: string = 'white';
  xPrev: number = 0;
  yPrev: number = 0;
  xCurr: number = 0;
  yCurr: number = 0;
  drawing: boolean = false;

  canvas: any;
  constructor() {

  }
  ngOnInit(): void {
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
    this.drawing = true;
    const coordinates = this.getCoordinates(event);
    this.xCurr = coordinates.x;
    this.yCurr = coordinates.y;
    const context = this.canvas.getContext('2d');
    context.beginPath();
    context.fillStyle = this.signatureColor;
    context.fillRect(this.xCurr, this.yCurr, this.signatureWidth, this.signatureWidth);
    context.closePath();
    this.xPrev = this.xCurr;
    this.yPrev = this.yCurr;
  }

  @HostListener('mousemove', ['$event'])
  @HostListener('touchmove', ['$event'])
  onMove(event: MouseEvent | TouchEvent) {
    if (!this.drawing) return;
    const coordinates = this.getCoordinates(event);
    this.xCurr = coordinates.x;
    this.yCurr = coordinates.y;
    const context = this.canvas.getContext('2d');
    context.beginPath();
    context.moveTo(this.xPrev, this.yPrev);
    context.lineTo(this.xCurr, this.yCurr);
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
}