Public Interface IUserGroup
	Inherits IUserRights

    Property Description() As String
    Property Users() As ArrayList
    Property CreateDate() As String
End Interface

'ya estan en Icore
'Enum UserState As Integer
'    Activo
'    Bloqueado
'End Enum
'Enum Usertypes As Integer
'    User
'    Group
'End Enum
'Enum RightsType As Integer ' Tipos de Permisos a asignar
'    None = 0
'    View = 1
'    Edit = 2
'    Create = 3
'    Delete = 4
'    Admin = 5
'    Buscar = 7
'    Print = 9
'    Saveas = 10
'    EnviarPorMail = 11
'    ReIndex = 12
'    Share = 15
'    ViewShared = 16
'    Execute = 18
'    Use = 19
'    EnviarPorMensaje = 20
'    ChangeName = 22
'    Move = 23
'    VerDocumentosAsociados = 24
'    Copy = 25
'    Configurar = 26
'    InicioFallidoDeSesion = 27
'    Asign = 28
'    Delegates = 29
'    Open = 30
'    Reject = 31
'    VerAsignadosAOtros = 32
'    VerAsignadosANadie = 33
'    CopyDoc = 34
'    Iniciar = 35
'    ModificarVencimiento = 36
'    UnAssign = 37
'    Terminar = 38
'    Monitorear = 39
'    AgregarDocumento = 40
'    Exclusividad = 41
'    insert = 42
'End Enum
'Enum ObjectTypes As Integer
'    None = 0
'    Index = 1
'    DocTypes = 2
'    Archivos = 3
'    Volumes = 4
'    Users = 5
'    Documents = 6
'    FrmImportConfig = 23
'    FrmDocProperty = 30
'    FrmDocHistory = 31
'    FrmEstation = 32
'    FrmUserProperty = 33
'    ModuleImport = 35
'    ModuleElectronicDoc = 36
'    ModuleMail = 37
'    WFSteps = 42
'    WFRules = 43
'    ModuleBatches = 44
'    ModuleDocumentalSearch = 45
'    ModuleForm = 46
'    ModuleHtmlExport = 47
'    ModuleInsert = 48
'    ModuleInternalMesages = 49
'    ModuleMonitor = 50
'    ModulePlantillas = 51
'    FormulariosElectronicos = 52
'    ModuleScan = 53
'    ModuleSearch = 54
'    ModuleWorkFlow = 55
'    Exportacion = 56
'    Notas = 57
'    ModuleSignature = 58
'    ModuleBarCode = 59
'    ModuleAutoMail = 60
'    ModuleReports = 61
'    ModuleDataAccess = 62
'    ModuleAutoComplete = 63
'    DataBase = 64
'    ServerImages = 65
'    ModuleImportsLocal = 66
'    Consultas = 67
'    HistorialDeUsuario = 68
'    PreferenciasDeUsuario = 69
'    Preproceso = 70
'    WFTask = 71
'    ModuleDynamicViews = 72
'    ExportarLista = 73
'    ModuleVersions = 74
'    ModuleReportBuilder = 75
'End Enum