Imports System.Collections.Generic
Imports Telerik.WinControls.UI
Imports Zamba.Core



Public Class txtIndexCtrl
    Inherits txtBaseIndexCtrl
    Implements IDisposable

    Private Const STR_CHAR As String = "char"
    Private Const STR_NUMERIC As String = "numeric"

#Region "Constructores"
    Private Mode As Modes
    Private _docTypeID As Int64 = 0
    Private _parentData As String

    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If Not isDisposed Then
            Try
                If disposing Then
                    If Not (components Is Nothing) Then
                        components.Dispose()
                    End If

                    RemoveHandlers()

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
                    'If frmListaSubstitucion IsNot Nothing Then
                    '    frmListaSubstitucion.Dispose()
                    '    frmListaSubstitucion = Nothing
                    'End If
                    If _AutoSubstitucionTable IsNot Nothing Then
                        _AutoSubstitucionTable.Dispose()
                        _AutoSubstitucionTable = Nothing
                    End If
                End If
                MyBase.Dispose(disposing)
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
            End Try

            isDisposed = True
        End If
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer
    Private isDisposed As Boolean
    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents ComboBox1 As System.Windows.Forms.Control
    Friend WithEvents Panel1 As ZPanel
    Friend WithEvents Button1 As ZButton
    '    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    <DebuggerStepThrough()> Private Sub InitializeComponent()

        Panel1 = New ZPanel
        ErrorProvider1 = New System.Windows.Forms.ErrorProvider
        SuspendLayout()


        '
        'Panel1
        '
        Panel1.BackColor = Color.FromArgb(229, 229, 229)
        Panel1.CausesValidation = False
        Panel1.Dock = DockStyle.Right
        Panel1.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, 0)
        Panel1.ForeColor = Color.FromArgb(76, 76, 76)
        Panel1.Location = New Point(504, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(22, 25)
        Panel1.TabIndex = 1
        Panel1.BorderStyle = BorderStyle.None

        '
        'ErrorProvider1
        '
        ErrorProvider1.ContainerControl = Me
        '
        'txtIndexCtrl
        '
        Name = "txtIndexCtrl"
        Size = New Size(528, 40)
        BorderStyle = BorderStyle.None
        BackColor = Color.FromArgb(229, 229, 229)
        ResumeLayout(False)

    End Sub
#End Region

#Region "Constructores"

    Public Sub New(ByVal docindex As Index, ByVal data2 As Boolean, ByVal Mode As Modes, ByVal DocTypeID As Int64, ByVal parentData As String)
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        _docTypeID = DocTypeID
        Me.ParentData = parentData

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()
        Me.Mode = Mode
        Index = docindex
        FlagData2 = data2
        Init()
    End Sub


#End Region

#Region "Propiedades Publicas"
    '  Public _IsValid As Boolean = True
    Public Overrides Property IsValid() As Boolean
        Get
            If _IsValid = False Then
                DataChangeProcedure(ComboBox1, New ComponentModel.CancelEventArgs)
                'Me.ErrorProvider1.SetError(Me.ComboBox1, String.Empty)
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
    '    Public Index As Index

    Private DropDowndata As New List(Of String)
    'Este flag me dice si trabajo con data1 o data2
    Private FlagData2 As Boolean
    'Picker para las fechas
    Private WithEvents DT As DateTimePicker
    'Formulario para las listas de Substitucion
    Private frmListaSubstitucion As frmIndexSubtitutiom
    Private _AutoSubstitucionTable As DataTable

    Private Property AutoSustitucionTable() As DataTable
        Get
            Return _AutoSubstitucionTable
        End Get
        Set(ByVal value As DataTable)
            _AutoSubstitucionTable = value
        End Set
    End Property

    Private Sub Init()

        'Inicializa el Control para el tipo del indice Fecha combobox y demas, desde aca te carga todo lo que se va a visualizar, mas que nada maneja estilos
        Try
            RemoveHandlers()

            Index.DataTemp = Index.Data
            Index.DataTemp2 = Index.Data2
            Index.dataDescriptionTemp = Index.dataDescription
            Index.dataDescriptionTemp2 = Index.dataDescription2

            If Me.Index.Type = IndexDataType.Fecha OrElse Me.Index.Type = IndexDataType.Fecha_Hora Then
                ComboBox1 = New RadTextBox
            Else
                Select Case Index.DropDown
                    Case IndexAdditionalType.AutoSustitución
                        ComboBox1 = New RadTextBox
                    Case IndexAdditionalType.AutoSustituciónJerarquico
                        ComboBox1 = New RadTextBox
                    Case IndexAdditionalType.DropDown
                        ComboBox1 = New RadDropDownList
                        DirectCast(ComboBox1, RadDropDownList).DropDownStyle = ComboBoxStyle.DropDownList
                        DirectCast(ComboBox1, RadDropDownList).SortStyle = True
                    Case IndexAdditionalType.DropDownJerarquico
                        ComboBox1 = New RadDropDownList
                        DirectCast(ComboBox1, RadDropDownList).DropDownStyle = ComboBoxStyle.DropDown
                        DirectCast(ComboBox1, RadDropDownList).DropDownStyle = ComboBoxStyle.DropDownList
                        DirectCast(ComboBox1, RadDropDownList).SortStyle = True
                    Case IndexAdditionalType.LineText
                        ComboBox1 = New RadTextBox
                End Select
            End If



            If Me.Index.Type = IndexDataType.Fecha Then
                DT = New DateTimePicker
                DT.CustomFormat = "dd/MM/yyyy"

                Try
                    DT.TabStop = False
                    If IsNothing(Data) = False AndAlso Data.Length > 0 Then DT.Value = Date.ParseExact(Data, "dd/MM/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                Catch ex As FormatException
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
                Panel1.Controls.Add(DT)
                DT.Dock = DockStyle.Fill
                DirectCast(ComboBox1, RadTextBox).MaxLength = 10
            ElseIf Me.Index.Type = IndexDataType.Fecha_Hora Then
                DT = New DateTimePicker
                DT.CustomFormat = "dd/MM/yyyy HH:mm:ss"

                Try
                    DT.TabStop = False
                    If IsNothing(Data) = False AndAlso Data.Length > 0 Then DT.Value = Date.ParseExact(Data, "dd/MM/yyyy HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                Catch ex As FormatException
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
                Panel1.Controls.Add(DT)
                DT.Dock = DockStyle.Fill
                DirectCast(ComboBox1, RadTextBox).MaxLength = 19
            Else
                Select Case Index.DropDown
                    'Para auto Substitucion
                    Case IndexAdditionalType.AutoSustitución
                        Button1 = New ZButton
                        Button1.Text = ""
                        Button1.BackgroundImage = My.Resources.appbar_magnify
                        Button1.BackgroundImageLayout = ImageLayout.Zoom
                        Button1.FlatStyle = FlatStyle.Flat
                        Panel1.Controls.Add(Button1)
                        Button1.Dock = DockStyle.Fill
                        If (Mode <> Modes.Search) Then
                            AutoSustitucionTable = AutoSubstitutionBusiness.GetIndexData(Index.ID, False)
                            SetAutosustitutionDisplay()
                            ComboBox1.Text = Index.dataDescription
                        End If
                    Case IndexAdditionalType.AutoSustituciónJerarquico
                        'Para auto Substitucion jearquico
                        Button1 = New ZButton
                        Button1.Text = ""
                        Button1.BackgroundImage = My.Resources.appbar_magnify
                        Button1.BackgroundImageLayout = ImageLayout.Zoom
                        Button1.FlatStyle = FlatStyle.Flat

                        Panel1.Controls.Add(Button1)
                        Button1.Dock = DockStyle.Fill
                        If (Mode <> Modes.Search) Then
                            AutoSustitucionTable = IndexsBussinesExt.GetHierarchicalTableByValue(Index.ID, Index.HierarchicalParentID, ParentData, True)
                            SetAutosustitutionDisplay()
                            ComboBox1.Text = Index.dataDescription
                        End If
                    Case IndexAdditionalType.DropDown
                        'Para dropdown
                        Panel1.Visible = False
                        DropDowndata.Clear()
                        DropDowndata.Add(String.Empty)
                        Dim List As List(Of String) = IndexsBusiness.GetDropDownList(CInt(Index.ID))
                        If Not List Is Nothing Then DropDowndata.AddRange(List)
                        DirectCast(ComboBox1, RadDropDownList).Items.Clear()
                        DirectCast(ComboBox1, RadDropDownList).Items.AddRange(DropDowndata)
                        ComboBox1.Dock = DockStyle.Fill
                        DirectCast(ComboBox1, RadDropDownList).MaxLength = Index.Len
                        ComboBox1.Text = Index.Data
                    Case IndexAdditionalType.DropDownJerarquico
                        'Para dropdown jearquico
                        Panel1.Visible = False
                        DirectCast(ComboBox1, RadDropDownList).DropDownStyle = ComboBoxStyle.DropDown
                        DropDowndata.Clear()
                        DropDowndata.Add(String.Empty)
                        DropDowndata = GetDropDownHierarchicalList(_parentData)
                        DirectCast(ComboBox1, RadDropDownList).Items.Clear()
                        DirectCast(ComboBox1, RadDropDownList).Items.AddRange(DropDowndata)
                        ComboBox1.Dock = DockStyle.Fill
                        DirectCast(ComboBox1, RadDropDownList).MaxLength = Index.Len
                        DropDownProcedure(Nothing, System.EventArgs.Empty)
                        ComboBox1.Text = Index.Data
                    Case IndexAdditionalType.LineText
                        Panel1.Visible = False
                        ComboBox1.Dock = DockStyle.Fill
                        DirectCast(ComboBox1, RadTextBox).MaxLength = Index.Len
                        'CAMBIE REALDTA POR DATA
                        ComboBox1.Text = Data
                End Select
                IsValid = True
                RaiseEvent ItemChanged(Index.ID, Index.Data)
            End If

            ComboBox1.Dock = DockStyle.Fill
            ErrorProvider1.SetIconPadding(ComboBox1, -40)
            ComboBox1.Location = New Point(0, 0)
            ComboBox1.Name = "ComboBox1"
            ComboBox1.Size = New Size(506, 25)
            ComboBox1.TabIndex = 0
            ComboBox1.BackColor = Color.White

            Controls.Add(ComboBox1)
            Controls.Add(Panel1)

        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            AddHandlers()
            '   Me.ResumeLayout()
        End Try
    End Sub

    Private Sub SetAutosustitutionDisplay()
        If IsNumeric(Data) AndAlso Data.IndexOf("-") = -1 Then

            Dim r() As DataRow

            r = GetFilterRows()

            If r.Length > 0 Then
                'el codigo pertenece a la lista

                ComboBox1.Text = r(0).Item(1).ToString()
                Index.dataDescriptionTemp = r(0).Item(1).ToString()
                Index.dataDescription = r(0).Item(1).ToString()
            Else
                ComboBox1.Text = Data
            End If
        Else
            ComboBox1.Text = Data
        End If
    End Sub

    Public Overrides Sub RefreshControl(ByRef index As IIndex)
        Try
            If index Is Nothing = False Then
                RemoveHandlers()
                Me.Index = index

                Me.Index.DataTemp = index.Data
                Me.Index.DataTemp2 = index.Data2
                Me.Index.Data = index.Data
                Me.Index.dataDescriptionTemp = index.dataDescription
                Me.Index.dataDescriptionTemp2 = index.dataDescription2

                Select Case Me.Index.DropDown
                    'Para auto Substitucion
                    Case IndexAdditionalType.AutoSustitución
                        AutoSustitucionTable = AutoSubstitutionBusiness.GetIndexData(index.ID, False)
                        SetAutosustitutionDisplay()
                        ComboBox1.Text = index.dataDescription

                    Case IndexAdditionalType.AutoSustituciónJerarquico
                        AutoSustitucionTable = IndexsBussinesExt.GetHierarchicalTableByValue(index.ID, index.HierarchicalParentID, ParentData, True)
                        SetAutosustitutionDisplay()
                        ComboBox1.Text = index.dataDescription

                    Case IndexAdditionalType.DropDown
                        DropDowndata.Clear()
                        DropDowndata.Add(String.Empty)
                        Dim List As List(Of String) = IndexsBusiness.GetDropDownList(CInt(Me.Index.ID))
                        If Not List Is Nothing Then DropDowndata.AddRange(List)
                        DirectCast(ComboBox1, RadDropDownList).Items.Clear()
                        DirectCast(ComboBox1, RadDropDownList).Items.AddRange(DropDowndata)
                        ComboBox1.Text = index.Data

                    Case IndexAdditionalType.DropDownJerarquico
                        DropDowndata.Clear()
                        DropDowndata.Add(String.Empty)
                        DropDowndata = GetDropDownHierarchicalList(_parentData)
                        DirectCast(ComboBox1, RadDropDownList).Items.Clear()
                        DirectCast(ComboBox1, RadDropDownList).Items.AddRange(DropDowndata)
                        DropDownProcedure(Nothing, System.EventArgs.Empty)
                        ComboBox1.Text = index.Data

                    Case IndexAdditionalType.LineText
                        'CAMBIE REALDTA POR DATA
                        ComboBox1.Text = Data
                End Select

                IsValid = True
                AddHandlers()
                RaiseEvent ItemChanged(index.ID, index.Data)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Overrides Sub RefreshControlDataTemp(ByRef index As IIndex)
        ComboBox1.Text = index.DataTemp
    End Sub

    Private Function GetDropDownHierarchicalList(ByVal parentData As String) As List(Of String)
        Dim hierarchyValues As DataTable = IndexsBussinesExt.GetHierarchicalTableByValue(Index.ID, Index.HierarchicalParentID, parentData, True)
        Dim arToReturn As New List(Of String)

        arToReturn.AddRange((From row In hierarchyValues.Rows
                             Where Not IsDBNull(row(0)) AndAlso Not String.IsNullOrEmpty(row(0).ToString())
                             Select text = row(0).ToString()).ToList())

        Return arToReturn
    End Function

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

    Public Property ParentData() As String
        Get
            Return _parentData
        End Get
        Set(ByVal value As String)
            _parentData = value
            RefreshControl(Index)
        End Set
    End Property

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
            If ComboBox1 IsNot Nothing Then
                RemoveHandler ComboBox1.Validating, AddressOf DataChangeProcedure
                AddHandler ComboBox1.Validating, AddressOf DataChangeProcedure
                RemoveHandler ComboBox1.TextChanged, AddressOf ComboBox1_TextChanged
                AddHandler ComboBox1.TextChanged, AddressOf ComboBox1_TextChanged
            End If

            Select Case Index.Type
                Case IndexDataType.Fecha, IndexDataType.Fecha_Hora
                    If DT IsNot Nothing Then
                        RemoveHandler DT.CloseUp, AddressOf DT_CloseUp
                        RemoveHandler DT.KeyDown, AddressOf DT_KeyDown
                        AddHandler DT.CloseUp, AddressOf DT_CloseUp
                        AddHandler DT.KeyDown, AddressOf DT_KeyDown
                    End If
                Case Else
                    Select Case Index.DropDown
                        Case IndexAdditionalType.AutoSustitución, IndexAdditionalType.AutoSustituciónJerarquico
                            If Button1 IsNot Nothing Then
                                RemoveHandler Button1.Click, AddressOf AutoSustitutionProcedure
                                AddHandler Button1.Click, AddressOf AutoSustitutionProcedure
                            End If
                            If ComboBox1 IsNot Nothing Then
                                If TypeOf ComboBox1 Is RadTextBox Then
                                    RemoveHandler DirectCast(ComboBox1, RadTextBox).TextChanged, AddressOf DropDownProcedure
                                    AddHandler DirectCast(ComboBox1, RadTextBox).TextChanged, AddressOf DropDownProcedure
                                    RemoveHandler DirectCast(ComboBox1, RadTextBox).TextChanged, AddressOf ComboBox1_SelectedValueChanged
                                    AddHandler DirectCast(ComboBox1, RadTextBox).TextChanged, AddressOf ComboBox1_SelectedValueChanged
                                Else

                                    RemoveHandler DirectCast(ComboBox1, RadDropDownList).SelectedIndexChanged, AddressOf DropDownProcedure
                                    AddHandler DirectCast(ComboBox1, RadDropDownList).SelectedIndexChanged, AddressOf DropDownProcedure
                                    RemoveHandler DirectCast(ComboBox1, RadDropDownList).SelectedValueChanged, AddressOf ComboBox1_SelectedValueChanged
                                    AddHandler DirectCast(ComboBox1, RadDropDownList).SelectedValueChanged, AddressOf ComboBox1_SelectedValueChanged
                                End If

                            End If
                        Case IndexAdditionalType.DropDown, IndexAdditionalType.DropDownJerarquico
                            If ComboBox1 IsNot Nothing Then
                                RemoveHandler DirectCast(ComboBox1, RadDropDownList).SelectedIndexChanged, AddressOf DropDownProcedure
                                AddHandler DirectCast(ComboBox1, RadDropDownList).SelectedIndexChanged, AddressOf DropDownProcedure
                                RemoveHandler DirectCast(ComboBox1, RadDropDownList).SelectedValueChanged, AddressOf ComboBox1_SelectedValueChanged
                                AddHandler DirectCast(ComboBox1, RadDropDownList).SelectedValueChanged, AddressOf ComboBox1_SelectedValueChanged
                            End If
                    End Select
            End Select
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub RemoveHandlers()
        Try
            If ComboBox1 IsNot Nothing Then
                RemoveHandler ComboBox1.Validating, AddressOf DataChangeProcedure
                RemoveHandler ComboBox1.TextChanged, AddressOf ComboBox1_TextChanged
            End If
            Select Case Index.Type
                Case IndexDataType.Fecha, IndexDataType.Fecha_Hora
                    If DT IsNot Nothing Then
                        RemoveHandler DT.ValueChanged, AddressOf DateProcedure
                        RemoveHandler DT.Validating, AddressOf DateProcedure
                        RemoveHandler DT.CloseUp, AddressOf DT_CloseUp
                        RemoveHandler DT.KeyDown, AddressOf DT_KeyDown
                    End If
                Case Else
                    Select Case Index.DropDown
                        Case IndexAdditionalType.AutoSustitución, IndexAdditionalType.AutoSustituciónJerarquico
                            If Button1 IsNot Nothing Then RemoveHandler Button1.Click, AddressOf AutoSustitutionProcedure
                        Case IndexAdditionalType.DropDown, IndexAdditionalType.DropDownJerarquico
                            If ComboBox1 IsNot Nothing Then
                                RemoveHandler DirectCast(ComboBox1, RadDropDownList).SelectedIndexChanged, AddressOf DropDownProcedure
                                RemoveHandler DirectCast(ComboBox1, RadDropDownList).SelectedValueChanged, AddressOf ComboBox1_SelectedValueChanged
                            End If
                    End Select
            End Select
        Catch
        End Try
    End Sub

    Private Sub ComboBox1_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try

            Dim FlagLastCharSelected As Boolean = False

            Dim SelectionStart As Int16
            If ComboBox1.GetType().Name = "RadTextBox" Then
                SelectionStart = DirectCast(ComboBox1, RadTextBox).SelectionStart
            Else
                SelectionStart = DirectCast(ComboBox1, RadDropDownList).SelectionStart
            End If

            If SelectionStart = Len(ComboBox1.Text) Then
                FlagLastCharSelected = True
            End If

            IsValid = False
            RemoveHandlers()
            If ComboBox1.Text.CompareTo(String.Empty) = 0 Then ErrorProvider1.SetError(ComboBox1, String.Empty)

            If (Me.Index.Type = IndexDataType.Numerico OrElse Me.Index.Type = IndexDataType.Numerico_Largo OrElse Me.Index.Type = IndexDataType.Moneda OrElse Me.Index.Type = IndexDataType.Numerico_Decimales) Then
                Try
                    'TODO: VER DE HACER TODOS ESTOS REPLACES DESPUES PARA QUE EL 

                    If ComboBox1.Text.Length > 0 AndAlso ComboBox1.Text.Substring(0, 1).CompareTo(".") = 0 Then
                        Dim Pos As Int32 = SelectionStart
                        If Pos < 0 Then Pos = ComboBox1.Text.Length
                        ComboBox1.Text = ComboBox1.Text.Substring(1)
                        If Pos >= 1 Then SelectionStart = Pos - 1
                    End If

                    If (ComboBox1.Text.IndexOf(",") >= 0 OrElse ComboBox1.Text.IndexOf("..") >= 0) AndAlso (Index.DropDown <> IndexAdditionalType.AutoSustitución AndAlso Index.DropDown <> IndexAdditionalType.AutoSustituciónJerarquico) Then
                        Dim Pos As Int32 = SelectionStart
                        If Pos < 0 Then Pos = ComboBox1.Text.Length
                        ComboBox1.Text = ComboBox1.Text.Replace(",", ".")
                        ComboBox1.Text = ComboBox1.Text.Replace("..", ".")
                        SelectionStart = Pos - 1

                        If ComboBox1.Text.Substring(0, 1).CompareTo(".") = 0 Then
                            Dim Pos2 As Int32 = SelectionStart
                            If Pos2 < 0 Then Pos2 = ComboBox1.Text.Length
                            ComboBox1.Text = ComboBox1.Text.Substring(1)
                            If Pos2 >= 1 Then SelectionStart = Pos2 - 1
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


                    Dim SelectedItem As String
                    If ComboBox1.GetType().Name = "RadTextBox" Then
                        SelectedItem = DirectCast(ComboBox1, RadTextBox).Text
                        If FlagLastCharSelected = True Then DirectCast(ComboBox1, RadTextBox).Select(Len(SelectedItem), 1)
                        'Else
                        '    SelectedItem = DirectCast(ComboBox1, RadDropDownList).Text
                        '    If FlagLastCharSelected = True Then DirectCast(ComboBox1, RadDropDownList).Selec(Len(SelectedItem), 1)
                    End If

                    If SelectedItem.IndexOf(".") < SelectedItem.LastIndexOf(".") Then
                        Dim Pos2 As Int32 = SelectionStart
                        If Pos2 < 0 Then Pos2 = SelectedItem.Length
                        ComboBox1.Text = ComboBox1.Tag.ToString()
                        If Pos2 >= 1 Then SelectionStart = Pos2 - 1
                    End If

                    If Not IsNumeric(ComboBox1.Text) Then

                        'If Val(Me.ComboBox1.Text).ToString = "0" Then
                        If String.Compare(ComboBox1.Text, "0") = 0 Then
                            ComboBox1.Text = String.Empty
                        ElseIf ComboBox1.Text.Length > 0 Then
                            'Me.ComboBox1.Text = Val(Me.ComboBox1.Text).ToString
                            'ComboBox1_TextChanged(Me, New EventArgs)
                        End If

                        If Me.Index.DropDown = IndexAdditionalType.AutoSustitución _
                            Or Me.Index.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                            'Me.ComboBox1.Text = "0"
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

                        If ComboBox1.GetType().Name = "RadTextBox" Then
                            DirectCast(ComboBox1, RadTextBox).SelectionStart = ComboBox1.Text.Length
                            DirectCast(ComboBox1, RadTextBox).SelectionLength = 0
                        Else
                            DirectCast(ComboBox1, RadDropDownList).SelectionStart = ComboBox1.Text.Length
                            DirectCast(ComboBox1, RadDropDownList).SelectionLength = 0
                        End If

                    End If
                    If Me.Index.DropDown = IndexAdditionalType.AutoSustitución _
                        Or Me.Index.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                        'TODO POR AHORA LO SACAMOS PARA QUE A PARTIR DEL ID ENCUENTRE EL VALUE
                        'Me.ComboBox1.Text = String.Empty
                    End If
                Catch ex As IndexOutOfRangeException
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            ElseIf Me.Index.Type = IndexDataType.Fecha OrElse Me.Index.Type = IndexDataType.Fecha_Hora Then
                Try
                    If ComboBox1.Text.Length > 0 Then
                        Dim C As Char = ComboBox1.Text.Chars(ComboBox1.Text.Length - 1)
                        If Char.IsLetter(C) Then
                            ComboBox1.Text = ComboBox1.Text.Substring(0, ComboBox1.Text.Length - 1)
                            If ComboBox1.GetType().Name = "RadTextBox" Then
                                DirectCast(ComboBox1, RadTextBox).SelectionStart = ComboBox1.Text.Length
                                DirectCast(ComboBox1, RadTextBox).SelectionLength = 0
                            Else
                                DirectCast(ComboBox1, RadDropDownList).SelectionStart = ComboBox1.Text.Length
                                DirectCast(ComboBox1, RadDropDownList).SelectionLength = 0
                            End If
                        End If
                    Else
                        RealData = String.Empty
                    End If
                Catch ex As IndexOutOfRangeException
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try

            End If
        Catch ex As IndexOutOfRangeException
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            AddHandlers()
            ComboBox1.Tag = ComboBox1.Text
        End Try
    End Sub

    Private Sub DropDownProcedure(ByVal sender As Object, ByVal ev As EventArgs)
        Try
            ComboBox1.Select()
            If ComboBox1.GetType().Name = "RadTextBox" Then
                DirectCast(ComboBox1, RadTextBox).SelectionStart = 0
                DirectCast(ComboBox1, RadTextBox).SelectionLength = 0
            Else
                DirectCast(ComboBox1, RadDropDownList).SelectionStart = 0
                DirectCast(ComboBox1, RadDropDownList).SelectionLength = 0
            End If
        Catch
        End Try
    End Sub
    Private Sub DateProcedure(ByVal sender As Object, ByVal ev As EventArgs)
        Try
            '     Me.IsValid = False
            RemoveHandlers()

            If Me.Index.Type = IndexDataType.Fecha_Hora Then
                ComboBox1.Text = DT.Value.ToShortDateString & " " & DT.Value.ToShortTimeString
            Else
                ComboBox1.Text = DT.Value.ToShortDateString
            End If

            '     Me.ComboBox1.Text = DT.Value.ToString("dd/MM/yyyy")
            ComboBox1.Select()
            AddHandlers()
        Catch
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
        Catch
        End Try
    End Sub
    Private Sub AutoSustitutionProcedure(ByVal sender As Object, ByVal ev As EventArgs)
        Try
            RemoveHandlers()
            If AutoSustitucionTable Is Nothing Then
                Select Case Index.DropDown
                    Case IndexAdditionalType.AutoSustitución
                        AutoSustitucionTable = AutoSubstitutionBusiness.GetIndexData(Index.ID, False)
                    Case IndexAdditionalType.AutoSustituciónJerarquico
                        AutoSustitucionTable = IndexsBussinesExt.GetHierarchicalTableByValue(Index.ID, Index.HierarchicalParentID, ParentData, True)
                End Select
            End If
            frmListaSubstitucion = CachefrmIndexSubtitutiom.GetFrmIndexSubtitutionControl(Index.ID, AutoSustitucionTable, Index.DropDown = IndexAdditionalType.AutoSustituciónJerarquico)

            If Me.frmListaSubstitucion.ShowDialog() = DialogResult.OK Then
                RealData = frmListaSubstitucion.Codigo.ToString()
                ComboBox1.Text = frmListaSubstitucion.Descripcion
                If ComboBox1.GetType().Name = "RadTextBox" Then
                    DirectCast(ComboBox1, RadTextBox).SelectionStart = 0
                    DirectCast(ComboBox1, RadTextBox).SelectionLength = 0
                Else
                    DirectCast(ComboBox1, RadDropDownList).SelectionStart = 0
                    DirectCast(ComboBox1, RadDropDownList).SelectionLength = 0
                End If
                RaiseEvent ItemChanged(Index.ID, RealData)
                'oscar
                IsValid = False
            End If
            AddHandlers()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Function GetFilterRows() As DataRow()
        Dim r As DataRow()
        If Index.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
            If _AutoSubstitucionTable.Columns(0).DataType.ToString().Contains("String") Then
                r = AutoSustitucionTable.Select("trim(Value)=" & IndexsBussinesExt.GetColumnToWhere(STR_CHAR, ComboBox1.Text))
            Else
                r = AutoSustitucionTable.Select("Value=" & IndexsBussinesExt.GetColumnToWhere(STR_NUMERIC, ComboBox1.Text))
            End If
        ElseIf _AutoSubstitucionTable.Columns(0).DataType.ToString().Contains("String") Then
            r = AutoSustitucionTable.Select("trim(Codigo)=" & IndexsBussinesExt.GetColumnToWhere(STR_CHAR, ComboBox1.Text))
        Else
            r = AutoSustitucionTable.Select("Codigo=" & IndexsBussinesExt.GetColumnToWhere(STR_NUMERIC, ComboBox1.Text))
        End If
        Return r
    End Function


    Private Sub DataChangeProcedure(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Try
            RemoveHandlers()
            If ComboBox1.Text = String.Empty AndAlso RealData <> ComboBox1.Text AndAlso Index.Type <> IndexDataType.Fecha AndAlso Index.Type <> IndexDataType.Fecha_Hora Then
                RealData = ComboBox1.Text
                If Me.Index.Type = IndexDataType.Fecha OrElse Me.Index.Type = IndexDataType.Fecha_Hora Then
                    Try
                        If IsNothing(DT) = False Then
                            DT.Value = Now
                        End If
                    Catch
                    End Try
                End If
                IsValid = True
                Exit Sub
            ElseIf ComboBox1.Text = String.Empty Then
                IsValid = True
                Exit Sub
            ElseIf Me.Index.Type = IndexDataType.Fecha Then  'AndAlso Me.RealData <> Me.ComboBox1.Text
                Try
                    ComboBox1.Text = ComboBox1.Text.Replace("-", "/")
                    ComboBox1.Text = ComboBox1.Text.Replace(".", "/")
                    ComboBox1.Text = ComboBox1.Text.Replace(" ", "/")
                    If ComboBox1.GetType().Name = "RadTextBox" Then
                        DirectCast(ComboBox1, RadTextBox).SelectionStart = ComboBox1.Text.Length
                        DirectCast(ComboBox1, RadTextBox).SelectionLength = 0
                    Else
                        DirectCast(ComboBox1, RadDropDownList).SelectionStart = ComboBox1.Text.Length
                        DirectCast(ComboBox1, RadDropDownList).SelectionLength = 0
                    End If

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
                    ''Me.ComboBox1.Text = Me.ComboBox1.Text.Replace(" ", "/")

                    If ComboBox1.GetType().Name = "RadTextBox" Then
                        DirectCast(ComboBox1, RadTextBox).SelectionStart = ComboBox1.Text.Length
                        DirectCast(ComboBox1, RadTextBox).SelectionLength = 0
                    Else
                        DirectCast(ComboBox1, RadDropDownList).SelectionStart = ComboBox1.Text.Length
                        DirectCast(ComboBox1, RadDropDownList).SelectionLength = 0
                    End If

                    Dim d As DateTime = DateTime.Parse(ComboBox1.Text, System.Globalization.DateTimeFormatInfo.CurrentInfo)
                    If IsNothing(DT) = False Then
                        If DT.Value = d Then
                            ComboBox1.Text = DT.Value.ToShortDateString & " " & DT.Value.ToShortTimeString
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

                End Try
            ElseIf Me.Index.DropDown = IndexAdditionalType.AutoSustitución OrElse Me.Index.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                If IsNumeric(ComboBox1.Text) AndAlso ComboBox1.Text.IndexOf("-") = -1 Then
                    Dim r() As DataRow

                    r = GetFilterRows()

                    If r.Length > 0 Then
                        'el codigo pertenece a la lista
                        RealData = r(0).Item(0).ToString()
                        RealDataDescription = r(0).Item(1).ToString()
                        ComboBox1.Text = r(0).Item(1).ToString()
                        IsValid = True
                        RaiseEvent ItemChanged(Index.ID, RealData)

                        Exit Sub
                    Else
                        'el codigo no pertenece a la lista
                        RealData = CInt(ComboBox1.Text).ToString()
                        IsValid = True
                        Exit Sub
                    End If
                Else
                    Dim r() As DataRow

                    If Not AutoSustitucionTable Is Nothing Then
                        If Index.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                            r = AutoSustitucionTable.Select("Description = '" & ComboBox1.Text & "'")
                        Else
                            r = AutoSustitucionTable.Select("Descripcion = '" & ComboBox1.Text & "'")
                        End If
                    End If

                    'la descripcion pertenece a la lista
                    If Not r Is Nothing AndAlso r.Length = 1 Then
                        RealData = r(0).Item(0).ToString()
                        ComboBox1.Text = r(0).Item(1).ToString()
                        RealDataDescription = r(0).Item(1).ToString()
                        IsValid = True
                        Exit Sub
                    ElseIf Not r Is Nothing AndAlso r.Length > 1 Then
                        Dim i As Int32
                        For i = 0 To r.Length - 1
                            If String.Compare(RealData, CType(r(i).Item(0), String)) = 0 Then
                                ComboBox1.Text = r(i).Item(1).ToString()
                                RealDataDescription = r(0).Item(1).ToString()
                                IsValid = True
                                Exit Sub
                            End If
                        Next

                        RealData = r(0).Item(0).ToString()
                        ComboBox1.Text = r(0).Item(1).ToString()
                        RealDataDescription = r(0).Item(1).ToString()
                        IsValid = True
                        Exit Sub

                    Else
                        'la descripcion no pertenece a la lista
                        RealData = ComboBox1.Text
                        RealDataDescription = ComboBox1.Text
                        IsValid = True
                        Exit Sub

                        '                        Me.ComboBox1.Text = Me.RealData
                        ErrorProvider1.SetError(ComboBox1, "El texto ingresado no pertenece a la lista")
                    End If
                End If
            ElseIf (Me.Index.DropDown = IndexAdditionalType.DropDown OrElse Me.Index.DropDown = IndexAdditionalType.DropDownJerarquico) AndAlso RealData <> ComboBox1.Text Then
                RealData = ComboBox1.Text
                IsValid = True
                Exit Sub
            ElseIf (Me.Index.Type = IndexDataType.Numerico OrElse Me.Index.Type = IndexDataType.Numerico_Largo) AndAlso RealData <> ComboBox1.Text Then
                Try
                    ComboBox1.Text = ComboBox1.Text.Replace(",", ".")
                    ComboBox1.Text = ComboBox1.Text.Replace("..", ".")

                    If ComboBox1.Text.IndexOf("-") <> -1 AndAlso Mid(ComboBox1.Text, 1, 1) <> "-" Then ComboBox1.Text.Replace("-", String.Empty)

                    Decimal.Parse(ComboBox1.Text)
                    RealData = ComboBox1.Text
                    IsValid = True
                    Exit Sub
                Catch ex As Exception
                    ErrorProvider1.SetError(ComboBox1, "El valor ingresado no es numerico")
                End Try
            ElseIf (Me.Index.Type = IndexDataType.Moneda OrElse Me.Index.Type = IndexDataType.Numerico_Decimales) AndAlso RealData <> ComboBox1.Text Then
                Try
                    ComboBox1.Text.Replace(",", ".")
                    ComboBox1.Text.Replace("..", ".")
                    RealData = ComboBox1.Text
                    IsValid = True
                    Exit Sub
                Catch ex As Exception
                    ErrorProvider1.SetError(ComboBox1, "El valor ingresado no es numerico")
                End Try
            ElseIf (Me.Index.Type = IndexDataType.Alfanumerico OrElse Me.Index.Type = IndexDataType.Alfanumerico_Largo) AndAlso RealData <> ComboBox1.Text Then
                RealData = ComboBox1.Text
                IsValid = True
                Exit Sub
            End If
        Catch ex As FormatException
            If Me.Mode = Modes.Search Then
                ErrorProvider1.SetError(ComboBox1, "El valor ingresado es invalido")
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
            ZClass.raiseerror(ex)
        Catch ex As ArgumentNullException
            If Me.Mode = Modes.Search Then
                ErrorProvider1.SetError(ComboBox1, "El valor ingresado es invalido")
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
            ZClass.raiseerror(ex)
        Catch ex As InvalidCastException
            If Me.Mode = Modes.Search Then
                ErrorProvider1.SetError(ComboBox1, "El valor ingresado es invalido")
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
            ZClass.raiseerror(ex)
        Catch ex As OutOfMemoryException
            If Me.Mode = Modes.Search Then
                ErrorProvider1.SetError(ComboBox1, "El valor ingresado es invalido")
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
            ZClass.raiseerror(ex)
        Catch ex As OverflowException
            If Me.Mode = Modes.Search Then
                ErrorProvider1.SetError(ComboBox1, "El valor ingresado es invalido")
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
            ZClass.raiseerror(ex)
        Catch ex As Exception
            If Me.Mode = Modes.Search Then
                ErrorProvider1.SetError(ComboBox1, "El valor ingresado es invalido")
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
            ZClass.raiseerror(ex)

        Finally
            IsValid = True
            AddHandlers()
        End Try
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
        'If Me.IsValid = False Then Me.ErrorProvider1.SetError(Me.ComboBox1, String.Empty)
        AddHandlers()
    End Sub

#End Region

#Region "Eventos de Enter y Tab"
    Public Shadows Event EnterPressed()
    Public Shadows Event TabPressed()
    Public Shadows Event ItemChanged(ByVal IndexID As Integer, ByVal NewValue As String)

    Private Sub txtIndexCtrl_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
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

    Private Sub ComboBox1_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles ComboBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            RaiseEvent EnterPressed()
        End If
        If e.KeyCode = Keys.Tab Then
            RaiseEvent TabPressed()
        End If
    End Sub

#End Region

    Private Sub DT_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            RaiseEvent EnterPressed()
        End If
        If e.KeyCode = Keys.Tab Then
            RaiseEvent TabPressed()
        End If

    End Sub


    Private Sub DT_CloseUp(ByVal sender As Object, ByVal e As EventArgs)
        Try
            IsValid = False
            DateProcedure(DT, New EventArgs)
        Catch
        End Try
    End Sub


    Private Sub ComboBox1_SelectedValueChanged(ByVal sender As Object, ByVal e As EventArgs)
        RemoveHandlers()
        Dim ce As New ComponentModel.CancelEventArgs
        DataChangeProcedure(sender, ce)

        Dim SelectedItem As String
        If ComboBox1.GetType().Name = "RadTextBox" Then
            SelectedItem = DirectCast(ComboBox1, RadTextBox).Text
        Else
            SelectedItem = DirectCast(ComboBox1, RadDropDownList).SelectedText
        End If

        RaiseEvent ItemChanged(Index.ID, SelectedItem.ToString())
        AddHandlers()
        DirectCast(ComboBox1, RadTextBox).TextBoxElement.Focus()
        DirectCast(ComboBox1, RadTextBox).TextBoxElement.Select()
        DirectCast(ComboBox1, RadTextBox).SelectionStart = ComboBox1.Text.Length
        DirectCast(ComboBox1, RadTextBox).SelectionLength = 0

    End Sub

End Class