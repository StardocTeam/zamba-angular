
Public Class PrimaryKey
    Inherits Constraint

#Region "Atributos"
    Dim _baseColumns As List(Of Column)
    Dim _relationName As String
    Dim _ascOrder As Boolean
#End Region

#Region "Propiedades"
    Property BaseColumns() As List(Of Column)
        Get
            Return _baseColumns
        End Get
        Set(ByVal value As List(Of Column))
            _baseColumns = value
        End Set
    End Property
    Property RelationName() As String
        Get
            Return _relationName
        End Get
        Set(ByVal value As String)
            _relationName = value
        End Set
    End Property
    Property AscOrder() As Boolean
        Get
            Return _ascOrder
        End Get
        Set(ByVal value As Boolean)
            _ascOrder = value
        End Set
    End Property
#End Region

#Region "Constructor"
    Public Sub New(ByVal cBaseColumns As List(Of Column), ByVal tBaseTable As Table)
        MyBase.New(cBaseColumns(0).Name)
        Me._baseColumns = cBaseColumns
        Me.Table = tBaseTable
    End Sub
    Public Sub New(ByVal columnName As String)
        MyBase.New(columnName)
    End Sub
#End Region

End Class
