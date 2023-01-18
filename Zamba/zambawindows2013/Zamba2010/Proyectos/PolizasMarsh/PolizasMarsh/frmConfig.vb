Imports Zamba.Servers

Public Class frmConfig

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Public Sub New(ByVal docTypeId As Int64, ByVal indexId As Int64, ByVal excelColumn As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        LoadDocTypes(docTypeId)
        LoadIndexes(indexId)
        txtExcelColumn.Text = excelColumn

    End Sub

    ''' <summary>
    ''' Carga los tipos de documento en el combo y selecciona uno
    ''' </summary>
    ''' <param name="selectedValue">DocTypeId (Int64)</param>
    ''' <remarks></remarks>
    Private Sub LoadDocTypes(ByVal selectedValue As Int64)
        Try
            Dim dt As DataTable = Server.Con.ExecuteDataset(CommandType.Text, "SELECT DOC_TYPE_ID,DOC_TYPE_NAME FROM DOC_TYPE ORDER BY DOC_TYPE_NAME").Tables(0)
            cmbDocType.DataSource = dt
            cmbDocType.DisplayMember = "DOC_TYPE_NAME"
            cmbDocType.ValueMember = "DOC_TYPE_ID"

            If selectedValue <> 0 Then
                cmbDocType.SelectedValue = selectedValue
            Else
                cmbDocType.SelectedIndex = 0
            End If
        Catch ex As Exception
            Trace.WriteLine("ERROR al cargar los tipos de documentos")
            Trace.WriteLine(ex.Message)
            cmbDocType.Text = "No se han podido cargar los tipos de documento"
            cmbDocType.Enabled = False
        End Try
    End Sub

    ''' <summary>
    ''' Carga los índices en el combo y selecciona uno
    ''' </summary>
    ''' <param name="selectedValue">IndexId (Int64)</param>
    ''' <remarks></remarks>
    Private Sub LoadIndexes(ByVal selectedValue As Int64)
        Try
            Dim dt As DataTable = Server.Con.ExecuteDataset(CommandType.Text, "SELECT INDEX_ID,INDEX_NAME FROM DOC_INDEX ORDER BY INDEX_NAME").Tables(0)
            cmbIndex.DataSource = dt
            cmbIndex.DisplayMember = "INDEX_NAME"
            cmbIndex.ValueMember = "INDEX_ID"

            If selectedValue <> 0 Then
                cmbIndex.SelectedValue = selectedValue
            Else
                cmbIndex.SelectedIndex = 0
            End If
        Catch ex As Exception
            Trace.WriteLine("ERROR al cargar los índices")
            Trace.WriteLine(ex.Message)
            cmbIndex.Text = "No se han podido cargar los índices"
            cmbIndex.Enabled = False
        End Try
    End Sub

End Class