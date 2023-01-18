Imports Zamba.Core

Public Class FrmEntityData

    Private lstTables As New List(Of String)
    Private selectedGeneration As Int32
    Private dataGen As New DataGeneratorBusiness(Application.StartupPath)
    Private dgvAttributes As DataGridView
    Private dtAttributes As DataTable = Nothing
    Private lstAttributes As New List(Of Int64)

    ''' <summary>
    ''' Carga de atributos iniciales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmEntityData_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
        Try
            'Se obtienen todos los atributos de Zamba
            dtAttributes = dataGen.GetAttributes()

            'Se carga la grilla
            dgvAttributes = New DataGridView()
            With dgvAttributes
                .DataSource = dtAttributes
                .RowHeadersVisible = False
                .AllowDrop = False
                .AllowUserToAddRows = False
                .AllowUserToDeleteRows = False
                .AllowUserToOrderColumns = False
                .AllowUserToResizeRows = False
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                'Mejora la experiencia de edición, evitando la necesidad de hacer multiples click para editar un combo o filtro
                .EditMode = DataGridViewEditMode.EditOnEnter
                .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
                .Top = pnlSearch.Height
                .Width = pnlSearch.Width
                .Height = pnlAttributes.Height - pnlSearch.Height
                .Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)

                pnlAttributes.Controls.Add(dgvAttributes)

                'Se carga correctamente la columna categoría
                Dim dcColCategory As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn()
                dcColCategory.Name = "category"
                dcColCategory.DataPropertyName = "category"
                dcColCategory.DataSource = dataGen.GetDataCategories()
                .Columns.RemoveAt(2)
                .Columns.Insert(2, dcColCategory)

                'Se modifican los nombres de las columnas
                .Columns(0).HeaderText = "ID"
                .Columns(1).HeaderText = "Atributo a modificar"
                .Columns(2).HeaderText = "Seleccione una categoría"
                .Columns(3).HeaderText = "Valores a conservar"
                .Columns(0).ToolTipText = "Id del atributo"
                .Columns(1).ToolTipText = "Es el atributo al que su contenido será modificado por datos genéricos"
                .Columns(2).ToolTipText = "Seleccione la categoría o tipo de dato al que corresponde"
                .Columns(3).ToolTipText = "Filtra valores para no modificarlos. Se puede filtrar mas de un valor mediante punto y coma. Ejemplo: 0;2;101"
                .Columns(0).ReadOnly = True
                .Columns(1).ReadOnly = True

                'Se agrega la grilla al formulario
                .AutoResizeColumns(DataGridViewAutoSizeColumnMode.ColumnHeader)
            End With
        Catch ex As Exception
            ZClass.raiseerror(ex)
            MessageBox.Show("Ha ocurrido un error al cargar la información de los atributos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    ''' <summary>
    ''' Carga todas las tablas consumidas por los atributos seleccionados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnLoadTables_Click(sender As System.Object, e As EventArgs) Handles btnLoadTables.Click
        Try
            'Guarda en una lista los atributos a modificar
            Dim dbNullType As Type = GetType(DBNull)
            lstAttributes.Clear()
            For i As Int32 = 0 To dgvAttributes.Rows.Count - 1
                Dim categoryValue As Object = dgvAttributes.Rows(i).Cells("category").Value
                If categoryValue.GetType IsNot dbNullType AndAlso Not String.IsNullOrEmpty(categoryValue) Then
                    lstAttributes.Add(dgvAttributes.Rows(i).Cells("Index_Id").Value)
                End If
            Next

            'Verifica si existe alguno seleccionado
            If lstAttributes.Count > 0 Then
                Dim tables() As Object = dataGen.GetTablesNames(lstAttributes)
                clbTables.Items.Clear()

                'Verifica haber encontrado tablas
                If tables IsNot Nothing AndAlso tables.Length > 0 Then
                    clbTables.Items.AddRange(tables)
                    btnPreview100Records.Enabled = True
                    btnPreviewAllRecords.Enabled = True
                    clbTables.Enabled = True
                    pgbData.Enabled = True
                Else
                    btnPreview100Records.Enabled = False
                    btnPreviewAllRecords.Enabled = False
                    btnStart.Enabled = False
                    clbTables.Enabled = False
                    pgbData.Enabled = False
                    MessageBox.Show("No se encontraron entidades que contengan los atributos seleccionados", _
                                    String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
            MessageBox.Show("Error al cargar las tablas", String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Genera una previsualización de los datos de la tabla ingresada
    ''' </summary>
    ''' <param name="filterTop100">True para mostrar los primeros 100 registros</param>
    ''' <remarks></remarks>
    Private Sub PreviewTable(filterTop100 As Boolean)
        'Verifica que al menos una tabla se encuentre seleccioanda
        If clbTables.SelectedIndex > -1 Then
            'Se expande el form para visualizar la grilla
            SplitContainer1.Panel2Collapsed = False

            Try
                'Consulta de previsualización
                Dim dt As DataTable = dataGen.GetTablePreview(clbTables.Items(clbTables.SelectedIndex), filterTop100, lstAttributes)
                Dim dgvPreview As Grid.Grid.TelerikGrid = New Grid.Grid.TelerikGrid(dt, False)

                'Diseño de la grilla
                dgvPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
                dgvPreview.Dock = System.Windows.Forms.DockStyle.Fill
                dgvPreview.Location = New System.Drawing.Point(2, 151)
                dgvPreview.Size = New System.Drawing.Size(679, 164)
                SplitContainer1.Panel2.Controls.Clear()
                SplitContainer1.Panel2.Controls.Add(dgvPreview)
            Catch ex As Exception
                ZClass.raiseerror(ex)
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
    Private Sub btnPreviewAllRecords_Click(sender As System.Object, e As EventArgs) Handles btnPreviewAllRecords.Click
        PreviewTable(False)
    End Sub
    Private Sub btnPreviewTable_Click(sender As System.Object, e As EventArgs) Handles btnPreview100Records.Click
        PreviewTable(True)
    End Sub

    ''' <summary>
    ''' Genera datos aleatorios a partir de un archivo de diccionario y actualiza los registros de una tabla especificada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnEdit_Click(sender As System.Object, e As EventArgs) Handles btnStart.Click
        'Se guardan los cambios realizados en la configuracion de los atributos en un XML
        dataGen.SaveChanges(dtAttributes)

        Try
            EditFormControlsVisibility(False)

            'Valores por defecto
            pgbData.Maximum = 100
            pgbData.Minimum = 0
            pgbData.Value = 0

            'Se obtiene el objeto que contiene toda la informacion sobre los datos a generar
            Dim dataHelper As List(Of DataGeneratorHelper) = GetDataGeneratorHelper()

            'Se ejecuta el hilo generador de datos
            bgwDataGeneration.WorkerReportsProgress = True
            bgwDataGeneration.WorkerSupportsCancellation = True
            bgwDataGeneration.RunWorkerAsync(dataHelper)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            MessageBox.Show("Error al inicializar la generación de datos", String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error)
            EditFormControlsVisibility(True)
        End Try
    End Sub

    ''' <summary>
    ''' Genera un objeto con toda la información necesaria para comenzar con la modificacion de datos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetDataGeneratorHelper() As List(Of DataGeneratorHelper)
        Dim dataHelper As New List(Of DataGeneratorHelper)
        Dim columnId As String
        Dim categoryIndex As Int32 = dgvAttributes.Columns("category").Index
        Dim filterIndex As Int32 = dgvAttributes.Columns("filter").Index
        Dim attributeIdIndex As Int32 = dgvAttributes.Columns("Index_Id").Index
        Dim category As DataGeneratorCategory
        Dim filter As String = String.Empty
        Dim tempId As Int64

        'Se recorren las tablas a impactar
        For Each table As String In clbTables.CheckedItems
            table = table.ToUpper()
            Dim attributes As New List(Of DataGeneratorHelper.ColumnHelper)
            Dim filters As New List(Of String)

            'Verifica si la tabla es una entidad 
            If table.StartsWith("D") Then
                columnId = "DOC_ID"

                'Se obtiene el ID de la entidad
                tempId = CLng(table.Remove(0, 5))

                'Se obtienen los atributos de la entidad
                Dim entityAttributes As List(Of IIndex) = IndexsBusiness.GetIndexsSchemaAsListOfDT(tempId, True)
                Dim index As IIndex

                'Se guardan unicamente los atributos que no sean slst ni ilst, 
                'ya que estos son procesados en sus correspondientes tablas
                For i As Int32 = 0 To entityAttributes.Count - 1
                    index = entityAttributes(i)
                    If index.DropDown = IndexAdditionalType.LineText AndAlso lstAttributes.Contains(index.ID) Then
                        'Se busca la información configurada del atributo
                        For j As Int32 = 0 To dgvAttributes.Rows.Count - 1
                            If dgvAttributes(attributeIdIndex, j).Value = index.ID Then
                                category = dataGen.GetCategoryEnumerator(dgvAttributes(categoryIndex, j).Value)
                                filter = dgvAttributes(filterIndex, j).Value.ToString
                                Exit For
                            End If
                        Next

                        'Se agrega el listado de filtros
                        filters.Clear()
                        filters.AddRange(filter.Split(New Char() {";"}, StringSplitOptions.RemoveEmptyEntries))

                        attributes.Add(New DataGeneratorHelper.ColumnHelper("I" & index.ID.ToString, category, filters))
                    End If
                Next
            Else
                'Se obtiene la columna primary key
                If table.StartsWith("S") Then
                    columnId = "CODIGO"
                Else
                    columnId = "ITEMID"
                End If

                'Se obtiene el ID del atributo
                tempId = CLng(table.Remove(0, 6))

                'Se busca la información del atributo
                For i As Int32 = 0 To dgvAttributes.Rows.Count - 1
                    If dgvAttributes(attributeIdIndex, i).Value = tempId Then
                        category = dataGen.GetCategoryEnumerator(dgvAttributes(categoryIndex, i).Value)
                        filter = dgvAttributes(filterIndex, i).Value.ToString
                        Exit For
                    End If
                Next

                'Se agrega el listado de filtros
                filters.AddRange(filter.Split(New Char() {";"}, StringSplitOptions.RemoveEmptyEntries))

                'Se define la configuración de generación de datos para el atributo
                If table.StartsWith("S") Then
                    attributes.Add(New DataGeneratorHelper.ColumnHelper("DESCRIPCION", category, filters))
                Else
                    attributes.Add(New DataGeneratorHelper.ColumnHelper("DESCRIPTION", category, filters))
                End If
            End If

            'Se agrega la información de generación de datos por tabla
            dataHelper.Add(New DataGeneratorHelper(table, columnId, attributes))
        Next

        Return dataHelper
    End Function

    ''' <summary>
    ''' Hilo encargado de generar los datos y actualizar la tabla deseada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bgwDataGeneration_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwDataGeneration.DoWork
        Try
            dataGen.GenerateEntityData(e.Argument, bgwDataGeneration)
        Catch ex As ZambaEx
            MessageBox.Show(ex.Message, String.Empty, MessageBoxButtons.OK, ex.Icon)
            If ex.Icon = MessageBoxIcon.Error Then ZClass.raiseerror(ex)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Evento encargado de mostrar el progreso del procesamiento mediante el aumento de la barra de progreso
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bgwDataGeneration_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bgwDataGeneration.ProgressChanged
        pgbData.Value = e.ProgressPercentage

        If Not String.IsNullOrEmpty(e.UserState) Then
            lblCurrentTable.Text = e.UserState
        End If
    End Sub

    ''' <summary>
    ''' Se encarga de reestablecer el estado original de los controles al finalizar el procesamiento de los datos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bgwDataGeneration_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwDataGeneration.RunWorkerCompleted
        EditFormControlsVisibility(True)
        pgbData.Value = 0
    End Sub

    ''' <summary>
    ''' Habilita o deshabilita los controles del form
    ''' </summary>
    ''' <param name="enable"></param>
    ''' <remarks></remarks>
    Private Sub EditFormControlsVisibility(enable As Boolean)
        SplitContainer1.Panel2.Enabled = enable
        SplitContainer2.Panel1.Enabled = enable
        clbTables.Enabled = enable
        btnLoadTables.Enabled = enable
        btnPreview100Records.Enabled = enable
        btnPreviewAllRecords.Enabled = enable
        btnStart.Enabled = enable
        btnStop.Enabled = Not enable
        lblCurrentTable.Text = String.Empty
    End Sub

    ''' <summary>
    ''' Realiza una busqueda sobre la lista de atributos hacia abajo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSearchDown_Click(sender As System.Object, e As EventArgs) Handles btnSearchDown.Click
        If dgvAttributes.Rows.Count > 0 Then
            If dgvAttributes.SelectedRows.Count = 0 Then
                dgvAttributes.CurrentCell = dgvAttributes.Rows(0).Cells(0)
            End If
            SearchDown(dgvAttributes.SelectedRows(0).Index, dgvAttributes.SelectedRows(0).Index)
            txtSearchData.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Realiza una busqueda sobre la lista de atributos hacia abajo
    ''' </summary>
    ''' <param name="fromIndex">Posición desde donde se comenzará a buscar</param>
    ''' <param name="startPosition">Posición inicial para poder identificar resultados no encontrados</param>
    ''' <remarks></remarks>
    Private Sub SearchDown(ByVal fromIndex As Int32, ByVal startPosition As Int32)
        Dim i As Int32

        Try
            If IsNumeric(txtSearchData.Text.Trim) Then
                'Busqueda por ID de atributo
                Dim indexId As Int64 = Int64.Parse(txtSearchData.Text.Trim)
                For i = fromIndex + 1 To dgvAttributes.Rows.Count - 1
                    If CLng(dtAttributes.Rows(i)("Index_Id")) = indexId Then
                        dgvAttributes.CurrentCell = dgvAttributes.Rows(i).Cells(0)
                        Exit For
                    End If
                    If i = startPosition Then
                        MessageBox.Show("El atributo no ha podido ser encontrado", "Buscador de Atributos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit For
                    End If
                Next
            Else
                'Busqueda por el nombre del atributo
                Dim indexName As String = txtSearchData.Text.Trim.ToLower
                For i = fromIndex + 1 To dgvAttributes.Rows.Count - 1
                    If dtAttributes.Rows(i)("Index_Name").ToString.ToLower.Contains(indexName) Then
                        dgvAttributes.CurrentCell = dgvAttributes.Rows(i).Cells(0)
                        Exit For
                    End If
                    If i = startPosition Then
                        MessageBox.Show("El atributo no ha podido ser encontrado", "Buscador de Atributos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit For
                    End If
                Next
            End If

            'Al no encontrar el atributo vuelve a comenzar la búsqueda desde arriba
            If i = dgvAttributes.Rows.Count Then
                SearchDown(-1, startPosition)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
            MessageBox.Show("Error inesperado al buscar sobre los atributos", "Buscador de Atributos", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Realiza una busqueda sobre la lista de atributos hacia arriba
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSearchUp_Click(sender As System.Object, e As EventArgs) Handles btnSearchUp.Click
        If dgvAttributes.Rows.Count > 0 Then
            If dgvAttributes.SelectedRows.Count = 0 Then
                dgvAttributes.CurrentCell = dgvAttributes.Rows(0).Cells(0)
            End If
            SearchUp(dgvAttributes.SelectedRows(0).Index, dgvAttributes.SelectedRows(0).Index)
            txtSearchData.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Realiza una busqueda sobre la lista de atributos hacia arriba
    ''' </summary>
    ''' <param name="fromIndex">Posición desde donde se comenzará a buscar</param>
    ''' <param name="startPosition">Posición inicial para poder identificar resultados no encontrados</param>
    ''' <remarks></remarks>
    Private Sub SearchUp(ByVal fromIndex As Int32, ByVal startPosition As Int32)
        Dim i As Int32

        Try
            If IsNumeric(txtSearchData.Text.Trim) Then
                'Busqueda por ID de atributo
                Dim indexId As Int64 = Int64.Parse(txtSearchData.Text.Trim)
                For i = fromIndex - 1 To 0 Step -1
                    If CLng(dtAttributes.Rows(i)("Index_Id")) = indexId Then
                        dgvAttributes.CurrentCell = dgvAttributes.Rows(i).Cells(0)
                        Exit For
                    End If
                    If i = startPosition Then
                        MessageBox.Show("El atributo no ha podido ser encontrado", "Buscador de Atributos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit For
                    End If
                Next
            Else
                'Busqueda por el nombre del atributo
                Dim indexName As String = txtSearchData.Text.Trim.ToLower
                For i = fromIndex - 1 To 0 Step -1
                    If dtAttributes.Rows(i)("Index_Name").ToString.ToLower.Contains(indexName) Then
                        dgvAttributes.CurrentCell = dgvAttributes.Rows(i).Cells(0)
                        Exit For
                    End If
                    If i = startPosition Then
                        MessageBox.Show("El atributo no ha podido ser encontrado", "Buscador de Atributos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit For
                    End If
                Next
            End If

            'Al no encontrar el atributo vuelve a comenzar la búsqueda desde abajo
            If i = -1 Then
                SearchUp(dgvAttributes.Rows.Count, startPosition)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
            MessageBox.Show("Error inesperado al buscar sobre los atributos", "Buscador de Atributos", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Busca por la tecla ENTER
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtSearchData_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearchData.KeyPress
        'Si es ENTER se busca hacia abajo
        If e.KeyChar = Chr(13) Then
            btnSearchDown_Click(sender, Nothing)
        End If
    End Sub

    ''' <summary>
    ''' Habilita o deshabilita el boton de comenzar generacion de datos, dependiendo si existen tablas seleccionadas a procesar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub clbTables_SelectedIndexChanged(sender As Object, e As EventArgs) Handles clbTables.SelectedIndexChanged
        btnStart.Enabled = clbTables.CheckedItems.Count > 0
    End Sub

    ''' <summary>
    ''' Verifica si el proceso de generación de datos se encuentra corriendo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmEntityData_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If bgwDataGeneration.IsBusy Then
            If MessageBox.Show("La generación de datos no ha finalizado." & vbCrLf & "¿Deséa detener el proceso y cerrar?",
                               "Procesamiento en curso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                e.Cancel = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' Detiene el proceso de generacion de datos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnStop_Click(sender As System.Object, e As EventArgs) Handles btnStop.Click
        If bgwDataGeneration.IsBusy Then
            If MessageBox.Show("La generación de datos no ha finalizado." & vbCrLf & "¿Deséa detener el proceso y cerrar?",
                               "Procesamiento en curso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                bgwDataGeneration.CancelAsync()
                lblCurrentTable.Text = "Deteniendo..."
            End If
        End If
    End Sub

End Class
