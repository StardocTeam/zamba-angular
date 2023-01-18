Imports System.Globalization
Imports System.Collections.Generic

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Core
''' Class	 : Core.Index
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase para crear objetos Atributos
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	26/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
<Serializable()> Public Class Index
    Inherits ZambaCore
    Implements IIndex, IDisposable


#Region " Atributos "
    Private _operator As String = "="
    Private _Data2 As String = String.Empty
    Private _DataTemp As String = String.Empty
    Private _Data As String = String.Empty
    Private _DataTemp2 As String = String.Empty
    Private _dataDescription As String = String.Empty
    Private _dataDescription2 As String = String.Empty
    Private _dataDescriptionTemp As String = String.Empty
    Private _dataDescriptionTemp2 As String = String.Empty
    Private _noIndex As Boolean = False
    Private _autoDisplay As Boolean = False
    Private _invisible As Boolean = False
    Private _obligatorio As Boolean = False
    Private _datachange As Boolean = False
    Private _autoFill As Boolean = False
    Private _visible As Boolean = False
    Private _noIndex1 As Integer
    Private _object_Type_Id As Integer = 1
    Private _autoDisplay1 As Integer
    Private _autoFill1 As Integer
    Private _len As Integer
    Private _typeIndex As Integer
    Private _invisible1 As Integer
    Private _DropDown As IndexAdditionalType
    Private _type As IndexDataType
    Private _orderSort As OrderSorts = OrderSorts.Ninguno
    Private _indexTypes As ArrayList = Nothing
    Private _dropDownList As List(Of String) = Nothing
    Private _isFull As Boolean
    Private _isLoaded As Boolean
    Private _DefaultValue As String
    Private _autoIncremental As Boolean
    Private _isReference As Boolean
    Private _enabled As Boolean = True
    Private _hierarchicalParentID As Integer = -2
    Private _hierarchicalChildID As List(Of Long)
    Private _hierarchicalDataTableName As String = String.Empty
#End Region

#Region " Propiedades "


    Public Property Enabled() As Boolean Implements IIndex.Enabled
        Get
            Return _enabled
        End Get
        Set(ByVal value As Boolean)
            _enabled = value
        End Set
    End Property

    Public Property AllowDataOutOfList() As Boolean Implements IIndex.AllowDataOutOfList

    Public Property AutoIncremental() As Boolean Implements IIndex.AutoIncremental
        Get
            Return _autoIncremental
        End Get
        Set(ByVal value As Boolean)
            _autoIncremental = value
        End Set
    End Property

    Public Property isReference() As Boolean Implements IIndex.isReference
        Get
            Return _isReference
        End Get
        Set(ByVal value As Boolean)
            _isReference = value
        End Set
    End Property

    Public Property Type() As IndexDataType Implements IIndex.Type
        Get
            Return _type
        End Get
        Set(ByVal value As IndexDataType)
            _type = value
        End Set
    End Property
    Public Property Len() As Integer Implements IIndex.Len
        Get
            Return _len
        End Get
        Set(ByVal value As Integer)
            _len = value
        End Set
    End Property
    Public Property TypeIndex() As Integer Implements IIndex.TypeIndex
        Get
            Return _typeIndex
        End Get
        Set(ByVal value As Integer)
            _typeIndex = value
        End Set
    End Property
    Public Property AutoFill() As Boolean Implements IIndex.AutoFill
        Get
            Return _autoFill
        End Get
        Set(ByVal value As Boolean)
            _autoFill = value
        End Set
    End Property
    Public Property NoIndex() As Boolean Implements IIndex.NoIndex
        Get
            Return _noIndex
        End Get
        Set(ByVal value As Boolean)
            _noIndex = value
        End Set
    End Property
    Public Property AutoDisplay() As Boolean Implements IIndex.AutoDisplay
        Get
            Return _autoDisplay
        End Get
        Set(ByVal value As Boolean)
            _autoDisplay = value
        End Set
    End Property
    Public Property Invisible() As Boolean Implements IIndex.Invisible
        Get
            Return _invisible
        End Get
        Set(ByVal value As Boolean)
            _invisible = value
        End Set
    End Property
    Public Property Object_Type_Id() As Integer Implements IIndex.Object_Type_Id
        Get
            Return _object_Type_Id
        End Get
        Set(ByVal value As Integer)
            _object_Type_Id = value
        End Set
    End Property
    Public Property AutoFill1() As Integer Implements IIndex.AutoFill1
        Get
            Return _autoFill1
        End Get
        Set(ByVal value As Integer)
            _autoFill1 = value
        End Set
    End Property
    Public Property NoIndex1() As Integer Implements IIndex.NoIndex1
        Get
            Return _noIndex1
        End Get
        Set(ByVal value As Integer)
            _noIndex1 = value
        End Set
    End Property
    Public Property AutoDisplay1() As Integer Implements IIndex.AutoDisplay1
        Get
            Return _autoDisplay1
        End Get
        Set(ByVal value As Integer)
            _autoDisplay1 = value
        End Set
    End Property
    Public Property Invisible1() As Integer Implements IIndex.Invisible1
        Get
            Return _invisible1
        End Get
        Set(ByVal value As Integer)
            _invisible1 = value
        End Set
    End Property
    Public Property IndexTypes() As ArrayList Implements IIndex.IndexTypes
        Get
            If IsNothing(_indexTypes) Then CallForceLoad(Me)
            If IsNothing(_indexTypes) Then _indexTypes = New ArrayList()

            Return _indexTypes
        End Get
        Set(ByVal value As ArrayList)
            _indexTypes = value
        End Set
    End Property
    Public Property Datachange() As Boolean Implements IIndex.Datachange
        Get
            Return _datachange
        End Get
        Set(ByVal value As Boolean)
            _datachange = value
        End Set
    End Property
    Public Property [Operator]() As String Implements IIndex.Operator
        Get
            Return _operator
        End Get
        Set(ByVal value As String)
            _operator = value
        End Set
    End Property
    Public Property DropDownList() As List(Of String) Implements IIndex.DropDownList
        Get
            If IsNothing(_dropDownList) Then CallForceLoad(Me)
            If IsNothing(_dropDownList) Then _dropDownList = New List(Of String)

            Return _dropDownList
        End Get
        Set(ByVal value As List(Of String))
            _dropDownList = value
        End Set
    End Property
    Public Property OrderSort() As OrderSorts Implements IIndex.OrderSort
        Get
            Return _orderSort
        End Get
        Set(ByVal value As OrderSorts)
            _orderSort = value
        End Set
    End Property
    Public Property dataDescription() As String Implements IIndex.dataDescription
        Get
            Return _dataDescription
        End Get
        Set(ByVal Value As String)
            If IsNothing(Value) = False Then
                _dataDescription = Value.Trim
            Else
                _dataDescription = Nothing
            End If
        End Set
    End Property
    Public Property dataDescription2() As String Implements IIndex.dataDescription2
        Get
            Return _dataDescription2
        End Get
        Set(ByVal Value As String)
            If IsNothing(Value) = False Then
                _dataDescription2 = Value.Trim
            Else
                _dataDescription2 = Nothing
            End If
        End Set
    End Property
    Public Property DataTemp() As String Implements IIndex.DataTemp
        Get
            Return _DataTemp
        End Get
        Set(ByVal Value As String)
            If IsNothing(Value) Then
                _DataTemp = Nothing
            Else
                _DataTemp = Value.Trim
            End If
        End Set
    End Property
    Public Property DataTemp2() As String Implements IIndex.DataTemp2
        Get
            Return _DataTemp2
        End Get
        Set(ByVal Value As String)
            If IsNothing(Value) Then
                _DataTemp2 = Nothing
            Else
                _DataTemp2 = Value.Trim
            End If
        End Set
    End Property
    Public Property dataDescriptionTemp() As String Implements IIndex.dataDescriptionTemp
        Get
            Return _dataDescriptionTemp
        End Get
        Set(ByVal Value As String)
            'If IsNothing(_dataDescriptionTemp) Then
            '    _dataDescriptionTemp = Nothing
            'Else
            If Not IsNothing(Value) Then
                _dataDescriptionTemp = Value.Trim
            End If
        End Set
    End Property
    Public Property dataDescriptionTemp2() As String Implements IIndex.dataDescriptionTemp2
        Get
            Return _dataDescriptionTemp2
        End Get
        Set(ByVal Value As String)
            'If IsNothing(_dataDescriptionTemp2) Then
            '    _dataDescriptionTemp2 = Nothing
            'Else
            If Not IsNothing(Value) Then
                _dataDescriptionTemp2 = Value.Trim
            End If
            ' End If
        End Set
    End Property

    Public Property HierarchicalParentID() As Integer Implements IIndex.HierarchicalParentID
        Get
            Return _hierarchicalParentID
        End Get
        Set(ByVal Value As Integer)
            _hierarchicalParentID = Value
        End Set
    End Property

    Public Property HierarchicalChildID() As List(Of Long) Implements IIndex.HierarchicalChildID
        Get
            Return _hierarchicalChildID
        End Get
        Set(ByVal Value As List(Of Long))
            _hierarchicalChildID = Value
        End Set
    End Property

    Public Property Data() As String Implements IIndex.Data
        Get

            If String.IsNullOrEmpty(_Data) Then Return _Data
            If Me.Type = IndexDataType.Fecha Then
                Try
                    Return Date.Parse(_Data).ToShortDateString
                Catch ex As Exception
                    ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString)
                End Try
                Try 'dd-MM-yyyy format
                    Dim datetime As String = _Data.Replace("-", "/")
                    Return Date.Parse(datetime).ToString()
                Catch ex As Exception
                    ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString)
                End Try
                raiseerror(New Exception("Error al parsear la fecha: " & _Data & " para el indice " & Me.Name & "(" & Me.ID & ")"))
            ElseIf Me.Type = IndexDataType.Fecha_Hora Then
                Try
                    Return DateTime.Parse(_Data).ToString()
                Catch ex As Exception
                    ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString)
                End Try
                Try 'dd-MM-yyyy format
                    Dim datetime1 As String = _Data.Replace("-", "/")
                    Return DateTime.Parse(datetime1).ToString()
                Catch ex As Exception
                    ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString)
                End Try
                raiseerror(New Exception("Error al parsear la fecha y hora: " & _Data & " para el indice " & Me.Name & "(" & Me.ID & ")"))
            Else
                Return _Data
            End If
            Return Nothing

        End Get
        Set(ByVal Value As String)

            If IsNothing(Value) = False Then
                If Value.Trim = String.Empty Then
                    If Value.Trim <> _Data Then Datachange = True
                    _Data = Value.Trim
                ElseIf Me.Type = IndexDataType.Fecha Then
                    Try
                        Dim Newdate As Date = Date.Parse(Value)
                        If Newdate.ToShortDateString <> _Data Then
                            Datachange = True
                        End If
                        _Data = Newdate.ToShortDateString
                    Catch ex As Exception
                        Datachange = True
                        _Data = Value

                    End Try
                ElseIf Me.Type = IndexDataType.Fecha_Hora Then
                    Try
                        Dim Newdate As DateTime = DateTime.Parse(Value)
                        If Newdate.ToString <> _Data Then
                            Datachange = True
                        End If
                        _Data = Newdate.ToString
                    Catch ex As Exception
                        Datachange = True
                        _Data = Value

                    End Try
                Else
                    If Value.Trim <> _Data Then
                        Datachange = True
                    End If
                    _Data = Value.Trim
                End If
            Else
                _Data = Nothing
                Datachange = True
            End If

        End Set
    End Property
    Public Property Data2() As String Implements IIndex.Data2
        Get
            If _Data2 = String.Empty Then Return _Data2
            If Me.Type = IndexDataType.Fecha Then
                Return Date.Parse(_Data2).ToShortDateString
            ElseIf Me.Type = IndexDataType.Fecha_Hora Then
                Return DateTime.Parse(_Data2).ToString()
            Else
                Return _Data2
            End If
        End Get
        Set(ByVal Value As String)
            If IsNothing(Value) = False Then
                If Value.Trim = String.Empty Then
                    If Value.Trim <> _Data2 Then Datachange = True
                    _Data2 = Value
                ElseIf Me.Type = IndexDataType.Fecha Then
                    Dim Newdate As Date = Date.Parse(Value)
                    If Newdate.ToShortDateString <> _Data2 Then
                        Datachange = True
                    End If
                    _Data2 = Newdate.ToShortDateString
                ElseIf Me.Type = IndexDataType.Fecha_Hora Then
                    Dim Newdate As DateTime = DateTime.Parse(Value)
                    If Newdate.ToString <> _Data2 Then
                        Datachange = True
                    End If
                    _Data2 = Newdate.ToString
                Else
                    If Value.Trim <> _Data2 Then
                        Datachange = True
                    End If
                    _Data2 = Value.Trim
                End If
            Else
                _Data2 = Nothing
                Datachange = True
            End If
        End Set
    End Property
    Public Property Required() As Boolean Implements IIndex.Required
        Get
            Return _obligatorio
        End Get
        Set(ByVal Value As Boolean)
            _obligatorio = Value
        End Set
    End Property
    Public Function isvalid() As Boolean Implements IIndex.isvalid

        Try
            If IsNothing(Data) = False AndAlso Data.Trim <> String.Empty Then
                Data = Data.Replace("'", "''")
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor de Data: " & Data)
                Select Case Type
                    Case IndexDataType.Numerico
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Numerico")
                        If IsNumeric(Data) = False Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Is Valid False 1")
                            Return False
                        End If
                    Case IndexDataType.Numerico_Largo
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "NumericoLargo")
                        If IsNumeric(Data) = False Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Is Valid False 2")
                            Return False
                        End If
                    Case IndexDataType.Numerico_Decimales
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "NumericoDecimales")
                        If IsNumeric(Data) = False Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Is Valid False 3")
                            Return False
                        End If
                    Case IndexDataType.Fecha
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Fecha 4")
                        Return True
                        '  Dim fecha As New Date
                        '                        Try
                        'Levanto la cadena con la fecha con el formato de argentina.
                        '       fecha = Date.Parse(_Data, New System.Globalization.CultureInfo("es-AR"))
                        '                            fecha = _Data
                        'Catch ex As Exception
                        'Return False
                        'End Try
                        'Codigo viejo
                        'If IsDate(Data) = False Then
                        'ValidateIndexTypeData = "Este Campo es una Fecha, Ingrese un dato correcto"
                        'End If
                    Case IndexDataType.Fecha_Hora
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "FechaHora 5")
                        Return True
                        '  Dim dt As DateTime
                        'Try
                        '    dt = DateTime.Parse(Data, New CultureInfo("es-AR"))
                        'Catch ex As Exception
                        'Return False
                        'End Try
                        'If IsDate(Data) = False Then
                        'ValidateIndexTypeData = "Este Campo es una Fecha, Ingrese un dato correcto"
                        'End If
                    Case IndexDataType.Moneda
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Moneda 6")
                        If IsNumeric(Data) = False Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Is Valid False")
                            Return False
                        End If
                End Select
            End If
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Is Valid Catch False " & ex.ToString)
            Return False
        End Try
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Is Valid True")
        Return True
    End Function
    Public Property DropDown() As IndexAdditionalType Implements IIndex.DropDown
        Get
            Return _DropDown
        End Get
        Set(ByVal Value As IndexAdditionalType)
            _DropDown = Value
        End Set
    End Property
    Public ReadOnly Property Column() As String Implements IIndex.Column
        Get
            Return "I" & ID
        End Get
    End Property
    ''' <summary>
    ''' Valor por defecto del indice, Configurado desde administrador
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>[Diego] 04-07-2008 created </history>
    Public Property DefaultValue() As String Implements IIndex.DefaultValue
        Get
            Return _DefaultValue
        End Get
        Set(ByVal value As String)
            _DefaultValue = value
        End Set
    End Property


    'Nueva implementación
    Public Property Visible() As Boolean Implements IIndex.Visible
        Get
            Return _visible
        End Get
        Set(ByVal Value As Boolean)
            _visible = Value
        End Set
    End Property
    Public Overrides ReadOnly Property IsFull() As Boolean
        Get
            Return _isFull
        End Get
    End Property
    Public Overrides ReadOnly Property IsLoaded() As Boolean
        Get
            Return _isLoaded
        End Get
    End Property

    'Javier: Agregado el nombre de la tabla a levantar los datos
    Public Property HierarchicalDataTableName() As String Implements IIndex.HierarchicalDataTableName
        Get
            Return _hierarchicalDataTableName
        End Get
        Set(ByVal value As String)
            _hierarchicalDataTableName = value
        End Set
    End Property

    Public Property MinValue As String Implements IIndex.MinValue


    Public Property MaxValue As String Implements IIndex.MaxValue


    Public Property SearchTermGroup As Integer Implements IIndex.SearchTermGroup


    Public Property IsSearchTermGroupParent As Boolean Implements IIndex.IsSearchTermGroupParent

#End Region

#Region " Constructores "
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="Id">Id de indice</param>
    ''' <param name="Name">Name</param>
    ''' <param name="Type">Tipo de datos</param>
    ''' <param name="Len"></param>
    ''' <param name="AutoFill"></param>
    ''' <param name="NoIndex"></param>
    ''' <param name="DropDown"></param>
    ''' <param name="AutoDisplay"></param>
    ''' <param name="Invisible"></param>
    ''' <param name="requerido"></param>
    ''' <param name="_defaultValue">Parametro Opcional que indica el valor por defecto del indice</param>
    ''' <param name="HierarchicalParentID">Parametro Opcional que indica el id del indice parent si es un indice jerarquico</param>
    ''' <param name="HierarchicalDataTableName">Parametro Opcional que indica el nombre de la tabla donde levantarán los valores</param>
    ''' <remarks></remarks>
    ''' <history>[Diego] 04-07-2008 Created
    ''' [Javier] 27-09-2011 Modified (add HierarchicalDataTableName)
    '''</history>
    Public Sub New(ByVal Id As Int32, ByVal Name As String, ByVal Type As Int32, ByVal Len As Int32, ByVal AutoFill As Boolean, ByVal NoIndex As Boolean, ByVal DropDown As Int16, ByVal AutoDisplay As Boolean, ByVal Invisible As Boolean, ByVal requerido As Int16, Optional ByVal _defaultValue As String = "", Optional ByVal HierarchicalParentID As Int32 = -2, Optional ByVal HierarchicalChildID As List(Of Long) = Nothing, Optional ByVal HierarchicalDataTableName As String = "")
        Me.ID = Id
        Me.Name = Name
        Me.Type = CType(Type, IndexDataType)
        Me.Len = Len
        Me.DropDown = CType(DropDown, IndexAdditionalType)
        Me.HierarchicalParentID = HierarchicalParentID
        Me.HierarchicalChildID = HierarchicalChildID
        Me.HierarchicalDataTableName = HierarchicalDataTableName
        If requerido = 0 Then
            Required = False
        Else
            Required = True
        End If
        DefaultValue = _defaultValue
        Data = _defaultValue
        DataTemp = _defaultValue
        Me.AutoFill = AutoFill
        If AutoFill = True Then AutoFill1 = 1
        Me.NoIndex = NoIndex
        If NoIndex = True Then NoIndex1 = 1 Else NoIndex1 = 0
        Me.AutoDisplay = AutoDisplay
        If AutoDisplay = True Then AutoDisplay1 = 1 Else AutoDisplay1 = 0
        Me.Invisible = Invisible
        If Invisible = True Then Invisible1 = 1 Else Invisible1 = 0
        'If Me.Type = IndexDataType.Alfanumerico OrElse Me.Type = IndexDataType.Alfanumerico_Largo Then
        '    [Operator] = "Contiene"
        'Else
        [Operator] = "="
        'End If
        Enabled = True
        FillIndexTypes()
    End Sub

    Public Sub New(ByVal Index As Index)

        ID = Index.ID
        Name = Index.Name
        Type = Index.Type
        Len = Index.Len
        DropDown = Index.DropDown
        Required = Index.Required
        Parent = Index.Parent
        HierarchicalParentID = Index.HierarchicalParentID
        HierarchicalChildID = Index.HierarchicalChildID
        'Javier: Agregado el nombre de la tabla a levantar los datos
        HierarchicalDataTableName = Index.HierarchicalDataTableName
        'If Index.Required = False Then
        '    Me.Required = False
        'Else
        '    Me.Required = True
        'End If

        AutoFill = Index.AutoFill
        If Index.AutoFill = True Then AutoFill1 = 1
        NoIndex = Index.NoIndex
        If Index.NoIndex = True Then NoIndex1 = 1 Else NoIndex1 = 0
        AutoDisplay = Index.AutoDisplay
        If Index.AutoDisplay = True Then AutoDisplay1 = 1 Else AutoDisplay1 = 0
        Invisible = Index.Invisible
        If Index.Invisible = True Then Invisible1 = 1 Else Invisible1 = 0
        'If Me.Type = IndexDataType.Alfanumerico OrElse Me.Type = IndexDataType.Alfanumerico_Largo Then
        '    [Operator] = "Contiene"
        'Else
        [Operator] = "="
        'End If
        Enabled = True
        isReference = Index.isReference
        FillIndexTypes()
    End Sub

    Public Sub New(ByVal Index As Index, ByVal InheritData As Boolean)
        Me.New(Index)

        If InheritData Then
            Data = Index.Data
            DataTemp = Index.DataTemp
            Data2 = Index.Data2
            DataTemp2 = Index.DataTemp2
            dataDescription = Index.dataDescription
            dataDescriptionTemp = Index.dataDescriptionTemp
            dataDescription2 = Index.dataDescription2
            dataDescriptionTemp2 = Index.dataDescriptionTemp2
            [Operator] = Index.Operator
            OrderSort = Index.OrderSort
        End If

    End Sub

    Public Sub New()
        Enabled = True
    End Sub

    'Javier: Agregado el nombre de la tabla a levantar los datos
    Public Sub New(ByVal indexName As String, ByVal Index_Id As Integer, ByVal indexType As IndexDataType, ByVal Index_Len As Integer, ByVal DropDown As IndexAdditionalType, Optional ByVal HierarchicalParentID As Int32 = -2, Optional ByVal HierarchicalChildID As List(Of Long) = Nothing, Optional ByVal HierarchicalDataTableName As String = "")
        Me.New()
        Name = indexName
        ID = Index_Id
        Type = indexType
        Len = Index_Len
        Me.DropDown = DropDown
        Me.HierarchicalParentID = HierarchicalParentID
        Me.HierarchicalChildID = HierarchicalChildID
        Me.HierarchicalDataTableName = HierarchicalDataTableName
        'If Me.Type = IndexDataType.Alfanumerico OrElse Me.Type = IndexDataType.Alfanumerico_Largo Then
        '    Me.[Operator] = "Contiene"
        'Else
        [Operator] = "="
        'End If
        Enabled = True
    End Sub

    Public Sub New(ByVal id As Int64, ByVal name As String)
        Me.New()
        Me.ID = id
        Me.Name = name
    End Sub

#End Region
    Private Sub FillIndexTypes()
        IndexTypes.Add("Numerico")
        IndexTypes.Add("Numerico_Largo")
        IndexTypes.Add("Numerico_Decimales")
        IndexTypes.Add("Fecha")
        IndexTypes.Add("Fecha_Hora")
        IndexTypes.Add("Moneda")
        IndexTypes.Add("Alfanumerico")
        IndexTypes.Add("Alfanumerico_Largo")
        IndexTypes.Add("Si_No")
    End Sub
    'Private Sub LoadAdditionalData()
    '    Select Case Me._DropDown
    '        Case IndexAdditionalType.DropDown
    '            Me.DropDownList = Index_factory.retrieveArraylist(Me.Id)
    '        Case IndexAdditionalType.AutoSustitución
    '    End Select
    'End Sub

    Public Shared Function ValidateIndexTypeData(ByVal Data As String, ByVal IndexType As Int32) As String
        Try
            Data = Replace(Data, "'", "''")
        Catch ex As Exception
        End Try

        Try
            If IsNothing(Data) = False OrElse Data.Trim <> String.Empty Then
                Select Case IndexType
                    Case 1
                        If IsNumeric(Data) = False Then
                            ValidateIndexTypeData = "Este Campo es Numerico, Ingrese un dato correcto"
                        End If
                    Case 2
                        If IsNumeric(Data) = False Then
                            ValidateIndexTypeData = "Este Campo es Numerico, Ingrese un dato correcto"
                        End If
                    Case 3
                        Dim g As Decimal
                        If IsNumeric(Data) = False Then
                            ValidateIndexTypeData = "Este Campo es Numerico, Ingrese un dato correcto"
                        ElseIf Decimal.TryParse(Data, g) = False Then
                            ValidateIndexTypeData = "Este Campo es Numerico, Ingrese un dato correcto"
                        ElseIf Data.Split(Char.Parse(".")).Length > 2 Then
                            ValidateIndexTypeData = "Este Campo es Numerico, Ingrese un dato correcto"
                        ElseIf Data.Split(Char.Parse(",")).Length > 2 Then
                            ValidateIndexTypeData = "Este Campo es Numerico, Ingrese un dato correcto"
                        ElseIf Data.Contains(".,") Then
                            ValidateIndexTypeData = "Este Campo es Numerico, Ingrese un dato correcto"
                        ElseIf Data.Contains(",.") Then
                            ValidateIndexTypeData = "Este Campo es Numerico, Ingrese un dato correcto"
                        End If
                    Case 4
                        Dim fecha As New Date
                        Try
                            'Levanto la cadena con la fecha con el formato de argentina.
                            fecha = Date.Parse(Data, New CultureInfo("es-AR"))
                            If fecha.Year < 1901 Then
                                ValidateIndexTypeData = "Este Campo es una Fecha con el Formato: dia/mes/año"
                            End If
                        Catch ex As Exception
                            ValidateIndexTypeData = "Este Campo es una Fecha con el Formato: dia/mes/año"
                        End Try
                        'Codigo viejo
                        'If IsDate(Data) = False Then
                        'ValidateIndexTypeData = "Este Campo es una Fecha, Ingrese un dato correcto"
                        'End If
                    Case 5
                        Dim dt As DateTime
                        Try
                            dt = DateTime.Parse(Data, New CultureInfo("es-AR"))
                            If dt.Year < 1901 Then
                                ValidateIndexTypeData = "Este Campo es una Fecha con el Formato: dia/mes/año"
                            End If
                        Catch ex As Exception
                            ValidateIndexTypeData = "Este Campo es de Fecha y Hora, Ingrese un dato correcto"
                        End Try
                        'If IsDate(Data) = False Then
                        'ValidateIndexTypeData = "Este Campo es una Fecha, Ingrese un dato correcto"
                        'End If
                    Case 6
                        Dim g As Decimal
                        If IsNumeric(Data) = False Then
                            ValidateIndexTypeData = "Este Campo es Numerico, Ingrese un dato correcto"
                        ElseIf Decimal.TryParse(Data, g) = False Then
                            ValidateIndexTypeData = "Este Campo es Numerico, Ingrese un dato correcto"
                        ElseIf Data.Split(Char.Parse(".")).Length > 2 Then
                            ValidateIndexTypeData = "Este Campo es Numerico, Ingrese un dato correcto"
                        ElseIf Data.Split(Char.Parse(",")).Length > 2 Then
                            ValidateIndexTypeData = "Este Campo es Numerico, Ingrese un dato correcto"
                        ElseIf Data.Contains(".,") Then
                            ValidateIndexTypeData = "Este Campo es Numerico, Ingrese un dato correcto"
                        ElseIf Data.Contains(",.") Then
                            ValidateIndexTypeData = "Este Campo es Numerico, Ingrese un dato correcto"
                        End If
                End Select
            End If
        Catch
        End Try
        If Data.IndexOf("'") > 0 Then
            ValidateIndexTypeData = "el atributo no puede contener apostrofes, Ingrese un dato correcto"
        End If
        Return ValidateIndexTypeData
    End Function
    Public Shared Function GetData(ByVal index() As Index) As ArrayList
        Dim i As Integer
        Dim r As New ArrayList
        For i = 0 To index.Length - 1
            r.Add(index(i).Data)
        Next
        Return r
    End Function
    Public Shared Function GetData2(ByVal index() As Index) As ArrayList
        Dim i As Integer
        Dim r As New ArrayList
        For i = 0 To index.Length - 1
            r.Add(index(i).Data2)
        Next
        Return r
    End Function
    Public Shared Function GetOperators(ByVal index() As Index) As ArrayList
        Dim i As Integer
        Dim r As New ArrayList
        For i = 0 To index.Length - 1
            r.Add(index(i).[Operator])
        Next
        Return r
    End Function
    Public Shared Function GetOrder(ByVal index() As Index) As ArrayList
        Dim i As Integer
        Dim r As New ArrayList
        For i = 0 To index.Length - 1
            r.Add(index(i).OrderSort)
        Next
        Return r
    End Function
    Public Shared Function GetTypes(ByVal index() As Index) As ArrayList
        Dim i As Integer
        Dim r As New ArrayList
        For i = 0 To index.Length - 1
            r.Add(index(i).Type)
        Next
        Return r
    End Function
    Public Shared Function GetIds(ByVal index() As Index) As ArrayList
        Dim i As Integer
        Dim r As New ArrayList
        For i = 0 To index.Length - 1
            r.Add(index(i).ID)
        Next
        Return r
    End Function
    Public Sub SetData(ByVal data As String, Optional ByVal Data1 As Boolean = True)
        If Data1 Then
            _Data = data
        Else
            _Data2 = data
        End If
    End Sub

    Private _disposed As Boolean
    Public Overrides Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)

        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        'Para evitar que se haga dispose 2 veces
        If Not _disposed Then
            If disposing Then
                Dim i As Int16

                If Not IsNothing(_dropDownList) Then
                    For i = 0 To _dropDownList.Count - 1
                        _dropDownList(i) = Nothing
                    Next
                    _dropDownList.Clear()
                End If
            End If

            ' Indicates that the instance has been disposed.
            _disposed = True

            _dropDownList = Nothing
            _type = Nothing
        End If
    End Sub
    Public Overrides Sub FullLoad()
        If Not _isFull AndAlso ID <> 0 Then

        End If
    End Sub
    Public Overrides Sub Load()
        If Not _isLoaded AndAlso ID <> 0 Then

        End If
    End Sub

    Public Function CloneScheme() As IIndex Implements IIndex.CloneScheme
        Throw New NotImplementedException()
    End Function
End Class

