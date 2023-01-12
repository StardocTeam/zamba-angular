Imports System.Windows.Forms
Imports Zamba.AppBlock

Public Class PlayDOSHOWEDITTABLE
    Private frm As ZForm
    Private showCheckColumn As Boolean
    Private cancelChildRulesExecution As Boolean = False
    Private justShow As Boolean = False
    Private dataIsEmpty As Boolean = False
    Private grid As DataGridView
    Private Dt As DataTable
    Private myRule As IDOShowEditTable


    ''' <summary>
    '''    
    ''' </summary>
    ''' <param name="results"></param>
    ''' <param name="myRule"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Sebastian] 22-09-09 MODIFIED It was modified to use zamba grid
    '''     [Sebastian] 05-10-09 MODIFIED Column check was removed after clic on "Aceptar button"
    '''     [Tomas] 13/10/2009  Modified    Se modifica la visualización del formulario y la grilla.
    '''     [Tomas] 11/11/2009  Modified    Se modifica el manejo del check en la grilla y se agrega el evento doble click.
    '''     [Tomas] 23/11/2009  Modified    Se modifica la prioridad de los registros seleccionados. Para más info consultar en la tarea 830.
    '''                                     Prioridad de seleccion: 
    '''                                         1) Doble click en un registro.
    '''                                         2) Registros tildados.
    '''                                         3) Registros seleccionados (si no existen tildados).
    '''                                     Se modifica la manera que cierra el formulario. Si no hay valores seleccionados muestra el 
    '''                                     messagebox sin cerrar el formulario. 
    '''                                     Si el dataset viene vacio se valido para que no genere error.
    '''     [Tomas] 24/11/2009  Modified    Se agrega el botón cancelar y la cancelacion de la regla.
    '''                                     Se agrega a la configuración de la regla un check que permite mostrar u ocultar la columna del check.
    '''     [Tomas] 25/11/2009  Modified    Se corrigen validaciones al recibir datos nulos.
    '''                                     Se agrega validacion para los datos que son de tipo datetime y se encuentran vacios.
    ''' </history>
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim NewResults As New System.Collections.Generic.List(Of Core.ITaskResult)
        Dim data As Object
        Dim btnOk As Button
        Dim btnCancel As Button
        Dim pnlButtons As SplitContainer

        Try
            'falta hacer que se puedan seleccionar consultas predefinidas Modulo de integracion con Base de Datos
            If VariablesInterReglas.ContainsKey(myRule.VarSource) = False Then
                Throw New Exception("No se encontró la variable de origen de datos")

            Else
                'Obtiene las variables
                Trace.WriteLineIf(ZTrace.IsInfo, "Obteniendo variables inter reglas.")
                data = VariablesInterReglas.Item(myRule.VarSource)

                'Configuración del formulario
                Trace.WriteLineIf(ZTrace.IsInfo, "Creando el formulario y sus componentes.")
                frm = New ZForm
                frm.StartPosition = FormStartPosition.CenterScreen
                frm.ShowIcon = False
                frm.Text = myRule.Name
                frm.AutoScroll = True
                frm.AutoSize = True
                frm.AutoSizeMode = AutoSizeMode.GrowOnly

                'Configuración de la grilla
                grid = New DataGridView()
                grid.BackColor = System.Drawing.Color.LightSteelBlue
                grid.Dock = System.Windows.Forms.DockStyle.Fill
                grid.ForeColor = System.Drawing.Color.Black
                grid.TabIndex = 0
                grid.Name = "grid"
                grid.Dock = DockStyle.Fill

                'Configuración del botón OK
                btnOk = New Windows.Forms.Button
                btnOk.Text = "Aceptar"
                btnOk.Dock = Windows.Forms.DockStyle.Fill
                btnOk.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold)
                'Configuración del botón CANCEL
                btnCancel = New Windows.Forms.Button
                btnCancel.Text = "Cancelar"
                btnCancel.Dock = Windows.Forms.DockStyle.Fill
                btnCancel.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold)
                'Configuración del panel
                pnlButtons = New Windows.Forms.SplitContainer
                pnlButtons.Height = btnOk.Height + 2
                pnlButtons.Dock = DockStyle.Bottom

                pnlButtons.Panel1.Controls.Add(btnOk)
                pnlButtons.Panel2.Controls.Add(btnCancel)

                pnlButtons.IsSplitterFixed = True
                pnlButtons.Name = "pnlButtons"

                Trace.WriteLineIf(ZTrace.IsInfo, "Agregando eventos al formulario y sus componentes.")
                'Botón Aceptar.
                AddHandler btnOk.Click, AddressOf Me.CloseForm
                'Corrige el aspecto de la grilla y el tamaño del formulario.
                AddHandler frm.Shown, AddressOf FixFormSize
                'Botón cancelar y cruz. Cancelan la ejecución.
                AddHandler frm.FormClosing, AddressOf CancelChildRules
                AddHandler btnCancel.Click, AddressOf CancelChildRules

                frm.Controls.Add(grid)
                frm.Controls.Add(pnlButtons)

                grid.BringToFront()

                frm.Width = 900
                frm.Height = 700

                'Obtiene el datatable configurado con las opciones de la regla
                showCheckColumn = myRule.ShowCheckColumn
                justShow = myRule.ShowDataOnly

                'Si se muestran los datos unicamente, la columna del check se oculta
                If justShow Then
                    showCheckColumn = False
                End If

                If results.Count > 0 Then
                    grid.DataSource = GetDataTable(data, myRule, results(0))
                Else
                    grid.DataSource = GetDataTable(data, myRule, Nothing)
                End If

                DirectCast(grid.DataSource, DataTable).MinimumCapacity = DirectCast(grid.DataSource, DataTable).Rows.Count

                'Verifica si hay datos cargados en la tabla
                If DirectCast(grid.DataSource, DataTable).Rows.Count = 0 Then
                    dataIsEmpty = True
                End If

                'Verifica que la tabla no este vacía
                If Not dataIsEmpty Then
                    'La columna zcheck se oculta por defecto para que no sea mostrada por los
                    'filtros. Luego será mostrada dependiendo de la configuración de la regla.

                    For Each column As DataGridViewColumn In grid.Columns
                        column.ReadOnly = True
                    Next

                    'Pongo las columnas como editables
                    If String.IsNullOrEmpty(myRule.EditColumns) = False Then
                        For Each column As String In myRule.EditColumns.Split(",")
                            'Aca iria poner la columna como editable
                            grid.Columns(column.Trim()).ReadOnly = False
                        Next
                    End If
                Else
                    'Aunque la regla se encontrará configurada para mostrar  
                    'la columna del check la oculta ya que no se verá.
                    showCheckColumn = False
                End If

                Trace.WriteLineIf(ZTrace.IsInfo, "Mostrando el formulario.")
                frm.DialogResult = DialogResult.Cancel
                frm.ShowDialog()

                'Se valida si el form fué cerrado por el botón OK o la cruz y que existan datos cargados.
                If frm.DialogResult = DialogResult.OK AndAlso (Not dataIsEmpty) AndAlso (Not justShow) Then

                    Trace.WriteLineIf(ZTrace.IsInfo, "Obteniendo las filas seleccionadas.")
                    Dim dt As DataTable = GetCleanDataTable(grid)
                    Dim processedRows As Int32 = 0

                    'If save only some columns or complete data table
                    If String.IsNullOrEmpty(myRule.GetSelectedCols) Then
                        Trace.WriteLineIf(ZTrace.IsInfo, "Guardando toda la tabla en variables inter reglas.")
                        If VariablesInterReglas.ContainsKey(myRule.VarDestiny) = False Then
                            VariablesInterReglas.Add(myRule.VarDestiny, dt, False)
                        Else
                            VariablesInterReglas.Item(myRule.VarDestiny) = dt
                        End If

                    Else
                        processedRows = 0

                        'gets selected rows to insert into data table
                        Trace.WriteLineIf(ZTrace.IsInfo, "Se guardarán las columnas seleccionadas.")
                        If myRule.GetSelectedCols.Split(",").Length = 1 AndAlso dt.Rows.Count = 1 Then
                            Dim col As String = myRule.GetSelectedCols.Split(",")(0)

                            If VariablesInterReglas.ContainsKey(myRule.VarDestiny) = True Then
                                If IsNumeric(col) Then
                                    VariablesInterReglas.Item(myRule.VarDestiny) = dt.Rows(0)(Integer.Parse(col) - 1)
                                Else
                                    VariablesInterReglas.Item(myRule.VarDestiny) = dt.Rows(0)(col)
                                End If
                            Else
                                If IsNumeric(col) Then
                                    VariablesInterReglas.Add(myRule.VarDestiny, dt.Rows(0)(Integer.Parse(col) - 1), False)
                                Else
                                    VariablesInterReglas.Add(myRule.VarDestiny, dt.Rows(0)(col), False)
                                End If
                            End If

                        Else
                            Trace.WriteLineIf(ZTrace.IsInfo, "Se ha seleccionado más de un registro y se guardarán los valores en más de una columna.")
                            For Each col As DataColumn In dt.Columns
                                If grid.Columns(col.ColumnName).Visible = False Then
                                    dt.Columns.Remove(col.ColumnName)
                                End If
                            Next

                            Trace.WriteLineIf(ZTrace.IsInfo, "Guardando valores en variables inter reglas.")
                            If VariablesInterReglas.ContainsKey(myRule.VarDestiny) = False Then
                                VariablesInterReglas.Add(myRule.VarDestiny, dt, False)
                            Else
                                VariablesInterReglas.Item(myRule.VarDestiny) = dt
                            End If
                        End If
                    End If
                    dt = Nothing

                End If
                NewResults.AddRange(results)
            End If

        Finally
            Trace.WriteLineIf(ZTrace.IsInfo, "Liberando recursos.")
            If Not IsNothing(data) Then data = Nothing
            If Not IsNothing(btnOk) Then
                btnOk.Dispose()
                btnOk = Nothing
            End If
            If Not IsNothing(btnCancel) Then
                btnCancel.Dispose()
                btnCancel = Nothing
            End If
            If Not IsNothing(pnlButtons) Then
                pnlButtons.Dispose()
                pnlButtons = Nothing
            End If
            If Not IsNothing(grid) Then
                grid.Dispose()
                grid = Nothing
            End If
            If Not IsNothing(frm) Then
                frm.Dispose()
                frm = Nothing
            End If
        End Try

        'Verifica la cancelacion de las reglas hijas
        If cancelChildRulesExecution Then
            Return Nothing
        Else
            Return NewResults
        End If
    End Function

    ''' <summary>
    ''' Cierra el formulario.
    ''' </summary>
    Private Sub CloseForm(ByVal sender As Object, ByVal e As System.EventArgs)
        CloseForm()
    End Sub

    ''' <summary>
    ''' Deselecciona las filas de la grilla
    ''' </summary>
    Private Sub ClearSelection(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim grid As DataGridView
        Try
            grid = DirectCast(sender, DataGridView)
            grid.ClearSelection()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            grid = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' Cierra el formulario.
    ''' </summary>
    ''' <history>
    '''     [Tomas] 18/11/2009  Created     Se crea una sobrecarga del original.
    '''     [Tomas] 24/11/2009  Modified    Se verifica si hay filas seleccionadas.
    ''' </history>
    Private Sub CloseForm()
        If HasRowsSelected() Then
            Trace.WriteLineIf(ZTrace.IsInfo, "Cerrando el formulario desde el aceptar.")
            'Se remueve el handler ya que al llamar a frm.Close() se llamaba también al evento.
            RemoveHandler frm.FormClosing, AddressOf CancelChildRules
            frm.DialogResult = DialogResult.OK
            frm.Close()
        Else
            If MessageBox.Show("No ha seleccionado registros, ¿Desea continuar?", "Zamba", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Trace.WriteLineIf(ZTrace.IsInfo, "Cerrando el formulario desde el aceptar.")
                'Se remueve el handler ya que al llamar a frm.Close() se llamaba también al evento.
                RemoveHandler frm.FormClosing, AddressOf CancelChildRules
                frm.DialogResult = DialogResult.Cancel
                frm.Close()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Cancela la ejecución de la regla y cierra el formulario.
    ''' </summary>
    ''' <history>
    '''     [Tomas] 24/11/2009  Created
    ''' </history>
    Private Sub CancelChildRules(ByVal sender As Object, ByVal e As EventArgs)
        RemoveHandler frm.FormClosing, AddressOf CancelChildRules
        cancelChildRulesExecution = True
        Trace.WriteLineIf(ZTrace.IsInfo, "Cerrando el formulario y cancelando ejecución de reglas hijas.")
        frm.DialogResult = DialogResult.Cancel
    End Sub

    ''' <summary>
    ''' Modifica el tamaño de un formulario teniendo en cuenta el tamaño del
    ''' contenido de un dataGridView y el tamaño de la pantalla del usuario.
    ''' </summary>
    ''' <history>
    '''     [Tomas] 13/10/2009  Created.
    '''     [Tomas] 19/10/2009  Modified    Se corrige el cálculo del ancho y alto del formulario.
    '''                                     Los valores hardcodeados representan las toolbars, titulo del form, barras de la grilla, etc...
    '''     [Tomás] 24/11/2009  Modified    Se agrega la corrección del tamaño del panel de los botones.
    ''' </history>
    Private Sub FixFormSize(ByVal sender As Object, ByVal e As EventArgs)
        Trace.WriteLineIf(ZTrace.IsInfo, "Corrigiendo el tamaño del formulario.")

        Dim pnlButtons As SplitContainer
        Dim frmHeight As Int32 = 0
        Dim frmWidth As Int32 = 0
        Dim pcHeight As Int32 = My.Computer.Screen.WorkingArea.Height
        Dim pcWidth As Int32 = My.Computer.Screen.WorkingArea.Width
        Dim lookForGrid As Boolean = True
        Dim lookForPnl As Boolean = True

        'Se obtiene el formulario y la grilla
        Dim frm As Form = DirectCast(sender, Form)
        Dim grid As DataGridView = DirectCast(frm.Controls("grid"), DataGridView)

        'Se verifica que la grilla tenga filas o no.
        If grid.Rows.Count > 0 Then
            Dim dgv As DataGridView = grid

            'Ancho total que debería tener el formulario.
            'El 27 representa el ancho faltante necesario para mostrar correctamente la última columna.
            frmWidth = dgv.Columns.GetColumnsWidth(DataGridViewElementStates.None) + 27


            'Alto total que debería tener el formulario. 
            'El 103 representa el alto faltante necesario para mostrar correctamente los registros.
            frmHeight = (dgv.Rows(0).Height * dgv.Rows.Count) + dgv.ColumnHeadersHeight + 303

            dgv = Nothing
        Else
            'Si no tiene filas se hardcodea valores para el tamaño
            frmWidth = 500
            frmHeight = 500
        End If

        'Modifico la altura del formulario.
        If frmHeight < pcHeight Then
            frm.Height = frmHeight
        Else
            frm.Height = pcHeight - CInt(pcHeight / 4)
        End If

        'Modifico el ancho del formulario.
        If frmWidth < pcWidth / 3 Then
            frm.Width = pcWidth / 2
        ElseIf frmWidth < pcWidth Then
            If (frmWidth < 700) Then frmWidth = 700
            frm.Width = frmWidth
        Else
            frm.Width = pcWidth - CInt(pcWidth / 4)
        End If

        'Centra el form en la pantalla ya que luego de haberle modificado
        'el tamaño, la posición no se actualizó.
        frm.Top = CInt(pcHeight / 2) - CInt(frm.Height / 2)
        frm.Left = CInt(pcWidth / 2) - CInt(frm.Width / 2)

        'Se obtiene el panel de los botones.
        pnlButtons = DirectCast(frm.Controls("pnlButtons"), SplitContainer)

        'Posición del splitter. La primer asignación es para posicionar el splitter
        'a la mitad del formulario. La segunda asignación es para indicarle que no
        'tenga un mínimo de tamaño. El splitter se encuentra bloqueado.
        pnlButtons.Panel1MinSize = CInt(pnlButtons.Width / 2)
        pnlButtons.Panel1MinSize = 0

        grid.BringToFront()

        pnlButtons = Nothing
        grid = Nothing
        frm = Nothing
    End Sub

    '''' <summary>
    '''' Check the selected rows
    '''' </summary>
    '''' <history>
    ''''    [Tomas] 11/11/2009  Created
    ''''    [Tomas] 24/11/2009  Modified    Se adapta el método a la nueva configuración de showCheckColumn.
    '''' </history>
    Private Sub CheckSelectedRows(ByVal sender As Object, ByVal e As EventArgs)
        Trace.WriteLineIf(ZTrace.IsInfo, "Se ha seleccionado un registro mediante el doble click.")

        If showCheckColumn Then
            Dim grid As DataGridView = DirectCast(sender, DataGridView)

            'Destilda todas las filas.
            For Each checkedRow As DataGridViewRow In grid.Rows
                checkedRow.Cells("zcheck").Value = False
            Next

            'Tilda las filas seleccionadas al momento de hacer el doble click.
            For Each checkedRow As DataGridViewRow In grid.SelectedRows
                checkedRow.Cells("zcheck").Value = True
            Next
            grid = Nothing
        End If

        'Cierra el formulario para continuar con la ejecución de la regla
        Me.CloseForm()
    End Sub

    ''' <summary>
    ''' Devuelve un datatable dependiendo de la configuracion de la regla y el dato obtenido.
    ''' </summary>
    ''' <history>
    '''     [Tomas] 20/11/2009  Created
    '''     [Tomas] 24/11/2009  Modified    Se agrega la validación de showCheckColumn.
    ''' </history>
    Private Function GetDataTable(ByRef data As Object, ByVal myRule As IDOShowEditTable, ByVal result As IResult) As DataTable
        Dt = New DataTable()

        'Verifica si los datos se encuentran nulos
        If (TypeOf data Is DataSet) Then
            If IsNothing(data) OrElse DirectCast(data, DataSet).Tables(0).Rows.Count = 0 Then
                Return Dt
            Else
                Dt = DirectCast(data, DataSet).Tables(0)
            End If
        Else
            If IsNothing(data) OrElse DirectCast(data, DataTable).Rows.Count = 0 Then
                Return Dt
            Else
                Dt = DirectCast(data, DataTable)
            End If
        End If

        If String.IsNullOrEmpty(myRule.EditColumns) = False Then
            For Each column As String In myRule.EditColumns.Split(",")
                Dt.Columns.Add(column.Trim())
            Next
        End If

        'Verifica si debe agregar o no la columna del check
        'If showCheckColumn Then
        '    Trace.WriteLineIf(ZTrace.IsInfo, "Creando la columna del check.")

        '    'Configuracion de la columna
        '    Dim checkCol As DataColumn = New DataColumn("zcheck", GetType(Boolean))
        '    checkCol.AllowDBNull = False
        '    checkCol.DefaultValue = False

        '    Dt.Columns.Add(checkCol)
        '    Trace.WriteLineIf(ZTrace.IsInfo, "Columna check agregada")
        '    Dt.Columns("zcheck").SetOrdinal(0)

        '    'Hago el chequeo previo de los items
        '    Trace.WriteLineIf(ZTrace.IsInfo, "Valores prechequeo: " & myRule.CheckedItems)
        '    Trace.WriteLineIf(ZTrace.IsInfo, "Valor columna prechequeo" & myRule.CheckedItemsColumn)
        '    If String.IsNullOrEmpty(myRule.CheckedItems) = False And String.IsNullOrEmpty(myRule.CheckedItemsColumn) = False Then
        '        Dim strColNumber As String = myRule.CheckedItemsColumn
        '        Dim intColNumber As Int32

        '        If Not IsNothing(result) Then
        '            strColNumber = Zamba.Core.TextoInteligente.ReconocerCodigo(myRule.CheckedItemsColumn, result)
        '        End If
        '        strColNumber = WFRuleParent.ReconocerVariables(strColNumber)

        '        Trace.WriteLineIf(ZTrace.IsInfo, "Nro de columna: " & strColNumber)

        '        'Si el nro de la columna por la cual comparar es valida
        '        If Int32.TryParse(strColNumber, intColNumber) Then
        '            strColNumber = myRule.CheckedItems
        '            Dim objeto As Object = Nothing

        '            strColNumber = WFRuleParent.ReconocerVariablesValuesSoloTexto(strColNumber)
        '            If Not IsNothing(result) Then
        '                strColNumber = Zamba.Core.TextoInteligente.ReconocerCodigo(strColNumber, result)
        '            End If
        '            If String.IsNullOrEmpty(strColNumber.Trim()) = False Then
        '                objeto = WFRuleParent.ReconocerVariablesAsObject(strColNumber)

        '                'Si tengo que comparar filas de un dataset
        '                If (TypeOf (objeto) Is DataSet) Then
        '                    For i As Int32 = 0 To Dt.Rows.Count
        '                        For j As Int32 = 0 To DirectCast(objeto, DataSet).Tables(0).Rows.Count
        '                            If String.Compare(Dt.Rows(i)(intColNumber + 1).ToString(), DirectCast(objeto, DataSet).Tables(0).Rows(j)(0).ToString(), True) = 0 Then
        '                                Dt.Rows(i)(0) = True
        '                            End If
        '                        Next
        '                    Next
        '                    'Si tengo que comparar filas de un datatable
        '                ElseIf (TypeOf (objeto) Is DataTable) Then
        '                    For i As Int32 = 0 To Dt.Rows.Count
        '                        For j As Int32 = 0 To DirectCast(objeto, DataTable).Rows.Count
        '                            If String.Compare(Dt.Rows(i)(intColNumber + 1).ToString(), DirectCast(objeto, DataTable).Rows(j)(0).ToString(), True) = 0 Then
        '                                Dt.Rows(i)(0) = True
        '                            End If
        '                        Next
        '                    Next
        '                    'Si es string, solo hay que seleccionar una fila a menos que este separado por comas
        '                ElseIf (TypeOf (objeto) Is String) Then
        '                    Trace.WriteLineIf(ZTrace.IsInfo,"Objeto: " & objeto.ToString())
        '                    If objeto.ToString().Contains(",") = False Then
        '                        Trace.WriteLineIf(ZTrace.IsInfo, "Cantidad filas: " & Dt.Rows.Count)
        '                        Trace.WriteLineIf(ZTrace.IsInfo, "Objeto comparacion:" & objeto.ToString().Trim())
        '                        For i As Int32 = 0 To Dt.Rows.Count - 1
        '                            Trace.WriteLineIf(ZTrace.IsInfo, "Valor fila(" & i & "): " & Dt.Rows(i)(intColNumber + 1).ToString().Trim())
        '                            If String.Compare(Dt.Rows(i)(intColNumber + 1).ToString().Trim(), objeto.ToString().Trim(), True) = 0 Then
        '                                Trace.WriteLineIf(ZTrace.IsInfo, "Encontrado")
        '                                Dt.Rows(i)(0) = True
        '                                Exit For
        '                            End If
        '                        Next
        '                    Else
        '                        Trace.WriteLineIf(ZTrace.IsInfo, "Cantidad filas: " & Dt.Rows.Count)
        '                        For Each valor As String In objeto.ToString().Split(",")
        '                            Trace.WriteLineIf(ZTrace.IsInfo, "Objeto comparacion:" & valor.Trim())
        '                            For i As Int32 = 0 To Dt.Rows.Count - 1
        '                                Trace.WriteLineIf(ZTrace.IsInfo, "Valor fila(" & i & "): " & Dt.Rows(i)(intColNumber + 1).ToString().Trim())
        '                                If String.Compare(Dt.Rows(i)(intColNumber + 1).ToString().Trim(), valor.Trim(), True) = 0 Then
        '                                    Trace.WriteLineIf(ZTrace.IsInfo, "Encontrado")
        '                                    Dt.Rows(i)(0) = True
        '                                    Exit For
        '                                End If
        '                            Next
        '                        Next
        '                    End If
        '                End If
        '            Else
        '                Trace.WriteLineIf(ZTrace.IsInfo, "Sin prechequeo")
        '                Return Dt
        '            End If
        '        Else
        '            Return Dt
        '        End If
        '    Else
        '        Return Dt
        '    End If
        'End If
        Return Dt
    End Function

    ''' <summary>
    ''' Get a clean datatable with the selected rows from a grid.
    ''' </summary>
    ''' <history>
    '''     [Tomas] 11/11/2009  Created
    ''' </history>
    Private Function GetCleanDataTable(ByRef grid As DataGridView) As DataTable
        'Se crea un datatable vacío con la estructura de la grilla
        Dim dtClean As DataTable = DirectCast(grid.DataSource, DataTable).Clone

        'Si la columna del check es visible, se obtienen las filas checkeadas
        'If showCheckColumn Then
        '    For Each row As DataGridViewRow In grid.Rows
        '        If Boolean.Parse(row.Cells("zcheck").Value) Then
        '            Dim drClean As DataRow = dtClean.NewRow
        '            drClean.ItemArray = GetRowValues(row, dtClean.Columns)
        '            dtClean.Rows.Add(drClean)
        '        End If
        '    Next
        'End If

        'Si no habían filas checkeadas se obtienen las seleccionadas.
        'If IsNothing(dtClean.Rows) OrElse dtClean.Rows.Count = 0 Then
        For Each row As DataGridViewRow In grid.Rows
            Dim drClean As DataRow = dtClean.NewRow
            drClean.ItemArray = GetRowValues(row, dtClean.Columns)
            dtClean.Rows.Add(drClean)
        Next
        'End If

        'Remueve la columna del check ya que no se utilizará mas
        'If showCheckColumn Then
        '    dtClean.Columns.Remove("zcheck")
        'End If

        Return dtClean
    End Function

    ''' <summary>
    ''' Pasa los valores de un DataGridViewRow a un array de Object
    ''' </summary>
    ''' <history>
    '''     [Tomas] 11/11/2009  Created
    ''' </history>
    Private Function GetRowValues(ByRef row As DataGridViewRow, ByVal columnsStructure As DataColumnCollection) As Object()
        Dim itemArray(row.Cells.Count - 1) As Object

        'Recorre las celdas del datagridviewrow obteniendo los valores
        For i As Int32 = 0 To row.Cells.Count - 1
            If IsDBNull(row.Cells(i).Value) = False Then
                'Si el tipo de dato de la columna es DateTime y el valor se encuentra vacío devuelve nothing
                If String.IsNullOrEmpty(row.Cells(i).Value) AndAlso _
                String.Compare(columnsStructure(i).DataType.Name, "DateTime") = 0 Then
                    itemArray(i) = Nothing
                Else
                    itemArray(i) = row.Cells(i).Value
                End If
            Else
                itemArray(i) = String.Empty
            End If
        Next

        Return itemArray
    End Function

    ''' <summary>
    ''' Verifica si la grilla tiene registros seleccionados
    ''' </summary>
    ''' <history>
    '''     [Tomas] 23/11/2009  Created     
    ''' </history>
    Private Function HasRowsSelected() As Boolean
        Return True
        'Dim grid As DataGridView = DirectCast(frm.Controls("grid"), DataGridView)

        ''Verifica las seleccionadas
        'If Not IsNothing(grid.SelectedRows) AndAlso grid.SelectedRows.Count > 0 Then
        '    grid = Nothing
        '    Return True
        'End If

        ''Si no tiene registros seleccionados verifico los checkeados
        'If showCheckColumn Then
        '    For Each row As DataGridViewRow In grid.Rows
        '        If Boolean.Parse(row.Cells("zcheck").Value) Then
        '            grid = Nothing
        '            Return True
        '        End If
        '    Next
        'End If

        ''Si no hay filas seleccionadas devuelve false
        'grid = Nothing
        'Return False
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    Public Sub New(ByVal rule As IDOShowEditTable)
        Me.myRule = rule
    End Sub
End Class