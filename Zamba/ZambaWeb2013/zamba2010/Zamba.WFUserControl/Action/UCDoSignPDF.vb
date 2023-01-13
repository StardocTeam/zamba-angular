Imports System.Security.Cryptography.X509Certificates
Imports iTextSharp.text.pdf

Public Class UCDoSignPDF
    Inherits ZRuleControl

    Dim CurrentRule As IDoSignPDF
    Dim certSN As String

    Public Sub New(ByVal CurrentRule As IDoSignPDF, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(CurrentRule, _wfPanelCircuit)
        InitializeComponent()
        Me.CurrentRule = CurrentRule

        fullPath.Text = CurrentRule.FullPath
        fileName.Text = CurrentRule.FileName
        author.Text = CurrentRule.Author
        title.Text = CurrentRule.Title
        subject.Text = CurrentRule.Subject
        keywords.Text = CurrentRule.Keywords
        creator.Text = CurrentRule.Creator
        producer.Text = CurrentRule.Producer
        certificate.Text = CurrentRule.Certificate
        password.Text = CurrentRule.Password
        reason.Text = CurrentRule.Reason
        contact.Text = CurrentRule.Contact
        location.Text = CurrentRule.Location
        writePDF.Checked = CurrentRule.WritePDF
    End Sub

    Private Sub button4_Click(sender As Object, e As EventArgs) Handles button4.Click
        Dim openFile As System.Windows.Forms.OpenFileDialog
        openFile = New System.Windows.Forms.OpenFileDialog()
        openFile.Filter = "PDF files *.pdf|*.pdf"
        openFile.Title = "Seleccione archivo PDF"
        If openFile.ShowDialog() <> DialogResult.OK Then
            Return
        End If

        fullPath.Text = openFile.FileName

        Dim reader As New PdfReader(fullPath.Text)
        For Each key As KeyValuePair(Of String, String) In reader.Info
            Select Case key.Key
                Case "Author"
                    author.Text = key.Value
                Case "Creator"
                    creator.Text = key.Value
                Case "Title"
                    title.Text = key.Value
                Case "Subject"
                    subject.Text = key.Value
                Case "Keywords"
                    Keywords.Text = key.Value
                Case "Producer"
                    producer.Text = key.Value
            End Select
        Next
    End Sub

    Private Sub btn_Aceptar_Click(sender As Object, e As EventArgs) Handles btn_Aceptar.Click

        Dim txtVal = New List(Of Zamba.AppBlock.TextoInteligenteTextBox)()
        txtVal.Add(fullPath)
        txtVal.Add(fileName)
        ' txtVal.Add(certificate);

        If Not ValidateControls(txtVal) OrElse String.IsNullOrEmpty(certSN) Then
            MessageBox.Show("Por favor complete los campos requeridos", "Zamba Software")
            Exit Sub
        End If

        CurrentRule.FullPath = fullPath.Text
        CurrentRule.FileName = fileName.Text
        CurrentRule.Author = author.Text
        CurrentRule.Title = title.Text
        CurrentRule.Subject = subject.Text
        CurrentRule.Keywords = Keywords.Text
        CurrentRule.Creator = creator.Text
        CurrentRule.Producer = producer.Text
        CurrentRule.Certificate = IIf(String.IsNullOrEmpty(certSN), certificate.Text, certSN)
        CurrentRule.Password = password.Text
        CurrentRule.Reason = reason.Text
        CurrentRule.Contact = contact.Text
        CurrentRule.Location = Location.Text
        CurrentRule.WritePDF = writePDF.Checked

        WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.FullPath)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 1, CurrentRule.FileName)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 2, CurrentRule.Author)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 3, CurrentRule.Title)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 4, CurrentRule.Subject)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 5, CurrentRule.Keywords)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 6, CurrentRule.Creator)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 7, CurrentRule.Producer)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 8, CurrentRule.Certificate)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 9, CurrentRule.Password)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 10, CurrentRule.Reason)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 11, CurrentRule.Contact)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 12, CurrentRule.Location)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 13, CurrentRule.WritePDF)

        UserBusiness.Rights.SaveAction(CurrentRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & CurrentRule.Name & "(" & CurrentRule.ID & ")")

    End Sub
    Private Function ValidateControls(txt As List(Of Zamba.AppBlock.TextoInteligenteTextBox)) As Boolean
        For Each t As Zamba.AppBlock.TextoInteligenteTextBox In txt
            If String.IsNullOrEmpty(t.Text) Then 'OrElse Not System.IO.File.Exists(t.Text)
                MessageBox.Show("Por favor complete el dato resaltado")
                t.Focus()
                Return False
            End If
        Next
        Return True
    End Function

    Private Sub button5_Click(sender As Object, e As EventArgs) Handles button5.Click
        Dim saveFile As New SaveFileDialog()

        saveFile.Filter = "PDF files *.pdf|*.pdf"
        saveFile.Title = "Guardar archivo"
        If (saveFile.ShowDialog() <> DialogResult.OK) Then Return
        fileName.Text = saveFile.FileName
    End Sub

    Private Sub button2_Click(sender As Object, e As EventArgs) Handles button2.Click
        Dim store As New X509Store(StoreLocation.CurrentUser)
        store.Open(OpenFlags.[ReadOnly])
        Dim sel As X509Certificate2Collection = X509Certificate2UI.SelectFromCollection(store.Certificates, Nothing, Nothing, X509SelectionFlag.SingleSelection)

        If sel.Count <> 0 Then
            Dim cert As X509Certificate2 = sel(0)
            certSN = sel(0).SerialNumber
            certificate.Text = certSN
            lblCert.Text = (sel(0).Issuer).ToUpper().Replace("CN=", "")
        End If
    End Sub
End Class
