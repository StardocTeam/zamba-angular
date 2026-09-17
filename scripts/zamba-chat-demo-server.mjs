import { createReadStream, existsSync, statSync } from 'node:fs';
import { extname, join, normalize, resolve } from 'node:path';
import { fileURLToPath } from 'node:url';
import http from 'node:http';

const __filename = fileURLToPath(import.meta.url);
const __dirname = resolve(fileURLToPath(new URL('.', import.meta.url)));
const workspaceRoot = resolve(__dirname, '..');
const port = Number(process.env.ZAMBA_CHAT_DEMO_PORT ?? '4200');

const mimeTypes = {
  '.html': 'text/html; charset=utf-8',
  '.js': 'text/javascript; charset=utf-8',
  '.css': 'text/css; charset=utf-8',
  '.map': 'application/json; charset=utf-8',
  '.ico': 'image/x-icon',
  '.ttf': 'font/ttf',
};

const server = http.createServer((req, res) => {
  const url = new URL(req.url ?? '/', `http://localhost:${port}`);
  let requestedPath = decodeURIComponent(url.pathname);

  if (requestedPath === '/') {
    requestedPath = '/src/zamba-chat-demo.html';
  }

  const filePath = normalize(join(workspaceRoot, requestedPath));

  if (!filePath.startsWith(workspaceRoot) || !existsSync(filePath) || !statSync(filePath).isFile()) {
    res.writeHead(404, { 'Content-Type': 'text/plain; charset=utf-8' });
    res.end('Not found');
    return;
  }

  const contentType = mimeTypes[extname(filePath)] ?? 'application/octet-stream';
  res.writeHead(200, { 'Content-Type': contentType });
  createReadStream(filePath).pipe(res);
});

server.listen(port, () => {
  console.log(`[zamba-chat-demo] Serving ${workspaceRoot} at http://localhost:${port}/ (open http://localhost:${port}/src/zamba-chat-demo.html)`);
});
