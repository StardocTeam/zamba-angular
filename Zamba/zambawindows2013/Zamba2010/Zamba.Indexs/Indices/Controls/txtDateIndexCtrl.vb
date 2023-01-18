Public Class txtDateIndexCtrl
    Inherits txtBaseIndexCtrl
    Implements IDisposable

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
    Private FlagData2 As Boolean
    'Picker para las fechas
    Private WithEvents DT As DateTimePicker

    Private Sub Init()
        'Inicializa el Control para el tipo del indice Fecha
        Try
            'Me.SuspendLayout()
            RemoveHandlers()

            Index.DataTemp = Index.Data
            Index.DataTemp2 = Index.Data2
            Index.dataDescriptionTemp = Index.dataDescription
            Index.dataDescriptionTemp2 = Index.dataDescription2

            If Me.Index.Type = IndexDataType.Fecha OrElse Me.Index.Type = IndexDataType.Fecha_Hora Then
                DT = New DateTimePicker
                DT.CustomFormat = "dd/MM/yyyy"
                Try
                    DT.TabStop = False
                    If IsNothing(Data) = False AndAlso Data.Length > 0 Then DT.Value = Date.ParseExact(Data, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                Catch ex As FormatException
                Catch ex As Exception
                    zamba.core.zclass.raiseerror(ex)
                End Try
                Panel1.Controls.Add(DT)
                DT.Dock = DockStyle.Fill
                ComboBox1.MaxLength = 10
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        Finally
            AddHandlers()
            '   Me.ResumeLayout()
        End Try
    End Sub

    Public Overrides Sub RefreshControl(ByRef index As IIndex)
        Try
            RemoveHandlers()
            Me.Index = index
            If index Is Nothing = False Then
                If Not (index.Data = String.Empty) Then
                    Dim dia As Date
                    If Date.TryParse(index.Data, dia) Then
                        index.Data = dia.ToString("dd/MM/yyyy")
                        Me.Index.DataTemp = index.Data
                        Me.Index.DataTemp2 = index.Data2
                        Me.Index.Data = index.Data
                        Me.Index.dataDescriptionTemp = index.dataDescription
                        Me.Index.dataDescriptionTemp2 = index.dataDescription2
                    End If
                End If
            End If

            If Data = String.Empty Then
                ComboBox1.Text = Data
            Else
                DT.Value = CType(Data, Date)
                DT.CustomFormat = "dd/MM/yyyy"
                DateProcedure(DT, New EventArgs)
            End If

            'CAMBIE REALDTA POR DATA
            ' Me.ComboBox1.Text = Me.Data
            IsValid = True
            AddHandlers()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Public Overrides Sub RefreshControlDataTemp(ByRef index As IIndex)
        ComboBox1.Text = index.DataTemp
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
            RemoveHandler ComboBox1.TextChanged, AddressOf ComboBox1_TextChanged
            AddHandler ComboBox1.TextChanged, AddressOf ComboBox1_TextChanged

            'RemoveHandler DT.ValueChanged, AddressOf Me.DateProcedure
            'AddHandler DT.ValueChanged, AddressOf Me.DateProcedure
            'RemoveHandler DT.Validating, AddressOf Me.DateProcedure
            'AddHandler DT.Validating, AddressOf Me.DateProcedure
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Sub RemoveHandlers()
        Try
            RemoveHandler ComboBox1.Validating, AddressOf DataChangeProcedure
            RemoveHandler ComboBox1.TextChanged, AddressOf ComboBox1_TextChanged


            If Not IsNothing(DT) Then

                RemoveHandler DT.ValueChanged, AddressOf DateProcedure
                RemoveHandler DT.Validating, AddressOf DateProcedure
            End If

        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Private Sub ComboBox1_Click(ByVal sender As Object, ByVal e As EventArgs)

    End Sub
    Private Sub ComboBox1_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try

            Dim FlagLastCharSelected As Boolean = False
            If ComboBox1.SelectionStart = Len(ComboBox1.Text) Then
                FlagLastCharSelected = True
            End If

            IsValid = False
            RemoveHandlers()
            If ComboBox1.Text.CompareTo("") = 0 Then ErrorProvider1.SetError(ComboBox1, "")
            Try
                If ComboBox1.Text.Length > 0 Then
                    Dim C As Char = ComboBox1.Text.Chars(ComboBox1.Text.Length - 1)
                    If Char.IsLetter(C) Then
                        ComboBox1.Text = ComboBox1.Text.Substring(0, ComboBox1.Text.Length - 1)
                        ComboBox1.SelectionStart = ComboBox1.Text.Length
                        ComboBox1.SelectionLength = 0
                    End If
                Else
                    RealData = ""
                End If
            Catch ex As IndexOutOfRangeException
            Catch ex As Exception
                zamba.core.zclass.raiseerror(ex)
            End Try
        Catch ex As IndexOutOfRangeException
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        Finally
            AddHandlers()
            ComboBox1.Tag = ComboBox1.Text
            'sebastian
            ComboBox1.Focus()
        End Try
    End Sub

    Private Sub DateProcedure(ByVal sender As Object, ByVal ev As EventArgs)
        Try

            ComboBox1.Text = DT.Value.ToShortDateString
            ComboBox1.Select()

        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Sub DateProcedure(ByVal sender As Object, ByVal ev As System.ComponentModel.CancelEventArgs)
        Try
            ' Me.IsValid = False
            RemoveHandlers()
            ComboBox1.Text = DT.Value.ToShortDateString
            '     Me.ComboBox1.Text = DT.Value.ToString("dd/MM/yyyy")
            ComboBox1.Select()
            AddHandlers()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Private Sub DataChangeProcedure(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Try
            RemoveHandlers()

            If ComboBox1.Text = "" Then
                IsValid = True
                Exit Sub
            ElseIf Me.Index.Type = IndexDataType.Fecha Then  'AndAlso Me.RealData <> Me.ComboBox1.Text
                Try
                    ComboBox1.Text = ComboBox1.Text.Replace("-", "/")
                    ComboBox1.Text = ComboBox1.Text.Replace(".", "/")
                    ComboBox1.Text = ComboBox1.Text.Replace(" ", "/")

                    ComboBox1.Text = ComboBox1.Text.Replace("//", "/")

                    ComboBox1.SelectionStart = ComboBox1.Text.Length
                    ComboBox1.SelectionLength = 0

                    Dim d As Date = Date.Parse(ComboBox1.Text, System.Globalization.DateTimeFormatInfo.CurrentInfo)
                    If IsNothing(DT) = False Then
                        If DT.Value = d Then
                            ComboBox1.Text = DT.Value.ToShortDateString
                        Else
                            DT.Value = d
                        End If
                    End If
                    'TODO: Verificar si le voy a mandar la fecha con la cultura actual o se lo mando con mi formato
                    RealData = d.ToString("dd/MM/yyyy")
                    'Me.RealData = d.ToString(System.Globalization.DateTimeFormatInfo.CurrentInfo)
                    IsValid = True
                    Exit Sub
                Catch ex As System.FormatException
                    If Me.Mode = Modes.Search Then
                        ErrorProvider1.SetError(ComboBox1, "La fecha ingresada no es valida")
                        'Muestro el icono de error
                    Else
                        ComboBox1.Text = RealData
                        IsValid = True
                        Exit Sub
                    End If
                    Try
                        If IsNothing(DT) = False Then DT.Value = Now
                    Catch
                    End Try
                Catch ex As System.ArgumentOutOfRangeException
                    If Me.Mode = Modes.Search Then
                        ErrorProvider1.SetError(ComboBox1, "La fecha ingresada no es valida")
                        'Muestro el icono de error
                    Else
                        ComboBox1.Text = RealData
                        IsValid = True
                        Exit Sub
                    End If
                    Try
                        If IsNothing(DT) = False Then DT.Value = Now
                    Catch
                    End Try
                Catch ex As System.IndexOutOfRangeException
                    If Me.Mode = Modes.Search Then
                        ErrorProvider1.SetError(ComboBox1, "La fecha ingresada no es valida")
                        'Muestro el icono de error
                    Else
                        ComboBox1.Text = RealData
                        IsValid = True
                        Exit Sub
                    End If
                    Try
                        If IsNothing(DT) = False Then DT.Value = Now
                    Catch
                    End Try
                Catch ex As System.InvalidCastException
                    If Me.Mode = Modes.Search Then
                        ErrorProvider1.SetError(ComboBox1, "La fecha ingresada no es valida")
                        'Muestro el icono de error
                    Else
                        ComboBox1.Text = RealData
                        IsValid = True
                        Exit Sub
                    End If
                    Try
                        If IsNothing(DT) = False Then DT.Value = Now
                    Catch
                    End Try
                Catch ex As OverflowException
                    If Me.Mode = Modes.Search Then
                        ErrorProvider1.SetError(ComboBox1, "La fecha ingresada no es valida")
                        'Muestro el icono de error
                    Else
                        ComboBox1.Text = RealData
                        IsValid = True
                        Exit Sub
                    End If
                    Try
                        If IsNothing(DT) = False Then DT.Value = Now
                    Catch
                    End Try
                Catch ex As Exception
                    If Me.Mode = Modes.Search Then
                        ErrorProvider1.SetError(ComboBox1, "La fecha ingresada no es valida")
                        'Muestro el icono de error
                    Else
                        ComboBox1.Text = RealData
                        IsValid = True
                        Exit Sub
                    End If
                    Try
                        If IsNothing(DT) = False Then DT.Value = Now
                    Catch
                    End Try
                    'Catch
                    '    If Me.Mode = Modes.Search Then
                    '        Me.ErrorProvider1.SetError(Me.ComboBox1, "La fecha ingresada no es valida")
                    '        'Muestro el icono de error
                    '    Else
                    '        Me.ComboBox1.Text = Me.RealData
                    '        Me.IsValid = True
                    '        Exit Sub
                    '    End If
                    '    Try
                    '        If IsNothing(Me.DT) = False Then Me.DT.Value = Now
                    '    Catch
                    '    End Try
                End Try
            ElseIf Me.Index.Type = IndexDataType.Fecha_Hora Then  'AndAlso Me.RealData <> Me.ComboBox1.Text
                Try
                    ComboBox1.Text = ComboBox1.Text.Replace("-", "/")
                    ComboBox1.Text = ComboBox1.Text.Replace(" ", "/")
                    ComboBox1.SelectionStart = ComboBox1.Text.Length
                    ComboBox1.SelectionLength = 0

                    Dim d As DateTime = DateTime.Parse(ComboBox1.Text, System.Globalization.DateTimeFormatInfo.CurrentInfo)
                    If IsNothing(DT) = False Then
                        If DT.Value = d Then
                            ComboBox1.Text = DT.Value.ToShortTimeString
                        Else
                            DT.Value = d
                        End If
                    End If
                    'TODO: Verificar si le voy a mandar la fecha con la cultura actual o se lo mando con mi formato
                    '            Me.RealData = d.ToString("dd/MM/yyyy")
                    RealData = d.ToString(System.Globalization.DateTimeFormatInfo.CurrentInfo)
                    IsValid = True
                    Exit Sub
                Catch ex As System.FormatException
                    If Me.Mode = Modes.Search Then
                        ErrorProvider1.SetError(ComboBox1, "La hora ingresada no es valida")
                        'Muestro el icono de error
                    Else
                        ComboBox1.Text = RealData
                        IsValid = True
                        Exit Sub
                    End If
                    Try
                        If IsNothing(DT) = False Then DT.Value = Now
                    Catch
                    End Try
                Catch ex As System.ArgumentOutOfRangeException
                    If Me.Mode = Modes.Search Then
                        ErrorProvider1.SetError(ComboBox1, "La hora ingresada no es valida")
                        'Muestro el icono de error
                    Else
                        ComboBox1.Text = RealData
                        IsValid = True
                        Exit Sub
                    End If
                    Try
                        If IsNothing(DT) = False Then DT.Value = Now
                    Catch
                    End Try
                Catch ex As System.IndexOutOfRangeException
                    If Me.Mode = Modes.Search Then
                        ErrorProvider1.SetError(ComboBox1, "La hora ingresada no es valida")
                        'Muestro el icono de error
                    Else
                        ComboBox1.Text = RealData
                        IsValid = True
                        Exit Sub
                    End If
                    Try
                        If IsNothing(DT) = False Then DT.Value = Now
                    Catch
                    End Try
                Catch ex As System.InvalidCastException
                    If Me.Mode = Modes.Search Then
                        ErrorProvider1.SetError(ComboBox1, "La hora ingresada no es valida")
                        'Muestro el icono de error
                    Else
                        ComboBox1.Text = RealData
                        IsValid = True
                        Exit Sub
                    End If
                    Try
                        If IsNothing(DT) = False Then DT.Value = Now
                    Catch
                    End Try
                Catch ex As OverflowException
                    If Me.Mode = Modes.Search Then
                        ErrorProvider1.SetError(ComboBox1, "La hora ingresada no es valida")
                        'Muestro el icono de error
                    Else
                        ComboBox1.Text = RealData
                        IsValid = True
                        Exit Sub
                    End If
                    Try
                        If IsNothing(DT) = False Then DT.Value = Now
                    Catch
                    End Try
                Catch ex As Exception
                    If Me.Mode = Modes.Search Then
                        ErrorProvider1.SetError(ComboBox1, "La hora ingresada no es valida")
                        'Muestro el icono de error
                    Else
                        ComboBox1.Text = RealData
                        IsValid = True
                        Exit Sub
                    End If
                    Try
                        If IsNothing(DT) = False Then DT.Value = Now
                    Catch
                    End Try
                    'Catch
                    '    If Me.Mode = Modes.Search Then
                    '        Me.ErrorProvider1.SetError(Me.ComboBox1, "La hora ingresada no es valida")
                    '        'Muestro el icono de error
                    '    Else
                    '        Me.ComboBox1.Text = Me.RealData
                    '        Me.IsValid = True
                    '        Exit Sub
                    '    End If
                    '    Try
                    '        If IsNothing(Me.DT) = False Then Me.DT.Value = Now
                    '    Catch
                    '    End Try
                End Try
            End If
        Catch ex As FormatException
            If Me.Mode = Modes.Search Then
                ErrorProvider1.SetError(ComboBox1, "La valor ingresado es invalido")
                'Muestro el icono de error
            Else
                ComboBox1.Text = RealData
                IsValid = True
                Exit Sub
            End If
            Try
                If IsNothing(DT) = False Then DT.Value = Now
            Catch
            End Try
            zamba.core.zclass.raiseerror(ex)
        Catch ex As ArgumentNullException
            If Me.Mode = Modes.Search Then
                ErrorProvider1.SetError(ComboBox1, "La valor ingresado es invalido")
                'Muestro el icono de error
            Else
                ComboBox1.Text = RealData
                IsValid = True
                Exit Sub
            End If
            Try
                If IsNothing(DT) = False Then DT.Value = Now
            Catch
            End Try
            zamba.core.zclass.raiseerror(ex)
        Catch ex As InvalidCastException
            If Me.Mode = Modes.Search Then
                ErrorProvider1.SetError(ComboBox1, "La valor ingresado es invalido")
                'Muestro el icono de error
            Else
                ComboBox1.Text = RealData
                IsValid = True
                Exit Sub
            End If
            Try
                If IsNothing(DT) = False Then DT.Value = Now
            Catch
            End Try
            zamba.core.zclass.raiseerror(ex)
        Catch ex As OutOfMemoryException
            If Me.Mode = Modes.Search Then
                ErrorProvider1.SetError(ComboBox1, "La valor ingresado es invalido")
                'Muestro el icono de error
            Else
                ComboBox1.Text = RealData
                IsValid = True
                Exit Sub
            End If
            Try
                If IsNothing(DT) = False Then DT.Value = Now
            Catch
            End Try
            zamba.core.zclass.raiseerror(ex)
        Catch ex As OverflowException
            If Me.Mode = Modes.Search Then
                ErrorProvider1.SetError(ComboBox1, "La valor ingresado es invalido")
                'Muestro el icono de error
            Else
                ComboBox1.Text = RealData
                IsValid = True
                Exit Sub
            End If
            Try
                If IsNothing(DT) = False Then DT.Value = Now
            Catch
            End Try
            zamba.core.zclass.raiseerror(ex)
        Catch ex As Exception
            If Me.Mode = Modes.Search Then
                ErrorProvider1.SetError(ComboBox1, "La valor ingresado es invalido")
                'Muestro el icono de error
            Else
                ComboBox1.Text = RealData
                IsValid = True
                Exit Sub
            End If
            Try
                If IsNothing(DT) = False Then DT.Value = Now
            Catch
            End Try
            zamba.core.zclass.raiseerror(ex)
            'Catch
            '    If Me.Mode = Modes.Search Then
            '        Me.ErrorProvider1.SetError(Me.ComboBox1, "La valor ingresado es invalido")
            '        'Muestro el icono de error
            '    Else
            '        Me.ComboBox1.Text = Me.RealData
            '        Me.IsValid = True
            '        Exit Sub
            '    End If
            '    Try
            '        If IsNothing(Me.DT) = False Then Me.DT.Value = Now
            '    Catch
            '    End Try
        Finally
            IsValid = True
            AddHandlers()

        End Try
    End Sub
#End Region

#Region " Código generado por el Diseñador de Windows Forms "

    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If Not isDisposed Then
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If

                RemoveHandler MyBase.KeyDown, AddressOf txtIndexCtrl_KeyDown
                RemoveHandler MyBase.KeyPress, AddressOf txtIndexCtrl_KeyPress

                If ComboBox1 IsNot Nothing Then
                    RemoveHandler ComboBox1.GotFocus, AddressOf ComboBox1_GotFocus
                    RemoveHandler ComboBox1.KeyDown, AddressOf ComboBox1_KeyDown
                    RemoveHandler ComboBox1.LostFocus, AddressOf ComboBox1_LostFocus
                    RemoveHandler ComboBox1.Validating, AddressOf DataChangeProcedure
                    RemoveHandler ComboBox1.TextChanged, AddressOf ComboBox1_TextChanged
                    ComboBox1.Dispose()
                    ComboBox1 = Nothing
                End If
                If Panel1 IsNot Nothing Then
                    Panel1.Dispose()
                    Panel1 = Nothing
                End If
                If ErrorProvider1 IsNot Nothing Then
                    ErrorProvider1.Dispose()
                    ErrorProvider1 = Nothing
                End If
                If DT IsNot Nothing Then
                    RemoveHandler DT.CloseUp, AddressOf DT_CloseUp
                    RemoveHandler DT.KeyDown, AddressOf DT_KeyDown
                    DT.Dispose()
                    DT = Nothing
                End If
            End If
            MyBase.Dispose(disposing)
            isDisposed = True
        End If
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer
    Private isDisposed As Boolean
    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents ComboBox1 As TextBox
    Friend WithEvents Panel1 As ZPanel
    '    Friend WithEvents Button1 As ZButton
    '    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        ComboBox1 = New TextBox
        Panel1 = New ZPanel
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
        ComboBox1.Size = New Size(514, 21)
        ComboBox1.TabIndex = 0
        '
        'Panel1
        '
        Panel1.BackColor = Color.White
        Panel1.CausesValidation = False
        Panel1.Dock = DockStyle.Right
        Panel1.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
        Panel1.ForeColor = Color.FromArgb(76, 76, 76)
        Panel1.Location = New Point(504, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(14, 20)
        Panel1.TabIndex = 1
        '
        'ErrorProvider1
        '
        ErrorProvider1.ContainerControl = Me
        '
        'txtIndexCtrl
        '
        Controls.Add(ComboBox1)
        Controls.Add(Panel1)
        Name = "txtDateIndexCtrl"
        Size = New Size(528, 40)
        ResumeLayout(False)

    End Sub
#End Region

#Region "Cambio de color al Editar el combo"

    ''' <summary>
    ''' Se ejecuta cuando obtiene el foco el combo box del control de indice de zamba
    ''' </summary>
    ''' <history>
    ''' [Sebastian] 27-10-2009 se agreo evento para indice del tipo fecha, porqueno guardaba el cambio la primera vez
    ''' sino la segunda vez que se hacia clic en el control, esto era porque el evento changed se lanzaba cuando se perdia el foco.
    ''' </history>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ComboBox1_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBox1.GotFocus
        RemoveHandlers()

        ComboBox1.BackColor = Color.FromArgb(255, 224, 192)
        If IsValid = False Then DataChangeProcedure(ComboBox1, New ComponentModel.CancelEventArgs)
        AddHandlers()
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

    Private Sub DT_CloseUp(ByVal sender As Object, ByVal e As EventArgs) Handles DT.CloseUp
        Try
            IsValid = False
            DateProcedure(DT, New EventArgs)
        Catch
        End Try
    End Sub

    Private Sub DT_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles DT.KeyDown
        If e.KeyCode = Keys.Enter Then
            RaiseEvent EnterPressed()
        End If
        If e.KeyCode = Keys.Tab Then
            RaiseEvent TabPressed()
        End If

    End Sub
#End Region



End Class

