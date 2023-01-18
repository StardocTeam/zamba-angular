Imports ZAMBA.Servers

Public Class ClsWorkflow
    Public Shared Function DocTypeInWf() As DataSet
        Dim datos As New DsDocsInWF
        Try
            'Dim sql As String = "Select * from DocTypeInWF order by Workflow,[Tipo de Documento]"
            Dim sql As String = "Select * from Zvw_DocTypeInWF_100 order by Workflow,[Tipo de Documento]"
            Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)
            ds.Tables(0).TableName = datos.Tables(0).TableName
            datos.Merge(ds)
        Catch
        End Try
        Return datos
    End Function
    Public Shared Function WFEstadoGeneral() As DataSet
        Dim datos As New DsWFStepStates
        Try
            'Dim sql As String = "select * from ZWfStepsStates order by Workflow,Etapa,Estado"
            Dim sql As String = "select * from Zvw_ZWfStepsStates_100 order by Workflow,Etapa,Estado"
            Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)
            ds.Tables(0).TableName = datos.Tables(0).TableName
            datos.Merge(ds)
        Catch
        End Try
        Return datos
    End Function
    Public Shared Function DocCountByStep() As DataSet
        Dim datos As New DsDocCountStep
        Try
            'Dim sql As String = "Select Name as Etapa, DCount as Cantidad from ZViewWFDocumentCOUNT inner join ZViewWFSteps on ZViewWFDocumentCOUNT.step_id=ZViewWFSteps.step_Id"
            Dim sql As String = "Select Name as Etapa, DCount as Cantidad from Zvw_WFDocumentCOUNT_100 inner join Zvw_WFSTEPS_100 on Zvw_WFDocumentCOUNT_100.step_id=Zvw_WFSTEPS_100.step_Id"
            Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)
            ds.Tables(0).TableName = datos.Tables(0).TableName
            ds.Tables(0).Columns(0).ColumnName = datos.Tables(0).Columns(0).ColumnName
            ds.Tables(0).Columns(1).ColumnName = datos.Tables(0).Columns(1).ColumnName
            datos.Merge(ds)
        Catch
            'MessageBox.Show(ex.Message)
        End Try
        Return datos
    End Function
    Public Shared Function RulesByStep() As DataSet
        Dim datos As New DsRulesByStep
        Try
            'Dim sql As String = "Select * from ZWFRulesByStep"
            Dim sql As String = "Select * from Zvw_ZWFRulesByStep_100"
            Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)
            ds.Tables(0).TableName = datos.Tables(0).TableName
            datos.Merge(ds)
        Catch
        End Try
        Return datos
    End Function
    Public Shared Function DelayDocuments() As DataSet

    End Function
End Class
