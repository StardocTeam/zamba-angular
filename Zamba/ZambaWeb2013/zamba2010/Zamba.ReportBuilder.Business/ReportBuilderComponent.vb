Imports Zamba.Core

Public Class ReportBuilderComponent

    ''' <summary>
    ''' Insert a new query on the database 
    ''' </summary>
    ''' <param name="name">Nombre de la consulta</param>
    ''' <param name="tables"></param>
    ''' <param name="fields"></param>
    ''' <param name="relations"></param>
    ''' <param name="conditions"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Marcelo]	22/05/2008	Modified
    ''' 	[Diego]	01/07/2008	Modified
    ''' 	[Marcelo]	07/07/2008	Modified 
    ''' </history>

    ''' <summary>
    ''' Update an existing query on the database
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tables"></param>
    ''' <param name="fields"></param>
    ''' <param name="relations"></param>
    ''' <param name="name">Nombre de la consulta</param>
    ''' <history>
    ''' 	[Marcelo]	22/05/2008	Modified
    ''' </history>
    ''' <remarks></remarks>

    ''' <summary>
    ''' Run the query and return a dataset
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RunQueryBuilder(ByVal id As Int64, ByVal isZamba As Boolean) As DataSet
        Dim RC As New ReportBuilderFactory
        Try
            Dim ds As DataSet = RC.RunQueryBuilder(id, isZamba)
            ds.Tables(0).MinimumCapacity = ds.Tables(0).Rows.Count
            Return ds
        Finally
            RC = Nothing
        End Try
    End Function


    ''' <summary>
    ''' Run the query with the specified completed conditions and return a dataset
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function EvaluationRunWebQueryBuilder(ByVal id As Int64, ByVal isZamba As Boolean, ByVal conditions As Hashtable, Optional currentTask As ITaskResult = Nothing) As DataSet
        Dim RC As New ReportBuilderFactory
        Dim Ds As New DataSet
        Dim DataString
        If id Then
            Ds = RunQueryBuilderReporteGeneral(id, conditions, currentTask)
            If Ds Is Nothing Then
                DataString = GenerateQueryBuilder(id, True)
            Else
                Return Ds
            End If
        End If
        Return RC.RunWebQueryBuilder(id, isZamba, conditions) '
    End Function


    ''' <summary>
    ''' Run the query with the specified completed conditions and return a dataset
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RunWebQueryBuilder(ByVal id As Int64, ByVal isZamba As Boolean, ByVal completedValues As Hashtable) As DataSet
        Dim RC As New ReportBuilderFactory
        Return RC.RunWebQueryBuilder(id, isZamba, completedValues)
    End Function

    Public Function RunQueryBuilderReporteGeneral(ByVal id As Int64, ByVal vars As Hashtable, Optional currentTask As ITaskResult = Nothing) As DataSet
        Dim RC As New ReportBuilderFactory
        Return RC.RunQueryBuilderReporteGeneral(id, vars, currentTask)
    End Function
    ''' <summary>
    ''' Run the query and return a dataset
    ''' </summary>
    ''' <param name="id"></param>
    ''' <remarks>[Andres] 28/7/08 No deberia volver el query?</remarks>
    Public Function GenerateQueryBuilder(ByVal id As Int64, ByVal isZamba As Boolean) As String
        Dim RC As New ReportBuilderFactory
        Return RC.GenerateQueryBuilder(id, isZamba)
    End Function
    ''' <summary>
    ''' Get all the tables from the specified query
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTables(ByVal id As Int64) As String
        Dim RC As New ReportBuilderFactory
        Return RC.GetTables(id)
    End Function
    ''' <summary>
    ''' Get all the tables from the specified query
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetName(ByVal id As Int64) As String
        Dim RC As New ReportBuilderFactory
        Return RC.GetName(id)
    End Function
    ''' <summary>
    ''' Get all the fields from the specified query
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFields(ByVal id As Int64) As String
        Dim RC As New ReportBuilderFactory
        Return RC.GetFields(id)
    End Function
    ''' <summary>
    ''' Get all the conditions from the specified query
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetConditions(ByVal id As Int64) As String
        Dim RC As New ReportBuilderFactory
        Return RC.GetConditions(id)
    End Function

    ''' <summary>
    ''' Get all the conditions from the specified query to be completed
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetConditionsToComplete(ByVal id As Int64) As ArrayList
        Dim RC As ReportBuilderFactory = Nothing
        Try
            RC = New ReportBuilderFactory
            Dim conditionsArray As ArrayList = New ArrayList()
            Dim value As String = RC.GetConditions(id)
            While value <> ""
                Dim values As String
                Dim strItem As String = value.Split(",")(0)
                values = strItem.Split(".")(1).Split("|")(2)

                If values = "" Then
                    conditionsArray.Add(strItem)
                End If

                value = value.Remove(0, value.Split(",")(0).Length)
                If value.Length > 0 Then
                    value = value.Remove(0, 1)
                End If
            End While

            Return conditionsArray
        Finally
            If Not IsNothing(RC) Then
                RC = Nothing
            End If
        End Try
    End Function

    ''' <summary>
    ''' Get all the relations from the specified query
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRelations(ByVal id As Int64) As String
        Dim RC As New ReportBuilderFactory
        Return RC.GetRelations(id)
    End Function
    ''' <summary>
    ''' Get all the ids and names
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetQueryIdsAndNames(ByVal userid As Int64) As DataSet
        Dim RC As New ReportBuilderFactory
        Return RC.GetQueryIdsAndNames(userid)
    End Function
    'obtiene todos los ids y nombres
    Public Function GetQueryIdsAndNamesReportGeneral(ByVal userid As Int64) As DataSet
        Dim RC As New ReportBuilderFactory
        Return RC.GetQueryIdsAndNamesReportGeneral(userid)
    End Function
    ''' <summary>
    ''' Get all the ids and names
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllQueryIdsAndNames() As DataSet
        Dim RC As New ReportBuilderFactory
        Return RC.GetAllQueryIdsAndNames()
    End Function
    Public Function GetAllQueryIdsAndNamesRepoerteGeneral() As DataSet
        Dim RC As New ReportBuilderFactory
        Return RC.GetAllQueryIdsAndNamesReporteGeneral()
    End Function
    ''' <summary>
    ''' Delete a query from the database
    ''' </summary>
    ''' <param name="id">Id de la consulta</param>
    ''' <param name="name">Nombre de la consulta</param>
    ''' <history>
    ''' 	[Marcelo]	22/05/2008	Modified
    ''' </history>
    ''' <remarks></remarks>

    ''' <summary>
    ''' Get all the table columns names
    ''' </summary>
    ''' <param name="name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetColumns(ByVal name As String) As DataSet
        Dim rc As New ReportBuilderFactory
        Return rc.GetColumns(name)
    End Function

    ''' <summary>
    ''' Get a specified general report ID by the report name.
    ''' </summary>
    ''' <param name="Name">General Report Name</param>
    ''' <returns>The General Report ID</returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Tomas] 06/04/2009  Created
    ''' </history>
    Public Function GetGeneralReportIdByName(ByVal Name As String) As Int64
        Dim rc As New ReportBuilderFactory
        Return rc.GetGeneralReportIdByName(Name.Replace("&&", " "))
    End Function

    ''' <summary>
    ''' Guarda en base la expresion de ordenamiento de registros
    ''' </summary>
    ''' <param name="rptid">Id de reporte</param>
    ''' <remarks></remarks>
    Public Function GetGroupExpression(ByVal rptid As Int64) As String
        Dim rc As New ReportBuilderFactory
        Return rc.GetGroupExpression(rptid)
    End Function

    ''' <summary>
    ''' Guarda en base la expresion de agrupamientos de registros
    ''' </summary>
    ''' <param name="rptid">Id de reporte</param>
    ''' <remarks></remarks>
    Public Function GetSortExpression(ByVal rptid As Int64) As String
        Dim rc As New ReportBuilderFactory
        Return rc.GetSortExpression(rptid)
    End Function

End Class