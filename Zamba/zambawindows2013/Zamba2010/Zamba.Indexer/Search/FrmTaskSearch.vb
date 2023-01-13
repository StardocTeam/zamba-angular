Public Class FrmTaskSearch

    Public Sub New(ByVal Results As DataTable)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        rgvTasks.DataSource = Results

        'Se ocultan las columnas de doc_id y doc_type_id
        rgvTasks.Columns("DOC_ID").IsVisible = False
        rgvTasks.Columns("DOC_TYPE_ID").IsVisible = False

        'Se ocultan los atributos
        For i As Int16 = 9 To rgvTasks.Columns.Count - 1
            rgvTasks.Columns(i).IsVisible = False
        Next
    End Sub

    Private _selectedDocId As Int64
    Public Property SelectedDocId() As Int64
        Get
            Return _selectedDocId
        End Get
        Set(ByVal value As Int64)
            _selectedDocId = value
        End Set
    End Property

    Private _selectedDocTypeId As Int64
    Public Property SelectedDocTypeId() As Int64
        Get
            Return _selectedDocTypeId
        End Get
        Set(ByVal value As Int64)
            _selectedDocTypeId = value
        End Set
    End Property

    Private Sub rgvTasks_CellDoubleClick(sender As System.Object, e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles rgvTasks.CellDoubleClick
        If e.RowIndex > -1 Then
            _selectedDocId = CLng(rgvTasks.SelectedRows(0).Cells("DOC_ID").Value)
            _selectedDocTypeId = CLng(rgvTasks.SelectedRows(0).Cells("DOC_TYPE_ID").Value)
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub
End Class