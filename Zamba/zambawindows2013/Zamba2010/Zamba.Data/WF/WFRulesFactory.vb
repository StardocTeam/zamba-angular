Imports Zamba.Core.Enumerators
Imports Zamba.Core
Imports System.Text

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
    ''' Obtiene reglas con posibilidades de ser removidas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetRulesToRemove() As DataTable
        Dim query As String = "select id,name,parentid,type,parenttype,class,enable from wfrules"
        Return Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)
    End Function

    ''' <summary>
    ''' Obtiene reglas con posibilidades de ser removidas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetNeverSavedRule() As DataTable
        Dim query As String = "select id,name,parentid,type,parenttype,class,enable from wfrules where class <> 'IFBRANCH' and class <> 'DODESIGN' and id not in (select distinct(rule_id) from wfruleparamitems)"
        Return Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)
    End Function

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
        Dim id As String = Server.Con.ExecuteScalar(CommandType.Text, QueryBuilder.ToString())
        QueryBuilder.Remove(0, QueryBuilder.Length)
        QueryBuilder = Nothing
        If Not String.IsNullOrEmpty(id) Then
            Return Int64.Parse(id)
        Else
            Return 0
        End If
    End Function


    Public Shared Function GetRuleInfo(ByVal id As Int64, Optional ByVal workflowId As Int64 = -1) As DataTable
        Dim sb As New StringBuilder()
        sb.Append("select distinct(r.id) as ID,")
        sb.Append("r.name as Nombre,")
        sb.Append("r.class as Tipo,")
        sb.Append("s.step_id as " & Chr(34) & "ID Etapa" & Chr(34) & ",")
        sb.Append("s.name as " & Chr(34) & "Nombre de Etapa" & Chr(34) & ",")
        sb.Append("w.work_id as " & Chr(34) & "ID de WF" & Chr(34) & ",")
        sb.Append("w.name as " & Chr(34) & "Nombre de WF" & Chr(34) & " from wfrules r ")
        sb.Append("inner join wfstep s on r.step_id = s.step_id ")
        sb.Append("inner join wfworkflow w on w.work_id = s.work_id ")
        sb.Append("inner join wfruleparamitems p on r.id = p.rule_id ")
        sb.Append("where ")

        If workflowId <> -1 Then
            sb.Append(" w.work_id = ")
            sb.Append(workflowId)
            sb.Append(" and ")
        End If

        sb.Append("r.id = ")
        sb.Append(id.ToString)
        sb.Append(" order by w.name, s.name, r.name")

        If Server.isOracle Then
            sb = sb.Replace("value", "c_value")
        End If

        Return Server.Con.ExecuteDataset(CommandType.Text, sb.ToString).Tables(0)
    End Function

    Public Shared Function GetRuleInfo(ByVal name As String, Optional ByVal workflowId As Int64 = -1) As DataTable
        Dim sb As New StringBuilder()
        sb.Append("select distinct(r.id) as ID,")
        sb.Append("r.name as Nombre,")
        sb.Append("r.class as Tipo,")
        sb.Append("s.step_id as " & Chr(34) & "ID Etapa" & Chr(34) & ",")
        sb.Append("s.name as " & Chr(34) & "Nombre de Etapa" & Chr(34) & ",")
        sb.Append("w.work_id as " & Chr(34) & "ID de WF" & Chr(34) & ",")
        sb.Append("w.name as " & Chr(34) & "Nombre de WF" & Chr(34) & " from wfrules r ")
        sb.Append("inner join wfstep s on r.step_id = s.step_id ")
        sb.Append("inner join wfworkflow w on w.work_id = s.work_id ")
        sb.Append("inner join wfruleparamitems p on r.id = p.rule_id ")
        sb.Append("where ")

        If workflowId <> -1 Then
            sb.Append(" w.work_id = ")
            sb.Append(workflowId)
            sb.Append(" and ")
        End If

        sb.Append("(r.Name like '%")
        sb.Append(name)
        sb.Append("%'")
        sb.Append(" or r.Class like '%")
        sb.Append(name)
        sb.Append("%')")

        sb.Append(" order by w.name, s.name, r.name")

        If Server.isOracle Then
            sb = sb.Replace("value", "c_value")
        End If

        Return Server.Con.ExecuteDataset(CommandType.Text, sb.ToString).Tables(0)
    End Function

    Public Shared Function GetRuleInfoBySettings(ByVal settings As String, Optional ByVal workflowId As Int64 = -1) As DataTable
        Dim sb As New StringBuilder()
        sb.Append("SELECT DISTINCT(r.id) AS ID, ")
        sb.Append("r.name AS Nombre, ")
        sb.Append("r.class AS Tipo, ")
        sb.Append("s.step_id AS " & Chr(34) & "ID Etapa" & Chr(34) & ", ")
        sb.Append("s.name AS " & Chr(34) & "Nombre de Etapa" & Chr(34) & ", ")
        sb.Append("w.work_id AS " & Chr(34) & "ID de WF" & Chr(34) & ", ")
        sb.Append("w.name AS " & Chr(34) & "Nombre de WF" & Chr(34) & ", ")
        sb.Append("p.value AS " & Chr(34) & "Valor del Parametro" & Chr(34) & " ")
        sb.Append("FROM wfrules r ")
        sb.Append("INNER JOIN wfstep s ")
        sb.Append("ON r.step_id = s.step_id ")
        sb.Append("INNER JOIN wfworkflow w ")
        sb.Append("ON w.work_id = s.work_id ")
        sb.Append("INNER JOIN wfruleparamitems p ")
        sb.Append("ON r.id = p.rule_id ")
        sb.Append("WHERE ")

        If workflowId <> -1 Then
            sb.Append(" w.work_id = ")
            sb.Append(workflowId)
            sb.Append(" And ")
        End If

        sb.Append("p.value Like '%")
        sb.Append(settings.ToLower())
        sb.Append("%' ORDER BY w.name, s.name, r.name")

        If Server.isOracle Then
            sb = sb.Replace("value", "c_value")
        End If

        Return Server.Con.ExecuteDataset(CommandType.Text, sb.ToString).Tables(0)
    End Function

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

    Public Shared Function GetRulesIdByStep(ByVal stepId As Int64, ByVal ruleTypeId As Int32, ByVal isEvent As Boolean) As DataTable

        Dim QueryBuilder As New StringBuilder

        QueryBuilder.Append("SELECT Id FROM WFRules WHERE step_id=")
        QueryBuilder.Append(stepId.ToString())

        If isEvent Then
            QueryBuilder.Append(" AND parenttype=15 and Type=")
            QueryBuilder.Append(ruleTypeId.ToString())
        Else
            QueryBuilder.Append(" AND parenttype=")
            QueryBuilder.Append(ruleTypeId.ToString())
        End If

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
            sql += " wfr.Name || ' (' || to_char(ID) || ')' || ' - ' || wfs.Name || ' - ' || wf.Name AS NAME, ID  "
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


    '[Ezequiel] - 04/5/11 - Metodo que obtiene las reglas de un wf.
    Public Shared Function GetRulesByWFIds(ByVal wfid As String) As DataSet
        Dim sql As String = "Select wfrules.*, work_id from wfrules inner join wfstep on wfrules.step_id = wfstep.step_id where work_id in (" & wfid & ") order by wfrules.name"
        Dim Dstemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)
        sql = Nothing
        Return Dstemp
    End Function

    Public Shared Function GetRulesParamItemsByWFIds(ByVal wfid As String) As DataSet
        Dim sql As String
        If Server.isOracle Then
            sql = "Select rule_id,item,c_value as value from WFRuleParamItems where rule_id in (select id from wfrules where step_id in (select step_id from wfstep where work_id in (" & wfid & "))) Order By Item"
        Else
            sql = "Select * from WFRuleParamItems where rule_id in (select id from wfrules where step_id in (select step_id from wfstep where work_id in (" & wfid & "))) Order By Item"
        End If
        Dim Dstemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)

        sql = Nothing

        Return Dstemp
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
    ''' Obtiene las reglas para un Workflow
    ''' </summary>
    ''' <param name="WF">Objeto Workflow del cual se desea obtener las reglas</param>
    ''' <returns>Objeto DSRules</returns>
    ''' <remarks></remarks>
    ''' <summary>
    ''' Obtiene las reglas para un Workflow
    ''' </summary>
    ''' <param name="WF">Objeto Workflow del cual se desea obtener las reglas</param>
    ''' <returns>Objeto DSRules</returns>
    ''' <remarks></remarks>
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

            'If Dstemp.Tables(0).Rows.Count = 0 Then Return Dstemp

            ''Cargo Items de Parametros 
            'Restriction = "("
            'For Each r As DataRow In Dstemp.Tables(0).Rows
            '    Restriction += "Rule_id=" & r("ruleId") & " or "
            'Next
            'Restriction = Restriction.Substring(0, Restriction.Length - 3)
            'Restriction += ")"
            'sql = "Select Rule_id as ruleId, Item, Value from WFRuleParamItems where " & Restriction & " Order By Item"
            'Dstemp2 = Server.Con.ExecuteDataset(CommandType.Text, sql)
            'Dstemp.Merge(Dstemp2)
        End If

        Return Dstemp
    End Function


    ''' <summary>
    ''' Obtiene las reglas para un Workflow
    ''' </summary>
    ''' <param name="WF">Objeto Workflow del cual se desea obtener las reglas</param>
    ''' <returns>Objeto DSRules</returns>
    ''' <remarks></remarks>
    ''' <summary>
    ''' Devuelve el dsRules de una etapa
    ''' </summary>
    ''' <param name="StepID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetRulesDSByStepID(ByVal StepID As Int64) As DsRules
        Dim Dstemp As DataSet

        Dstemp = Server.Con.ExecuteDataset(CommandType.Text, "Select * from wfrules where step_id=" & StepID & " order by name")

        Dim Dsrules As New DsRules
        Dstemp.Tables(0).TableName = Dsrules.WFRules.TableName
        Dsrules.Merge(Dstemp.Tables(0))
        Dsrules.Tables(0).PrimaryKey = New DataColumn() {Dsrules.Tables(0).Columns(0)}

        If Dsrules.WFRules.Rows.Count = 0 Then Return Dsrules

        Dim sql As String

        sql = "Select rule_id, item, c_value as value from WFRuleParamItems inner join WFRules on WFRuleParamItems.Rule_id = WFRules.Id and WFRules.step_Id = " & StepID & " Order By Item"

        If Not Server.isOracle Then
            sql = sql.Replace("c_value", "value")
        End If

        Dstemp = Server.Con.ExecuteDataset(CommandType.Text, sql)

        Dstemp.Tables(0).TableName = Dsrules.WFRuleParamItems.TableName
        Dsrules.Merge(Dstemp.Tables(0))

        Dstemp.Dispose()
        Dstemp = Nothing

        sql = Nothing
        Return Dsrules
    End Function

    ''' <summary>
    ''' Devuelve el dsRules de una regla
    ''' </summary>
    ''' <param name="RuleId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetRulesDSByRuleId(ByVal RuleId As Int64) As DsRules
        Dim Dstemp As DataSet
        If Server.isOracle Then
            Dstemp = Server.Con.ExecuteDataset(CommandType.Text, "Select * from wfrules where id=" & RuleId)
        Else
            Dstemp = Server.Con.ExecuteDataset(CommandType.Text, "Select * from wfrules where id=" & RuleId)
        End If

        Dim Dsrules As New DsRules
        Dstemp.Tables(0).TableName = Dsrules.WFRules.TableName
        Dsrules.Merge(Dstemp.Tables(0))

        If Dsrules.WFRules.Rows.Count = 0 Then Return Dsrules

        Dim sql As String
        If Server.isOracle Then
            '           sql = "Select rule_id,item,c_value as value from WFRuleParamItems inner join WFRules on WFRuleParamItems.Rule_id = WFRules.Id where WFRules.rule_Id =   " & RuleId & " Order By Item"
            sql = "Select rule_id,item,c_value as value from WFRuleParamItems where rule_Id =   " & RuleId & " Order By Item"
            Dstemp = Server.Con.ExecuteDataset(CommandType.Text, sql)
        Else
            '            sql = "Select rule_id,item,value from WFRuleParamItems inner join WFRules on WFRuleParamItems.Rule_id = WFRules.Id where WFRules.rule_Id =   " & RuleId & " Order By Item"
            sql = "SELECT rule_id,item,value FROM WFRuleParamItems WHERE rule_Id =   " & RuleId & " Order By Item"
            Dstemp = Server.Con.ExecuteDataset(CommandType.Text, sql)
        End If

        Dstemp.Tables(0).TableName = Dsrules.WFRuleParamItems.TableName
        Dsrules.Merge(Dstemp.Tables(0))

        Dstemp.Dispose()
        Dstemp = Nothing

        sql = Nothing
        Return Dsrules
    End Function


    ''' <summary>
    ''' Método que sirve para obtener los argumentos de la regla duplicada y devolver un DsRules
    ''' </summary>
    ''' <param name="ruleId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]    01/10/2008	Created
    ''' </history>
    Public Shared Function getArgumentsOfDuplicateRule(ByVal ruleId As Long) As DsRules

        Dim Dsrules As New DsRules
        Dim Dstemp As DataSet
        Dim sql As String

        If (Server.isOracle) Then
            sql = "Select rule_id,item,c_value as value from WFRuleParamItems where rule_id = " & ruleId
        Else
            sql = "Select * from WFRuleParamItems where rule_id = " & ruleId
        End If

        Dstemp = Server.Con.ExecuteDataset(CommandType.Text, sql)
        Dstemp.Tables(0).TableName = Dsrules.WFRuleParamItems.TableName
        Dsrules.Merge(Dstemp.Tables(0))

        Dstemp.Dispose()
        Dstemp = Nothing
        sql = Nothing

        Return Dsrules

    End Function

    ''' <summary>
    ''' Obtiene un datatable con todas las opciones de las rglas pertenecientes a un wf
    ''' [2/5/11] Ezequiel - Created
    ''' </summary>
    ''' <param name="wflist"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetRuleOptionsByWFList(ByVal wflist As String) As DataTable
        Dim Ds As DataSet = Nothing
        If Server.isOracle Then
            ''Dim parNames() As String = {"workids", "io_cursor"}
            'Dim parTypes() As Object = {13, 5}
            Dim parValues() As Object = {wflist, 2}
            Ds = Server.Con.ExecuteDataset("zsp_workflow_200.GetRulesPreferences", parValues)
        Else
            Dim parameters() As Object = {(wflist)}
            Ds = Server.Con.ExecuteDataset("zsp_workflow_200_GetRulesPreferences", parameters)
        End If
        Return Ds.Tables(0)
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
    Public Shared Function GetRuleParamItems(ByVal p_iRuleID As Int64) As DsRules

        Dim sql As String = "SELECT Id, Name, step_Id, Type, ParentId, ParentType, Class, Enable, Version FROM wfRules WHERE Id=" & p_iRuleID.ToString
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)
        Dim Dsrules As New DsRules

        ds.Tables(0).TableName = Dsrules.Tables(0).TableName
        Dsrules.Merge(ds)

        If (Server.isOracle) Then

            Dim counter As Short = 0

            While (counter <> ds.Tables(0).Columns.Count)
                Dsrules.Tables("WFRules").Columns.RemoveAt(Dsrules.Tables("WFRules").Columns.Count - 1)
                counter = counter + 1
            End While

        End If

        ds.Dispose()
        ds = Nothing

        If (Server.isOracle) Then
            sql = "Select rule_id,item,c_value as value,objecttypes from WFRuleParamItems where Rule_id=" & p_iRuleID.ToString & " Order By Item"
        Else
            sql = "Select * from WFRuleParamItems where Rule_id=" & p_iRuleID.ToString & " Order By Item"
        End If

        Dim Dstemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)

        sql = Nothing

        Dstemp.Tables(0).TableName = Dsrules.WFRuleParamItems.TableName
        Dsrules.Merge(Dstemp.Tables(0))

        If (Server.isOracle) Then

            Dim counter As Short = 0

            While (counter <> Dstemp.Tables(0).Columns.Count - 1)
                Dsrules.Tables("WFRuleParamItems").Columns.RemoveAt(Dsrules.Tables("WFRuleParamItems").Columns.Count - 2)
                counter = counter + 1
            End While

        End If

        Dstemp.Dispose()
        Dstemp = Nothing

        Return Dsrules

    End Function

    '''' <summary>
    '''' Obtiene una regla a partir de su Id
    '''' </summary>
    '''' <param name="iRuleId">id de la regla</param>
    '''' <returns>DataSet tipado DsRules</returns>
    '''' <remarks></remarks>
    'Public Shared Function GetRuleById(ByVal iRuleId As Int32) As DsRules
    '    Dim sSql As String
    '    Dim dsRule As New DsRules
    '    Dim dsTemp As DataSet
    '    Try
    '        sSql = "Select * from WFRuleParamItems where Rule_id=" & iRuleId & " Order By Item"
    '        dsTemp = Server.Con.ExecuteDataset(CommandType.Text, sSql)
    '        dsTemp.Tables(0).TableName = dsRule.WFRuleParamItems.TableName
    '        dsRule.Merge(dsTemp.Tables(0))
    '    Catch ex As Exception
    '       zamba.core.zclass.raiseerror(ex)
    '    End Try
    '    Return dsRule
    'End Function

    ''' <summary>
    ''' Obtiene una regla a partir de su Id
    ''' </summary>
    ''' <param name="RuleId">id de la regla</param>
    ''' <returns>DsRules</returns>
    ''' <remarks></remarks>
    Public Shared Function GetRuleByID(ByVal p_iRuleID As Int64) As DsRules
        Dim dsRule As New DsRules

        Dim sql As String = "SELECT Id, Name, step_Id, Type, ParentId, ParentType, Class, Enable, Version FROM wfRules WHERE Id=" & p_iRuleID.ToString()
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)
        ds.Tables(0).TableName = dsRule.Tables(0).TableName
        dsRule.Merge(ds)

        ds.Dispose()
        ds = Nothing
        sql = Nothing

        Return dsRule
    End Function

    Public Shared Function GetRuleNameById(ByVal p_iRuleId As Int64) As String
        'Dim i_dsRule As DsRules

        Using i_dsRule As DsRules = GetRuleByID(p_iRuleId)
            'i_dsRule = GetRuleByID(p_iRuleId)
            If Not i_dsRule Is Nothing AndAlso i_dsRule.Tables(0).Rows.Count > 0 Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "se encontro una regla con Id: " & p_iRuleId & "con nombre " & i_dsRule.WFRules(0).Item("Name"))
                Return i_dsRule.WFRules(0).Item("Name")
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "no se encontro una regla con Id: " & p_iRuleId)
            End If
        End Using

        Return String.Empty
    End Function

    'Public Shared Function GetRule(ByVal ID As Int32, ByVal Name As String) As Object
    '    Return Nothing
    'End Function

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
        query.Append("DELETE FROM ZRuleOptbase WHERE RuleId =")
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
            query.Append("INSERT INTO ZRuleOptbase(id,RuleId, SectionId, ObjectId, ObjExtraData, ObjValue) VALUES(" & CoreData.GetNewID(IdTypes.RulePreference) & ",")
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

    '''' <summary>
    '''' Devuelve un listado de ids y nombres de reglas de actualizacion perteneciente a 1 etapa
    '''' </summary>
    '''' <param name="stepId"></param>
    '''' <history>Marcelo Created 10/12/2008</history>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Public Shared Function GetActualizationRulesIdsAndNames(ByVal stepId As Int64) As DataTable
    '    Dim query As String = "select Id, Name from wfrules where step_id = " & stepId.ToString() & " and ParentType = " & TypesofRules.Actualizacion
    '    Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, query)

    '    If (Not IsNothing(ds) AndAlso ds.Tables.Count > 0) Then
    '        Return ds.Tables(0)
    '    End If

    '    Return Nothing
    'End Function

    ''' <summary>
    ''' Devuelve un listado de ids y nombres de reglas de planificada perteneciente a 1 etapa
    ''' </summary>
    ''' <param name="stepId"></param>
    ''' <history>Martin Created 01/09/2009</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    'Public Shared Function GetScheduledRulesIdsAndNames(ByVal stepId As Int64) As DataTable
    '    Dim query As String = "select Id, Name from wfrules where step_id = " & stepId.ToString() & " and ParentType = " & TypesofRules.Planificada
    '    Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, query)

    '    If (Not IsNothing(ds) AndAlso ds.Tables.Count > 0) Then
    '        Return ds.Tables(0)
    '    End If

    '    Return Nothing
    'End Function
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
    ''' 	[Marcelo]	22/10/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub InsertRule(ByVal id As Int64,
                                 ByVal parentRuleId As Int64,
                                 ByVal wfStepId As Int32,
                                 ByVal name As String,
                                 ByVal ruleType As Int32,
                                 ByVal parentType As Int32,
                                 ByVal ruleClass As String,
                                 ByVal enable As Boolean,
                                 ByVal version As Int32)
        Dim sql As String = String.Empty
        Dim EnableValue As Int64 = 0
        If enable Then
            EnableValue = -1
        End If

        If parentRuleId = 0 Then
            ' Regla Padre
            If Server.isOracle Then
                sql = "Insert into wfrules values(" & id & ",'" & name & "'," & wfStepId & "," & ruleType & ",0, " & parentType & ",'" & ruleClass & "'," & EnableValue.ToString & "," & version & "," & 31 & ")"
            Else
                sql = "Insert into wfrules ([Id],[Name],[step_Id],[Type],[ParentId],[ParentType],[Class],[Enable],[Version],[IconId]) values(" & id & ",'" & name & "'," & wfStepId & "," & ruleType & ",0, " & parentType & ",'" & ruleClass & "'," & EnableValue.ToString & "," & version & "," & 31 & ")"
            End If
        Else
            ' Regla Hija de otra regla
            sql = "Insert into wfrules values(" & id & ",'" & name & "'," & wfStepId & "," & ruleType & "," & parentRuleId & "," & parentType & ",'" & ruleClass & "'," & EnableValue.ToString & "," & version & "," & 31 & ")"
        End If

        Server.Con.ExecuteNonQuery(CommandType.Text, sql)

        sql = Nothing
    End Sub

    Public Shared Sub InsertRuleParamItem(ByVal RuleId As Int64, ByVal Item As Int32, ByVal Value As String)
        If Server.isSQLServer Then
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
        If Server.isSQLServer Then
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

    Public Shared Sub DeleteRule(ByVal ruleid As Int64)
        'TODO Oracle
        Dim sql1 As String = "Delete from wfrules where id=" & ruleid
        Dim sql2 As String = "Delete from zruleoptbase where ruleid=" & ruleid
        Dim sql3 As String = "Delete from wfruleparamitems where rule_id=" & ruleid
        Server.Con.ExecuteNonQuery(CommandType.Text, sql2)
        Server.Con.ExecuteNonQuery(CommandType.Text, sql3)
        Server.Con.ExecuteNonQuery(CommandType.Text, sql1)
    End Sub


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
    Public Shared Sub SetFloatingRule(ByRef ruleid As Int64)
        Dim strupdate As String = "Update wfrules set parentid = 0, parenttype = 14, type=10 where id = " & ruleid
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

    Public Shared Sub AttachAFloatingRule(ByRef ruleid As Int64, ByVal StepId As Int32, ByVal ruleType As TypesofRules, ByVal ParentId As Int32, ByVal ParentType As Int32)
        Dim strupdate As String = "Update wfrules set step_id = " & StepId & ", type = " & ruleType & ", parentid = " & ParentId & ", parenttype = " & ParentType & " where id = " & ruleid
        Server.Con.ExecuteNonQuery(CommandType.Text, strupdate)
        strupdate = Nothing

    End Sub

    Public Shared Sub UpdateRule(ByRef ruleid As Int64, ByVal ParentId As Int32, ByVal ParentType As Int32, ByVal StepId As Int32)
        Dim strupdate As String = "Update wfrules set parentid = " & ParentId & ",parenttype = " & ParentType & " ,step_id = " & StepId & " where id = " & ruleid
        Server.Con.ExecuteNonQuery(CommandType.Text, strupdate)

        strupdate = Nothing
    End Sub


    Public Shared Sub UpdateRule(ByRef ruleid As Int64, ByVal Type As Int32, ByVal ParentId As Int32, ByVal ParentType As Int32, ByVal StepId As Int32)
        Dim strupdate As String = "Update wfrules set type=" & Type & ",parentid = " & ParentId & ",parenttype = " & ParentType & " ,step_id = " & StepId & " where id = " & ruleid
        Server.Con.ExecuteNonQuery(CommandType.Text, strupdate)

        strupdate = "Update wfrules set parenttype = " & Type & " where parentid = " & ruleid
        Server.Con.ExecuteNonQuery(CommandType.Text, strupdate)

        strupdate = Nothing
    End Sub

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
    Public Shared Sub UpdateRuleName(ByVal ruleid As Int64, rulename As String)
        Dim sql As String = "UPDATE wfrules SET name = '" & rulename & "' where id = " & ruleid
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

            Value = Value.Replace("'", "''")

            If Server.isSQLServer Then

                Dim sql As String
                sql = "select count(1) from WFRuleParamItems where  rule_id=" & RuleActionId & " And Item = " & Item
                Dim i As Int32 = Server.Con.ExecuteScalar(CommandType.Text, sql)

                If i = 0 Then
                    sql = "INSERT INTO WFRuleParamItems (Rule_id, Item, Value, ObjectTypes) VALUES(" & RuleActionId & "," & Item & ",'" & Value & "'," & objectTypes & ")"
                Else
                    sql = "Update WFRuleParamItems set value='" & Value & "', ObjectTypes = " & objectTypes _
                                    & " where rule_id=" & RuleActionId & " And Item = " & Item
                End If
                Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                sql = Nothing

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
            If Server.isSQLServer Then
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
    Public Shared Sub UpdateRuleById(ByVal p_iRuleId As Int64, ByVal p_bEstado As Boolean)
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

    Public Shared Sub UpdateParentType(ByVal ruleID As Int64, ByVal parentType As TypesofRules)
        Dim query As String = "UPDATE WFRules SET ParentType=" & parentType & " WHERE Id=" & ruleID
        Server.Con.ExecuteNonQuery(CommandType.Text, query)
    End Sub
#End Region

End Class