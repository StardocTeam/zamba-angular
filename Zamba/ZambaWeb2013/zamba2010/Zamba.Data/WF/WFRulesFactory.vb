Imports Zamba.Core.Enumerators
Imports Zamba.servers
Imports Zamba.Data
Imports zamba.Core
Imports System.Drawing
Imports System.Text
Imports System.Collections.Generic
Imports System.Windows.Forms

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Data
''' Class	 : Data.WFRulesFactory
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Factory para manipular Reglas
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
'''     [Gaston]	10/07/2008	Modified
''' </history>
''' -----------------------------------------------------------------------------
Public Class WFRulesFactory
    'Inherits ZClass
    'Public Overrides Sub Dispose()

    'End Sub

#Region "Get"


    ''' <summary>
    ''' Obtiene el stepid por el id de la regla
    ''' </summary>
    ''' <param name="ruleId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetWFStepIdbyRuleID(ByVal ruleId As Int64) As Int64
        Dim QueryBuilder As New StringBuilder()
        QueryBuilder.Append("SELECT step_Id FROM WFRules WHERE ID = ")
        QueryBuilder.Append(ruleId.ToString())

        Dim TempDs As DataSet = Server.Con.ExecuteDataset(CommandType.Text, QueryBuilder.ToString())
        QueryBuilder.Remove(0, QueryBuilder.Length)
        QueryBuilder = Nothing

        If Not IsNothing(TempDs) AndAlso TempDs.Tables.Count = 1 AndAlso TempDs.Tables(0).Rows.Count = 1 Then
            Return TempDs.Tables(0).Rows(0).Item("step_Id")
        End If

        Return 0
    End Function

    'Public Shared Function GetRuleClass(ByVal ruleId As Int64) As String
    '    Dim QueryBuilder As New StringBuilder(102 + ruleId.ToString.Length)
    '    QueryBuilder.Append("SELECT Class FROM WFRules WHERE ID = ")
    '    QueryBuilder.Append(ruleId.ToString())

    '    Dim TempDs As DataSet = Server.Con.ExecuteDataset(CommandType.Text, QueryBuilder.ToString())
    '    QueryBuilder.Remove(0, QueryBuilder.Length)
    '    QueryBuilder = Nothing

    '    If Not IsNothing(TempDs) AndAlso TempDs.Tables.Count = 1 AndAlso TempDs.Tables(0).Rows.Count = 1 Then
    '        Return TempDs.Tables(0).Rows(0).Item("Class")
    '    End If

    '    Return Nothing
    'End Function
    'Public Overloads Shared Function GetRules(ByVal ruleIds As List(Of Int64)) As DataTable

    '    Dim QueryBuilder As New StringBuilder()
    '    QueryBuilder.Append("SELECT Id, Name, step_Id, Type, ParentId, ")
    '    QueryBuilder.Append("ParentType, Class, Enable, Version ")
    '    QueryBuilder.Append("FROM WFRules WHERE ")

    '    For Each RuleId As Int64 In ruleIds
    '        QueryBuilder.Append(" ID = ")
    '        QueryBuilder.Append(RuleId.ToString())
    '        QueryBuilder.Append(" OR ")
    '    Next

    '    QueryBuilder.Remove(QueryBuilder.Length - 4, 4)

    '    Dim TempDs As DataSet = Server.Con.ExecuteDataset(CommandType.Text, QueryBuilder.ToString())
    '    QueryBuilder.Remove(0, QueryBuilder.Length)
    '    QueryBuilder = Nothing

    '    If Not IsNothing(TempDs) AndAlso TempDs.Tables.Count = 1 Then
    '        Return TempDs.Tables(0)
    '    End If

    '    Return Nothing
    'End Function


    Public Shared Function GetRulesIdByStep(ByVal stepId As Int64) As DataTable
        Dim QueryBuilder As New StringBuilder

        QueryBuilder.Append("SELECT Id FROM WFRules WHERE step_id=")
        QueryBuilder.Append(stepId.ToString & "  order by wfrules.name")

        Dim Ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, QueryBuilder.ToString())

        QueryBuilder.Remove(0, QueryBuilder.Length)
        QueryBuilder = Nothing

        If Not IsNothing(Ds) AndAlso Ds.Tables.Count = 1 Then
            Return Ds.Tables(0)
        End If

        Return Nothing
    End Function

    Public Shared Function GetRulesIdandParentByStep(ByVal stepId As Int64) As DataTable
        Dim QueryBuilder As New StringBuilder

        QueryBuilder.Append("SELECT Id,parentid  FROM WFRules WHERE step_id=")
        QueryBuilder.Append(stepId.ToString)
        QueryBuilder.Append(" order by name")

        Dim Ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, QueryBuilder.ToString())

        QueryBuilder.Remove(0, QueryBuilder.Length)
        QueryBuilder = Nothing

        If Not IsNothing(Ds) AndAlso Ds.Tables.Count = 1 Then
            Return Ds.Tables(0)
        End If

        Return Nothing
    End Function

    Public Shared Function GetRulesIdByStep(ByVal stepId As Int64, ByVal ruleTypeId As Int32) As DataTable

        Dim QueryBuilder As New StringBuilder

        QueryBuilder.Append("SELECT Id FROM WFRules WHERE step_id=")
        QueryBuilder.Append(stepId.ToString())
        QueryBuilder.Append("AND Type=")
        QueryBuilder.Append(ruleTypeId.ToString())
        QueryBuilder.Append(" order by name")

        Dim Ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, QueryBuilder.ToString())

        QueryBuilder.Remove(0, QueryBuilder.Length)
        QueryBuilder = Nothing

        If Not IsNothing(Ds) AndAlso Ds.Tables.Count = 1 Then
            Return Ds.Tables(0)
        End If

        Return Nothing
    End Function


    ''' <summary>
    ''' Obtiene todas las reglas existentes
    ''' </summary>
    ''' <returns>Objeto DSRules</returns>
    ''' <remarks></remarks>
    Public Overloads Shared Function GetDoExecuteRules() As DataSet
        'Dim Dsrules As New DsRules
        'Dim Restriction As String
        Dim sql As String

        sql = "select "

        If Server.isOracle Then
            sql += " wfr.Name || ' (' || convert(varchar,ID) || ')' || ' - ' || wfs.Name || ' - ' || wf.Name, ID  "
        Else
            sql += " wfr.Name +  ' (' + convert(varchar,ID) + ')' + ' - ' + wfs.Name + ' - ' + wf.Name, ID  "
        End If

        sql += "from "
        sql += "    wfrules wfr "
        sql += "    inner join wfstep wfs on wfs.step_id = wfr.step_id "
        sql += "    inner join wfworkflow wf on wf.work_id = wfs.work_id "
        sql += "order by "
        sql += "    wfr.NAMe "
       
        Return Server.Con.ExecuteDataset(CommandType.Text, sql)
        'Cargo Items de Parametros 
        'sql = "Select * from WFRuleParamItems Order By Item"
        'Dstemp.Tables.Add(Server.Con.ExecuteDataset(CommandType.Text, sql).Tables(0))

    End Function

    Public Shared Function GetRulesByStepId(ByVal StepId As Int32) As DataSet
        Dim sql As String = "Select * from wfrules where step_id = " & StepId.ToString() & " order by name"
        Dim Dstemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)

        sql = Nothing

        Dim Dsrules As New DataSet
        Dstemp.Tables(0).TableName = "WFRules"
        Dsrules.Merge(Dstemp.Tables(0))

        Dstemp.Dispose()
        Dstemp = Nothing

        Return Dsrules
    End Function


    Public Function GetRulesOptionsDT(ruleid As Int64) As DataTable
        Dim Ds As DataSet = Nothing
        Ds = Server.Con.ExecuteDataset(CommandType.Text, "Select DISTINCT RULEID ID,OBJVALUE,OBJEXTRADATA,OBJECTID,SECTIONID FROM ZRULEOPT where ruleid = " & ruleid)
        Return Ds.Tables(0)
    End Function

    '[Ezequiel] - 04/5/11 - Metodo que obtiene las reglas de una etapa.
    Public Shared Function GetRulesByStepIds(ByVal stepids As String) As DataSet
        Dim sql As String = "Select wfrules.*, work_id from wfrules inner join wfstep on wfrules.step_id = wfstep.step_id where wfrules.step_id in (" & stepids & ") order by wfrules.name"
        Dim Dstemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)
        sql = Nothing
        Return Dstemp
    End Function

    Public Shared Function GetRulesParamItemsByStepIds(ByVal stepids As String) As DataSet
        Dim sql As String
        If Server.isOracle Then
            sql = "Select rule_id,item,c_value as value from WFRuleParamItems where rule_id in (select id from wfrules where step_id in (" & stepids & ")) Order By Item"
        Else
            sql = "Select * from WFRuleParamItems where rule_id in (select id from wfrules where step_id in (" & stepids & ")) Order By Item"
        End If
        Dim Dstemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)

        sql = Nothing

        Return Dstemp
    End Function

    ''' <summary>
    ''' Obtiene un dataset solo con las reglas de validacion de entrada y de entrada
    ''' </summary>
    ''' <param name="StepId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetEntranceRulesByStepId(ByVal StepId As Int32) As DataSet
        Dim sql As String = "Select * from wfrules where step_id = " & StepId.ToString() & " and (parenttype = 6 or parenttype = 12) order by wfrules.name"
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Select para obtener las reglas de entrada y validacion de entrada " & sql)
        Dim Dstemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)

        sql = Nothing

        Dim Dsrules As New DataSet
        Dstemp.Tables(0).TableName = "WFRules"
        Dsrules.Merge(Dstemp.Tables(0))

        Dstemp.Dispose()
        Dstemp = Nothing

        Return Dsrules
    End Function

    ''' <summary>
    ''' Obtiene las reglas para un Workflow
    ''' </summary>
    ''' <param name="WF">Objeto Workflow del cual se desea obtener las reglas</param>
    ''' <returns>Objeto DSRules</returns>
    ''' <remarks></remarks>
    Public Overloads Shared Function GetRulesAsDataSet(ByVal Restriction As String) As DataSet
        Dim Dstemp As DataSet = Nothing
        'Dim Dstemp2 As DataSet
        Dim sql As New StringBuilder

        'Cargo Reglas

        If Restriction.Length > 3 Then
            Restriction = Restriction.Substring(0, Restriction.Length - 3)
            Restriction += ")"
            If Server.isOracle Then
                sql.Append("Select wfstep.step_id as " & Chr(34) & "stepId" & Chr(34))
                sql.Append(", wfrules.name as " & Chr(34) & "ruleName" & Chr(34))
                sql.Append(", wfStep.name as " & Chr(34) & "stepName" & Chr(34) & ", wfrules.id as " & Chr(34) & "ruleId" & Chr(34) & ", wfstep.name || ' - ' || wfrules.name as " & Chr(34) & "ruleFullName" & Chr(34) & " from wfrules inner join wfstep on wfstep.step_id = wfrules.step_id where ")
                sql.Append(Restriction)
                sql.Append(" order by wfrules.name")
            Else
                sql.Append("Select wfstep.step_id as stepId, wfrules.name as ruleName, wfStep.name as stepName, wfrules.id as ruleId, wfstep.name + ' - ' + wfrules.name as ruleFullName from wfrules inner join wfstep on wfstep.step_id = wfrules.step_id where " & Restriction & " order by wfrules.name")
            End If

            Dstemp = Server.Con.ExecuteDataset(CommandType.Text, sql.ToString)
        End If

        Return Dstemp
    End Function

    ''' <summary>
    ''' Obtiene las reglas de una etapa de un workflow
    ''' </summary>
    ''' <param name="stepID">Id de etapa</param>
    ''' <returns>Objeto DSRules</returns>
    ''' <remarks></remarks>
    Public Overloads Shared Function GetRulesByWFStepID(ByVal stepID As Int64) As DataSet
        Dim Dstemp As DataSet
        If Server.isOracle Then
            Dstemp = Server.Con.ExecuteDataset(CommandType.Text, "Select * from wfrules where step_id=" & stepID & " order by name")
        Else
            Dim parvalues() As Object = {stepID}
            Dstemp = Server.Con.ExecuteDataset("zsp_workflow_100_GetRulesByStepID", parvalues)
            parvalues = Nothing
        End If

        Dim Dsrules As New DataSet
        Dstemp.Tables(0).TableName = "WFRules"
        Dsrules.Merge(Dstemp.Tables(0))

        If Dsrules.Tables("WFRules").Rows.Count = 0 Then Return Dsrules

        Dim sql As String
        If Server.isOracle Then
            sql = "Select RULE_ID,ITEM,C_VALUE AS VALUE from WFRuleParamItems inner join WFRules on WFRuleParamItems.Rule_id = WFRules.Id where WFRules.step_Id =   " & stepID & " order by WFRuleParamItems.Rule_id, WFRuleParamItems.item "
            Dstemp = Server.Con.ExecuteDataset(CommandType.Text, sql)
        Else
            Dim parvalues() As Object = {stepID}
            Dstemp = Server.Con.ExecuteDataset("zsp_workflow_100_GetRulesParamItemsByStepID", parvalues)
            parvalues = Nothing
        End If

        Dstemp.Tables(0).TableName = "WFRuleParamItems"
        Dsrules.Merge(Dstemp.Tables(0))

        Dstemp.Dispose()
        Dstemp = Nothing

        sql = Nothing
        Return Dsrules
    End Function


    ''' <summary>
    ''' Método que devuelve los argumentos de una determinada regla
    ''' </summary>
    ''' <param name="p_iRuleID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <remarks></remarks>
    '''     [Gaston]	06/10/2008	Modified
    ''' </history>
    Public Shared Function GetRuleParamItems(ByVal p_iRuleID As Int32) As DataSet

        Dim sql As String = "SELECT Id, Name, step_Id, Type, ParentId, ParentType, Class, Enable, Version FROM wfRules  " & If(Zamba.Servers.Server.isSQLServer, " WITH(NOLOCK) ", "") & "  WHERE Id=" & p_iRuleID.ToString
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)
        Dim Dsrules As New DataSet

        ds.Tables(0).TableName = "WFRules"
        Dsrules.Merge(ds)

        ds.Dispose()
        ds = Nothing

        If (Server.isOracle) Then
            sql = "Select rule_id,item,c_value as value,objecttypes from WFRuleParamItems where Rule_id=" & p_iRuleID.ToString & " Order By Item"
        Else
            sql = "Select * from WFRuleParamItems where Rule_id=" & p_iRuleID.ToString & " Order By Item"
        End If

        Dim Dstemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)

        sql = Nothing

        Dstemp.Tables(0).TableName = "WFRuleParamItems"
        Dsrules.Merge(Dstemp.Tables(0))


        Dstemp.Dispose()
        Dstemp = Nothing

        Return Dsrules

    End Function

    ''' <summary>
    ''' Obtiene una regla a partir de su Id
    ''' </summary>
    ''' <param name="RuleId">id de la regla</param>
    ''' <returns>DsRules</returns>
    ''' <remarks></remarks>
    Public Shared Function GetRuleByID(ByVal p_iRuleID As Int64) As DataSet
        Dim dsRule As New DataSet

        Dim sql As String = "SELECT Id, Name, step_Id, Type, ParentId, ParentType, Class, Enable, Version FROM wfRules  " & If(Zamba.Servers.Server.isSQLServer, " WITH(NOLOCK) ", "") & "  WHERE Id=" & p_iRuleID.ToString()
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)
        ds.Tables(0).TableName = "WFRules"
        dsRule.Merge(ds)

        ds.Dispose()
        ds = Nothing
        sql = Nothing

        Return dsRule
    End Function

    ''' <summary>
    ''' Obtiene el estado de una regla
    ''' </summary>
    ''' <param name="RuleId">id de la regla</param>
    ''' <returns>Estado de la regla</returns>
    ''' <remarks></remarks>
    Public Function GetRuleStateById(ByVal p_iRuleId As Int32) As Boolean
        'Dim i_dsRule As DsRules
        Dim bEstado As Boolean

        Using i_dsRule As DataSet = GetRuleByID(p_iRuleId)
            'i_dsRule = GetRuleByID(p_iRuleId)
            If Not i_dsRule Is Nothing AndAlso i_dsRule.Tables("WFRules").Rows.Count > 0 Then
                bEstado = Convert.ToBoolean(i_dsRule.Tables("WFRules")(0).Item("Enable"))
                Return bEstado
            End If
        End Using

        Return False
    End Function





    ''' <summary>
    ''' inserta los Usuario que se van a notificar sobre la ejecucion de la regla
    ''' </summary>
    ''' <param name="ruleid"></param>
    ''' <param name="RuleSection"></param>
    ''' <param name="_RulePreference"></param>
    ''' <param name="DestType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Sub InsertUsersToNotifyAboutRuleExecution(ByVal ruleid As Int64, ByVal RuleSectionId As Int32, ByVal _RulePreferenceid As Int32, ByVal DestTypeid As Int32, ByVal items As Generic.List(Of String))
        Dim query As New StringBuilder
        query.Append("DELETE FROM ZRuleOpt WHERE RuleId =")
        query.Append(ruleid)
        query.Append(" and SectionId = ")
        query.Append(RuleSectionId)
        query.Append(" and ObjectId =  ")
        query.Append(_RulePreferenceid)
        query.Append(" And ObjValue =")
        query.Append(DestTypeid)
        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)

        For Each Item As String In items
            query.Remove(0, query.Length)
            query.Append("INSERT INTO ZRuleOpt(RuleId, SectionId, ObjectId, ObjExtraData, ObjValue) VALUES(")
            query.Append(ruleid)
            query.Append(",")
            query.Append(RuleSectionId)
            query.Append(",")
            query.Append(_RulePreferenceid)
            query.Append(",'")
            query.Append(Item)
            query.Append("',")
            query.Append(DestTypeid)
            query.Append(")")
            Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
        Next
    End Sub

    ''' <summary>
    ''' Devuelve un listado de ids y nombres de reglas perteneciente a 1 etapa
    ''' </summary>
    ''' <param name="stepId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetRulesIdsAndNames(ByVal stepId As Int64) As DataTable
        Dim query As String = "select Id, Name from wfrules where step_id = " & stepId.ToString() & " order by name"

        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, query)

        If (Not IsNothing(ds) AndAlso ds.Tables.Count > 0) Then
            Return ds.Tables(0)
        End If

        Return Nothing
    End Function

    Public Shared Function GetChildRulesIds(ByVal ruleId As Int64) As DataTable
        Return Server.Con.ExecuteDataset(CommandType.Text, "SELECT ID FROM WFRULES WHERE PARENTID=" & ruleId.ToString & " ORDER BY NAME").Tables(0)
    End Function
#End Region

#Region "Insert"
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Metodo para persistir una regla en la base de datos
    ''' </summary>
    ''' <param name="NewRule">Objeto WFRule que se desea persistir</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    '<Obsolete("Las entidades no corresponden a la capa de datos")> _
    'Public Shared Sub InsertRule(ByVal NewRule As WFRuleParent)
    '    Dim sql As String = String.Empty

    '    If NewRule.ParentRule Is Nothing Then
    '        'Regla Padre
    '        sql = "Insert into wfrules values(" & NewRule.ID & ",'" & NewRule.Name & "'," & NewRule.WFStep.ID & "," & NewRule.RuleType & ",0, " & NewRule.ParentType & ",'" & NewRule.RuleClass & "'," & CInt(NewRule.Enable) & "," & NewRule.Version & ")"
    '    Else
    '        'Regla Hija de otra regla
    '        '            Dim ParamId As Int32 = CInt(Server.Con.ExecuteScalar(CommandType.Text, "select id from wfruleparams where rule_id=" & NewRule.ParentRule.Id))
    '        sql = "Insert into wfrules values(" & NewRule.ID & ",'" & NewRule.Name & "'," & NewRule.WFStep.ID & "," & NewRule.RuleType & "," & NewRule.ParentRule.ID & "," & NewRule.ParentType & ",'" & NewRule.RuleClass & "'," & CInt(NewRule.Enable) & "," & NewRule.Version & ")"
    '    End If

    '    Server.Con.ExecuteNonQuery(CommandType.Text, sql)


    '    sql = Nothing
    'End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Metodo para persistir una regla en la base de datos
    ''' </summary>
    ''' <param name="NewRule">Objeto WFRule que se desea persistir</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' 	[Marcelo]	22/10/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub InsertRule(ByVal ID As Int64, ByVal ParentRule As IRule, ByVal WFStepID As Int64, ByVal Name As String, ByVal RuleType As TypesofRules, ByVal ParentType As TypesofRules, ByVal RuleClass As String, ByVal Enable As Boolean, ByVal Version As Int32)
        Dim sql As String = String.Empty
        Dim EnableValue As Byte = 0
        '(pablo)
        If Enable Then
            EnableValue = 1
        End If

        If ParentRule Is Nothing Then
            ' Regla Padre
            sql = "Insert into wfrules ([Id],[Name],[step_Id],[Type],[ParentId],[ParentType],[Class],[Enable],[Version],[IconId]) values(" & ID & ",'" & Name & "'," & WFStepID & "," & RuleType & ",0, " & ParentType & ",'" & RuleClass & "'," & EnableValue.ToString & "," & Version & "," & 31 & ")"
        Else
            ' Regla Hija de otra regla
            'Dim ParamId As Int32 = CInt(Server.Con.ExecuteScalar(CommandType.Text, "select id from wfruleparams where rule_id=" & NewRule.ParentRule.Id))
            sql = "Insert into wfrules values(" & ID & ",'" & Name & "'," & WFStepID & "," & RuleType & "," & ParentRule.ID & "," & ParentType & ",'" & RuleClass & "'," & EnableValue.ToString & "," & Version & "," & 31 & ")"
        End If

        Server.Con.ExecuteNonQuery(CommandType.Text, sql)

        sql = Nothing
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Metodo para persistir una regla en la base de datos
    ''' </summary>
    ''' <param name="NewRule">Objeto WFRule que se desea persistir</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Marcelo]	14/01/2010	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub InsertRule(ByVal ID As Int64, ByVal ParentRuleID As Int64, ByVal WFStepID As Int64, ByVal Name As String, ByVal RuleType As TypesofRules, ByVal ParentType As TypesofRules, ByVal RuleClass As String, ByVal Enable As Boolean, ByVal Version As Int32)
        Dim sql As String = String.Empty
        Dim EnableValue As Byte = 0
        '(pablo)
        If Enable Then
            EnableValue = 1
        End If

        sql = "Insert into wfrules values(" & ID & ",'" & Name & "'," & WFStepID & "," & RuleType & "," & ParentRuleID & "," & ParentType & ",'" & RuleClass & "'," & EnableValue.ToString & "," & Version & "," & 31 & ")"
        Server.Con.ExecuteNonQuery(CommandType.Text, sql)

        sql = Nothing
    End Sub

    Public Shared Sub InsertRule(ByVal id As Int64, ByVal name As String, ByVal WfStepId As Int32, ByVal RuleType As Int32, ByVal ParentRuleId As Int64, ByVal ParentType As Int32, ByVal RuleClass As String, ByVal Enable As Boolean, ByVal Version As Int32)
        Dim EnableValue As Byte = 0
        '(pablo)
        If Enable Then
            EnableValue = 1
        End If
        Dim sql As String = "Insert into wfrules values(" & id & ",'" & name & "'," & WfStepId & "," & RuleType & "," & ParentRuleId & "," & ParentType & ",'" & RuleClass & "'," & EnableValue.ToString & "," & Version & "," & 31 & ")"
        Server.Con.ExecuteNonQuery(CommandType.Text, sql)

        sql = Nothing
    End Sub

    Public Shared Sub InsertRuleParamItem(ByVal RuleId As Int32, ByVal Item As Int32, ByVal Value As String)
        If Server.ServerType = DBTypes.MSSQLServer7Up OrElse Server.ServerType = DBTypes.MSSQLServer Then
            Dim parvalues() As Object = {RuleId, Item, Value}
            Server.Con.ExecuteNonQuery("Zsp_workflow_100_InsertRuleParamItem", parvalues)

            parvalues = Nothing
        Else
            'TODO Oracle
            Dim sql As String = "Insert into WFRuleParamItems (rule_id,item,c_value) values(" & RuleId & "," & Item & ",'" & Value & "')"
            Server.Con.ExecuteNonQuery(CommandType.Text, sql)

            sql = Nothing
        End If
    End Sub
    <Obsolete("Las entidades no corresponden a la capa de datos")>
    Public Shared Sub InsertRuleParamItem(ByVal ZConditionParam As WFRuleParent, ByVal Item As Int32, ByVal Value As String)
        If Server.ServerType = DBTypes.MSSQLServer7Up OrElse Server.ServerType = DBTypes.MSSQLServer Then
            Dim parvalues() As Object = {ZConditionParam.ID, Item, Value}
            Server.Con.ExecuteNonQuery("Zsp_workflow_100_InsertRuleParamItem", parvalues)

            parvalues = Nothing
        Else
            'TODO Oracle
            Dim sql As String = "Insert into WFRuleParamItems (rule_id,item,c_value) values(" & ZConditionParam.ID & "," & Item & ",'" & Value & "')"
            Server.Con.ExecuteNonQuery(CommandType.Text, sql)

            sql = Nothing
        End If
    End Sub

#End Region

#Region "Delete"
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Metodo para eliminar una Regla
    ''' </summary>
    ''' <param name="Rule">Objeto WFRule que se desea eliminar</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	22/09/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    <Obsolete("Las entidades no corresponden a la capa de datos")>
    Public Shared Sub DeleteRule(ByRef rule As WFRuleParent)
        If Server.ServerType = DBTypes.MSSQLServer7Up OrElse Server.ServerType = DBTypes.MSSQLServer Then
            Dim parvalues() As Object = {rule.ID}
            Server.Con.ExecuteNonQuery("Zsp_workflow_100_DeleteRule", parvalues)

            parvalues = Nothing
        Else
            'TODO Oracle
            Dim sql As String = "Delete from wfrules where id=" & rule.ID
            Server.Con.ExecuteNonQuery(CommandType.Text, sql)

            sql = Nothing
        End If
    End Sub

    Public Shared Sub DeleteRuleByID(ByVal id As Int64)
        If Server.ServerType = DBTypes.MSSQLServer7Up OrElse Server.ServerType = DBTypes.MSSQLServer Then
            Dim parvalues() As Object = {id}
            Server.Con.ExecuteNonQuery("Zsp_workflow_100_DeleteRule", parvalues)

            parvalues = Nothing
        Else
            'TODO Oracle
            Dim sql As String = "Delete from wfrules where id=" & id
            Server.Con.ExecuteNonQuery(CommandType.Text, sql)

            sql = Nothing
        End If
    End Sub
    'Public Shared Sub DeleteRuleByWFStep(ByVal id As Int32)
    '    If Server.ServerType = DBTYPES.MSSQLServer7Up OrElse Server.ServerType = DBTYPES.MSSQLServer Then
    '        Dim parvalues() As Object = {id}
    '        Server.Con.ExecuteNonQuery("Zsp_workflow_100_DeleteRule", parvalues)
    '    Else
    '        'TODO Oracle
    '        Dim sql As String = "Delete from wfrules where id=" & id
    '        Server.Con.ExecuteNonQuery(CommandType.Text, sql)
    '    End If
    'End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Marca una regla para realizar un drag and drop
    ''' </summary>
    ''' <param name="Rule"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Martin]	19/10/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------

    <Obsolete("Las entidades no corresponden a la capa de datos")>
    Public Shared Sub SetFloatingRule(ByRef rule As WFRuleParent)
        Dim strupdate As String = "Update wfrules set parentid = 0, parenttype = 14, type=10 where id = " & rule.ID
        'strupdate = "Update wfrules set parentid = 0, parenttype = 14,step_id = 0 where id = " & Rule.ID
        Server.Con.ExecuteNonQuery(CommandType.Text, strupdate)

        strupdate = Nothing
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Finaliza el drag and drop de una regla
    ''' </summary>
    ''' <param name="Rule"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Martin]	19/10/2006	Created
    ''' 	[Gaston]	12/12/2008	Modified    Se agrego un nuevo parámetro: ruleType
    ''' </history>
    ''' -----------------------------------------------------------------------------
    <Obsolete("Las entidades no corresponden a la capa de datos")>
    Public Shared Sub AttachAFloatingRule(ByRef rule As WFRuleParent, ByVal StepId As Int32, ByVal ruleType As TypesofRules, ByVal ParentId As Int32, ByVal ParentType As Int32)

        'Dim strupdate As String = "Update wfrules set parentid = " & ParentId & ",parenttype = " & ParentType & " ,step_id = " & StepId & " where id = " & rule.ID
        Dim strupdate As String = "Update wfrules set step_id = " & StepId & ", type = " & ruleType & ", parentid = " & ParentId & ", parenttype = " & ParentType & " where id = " & rule.ID
        Server.Con.ExecuteNonQuery(CommandType.Text, strupdate)
        strupdate = Nothing

    End Sub

    <Obsolete("Las entidades no corresponden a la capa de datos")>
    Public Shared Sub UpdateRule(ByRef rule As WFRuleParent, ByVal ParentId As Int32, ByVal ParentType As Int32, ByVal StepId As Int32)
        Dim strupdate As String = "Update wfrules set parentid = " & ParentId & ",parenttype = " & ParentType & " ,step_id = " & StepId & " where id = " & rule.ID
        Server.Con.ExecuteNonQuery(CommandType.Text, strupdate)

        strupdate = Nothing
    End Sub

    <Obsolete("Las entidades no corresponden a la capa de datos")>
    Public Shared Sub UpdateRule(ByRef rule As WFRuleParent, ByVal Type As Int32, ByVal ParentId As Int32, ByVal ParentType As Int32, ByVal StepId As Int32)
        Dim strupdate As String = "Update wfrules set type=" & Type & ",parentid = " & ParentId & ",parenttype = " & ParentType & " ,step_id = " & StepId & " where id = " & rule.ID
        Server.Con.ExecuteNonQuery(CommandType.Text, strupdate)

        strupdate = "Update wfrules set parenttype = " & Type & " where parentid = " & rule.ID
        Server.Con.ExecuteNonQuery(CommandType.Text, strupdate)

        strupdate = Nothing
    End Sub

    ''' <summary>
    ''' ELimina los parametros de un regla
    ''' </summary>
    ''' <param name="Rule">Regla padre</param>
    ''' <remarks>Esto deberia borrarlo la base de datos en cascada al borrar la regla</remarks>
    'Public Shared Sub DeleteParams(byref rule As WFRuleParent)
    '    If Server.ServerType = DBTYPES.MSSQLServer7Up OrElse Server.ServerType = DBTYPES.MSSQLServer Then
    '        Dim parvalues() As Object = {Rule.Id}
    '        Server.Con.ExecuteNonQuery("Zsp_workflow_100_DeleteRuleParams", parvalues)
    '    Else
    '        'TODO: SP Oracle
    '        Dim sql As String = "Delete from wfruleparams where id=" & Rule.Id
    '        Server.Con.ExecuteNonQuery(CommandType.Text, sql)
    '    End If
    'End Sub
#End Region

#Region "Update"
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Metodo para actualizar el nombre de una regla existente
    ''' </summary>
    ''' <param name="Rule">Objeto WFRule con el nombre modificado</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    <Obsolete("Las entidades no corresponden a la capa de datos")>
    Public Shared Sub UpdateRuleName(ByVal rule As WFRuleParent)
        Dim sql As String = "UPDATE wfrules SET name = '" & rule.Name & "' where id = " & rule.ID
        Try
            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            sql = Nothing
        End Try
    End Sub

    Public Shared Sub UpdateRuleNameByID(ByVal Id As Int64, ByVal Name As String)
        Dim sql As String = "UPDATE wfrules SET name = '" & Name & "' where id = " & Id
        Try
            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            sql = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' Actualiza la clase de la regla
    ''' </summary>
    ''' <param name="Id">Id de la regla</param>
    ''' <param name="ClassName">Nueva clase</param>
    ''' <remarks></remarks>
    Public Shared Sub updateClass(ByVal Id As Int32, ByVal ClassName As String)
        Dim sql As String = "UPDATE wfrules SET Class = '" & ClassName & "' where id = " & Id
        Try
            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
            sql = "delete wfruleparamitems where rule_id=" & Id
            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            sql = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' Método que sirve para actualizar una propiedad de una regla
    ''' </summary>
    ''' <param name="RuleAction">Regla a actualizar</param>
    ''' <param name="Item">Propiedad</param>
    ''' <param name="Value">Valor</param>
    ''' <param name="carp"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	10/07/2008	Modified    Se agrego un nuevo parametro Optional (ObjectTypes), un store 
    '''                                         version 2 para SQL y se modifico la consulta en Oracle
    ''' </history>
    Public Shared Sub UpdateParamItem(ByVal RuleActionId As Int64, ByVal Item As Int32, ByVal Value As String, Optional ByVal objectTypes As Int32 = ObjectTypes.None, Optional ByVal carp As Boolean = False)

        Try
            If Server.isOracle Then
                Value = Value.Replace("'", "''")
            End If
            If ((Server.ServerType = DBTypes.MSSQLServer7Up) OrElse (Server.ServerType = DBTypes.MSSQLServer)) Then
                Dim parvalues() As Object = {Value, RuleActionId, Item, objectTypes}
                'Server.Con.ExecuteNonQuery("Zsp_workflow_100_UpdateParamItem", parvalues)
                Server.Con.ExecuteNonQuery("Zsp_workflow_300_UpdateParamItem", parvalues)
                parvalues = Nothing
            Else
                Dim sql As String
                sql = "select count(1) from WFRuleParamItems where  rule_id=" & RuleActionId & " And Item = " & Item
                Dim i As Int32 = Server.Con.ExecuteScalar(CommandType.Text, sql)

                If i = 0 Then
                    sql = "INSERT INTO WFRuleParamItems (Rule_id, Item, C_Value, ObjectTypes) VALUES(" & RuleActionId & "," & Item & ",'" & Value & "'," & objectTypes & ")"
                Else
                    sql = "Update WFRuleParamItems set c_value='" & Value & "', ObjectTypes = " & objectTypes _
                                    & " where rule_id=" & RuleActionId & " And Item = " & Item
                End If
                Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                sql = Nothing
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    ''' <summary>
    '''  Método que sirve para actualizar una propiedad de una regla
    ''' </summary>
    ''' <param name="RuleAction">Regla a actualizar</param>
    ''' <param name="Item">Propiedad</param>
    ''' <param name="Value">Valor</param>
    ''' <param name="carp"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	10/07/2008	Modified    Se agrego un nuevo parametro Optional (ObjectTypes), un store 
    '''                                         version 2 para SQL y se modifico la consulta en Oracle
    ''' </history>
    <Obsolete("Las entidades no corresponden a la capa de datos")>
    Public Shared Sub UpdateParamItem(ByVal RuleAction As WFRuleParent, ByVal Item As Int32, ByVal Value As String, Optional ByVal objectTypes As Int32 = ObjectTypes.None)

        Try
            If ((Server.ServerType = DBTypes.MSSQLServer7Up) OrElse (Server.ServerType = DBTypes.MSSQLServer)) Then
                Dim parvalues() As Object = {Value, RuleAction.ID, Item, objectTypes}
                'Server.Con.ExecuteNonQuery("Zsp_workflow_100_UpdateParamItem", parvalues)
                Server.Con.ExecuteNonQuery("Zsp_workflow_200_UpdateParamItem", parvalues)
                parvalues = Nothing
            Else
                'TODO Oracle
                Dim sql As String = "Update WFRuleParamItems set c_value='" & Value & "', ObjectTypes = " & objectTypes _
                                    & " where rule_id = " & RuleAction.ID & " And Item = " & Item
                Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                sql = Nothing
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Metodo para actualizar el estdo de una regla
    ''' </summary>
    ''' <param name="p_iRuleId">id de la regla</param>
    ''' <param name="p_bEstado">estado</param>
    ''' <remarks>
    '''  El estado de la regla esta mapeado en la columna enable.
    ''' </remarks>
    ''' <history>
    ''' 	[Oscar]	03/10/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub UpdateRuleById(ByVal p_iRuleId As Int32, ByVal p_bEstado As Boolean)
        Dim sSql As String = "UPDATE wfrules SET Enable = " & Convert.ToInt32(p_bEstado) & " where id = " & p_iRuleId
        Server.Con.ExecuteNonQuery(CommandType.Text, sSql)
        sSql = Nothing
    End Sub

    Shared Sub UpdateParentRuleId(ByVal RuleID As Int64, ByVal ParentRuleID As Int64)
        Dim query As New StringBuilder
        query.Append("UPDATE WFRules SET ParentId = ")
        query.Append(ParentRuleID.ToString)
        query.Append(" WHERE Id = ")
        query.Append(RuleID.ToString)
        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
    End Sub

    ''' <summary>
    ''' Método que sirve para actualizar el stepId de una determinada regla
    ''' </summary>
    ''' <param name="ruleId">Id de la regla</param>
    ''' <param name="stepId">Id de la nueva etapa</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	04/11/2008	Created
    ''' </history>
    Shared Sub updateStepIdofRule(ByVal ruleId As Int64, ByVal stepId As Int64)
        Dim query As New StringBuilder
        query.Append("UPDATE WFRules SET step_id = ")
        query.Append(stepId)
        query.Append(" WHERE Id = ")
        query.Append(ruleId)
        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString())
        query = Nothing
    End Sub

#End Region

End Class