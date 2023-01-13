Imports Zamba.Core


Public Class ZForm
    Inherits Form

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()
        If Parent IsNot Nothing Then
            path = Parent.Name & "-" & Name
        Else
            path = Name
        End If
        Dim HelpMenu As New HelpMenu
        HelpMenu.LoadContextMenu(Me, ContextMenu, Handle)
    End Sub

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        Catch ex As System.NullReferenceException
        Catch ex As Exception
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(ZForm))
        '
        'ZForm
        '
        BackColor = System.Drawing.Color.White
        DockPadding.All = 2
        Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Try
            Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Catch ex As Exception
        End Try
        Name = "ZForm"
        Text = " "

    End Sub

#End Region

    'Este evento se usa para que una regla le pase a quien corresponda un result (y posiblemente mas parametros) mas 
    'una String describiendo el modulo contiene que modulo se debe cargar (Ej :"Caratulas" , "Importacion")

    Public Shared Event eHandleModule(ByVal resultActionType As ResultActions, ByVal currentResult As IZambaCore)
    Public Shared Event eHandleModuleWithDoctype(ByVal resultActionType As ResultActions, ByVal currentResult As IZambaCore, ByVal docType As IZambaCore)

    Public Shared Sub HandleModule(ByVal resultActionType As ResultActions, ByVal currentResult As IZambaCore, ByVal docType As IZambaCore)
        RaiseEvent eHandleModuleWithDoctype(resultActionType, currentResult, docType)
    End Sub

    Public Shared Sub HandleModule(ByVal moduleName As ResultActions, ByVal currentResult As IZambaCore)
        RaiseEvent eHandleModule(moduleName, currentResult)
    End Sub

    '  Public Shared Event LogError(ByVal ex As Exception)
    Public Shared Event RefreshTimeOut()
    Public Shared Event ShowWait(ByVal Estado As Boolean, ByVal Cancel As Boolean)

    Protected Shared Sub MostrarWait(ByVal Estado As Boolean, Optional ByVal Cancel As Boolean = False)
        RaiseEvent ShowWait(Estado, Cancel)
    End Sub

    'Protected Sub zamba.core.zclass.raiseerror(ByVal ex As Exception)
    '    Zamba.Core.ZClass.raiseerror(ex)
    '    '        RaiseEvent LogError(ex)
    'End Sub

    Private Sub ZForm_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Enter
        RaiseEvent RefreshTimeOut()
    End Sub
    Private Sub ZForm_MouseEnter(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.MouseEnter
        RaiseEvent RefreshTimeOut()
    End Sub

    Public Event EnterPressed()
    Public Event TabPressed()
    Protected Sub RaiseEnterPressed()
        RaiseEvent EnterPressed()
    End Sub
    Protected Sub RaiseTabPressed()
        RaiseEvent TabPressed()
    End Sub
    Public Shared Event FilesDragged(ByVal Files() As String)
    Public Event Finish()
    Public Sub ShowMessage(ByVal msg As String, ByVal Titulo As String)
        System.Windows.Forms.MessageBox.Show(msg, Titulo, MessageBoxButtons.OK, MessageBoxIcon.None)
    End Sub

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

    Private _Id As Int32
    Public Property Id() As Int32
        Get
            Return _Id
        End Get
        Set(ByVal Value As Int32)
            _Id = Value
        End Set
    End Property

    Private Sub ZForm_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Click
        RaiseEvent RefreshTimeOut()
    End Sub

    Private Sub ZForm_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        RaiseEvent RefreshTimeOut()
    End Sub

    Private Sub ZForm_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragEnter
        If (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop)) Then
            e.Effect = System.Windows.Forms.DragDropEffects.Link
        Else
            e.Effect = System.Windows.Forms.DragDropEffects.None
        End If
    End Sub

    Private Sub ZForm_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragDrop
        Dim sFileArray() As String = e.Data.GetData(System.Windows.Forms.DataFormats.FileDrop)
        RaiseEvent FilesDragged(sFileArray)
    End Sub


End Class

