Imports System.Collections
Imports Zamba.Core

Public Class HelpMenu
    Public Property _sender As Object
    Private Property _handle As IntPtr

    Public Sub LoadContextMenu(ByRef sender As Object, ByRef currentContextMenu As ContextMenu, ByVal Handle As IntPtr)
        Try
            _handle = Handle
            _sender = sender
            If currentContextMenu Is Nothing Then
                Dim ctx As New ContextMenu
                currentContextMenu = ctx
            End If

            Dim HelpMenu As New MenuItem("Ayuda")
            AddHandler HelpMenu.Click, AddressOf HelpClick
            currentContextMenu.MenuItems.Add(HelpMenu)

            Dim HelpMenuIdentificator As New MenuItem("Help Id")
            AddHandler HelpMenuIdentificator.Click, AddressOf HelpMenuIdentificatorClick
            currentContextMenu.MenuItems.Add(HelpMenuIdentificator)

        Catch ex As Exception
        End Try
    End Sub

    Private Sub HelpClick(ByVal sender As Object, ByVal e As EventArgs)
        ShowHelp()
    End Sub

    Private Sub ShowHelp()
        Dim params As New Hashtable
        params.Add("Handle", _handle)
        Dim path As String
        path = GetSenderPath()
        params.Add("Path", path)
        ZClass.HandleModule(ResultActions.ShowHelp, Nothing, params)
    End Sub

    Private Sub HelpMenuIdentificatorClick(ByVal sender As Object, ByVal e As EventArgs)
        ShowHelpId()
    End Sub

    Private Sub ShowHelpId()
        Try
            Dim path As String
            path = GetSenderPath()
            MessageBox.Show("ID: " & _handle.ToString & " Path: " & path)
        Catch ex As Exception

        End Try
    End Sub

    Private Function GetSenderPath() As String
        Dim path As String
        If _sender.name.ToString().Length > 0 AndAlso _sender.text.ToString().Length > 0 Then
            path = _sender.name & "-" & _sender.text
        ElseIf _sender.name.ToString().Length > 0 Then
            path = _sender.name
        ElseIf _sender.text.ToString().Length > 0 Then
            path = _sender.text
        End If
        If _sender.parent IsNot Nothing Then
            If _sender.parent.name.ToString().Length > 0 AndAlso _sender.parent.text.ToString().Length > 0 Then
                path += "-" & _sender.parent.name & "-" & _sender.parent.text
            ElseIf _sender.parent.name.ToString().Length > 0 Then
                path += "-" & _sender.parent.name
            ElseIf _sender.parent.text.ToString().Length > 0 Then
                path += "-" & _sender.parent.text
            End If
        End If
        Return path
    End Function
End Class
