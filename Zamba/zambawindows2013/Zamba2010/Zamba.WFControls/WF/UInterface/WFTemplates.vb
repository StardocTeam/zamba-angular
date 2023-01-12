Imports Zamba.Core
Imports System.Xml
Imports System.Text

Public Class WFTemplates

    Private _dtWfSelected As DataTable
    Private _dtStepSelected As DataTable
    Private _dtRuleSelected As DataTable
    Private writer As XmlTextWriter

    Private _listStep As List(Of WFStep)
    Private _listRule As List(Of WFRuleParent)

    Private Sub WFTemplates_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
        _dtWfSelected = New DataTable()
        _dtStepSelected = New DataTable()
        _dtRuleSelected = New DataTable()

        _listStep = New List(Of WFStep)
        _listRule = New List(Of WFRuleParent)

        AddDTColumns()
    End Sub

    Public Sub AddRuleToform(RuleNode As RuleNode, hasChilds As Boolean)
        _dtRuleSelected.Rows.Add(New String() {RuleNode.RuleId.ToString, "58", RuleNode.RuleName, hasChilds.ToString})
        ReloadTabControl()
    End Sub

    'Carga o Refresca el control de tabs
    Sub ReloadTabControl()
        tabControlGeneral.TabPages.Clear()

        Dim newPageWork As New ZTabPage("WorkFlow")
        Dim newPageStep As New ZTabPage("Etapa")
        Dim newPageRule As New ZTabPage("Reglas")

        Dim dgv As New DataGridView()
        dgv.AllowUserToAddRows = False
        dgv.RowHeadersVisible = False
        dgv.DataSource = _dtWfSelected
        dgv.AutoSize = True

        newPageWork.Controls.Add(dgv)
        tabControlGeneral.TabPages.Add(newPageWork)


        Dim dgv2 As New DataGridView()
        dgv2.AllowUserToAddRows = False
        dgv2.RowHeadersVisible = False
        dgv2.DataSource = _dtStepSelected
        dgv2.AutoSize = True

        newPageStep.Controls.Add(dgv2)
        tabControlGeneral.TabPages.Add(newPageStep)


        Dim dgv3 As New DataGridView()
        dgv3.AllowUserToAddRows = False
        dgv3.RowHeadersVisible = False
        dgv3.DataSource = _dtRuleSelected
        dgv3.AutoSize = True

        newPageRule.Controls.Add(dgv3)
        tabControlGeneral.TabPages.Add(newPageRule)
    End Sub

    Sub AddWorkflow(ZComp As Zamba.Core.ZambaCore, hasChilds As Boolean)
        _dtWfSelected.Rows.Add(New String() {ZComp.ID.ToString, ZComp.Name, hasChilds.ToString})
        ReloadTabControl()
    End Sub

    Sub AddStep(ZComp As Zamba.Core.ZambaCore, hasChilds As Boolean)
        _dtStepSelected.Rows.Add(New String() {ZComp.ID.ToString, "28", ZComp.Name, hasChilds.ToString})
        ReloadTabControl()
    End Sub

    Sub AddRule(WfRule As IWFRuleParent, Optional haschilds As Boolean = True)
        _listRule.Add(WfRule)

        If haschilds Then
            For Each ChilDRule As IWFRuleParent In WfRule.ChildRules
                AddRule(ChilDRule)
            Next
        End If
    End Sub


    Sub AddDTColumns()
        _dtWfSelected.Columns.Add("Id")
        _dtWfSelected.Columns.Add("Name")
        _dtWfSelected.Columns.Add("HasChilds")

        _dtStepSelected.Columns.Add("Id")
        _dtStepSelected.Columns.Add("Type")
        _dtStepSelected.Columns.Add("Name")
        _dtStepSelected.Columns.Add("HasChilds")

        _dtRuleSelected.Columns.Add("Id")
        _dtRuleSelected.Columns.Add("Type")
        _dtRuleSelected.Columns.Add("Name")
        _dtRuleSelected.Columns.Add("HasChilds")
    End Sub

    Private Sub btEditar_Click(sender As System.Object, e As EventArgs) Handles btEditar.Click
        writer = New XmlTextWriter(Application.StartupPath & "\tables.xml", System.Text.Encoding.UTF8)
        writer.WriteStartDocument(True)
        writer.Formatting = Formatting.Indented
        writer.Indentation = 2
        writer.WriteStartElement("xml")
        'Agrega workflows al xml.
        GenerateXmlTableWorkflow()

        'Agrega steps al xml.
        AddStep()
        GenerateXmlTableStep()

        'Agrega rules al xml
        GenerateXmlTableRule()

        writer.WriteEndElement()
        writer.WriteEndDocument()
        writer.Close()
    End Sub

    Private Sub CreateNode(tableName As String, filter As String, enabled As String)
        writer.WriteStartElement("table")
        writer.WriteStartElement("name")
        writer.WriteString(tableName)
        writer.WriteEndElement()
        writer.WriteStartElement("filter")
        writer.WriteString(filter)
        writer.WriteEndElement()
        writer.WriteStartElement("enabled")
        writer.WriteString(enabled)
        writer.WriteEndElement()
        writer.WriteEndElement()
    End Sub

    Private Sub GenerateXmlTableWorkflow()
        Dim filter As StringBuilder
        Dim rowcount As Integer = _dtWfSelected.Rows.Count
        Dim i As Integer = 1

        If rowcount > 0 Then
            filter = New StringBuilder
            filter.Append("work_id IN(")

            'Recorro los workflow seleccionados para armar el filtro.
            For Each rowWf As DataRow In _dtWfSelected.Rows
                filter.Append(rowWf(0).ToString)

                'Valido si es el último valor a concatenar.
                If rowcount = i Then
                    filter.Append(")")
                Else
                    filter.Append(",")
                End If
                i = i + 1

                'Recorro steps del workflow y los agrego.
                Dim Steps As List(Of IWFStep) = WFStepBusiness.GetStepsByWorkflow(rowWf(0).ToString)

                For Each rowWfStep As IWFStep In Steps
                    AddStep(rowWfStep)
                Next
                CreateNode("WFWORKFLOW", filter.ToString, "1")
                CreateNode("WF_DT", filter.ToString.Replace("work_id", "WFId"), "1")
            Next
        End If

    End Sub

    Private Sub GenerateXmlTableStep()
        Dim filter As StringBuilder
        Dim rowcount As Integer = _listStep.Count

        Dim i As Integer = 1

        If rowcount > 0 Then
            filter = New StringBuilder
            filter.Append("step_id IN(")

            'Recorro los steps seleccionados para armar el filtro.
            For Each rowStep As WFStep In _listStep
                filter.Append(rowStep.ID.ToString)

                'Valido si es el último valor a concatenar.
                If rowcount = i Then
                    filter.Append(")")
                Else
                    filter.Append(",")
                End If
                i = i + 1
            Next
            CreateNode("WFSTEP", filter.ToString, "1")
            CreateNode("WFSTEPSTATES", filter.ToString, "1")
            CreateNode("WFSTEPOPT", filter.ToString.Replace("step_id", "stepid"), "1")
            CreateNode("USR_RIGHTS", (filter.ToString.Replace("step_id", "Aditional") & " AND OBJID=28"), "1")
        End If
    End Sub

    Private Sub GenerateXmlTableRule()
        Dim filter As StringBuilder
        Dim rowcount As Integer = _listRule.Count

        Dim i As Integer = 1

        If rowcount > 0 Then
            filter = New StringBuilder
            filter.Append("id IN(")

            'Recorro los steps seleccionados para armar el filtro.
            For Each rowRule As WFRuleParent In _listRule
                filter.Append(rowRule.ID.ToString)

                'Valido si es el último valor a concatenar.
                If rowcount = i Then
                    filter.Append(")")
                Else
                    filter.Append(",")
                End If
                i = i + 1
            Next
            CreateNode("WFRULES", filter.ToString, "1")
            CreateNode("ZRULEOPTBASE", filter.ToString.Replace("id", "RULEID"), "1")
            CreateNode("WFRULEPARAMITEMS", filter.ToString.Replace("id", "rule_id"), "1")
        End If

    End Sub

    Private Sub AddStep(rowWfStep As IWFStep)

        _listStep.Add(rowWfStep)

        'Obtengo y recorro las reglas de la etapa.
        Dim Rules As List(Of IWFRuleParent) = WFRulesBusiness.GetRulesByStepId(CLng(rowWfStep.ID), False)

        For Each rowWfRule As IWFRuleParent In Rules
            AddRule(rowWfRule)
        Next
        ReloadTabControl()
    End Sub

    'Método que agrega los steps seleccionados a la lista de steps.
    Private Sub AddStep()
        For Each rowWfStep As DataRow In _dtStepSelected.Rows
            _listStep.Add(WFStepBusiness.GetStepById(CLng(rowWfStep(0).ToString)))

            Dim Rules As List(Of IWFRuleParent) = WFRulesBusiness.GetRulesByStepId(CLng(rowWfStep(0).ToString), False)

            For Each rowWfRule As IWFRuleParent In Rules
                AddRule(rowWfRule)
            Next
        Next
        ReloadTabControl()
    End Sub

    Private Sub btnDescripcion_Click(sender As System.Object, e As EventArgs) Handles btnDescripcion.Click
        Try
            System.Diagnostics.Process.Start(Application.StartupPath & "\DbToXML.exe")
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
End Class