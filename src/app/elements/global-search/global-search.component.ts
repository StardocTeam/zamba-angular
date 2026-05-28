import { DOCUMENT } from '@angular/common';
import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  Inject,
  Input,
  OnInit,
  Optional,
  Output,
  ViewEncapsulation,
} from '@angular/core';

import { ZambaService } from '../../services/zamba/zamba.service';

type ElementInputValue = number | string | null | undefined;

interface GlobalSearchPayload {
  EntityId: string;
  IndexId: string;
  value: string;
}

@Component({
  selector: 'app-global-search-element',
  templateUrl: './global-search.component.html',
  styleUrls: ['./global-search.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.ShadowDom,
})
export class GlobalSearchElementComponent implements OnInit {
  @Input('entity-id') entityId?: ElementInputValue;
  @Input('index-id') indexId?: ElementInputValue;
  @Input() assetsUrl: string = 'assets/global-search';

  @Output() searchSubmitted = new EventEmitter<GlobalSearchPayload>();

  zambaReady = false;
  searchValue = '';

  constructor(
    @Optional() private readonly zambaService: ZambaService | null,
    @Inject(DOCUMENT) private readonly document: Document,
  ) {}

  ngOnInit(): void {
    this.ensureExternalStylesheet();
    this.syncInputsFromAttributes();
    this.zambaReady = !!this.zambaService;
  }

  onSearchSubmitted(): void {
    const payload = this.buildPayload();
    console.log(payload);
    this.searchSubmitted.emit(payload);
  }

  get entityIdLabel(): string {
    return this.normaliseValue(this.entityId) || 'No definido';
  }

  get indexIdLabel(): string {
    return this.normaliseValue(this.indexId) || 'No definido';
  }

  private buildPayload(): GlobalSearchPayload {
    return {
      EntityId: this.normaliseValue(this.entityId),
      IndexId: this.normaliseValue(this.indexId),
      value: this.searchValue.trim(),
    };
  }

  private syncInputsFromAttributes(): void {
    const host = this.document.querySelector('zamba-global-search');

    if (!this.entityId) {
      this.entityId = host?.getAttribute('entity-id') ?? host?.getAttribute('entityid') ?? undefined;
    }

    if (!this.indexId) {
      this.indexId = host?.getAttribute('index-id') ?? host?.getAttribute('indexid') ?? undefined;
    }
  }

  private ensureExternalStylesheet(): void {
    const href = this.resolveAssetUrl('global-search.css');
    const existing = this.document.querySelector(`link[data-zamba-global-search-styles="${href}"]`);

    if (existing) {
      return;
    }

    const link = this.document.createElement('link');
    link.rel = 'stylesheet';
    link.href = href;
    link.setAttribute('data-zamba-global-search-styles', href);
    this.document.head.appendChild(link);
  }

  private resolveAssetUrl(pathSuffix?: string): string {
    const detectedAssetsUrl = this.detectAssetsUrlFromBundle();
    const assetsUrl = this.assetsUrl && this.assetsUrl !== 'assets/global-search' ? this.assetsUrl : detectedAssetsUrl;

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
      return 'assets/global-search';
    }

    try {
      return new URL('./assets/global-search/', bundleScript.src).toString();
    } catch {
      return 'assets/global-search';
    }
  }

  private normaliseValue(value: ElementInputValue): string {
    if (value === null || value === undefined) {
      return '';
    }

    return String(value).trim();
  }
}
