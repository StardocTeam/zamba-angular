Imports System.Collections.Generic

Public Class Table
#Region "Atributos"
    Private _name As String
    Private _columns As List(Of Column)
    Private _primaryKeyColumnNames As List(Of String)
    'Private _constraint As List(Of Constraint)
#End Region

#Region "Propiedades"
    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property
    Public Property Columns() As List(Of Column)
        Get
            Return _columns
        End Get
        Set(ByVal value As List(Of Column))
            _columns = value
        End Set
    End Property
    Public Property PrimaryKeyColumnNames() As List(Of String)
        Get
            Return _primaryKeyColumnNames
        End Get
        Set(ByVal value As List(Of String))
            _primaryKeyColumnNames = value
        End Set
    End Property

    'Public Property Constraints() As List(Of Constraint)
    '    Get
    '        Return _constraint
    '    End Get
    '    Set(ByVal value As List(Of Constraint))
    '        _constraint = value
    '    End Set
    'End Property
    
#End Region


#Region "Contructores"
    Public Sub New(ByVal name As String)
        _name = name
        _columns = New List(Of Column)
        '_constraint = New List(Of Constraint)
    End Sub
    Public Sub New(ByVal name As String, ByVal columns As List(Of Column)) ', ByVal constraints As List(Of Constraint))
        _name = name
        _columns = columns
        '_constraint = constraints
    End Sub
#End Region
End Class