Public Class txtDateTimeIndexCtrl
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
        FlagData2 = data2
        Init()
        RefreshControl(Index)
    End Sub

#End Region

#Region "Propiedades Publicas"
    Private Shadows _IsValid As Boolean = True
    Public Overrides Property IsValid() As Boolean
        Get
            If _IsValid = False Then
                DataChangeProcedure(ComboBox1, New ComponentModel.CancelEventArgs)
                'Me.ErrorProvider1.SetError(Me.ComboBox1, "")
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

    'Este flag me dice si trabajo con data1 o data2
    Private FlagData2 As Boolean
    'Picker para las fechas
    '    Private WithEvents DT As Windows.Forms.DateTimePicker



    Private Sub Init()
        Try
            RemoveHandlers()

            Index.DataTemp = Index.Data
            Index.DataTemp2 = Index.Data2
            Index.dataDescriptionTemp = Index.dataDescription
            Index.dataDescriptionTemp2 = Index.dataDescription2

            If IsNothing(Data) = False AndAlso Data.Length > 0 Then
                ComboBox1.Value = DateTime.ParseExact(Data, "dd/MM/yyyy HH:mm", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                ComboBox1.Checked = True
            End If
        Catch ex As FormatException
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        Finally
            AddHandlers()
        End Try
    End Sub

    Public Overrides Sub RefreshControl(ByRef index As IIndex)
        Try
            RemoveHandlers()
            Me.Index = index
            If index Is Nothing = False Then
                Me.Index.DataTemp = index.Data
                Me.Index.DataTemp2 = index.Data2
                Me.Index.Data = index.Data
                Me.Index.dataDescriptionTemp = index.dataDescription
                Me.Index.dataDescriptionTemp2 = index.dataDescription2
            End If

            'CAMBIE REALDTA POR DATA
            If IsNothing(Data) = False AndAlso Data.Length > 0 Then
                ComboBox1.Text = Data
                ComboBox1.Checked = True
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
        ComboBox1.Text = index.DataTemp
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
            If FlagData2 Then
                Return Index.DataTemp2
            Else
                Return Index.DataTemp
            End If
        End Get
        Set(ByVal Value As String)

            Dim FlagCambioElContenido As Boolean = False

            If FlagData2 Then
                If IsNothing(Index.DataTemp2) OrElse Index.DataTemp2.Trim <> Value.Trim Then FlagCambioElContenido = True
                Index.DataTemp2 = Value.Trim
            Else
                If IsNothing(Index.DataTemp) OrElse Index.DataTemp.Trim <> Value.Trim Then FlagCambioElContenido = True
                Index.DataTemp = Value.Trim
            End If
            If FlagCambioElContenido = True Then RaiseEvent IndexChanged()
        End Set
    End Property
    Private ReadOnly Property Data() As String
        Get
            If FlagData2 Then
                Return Index.Data2
            Else
                Return Index.Data
            End If
        End Get
    End Property
    Private Property RealDataDescription() As String
        Get
            If FlagData2 Then
                Return Index.dataDescriptionTemp2
            Else
                Return Index.dataDescriptionTemp
            End If
        End Get
        Set(ByVal Value As String)
            If FlagData2 Then
                Index.dataDescriptionTemp2 = Value
            Else
                Index.dataDescriptionTemp = Value
            End If
        End Set
    End Property

#End Region

#Region "Eventos de los controles especificos"
    Private Sub AddHandlers()
        Try
            RemoveHandler ComboBox1.Validating, AddressOf DataChangeProcedure
            AddHandler ComboBox1.Validating, AddressOf DataChangeProcedure
        Catch
        End Try
    End Sub
    Private Sub RemoveHandlers()
        Try
            RemoveHandler ComboBox1.Validating, AddressOf DataChangeProcedure
        Catch
        End Try
    End Sub

    Private Sub DataChangeProcedure(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Try
            RemoveHandlers()

            If ComboBox1.Checked = False Then
                IsValid = True
                Exit Sub
            Else
                Try
                    If ComboBox1.Checked = True Then
                        'Me.RealData = Me.ComboBox1.Value.ToString("dd/MM/yyyy HH:mm")
                        RealData = ComboBox1.Value.ToString()
                    Else
                        RealData = ""
                    End If
                    IsValid = True
                    Exit Sub
                Catch ex As Exception
                    If Me.Mode = Modes.Search Then
                        ErrorProvider1.SetError(ComboBox1, "La hora ingresada no es valida")
                        'Muestro el icono de error
                    Else
                        ComboBox1.Text = RealData
                        IsValid = True
                        Exit Sub
                    End If
                End Try
            End If
        Finally
            IsValid = True
            AddHandlers()
        End Try
    End Sub
#End Region

#Region " Código generado por el Diseñador de Windows Forms "

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
            If ComboBox1 IsNot Nothing Then
                ComboBox1.Dispose()
                ComboBox1 = Nothing
            End If
            If ErrorProvider1 IsNot Nothing Then
                ErrorProvider1.Dispose()
                ErrorProvider1 = Nothing
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents ComboBox1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        ComboBox1 = New System.Windows.Forms.DateTimePicker
        ErrorProvider1 = New System.Windows.Forms.ErrorProvider
        SuspendLayout()
        '
        'ComboBox1
        '
        ComboBox1.CustomFormat = "dd/MM/yyyy HH:mm"
        ComboBox1.Format = DateTimePickerFormat.Custom
        ComboBox1.ShowCheckBox = True
        ComboBox1.Checked = False
        ComboBox1.BackColor = Color.White
        ComboBox1.Dock = DockStyle.Fill
        ComboBox1.ForeColor = Color.FromArgb(76, 76, 76)
        ErrorProvider1.SetIconPadding(ComboBox1, -40)
        ComboBox1.Location = New Point(0, 0)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(504, 21)
        ComboBox1.TabIndex = 0
        '
        'ErrorProvider1
        '
        ErrorProvider1.ContainerControl = Me
        '
        'txtIndexCtrl
        '
        Controls.Add(ComboBox1)
        Name = "txtDateIndexCtrl"
        Size = New Size(528, 40)
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
        'If Me.IsValid = False Then Me.ErrorProvider1.SetError(Me.ComboBox1, "")
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

    Private Sub ComboBox1_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBox1.ValueChanged
        DataChangeProcedure(ComboBox1, New ComponentModel.CancelEventArgs)
    End Sub
End Class

