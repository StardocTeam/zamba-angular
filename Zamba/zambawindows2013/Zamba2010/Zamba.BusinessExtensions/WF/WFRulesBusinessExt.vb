Imports Zamba.Data

Public Class WFRulesBusinessExt
    Private Shared _rulesConditions As Dictionary(Of Long, String) = New Dictionary(Of Long, String)()

    ''' <summary>
    ''' Obtiene en un string todas la condiciones de la solapa de habilitacion
    ''' El mismo es devuelto en forma de condicion
    ''' </summary>
    ''' <param name="ruleID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRuleCondition(ByVal ruleID As Long) As String
        SyncLock _rulesConditions
            Dim rulesFactoryExt As New WFRulesFactoryExt

            If Not _rulesConditions.ContainsKey(ruleID) Then
                _rulesConditions.Add(ruleID, rulesFactoryExt.GetRuleCondition(ruleID))
            End If

            Return _rulesConditions(ruleID)
        End SyncLock
    End Function

    ''' <summary>
    ''' Obtiene el nombre del proceso y la etapa donde se encuentra una regla
    ''' </summary>
    ''' <param name="ruleId">Id de regla</param>
    ''' <returns>Array de string que contiene el nombre del proceso y etapa al que pertenece la regla</returns>
    ''' <remarks></remarks>
    Public Function GetWfAndStepNameByRuleId(ByVal ruleId As Int64) As String()
        Dim wfRulesFactoryExt As New WFRulesFactoryExt
        Dim dtRuleInfo As DataTable = wfRulesFactoryExt.GetWfAndStepNameByRuleId(ruleId)
        wfRulesFactoryExt = Nothing

        If dtRuleInfo.Rows.Count > 0 Then
            Return New String() {dtRuleInfo.Rows(0)("WFNAME").ToString, dtRuleInfo.Rows(0)("STEPNAME").ToString}
        Else
            Return New String() {String.Empty, String.Empty}
        End If
    End Function

    ''' <summary>
    ''' Devuelve los pasos de caso de uso para el tipo de regla especificada como parametro
    ''' </summary>
    ''' <param name="typeID">Tipo de regla (ZUC_UCS)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetUseCaseTypeSteps(ByVal Rule As IRule) As DataSet
        Dim wfRulesFactoryExt As New WFRulesFactoryExt

        Dim Replaces As New Dictionary(Of String, String)

        Replaces.Add("<<Workflow>>", WFBusiness.GetWorkflowNameByWFId(WFBusiness.GetWorkflowIdByStepId(Rule.WFStepId)))
        Replaces.Add("<<Etapa>>", WFStepBusiness.GetStepNameById(Rule.WFStepId))

        If Rule.ParentType = Enumerators.TypesofRules.AccionUsuario Then
            'Busca en la tabla si existe un nombre de acción de usuario para esa regla
            Dim WFB As New WFBusiness
            Dim userActionName As String = WFB.GetUserActionName(Rule.ID, Rule.WFStepId, Rule.Name, True)
            WFB = Nothing
            'Si el nombre no existe entonces le asigna el nombre de la regla
            If String.IsNullOrEmpty(userActionName) Then
                userActionName = Rule.Name
            End If

            Replaces.Add("<<AccionDeUsuario>>", userActionName)
        ElseIf Rule.ParentType = Enumerators.TypesofRules.Eventos Then
            Return wfRulesFactoryExt.GetUseCaseTypeSteps(Rule.RuleType, Replaces)
        End If

        Return wfRulesFactoryExt.GetUseCaseTypeSteps(Rule.ParentType, Replaces)
    End Function
End Class
