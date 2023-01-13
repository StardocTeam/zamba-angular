Imports System.IO
Imports System.Text

''' <summary>
''' Proyecto que se encarga de escribir el contenido de muchos scripts en uno solo 
''' y de realizar los reemplazos necesarios.
''' </summary>
''' <remarks></remarks>
''' <history>
''' [Tomás] 16/07/2009 Created
''' </history>
Public Class frmMain

    ''' <summary>
    ''' Agrega la ruta del archivo donde se generará el script
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnOpenFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenFile.Click
        Dim file As New OpenFileDialog()

        file.CheckFileExists = True
        file.CheckPathExists = True
        file.Multiselect = False
        file.Title = "Seleccione el archivo donde se generará el script"
        file.ShowDialog()

        If Not String.IsNullOrEmpty(file.FileName) Then
            txtScript.Text = file.FileName
        End If

        file.Dispose()
    End Sub

    ''' <summary>
    ''' Agrega una ruta de un directorio a la lista
    ''' Ese directorio es aquel que contiene los scripts 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAddPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddPath.Click
        Dim folder As New FolderBrowserDialog()

        folder.Description = "Seleccione el directorio de la ubicación de los scripts"
        folder.ShowNewFolderButton = True
        folder.ShowDialog()

        If Not String.IsNullOrEmpty(folder.SelectedPath) Then
            lstPaths.Items.Add(folder.SelectedPath)
        End If

        folder.Dispose()
    End Sub

    ''' <summary>
    ''' Remueve una ruta de directorio de la lista
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnRemovePath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemovePath.Click
        If lstPaths.SelectedIndex <> -1 Then
            lstPaths.Items.RemoveAt(lstPaths.SelectedIndex)
        End If
        If lstPaths.Items.Count > 0 Then
            lstPaths.SelectedIndex = 0
        End If
    End Sub

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Dim cur As Cursor
        cur = Me.Cursor

        Try
            'Validación de controles
            If lstPaths.Items.Count = 0 Then
                MessageBox.Show("Debe ingresar al menos un directorio de ubicación de scripts.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            If String.IsNullOrEmpty(txtScript.Text) Then
                MessageBox.Show("Debe ingresar un archivo de salida", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            SaveChanges()

            'Cursor de espera
            Me.Cursor = Cursors.WaitCursor
            Me.Enabled = False

            'Objetos que manejan los archivos
            Dim query As New StringBuilder()
            Dim tempQuery As String = String.Empty
            Dim writer As New StreamWriter(txtScript.Text, False, System.Text.Encoding.ASCII)
            Dim toSearch As String = String.Empty
            Dim toReplace As String = String.Empty
            Dim separador As String = String.Empty

            'Se asigna el separador. En caso de no elegir nada queda en String.Empty
            If rdbSQL.Checked Then
                separador = vbCrLf & "GO" & vbCrLf
            ElseIf rdbORA.Checked Then
                separador = vbCrLf & "/" & vbCrLf
            End If

            'Recorre la lista de directorios
            For Each directorio As String In lstPaths.Items
                'Obtiene la información del directorio
                Dim dir As New IO.DirectoryInfo(directorio)
                'Recorre los archivos del directorio
                For Each archivo As IO.FileInfo In dir.GetFiles
                    'Se lee el archivo y se guarda en el script a generar
                    Dim reader As New IO.StreamReader(archivo.FullName)
                    tempQuery = reader.ReadToEnd()

                    'Reemplazo de espacios
                    If chkRepSpaces.Checked Then
                        While tempQuery.ToString.Contains("  ")
                            tempQuery = tempQuery.Replace("  ", " ")
                        End While
                    End If

                    query.AppendLine(tempQuery)
                    query.Append(separador)
                    reader.Close()
                    reader.Dispose()
                    reader = Nothing
                Next
            Next

            'Se valida si se escribió algo en el query
            If String.IsNullOrEmpty(query.ToString) Then
                MessageBox.Show("El texto a escribir se encuentra vacío. Verifique que existan archivos en el o" & vbCrLf & _
                                "los directorios seleccionados o el contenido los mismos.", "Datos inexistentes", _
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Me.Cursor = cur
                Me.Enabled = True
                Exit Sub
            End If

            'Reemplazo de caracteres
            For Each fila As DataGridViewRow In dgvReplaces.Rows
                'Si la celda es null, le agrega un espacio
                If IsNothing(fila.Cells("Search").Value) Then
                    toSearch = " "
                Else
                    toSearch = fila.Cells("Search").Value
                End If
                If IsNothing(fila.Cells("Replace").Value) Then
                    toReplace = " "
                Else
                    toReplace = fila.Cells("Replace").Value
                End If
                'Si ambas son iguales, continua la iteración
                If String.Compare(toSearch, toReplace) = 0 Then
                    Continue For
                End If
                query = query.Replace(toSearch, toReplace)
            Next

            'Se escribe el archivo
            writer.Write(query.ToString)
            MessageBox.Show("El script se ha creado con éxito", "Exito en la operación", MessageBoxButtons.OK, MessageBoxIcon.Information)

            'Se liberan recursos
            writer.Close()
            writer.Dispose()
            writer = Nothing

            Try
                'Se muestra el script
                Process.Start(txtScript.Text)
            Catch ex As Exception
            End Try

        Catch ex As Exception
            MessageBox.Show("Error en la operación" & vbCrLf & "Información técnica: " & ex.ToString & vbCrLf & _
                            ex.Source & vbCrLf & ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = cur
            Me.Enabled = True
        End Try
    End Sub

    ''' <summary>
    ''' Persiste las modificaciones de las rutas y los replaces para la proxima vez que la aplicación se abra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SaveChanges()
        'Rutas
        Dim appPath As String = Application.StartupPath
        Dim sr As StreamWriter
        Try
            sr = New StreamWriter((Path.Combine(appPath, "paths.txt")), False, System.Text.Encoding.UTF8)
            sr.AutoFlush = True
            For Each ruta As String In lstPaths.Items
                sr.WriteLine(ruta)
            Next
            sr.WriteLine("#" & txtScript.Text)
        Catch ex As Exception
        Finally
            If Not IsNothing(sr) Then
                sr.Close()
                sr.Dispose()
                sr = Nothing
            End If
        End Try

        'Replaces
        Dim dt As DataTable = New DataTable("Replaces")
        Dim ser As Xml.Serialization.XmlSerializer = New Xml.Serialization.XmlSerializer(GetType(DataTable))
        Dim tr As TextWriter
        Try
            tr = New StreamWriter(Path.Combine(appPath, "replaces.xml"))
            dt.Columns.Add(New DataColumn("Search"))
            dt.Columns.Add(New DataColumn("Replace"))
            Dim row As DataRow
            Dim toSearch As String = String.Empty
            Dim toReplace As String = String.Empty
            For Each reemplazo As DataGridViewRow In dgvReplaces.Rows
                'Si la celda es null, le agrega un espacio
                If IsNothing(reemplazo.Cells("Search").Value) Then
                    toSearch = " "
                Else
                    toSearch = reemplazo.Cells("Search").Value
                End If
                If IsNothing(reemplazo.Cells("Replace").Value) Then
                    toReplace = " "
                Else
                    toReplace = reemplazo.Cells("Replace").Value
                End If
                'Si ambas son iguales, continua la iteración
                If String.Compare(toSearch, toReplace) = 0 Then
                    Continue For
                End If

                row = dt.NewRow
                row("Search") = toSearch
                row("Replace") = toReplace

                dt.Rows.Add(row)
            Next
            ser.Serialize(tr, dt)
        Catch ex As Exception
        Finally
            If Not IsNothing(tr) Then
                tr.Close()
                tr.Dispose()
                tr = Nothing
            End If
            If Not IsNothing(ser) Then
                ser = Nothing
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Carga los últimos path y replaces hechos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadChanges()
        'Rutas
        Dim appPath As String = Application.StartupPath
        Dim sr As StreamReader
        Try
            sr = New StreamReader((Path.Combine(appPath, "paths.txt")))
            Dim ruta As String
            While Not sr.EndOfStream
                ruta = sr.ReadLine()
                If ruta.StartsWith("#") Then
                    txtScript.Text = ruta.Remove(0, 1)
                Else
                    lstPaths.Items.Add(ruta)
                End If
            End While
        Catch ex As Exception
        Finally
            If Not IsNothing(sr) Then
                sr.Close()
                sr.Dispose()
                sr = Nothing
            End If
        End Try

        'Replaces
        Dim dt As DataTable = New DataTable("Replaces")
        Dim ser As Xml.Serialization.XmlSerializer = New Xml.Serialization.XmlSerializer(GetType(DataTable))
        Dim tr As TextReader
        Try
            tr = New StreamReader(Path.Combine(appPath, "replaces.xml"))
            dt.Columns.Add(New DataColumn("Search"))
            dt.Columns.Add(New DataColumn("Replace"))
            dt = DirectCast(ser.Deserialize(tr), DataTable)
            dgvReplaces.DataSource() = dt
            dgvReplaces.Columns("Search").HeaderText = "Texto a buscar"
            dgvReplaces.Columns("Replace").HeaderText = "Texto de reemplazo"
        Catch ex As Exception
            If dgvReplaces.Columns.Count = 0 Then
                dgvReplaces.Columns.Add("Search", "Texto a buscar")
                dgvReplaces.Columns.Add("Replace", "Texto de reemplazo")
            End If
        Finally
            If Not IsNothing(tr) Then
                tr.Close()
                tr.Dispose()
                tr = Nothing
            End If
            If Not IsNothing(ser) Then
                ser = Nothing
            End If
        End Try
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadChanges()
    End Sub
End Class
