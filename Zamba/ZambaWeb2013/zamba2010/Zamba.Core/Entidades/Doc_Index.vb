

Public MustInherit Class Doc_Index1
    Implements IDoc_Index1

#Region "Atributos"
    Private _dropDownList As ArrayList = Nothing
    Private _id As Integer
    Private _name As String = String.Empty
    Private _type As IndexDataType
    Private _len As Integer
    Private _autoFill As Boolean = False
    Private _noIndex As Boolean = False
    Private _DropDown As IndexAdditionalType = IndexAdditionalType.LineText
    Private _autoDisplay As Boolean = False
    Private _invisible As Boolean = False
    Private _soloLectura As Boolean = False
    Private _object_Type_Id As Integer = 1
    Private _indexTypes As New ArrayList()
    Private _data As String = String.Empty
    Private _data2 As String = String.Empty
    Private _operator As String = "="
    Private _orderSort As OrderSorts = OrderSorts.Ninguno
    'Private AutoFill1 As Integer
    'Private NoIndex1 As Integer
    'Private AutoDisplay1 As Integer
    'Private Invisible1 As Integer
#End Region

#Region "Constructores"
    Public Sub New(ByVal Id As Int32, ByVal Name As String, ByVal Type As Int32, ByVal Len As Int32, ByVal AutoFill As Boolean, ByVal NoIndex As Boolean, ByVal DropDown As Int16, ByVal AutoDisplay As Boolean, ByVal Invisible As Boolean, Optional ByVal data As String = "")
        _id = Id
        _Name = Name
        _Type = CType(Type, IndexDataType)
        _Len = Len
        _DropDown = CType(DropDown, IndexAdditionalType)
        _Data = data
        _AutoFill = AutoFill
        'If AutoFill = True Then AutoFill1 = 1 Else AutoFill1 = 0
        _NoIndex = NoIndex
        'If NoIndex = True Then NoIndex1 = 1 Else NoIndex1 = 0
        _AutoDisplay = AutoDisplay
        'If AutoDisplay = True Then AutoDisplay1 = 1 Else AutoDisplay1 = 0
        _Invisible = Invisible
        'If Invisible = True Then Invisible1 = 1 Else Invisible1 = 0

        'FillIndexTypes()
    End Sub
    Public Sub New(ByVal index As DataRow)
        MyBase.New()
        FillIndexData(index)
    End Sub
    Public Sub New(ByVal Index As Index)
        _Id = Convert.ToInt32(Index.ID)
        _Name = Index.Name
        _Type = Index.Type
        _Len = Index.Len
        _DropDown = Index.DropDown
        _Data = Index.Data
        _AutoFill = AutoFill
        'If AutoFill = True Then AutoFill1 = 1 Else AutoFill1 = 0
        _NoIndex = NoIndex
        'If NoIndex = True Then NoIndex1 = 1 Else NoIndex1 = 0
        _AutoDisplay = AutoDisplay
        'If AutoDisplay = True Then AutoDisplay1 = 1 Else AutoDisplay1 = 0
        _Invisible = Invisible
        'If Invisible = True Then Invisible1 = 1 Else Invisible1 = 0
    End Sub
    'Public Sub New(ByVal Id As Int32, ByVal Name As String, ByVal Type As Int32, ByVal Len As Int32, ByVal AutoFill As Boolean, ByVal NoIndex As Boolean, ByVal DropDown As Int16, ByVal AutoDisplay As Boolean, ByVal Invisible As Boolean)
    '    Me.Id = Id
    '    Me.Name = Name
    '    Me.Type = Type
    '    Me.Len = Len
    '    Me.DropDown = DropDown

    '    Me.AutoFill = AutoFill
    '    If AutoFill = True Then AutoFill1 = 1 Else AutoFill1 = 0
    '    Me.NoIndex = NoIndex
    '    If NoIndex = True Then NoIndex1 = 1 Else NoIndex1 = 0
    '    Me.AutoDisplay = AutoDisplay
    '    If AutoDisplay = True Then AutoDisplay1 = 1 Else AutoDisplay1 = 0
    '    Me.Invisible = Invisible
    '    If Invisible = True Then Invisible1 = 1 Else Invisible1 = 0
    'End Sub
    Public Sub New()

    End Sub
#End Region

#Region "Propiedades"

    Public Property [Operator]() As String Implements IDoc_Index1.[Operator]
        Get
            Return _operator
        End Get
        Set(ByVal value As String)
            _operator = value
        End Set
    End Property
    Public Property AutoDisplay() As Boolean Implements IDoc_Index1.AutoDisplay
        Get
            Return _autoDisplay
        End Get
        Set(ByVal value As Boolean)
            _autoDisplay = value
        End Set
    End Property
    Public ReadOnly Property Column() As String Implements IDoc_Index1.Column
        Get
            Return "I" & Id
        End Get
    End Property
    Public Property Data() As String Implements IDoc_Index1.Data
        Get
            Return _data
        End Get
        Set(ByVal value As String)
            _data = value
        End Set
    End Property
    Public Property Data2() As String Implements IDoc_Index1.Data2
        Get
            Return _data2
        End Get
        Set(ByVal value As String)
            _data2 = value
        End Set
    End Property
    Public Property DropDown() As IndexAdditionalType Implements IDoc_Index1.DropDown
        Get
            Return Me._DropDown
        End Get
        Set(ByVal Value As IndexAdditionalType)
            Me._DropDown = Value
            'TODO Falta ver esto cuando se llama y para que
            ''''''''''''            LoadAdditionalData()
        End Set
    End Property
    Public Property AutoFill() As Boolean Implements IDoc_Index1.AutoFill
        Get
            Return _autoFill
        End Get
        Set(ByVal value As Boolean)
            _autoFill = value
        End Set
    End Property
    Public Property Id() As Integer Implements IDoc_Index1.Id
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property
    Public Property IndexTypes() As ArrayList Implements IDoc_Index1.IndexTypes
        Get
            Return _indexTypes
        End Get
        Set(ByVal value As ArrayList)
            _indexTypes = value
        End Set
    End Property
    Public Property Invisible() As Boolean Implements IDoc_Index1.Invisible
        Get
            Return _invisible
        End Get
        Set(ByVal value As Boolean)
            _invisible = value
        End Set
    End Property
    Public Property Len() As Integer Implements IDoc_Index1.Len
        Get
            Return _len
        End Get
        Set(ByVal value As Integer)
            _len = value
        End Set
    End Property
    Public Property Name() As String Implements IDoc_Index1.Name
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property
    Public Property NoIndex() As Boolean Implements IDoc_Index1.NoIndex
        Get
            Return _noIndex
        End Get
        Set(ByVal value As Boolean)
            _noIndex = value
        End Set
    End Property
    Public Property Object_Type_Id() As Integer Implements IDoc_Index1.Object_Type_Id
        Get
            Return _object_Type_Id
        End Get
        Set(ByVal value As Integer)
            _object_Type_Id = value
        End Set
    End Property
    Public Property OrderSort() As OrderSorts Implements IDoc_Index1.OrderSort
        Get
            Return _orderSort
        End Get
        Set(ByVal value As OrderSorts)
            _orderSort = value
        End Set
    End Property
    Public Property SoloLectura1() As Boolean Implements IDoc_Index1.SoloLectura1
        Get
            Return _soloLectura
        End Get
        Set(ByVal value As Boolean)
            _soloLectura = value
        End Set
    End Property
    Public Property Type() As IndexDataType Implements IDoc_Index1.Type
        Get
            Return _type
        End Get
        Set(ByVal value As IndexDataType)
            _type = value
        End Set
    End Property
    Public Property DropDownList() As ArrayList Implements IDoc_Index1.DropDownList
        Get
            Return _dropDownList
        End Get
        Set(ByVal value As ArrayList)
            _dropDownList = value
        End Set
    End Property
#End Region

    Public Event LogError() Implements IDoc_Index1.LogError
    Public MustOverride Function Validate() As Boolean Implements IDoc_Index1.Validate
    Public MustOverride Function Validate2() As Boolean Implements IDoc_Index1.Validate2
    Public Sub FillIndexData(ByVal index2 As DataRow) Implements IDoc_Index1.FillIndexData
        Dim index As DataRow = index2
        Me.Id = Convert.ToInt32(index("INDEX_ID"))
        Me.Name = index("INDEX_NAME")
        Me.Type = DirectCast(Int32.Parse(index("INDEX_TYPE")), IndexDataType)
        Me.Len = Convert.ToInt32(index("INDEX_LEN"))
        Me.DropDown = DirectCast(Convert.ToInt32(index("DROPDOWN")), IndexAdditionalType)

        Me.AutoFill = Convert.ToBoolean(Convert.ToInt32(index("AUTOFILL")))
        'If AutoFill = True Then AutoFill1 = 1 Else AutoFill1 = 0
        Me.NoIndex = Convert.ToBoolean(Convert.ToInt32(index("NOINDEX")))
        'If NoIndex = True Then NoIndex1 = 1 Else NoIndex1 = 0
        Me.AutoDisplay = Convert.ToBoolean(Convert.ToInt32(index("AUTODISPLAY")))
        'If AutoDisplay = True Then AutoDisplay1 = 1 Else AutoDisplay1 = 0
        Me.Invisible = Convert.ToBoolean(Convert.ToInt32(index("INVISIBLE")))
        'If Invisible = True Then Invisible1 = 1 Else Invisible1 = 0
    End Sub
End Class


