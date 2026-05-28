import { Component, OnInit, Optional, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NzMessageService } from 'ng-zorro-antd/message';

import { ZambaService } from '../../services/zamba/zamba.service';

interface LinkData {
  id?: number;
  title: string;
  description: string;
  image: string;
  url: string;
}

@Component({
  selector: 'app-web-bookmark',
  templateUrl: './web-bookmark.component.html',
  styleUrls: ['./web-bookmark.component.less']
})
export class WebBookmarkComponent implements OnInit {
  @Input() singleLinkMode: boolean = false;
  bookmarks: LinkData[] = [];
  showInput: boolean = false;
  isLoading: boolean = false;
  linkUrl: string = '';
  linkTitle: string = '';

  constructor(
    private msg: NzMessageService,
    private route: ActivatedRoute,
    @Optional() private readonly zambaService: ZambaService | null,
  ) { }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      if (data['singleLinkMode'] !== undefined) {
        this.singleLinkMode = data['singleLinkMode'];
      }
    });
    this.loadBookmarks();
  }

  getContextData() {
    if (!this.zambaService) return { singleLinkMode: String(this.singleLinkMode) };
    const urlParams = this.zambaService.getParametersFromURL(window.location.href);
    return {
      docid: urlParams['docid'],
      doctypeid: urlParams['doctypeid'] || urlParams['doctype'],
      taskid: urlParams['taskid'],
      singleLinkMode: String(this.singleLinkMode)
    };
  }

  loadBookmarks() {
    if (!this.zambaService) {
      this.msg.error('ZambaService no está disponible.');
      // Fallback a datos mock si no hay servicio (opcional)
      this.bookmarks = [
        {
          id: 1,
          title: 'YouTube',
          description: 'Enjoy the videos and music you love...',
          image: '',
          url: 'https://www.youtube.com/',
        },
      ];
      return;
    }

    this.zambaService.getBookmarks(this.getContextData()).subscribe({
      next: (res: any) => {
        // Suponiendo que el backend devuelve { success: boolean, data: LinkData[] }
        // Ajusta esto según el formato real de tu API en .NET
        if (res && res.data) {
          this.bookmarks = res.data;
        }
      },
      error: err => {
        console.error(err);
        this.msg.error('Error al cargar marcadores');
      },
    });
  }

  toggleInput() {
    this.showInput = !this.showInput;
    if (!this.showInput) {
      this.linkUrl = '';
      this.linkTitle = '';
    }
  }

  crearMarcador() {
    if (!this.linkUrl) return;

    this.isLoading = true;

    const newBookmark: LinkData = {
      title: this.linkTitle.trim() ? this.linkTitle : 'Nuevo Enlace Generado',
      description: `Esta es la metadata obtenida para: ${this.linkUrl}`,
      image: '',
      url: this.linkUrl,
    };

    if (this.zambaService) {
      this.zambaService.saveBookmark(newBookmark, this.getContextData()).subscribe({
        next: (res: any) => {
          this.isLoading = false;

          // Si el backend nos devuelve toda la info enriquecida en res.data, la usamos
          if (res && res.data) {
            this.bookmarks.unshift(res.data);
          } else {
            // Fallback por si el backend no mandó data completa
            if (res && res.id) newBookmark.id = res.id;
            this.bookmarks.unshift(newBookmark);
          }

          this.msg.success('Marcador guardado exitosamente');
          this.linkUrl = '';
          this.linkTitle = '';
          this.showInput = false;
        },
        error: err => {
          console.error(err);
          this.isLoading = false;
          this.msg.error('Error al guardar el marcador');
        },
      });
    } else {
      // Comportamiento mock original
      setTimeout(() => {
        newBookmark.id = new Date().getTime();
        this.bookmarks.unshift(newBookmark);
        this.isLoading = false;
        this.linkUrl = '';
        this.linkTitle = '';
        this.showInput = false;
      }, 1000);
    }
  }

  eliminarMarcador(event: MouseEvent, bookmarkId: number | undefined) {
    event.preventDefault();
    event.stopPropagation();

    if (!bookmarkId) return;

    if (!this.zambaService) {
      this.bookmarks = this.bookmarks.filter(b => b.id !== bookmarkId);
      this.msg.success('Marcador eliminado (Mock)');
      return;
    }

    this.zambaService.deleteBookmark(bookmarkId, this.getContextData()).subscribe({
      next: () => {
        this.bookmarks = this.bookmarks.filter(b => b.id !== bookmarkId);
        this.msg.success('Marcador eliminado');
      },
      error: err => {
        console.error(err);
        this.msg.error('Error al eliminar el marcador');
      },
    });
  }

  copiarEnlace(event: MouseEvent, url: string) {
    event.preventDefault(); // Evitamos cambiar de vista/página
    event.stopPropagation(); // Evitamos que el evento llegue a la etiqueta <a>

    navigator.clipboard
      .writeText(url)
      .then(() => {
        this.msg.success('Enlace copiado al portapapeles');
      })
      .catch(err => {
        this.msg.error('Error al copiar el enlace');
        console.error('Error copiando al portapapeles: ', err);
      });
  }
}
