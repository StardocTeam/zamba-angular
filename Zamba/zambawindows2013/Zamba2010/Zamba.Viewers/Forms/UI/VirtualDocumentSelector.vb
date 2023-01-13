Imports Zamba.Core

Public Class VirtualDocumentSelector
    Inherits Zamba.AppBlock.ZForm

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

    ''' <summary>
    ''' Evento que se ejecuta cuando se presiona en el botón "Crear"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	20/04/2009	Modified    Se coloca temporalmente en la propiedad AutoName del Result el id y nombre del formulario. Esto es 
    '''                                         necesario para recuperar después el id y el nombre del formulario si es que el elemento que se 
    '''                                         selecciona es un formulario dinámico. Más tarde la propiedad Autoname vuelve a su estado original 
    ''' </history>
    Private Sub btCrearDocumentoVirtual_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btCrearDocumentoVirtual.Click

        CreateForm()

    End Sub

    Public Function GetInsertedDocument() As NewResult
        Return DirectCast(_insertedNewResult, NewResult)
    End Function

    Private Sub cboTipoDeDocumento_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTipoDeDocumento.SelectedIndexChanged

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

            DialogResult = DialogResult.OK
            _insertedNewResult.CurrentFormID = CurrentForm.ID
            Close()

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
End Class