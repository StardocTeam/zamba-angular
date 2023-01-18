Imports Zamba.Core
Public Class txtSustIndexCtrl
    Inherits txtBaseIndexCtrl
    Implements IDisposable
    Private Const STR_CHAR As String = "char"
    Private Const STR_NUMERIC As String = "numeric"

#Region " Código generado por el Diseñador de Windows Forms "

    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
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
                    RemoveHandler ComboBox1.LostFocus, AddressOf ComboBox1_LostFocus
                    RemoveHandler ComboBox1.GotFocus, AddressOf ComboBox1_GotFocus
                    RemoveHandler ComboBox1.LostFocus, AddressOf ComboBox1_LostFocus
                    RemoveHandler ComboBox1.KeyDown, AddressOf ComboBox1_KeyDown
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
            isDisposed = True
        End If
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer
    Private isDisposed As Boolean
    Friend WithEvents ComboBox1 As TextBox
    Friend WithEvents Button1 As ZButton
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        ComboBox1 = New TextBox
        Button1 = New ZButton
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
        'ErrorProvider1
        '
        ErrorProvider1.ContainerControl = Me


        Button1.Text = ""
        Button1.BackgroundImage = My.Resources.appbar_magnify
        Button1.BackgroundImageLayout = ImageLayout.Zoom
        Button1.FlatStyle = FlatStyle.Flat
        Button1.Size = New Size(35, 21)
        Button1.Dock = DockStyle.Right
        '
        'txtIndexCtrl
        '
        Controls.Add(Button1)
        Controls.Add(ComboBox1)
        ComboBox1.BringToFront()
        Name = "txtSustIndexCtrl"
        Size = New Size(528, 25)
        ResumeLayout(False)

    End Sub
#End Region

#Region "Atributos, eventos"
    Private Const CERO As String = "0"
    Private Mode As Modes
    Private _docTypeId As Int64
    Private _parentIndexs As Hashtable = Nothing
    'Este flag me dice si trabajo con data1 o data2
    Private FlagData2 As Boolean
    'Formulario para las listas de Substitucion
    Private frmListaSubstitucion As frmIndexSubtitutiom

    Private _parentIndexData As String
    Public Shadows Event IndexChanged()
#End Region


    Public Sub New(ByVal docindex As IIndex, ByVal data2 As Boolean, ByVal Mode As Modes, ByVal DocTypeId As Int64, ByVal ParentIndexs As Hashtable)
        MyBase.New()

        _docTypeId = DocTypeId
        _parentIndexs = ParentIndexs

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()
        Me.Mode = Mode
        Index = docindex
        FlagData2 = data2
        Init()
        RefreshControl(Index)

        If UserPreferences.getValue("AllowInputTextInDropDownAndSustitutionIndexs", UPSections.UserPreferences, "True") = False Then
            ComboBox1.Enabled = False
        End If
    End Sub


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
                ErrorProvider1.SetError(ComboBox1, String.Empty)
            End If
        End Set
    End Property
    Private ReadOnly Property AutoSustitucionTable() As DataTable
        Get
            Return CachefrmIndexSubtitutiom.AutoSustitucionTable(Index.ID, Index.HierarchicalParentID, _parentIndexData)
        End Get
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

    Private Sub Init()
        Try
            RemoveHandlers()
            Index.DataTemp = Index.Data
            Index.DataTemp2 = Index.Data2
            Index.dataDescriptionTemp = Index.dataDescription
            Index.dataDescriptionTemp2 = Index.dataDescription2
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            AddHandlers()
        End Try
    End Sub

    Private Function GetFilterRows(ByVal index As IIndex) As DataRow()
        Dim r As DataRow()
        If index.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
            If AutoSustitucionTable.Columns(0).DataType.ToString().Contains("String") Then
                r = AutoSustitucionTable.Select("trim(Codigo)=" & IndexsBussinesExt.GetColumnToWhere(STR_CHAR, ComboBox1.Text))
            Else
                r = AutoSustitucionTable.Select("Codigo=" & IndexsBussinesExt.GetColumnToWhere(STR_NUMERIC, ComboBox1.Text))
            End If
        ElseIf AutoSustitucionTable.Columns(0).DataType.ToString().Contains("String") Then
            r = AutoSustitucionTable.Select("trim(Codigo)=" & IndexsBussinesExt.GetColumnToWhere(STR_CHAR, ComboBox1.Text))
        Else
            r = AutoSustitucionTable.Select("Codigo=" & IndexsBussinesExt.GetColumnToWhere(STR_NUMERIC, ComboBox1.Text))
        End If
        Return r
    End Function

    ''' <summary>
    ''' Carga el control
    ''' </summary>
    ''' <param name="index">Atributo del control</param>
    ''' <history>   Marcelo    Modified    14/08/09</history>
    ''' <remarks></remarks>
    Public Overrides Sub RefreshControl(ByRef index As IIndex)
        RemoveHandlers()
        Try
            Me.Index = index
            If index Is Nothing = False Then
                Me.Index.DataTemp = index.Data
                Me.Index.DataTemp2 = index.Data2
                Me.Index.Data = index.Data
                Me.Index.dataDescriptionTemp = index.dataDescription
                Me.Index.dataDescriptionTemp2 = index.dataDescription2
            End If

            If Data.IndexOf("-") = -1 _
                OrElse (Data.IndexOf("-") = 1 And index.DropDown = IndexAdditionalType.AutoSustituciónJerarquico) Then
                Try
                    If String.IsNullOrEmpty(index.dataDescription) Then
                        If AutoSustitucionTable.Rows.Count > 0 Then
                            Dim r() As DataRow
                            If Data <> String.Empty Then
                                r = GetFilterRows(index)
                            End If

                            If Not r Is Nothing AndAlso r.Length > 0 Then
                                'el codigo pertenece a la lista
                                ComboBox1.Text = r(0).Item(1).ToString()
                                Me.Index.dataDescriptionTemp = r(0).Item(1).ToString()
                                Me.Index.dataDescription = r(0).Item(1).ToString()
                            Else
                                ComboBox1.Text = Data
                            End If
                        Else
                            ComboBox1.Text = Me.Index.dataDescription
                        End If
                    Else
                        ComboBox1.Text = Me.Index.dataDescription
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                    ComboBox1.Text = Data
                End Try
            End If
            _IsValid = True
            'todo Ver xq tira esta exception
        Catch ex As ArgumentOutOfRangeException
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            AddHandlers()
        End Try
    End Sub
    Public Overrides Sub RefreshControlDataTemp(ByRef index As IIndex)
        RemoveHandlers()
        Try
            If Not String.IsNullOrEmpty(index.DataTemp) Then
                If AutoSustitucionTable.Rows.Count > 0 Then
                    Dim r() As DataRow

                    r = GetFilterRows(index)

                    If r.Length > 0 Then
                        'el codigo pertenece a la lista
                        ComboBox1.Text = r(0).Item(0).ToString()
                        Me.Index.dataDescriptionTemp = r(0).Item(1).ToString()
                    Else
                        ComboBox1.Text = index.DataTemp
                    End If
                End If
            Else
                ComboBox1.Text = String.Empty
            End If
            DataChangeProcedure(ComboBox1, New ComponentModel.CancelEventArgs)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            ComboBox1.Text = index.DataTemp
        Finally
            AddHandlers()
        End Try
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
                RaiseEvent ItemChanged(Index.ID, Value.Trim)
            End If

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
            If Not IsNothing(ComboBox1) Then
                RemoveHandler ComboBox1.Validating, AddressOf DataChangeProcedure
                AddHandler ComboBox1.Validating, AddressOf DataChangeProcedure
                RemoveHandler ComboBox1.TextChanged, AddressOf ComboBox1_TextChanged
                AddHandler ComboBox1.TextChanged, AddressOf ComboBox1_TextChanged

            End If
            If Not IsNothing(Button1) Then
                RemoveHandler Button1.Click, AddressOf AutoSustitutionProcedure
                AddHandler Button1.Click, AddressOf AutoSustitutionProcedure
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub RemoveHandlers()
        Try
            If Not IsNothing(ComboBox1) Then
                RemoveHandler ComboBox1.Validating, AddressOf DataChangeProcedure
                RemoveHandler ComboBox1.TextChanged, AddressOf ComboBox1_TextChanged
            End If
            If Not IsNothing(Button1) Then
                RemoveHandler Button1.Click, AddressOf AutoSustitutionProcedure
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub ComboBox1_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try

            Dim FlagLastCharSelected As Boolean = False
            If ComboBox1.SelectionStart = Len(ComboBox1.Text) Then
                FlagLastCharSelected = True
            End If

            _IsValid = False
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
                        ComboBox1.Text = ComboBox1.Tag.ToString()
                        If Pos2 >= 1 Then ComboBox1.SelectionStart = Pos2 - 1
                    End If

                    If Not IsNumeric(ComboBox1.Text) Then

                        'If Val(Me.ComboBox1.Text).ToString = CERO Then
                        If String.Compare(ComboBox1.Text, CERO) = 0 Then
                            ComboBox1.Text = String.Empty
                        ElseIf ComboBox1.Text.Length > 0 Then
                            ComboBox1.Text = Val(ComboBox1.Text).ToString
                            ComboBox1_TextChanged(Me, New EventArgs)
                        End If

                        '  Me.ComboBox1.Text = CERO
                        'Deberia reemplazar todas las letras no solo la ultima
                        'Try
                        '    'TODO NO SE PUEDE HACER LO SIGUIENTE PORQUE EL USUARIO PUEDE ESTAR
                        '    'ESCRIBIENDO EN CUALQUIER POSICION DEL COMBO
                        '    'HABRIA QUE HACER UN REPLACE O UN UNDO
                        '    'Me.ComboBox1.Text = Me.ComboBox1.Text.Substring(0, Me.ComboBox1.Text.Length - 1)
                        'Catch ex As IndexOutOfRangeException
                        '    Zamba.Core.ZClass.raiseerror(ex)
                        'Catch ex As Exception
                        '    Zamba.Core.ZClass.raiseerror(ex)
                        'End Try
                        ComboBox1.SelectionStart = ComboBox1.Text.Length
                        ComboBox1.SelectionLength = 0
                    End If
                    'TODO POR AHORA LO SACAMOS PARA QUE A PARTIR DEL ID ENCUENTRE EL VALUE
                    'Me.ComboBox1.Text = String.Empty
                Catch ex As IndexOutOfRangeException
                    ZClass.raiseerror(ex)
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            End If
        Catch ex As IndexOutOfRangeException
            ZClass.raiseerror(ex)
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
            If Not IsNothing(ComboBox1) Then
                ComboBox1.Select()
                ComboBox1.SelectionLength = 0  ' Me.ComboBox1.Text.Length - 1
                ComboBox1.SelectionStart = 0
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub AutoSustitutionProcedure(ByVal sender As Object, ByVal ev As EventArgs)
        Try
            RemoveHandlers()
            frmListaSubstitucion = CachefrmIndexSubtitutiom.GetFrmIndexSubtitutionControl(Index.ID, AutoSustitucionTable, Index.DropDown = IndexAdditionalType.AutoSustituciónJerarquico)

            If Me.frmListaSubstitucion.ShowDialog() = DialogResult.OK Then
                If frmListaSubstitucion.Descripcion = String.Empty Then
                    ComboBox1.Text = String.Empty
                Else
                    ComboBox1.Text = frmListaSubstitucion.Descripcion
                End If
                ComboBox1.SelectionStart = 0
                ComboBox1.SelectionLength = 0   'Me.ComboBox1.Text.Length - 1
                RealData = frmListaSubstitucion.Codigo.ToString()
                _IsValid = False
            End If
            AddHandlers()
            ComboBox1.Select()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' [Sebastian 15-09-09] Modified se corrigio el error al ingresar un codigo y luego perder el foco que no completa con la 
    ''' descripcion.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DataChangeProcedure(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Try
            RemoveHandlers()
            If String.IsNullOrEmpty(ComboBox1.Text) AndAlso RealData <> ComboBox1.Text Then
                RealData = ComboBox1.Text
                IsValid = True
                Exit Sub
            ElseIf String.IsNullOrEmpty(ComboBox1.Text) Then
                IsValid = True
                Exit Sub
            ElseIf Me.Index.DropDown = IndexAdditionalType.AutoSustitución Then
                If IsNumeric(ComboBox1.Text) AndAlso ComboBox1.Text.IndexOf("-") = -1 Then
                    'If Me.ComboBox1.Text.IndexOf("-") = -1 Then
                    'Dim r() As DataRow = Me.AutoSustitucionTable.Select("Codigo=" & CInt(Me.ComboBox1.Text))
                    '[Sebastian 15-09-09] se agrego esta validacion para el caso en que comience con cero.
                    'se le saca y se consulta por el nuero sin el cero, tal como esta en la base de datos.
                    If ComboBox1.Text.StartsWith(CERO) AndAlso String.Compare(ComboBox1.Text, CERO) AndAlso (Me.Index.Type = IndexDataType.Numerico_Largo <> 0 OrElse Me.Index.Type = IndexDataType.Numerico <> 0 OrElse Me.Index.Type = IndexDataType.Numerico_Decimales <> 0) Then
                        While ComboBox1.Text.StartsWith(CERO)
                            ComboBox1.Text = ComboBox1.Text.Remove(CERO, 1)
                        End While
                    ElseIf String.Compare(ComboBox1.Text, CERO) = 0 Then
                        ComboBox1.Text = String.Empty
                    End If
                    Dim r() As DataRow = AutoSustitucionTable.Select("Codigo='" & ComboBox1.Text.Trim & Chr(39))
                    If r.Length > 0 Then
                        'el codigo pertenece a la lista
                        '[Emiliano]Se des-comentaron las siguientes lineas debido a al bug TFS:4136
                        RealData = r(0).Item(0).ToString()
                        RealDataDescription = r(0).Item(1).ToString()
                        '[Emiliano]Se reemplazaron las siguientes lineas por las anteriores debido al bug TFS:4136
                        'Me.Index.Data = r(0).Item(0).ToString()
                        'Me.Index.dataDescription = r(0).Item(1).ToString()
                        ComboBox1.Text = r(0).Item(1).ToString()
                        IsValid = True
                        Exit Sub
                    Else
                        If UserPreferences.getValue("AllowIndexValueOutofList", UPSections.InsertPreferences, True) = False AndAlso Mode <> Modes.Search Then
                            'el codigo no pertenece a ala lista y no estan permitidos codigos fuera de la lista
                            ErrorProvider1.SetError(ComboBox1, "El texto ingresado no pertenece a la lista")
                            IsValid = False
                            Exit Sub
                        Else
                            'el codigo no pertenece a la lista
                            RealData = CInt(ComboBox1.Text).ToString()
                            'Me.Index.Data = CInt(Me.ComboBox1.Text).ToString()
                            IsValid = True
                            Exit Sub

                        End If
                    End If
                Else
                    Dim r() As DataRow
                    AutoSustitucionTable.CaseSensitive = False
                    If Not String.IsNullOrEmpty(RealData) AndAlso RealData = ComboBox1.Text Then
                        r = AutoSustitucionTable.Select("Codigo='" & RealData.Trim & Chr(39) & " or Descripcion like '%" & Trim(RealData) & "%'")
                    Else
                        r = AutoSustitucionTable.Select("Codigo='" & RealData.Trim & Chr(39))
                        If r.Length = 0 Then
                            r = AutoSustitucionTable.Select("Descripcion like '%" & Trim(ComboBox1.Text) & "%'")
                        End If
                    End If
                    'la descripcion pertenece a la lista
                    If r.Length = 1 Then
                        RealData = r(0).Item(0).ToString()
                        ComboBox1.Text = r(0).Item(1).ToString()
                        RealDataDescription = r(0).Item(1).ToString()
                        IsValid = True
                        Exit Sub
                    ElseIf r.Length > 1 Then
                        Dim i As Int32
                        For i = 0 To r.Length - 1
                            If String.Compare(RealData, CType(r(i).Item(0), String)) = 0 Then
                                ComboBox1.Text = r(i).Item(1).ToString()
                                RealDataDescription = r(0).Item(1).ToString()
                                IsValid = True
                                Exit Sub
                            End If
                        Next
                        If _IsValid = False Then
                            RealData = r(0).Item(0).ToString()
                            ComboBox1.Text = r(0).Item(1).ToString()
                            RealDataDescription = r(0).Item(1).ToString()
                            IsValid = True
                            Exit Sub
                        End If
                    Else
                        'la descripcion no pertenece a la lista
                        '                        Me.ComboBox1.Text = Me.RealData
                        ErrorProvider1.SetError(ComboBox1, "El texto ingresado no pertenece a la lista")
                        IsValid = False
                        Exit Sub
                    End If
                End If
            ElseIf Me.Index.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                If ComboBox1.Text.IndexOf("-") = -1 Then
                    '[Sebastian 15-09-09] se agrego esta validacion para el caso en que comience con cero.
                    'se le saca y se consulta por el nuero sin el cero, tal como esta en la base de datos.
                    If ComboBox1.Text.StartsWith(CERO) AndAlso String.Compare(ComboBox1.Text, CERO) <> 0 Then
                        ComboBox1.Text = ComboBox1.Text.Remove(CERO, 1)
                    ElseIf String.Compare(ComboBox1.Text, CERO) = 0 Then
                        ComboBox1.Text = String.Empty
                    End If

                    Dim r() As DataRow

                    'Indica si la descripcion coincide en algunas palabras y no en todas
                    Dim containsOperator As Boolean = False

                    'Busca el elemento por su descripción completa
                    r = AutoSustitucionTable.Select("Descripcion = '" & ComboBox1.Text.Trim & "'")

                    If r.Length > 0 Then
                        'Unicamente se busca por el código en caso de que el texto ingresado 
                        'y el del formulario de sustitucion coincidan. Esto por si hubo un
                        'ingreso erroneo de la información con duplicados
                        If frmListaSubstitucion IsNot Nothing AndAlso
                            frmListaSubstitucion.Codigo IsNot Nothing AndAlso
                            String.Compare(frmListaSubstitucion.Descripcion, ComboBox1.Text) = 0 Then
                            'Busca por el código, dando prioridad a lo seleccionado en el formulario
                            r = AutoSustitucionTable.Select("Codigo='" & frmListaSubstitucion.Codigo.ToString().Trim & Chr(39))

                            'Si por alguna razón no lo encuentra, vuelve a buscar el valor por la descripción
                            If r.Length = 0 Then
                                r = AutoSustitucionTable.Select("Descripcion = '" & ComboBox1.Text.Trim & "'")
                            End If
                        End If
                    Else
                        'Si no encuentra resultados, busca si los elementos contienen al ingresado
                        r = AutoSustitucionTable.Select("Descripcion like '%" & ComboBox1.Text.Trim & "%'")
                        containsOperator = True
                    End If

                    If r.Length > 0 Then
                        If containsOperator Then
                            'Corresponde a los casos que provienen de los filtros con operadores de tipo Contiene
                            RealData = String.Empty
                            RealDataDescription = ComboBox1.Text
                        Else
                            'Corresponden a los casos comunes, donde se completa un único valor
                            RealData = r(0).Item(0).ToString()
                            RealDataDescription = r(0).Item(1).ToString()
                            ComboBox1.Text = r(0).Item(1).ToString()
                        End If

                        IsValid = True
                        Exit Sub
                    Else
                        If UserPreferences.getValue("AllowIndexValueOutofList", UPSections.InsertPreferences, True) = False AndAlso Mode <> Modes.Search Then
                            'el codigo no pertenece a ala lista y no estan permitidos codigos fuera de la lista
                            ErrorProvider1.SetError(ComboBox1, "El texto ingresado no pertenece a la lista")
                            IsValid = False
                            Exit Sub
                        Else
                            'el codigo no pertenece a la lista
                            RealData = CInt(ComboBox1.Text).ToString()
                            IsValid = True
                            Exit Sub
                        End If
                    End If
                Else
                    Dim r() As DataRow
                    If Not String.IsNullOrEmpty(RealData) Then
                        r = AutoSustitucionTable.Select("Codigo='" & RealData.Trim & Chr(39))
                    Else
                        r = AutoSustitucionTable.Select("Descripcion='" & Trim(ComboBox1.Text) & "'")
                    End If
                    'la descripcion pertenece a la lista
                    If r.Length = 1 Then
                        RealData = r(0).Item(0).ToString()
                        ComboBox1.Text = r(0).Item(1).ToString()
                        RealDataDescription = r(0).Item(1).ToString()
                        IsValid = True
                        Exit Sub
                    ElseIf r.Length > 1 Then
                        Dim i As Int32
                        For i = 0 To r.Length - 1
                            If String.Compare(RealData, CType(r(i).Item(0), String)) = 0 Then
                                ComboBox1.Text = r(i).Item(1).ToString()
                                RealDataDescription = r(0).Item(1).ToString()
                                IsValid = True
                                Exit Sub
                            End If
                        Next
                        If _IsValid = False Then
                            RealData = r(0).Item(0).ToString()
                            ComboBox1.Text = r(0).Item(1).ToString()
                            RealDataDescription = r(0).Item(1).ToString()
                            IsValid = True
                            Exit Sub
                        End If
                    Else
                        'la descripcion no pertenece a la lista
                        '                        Me.ComboBox1.Text = Me.RealData
                        ErrorProvider1.SetError(ComboBox1, "El texto ingresado no pertenece a la lista")
                        IsValid = False
                        Exit Sub
                    End If
                End If
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

#Region "Color y perdida de foco"

    Private Sub ComboBox1_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBox1.GotFocus
        ComboBox1.BackColor = Color.FromArgb(255, 224, 192)
    End Sub
    Private Sub ComboBox1_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBox1.MouseLeave
        CheckDataChange()
    End Sub
    Private Sub ComboBox1_LostFocus(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBox1.LostFocus
        CheckDataChange()
    End Sub
    Private Sub CheckDataChange()
        RemoveHandlers()
        ComboBox1.BackColor = Color.White
        If _IsValid = False Then DataChangeProcedure(ComboBox1, New ComponentModel.CancelEventArgs)
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
        '[Tomas] Al presionar TAB ejecuta el evento datachange, no el KeyDown, por eso se comenta.
        'If e.KeyCode = Keys.Tab Then
        '    RaiseEvent TabPressed()
        'End If
    End Sub

#End Region

#Region "Metodos"
    Private Sub RefreshHierarchyControl()
        ZTrace.WriteLineIf(ZTrace.IsInfo, "txtSustIndexCtrl - Actualizando indice: " & Index.ID & " parentData: " & _parentIndexData)

        AutoSustitucionTable.Clear()
        '_AutoSubstitucionTable = IndexsBussinesExt.GetHierarchicalTableByValue(Index.ID, Index.HierarchicalParentID, _parentIndexData)

        'Se aplica un parche para que la tabla _AutoSubstitucionTable tenga 
        'el mismo nombre en sus columnas en todo el código de la clase.
        If AutoSustitucionTable.Columns.IndexOf("Description") <> -1 Then
            AutoSustitucionTable.Columns("Description").ColumnName = AutoSubstitutionBusiness.NombreColumnaDescripcion
        End If

        If Not IndexsBussinesExt.ValidateHierarchyValue(RealData, Index.ID, Index.HierarchicalParentID, _parentIndexData) Then
            Index.DataTemp = String.Empty
            Index.dataDescriptionTemp = String.Empty
            RealData = String.Empty
            RealDataDescription = String.Empty
            ComboBox1.Text = String.Empty
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
