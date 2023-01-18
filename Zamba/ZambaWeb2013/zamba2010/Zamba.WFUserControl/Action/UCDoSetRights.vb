Imports System.Text

Public Class UCDoSetRights
    Inherits ZRuleControl

    Private Const _COMMA As String = ","
    Private Const _SEMICOLON As String = ";"
    Private Const _SAVED_DATA As String = "Datos guardados con éxito "
    Private currentRule As IDoSetRights
    Private Event updatePanelCircuit(ByVal RuleId As Int64)

    Public Sub New(ByRef DoSetRights As IDoSetRights, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(DoSetRights, _wfPanelCircuit)
        InitializeComponent()
        Try
            CommonConstructor(DoSetRights)
            RemoveHandler updatePanelCircuit, AddressOf _wfPanelCircuit.UpdateRuleType
            AddHandler updatePanelCircuit, AddressOf _wfPanelCircuit.UpdateRuleType
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Constructor en común
    ''' </summary>
    ''' <param name="DoSetRights"></param>
    ''' <remarks></remarks>
    Private Sub CommonConstructor(ByRef DoSetRights As IDoSetRights)
        currentRule = DoSetRights

        'Carga de permisos implementados en el combo
        FillCombo()

        'Carga la configuracion de grilla
        LoadRights()
    End Sub

    ''' <summary>
    ''' Carga el combo con los permisos implementados
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillCombo()
        'Se crea la estructura del datatable
        Dim dtRights As New DataTable
        Dim colName As New DataColumn("name", GetType(String))
        Dim colValue As New DataColumn("value", GetType(RightsType))
        dtRights.Columns.AddRange(New DataColumn() {colName, colValue})

        'Se agregan los datos de los permisos
        dtRights.Rows.Add(GetRightName(RightsType.Edit), RightsType.Edit)
        dtRights.Rows.Add(GetRightName(RightsType.ReIndex), RightsType.ReIndex)
        dtRights.Rows.Add(GetRightName(RightsType.HideReplaceDocument), RightsType.HideReplaceDocument)

        cmbRights.DataSource = dtRights
        cmbRights.ValueMember = "value"
        cmbRights.DisplayMember = "name"
    End Sub

    ''' <summary>
    ''' Carga la grilla de permisos configurados
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadRights()
        dgvRights.SuspendLayout()

        'Se crea la estructura de la grilla
        dgvRights.Columns.Add("name", "Permiso")
        dgvRights.Columns.Add("rightType")
        dgvRights.Columns.Add("displayMember", "Acción")
        dgvRights.Columns.Add("value")
        dgvRights.Columns("rightType").IsVisible = False
        dgvRights.Columns("value").IsVisible = False

        'Se procesan los permisos configurados
        For Each rightToSet As KeyValuePair(Of RightsType, Boolean) In currentRule.Rights
            dgvRights.Rows.Add(New Object() {GetRightName(rightToSet.Key), _
                                                CInt(rightToSet.Key), _
                                                GetActionValue(rightToSet.Value), _
                                                rightToSet.Value})
        Next

        dgvRights.ResumeLayout()
    End Sub

    ''' <summary>
    ''' Devuelve el nombre del permiso
    ''' </summary>
    ''' <param name="rightType">Tipo de Permiso</param>
    ''' <returns>Nombre del permiso</returns>
    ''' <remarks></remarks>
    Private Function GetRightName(ByVal rightType As RightsType) As String
        Select Case rightType
            Case RightsType.Edit
                Return "Edición"
            Case RightsType.ReIndex
                Return "Reindexar"
            Case RightsType.HideReplaceDocument
                Return "Reemplazar documento"
        End Select
    End Function

    ''' <summary>
    ''' Obtiene la descripción del valor de la acción a tomar con el permiso
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetActionValue(ByVal value As Boolean) As String
        If value Then
            Return "Deshabilitar"
        Else
            Return "Habilitar"
        End If
    End Function

    Private Sub btnSaveRule_Click(sender As System.Object, e As EventArgs) Handles btnSaveRule.Click
        Try
            EncodeRights()
            WFRulesBusiness.UpdateParamItem(Rule.ID, 0, currentRule.EncodedRights)
            UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
            lblSavedData.Text = _SAVED_DATA & Now.ToLongTimeString
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub btnRemoveRight_Click(sender As System.Object, e As EventArgs) Handles btnRemoveRight.Click
        If dgvRights.SelectedRows.Count = 1 Then
            dgvRights.Rows.Remove(dgvRights.SelectedRows(0))
        End If
    End Sub

    Private Sub btnAddRight_Click(sender As System.Object, e As EventArgs) Handles btnAddRight.Click
        Dim selectedRight As RightsType = cmbRights.SelectedValue
        Dim i As Int16

        'Verifica que el permiso no se haya agregado anteriormente
        For i = 0 To dgvRights.Rows.Count - 1
            If dgvRights.Rows(i).Cells("rightType").Value = selectedRight Then
                MessageBox.Show("El permiso seleccionado ya ha sido agregado a la lista", "Permiso ya configurado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit For
            End If
        Next

        'Si el permiso no se encuentra en la lista
        If i = dgvRights.Rows.Count Then
            'Agrega el permiso a la grilla
            dgvRights.Rows.Add(New Object() {cmbRights.Text, _
                                     CInt(selectedRight), _
                                     GetActionValue(chkAction.Checked), _
                                     chkAction.Checked})
        End If
    End Sub

    ''' <summary>
    ''' Codifica los permisos para poder ser guardados en la configuracion de la regla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EncodeRights()
        Dim sbRights As New StringBuilder
        For i As Int16 = 0 To dgvRights.Rows.Count - 1
            sbRights.Append(CInt(dgvRights.Rows(i).Cells("rightType").Value))
            sbRights.Append(_COMMA)
            sbRights.Append(dgvRights.Rows(i).Cells("value").Value)
            sbRights.Append(_SEMICOLON)
        Next

        'Se quita el punto y coma final
        sbRights = sbRights.Remove(sbRights.Length - 1, 1)

        'Se pisan los permisos codificados
        currentRule.EncodedRights = sbRights.ToString

        'Se iguala a nothing para que la próxima vez se recarguen los permisos actualizados
        currentRule.Rights = Nothing

        sbRights.Remove(0, sbRights.Length)
        sbRights = Nothing
    End Sub

End Class