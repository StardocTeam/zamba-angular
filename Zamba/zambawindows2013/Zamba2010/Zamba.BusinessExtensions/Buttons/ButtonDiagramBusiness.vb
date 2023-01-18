Imports Zamba.Data

Public Class ButtonDiagramBusiness
    Public Shared Function getButtonsInMTandH() As List(Of IButtonDiagram)
        Dim Buttons As New List(Of IButtonDiagram)
        Dim BF As New ButtonDiagramFactory


        For Each r As DataRow In BF.GetButtonsMTAndH.Rows
            Dim button As New ButtonDiagram
            button.ID = r("ID")
            button.Name = r("Caption")
            button.WFID = r("WFID")
            button.PlaceId = r("PlaceID")
            button.ButtonID = r("ButtonID")
            Buttons.Add(button)
        Next
        Return Buttons
    End Function
End Class
