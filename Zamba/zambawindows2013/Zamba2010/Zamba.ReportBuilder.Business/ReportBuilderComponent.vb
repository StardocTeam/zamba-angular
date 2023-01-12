Imports Zamba.Core

Public Class ReportBuilderComponent
    Implements IReportBuilderComponent

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
    ''' 

    Public Sub InsertQueryReportGeneral(ByVal name As String, ByVal conditions As String, ByVal query As String, ByVal group As String) Implements IReportBuilderComponent.InsertQueryReportGeneral
        Dim RC As New ReportBuilderFactory
        Dim id As Int32 = FuncionesZamba.GetNewID(True)
        RC.InsertQueryReportGeneral(id, name, conditions, query, group)

        UserBusiness.Rights.SaveAction(id, ObjectTypes.ModuleReportBuilder, RightsType.Create, "Se creó el reporte general: " & name & "(" & id & ")")

        'For Each UserGroupid As Int32 In UserGroupBusiness.GetUserGroupsIds(id)
        ' UserBusiness.Rights.SetRight(UserGroupid, ObjectTypes.ModuleReports, RightsType.View, id, True)
        'Next
    End Sub
    Public Sub InsertQueryBuilder(ByVal name As String, ByVal tables As String, ByVal fields As String, ByVal relations As String, ByVal conditions As String, ByVal userid As Int32, ByVal sortExpression As String, ByVal groupExpression As String) Implements IReportBuilderComponent.InsertQueryBuilder
        Dim RC As New ReportBuilderFactory
        Dim id As Int32 = FuncionesZamba.GetNewID(True)

        RC.InsertQueryBuilder(id, name, tables, fields, relations, conditions, sortExpression, groupExpression)

        'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
        UserBusiness.Rights.SaveAction(id, ObjectTypes.ModuleReportBuilder, RightsType.Create, "Se creó el reporte: " & name & "(" & id & ")")

        'Asigno permisos para usar la consulta
        For Each UserGroupid As Int32 In UserGroupBusiness.GetUserGroupsIds(userid)
            UserBusiness.Rights.SetRight(UserGroupid, ObjectTypes.ModuleReports, RightsType.View, id, True)
            UserBusiness.Rights.SetRight(UserGroupid, ObjectTypes.ModuleReports, RightsType.Edit, id, True)
            UserBusiness.Rights.SetRight(UserGroupid, ObjectTypes.ModuleReports, RightsType.Delete, id, True)
        Next
    End Sub
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
    Public Sub UpdateQueryBuilder(ByVal id As Int32, ByVal tables As String, ByVal fields As String, ByVal relations As String, ByVal conditions As String, ByVal name As String, ByVal sortExpression As String, ByVal GroupExpression As String) Implements IReportBuilderComponent.UpdateQueryBuilder
        Dim RC As New ReportBuilderFactory
        RC.UpdateQueryBuilder(id, tables, fields, relations, conditions, name, sortExpression, GroupExpression)

        'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
        UserBusiness.Rights.SaveAction(id, ObjectTypes.ModuleReportBuilder, RightsType.Edit, "Se modificó el reporte: " & name & "(" & id & ")")
    End Sub
    ''' <summary>
    ''' Run the query and return a dataset
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RunQueryBuilder(ByVal id As Int32, ByVal isZamba As Boolean, ByVal askCondition As Boolean) As DataSet Implements IReportBuilderComponent.RunQueryBuilder
        Dim RC As New ReportBuilderFactory
        Try
            Dim ds As DataSet = RC.RunQueryBuilder(id, isZamba, askCondition, False)
            If Not IsNothing(ds) Then
                ds.Tables(0).MinimumCapacity = ds.Tables(0).Rows.Count
            End If
            Return ds
        Finally
            RC = Nothing
        End Try
    End Function

    Public Function RunQueryBuilderReporteGeneral(ByVal id As Int32) As DataSet Implements IReportBuilderComponent.RunQueryBuilderReporteGeneral
        Dim RC As New ReportBuilderFactory
        Return RC.RunQueryBuilderReporteGeneral(id)
    End Function
    ''' <summary>
    ''' Run the query and return a dataset
    ''' </summary>
    ''' <param name="id"></param>
    ''' <remarks>[Andres] 28/7/08 No deberia volver el query?</remarks>
    Public Function GenerateQueryBuilder(ByVal id As Int32, ByVal isZamba As Boolean, ByVal askCondition As Boolean) As String Implements IReportBuilderComponent.GenerateQueryBuilder
        Dim RC As New ReportBuilderFactory
        Return RC.GenerateQueryBuilder(id, isZamba, askCondition)
    End Function
    ''' <summary>
    ''' Get all the tables from the specified query
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTables(ByVal id As Int32) As String Implements IReportBuilderComponent.GetTables
        Dim RC As New ReportBuilderFactory
        Return RC.GetTables(id)
    End Function
    ''' <summary>
    ''' Get all the tables from the specified query
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetName(ByVal id As Int32) As String Implements IReportBuilderComponent.GetName
        Dim RC As New ReportBuilderFactory
        Return RC.GetName(id)
    End Function
    ''' <summary>
    ''' Get all the fields from the specified query
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFields(ByVal id As Int32) As String Implements IReportBuilderComponent.GetFields
        Dim RC As New ReportBuilderFactory
        Return RC.GetFields(id)
    End Function
    ''' <summary>
    ''' Get all the conditions from the specified query
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetConditions(ByVal id As Int32) As String Implements IReportBuilderComponent.GetConditions
        Dim RC As New ReportBuilderFactory
        Return RC.GetConditions(id)
    End Function

    ''' <summary>
    ''' Get all the conditions from the specified query to be completed
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetConditionsToComplete(ByVal id As Int32) As ArrayList Implements IReportBuilderComponent.GetConditionsToComplete
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
    Public Function GetRelations(ByVal id As Int32) As String Implements IReportBuilderComponent.GetRelations
        Dim RC As New ReportBuilderFactory
        Return RC.GetRelations(id)
    End Function
    ''' <summary>
    ''' Get all the ids and names
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetQueryIdsAndNames(ByVal ids As List(Of Int64)) As DataSet Implements IReportBuilderComponent.GetQueryIdsAndNames
        Dim RC As New ReportBuilderFactory
        Return RC.GetQueryIdsAndNames(ids)
    End Function
    'obtiene todos los ids y nombres
    Public Function GetQueryIdsAndNamesReportGeneral(ByVal ids As List(Of Int64)) As DataSet Implements IReportBuilderComponent.GetQueryIdsAndNamesReportGeneral
        Dim RC As New ReportBuilderFactory
        Return RC.GetQueryIdsAndNamesReportGeneral(ids)
    End Function
    ''' <summary>
    ''' Get all the ids and names
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllQueryIdsAndNames() As DataSet Implements IReportBuilderComponent.GetAllQueryIdsAndNames
        Dim RC As New ReportBuilderFactory
        Return RC.GetAllQueryIdsAndNames()
    End Function
    Public Function GetAllQueryIdsAndNamesReporteGeneral() As DataSet Implements IReportBuilderComponent.GetAllQueryIdsAndNamesReporteGeneral
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
    Public Sub DeleteQueryBuilder(ByVal id As Int32, ByVal name As String) Implements IReportBuilderComponent.DeleteQueryBuilder
        Dim rc As New ReportBuilderFactory
        rc.DeleteQueryBuilder(id)

        'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
        UserBusiness.Rights.SaveAction(id, ObjectTypes.ModuleReportBuilder, RightsType.Delete, "Se eliminó el reporte: " & name & "(" & id & ")")
    End Sub
    ''' <summary>
    ''' Get all the table columns names
    ''' </summary>
    ''' <param name="name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetColumns(ByVal name As String) As DataSet Implements IReportBuilderComponent.GetColumns
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
    Public Function GetGeneralReportIdByName(ByVal Name As String) As Int32 Implements IReportBuilderComponent.GetGeneralReportIdByName
        Dim rc As New ReportBuilderFactory
        Return rc.GetGeneralReportIdByName(Name.Replace("&&", " "))
    End Function

    ''' <summary>
    ''' Obtiene el agrupamiento de los reportes
    ''' </summary>
    ''' <param name="rptid">Id de reporte</param>
    ''' <remarks></remarks>
    Public Function GetGroupExpression(ByVal rptid As Int32, ByVal isGral As Boolean) As String Implements IReportBuilderComponent.GetGroupExpression
        Dim rc As New ReportBuilderFactory
        If isGral = True Then
            Return rc.GetGeneralGroupExpression(rptid)
        Else
            Return rc.GetGroupExpression(rptid)
        End If
    End Function

    ''' <summary>
    ''' Guarda en base la expresion de agrupamientos de registros
    ''' </summary>
    ''' <param name="rptid">Id de reporte</param>
    ''' <remarks></remarks>
    Public Function GetSortExpression(ByVal rptid As Int32) As String Implements IReportBuilderComponent.GetSortExpression
        Dim rc As New ReportBuilderFactory
        Return rc.GetSortExpression(rptid)
    End Function

    Public Function GetLink(ByVal id As Int32) As String Implements IReportBuilderComponent.GetLink
        Dim rc As New ReportBuilderFactory
        rc.GetLinks(id)
    End Function

    ''' <summary>
    ''' Las columnas utilizadas para el eje Y del grafico
    ''' </summary>
    ''' <param name="idquery">Id de reporte</param>
    ''' <remarks></remarks>
    Function GetYValueColumn(ByVal idquery As Integer) As String Implements IReportBuilderComponent.GetYValueColumn
        Dim rc As New ReportBuilderFactory
        Return rc.GetYValueColumn(idquery)
    End Function

    ''' <summary>
    ''' Las columnas utilizadas para el eje X del grafico
    ''' </summary>
    ''' <param name="idquery">Id de reporte</param>
    ''' <remarks></remarks>
    Function GetXValueColumn(ByVal idquery As Integer) As String Implements IReportBuilderComponent.GetXValueColumn
        Dim rc As New ReportBuilderFactory
        Return rc.GetXValueColumn(idquery)
    End Function

    ''' <summary>
    ''' Actualiza los campos de configuracion basica para el grafico en reportes
    ''' </summary>
    ''' <param name="idquery">Id de reporte</param>
    ''' <param name="XValueColumns">Valor para el eje X</param>
    ''' <param name="YValueColumns">Valor para el eje Y</param>
    ''' <param name="ChartType">Tipo de grafico</param>
    ''' <remarks></remarks>
    Sub UpdateChartOption(ByVal ReportID As Integer, ByVal xValueColumns As String, ByVal yValueColumns As String, ByVal ChartType As Integer) Implements IReportBuilderComponent.UpdateChartOption
        Dim rc As New ReportBuilderFactory
        rc.UpdateChartOption(ReportID, xValueColumns, yValueColumns, ChartType)
    End Sub

    ''' <summary>
    ''' Elimina la configuracion de gráficos, con esto se marca como que no posee
    ''' </summary>
    ''' <param name="idquery">Id de reporte</param>
    ''' <remarks></remarks>
    Sub DeleteChartOption(ByVal idquery As Integer) Implements IReportBuilderComponent.DeleteChartOption
        Dim rc As New ReportBuilderFactory
        rc.DeleteChartOption(idquery)
    End Sub

    ''' <summary>
    ''' Obtiene el tipo del grafico
    ''' </summary>
    ''' <param name="idquery">Id de reporte</param>
    ''' <remarks></remarks>
    Function GetChartType(ByVal idquery As Integer) As Integer Implements IReportBuilderComponent.GetChartType
        Dim rc As New ReportBuilderFactory
        Return rc.GetChartType(idquery)
    End Function

    ''' <summary>
    ''' Chequea si hay alguna configuracion para graficos.
    ''' </summary>
    ''' <param name="idquery">Id de reporte</param>
    ''' <remarks></remarks>
    Function CheckReportHaveChart(ByVal idquery As Integer) As Boolean Implements IReportBuilderComponent.CheckReportHaveChart
        Dim rc As New ReportBuilderFactory
        Return rc.CheckReportHaveChart(idquery)
    End Function

    ''' <summary>
    ''' Chequea si hay alguna configuracion para graficos.
    ''' </summary>
    ''' <param name="idquery">Id de reporte</param>
    ''' <remarks></remarks>
    Function GetLinkValuesByReportID(ByVal idquery As Integer) As DataTable Implements IReportBuilderComponent.GetLinkValuesByReportID
        Dim rc As New ReportBuilderFactory
        Return rc.GetLinkValuesbyReportID(idquery)
    End Function
    ''' <summary>
    ''' Chequea si hay alguna configuracion para graficos.
    ''' </summary>
    ''' <param name="idquery">Id de reporte</param>
    ''' <remarks></remarks>
    Sub InsertUpdateLinkValues(ByVal idquery As Integer, ByVal relatinalField As String,
                                      ByVal Entity As String, ByVal Description As String, ByVal Position As Short) Implements IReportBuilderComponent.InsertUpdateLinkValues
        Dim rc As New ReportBuilderFactory
        rc.InsertUpdateLinkValues(idquery, relatinalField, Entity, Description, Position)
    End Sub
    ''' <summary>
    ''' Chequea si hay alguna configuracion para graficos.
    ''' </summary>
    ''' <param name="idquery">Id de reporte</param>
    ''' <remarks></remarks>
    Sub DeleteLinkValues(ByVal idquery As Integer) Implements IReportBuilderComponent.DeleteLinkValues
        Dim rc As New ReportBuilderFactory
        rc.DeleteLinkValuesbyReportID(idquery)
    End Sub
End Class
