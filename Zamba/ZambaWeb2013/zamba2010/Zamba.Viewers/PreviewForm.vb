Imports Zamba.Core
Imports System.IO

Public Class PreviewForm

    Private _result As IResult
    Private _form As ZwebForm
    Public frmbrowser As FormBrowser

    Public Sub New(ByVal r As IResult)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me._result = r
        ' Add any initialization after the InitializeComponent() call.
        frmbrowser = New FormBrowser
        '[sebastian] 09-06-2009 se agrego parse 
        _form = FormBusiness.GetShowAndEditForms(Int32.Parse(r.DocType.ID.ToString))(0)
        If (_form.Type <> FormTypes.Edit) Then
            _form.Type = FormTypes.Show
        End If
        r.CurrentFormID = _form.ID
        frmbrowser.ShowDocument(DirectCast(r, Result), _form)
        frmbrowser.Dock = DockStyle.Fill
        Me.Panel2.Controls.Add(frmbrowser)
    End Sub

    Public Sub New(ByVal r As IResult, ByVal str As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.btnSend.Text = str
        Me._result = r
        ' Add any initialization after the InitializeComponent() call.
        frmbrowser = New FormBrowser
        '[sebastian] 09-06-2009 se agrego parse 
        _form = FormBusiness.GetShowAndEditForms(Int32.Parse(r.DocType.ID.ToString))(0)
        If (_form.Type <> FormTypes.Edit) Then
            _form.Type = FormTypes.Show
        End If
        r.CurrentFormID = _form.ID
        frmbrowser.ShowDocument(DirectCast(r, Result), _form)
        frmbrowser.Dock = DockStyle.Fill
        Me.Panel2.Controls.Add(frmbrowser)
    End Sub

    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
        'Dim fil As String = Tools.EnvironmentUtil.GetTempDir("\temp").FullName & "\" & (New FileInfo(_form.Path)).Name
        'If File.Exists(fil) Then
        Me.DialogResult = Windows.Forms.DialogResult.OK
        'Else
        'Me.DialogResult = Windows.Forms.DialogResult.Cancel
        'End If
    End Sub
End Class
