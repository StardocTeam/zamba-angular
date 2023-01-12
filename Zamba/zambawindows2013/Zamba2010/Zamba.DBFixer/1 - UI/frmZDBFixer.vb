Imports System.Text
Imports Zamba.Servers
Imports System.IO

''' <summary>
''' Formulario principal, se encarga de la ejecucion
''' </summary>
''' <history>
''' Marcelo  Modified 09/05/08
''' [Gaston] Modified 13/06/2008
''' </history>
''' <remarks></remarks>
Public Class frmZDBFixer

#Region "Attributes"

    'Flag: Buttons
    Private flagStop As Boolean = False
    Private flagPause As Boolean = False

    'Additional: Errors
    Private errorString As String = String.Empty
    Private errorList As List(Of String)
    Private currentColumn As Column

    'Flag: Options
    Private flagOTableName As Boolean = True
    Private flagOTableCreated As Boolean = False
    Private flagOTableLength As Boolean = False
    Private flagOTablePrimaryKey As Boolean = False
    Private flagORelationsName As Boolean = False
    Private flagOColumnName As Boolean = False
    Private flagOColumnCreated As Boolean = False
    Private flagOColumnType As Boolean = False
    Private flagOColumnLength As Boolean = False
    Private flagOColumnNullable As Boolean = False
    Private flagOColumnDefaultData As Boolean = False
    Private flagOViewName As Boolean = False
    Private flagOSPName As Boolean = False
    Private flagOProcessBeforeQuery As Boolean = True
    Private flagOProcessTables As Boolean = True
    Private flagOProcessStores As Boolean = True
    Private flagOProcessViews As Boolean = True
    Private flagOProcessRelations As Boolean = True
    Private flagOProcessAfterQuery As Boolean = True
    Private flagOErrorComplete As Boolean = True
    Public Shared repeatedObject As Int16

    'Variables Globales
    'Public Shared userId As Int32 = 0
    'Public Shared userName As String = "DBO"
#End Region

#Region "Events"

    Private Sub frmZPaquetes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim sConString As String = Zamba.Servers.Server.Con.ConString
            If Not String.IsNullOrEmpty(sConString) Then
                For Each sConParam As String In sConString.Split(";")
                    If sConParam.Contains("User Id") Then
                        Me.lblUser2.Text = sConParam.Replace("User Id=", "")
                    ElseIf sConParam.Contains("User ID") Then
                        Me.lblUser2.Text = sConParam.Replace("User ID=", "")
                    ElseIf sConParam.Contains("Data Source") Then
                        Me.lblServer2.Text = sConParam.Replace("Data Source=", "")
                    ElseIf sConParam.Contains("Initial Catalog") Then
                        Me.lblDataBase2.Text = sConParam.Replace("Initial Catalog=", "")
                    ElseIf sConParam.Contains("Server=") Then
                        Me.lblDataBase2.Text = sConParam.Replace("Server=", "")
                    End If
                Next
            End If
        Catch
        End Try
        Try
            Me.lblBaseType2.Text = Zamba.Servers.Server.ServerType.ToString()
        Catch
        End Try
        Try
            Me.lblBaseState2.Text = Zamba.Servers.Server.Con.State.ToString()
        Catch
        End Try

        Try
            Dim ttpBtnShowDetails As New ToolTip()
            ttpBtnShowDetails.SetToolTip(Me.btnShowDetails, "Mostrar Detalles")
            Dim ttpBtnFixBase As New ToolTip()
            ttpBtnShowDetails.SetToolTip(Me.btnFixBase, "Comenzar el proceso de arreglo")
        Catch
        End Try

        'userId = DBFixerValidations.GetSQLUserId(Me.lblUser2.Text)
        'userName = DBFixerValidations.GetSQLUserName(userId)
        Try
            'El evento es shared, asi que de nada serviría quitarlo por las dudas,
            'pero como se carga en el load del formulario, nos aseguramos que se 
            'va a colgar una sola vez.
            AddHandler DBFixerValidations.ErrorOccurred, AddressOf ExceptionHandler
        Catch
        End Try

        btnShowDetails_Click(Nothing, System.EventArgs.Empty)
        Me.pgbTablesFixed.Step = 1
        Me.txtDetails.BackColor = Color.White
        Me.txtDetails.Text = "Sin actividad."
        ' btnDetailsOptions_Click(Nothing, System.EventArgs.Empty)

    End Sub

#Region "Buttons"

    Private Sub btnPlayPause_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPlayPause.Click
        If btnPlayPauseIsPlay() Then
            Me.FixDataBaseToolStripMenuItem.Enabled = False
            Me.PauseFixToolStripMenuItem.Enabled = True
            Me.btnPlayPauseAspect(False)
            Me.flagPause = False
            WriteInDetails(Nothing)
            WriteInDetails(Now.ToString() & " . Proceso de arreglo reiniciado por el usuario.")
        Else
            Me.FixDataBaseToolStripMenuItem.Enabled = True
            Me.PauseFixToolStripMenuItem.Enabled = False
            Me.btnPlayPauseAspect(True)
            Me.flagPause = True
            WriteInDetails(Nothing)
            WriteInDetails(Now.ToString() & " . Proceso de arreglo pausado por el usuario.")
        End If
    End Sub

    Private Sub btnStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStop.Click
        Me.flagStop = True
        Me.flagPause = False
        Me.btnStop.Visible = False
        Me.FixDataBaseToolStripMenuItem.Enabled = True
        Me.StopFixToolStripMenuItem.Enabled = False
        Me.PauseFixToolStripMenuItem.Enabled = False
        Me.btnPlayPauseAspect(False)
        Me.btnPlayPause.Visible = False
        Me.btnFixBase.Enabled = True
        Me.pgbTablesFixed.Value = 0
    End Sub

    ''' <summary>
    ''' Comienza el proceso de arreglo de la base de datos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>
    ''' Marcelo  Modified 12/06/2008
    ''' [Gaston] Modified 13/06/2008
    ''' </history>
    ''' <remarks></remarks>
    Private Sub btnFixBase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFixBase.Click

        'Inicializacion
        '********************************************************************************************************
        'Miembros del método: listas.
        Dim spList As List(Of StoredProcedure) = Nothing
        Dim pkgList As List(Of Package) = Nothing
        Dim pkgBodyList As List(Of PackageBody) = Nothing

        Dim vList As List(Of View) = Nothing
        Dim tList As List(Of Table) = Nothing
        Dim fkList As List(Of ForeignKey) = Nothing
        Dim tgList As List(Of Trigger) = Nothing
        Dim uqList As List(Of Unique) = Nothing
        Dim ckList As List(Of Check) = Nothing
        Dim IndexList As List(Of Index) = Nothing

        'Miembros del método: generales
        Dim currentTableName As String = String.Empty
        Dim currentSPName As String = String.Empty
        Dim currentVName As String = String.Empty
        Dim tmpQuery As String = String.Empty

        'Seteo visual: links
        Me.lnkErrorList.Visible = False
        Me.lnkTrace.Visible = False
        'Seteo visual: ToolStripMenu
        Me.StopFixToolStripMenuItem.Enabled = True
        Me.PauseFixToolStripMenuItem.Enabled = True
        Me.FixDataBaseToolStripMenuItem.Enabled = False
        'Seteo visual: Botones
        Me.btnFixBase.Enabled = False
        Me.btnStop.Visible = True
        Me.btnPlayPause.Visible = True

        'Seteo funcional: flag de errores.
        Dim flagErrorInGetSP As Boolean = False
        Dim flagErrorInGetViews As Boolean = False
        Dim flagErrorInGetTables As Boolean = False
        Dim flagErrorInGetRelations As Boolean = False
        'Seteo funcional: lista de errores.
        errorList = New List(Of String)
        '********************************************************************************************************


        WriteInDetails("")
        WriteInDetails(Nothing)
        WriteInDetails(Now.ToString() & " . Estado inicial de la Base de Datos.")
        'Guarda en el trace la cantidad de objetos de la base de datos
        WriteDbObjectsCount()

        WriteInDetails(Now.ToString() & " . Comenzando arreglo de la Base de Datos.")


        'Obtencion de Scripts
        '********************************************************************************************************
        'Obteniendo Stores: si hay algun error el mismo se almacena (errorList) y se sube una bandera (flagErrorInGetSP).
        If Me.flagOProcessStores Then
            Try
                DBFixerParser.GetStoredProcedures(spList, pkgList, pkgBodyList)
            Catch ex As Exception
                flagErrorInGetSP = True
                Dim exBuilder As New System.Text.StringBuilder
                exBuilder.AppendLine()
                exBuilder.Append(Now.ToString() & " Error ")
                exBuilder.Append(errorList.Count + 1)
                exBuilder.Append(" : No se han podido obtener los Stored Procedures.")
                If Me.flagOErrorComplete Then
                    exBuilder.Append("Detalle: ")
                    exBuilder.AppendLine(ex.Message)
                End If
                errorList.Add(exBuilder.ToString())
            End Try
        End If

        'Obteniendo las vistas
        If Me.flagOProcessViews Then
            Try
                vList = DBFixerParser.GetViews()
            Catch ex As Exception
                flagErrorInGetViews = True
                Dim exBuilder As New System.Text.StringBuilder
                exBuilder.AppendLine()
                exBuilder.Append(Now.ToString() & " Error ")
                exBuilder.Append(errorList.Count + 1)
                exBuilder.Append(" : No se han podido obtener las Vistas.")
                If Me.flagOErrorComplete Then
                    exBuilder.Append("Detalle: ")
                    exBuilder.AppendLine(ex.Message)
                End If
                errorList.Add(exBuilder.ToString())
            End Try
        End If

        'Obteniendo las tablas
        If Me.flagOProcessTables Then
            Try
                tList = DBFixerParser.GetTables(Server.isOracle)
            Catch ex As Exception
                flagErrorInGetTables = True
                Dim exBuilder As New System.Text.StringBuilder
                exBuilder.AppendLine()
                exBuilder.Append(Now.ToString() & " Error ")
                exBuilder.Append(errorList.Count + 1)
                exBuilder.Append(" : No se han podido obtener las Tablas.")
                If Me.flagOErrorComplete Then
                    exBuilder.Append("Detalle: ")
                    exBuilder.AppendLine(ex.Message)
                End If
                errorList.Add(exBuilder.ToString())
            End Try
        End If

        'Obteniendo las relaciones
        If Me.flagOProcessRelations Then
            Try
                If flagErrorInGetTables Then
                    tList = New List(Of Table)
                End If
                DBFixerParser.GetGeneral(tList, tgList, fkList, uqList, ckList, IndexList, Server.isOracle)
            Catch ex As Exception
                flagErrorInGetRelations = True
                Dim exBuilder As New System.Text.StringBuilder
                exBuilder.AppendLine()
                exBuilder.Append(Now.ToString() & " Error ")
                exBuilder.Append(errorList.Count + 1)
                exBuilder.Append(" : No se han podido obtener las Relaciones.")
                If Me.flagOErrorComplete Then
                    exBuilder.Append("Detalle: ")
                    exBuilder.AppendLine(ex.Message)
                End If
                errorList.Add(exBuilder.ToString())
            End Try
        End If
        '********************************************************************************************************

        'Traer consultas
        If flagOProcessBeforeQuery Then
            Dim BeforeQueries As List(Of String) = DBFixerParser.GetBeforeQueries()
            WriteInDetails(Now.ToString().Split(" ")(1) & " . Ejecutando Preconsultas.")
            WriteInDetails("Se van a ejecutar " & BeforeQueries.Count.ToString() & " preconsultas")

            'Ejecutar consultas
            For Each Query As String In BeforeQueries
                Try
                    WriteInDetails("Ejecutando: " & Query)
                    DBFixerBusiness.ExecuteQuery(Query)
                Catch ex As Exception
                    ExceptionHandler(ex, String.Empty, String.Empty, Query)
                End Try
            Next

            WriteInDetails("Preconsultas Ejecutadas")
        End If

        'Arreglo de la base
        '********************************************************************************************************
        If flagOProcessTables Then
            If Not flagErrorInGetTables Then
                WriteInDetails(Nothing)
                WriteInDetails(Now.ToString().Split(" ")(1) & " . Procesando Tablas.")
                pgbTablesFixed.Maximum = tList.Count
                pgbTablesFixed.Value = 0
                pgbTablesFixed.Step = 1
                For Each tabla As Table In tList
                    If CheckControlButtons() Then
                        Exit Sub
                    End If
                    currentTableName = tabla.Name
                    'Log
                    If Me.flagOTableName Then WriteInDetails(Now.ToString().Split(" ")(1) & " . Tabla " & tabla.Name & ": Iniciando proceso de arreglo.")
                    'Arregla la tabla
                    FixTable(tabla, tmpQuery)
                    'Log
                    If Me.flagOTableName Then WriteInDetails(Now.ToString().Split(" ")(1) & " . Tabla " & tabla.Name & ": Proceso terminado.")
                    pgbTablesFixed.PerformStep()
                    Application.DoEvents()
                Next
            End If
        End If

        If Me.flagOProcessRelations Then
            If Not flagErrorInGetRelations Then
                WriteInDetails(Nothing)
                WriteInDetails(Now.ToString().Split(" ")(1) & " . Procesando Relaciones.")
                pgbTablesFixed.Maximum = fkList.Count * 3
                pgbTablesFixed.Value = 0
                pgbTablesFixed.Step = 3
                For Each fk As ForeignKey In fkList

                    Try
                        If CheckControlButtons() Then
                            Exit Sub
                        End If
                        If Me.flagORelationsName Then WriteInDetails(Now.ToString().Split(" ")(1) & " . Relacion " & fk.Name & ": Iniciando proceso de arreglo.")
                        If Not DBFixerValidations.DBContainsForeignKey(fk) Then
                            DBFixerBusiness.FixGeneral.CreateForeignKey(fk, tmpQuery)
                        End If
                    Catch ex As Exception

                        ExceptionHandler(ex, "Relación", fk.Name, tmpQuery)
                    End Try
                    pgbTablesFixed.PerformStep()
                    Application.DoEvents()
                    If Me.flagORelationsName Then WriteInDetails(Now.ToString().Split(" ")(1) & " . Relacion " & fk.Name & ": Proceso terminado.")
                Next

                'Creacion de triggers
                For Each tg As Trigger In tgList
                    If Not Server.isOracle Then
                        Try
                            If CheckControlButtons() Then
                                Exit Sub
                            End If
                            If Me.flagORelationsName Then WriteInDetails(Now.ToString().Split(" ")(1) & " . Trigger " & tg.Name & ": Iniciando proceso de arreglo.")
                            If Not DBFixerValidations.DBContainsTrigger(tg) Then
                                DBFixerBusiness.FixGeneral.CreateTrigger(tg)
                            End If
                        Catch ex As Exception
                            ExceptionHandler(ex, "Trigger", tg.Name, tmpQuery)
                        End Try
                    End If
                Next

                'Creacion de unique
                For Each uq As Unique In uqList
                    Try
                        If CheckControlButtons() Then
                            Exit Sub
                        End If
                        If Me.flagORelationsName Then WriteInDetails(Now.ToString().Split(" ")(1) & " . Unique " & uq.Name & ": Iniciando proceso de arreglo.")

                        If Not DBFixerValidations.DBContainsUnique(uq) Then
                            DBFixerBusiness.FixGeneral.CreateUnique(uq)
                        End If
                    Catch sqlex As SqlClient.SqlException
                        ExceptionHandler(sqlex, "Unique", uq.Name, tmpQuery)
                    Catch Oraex As OracleClient.OracleException
                        'En los scripts de sql hay algunas tablas como por ejemplo la USR_RIGHTS que contienen constraints Pk y Uniques
                        'En ese caso oracle permite tener una de las dos, al querer crear una segunda constraint tira exception
                        'ORA-02261: such unique or primary key already exists in the table
                        If Oraex.ErrorCode <> -2146232008 Then
                            ExceptionHandler(Oraex, "Unique", uq.Name, tmpQuery)
                        End If
                    End Try
                Next

                'Creacion de restricciones Check
                For Each ck As Check In ckList
                    Try
                        If CheckControlButtons() Then
                            Exit Sub
                        End If
                        If Me.flagORelationsName Then WriteInDetails(Now.ToString().Split(" ")(1) & " . Check " & ck.Name & ": Iniciando proceso de arreglo.")
                        If Not DBFixerValidations.DBContainsCheck(ck) Then
                            DBFixerBusiness.FixGeneral.CreateCheck(ck)
                        End If
                    Catch ex As Exception
                        ExceptionHandler(ex, "Check", ck.Name, tmpQuery)
                    End Try
                Next

            End If
        End If

        If Me.flagOProcessViews Then
            If Not flagErrorInGetViews Then
                WriteInDetails(Nothing)
                WriteInDetails(Now.ToString().Split(" ")(1) & " . Procesando Vistas.")
                pgbTablesFixed.Maximum = vList.Count
                pgbTablesFixed.Value = 0
                pgbTablesFixed.Step = 1
                For Each v As View In vList
                    Try
                        If CheckControlButtons() Then
                            Exit Sub
                        End If
                        If Me.flagOViewName Then WriteInDetails(Now.ToString().Split(" ")(1) & " . Vista " & v.Name & ": Iniciando proceso de arreglo.")
                        tmpQuery = v.Text
                        If Not DBFixerValidations.DBContainsView(v.Name) Then
                            DBFixerBusiness.FixGeneral.CreateView(v)
                        End If
                        If Me.flagOViewName Then WriteInDetails(Now.ToString().Split(" ")(1) & " . Vista " & v.Name & ": Proceso terminado.")
                    Catch ex As Exception
                        ExceptionHandler(ex, "Vista", v.Name, tmpQuery)
                    End Try
                    pgbTablesFixed.PerformStep()
                    Application.DoEvents()
                Next
            End If
        End If

        If Me.flagOProcessStores Then

            If Not flagErrorInGetSP Then

                WriteInDetails(Nothing)
                WriteInDetails(Now.ToString().Split(" ")(1) & " . Procesando Stores.")
                pgbTablesFixed.Value = 0
                pgbTablesFixed.Step = 1

                ' SQL
                If (Server.isSQLServer) Then

                    If (spList IsNot Nothing) Then verifyStoredProcedures(spList)

                Else

                    If (pkgList IsNot Nothing) Then verifyPackages(pkgList)
                    If (pkgBodyList IsNot Nothing) Then verifyBodyOfPackages(pkgBodyList)

                End If

            End If

        End If

        '********************************************************************************************************

        'Traer consultas
        If flagOProcessAfterQuery Then
            Dim AfterQueries As List(Of String) = DBFixerParser.GetAfterQueries()
            WriteInDetails(Now.ToString().Split(" ")(1) & " . Ejecutando Postconsultas.")
            WriteInDetails("Se van a ejecutar " & AfterQueries.Count.ToString() & " Postconsultas")

            'Ejecutar consultas
            For Each Query As String In AfterQueries
                Try
                    DBFixerBusiness.ExecuteQuery(Query)
                Catch ex As Exception
                    ExceptionHandler(ex, String.Empty, String.Empty, Query)
                End Try
            Next

            WriteInDetails("Postconsultas ejecutadas")
        End If

        '*************************************************************************************************


        'Cargo en el log todos los errores ocurridos en el proceso
        Try
            WriteInDetails(Nothing)
            If errorList.Count = 0 Then
                WriteInDetails(Now.ToString() & " . El proceso de arreglo ha finalizado correctamente.")
            Else
                WriteInDetails(Now.ToString() & " . El proceso de arreglo ha finalizado. Se han producido " & errorList.Count & " errores. Haga click en el link 'Ver Lista de Errores' para poder visualizarlos.")
                LoadErrors(errorList)
            End If
            MessageBox.Show("El proceso de arreglo ha finalizado.", "Zamba DataBase Fixer", MessageBoxButtons.OK)
            RestoreAspect()

        Catch
        End Try
        WriteInDetails(Now.ToString() & " . Estado final de la Base de Datos.")
        'Guarda en el trace la cantidad de objetos de la base de datos
        WriteDbObjectsCount()
    End Sub

    ''' <summary>
    ''' Se recorren los procedimientos almacenados que contiene la lista de procedimientos almacenados y se crea el procedimiento que corresponde
    ''' si no existe en la tabla de sistema de SQL (sysobjects)
    ''' </summary>
    ''' <param name="spList"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	13/06/2008	Modified    Se creo un método para este código que antes estaba contenido en el evento btnFixBase_Click
    ''' </history>
    Private Sub verifyStoredProcedures(ByRef spList As List(Of StoredProcedure))

        pgbTablesFixed.Maximum = spList.Count
        Dim tmpquery As String = String.Empty

        For Each sp As StoredProcedure In spList

            Try

                If CheckControlButtons() Then
                    Exit Sub
                End If

                If Me.flagOSPName Then WriteInDetails(Now.ToString().Split(" ")(1) & " . Store " & sp.Name & ": Iniciando proceso de arreglo.")

                tmpquery = sp.Text

                If Not (DBFixerValidations.DBContainsStoredProcedure(sp.Name)) Then
                    WriteInDetails(Now.ToString() & " . Creando Procedimiento:" & sp.Name)
                    DBFixerBusiness.FixGeneral.createStoredProcedure(sp.Text)
                End If

                If Me.flagOSPName Then WriteInDetails(Now.ToString().Split(" ")(1) & " . Store " & sp.Name & ": Proceso terminado.")

            Catch ex As Exception
                ExceptionHandler(ex, "Store", sp.Name, tmpquery)
            End Try

            pgbTablesFixed.PerformStep()
            Application.DoEvents()

        Next

    End Sub

    ''' <summary>
    ''' Se recorren los paquetes que contiene pkgList y se crea el paquete si no existe en la tabla de sistema de Oracle (DBA_SOURCE)
    ''' </summary>
    ''' <param name="pkgList"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	13/06/2008	Created
    ''' </history>
    Private Sub verifyPackages(ByRef pkgList As List(Of Package))

        pgbTablesFixed.Maximum = pkgList.Count
        Dim tmpquery As String = String.Empty

        For Each pkg As Package In pkgList

            Try

                If CheckControlButtons() Then
                    Exit Sub
                End If

                If Me.flagOSPName Then WriteInDetails(Now.ToString().Split(" ")(1) & " . Package " & pkg.Name & ": Iniciando proceso de arreglo.")

                tmpquery = pkg.Text

                If Not (DBFixerValidations.DBContainsPackageOrBodyOfPackage(pkg.Name, "Package")) Then
                    WriteInDetails(Now.ToString() & " . Creando Paquete:" & pkg.Name)
                    DBFixerBusiness.FixGeneral.createStoredProcedure(pkg.Text)
                End If

                If Me.flagOSPName Then WriteInDetails(Now.ToString().Split(" ")(1) & " . Package " & pkg.Name & ": Proceso terminado.")

            Catch ex As Exception
                ExceptionHandler(ex, "Package", pkg.Name, tmpquery)
            End Try

            pgbTablesFixed.PerformStep()
            Application.DoEvents()
        Next

    End Sub

    ''' <summary>
    ''' Se recorren los cuerpos de paquetes que contiene el pkgBodyList y se crea el cuerpo de paquete si no existe en la tabla de sistema de Oracle
    ''' (DBA_SOURCE)
    ''' </summary>
    ''' <param name="pkgBodyList"></param>
    ''' <remarks></remarks
    ''' <history>
    ''' 	[Gaston]	13/06/2008	Created
    ''' </history>
    Private Sub verifyBodyOfPackages(ByRef pkgBodyList As List(Of PackageBody))

        pgbTablesFixed.Maximum = pkgBodyList.Count
        Dim tmpquery As String = String.Empty

        For Each pkgBody As PackageBody In pkgBodyList

            Try

                If CheckControlButtons() Then
                    Exit Sub
                End If

                If Me.flagOSPName Then WriteInDetails(Now.ToString().Split(" ")(1) & " . Package Body " & pkgBody.Name & ": Iniciando proceso de arreglo.")
                tmpquery = pkgBody.Text

                If Not (DBFixerValidations.DBContainsPackageOrBodyOfPackage(pkgBody.Name, "Package Body")) Then
                    WriteInDetails(Now.ToString() & " . Creando Cuerpo de Paquete:" & pkgBody.Name)
                    DBFixerBusiness.FixGeneral.createStoredProcedure(pkgBody.Text)
                End If

                If Me.flagOSPName Then WriteInDetails(Now.ToString().Split(" ")(1) & " . Package Body " & pkgBody.Name & ": Proceso terminado.")

            Catch ex As Exception
                ExceptionHandler(ex, "Package Body", pkgBody.Name, tmpquery)
            End Try

            pgbTablesFixed.PerformStep()
            Application.DoEvents()
        Next

    End Sub

    ''' <summary>
    ''' Muestra en un textbox los detalles de la ejecucion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnShowDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowDetails.Click
        If Me.txtDetails.Visible Then
            Me.txtDetails.Visible = False
            Me.VerDetallesToolStripMenuItem.Checked = False
            Dim frmSize As New Size(Me.Size.Width, Me.Size.Height - Me.txtDetails.Size.Height - 16)
            Me.Size = frmSize
        Else
            Me.txtDetails.Visible = True
            Me.VerDetallesToolStripMenuItem.Checked = True
            Dim frmSize As New Size(Me.Size.Width, Me.Size.Height + Me.txtDetails.Size.Height + 16)
            Me.Size = frmSize
        End If
    End Sub

    Private Sub btnDetailsOptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        OpcionesToolStripMenuItem_Click(Nothing, System.EventArgs.Empty)
    End Sub

    Private Sub btnExitOptionDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        btnDetailsOptions_Click(Nothing, System.EventArgs.Empty)
    End Sub

#End Region

#Region "Labels"

    Private Sub lnkErrorList_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkErrorList.LinkClicked
        Dim frmErrors As New frmZDBFixerErrors(Me.errorString)
        frmErrors.StartPosition = FormStartPosition.CenterScreen
        frmErrors.ShowDialog()
    End Sub

    Private Sub lnkTrace_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkTrace.LinkClicked
        Dim tmpSFD As New SaveFileDialog()
        tmpSFD.Filter = "txt Files (*.txt)|*.txt|All Files (*.*)|*.*"
        tmpSFD.FilterIndex = 0
        tmpSFD.RestoreDirectory = True
        tmpSFD.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        tmpSFD.DefaultExt = "*.txt"
        tmpSFD.FileName = "ZDBFixer.Trace"
        If tmpSFD.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Try
                If Not String.IsNullOrEmpty(tmpSFD.FileName) Then
                    Dim traceFileStream As New IO.FileInfo(tmpSFD.FileName)
                    Dim traceStreamWriter As StreamWriter = traceFileStream.CreateText()
                    traceStreamWriter.Write(Me.txtDetails.Text)
                    traceStreamWriter.Close()
                    MessageBox.Show("Se ha creado el archivo correctamente.", "Zamba DBFixer", MessageBoxButtons.OK)
                Else
                    MessageBox.Show("No ha seleccionado ningún nombre para el archivo, el archivo no puede ser creado.", "Zamba DBFixer", MessageBoxButtons.OK)
                End If
            Catch
                MessageBox.Show("Ha ocurrido un error al guardar el archivo. Favor de intentarlo nuevamente", "Zamba DBFixer", MessageBoxButtons.OK)
            End Try
        End If
    End Sub

#End Region

#Region "ToolStripMenu"

    Private Sub FixDataBaseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FixDataBaseToolStripMenuItem.Click
        If btnPlayPauseIsPlay() Then
            'Esta condicion significa que el proceso esta pausado
            btnPlayPause_Click(Nothing, System.EventArgs.Empty)
        Else
            FixDataBaseToolStripMenuItem.Enabled = False
            btnFixBase_Click(Nothing, System.EventArgs.Empty)
        End If
    End Sub

    Private Sub StopFixToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StopFixToolStripMenuItem.Click
        btnStop_Click(Nothing, System.EventArgs.Empty)
    End Sub

    Private Sub CerrarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CerrarToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub VerDetallesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VerDetallesToolStripMenuItem.Click
        If Me.VerDetallesToolStripMenuItem.Checked Then
            If Me.txtDetails.Visible Then
                Me.VerDetallesToolStripMenuItem.Checked = Not Me.VerDetallesToolStripMenuItem.Checked
                btnShowDetails_Click(Nothing, System.EventArgs.Empty)
            End If
        Else
            If Not Me.txtDetails.Visible Then
                Me.VerDetallesToolStripMenuItem.Checked = Not Me.VerDetallesToolStripMenuItem.Checked
                btnShowDetails_Click(Nothing, System.EventArgs.Empty)
            End If
        End If
    End Sub

    Private Sub PauseFixToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PauseFixToolStripMenuItem.Click
        btnPlayPause_Click(Nothing, System.EventArgs.Empty)
    End Sub

    Private Sub OpcionesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpcionesToolStripMenuItem.Click
        Dim frmOpciones As New frmZDBFixerOptions()
        LoadOptions(frmOpciones, False)
        frmOpciones.StartPosition = FormStartPosition.CenterScreen
        If Windows.Forms.DialogResult.OK = frmOpciones.ShowDialog() Then
            LoadOptions(frmOpciones, True)
        End If
    End Sub

    Private Sub ExportarDetallesAArchivoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        lnkTrace_LinkClicked(Nothing, Nothing)
    End Sub

    Private Sub TablasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TablasToolStripMenuItem.Click
        Try
            Dim reader As New IO.StreamReader(Application.StartupPath & "\tables.sql")
            Dim tmpText As String = reader.ReadToEnd()
            Dim frmTemp As New frmZDBFixerErrors(tmpText, "Archivo de Tablas")
            frmTemp.ShowDialog()
        Catch ex As Exception
            Dim sBuilder As New StringBuilder()
            sBuilder.Append("El archivo de Tablas no pudo ser encontrado en:")
            sBuilder.Append(Application.StartupPath & "\tables.sql")
            MessageBox.Show(sBuilder.ToString())
        End Try
    End Sub

    Private Sub VistasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VistasToolStripMenuItem.Click
        Try
            Dim reader As New IO.StreamReader(Application.StartupPath & "\stores.sql")
            Dim tmpText As String = reader.ReadToEnd()
            Dim frmTemp As New frmZDBFixerErrors(tmpText, "Archivo de Vistas")
            frmTemp.ShowDialog()
        Catch ex As Exception
            Dim sBuilder As New StringBuilder()
            sBuilder.Append("El archivo de Vistas no pudo ser encontrado en:")
            sBuilder.Append(Application.StartupPath & "\vistas.sql")
            MessageBox.Show(sBuilder.ToString())
        End Try
    End Sub

    Private Sub StoredProceduresToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StoredProceduresToolStripMenuItem.Click
        Try
            Dim reader As New IO.StreamReader(Application.StartupPath & "\stores.sql")
            Dim tmpText As String = reader.ReadToEnd()
            Dim frmTemp As New frmZDBFixerErrors(tmpText, "Archivo de Stored Procedures")
            frmTemp.ShowDialog()
        Catch ex As Exception
            Dim sBuilder As New StringBuilder()
            sBuilder.Append("El archivo de Stored Procedures no pudo ser encontrado en:")
            sBuilder.Append(Application.StartupPath & "\stores.sql")
            MessageBox.Show(sBuilder.ToString())
        End Try
    End Sub

    Private Sub VerListaDeErroresToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VerListaDeErroresToolStripMenuItem.Click
        lnkErrorList_LinkClicked(Nothing, Nothing)
    End Sub

    Private Sub VerExploradorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VerExploradorToolStripMenuItem.Click
        Dim tmpTList As Generic.List(Of Table)
        Dim tmpVList As Generic.List(Of View)
        Dim tmpSPList As Generic.List(Of StoredProcedure)
        Dim tmpPkgList As Generic.List(Of Package)
        Dim tmpPkgBodyList As Generic.List(Of PackageBody)
        Try
            tmpTList = DBFixerParser.GetTables(Server.isOracle)
        Catch ex As Exception
            tmpTList = Nothing
        End Try
        Try
            tmpVList = DBFixerParser.GetViews()
        Catch ex As Exception
            tmpVList = Nothing
        End Try
        Try
            DBFixerParser.GetStoredProcedures(tmpSPList, tmpPkgList, tmpPkgBodyList)
        Catch ex As Exception
            tmpSPList = Nothing
        End Try
        'TODO FALTARIA AGREGAR LOS PAQUETES PARA ORACLE
        Dim newFrmExplorer As New frmZDBFixerExplorer(tmpTList, tmpVList, tmpSPList)
        newFrmExplorer.ShowDialog()

    End Sub

    Private Sub InformaciónGeneralToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InformaciónGeneralToolStripMenuItem.Click
        Dim sR As New StreamReader(Path.Combine(Application.StartupPath, "Zamba.DBFixer.Help.txt"))
        Dim newFrm As New frmZDBFixerErrors(sR.ReadToEnd(), "Ayuda de Zamba DBFixer")
        newFrm.Show()
    End Sub

#End Region
#End Region

#Region "Generals"
    Private Function CheckControlButtons() As Boolean
        If Me.flagStop Then
            WriteInDetails(Nothing)
            WriteInDetails(Now.ToString() & " . Proceso de arreglo detenido por el usuario.")
            flagStop = False
            Return True
        End If
        While Me.flagPause
            Application.DoEvents()
            If Me.flagStop Then
                WriteInDetails(Nothing)
                WriteInDetails(Now.ToString() & " . Proceso de arreglo detenido por el usuario.")
                flagStop = False
                Return True
            End If
        End While
        Return False
    End Function


    ''' <summary>
    ''' Metodo que arregla cada table de la base
    ''' </summary>
    ''' <param name="tabla"></param>
    ''' <param name="tmpQuery"></param>
    ''' <remarks></remarks>
    ''' <history> 
    '''      [Gaston]   26/06/2008   Modified   se agrego código de validación para exception
    ''' </history>
    Private Sub FixTable(ByRef tabla As Table, Optional ByRef tmpQuery As String = "")
        Try
            Dim tablename1 As String = tabla.Name
            Dim tablename2 As String = tablename1

            'Si la base no contiene la tabla, la creo desde cero
            If Not DBFixerValidations.DBContainsTable(tabla.Name) Then
                If Me.flagOTableCreated Then WriteInDetails(Now.ToString().Split(" ")(1) & " . Tabla " & tabla.Name & ": Creando Tabla.")
                DBFixerBusiness.FixTable.CreateTable(tabla, tmpQuery)
            Else
                For Each col As Column In tabla.Columns
                    'Se guarda la columna en uso en una variable global para poder utilizarla fuera del método
                    '(caso de ExceptionHandler que puede levantarse por un evento)
                    currentColumn = col

                    If Me.flagOColumnName Then WriteInDetails(Now.ToString().Split(" ")(1) & " . Tabla " & tabla.Name & ": Verificando Columna '" & col.Name & "'.")

                    'Si no existe la columna, la creo
                    If DBFixerValidations.DBContainsColumn(col.Name, tabla.Name) = False Then
                        If Me.flagOColumnCreated Then WriteInDetails(Now.ToString().Split(" ")(1) & " . Tabla " & tabla.Name & ": Creando Columna '" & col.Name & "'.")
                        Try
                            DBFixerBusiness.FixTable.AddColumn(tabla.Name, col, tmpQuery)
                        Catch ex As Exception
                            ExceptionHandler(ex, "Tabla", tabla.Name, tmpQuery)
                        End Try
                    Else

                        'Si el tipo de la columna no coincide
                        If Not DBFixerValidations.IsColumnType(col.Name, tabla.Name, col.Type(Server.isOracle)) Then
                            If Me.flagOColumnType Then WriteInDetails(Now.ToString().Split(" ")(1) & " . Tabla " & tabla.Name & ": Cambiando tipo a '" & col.Name & "'.")
                            Try
                                DBFixerBusiness.FixColumn.AlterType(col, tabla.Name, tmpQuery)
                            Catch ex As Exception
                                ' ERROR 1: Si el error no es "column to be modified must be empty to decrease precision or scale"
                                ' ERROR 2: Si el error no es "column to be modified must be empty to change datatype" 
                                ' ERROR 3: Si el error no es "cannot enable (string.string) - null values found"
                                'ORA-01408: Such Column List Already Indexed se puede dar al queres crear un indice de una columna Ya indexada
                                If Server.isOracle AndAlso ex.Message.ToUpper.StartsWith("ORA-01408:") Then Exit Try
                                If ((ex.Message.Contains("ORA-01440") <> True) And (ex.Message.Contains("ORA-01439") <> True) And _
                                   (ex.Message.Contains("ORA-02296") <> True)) Then
                                    ExceptionHandler(ex, "Tabla", tabla.Name, tmpQuery)
                                End If
                            End Try
                        End If

                        'Si la precision no coincide
                        If (col.Precition <> -1) Then
                            If Not DBFixerValidations.IsColumnLength(col, tabla.Name) Then
                                If Me.flagOColumnLength Then WriteInDetails(Now.ToString().Split(" ")(1) & " . Tabla " & tabla.Name & ": Cambiando length a '" & col.Name & "'.")

                                Try
                                    DBFixerBusiness.FixColumn.AlterLength(col, tabla.Name, tmpQuery)
                                Catch ex As Exception
                                    'ORA-01408: Such Column List Already Indexed se puede dar al queres crear un indice de una columna Ya indexada
                                    If Server.isOracle AndAlso ex.Message.ToUpper.StartsWith("ORA-01408:") Then Exit Try
                                    If ((ex.Message.Contains("ORA-01440") <> True) And (ex.Message.Contains("ORA-01439") <> True) And _
                                       (ex.Message.Contains("ORA-02296") <> True)) Then
                                        ExceptionHandler(ex, "Tabla", tabla.Name, tmpQuery)
                                    End If
                                End Try
                            End If
                        Else

                            If (col.Precition = -1) Then
                                If Me.flagOColumnLength Then WriteInDetails(Now.ToString().Split(" ")(1) & " . Tabla " & tabla.Name & ": La Columna '" & col.Name & "' no posee definición de Length.")
                            End If

                        End If

                        'Si la columna es nullable
                        'If Not DBFixerValidations.IsColumnNullable(col.Name, tabla.Name, col.getIsNull(), userId) Then
                        If Not DBFixerValidations.IsColumnNullable(col.Name, tabla.Name, col.getIsNull()) Then
                            If Me.flagOColumnNullable Then WriteInDetails(Now.ToString().Split(" ")(1) & " . Tabla " & tabla.Name & ": Cambiando nullable a '" & col.getIsNull & "'.")
                            Try
                                DBFixerBusiness.FixColumn.AlterIsNull(col, tabla.Name, tmpQuery)
                            Catch ex As Exception

                                'ORA-01408: Such Column List Already Indexed se puede dar al queres crear un indice de una columna Ya indexada
                                If Server.isOracle AndAlso ex.Message.ToUpper.StartsWith("ORA-01408:") Then Exit Try
                                If ((ex.Message.Contains("ORA-01440") <> True) And (ex.Message.Contains("ORA-01439") <> True) And _
                                   (ex.Message.Contains("ORA-02296") <> True)) Then
                                    ExceptionHandler(ex, "Tabla", tabla.Name, tmpQuery)
                                End If

                            End Try
                        End If
                        'Si el valor por defecto fue modificado
                        If Not String.IsNullOrEmpty(col.DefaultData) Then
                            'If Not DBFixerValidations.IsColumnDefaultData(col.Name, tabla.Name, col.DefaultData, userId) Then
                            If Not DBFixerValidations.IsColumnDefaultData(col.Name, tabla.Name, col.DefaultData) Then
                                If Me.flagOColumnDefaultData Then WriteInDetails(Now.ToString().Split(" ")(1) & " . Tabla " & tabla.Name & ": Cambiando default a '" & col.DefaultData & "'.")
                                Try
                                    DBFixerBusiness.FixColumn.AlterDefault(col, tabla.Name, tmpQuery)
                                Catch ex As Exception
                                    ExceptionHandler(ex, "Tabla", tabla.Name, tmpQuery)
                                End Try
                            End If
                        End If

                    End If
                Next

                'Verifico las primary keys
                If Not IsNothing(tabla.PrimaryKeyColumnNames) AndAlso tabla.PrimaryKeyColumnNames.Count > 0 Then
                    Dim pkCols As New List(Of Column)
                    For Each columnname As String In tabla.PrimaryKeyColumnNames
                        For Each c As Column In tabla.Columns
                            If columnname.ToUpper = c.Name.ToUpper Then
                                pkCols.Add(c)
                                Exit For
                            End If
                        Next
                    Next

                    Dim pkey As New PrimaryKey(pkCols, tabla)
                    If Not DBFixerValidations.DBContainsPrimaryKey(pkey) Then
                        Try
                            DBFixerBusiness.FixGeneral.CreatePrimaryKey(pkey, tmpQuery)
                        Catch ex As Exception
                            'If Server.isOracle AndAlso ex.Message.StartsWith("ORA-02437:") Then Exit Try
                            ExceptionHandler(ex, "Tabla", tabla.Name, tmpQuery)
                        End Try
                    End If
                End If
            End If
        Catch ex As Exception

            ' Si el error no es column to be modified must be empty to decrease precision or scale
            If Not (ex.Message.Contains("ORA-01440")) Then
                ExceptionHandler(ex, "Tabla", tabla.Name, tmpQuery)
            End If

        End Try
    End Sub

    ''' <summary>
    ''' Maneja las exceptions ocurridas en el proceso
    ''' </summary>
    ''' <param name="ex"></param>
    ''' <param name="element"></param>
    ''' <param name="elementName"></param>
    ''' <param name="tmpQuery"></param>
    ''' <param name="additional"></param>
    ''' <remarks></remarks>
    Private Sub ExceptionHandler(ByVal ex As Exception, ByVal element As String, ByVal elementName As String, Optional ByVal tmpQuery As String = "No se ha podido obtener la consulta.", Optional ByVal additional As String = "")
        Dim flagHandle As Boolean = False
        Dim exType As DBFixerParser.ExceptionTypes
        exType = DBFixerParser.GetExceptionType(ex.GetType.ToString)

        Select Case exType
            Case DBFixerParser.ExceptionTypes.SystemDataSqlClientSqlException
                Dim exNumber As Int32 = DirectCast(ex, System.Data.SqlClient.SqlException).Number
                additional = "[" & exNumber & "]"
                flagHandle = DBFixerParser.HandleSQLException(exNumber, currentColumn, tmpQuery)
            Case DBFixerParser.ExceptionTypes.SystemDataOracleClientException
                Dim exNumber As Int32 = DirectCast(ex, System.Data.OracleClient.OracleException).Code
                additional = "[" & exNumber & "]"
                'DBFixerParser.HandleOracleException(exNumber, userId, currentColumn)
                flagHandle = DBFixerParser.HandleOracleException(exNumber, currentColumn)
        End Select


        If flagHandle = False Then
            Zamba.AppBlock.ZException.Log(ex)
            Dim exBuilder As New System.Text.StringBuilder
            exBuilder.AppendLine()
            exBuilder.Append(Now.ToString() & " Error ")
            exBuilder.Append(errorList.Count + 1)
            exBuilder.Append(" (")
            exBuilder.Append(element)
            exBuilder.Append(" '")
            exBuilder.Append(elementName)
            exBuilder.Append("'): ")
            If Not String.IsNullOrEmpty(additional) Then
                exBuilder.Append(additional)
            End If
            exBuilder.AppendLine(ex.Message)
            If Me.flagOErrorComplete Then
                exBuilder.AppendLine("Consulta: ")
                exBuilder.AppendLine(tmpQuery)
            End If
            errorList.Add(exBuilder.ToString())
        End If
    End Sub

    Private Sub WriteInDetails(ByVal sToWrite As String)
        If Not String.IsNullOrEmpty(sToWrite) Then
            Me.txtDetails.Text = sToWrite + Me.txtDetails.Text
            Me.txtDetails.Text = Environment.NewLine + Me.txtDetails.Text
        ElseIf String.Compare(sToWrite, "") = 0 Then
            Me.txtDetails.Text = ""
        ElseIf IsNothing(sToWrite) Then
            Me.txtDetails.Text = Environment.NewLine + Me.txtDetails.Text
        End If
    End Sub

    Private Sub LoadErrors(ByVal errorList As List(Of String))
        Me.lnkErrorList.Visible = True
        Me.VerListaDeErroresToolStripMenuItem.Enabled = True
        errorString = String.Empty
        Dim eBuilder As New StringBuilder()
        For Each e As String In errorList
            eBuilder.Append(e)
        Next
        errorString = eBuilder.ToString()
    End Sub

    Private Sub RestoreAspect()
        Me.btnPlayPause.Visible = False
        Me.btnStop.Visible = False
        Me.btnFixBase.Enabled = True
        Me.pgbTablesFixed.Value = 0
        Me.ExportarDetallesAArchivoToolStripMenuItem.Enabled = True
        Me.lnkTrace.Visible = True
        Me.FixDataBaseToolStripMenuItem.Enabled = True
        Me.StopFixToolStripMenuItem.Enabled = False
        Me.PauseFixToolStripMenuItem.Enabled = False
    End Sub

    Private Sub btnPlayPauseAspect(ByVal isPlay As Boolean)
        If isPlay Then
            Me.btnPlayPause.Text = ">"
            Me.btnPlayPause.TextAlign = ContentAlignment.MiddleRight
        Else
            Me.btnPlayPause.Text = "||"
            Me.btnPlayPause.TextAlign = ContentAlignment.MiddleCenter
        End If
    End Sub

    Private Function btnPlayPauseIsPlay() As Boolean
        If Me.btnPlayPause.Text.Contains(">") Then
            Return True
        End If
    End Function

    Private Sub LoadOptions(ByRef frmOptions As frmZDBFixerOptions, ByVal getValues As Boolean)
        If Not IsNothing(frmOptions) Then
            If getValues Then
                Me.flagOColumnCreated = frmOptions.ucDetailsOptions.chkVColumnCreated.Checked
                Me.flagOColumnDefaultData = frmOptions.ucDetailsOptions.chkVColumnDefault.Checked
                Me.flagOColumnLength = frmOptions.ucDetailsOptions.chkVColumnLength.Checked
                Me.flagOColumnName = frmOptions.ucDetailsOptions.chkVColumnName.Checked
                Me.flagOColumnNullable = frmOptions.ucDetailsOptions.chkVColumnNullable.Checked
                Me.flagOColumnType = frmOptions.ucDetailsOptions.chkVColumnType.Checked
                Me.flagOSPName = frmOptions.ucDetailsOptions.chkVStoreName.Checked
                Me.flagOTableCreated = frmOptions.ucDetailsOptions.chkVTableCreated.Checked
                Me.flagOTableName = frmOptions.ucDetailsOptions.chkVTableName.Checked
                Me.flagOTableLength = frmOptions.ucDetailsOptions.chkVTableLength.Checked
                Me.flagOTablePrimaryKey = frmOptions.ucDetailsOptions.chkVTablePK.Checked
                Me.flagOViewName = frmOptions.ucDetailsOptions.chkVViewName.Checked
                Me.flagOErrorComplete = frmOptions.ucDetailsOptions.chkVErrorComplete.Checked
                Me.flagOProcessTables = frmOptions.chkProcessTables.Checked
                Me.flagOProcessViews = frmOptions.chkProcessViews.Checked
                Me.flagOProcessStores = frmOptions.chkProcessStores.Checked
                Me.flagOProcessRelations = frmOptions.chkProcessRelations.Checked
            Else
                frmOptions.ucDetailsOptions.chkVErrorComplete.Checked = Me.flagOErrorComplete
                frmOptions.ucDetailsOptions.chkVColumnCreated.Checked = Me.flagOColumnCreated
                frmOptions.ucDetailsOptions.chkVColumnDefault.Checked = Me.flagOColumnDefaultData
                frmOptions.ucDetailsOptions.chkVColumnLength.Checked = Me.flagOColumnLength
                frmOptions.ucDetailsOptions.chkVColumnName.Checked = Me.flagOColumnName
                frmOptions.ucDetailsOptions.chkVColumnNullable.Checked = Me.flagOColumnNullable
                frmOptions.ucDetailsOptions.chkVColumnType.Checked = Me.flagOColumnType
                frmOptions.ucDetailsOptions.chkVStoreName.Checked = Me.flagOSPName
                frmOptions.ucDetailsOptions.chkVTableCreated.Checked = Me.flagOTableCreated
                frmOptions.ucDetailsOptions.chkVTableName.Checked = Me.flagOTableName
                frmOptions.ucDetailsOptions.chkVTableLength.Checked = Me.flagOTableLength
                frmOptions.ucDetailsOptions.chkVTablePK.Checked = Me.flagOTablePrimaryKey
                frmOptions.ucDetailsOptions.chkVViewName.Checked = Me.flagOViewName
                frmOptions.chkProcessTables.Checked = Me.flagOProcessTables
                frmOptions.chkProcessViews.Checked = Me.flagOProcessViews
                frmOptions.chkProcessStores.Checked = Me.flagOProcessStores
                frmOptions.chkProcessRelations.Checked = Me.flagOProcessRelations
            End If
        End If
    End Sub


    ''' <summary>
    ''' Escribe en el trace la cantidad de objetos existentes en la base de datos agrupados por Objeto
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub WriteDbObjectsCount()
        If Server.isSQLServer Then
            Dim query As New StringBuilder
            Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, "select xtype,count(name) from sysobjects group by xtype order by xtype")

            For Each r As DataRow In ds.Tables(0).Rows

                Select Case r.Item(0).ToString.ToUpper.TrimEnd

                    Case "C"
                        query.AppendLine("CHECK: " & r.Item(1).ToString)
                    Case "D"
                        query.AppendLine("DEFAULT: " & r.Item(1).ToString)
                    Case "F"
                        query.AppendLine("FOREIGN KEY: " & r.Item(1).ToString)
                    Case "FN"
                        query.AppendLine("SCALAR FUNCTION: " & r.Item(1).ToString)
                    Case "P"
                        query.AppendLine("STORED PROCEDURE: " & r.Item(1).ToString)
                    Case "PK"
                        query.AppendLine("PRIMARY KEY: " & r.Item(1).ToString)
                    Case "TR"
                        query.AppendLine("TRIGGER: " & r.Item(1).ToString)
                    Case "U"
                        query.AppendLine("TABLAS DE USUARIO: " & r.Item(1).ToString)
                    Case "UQ"
                        query.AppendLine("UNIQUE: " & r.Item(1).ToString)
                    Case "V"
                        query.AppendLine("VISTAS: " & r.Item(1).ToString)
                    Case Else
                        query.AppendLine(r.Item(0).ToString & r.Item(1).ToString)
                End Select
            Next
            WriteInDetails(query.ToString)
        End If
    End Sub



#End Region

    Private Sub PaquetesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PaquetesToolStripMenuItem.Click
        Dim frm As New Zamba.Paquetes.frmActualizaciones()
        frm.ShowDialog()
    End Sub
End Class