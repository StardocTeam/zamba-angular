Imports System.Diagnostics.CodeAnalysis
Imports Zamba.AppBlock
Imports System.Collections.Generic

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Core
''' Class	 : Core.ZClass
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase base de Zamba. Tiene metodos para manipular errores
''' y las enumeraciones con los objetos mas comunes de Zamba
''' </summary>
''' <remarks>
''' Todas las clases utilizadas en Zamba deberian heredar de esta
''' </remarks>
''' <history>
''' 	[Hernan]	26/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
<Serializable()> Public MustInherit Class ZClass
    'Inherits Object
    Implements IZClass

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Deja un log del error especificado
    ''' </summary>
    ''' <param name="ex"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub raiseerror(ByVal ex As Exception)
        ZException.Log(ex)
        ZTrace.WriteLineIf(ZTrace.IsError, ex.ToString())
    End Sub

    Public MustOverride Sub Dispose() Implements IZClass.Dispose

    '    '''' -----------------------------------------------------------------------------
    '    '''' <summary>
    '    '''' Deja un log del error especificado
    '    '''' </summary>
    '    '''' <param name="ex"></param>
    '    '''' <remarks>
    '    '''' </remarks>
    '    '''' <history>
    '    '''' 	[Hernan]	26/05/2006	Created
    '    '''' </history>
    '    '''' -----------------------------------------------------------------------------
    '    ''Public Shared Sub zamba.core.zclass.raiseerrors(ByVal ex As Exception)
    '    ''    RaiseEvent LogError(ex)
    '    ''End Sub
    '    <SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")> Public Shared Event ShowWait(ByVal Estado As Boolean, ByVal Cancel As Boolean)
    '    <SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")> Public Shared Event MessageEvent(ByVal msg As String, ByVal titulo As String)
    '    <SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")> Public Shared Event MessageError(ByVal ex As Exception)
    '    Public Shared Sub MostrarWait(ByVal Estado As Boolean)
    '        RaiseEvent ShowWait(Estado, False)
    '    End Sub
    '    Public Shared Sub MostrarWait(ByVal Estado As Boolean, ByVal Cancel As Boolean)
    '        RaiseEvent ShowWait(Estado, Cancel)
    '    End Sub
    '    Public Shared Sub MostrarWaitForm(ByVal Estado As Boolean)
    '        RaiseEvent ShowWait(Estado, False)
    '    End Sub
    '    Public Shared Sub MostrarWaitForm(ByVal Estado As Boolean, ByVal Cancel As Boolean)
    '        RaiseEvent ShowWait(Estado, Cancel)
    '    End Sub
    '    ''' -----------------------------------------------------------------------------
    '    ''' <summary>
    '    ''' Para mostrar mensajes al usuario.
    '    ''' </summary>
    '    ''' <param name="msg">Mensaje que se desea mostrar</param>
    '    ''' <param name="titulo">Titulo del mensaje</param>
    '    ''' <remarks>
    '    ''' Es importante que la interfaz grafica capture el evento y muestre el mensaje.
    '    ''' La idea es no mostrar MessageBoxes desde clases
    '    ''' </remarks>
    '    ''' <history>
    '    ''' 	[Hernan]	19/07/2006	Created
    '    ''' </history>
    '    ''' -----------------------------------------------------------------------------
    '    Public Overloads Shared Sub Message(ByVal msg As String, ByVal titulo As String)
    '        RaiseEvent MessageEvent(msg, titulo)
    '    End Sub
    '    Public Overloads Shared Sub Message(ByVal Ex As Exception)
    '        RaiseEvent MessageError(Ex)
    '    End Sub

    '#Region "NotifyIcon"
    '    <SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")> Public Shared Event ShowInfo(ByVal Msg As String, ByVal Title As String)
    '    Protected Shared Sub RaiseInfo(ByVal Msg As String)
    '        RaiseEvent ShowInfo(Msg, "Zamba Info")
    '    End Sub
    '    Protected Shared Sub RaiseInfo(ByVal Msg As String, ByVal Title As String)
    '        RaiseEvent ShowInfo(Msg, Title)
    '    End Sub
    '    Public Shared Sub RaiseInfos(ByVal Msg As String)
    '        RaiseEvent ShowInfo(Msg, "Zamba Info")
    '    End Sub
    '    Public Shared Sub RaiseInfos(ByVal Msg As String, ByVal Title As String)
    '        RaiseEvent ShowInfo(Msg, Title)
    '    End Sub
    '    <SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")> Public Shared Event ShowError(ByVal Msg As String)
    '    Public Shared Sub RaiseNotifyError(ByVal Msg As String)
    '        RaiseEvent ShowError(Msg)
    '    End Sub
    '    Public Shared Sub RaiseNotifyErrors(ByVal Msg As String)
    '        RaiseEvent ShowError(Msg)
    '    End Sub
    '    <SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")> Public Shared Event ShowWarning(ByVal Msg As String)
    '    Public Shared Sub RaiseWarning(ByVal Msg As String)
    '        RaiseEvent ShowWarning(Msg)
    '    End Sub
    '    Public Shared Sub RaiseWarnings(ByVal Msg As String)
    '        RaiseEvent ShowWarning(Msg)
    '    End Sub

    '#End Region

    '    'Este evento se usa para que una regla le pase a quien corresponda un result (y posiblemente mas parametros) mas 
    '    'una String describiendo el modulo contiene que modulo se debe cargar (Ej :"Caratulas" , "Importacion")

    '    'Este evento se usa para que una regla le pase a quien corresponda un result (y posiblemente mas parametros) mas 
    '    'una String describiendo el modulo contiene que modulo se debe cargar (Ej :"Caratulas" , "Importacion")

    '    Public Shared Event eHandleModule(ByVal resultActionType As ResultActions, ByRef currentResult As ZambaCore, ByVal Params As Hashtable)
    '    Public Shared Event eHandleModuleWithDoctype(ByVal resultActionType As ResultActions, ByRef currentResult As ZambaCore, ByVal docType As ZambaCore, ByVal Params As Hashtable)
    '    Public Shared Event eHandleModuleEmpty(ByVal _subject As String, ByVal _body As String, ByRef _flagSaved As Boolean)
    '    Public Shared Event eHandleModuleAction(ByVal Action As ResultActions, ByRef currentResult As Result)
    '    Public Shared Event eHandleGenericAction(ByVal action As GenericActions)
    '    Public Shared Event eHandleModuleRuleAction(ByVal Action As ResultActions, ByRef currentResult As List(Of ITaskResult), ByVal Params As Hashtable)
    '    Public Shared Event eHandleEventDialogReseult(ByVal Dialog_Result AsDialogResult)
    '    'Public Shared Sub HandleModule(ByVal resultActionType As ResultActions, ByVal currentResult As ZambaCore, ByVal docType As ZambaCore)
    '    '    RaiseEvent eHandleModuleWithDoctype(resultActionType, currentResult, docType)
    '    'End Sub
    '    Public Shared Sub HandleModule(ByVal resultActionType As ResultActions, ByVal currentResult As ZambaCore, ByVal docType As ZambaCore, ByVal Params As Hashtable)
    '        RaiseEvent eHandleModuleWithDoctype(resultActionType, currentResult, docType, Params)
    '    End Sub

    '    'Public Shared Sub HandleModule(ByVal moduleName As ResultActions, ByVal currentResult As ZambaCore)
    '    '    RaiseEvent eHandleModule(moduleName, currentResult)
    '    'End Sub
    '    'Le agregue Params para poder enviar variados parametros y que cada accion sepa como tratarlos

    '    ''' <summary>
    '    ''' captura el dialog result del boton cancelar de index viewer
    '    ''' </summary>
    '    ''' <history>
    '    ''' [Sebastian] 23/12/2009  CREATED Atrapa el evento del clic cancela de index viewer
    '    ''' </history>
    '    ''' <param name="Dialog_Result"></param>
    '    ''' <remarks></remarks>
    '    Public Shared Sub HandleEventDialogResult(ByVal Dialog_Result AsDialogResult)
    '        RaiseEvent eHandleEventDialogReseult(Dialog_Result)
    '    End Sub
    '    Public Shared Sub HandleModule(ByVal moduleName As ResultActions, ByVal currentResult As ZambaCore, ByVal Params As Hashtable)
    '        RaiseEvent eHandleModule(moduleName, currentResult, Params)
    '    End Sub

    '    Public Shared Sub HandleModule(ByVal _subject As String, ByVal _body As String, ByRef _flagSaved As Boolean)
    '        RaiseEvent eHandleModuleEmpty(_subject, _body, _flagSaved)
    '    End Sub
    '    Public Shared Sub HandleModule(ByVal resultActionType As ResultActions, ByRef _result As Result)
    '        RaiseEvent eHandleModuleAction(resultActionType, _result)
    '    End Sub

    '    Public Shared Sub HandleRuleModule(ByVal resultActionType As ResultActions, ByRef _results As List(Of ITaskResult), ByVal Params As Hashtable)
    '        RaiseEvent eHandleModuleRuleAction(resultActionType, _results, Params)

    '    End Sub

    '    Public Shared Sub HandleGenericAction(ByVal genericActions As GenericActions)
    '        RaiseEvent eHandleGenericAction(genericActions)
    '    End Sub
End Class