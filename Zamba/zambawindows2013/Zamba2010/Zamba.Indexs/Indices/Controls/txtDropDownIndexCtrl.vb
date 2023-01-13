Imports System.Collections.Generic
Imports Zamba.Core
Public Class txtDropDownIndexCtrl
    Inherits txtBaseIndexCtrl
    Implements IDisposable

#Region "Constructores"
    Private Mode As Modes
    Private _docTypeID As Int64 = 0
    Private _parentIndexData As String

    Public Sub New(ByRef docindex As IIndex, ByVal data2 As Boolean, ByVal Mode As Modes, ByVal DocTypeId As Int64, ByVal parentData As String)
        MyBase.New()

        _docTypeID = DocTypeId

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()
        Me.Mode = Mode
        Index = docindex
        FlagData2 = data2
        Init()

        If Not String.IsNullOrEmpty(parentData) Then
            _parentIndexData = parentData
            RefreshHierarchyControl(parentData)
        Else
            RefreshControl(Index)
        End If

        If UserPreferences.getValue("AllowInputTextInDropDownAndSustitutionIndexs", UPSections.UserPreferences, "True") = False Then
            ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
        End If
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

    ''' <summary>
    ''' Se utiliza esta propiedad para dar a conocer al control el valor del padre
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ParentData() As String
        Get
            Return _parentIndexData
        End Get
        Set(ByVal value As String)
            _parentIndexData = value
            RefreshHierarchyControl()
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
            RemoveHandlers()

            Index.DataTemp = Index.Data
            Index.DataTemp2 = Index.Data2
            Index.dataDescriptionTemp = Index.dataDescription
            Index.dataDescriptionTemp2 = Index.dataDescription2

            'Para dropdown
            Panel1.Visible = False
            ComboBox1.DropDownStyle = ComboBoxStyle.DropDown
            ComboBox1.FlatStyle = FlatStyle.Flat
            DropDowndata.Clear()
            DropDowndata.Add(String.Empty)

            Dim IndexId As Int32 = 0
            If Int32.TryParse(Index.ID.ToString, IndexId) = True Then
                ' Agrega una lista de sustitución al comboBox...

                DropDowndata.Clear()

                Dim List As List(Of String) = IndexsBusiness.GetDropDownList(IndexId)
                If Not List Is Nothing Then DropDowndata.AddRange(List)

            End If

            ComboBox1.Items.Clear()
            ComboBox1.Items.AddRange(DropDowndata.ToArray)
            ComboBox1.Dock = DockStyle.Fill
            ComboBox1.MaxLength = Index.Len

            ComboBox1.DropDownWidth = 250 'resizeWidthCombo(ComboBox1)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            AddHandlers()
            '   Me.ResumeLayout()
        End Try
    End Sub

    Private Function resizeWidthCombo(comboBox1 As ComboBox) As Integer

        Dim maxWidth As Integer
        Dim tempWidth As Integer

        For Each obj As Object In comboBox1.Items

            tempWidth = obj.ToString().Trim().Length
            If tempWidth > maxWidth Then
                maxWidth = tempWidth
            End If
        Next
        Return maxWidth
    End Function

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

            ComboBox1.SelectedIndex = -1
            'CAMBIE REALDTA POR DATA
            'If Me.Index.DropDown <> IndexAdditionalType.AutoSustituciónJerarquico And Me.Index.DropDown <> IndexAdditionalType.DropDownJerarquico Then
            ComboBox1.Text = Data

            IsValid = True
            AddHandlers()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Sub RefreshHierarchyControl(ByRef parentData As String)
        Try
            RemoveHandlers()

            ZTrace.WriteLineIf(ZTrace.IsInfo, "txtDropDownIndexCtrl - Refrescando los items del combo, indice: " & Index.ID & " parendData: " & parentData)
            Dim hierarchyValues As DataTable = IndexsBussinesExt.GetHierarchicalTableByValue(Index.ID, Index.HierarchicalParentID, parentData, True)

            ComboBox1.Items.Clear()
            ComboBox1.Items.Add(String.Empty)

            If hierarchyValues.Rows.Count > 0 Then
                For Each item As DataRow In hierarchyValues.Rows
                    If Not String.IsNullOrEmpty(item(0).ToString()) Then
                        ComboBox1.Items.Add(item(0).ToString())
                    End If
                Next
            End If

            ComboBox1.SelectedValue = RealData

            IsValid = True
            AddHandlers()
            'RaiseEvent ItemChanged(Index.ID, Me.RealData)
        Catch ex As Exception
            ZClass.raiseerror(ex)
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
        RemoveHandlers()
        RefreshControl(Index)
        AddHandlers()
    End Sub
    Public Overrides Sub Commit()
        If IsNothing(Index.DataTemp) = False Then
            Index.Data = Index.DataTemp
            Index.dataDescription = Index.dataDescriptionTemp
            RemoveHandlers()
            RefreshControl(Index)
            AddHandlers()
        End If
        If IsNothing(Index.DataTemp2) = False Then
            Index.Data2 = Index.DataTemp2
            Index.dataDescription2 = Index.dataDescriptionTemp2
            RemoveHandlers()
            RefreshControl(Index)
            AddHandlers()
        End If
    End Sub


    Public Shadows Event IndexChanged()
    Public Shadows Event ItemChanged(ByVal IndexID As Integer, ByVal NewValue As String)

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
            If FlagCambioElContenido = True Then
                RaiseEvent IndexChanged()
            End If

            RaiseEvent ItemChanged(Index.ID, Value)
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
            If Not IsNothing(ComboBox1) Then
                RemoveHandler ComboBox1.Validating, AddressOf DataChangeProcedure
                AddHandler ComboBox1.Validating, AddressOf DataChangeProcedure
                RemoveHandler ComboBox1.TextChanged, AddressOf ComboBox1_TextChanged
                AddHandler ComboBox1.TextChanged, AddressOf ComboBox1_TextChanged

                RemoveHandler ComboBox1.SelectedIndexChanged, AddressOf ComboBox1_TextChanged
                AddHandler ComboBox1.SelectedIndexChanged, AddressOf ComboBox1_TextChanged

                RemoveHandler ComboBox1.LostFocus, AddressOf ComboBox1_LostFocus
                AddHandler ComboBox1.LostFocus, AddressOf ComboBox1_LostFocus
            End If
        Catch
        End Try
    End Sub

    Private Sub RemoveHandlers()
        Try
            RemoveHandler ComboBox1.Validating, AddressOf DataChangeProcedure
            RemoveHandler ComboBox1.TextChanged, AddressOf ComboBox1_TextChanged
            RemoveHandler ComboBox1.SelectedIndexChanged, AddressOf DropDownProcedure
            RemoveHandler ComboBox1.LostFocus, AddressOf ComboBox1_LostFocus
        Catch
        End Try
    End Sub

    Private Sub ComboBox1_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If isDisposed = False AndAlso Disposing = False Then
                Dim FlagLastCharSelected As Boolean = False
                If ComboBox1.SelectionStart = ComboBox1.Text.Length Then
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
                            If Pos >= 1 Then ComboBox1.SelectionStart = Pos - 1
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
                            '    TODO NO SE PUEDE HACER LO SIGUIENTE PORQUE EL USUARIO PUEDE ESTAR
                            '    ESCRIBIENDO EN CUALQUIER POSICION DEL COMBO
                            '    HABRIA QUE HACER UN REPLACE O UN UNDO
                            '    Me.ComboBox1.Text = Me.ComboBox1.Text.Substring(0, Me.ComboBox1.Text.Length - 1)
                            'Catch ex As IndexOutOfRangeException
                            'Catch
                            'End Try
                            ComboBox1.SelectionStart = ComboBox1.Text.Length
                            ComboBox1.SelectionLength = 0
                        End If
                        'If Me.Index.DropDown = IndexAdditionalType.AutoSustitución Then
                        '    'TODO POR AHORA LO SACAMOS PARA QUE A PARTIR DEL ID ENCUENTRE EL VALUE
                        '    'Me.ComboBox1.Text = String.Empty
                        'End If
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
                                ComboBox1.SelectionStart = ComboBox1.Text.Length
                                ComboBox1.SelectionLength = 0
                            End If
                        Else
                            RealData = String.Empty
                        End If
                    Catch ex As IndexOutOfRangeException
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                End If

                'If Not String.IsNullOrEmpty(Me.ComboBox1.Text) Then
                DataChangeProcedure(sender, New ComponentModel.CancelEventArgs)
                'RaiseEvent ItemChanged(Index.ID, Me.ComboBox1.Text)
                'End If
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
            '            Me.IsValid = False
            ComboBox1.Select()
            ComboBox1.SelectionLength = 0  ' Me.ComboBox1.Text.Length - 1
            ComboBox1.SelectionStart = 0
        Catch
        End Try
    End Sub

    Private Sub DataChangeProcedure(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Try
            RemoveHandlers()

            If ComboBox1.Text = String.Empty AndAlso RealData <> ComboBox1.Text AndAlso Index.Type <> IndexDataType.Fecha AndAlso Index.Type <> IndexDataType.Fecha_Hora Then
                RealData = ComboBox1.Text
                IsValid = True
                Exit Sub
            ElseIf ComboBox1.Text = "" Then
                IsValid = True
                Exit Sub
            ElseIf Me.Index.DropDown = IndexAdditionalType.DropDown OrElse Me.Index.DropDown = IndexAdditionalType.DropDownJerarquico AndAlso RealData <> ComboBox1.Text Then
                'Verifico que el valor pertenezca a la lista

                If DropDowndata.Contains(ComboBox1.Text.Trim) Then
                    'la descripcion pertenece a la lista
                    RealData = ComboBox1.Text
                    IsValid = True
                    Exit Sub
                Else
                    'la descripcion no pertenece a la lista
                    If UserPreferences.getValue("AllowIndexValueOutofList", UPSections.InsertPreferences, True) = False AndAlso Mode <> Modes.Search Then
                        'el codigo no pertenece a ala lista y no estan permitidos codigos fuera de la lista
                        ErrorProvider1.SetError(ComboBox1, "El texto ingresado no pertenece a la lista")
                        IsValid = False
                        Exit Sub
                    Else
                        'Me.RealData = Me.ComboBox1.Text
                        IsValid = True
                        Exit Sub
                    End If
                    'Tomas: Se permite guardar el valor aunque se le avise al usuario de que existe un error (wi:6725)
                    RealData = ComboBox1.Text
                End If
            Else
                ComboBox1.Text = RealData
                'Me.ErrorProvider1.Clear()
                IsValid = True
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
            ZClass.raiseerror(ex)
        Catch ex As ArgumentNullException
            If Me.Mode = Modes.Search Then
                ErrorProvider1.SetError(ComboBox1, "La valor ingresado es invalido")
                'Muestro el icono de error
            Else
                ComboBox1.Text = RealData
                IsValid = True
                Exit Sub
            End If
            ZClass.raiseerror(ex)
        Catch ex As InvalidCastException
            If Me.Mode = Modes.Search Then
                ErrorProvider1.SetError(ComboBox1, "La valor ingresado es invalido")
                'Muestro el icono de error
            Else
                ComboBox1.Text = RealData
                IsValid = True
                Exit Sub
            End If
            ZClass.raiseerror(ex)
        Catch ex As OutOfMemoryException
            If Me.Mode = Modes.Search Then
                ErrorProvider1.SetError(ComboBox1, "La valor ingresado es invalido")
                'Muestro el icono de error
            Else
                ComboBox1.Text = RealData
                IsValid = True
                Exit Sub
            End If
            ZClass.raiseerror(ex)
        Catch ex As OverflowException
            If Me.Mode = Modes.Search Then
                ErrorProvider1.SetError(ComboBox1, "La valor ingresado es invalido")
                'Muestro el icono de error
            Else
                ComboBox1.Text = RealData
                IsValid = True
                Exit Sub
            End If
            ZClass.raiseerror(ex)
        Catch ex As Exception
            If Me.Mode = Modes.Search Then
                ErrorProvider1.SetError(ComboBox1, "La valor ingresado es invalido")
                'Muestro el icono de error
            Else
                ComboBox1.Text = RealData
                IsValid = True
                Exit Sub
            End If
            ZClass.raiseerror(ex)
        Finally
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
                    RemoveHandler ComboBox1.TextChanged, AddressOf ComboBox1_TextChanged
                    RemoveHandler ComboBox1.SelectedIndexChanged, AddressOf ComboBox1_TextChanged
                    RemoveHandler ComboBox1.LostFocus, AddressOf ComboBox1_LostFocus
                    RemoveHandler ComboBox1.GotFocus, AddressOf ComboBox1_GotFocus
                    RemoveHandler ComboBox1.LostFocus, AddressOf ComboBox1_LostFocus
                    RemoveHandler ComboBox1.KeyDown, AddressOf ComboBox1_KeyDown
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
            isDisposed = True
        End If
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer
    Private isDisposed As Boolean
    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents ComboBox1 As ComboBox
    Public WithEvents Panel1 As ZPanel
    Friend WithEvents Button1 As ZButton
    '    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        ComboBox1 = New ComboBox
        Panel1 = New ZPanel
        ErrorProvider1 = New System.Windows.Forms.ErrorProvider
        SuspendLayout()
        '
        'ComboBox1
        '
        ComboBox1.BackColor = Color.White
        ComboBox1.Dock = DockStyle.Fill
        ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox1.ForeColor = Color.FromArgb(76, 76, 76)
        ErrorProvider1.SetIconPadding(ComboBox1, -40)
        ComboBox1.Location = New Point(0, 0)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(504, 21)
        ComboBox1.Sorted = True
        ComboBox1.TabIndex = 0
        BorderStyle = BorderStyle.Fixed3D
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
        Name = "txtDropDownIndexCtrl"
        Size = New Size(528, 40)
        ResumeLayout(False)




    End Sub
#End Region

#Region "Cambio de color al Editar el combo"

    Private Sub ComboBox1_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBox1.GotFocus
        ComboBox1.BackColor = Color.FromArgb(255, 224, 192)
    End Sub
    Private Sub ComboBox1_LostFocus(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBox1.LostFocus
        CheckDataChange()
    End Sub
    Private Sub ComboBox1_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBox1.MouseLeave
        CheckDataChange()
    End Sub
    Private Sub CheckDataChange()
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

#Region "Metodos"
    Private Sub RefreshHierarchyControl()
        ZTrace.WriteLineIf(ZTrace.IsInfo, "txtDropDownIndexCtrl - Entrando al setter de parentdata, indice: " & Index.ID)

        If Not IndexsBussinesExt.ValidateHierarchyValue(RealData, Index.ID, Index.HierarchicalParentID, _parentIndexData) Then
            Index.DataTemp = String.Empty
            Index.dataDescriptionTemp = String.Empty
            RealData = String.Empty
            RealDataDescription = String.Empty
            ComboBox1.SelectedIndex = -1
            ComboBox1.Text = String.Empty
            ComboBox1.DataSource = Nothing
            ComboBox1.Items.Clear()
            'RaiseEvent ItemChanged(Me.Index.ID, String.Empty)
        End If

        RefreshHierarchyControl(_parentIndexData)

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


