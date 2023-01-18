Imports System.Collections.Generic
Imports Zamba.Framework


Namespace Searchs
    Public Class Search
        Implements ISearch

#Region "Atributos"
        Private _indexs As IIndex() = Nothing
        Private _docGroupId As Long
        Private _doctypesIds() As Int64 = Nothing
        Private _raiseResults As Boolean = False
        Private _parentName As String = String.Empty
        Private _textSearchInAllIndexs As String = String.Empty
        Private _blnSearchInAllDocsType As Boolean = False
        Private _advanceSearch As Boolean
        Private _advanceSearchFilter As String
        Private _notessearch As String = String.Empty
        Private _textsearch As String = String.Empty
#End Region

#Region "Propiedades"

        ' ivan 21/12/15 - agreagdo desde branch principal.
        Public Property NotesSearch() As String Implements ISearch.NotesSearch
            Get
                Return _notessearch
            End Get
            Set(ByVal value As String)
                _notessearch = value
            End Set
        End Property

        ' ivan 21/12/15 - agreagdo desde branch principal.
        Public Property Textsearch() As String Implements ISearch.Textsearch
            Get
                Return _textsearch
            End Get
            Set(ByVal value As String)
                _textsearch = value
            End Set
        End Property

        Public Property blnSearchInAllDocsType() As Boolean Implements ISearch.blnSearchInAllDocsType

            Get
                Return _blnSearchInAllDocsType
            End Get
            Set(ByVal value As Boolean)
                _blnSearchInAllDocsType = value
            End Set
        End Property


        Public Property TextSearchInAllIndexs() As String Implements ISearch.TextSearchInAllIndexs
            Get
                Return _textSearchInAllIndexs
            End Get
            Set(ByVal value As String)
                _textSearchInAllIndexs = value
            End Set
        End Property


        Public Property RaiseResults() As Boolean Implements ISearch.RaiseResults
            Get
                Return _raiseResults
            End Get
            Set(ByVal value As Boolean)
                _raiseResults = value
            End Set
        End Property
        Public Property DoctypesIds() As Int64() Implements ISearch.DoctypesIds
            Get
                Return _doctypesIds
            End Get
            Set(ByVal value As Int64())
                _doctypesIds = value
            End Set
        End Property
        Public Property DocGroupId() As Long Implements ISearch.DocGroupId
            Get
                Return _docGroupId
            End Get
            Set(ByVal value As Long)
                _docGroupId = value
            End Set
        End Property

        Public Property Indexs() As IIndex() Implements ISearch.Indexs
            Get
                Return _indexs
            End Get
            Set(ByVal value As IIndex())
                _indexs = value
            End Set
        End Property
        Public Property ParentName() As String Implements ISearch.ParentName
            Get
                Return _parentName
            End Get
            Set(ByVal value As String)
                _parentName = value
            End Set
        End Property
        Public Property AdvanceSearch() As Boolean Implements ISearch.AdvanceSearch
            Get
                Return _advanceSearch
            End Get
            Set(ByVal value As Boolean)
                _advanceSearch = value
            End Set
        End Property
        Public Property AdvanceSearchFilter() As String Implements ISearch.AdvanceSearchFilter
            Get
                Return _advanceSearchFilter
            End Get
            Set(ByVal value As String)
                _advanceSearchFilter = value
            End Set
        End Property
#End Region

#Region "Constructores"
        Public Sub New(ByVal indexs() As IIndex, ByVal DoctypesIds As Int64(), ByVal RaiseResults As Boolean, ByVal ParentName As String)
            _indexs = indexs
            _doctypesIds = DoctypesIds
            _raiseResults = RaiseResults
            _parentName = ParentName
            _advanceSearch = False
            _advanceSearchFilter = Nothing
        End Sub
        Public Sub New(ByVal indexs() As IIndex, ByVal doctypesIds As ArrayList, ByVal RaiseResults As Boolean, ByVal ParentName As String)
            _indexs = indexs

            ReDim Me.DoctypesIds(doctypesIds.Count - 1)
            doctypesIds.CopyTo(Me.DoctypesIds)
            _raiseResults = RaiseResults
            _parentName = ParentName
            _advanceSearch = False
            _advanceSearchFilter = Nothing
        End Sub
        Public Sub New(ByVal indexs() As IIndex, ByVal txtSearchInAllIndex As String, ByVal InAllDocTypes As Boolean, ByVal doctypesIds As ArrayList, ByVal RaiseResults As Boolean, ByVal ParentName As String)
            _indexs = indexs
            _textSearchInAllIndexs = txtSearchInAllIndex
            _blnSearchInAllDocsType = InAllDocTypes


            ReDim Me.DoctypesIds(doctypesIds.Count - 1)
            doctypesIds.CopyTo(Me.DoctypesIds)

            _raiseResults = RaiseResults
            _parentName = ParentName
            _advanceSearch = False
            _advanceSearchFilter = Nothing
        End Sub
        Public Sub New(ByVal index As IIndex, ByVal DoctypeId As Int64)
            _indexs = New IIndex(0) {index}

            _doctypesIds = New Int64() {DoctypeId}
            _raiseResults = False
            _parentName = String.Empty
            _advanceSearch = False
            _advanceSearchFilter = Nothing
        End Sub
        Public Sub New(ByVal indexs As Generic.List(Of IIndex), ByVal DoctypeId As Int64)
            _indexs = indexs.ToArray()

            _doctypesIds = New Int64() {DoctypeId}
            _raiseResults = False
            _parentName = String.Empty
            _advanceSearch = False
            _advanceSearchFilter = Nothing
        End Sub
#End Region

    End Class



    ''' -----------------------------------------------------------------------------
    ''' Project	 : Zamba.Core
    ''' Class	 : Core.Searchs.LastSearch
    ''' 
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Clase para administrar las ultimas busquedas
    ''' </summary>
    ''' <remarks>
    ''' Los objetos de esta clase, guardan las busquedas en la PC local
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class LastSearch
        Implements IDisposable, ILastSearch

        Public Property Id() As Int64 Implements ILastSearch.Id
        Public Property Name() As String Implements ILastSearch.Name
        Public Property SQL() As List(Of String) Implements ILastSearch.SQL

        Private Sub New()
        End Sub

        ' ivan 21/12/15 - agregados desde branch principal
        Public Sub New(id As Int64, name As String, sql As List(Of String))
            _Id = id
            _Name = name
            _SQL = sql
        End Sub

        ' ivan 21/12/15 - agregados desde branch principal
        Public Sub New(id As Int64, name As String)
            _Id = id
            _Name = name
            SQL = New List(Of String)
        End Sub

        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    If SQL IsNot Nothing Then
                        SQL.Clear()
                    End If
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
                SQL = Nothing
            End If
            Me.disposedValue = True
        End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

    End Class
End Namespace