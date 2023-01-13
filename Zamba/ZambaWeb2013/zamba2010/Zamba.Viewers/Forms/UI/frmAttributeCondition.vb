Imports Zamba.Core
Imports System.Collections.Generic

Public Class frmAttributeCondition
    Inherits ZForm
#Region "Properties"
    Property DocTypeID As Int64
    Property Frm() As ZwebForm
    Property Indexs As New List(Of IIndex)
    Property DocType() As DocType
#End Region

#Region "Form envents"
    Sub New(ByVal formID As Int64, ByVal docTypeID As Int64)
        InitializeComponent()

        Me.DocTypeID = docTypeID
        Frm = FormBusiness.GetForm(formID)
        DocType = DocTypesBusiness.GetDocType(docTypeID, True)
        Indexs = DocType.Indexs
        LoadActions()
    End Sub

    Private Sub frmAttributeCondition_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        lblEntity.Text = DocType.Name
        lblForm.Text = Frm.Name

        cmbIndexSource.DataSource = Indexs
        cmbIndexSource.DisplayMember = "Name"
        cmbIndexSource.ValueMember = "ID"
        cmbIndexTarget.DataSource = Indexs
        cmbIndexTarget.DisplayMember = "Name"
        cmbIndexTarget.ValueMember = "ID"

        If cmbIndexSource.SelectedIndex > -1 Then
            LoadComparators(cmbIndexSource.SelectedItem.Type)
        Else
            LoadComparators(IndexDataType.None)
        End If

        LoadConditionTable()
    End Sub
#End Region

#Region "Controls events"
    Private Sub cmbIndexSource_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles cmbIndexSource.SelectedIndexChanged
        If cmbIndexSource.SelectedIndex > -1 Then
            LoadComparators(cmbIndexSource.SelectedItem.Type)
        End If
    End Sub

    Private Sub gvDisplayConditions_SelectionChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles gvDisplayConditions.SelectionChanged
        If gvDisplayConditions.SelectedRows.Count > 0 Then
            btnEdit.Visible = True
            btnDelete.Visible = True
            LoadOptions(gvDisplayConditions.SelectedRows(0).Cells("ID").Value)
        End If
    End Sub

    Private Sub btnAddCondition_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAddCondition.Click
        If cmbIndexSource.SelectedIndex > -1 AndAlso cmbComparator.SelectedIndex > -1 AndAlso _
            cmbIndexTarget.SelectedIndex > -1 AndAlso cmbAction.SelectedIndex > -1 Then

            FormBusiness.AddCondition(Frm.ID, cmbIndexSource.SelectedValue, cmbComparator.SelectedItem.ID, _
                                      txtValue.Text, cmbIndexTarget.SelectedValue, cmbAction.SelectedItem.ID)

            LoadConditionTable()
        Else
            MessageBox.Show("Solo se permite dejar el valor a comparar vacio", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnEdit.Click
        If cmbIndexSource.SelectedIndex > -1 AndAlso cmbComparator.SelectedIndex > -1 AndAlso _
           cmbIndexTarget.SelectedIndex > -1 AndAlso cmbAction.SelectedIndex > -1 Then
            FormBusiness.EditCondition(gvDisplayConditions.SelectedRows(0).Cells("ID").Value, cmbIndexSource.SelectedValue, cmbComparator.SelectedItem.ID, _
                                      txtValue.Text, cmbIndexTarget.SelectedValue, cmbAction.SelectedItem.ID)

            LoadConditionTable()
        Else
            MessageBox.Show("Solo se permite dejar el valor a comparar vacio", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnDelete.Click
        If gvDisplayConditions.SelectedRows.Count > 0 Then
            If MessageBox.Show("¿Desea borrar la condicion seleccionada?", "Zamba", MessageBoxButtons.OKCancel) = DialogResult.OK Then
                FormBusiness.DeleteCondition(gvDisplayConditions.SelectedRows(0).Cells("ID").Value)

                LoadConditionTable()
            End If
        End If
    End Sub
#End Region

#Region "Methods"
    Private Sub LoadComparators(ByVal indexType As IndexDataType)
        cmbComparator.Items.Clear()
        Select Case indexType
            Case IndexDataType.Alfanumerico To IndexDataType.Alfanumerico_Largo
                cmbComparator.Items.Add(New ZCoreView(Comparators.Equal, "Igual"))
                cmbComparator.Items.Add(New ZCoreView(Comparators.Different, "Distinto"))
                cmbComparator.Items.Add(New ZCoreView(Comparators.Contents, "Contiene"))
                cmbComparator.Items.Add(New ZCoreView(Comparators.Starts, "Empieza con"))
                cmbComparator.Items.Add(New ZCoreView(Comparators.Ends, "Termina con"))
                cmbComparator.Items.Add(New ZCoreView(Comparators.Into, "Dentro de"))
                cmbComparator.Items.Add(New ZCoreView(Comparators.NotInto, "Fuera de"))
            Case IndexDataType.None
                cmbComparator.Items.Add(New ZCoreView(Comparators.Equal, "Igual"))
                cmbComparator.Items.Add(New ZCoreView(Comparators.Different, "Distinto"))
                cmbComparator.Items.Add(New ZCoreView(Comparators.Contents, "Contiene"))
                cmbComparator.Items.Add(New ZCoreView(Comparators.Starts, "Empieza con"))
                cmbComparator.Items.Add(New ZCoreView(Comparators.Ends, "Termina con"))
                cmbComparator.Items.Add(New ZCoreView(Comparators.Lower, "Menor que"))
                cmbComparator.Items.Add(New ZCoreView(Comparators.Upper, "Mayor que"))
                cmbComparator.Items.Add(New ZCoreView(Comparators.EqualLower, "Menor o igual"))
                cmbComparator.Items.Add(New ZCoreView(Comparators.EqualUpper, "Mayor o igual"))
            Case Else
                cmbComparator.Items.Add(New ZCoreView(Comparators.Equal, "Igual"))
                cmbComparator.Items.Add(New ZCoreView(Comparators.Different, "Distinto"))
                cmbComparator.Items.Add(New ZCoreView(Comparators.Lower, "Menor que"))
                cmbComparator.Items.Add(New ZCoreView(Comparators.Upper, "Mayor que"))
                cmbComparator.Items.Add(New ZCoreView(Comparators.EqualLower, "Menor o igual"))
                cmbComparator.Items.Add(New ZCoreView(Comparators.EqualUpper, "Mayor o igual"))
                cmbComparator.Items.Add(New ZCoreView(Comparators.Into, "Dentro de"))
                cmbComparator.Items.Add(New ZCoreView(Comparators.NotInto, "Fuera de"))
        End Select

        cmbComparator.DisplayMember = "Name"
        cmbComparator.ValueMember = "ID"

        cmbComparator.SelectedIndex = 0
    End Sub

    Private Sub LoadActions()
        cmbAction.Items.Clear()
        cmbAction.Items.Add(New ZCoreView(FormActions.Enable, "Habilitar"))
        cmbAction.Items.Add(New ZCoreView(FormActions.Disable, "Deshabilitar"))
        cmbAction.Items.Add(New ZCoreView(FormActions.MakeRequired, "Hacer requerido"))
        cmbAction.Items.Add(New ZCoreView(FormActions.MakeNonRequired, "Hacer no requerido"))
        cmbAction.Items.Add(New ZCoreView(FormActions.Hidden, "Ocultar"))
        cmbAction.Items.Add(New ZCoreView(FormActions.Visible, "Mostrar"))

        cmbAction.DisplayMember = "Name"
        cmbAction.ValueMember = "ID"
    End Sub

    Private Sub LoadOptions(ByVal optionID As Int64)
        Dim condition As IZFormCondition = FormBusiness.GetCondition(optionID)

        cmbIndexSource.SelectedValue = condition.IndexToValidate
        SelectItem(cmbComparator, condition.Comparator)
        txtValue.Text = condition.ComparateValue
        cmbIndexTarget.SelectedValue = condition.TargetIndex
        SelectItem(cmbAction, condition.TargetAction)
    End Sub

    Private Sub LoadConditionTable()
        gvDisplayConditions.DataSource = FormBusiness.GetFormConditionsTable(Frm.ID)

        gvDisplayConditions.Columns("ID").Visible = False
        gvDisplayConditions.Columns("IndexToValidate").Visible = False
        gvDisplayConditions.Columns("Comparator").Visible = False
        gvDisplayConditions.Columns("TargetIndex").Visible = False
        gvDisplayConditions.Columns("TargetAction").Visible = False
    End Sub

    Private Sub SelectItem(ByRef cmbToSelect As ComboBox, ByVal value As Long)
        Dim item As Object = Nothing
        Dim max As Long = cmbToSelect.Items.Count

        For i As Integer = 0 To max - 1
            item = cmbToSelect.Items(i)
            If item.ID = value Then
                Exit For
            End If
        Next

        If Not item Is Nothing Then
            cmbToSelect.SelectedItem = item
        End If
    End Sub
#End Region
End Class