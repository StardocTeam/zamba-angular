Imports ZAMBA.AppBlock
Imports ZAMBA.Core
Public Class txtDropDownIndexCtrl
    Inherits txtBaseIndexCtrl

#Region "Constructores"
    Private Mode As Modes
    Private _docTypeID As Int32 = 0
    Private _parentIndexs As Hashtable = Nothing

    Public Sub New(ByRef docindex As IIndex, ByVal data2 As Boolean, ByVal Mode As Modes, ByVal DocTypeId As Int32, ByVal ParentIndexs As Hashtable)
        MyBase.New()

        _docTypeID = DocTypeId
        _parentIndexs = ParentIndexs

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()



        'Agregar cualquier inicialización después de la llamada a InitializeComponent()
        Me.Mode = Mode
        Me.Index = docindex
        Me.FlagData2 = data2
        Me.Init()
        RefreshControl(Index)


    End Sub

    Public Sub New(ByRef docindex As IIndex, ByVal data2 As Boolean, ByVal Mode As Modes)
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()
        Me.Mode = Mode
        Me.Index = docindex
        Me.FlagData2 = data2
        Me.Init()
        RefreshControl(Index)

        If UserPreferences.getValue("AllowInputTextInDropDownAndSustitutionIndexs", Sections.UserPreferences, "True") = False Then
            Me.ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
        End If
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
                'Me.ErrorProvider1.SetError(Me.ComboBox1, String.Empty)
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


    Private DropDowndata As New ArrayList
    'Este flag me dice si trabajo con data1 o data2
    Private FlagData2 As Boolean
    'Picker para las fechas

    Private Sub Init()
        'Inicializa el Control para el tipo del indice Fecha
        Try
            'Me.SuspendLayout()
            Me.RemoveHandlers()

            Index.DataTemp = Index.Data
            Index.DataTemp2 = Index.Data2
            Index.dataDescriptionTemp = Index.dataDescription
            Index.dataDescriptionTemp2 = Index.dataDescription2

            'Para dropdown
            Me.Panel1.Visible = False
            Me.ComboBox1.DropDownStyle = Windows.Forms.ComboBoxStyle.DropDown
            Me.DropDowndata.Clear()
            Me.DropDowndata.Add(String.Empty)

            Dim IndexId As Int32 = 0
            If Int32.TryParse(Me.Index.ID.ToString, IndexId) = True Then
                ' Agrega una lista de sustitución al comboBox...

                Me.DropDowndata.Clear()

                If _parentIndexs Is Nothing Then
                    Me.DropDowndata.AddRange(IndexsBusiness.GetDropDownList(IndexId))
                Else
                    'Me.DropDowndata.AddRange(IndexsBusiness.GetDropDownListHierachical(_docTypeID, IndexId, _parentIndexs))
                    'Me.Index.Data = ""
                    'Me.Index.DataTemp = ""
                End If

            End If
            


            Me.ComboBox1.Items.Clear()
            'Try
            Me.ComboBox1.Items.AddRange(Me.DropDowndata.ToArray)
            'Catch ex As System.OutOfMemoryException
            '	zamba.core.zclass.raiseerror(ex)
            'Catch ex As Exception
            '	zamba.core.zclass.raiseerror(ex)
            'End Try
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
            'If Me.Index.DropDown <> IndexAdditionalType.AutoSustituciónJerarquico And Me.Index.DropDown <> IndexAdditionalType.DropDownJerarquico Then
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
        Me.RemoveHandlers()
        RefreshControl(Index)
        Me.AddHandlers()
    End Sub
    Public Overrides Sub Commit()
        If IsNothing(Index.DataTemp) = False Then
            Index.Data = Index.DataTemp
            Index.dataDescription = Index.dataDescriptionTemp
            Me.RemoveHandlers()
            RefreshControl(Index)
            Me.AddHandlers()
        End If
        If IsNothing(Index.DataTemp2) = False Then
            Index.Data2 = Index.DataTemp2
            Index.dataDescription2 = Index.dataDescriptionTemp2
            Me.RemoveHandlers()
            RefreshControl(Index)
            Me.AddHandlers()
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
    Public Shadows Event ItemChanged(ByVal IndexID As Integer, ByVal NewValue As String)

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
                If IsNothing(Me.Index.DataTemp2) OrElse Me.Index.DataTemp2.Trim <> Value.Trim Then FlagCambioElContenido = True
                Me.Index.DataTemp2 = Value.Trim
            Else
                If IsNothing(Me.Index.DataTemp) OrElse Me.Index.DataTemp.Trim <> Value.Trim Then FlagCambioElContenido = True
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

            RemoveHandler ComboBox1.SelectedIndexChanged, AddressOf Me.ComboBox1_TextChanged
            AddHandler ComboBox1.SelectedIndexChanged, AddressOf Me.ComboBox1_TextChanged

            RemoveHandler ComboBox1.LostFocus, AddressOf Me.ComboBox1_LostFocus
            AddHandler ComboBox1.LostFocus, AddressOf Me.ComboBox1_LostFocus

        Catch
        End Try
    End Sub
    Private Sub RemoveHandlers()
        Try
            RemoveHandler ComboBox1.Validating, AddressOf DataChangeProcedure
            RemoveHandler ComboBox1.TextChanged, AddressOf ComboBox1_TextChanged
            RemoveHandler ComboBox1.SelectedIndexChanged, AddressOf Me.DropDownProcedure
            RemoveHandler ComboBox1.LostFocus, AddressOf Me.ComboBox1_LostFocus
        Catch
        End Try
    End Sub

    Private Sub ComboBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim FlagLastCharSelected As Boolean = False
            If Me.ComboBox1.SelectionStart = Me.ComboBox1.Text.Length Then
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
                        If Pos >= 1 Then Me.ComboBox1.SelectionStart = Pos - 1
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
                        'Try
                        '    TODO NO SE PUEDE HACER LO SIGUIENTE PORQUE EL USUARIO PUEDE ESTAR
                        '    ESCRIBIENDO EN CUALQUIER POSICION DEL COMBO
                        '    HABRIA QUE HACER UN REPLACE O UN UNDO
                        '    Me.ComboBox1.Text = Me.ComboBox1.Text.Substring(0, Me.ComboBox1.Text.Length - 1)
                        'Catch ex As IndexOutOfRangeException
                        'Catch
                        'End Try
                        Me.ComboBox1.SelectionStart = Me.ComboBox1.Text.Length
                        Me.ComboBox1.SelectionLength = 0
                    End If
                    'If Me.Index.DropDown = IndexAdditionalType.AutoSustitución Then
                    '    'TODO POR AHORA LO SACAMOS PARA QUE A PARTIR DEL ID ENCUENTRE EL VALUE
                    '    'Me.ComboBox1.Text = String.Empty
                    'End If
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

            'If Not String.IsNullOrEmpty(Me.ComboBox1.Text) Then
            DataChangeProcedure(sender, New System.ComponentModel.CancelEventArgs)
            RaiseEvent ItemChanged(Index.ID, Me.ComboBox1.Text)
            'End If

        Catch ex As IndexOutOfRangeException
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        Finally
            AddHandlers()
            ComboBox1.Tag = ComboBox1.Text
        End Try
    End Sub

    Private Sub DropDownProcedure(ByVal sender As Object, ByVal ev As EventArgs)
        Try
            '            Me.IsValid = False
            Me.ComboBox1.Select()
            Me.ComboBox1.SelectionLength = 0  ' Me.ComboBox1.Text.Length - 1
            Me.ComboBox1.SelectionStart = 0
        Catch
        End Try
    End Sub

    Private Sub DataChangeProcedure(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Try
            RemoveHandlers()

            If Me.ComboBox1.Text = String.Empty AndAlso Me.RealData <> Me.ComboBox1.Text AndAlso Me.Index.Type <> IndexDataType.Fecha AndAlso Me.Index.Type <> IndexDataType.Fecha_Hora Then
                Me.RealData = Me.ComboBox1.Text
                Me.IsValid = True
                Exit Sub
            ElseIf Me.ComboBox1.Text = "" Then
                Me.IsValid = True
                Exit Sub
            ElseIf Me.Index.DropDown = IndexAdditionalType.DropDown OrElse Me.Index.DropDown = IndexAdditionalType.DropDownJerarquico AndAlso Me.RealData <> Me.ComboBox1.Text Then
                'Verifico que el valor pertenezca a la lista

                If Me.DropDowndata.Contains(Me.ComboBox1.Text.Trim) Then
                    'la descripcion pertenece a la lista
                    Me.RealData = Me.ComboBox1.Text
                    Me.IsValid = True
                    Exit Sub
                Else
                    'la descripcion no pertenece a la lista
                    If UserPreferences.getValue("AllowIndexValueOutofList", Sections.InsertPreferences, True) = False AndAlso Me.Mode <> Modes.Search Then
                        'el codigo no pertenece a ala lista y no estan permitidos codigos fuera de la lista
                        Me.ErrorProvider1.SetError(Me.ComboBox1, "El texto ingresado no pertenece a la lista")
                        Me.IsValid = False
                    Else
                        'Me.RealData = Me.ComboBox1.Text
                        Me.IsValid = True
                        Exit Sub
                    End If
                    'Tomas: Se permite guardar el valor aunque se le avise al usuario de que existe un error (wi:6725)
                    Me.RealData = Me.ComboBox1.Text
                End If
            Else
                Me.ErrorProvider1.Clear()
                Me.IsValid = True
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
            Zamba.Core.ZClass.raiseerror(ex)
        Catch ex As ArgumentNullException
            If Me.Mode = Modes.Search Then
                Me.ErrorProvider1.SetError(Me.ComboBox1, "La valor ingresado es invalido")
                'Muestro el icono de error
            Else
                Me.ComboBox1.Text = Me.RealData
                Me.IsValid = True
                Exit Sub
            End If
            Zamba.Core.ZClass.raiseerror(ex)
        Catch ex As InvalidCastException
            If Me.Mode = Modes.Search Then
                Me.ErrorProvider1.SetError(Me.ComboBox1, "La valor ingresado es invalido")
                'Muestro el icono de error
            Else
                Me.ComboBox1.Text = Me.RealData
                Me.IsValid = True
                Exit Sub
            End If
            Zamba.Core.ZClass.raiseerror(ex)
        Catch ex As OutOfMemoryException
            If Me.Mode = Modes.Search Then
                Me.ErrorProvider1.SetError(Me.ComboBox1, "La valor ingresado es invalido")
                'Muestro el icono de error
            Else
                Me.ComboBox1.Text = Me.RealData
                Me.IsValid = True
                Exit Sub
            End If
            Zamba.Core.ZClass.raiseerror(ex)
        Catch ex As OverflowException
            If Me.Mode = Modes.Search Then
                Me.ErrorProvider1.SetError(Me.ComboBox1, "La valor ingresado es invalido")
                'Muestro el icono de error
            Else
                Me.ComboBox1.Text = Me.RealData
                Me.IsValid = True
                Exit Sub
            End If
            Zamba.Core.ZClass.raiseerror(ex)
        Catch ex As Exception
            If Me.Mode = Modes.Search Then
                Me.ErrorProvider1.SetError(Me.ComboBox1, "La valor ingresado es invalido")
                'Muestro el icono de error
            Else
                Me.ComboBox1.Text = Me.RealData
                Me.IsValid = True
                Exit Sub
            End If
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
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
                If Panel1 IsNot Nothing Then
                    Panel1.Dispose()
                    Panel1 = Nothing
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
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Public WithEvents Panel1 As ZPanel
    Friend WithEvents Button1 As ZButton
    '    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.Panel1 = New Zamba.AppBlock.ZPanel
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider
        Me.SuspendLayout()
        '
        'ComboBox1
        '
        Me.ComboBox1.BackColor = System.Drawing.Color.White
        Me.ComboBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.ForeColor = System.Drawing.Color.Black
        Me.ErrorProvider1.SetIconPadding(Me.ComboBox1, -40)
        Me.ComboBox1.Location = New System.Drawing.Point(0, 0)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(504, 21)
        Me.ComboBox1.Sorted = True
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
        Me.Name = "txtDropDownIndexCtrl"
        Me.Size = New System.Drawing.Size(528, 20)
        Me.ResumeLayout(False)

    End Sub
#End Region

#Region "Cambio de color al Editar el combo"

    Private Sub ComboBox1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.GotFocus
        Me.ComboBox1.BackColor = Color.FromArgb(255, 224, 192)
    End Sub
    Private Sub ComboBox1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.LostFocus
        CheckDataChange()
    End Sub
    Private Sub ComboBox1_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.MouseLeave
        CheckDataChange()
    End Sub
    Private Sub CheckDataChange()
        RemoveHandlers()
        Me.ComboBox1.BackColor = Color.White
        If Me.IsValid = False Then Me.DataChangeProcedure(Me.ComboBox1, New System.ComponentModel.CancelEventArgs)
        'If Me.IsValid = False Then Me.ErrorProvider1.SetError(Me.ComboBox1, String.Empty)
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

    '#Region "Multiplecharacter TextBox"
    '    Private Sub ComboBox1_DoubleCLick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.DoubleClick
    '        If Me.Index.Type = IndexDataType.Alfanumerico OrElse Me.Index.Type = IndexDataType.Alfanumerico_Largo Then
    '            'Se guardan los cambios en datatemp
    '            Me.DataChangeProcedure(Me.ComboBox1, New System.ComponentModel.CancelEventArgs)

    '            'Se muestra el formulario multilinea
    '            Dim frm As frmMultiline = New frmMultiline(Me.Index.DataTemp, Me.Index.Name)
    '            frm.ShowDialog()

    '            'Se guarda el valor
    '            Me.ComboBox1.Text = frm.txtIndexValue.Text
    '            Me.Index.DataTemp = frm.txtIndexValue.Text
    '            Me.ComboBox1.Select()
    '            Me.ComboBox1.Focus()
    '            frm.Dispose()
    '        End If
    '    End Sub
    '#End Region

End Class


