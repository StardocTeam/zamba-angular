Imports System.Windows.Forms
Imports Zamba.Grid.Grid
Imports Zamba.Filters
Imports Zamba.Grid
Imports Zamba.Filters.Interfaces
Imports Zamba.AppBlock
Imports System.Reflection

Public Class PlayDOSHOWTABLE
    Implements IFilter

    ' Private frm As ZForm
    Private showCheckColumn As Boolean
    Private cancelChildRulesExecution As Boolean = False
    Private justShow As Boolean = False
    Private dataIsEmpty As Boolean = False
    Private Dt As DataTable

    Private _myRule As IDOShowTable

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
        Return PlayWeb(results, New Hashtable, _myRule)
    End Function

    ''' <summary>
    ''' Ejecución web de la regla
    ''' </summary>
    ''' <history>
    '''     [Javier] 06/07/11  Created     
    ''' </history>
    Public Function PlayWeb(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal Params As Hashtable, ByVal myRule As IDOShowTable) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim NewResults As New System.Collections.Generic.List(Of Core.ITaskResult)
        Dim data As Object
        Dim isMultiSelect As Boolean

        If myRule.VarSource.Contains("zvar") Then
            myRule.VarSource = myRule.VarSource.Replace("zvar(", "")
            myRule.VarSource = myRule.VarSource.Replace(")", "")
        End If

        'falta hacer que se puedan seleccionar consultas predefinidas Modulo de integracion con Base de Datos
        If VariablesInterReglas.ContainsKey(myRule.VarSource) Then
            'Obtiene las variables
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo variables inter reglas.")
            data = VariablesInterReglas.Item(myRule.VarSource)

            'Obtiene el datatable configurado con las opciones de la regla
            isMultiSelect = myRule.SelectMultiRow
            justShow = myRule.ShowDataOnly

            'Si se muestran los datos unicamente, no se podrá seleccionar nada
            If justShow Then
                isMultiSelect = False
            End If

            If results.Count > 0 Then
                Params.Add("tableToView", GetDataTableWeb(data, myRule, results(0)))
            Else
                Params.Add("tableToView", GetDataTableWeb(data, myRule, Nothing))
            End If

            'Verifica si hay datos cargados en la tabla
            If DirectCast(Params("tableToView"), DataTable).Rows.Count = 0 Then
                dataIsEmpty = True
            End If

            If dataIsEmpty Then
                isMultiSelect = False
            End If

            'Propiedades de configuración
            Params.Add("dataIsEmpty", dataIsEmpty)
            Params.Add("isMultiSelect", isMultiSelect)
            Params.Add("showColumns", myRule.ShowColumns)
            Params.Add("saveColumns", myRule.GetSelectedCols)
            Params.Add("justShow", justShow)
            Params.Add("tableTitle", myRule.Name)
            Params.Add("editColumns", myRule.EditColumns)

            'TO-DO: Crear y cagar los filtros.
        Else
            Throw New Exception("No se encontró la variable de origen de datos")
        End If

        Return results
    End Function

    ''' <summary>
    ''' Segunda parte de la ejecución web de la regla
    ''' </summary>
    ''' <history>
    '''     [Javier] 06/07/11  Created     
    ''' </history>
    Public Function PlayWebSecondExecution(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal Params As Hashtable, ByVal myRule As IDOShowTable) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim lstResults As New System.Collections.Generic.List(Of Core.ITaskResult)

        If (Not dataIsEmpty) AndAlso (Not justShow) Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Hay registros seleccionados en la tabla.")
            'obtengo las filas seleccionadas
            Dim dt As DataTable = Params("returnValue")

            If String.IsNullOrEmpty(myRule.GetSelectedCols) Then

                If VariablesInterReglas.ContainsKey(myRule.VarDestiny) = False Then
                    VariablesInterReglas.Add(myRule.VarDestiny, dt)
                Else
                    VariablesInterReglas.Item(myRule.VarDestiny) = dt
                End If

                If (dt.Rows.Count = 1) Then

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Hay solo un registro seleccionado.")

                    For Each col As DataColumn In dt.Columns
                        Dim ColVariable As String = myRule.VarDestiny & "." & col.ColumnName
                        If VariablesInterReglas.ContainsKey(ColVariable) = False Then
                            VariablesInterReglas.Add(ColVariable, dt.Rows(0)(col.ColumnName).ToString())
                        Else
                            VariablesInterReglas.Item(ColVariable) = dt.Rows(0)(col.ColumnName).ToString()
                        End If
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Se completa la Variable columna " & ColVariable & " con valor: " & dt.Rows(0)(col.ColumnName).ToString())
                    Next
                Else
                    If (dt.Rows.Count = 0) Then

                        ZTrace.WriteLineIf(ZTrace.IsInfo, "NO Hay registro seleccionado.")

                        For Each col As DataColumn In dt.Columns
                            Dim ColVariable As String = myRule.VarDestiny & "." & col.ColumnName
                            If VariablesInterReglas.ContainsKey(ColVariable) = False Then
                                VariablesInterReglas.Add(ColVariable, "")
                            Else
                                VariablesInterReglas.Item(ColVariable) = ""
                            End If
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Se completa la Variable columna " & ColVariable & " sin valor")
                        Next

                    End If
                End If

            Else

                If myRule.GetSelectedCols.Split(",").Length = 1 AndAlso dt.Rows.Count = 1 Then
                    Dim col As String = myRule.GetSelectedCols.Split(",")(0)

                    If VariablesInterReglas.ContainsKey(myRule.VarDestiny) = True Then
                        If IsNumeric(col) Then
                            VariablesInterReglas.Item(myRule.VarDestiny) = dt.Rows(0).ItemArray(0)
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Se completa la Variable " & myRule.VarDestiny & " con valor: " & dt.Rows(0).ItemArray(0))
                        Else
                            VariablesInterReglas.Item(myRule.VarDestiny) = dt.Rows(0).ItemArray(0)
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Se completa la Variable " & myRule.VarDestiny & " con valor: " & dt.Rows(0).ItemArray(0))
                        End If
                    Else
                        If IsNumeric(col) Then
                            VariablesInterReglas.Add(myRule.VarDestiny, dt.Rows(0).ItemArray(0))
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Se completa la Variable " & myRule.VarDestiny & " con valor: " & dt.Rows(0).ItemArray(0))
                        Else
                            VariablesInterReglas.Add(myRule.VarDestiny, dt.Rows(0)(col))
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Se completa la Variable " & myRule.VarDestiny & " con valor: " & dt.Rows(0).ItemArray(0))
                        End If
                    End If

                Else

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Se ha seleccionado más de un registro.")
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
                        VariablesInterReglas.Add(myRule.VarDestiny, dt)
                    Else
                        VariablesInterReglas.Item(myRule.VarDestiny) = dt
                    End If
                End If

            End If

            dt = Nothing

        End If

        lstResults.AddRange(results)

        Params.Clear()
        If cancelChildRulesExecution Then
            Return Nothing
        Else
            Return lstResults
        End If
    End Function

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
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Es array")
            For Each prop As PropertyInfo In data(0).GetType().GetProperties()
                Dim dc As New DataColumn(prop.Name)
                Dt.Columns.Add(dc)
            Next

            For Each r As Object In data
                Dim arlist As New ArrayList()

                For Each prop As PropertyInfo In data(0).GetType().GetProperties()
                    arlist.Add(prop.GetValue(r, Nothing))
                Next

                Dt.LoadDataRow(arlist.ToArray(), True)
            Next

            'Verifica si los datos se encuentran nulos
        ElseIf (TypeOf data Is DataSet) Then
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
                Dt.Columns.Add(column)
            Next
        End If

        'Verifica si debe agregar o no la columna del check
        If showCheckColumn Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Creando la columna del check.")

            'Configuracion de la columna
            Dim checkCol As DataColumn = New DataColumn("zcheck", GetType(Boolean))
            checkCol.AllowDBNull = False
            checkCol.DefaultValue = False

            Dt.Columns.Add(checkCol)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Columna check agregada")
            Dt.Columns("zcheck").SetOrdinal(0)

            'Hago el chequeo previo de los items
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Valores prechequeo: " & myRule.CheckedItems)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor columna prechequeo" & myRule.CheckedItemsColumn)
            If String.IsNullOrEmpty(myRule.CheckedItems) = False And String.IsNullOrEmpty(myRule.CheckedItemsColumn) = False Then
                Dim strColNumber As String = myRule.CheckedItemsColumn
                Dim intColNumber As Int32

                If Not IsNothing(result) Then
                    strColNumber = Zamba.Core.TextoInteligente.ReconocerCodigo(myRule.CheckedItemsColumn, result)
                End If
                Dim VarInterReglas As New VariablesInterReglas()
                strColNumber = VarInterReglas.ReconocerVariables(strColNumber)

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Nro de columna: " & strColNumber)

                'Si el nro de la columna por la cual comparar es valida
                If Int32.TryParse(strColNumber, intColNumber) Then
                    strColNumber = myRule.CheckedItems
                    Dim objeto As Object = Nothing

                    strColNumber = VarInterReglas.ReconocerVariablesValuesSoloTexto(strColNumber)
                    If Not IsNothing(result) Then
                        strColNumber = Zamba.Core.TextoInteligente.ReconocerCodigo(strColNumber, result)
                    End If
                    If String.IsNullOrEmpty(strColNumber.Trim()) = False Then
                        objeto = VarInterReglas.ReconocerVariablesAsObject(strColNumber)
                        VarInterReglas = Nothing

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
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Objeto: " & objeto.ToString())
                            If objeto.ToString().Contains(",") = False Then
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Cantidad filas: " & Dt.Rows.Count)
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Objeto comparacion:" & objeto.ToString().Trim())
                                For i As Int32 = 0 To Dt.Rows.Count - 1
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor fila(" & i & "): " & Dt.Rows(i)(intColNumber + 1).ToString().Trim())
                                    If String.Compare(Dt.Rows(i)(intColNumber + 1).ToString().Trim(), objeto.ToString().Trim(), True) = 0 Then
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Encontrado")
                                        Dt.Rows(i)(0) = True
                                        Exit For
                                    End If
                                Next
                            Else
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Cantidad filas: " & Dt.Rows.Count)
                                For Each valor As String In objeto.ToString().Split(",")
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Objeto comparacion:" & valor.Trim())
                                    For i As Int32 = 0 To Dt.Rows.Count - 1
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor fila(" & i & "): " & Dt.Rows(i)(intColNumber + 1).ToString().Trim())
                                        If String.Compare(Dt.Rows(i)(intColNumber + 1).ToString().Trim(), valor.Trim(), True) = 0 Then
                                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Encontrado")
                                            Dt.Rows(i)(0) = True
                                            Exit For
                                        End If
                                    Next
                                Next
                            End If
                        End If
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Sin prechequeo")
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
    ''' Devuelve un datatable dependiendo de la configuracion de la regla y el dato obtenido. Esta versión web quita el parámetro showCheckColumns
    ''' </summary>
    ''' <history>
    '''     [Javier] 07/07/2009  Created
    ''' </history>
    Private Function GetDataTableWeb(ByRef data As Object, ByVal myRule As IDOShowTable, ByVal result As IResult) As DataTable
        Dt = New DataTable()

        If (TypeOf data Is Array) Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Es array")
            If Not IsNothing(data) Then
                For Each r As Object In data
                    If Not IsNothing(r) Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Tipo objeto: " & r.GetType().ToString())
                        For Each prop As PropertyInfo In r.GetType().GetProperties()
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Agregando columna: " & prop.Name)
                            Dim dc As New DataColumn(prop.Name)
                            Dt.Columns.Add(dc)
                        Next
                        Exit For
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Objeto en array vacio")
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
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "El array esta vacio")
            End If
            'Verifica si los datos se encuentran nulos
        ElseIf (TypeOf data Is DataSet) Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Es dataset")
            If IsNothing(data) OrElse DirectCast(data, DataSet).Tables(0).Rows.Count = 0 Then
                Return Dt
            Else
                Dt = DirectCast(data, DataSet).Tables(0)
            End If
        Else
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Es otro tipo")
            If Not IsNothing(data) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Tipo: " & data.GetType().ToString())
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Vacio")
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



        'ZTrace.WriteLineIf(ZTrace.IsInfo, "Creando la columna del check.")
        'If Not myRule.ShowDataOnly Then
        '    'Configuracion de la columna
        '    Dim checkCol As DataColumn = New DataColumn("zcheck", GetType(Boolean))
        '    checkCol.AllowDBNull = False
        '    checkCol.DefaultValue = False

        '    Dt.Columns.Add(checkCol)
        '    ZTrace.WriteLineIf(ZTrace.IsInfo, "Columna check agregada")
        '    Dt.Columns("zcheck").SetOrdinal(0)

        '    'Hago el chequeo previo de los items
        '    ZTrace.WriteLineIf(ZTrace.IsInfo, "Valores prechequeo: " & myRule.CheckedItems)
        '    ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor columna prechequeo" & myRule.CheckedItemsColumn)
        '    If String.IsNullOrEmpty(myRule.CheckedItems) = False And String.IsNullOrEmpty(myRule.CheckedItemsColumn) = False Then
        '        Dim strColNumber As String = myRule.CheckedItemsColumn
        '        Dim intColNumber As Int32

        '        If Not IsNothing(result) Then
        '            strColNumber = Zamba.Core.TextoInteligente.ReconocerCodigo(myRule.CheckedItemsColumn, result)
        '        End If
        '        strColNumber = WFRuleParent.ReconocerVariables(strColNumber)

        '        ZTrace.WriteLineIf(ZTrace.IsInfo, "Nro de columna: " & strColNumber)

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
        '                    ZTrace.WriteLineIf(ZTrace.IsInfo,"Objeto: " & objeto.ToString())
        '                    If objeto.ToString().Contains(",") = False Then
        '                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Cantidad filas: " & Dt.Rows.Count)
        '                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Objeto comparacion:" & objeto.ToString().Trim())
        '                        For i As Int32 = 0 To Dt.Rows.Count - 1
        '                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor fila(" & i & "): " & Dt.Rows(i)(intColNumber + 1).ToString().Trim())
        '                            If String.Compare(Dt.Rows(i)(intColNumber + 1).ToString().Trim(), objeto.ToString().Trim(), True) = 0 Then
        '                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Encontrado")
        '                                Dt.Rows(i)(0) = True
        '                                Exit For
        '                            End If
        '                        Next
        '                    Else
        '                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Cantidad filas: " & Dt.Rows.Count)
        '                        For Each valor As String In objeto.ToString().Split(",")
        '                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Objeto comparacion:" & valor.Trim())
        '                            For i As Int32 = 0 To Dt.Rows.Count - 1
        '                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor fila(" & i & "): " & Dt.Rows(i)(intColNumber + 1).ToString().Trim())
        '                                If String.Compare(Dt.Rows(i)(intColNumber + 1).ToString().Trim(), valor.Trim(), True) = 0 Then
        '                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Encontrado")
        '                                    Dt.Rows(i)(0) = True
        '                                    Exit For
        '                                End If
        '                            Next
        '                        Next
        '                    End If
        '                End If
        '            Else
        '                ZTrace.WriteLineIf(ZTrace.IsInfo, "Sin prechequeo")
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
    ''' Pasa los valores de un DataGridViewRow a un array de Object
    ''' </summary>
    ''' <history>
    '''     [Tomas] 11/11/2009  Created
    ''' </history>
    Private Function GetRowValues(ByRef row As DataGridViewRow, ByVal columnsStructure As DataColumnCollection) As Object()
        Dim itemArray(row.Cells.Count - 1) As Object

        'Recorre las celdas del datagridviewrow obteniendo los valores
        For i As Int32 = 0 To row.Cells.Count - 1
            'Si el tipo de dato de la columna es DateTime y el valor se encuentra vacío devuelve nothing
            If String.IsNullOrEmpty(row.Cells(i).Value) AndAlso
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

    Public Property Fc() As IFiltersComponent Implements IFilter.Fc
        Get

        End Get
        Set(ByVal value As IFiltersComponent)

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

    End Sub
End Class
