import { Component, OnInit, OnDestroy, Optional, Input, Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzModalService } from 'ng-zorro-antd/modal';
import { DOCUMENT } from '@angular/common';

import { ZambaService } from '../../services/zamba/zamba.service';

interface LinkData {
  id?: number;
  title: string;
  description: string;
  image: string;
  url: string;
}

type BookmarkChangeAction = 'added' | 'deleted';

@Component({
  selector: 'app-miniweb-bookmark',
  templateUrl: './miniweb-bookmark.component.html',
  styleUrls: ['./miniweb-bookmark.component.less']
})
export class MiniwebBookmarkComponent implements OnInit, OnDestroy {
  @Input() singleLinkMode: boolean = false;
  @Input() assetsUrl: string = 'assets/miniweb-bookmark';

  bookmarks: LinkData[] = [];
  showInput: boolean = false;
  isLoading: boolean = false;
  linkUrl: string = '';
  linkTitle: string = '';

  private currentDocId: string | undefined;
  private currentDocTypeId: string | undefined;
  private currentTaskId: string | undefined;

  constructor(
    private msg: NzMessageService,
    private modalSrv: NzModalService,
    private route: ActivatedRoute,
    @Optional() private readonly zambaService: ZambaService | null,
    @Inject(DOCUMENT) private readonly document: Document
  ) { }

  ngOnInit(): void {
    this.ensureExternalStylesheet();
    this.syncContextFromUrl();
    this.document.addEventListener('documentAdded', this.handleDocumentAdded as EventListener);
    this.route.data.subscribe(data => {
      if (data['singleLinkMode'] !== undefined) {
        this.singleLinkMode = data['singleLinkMode'];
      }
    });
    this.loadBookmarks();
  }

  ngOnDestroy(): void {
    this.document.removeEventListener('documentAdded', this.handleDocumentAdded as EventListener);
  }

  getContextData() {
    this.syncContextFromUrl();

    if (!this.zambaService) {
      return {
        docid: this.currentDocId,
        doctypeid: this.currentDocTypeId,
        taskid: this.currentTaskId,
        singleLinkMode: String(this.singleLinkMode)
      };
    }

    return {
      docid: this.currentDocId,
      doctypeid: this.currentDocTypeId,
      taskid: this.currentTaskId,
      singleLinkMode: String(this.singleLinkMode)
    };
  }

  private getNormalizedUrlParams(): Record<string, string> {
    if (this.zambaService) {
      const rawParams = this.zambaService.getParametersFromURL(window.location.href) || {};
      const normalized: Record<string, string> = {};

      Object.keys(rawParams).forEach(key => {
        const value = rawParams[key];
        if (value !== undefined && value !== null) {
          normalized[key.toLowerCase()] = String(value);
        }
      });

      return normalized;
    }

    const params = new URLSearchParams(globalThis.location?.search || '');
    const normalized: Record<string, string> = {};

    params.forEach((value, key) => {
      normalized[key.toLowerCase()] = value;
    });

    return normalized;
  }

  private syncContextFromUrl(): void {
    const urlParams = this.getNormalizedUrlParams();

    this.currentDocId = urlParams['docid'];
    this.currentDocTypeId = urlParams['doctypeid'] || urlParams['doctype'];
    this.currentTaskId = urlParams['taskid'];
  }

  private readonly handleDocumentAdded = (event: Event): void => {
    const customEvent = event as CustomEvent<{ mensaje?: string; docId?: string | number }>;
    const eventDocId = customEvent.detail?.docId !== undefined && customEvent.detail?.docId !== null
      ? String(customEvent.detail.docId)
      : undefined;

    this.syncContextFromUrl();

    if (!this.singleLinkMode || !eventDocId || !this.currentDocId || eventDocId !== this.currentDocId) {
      return;
    }

    this.clearSingleBookmark();
  };

  private emitSingleLinkBookmarkChanged(action: BookmarkChangeAction): void {
    if (!this.singleLinkMode) {
      return;
    }

    this.syncContextFromUrl();

    const event = new CustomEvent('bookmarkSingleLinkChanged', {
      detail: {
        action,
        docId: this.currentDocId,
        doctypeId: this.currentDocTypeId,
      },
    });

    this.document.dispatchEvent(event);
  }

  private clearSingleBookmark(): void {
    if (!this.bookmarks.length) {
      return;
    }

    const bookmarkToRemove = this.bookmarks[0];

    if (this.zambaService && bookmarkToRemove.id) {
      this.zambaService.deleteBookmark(bookmarkToRemove.id, this.getContextData()).subscribe({
        next: () => {
          this.bookmarks = this.bookmarks.filter(b => b.id !== bookmarkToRemove.id);
          this.resetInputState();
          this.emitSingleLinkBookmarkChanged('deleted');
        },
        error: err => {
          console.error(err);
        },
      });
      return;
    }

    this.bookmarks = this.bookmarks.slice(1);
    this.resetInputState();
    this.emitSingleLinkBookmarkChanged('deleted');
  }

  private resetInputState(): void {
    this.showInput = false;
    this.linkUrl = '';
    this.linkTitle = '';
  }

  private ensureExternalStylesheet(): void {
    const href = this.resolveAssetUrl('miniweb-bookmark.css');
    const existing = this.document.querySelector(`link[data-zamba-miniweb-bookmark-styles="${href}"]`);

    if (existing) {
      return;
    }

    const link = this.document.createElement('link');
    link.rel = 'stylesheet';
    link.href = href;
    link.setAttribute('data-zamba-miniweb-bookmark-styles', href);
    this.document.head.appendChild(link);
  }

  private resolveAssetUrl(pathSuffix?: string): string {
    const detectedAssetsUrl = this.detectAssetsUrlFromBundle();
    const assetsUrl = this.assetsUrl && this.assetsUrl !== 'assets/miniweb-bookmark' ? this.assetsUrl : detectedAssetsUrl;

    const basePath = assetsUrl.endsWith('/') ? assetsUrl : `${assetsUrl}/`;
    const relativePath = pathSuffix ? `${basePath}${pathSuffix}` : basePath;
    const baseUri = this.document.baseURI ?? globalThis.location?.href;

    if (!baseUri) {
      return relativePath;
    }

    if (basePath.startsWith('/') || basePath.startsWith('http')) {
      const url = new URL(relativePath, baseUri).toString();
      return pathSuffix ? url : url.replace(/\/$/, '');
    }

    const resolvedUrl = new URL(relativePath, baseUri).toString();
    return pathSuffix ? resolvedUrl : resolvedUrl.replace(/\/$/, '');
  }

  private detectAssetsUrlFromBundle(): string {
    const scripts = Array.from(this.document.querySelectorAll('script[src]')) as HTMLScriptElement[];
    const bundleScript =
      scripts.find(script => script.src.includes('/zamba-elements/main.js')) ??
      scripts.find(script => script.src.includes('/zamba-elements/runtime.js'));

    if (!bundleScript?.src) {
      return 'assets/miniweb-bookmark';
    }

    try {
      return new URL('./assets/miniweb-bookmark/', bundleScript.src).toString();
    } catch {
      return 'assets/miniweb-bookmark';
    }
  }

  loadBookmarks() {
    if (!this.zambaService) {
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
    if (!this.linkUrl || this.isLoading) return;
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
          if (res && res.data) {
            this.bookmarks.unshift(res.data);
          } else {
            if (res && res.id) newBookmark.id = res.id;
            this.bookmarks.unshift(newBookmark);
          }
          this.emitSingleLinkBookmarkChanged('added');
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
      setTimeout(() => {
        newBookmark.id = new Date().getTime();
        this.bookmarks.unshift(newBookmark);
        this.isLoading = false;
        this.emitSingleLinkBookmarkChanged('added');
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

    this.modalSrv.confirm({
      nzTitle: 'Confirmar eliminación',
      nzContent: '¿Desea borrar este marcador?',
      nzOnOk: () => {
        if (!this.zambaService) {
          this.bookmarks = this.bookmarks.filter(b => b.id !== bookmarkId);
          this.emitSingleLinkBookmarkChanged('deleted');
          return;
        }

        this.zambaService.deleteBookmark(bookmarkId, this.getContextData()).subscribe({
          next: () => {
            this.bookmarks = this.bookmarks.filter(b => b.id !== bookmarkId);
            this.emitSingleLinkBookmarkChanged('deleted');
          },
          error: err => {
            console.error(err);
          },
        });
      },
    });
  }

  copiarEnlace(event: MouseEvent, url: string) {
    event.preventDefault();
    event.stopPropagation();
    navigator.clipboard
      .writeText(url)
      .then(() => {
        this.msg.success('Enlace copiado');
      })
      .catch(err => {
        console.error('Error copiando:', err);
      });
  }
}
