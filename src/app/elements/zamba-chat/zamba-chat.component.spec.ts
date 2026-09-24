import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { CommonModule } from '@angular/common';
import { CopilotPromptResponse } from './zamba-chat.models';
import { FormsModule } from '@angular/forms';
import { ZambaChatComponent } from './zamba-chat.component';

describe('ZambaChatComponent', () => {
    let component: ZambaChatComponent;
    let fixture: ComponentFixture<ZambaChatComponent>;
    let httpMock: HttpTestingController;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [CommonModule, HttpClientTestingModule, FormsModule],
            declarations: [ZambaChatComponent],
        });
        fixture = TestBed.createComponent(ZambaChatComponent);
        component = fixture.componentInstance;
        httpMock = TestBed.inject(HttpTestingController);
        fixture.detectChanges();
    });

    afterEach(() => {
        httpMock.verify();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should not allow sending without a file or documentId', () => {
        component.prompt = 'hola';
        expect(component.canSend()).toBeFalse();
    });

    it('should send fileBase64/fileName on first prompt and store the returned documentId', () => {
        component.prompt = '¿Qué dice el documento?';
        (component as any).pendingFileBase64 = 'QUJD';
        component.pendingFileName = 'test.txt';

        component.sendPrompt();

        const req = httpMock.expectOne('https://localhost:7088/api/Copilot/ask');
        expect(req.request.method).toBe('POST');
        expect(req.request.body.fileBase64).toBe('QUJD');
        expect(req.request.body.fileName).toBe('test.txt');
        expect(req.request.body.documentId).toBeUndefined();

        const response: CopilotPromptResponse = {
            response: 'Esta es la respuesta',
            documentId: 'doc-123',
            chunksUsed: 2,
            stats: {},
        };
        req.flush(response);

        expect(component.documentId).toBe('doc-123');
        expect(component.pendingFileName).toBeNull();
        expect(component.messages[1].text).toBe('Esta es la respuesta');
        expect(component.isSending).toBeFalse();
    });

    it('should reuse the stored documentId on subsequent prompts instead of resending the file', () => {
        component.documentId = 'doc-123';
        component.prompt = 'Otra pregunta';

        component.sendPrompt();

        const req = httpMock.expectOne('https://localhost:7088/api/Copilot/ask');
        expect(req.request.body.documentId).toBe('doc-123');
        expect(req.request.body.fileBase64).toBeUndefined();

        req.flush({ response: 'ok', documentId: 'doc-123', chunksUsed: 1, stats: {} } as CopilotPromptResponse);
    });

    it('should push an error message when the API call fails', () => {
        component.documentId = 'doc-123';
        component.prompt = 'pregunta';

        component.sendPrompt();

        const req = httpMock.expectOne('https://localhost:7088/api/Copilot/ask');
        req.flush('Failed to reach the GitHub Copilot API.', { status: 502, statusText: 'Bad Gateway' });

        expect(component.messages.some(m => m.role === 'error')).toBeTrue();
        expect(component.isSending).toBeFalse();
    });

    it('should auto-extract field values into bound inputs when a file is attached', () => {
        document.body.insertAdjacentHTML('beforeend', '<input id="zamba_index_20" /><input id="zamba_index_21" />');
        component.fieldBindings = [
            { inputId: 'zamba_index_20', prompt: 'Extract the title.' },
            { inputId: 'zamba_index_21', prompt: 'Extract the date.' },
        ];

        (component as any).pendingFileBase64 = 'QUJD';
        component.pendingFileName = 'test.txt';
        (component as any).runFieldExtractions();

        expect(component.isExtracting).toBeTrue();

        const firstReq = httpMock.expectOne('https://localhost:7088/api/Copilot/ask');
        expect(firstReq.request.body.fileBase64).toBe('QUJD');
        expect(firstReq.request.body.prompt).toBe('Extract the title.');
        firstReq.flush({ response: 'My Title', documentId: 'doc-123', chunksUsed: 1, stats: {} } as CopilotPromptResponse);

        expect((document.getElementById('zamba_index_20') as HTMLInputElement).value).toBe('My Title');
        expect(component.documentId).toBe('doc-123');

        const secondReq = httpMock.expectOne('https://localhost:7088/api/Copilot/ask');
        expect(secondReq.request.body.documentId).toBe('doc-123');
        expect(secondReq.request.body.fileBase64).toBeUndefined();
        expect(secondReq.request.body.prompt).toBe('Extract the date.');
        secondReq.flush({ response: '2026-01-01', documentId: 'doc-123', chunksUsed: 1, stats: {} } as CopilotPromptResponse);

        expect((document.getElementById('zamba_index_21') as HTMLInputElement).value).toBe('2026-01-01');
        expect(component.isExtracting).toBeFalse();

        document.getElementById('zamba_index_20')?.remove();
        document.getElementById('zamba_index_21')?.remove();
    });
});
