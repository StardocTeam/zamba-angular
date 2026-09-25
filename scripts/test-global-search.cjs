const { test } = require('node:test');
const assert = require('node:assert/strict');
const ts = require('typescript');
const fs = require('node:fs');
const vm = require('node:vm');
const decorator = () => () => {};
function load(file, extra = {}) {
  const exports = {};
  const code = ts.transpileModule(fs.readFileSync(`src/app/elements/global-search/${file}.ts`, 'utf8'), {
    compilerOptions: { module: ts.ModuleKind.CommonJS, target: ts.ScriptTarget.ES2022, experimentalDecorators: true }
  }).outputText;
  vm.runInNewContext(code, { exports, URL, window: {}, require: name => ({
    '@angular/common': { DOCUMENT: {} },
    '@angular/core': { Injectable: decorator, Inject: decorator, Component: decorator, Input: decorator,
      Output: decorator, ViewChild: decorator, EventEmitter: class { emit() {} }, ChangeDetectionStrategy: {}, ViewEncapsulation: {} },
    '@angular/common/http': { HttpClient: class { constructor(backend) { this.backend = backend; } post(...args) { return this.backend.post(...args); } },
      HttpHeaders: class { constructor(values) { this.values = values; } }, HttpParams: class { constructor() { this.values = {}; } set(k,v) { this.values[k] = v; return this; } } },
    rxjs: { firstValueFrom: value => Promise.resolve(value) }, ...extra
  })[name] });
  return exports;
}
const serviceModule = load('global-search.service');
function fixture(permission = false) {
  const requests = [];
  const host = { GetUID: () => '42', ZambaWebRestApiURL: 'https://example.test/api/', thisDomain: 'https://example.test/Zamba.Web',
    location: { origin: 'https://example.test' }, localStorage: { getItem: () => 'stored-token' } };
  const doc = { defaultView: host, baseURI: 'https://example.test/', body: {}, querySelector: () => null };
  const backend = { post: (url, body, options) => { requests.push({url,body,options}); return url.includes('GetUsersWFStepsRights') ? permission : { data: [], total: 0 }; } };
  return { service: new serviceModule.GlobalSearchService(backend,doc), host, requests };
}
test('legacy search body, zero-based pagination and stored bearer', async () => {
  const {service,requests} = fixture(); await service.search('texto',2);
  assert.equal(requests[0].url,'https://example.test/api/search/Results');
  assert.equal(requests[0].body.UserId,42);
  assert.equal(requests[0].body.SizePage.LastPage,2);
  assert.equal(requests[0].body.SizePage.PageSize,100);
  assert.equal(requests[0].body.Parameters[0].operator,'Empieza');
  assert.equal(requests[0].options.headers.values.Authorization,'Bearer stored-token');
});
test('AngularJS bearer has priority over persisted token', async () => {
  const {service,host,requests} = fixture();
  host.angular = { element: () => ({ injector: () => ({ get: () => ({ defaults: { headers: { common: {Authorization:'Bearer live-token'} } } }) }) }) };
  await service.search('texto',0); assert.equal(requests[0].options.headers.values.Authorization,'Bearer live-token');
});
test('a permitted task opens TaskViewer directly with aliases and token', async () => {
  const {service,requests} = fixture(true);
  const url = new URL(await service.resultUrl({Doc_Id:9,Doc_Type_Id:8,Step_Id:7,Task_Id:6}));
  assert.equal(url.pathname, '/Zamba.Web/views/WF/TaskViewer.aspx');
  assert.equal(url.searchParams.get('DocType'),'8');
  assert.equal(url.searchParams.get('docid'),'9');
  assert.equal(url.searchParams.get('taskid'),'6');
  assert.equal(url.searchParams.get('s'),'7');
  assert.equal(url.searchParams.get('user'),'42');
  assert.equal(url.searchParams.get('t'),'stored-token');
  assert.equal(url.searchParams.get('gridClicked'),'1');
  assert.equal(requests.length,1);
  assert.equal(requests[0].url, 'https://example.test/api/Tasks/GetUsersWFStepsRights');
  assert.equal(requests[0].options.params.values.stepId, '7');
  assert.equal(requests[0].options.params.values.right, '19');
  assert.equal(requests[0].options.params.values.userid, '42');
});
test('missing task or permission opens DocViewer without task parameters', async () => {
  const {service,requests} = fixture(false);
  const denied = new URL(await service.resultUrl({DOC_ID:1,DOC_TYPE_ID:2,TASK_ID:3,STEP_ID:4}));
  assert.equal(denied.pathname, '/Zamba.Web/views/search/docviewer.aspx');
  assert.equal(denied.searchParams.get('taskid'), null);
  assert.equal(requests.length,1);
  const missing = fixture();
  const url = new URL(await missing.service.resultUrl({DOC_ID:1,DOC_TYPE_ID:2}));
  assert.equal(url.pathname, '/Zamba.Web/views/search/docviewer.aspx');
  assert.equal(missing.requests.length,0); missing.host.GetUID = () => '';
  await assert.rejects(missing.service.search('texto',0),/Usuario/);
});
const {GlobalSearchElementComponent} = load('global-search.component', {'./global-search.service':serviceModule});
function component(service) {
  const component = new GlobalSearchElementComponent(service,{markForCheck(){}});
  component.dialog = {nativeElement:{open:true}};
  return component;
}
test('page-sized total does not hide pagination; subsequent page is appended', async () => {
  const pages = [];
  const c = component({ pageSize:100, search: async (_,page) => {pages.push(page); return {data:Array.from({length:page ? 2:100},()=>({})),total:page ? 2:100};} });
  c.searchValue='texto'; await c.search(); assert.equal(c.hasMore,true);
  await c.search(true); assert.equal(c.rows.length,102); assert.equal(c.hasMore,false); assert.deepEqual(pages,[0,1]);
});
test('editing cancels stale results and clear removes selection', async () => {
  let finish;
  const c = component({ pageSize:100, search: () => new Promise(resolve=>finish=resolve) });
  c.searchValue='anterior'; const pending=c.search(); c.changeText('nuevo');
  finish({data:[{NAME:'stale'}]}); await pending;
  assert.equal(c.rows.length,0); assert.equal(c.busy,false); assert.equal(c.selected,-1);
});
