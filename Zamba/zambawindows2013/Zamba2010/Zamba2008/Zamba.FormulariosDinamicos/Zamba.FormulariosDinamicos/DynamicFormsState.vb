Public Class DynamicFormState

    Private _isFormFinalized As Boolean
    Private _formValues As New List(Of Hashtable)
    Private _doctypeid As Int64
    Private _formid As Int64
    Private _edit As Boolean
    Private _useConditions As Boolean
    Private _useIndexProperties As Boolean

    Sub New(ByVal doctypeid As Int64)
        _doctypeid = doctypeid
    End Sub


    Public Property Doctypeid() As Int64
        Get
            Return _doctypeid
        End Get
        Set(ByVal value As Int64)
            _doctypeid = value
        End Set
    End Property

    Public Property Edit() As Boolean
        Get
            Return _edit
        End Get
        Set(ByVal value As Boolean)
            _edit = value
        End Set
    End Property
    Public Property Formid() As Int64
        Get
            Return _formid
        End Get
        Set(ByVal value As Int64)
            _formid = value
        End Set
    End Property

    Public Property IsFormFinalized() As Boolean
        Get
            Return _isFormFinalized
        End Get
        Set(ByVal value As Boolean)
            _isFormFinalized = value
        End Set
    End Property
    Public Property FormValues() As List(Of Hashtable)
        Get
            Return _formValues
        End Get
        Set(ByVal value As List(Of Hashtable))
            _formValues = value
        End Set
    End Property
    Public Property UseConditions() As Boolean
        Get
            Return _useConditions
        End Get
        Set(ByVal value As Boolean)
            _useConditions = value
        End Set
    End Property

    Public Property UseIndexProperties() As Boolean
        Get
            Return _useIndexProperties
        End Get
        Set(ByVal value As Boolean)
            _useIndexProperties = value
        End Set
    End Property



End Class
