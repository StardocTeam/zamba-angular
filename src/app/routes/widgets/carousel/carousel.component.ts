import {
  Component,
  OnInit,
  Inject,
  Input,
  EventEmitter,
  ChangeDetectorRef,
  ElementRef,
  Renderer2,
  QueryList,
  SimpleChanges,
  ViewChild,
  ViewChildren,
  AfterViewInit,
  OnDestroy
} from '@angular/core';
import { DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';
import { GridsterItem } from 'angular-gridster2';
import { NzCarouselComponent } from 'ng-zorro-antd/carousel';
import { Subscription, catchError } from 'rxjs';

import { CarouselService } from './service/carousel.service';

@Component({
  selector: 'app-carousel',
  styles: [],
  templateUrl: './carousel.component.html',
  styleUrls: ['./carousel.component.less']
})
export class CarouselComponent implements OnInit, OnDestroy {
  @Input() showImages: boolean = false;
  @ViewChild('myCarousel') myCarousel!: NzCarouselComponent;

  @Input()
  widget: GridsterItem = {
    type: '',
    title: '',
    cols: 0,
    rows: 0,
    x: 0,
    y: 0,
    resizeEvent: new EventEmitter<GridsterItem>()
  };

  @Input()
  resizeEvent: EventEmitter<GridsterItem> = new EventEmitter<GridsterItem>();

  @Input()
  changeEvent: EventEmitter<GridsterItem> = new EventEmitter<GridsterItem>();

  @Input()
  gridItem: ElementRef | undefined;

  private resizeSubscription: Subscription | undefined;
  private changeSubscription: Subscription | undefined;
  config: any = [];
  listContent = new Array();
  images: boolean = true;

  dotPosition = 'top'; //posible options: buttom, rigth, left
  AutoPlaySpeed = 5000;
  AutoPlay = true;
  EnableSwipe = true;
  Loop = true;
  classImg = 'max-width: 100%';

  carouselItems = [
    { image: 'path/to/image1.jpg', caption: 'Caption 1' },
    { image: 'path/to/image2.jpg', caption: 'Caption 2' },
    { image: 'path/to/image3.jpg', caption: 'Caption 3' }
    // Agrega más elementos según sea necesario
  ];

  constructor(
    @Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
    private carouselService: CarouselService,
    private cdr: ChangeDetectorRef,
    private renderer: Renderer2
  ) {}

  ChangeFlag() {
    this.showImages = !this.showImages;
  }

  @ViewChildren('elementosDinamicos') elementosDinamicos!: QueryList<ElementRef>;

  ngOnInit(): void {
    this.getCarouselConfig();
    this.getCarouselContent();

    this.resizeSubscription = this.resizeEvent.subscribe((event: any) => {
      if (this.widget['name'] == event.item['name']) {
        var Height = event.itemComponent.el.offsetHeight;
        var Width = event.itemComponent.el.offsetWidth;

        //TODO: Hacer cambio de dimensiones con los elementos y no con el evento.

        //Primera carga de los elementos
        var imgs = event.itemComponent.el.getElementsByTagName('img');
        //imgs = this.myCarousel.el.getElementsByTagName('img');

        this.showImages = false;
        this.cdr.detectChanges();

        this.showImages = true;
        this.cdr.detectChanges();

        for (let index = 0; index < imgs.length; index++) {
          if (Height > Width) {
            if (imgs[index].offsetHeight < Height) {
              imgs[index].style['max-width'] = `${Width}px`;
              this.classImg = 'max-height: 100%';
            } else {
              imgs[index].style['max-height'] = `${Height}px`;
              this.classImg = 'max-width: 100%';
            }
          } else {
            if (imgs[index].offsetWidth < Width) {
              imgs[index].style['max-height'] = `${Height}px`;
              this.classImg = 'max-width: 100%';
            } else {
              imgs[index].style['max-width'] = `${Width}px`;
              this.classImg = 'max-height: 100%';
            }
          }
        }

        this.cdr.detectChanges();
      }
    });

    this.changeSubscription = this.changeEvent.subscribe((item: any) => {});
  }

  ngOnDestroy() {
    if (this.resizeSubscription) {
      this.resizeSubscription.unsubscribe();
    }
  }

  getCarouselContent() {
    this.EnableSwipe = true;

    const tokenData: any = this.tokenService.get();
    let genericRequest = {};
        genericRequest = {
          UserId: 0,
          token: tokenData['token'],

        Params: ''
      };

      this.carouselService
        ._getCarouselContent(genericRequest)
        .pipe(
          catchError(error => {
            // Manejo de errores
            this.images = false;
            this.cdr.detectChanges();
            console.error('Error al obtener datos:', error);
            throw error; // Puedes relanzar el error o retornar un valor por defecto
          })
        )
        .subscribe(
          data => {
            this.images = true;
            this.cdr.detectChanges();
            this.listContent = JSON.parse(data);

            if (this.listContent.length == 0) {
              this.EnableSwipe = false;
            }

            this.cdr.detectChanges();
          },
          error => {
            this.images = false;
            this.cdr.detectChanges();
            console.error('Error al obtener datos:', error);
          }
        );
  }

  getCarouselConfig() {
    const tokenData: any = this.tokenService.get();
    let genericRequest = {};

    genericRequest = {
          UserId: 0,
          token: tokenData['token'],
        Params: ''
      };

      this.carouselService
        ._getCarouselConfig(genericRequest)
        .pipe(
          catchError(error => {
            // Manejo de errores
            this.images = false;
            this.cdr.detectChanges();
            console.error('Error al obtener datos:', error);
            throw error; // Puedes relanzar el error o retornar un valor por defecto
          })
        )
        .subscribe(
          data => {
            this.images = true;
            this.cdr.detectChanges();
            var dataJson = JSON.parse(data);

            this.dotPosition = dataJson.DotPosition;
            this.AutoPlaySpeed = dataJson.AutoPlaySpeed;
            this.AutoPlay = dataJson.AutoPlay;
            this.EnableSwipe = dataJson.EnableSwipe;
            this.Loop = dataJson.Loop;
            this.cdr.detectChanges();
          },
          error => {
            this.images = false;
            this.cdr.detectChanges();
            console.error('Error al obtener datos:', error);
          }
        );
  }

  nextSlide() {
    this.myCarousel.next();
  }

  prevSlide() {
    this.myCarousel.pre();
  }
}
