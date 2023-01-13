Imports System.Collections.Generic


Public Class oLinked
    Implements IoLinked
    ' Private _doctype As DocType
    ' Private _docids As ArrayList

    'Public Property DOCTYPE() As DocType
    '    Get
    '        Return _doctype
    '    End Get
    '    Set(ByVal value As DocType)
    '        _doctype = value
    '    End Set
    'End Property
    'Public Property DocIds() As ArrayList
    '    Get
    '        Return _docids
    '    End Get
    '    Set(ByVal value As ArrayList)
    '        _docids = value
    '    End Set
    'End Property
    'Public Sub New(ByVal DOCTYPEID As Int32)
    '    _doctype = New DocType(DOCTYPEID)
    '    _docids = New ArrayList
    'End Sub
    Private _lista As List(Of IResult)

    Public Property Results() As List(Of IResult) Implements IoLinked.Results
        Get
            Return _lista
        End Get
        Set(ByVal value As List(Of IResult))
            _lista = value
        End Set
    End Property
    Public Sub New()
        _lista = New List(Of IResult)
    End Sub
End Class
