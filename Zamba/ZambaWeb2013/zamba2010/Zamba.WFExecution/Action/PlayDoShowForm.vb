Imports System.Collections.Generic
Imports Zamba.Core.DocTypes.DocAsociated
Imports Zamba.Core.Enumerators
Imports Zamba.Viewers
Imports Zamba.Core
Imports System.Windows.Forms



Public Class PlayDoShowForm
    Implements IViewerContainer


    Public Event showFormInsideTheTab()
    Private Changed As Boolean
    Private CancelRules As Boolean
    Private _myRule As IDoShowForm

    Sub New(ByVal rule As IDoShowForm)
        Me._myRule = rule
    End Sub ''' <summary>
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
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myRule As IDoShowForm) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return PlayWeb(results, Nothing, myRule)
    End Function

    Public Function PlayWeb(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal Params As Hashtable, ByVal myRule As IDoShowForm) As System.Collections.Generic.List(Of Core.ITaskResult)
        Me._myRule = myRule
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Cargando en el result el ID de formulario que corresponde: " & myRule.FormID.ToString())

        For Each r As ITaskResult In results
            Params.Add("formid", myRule.FormID.ToString())
            Params.Add("openmode", If(myRule.DontShowDialogMaximized, "modal", ""))
            'Params.Add("docid", myRule.)

            r.CurrentFormID = myRule.FormID



        Next

        Return results
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

            Dim DAB As New DocAsociatedBusiness
            Dim UP As New UserPreferences
            ' Se recorren los results
            For Each r As Core.ITaskResult In results
                Dim ResultsAsociated As ArrayList
                ' Se obtienen los documentos asociados
                ResultsAsociated = DAB.getAsociatedResultsFromResult(r, Int32.Parse(UP.getValue("CantidadFilas", UPSections.UserPreferences, 100, Zamba.Membership.MembershipHelper.CurrentUser.ID)), Membership.MembershipHelper.CurrentUser.ID)

                ' Si la variable doc_id no está vacía
                If Not (String.IsNullOrEmpty(myRule.varDocId)) Then
                    ' Se busca el valor que contiene la variable doc_id (id del new result que se creo en el play
                    ' de la regla DOCreateForm) 
                    Dim id_VarDocId As Long = WFRuleParent.ObtenerValorVariableObjeto(myRule.varDocId)

                    If id_VarDocId <> 0 Then
                        ' Se recorren los documentos asociados para buscar el que coincide con el form configurado en
                        ' la regla
                        For Each asociatedResult As Result In ResultsAsociated
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & r.Name)

                            If (asociatedResult.ID = id_VarDocId) Then
                                ' Se obtiene el formID
                                asociatedResult.CurrentFormID = myRule.FormID
                                ShowAsociatedResult(asociatedResult, r)
                                Exit For
                            End If
                        Next
                    Else
                        If ResultsAsociated.Count > 0 Then
                            ResultsAsociated(0).CurrentFormID = myRule.FormID
                            ShowAsociatedResult(ResultsAsociated(0), r)
                        End If
                    End If
                Else
                    If ResultsAsociated.Count > 0 Then
                        ResultsAsociated(0).CurrentFormID = myRule.FormID
                        ShowAsociatedResult(ResultsAsociated(0), r)
                    End If
                End If
            Next
            DAB = Nothing
        Else
            ' El result debe ser uno, ya que el evento AbrirDocumento se ejecuta sólo cuando se hace doble click en una tarea
            If (results.Count = 1) Then

                ' Se guarda el formID guardado en la regla
                results(0).CurrentFormID = myRule.FormID
                ' Se guarda el valor de varDocId, lo que quiere decir que al ejecutar el evento AbrirDocumento se inserta un formulario de tipo
                ' asociado adentro de la correspondiente solapa
                Dim id_VarDocId As Long = WFRuleParent.ObtenerValorVariableObjeto(myRule.varDocId)

                If (VariablesInterReglas.ContainsKey("AsociatedDocIdForPreview") = False) Then
                    VariablesInterReglas.Add("AsociatedDocIdForPreview", id_VarDocId)
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
            ' Se traen todos los formsID de ese doctype y se itera por cada uno de ellos
            Dim dsFormsID As DataSet = DocAsociatedBusiness.getAsociatedFormsId(r.DocType.ID)

            'If (dsFormsID.Tables(0).Rows.Count > 0) Then
            '    For Each row As DataRow In dsFormsID.Tables(0).Rows
            '        If (row.Item(0) = myRule.FormID) Then
            r.CurrentFormID = myRule.FormID
            ' ShowAsociatedResult(r)
            'Exit For
            '        End If
            'Next

            If (myRule.RuleParentType <> TypesofRules.AbrirDocumento) Then
                showNormalForm(r, myRule)
            End If
            'End If
        Next

        If (myRule.RuleParentType = TypesofRules.AbrirDocumento) Then
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
        Dim FB As New FormBusiness
        Try
            ' Propiedades del formulario
            form.ShowIcon = False
            form.ShowInTaskbar = False
            form.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            form.Text = AsociatedResult.Name
            If _myRule.DontShowDialogMaximized = True Then
                form.WindowState = FormWindowState.Normal
            Else
                form.WindowState = FormWindowState.Maximized
            End If

            Dim point As New System.Drawing.Point(650, 700)
            form.Size = point

            Dim webForm As ZwebForm = FB.GetForm(_myRule.FormID)

            If Not (IsNothing(webForm)) Then
                'Dim formBrowser As New FormBrowser()

                'RemoveHandler formBrowser.FormChanged, AddressOf FormChanged
                'AddHandler formBrowser.FormChanged, AddressOf FormChanged
                'RemoveHandler formBrowser.FormClose, AddressOf CloseForm
                'AddHandler formBrowser.FormClose, AddressOf CloseForm
                'RemoveHandler formBrowser.CancelChildRules, AddressOf CancelChildRules
                'AddHandler formBrowser.CancelChildRules, AddressOf CancelChildRules

                'formBrowser.Dock =DockStyle.Fill
                '[Sebastian 02-12-2009] Valido si tiene o no habilitado del check de ver el doc original
                If _myRule.ViewOriginal = True Then

                    SplitTasks.Dock = System.Windows.Forms.DockStyle.Fill

                    'SplitTasks.Panel1
                    SplitTasks.Panel1.Controls.Add(TabControlShowForm)
                    SplitTasks.Panel1MinSize = 1

                    'SplitTasks.Panel2
                    SplitTasks.Panel2.Controls.Add(TabSecondaryTask)
                    SplitTasks.Panel2Collapsed = True
                    SplitTasks.Panel2MinSize = 0
                    'SplitTasks.Size = New System.Drawing.Size(735, 392)
                    'SplitTasks.SplitterDistance = 245
                    '
                    'TabSecondaryTask
                    '
                    TabSecondaryTask.Dock = System.Windows.Forms.DockStyle.Fill
                    TabSecondaryTask.HotTrack = True
                    TabSecondaryTask.Name = "TabSecondaryTask"
                    TabSecondaryTask.SelectedIndex = 0


                    Dim TPageForm As New TabPage("Formulario")
                    'Dim TPageDoc As TabPage = New UCDocumentViewer2(Me, originalResult, True, False, Nothing, False, True, False)

                    TabControlShowForm.Name = "tabShowForm"

                    '[sebastian 06-05-09] se agrego el refresh porque no se mostraba bien el formulario
                    'hasta que se refrescaba el mismo con "F5"
                    'formBrowser.RefreshData() [Emiliano] comente esta lina porque me borraba los el contenido de los indices
                    'TPageForm.Controls.Add(formBrowser)
                    TabControlShowForm.TabPages.Add(TPageForm)
                    'TabControlShowForm.TabPages.Add(TPageDoc)
                    TabControlShowForm.Dock = DockStyle.Fill
                    form.Controls.Add(TabControlShowForm)
                Else
                    'form.Controls.Add(formBrowser)
                End If
                'formBrowser.BringToFront()

                'formBrowser.ShowDocument(AsociatedResult, webForm)
                form.ShowDialog()
                form.Dispose()
                'formBrowser.Dispose()
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            FB = Nothing
        End Try

    End Sub
    Dim form As New System.Windows.Forms.Form



#Region "Split"

    Public Sub Split(ByVal Viewer As System.Windows.Forms.TabPage, ByVal Splited As Boolean) Implements Core.IViewerContainer.Split
        Try
            If Not Splited Then
                TabSecondaryTask.TabPages.Remove(Viewer)
                TabControlShowForm.TabPages.Add(Viewer)
                Me.TabControlShowForm.SelectedTab = Viewer

                If Me.TabSecondaryTask.TabPages.Count = 0 Then
                    SplitTasks.Panel2Collapsed = True
                    Me.SplitTasks.SplitterDistance = Me.SplitTasks.Width
                End If
            Else
                TabControlShowForm.TabPages.Remove(Viewer)
                TabSecondaryTask.TabPages.Add(Viewer)

                If SplitTasks.Panel2Collapsed = True Then
                    SplitTasks.Panel2Collapsed = False
                    Me.SplitTasks.SplitterDistance = Me.SplitTasks.Panel1.Width / 2
                End If
            End If

        Catch ex As Exception
            ZCore.raiseerror(ex)
        End Try
    End Sub

#End Region
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
        Dim FB As New FormBusiness
        Dim webForm As ZwebForm = FB.GetForm(myRule.FormID)
        FB = Nothing

        If Not (IsNothing(webForm)) Then

            '' Dim formBrowser As New FormBrowser()

            'RemoveHandler formBrowser.FormChanged, AddressOf FormChanged
            'AddHandler formBrowser.FormChanged, AddressOf FormChanged
            'RemoveHandler formBrowser.FormClose, AddressOf CloseForm
            'AddHandler formBrowser.FormClose, AddressOf CloseForm
            'RemoveHandler formBrowser.CancelChildRules, AddressOf CancelChildRules
            'AddHandler formBrowser.CancelChildRules, AddressOf CancelChildRules
            'RemoveHandler formBrowser.CloseWindow, AddressOf CloseForm
            'AddHandler formBrowser.CloseWindow, AddressOf CloseForm

            'formBrowser.Dock =DockStyle.Fill

            '[Sebastian 02-12-2009] Valido si tiene o no habilitado del check de ver el doc original
            If myRule.ViewOriginal = True Then
                Dim TabControlShowForm As New TabControl()
                Dim TPageForm As New TabPage("Formulario")
                ' Dim TPageDoc As TabPage = New UCDocumentViewer2(Me, result, True, False, Nothing, False, True, False)

                TabControlShowForm.Name = "tabShowForm"

                '[sebastian 06-05-09] se agrego el refresh porque no se mostraba bien el formulario
                'hasta que se refrescaba el mismo con "F5"
                'formBrowser.RefreshData() [Emiliano] comente esta lina porque me borraba los el contenido de los indices
                'TPageForm.Controls.Add(formBrowser)
                TabControlShowForm.TabPages.Add(TPageForm)
                'TabControlShowForm.TabPages.Add(TPageDoc)
                TabControlShowForm.Dock = DockStyle.Fill
                form.Controls.Add(TabControlShowForm)
            Else
                'form.Controls.Add(formBrowser)
            End If

            '[Sebastian 02-12-2009] 
            'muestro o no la caja de controles según las preferencias del usuario en la regla
            form.ControlBox = myRule.ControlBox

            'formBrowser.CloseFormWindowAfterRuleExecution = myRule.CloseFormWindowAfterRuleExecution
            'formBrowser.BringToFront()
            'formBrowser.ShowDocument(result, webForm)
            form.BringToFront()
            form.ShowDialog()
            'Comente esta linea porq aparentemente no hace nada y tira error
            'Dim texto As String = formBrowser.Controls(1).Text

            form.Dispose()
            'formBrowser.Dispose()
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
        Me.form.Close()
    End Sub

End Class