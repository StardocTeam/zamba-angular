Imports System.Windows.Forms
Imports Zamba.PDFSigner

Public Class PlayDoSignPDF
    Private _myRule As IDoSignPDF

    Private fullPath As String
    Private fileName As String
    'PDF Metadata
    Private author As String
    Private title As String
    Private subject As String
    Private keywords As String
    Private creator As String
    Private producer As String
    'Certificate
    Private certificate As String
    Private password As String
    'Signature
    Private reason As String
    Private contact As String
    Private location As String
    Private writePDF As Boolean

    Sub New(ByVal rule As IDoSignPDF)
        _myRule = rule
    End Sub

    Private Function ShowFormPassword() As Form
        Dim f As New Form
        Dim lblCtrl As New Label
        With lblCtrl
            .Text = "Por favor ingrese la contraseña de la credencial"
            .AutoSize = True
            .Left = 30
            .Top = 20
        End With
        Dim txtCtrl As New TextBox
        With txtCtrl
            .PasswordChar = "*"
            .Name = "txtPassword"
            .Width = 150
            .Left = 30
            .Top = 50
        End With
        Dim btnCtrl As New Button
        With btnCtrl
            .Text = "&Aceptar"
            .Left = 185
            .DialogResult = DialogResult.OK
            .Top = 49
        End With
        Dim btnCancelCtrl As New Button
        With btnCancelCtrl
            .Text = "&Cancelar"
            .Left = 185
            .DialogResult = DialogResult.Cancel
            .Top = 75
        End With
        f.StartPosition = FormStartPosition.CenterScreen
        f.Height = 150
        f.ControlBox = False
        f.Controls.AddRange({txtCtrl, lblCtrl, btnCtrl, btnCancelCtrl})
        Return f
    End Function
    Private Function GetPasswordFromForm(f As Form) As String
        For Each c As Control In f.Controls
            If TypeOf c Is TextBox AndAlso c.Name = "txtPassword" Then
                Return c.Text
            End If
        Next
        Return String.Empty
    End Function

    Function Play(ByVal results As List(Of ITaskResult)) As List(Of ITaskResult)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Play de la DoSignPDF")
        Dim resCount As Integer = results.Count

        If (resCount > 0) Then
            Dim f As Form = ShowFormPassword()
            Dim dr As DialogResult
            dr = f.ShowDialog()
            If f.DialogResult() = DialogResult.OK Then
                password = GetPasswordFromForm(f)
                'TODO: ML                 
                ' ValidatePassword(UserId, Encrypt(password))
            Else
                Return results
            End If

            For Each r As ITaskResult In results
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Validando texto inteligente -> DoSignPDF")
                fullPath = TextoInteligente.ReconocerCodigo(_myRule.FullPath, r)
                fullPath = WFRuleParent.ReconocerVariablesValuesSoloTexto(fullPath)
                fileName = TextoInteligente.ReconocerCodigo(_myRule.FileName, r)
                fileName = WFRuleParent.ReconocerVariablesValuesSoloTexto(fileName)

                author = TextoInteligente.ReconocerCodigo(_myRule.Author, r)
                author = WFRuleParent.ReconocerVariablesValuesSoloTexto(author)
                title = TextoInteligente.ReconocerCodigo(_myRule.Title, r)
                title = WFRuleParent.ReconocerVariablesValuesSoloTexto(title)
                subject = TextoInteligente.ReconocerCodigo(_myRule.Subject, r)
                subject = WFRuleParent.ReconocerVariablesValuesSoloTexto(subject)
                keywords = TextoInteligente.ReconocerCodigo(_myRule.Keywords, r)
                keywords = WFRuleParent.ReconocerVariablesValuesSoloTexto(keywords)
                creator = TextoInteligente.ReconocerCodigo(_myRule.Creator, r)
                creator = WFRuleParent.ReconocerVariablesValuesSoloTexto(creator)
                producer = TextoInteligente.ReconocerCodigo(_myRule.Producer, r)
                producer = WFRuleParent.ReconocerVariablesValuesSoloTexto(producer)

                certificate = TextoInteligente.ReconocerCodigo(_myRule.Certificate, r)
                certificate = WFRuleParent.ReconocerVariablesValuesSoloTexto(certificate)

                reason = TextoInteligente.ReconocerCodigo(_myRule.Reason, r)
                reason = WFRuleParent.ReconocerVariablesValuesSoloTexto(reason)
                contact = TextoInteligente.ReconocerCodigo(_myRule.Contact, r)
                contact = WFRuleParent.ReconocerVariablesValuesSoloTexto(contact)
                location = TextoInteligente.ReconocerCodigo(_myRule.Location, r)
                location = WFRuleParent.ReconocerVariablesValuesSoloTexto(location)
                writePDF = _myRule.WritePDF


                Dim sPdf As New PDFSign()
                sPdf.Sign(fullPath, fileName, author, title, subject, keywords, creator, producer, certificate, password, reason, contact, location, writePDF)
            Next
        Else
            ZTrace.WriteLineIf(ZTrace.IsInfo, "No hay tareas para convertir a PDF.")
        End If

        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class