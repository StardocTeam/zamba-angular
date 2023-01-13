Imports ZAMBA.AppBlock
Imports ZAMBA.Core
'Imports Zamba.Data
Public Class txtLineIndexCtrl
    Inherits txtBaseIndexCtrl

#Region "Constructores"
    Private Mode As Modes
    Public Sub New(ByVal docindex As IIndex, ByVal data2 As Boolean, ByVal Mode As Modes)
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()
        Me.Mode = Mode
        Me.Index = docindex
        Me.FlagData2 = data2
        Me.Init()
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
    '  Public _IsValid As Boolean = True
    Public Overrides Property IsValid() As Boolean
        Get
            If _IsValid = False Then
                Me.DataChangeProcedure(Me.ComboBox1, New System.ComponentModel.CancelEventArgs)
                'Me.ErrorProvider1.SetError(Me.ComboBox1, "")
            End If
            Return _IsValid
        End Get
        Set(ByVal Value As Boolean)
            _IsValid = Value
            If Value = True Then
                Me.ErrorProvider1.SetError(Me.ComboBox1, String.Empty)
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
            Me.RemoveHandlers()

            Index.DataTemp = Index.Data
            Index.DataTemp2 = Index.Data2
            Index.dataDescriptionTemp = Index.dataDescription
            Index.dataDescriptionTemp2 = Index.dataDescription2

            Me.Panel1.Visible = False
                        Me.ComboBox1.Dock = Windows.Forms.DockStyle.Fill
                        Me.ComboBox1.MaxLength = Me.Index.Len
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
            Me.ComboBox1.Text = Me.Data
            Me.IsValid = True
            AddHandlers()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Public Overrides Sub RefreshControlDataTemp(ByRef index As IIndex)
        Me.ComboBox1.Text = index.DataTemp
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
    'Public Sub RefreshControl(ByVal Data As String)
    '    Try
    '        RemoveHandlers()
    '        Me.RealData = Data
    '        If Me.Index.DropDown = IndexAdditionalType.AutoSustitución Then
    '            If IsNumeric(Me.RealData) AndAlso Me.RealData.IndexOf("-") = -1 Then
    '                Dim r() As dsSubstitucion.dsSubstitucionRow = Me.AutoSubstitucionTable.Select("Codigo=" & CInt(Me.RealData))
    '                If r.Length > 0 Then
    '                    'el codigo pertenece a la lista
    '                    Me.ComboBox1.Text = r(0).Descripcion
    '                Else
    '                    Me.ComboBox1.Text = Me.RealData
    '                End If
    '            Else
    '                Me.ComboBox1.Text = Me.RealData
    '            End If
    '        Else
    '            If Me.Index.Type = IndexDataType.Fecha OrElse Me.Index.Type = IndexDataType.Fecha_Hora Then
    '                Try
    '                    If IsNothing(Me.DT) = False Then Me.DT.Value = CDate(Me.RealData)
    '                Catch
    '                End Try
    '            End If
    '            Me.ComboBox1.Text = Me.RealData
    '        End If
    '        AddHandlers()
    '    Catch ex As Exception
    '       zamba.core.zclass.raiseerror(ex)
    '    End Try
    'End Sub

    Public Shadows Event IndexChanged()
    Private Property RealData() As String
        Get
            If FlagData2 Then
                Return Me.Index.DataTemp2
            Else
                Return Me.Index.DataTemp
            End If
        End Get
        Set(ByVal Value As String)

            Dim FlagCambioElContenido As Boolean = False

            If FlagData2 Then
                If IsNothing(Me.Index.DataTemp2) OrElse String.Compare(Me.Index.DataTemp2.Trim, Value.Trim) <> 0 Then FlagCambioElContenido = True
                Me.Index.DataTemp2 = Value.Trim
            Else
                If IsNothing(Me.Index.DataTemp) OrElse String.Compare(Me.Index.DataTemp.Trim, Value.Trim) <> 0 Then FlagCambioElContenido = True
                Me.Index.DataTemp = Value.Trim
            End If
            If FlagCambioElContenido = True Then RaiseEvent IndexChanged()
        End Set
    End Property
    Private ReadOnly Property Data() As String
        Get
            If FlagData2 Then
                Return Me.Index.Data2
            Else
                Return Me.Index.Data
            End If
        End Get
    End Property
    Private Property RealDataDescription() As String
        Get
            If FlagData2 Then
                Return Me.Index.dataDescriptionTemp2
            Else
                Return Me.Index.dataDescriptionTemp
            End If
        End Get
        Set(ByVal Value As String)
            If FlagData2 Then
                Me.Index.dataDescriptionTemp2 = Value
            Else
                Me.Index.dataDescriptionTemp = Value
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

    Private Sub ComboBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Dim FlagLastCharSelected As Boolean = False
            If Me.ComboBox1.SelectionStart = Len(Me.ComboBox1.Text) Then
                FlagLastCharSelected = True
            End If

            Me.IsValid = False
            RemoveHandlers()
            If Me.ComboBox1.Text.CompareTo(String.Empty) = 0 Then Me.ErrorProvider1.SetError(Me.ComboBox1, String.Empty)
            If (Me.Index.Type = IndexDataType.Numerico OrElse Me.Index.Type = IndexDataType.Numerico_Largo OrElse Me.Index.Type = IndexDataType.Moneda OrElse Me.Index.Type = IndexDataType.Numerico_Decimales) Then 'AndAlso Me.Index.DropDown <> IndexAdditionalType.AutoSustitución Then
                Try
                    'TODO: VER DE HACER TODOS ESTOS REPLACES DESPUES PARA QUE EL 

                    If Me.ComboBox1.Text.Length > 0 AndAlso Me.ComboBox1.Text.Substring(0, 1).CompareTo(".") = 0 Then
                        Dim Pos As Int32 = Me.ComboBox1.SelectionStart
                        If Pos < 0 Then Pos = Me.ComboBox1.Text.Length
                        Me.ComboBox1.Text = Me.ComboBox1.Text.Substring(1)
                        If pos >= 1 Then Me.ComboBox1.SelectionStart = Pos - 1
                    End If

                    If Me.ComboBox1.Text.IndexOf(",") >= 0 OrElse Me.ComboBox1.Text.IndexOf("..") >= 0 Then
                        Dim Pos As Int32 = Me.ComboBox1.SelectionStart
                        If Pos < 0 Then Pos = Me.ComboBox1.Text.Length
                        Me.ComboBox1.Text = Me.ComboBox1.Text.Replace(",", ".")
                        Me.ComboBox1.Text = Me.ComboBox1.Text.Replace("..", ".")
                        Me.ComboBox1.SelectionStart = Pos - 1

                        If Me.ComboBox1.Text.Substring(0, 1).CompareTo(".") = 0 Then
                            Dim Pos2 As Int32 = Me.ComboBox1.SelectionStart
                            If Pos2 < 0 Then Pos2 = Me.ComboBox1.Text.Length
                            Me.ComboBox1.Text = Me.ComboBox1.Text.Substring(1)
                            If Pos2 >= 1 Then Me.ComboBox1.SelectionStart = Pos2 - 1
                        End If

                    End If

                    'If Mid(ComboBox1.Text, 1, 1) = "-" Then
                    If Me.ComboBox1.Text.Length > 1 AndAlso ComboBox1.Text.Substring(1, 1).CompareTo("-") = 0 Then
                        Me.ComboBox1.Text = Me.ComboBox1.Text.Replace("-", String.Empty)
                        Me.ComboBox1.Text = "-" & Me.ComboBox1.Text
                    Else
                        Me.ComboBox1.Text = Me.ComboBox1.Text.Replace("-", String.Empty)
                    End If

                    'If Mid(ComboBox1.Text, 1, 1) = "+" Then
                    If Me.ComboBox1.Text.Length > 1 AndAlso ComboBox1.Text.Substring(1, 1).CompareTo("+") = 0 Then
                        Me.ComboBox1.Text = Me.ComboBox1.Text.Replace("+", String.Empty)
                        Me.ComboBox1.Text = "+" & Me.ComboBox1.Text
                    Else
                        Me.ComboBox1.Text = Me.ComboBox1.Text.Replace("+", String.Empty)
                    End If



                    If FlagLastCharSelected = True Then Me.ComboBox1.Select(Len(ComboBox1.Text), 1)

                    If ComboBox1.Text.IndexOf(".") < ComboBox1.Text.LastIndexOf(".") Then
                        Dim Pos2 As Int32 = Me.ComboBox1.SelectionStart
                        If Pos2 < 0 Then Pos2 = Me.ComboBox1.Text.Length
                        ComboBox1.Text = ComboBox1.Tag.ToString
                        If Pos2 >= 1 Then Me.ComboBox1.SelectionStart = Pos2 - 1
                    End If

                    If IsNumeric(Me.ComboBox1.Text) = False Then

                        'If Val(Me.ComboBox1.Text).ToString = "0" Then
                        If String.Compare(Me.ComboBox1.Text, "0") = 0 Then
                            Me.ComboBox1.Text = String.Empty
                        ElseIf Me.ComboBox1.Text.Length > 0 Then
                            Me.ComboBox1.Text = Val(Me.ComboBox1.Text).ToString
                            ComboBox1_TextChanged(Me, New EventArgs)
                        End If

                        If Me.Index.DropDown = IndexAdditionalType.AutoSustitución Then
                            Me.ComboBox1.Text = "0"
                        End If
                        'Deberia reemplazar todas las letras no solo la ultima
                        Try
                            'TODO NO SE PUEDE HACER LO SIGUIENTE PORQUE EL USUARIO PUEDE ESTAR
                            'ESCRIBIENDO EN CUALQUIER POSICION DEL COMBO
                            'HABRIA QUE HACER UN REPLACE O UN UNDO
                            'Me.ComboBox1.Text = Me.ComboBox1.Text.Substring(0, Me.ComboBox1.Text.Length - 1)
                        Catch ex As IndexOutOfRangeException
                        Catch
                        End Try
                        Me.ComboBox1.SelectionStart = Me.ComboBox1.Text.Length
                        Me.ComboBox1.SelectionLength = 0
                    End If
                    If Me.Index.DropDown = IndexAdditionalType.AutoSustitución Then
                        'TODO POR AHORA LO SACAMOS PARA QUE A PARTIR DEL ID ENCUENTRE EL VALUE
                        'Me.ComboBox1.Text = ""
                    End If
                Catch ex As IndexOutOfRangeException
                Catch ex As Exception
                    zamba.core.zclass.raiseerror(ex)
                End Try
            ElseIf Me.Index.Type = IndexDataType.Fecha OrElse Me.Index.Type = IndexDataType.Fecha_Hora Then
                Try
                    If Me.ComboBox1.Text.Length > 0 Then
                        Dim C As Char = Me.ComboBox1.Text.Chars(Me.ComboBox1.Text.Length - 1)
                        If Char.IsLetter(C) Then
                            Me.ComboBox1.Text = Me.ComboBox1.Text.Substring(0, Me.ComboBox1.Text.Length - 1)
                            Me.ComboBox1.SelectionStart = Me.ComboBox1.Text.Length
                            Me.ComboBox1.SelectionLength = 0
                        End If
                    Else
                        Me.RealData = String.Empty
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
            If String.IsNullOrEmpty(Me.ComboBox1.Text) _
            AndAlso String.Compare(Me.RealData, Me.ComboBox1.Text) <> 0 _
            AndAlso Me.Index.Type <> IndexDataType.Fecha _
            AndAlso Me.Index.Type <> IndexDataType.Fecha_Hora Then
                Me.RealData = Me.ComboBox1.Text
                Me.IsValid = True
                Exit Sub
            ElseIf String.IsNullOrEmpty(Me.ComboBox1.Text) Then
                Me.IsValid = True
                Exit Sub
            ElseIf (Me.Index.Type = IndexDataType.Numerico OrElse Me.Index.Type = IndexDataType.Numerico_Largo) _
            AndAlso String.Compare(Me.RealData, Me.ComboBox1.Text) <> 0 Then
                Try
                    Me.ComboBox1.Text = Me.ComboBox1.Text.Replace(",", ".")
                    Me.ComboBox1.Text = Me.ComboBox1.Text.Replace("..", ".")

                    If ComboBox1.Text.IndexOf("-") <> -1 AndAlso Mid(ComboBox1.Text, 1, 1) <> "-" Then Me.ComboBox1.Text.Replace("-", "")

                    Decimal.Parse(Me.ComboBox1.Text)
                    Me.RealData = Me.ComboBox1.Text
                    Me.IsValid = True
                    Exit Sub
                Catch ex As Exception
                    '                    Me.ComboBox1.Text = Me.RealData
                    Me.ErrorProvider1.SetError(Me.ComboBox1, "El valor ingresado no es numerico")
                    '                   Me.IsValid = True
                End Try
            ElseIf (Me.Index.Type = IndexDataType.Moneda OrElse Me.Index.Type = IndexDataType.Numerico_Decimales) _
            AndAlso String.Compare(Me.RealData, Me.ComboBox1.Text) <> 0 Then
                Try
                    Me.ComboBox1.Text.Replace(",", ".")
                    Me.ComboBox1.Text.Replace("..", ".")
                    ' Dim n As Decimal = CDec(Me.ComboBox1.Text)
                    Me.RealData = Me.ComboBox1.Text
                    Me.IsValid = True
                    Exit Sub
                Catch ex As Exception
                    '                 Me.ComboBox1.Text = Me.RealData
                    Me.ErrorProvider1.SetError(Me.ComboBox1, "El valor ingresado no es numerico")
                    '                  Me.IsValid = True
                End Try
            ElseIf (Me.Index.Type = IndexDataType.Alfanumerico OrElse Me.Index.Type = IndexDataType.Alfanumerico_Largo) _
            AndAlso String.Compare(Me.RealData, Me.ComboBox1.Text) <> 0 Then
                Me.RealData = Me.ComboBox1.Text
                Me.IsValid = True
                Exit Sub
            End If
        Catch ex As FormatException
            If Me.Mode = Modes.Search Then
                Me.ErrorProvider1.SetError(Me.ComboBox1, "La valor ingresado es invalido")
                'Muestro el icono de error
            Else
                Me.ComboBox1.Text = Me.RealData
                Me.IsValid = True
                Exit Sub
            End If
            zamba.core.zclass.raiseerror(ex)
        Catch ex As ArgumentNullException
            If Me.Mode = Modes.Search Then
                Me.ErrorProvider1.SetError(Me.ComboBox1, "La valor ingresado es invalido")
                'Muestro el icono de error
            Else
                Me.ComboBox1.Text = Me.RealData
                Me.IsValid = True
                Exit Sub
            End If
            zamba.core.zclass.raiseerror(ex)
        Catch ex As InvalidCastException
            If Me.Mode = Modes.Search Then
                Me.ErrorProvider1.SetError(Me.ComboBox1, "La valor ingresado es invalido")
                'Muestro el icono de error
            Else
                Me.ComboBox1.Text = Me.RealData
                Me.IsValid = True
                Exit Sub
            End If
            zamba.core.zclass.raiseerror(ex)
        Catch ex As OutOfMemoryException
            If Me.Mode = Modes.Search Then
                Me.ErrorProvider1.SetError(Me.ComboBox1, "La valor ingresado es invalido")
                'Muestro el icono de error
            Else
                Me.ComboBox1.Text = Me.RealData
                Me.IsValid = True
                Exit Sub
            End If
            zamba.core.zclass.raiseerror(ex)
        Catch ex As OverflowException
            If Me.Mode = Modes.Search Then
                Me.ErrorProvider1.SetError(Me.ComboBox1, "La valor ingresado es invalido")
                'Muestro el icono de error
            Else
                Me.ComboBox1.Text = Me.RealData
                Me.IsValid = True
                Exit Sub
            End If
            zamba.core.zclass.raiseerror(ex)
        Catch ex As Exception
            If Me.Mode = Modes.Search Then
                Me.ErrorProvider1.SetError(Me.ComboBox1, "La valor ingresado es invalido")
                'Muestro el icono de error
            Else
                Me.ComboBox1.Text = Me.RealData
                Me.IsValid = True
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
            Me.IsValid = True
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
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents ComboBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As ZPanel
    '   Friend WithEvents Button1 As ZButton
    '    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ComboBox1 = New System.Windows.Forms.TextBox
        Me.Panel1 = New Zamba.AppBlock.ZPanel
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider
        Me.SuspendLayout()
        '
        'ComboBox1
        '
        Me.ComboBox1.BackColor = System.Drawing.Color.White
        Me.ComboBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ComboBox1.ForeColor = System.Drawing.Color.Black
        Me.ErrorProvider1.SetIconPadding(Me.ComboBox1, -40)
        Me.ComboBox1.Location = New System.Drawing.Point(0, 0)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(504, 21)
        Me.ComboBox1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Silver
        Me.Panel1.CausesValidation = False
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.ForeColor = System.Drawing.Color.Black
        Me.Panel1.Location = New System.Drawing.Point(504, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(24, 20)
        Me.Panel1.TabIndex = 1
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'txtIndexCtrl
        '
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "txtLineIndexCtrl"
        Me.Size = New System.Drawing.Size(528, 20)
        Me.ResumeLayout(False)

    End Sub
#End Region

#Region "Cambio de color al Editar el combo"

    Private Sub ComboBox1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.GotFocus
        Me.ComboBox1.BackColor = Color.FromArgb(255, 224, 192)
    End Sub
    Private Sub ComboBox1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.LostFocus
        RemoveHandlers()
        Me.ComboBox1.BackColor = Color.White
        If Me.IsValid = False Then Me.DataChangeProcedure(Me.ComboBox1, New System.ComponentModel.CancelEventArgs)
        'If Me.IsValid = False Then Me.ErrorProvider1.SetError(Me.ComboBox1, "")
        AddHandlers()
    End Sub

#End Region

#Region "Eventos de Enter y Tab"
    Public Shadows Event EnterPressed()
    Public Shadows Event TabPressed()
    Private Sub txtIndexCtrl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Enter Then
            Me.DataChangeProcedure(Me.ComboBox1, New System.ComponentModel.CancelEventArgs)
            RaiseEvent EnterPressed()
        End If
        If e.KeyCode = Keys.Tab Then
            RaiseEvent TabPressed()
        End If
    End Sub

    Private Sub txtIndexCtrl_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        If e.KeyChar = Chr(13) Then
            Me.DataChangeProcedure(Me.ComboBox1, New System.ComponentModel.CancelEventArgs)
            RaiseEvent EnterPressed()
        End If
        If e.KeyChar = Chr(9) Then
            RaiseEvent TabPressed()
        End If

    End Sub

    Private Sub ComboBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Me.DataChangeProcedure(Me.ComboBox1, New System.ComponentModel.CancelEventArgs)
            RaiseEvent EnterPressed()
        End If
        If e.KeyCode = Keys.Tab Then
            RaiseEvent TabPressed()
        End If
    End Sub

#End Region

#Region "Multiplecharacter TextBox"
    Private Sub ComboBox1_DoubleCLick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.DoubleClick
        If Me.Index.Type = IndexDataType.Alfanumerico OrElse Me.Index.Type = IndexDataType.Alfanumerico_Largo Then
            Dim frm As Form1 = Nothing
            If Me.Index.DataTemp.Trim() = String.Empty Then
                frm = New Form1(Me.Index.Data, Me.Index.Name)
            Else
                frm = New Form1(Me.Index.DataTemp, Me.Index.Name)
            End If



            frm.ShowDialog()
            Me.ComboBox1.Text = frm.TextBox1.Text
            Me.ComboBox1.Select()
            Me.ComboBox1.Focus()
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
