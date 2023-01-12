Public Class UCDoExecuteReport
    Inherits ZRuleControl

    Private _rule As IDoReportBuilder

    Public Sub New(ByRef rule As IDoReportBuilder, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(rule, _wfPanelCircuit)
        InitializeComponent()

        _rule = rule
        LoadData()
    End Sub

    ''' <summary>
    ''' Carga los reportes en el combo
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>Diego 27-06-2008 [Created]</history>
    Private Sub LoadData()
        Try

            Dim ds As DataSet = ReportBuilder.Business.ReportBuilderComponent.GetAllQueryIdsAndNames()
            cmbReports.DataSource = ds.Tables(0)
            cmbReports.DisplayMember = "Name"
            cmbReports.ValueMember = "Id"

            If _rule.ReportId = 0 Then Exit Sub

            Dim reportname As String = String.Empty
            For Each r As DataRow In ds.Tables(0).Rows
                If r.Item("Id") = _rule.ReportId Then reportname = r.Item("Name")
            Next
            If String.IsNullOrEmpty(reportname) Then Exit Sub
            cmbReports.SelectedText = reportname
            cmbReports.Text = reportname

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub


    ''' <summary>
    ''' Evento click del boton
    ''' </summary>
    ''' <param name="sender">objeto emisor del evento</param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>Diego 27-06-2008 [Created]</history>
    Private Sub btGuardar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btGuardar.Click
        Try
            Dim id As Int32 = cmbReports.SelectedValue
            WFRulesBusiness.UpdateParamItem(MyRule.ID, 0, id)
            UserBusiness.Rights.SaveAction(MyRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & MyRule.Name & "(" & MyRule.ID & ")")
            _rule.ReportId = id
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
End Class
