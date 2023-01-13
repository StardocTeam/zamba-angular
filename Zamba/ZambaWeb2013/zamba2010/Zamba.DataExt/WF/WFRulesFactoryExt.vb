Imports Zamba.Servers
Imports System.Text

Public Class WFRulesFactoryExt

    ''' <summary>
    ''' Llama a stored procedure zsp_rules_100_MakeStringFormRuleCondition que devuelve en un string la condicion de habilitacion de la regla
    ''' </summary>
    ''' <param name="ruleID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRuleCondition(ByVal ruleID As Long) As String
        Dim ds As DataSet = Servers.Server.Con.ExecuteDataset("zsp_rules_100_MakeStringFormRuleCondition", New Object() {ruleID})

        If ds IsNot Nothing AndAlso ds.Tables IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0)("Condition").ToString()
        Else
            Return String.Empty
        End If
    End Function

    ''' <summary>
    ''' Obtiene el nombre del proceso y la etapa donde se encuentra una regla
    ''' </summary>
    ''' <param name="ruleId">Id de regla</param>
    ''' <returns>Datatable con dos columnas, WFNAME y STEPNAME</returns>
    ''' <remarks></remarks>
    Public Function GetWfAndStepNameByRuleId(ByVal ruleId As Int64) As DataTable
        Dim query As String = "SELECT W.NAME AS WFNAME, S.NAME AS STEPNAME FROM WFWORKFLOW W " & _
            "INNER JOIN WFSTEP S ON W.WORK_ID=S.WORK_ID " & _
            "INNER JOIN WFRULES R ON R.STEP_ID=S.STEP_ID " & _
            "WHERE R.ID=" & ruleId.ToString

        Return Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)
    End Function

    ''' <summary>
    ''' Devuelve los pasos de caso de uso para el tipo de regla especificada como parametro
    ''' </summary>
    ''' <param name="typeID">Tipo de regla (ZUC_UCS)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetUseCaseTypeSteps(ByVal typeID As Long, ByVal Replaces As Dictionary(Of String, String)) As DataSet
        Dim sb As New StringBuilder()
        Dim description As String

        description = "Description"
        For Each de As KeyValuePair(Of String, String) In Replaces
            description = "Replace(" + description + ",'" + de.Key + "','" & de.Value & "')"
        Next

        sb.Append("select StepID as Paso, ")
        sb.Append(description)
        sb.Append(" as Descripcion from ZUC_UCS where TypeID=")
        sb.Append(typeID)

        Return Servers.Server.Con.ExecuteDataset(CommandType.Text, sb.ToString())
    End Function
End Class