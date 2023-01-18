Public Class UCDODocToPDF

    Inherits ZRuleControl

    Dim CurrentRule As IDoDocToPDF

    Public Sub New(ByRef CurrentRule As IDoDocToPDF, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(CurrentRule, _wfPanelCircuit)
        InitializeComponent()
        Me.CurrentRule = CurrentRule

        If Me.CurrentRule.ExportTask Then
            optDocEnTarea.Checked = True
        Else
            opOtroDoc.Checked = True
            txtPath.Text = Me.CurrentRule.FullPath
        End If

        opNuevaConversion.Checked = Me.CurrentRule.UseNewConversion

        txtFileName.Text = Me.CurrentRule.FileName
    End Sub

    Public Shadows ReadOnly Property MyRule() As IDoDocToPDF
        Get
            Return DirectCast(Rule, IDoDocToPDF)
        End Get
    End Property

    Private Sub optDocEnTarea_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles optDocEnTarea.CheckedChanged
        txtPath.Text = String.Empty
        txtPath.Enabled = False
    End Sub

    Private Sub opOtroDoc_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles opOtroDoc.CheckedChanged
        txtPath.Enabled = True
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click

        If opOtroDoc.Checked AndAlso String.IsNullOrEmpty(txtPath.Text) Then
            MessageBox.Show("Se debe configurar el nombre y la ruta completa del archivo a convertir", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If String.IsNullOrEmpty(txtFileName.Text) Then
            MessageBox.Show("Se debe configurar el nombre del archivo a convertir", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If optDocEnTarea.Checked Then
            CurrentRule.ExportTask = True
            CurrentRule.FullPath = String.Empty
        Else
            CurrentRule.ExportTask = False
            CurrentRule.FullPath = txtPath.Text
        End If

        CurrentRule.UseNewConversion = opNuevaConversion.Checked

        CurrentRule.FileName = txtFileName.Text

        WFRulesBusiness.UpdateParamItem(Rule.ID, 0, CurrentRule.ExportTask)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 1, CurrentRule.FullPath)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 2, CurrentRule.FileName)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 3, CurrentRule.UseNewConversion)

        If opConversionAnterior.Checked = True AndAlso String.IsNullOrEmpty(ZOptBusiness.GetValue("PDFPrinter")) Then
            MessageBox.Show("Para poder ejecutar esta regla con impresora virtual, se debe configurar la impresora de PDFs en el administrador", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
End Class