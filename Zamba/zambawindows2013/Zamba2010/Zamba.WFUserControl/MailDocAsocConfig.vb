Public Class MailDocAsocConfig
    Inherits Zamba.AppBlock.ZForm

    Public mRule As IDOMail
    Public Sub New(ByVal DoMail As IDOMail)
        mRule = DoMail

        InitializeComponent()

        Dim DSDocTypes As DataSet = Zamba.Core.DocTypesBusiness.GetDocTypesIdsAndNamesSorted
        If IsNothing(DSDocTypes) = False AndAlso DSDocTypes.Tables.Count > 0 Then
            LstDocTypes.DisplayMember = "Doc_Type_Name"
            LstDocTypes.ValueMember = "Doc_Type_Id"
            LstDocTypes.DataSource = DSDocTypes.Tables(0)
        End If
        Dim Indexs As New DataSet
        Indexs = IndexsBusiness.GetIndex

        cbIndexs.DisplayMember = "INDEX_NAME"
        cbIndexs.ValueMember = "INDEX_ID"
        cbIndexs.DataSource = Indexs.Tables(0)
        'Me.cbIndexs.SelectedIndex = 0

        Dim enumtype As Type = GetType(Comparadores)
        For Each x As Comparadores In [Enum].GetValues(enumtype)
            cbOperator.Items.Add(x.ToString)
        Next

        LoadConfiguration()

    End Sub

    ''' <summary>
    ''' Metodo que carga los datos de configuracion.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Ezequiel] 11/05/09 - Modified: Se realizo la carga de datos real, ya que antes no lo hacia como debia hacerlo
    Private Sub LoadConfiguration()
        Try
            If mRule.DTType = IMailConfigDocAsoc.DTTypes.AllDT Then
                rdbAll.Checked = True
            Else
                rdbSelect.Checked = True
                LstDocTypes.Enabled = True
            End If

            If mRule.Selection = IMailConfigDocAsoc.Selections.First Then
                rdbFirst.Checked = True
            ElseIf mRule.Selection = IMailConfigDocAsoc.Selections.Filter Then
                rdbFilter.Checked = True
                PanelIndexs.Enabled = True
            ElseIf mRule.Selection = IMailConfigDocAsoc.Selections.Manual Then
                rdbManual.Checked = True
            ElseIf mRule.Selection = IMailConfigDocAsoc.Selections.FilterDocId Then
                rdbFilterDocId.Checked = True
            Else
                rdbSelAll.Checked = True
            End If

            cbOperator.SelectedItem = cbOperator.Items(cbOperator.Items.IndexOf(mRule.Oper.ToString))
            txtIndexValue.Text = mRule.IndexValue
            cbIndexs.SelectedValue = mRule.Index
            txtDocIdFilter.Text = mRule.FilterDocID

            Dim docIDs As ArrayList = ArrayList.Adapter(mRule.DocTypes.Split("|"))
            Dim objItems As New ArrayList
            For Each selit As Object In LstDocTypes.Items
                If docIDs.Contains(selit.row.itemarray(0).ToString) Then
                    objItems.Add(selit)
                End If
            Next
            LstDocTypes.SelectedItems.Clear()
            For Each it As Object In objItems
                LstDocTypes.SelectedItems.Add(it)
            Next
            chkKeepAssociatedDocName.Checked = mRule.KeepAssociatedDocsName

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnSave.Click
        Try
            DialogResult = DialogResult.OK
            SaveData()
            Close()
        Catch ex As Exception
            ZClass.raiseerror(ex)

        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    ''' <summary>
    ''' Graba los datos en la regla
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Ezequiel] 08/05/09 - Created
    Private Sub SaveData()
        mRule.IndexValue = txtIndexValue.Text
        Dim selDocs As String

        For Each s As DataRowView In LstDocTypes.SelectedItems
            selDocs += s.Row.ItemArray(0) & "|"
        Next
        If IsNothing(selDocs) = False AndAlso selDocs.Length > 0 Then selDocs = selDocs.Substring(0, selDocs.Length - 1)
        mRule.DocTypes = selDocs
        mRule.Oper = [Enum].Parse(GetType(Comparadores), cbOperator.SelectedItem.ToString)
        mRule.Index = cbIndexs.SelectedValue
        mRule.FilterDocID = txtDocIdFilter.Text
        If rdbAll.Checked = True Then
            mRule.DTType = IMailConfigDocAsoc.DTTypes.AllDT
        Else
            mRule.DTType = IMailConfigDocAsoc.DTTypes.SelectDT
        End If

        If rdbFirst.Checked = True Then
            mRule.Selection = IMailConfigDocAsoc.Selections.First
        ElseIf rdbFilter.Checked = True Then
            mRule.Selection = IMailConfigDocAsoc.Selections.Filter
        ElseIf rdbManual.Checked = True Then
            mRule.Selection = IMailConfigDocAsoc.Selections.Manual
        ElseIf rdbFilterDocId.Checked = True Then
            mRule.Selection = IMailConfigDocAsoc.Selections.FilterDocId
        Else
            mRule.Selection = IMailConfigDocAsoc.Selections.All
        End If
        mRule.KeepAssociatedDocsName = chkKeepAssociatedDocName.Checked

    End Sub

    '[Ezequiel] 08/05/09 - Comente lo siguiente debido a que estaba mal aplicado, los cambios a la regla los
    '                      debe realizar cuando se clickea sobre el boton guardar.
    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rdbAll.CheckedChanged, rdbSelect.CheckedChanged
        If rdbAll.Checked = True Then
            LstDocTypes.Enabled = False
        Else
            LstDocTypes.Enabled = True
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rdbSelAll.CheckedChanged, rdbManual.CheckedChanged, rdbFirst.CheckedChanged, rdbFilter.CheckedChanged, rdbFilterDocId.CheckedChanged
        If rdbFilter.Checked = True Then
            PanelIndexs.Enabled = True
            txtDocIdFilter.Enabled = False
        Else
            PanelIndexs.Enabled = False
            If rdbFilterDocId.Checked Then
                txtDocIdFilter.Enabled = True
            Else
                txtDocIdFilter.Enabled = False
            End If
        End If
    End Sub
End Class