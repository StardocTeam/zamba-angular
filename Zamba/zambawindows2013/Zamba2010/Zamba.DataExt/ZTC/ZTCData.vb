Imports Zamba.Servers
Imports Zamba.Core
Imports System.Text

Public Class ZTCData

    Public Shared Function GetCases(ByVal objType As Int64, _
                                       ByVal objectId As Int64) As DataTable
        Dim strObjType As String = CLng(objType).ToString
        Dim query As String = "SELECT ObjectTypeID,ObjectID,VN,ParentNode,NodeType,TestCaseId,Author,CreateDate,UpdateDate,NodeName,NodeDescription FROM ZTC_CT where ObjectTypeID=" & strObjType & " and objectid=" & objectId.ToString
        Return Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)
    End Function

    Public Shared Sub AddCategory(ByVal objType As Int64, _
                                       ByVal objectId As Int64, _
                                       ByVal VN As Int64, _
                                       ByVal nodeName As String, _
                                       ByVal nodeDescription As String, _
                                       ByVal parentNode As Int64, _
                                       ByVal nodeType As TestCaseNodeTypes, _
                                       ByVal author As Int64)

        Dim strObjType As String = CLng(objType).ToString
        Dim testCaseId As Int32 = CoreData.GetNewID(IdTypes.TestCase)

        Dim query As New StringBuilder()
        query.Append("INSERT INTO ZTC_CT(ObjectTypeID,ObjectID,VN,ParentNode,NodeType,TestCaseId,Author,CreateDate,UpdateDate,NodeName,NodeDescription) VALUES(")
        query.Append(strObjType)
        query.Append(",")
        query.Append(objectId)
        query.Append(",")
        query.Append(VN)
        query.Append(",")
        query.Append(parentNode)
        query.Append(",")
        query.Append(CLng(nodeType))
        query.Append(",")
        query.Append(testCaseId)
        query.Append(",")
        query.Append(author)
        query.Append(",getdate(),getdate(),'")
        query.Append(nodeName.Replace("'", "''"))
        query.Append("','")
        query.Append(nodeDescription.Replace("'", "''"))
        query.Append("')")
        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
    End Sub

    Public Shared Sub ChangeCategoryName(ByVal testCaseId As Int64, ByVal name As String)
        Dim query As String = "update ztc_ct set nodename='" & name.Replace("'", "''") & "' where testcaseid=" & testCaseId.ToString
        Server.Con.ExecuteNonQuery(CommandType.Text, query)
    End Sub

    Public Shared Sub DeleteTestCase(ByVal testCaseId As Int64)
        Dim query As String = "delete from ztc_ct where testcaseid=" & testCaseId.ToString
        Server.Con.ExecuteNonQuery(CommandType.Text, query)
    End Sub


    ''' <summary>
    ''' Obtiene los casos de prueba
    ''' </summary>
    ''' <param name="testCaseID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetTestCase(ByVal testCaseID As Decimal) As DataTable
        Dim sb As StringBuilder = New StringBuilder()
        Try
            sb.Append("select Author ")
            sb.Append(",CreateDate ")
            sb.Append(",UpdateDate ")
            sb.Append(",NodeName ")
            sb.Append(",Nodetype ")
            sb.Append(",NodeDescription ")
            sb.Append(",VN ")
            sb.Append("FROM ")
            sb.Append("ZTC_CT ")
            sb.Append("WHERE ")
            sb.Append("TestCaseID = ")
            sb.Append(testCaseID.ToString())
            Return Server.Con.ExecuteDataset(CommandType.Text, sb.ToString()).Tables(0)
        Finally
            sb = Nothing
        End Try
    End Function

    Public Shared Function GetTCD(ByVal testCaseID As Decimal) As DataTable
        Dim sb As StringBuilder = New StringBuilder()
        sb.Append("select TestCaseID, StepId, StepDescription, StepObservation,StepTypeId from ztc_ts WHERE TestCaseID = ")
        sb.Append(testCaseID.ToString())
        Return Server.Con.ExecuteDataset(CommandType.Text, sb.ToString()).Tables(0)
    End Function


    Public Shared Function PasteCopiedTestCase(ByVal objType As Int64, _
                                   ByVal objectId As Int64, _
                                   ByVal VN As Int64, _
                                   ByVal nodeName As String, _
                                   ByVal nodeDescription As String, _
                                   ByVal parentNode As Int64, _
                                   ByVal nodeType As TestCaseNodeTypes, _
                                   ByVal author As Int64, _
                                   ByVal oldTestCaseId As Int64) As Int32

        Dim strObjType As String = CLng(objType).ToString
        Dim testCaseId As Int32 = CoreData.GetNewID(IdTypes.TestCase)

        Dim query As New StringBuilder()
        query.Append("INSERT INTO ZTC_CT(ObjectTypeID,ObjectID,VN,ParentNode,NodeType,TestCaseId,Author,CreateDate,UpdateDate,NodeName,NodeDescription) VALUES(")
        query.Append(strObjType)
        query.Append(",")
        query.Append(objectId)
        query.Append(",")
        query.Append(VN)
        query.Append(",")
        query.Append(parentNode)
        query.Append(",")
        query.Append(CLng(nodeType))
        query.Append(",")
        query.Append(testCaseId)
        query.Append(",")
        query.Append(author)
        query.Append(",getdate(),getdate(),'")
        query.Append(nodeName.Replace("'", "''"))
        query.Append("','")
        query.Append(nodeDescription.Replace("'", "''"))
        query.Append("')")
        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)

        'Arma dinamicamente la consulta el set de TestSteps para facilitar su inserción
        query.Remove(0, query.Length)
        query.Append("SELECT 'INSERT INTO ZTC_TS(TestCaseID,StepId,StepDescription,StepObservation,StepTypeID) VALUES (")
        query.Append(testCaseId)
        query.Append(",' + CAST(stepid AS VARCHAR) + ',''' + StepDescription + ''',''' + StepObservation + ''',' + CAST(StepTypeID AS VARCHAR) + ')' FROM ZTC_TS WHERE testCaseId=")
        query.Append(oldTestCaseId)
        Dim dtTestSteps As DataTable = Server.Con.ExecuteDataset(CommandType.Text, query.ToString).Tables(0)

        If dtTestSteps.Rows.Count > 0 Then
            Dim strQuery As String

            For i As Int32 = 0 To dtTestSteps.Rows.Count - 1
                strQuery = dtTestSteps.Rows(i)(0).ToString
                Server.Con.ExecuteNonQuery(CommandType.Text, strQuery)
            Next

            strQuery = Nothing
        End If

        query.Remove(0, query.Length)
        query = Nothing
        dtTestSteps.Dispose()
        dtTestSteps = Nothing
        Return testCaseId
    End Function

    Public Function GetTestCaseLastModification(ByVal objectId As Int64, ByVal objectTypeId As Int64) As DataTable

        Dim sb As StringBuilder = New StringBuilder()

        sb.Append("SELECT top(1) user_id, action_date, s_object_id  FROM USER_HST  WHERE object_id = ")
        sb.Append(objectId.ToString())
        sb.Append(" AND object_type_id = ")
        sb.Append(objectTypeId.ToString())
        sb.Append(" order by action_date desc")

        Return Server.Con.ExecuteDataset(CommandType.Text, sb.ToString()).Tables(0)
    End Function

    ''' <summary>
    ''' Obtiene los tipos disponibles para Zamba General
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetGeneralTypes() As DataTable
        Return Server.Con.ExecuteDataset(CommandType.Text, "select * from ztc_zg").Tables(0)
    End Function

    ''' <summary>
    ''' Obtiene los tipos generales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAsocGeneralTypes(ByVal _projectID As Int64, ByVal Type As Int64) As DataTable
        Return Server.Con.ExecuteDataset(CommandType.Text, String.Format(" SELECT {0} as 'ID Projecto',{1} as 'Tipo de objeto',ID as 'ID de Tipo',Name       FROM ztc_zg      WHERE ID IN      (SELECT OBJTYP      FROM PRJ_R_O      WHERE PrjID = {0} and OBJID = {1})", _projectID, Type)).Tables(0)
    End Function

    ''' <summary>
    ''' Obtiene la descripcion de un tipo para Zamba General
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetTypeDescription(ByVal TypeID As Int64) As String
        Return Server.Con.ExecuteScalar(CommandType.Text, "  select name from ztc_zg where id = " & TypeID)
    End Function
End Class