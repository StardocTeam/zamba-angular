Imports Zamba.Servers
Imports Zamba.Core
Public Class ButtonDiagramFactory
    Public Function GetButtonsMTAndH() As DataTable

        Dim query As String = "SELECT BUTTONID, PARAMS, NEEDRIGHTS, CAPTION, BUTTONORDER, WFID, ICONID,ID,PLACEID FROM ZBUTTONS WHERE PLACEID=" & ButtonPlace.WebHome & "OR PLACEID = " & ButtonPlace.BarraTareas

        Return Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)

    End Function
End Class
