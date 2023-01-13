
Public Class Unique
    Inherits Constraint

#Region "Atributos"
    Private _basecolumns As Generic.List(Of Column)
    Private _objname As String
#End Region

#Region "Propiedades"

    Public Property ObjName() As String
        Get
            Return _objname
        End Get
        Set(ByVal value As String)
            _objname = value
        End Set
    End Property


    Public Property BaseColumns() As Generic.List(Of Column)
        Get
            Return _basecolumns
        End Get
        Set(ByVal value As Generic.List(Of Column))
            _basecolumns = value
        End Set
    End Property

#End Region


#Region "Constructores"
    Public Sub New(ByVal columns As List(Of Column))
        MyBase.New(columns(0).Name)
        BaseColumns = columns
    End Sub
    Public Sub New(ByVal columns As List(Of Column), ByVal table As Table)
        MyBase.New(columns(0).Name, table)
        BaseColumns = columns
    End Sub
#End Region

End Class
