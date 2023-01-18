Imports Zamba.Core

Public Class UCVirtualDocumentSelector
    Inherits Zamba.AppBlock.ZControl

    Private _insertedNewResult As INewResult = Nothing

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub VirtualDocumentSelector_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load

        Try
            cboTipoDeDocumento.DataSource = FormBusiness.GetVirtualDocumentsByRightsOfCreate(FormTypes.Insert, UserBusiness.Rights.CurrentUser.ID)
            cboTipoDeDocumento.DisplayMember = "Name"
            cboTipoDeDocumento.ValueMember = "ID"
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub


     

    Private Sub cboTipoDeDocumento_DoubleClick(sender As Object, e As EventArgs) Handles cboTipoDeDocumento.DoubleClick
        CreateForm()
    End Sub

    Private Sub CreateForm()
        Try

            Dim CurrentForm As ZwebForm = DirectCast(cboTipoDeDocumento.SelectedItem, ZwebForm)
            _insertedNewResult = FormBusiness.CreateVirtualDocument(CurrentForm.DocTypeId)

            ' Si la propiedad AutoName se encuentra en Nothing entonces se coloca en esa propiedad el nombre del formulario
            If (IsNothing(_insertedNewResult.AutoName)) Then
                _insertedNewResult.AutoName = CurrentForm.Name & " - " & DateTime.Now.ToString()
                ' de lo contrario se adjunta el nombre del formulario a lo que ya se encuentra
            Else
                _insertedNewResult.AutoName = _insertedNewResult.AutoName & " - " & CurrentForm.Name & DateTime.Now.ToString()
            End If

            _insertedNewResult.CurrentFormID = CurrentForm.ID

            RaiseEvent FormSelected(_insertedNewResult)

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public event FormSelected(NewResult As INewResult)
End Class