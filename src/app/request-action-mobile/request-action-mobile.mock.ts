import { RequestActionTask } from './request-action-mobile.model';

export const MOCK_USER = { id: 7042, username: 'sofia.martinez', token: 'mock-mobile-session' };
const MOCK_INVOICE_THUMBNAIL = 'data:image/svg+xml,' + encodeURIComponent('<svg xmlns="http://www.w3.org/2000/svg" width="240" height="320" viewBox="0 0 240 320"><rect width="240" height="320" fill="#fff"/><rect width="240" height="42" fill="#26b4bd"/><text x="18" y="27" fill="#fff" font-family="Arial,sans-serif" font-size="15" font-weight="700">ZAMBA · COMPROBANTE</text><text x="18" y="79" fill="#18302e" font-family="Arial,sans-serif" font-size="12" font-weight="700">FACTURA DE SERVICIOS</text><text x="18" y="102" fill="#71817c" font-family="Arial,sans-serif" font-size="10">Servicios del Sur S.A.</text><rect x="18" y="125" width="204" height="1" fill="#dce4dd"/><rect x="18" y="146" width="128" height="7" rx="3" fill="#dce4dd"/><rect x="18" y="164" width="185" height="7" rx="3" fill="#edf1ed"/><rect x="18" y="182" width="160" height="7" rx="3" fill="#edf1ed"/><rect x="18" y="200" width="192" height="7" rx="3" fill="#edf1ed"/><rect x="18" y="245" width="204" height="1" fill="#dce4dd"/><text x="18" y="272" fill="#71817c" font-family="Arial,sans-serif" font-size="10">TOTAL</text><text x="18" y="296" fill="#18302e" font-family="Arial,sans-serif" font-size="18" font-weight="700">ARS 1.284.500</text></svg>');

export const MOCK_TASKS: Record<number, RequestActionTask[]> = {
  11535116: [
    { ID: 68124, ID1: 982401, EID: 104, TID: 771902, PROVEEDOR: 'Servicios del Sur S.A.', MONEDA: 'ARS', IMPORTE: '1.284.500,00', TIPO_GASTO: 'Servicios profesionales', CONCEPTO: 'Asesoría legal · septiembre', AREA: 'Legales', FECHA: '16/10/2026', SOLICITANTE: 'Lucía Fernández', THUMBNAIL_URL: MOCK_INVOICE_THUMBNAIL },
    { ID: 68118, ID1: 982377, EID: 104, TID: 771884, PROVEEDOR: 'Norte Insumos Médicos', MONEDA: 'ARS', IMPORTE: '348.920,50', TIPO_GASTO: 'Insumos', CONCEPTO: 'Material descartable', AREA: 'Operaciones', FECHA: '15/10/2026', SOLICITANTE: 'Diego Luna' },
    { ID: 68097, ID1: 982291, EID: 104, TID: 771802, PROVEEDOR: 'Andina Tecnología', MONEDA: 'USD', IMPORTE: '2.140,00', TIPO_GASTO: 'Tecnología', CONCEPTO: 'Renovación de licencias', AREA: 'Sistemas', FECHA: '14/10/2026', SOLICITANTE: 'Mariana Ríos' },
  ],
  11535117: [
    { ID: 67231, ID1: 981104, EID: 108, TID: 770532, PROVEEDOR: 'Logística Federal', MONEDA: 'ARS', IMPORTE: '516.780,00', TIPO_GASTO: 'Traslados', CONCEPTO: 'Distribución regional', AREA: 'Logística', FECHA: '16/10/2026', SOLICITANTE: 'Pablo Acosta' },
    { ID: 67206, ID1: 981031, EID: 108, TID: 770419, PROVEEDOR: 'Estudio Litoral', MONEDA: 'ARS', IMPORTE: '226.000,00', TIPO_GASTO: 'Servicios profesionales', CONCEPTO: 'Revisión de contratos', AREA: 'Legales', FECHA: '13/10/2026', SOLICITANTE: 'Lucía Fernández' },
  ],
  11535126: [
    { DOC_ID: 56012, ID: 56012, ID1: 56012, EID: 244, TID: 755120, TITLE: 'Siniestro · Ruta 9 km 42', AREA: 'Siniestros', FECHA: '15/10/2026', SOLICITANTE: 'Dr. Esteban Silva', RESPONSABILIDAD: 'Responsabilidad compartida', MECANICA_DEL_ACCIDENTE: 'Colisión lateral en intersección. La unidad asegurada circulaba por la avenida principal.', VICTIMAS: 'Una persona con lesiones leves, atendida en el lugar.', RECLAMANTE: 'María Belén Torres', NEGOCIACION: 'Propuesta inicial recibida por $ 3.250.000', OBSERVACIONES: 'La documentación médica se encuentra completa.' },
    { DOC_ID: 55984, ID: 55984, ID1: 55984, EID: 244, TID: 755042, TITLE: 'Siniestro · Av. Belgrano 1840', AREA: 'Siniestros', FECHA: '14/10/2026', SOLICITANTE: 'Dra. Carolina Vega', RESPONSABILIDAD: 'A determinar', MECANICA_DEL_ACCIDENTE: 'Impacto en cadena durante detención por congestión.', VICTIMAS: 'Sin lesiones informadas.', RECLAMANTE: 'Transportes Roca S.R.L.', NEGOCIACION: 'Pendiente de primera propuesta', OBSERVACIONES: 'Solicitar presupuesto actualizado.' },
  ],
};
