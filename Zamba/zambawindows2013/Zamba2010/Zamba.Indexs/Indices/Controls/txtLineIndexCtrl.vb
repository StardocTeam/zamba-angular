'Imports Zamba.Data
Public Class txtLineIndexCtrl
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
    '  Public _IsValid As Boolean = True
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
                ErrorProvider1.SetError(ComboBox1, String.Empty)
            End If
        End Set
    End Property
#End Region

#Region "Inicializadores"

    'Este flag me dice si trabajo con data1 o data2
    Private FlagData2 As Boolean

    Private Sub Init()
        'Inicializa el Control para el tipo del indice Fecha
        Try
            'Me.SuspendLayout()
            RemoveHandlers()

            Index.DataTemp = Index.Data
            Index.DataTemp2 = Index.Data2
            Index.dataDescriptionTemp = Index.dataDescription
            Index.dataDescriptionTemp2 = Index.dataDescription2

            Panel1.Visible = False
            ComboBox1.Dock = DockStyle.Fill
            ComboBox1.MaxLength = Index.Len
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
                Me.Index.DataTemp = index.Data
                Me.Index.DataTemp2 = index.Data2
                Me.Index.Data = index.Data
                Me.Index.dataDescriptionTemp = index.dataDescription
                Me.Index.dataDescriptionTemp2 = index.dataDescription2
            End If


            'CAMBIE REALDTA POR DATA
            ComboBox1.Text = Data
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
                If IsNothing(Index.DataTemp2) OrElse String.Compare(Index.DataTemp2.Trim, Value.Trim) <> 0 Then FlagCambioElContenido = True
                Index.DataTemp2 = Value.Trim
            Else
                If IsNothing(Index.DataTemp) OrElse String.Compare(Index.DataTemp.Trim, Value.Trim) <> 0 Then FlagCambioElContenido = True
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
    'Public Property Data() As String
    '    Get
    '        If Me.IsValid = False Then Me.DataChangeProcedure(Me.ComboBox1, New EventArgs)
    '        If FlagData2 Then
    '            Return Me.Index.Data2
    '        Else
    '            Return Me.Index.Data
    '        End If
    '    End Get
    '    Set(ByVal Value As String)
    '        If FlagData2 Then
    '            Me.Index.Data2 = Value
    '        Else
    '            Me.Index.Data = Value
    '        End If
    '    End Set
    'End Property

#End Region

#Region "Eventos de los controles especificos"
    Private Sub AddHandlers()
        Try
            RemoveHandler ComboBox1.Validating, AddressOf DataChangeProcedure
            AddHandler ComboBox1.Validating, AddressOf DataChangeProcedure
            RemoveHandler ComboBox1.TextChanged, AddressOf ComboBox1_TextChanged
            AddHandler ComboBox1.TextChanged, AddressOf ComboBox1_TextChanged

        Catch
        End Try
    End Sub
    Private Sub RemoveHandlers()
        Try
            RemoveHandler ComboBox1.Validating, AddressOf DataChangeProcedure
            RemoveHandler ComboBox1.TextChanged, AddressOf ComboBox1_TextChanged
        Catch
        End Try
    End Sub

    Private Sub ComboBox1_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try

            Dim FlagLastCharSelected As Boolean = False
            If ComboBox1.SelectionStart = Len(ComboBox1.Text) Then
                FlagLastCharSelected = True
            End If

            IsValid = False
            RemoveHandlers()
            If ComboBox1.Text.CompareTo(String.Empty) = 0 Then ErrorProvider1.SetError(ComboBox1, String.Empty)
            If (Me.Index.Type = IndexDataType.Numerico OrElse Me.Index.Type = IndexDataType.Numerico_Largo OrElse Me.Index.Type = IndexDataType.Moneda OrElse Me.Index.Type = IndexDataType.Numerico_Decimales) Then 'AndAlso Me.Index.DropDown <> IndexAdditionalType.AutoSustitución Then
                Try
                    'TODO: VER DE HACER TODOS ESTOS REPLACES DESPUES PARA QUE EL 

                    If ComboBox1.Text.Length > 0 AndAlso ComboBox1.Text.Substring(0, 1).CompareTo(".") = 0 Then
                        Dim Pos As Int32 = ComboBox1.SelectionStart
                        If Pos < 0 Then Pos = ComboBox1.Text.Length
                        ComboBox1.Text = ComboBox1.Text.Substring(1)
                        If pos >= 1 Then ComboBox1.SelectionStart = Pos - 1
                    End If

                    If ComboBox1.Text.IndexOf(",") >= 0 OrElse ComboBox1.Text.IndexOf("..") >= 0 Then
                        Dim Pos As Int32 = ComboBox1.SelectionStart
                        If Pos < 0 Then Pos = ComboBox1.Text.Length
                        ComboBox1.Text = ComboBox1.Text.Replace(",", ".")
                        ComboBox1.Text = ComboBox1.Text.Replace("..", ".")
                        ComboBox1.SelectionStart = Pos - 1

                        If ComboBox1.Text.Substring(0, 1).CompareTo(".") = 0 Then
                            Dim Pos2 As Int32 = ComboBox1.SelectionStart
                            If Pos2 < 0 Then Pos2 = ComboBox1.Text.Length
                            ComboBox1.Text = ComboBox1.Text.Substring(1)
                            If Pos2 >= 1 Then ComboBox1.SelectionStart = Pos2 - 1
                        End If

                    End If

                    'If Mid(ComboBox1.Text, 1, 1) = "-" Then
                    If ComboBox1.Text.Length > 1 AndAlso ComboBox1.Text.Substring(1, 1).CompareTo("-") = 0 Then
                        ComboBox1.Text = ComboBox1.Text.Replace("-", String.Empty)
                        ComboBox1.Text = "-" & ComboBox1.Text
                    Else
                        ComboBox1.Text = ComboBox1.Text.Replace("-", String.Empty)
                    End If

                    'If Mid(ComboBox1.Text, 1, 1) = "+" Then
                    If ComboBox1.Text.Length > 1 AndAlso ComboBox1.Text.Substring(1, 1).CompareTo("+") = 0 Then
                        ComboBox1.Text = ComboBox1.Text.Replace("+", String.Empty)
                        ComboBox1.Text = "+" & ComboBox1.Text
                    Else
                        ComboBox1.Text = ComboBox1.Text.Replace("+", String.Empty)
                    End If



                    If FlagLastCharSelected = True Then ComboBox1.Select(Len(ComboBox1.Text), 1)

                    If ComboBox1.Text.IndexOf(".") < ComboBox1.Text.LastIndexOf(".") Then
                        Dim Pos2 As Int32 = ComboBox1.SelectionStart
                        If Pos2 < 0 Then Pos2 = ComboBox1.Text.Length
                        ComboBox1.Text = ComboBox1.Tag.ToString
                        If Pos2 >= 1 Then ComboBox1.SelectionStart = Pos2 - 1
                    End If

                    If IsNumeric(ComboBox1.Text) = False Then

                        'If Val(Me.ComboBox1.Text).ToString = "0" Then
                        If String.Compare(ComboBox1.Text, "0") = 0 Then
                            ComboBox1.Text = String.Empty
                        ElseIf ComboBox1.Text.Length > 0 Then
                            ComboBox1.Text = Val(ComboBox1.Text).ToString
                            ComboBox1_TextChanged(Me, New EventArgs)
                        End If

                        If Me.Index.DropDown = IndexAdditionalType.AutoSustitución _
                            Or Me.Index.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                            ComboBox1.Text = "0"
                        End If
                        'Deberia reemplazar todas las letras no solo la ultima
                        'Try
                        '    'TODO NO SE PUEDE HACER LO SIGUIENTE PORQUE EL USUARIO PUEDE ESTAR
                        '    'ESCRIBIENDO EN CUALQUIER POSICION DEL COMBO
                        '    'HABRIA QUE HACER UN REPLACE O UN UNDO
                        '    'Me.ComboBox1.Text = Me.ComboBox1.Text.Substring(0, Me.ComboBox1.Text.Length - 1)
                        'Catch ex As IndexOutOfRangeException
                        'Catch
                        'End Try
                        ComboBox1.SelectionStart = ComboBox1.Text.Length
                        ComboBox1.SelectionLength = 0
                    End If
                    'If Me.Index.DropDown = IndexAdditionalType.AutoSustitución Then
                    '    'TODO POR AHORA LO SACAMOS PARA QUE A PARTIR DEL ID ENCUENTRE EL VALUE
                    '    'Me.ComboBox1.Text = ""
                    'End If
                Catch ex As IndexOutOfRangeException
                Catch ex As Exception
                    zamba.core.zclass.raiseerror(ex)
                End Try
            ElseIf Me.Index.Type = IndexDataType.Fecha OrElse Me.Index.Type = IndexDataType.Fecha_Hora Then
                Try
                    If ComboBox1.Text.Length > 0 Then
                        Dim C As Char = ComboBox1.Text.Chars(ComboBox1.Text.Length - 1)
                        If Char.IsLetter(C) Then
                            ComboBox1.Text = ComboBox1.Text.Substring(0, ComboBox1.Text.Length - 1)
                            ComboBox1.SelectionStart = ComboBox1.Text.Length
                            ComboBox1.SelectionLength = 0
                        End If
                    Else
                        RealData = String.Empty
                    End If
                Catch ex As IndexOutOfRangeException
                Catch ex As Exception
                    zamba.core.zclass.raiseerror(ex)
                End Try
            End If
        Catch ex As IndexOutOfRangeException
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        Finally
            AddHandlers()
            ComboBox1.Tag = ComboBox1.Text
        End Try
    End Sub
    Private Sub DataChangeProcedure(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Try
            RemoveHandlers()
            If String.IsNullOrEmpty(ComboBox1.Text) _
            AndAlso String.Compare(RealData, ComboBox1.Text) <> 0 _
            AndAlso Index.Type <> IndexDataType.Fecha _
            AndAlso Index.Type <> IndexDataType.Fecha_Hora Then
                RealData = ComboBox1.Text
                IsValid = True
                Exit Sub
            ElseIf String.IsNullOrEmpty(ComboBox1.Text) Then
                IsValid = True
                Exit Sub
            ElseIf (Me.Index.Type = IndexDataType.Numerico OrElse Me.Index.Type = IndexDataType.Numerico_Largo) _
            AndAlso String.Compare(RealData, ComboBox1.Text) <> 0 Then
                Try
                    ComboBox1.Text = ComboBox1.Text.Replace(",", ".")
                    ComboBox1.Text = ComboBox1.Text.Replace("..", ".")

                    If ComboBox1.Text.IndexOf("-") <> -1 AndAlso Mid(ComboBox1.Text, 1, 1) <> "-" Then ComboBox1.Text.Replace("-", String.Empty)

                    Decimal.Parse(ComboBox1.Text)
                    RealData = ComboBox1.Text
                    IsValid = True
                    Exit Sub
                Catch ex As Exception
                    '                    Me.ComboBox1.Text = Me.RealData
                    ErrorProvider1.SetError(ComboBox1, "El valor ingresado no es numerico")
                    '                   Me.IsValid = True
                End Try
            ElseIf (Me.Index.Type = IndexDataType.Moneda OrElse Me.Index.Type = IndexDataType.Numerico_Decimales) _
            AndAlso String.Compare(RealData, ComboBox1.Text) <> 0 Then
                Try
                    ComboBox1.Text.Replace(",", ".")
                    ComboBox1.Text.Replace("..", ".")
                    ' Dim n As Decimal = CDec(Me.ComboBox1.Text)
                    RealData = ComboBox1.Text
                    IsValid = True
                    Exit Sub
                Catch ex As Exception
                    '                 Me.ComboBox1.Text = Me.RealData
                    ErrorProvider1.SetError(ComboBox1, "El valor ingresado no es numerico")
                    '                  Me.IsValid = True
                End Try
            ElseIf (Me.Index.Type = IndexDataType.Alfanumerico OrElse Me.Index.Type = IndexDataType.Alfanumerico_Largo) _
            AndAlso String.Compare(RealData, ComboBox1.Text) <> 0 Then
                RealData = ComboBox1.Text
                IsValid = True
                Exit Sub
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
                    RemoveHandler ComboBox1.Validating, AddressOf DataChangeProcedure
                    RemoveHandler ComboBox1.GotFocus, AddressOf ComboBox1_GotFocus
                    RemoveHandler ComboBox1.LostFocus, AddressOf ComboBox1_LostFocus
                    RemoveHandler ComboBox1.KeyDown, AddressOf ComboBox1_KeyDown
                    RemoveHandler ComboBox1.DoubleClick, AddressOf ComboBox1_DoubleCLick
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
    '   Friend WithEvents Button1 As ZButton
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
        ComboBox1.Size = New Size(504, 21)
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
        Panel1.Size = New Size(24, 20)
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
        Name = "txtLineIndexCtrl"
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
        'If e.KeyCode = Keys.Tab Then
        '    RaiseEvent TabPressed()
        'End If
    End Sub

#End Region

#Region "Multiplecharacter TextBox"
    Private Sub ComboBox1_DoubleCLick(ByVal sender As System.Object, ByVal e As EventArgs) Handles ComboBox1.DoubleClick
        If Me.Index.Type = IndexDataType.Alfanumerico OrElse Me.Index.Type = IndexDataType.Alfanumerico_Largo Then
            'Se guardan los cambios en datatemp
            DataChangeProcedure(ComboBox1, New ComponentModel.CancelEventArgs)

            'Se muestra el formulario multilinea
            Dim frm As frmMultiline = New frmMultiline(Index.DataTemp, Index.Name)
            frm.ShowDialog()

            'Se guarda el valor
            ComboBox1.Text = frm.txtIndexValue.Text
            'Me.Index.DataTemp = frm.txtIndexValue.Text

            'Se guardan los cambios en datatemp
            DataChangeProcedure(ComboBox1, New ComponentModel.CancelEventArgs)

            ComboBox1.Select()
            ComboBox1.Focus()
            frm.Dispose()
        End If
    End Sub
#End Region



End Class


'                Catch ex As Exception
'Try
'    Dim d As Date = Date.ParseExact(Me.ComboBox1.Text, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
'    If IsNothing(Me.DT) = False Then
'        If Me.DT.Value = D Then
'            Me.ComboBox1.Text = DT.Value.ToString("dd/MM/yyyy")
'        Else
'            Me.DT.Value = D
'        End If
'    End If
'    Me.RealData = d.ToString("dd/MM/yyyy")
'    Me.IsValid = True
'catch ex as exception
'                        Try
'Dim d As Date = Date.ParseExact(Me.ComboBox1.Text, "d/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
'                            If IsNothing(Me.DT) = False Then
'                                If Me.DT.Value = D Then
'                                    Me.ComboBox1.Text = DT.Value.ToString("dd/MM/yyyy")
'                                Else
'                                    Me.DT.Value = D
'                                End If
'                            End If
'                            Me.RealData = d.ToString("dd/MM/yyyy")
'                            Me.IsValid = True
'                        Catch
'                            Try
'Dim d As Date = Date.ParseExact(Me.ComboBox1.Text, "dd/M/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
'                                If IsNothing(Me.DT) = False Then
'                                    If Me.DT.Value = D Then
'                                        Me.ComboBox1.Text = DT.Value.ToString("dd/MM/yyyy")
'                                    Else
'                                        Me.DT.Value = D
'                                    End If
'                                End If
'                                Me.RealData = d.ToString("dd/MM/yyyy")
'                                Me.IsValid = True
'                            Catch
'                                Try
'Dim d As Date = Date.ParseExact(Me.ComboBox1.Text, "d/MM/yy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
'                                    If IsNothing(Me.DT) = False Then
'                                        If Me.DT.Value = D Then
'                                            Me.ComboBox1.Text = DT.Value.ToString("dd/MM/yyyy")
'                                        Else
'                                            Me.DT.Value = D
'                                        End If
'                                    End If
'                                    Me.RealData = d.ToString("dd/MM/yyyy")
'                                    Me.IsValid = True
'                                Catch
'                                    Try
'Dim d As Date = Date.ParseExact(Me.ComboBox1.Text, "dd/MM/yy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
'                                        If IsNothing(Me.DT) = False Then
'                                            If Me.DT.Value = D Then
'                                                Me.ComboBox1.Text = DT.Value.ToString("dd/MM/yyyy")
'                                            Else
'                                                Me.DT.Value = D
'                                            End If
'                                        End If
'                                        Me.RealData = d.ToString("dd/MM/yyyy")
'                                        Me.IsValid = True
'                                    Catch
'                                        Try
'Dim d As Date = Date.ParseExact(Me.ComboBox1.Text, "dd/MM/y", System.Globalization.DateTimeFormatInfo.InvariantInfo)
'                                            If IsNothing(Me.DT) = False Then
'                                                If Me.DT.Value = D Then
'                                                    Me.ComboBox1.Text = DT.Value.ToString("dd/MM/yyyy")
'                                                Else
'                                                    Me.DT.Value = D
'                                                End If
'                                            End If
'                                            Me.RealData = d.ToString("dd/MM/yyyy")
'                                            Me.IsValid = True
'                                        Catch
'                                            Try
'Dim d As Date = Date.ParseExact(Me.ComboBox1.Text, "d/MM/y", System.Globalization.DateTimeFormatInfo.InvariantInfo)
'                                                If IsNothing(Me.DT) = False Then
'                                                    If Me.DT.Value = D Then
'                                                        Me.ComboBox1.Text = DT.Value.ToString("dd/MM/yyyy")
'                                                    Else
'                                                        Me.DT.Value = D
'                                                    End If
'                                                End If
'                                                Me.RealData = d.ToString("dd/MM/yyyy")
'                                                Me.IsValid = True
'                                            Catch
'                                                Try
'Dim d As Date = Date.ParseExact(Me.ComboBox1.Text, "dd/M/y", System.Globalization.DateTimeFormatInfo.InvariantInfo)
'                                                    If IsNothing(Me.DT) = False Then
'                                                        If Me.DT.Value = D Then
'                                                            Me.ComboBox1.Text = DT.Value.ToString("dd/MM/yyyy")
'                                                        Else
'                                                            Me.DT.Value = D
'                                                        End If
'                                                    End If
'                                                    Me.RealData = d.ToString("dd/MM/yyyy")
'                                                    Me.IsValid = True
'                                                Catch
'                                                    Try
'Dim d As Date = Date.ParseExact(Me.ComboBox1.Text, "d/M/y", System.Globalization.DateTimeFormatInfo.InvariantInfo)
'                                                        If IsNothing(Me.DT) = False Then
'                                                            If Me.DT.Value = D Then
'                                                                Me.ComboBox1.Text = DT.Value.ToString("dd/MM/yyyy")
'                                                            Else
'                                                                Me.DT.Value = D
'                                                            End If
'                                                        End If
'                                                        Me.RealData = d.ToString("dd/MM/yyyy")
'                                                        Me.IsValid = True
'                                                    Catch
'                                                        Try
'                                                            If IsNothing(Me.DT) = False Then Me.DT.Value = Date.ParseExact(Now, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
'                                                        Catch
'                                                        End Try
'                                                        Me.IsValid = True
'                                                        If Me.Mode = Modes.Search Then
'                                                        Else
'                                                            Me.ComboBox1.Text = Me.RealData
'                                                        End If
'                                                    End Try
'                                                End Try
'                                            End Try
'                                        End Try
'                                    End Try
'                                End Try
'                            End Try
'                        End Try
