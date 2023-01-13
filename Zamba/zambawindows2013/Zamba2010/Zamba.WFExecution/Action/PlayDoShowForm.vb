Imports Zamba.Core.DocTypes.DocAsociated
Imports Zamba.Core.Enumerators
Imports Zamba.Viewers
Imports System.Windows.Forms
Imports Zamba.Controls

Public Class PlayDoShowForm
    Public Event showFormInsideTheTab()
    Private Changed As Boolean
    Private CancelRules As Boolean
    Private myRule As IDoShowForm
    ''' <summary>
    ''' Método utilizado para ejecutar la regla DoShowForm
    ''' </summary>
    ''' <param name="results"></param>
    ''' <param name="myRule"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	07/07/2008	Modified
    '''     [Gaston]	22/08/2008	Modified
    ''' </history>
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        myRule = myRule

        If myRule.associatedDocDataShow Then
            showAsociatedDocuments(results, myRule)
        Else
            showDocuments(results, myRule)
        End If

        If CancelRules Then
            CancelRules = False
            If myRule.ThrowExceptionIfCancel Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Regla configurada para provocar una exception en caso de cancelar la misma")
                Throw New Exception("El usuario cancelo la ejecucion de la regla")
            Else
                Return Nothing
            End If
        Else
            CancelRules = False
            Return results
        End If
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    ''' <summary>
    ''' Método que sirve para mostrar documentos asociados al result, más especificamente un formulario asociado
    ''' al documento asociado al result
    ''' </summary>
    ''' <param name="results"></param>
    ''' <param name="myRule"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	08/07/2008	Created
    '''     [Gaston]	18/07/2008	Modified
    '''     [Gaston]	06/11/2008	Modified      Si la regla es de tipo AbrirDocumento entonces se guarda el varDocId en file
    ''' </history>
    Private Sub showAsociatedDocuments(ByRef results As System.Collections.Generic.List(Of Core.ITaskResult), ByRef myRule As IDoShowForm)

        If (myRule.RuleParentType <> TypesofRules.AbrirDocumento) Then

            ' Se recorren los results
            For Each r As Core.ITaskResult In results
                Dim ResultsAsociated As ArrayList
                ' Se obtienen los documentos asociados
                ResultsAsociated = DocAsociatedBusiness.getAsociatedResultsFromResult(r)

                ' Si la variable doc_id no está vacía
                If Not (String.IsNullOrEmpty(myRule.varDocId)) Then
                    ' Se busca el valor que contiene la variable doc_id (id del new result que se creo en el play
                    ' de la regla DOCreateForm) 
                    Dim id_VarDocId As Long = WFRuleParent.ObtenerValorVariableObjeto(myRule.varDocId)

                    If id_VarDocId <> 0 Then
                        ' Se recorren los documentos asociados para buscar el que coincide con el form configurado en
                        ' la regla
                        For Each asociatedResult As Result In ResultsAsociated

                            If (asociatedResult.ID = id_VarDocId) Then
                                ' Se obtiene el formID
                                asociatedResult.PreviusFormID = asociatedResult.CurrentFormID
                                asociatedResult.CurrentFormID = myRule.FormID
                                ShowAsociatedResult(asociatedResult, r)
                                asociatedResult.CurrentFormID = asociatedResult.PreviusFormID
                                Exit For
                            End If
                        Next
                    Else
                        If ResultsAsociated.Count > 0 Then
                            ResultsAsociated(0).PreviusFormID = ResultsAsociated(0).CurrentFormID
                            ResultsAsociated(0).CurrentFormID = myRule.FormID
                            ShowAsociatedResult(ResultsAsociated(0), r)
                            ResultsAsociated(0).CurrentFormID = ResultsAsociated(0).PreviusFormID

                        End If
                    End If
                Else
                    If ResultsAsociated.Count > 0 Then
                        ResultsAsociated(0).PreviusFormID = ResultsAsociated(0).CurrentFormID
                        ResultsAsociated(0).CurrentFormID = myRule.FormID
                        ShowAsociatedResult(ResultsAsociated(0), r)
                        ResultsAsociated(0).CurrentFormID = ResultsAsociated(0).PreviusFormID
                    End If
                End If
            Next
        Else
            ' El result debe ser uno, ya que el evento AbrirDocumento se ejecuta sólo cuando se hace doble click en una tarea
            If (results.Count = 1) Then

                ' Se guarda el formID guardado en la regla
                results(0).CurrentFormID = myRule.FormID
                ' Se guarda el valor de varDocId, lo que quiere decir que al ejecutar el evento AbrirDocumento se inserta un formulario de tipo
                ' asociado adentro de la correspondiente solapa
                Dim id_VarDocId As Long = WFRuleParent.ObtenerValorVariableObjeto(myRule.varDocId)

                If (VariablesInterReglas.ContainsKey("AsociatedDocIdForPreview") = False) Then
                    VariablesInterReglas.Add("AsociatedDocIdForPreview", id_VarDocId, False)
                Else
                    VariablesInterReglas.Item("AsociatedDocIdForPreview") = id_VarDocId
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Método que sirve para mostrar documentos no asociados al result, más específicamente el formulario que se selecciono en el Administrador
    ''' </summary>
    ''' <param name="results"></param>
    ''' <param name="myRule"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	08/07/2008	Created
    '''     [Gaston]	06/11/2008	Modified     Si el tipo de regla es "AbrirDocumento" el file se coloca como String.Empty
    ''' </history>
    Private Sub showDocuments(ByRef results As System.Collections.Generic.List(Of Core.ITaskResult), ByRef myRule As IDoShowForm)
        For Each r As Core.ITaskResult In results
            r.PreviusFormID = r.CurrentFormID
            r.CurrentFormID = myRule.FormID

            If (myRule.RuleParentType <> TypesofRules.AbrirDocumento AndAlso myRule.RefreshForm = False) Then
                showNormalForm(r, myRule)
                r.CurrentFormID = r.PreviusFormID
            End If

        Next

        If (myRule.RuleParentType = TypesofRules.AbrirDocumento OrElse myRule.RefreshForm = True) Then
            ' El result debe ser uno, ya que el evento AbrirDocumento se ejecuta sólo cuando se hace doble click en una tarea
            If (results.Count = 1) Then
                ' Se guarda el formID guardado en la regla
                results(0).CurrentFormID = myRule.FormID

                ' Si ya se ejecuto un PlayDoShowForm con form. asociado (regla de tipo AbrirDocumento) se elimina la variable de dicha colección 
                ' para poder identificar al form. no asociado
                If (VariablesInterReglas.ContainsKey("AsociatedDocIdForPreview") = True) Then
                    VariablesInterReglas.Remove("AsociatedDocIdForPreview")
                End If
            End If
        End If
    End Sub

    Dim TabControlShowForm As New TabControl()
    Dim TabSecondaryTask As New TabControl()


    'SplitTasks
    Dim SplitTasks As New SplitContainer

    ''' <summary>
    ''' Método utilizado para ver un documento o formulario asociado a un result
    ''' </summary>
    ''' <param name="AsociatedResult"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	08/07/2008	Created
    ''' </history>
    Private Sub ShowAsociatedResult(ByVal AsociatedResult As Result, ByVal originalResult As Result)
        Try
            Dim webForm As ZwebForm = FormBusiness.GetForm(myRule.FormID)

            If Not (IsNothing(webForm)) Then
                Dim formBrowser As New FormBrowser()

                RemoveHandler formBrowser.FormChanged, AddressOf FormChanged
                AddHandler formBrowser.FormChanged, AddressOf FormChanged
                RemoveHandler formBrowser.FormClose, AddressOf CloseForm
                AddHandler formBrowser.FormClose, AddressOf CloseForm
                RemoveHandler formBrowser.CancelChildRules, AddressOf CancelChildRules
                AddHandler formBrowser.CancelChildRules, AddressOf CancelChildRules

                formBrowser.Dock = DockStyle.Fill

                form = New frmDocumentVisualizer(AsociatedResult.Name, AsociatedResult.Name, formBrowser, originalResult, myRule.ViewAsociatedDocs, myRule.ViewOriginal)

                ' Propiedades del formulario
                form.ShowIcon = False
                form.ShowInTaskbar = False
                form.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                form.Text = AsociatedResult.Name
                If myRule.DontShowDialogMaximized = True Then
                    form.WindowState = FormWindowState.Normal
                Else
                    form.WindowState = FormWindowState.Maximized
                End If

                Dim point As New System.Drawing.Point(650, 700)
                form.Size = point

                formBrowser.BringToFront()

                formBrowser.ShowDocument(AsociatedResult, webForm)
                form.ShowDialog()
                form.Dispose()
                formBrowser.Dispose()
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub
    Dim form As frmDocumentVisualizer

    ''' <summary>
    ''' Método utilizado para ver el formulario seleccionado en el Administrador
    ''' </summary>
    ''' <param name="result"></param>
    ''' <param name="myRule"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	21/08/2008	Created
    ''' [Sebastian]     06/05/2009  Modified
    ''' </history>
    Private Sub showNormalForm(ByRef result As TaskResult, ByRef myRule As IDoShowForm)
        Dim webForm As ZwebForm = FormBusiness.GetForm(myRule.FormID)

        If Not (IsNothing(webForm)) Then

            Dim formBrowser As New FormBrowser()

            RemoveHandler formBrowser.FormChanged, AddressOf FormChanged
            AddHandler formBrowser.FormChanged, AddressOf FormChanged
            RemoveHandler formBrowser.FormClose, AddressOf CloseForm
            AddHandler formBrowser.FormClose, AddressOf CloseForm
            RemoveHandler formBrowser.CancelChildRules, AddressOf CancelChildRules
            AddHandler formBrowser.CancelChildRules, AddressOf CancelChildRules
            RemoveHandler formBrowser.CloseWindow, AddressOf CloseForm
            AddHandler formBrowser.CloseWindow, AddressOf CloseForm

            formBrowser.Dock = DockStyle.Fill

            form = New frmDocumentVisualizer(result.Name, "Formulario", formBrowser, result, myRule.ViewAsociatedDocs, myRule.ViewOriginal)

            ' Propiedades del formulario
            RemoveHandler form.FormClosing, AddressOf ClosingForm
            AddHandler form.FormClosing, AddressOf ClosingForm

            form.ShowIcon = False
            form.ShowInTaskbar = False
            form.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            form.Text = result.Name

            If myRule.DontShowDialogMaximized = True Then
                form.WindowState = FormWindowState.Normal
            Else
                form.WindowState = FormWindowState.Maximized
            End If

            Dim point As New System.Drawing.Point(650, 700)
            form.Size = point

            '[Sebastian 02-12-2009] 
            'muestro o no la caja de controles según las preferencias del usuario en la regla
            form.ControlBox = myRule.ControlBox

            formBrowser.CloseFormWindowAfterRuleExecution = myRule.CloseFormWindowAfterRuleExecution
            formBrowser.BringToFront()
            formBrowser.ShowDocument(result, webForm)
            form.BringToFront()
            form.ShowDialog()
            'Comente esta linea porq aparentemente no hace nada y tira error
            'Dim texto As String = formBrowser.Controls(1).Text

            form.Dispose()
            formBrowser.Dispose()
        End If

    End Sub

    Private Sub ClosingForm(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs)
        If Changed = True Then
            If MessageBox.Show("Se han realizado cambios. ¿Desea salir sin guardar el formulario?", "Zamba Cliente", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub CancelChildRules(ByVal cancel As Boolean)
        CancelRules = cancel
    End Sub

    Private Sub FormChanged(ByVal IsChanged As Boolean)
        Changed = IsChanged
    End Sub

    Private Sub CloseForm()
        form.Close()
    End Sub

    Public Sub New(ByVal rule As IDoShowForm)
        myRule = rule
    End Sub
End Class