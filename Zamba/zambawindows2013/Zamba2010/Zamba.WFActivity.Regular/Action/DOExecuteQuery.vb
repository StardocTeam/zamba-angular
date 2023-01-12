Imports Zamba.Core
Imports System.Xml.Serialization

''' <summary>
''' Regla para ejecutar una consulta en la base
''' </summary>
''' <remarks></remarks>
<RuleCategory("Base de Datos"), RuleDescription("Ejecutar Consulta SQL"), RuleHelp("Permite ingresar una consulta de SQL y ejecutarla en la base de datos mostrando los resultados como sin resultados. un unico valor o en una tabla,"), RuleFeatures(False)> <Serializable()> _
Public Class DOExecuteQuery
    Inherits WFRuleParent
    Implements IDOExecuteQuery
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Public Overrides ReadOnly Property IsFull() As Boolean
        Get
            Return _isFull
        End Get
    End Property
    Public Overrides ReadOnly Property IsLoaded() As Boolean
        Get
            Return _isLoaded
        End Get
    End Property
    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub
    Public Overrides Sub Dispose()

    End Sub
#Region "Variables locales"
    Private _sql As String 'cadena SQL que debe ejecutar la regla
    Private _queryType As ReturnType
    Private _folder As String = ""
#End Region

    Public Property Sql() As String Implements IDOExecuteQuery.Sql
        Get
            Return _sql
        End Get
        Set(ByVal value As String)
            _sql = value
        End Set
    End Property
    Public Property QueryType() As ReturnType Implements IDOExecuteQuery.QueryType
        Get
            Return _queryType
        End Get
        Set(ByVal value As ReturnType)
            _queryType = value
        End Set
    End Property
    Public Property Folder() As String Implements IDOExecuteQuery.Folder
        Get
            Return _folder
        End Get
        Set(ByVal value As String)
            _folder = value
        End Set
    End Property
    ''' <summary>
    ''' Constructor de la Regla, recibe los parametros fundamentales para funcionar
    ''' </summary>
    ''' <param name="Id">Identificador de la regla</param>
    ''' <param name="Name">Nombre de la regla</param>
    ''' <param name="WFStep">Objeto Etapa que representa el contexto donde se ejecuta la regla</param>
    ''' <param name="strSql">Consulta SQL que se desea ejecutar</param>
    ''' <param name="ReturnType">Numero entero que representa 1: Table, 2: Return Scalar, 3: NoValue</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfStepId As Int64, ByVal strSql As String, ByVal ReturnType As Int16, ByVal folder As String)
        MyBase.New(Id, Name, wfStepId)
        Me.QueryType = ReturnType
        Me.Sql = strSql
        Me.Folder = folder.Trim
    End Sub
    Public Overrides ReadOnly Property MaskName() As String
        Get
            Return "Ejecutar sentencia SQL"
        End Get
    End Property

    'Public Overrides Function Play(ByVal Results As System.Collections.SortedList) As System.Collections.SortedList
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Try
            Dim playRule As New Zamba.WFExecution.PlayDOExecuteQuery()
            Return playRule.Play(results, Me)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Return results
    End Function
End Class
