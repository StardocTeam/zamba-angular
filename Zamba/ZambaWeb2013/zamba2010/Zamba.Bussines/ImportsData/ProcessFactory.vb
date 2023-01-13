Imports Zamba.Data
Imports System
Imports System.Data
Imports Zamba.Core
Imports Zamba.Servers
Imports Zamba.DocTypes.Factory
Imports Zamba.Membership

Public Class ProcessFactory
    Private Sub New()
    End Sub


    Public Shared Sub SaveProcess(ByVal Process As Process)

        Try
            If IsNothing(Process.MultipleCaracter) OrElse Process.MultipleCaracter.Trim = "" Then Process.MultipleCaracter = " "
            If IsNothing(Process.Path) Then Process.Path = String.Empty

            Process.ID = CoreData.GetNewID(IdTypes.IPTYPEID)
            Dim StrInsert As String
            StrInsert = "INSERT INTO Ip_type (IP_ID,IP_NAME, IP_PATH, IP_CHR, IP_DOCTYPEID, IP_MOVE, IP_VERIFY, IP_ACEPTBLANK, IP_BACKUP,IP_DELSOURCE, IP_SOURCEVARIABLE,IP_MULTIPLEFILES,IP_MULTIPLECHR,IP_BACKUPPATH,IP_GROUP,IP_CHECKBATCH) VALUES (" _
            & Process.ID & ",'" & Process.Name.Trim & "','" & Process.Path.Trim & "','" & Process.Caracter.Trim & "'," & Process.DocType.ID & ", " & CInt(Process.Move) & ", " & CInt(Process.Verify) & ", " & CInt(Process.FlagAceptBlankData) & ", " & CInt(Process.FlagBackUp) & ", " & CInt(Process.FlagDelSourceFile) & ", " & CInt(Process.FlagSourceVariable) & ", " & CInt(Process.FlagMultipleFiles) & ",'" & Process.MultipleCaracter.Trim & "','" & Process.BackUpPath.Trim & "',0," & CInt(Process.CheckBatch) & ")"

            Server.Con.ExecuteNonQuery(CommandType.Text, StrInsert)

            Dim i As Integer
            Dim order As Integer = 0
            For i = 0 To Process.Index.Count - 1
                StrInsert = "INSERT INTO Ip_Index (IP_ID, Array_ID, Index_ID, Index_Order) VALUES (" & Process.ID & ", '" & Trim(Process.Index.Item(i)) & "', '" & Trim(Process.Index.Item(i)) & "', " & order + 1 & ")"
                Server.Con.ExecuteNonQuery(CommandType.Text, StrInsert)
                order = order + 1
            Next
        Catch ex As Exception
            Throw New Exception("Error al guardar el Proceso: " & ex.ToString)
        End Try
    End Sub
    Public Shared Sub SaveEditedProcess(ByVal Process As Process)


        If Process.MultipleCaracter.Trim = "" Then Process.MultipleCaracter = " "

        'agregos los datos de la importacion
        Dim strupdate As String
        strupdate = "UPDATE Ip_Type SET IP_NAME = '" & Process.Name.Trim & "', IP_Path = '" & Trim(Process.Path) & "', IP_Chr = '" & Trim(Process.Caracter) & "',IP_DOCTYPEID = " & Process.DocType.ID & ", IP_MOVE = " & CInt(Process.Move) & ", IP_VERIFY = " & CInt(Process.Verify) & ", IP_ACEPTBLANK = " & CInt(Process.FlagAceptBlankData) & ",IP_BACKUP = " & CInt(Process.FlagBackUp) & ", IP_DELSOURCE = " & CInt(Process.FlagDelSourceFile) & ", IP_SOURCEVARIABLE = " & CInt(Process.FlagSourceVariable) & ", IP_MULTIPLEFILES = " & CInt(Process.FlagMultipleFiles) & ", IP_MULTIPLECHR = '" & Process.MultipleCaracter & "', IP_BACKUPPATH = '" & Process.BackUpPath & "' where (IP_ID = " & Process.ID & ")"

        Server.Con.ExecuteNonQuery(CommandType.Text, strupdate)

        'borro la lista anterior de atributos
        'PACKAGE=delIPindex_pkg
        'Stored Proc named  Borrarindex
        Dim strDelete As String = "DELETE FROM Ip_Index where (IP_ID = " & Process.ID & ")"
        Server.Con.ExecuteNonQuery(CommandType.Text, strDelete)
        Dim LastId As Integer = Process.ID
        Dim i As Integer
        Dim order As Integer = 0
        For i = 0 To Process.Index.Count - 1
            'PACKAGE= Process_pkg
            ' SP= Import_JobT2
            Dim StrInsert As String = "INSERT INTO Ip_Index (IP_ID, Array_ID, Index_ID, Index_Order) VALUES (" _
            & LastId & ", '" & Trim(Process.Index.Item(i)) & "', '" & Trim(Process.Index.Item(i)) & "', " & order + 1 & ")"
            Server.Con.ExecuteNonQuery(CommandType.Text, StrInsert)
            order = order + 1
        Next
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Metodo que recupera de la base de datos el historia de procesos ejecutados para un proceso de importacion dado.
    ''' </summary>
    ''' <param name="ProcessId"></param>
    ''' <returns>Devuelve un dataset con los datos del historial, que apuntan a los archivos de log y error que se ubican en la carpeta de backup</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Martin]	08/05/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function ShowProcessHistory(ByVal ProcessId As Int32) As DsProcessHistory
        Dim DsProcessHistory As New DsProcessHistory
        If Server.IsOracle Then
            Dim dstemp As DataSet
            Dim parValues() As Object = {ProcessId, 2}
            dstemp = Server.Con.ExecuteDataset("SHOWPROCESSHISTORY159_PKG.showProcessHistory", parValues)
            dstemp.Tables(0).TableName = DsProcessHistory.ProcessHistory.TableName
            DsProcessHistory.Merge(dstemp)
        Else
            Dim parameters() As Object = {ProcessId}
            Dim dstemp As DataSet
            'dstemp = Server.Con.ExecuteDataset("FrmImports_ShowPHistory159", parameters)
            dstemp = Server.Con.ExecuteDataset("zsp_imports_100_GetProcessHistory", parameters)
            dstemp.Tables(0).TableName = DsProcessHistory.ProcessHistory.TableName
            DsProcessHistory.Merge(dstemp)
        End If
        Return DsProcessHistory
    End Function
    Public Shared Sub DelProcess(ByVal ProcessId As Integer)
        Dim RiB As New RightsBusiness
        Try
            'Package= delIPindex_pkg
            'SP BorrarType
            Dim strdelete As String = "DELETE FROM IP_INDEX where IP_ID = " & ProcessId
            ' Dim DtIpType1 = New DataSet
            Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
            'Package=delIPindex_pkg
            'SP Borrarindex
            strdelete = "DELETE FROM IP_TYPE where IP_ID = " & ProcessId
            Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)

            RiB.SaveAction(ProcessId, Zamba.ObjectTypes.ModuleImport, Zamba.Core.RightsType.Delete, "Eliminando proceso")
        Catch ex As Exception
            'MessageBox.Show("Ocurrio un error al intentar borrar el proceso. " & ex.ToString, "Zamba Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            RiB = Nothing
        End Try
    End Sub
    Public Shared Function Get_DocTypes() As ArrayList
        Dim strSelect As String = ("SELECT Doc_Type_Id, Doc_Type_Name, File_Format_ID, Disk_Group_ID, Thumbnails, Icon_Id, Object_Type_Id FROM Doc_Type")
        Dim DSTEMP As DataSet
        Dim ReturnVariable As New ArrayList
        DSTEMP = Server.Con.ExecuteDataset(CommandType.Text, strSelect)
        DSTEMP.Tables(0).TableName = "DOC_TYPE"
        Dim dt As DataTable = DSTEMP.Tables(0)
        Dim qRows As Integer = dt.Rows.Count
        If qRows > 0 Then
            ' Dim row As DataRow
            Dim i As Integer
            For i = 0 To qRows - 1
                Dim Doc_Type_Name As String = Trim(dt.Rows(i).Item("Doc_Type_Name"))
                '  Dim Doc_Type_Id As Integer = dt.Rows(i).Item("Doc_Type_Id")
                ReturnVariable.Add(Doc_Type_Name)
            Next
        End If
        Return ReturnVariable
    End Function
    Public Shared Function GetProcess(ByVal ProcessId As Integer) As Process
        'PKG=GetProcess_pkg  
        'SP=GetProcess(JobID)
        Dim StrSelect As String = "SELECT IP_NAME, IP_PATH, IP_CHR, IP_DOCTYPEID, IP_ID, IP_MOVE, IP_VERIFY, IP_ACEPTBLANK, IP_BACKUP, IP_DELSOURCE, IP_SOURCEVARIABLE, IP_MULTIPLEFILES,IP_MULTIPLECHR , IP_BACKUPPATH,IP_GROUP FROM IP_TYPE WHERE (IP_ID = " & ProcessId & ") ORDER BY IP_NAME"
        Dim Table As String = "IP_TYPE"
        Dim DsIPTYPE1 As New DSIpType
        Dim DSTEMP As DataSet

        DSTEMP = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
        DSTEMP.Tables(0).TableName = Table
        DsIPTYPE1.Merge(DSTEMP)
        'PKG= seleccionar_pkg
        'SP=seleccionar
        StrSelect = "SELECT IP_ID, ARRAY_ID, INDEX_ID, INDEX_ORDER FROM IP_INDEX WHERE(IP_ID = " & ProcessId & ") ORDER BY INDEX_ORDER ASC"
        Table = "IP_INDEX"
        Dim DsIPINDEX1 As New DsIpIndex
        DSTEMP.Clear()
        DSTEMP = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
        DSTEMP.Tables(0).TableName = Table
        DsIPINDEX1.Merge(DSTEMP)
        Dim Process As Process = ProcessFactory.GetTempProcess(DsIPTYPE1, 0)
        Dim i As Integer
        For i = 0 To DsIPINDEX1.IP_INDEX.Rows.Count - 1
            Process.Index.Add(DsIPINDEX1.IP_INDEX.Rows(i).Item("Index_ID"))
        Next
        Return Process
    End Function
    Public Shared Function GetProcessIndexData(ByVal ProcessId As Int32) As DsIpIndex
        Dim DSIPIndex As New DsIpIndex
        'PKG=Selectallindex_pkg
        'SP=Selectallindex
        Dim strSelect As String = "SELECT * FROM IP_INDEX where (IP_ID = " & ProcessId & ") ORDER BY INDEX_ORDER ASC"
        Dim DsTemp As DataSet
        DsTemp = Server.Con.ExecuteDataset(CommandType.Text, strSelect)
        DsTemp.Tables(0).TableName = DSIPIndex.Tables(0).TableName
        DSIPIndex.Merge(DsTemp)
        Return DSIPIndex
    End Function
    Public Shared Sub CopyProcessJob(ByVal processid As Int32, ByVal NewName As String)
        Dim IPJOB As Process = ProcessFactory.GetProcess(processid)
        If NewName.Length > 30 Then
            MessageBox.Show("Nombre muy largo", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        IPJOB.Name = NewName
        ProcessFactory.SaveProcess(IPJOB)
    End Sub
    Public Shared Function GetTempProcess(ByVal DsProcess As DSIpType, ByVal Index As Int32) As Process
        Dim Process As New Process
        Process.ID = DsProcess.IP_TYPE(Index).IP_ID
        Process.Name = DsProcess.IP_TYPE(Index).IP_NAME
        Process.Caracter = DsProcess.IP_TYPE(Index).IP_CHR
        Dim DTB As New DocTypesBusiness
        Dim DocType As DocType = DTB.GetDocType(CInt(DsProcess.IP_TYPE(Index).IP_DOCTYPEID))
        DTB = Nothing
        Process.DocType = DocType
        Process.Path = DsProcess.IP_TYPE(Index).IP_PATH
        Try
            If IsDBNull(DsProcess.IP_TYPE(Index).IP_GROUP) Then
                Process.BackUpPath = 0
            Else
                Process.BackUpPath = DsProcess.IP_TYPE(Index).IP_BACKUPPATH
            End If
        Catch
            Process.BackUpPath = 0
        End Try
        Try
            If IsDBNull(DsProcess.IP_TYPE(Index).IP_GROUP) Then
                Process.IP_GROUP = 0
            Else
                Process.IP_GROUP = DsProcess.IP_TYPE(Index).IP_GROUP
            End If
        Catch
            Process.IP_GROUP = 0
        End Try
        Process.Move = DsProcess.IP_TYPE(Index).IP_MOVE
        Process.Verify = DsProcess.IP_TYPE(Index).IP_VERIFY
        Process.FlagAceptBlankData = DsProcess.IP_TYPE(Index).IP_ACEPTBLANK
        Process.FlagBackUp = DsProcess.IP_TYPE(Index).IP_BACKUP
        Process.FlagDelSourceFile = DsProcess.IP_TYPE(Index).IP_DELSOURCE
        Process.FlagSourceVariable = DsProcess.IP_TYPE(Index).IP_SOURCEVARIABLE
        Process.FlagMultipleFiles = DsProcess.IP_TYPE(Index).IP_MULTIPLEFILES
        If Process.FlagMultipleFiles = True Then
            If Not IsNothing(DsProcess.IP_TYPE(Index).IP_MULTIPLECHR) Then Process.MultipleCaracter = DsProcess.IP_TYPE(Index).IP_MULTIPLECHR
        End If
        Return Process
    End Function
    Public Shared Function GetProcessByProcessListId(ByVal ProcessListId As Int64) As DSIpType
        Dim StrSelect As String = "SELECT IP_NAME, IP_PATH, IP_CHR, IP_DOCTYPEID, IP_ID, IP_MOVE, IP_VERIFY, IP_ACEPTBLANK, IP_BACKUP, IP_DELSOURCE, IP_SOURCEVARIABLE, IP_MULTIPLEFILES,IP_MULTIPLECHR , IP_BACKUPPATH,IP_GROUP FROM IP_TYPE where IP_GROUP =" & ProcessListId & " ORDER BY IP_NAME"
        Dim DsProcess As New DSIpType
        Dim DSTEMP As DataSet
        DSTEMP = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
        DSTEMP.Tables(0).TableName = DsProcess.IP_TYPE.TableName
        DsProcess.Merge(DSTEMP)
        Return DsProcess
    End Function
    Public Shared Function GetAllProcess() As DSIpType
        Dim StrSelect As String = "SELECT IP_NAME, IP_PATH, IP_CHR, IP_DOCTYPEID, IP_ID, IP_MOVE, IP_VERIFY, IP_ACEPTBLANK, IP_BACKUP, IP_DELSOURCE, IP_SOURCEVARIABLE, IP_MULTIPLEFILES,IP_MULTIPLECHR , IP_BACKUPPATH,IP_GROUP, IP_CHECKBATCH FROM IP_TYPE ORDER BY IP_NAME"
        Dim DsProcess As New DSIpType
        Dim DSTEMP As DataSet
        DSTEMP = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
        DSTEMP.Tables(0).TableName = DsProcess.IP_TYPE.TableName
        DsProcess.Merge(DSTEMP)
        Return DsProcess
    End Function
    Public Shared Sub GetprocessIndexData(ByVal Process As Process)
        Process.DsProcessIndex = ProcessFactory.GetProcessIndexData(Process.ID)
    End Sub
    Public Shared Function getDiskVolumes(ByVal doc_type_id As Int32) As DataSet
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, "select distinct vol_id from doc_t" & doc_type_id)
        Return ds
    End Function

    Public Shared Function getInsertedFiles(ByVal doc_id As String, ByVal ds3 As DsMails) As DataSet
        '   Dim ds As DataSet = ProcessFactory.getDiskVolumes(doc_id)
        Dim i As Integer
        Dim path As String = ""

        For i = 1 To 10
            If i < 10 Then
                path = "\\Arbuedf01\ZAMBA\Volumenes\Mails\Mails00" & i.ToString() & "\" & doc_id.ToString
            Else
                path = "\\Arbuedf01\ZAMBA\Volumenes\Mails\Mails0" & i.ToString() & "\" & doc_id.ToString
            End If

            Dim ind As Integer
            For ind = 1 To 99
                Dim d As New IO.DirectoryInfo(path & "\" & ind)
                If d.Exists = True Then
                    Dim files() As IO.FileInfo = d.GetFiles
                    For Each f As IO.FileInfo In files
                        Dim row As DsMails.VolFilesRow = ds3.VolFiles.NewVolFilesRow()
                        row.FileName = f.FullName
                        row.Offset = d.Name
                        row.Doc_id = f.Name.Split(".")(0)
                        ds3.VolFiles.AddVolFilesRow(row)
                        Try
                            Server.Con.ExecuteNonQuery(CommandType.Text, "insert into mVolumenes(filename, offset,doc_id) values('" & row.FileName.Replace("'", "''") & "'," & row.Offset & "," & row.Doc_id & ")")
                        Catch ex As Exception
                            MessageBox.Show("Hubo un error al registrar la fila en la base de datos. " & ex.ToString, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                    Next
                End If
            Next
        Next
        Return ds3
    End Function
    Public Shared Function CheckExist(ByVal row As DsMails.dsMailMasterRow, Optional ByVal doc_type_id As Integer = 55) As Integer
        Dim strb As New Text.StringBuilder
        Try
            strb.Append("select count(1) from doc_i" & doc_type_id)
            strb.Append(" where ")
            strb.Append("i29 = '" & row.Por.Replace("'", "''") & "' AND ")
            strb.Append("i30 = '" & row.Fecha.Replace("'", "''") & "' AND ")
            If row.Para <> "" Then
                strb.Append("i31 = '" & row.Para.Replace("'", "''") & "' ")
            End If
            If row.CC <> "" Then
                strb.Append("and i32 = '" & row.CC.Replace("'", "''") & "' ")
            End If
            If row.BCC <> "" Then
                strb.Append("and i33 = '" & row.BCC.Replace("'", "''") & "' ")
            End If
            If row.Asunto <> "" Then
                strb.Append("and i34 = '" & row.Asunto.Replace("'", "''") & "'")
            End If

            Dim count As Integer = Server.Con.ExecuteScalar(CommandType.Text, strb.ToString)
            Return count
        Finally
            strb = Nothing
        End Try
    End Function
    Public Shared Function getDocumentInfo(ByVal doc_id As Integer) As DataSet
        Dim strb As New Text.StringBuilder
        Try
            Try
                Server.Con.ExecuteNonQuery(CommandType.Text, "truncate table mins")
            Catch ex As Exception
            End Try

            strb.Append("select i.doc_id IDs, i29 Por,i30 Fecha ,i31 Para,i32 CC,i33 BCC,i34 Asunto,i16 Cliente ,i18 Poliza,folder_id Folder , dg.disk_group_id Grupo ,disk_group_name NombreGrupo, vol_id Volume, dv.disk_vol_path Path ,doc_file Files,offset Offset,")
            strb.Append("concat(concat(concat(dv.disk_vol_path,'\" & doc_id.ToString & "\'), offset),concat('\', doc_file )) FileName ")
            strb.Append("from doc_i" & doc_id & " i ,doc_t" & doc_id & " t,disk_group dg,disk_volume dv ")
            strb.Append("where i.doc_id=t.doc_id and dg.disk_group_id = t.disk_group_id and dv.disk_vol_id=vol_id")

            Server.Con.ExecuteNonQuery(CommandType.Text, "insert into MIns " & strb.ToString)

            Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strb.ToString)
            Return ds
        Finally
            strb = Nothing
        End Try
    End Function

    Public Shared Function getMasterInfo(ByVal path As String, ByVal errores As Integer, ByVal mailspublicos As Integer) As DataSet
        Dim files As ArrayList = getmasters(path, False)
        Dim errors As Integer = 0
        Dim Count As Int32 = 0
        Dim line As String = ""
        Dim ds As New DsMails
        ' Dim dsPublicos As New DsMails
        Try
            Server.Con.ExecuteNonQuery(CommandType.Text, "truncate table maestrot")
        Catch ex As Exception
            MessageBox.Show("No se pudo borrar la tabla maestrot " & ex.ToString, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        For Each file As IO.FileInfo In files
            Dim str As New IO.StreamReader(file.FullName, System.Text.Encoding.Default)
            While str.Peek <> -1
                line = str.ReadLine()
                Dim campos() As String = line.Split("|")
                Try
                    If campos(8) = String.Empty AndAlso campos(9) = String.Empty Then
                        Dim filesnames() As String = campos(7).Split(",")
                        For Each f As String In filesnames
                            Try
                                Dim r As DsMails.dsMailMasterRow = ds.dsMailMaster.NewdsMailMasterRow()
                                r.Por = campos(0)
                                r.Fecha = campos(1)

                                r.Para = campos(3)
                                r.CC = campos(4)
                                r.BCC = campos(5)
                                r.Asunto = campos(6)
                                Try
                                    r.Cliente = CInt(campos(8))
                                Catch
                                    r.Cliente = 0
                                End Try
                                Try
                                    r.Poliza = CInt(campos(9))
                                Catch
                                    r.Poliza = 0
                                End Try
                                Dim fi As New IO.FileInfo(f)
                                r.Path = file.Directory.FullName + "\" + fi.Name

                                ds.dsMailMaster.Rows.Add(r)
                                Try
                                    Server.Con.ExecuteNonQuery(CommandType.Text, "insert into maestrot(fecha,por,para,cc,bcc,asunto,cliente,poliza,path) values('" & r.Fecha.Replace("'", "''") & "','" & r.Por.Replace("'", "''") & "','" & r.Para.Replace("'", "''") & "','" & r.CC.Replace("'", "''") & "','" & r.BCC.Replace("'", "''") & "','" & r.Asunto.Replace("'", "''") & "'," & r.Cliente & "," & r.Poliza & ",'" & r.Path.Replace("'", "''") & "')")
                                Catch ex As Exception
                                    errors += 1
                                End Try

                                Count += 1
                            Catch ex As Exception
                                errors += 1
                            End Try
                        Next
                    Else
                        mailspublicos += 1
                        Dim filesnames() As String = campos(7).Split(",")
                        For Each f As String In filesnames
                            Try
                                Dim r As DsMails.dsMailMasterRow = ds.dsMailMaster.NewdsMailMasterRow()
                                r.Por = campos(0)
                                r.Fecha = campos(1)

                                r.Para = campos(3)
                                r.CC = campos(4)
                                r.BCC = campos(5)
                                r.Asunto = campos(6)
                                Try
                                    r.Cliente = CInt(campos(8))
                                Catch
                                    r.Cliente = 0
                                End Try
                                Try
                                    r.Poliza = CInt(campos(9))
                                Catch
                                    r.Poliza = 0
                                End Try
                                Dim fi As New IO.FileInfo(f)
                                r.Path = file.Directory.FullName + "\" + fi.Name

                                ds.dsMailMaster.Rows.Add(r)
                                Try
                                    Server.Con.ExecuteNonQuery(CommandType.Text, "insert into maestrot(fecha,por,para,cc,bcc,asunto,cliente,poliza,path) values('" & r.Fecha.Replace("'", "''") & "','" & r.Por.Replace("'", "''") & "','" & r.Para.Replace("'", "''") & "','" & r.CC.Replace("'", "''") & "','" & r.BCC.Replace("'", "''") & "','" & r.Asunto.Replace("'", "''") & "'," & r.Cliente & "," & r.Poliza & ",'" & r.Path.Replace("'", "''") & "')")
                                Catch ex As Exception
                                    errors += 1
                                End Try

                                Count += 1
                            Catch ex As Exception
                                errors += 1
                            End Try
                        Next
                    End If

                Catch ex As Exception
                    errors += 1
                End Try
                Try

                Catch ex As Exception
                    errors += 1
                End Try
            End While
            str.Close()
        Next
        errores = errors
        Return ds
    End Function
    Public Shared Function getmasters(ByVal path As String, ByVal processroot As Boolean) As ArrayList
        Dim result As New ArrayList
        Dim di As New IO.DirectoryInfo(path)
        If processroot = True Then
            result.AddRange(di.GetFiles("Maestro.txt"))
        End If

        Dim dirs() As IO.DirectoryInfo = di.GetDirectories
        For Each d As IO.DirectoryInfo In dirs
            result.AddRange(getmasters(d.FullName, True))
        Next

        Return result
    End Function
    ''' <summary>
    ''' Devuelve el ID de un proceso por su nombre. Si el proceso no existe en la base de datos
    ''' el Stored Procedure se encarga de crear un ID al proceso y devolverlo
    ''' </summary>
    ''' <param name="processName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetProcessIDByName(ByVal processName As String) As Int64
        Dim id As Int64
        If Server.ServerType = DBTypes.MSSQLServer7Up Then
            id = Server.Con.ExecuteScalar(CommandType.StoredProcedure, "PreprocessGetID", New SqlClient.SqlParameter("@name", processName))
        End If
        Return id
    End Function
    ''' <summary>
    ''' Obtiene los procesos los cuales el usuario tiene permiso.
    ''' </summary>
    ''' <param name="ProcessId"></param>
    ''' <remarks></remarks>
    ''' <history>   [Ezequiel] 03/07/2009 - Created
    Public Shared Function GetProcessByUserId(ByVal usrid As Int64) As ArrayList
        Dim list As DataSet
        Dim array As New ArrayList
        If Server.isOracle Then
            ''Dim parNames() As String = {"userid", "io_cursor"}
            ''' Dim parTypes() As Object = {13, 5}
            Dim parValues() As Object = {MembershipHelper.CurrentUser.ID, 2}
            list = Server.Con.ExecuteDataset("Zsp_Preprocess_100.GeTPreprocessByUsrID", parValues)
        Else
            list = Server.Con.ExecuteDataset(CommandType.StoredProcedure, "Zsp_Preprocess_GeTPreprocessByUsrID", New SqlClient.SqlParameter("@userid", usrid))
            For i As Int32 = 0 To list.Tables(0).Rows.Count - 1
                array.Add(DirectCast(list.Tables(0).Rows(i).Item(1), String).Trim)
            Next
        End If
        Return array
    End Function

    Public Shared Sub asignprocesslist(ByVal processID As Int32, ByVal ProcessListId As Int32)
        Dim strupdate As String = "UPDATE IP_TYPE SET IP_GROUP = " & ProcessListId & " where IP_ID = " & processID
        Server.Con.ExecuteNonQuery(CommandType.Text, strupdate)
    End Sub

    Public Shared Sub Removeprocesslist(ByVal ProcessId As Int64)
        Dim strupdate As String = "UPDATE IP_TYPE SET IP_GROUP = 0 where IP_ID = " & ProcessId
        Server.Con.ExecuteNonQuery(CommandType.Text, strupdate)
    End Sub
End Class