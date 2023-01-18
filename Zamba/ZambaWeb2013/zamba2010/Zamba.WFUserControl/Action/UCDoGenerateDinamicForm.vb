'Imports Zamba.WFBusiness

Public Class UCDoGenerateDinamicForm

    Inherits ZRuleControl
    Dim CurrentRule As IDoGenerateDinamicForm

#Region "CONSTRUCTOR"

    Public Sub New(ByVal _CurrentRule As IDoGenerateDinamicForm, ByRef _wfPanelCircuit As IWFPanelCircuit)

        MyBase.New(_CurrentRule, _wfPanelCircuit)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        CurrentRule = _CurrentRule
        LoadDocTypes()
        LoadForms()
        LoadParams()

    End Sub

#End Region

#Region "METODOS"

    ''' <summary>
    ''' [Sebastian 09-09-09] 
    ''' Guarda la configuración de la regla
    ''' [Sebastian 17-09-09] se agrego el object type al UpdateParamItem de la regla en la base.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnSave.Click

        Try
            CurrentRule.DocType = lstDocType.SelectedValue
            CurrentRule.Variable = txtVariable.Text.Trim
            CurrentRule.Name = txtFormName.Text
            CurrentRule.ColumnName = txtColumnaDesc.Text

            If chkNoUsarFormDinamico.Checked Then
                CurrentRule.FormId = 0
            Else
                CurrentRule.FormId = cmbForms.SelectedValue
            End If

            WFRulesBusiness.UpdateParamItem(CurrentRule, 0, CurrentRule.Variable.ToString(), ObjectTypes.None)
            WFRulesBusiness.UpdateParamItem(CurrentRule, 1, CurrentRule.DocType.ToString(), ObjectTypes.DocTypes)
            WFRulesBusiness.UpdateParamItem(CurrentRule, 2, CurrentRule.Name, ObjectTypes.None)
            WFRulesBusiness.UpdateParamItem(CurrentRule, 3, CurrentRule.FormId, ObjectTypes.None)
            WFRulesBusiness.UpdateParamItem(CurrentRule, 4, CurrentRule.ColumnName, ObjectTypes.None)
            UserBusiness.Rights.SaveAction(CurrentRule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & CurrentRule.Name & "(" & CurrentRule.ID & ")")
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    ''' <summary>
    ''' [Sebastian 09-09-09]
    ''' Carga los valores de la configuración de la regla.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadParams()

        Try
            lstDocType.SelectedValue = CurrentRule.DocType
            txtVariable.Text = CurrentRule.Variable
            txtFormName.Text = CurrentRule.Name
            txtColumnaDesc.Text = CurrentRule.ColumnName
            If CurrentRule.FormId > 0 Then
                chkNoUsarFormDinamico.Checked = False
                cmbForms.SelectedValue = CurrentRule.FormId
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    ''' <summary>
    ''' [Sebastián 09-09-09]
    ''' Carga los entidades al list box de la regla.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadDocTypes()

        Dim dsDocType As DataSet = DocTypesBusiness.GetAllDocTypes()
        lstDocType.DataSource = dsDocType.Tables(0)
        lstDocType.ValueMember = "doc_type_id"
        lstDocType.DisplayMember = "doc_type_name"

    End Sub

    ''' <summary>
    ''' [AlejandroR 05-01-10]
    ''' Carga los tipos de formularios.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadForms()

        cmbForms.DataSource = FormBusiness.GetForms(true)
        cmbForms.DisplayMember = "Name"
        cmbForms.ValueMember = "ID"

    End Sub

    Private Sub chkNoUsarFormDinamico_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles chkNoUsarFormDinamico.CheckedChanged
        cmbForms.Enabled = Not chkNoUsarFormDinamico.Checked
    End Sub

#End Region

End Class
