Imports Zamba.Core.Search
Imports Zamba.Core

Public Class frmVersionedDetails
    Inherits Zamba.AppBlock.ZForm

    Dim _result As Result
    Dim ResultsList As New ArrayList
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
            If Cache.DocTypesAndIndexs.hsDocTypes.Contains(CInt(row("doc_type_id"))) = False Then
                Dim _doctype As New DocType(CInt(row("doc_type_id")), row("Entidad").ToString(), CInt(row("icon_Id")))
                _doctype.Indexs = ZCore.FilterIndex(CInt(_doctype.ID))
                Cache.DocTypesAndIndexs.hsDocTypes.Add(CInt(row("doc_type_id")), _doctype)
            End If
            Dim doctypeid As Int64 = CInt(row("doc_type_id"))
            Dim r As Result = New Result(CInt(row("doc_id")), Cache.DocTypesAndIndexs.hsDocTypes(doctypeid), row("Nombre del Documento").ToString(), 0)

            Results_Business.CompleteDocument(r, row)
            ResultsList.Add(r)
        Next


        For Each result As Result In ResultsList
            If result.RootDocumentId = 0 Then

                Dim RootNode As New TreeNode(result.Name)
                RootNode.Name = result.Name
                RootNode.Tag = result.ID
                treeViewVersionedDetails.Nodes.Add(RootNode)
                LoadChilds(result, RootNode)
            End If
        Next
        treeViewVersionedDetails.Nodes(0).ExpandAll()
    End Sub

    Private Sub LoadChilds(ByVal Result As Result, ByVal RootNode As TreeNode)
        For Each R As Result In ResultsList
            If R.ParentVerId = Result.ID Then

                Dim NewNode As New TreeNode(R.Name)
                NewNode.Name = R.Name
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
                lblFechaCreacion.Text = _result.CreateDate.ToString
                lblFechaEdicion.Text = _result.EditDate.ToString
                lblUsuarioCreador.Text = Results_Business.GetCreatorUser(docid)
                txtComentarioVersion.Text = Results_Business.GetVersionComment(docid)
            End If
        Next
    End Sub
End Class