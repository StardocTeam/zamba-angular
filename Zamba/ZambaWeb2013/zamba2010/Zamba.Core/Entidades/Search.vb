Imports System.Collections.Generic
Imports Zamba.Framework
Imports Zamba.Searchs

Namespace Searchs
    Public Class Search
        Implements ISearch


#Region "Atributos y Propiedades"

        Public Property Doctypes As Generic.List(Of IDocType) Implements ISearch.Doctypes

        Public Property Indexs As Generic.List(Of IIndex) Implements ISearch.Indexs
        Public Property ParentName As String Implements ISearch.ParentName
        Public Property CaseSensitive As Boolean Implements ISearch.CaseSensitive
        Public Property MaxResults As Integer Implements ISearch.MaxResults
        Public Property ShowIndexOnGrid As Boolean Implements ISearch.ShowIndexOnGrid
        Public Property UseVersion As Boolean Implements ISearch.UseVersion
        Public Property UserId As Long Implements ISearch.UserId
        Public Property StepId As Long Implements ISearch.StepId
        Public Property StepStateId As Long Implements ISearch.StepStateId
        Public Property TaskStateId As Long Implements ISearch.TaskStateId
        Public Property WorkflowId As Long Implements ISearch.WorkflowId
        Public Property UserAssignedId As Long Implements ISearch.UserAssignedId
        Public Property UserAssignedEnabled As Boolean Implements ISearch.UserAssignedEnabled
        Public Property Textsearch As String Implements ISearch.Textsearch

        Public Property Name As String Implements ISearch.Name

        Public Property SQL As List(Of String) Implements ISearch.SQL


        Public Property SQLCount As List(Of String) Implements ISearch.SQLCount


        Public Property SearchType As SearchTypes Implements ISearch.SearchType


        Public Property EntitiesEnabledForQuickSearch As List(Of IEntityEnabledForQuickSearch) Implements ISearch.EntitiesEnabledForQuickSearch


        Public Property LastPage As Long = 0
        Public Property PageSize As Integer = 100


        Public Property OrderBy As String Implements ISearch.OrderBy


        Public Property Filters As List(Of ikendoFilter) Implements ISearch.Filters

        Public Property View As String Implements ISearch.View
        Public Property ResultsCount As New Hashtable

        Public Property Restriction As String Implements ISearch.Restriction

        Public Property Lista_ColumnasFiltradas As List(Of String) Implements ISearch.Lista_ColumnasFiltradas

        Public Property crdateFilters As Generic.List(Of kendoFilter) Implements ISearch.crdateFilters

        Public Property lupdateFilters As Generic.List(Of kendoFilter) Implements ISearch.lupdateFilters

        Public Property nameFilters As Generic.List(Of kendoFilter) Implements ISearch.nameFilters

        Public Property originalFilenameFilters As Generic.List(Of kendoFilter) Implements ISearch.originalFilenameFilters

        Public Property stateFilters As Generic.List(Of kendoFilter) Implements ISearch.stateFilters

        Public Property ParentEntity As IResult Implements ISearch.ParentEntity



#End Region

#Region "Constructores"
        Public Sub New()
            Me.UserAssignedId = -1
            Me.UserAssignedEnabled = True
        End Sub

        Public Sub New(ByVal indexs As Generic.List(Of IIndex), ByVal txtSearchInAllIndex As String, ByVal InAllDocTypes As Boolean, ByVal doctypes As Generic.List(Of IDocType), ByVal RaiseResults As Boolean, ByVal ParentName As String, ByVal UserId As Int64)
            Me.New()
            Me.Indexs = indexs
            Me.Textsearch = txtSearchInAllIndex
            Me.Doctypes = doctypes
            Me.ParentName = ParentName
            Me.UserId = UserId
        End Sub


#End Region

#Region "Metodos"
        Public Sub AddIndex(index As IIndex) Implements ISearch.AddIndex
            If Indexs Is Nothing Then
                Indexs = New Generic.List(Of IIndex)
            End If
            Indexs.Add(index)
        End Sub
        Public Sub AddFilter(filter As ikendoFilter) Implements ISearch.AddFilter
            If Filters Is Nothing Then
                Filters = New Generic.List(Of ikendoFilter)
            End If
            Filters.Add(filter)
        End Sub
        Public Sub AddDocType(docType As IDocType) Implements ISearch.AddDocType
            If Doctypes Is Nothing Then
                Doctypes = New Generic.List(Of IDocType)
            End If
            Doctypes.Add(docType)
        End Sub



        Public Sub SetOrderBy(orderString As String) Implements ISearch.SetOrderBy

        End Sub

        Public Sub SetGroupBy(orderString As String) Implements ISearch.SetGroupBy

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
        Implements ILastSearch
#Region "Atributos"
        Private _id As Int32
        Private _name As String = String.Empty
        Private _results As New DataTable()
        Private _sql As String = String.Empty
#End Region

#Region "Propiedades"
        Public Property Id() As Int32 Implements ILastSearch.Id
            Get
                Return _id
            End Get
            Set(ByVal value As Int32)
                _id = value
            End Set
        End Property
        Public Property Name() As String Implements ILastSearch.Name
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property
        Public Property Results() As DataTable Implements ILastSearch.Results
            Get
                Return _results
            End Get
            Set(ByVal value As DataTable)
                _results = value
            End Set
        End Property
        Public Property SQL() As String Implements ILastSearch.SQL
            Get
                Return _sql
            End Get
            Set(ByVal value As String)
                _sql = value
            End Set
        End Property
#End Region

#Region "Constructores"

        Private Sub New()

        End Sub
        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="Id">ID de la Busqueda</param>
        ''' <param name="Name">Nombre para mostrar la busqueda guardada</param>
        ''' <param name="Results">Arraylist de results</param>
        ''' <param name="SQL">Cadena SQL ejecutada</param>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Hernan]	26/05/2006	Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub New(ByVal Id As Int32, ByVal Name As String, ByVal Results As DataTable, ByVal SQL As String)
            _id = Id
            _name = Name
            _results = Results
            _sql = SQL
        End Sub
        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="Id">ID de la Busqueda</param>
        ''' <param name="Name">Nombre para mostrar la busqueda guardada</param>
        ''' <param name="Results">Arraylist de results</param>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Hernan]	26/05/2006	Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub New(ByVal Id As Int32, ByVal Name As String, ByVal Results As DataTable)
            _id = Id
            _name = Name
            _results = Results
        End Sub

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="Id">ID de la Busqueda</param>
        ''' <param name="Name">Nombre para mostrar la busqueda guardada</param>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Hernan]	26/05/2006	Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Sub New(ByVal Id As Int32, ByVal Name As String)
            _id = Id
            _name = Name
        End Sub
#End Region
    End Class
End Namespace