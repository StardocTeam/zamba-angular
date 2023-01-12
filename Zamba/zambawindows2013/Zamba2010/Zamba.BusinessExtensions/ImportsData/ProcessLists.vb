Imports Zamba.Data
Public Class ProcessLists


    Public Shared Sub AddIPLIST(ByVal Name As String, ByVal Description As String, Optional ByVal Enabled As Boolean = True)
        Name = Name.Replace("'", "''").ToUpper
        Description = Description.Replace("'", "''")
        Dim EnabledNumber As Int32 = Enabled
        ImportsFactory.InsertIPList(Name, Description, EnabledNumber)
    End Sub
    Public Shared Sub DelIPLIST(ByVal Id As Int32)
        ImportsFactory.DelIPLIST(Id)
    End Sub
    Public Shared Function GetProcessList() As DataSet
        Return ImportsFactory.GetProcessList()
    End Function
    Public Shared Function getProcess(ByVal IPLIST As Int32) As DataSet
        Return ImportsFactory.getProcess(IPLIST)
    End Function

End Class
