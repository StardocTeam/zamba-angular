Public Class UCDoShowForm
    Inherits ZRuleControl

    Private arrDocTypes As New ArrayList
    Private m_bInit As Boolean = True
    Private tmpRule As IDoShowForm

    Public Sub New(ByVal _tmpRule As IDoShowForm, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(_tmpRule, _wfPanelCircuit)
        InitializeComponent()
        tmpRule = _tmpRule
        this_load()
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un objeto DocType partiendo desde su Id
    ''' </summary>
    ''' <param name="p_iId">DocTypeId</param>
    ''' <returns>DocType</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[oscar]	16/08/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Function BuscarDocTypeById(ByVal p_iId As Int32) As Zamba.Core.DocType
        Dim e As IEnumerator
        Try
            e = arrDocTypes.GetEnumerator
            While e.MoveNext
                If DirectCast(e.Current, Zamba.Core.DocType).Id = p_iId Then
                    Return DirectCast(e.Current, Zamba.Core.DocType)
                End If
            End While
            Return Nothing
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
            Return Nothing
        Finally
            e = Nothing
        End Try
    End Function



    Private Sub LlenarCombo()
        Dim doc As Zamba.Core.DocType = Nothing
        Cursor = Cursors.WaitCursor
        'RemoveHandler Me.cmbForms.SelectedIndexChanged, AddressOf SelectedIndexChanged
        cmbForms.BeginUpdate()
        Try
            If Not IsNothing(arrDocTypes) Then
                If arrDocTypes.Count > 0 Then
                    cmbForms.DataSource = arrDocTypes
                    cmbForms.DisplayMember = "Name"
                    cmbForms.ValueMember = "Id"
                    If m_bInit Then
                        Try
                            'doc = BuscarDocTypeById(m_iDocTypeIdVirtual)
                            If Not IsNothing(doc) Then
                                cmbForms.SelectedItem = doc
                            End If
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                        End Try
                        m_bInit = False
                    End If
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        cmbForms.EndUpdate()
        'AddHandler Me.cmbForms.SelectedIndexChanged, AddressOf SelectedIndexChanged
        Cursor = Cursors.Arrow
    End Sub

    Private Sub UCDoShowForm_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        this_load()
    End Sub

    ''' <summary>
    ''' Método que se ejecuta cuando se carga la regla DoShowForm
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	07/07/2008	Modified
    '''                 18/07/2008	Modified
    '''     [Sebastian] 31-08-09    Modified se agrego showdialog
    '''     Marcelo     19/10/2009  Modified    Add Maximized Dialog CheckBox
    ''' </history>
    Private Sub this_load()

        cmbForms.DataSource = FormBusiness.GetForms(true)
        cmbForms.DisplayMember = "Name"
        cmbForms.ValueMember = "ID"
        'arrDocTypes = Me.GetDocType()
        'Me.LlenarCombo()

        If tmpRule.FormID > 0 Then
            cmbForms.SelectedValue = Int64.Parse(tmpRule.FormID.ToString())
        End If

        chkAssociatedDocDataShow.Checked = tmpRule.associatedDocDataShow

        txtVarDoc_id.Text = tmpRule.varDocId

        chkShowDialogMaximized.Checked = tmpRule.DontShowDialogMaximized
        chkViewOriginal.Checked = tmpRule.ViewOriginal
        chkCerrarConCruz.Checked = tmpRule.ControlBox
        chkCloseFormAfterRuleExecution.Checked = tmpRule.CloseFormWindowAfterRuleExecution
        chkViewAsoc.Checked = tmpRule.ViewAsociatedDocs
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se presiona el botón Aceptar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	07/07/2008	Modified
    '''                 18/07/2008	Modified
    '''     [Sebastian] 31-08-09    Modified se agrego para mostrar el form como showdialog
    '''     Marcelo     19/10/2009  Modified    Add Maximized Dialog CheckBox, I remove showdialog, because it was never implemented
    ''' </history>
    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAceptar.Click

        tmpRule.FormID = cmbForms.SelectedValue
        WFRulesBusiness.UpdateParamItem(tmpRule.ID, 0, tmpRule.FormID)

        tmpRule.associatedDocDataShow = chkAssociatedDocDataShow.Checked
        WFRulesBusiness.UpdateParamItem(tmpRule.ID, 1, tmpRule.associatedDocDataShow)

        tmpRule.varDocId = txtVarDoc_id.Text
        WFRulesBusiness.UpdateParamItem(tmpRule.ID, 2, tmpRule.varDocId)

        tmpRule.DontShowDialogMaximized = chkShowDialogMaximized.Checked
        WFRulesBusiness.UpdateParamItem(tmpRule.ID, 3, tmpRule.DontShowDialogMaximized)

        tmpRule.ViewOriginal = chkViewOriginal.Checked
        WFRulesBusiness.UpdateParamItem(tmpRule.ID, 4, tmpRule.ViewOriginal)

        tmpRule.ControlBox = chkCerrarConCruz.Checked
        WFRulesBusiness.UpdateParamItem(tmpRule.ID, 5, tmpRule.ControlBox)

        tmpRule.CloseFormWindowAfterRuleExecution = chkCloseFormAfterRuleExecution.Checked
        WFRulesBusiness.UpdateParamItem(tmpRule.ID, 6, tmpRule.CloseFormWindowAfterRuleExecution)

        tmpRule.ViewAsociatedDocs = chkViewAsoc.Checked
        WFRulesBusiness.UpdateParamItem(tmpRule.ID, 7, tmpRule.ViewAsociatedDocs)

        tmpRule.RefreshForm = CheckBox1.Checked
        WFRulesBusiness.UpdateParamItem(tmpRule.ID, 8, tmpRule.RefreshForm)

        UserBusiness.Rights.SaveAction(tmpRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & tmpRule.Name & "(" & tmpRule.ID & ")")
    End Sub

    Private Sub chkCerrarConCruz_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles chkCerrarConCruz.CheckedChanged

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

    End Sub

    Private Sub chkAssociatedDocDataShow_CheckedChanged(sender As Object, e As EventArgs) Handles chkAssociatedDocDataShow.CheckedChanged

    End Sub
End Class