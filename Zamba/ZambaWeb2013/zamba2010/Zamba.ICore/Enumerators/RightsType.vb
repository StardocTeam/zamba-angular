Public Enum RightsType As Integer ' Tipos de Permisos a asignar
    None = 0
    View = 1
    Edit = 2
    Create = 3
    Delete = 4
    Admin = 5
    Buscar = 7
    Print = 9
    Saveas = 10
    EnviarPorMail = 11
    ReIndex = 12
    Share = 15
    ViewShared = 16
    Execute = 18
    Use = 19
    EnviarPorMensaje = 20
    ChangeName = 22
    Move = 23
    VerDocumentosAsociados = 24
    Copy = 25
    Configurar = 26
    InicioFallidoDeSesion = 27
    Asign = 28
    Delegates = 29
    Open = 30
    Reject = 31
    VerAsignadosAOtros = 32
    VerAsignadosANadie = 33
    CopyDoc = 34
    Iniciar = 35
    ModificarVencimiento = 36
    UnAssign = 37
    Terminar = 38
    Monitorear = 39
    AgregarDocumento = 40
    Exclusividad = 41
    insert = 42
    ViewVersions = 43
    AddNewVersions = 44
    PublishVersions = 45
    EditVersions = 46
    AddFromVersions = 47
    DeleteVersions = 48
    'false: Envia Mail - True : Envia Msj Interno
    NotifyOptions = 49
    AutomaticVersion = 50
    IndexSearch = 51
    IndexRequired = 52
    IndexView = 53
    IndexEdit = 54
    IndexExport = 55
    ViewRightsByIndex = 56
    Replicate = 57
    ExecuteRule = 58
    InitializeTask = 59
    FinishTask = 60
    RejectTask = 61
    CloseTask = 62
    DerivateTask = 63
    NuevoTemaGuardar = 64
    NuevoTemaGuardarNotificar = 65
    ResponderGuardar = 66
    ResponderGuardarNotificar = 67
    agregarPlantilla = 68
    actualizarPlantilla = 69
    eliminarPlantilla = 70
    allowExecuteTasksAssignedToOtherUsers = 71
    '[SEBASTIAN]
    IndexDefaultSearch = 72
    '[Gaston]
    AllowExecuteAssociatedDocuments = 73
    AllowStateComboBox = 74
    ''[sebastian 26/01/2009]
    'AutoComplete = 75
    '[Ezequiel 04/02/2009]
    DeleteMsgForum = 76
    '[Ezequiel 12/03/09]
    OwnerChanges = 77
    '[sebastian 13-03-2009]
    LogIn = 78
    '[sebastian 03-04-09]
    RemoveDefaultFilters = 79
    '[Ezequiel 04]
    TaskGridIndexView = 80
    ChangeTaskState = 81
    '[Sebastian 30-11-2009]
    ExportToOutlook = 82
    '[AlejandroR 14-12-2009]
    InicioForzadoDeSesion = 83
    '[Pablo 14-07-2010] - Permisos de Grillas
    ResultGridShowResultNameColumn = 87
    ResultGridShowIconNameColumn = 88
    ResultGridShowEntityNameColumn = 89
    ResultGridShowCreatedDateColumn = 90
    ResultGridShowLastEditDateColumn = 91
    ResultGridShowVersionNumberColumn = 92
    ResultGridShowVersionColumn = 93

    AsociatedResultGridShowResultNameColumn = 94
    AsociatedResultGridShowIconNameColumn = 95
    AsociatedResultGridShowEntityNameColumn = 96
    AsociatedResultGridShowCreatedDateColumn = 97
    AsociatedResultGridShowLastEditDateColumn = 98
    AsociatedResultGridShowVersionNumberColumn = 99
    AsociatedResultGridShowVersionColumn = 100

    TaskResultGridShowResultNameColumn = 101
    TaskResultGridShowIconNameColumn = 102
    TaskResultGridShowEntityNameColumn = 103
    TaskResultGridShowTaskStateColumn = 104
    TaskResultGridShowAssignedColumn = 105
    TaskResultGridShowSituationColumn = 106
    TaskResultGridShowVersionColumn = 107

    ResultGridShowDocumentType = 129
    AsociatedResultGridShowDocumentType = 130
    TaskResultGridShowCheckTaskColumn = 131
    '----------------------------------------------------------
    '[Pablo 29-09-2010] - Permisos de Visualizacion de Asociados
    AssociateIndexView = 132
    ViewAssociateRightsByIndex = 133
    '----------------------------------------------------------
    ' -  FORO  -
    '----------------------------------------------------------
    '[Pablo 29-09-2010] - Permisos de Foro
    MostrarConversaciones = 134
    '----------------------------------------------------------
    'Marcelo 21-03-11 - Permiso de deshabilitar filtros por defecto
    DisableDefaultFilters = 135
    ShowAsociatedTab = 136
    'Do Not use value 137
    'will cause administrative conflicts
    'between Zamba Windows and Zamba web systems

    WebResultGridShowEntityNameColumn = 163
    WebResultGridShowCreatedDateColumn = 138
    WebResultGridShowLastEditDateColumn = 139
    WebResultGridShowVersionNumberColumn = 140
    WebResultGridShowVersionColumn = 141
    WebResultGridShowOriginalName = 142

    AssociatedWebResultGridShowResultNameColumn = 143
    AssociatedWebResultGridShowIconNameColumn = 144
    AssociatedWebResultGridShowDocumentTypeColumn = 145
    AssociatedWebResultGridShowCreatedDateColumn = 146
    AssociatedWebResultGridShowLastEditDateColumn = 147
    AssociatedWebResultGridShowVersionNumberColumn = 148
    AssociatedWebResultGridShowVersionColumn = 149
    AssociatedWebResultGridShowOriginalName = 150

    TaskWebResultGridShowResultNameColumn = 151
    TaskWebResultGridShowIconNameColumn = 152
    TaskWebResultGridShowTaskStateColumn = 153
    TaskWebResultGridShowCreatedDateColumn = 154
    TaskWebResultGridShowAssignedToColumn = 155
    TaskWebResultGridShowSituationColumn = 156
    TaskWebResultGridVerColumn = 157
    TaskWebResultGridShowOriginalName = 158
    WebResultGridShowResultNameColumn = 159
    WebResultGridShowIconNameColumn = 160

    HideStepFromWfTree = 161
    EnviarPorMailAsoc = 162
    'el 163 esta arriba, lo ocupa WebResultGridShowEntityNameColumn, continuar con el 164
    EnviarPorMailWeb = 164
    VisualizarConsulta = 165
    ChangeParticipants = 166

    ResultGridShowImportanceColumn = 167
    AsociatedResultGridShowImportanceColumn = 168
    TaskResultGridShowImportanceColumn = 169
    ResultGridShowFavoriteColumn = 170
    AsociatedResultGridShowFavoriteColumn = 171
    TaskResultGridShowFavoriteColumn = 172
    FlagAsImportant = 173
    FlagAsFavorite = 174
    'Remove SendMail Button from
    'the task toolbar if checked
    RemoveSendMailInTasks = 175
    FlagAsRead = 176

    'Ocultar WF en modulo web
    WebHideWf = 178

    'Expandir estado en el arbol de WF
    ShowStates = 179

    Decrypt = 180

    'Este no llega a ser un permiso, ya que el correcto seria el de edición, pero solo necesitaba ocultar este boton por regla mediante la DoSetRights
    HideReplaceDocument = 181
    EditAttributesInForms = 182

    AsociatedResultGridShowOriginalNameColumn = 184
    AsociatedResultGridShowParentIdColumn = 185
    AsociatedResultGridShowDocIdColumn = 186
    UseOCR = 187
    RecognizeBarCode = 188
    GlobalSearch = 190
    ChangePassword = 191
    ShowWFTreeView = 192

    ShowHome = 193
    ShowMyTask = 194
    ShowAnchorSidebar = 195
    UsebtnFlotante = 196
End Enum
