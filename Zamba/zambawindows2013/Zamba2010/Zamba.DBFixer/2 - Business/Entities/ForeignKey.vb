
Public Class ForeignKey
    Inherits Constraint

    'TODO: volar el primary Key. Esta es siempre la columna base de la que hereda foreignKey->Constraint->Column.

#Region "Atributos"
    Private _refColumn As List(Of Column)
    Private _baseColumn As List(Of Column)
    Dim _relationName As String
    Dim _baseTable As Table
    Dim _refTable As Table
    Dim _onDeleteCascade As Boolean = False
    Dim _onUpdateCascade As Boolean = False
    Dim _OnCheckForReplication As Boolean = False
    Private _objname As String

#End Region

#Region "Propiedades"
    Public Property BaseColumn() As List(Of Column)
        Get
            Return _baseColumn
        End Get
        Set(ByVal value As List(Of Column))
            _baseColumn = value
        End Set
    End Property
    'Public Property BaseTable() As Table
    '    Get
    '        Return _baseTable
    '    End Get
    '    Set(ByVal value As Table)
    '        _baseTable = value
    '    End Set
    'End Property

    Public Property ObjName() As String
        Get
            Return _objname
        End Get
        Set(ByVal value As String)
            _objname = value
        End Set
    End Property

    Public Property RefColumn() As List(Of Column)
        Get
            Return _refColumn
        End Get
        Set(ByVal value As List(Of Column))
            _refColumn = value
        End Set
    End Property
    'Public Property RefTable() As Table
    '    Get
    '        Return _refTable
    '    End Get
    '    Set(ByVal value As Table)
    '        _refTable = value
    '    End Set
    'End Property
    Property RelationName() As String
        Get
            Return _relationName
        End Get
        Set(ByVal value As String)
            _relationName = value
        End Set
    End Property
    Public Property OnUpdateCascade() As Boolean
        Get
            Return _onUpdateCascade
        End Get
        Set(ByVal value As Boolean)
            _onUpdateCascade = value
        End Set
    End Property
    Public Property OnDeleteCascade() As Boolean
        Get
            Return _onDeleteCascade
        End Get
        Set(ByVal value As Boolean)
            _onDeleteCascade = value
        End Set
    End Property

    Public Property OnCheckForReplication() As Boolean
        Get
            Return _OnCheckForReplication
        End Get
        Set(ByVal value As Boolean)
            _OnCheckForReplication = value
        End Set
    End Property

#End Region

#Region "Constructores"
    Public Sub New(ByVal columnName As String)
        MyBase.New(columnName)
    End Sub
    Public Sub New(ByVal columnName As String, ByVal primaryKey As List(Of Column), ByVal foreignKey As List(Of Column))
        MyBase.New(String.Empty)
        _baseColumn = primaryKey
        _refColumn = foreignKey
    End Sub
    Public Sub New(ByVal cBaseColumn As List(Of Column), ByVal tBaseTable As Table, ByVal cRefColumn As List(Of Column), ByVal tRefTable As Table, ByVal pOnUpdateCascade As Boolean, ByVal pOnDeleteCascade As Boolean, ByVal pOnCheckForReplication As Boolean)
        MyBase.New(String.Empty)
        Me._baseColumn = cBaseColumn
        Me._baseTable = tBaseTable
        Me._refColumn = cRefColumn
        Me._refTable = tRefTable
        Me._onDeleteCascade = pOnDeleteCascade
        Me._onUpdateCascade = pOnUpdateCascade
        Me._OnCheckForReplication = pOnCheckForReplication
    End Sub
#End Region

End Class