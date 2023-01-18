Imports Zamba.Servers
Imports Zamba.Data
Imports Zamba.Core
Imports Zamba.Servers.Server
Imports Zamba.Core.ZClass
Imports System.Text
Imports System.Collections.Generic
Public Class ButtonDiagramFactory
    Public Function GetButtonsMTAndH() As DataTable

        Dim query As String = "SELECT BUTTONID, PARAMS, NEEDRIGHTS, CAPTION, BUTTONORDER, WFID, ICONID,ID,PLACEID FROM ZBUTTONS WHERE PLACEID=" & ButtonPlace.WebHome & "OR PLACEID = " & ButtonPlace.BarraTareas

        Return Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)

    End Function
End Class
