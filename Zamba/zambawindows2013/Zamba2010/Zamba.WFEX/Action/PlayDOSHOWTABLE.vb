Imports System.Windows.Forms
Imports Zamba.Grid.Grid
Imports Zamba.Filters
Imports Zamba.Grid
Imports Zamba.Filters.Interfaces
Imports Zamba.AppBlock
Imports System.Reflection
Imports Telerik.WinControls.UI
Imports Zamba.Core

Public Class PlayDOSHOWTABLE
    Implements IFilter

    Private _myRule As IDOShowTable
    Private frm As ZForm
    Private showCheckColumn As Boolean
    Private cancelChildRulesExecution As Boolean
    Private justShow As Boolean = False
    Private dataIsEmpty As Boolean = False
    Private grid As GroupGrid
    Private Dt As DataTable

    Sub New(ByVal rule As IDOShowTable)
        Me._myRule = rule
    End Sub

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
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myRule As IDOShowTable) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim NewResults As New System.Collections.Generic.List(Of Core.ITaskResult)
        Dim data As Object
        Dim btnOk As Button
        Dim btnCancel As Button
        Dim pnlButtons As SplitContainer
        cancelChildRulesExecution = False

        Try
            'Esta línea es importante para solucionar un error que se genera al utilizar llamadas a reglas desde botones 
            'de formularios y a su vez activar los mismos desde un evento javascript como un onkeypress. Si esta línea
            'no esta lo que hace, por ejemplo al presionar enter sobre el boton javascript que acciona esta regla, 
            'es accionar directamente el boton aceptar de esta regla, como que el evento se dispara para arriba.
            'Con esto se frena ese evento. NO BORRAR.
            Application.DoEvents()

            'falta hacer que se puedan seleccionar consultas predefinidas Modulo de integracion con Base de Datos
            If VariablesInterReglas.ContainsKey(myRule.VarSource) = False Then
                Throw New Exception("No se encontró la variable de origen de datos")

            Else
                'Obtiene las variables
                data = VariablesInterReglas.Item(myRule.VarSource)

                'Configuración del formulario
                frm = New ZForm
                frm.StartPosition = FormStartPosition.CenterScreen
                frm.ShowIcon = False
                frm.Text = myRule.Name
                frm.AutoScroll = True
                frm.AutoSize = True
                frm.AutoSizeMode = AutoSizeMode.GrowOnly

                _fc = New FiltersComponent
                'Configuración de la grilla
                grid = New GroupGrid(True, Membership.MembershipHelper.CurrentUser.ID, Me, FilterTypes.Document)
                grid.BackColor = System.Drawing.Color.LightSteelBlue
                grid.Dock = System.Windows.Forms.DockStyle.Fill
                grid.ForeColor = System.Drawing.Color.Black
                grid.TabIndex = 0
                grid.WithExcel = False
                grid.UseColor = False
                grid.AlwaysFit = True
                grid.UseZamba = False
                grid.ShowPrintButton(False)
                grid.Name = "grid"
                grid.NewGrid.MultiSelect = myRule.SelectMultiRow
                grid.ShowSelectBtn(myRule.SelectMultiRow)
                grid.Dock = DockStyle.Fill


                'Configuración del botón OK
                btnOk = New Windows.Forms.Button
                btnOk.Text = "Aceptar"
                btnOk.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold)
                btnOk.Dock = Windows.Forms.DockStyle.Fill

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

                'Botón Aceptar.
                AddHandler btnOk.Click, AddressOf Me.CloseForm
                'Seleccion del valor al hacer doble click.
                AddHandler grid.OnDoubleClick, AddressOf CheckSelectedRows
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
                    If showCheckColumn Then
                        grid.NewGrid.Columns("zcheck").IsVisible = False
                        grid.NewGrid.Columns("zcheck").HeaderText = "     "
                    End If

                    'Verifica si debe ocultar columnas
                    If Not String.IsNullOrEmpty(myRule.ShowColumns) AndAlso Not dataIsEmpty Then
                        'Oculta todas las columnas
                        Dim cols() As String = myRule.ShowColumns.Split(",")
                        For Each col As GridViewColumn In grid.NewGrid.Columns
                            col.IsVisible = False
                        Next

                        'Muestra las columnas configuradas en la regla
                        For Each col As String In cols
                            If IsNumeric(col) Then
                                grid.NewGrid.Columns(Integer.Parse(col) - 1).IsVisible = True
                            Else
                                grid.NewGrid.Columns(col).IsVisible = True
                            End If
                        Next
                        cols = Nothing
                    End If
                    'Recargo los filtros luego de haberle agregado la columna check (no debe verse)
                    'y en caso de que hayan columnas que se ocultaron.
                    grid.LoadCmbFilterColumn(Nothing)

                    'Muestra u oculta la columna del check. Se ubica debajo
                    'de la carga de filtros para no incluirlo en el combo.
                    If showCheckColumn Then
                        grid.NewGrid.Columns("zcheck").IsVisible = True
                        RemoveHandler grid.OnDoubleClick, AddressOf CheckSelectedRows
                        AddHandler grid.OnRowClick, AddressOf ClearSelection
                    ElseIf Not IsNothing(grid.NewGrid.Rows) AndAlso grid.NewGrid.Rows.Count > 0 Then
                        grid.NewGrid.Rows(0).IsSelected = True
                    End If

                    If justShow Then
                        'Se remueve ya que al no haber filas no es necesario.
                        RemoveHandler grid.OnDoubleClick, AddressOf CheckSelectedRows
                        RemoveHandler grid.OnRowClick, AddressOf ClearSelection
                    End If
                Else
                    'Se remueve ya que al no haber filas no es necesario.
                    RemoveHandler grid.OnDoubleClick, AddressOf CheckSelectedRows
                    'Aunque la regla se encontrará configurada para mostrar  
                    'la columna del check la oculta ya que no se verá.
                    showCheckColumn = False
                End If

                frm.DialogResult = DialogResult.Cancel
                frm.ShowDialog()

                'Se valida si el form fué cerrado por el botón OK o la cruz y que existan datos cargados.
                If frm.DialogResult = DialogResult.OK AndAlso (Not dataIsEmpty) AndAlso (Not justShow) Then
                    Dim dt As DataTable = GetCleanDataTable(grid)

                    'If save only some columns or complete data table
                    If String.IsNullOrEmpty(myRule.GetSelectedCols) Then
                        If VariablesInterReglas.ContainsKey(myRule.VarDestiny) = False Then
                            VariablesInterReglas.Add(myRule.VarDestiny, dt, False)
                        Else
                            VariablesInterReglas.Item(myRule.VarDestiny) = dt
                        End If

                    Else
                        If myRule.GetSelectedCols.Split(",").Length = 1 AndAlso dt.Rows.Count = 1 Then
                            Dim col As String = myRule.GetSelectedCols.Split(",")(0)
                            Trace.WriteLineIf(ZTrace.IsInfo, "Se ha seleccionado un registro. El dato obtenido será escalar. Guardando el valor a la variable inter regla.")
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
                            Trace.WriteLineIf(ZTrace.IsInfo, "Se ha seleccionado más de un registro.")
                            Dim columnOrder As New List(Of String)

                            'Se recorre el datatable original marcando unicamente las columnas solicitadas
                            For Each col As Object In myRule.GetSelectedCols.Split(New Char() {","}, StringSplitOptions.RemoveEmptyEntries)
                                If IsNumeric(col) Then
                                    dt.Columns(CInt(col) - 1).Caption = "§"
                                    columnOrder.Add(dt.Columns(CInt(col) - 1).ColumnName)
                                Else
                                    dt.Columns(col.ToString).Caption = "§"
                                    columnOrder.Add(dt.Columns(col.ToString).ColumnName)
                                End If
                            Next

                            'Se remueven las columnas que no se encuentren marcadas
                            Dim columnsCount As Int32 = dt.Columns.Count
                            For i As Int32 = columnsCount - 1 To 0 Step -1
                                If String.Compare(dt.Columns(i).Caption, "§") <> 0 Then
                                    dt.Columns.RemoveAt(i)
                                End If
                            Next

                            'Se ordena como se encuentre configurado en la regla
                            For i As Int32 = 0 To columnOrder.Count - 1
                                dt.Columns(columnOrder(i)).SetOrdinal(i)
                            Next

                            If VariablesInterReglas.ContainsKey(myRule.VarDestiny) = False Then
                                VariablesInterReglas.Add(myRule.VarDestiny, dt, False)
                            Else
                                VariablesInterReglas.Item(myRule.VarDestiny) = dt
                            End If
                        End If
                    End If
                    dt = Nothing
                Else
                    If myRule.ThrowExceptionIfCancel Then
                        Trace.WriteLineIf(ZTrace.IsInfo, "Regla configurada para provocar una exception en caso de cancelar la misma")
                        Throw New Exception("El usuario cancelo la ejecucion de la regla")
                    End If
                End If
                NewResults.AddRange(results)
            End If

        Finally
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
        Dim grid As GroupGrid
        Try
            grid = DirectCast(sender, GroupGrid)
            grid.NewGrid.ClearSelection()
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
            'Se remueve el handler ya que al llamar a frm.Close() se llamaba también al evento.
            RemoveHandler frm.FormClosing, AddressOf CancelChildRules
            frm.DialogResult = DialogResult.OK
            frm.Close()
        Else
            If MessageBox.Show("No ha seleccionado registros, ¿Desea continuar?", "Zamba", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                'Se remueve el handler ya que al llamar a frm.Close() se llamaba también al evento.
                RemoveHandler frm.FormClosing, AddressOf CancelChildRules
                'frm.DialogResult = DialogResult.Cancel
                frm.DialogResult = DialogResult.OK
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

        Dim pnlButtons As SplitContainer
        Dim frmHeight As Int32 = 0
        Dim frmWidth As Int32 = 0
        Dim pcHeight As Int32 = My.Computer.Screen.WorkingArea.Height
        Dim pcWidth As Int32 = My.Computer.Screen.WorkingArea.Width
        Dim lookForGrid As Boolean = True
        Dim lookForPnl As Boolean = True

        'Se obtiene el formulario y la grilla
        Dim frm As Form = DirectCast(sender, Form)
        Dim grid As GroupGrid = DirectCast(frm.Controls("grid"), GroupGrid)
        grid.FixColumns()

        'Se verifica que la grilla tenga filas o no.
        If grid.NewGrid.Rows.Count > 0 Then
            Dim dgv As RadGridView = grid.NewGrid

            'Ancho total que debería tener el formulario.
            'El 27 representa el ancho faltante necesario para mostrar correctamente la última columna.
            'frmWidth = dgv.Columns.GetColumnsWidth(DataGridViewElementStates.None) + 27

            ' [ivan] No tengo esa propiedad, asi que hago un bucle para calcular el total de ancho de las columnas
            For Each columna As GridViewColumn In dgv.Columns
                frmWidth += columna.Width
            Next
            frmHeight += 27

            'Alto total que debería tener el formulario. 
            'El 103 representa el alto faltante necesario para mostrar correctamente los registros.
            '[ivan] Ver esto, porque no se si "TableHeaderHeight" es la propiedad que necesito 
            frmHeight = (dgv.Rows(0).Height * dgv.Rows.Count) + dgv.TableElement.TableHeaderHeight + 303



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

        'Por defecto oculta los filtros para mejorar la visualización de los datos mostrados.
        grid.HideFiltersAtStart()

        'Remueve la seleccion de las filas en caso de que se permita la columna del check.
        If showCheckColumn Then
            grid.NewGrid.ClearSelection()
        End If

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

        If showCheckColumn Then
            Dim grid As Zamba.Grid.NewGrid = DirectCast(sender, GroupGrid).NewGrid

            'Destilda todas las filas.
            For Each checkedRow As GridViewRowInfo In grid.Rows
                checkedRow.Cells("zcheck").Value = False
            Next

            'Tilda las filas seleccionadas al momento de hacer el doble click.
            For Each checkedRow As GridViewRowInfo In grid.SelectedRows
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
    Private Function GetDataTable(ByRef data As Object, ByVal myRule As IDOShowTable, ByVal result As IResult) As DataTable
        Dt = New DataTable()

        If (TypeOf data Is Array) Then
            If Not IsNothing(data) Then
                For Each r As Object In data
                    If Not IsNothing(r) Then
                        '                      Trace.WriteLineIf(ZTrace.IsInfo, "Tipo objeto: " & r.GetType().ToString())
                        For Each prop As PropertyInfo In r.GetType().GetProperties()
                            Dim dc As New DataColumn(prop.Name)
                            Dt.Columns.Add(dc)
                        Next
                        '               Else
                        ''                     Trace.WriteLineIf(ZTrace.IsInfo, "Objeto en array vacio")
                    End If
                Next

                For Each r As Object In data
                    If Not IsNothing(r) Then
                        Dim arlist As New ArrayList()

                        For Each prop As PropertyInfo In r.GetType().GetProperties()
                            arlist.Add(prop.GetValue(r, Nothing))
                        Next

                        Dt.LoadDataRow(arlist.ToArray(), True)
                    End If
                Next
            End If

            'Verifica si los datos se encuentran nulos
        ElseIf (TypeOf data Is DataSet) Then
            If IsNothing(data) OrElse DirectCast(data, DataSet).Tables(0).Rows.Count = 0 Then
                Return Dt
            Else
                Dt = DirectCast(data, DataSet).Tables(0)
            End If
        Else
            If Not IsNothing(data) Then
                Trace.WriteLineIf(ZTrace.IsInfo, "Tipo: " & data.GetType().ToString())
            Else
                Trace.WriteLineIf(ZTrace.IsInfo, "Vacio")
            End If
            If IsNothing(data) OrElse DirectCast(data, DataTable).Rows.Count = 0 Then
                Return Dt
            Else
                Dt = DirectCast(data, DataTable)
            End If
        End If

        If String.IsNullOrEmpty(myRule.EditColumns) = False Then
            For Each column As String In myRule.EditColumns.Split(",")
                Dt.Columns.Add(column)
            Next
        End If

        'Verifica si debe agregar o no la columna del check
        If showCheckColumn Then

            'Configuracion de la columna
            Dim checkCol As DataColumn = New DataColumn("zcheck", GetType(Boolean))
            checkCol.AllowDBNull = False
            checkCol.DefaultValue = False

            Dt.Columns.Add(checkCol)
            Dt.Columns("zcheck").SetOrdinal(0)

            'Hago el chequeo previo de los items
            If String.IsNullOrEmpty(myRule.CheckedItems) = False And String.IsNullOrEmpty(myRule.CheckedItemsColumn) = False Then
                Dim strColNumber As String = myRule.CheckedItemsColumn
                Dim intColNumber As Int32

                If Not IsNothing(result) Then
                    strColNumber = Zamba.Core.TextoInteligente.ReconocerCodigo(myRule.CheckedItemsColumn, result)
                End If
                strColNumber = WFRuleParent.ReconocerVariables(strColNumber)


                'Si el nro de la columna por la cual comparar es valida
                If Int32.TryParse(strColNumber, intColNumber) Then
                    strColNumber = myRule.CheckedItems
                    Dim objeto As Object = Nothing

                    strColNumber = WFRuleParent.ReconocerVariablesValuesSoloTexto(strColNumber)
                    If Not IsNothing(result) Then
                        strColNumber = Zamba.Core.TextoInteligente.ReconocerCodigo(strColNumber, result)
                    End If
                    If String.IsNullOrEmpty(strColNumber.Trim()) = False Then
                        objeto = WFRuleParent.ReconocerVariablesAsObject(strColNumber)

                        'Si tengo que comparar filas de un dataset
                        If (TypeOf (objeto) Is DataSet) Then
                            For i As Int32 = 0 To Dt.Rows.Count
                                For j As Int32 = 0 To DirectCast(objeto, DataSet).Tables(0).Rows.Count
                                    If String.Compare(Dt.Rows(i)(intColNumber + 1).ToString(), DirectCast(objeto, DataSet).Tables(0).Rows(j)(0).ToString(), True) = 0 Then
                                        Dt.Rows(i)(0) = True
                                    End If
                                Next
                            Next
                            'Si tengo que comparar filas de un datatable
                        ElseIf (TypeOf (objeto) Is DataTable) Then
                            For i As Int32 = 0 To Dt.Rows.Count
                                For j As Int32 = 0 To DirectCast(objeto, DataTable).Rows.Count
                                    If String.Compare(Dt.Rows(i)(intColNumber + 1).ToString(), DirectCast(objeto, DataTable).Rows(j)(0).ToString(), True) = 0 Then
                                        Dt.Rows(i)(0) = True
                                    End If
                                Next
                            Next
                            'Si es string, solo hay que seleccionar una fila a menos que este separado por comas
                        ElseIf (TypeOf (objeto) Is String) Then
                            If objeto.ToString().Contains(",") = False Then
                                'Trace.WriteLineIf(ZTrace.IsInfo, "Cantidad filas: " & Dt.Rows.Count)
                                'Trace.WriteLineIf(ZTrace.IsInfo, "Objeto comparacion:" & objeto.ToString().Trim())
                                For i As Int32 = 0 To Dt.Rows.Count - 1
                                    'Trace.WriteLineIf(ZTrace.IsInfo, "Valor fila(" & i & "): " & Dt.Rows(i)(intColNumber + 1).ToString().Trim())
                                    If String.Compare(Dt.Rows(i)(intColNumber + 1).ToString().Trim(), objeto.ToString().Trim(), True) = 0 Then
                                        'Trace.WriteLineIf(ZTrace.IsInfo, "Encontrado")
                                        Dt.Rows(i)(0) = True
                                        Exit For
                                    End If
                                Next
                            Else
                                'Trace.WriteLineIf(ZTrace.IsInfo, "Cantidad filas: " & Dt.Rows.Count)
                                For Each valor As String In objeto.ToString().Split(",")
                                    'Trace.WriteLineIf(ZTrace.IsInfo, "Objeto comparacion:" & valor.Trim())
                                    For i As Int32 = 0 To Dt.Rows.Count - 1
                                        'Trace.WriteLineIf(ZTrace.IsInfo, "Valor fila(" & i & "): " & Dt.Rows(i)(intColNumber + 1).ToString().Trim())
                                        If String.Compare(Dt.Rows(i)(intColNumber + 1).ToString().Trim(), valor.Trim(), True) = 0 Then
                                            'Trace.WriteLineIf(ZTrace.IsInfo, "Encontrado")
                                            Dt.Rows(i)(0) = True
                                            Exit For
                                        End If
                                    Next
                                Next
                            End If
                        End If
                    Else
                        'Trace.WriteLineIf(ZTrace.IsInfo, "Sin prechequeo")
                        Return Dt
                    End If
                Else
                    Return Dt
                End If
            Else
                Return Dt
            End If
        End If
        Return Dt
    End Function

    ''' <summary>
    ''' Get a clean datatable with the selected rows from a grid.
    ''' </summary>
    ''' <history>
    '''     [Tomas] 11/11/2009  Created
    ''' </history>
    Private Function GetCleanDataTable(ByRef grid As GroupGrid) As DataTable
        'Se crea un datatable vacío con la estructura de la grilla
        Dim dtClean As DataTable = DirectCast(grid.DataSource, DataTable).Clone

        'Si la columna del check es visible, se obtienen las filas checkeadas
        If showCheckColumn Then
            For Each row As GridViewRowInfo In grid.NewGrid.Rows
                If Boolean.Parse(row.Cells("zcheck").Value) Then
                    Dim drClean As DataRow = dtClean.NewRow
                    drClean.ItemArray = GetRowValues(row, dtClean.Columns)
                    dtClean.Rows.Add(drClean)
                End If
            Next
        End If

        'Si no habían filas checkeadas se obtienen las seleccionadas.
        If IsNothing(dtClean.Rows) OrElse dtClean.Rows.Count = 0 Then
            For Each row As GridViewRowInfo In grid.NewGrid.SelectedRows
                Dim drClean As DataRow = dtClean.NewRow
                drClean.ItemArray = GetRowValues(row, dtClean.Columns)
                dtClean.Rows.Add(drClean)
            Next
        End If

        'Remueve la columna del check ya que no se utilizará mas
        If showCheckColumn Then
            dtClean.Columns.Remove("zcheck")
        End If

        Return dtClean
    End Function

    ''' <summary>
    ''' Pasa los valores de un DataGridViewRow a un array de Object
    ''' </summary>
    ''' <history>
    '''     [Tomas] 11/11/2009  Created
    ''' </history>
    Private Function GetRowValues(ByRef row As GridViewRowInfo, ByVal columnsStructure As DataColumnCollection) As Object()
        Dim itemArray(row.Cells.Count - 1) As Object

        'Recorre las celdas del datagridviewrow obteniendo los valores
        For i As Int32 = 0 To row.Cells.Count - 1
            'Si el tipo de dato de la columna es DateTime y el valor se encuentra vacío devuelve nothing
            If String.IsNullOrEmpty(row.Cells(i).Value) AndAlso _
            String.Compare(columnsStructure(i).DataType.Name, "DateTime") = 0 Then
                itemArray(i) = Nothing
            Else
                itemArray(i) = row.Cells(i).Value
            End If
            'PERFORMANCE?
            'If String.IsNullOrEmpty(row.Cells(i).Value) AndAlso _
            'columnsStructure(i).DataType is GetType(System.DateTime) Then
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
        Dim grid As RadGridView = DirectCast(frm.Controls("grid"), GroupGrid).NewGrid

        'Verifica las seleccionadas
        If Not IsNothing(grid.SelectedRows) AndAlso grid.SelectedRows.Count > 0 Then
            grid = Nothing
            Return True
        End If

        'Si no tiene registros seleccionados verifico los checkeados
        If showCheckColumn Then
            For Each row As GridViewRowInfo In grid.Rows
                If Boolean.Parse(row.Cells("zcheck").Value) Then
                    grid = Nothing
                    Return True
                End If
            Next
        End If

        'Si no hay filas seleccionadas devuelve false
        grid = Nothing
        Return False
    End Function

    Public Function PlayTest() As Boolean
        Return True
    End Function


    Public Function DiscoverParams() As List(Of String)
        Return New List(Of String)
    End Function

    Private _fc As IFiltersComponent

    Public Property Fc() As IFiltersComponent Implements IFilter.Fc
        Get
            Return _fc
        End Get
        Set(ByVal value As IFiltersComponent)
            _fc = value
        End Set
    End Property
    Private _lastPage As Integer

    Public Property LastPage() As Integer Implements IFilter.LastPage
        Get
            Return _lastPage
        End Get
        Set(ByVal value As Integer)
            _lastPage = value
        End Set
    End Property
    Public Sub ShowTaskOfDT() Implements IFilter.ShowTaskOfDT
        Dim FilterString As String = FiltersComponent.GetFiltersString(Fc.GetLastUsedFilters(0, Membership.MembershipHelper.CurrentUser.ID, FilterTypes.Document), False, Nothing)
        Dim DV As New DataView(Dt)
        DV.RowFilter = FilterString
        Me.grid.DataSource = DV.ToTable
    End Sub
End Class
