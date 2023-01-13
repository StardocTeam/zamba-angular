Public Class frmGeneralRules
    Inherits Zamba.AppBlock.ZForm

    Private Delegate Sub ChangeCursorDelegate(ByVal cur As Cursor)
    Private Delegate Sub DFinishRulesAsync(ByVal dt As DataTable)
    Private Event loadRule(ByVal intRuleId As Int64, ByVal intStepId As Int64)
    Private selectedRuleId As Int64
    Private ImgList As ImageList

    Public Sub New(Optional ByVal selectedRuleId As Int64 = 0)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Try
            'Carga los datos
            FillRules()
            FillPlaces()
            GenerateHelp()
            If txtOrder.Text.Length = 0 Then
                txtOrder.Text = "0"
            End If
            selectedRuleId = selectedRuleId
            SetImageList()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnOK.Click
        DialogResult = DialogResult.OK
        Close()
    End Sub

    ''' <summary>
    ''' Completa las reglas en el combo
    ''' </summary>
    ''' <remarks></remarks>
    ''' <summary>
    ''' Obtiene las reglas para mostrarlas en el combo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillRules()
        Dim dt As DataTable = WFRulesBusiness.GetDoExecuteRules().Tables(0)
        cboRules.DataSource = dt
        cboRules.DisplayMember = dt.Columns(0).ColumnName '  "NAME"
        cboRules.ValueMember = dt.Columns(1).ColumnName    '"ID"

        If selectedRuleId <> 0 Then
            cboRules.SelectedValue = selectedRuleId
        End If
    End Sub

    ''' <summary>
    ''' Completa las ubicaciones posibles del boton
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillPlaces()
        cboPlace.DataSource = GenericButtonBusiness.GetPlaces()
        cboPlace.ValueMember = "PLACEID"
        cboPlace.DisplayMember = "PLACEDESC"
        cboPlace.SelectedIndex = 0

        RemoveHandler cboPlace.SelectedIndexChanged, AddressOf cboPlace_SelectedIndexChanged
        AddHandler cboPlace.SelectedIndexChanged, AddressOf cboPlace_SelectedIndexChanged
    End Sub

    ''' <summary>
    ''' Genera un tooltip asociado al control que determina el orden de los botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GenerateHelp()
        Try
            Dim help As New ToolTip()
            help.Show("El orden se determina con un valor numérico que comienza por el cero," & vbCrLf & _
                      "donde el cero sería el primer item del lado izquierdo dentro de un control.", Me)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Abre la regla seleccionada en el combo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnGoToRule_Click(ByVal sender As System.Object, ByVal e As EventArgs)
        If Not IsNothing(cboRules.SelectedValue) Then
            Try
                Invoke(New ChangeCursorDelegate(AddressOf ChangeCursor), New Object() {Cursors.WaitCursor})
                Dim ruleId As Int64 = Int64.Parse(cboRules.SelectedValue)
                '
                'TODO: NO ESTA FUNCIONANDO. UNA VEZ CORREGIDO HABILITAR EL BOTON DESDE "Private Sub FinishRulesAsync(ByVal dt As DataTable)"
                '
                'RaiseEvent loadRule(ruleId, WFRulesBusiness.GetRuleStepId(ruleId))
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                Invoke(New ChangeCursorDelegate(AddressOf ChangeCursor), New Object() {Cursors.Default})
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Cambia el cursor actual a uno determinado
    ''' </summary>
    ''' <param name="cur"></param>
    ''' <remarks></remarks>
    Private Sub ChangeCursor(ByVal cur As Cursor)
        Cursor = cur
    End Sub

    ''' <summary>
    ''' Verifica que solo se pueda ingresar valores numéricos y la tecla borrar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtOrder_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtOrder.KeyPress
        If Not (Char.IsDigit(e.KeyChar) OrElse Char.GetUnicodeCategory(e.KeyChar) = 14) Then
            e.Handled = True
        End If
    End Sub

    ''' <summary>
    ''' Controla la habilitación del check "Pertenece al workflow"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboPlace_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs)
        Dim place As ButtonPlace = DirectCast(CInt(cboPlace.SelectedValue), ButtonPlace)

        If place = ButtonPlace.ArbolTareas OrElse place = ButtonPlace.BarraTareas OrElse place = ButtonPlace.DocumentToolbar_tasks Then
            chkForOneWf.Enabled = True
        Else
            chkForOneWf.Enabled = False
            chkForOneWf.Checked = False
        End If

        If cboPlace.SelectedValue = ButtonPlace.WebHome OrElse cboPlace.SelectedValue = ButtonPlace.WebHeader Then
            txtCssClass.Enabled = True
            txtGroupClass.Enabled = True

            cmbSelectIcon.Enabled = False
        Else
            txtCssClass.Enabled = False
            txtGroupClass.Enabled = False

            cmbSelectIcon.Enabled = True
        End If
    End Sub

    Private Sub cmbSelectIcon_DrawItem(sender As Object, e As System.Windows.Forms.DrawItemEventArgs) Handles cmbSelectIcon.DrawItem
        'Carga los iconos del imageList al ComboBox.
        If e.Index <> -1 Then
            e.Graphics.DrawImage(ImgList.Images(e.Index) _
             , e.Bounds.Left, e.Bounds.Top)
        End If
    End Sub

    Private Sub cmbSelectIcon_MeasureItem(sender As Object, e As System.Windows.Forms.MeasureItemEventArgs) Handles cmbSelectIcon.MeasureItem
        'Se setea el tamaño de las imagenes en el combobox.
        e.ItemHeight = ImgList.ImageSize.Height
        e.ItemWidth = ImgList.ImageSize.Width
    End Sub

    ''' <summary>
    ''' Carga las imágenes del ImageList en el combo de selección de íconos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetImageList()
        Try
            'Seteo el ImageList que contendrá los distintos iconos disponibles para cada regla general.
            ImgList = New ImageList()

            ImgList.Images.Add(My.Resources.gear)
            ImgList.Images.Add(My.Resources.mail)

            'Se carga en un arraay las imagenes contenidas en el imagelist.
            Dim items(ImgList.Images.Count - 1) As String
            For i As Int32 = 0 To ImgList.Images.Count - 1
                items(i) = "Item " & i.ToString
            Next

            'Se cargan las imagenes del array en el comboBox. Se setean propiedades del combo para
            'poder mostrar las mismas.
            cmbSelectIcon.Items.AddRange(items)
            cmbSelectIcon.DropDownStyle = ComboBoxStyle.DropDownList
            cmbSelectIcon.DrawMode = DrawMode.OwnerDrawVariable
            cmbSelectIcon.ItemHeight = ImgList.ImageSize.Height
            cmbSelectIcon.Width = ImgList.ImageSize.Width + 25
            cmbSelectIcon.MaxDropDownItems = ImgList.Images.Count
            cmbSelectIcon.SelectedIndex = 0
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
End Class