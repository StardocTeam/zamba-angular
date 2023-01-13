Imports Zamba.Core

Public Class ucbrowserpreview

    Private path As String
    Private lastheight As Decimal



    Public Sub ShowDocument(DocumentPath As String)
        Try
            path = DocumentPath
            WebBrowser1.Navigate(DocumentPath)
            Height = UserPreferences.getValue("PreviewHeight", UPSections.Viewer, 2500)
            Try
                WebBrowser1.Height = DirectCast(DirectCast(WebBrowser1.Document.DomDocument, mshtml.HTMLDocumentClass).body, mshtml.HTMLBodyClass).IHTMLElement2_scrollHeight
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            lastheight = Height
            WebBrowser1.AutoSize = True
            Height = WebBrowser1.Height
            btnmaximize.Image = Global.Zamba.Viewers.My.Resources.Resources._1446231912_Exit_Full_Screen
            btnmaximize.ToolTipText = "Colapsar"

            RemoveHandler MyBase.Resize, AddressOf ucbrowserpreview_Resize
            AddHandler MyBase.Resize, AddressOf ucbrowserpreview_Resize
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    Private BtnFunctionMaximize As Boolean
    Private Sub btnmaximize_Click(sender As Object, e As EventArgs) Handles btnmaximize.Click
        RemoveHandler MyBase.Resize, AddressOf ucbrowserpreview_Resize

        If BtnFunctionMaximize Then
            Height = lastheight
            btnmaximize.Image = Global.Zamba.Viewers.My.Resources.Resources._1446231912_Exit_Full_Screen
            btnmaximize.ToolTipText = "Colapsar"
            BtnFunctionMaximize = Not BtnFunctionMaximize
        Else
            BtnFunctionMaximize = Not BtnFunctionMaximize
            lastheight = Height
            Height = 25
            btnmaximize.Image = Global.Zamba.Viewers.My.Resources.Resources._1446231902_Full_Screen
            btnmaximize.ToolTipText = "Expandir"
        End If
        AddHandler MyBase.Resize, AddressOf ucbrowserpreview_Resize

    End Sub


    Private Sub btmrefresh_Click(sender As Object, e As EventArgs) Handles btmrefresh.Click
        Try
            WebBrowser1.Navigate(path)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub


    Private Sub ucbrowserpreview_Resize(sender As Object, e As EventArgs)
        Try
            If Height <> 25 Then
                UserPreferences.setValue("PreviewHeight", Height, UPSections.Viewer)
                lastheight = Height
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    Private Sub WebBrowser1_AutoSizeChanged(sender As Object, e As EventArgs) Handles WebBrowser1.AutoSizeChanged
    End Sub

    Private Sub WebBrowser1_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted
        Height = WebBrowser1.Height
        Try
            WebBrowser1.Height = DirectCast(DirectCast(WebBrowser1.Document.DomDocument, mshtml.HTMLDocumentClass).body, mshtml.HTMLBodyClass).IHTMLElement2_scrollHeight
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    Private Sub WebBrowser1_Navigated(sender As Object, e As WebBrowserNavigatedEventArgs) Handles WebBrowser1.Navigated
    End Sub
End Class
