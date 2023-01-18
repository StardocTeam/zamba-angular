
Public Class Check
    Inherits Constraint

#Region "Atributos"
    Private _CheckExpression As String
    Private _table As Table
    Private _NotForReplication As Boolean
    Private _ObjName As String
#End Region

#Region "Propiedades"
    Public Property ObjName() As String
        Get
            Return _ObjName
        End Get
        Set(ByVal value As String)
            _ObjName = value
        End Set
    End Property


    Public Property NotForReplication() As Boolean
        Get
            Return _NotForReplication
        End Get
        Set(ByVal value As Boolean)
            _NotForReplication = value
        End Set
    End Property


    Public Property CheckExpression() As String
        Get
            Return _CheckExpression
        End Get
        Set(ByVal value As String)
            _CheckExpression = value
        End Set
    End Property

    Public Property BaseTable() As Table
        Get
            Return _table
        End Get
        Set(ByVal value As Table)
            _table = value
        End Set
    End Property
#End Region


#Region "Constructores"

    Public Sub New(ByVal table As Table)
        MyBase.New(table)
        _table = table
    End Sub
#End Region

End Class
