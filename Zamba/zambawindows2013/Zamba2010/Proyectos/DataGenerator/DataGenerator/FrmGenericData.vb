Imports Zamba.Core

Public Class FrmGenericData

    Private table As String
    Private selectedGeneration As Int32
    Private lstFilter As List(Of String)
    Private selectedCategory As DataGeneratorCategory
    Private dataGen As New DataGeneratorBusiness(Application.StartupPath)
    Private dtAttributes As DataTable = Nothing

    ''' <summary>
    ''' Carga el listado de tablas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmGenericData_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
        Dim dbTools As New DBToolsExt

        Try
            'Se obtiene una lista con los nombres de todas las tablas de la base de datos
            lbxTables.DataSource = dataGen.GetAllTableNamesList()

            'Se selecciona el primero
            If lbxTables.Items.Count > 0 Then
                lbxTables.SelectedIndex = 0
            Else
                MessageBox.Show("No se encontraron tablas. El programa se cerrará.")
                Application.Exit()
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Application.Exit()
        Finally
            dbTools = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' Genera una previsualización de los datos de la tabla ingresada
    ''' </summary>
    ''' <param name="filterTop100">True para mostrar los primeros 100 registros</param>
    ''' <remarks></remarks>
    Private Sub PreviewTable(filterTop100 As Boolean)
        table = lbxTables.Text

        'Valida que todos los datos se encuentren completos
        If table.Length > 0 Then
            'Se expande el form para visualizar la grilla
            SplitContainer1.Panel2Collapsed = False
            SplitContainer1.SplitterDistance = SplitContainer1.Height - (SplitContainer1.Height / 3)

            Try
                'Consulta de previsualización
                Dim dt As DataTable = dataGen.GetTablePreview(table, filterTop100)
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
    ''' Carga las columnas de la tabla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub lbxTables_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles lbxTables.SelectedIndexChanged
        Dim firstLoad As Boolean = (dgvColumns.Rows.Count = 0)
        table = lbxTables.Text

        Try
            'Se obtienen todos los atributos de Zamba
            dtAttributes = dataGen.GetColumns(table)

            'Se carga la grilla
            With dgvColumns
                .DataSource = dtAttributes

                'Se carga correctamente la columna categoría
                Dim dcColCategory As DataGridViewComboBoxColumn = New DataGridViewComboBoxColumn()
                dcColCategory.Name = "category"
                dcColCategory.DataPropertyName = "category"
                dcColCategory.DataSource = dataGen.GetDataCategories()
                .Columns.RemoveAt(1)
                .Columns.Insert(1, dcColCategory)

                'Se modifican los nombres de las columnas
                .Columns(0).HeaderText = "Columna"
                .Columns(1).HeaderText = "Seleccione una categoría"
                .Columns(0).ToolTipText = "Es la columna que su contenido será modificado por datos genéricos"
                .Columns(1).ToolTipText = "Seleccione la categoría o tipo de dato al que corresponde"
                .Columns(0).ReadOnly = True
            End With

            'Se carga el combo con la clave primaria
            Dim lstPrimaryKey As New List(Of String)
            lstPrimaryKey.Add(String.Empty) 'Se agrega un vacio para obligar a seleccionar uno
            For i As Int32 = 0 To dtAttributes.Rows.Count - 1
                lstPrimaryKey.Add(dtAttributes.Rows(i)(0).ToString)
            Next
            cboPrimaryKey.DataSource = lstPrimaryKey

        Catch ex As Exception
            ZClass.raiseerror(ex)
            MessageBox.Show("Ha ocurrido un error al cargar las columnas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Genera datos aleatorios a partir de un archivo de diccionario y actualiza los registros de una tabla especificada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnStart_Click(sender As System.Object, e As EventArgs) Handles btnStart.Click
        'Valida que todos los datos se encuentren completos
        If cboPrimaryKey.Text.Length > 0 Then
            If dgvColumns.Columns.Count > 0 AndAlso dgvColumns.Rows.Count > 0 Then
                Try
                    EditFormControlsVisibility(False)

                    'Valores por defecto
                    pgbData.Maximum = 100
                    pgbData.Minimum = 0
                    pgbData.Value = 0

                    'Se obtiene el objeto que contiene toda la informacion sobre los datos a generar
                    Dim dataHelper As DataGeneratorHelper = GetDataGeneratorHelper()

                    'Se ejecuta el hilo generador de datos
                    bgwDataGeneration.WorkerReportsProgress = True
                    bgwDataGeneration.WorkerSupportsCancellation = True
                    bgwDataGeneration.RunWorkerAsync(dataHelper)
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                    MessageBox.Show("Error al inicializar la generación de datos", String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    EditFormControlsVisibility(True)
                End Try
            Else
                MessageBox.Show("No hay datos a procesar para la tabla seleccionada", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Else
            MessageBox.Show("Debe seleccionar una clave primaria", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            cboPrimaryKey.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Genera un objeto con toda la información necesaria para comenzar con la modificacion de datos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetDataGeneratorHelper() As DataGeneratorHelper
        Dim columnId As String
        Dim categoryIndex As Int32 = dgvColumns.Columns("category").Index
        Dim columnIndex As Int32 = dgvColumns.Columns("name").Index
        Dim categoryEnum As DataGeneratorCategory
        Dim filter As String = String.Empty
        Dim column As String
        Dim columns As New List(Of DataGeneratorHelper.ColumnHelper)
        Dim categoryDescription As String

        columnId = cboPrimaryKey.Text

        'Se agregan los datos de las columnas a modificar
        For i As Int32 = 0 To dgvColumns.Rows.Count - 1
            categoryDescription = dgvColumns(categoryIndex, i).Value.ToString

            'Verifica si se selecciono para ser modificado
            If categoryDescription.Length > 0 Then
                categoryEnum = dataGen.GetCategoryEnumerator(categoryDescription)
                column = dgvColumns(columnIndex, i).Value.ToString
                columns.Add(New DataGeneratorHelper.ColumnHelper(column, categoryEnum))
            End If
        Next

        Return New DataGeneratorHelper(table, columnId, columns)
    End Function

    ''' <summary>
    ''' Habilita o deshabilita los controles del form
    ''' </summary>
    ''' <param name="enable"></param>
    ''' <remarks></remarks>
    Private Sub EditFormControlsVisibility(enable As Boolean)
        SplitContainer1.Panel2.Enabled = enable
        SplitContainer2.Panel1.Enabled = enable
        dgvColumns.Enabled = enable
        cboPrimaryKey.Enabled = enable
        btnPreview100Records.Enabled = enable
        btnPreviewAllRecords.Enabled = enable
        btnStart.Enabled = enable
        btnStop.Enabled = Not enable
    End Sub

    ''' <summary>
    ''' Hilo encargado de generar los datos y actualizar la tabla deseada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bgwDataGeneration_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwDataGeneration.DoWork
        Try
            dataGen.GenerateGenericData(e.Argument, bgwDataGeneration)
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
    ''' Realiza una busqueda sobre la lista de atributos hacia abajo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSearchDown_Click(sender As System.Object, e As EventArgs) Handles btnSearchDown.Click
        If lbxTables.Items.Count > 0 Then
            SearchDown(lbxTables.SelectedIndex, lbxTables.SelectedIndex)
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
        Dim column As String = txtSearchData.Text.Trim.ToLower

        Try
            'Busqueda por el nombre de la columna
            For i = fromIndex + 1 To lbxTables.Items.Count - 1
                'Verifica si contiene el valor buscado
                If lbxTables.Items(i).ToString.ToLower.Contains(column) Then
                    lbxTables.SelectedIndex = i
                    Exit For
                End If
                If i = startPosition Then
                    MessageBox.Show("El atributo no ha podido ser encontrado", "Buscador de Atributos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit For
                End If
            Next

            'Al no encontrar el atributo vuelve a comenzar la búsqueda desde arriba
            If i = lbxTables.Items.Count Then
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
        If lbxTables.Items.Count > 0 Then
            SearchUp(lbxTables.SelectedIndex, lbxTables.SelectedIndex)
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
        Dim column As String = txtSearchData.Text.Trim.ToLower

        Try
            'Busqueda por el nombre del atributo
            For i = fromIndex - 1 To 0 Step -1
                'Verifica si contiene el valor buscado
                If lbxTables.Items(i).ToString.ToLower.Contains(column) Then
                    lbxTables.SelectedIndex = i
                    Exit For
                End If
                If i = startPosition Then
                    MessageBox.Show("El atributo no ha podido ser encontrado", "Buscador de Atributos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit For
                End If
            Next

            'Al no encontrar el atributo vuelve a comenzar la búsqueda desde abajo
            If i = -1 Then
                SearchUp(lbxTables.Items.Count, startPosition)
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
            End If
        End If
    End Sub

End Class
