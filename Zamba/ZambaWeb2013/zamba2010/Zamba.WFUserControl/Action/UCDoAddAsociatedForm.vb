Imports System.Text

Public Class UCDoAddAsociatedForm
    Inherits ZRuleControl

#Region "Properties"
    Public Shadows ReadOnly Property MyRule() As IDoAddAsociatedForm
        Get
            Return DirectCast(Rule, IDoAddAsociatedForm)
        End Get
    End Property
#End Region

#Region "Miembros"
    Private _docTypeID As Long
#End Region

#Region "Constructor"

    ''' <summary>
    ''' Constructor de la uc de la regla
    ''' </summary>
    ''' <history>
    ''' [Javier]    30-09-2011 CREATED
    ''' </history>
    ''' <param name="rule"></param>
    ''' <remarks></remarks>
    Public Sub New(ByRef rule As IDoAddAsociatedForm, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New(rule, _wfPanelCircuit)
        InitializeComponent()
    End Sub

#End Region

#Region "Eventos"
    ''' <summary>
    ''' User control load event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>
    ''' [Javier]    30-09-2011 CREATED
    ''' </history>
    Private Sub UCAddAsociatedForm_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles Me.Load, MyBase.Load
        Try
            Dim arrayForms As ArrayList = FormBusiness.GetFormsByType(FormTypes.WebInsert)
            arrayForms.AddRange(FormBusiness.GetFormsByType(FormTypes.Insert))
            cboFormType.DataSource = arrayForms
            cboFormType.DisplayMember = "Name"
            cboFormType.ValueMember = "ID"

            AddHandler cboFormType.SelectedIndexChanged, AddressOf cboFormType_SelectedIndexChanged

            If MyRule.FormID <> 0 Then
                cboFormType.SelectedValue = MyRule.FormID
            End If

            chkContinueWithCurrentTasks.Checked = MyRule.ContinueWithCurrentTasks
            chkDontOpenTaskAfterInsert.Checked = MyRule.DontOpenTaskAfterInsert
            chkFillCommonAttributes.Checked = MyRule.FillCommonAttributes
            chkSpecificConfig.Checked = MyRule.HaveSpecificAttributes

            If MyRule.HaveSpecificAttributes Then
                gvAttribute.DataSource = GetAttributesSource(_docTypeID)
                gvAttribute.Columns("DONOTCOMPLETE").HeaderText = "No Autocompletar"
            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Saves rules params
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>
    ''' [Javier]    30-09-2011 CREATED
    ''' </history>
    Private Sub btSave_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btSave.Click
        MyRule.FormID = cboFormType.SelectedValue
        MyRule.ContinueWithCurrentTasks = chkContinueWithCurrentTasks.Checked
        MyRule.DontOpenTaskAfterInsert = chkDontOpenTaskAfterInsert.Checked
        MyRule.FillCommonAttributes = chkFillCommonAttributes.Checked
        MyRule.HaveSpecificAttributes = chkSpecificConfig.Checked
        MyRule.SpecificAttrubutes = FormatSpecificConfiguration()


        WFRulesBusiness.UpdateParamItem(Rule.ID, 0, MyRule.FormID)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 1, MyRule.ContinueWithCurrentTasks)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 2, MyRule.DontOpenTaskAfterInsert)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 3, MyRule.FillCommonAttributes)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 4, MyRule.HaveSpecificAttributes)
        WFRulesBusiness.UpdateParamItem(Rule.ID, 5, MyRule.SpecificAttrubutes)
        UserBusiness.Rights.SaveAction(Rule.ID, ObjectTypes.WFRules, RightsType.Edit, "Se editaron los datos de la regla " & Rule.Name & "(" & Rule.ID & ")")
        HasBeenModified = False
    End Sub

    Private Sub cbSpecificConfig_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles chkSpecificConfig.CheckedChanged
        'Si se chequea la configuracion especifica 
        If chkSpecificConfig.Checked Then
            gvAttribute.Enabled = True
            gvAttribute.Visible = True
            gvAttribute.DataSource = GetAttributesSource(_docTypeID)
            gvAttribute.Columns("DONOTCOMPLETE").HeaderText = "No Autocompletar"
            chkFillCommonAttributes.Enabled = False
            chkFillCommonAttributes.Checked = False
        Else
            gvAttribute.Enabled = False
            gvAttribute.Visible = False
            chkFillCommonAttributes.Enabled = True
        End If
        HasBeenModified = True
    End Sub

    Private Sub cboFormType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs)
        _docTypeID = FormBusiness.GetForm(cboFormType.SelectedValue).DocTypeId

        If chkSpecificConfig.Checked Then
            gvAttribute.DataSource = GetAttributesSource(_docTypeID)
            gvAttribute.Columns("DONOTCOMPLETE").HeaderText = "No Autocompletar"
            gvAttribute.Enabled = True
            gvAttribute.Visible = True
        Else
            gvAttribute.Enabled = False
            gvAttribute.Visible = False
        End If
    End Sub
#End Region

#Region "Metodos"
    ''' <summary>
    ''' Carga en un datatable en base a la los atributos de la entidad y a la configuracion de la regla
    ''' </summary>
    ''' <param name="docTypeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetAttributesSource(ByVal docTypeId As Long) As DataTable
        Dim dt As DataTable = IndexsBusiness.GetIndexSchemaAsDataSet(docTypeId).Tables(0)
        If Not dt.Columns.Contains("ValorIndice") Then
            dt.Columns.Add("ValorIndice")
        End If

        If dt.Columns.Contains("Index_Type") Then
            dt.Columns.Remove(dt.Columns("Index_Type"))
        End If

        If dt.Columns.Contains("Index_Len") Then
            dt.Columns.Remove(dt.Columns("Index_Len"))
        End If

        If dt.Columns.Contains("DropDown") Then
            dt.Columns.Remove(dt.Columns("DropDown"))
        End If

        If dt.Columns.Contains("IsReferenced") Then
            dt.Columns.Remove(dt.Columns("IsReferenced"))
        End If

        If dt.Columns.Contains("Orden") Then
            dt.Columns.Remove(dt.Columns("Orden"))
        End If

        If dt.Columns.Contains("IndicePadre") Then
            dt.Columns.Remove(dt.Columns("IndicePadre"))
        End If

        If dt.Columns.Contains("IndiceHijo") Then
            dt.Columns.Remove(dt.Columns("IndiceHijo"))
        End If

        If dt.Columns.Contains("DataTableName") Then
            dt.Columns.Remove(dt.Columns("DataTableName"))
        End If

        Dim col As New DataColumn("DONOTCOMPLETE")
        col.DataType = GetType(Boolean)
        col.DefaultValue = False
        col.Caption = "No Autocompletar"

        If Not dt.Columns.Contains("DONOTCOMPLETE") Then
            dt.Columns.Add(col)
        End If

        dt.Columns("DONOTCOMPLETE").Caption = "No Autocompletar"

        If Not String.IsNullOrEmpty(MyRule.SpecificAttrubutes) AndAlso _
            MyRule.FormID = cboFormType.SelectedValue Then

            dt = CompleteAttributesValues(dt)
        End If

        Return dt
    End Function

    ''' <summary>
    ''' Completa los valores del datatable con los atributos
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CompleteAttributesValues(ByVal dt As DataTable) As DataTable

        Dim strIndex As String = MyRule.SpecificAttrubutes.Replace("//", "§")
        Dim value As String
        Dim id As Int64
        Dim strItem As String
        Dim indices As New Hashtable

        While Not String.IsNullOrEmpty(strIndex)
            'Obtengo el item (// separa por items y | separa por valor y no completar)

            strItem = strIndex.Split("§")(0)
            id = Int(strItem.Split("|")(0))
            value = strItem.Substring(strItem.IndexOf("|", StringComparison.Ordinal) + 1)

            indices.Add(Int64.Parse(id), value)

            strIndex = strIndex.Remove(0, strIndex.Split("§")(0).Length)
            If strIndex.Length > 0 Then
                strIndex = strIndex.Remove(0, 1)
            End If
        End While

        Dim rowCount As Integer = dt.Rows.Count
        Dim currRow As DataRow
        For i As Integer = 0 To rowCount - 1
            currRow = dt.Rows(i)
            If Not IsNothing(currRow("Index_Id")) Then
                If (indices.Contains(Int64.Parse(currRow("Index_Id")))) Then

                    'Obtiene el valor y el check para no autocompletar (si es que existe)
                    value = indices(Int64.Parse(currRow("Index_Id")))

                    'Obtiene la posición del último "|" que separa la configuracion del check
                    id = value.LastIndexOf("|", StringComparison.Ordinal)

                    'Verifica que exista
                    If id > -1 Then
                        'Obtiene el autocompletado
                        strItem = value.Substring(id + 1)

                        'Verifica si debe dejar el valor que tiene y no autocompletarlo
                        If String.Compare(strItem, "[no_completar]") = 0 Then
                            currRow("ValorIndice") = value.Substring(0, id)
                            currRow("DONOTCOMPLETE") = True
                        Else
                            currRow("ValorIndice") = value
                            currRow("DONOTCOMPLETE") = False
                        End If
                    Else
                        'En caso de no encontrar un valor para no autocompletar
                        currRow("ValorIndice") = value
                        currRow("DONOTCOMPLETE") = False
                    End If

                End If
            End If
        Next

        Return dt
    End Function

    ''' <summary>
    ''' Formatea la configuracion hecha en la grilla a un string
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function FormatSpecificConfiguration() As String
        Dim config As New StringBuilder()

        Dim valor As Boolean
        Dim completar As Boolean

        For Each row As DataGridViewRow In gvAttribute.Rows
            'Se guardan las condiciones
            valor = (Not IsNothing(row.Cells("valorIndice").Value) AndAlso Not IsDBNull(row.Cells("valorIndice").Value))
            completar = row.Cells("DONOTCOMPLETE").Value

            'Verifica que se deba completar algún dato del Atributo
            If (chkSpecificConfig.Checked) Then

                'Verifica si debe completar información del valor
                If valor Then
                    config.Append(row.Cells("Index_Id").Value)
                    config.Append("|")
                    config.Append(row.Cells("valorIndice").Value)
                Else
                    config.Append(row.Cells("Index_Id").Value)
                    config.Append("|")
                End If

                'Verifica si debe completar informacion de 
                If completar Then
                    config.Append("|")
                    config.Append("[no_completar]")
                End If

                config.Append("//")
            End If
        Next

        Return config.ToString()
    End Function

    Private Sub cboFormType_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles cboFormType.SelectedIndexChanged

    End Sub

    Private Sub chkContinueWithCurrentTasks_CheckedChanged(sender As Object, e As EventArgs) Handles chkContinueWithCurrentTasks.CheckedChanged
        HasBeenModified = True
    End Sub

    Private Sub chkDontOpenTaskAfterInsert_CheckedChanged(sender As Object, e As EventArgs) Handles chkDontOpenTaskAfterInsert.CheckedChanged
        HasBeenModified = True
    End Sub

    Private Sub chkFillCommonAttributes_CheckedChanged(sender As Object, e As EventArgs) Handles chkFillCommonAttributes.CheckedChanged
        HasBeenModified = True
    End Sub
#End Region

End Class
