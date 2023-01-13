Public Enum ResultActions As Int16

    InsertarDocumento = 0
    Caratula = 1
    EnvioDeMail = 2
    MostrarResult = 3
    InsertarTemplate = 4
    InsertarDocWithFormat = 5
    Foro_NewMessage = 6
    RefreshIndexs = 7
    'MostrarIndices = 8     OBSOLETO
    ShowAsForm = 9
    RefreshUserAsigned = 10
    UpdateUserAsigned = 11
    CloseTask = 12
    ShowNewForm = 13
    DoAddAsoc = 14
    OpenBrowser = 15
    CloseBrowser = 16
    ChangeVisualization = 17
    'ShowWaitForm = 18  OBSOLETO
    'CloseWaitForm = 19 OBSOLETO
    InsertarCarpetaLoadIndexerDelegado = 20 'analizar si insertar documento no hace algo diferente o si no hace nada en este contexto y usar ese
    ExecuteRule = 21
    CloseNewFormTab = 22
    CloseDocument = 23
    SetReadOnly = 24
    SetReindex = 25
    HideReplaceDocument = 26
    ShowHelp = 27
End Enum
