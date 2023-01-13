Public Class ChkIndexCtrl
    Inherits txtBaseIndexCtrl
#Region "Constructores"
    Private Mode As Modes
    Public Sub New(ByVal docindex As IIndex, ByVal data2 As Boolean, ByVal Mode As Modes)
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()
        Me.Mode = Mode
        Index = docindex
        Init()
        RefreshControl(Index)
    End Sub
    Public Sub New(ByVal mode As Modes)
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()
        Me.Mode = mode

    End Sub
#End Region

#Region "Propiedades Publicas"
    Public Overrides Property IsValid() As Boolean
        Get
            If _IsValid = False Then
                DataChangeProcedure(ComboBox1, New ComponentModel.CancelEventArgs)
            End If
            Return _IsValid
        End Get
        Set(ByVal Value As Boolean)
            _IsValid = Value
            If Value = True Then
                ErrorProvider1.SetError(ComboBox1, "")
            End If
        End Set
    End Property
#End Region

#Region "Inicializadores"

    Private Sub Init()
        'Inicializa el Control para el tipo del indice Fecha
        Try
            SuspendLayout()
            RemoveHandlers()
            '            If IsNothing(Index.Data) Then Index.Data = 0
            Index.DataTemp = Index.Data
            Index.DataTemp2 = Index.Data2
            Index.dataDescriptionTemp = Index.dataDescription
            Index.dataDescriptionTemp2 = Index.dataDescription2
        Finally
            AddHandlers()
            ResumeLayout()
        End Try
    End Sub
    Public Overrides Sub RefreshControl(ByRef index As IIndex)
        Try
            RemoveHandlers()
            Me.Index = index
            If index Is Nothing = False Then
                '               If IsNothing(index.Data) Then index.Data = 0
                index.DataTemp = index.Data
                index.DataTemp2 = index.Data2
                index.dataDescriptionTemp = index.dataDescription
                index.dataDescriptionTemp2 = index.dataDescription2
            End If

            'If IsNothing(index) = False Then
            '    Me.Index = index
            'Else
            '    index = Me.Index
            'End If
            '          If IsNothing(index.Data) Then index.Data = 0
            If IsNothing(Data) = False AndAlso Data.Length > 0 Then
                ComboBox1.Checked = CBool(Data)
            Else
                ComboBox1.Checked = False
            End If
            IsValid = True
            AddHandlers()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Public Overrides Sub RefreshControlDataTemp(ByRef index As IIndex)
        ComboBox1.Checked = False
    End Sub
    Public Overrides Sub RollBack()
        Index.DataTemp = Index.Data
        Index.DataTemp2 = Index.Data2
        Index.dataDescriptionTemp = Index.dataDescription
        Index.dataDescriptionTemp2 = Index.dataDescription2
        RefreshControl(Index)
    End Sub
    Public Overrides Sub Commit()
        If IsNothing(Index.DataTemp) = False Then
            Index.Data = Index.DataTemp
            Index.dataDescription = Index.dataDescriptionTemp
            RefreshControl(Index)
        End If
        If IsNothing(Index.DataTemp2) = False Then
            Index.Data2 = Index.DataTemp2
            Index.dataDescription2 = Index.dataDescriptionTemp2
            RefreshControl(Index)
        End If
    End Sub
    Public Shadows Event IndexChanged()
    Private Property RealData() As String
        Get
            Return Index.DataTemp
        End Get
        Set(ByVal Value As String)
            Dim FlagCambioElContenido As Boolean = False
            If IsNothing(Index.DataTemp) = False Then
                If Index.DataTemp.Trim <> Value.Trim Then FlagCambioElContenido = True
            Else
                FlagCambioElContenido = True
            End If
            Index.DataTemp = Value.Trim
            If ComboBox1.Text = Value Then ComboBox1.Text = Value.Trim
            If FlagCambioElContenido = True Then RaiseEvent IndexChanged()
        End Set
    End Property
    Private ReadOnly Property Data() As String
        Get
            Return Index.Data
        End Get
    End Property
    Private Property RealDataDescription() As String
        Get
            Return Index.dataDescriptionTemp
        End Get
        Set(ByVal Value As String)
            Index.dataDescriptionTemp = Value
        End Set
    End Property
#End Region

#Region "Eventos de los controles especificos"
    Private Sub AddHandlers()
        Try
            RemoveHandler ComboBox1.CheckedChanged, AddressOf DataChangeProcedure
            AddHandler ComboBox1.CheckedChanged, AddressOf DataChangeProcedure
        Catch
        End Try
    End Sub
    Private Sub RemoveHandlers()
        Try
            RemoveHandler ComboBox1.CheckedChanged, AddressOf DataChangeProcedure
        Catch
        End Try
    End Sub

    Private Sub DataChangeProcedure(ByVal sender As Object, ByVal e As EventArgs)
        Try
            RemoveHandlers()
            If ComboBox1.Checked Then
                RealData = "1"
            Else
                RealData = "0"
            End If
            IsValid = True
        Finally
            '            Me.IsValid = True
            AddHandlers()
        End Try
    End Sub
#End Region

#Region " Código generado por el Diseñador de Windows Forms "

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
                If ComboBox1 IsNot Nothing Then
                    ComboBox1.Dispose()
                    ComboBox1 = Nothing
                End If
                If Button1 IsNot Nothing Then
                    Button1.Dispose()
                    Button1 = Nothing
                End If
                If ErrorProvider1 IsNot Nothing Then
                    ErrorProvider1.Dispose()
                    ErrorProvider1 = Nothing
                End If
            End If
            MyBase.Dispose(disposing)
        Catch
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents ComboBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents Button1 As ZButton
    '    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        ComboBox1 = New System.Windows.Forms.CheckBox
        ErrorProvider1 = New System.Windows.Forms.ErrorProvider
        SuspendLayout()
        '
        'ComboBox1
        '
        ComboBox1.BackColor = Color.White
        ComboBox1.Dock = DockStyle.Fill
        ComboBox1.ForeColor = Color.FromArgb(76, 76, 76)
        ErrorProvider1.SetIconPadding(ComboBox1, -40)
        ComboBox1.Location = New Point(0, 0)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(168, 20)
        ComboBox1.TabIndex = 0
        '
        'ErrorProvider1
        '
        ErrorProvider1.ContainerControl = Me
        '
        'ChkIndexCtrl
        '
        Controls.Add(ComboBox1)
        Name = "ChkIndexCtrl"
        Size = New Size(168, 20)
        ResumeLayout(False)

    End Sub
#End Region

#Region "Cambio de color al Editar el combo"

    Private Sub ComboBox1_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBox1.GotFocus
        ComboBox1.BackColor = Color.FromArgb(255, 224, 192)
    End Sub
    Private Sub ComboBox1_LostFocus(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBox1.LostFocus
        RemoveHandlers()
        ComboBox1.BackColor = Color.White
        If IsValid = False Then DataChangeProcedure(ComboBox1, New ComponentModel.CancelEventArgs)
        AddHandlers()
    End Sub

#End Region

#Region "Eventos de Enter y Tab"
    Public Shadows Event EnterPressed()
    Public Shadows Event TabPressed()
    Private Sub txtIndexCtrl_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Enter Then
            DataChangeProcedure(ComboBox1, New ComponentModel.CancelEventArgs)
            RaiseEvent EnterPressed()
        End If
        If e.KeyCode = Keys.Tab Then
            RaiseEvent TabPressed()
        End If
    End Sub

    Private Sub txtIndexCtrl_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        If e.KeyChar = Chr(13) Then
            DataChangeProcedure(ComboBox1, New ComponentModel.CancelEventArgs)
            RaiseEvent EnterPressed()
        End If
        If e.KeyChar = Chr(9) Then
            RaiseEvent TabPressed()
        End If

    End Sub

    Private Sub ComboBox1_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles ComboBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            DataChangeProcedure(ComboBox1, New ComponentModel.CancelEventArgs)
            RaiseEvent EnterPressed()
        End If
        If e.KeyCode = Keys.Tab Then
            RaiseEvent TabPressed()
        End If
    End Sub

#End Region

End Class