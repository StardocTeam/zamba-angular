Imports Zamba.Core
'Imports Zamba.Controls

Public Class frmAbmSections

    Dim flagEditModeON As Boolean = False
    Dim flagSelectedModeON As Boolean = False
    Dim flagDirtyModeON As Boolean = False

    Private Const COL_IDSECCION As String = "IdSeccion"
    Private Const COL_NOMBRE As String = "nombre"
    Dim editedSectionID As Int32
    Private Const CONS_HELP_MSJ As String = "Permite Agregar, Modificar y eliminar secciones, las mismas son utilizadas para ubicar índices en formularios dinámicos"
    Private Const MSG_TEXT_ERROR_SAVE As String = "Se ha producido un error al agregar la sección {0}"
    Private Const MSG_TITLE_ERROR_SAVE As String = "Error al guardar"
    Private Const MSG_TEXT_SAVE As String = "Se han guardado todas las secciones."
    Private Const MSG_TITLE_SAVE As String = "Guardar secciones"
    Private Const MSG_TEXT_SAVE_QUESTION As String = "¿Desea guardar los cambios?"
    Private Const MSG_TEXT_CANCEL As String = "¿Desea salir?"
    Private Const MSG_TITLE_CANCEL As String = "Salir"
    Private Const MSG_TEXT_ANULARCAMBIOS As String = "¿Desea anular los cambios?"
    Private Const MSG_TITLE_ANULARCAMBIOS As String = "Cancelar"
    Private Const MSG_TEXT_ERROR_ACEPTAR As String = "Se han producido algunos errores al guardar"
    Private Const MSG_ATENCION_TITLE As String = "Atención"
    Private Const MSG_REMOVE_SECCION_EXISTENTE As String = "No se puede eliminar una sección que ha sido previamente guardada."
    Private Const MSG_TEXT_REMOVE As String = "¿Desea eleminar el registro?"
    Private Const MSG_TITLE_REMOVE As String = "Eliminar Registro"

    Public Event SectionEdited(ByVal sectionId As Int64, ByVal SectionName As String)

    Private Sub LoadData()
        Try
            Dim ds As DataSet = FormBusiness.GetAllDynamicFormSections()

            dgrvSectionList.DataSource = ds.Tables(0)
            dgrvSectionList.Columns(COL_NOMBRE).HeaderText = "Nombre"
            dgrvSectionList.Columns(COL_IDSECCION).HeaderText = "ID Sección"
            dgrvSectionList.Columns(COL_IDSECCION).Visible = False
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub frmAbmSections_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadData()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            ' Valida si el nombre es correcto
            If IsValidSectionName(txtSection.Text) Then

                Dim dt As DataTable
                dt = dgrvSectionList.DataSource

                Dim row As DataRow
                row = dt.NewRow()

                row(COL_NOMBRE) = txtSection.Text.Trim()
                row(COL_IDSECCION) = -1

                dt.Rows.Add(row)

                dgrvSectionList.DataSource = dt

                flagDirtyModeON = True

                txtSection.Clear()
                txtSection.Focus()

            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        If dgrvSectionList.SelectedRows.Count = 1 Then
            Try
                If MessageBox.Show(MSG_TEXT_REMOVE, MSG_TEXT_REMOVE, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Dim row As DataGridViewRow
                    row = dgrvSectionList.SelectedRows(0)

                    Dim valor As Int64
                    valor = Int64.Parse(row.Cells(COL_IDSECCION).Value.ToString())
                    'No se elimina la seccion de la base de datos
                    'solo de la grilla
                    If valor = -1 Then
                        dgrvSectionList.Rows.Remove(row)
                    Else
                        MessageBox.Show(MSG_REMOVE_SECCION_EXISTENTE, MSG_ATENCION_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                    'FormBusiness.DeleteDynamicFormSection(dgrvSectionList.SelectedRows(0).Cells(COL_IDSECCION).Value)
                End If
            Catch ex As Exception
                MessageBox.Show("No se pudo eliminar la sección, se encuentre en uso en otro formulario", "Zamba")
                ZClass.raiseerror(ex)
            End Try
        Else
            MessageBox.Show("Debe seleccionar una sección", "Zamba")
        End If
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click

        If flagEditModeON = False Then
            If dgrvSectionList.SelectedRows.Count <> 0 Then
                txtSection.Text = dgrvSectionList.SelectedRows(0).Cells(COL_NOMBRE).Value.ToString.Trim
                editedSectionID = dgrvSectionList.SelectedRows(0).Cells(COL_IDSECCION).Value
                EnableControls(False)
                flagEditModeON = True
            End If
        Else

        End If

    End Sub

    Private Sub btnAcept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAcept.Click

        Dim rows As DataGridViewRowCollection
        rows = dgrvSectionList.Rows

        Dim nombre As String = String.Empty
        Dim lastid As Int64
        Dim isError As Boolean = False

        If flagEditModeON Then
            If IsValidSectionName(txtSection.Text) Then
                dgrvSectionList.SelectedRows(0).Cells(COL_NOMBRE).Value = txtSection.Text
                If editedSectionID <> -1 Then
                    FormBusiness.UpdateDynamicFormSection(editedSectionID, txtSection.Text)
                Else
                    flagDirtyModeON = True
                End If
                RaiseEvent SectionEdited(editedSectionID, txtSection.Text)
                EnableControls(True)
                flagEditModeON = False
            End If
        Else
            If MessageBox.Show(MSG_TEXT_SAVE_QUESTION, MSG_TITLE_SAVE, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                For Each row As DataGridViewRow In rows
                    Try
                        lastid = Int64.Parse(row.Cells(COL_IDSECCION).Value.ToString())
                        If lastid = -1 Then
                            lastid = ToolsBusiness.GetNewID(Zamba.Core.IdTypes.DynamicFormsSection)
                            nombre = row.Cells(COL_NOMBRE).Value.ToString().Trim()
                            FormBusiness.InsertDynamicFormSection(lastid, nombre)
                        End If
                    Catch ex As Exception
                        MessageBox.Show(String.Format(MSG_TEXT_ERROR_SAVE, nombre), MSG_TITLE_ERROR_SAVE, _
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        ZClass.raiseerror(ex)
                        isError = True
                    End Try
                Next
                If isError Then
                    MessageBox.Show(MSG_TEXT_ERROR_ACEPTAR, MSG_ATENCION_TITLE, _
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Else
                    MessageBox.Show(MSG_TEXT_SAVE, MSG_TITLE_SAVE, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                    Me.Close()
                End If
            End If
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If flagEditModeON Then
            If MessageBox.Show(MSG_TEXT_ANULARCAMBIOS, MSG_TITLE_ANULARCAMBIOS, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                EnableControls(True)
                flagEditModeON = False
            End If
        Else
            If MessageBox.Show(MSG_TEXT_CANCEL, MSG_TITLE_CANCEL, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                Me.Close()
            End If
        End If
    End Sub

#Region "Funciones propias del formulario"

    ''' <summary>
    ''' Comprueba que no exista dos veces la misma sección
    ''' </summary>
    ''' <param name="sectionName">Nombre de la sección</param>
    ''' <returns>True si la sección existe</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Tomas 13/03/09]   Created
    ''' </history>
    Private Function DoesSectionExists(ByVal sectionName As String) As Boolean

        ' Recorro las secciones que ya se encuentran en el combo
        For Each dgvr As DataGridViewRow In dgrvSectionList.Rows
            If sectionName.ToLower() = dgvr.Cells(COL_NOMBRE).Value.ToString.ToLower.Trim Then
                Return True
            End If
        Next
        Return False

    End Function

    ''' <summary>
    ''' Habilita o deshabilita los botones de Agregar, Aceptar y Remover
    ''' </summary>
    ''' <param name="enable">True para habilitar los botones</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Tomas 13/03/09] Created
    ''' </history>
    Private Sub EnableControls(ByVal enable As Boolean)

        If enable Then
            btnAdd.Enabled = True
            If flagSelectedModeON Then
                btnRemove.Enabled = True
                btnEdit.Enabled = True
            End If
            dgrvSectionList.Enabled = True
        Else
            btnAdd.Enabled = False
            btnRemove.Enabled = False
            btnEdit.Enabled = False
            dgrvSectionList.Enabled = False
        End If

    End Sub

    ''' <summary>
    ''' Valida si el nombre de la sección es correcto
    ''' </summary>
    ''' <param name="sectionName">Nombre de la sección</param>
    ''' <returns>True si el nombre de la seción es válido</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Tomas 13/03/09]   Created
    ''' </history>
    Private Function IsValidSectionName(ByVal sectionName As String) As Boolean

        If String.IsNullOrEmpty(sectionName) Then
            MessageBox.Show("El nombre de la sección no puede estar vacio." & vbCrLf & _
                            "Complete el nombre y vuelva a intentar.", "Error de ingreso", _
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False

        End If

        If DoesSectionExists(sectionName) Then
            MessageBox.Show("El nombre de la sección ya existe y no puede repetirse." & vbCrLf & _
                "Cambie el nombre y vuelva a intentar.", "Error de ingreso", _
                MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False

        End If

        Return True

    End Function

#End Region


    Private Sub frmAbmSections_HelpButtonClicked(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.HelpButtonClicked
        Help.ShowPopup(btnAdd, CONS_HELP_MSJ, New Point(Me.Location.X + btnAdd.Location.X, Me.Location.Y + btnAdd.Location.Y))
    End Sub

   
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub dgrvSectionList_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dgrvSectionList.CellFormatting
        Try
            Dim row As DataGridViewRow
            row = dgrvSectionList.Rows(e.RowIndex)
            Dim id As Int64
            id = Int64.Parse(row.Cells(COL_IDSECCION).Value.ToString())
            If id = -1 Then
                row.DefaultCellStyle.BackColor = Color.LightBlue
            End If
        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Sub

    Private Sub dgrvSectionList_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgrvSectionList.SelectionChanged
        flagSelectedModeON = True
    End Sub
End Class