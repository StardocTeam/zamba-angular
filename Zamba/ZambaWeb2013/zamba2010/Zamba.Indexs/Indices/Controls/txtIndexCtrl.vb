Imports ZAMBA.AppBlock
Imports Zamba.Core


Public Class txtIndexCtrl
    Inherits txtBaseIndexCtrl

#Region "Constructores"
    Private Mode As Modes
    Private _docTypeID As Int32 = 0
    Private _parentIndexs As Hashtable = Nothing

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
                If DT IsNot Nothing Then
                    DT.Dispose()
                    DT = Nothing
                End If
                If frmListaSubstitucion IsNot Nothing Then
                    frmListaSubstitucion.Dispose()
                    frmListaSubstitucion = Nothing
                End If
                If _AutoSubstitucionTable IsNot Nothing Then
                    _AutoSubstitucionTable.Dispose()
                    _AutoSubstitucionTable = Nothing
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
    Friend WithEvents Panel1 As ZPanel
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
        Me.Name = "txtIndexCtrl"
        Me.Size = New System.Drawing.Size(528, 20)
        Me.ResumeLayout(False)

    End Sub
#End Region

#Region "Constructores"



    Public Sub New(ByVal docindex As Index, ByVal data2 As Boolean, ByVal Mode As Modes, ByVal DocTypeID As Int32, ByVal ParentIndexs As Hashtable)
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        _docTypeID = DocTypeID
        _parentIndexs = ParentIndexs

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()
        Me.Mode = Mode
        Me.Index = docindex
        Me.FlagData2 = data2
        Me.Init()
        RefreshControl(Index)
    End Sub

    Public Sub New(ByVal docindex As Index, ByVal data2 As Boolean, ByVal Mode As Modes)
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
    '    Public Index As Index

    Private DropDowndata As New ArrayList
    'Este flag me dice si trabajo con data1 o data2
    Private FlagData2 As Boolean
    'Picker para las fechas
    Private WithEvents DT As Windows.Forms.DateTimePicker
    'Formulario para las listas de Substitucion
    Private frmListaSubstitucion As frmIndexSubtitutiom
    Private _AutoSubstitucionTable As DataTable
     

    Private ReadOnly Property AutoSustitucionTable() As DataTable
        Get
            If IsNothing(Me._AutoSubstitucionTable) Then Me._AutoSubstitucionTable = AutoSubstitutionBusiness.GetIndexData(Me.Index.ID, False)
            Return Me._AutoSubstitucionTable
        End Get
    End Property

    Private Sub Init()
        'Inicializa el Control para el tipo del indice Fecha
        Try
            'Me.SuspendLayout()
            Me.RemoveHandlers()

            Index.DataTemp = Index.Data
            Index.DataTemp2 = Index.Data2
            Index.dataDescriptionTemp = Index.dataDescription
            Index.dataDescriptionTemp2 = Index.dataDescription2

            If Me.Index.Type = IndexDataType.Fecha OrElse Me.Index.Type = IndexDataType.Fecha_Hora Then
                Me.ComboBox1.DropDownStyle = Windows.Forms.ComboBoxStyle.Simple
                Me.DT = New Windows.Forms.DateTimePicker
                Me.DT.CustomFormat = "dd/MM/yyyy"
                Try
                    Me.DT.TabStop = False
                    If IsNothing(Me.Data) = False AndAlso Me.Data.Length > 0 Then Me.DT.Value = Date.ParseExact(Me.Data, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                Catch ex As FormatException
                Catch ex As Exception
                    zamba.core.zclass.raiseerror(ex)
                End Try
                Me.Panel1.Controls.Add(DT)
                DT.Dock = Windows.Forms.DockStyle.Fill
                Me.ComboBox1.MaxLength = 10
            Else
                Select Case Me.Index.DropDown
                    'Para auto Substitucion
                    Case IndexAdditionalType.AutoSustitución
                        Me.ComboBox1.DropDownStyle = Windows.Forms.ComboBoxStyle.Simple
                        Me.Button1 = New Zamba.AppBlock.ZButton
                        Me.Button1.Text = "..."
                        Me.Panel1.Controls.Add(Me.Button1)
                        Me.Button1.Dock = Windows.Forms.DockStyle.Fill

                    Case IndexAdditionalType.AutoSustituciónJerarquico
                        'Para auto Substitucion jearquico
                        Me.ComboBox1.DropDownStyle = Windows.Forms.ComboBoxStyle.Simple
                        Me.Button1 = New Zamba.AppBlock.ZButton
                        Me.Button1.Text = "..."
                        Me.Panel1.Controls.Add(Me.Button1)
                        Me.Button1.Dock = Windows.Forms.DockStyle.Fill

                    Case IndexAdditionalType.DropDown
                        'Para dropdown
                        Me.Panel1.Visible = False
                        Me.ComboBox1.DropDownStyle = Windows.Forms.ComboBoxStyle.DropDown
                        Me.DropDowndata.Clear()
                        Me.DropDowndata.Add(String.Empty)
                        Me.DropDowndata.AddRange(IndexsBusiness.GetDropDownList(CInt(Me.Index.ID)))
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

                    Case IndexAdditionalType.DropDownJerarquico
                        'Para dropdown jearquico
                        Me.Panel1.Visible = False
                        Me.ComboBox1.DropDownStyle = Windows.Forms.ComboBoxStyle.DropDown
                        Me.DropDowndata.Clear()
                        If _parentIndexs Is Nothing Then
                            Me.DropDowndata.AddRange(IndexsBusiness.GetDropDownList(CInt(Me.Index.ID)))
                        Else
                            'Me.DropDowndata.AddRange(IndexsBusiness.GetDropDownListHierachical(_docTypeID, Me.Index.ID, _parentIndexs))
                        End If
                        Me.ComboBox1.Items.Clear()
                        Me.ComboBox1.Items.AddRange(Me.DropDowndata.ToArray)
                        Me.ComboBox1.Dock = Windows.Forms.DockStyle.Fill
                        Me.ComboBox1.MaxLength = Me.Index.Len
                        Me.DropDownProcedure(Nothing, System.EventArgs.Empty)
                    Case IndexAdditionalType.LineText
                        Me.ComboBox1.DropDownStyle = Windows.Forms.ComboBoxStyle.Simple
                        Me.Panel1.Visible = False
                        Me.ComboBox1.Dock = Windows.Forms.DockStyle.Fill
                        Me.ComboBox1.MaxLength = Me.Index.Len
                End Select
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
                Me.Index.DataTemp = index.Data
                Me.Index.DataTemp2 = index.Data2
                Me.Index.Data = index.Data
                Me.Index.dataDescriptionTemp = index.dataDescription
                Me.Index.dataDescriptionTemp2 = index.dataDescription2
            End If

            'If IsNothing(index) = False Then Me.Index = index
            If Me.Index.DropDown = IndexAdditionalType.AutoSustitución Then
                If IsNumeric(Me.Data) AndAlso Me.Data.IndexOf("-") = -1 Then


                    Dim r() As DataRow = Me.AutoSustitucionTable.Select("Codigo=" & CInt(Me.Data))


                    If r.Length > 0 Then
                        'el codigo pertenece a la lista

                        Me.ComboBox1.Text = r(0).Item(1).ToString()
                        Me.Index.dataDescriptionTemp = r(0).Item(1).ToString()
                        Me.Index.dataDescription = r(0).Item(1).ToString()

                        'Me.ComboBox1.Text = r(0).Descripcion
                        'Me.Index.dataDescriptionTemp = r(0).Descripcion
                        'Me.Index.dataDescription = r(0).Descripcion
                    Else
                        Me.ComboBox1.Text = Me.Data
                    End If
                Else
                    Me.ComboBox1.Text = Me.Data
                End If
            Else
                'CAMBIE REALDTA POR DATA
                Me.ComboBox1.Text = Me.Data
            End If
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
           

            Select Case Me.Index.Type
                Case IndexDataType.Fecha, IndexDataType.Fecha_Hora
                    'RemoveHandler DT.ValueChanged, AddressOf Me.DateProcedure
                    'AddHandler DT.ValueChanged, AddressOf Me.DateProcedure
                    'RemoveHandler DT.Validating, AddressOf Me.DateProcedure
                    'AddHandler DT.Validating, AddressOf Me.DateProcedure
                Case Else
                    Select Case Me.Index.DropDown
                        Case IndexAdditionalType.AutoSustitución, IndexAdditionalType.AutoSustituciónJerarquico
                            RemoveHandler Button1.Click, AddressOf Me.AutoSustitutionProcedure
                            AddHandler Button1.Click, AddressOf Me.AutoSustitutionProcedure
                        Case IndexAdditionalType.DropDown, IndexAdditionalType.DropDownJerarquico
                            RemoveHandler ComboBox1.SelectedIndexChanged, AddressOf Me.DropDownProcedure
                            AddHandler ComboBox1.SelectedIndexChanged, AddressOf Me.DropDownProcedure
                            RemoveHandler ComboBox1.SelectedValueChanged, AddressOf ComboBox1_SelectedValueChanged
                            AddHandler ComboBox1.SelectedValueChanged, AddressOf ComboBox1_SelectedValueChanged
                            'RemoveHandler ComboBox1.LostFocus, AddressOf ComboBox1_LostFocus
                            'AddHandler ComboBox1.LostFocus, AddressOf ComboBox1_LostFocus
                        Case IndexAdditionalType.LineText
                    End Select
            End Select
        Catch
        End Try
    End Sub
    Private Sub RemoveHandlers()
        Try
            RemoveHandler ComboBox1.Validating, AddressOf DataChangeProcedure
            RemoveHandler ComboBox1.TextChanged, AddressOf ComboBox1_TextChanged
            Select Case Me.Index.Type
                Case IndexDataType.Fecha, IndexDataType.Fecha_Hora
                    RemoveHandler DT.ValueChanged, AddressOf Me.DateProcedure
                    RemoveHandler DT.Validating, AddressOf Me.DateProcedure
                Case Else
                    Select Case Me.Index.DropDown
                        Case IndexAdditionalType.AutoSustitución, IndexAdditionalType.AutoSustituciónJerarquico
                            RemoveHandler Button1.Click, AddressOf Me.AutoSustitutionProcedure
                        Case IndexAdditionalType.DropDown, IndexAdditionalType.DropDownJerarquico
                            RemoveHandler ComboBox1.SelectedIndexChanged, AddressOf Me.DropDownProcedure
                            RemoveHandler ComboBox1.SelectedValueChanged, AddressOf ComboBox1_SelectedValueChanged
                            'RemoveHandler ComboBox1.LostFocus, AddressOf ComboBox1_LostFocus
                        Case IndexAdditionalType.LineText
                    End Select
            End Select
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
                        ComboBox1.Text = ComboBox1.Tag.ToString()
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
                        'Me.ComboBox1.Text = String.Empty
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

    Private Sub DropDownProcedure(ByVal sender As Object, ByVal ev As EventArgs)
        Try
            '            Me.IsValid = False
            Me.ComboBox1.Select()
            Me.ComboBox1.SelectionLength = 0  ' Me.ComboBox1.Text.Length - 1
            Me.ComboBox1.SelectionStart = 0
        Catch
        End Try
    End Sub
    Private Sub DateProcedure(ByVal sender As Object, ByVal ev As EventArgs)
        Try
            '     Me.IsValid = False
            RemoveHandlers()
            Me.ComboBox1.Text = DT.Value.ToShortDateString
            '     Me.ComboBox1.Text = DT.Value.ToString("dd/MM/yyyy")
            Me.ComboBox1.Select()
            AddHandlers()
        Catch
        End Try
    End Sub
    Private Sub DateProcedure(ByVal sender As Object, ByVal ev As System.ComponentModel.CancelEventArgs)
        Try
            ' Me.IsValid = False
            RemoveHandlers()
            Me.ComboBox1.Text = DT.Value.ToShortDateString
            '     Me.ComboBox1.Text = DT.Value.ToString("dd/MM/yyyy")
            Me.ComboBox1.Select()
            AddHandlers()
        Catch
        End Try
    End Sub
    Private Sub AutoSustitutionProcedure(ByVal sender As Object, ByVal ev As EventArgs)
        Try
            '  Me.IsValid = False
            RemoveHandlers()
            If Me.frmListaSubstitucion Is Nothing Then
                Me.frmListaSubstitucion = New frmIndexSubtitutiom(Me.Index.ID, Me.AutoSustitucionTable)
            End If
            Me.frmListaSubstitucion.ShowDialog()
            If Me.frmListaSubstitucion.DialogResult = DialogResult.OK Then
                Me.RealData = Me.frmListaSubstitucion.Codigo.ToString()
                Me.ComboBox1.Text = Me.frmListaSubstitucion.Descripcion
                Me.ComboBox1.SelectionStart = 0
                Me.ComboBox1.SelectionLength = 0   'Me.ComboBox1.Text.Length - 1
                RaiseEvent ItemChanged(Me.Index.ID, Me.RealData)
                'oscar
                Me.IsValid = False
            End If
            AddHandlers()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    'Public Sub s()
    '    Try
    '        '  Me.IsValid = False

    '        If Me.frmListaSubstitucion Is Nothing Then
    '            Me.frmListaSubstitucion = New frmIndexSubtitutiom(Me.Index.ID, Me.AutoSustitucionTable)
    '        End If
    '        Me.frmListaSubstitucion.ShowDialog()
    '        If Me.frmListaSubstitucion.DialogResult = DialogResult.OK Then
    '            Me.RealData = Me.frmListaSubstitucion.Codigo.ToString()
    '            Me.ComboBox1.Text = Me.frmListaSubstitucion.Descripcion
    '            Me.ComboBox1.SelectionStart = 0
    '            Me.ComboBox1.SelectionLength = 0   'Me.ComboBox1.Text.Length - 1
    '            'oscar
    '            Me.IsValid = False
    '        End If

    '    Catch ex As Exception
    '        Zamba.Core.ZClass.raiseerror(ex)
    '    End Try
    'End Sub

    Private Sub DataChangeProcedure(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Try
            RemoveHandlers()
            If Me.ComboBox1.Text = String.Empty AndAlso Me.RealData <> Me.ComboBox1.Text AndAlso Me.Index.Type <> IndexDataType.Fecha AndAlso Me.Index.Type <> IndexDataType.Fecha_Hora Then
                Me.RealData = Me.ComboBox1.Text
                If Me.Index.Type = IndexDataType.Fecha OrElse Me.Index.Type = IndexDataType.Fecha_Hora Then
                    Try
                        If IsNothing(Me.DT) = False Then
                            Me.DT.Value = Now
                        End If
                    Catch
                    End Try
                End If
                Me.IsValid = True
                Exit Sub
            ElseIf Me.ComboBox1.Text = String.Empty Then
                Me.IsValid = True
                Exit Sub
            ElseIf Me.Index.Type = IndexDataType.Fecha Then  'AndAlso Me.RealData <> Me.ComboBox1.Text
                Try
                    Me.ComboBox1.Text = Me.ComboBox1.Text.Replace("-", "/")
                    Me.ComboBox1.Text = Me.ComboBox1.Text.Replace(".", "/")
                    Me.ComboBox1.Text = Me.ComboBox1.Text.Replace(" ", "/")
                    Me.ComboBox1.SelectionStart = Me.ComboBox1.Text.Length
                    Me.ComboBox1.SelectionLength = 0

                    Dim d As Date = Date.Parse(Me.ComboBox1.Text, System.Globalization.DateTimeFormatInfo.CurrentInfo)
                    If IsNothing(Me.DT) = False Then
                        If Me.DT.Value = d Then
                            Me.ComboBox1.Text = DT.Value.ToShortDateString
                        Else
                            Me.DT.Value = d
                        End If
                    End If
                    'TODO: Verificar si le voy a mandar la fecha con la cultura actual o se lo mando con mi formato
                    Me.RealData = d.ToString("dd/MM/yyyy")
                    'Me.RealData = d.ToString(System.Globalization.DateTimeFormatInfo.CurrentInfo)
                    Me.IsValid = True
                    Exit Sub
                Catch ex As System.FormatException
                    If Me.Mode = Modes.Search Then
                        Me.ErrorProvider1.SetError(Me.ComboBox1, "La fecha ingresada no es valida")
                        'Muestro el icono de error
                    Else
                        Me.ComboBox1.Text = Me.RealData
                        Me.IsValid = True
                        Exit Sub
                    End If
                    Try
                        If IsNothing(Me.DT) = False Then Me.DT.Value = Now
                    Catch
                    End Try
                Catch ex As System.ArgumentOutOfRangeException
                    If Me.Mode = Modes.Search Then
                        Me.ErrorProvider1.SetError(Me.ComboBox1, "La fecha ingresada no es valida")
                        'Muestro el icono de error
                    Else
                        Me.ComboBox1.Text = Me.RealData
                        Me.IsValid = True
                        Exit Sub
                    End If
                    Try
                        If IsNothing(Me.DT) = False Then Me.DT.Value = Now
                    Catch
                    End Try
                Catch ex As System.IndexOutOfRangeException
                    If Me.Mode = Modes.Search Then
                        Me.ErrorProvider1.SetError(Me.ComboBox1, "La fecha ingresada no es valida")
                        'Muestro el icono de error
                    Else
                        Me.ComboBox1.Text = Me.RealData
                        Me.IsValid = True
                        Exit Sub
                    End If
                    Try
                        If IsNothing(Me.DT) = False Then Me.DT.Value = Now
                    Catch
                    End Try
                Catch ex As System.InvalidCastException
                    If Me.Mode = Modes.Search Then
                        Me.ErrorProvider1.SetError(Me.ComboBox1, "La fecha ingresada no es valida")
                        'Muestro el icono de error
                    Else
                        Me.ComboBox1.Text = Me.RealData
                        Me.IsValid = True
                        Exit Sub
                    End If
                    Try
                        If IsNothing(Me.DT) = False Then Me.DT.Value = Now
                    Catch
                    End Try
                Catch ex As OverflowException
                    If Me.Mode = Modes.Search Then
                        Me.ErrorProvider1.SetError(Me.ComboBox1, "La fecha ingresada no es valida")
                        'Muestro el icono de error
                    Else
                        Me.ComboBox1.Text = Me.RealData
                        Me.IsValid = True
                        Exit Sub
                    End If
                    Try
                        If IsNothing(Me.DT) = False Then Me.DT.Value = Now
                    Catch
                    End Try
                Catch ex As Exception
                    If Me.Mode = Modes.Search Then
                        Me.ErrorProvider1.SetError(Me.ComboBox1, "La fecha ingresada no es valida")
                        'Muestro el icono de error
                    Else
                        Me.ComboBox1.Text = Me.RealData
                        Me.IsValid = True
                        Exit Sub
                    End If
                    Try
                        If IsNothing(Me.DT) = False Then Me.DT.Value = Now
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
                    Me.ComboBox1.Text = Me.ComboBox1.Text.Replace("-", "/")
                    Me.ComboBox1.Text = Me.ComboBox1.Text.Replace(" ", "/")
                    Me.ComboBox1.SelectionStart = Me.ComboBox1.Text.Length
                    Me.ComboBox1.SelectionLength = 0

                    Dim d As DateTime = DateTime.Parse(Me.ComboBox1.Text, System.Globalization.DateTimeFormatInfo.CurrentInfo)
                    If IsNothing(Me.DT) = False Then
                        If Me.DT.Value = d Then
                            Me.ComboBox1.Text = DT.Value.ToShortTimeString
                        Else
                            Me.DT.Value = d
                        End If
                    End If
                    'TODO: Verificar si le voy a mandar la fecha con la cultura actual o se lo mando con mi formato
                    '            Me.RealData = d.ToString("dd/MM/yyyy")
                    Me.RealData = d.ToString(System.Globalization.DateTimeFormatInfo.CurrentInfo)
                    Me.IsValid = True
                    Exit Sub
                Catch ex As System.FormatException
                    If Me.Mode = Modes.Search Then
                        Me.ErrorProvider1.SetError(Me.ComboBox1, "La hora ingresada no es valida")
                        'Muestro el icono de error
                    Else
                        Me.ComboBox1.Text = Me.RealData
                        Me.IsValid = True
                        Exit Sub
                    End If
                    Try
                        If IsNothing(Me.DT) = False Then Me.DT.Value = Now
                    Catch
                    End Try
                Catch ex As System.ArgumentOutOfRangeException
                    If Me.Mode = Modes.Search Then
                        Me.ErrorProvider1.SetError(Me.ComboBox1, "La hora ingresada no es valida")
                        'Muestro el icono de error
                    Else
                        Me.ComboBox1.Text = Me.RealData
                        Me.IsValid = True
                        Exit Sub
                    End If
                    Try
                        If IsNothing(Me.DT) = False Then Me.DT.Value = Now
                    Catch
                    End Try
                Catch ex As System.IndexOutOfRangeException
                    If Me.Mode = Modes.Search Then
                        Me.ErrorProvider1.SetError(Me.ComboBox1, "La hora ingresada no es valida")
                        'Muestro el icono de error
                    Else
                        Me.ComboBox1.Text = Me.RealData
                        Me.IsValid = True
                        Exit Sub
                    End If
                    Try
                        If IsNothing(Me.DT) = False Then Me.DT.Value = Now
                    Catch
                    End Try
                Catch ex As System.InvalidCastException
                    If Me.Mode = Modes.Search Then
                        Me.ErrorProvider1.SetError(Me.ComboBox1, "La hora ingresada no es valida")
                        'Muestro el icono de error
                    Else
                        Me.ComboBox1.Text = Me.RealData
                        Me.IsValid = True
                        Exit Sub
                    End If
                    Try
                        If IsNothing(Me.DT) = False Then Me.DT.Value = Now
                    Catch
                    End Try
                Catch ex As OverflowException
                    If Me.Mode = Modes.Search Then
                        Me.ErrorProvider1.SetError(Me.ComboBox1, "La hora ingresada no es valida")
                        'Muestro el icono de error
                    Else
                        Me.ComboBox1.Text = Me.RealData
                        Me.IsValid = True
                        Exit Sub
                    End If
                    Try
                        If IsNothing(Me.DT) = False Then Me.DT.Value = Now
                    Catch
                    End Try
                Catch ex As Exception
                    If Me.Mode = Modes.Search Then
                        Me.ErrorProvider1.SetError(Me.ComboBox1, "La hora ingresada no es valida")
                        'Muestro el icono de error
                    Else
                        Me.ComboBox1.Text = Me.RealData
                        Me.IsValid = True
                        Exit Sub
                    End If
                    Try
                        If IsNothing(Me.DT) = False Then Me.DT.Value = Now
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
            ElseIf Me.Index.DropDown = IndexAdditionalType.AutoSustitución OrElse Me.Index.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                If IsNumeric(Me.ComboBox1.Text) AndAlso Me.ComboBox1.Text.IndexOf("-") = -1 Then
                    Dim r() As DataRow = Me.AutoSustitucionTable.Select("trim(Codigo)=" & Me.ComboBox1.Text)
                    If r.Length > 0 Then
                        'el codigo pertenece a la lista
                        Me.RealData = r(0).Item(0).ToString()
                        Me.RealDataDescription = r(0).Item(1).ToString()
                        Me.ComboBox1.Text = r(0).Item(1).ToString()
                        Me.IsValid = True
                        RaiseEvent ItemChanged(Me.Index.ID, Me.RealData)

                        Exit Sub
                    Else
                        'el codigo no pertenece a la lista
                        Me.RealData = CInt(Me.ComboBox1.Text).ToString()
                        Me.IsValid = True
                        Exit Sub
                    End If
                Else
                    Dim r() As DataRow = Me.AutoSustitucionTable.Select("Descripcion='" & Trim(Me.ComboBox1.Text) & "'")
                    'la descripcion pertenece a la lista
                    If r.Length = 1 Then
                        Me.RealData = r(0).Item(0).ToString()
                        Me.ComboBox1.Text = r(0).Item(1).ToString()
                        Me.RealDataDescription = r(0).Item(1).ToString()
                        Me.IsValid = True
                        Exit Sub
                    ElseIf r.Length > 1 Then
                        Dim i As Int32
                        For i = 0 To r.Length - 1
                            If String.Compare(Me.RealData, CType(r(i).Item(0), String)) = 0 Then
                                Me.ComboBox1.Text = r(i).Item(1).ToString()
                                Me.RealDataDescription = r(0).Item(1).ToString()
                                Me.IsValid = True
                                Exit Sub
                            End If
                        Next
                        If Me.IsValid = False Then
                            Me.RealData = r(0).Item(0).ToString()
                            Me.ComboBox1.Text = r(0).Item(1).ToString()
                            Me.RealDataDescription = r(0).Item(1).ToString()
                            Me.IsValid = True
                            Exit Sub
                        End If
                    Else
                        'la descripcion no pertenece a la lista
                        '                        Me.ComboBox1.Text = Me.RealData
                        Me.ErrorProvider1.SetError(Me.ComboBox1, "El texto ingresado no pertenece a la lista")
                    End If
                End If
            ElseIf (Me.Index.DropDown = IndexAdditionalType.DropDown OrElse Me.Index.DropDown = IndexAdditionalType.DropDownJerarquico) AndAlso Me.RealData <> Me.ComboBox1.Text Then
                Me.RealData = Me.ComboBox1.Text
                Me.IsValid = True
                Exit Sub
            ElseIf (Me.Index.Type = IndexDataType.Numerico OrElse Me.Index.Type = IndexDataType.Numerico_Largo) AndAlso Me.RealData <> Me.ComboBox1.Text Then
                Try
                    Me.ComboBox1.Text = Me.ComboBox1.Text.Replace(",", ".")
                    Me.ComboBox1.Text = Me.ComboBox1.Text.Replace("..", ".")

                    If ComboBox1.Text.IndexOf("-") <> -1 AndAlso Mid(ComboBox1.Text, 1, 1) <> "-" Then Me.ComboBox1.Text.Replace("-", String.Empty)

                    Decimal.Parse(Me.ComboBox1.Text)
                    Me.RealData = Me.ComboBox1.Text
                    Me.IsValid = True
                    Exit Sub
                Catch ex As Exception
                    '                    Me.ComboBox1.Text = Me.RealData
                    Me.ErrorProvider1.SetError(Me.ComboBox1, "El valor ingresado no es numerico")
                    '                   Me.IsValid = True
                End Try
            ElseIf (Me.Index.Type = IndexDataType.Moneda OrElse Me.Index.Type = IndexDataType.Numerico_Decimales) AndAlso Me.RealData <> Me.ComboBox1.Text Then
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
            ElseIf (Me.Index.Type = IndexDataType.Alfanumerico OrElse Me.Index.Type = IndexDataType.Alfanumerico_Largo) AndAlso Me.RealData <> Me.ComboBox1.Text Then
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
            Try
                If IsNothing(Me.DT) = False Then Me.DT.Value = Now
            Catch
            End Try
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
            Try
                If IsNothing(Me.DT) = False Then Me.DT.Value = Now
            Catch
            End Try
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
            Try
                If IsNothing(Me.DT) = False Then Me.DT.Value = Now
            Catch
            End Try
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
            Try
                If IsNothing(Me.DT) = False Then Me.DT.Value = Now
            Catch
            End Try
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
            Try
                If IsNothing(Me.DT) = False Then Me.DT.Value = Now
            Catch
            End Try
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
            Try
                If IsNothing(Me.DT) = False Then Me.DT.Value = Now
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
            Me.IsValid = True
            AddHandlers()
        End Try
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
        'If Me.IsValid = False Then Me.ErrorProvider1.SetError(Me.ComboBox1, String.Empty)
        AddHandlers()
    End Sub

#End Region

#Region "Eventos de Enter y Tab"
    Public Shadows Event EnterPressed()
    Public Shadows Event TabPressed()
    Public Shadows Event ItemChanged(ByVal IndexID As Integer, ByVal NewValue As String)

    Private Sub txtIndexCtrl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Enter Then
            RaiseEvent EnterPressed()
        End If
        If e.KeyCode = Keys.Tab Then
            RaiseEvent TabPressed()
        End If
    End Sub

    Private Sub txtIndexCtrl_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        If e.KeyChar = Chr(13) Then
            RaiseEvent EnterPressed()
        End If
        If e.KeyChar = Chr(9) Then
            RaiseEvent TabPressed()
        End If

    End Sub

    Private Sub ComboBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
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
    '            Dim frm As frmMultiline = Nothing
    '            If String.IsNullOrEmpty(Me.Index.DataTemp.Trim()) Then
    '                frm = New frmMultiline(Me.Index.Data, Me.Index.Name)
    '            Else
    '                frm = New frmMultiline(Me.Index.DataTemp, Me.Index.Name)
    '            End If

    '            frm.ShowDialog()
    '            Me.ComboBox1.Text = frm.txtIndexValue.Text
    '            Me.Index.DataTemp = frm.txtIndexValue.Text
    '            Me.ComboBox1.Select()
    '            Me.ComboBox1.Focus()
    '            frm.Dispose()
    '        End If
    '    End Sub
    '#End Region

    Private Sub DT_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DT.KeyDown
        If e.KeyCode = Keys.Enter Then
            RaiseEvent EnterPressed()
        End If
        If e.KeyCode = Keys.Tab Then
            RaiseEvent TabPressed()
        End If

    End Sub

    'Private Sub DT_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DT.MouseDown
    '    '    Try
    '    '        Me.IsValid = False
    '    '        Me.DateProcedure(Me.DT, New EventArgs)
    '    '    Catch
    '    '    End Try
    'End Sub

    Private Sub DT_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles DT.CloseUp
        Try
            Me.IsValid = False
            Me.DateProcedure(Me.DT, New EventArgs)
        Catch
        End Try
    End Sub

    'Private Sub DT_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DT.ValueChanged
    'End Sub


    'Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

    'End Sub

    Private Sub ComboBox1_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        RemoveHandlers()
        Dim ce As New System.ComponentModel.CancelEventArgs
        DataChangeProcedure(sender, ce)
        RaiseEvent ItemChanged(Index.ID, ComboBox1.SelectedItem.ToString())
        AddHandlers()
    End Sub

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
