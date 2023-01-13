Imports System.IO

Public Class UCHelp
    'Public Event CloseControl()

#Region "Constructores"
    Private Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(ByVal helptype As HelpTypes, ByVal helpfilename As String)
        Me.New()
        Dim filepath As String = String.Empty

        Select Case helptype
            Case HelpTypes.DynamicForms
                filepath = Application.StartupPath & "\ZambaHelp\DynamicForms\" & helpfilename & ".html"

                If File.Exists(filepath) Then
                    WebBrowser.Navigate(filepath)
                Else
                    ZClass.raiseerror(New IO.FileNotFoundException("No se encontro el archivo de ayuda " & filepath))
                End If

            Case Else
                ZClass.raiseerror(New NotImplementedException("No esta implementado el tipo"))
        End Select


    End Sub

    Public Sub New(ByVal Rule As IRule)
        Me.New()
        LoadRuleHelp(Rule)
    End Sub
#End Region

#Region "Métodos"
    Private Sub LoadRuleHelp(ByVal Rule As IRule)
        Try
            Dim filepath As String = Application.StartupPath & "\WFRulesHelp\" & Rule.GetType.Name & ".html"
            Dim File As New FileInfo(filepath)
            If File.Exists Then
                WebBrowser.Navigate(File.FullName)
            Else
                ZClass.raiseerror(New IO.FileNotFoundException("No se encontro el archivo de ayuda " & filepath))
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    'Private Sub lblClose_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
    '    RaiseEvent CloseControl()
    'End Sub
#End Region

End Class
