Public Class UCDOGenerateExcelVis
    Inherits ZControl

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal bolInd As Boolean, ByVal bolSum As Boolean, ByVal bolCount As Boolean, ByVal titles As Boolean)
        InitializeComponent()
        chkSum.Checked = bolSum
        chkIndex.Checked = bolInd
        chkCount.Checked = bolCount
        pID = Id
        txtName.Text = Name
        If titles = False Then
            setTitle()
        End If
    End Sub

    Private Sub setTitle()
        lblShow.Visible = False
        lblName.Visible = False
        lblSum.Visible = False
        lblCount.Visible = False
        chkSum.Location = New Point(chkSum.Location.X, 0)
        chkIndex.Location = New Point(chkIndex.Location.X, 0)
        chkCount.Location = New Point(chkCount.Location.X, 0)
        txtName.Location = New Point(txtName.Location.X, 0)
    End Sub

    Private pID As Int64

    Public ReadOnly Property indexName() As String
        Get
            Return txtName.Text
        End Get
    End Property

    Public ReadOnly Property Id() As Int64
        Get
            Return pID
        End Get
    End Property
    Public ReadOnly Property bolSum() As Boolean
        Get
            Return chkSum.Checked
        End Get
    End Property
    Public ReadOnly Property bolShow() As Boolean
        Get
            Return chkIndex.Checked
        End Get
    End Property
    Public ReadOnly Property bolCount() As Boolean
        Get
            Return chkCount.Checked
        End Get
    End Property

    Private Sub chkIndex_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkIndex.CheckedChanged
        If chkIndex.Checked = True Then
            chkSum.Enabled = True
            chkCount.Enabled = True
        Else
            chkSum.Enabled = False
            chkCount.Enabled = False
        End If
    End Sub
End Class
