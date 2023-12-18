Public Enum ObjectTypes As Integer
    None = 0
    Index = 1
    DocTypes = 2
    Archivos = 3
    Volumes = 4
    Users = 5
    Documents = 6
    FrmImportConfig = 23
    FrmDocProperty = 30
    FrmDocHistory = 31
    FrmEstation = 32
    FrmUserProperty = 33
    ModuleImport = 35
    ModuleElectronicDoc = 36
    ModuleMail = 37
    WFSteps = 42
    WFRules = 43
    ModuleForm = 46
    ModuleInsert = 48
    ModuleInternalMesages = 49
    ModuleMonitor = 50
    FormulariosElectronicos = 52
    ModuleScan = 53
    ModuleSearch = 54
    ModuleWorkFlow = 55
    Exportacion = 56
    Notas = 57
    ModuleBarCode = 59
    ModuleAutoMail = 60
    ModuleReports = 61
    ModuleDataAccess = 62
    DataBase = 64
    ServerImages = 65
    Consultas = 67
    HistorialDeUsuario = 68
    PreferenciasDeUsuario = 69
    Preproceso = 70
    WFTask = 71
    ModuleDynamicViews = 72
    ExportarLista = 73
    ModuleVersions = 74

    ModuleExports = 76

    ModuleElectronicForms = 78

    ModuleSeeLastVersions = 81

    Foro = 83
    Restriccion = 84
    Security = 85
    Picturespath = 86
    Licencias = 87
    Version = 88
    LogIn = 89
    RemoveFilters = 92
    '[Ezequiel] 18/03/09
    SearchDictionary = 90
    SearchValues = 91
    '[Sebastian 11-08-2009]
    Grids = 95
    Services = 97
    '[Cristian 09-09-2011]
    InsertWeb = 98
    '[Cristian 12-09-2011]
    ForoWeb = 99
    '[Cristian 15-09-2011]
    'Services = 100
    AsocWeb = 100
    MailsHistoryWeb = 101
    ErrorLog = 102
    UserGroups = 103
    WFStates = 104
    WebModule = 105
    '[Jerem�as 15-10-2012 Motivo: ZTC- Guardar en historial ABM sobre TestCase y Cat]
    TestCase = 106
    TestCaseCategory = 107
    Feeds = 108
    '[Marcelo 11-07-2013 Motivo: ZTC - Agrupar casos de prueba de windows propios de Zamba]
    WindowsModule = 109
    'Se utiliza como clave primaria en la tabla ZRuleOptBase, la cual se completa mediante el trigger TRG_ZRuleOpt_InsteadInsert
    RulePreference = 110
    ModuleReportBuilder = 111
    Administrador = 112
    Cliente = 113
    ModuleDiagrams = 114
    ModuleQuery = 115
    ServiceInstaller = 116
    RemoteInsert = 117
    DBtoXML = 118
    VolumeCheck = 119
    ModuleTestCase = 120
    Link = 121
    Configuracion = 122
    Start = 123

    ExportaAdministrator = 124
    Templates = 125
    Helper = 126
    GeneralOptions = 127
    MonitorServices = 128
    MonitorUserActivity = 129
    MonitorAdminActivity = 130
    MonitorErrorsActivity = 131
    MonitorPerformanceIssues = 132
    SearchWeb = 133

    Observaciones = 134
    Views = 135
    WFStepsTree = 136

    HomeWeb = 137
    ShowAnchorSidebar = 138
    UsebtnFlotante = 139
    Cache = 140

End Enum