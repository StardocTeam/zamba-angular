Imports Zamba.Core.Search
Imports Zamba.Core
Imports Zamba.Data

Public Class frmVersionedDetails
    Inherits ZForm

    Dim _result As Result
    Dim _resultSelected As Result
    Dim ResultsList As New ArrayList
    Dim isDoubleCLick As Boolean = False

    Public Event ShowVersion(ByRef result As Result)
    Public Event PublishVersion(ByVal result As Result)

    Public Sub New(ByVal result As Result)
        InitializeComponent()
        _result = result
        loadVersioned()
        lblFechaCreacion.Text = "Seleccione"
        lblFechaCreacion.Text = "Seleccione"
        lblUsuarioCreador.Text = "Seleccione"
    End Sub


    ''' <summary>
    ''' Carga las versiones
    ''' </summary>
    ''' <history>   Marcelo Modified 20/08/2009</history>
    ''' <remarks></remarks>
    Private Sub loadVersioned()
        Dim dt As DataTable = ModDocuments.SearchVersions(DirectCast(_result.DocType, DocType), _result.RootDocumentId, _result.ID, Nothing)

        For Each row As DataRow In dt.Rows
            If Not Cache.DocTypesAndIndexs.hsDocTypes.Contains(CInt(row("doc_type_id"))) Then
                Dim _doctype As New DocType(CInt(row("doc_type_id")), row("Entidad").ToString(), CInt(row("icon_Id")))
                _doctype.Indexs = ZCore.FilterIndex(CInt(_doctype.ID))
                Cache.DocTypesAndIndexs.hsDocTypes.Add(CInt(row("doc_type_id")), _doctype)
            End If

            Dim doctypeid As Int64 = CInt(row("doc_type_id"))
            Dim r As Result = New Result(CInt(row("doc_id")), Cache.DocTypesAndIndexs.hsDocTypes(doctypeid), row(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME).ToString(), 0)

            Results_Business.CompleteDocument(r, row)
            ResultsList.Add(r)
        Next

        For Each result As Result In ResultsList
            Dim name As String = result.Name & " (V. " & result.VersionNumber & ")"

            If result.RootDocumentId = 0 Then

                Dim RootNode As New TreeNode(name)
                RootNode.Name = name
                RootNode.Tag = result.ID
                treeViewVersionedDetails.Nodes.Add(RootNode)
                LoadChilds(result, RootNode)
            End If
        Next
        treeViewVersionedDetails.Nodes(0).ExpandAll()
    End Sub

    Private Sub LoadChilds(ByVal Result As Result, ByVal RootNode As TreeNode)
        Dim name As String = String.Empty
        For Each R As Result In ResultsList
            If R.ParentVerId = Result.ID Then
                name = R.Name & " (V. " & R.VersionNumber & If(R.HasVersion = 2, " - Publicación)", ")")
                Dim NewNode As New TreeNode(name)
                NewNode.Name = name
                NewNode.Tag = R.ID
                RootNode.Nodes.Add(NewNode)
                LoadChilds(R, NewNode)
            End If
        Next
    End Sub


    Private Sub treeViewVersionedDetails_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles treeViewVersionedDetails.AfterSelect
        Dim docid As Int64 = CLng(treeViewVersionedDetails.SelectedNode.Tag)
        For Each _result As Result In ResultsList
            If _result.ID = docid Then
                _resultSelected = _result
                lblDoc.Text = _result.Name.ToString & "(" & _result.OriginalName & ")"
                lblVersionNum.Text = _result.VersionNumber.ToString
                lblFechaCreacion.Text = _result.CreateDate.ToString
                lblFechaEdicion.Text = _result.EditDate.ToString
                lblUsuarioCreador.Text = Results_Business.GetCreatorUser(docid)
                txtComentarioVersion.Text = Results_Business.GetVersionComment(docid)

                If Results_Business.IsResultPublished(_result.DocTypeId, docid) Then
                    lblPublish.Text = "SI"
                    lblUPub.Visible = True
                    lblUserPublish.Visible = True
                    lblPublicationDate.Visible = True
                    lblFP.Visible = True
                    lblUserPublish.Text = Results_Business.GetPublisherUser(docid)
                    lblPublicationDate.Text = Results_Business.GetPublishDate(docid)
                Else
                    lblPublish.Text = "NO"
                    lblUPub.Visible = False
                    lblUserPublish.Visible = False
                    lblPublicationDate.Visible = False
                    lblFP.Visible = False
                End If
            End If
        Next
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Try
            If _resultSelected IsNot Nothing Then
                RaiseEvent ShowVersion(_resultSelected)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Try
            If _resultSelected IsNot Nothing Then
                RaiseEvent PublishVersion(_resultSelected)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub treeViewVersionedDetails_DoubleClick(sender As Object, e As EventArgs) Handles treeViewVersionedDetails.DoubleClick
        Try
            Try
                If _resultSelected IsNot Nothing Then
                    RaiseEvent ShowVersion(_resultSelected)
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub treeViewVersionedDetails_BeforeCollapse(sender As Object, e As TreeViewCancelEventArgs) Handles treeViewVersionedDetails.BeforeCollapse
        If isDoubleCLick AndAlso e.Action = TreeViewAction.Collapse Then
            e.Cancel = True
        End If
    End Sub

    Private Sub treeViewVersionedDetails_BeforeExpand(sender As Object, e As TreeViewCancelEventArgs) Handles treeViewVersionedDetails.BeforeExpand
        If isDoubleCLick AndAlso e.Action = TreeViewAction.Expand Then
            e.Cancel = True
        End If
    End Sub

    Private Sub treeViewVersionedDetails_MouseDown(sender As Object, e As MouseEventArgs) Handles treeViewVersionedDetails.MouseDown
        If e.Clicks > 1 Then
            isDoubleCLick = True
        Else
            isDoubleCLick = False
        End If
    End Sub
End Class