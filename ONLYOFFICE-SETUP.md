# OnlyOffice local setup

This project now includes a minimal local setup to test OnlyOffice with a real Document Server.

## What was added

- Docker compose file: [docker-compose.onlyoffice.yml](docker-compose.onlyoffice.yml)
- Demo callback/file server: [scripts/onlyoffice-demo-server.mjs](scripts/onlyoffice-demo-server.mjs)
- Local storage folder: [onlyoffice-storage](onlyoffice-storage)

## What each part does

### Document Server

The Docker service runs the official `onlyoffice/documentserver` image on:

- http://localhost:8081

### Demo file/callback server

The local Node server:

- serves a DOCX file to OnlyOffice
- receives the `callbackUrl` requests from OnlyOffice
- downloads the updated DOCX sent by OnlyOffice and stores it locally

It runs on:

- http://localhost:3001

Useful endpoints:

- Health: http://localhost:3001/health
- DOCX file: http://localhost:3001/files/demo.docx
- Callback: http://localhost:3001/onlyoffice/callback

Inside Docker, OnlyOffice must access the host using:

- File URL: http://host.docker.internal:3001/files/demo.docx
- Callback URL: http://host.docker.internal:3001/onlyoffice/callback

## Start OnlyOffice

From the workspace root:

- `docker compose -f docker-compose.onlyoffice.yml up -d`

## Start the demo callback/file server

From the workspace root:

- `node scripts/onlyoffice-demo-server.mjs`

## How to use in the Angular screen

Open the OnlyOffice test route and use these values:

- Document Server URL: `http://localhost:8081/`
- Document URL: `http://host.docker.internal:3001/files/demo.docx`
- Callback URL: `http://host.docker.internal:3001/onlyoffice/callback`
- Title: `demo.docx`

Then click the load button.

## Notes

- This setup is for local testing only.
- JWT is disabled for simplicity.
- If Docker on Windows cannot resolve `host.docker.internal`, Docker Desktop must be running with host gateway support.
- The updated file is stored at [onlyoffice-storage/demo.docx](onlyoffice-storage/demo.docx) after successful saves.
- The latest callback payload is stored at [onlyoffice-storage/last-callback.json](onlyoffice-storage/last-callback.json).
