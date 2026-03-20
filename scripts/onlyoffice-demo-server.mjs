import { createReadStream, createWriteStream, existsSync, mkdirSync, writeFileSync } from 'node:fs';
import { basename, dirname, join, resolve } from 'node:path';
import { fileURLToPath } from 'node:url';
import http from 'node:http';
import https from 'node:https';
import { pipeline } from 'node:stream/promises';

const __filename = fileURLToPath(import.meta.url);
const __dirname = dirname(__filename);
const workspaceRoot = resolve(__dirname, '..');
const storageDir = resolve(workspaceRoot, 'onlyoffice-storage');
const demoFileName = 'demo.docx';
const demoFilePath = join(storageDir, demoFileName);
const callbackLogPath = join(storageDir, 'last-callback.json');
const port = Number(process.env.ONLYOFFICE_DEMO_PORT ?? '3001');
const sampleDocumentUrl = 'https://static.onlyoffice.com/assets/docs/samples/demo.docx';
const docxMimeType = 'application/vnd.openxmlformats-officedocument.wordprocessingml.document';

function ensureStorage() {
  mkdirSync(storageDir, { recursive: true });
}

function sendJson(response, statusCode, payload) {
  response.writeHead(statusCode, {
    'Content-Type': 'application/json; charset=utf-8',
    'Access-Control-Allow-Origin': '*'
  });
  response.end(JSON.stringify(payload, null, 2));
}

function streamToFile(url, destinationPath) {
  return new Promise((resolvePromise, rejectPromise) => {
    const client = url.startsWith('https://') ? https : http;
    client.get(url, response => {
      if (response.statusCode && response.statusCode >= 300 && response.statusCode < 400 && response.headers.location) {
        response.resume();
        streamToFile(response.headers.location, destinationPath).then(resolvePromise).catch(rejectPromise);
        return;
      }

      if (response.statusCode !== 200) {
        rejectPromise(new Error(`Download failed with status ${response.statusCode ?? 'unknown'}.`));
        response.resume();
        return;
      }

      pipeline(response, createWriteStream(destinationPath)).then(resolvePromise).catch(rejectPromise);
    }).on('error', rejectPromise);
  });
}

async function ensureSeedDocument() {
  ensureStorage();
  if (existsSync(demoFilePath)) {
    return;
  }

  console.log(`[onlyoffice-demo] Downloading sample document from ${sampleDocumentUrl}`);
  await streamToFile(sampleDocumentUrl, demoFilePath);
}

function readRequestBody(request) {
  return new Promise((resolvePromise, rejectPromise) => {
    const chunks = [];

    request.on('data', chunk => {
      chunks.push(chunk);
    });

    request.on('end', () => {
      resolvePromise(Buffer.concat(chunks).toString('utf8'));
    });

    request.on('error', rejectPromise);
  });
}

async function handleCallback(request, response) {
  try {
    const rawBody = await readRequestBody(request);
    const payload = rawBody ? JSON.parse(rawBody) : {};

    ensureStorage();
    writeFileSync(callbackLogPath, JSON.stringify(payload, null, 2), 'utf8');

    if ((payload.status === 2 || payload.status === 6) && typeof payload.url === 'string' && payload.url.length > 0) {
      console.log(`[onlyoffice-demo] Persisting updated file from ${payload.url}`);
      await streamToFile(payload.url, demoFilePath);
    }

    sendJson(response, 200, { error: 0 });
  } catch (error) {
    console.error('[onlyoffice-demo] Callback error:', error);
    sendJson(response, 500, {
      error: 1,
      message: error instanceof Error ? error.message : 'Unknown callback error'
    });
  }
}

function handleFile(response, filePath) {
  response.writeHead(200, {
    'Content-Type': docxMimeType,
    'Content-Disposition': `inline; filename="${basename(filePath)}"`,
    'Access-Control-Allow-Origin': '*',
    'Cache-Control': 'no-store'
  });

  createReadStream(filePath).pipe(response);
}

async function start() {
  await ensureSeedDocument();

  const server = http.createServer(async (request, response) => {
    const host = request.headers.host ?? `localhost:${port}`;
    const requestUrl = new URL(request.url ?? '/', `http://${host}`);

    if (request.method === 'OPTIONS') {
      response.writeHead(204, {
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Methods': 'GET,POST,OPTIONS',
        'Access-Control-Allow-Headers': 'Content-Type, Authorization'
      });
      response.end();
      return;
    }

    if (request.method === 'GET' && requestUrl.pathname === '/health') {
      sendJson(response, 200, {
        ok: true,
        documentUrlForDocker: `http://host.docker.internal:${port}/files/${demoFileName}`,
        documentUrlForHost: `http://localhost:${port}/files/${demoFileName}`,
        callbackUrlForDocker: `http://host.docker.internal:${port}/onlyoffice/callback`
      });
      return;
    }

    if (request.method === 'GET' && requestUrl.pathname === `/files/${demoFileName}`) {
      if (!existsSync(demoFilePath)) {
        await ensureSeedDocument();
      }
      handleFile(response, demoFilePath);
      return;
    }

    if (request.method === 'POST' && requestUrl.pathname === '/onlyoffice/callback') {
      await handleCallback(request, response);
      return;
    }

    if (request.method === 'GET' && requestUrl.pathname === '/') {
      sendJson(response, 200, {
        name: 'onlyoffice-demo-server',
        health: `http://localhost:${port}/health`,
        file: `http://localhost:${port}/files/${demoFileName}`,
        callback: `http://localhost:${port}/onlyoffice/callback`,
        dockerFile: `http://host.docker.internal:${port}/files/${demoFileName}`,
        dockerCallback: `http://host.docker.internal:${port}/onlyoffice/callback`
      });
      return;
    }

    sendJson(response, 404, { error: 'Not found' });
  });

  server.listen(port, '0.0.0.0', () => {
    console.log(`[onlyoffice-demo] Server listening on http://localhost:${port}`);
    console.log(`[onlyoffice-demo] File URL for Docker container: http://host.docker.internal:${port}/files/${demoFileName}`);
    console.log(`[onlyoffice-demo] Callback URL for Docker container: http://host.docker.internal:${port}/onlyoffice/callback`);
  });
}

try {
  await start();
} catch (error) {
  console.error('[onlyoffice-demo] Failed to start:', error);
  process.exitCode = 1;
}
