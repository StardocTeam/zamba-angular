Imports Zamba.Core

Public Class ZControl
    Inherits System.Windows.Forms.UserControl
    Implements IZControl
    Implements System.IDisposable

#Region " Código generado por el Diseñador de Windows Forms "
    Private _helpId As String
    Public Property HelpId() As String
        Get
            Return _helpId
        End Get
        Set(ByVal value As String)

            _helpId = value
        End Set
    End Property
    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    Public Event Finish()
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        RaiseEvent Finish()
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        Try
            MyBase.Dispose(False)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        '
        'ZControl
        '
        BackColor = System.Drawing.Color.White
        Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Name = "ZControl"

    End Sub

#End Region

#Region "NotifyIcon"
    Public Shared Event ShowInfo(ByVal Msg As String, ByVal Title As String, ByVal Tmsg As Enums.TMsg, ByVal Interfaz As Enums.Tinterfaz)
    Protected Shared Sub RaiseInfo(ByVal Msg As String, Optional ByVal Title As String = "Info", Optional ByVal Tipo As Enums.TMsg = Enums.TMsg.NO, Optional ByVal Interfaz As Enums.Tinterfaz = Enums.Tinterfaz.Both)
        RaiseEvent ShowInfo(Msg, Title, Tipo, Interfaz)
    End Sub
    Public Shared Sub RaiseInfos(ByVal Msg As String, Optional ByVal Title As String = "Info", Optional ByVal Tipo As Enums.TMsg = Enums.TMsg.NO, Optional ByVal Interfaz As Enums.Tinterfaz = Enums.Tinterfaz.Both)
        RaiseEvent ShowInfo(Msg, Title, Tipo, Interfaz)
    End Sub
    Public Shared Event ShowError(ByVal Msg As String)
    Protected Shared Sub RaiseNotifyError(ByVal Msg As String)
        RaiseEvent ShowError(Msg)
    End Sub
    Public Shared Event ShowWarning(ByVal Msg As String)
    Protected Shared Sub RaiseWarning(ByVal Msg As String)
        RaiseEvent ShowWarning(Msg)
    End Sub
#End Region

#Region "Metodos"

#Region "Globales"

#Region "Protegidos"

    Protected Shared Sub MostrarWait(ByVal Estado As Boolean, Optional ByVal Cancel As Boolean = False)
        RaiseEvent ShowWait(Estado, Cancel)
    End Sub

#End Region

#Region "Publicos"

    'Este evento se usa para que una regla le pase a quien corresponda un result (y posiblemente mas parametros) mas 
    'una String describiendo el modulo contiene que modulo se debe cargar (Ej :"Caratulas" , "Importacion")
    Public Shared Sub HandleModule(ByVal resultActionType As ResultActions, ByVal currentResult As IZambaCore, ByVal docType As IZambaCore)
        RaiseEvent eHandleModuleWithDoctype(resultActionType, currentResult, docType)
    End Sub

    Public Shared Sub HandleModule(ByVal moduleName As ResultActions, ByVal currentResult As IZambaCore)
        RaiseEvent eHandleModule(moduleName, currentResult)
    End Sub

#End Region

#End Region

#Region "Locales"

#Region "Privados"

    Private Sub ZControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = e.KeyCode.Enter Then 'e.Keys.Enter Then
            RaiseEvent EnterPressed()
        End If
    End Sub

    Private Sub ZControl_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Click
        RaiseEvent RefreshTimeOut()
    End Sub

    Private Sub ZControl_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        RaiseEvent RefreshTimeOut()
    End Sub

    Private Sub ZControl_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragEnter
        If (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop)) Then
            e.Effect = System.Windows.Forms.DragDropEffects.Link
        Else
            e.Effect = System.Windows.Forms.DragDropEffects.None
        End If
    End Sub

    Private Sub ZControl_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Enter
        RaiseEvent RefreshTimeOut()
    End Sub
    Private Sub ZControl_MouseEnter(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.MouseEnter
        RaiseEvent RefreshTimeOut()
    End Sub

#End Region

#Region "Protegidos"

    Protected Sub RaiseEnterPressed()
        RaiseEvent EnterPressed()
    End Sub
    Protected Sub RaiseTabPressed()
        RaiseEvent TabPressed()
    End Sub

    Protected Sub CloseControl()
        RaiseEvent CloseCtrl()
    End Sub

    ''' <summary>
    ''' Metodo el cual es utilizado por las clases hijas
    ''' para que se dispare el evento de modificacion de atributos
    ''' </summary>
    ''' <param name="Result"></param>
    ''' <remarks></remarks>
    Protected Sub ChangeControl(ByRef Item As IZBaseCore) Implements IZControl.ChangeControl
        RaiseEvent OnChangeControl(Item)
    End Sub

#End Region

#End Region

#End Region

#Region "Eventos"

#Region "Globales"

#Region "Publicos"

    Public Shared Event eHandleModule(ByVal resultActionType As ResultActions, ByVal currentResult As IZambaCore)
    Public Shared Event eHandleModuleWithDoctype(ByVal resultActionType As ResultActions, ByVal currentResult As IZambaCore, ByVal docType As IZambaCore)
    Public Shared Event RefreshTimeOut()
    Public Shared Event ShowWait(ByVal Estado As Boolean, ByVal Cancel As Boolean)
    'Este evento no se para que es. Lo agregue porque se usa en el MAINFORM
    Public Shared Event EnterControl(ByVal sender As Object, ByVal e As EventArgs)

#End Region

#End Region


#Region "Locales"

#Region "Publicos"

    '[Ezequiel] - Evento que se dispara al modificar los atributos
    Public Event OnChangeControl(ByRef Item As IZBaseCore) Implements IZControl.OnChangeControl
    Public Event EnterPressed()
    Public Event TabPressed()
    Public Event CloseCtrl()

#End Region

#End Region


#End Region

End Class
