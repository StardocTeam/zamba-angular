Imports System.Text
Public Class PlayDoInsertTable
    Private dsValues As DataSet
    ''' <summary>
    ''' Inserta un DataSet en una Tabla especificada
    ''' </summary>
    ''' <param name="results"></param>
    ''' <param name="myRule"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Pablo] 19-10-2010 Created   
    ''' </history>
    ''' 

    Private myRule As IDoInsertTable

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim NewResults As New System.Collections.Generic.List(Of Core.ITaskResult)
        Dim data As DataSet
        Dim QueryBuilder As New StringBuilder()
        Dim value As String = myRule.DataSet

        If value.Contains("zvar") = True Then
            value = value.Replace("zvar(", "")
            value = value.Replace(")", "")
        End If

        data = VariablesInterReglas.Item(value)
        Try
            For Each dr As DataRow In data.Tables(0).Rows
                QueryBuilder.Append("Insert into ")
                QueryBuilder.Append(myRule.Table.ToString)
                QueryBuilder.Append(" values ('")
                For Each dc As Object In dr.ItemArray
                    QueryBuilder.Append(dc.ToString())
                    QueryBuilder.Append("','")
                Next
                QueryBuilder.Remove(QueryBuilder.Length - 2, 2)
                QueryBuilder.Append(")")
                ServersBusiness.BuildExecuteNonQuery(CommandType.Text, QueryBuilder.ToString())
                QueryBuilder.Remove(0, QueryBuilder.Length)
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Return NewResults
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    Public Sub New(ByVal rule As IDoInsertTable)
        myRule = rule
    End Sub
End Class
