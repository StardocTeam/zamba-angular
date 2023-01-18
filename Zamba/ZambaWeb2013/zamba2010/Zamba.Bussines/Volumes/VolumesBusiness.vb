Imports System.IO
Imports System.Text
Imports Zamba.Core
Imports ZAMBA.Servers
Imports Zamba.Data
'Imports Zamba.Volumes.Core

Public Class VolumesBusiness
    Inherits ZClass
#Region "ABM"
    Public Shared Sub AddVolume(ByRef Volume As IVolume, Optional ByVal Temporal As Boolean = False)
        Dim StrInsert As String

        If Temporal = True Then
            Volume.ID = -2
        Else
            Volume.ID = CoreData.GetNewID(IdTypes.VOLUMEID)
        End If

        StrInsert = "INSERT INTO Disk_Volume (DISK_VOL_ID,Disk_Vol_Name,Disk_Vol_Size,Disk_Vol_Type,Disk_Vol_Copy,Disk_Vol_Path,Disk_Vol_Size_Len,Disk_Vol_State,Disk_Vol_LstOffSet, Disk_Vol_Files) Values (" _
       & Volume.ID & ",'" & Volume.Name & "'," & Volume.Size & "," & Volume.VolumeType & "," & Volume.copy & ",'" & Volume.path & "'," & Volume.sizelen & "," & Volume.state & "," & Volume.offset & "," & Volume.Files & ")"

        Server.Con.ExecuteNonQuery(CommandType.Text, StrInsert)
    End Sub
    Public Shared Function IsVolumeDuplicated(ByVal VolumeName As String) As Boolean
        Dim table As String = "Disk_Volume"
        Try
            Dim strSelect As String = "SELECT COUNT(1) from " & table & " where (Disk_Vol_Name = '" & VolumeName.Trim & "')"
            Dim qrows As Int32 = Server.Con.ExecuteScalar(CommandType.Text, strSelect)
            If qrows > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New Exception("Ocurrio un error al consultar la duplicidad del Volumen " & ex.ToString)
        End Try
    End Function
    Public Shared Sub SetStateFull(ByVal VolumeId As Integer)
        Dim StrUpdate As String = "UPDATE DISK_VOLUME SET DISK_VOL_STATE = 1 WHERE DISK_VOL_ID = " & VolumeId
        Server.Con.ExecuteNonQuery(CommandType.Text, StrUpdate)
    End Sub
    Public Shared Sub SetLastOffSetUsed(ByVal LastOffsetUsed As Integer, ByVal VolumeId As Integer)
        Dim StrUpdate As String = "UPDATE DISK_VOLUME SET DISK_VOL_LSTOFFSET = " & LastOffsetUsed & " WHERE DISK_VOL_ID = " & VolumeId
        Server.Con.ExecuteNonQuery(CommandType.Text, StrUpdate)
    End Sub
    Public Shared Sub UpdateVolume(ByRef Volume As IVolume, ByVal NewName As String, ByVal NewPath As String, ByVal NewSize As Decimal, ByVal NewState As Int32)


        Dim path As Boolean = False
        If NewPath <> Volume.path Then
            path = True
        End If

        Volume.state = NewState

        NewSize = Replace(NewSize, ",", ".")
        Dim Newsizelen As Decimal = Replace(Volume.sizelen, ",", ".")

        Try
            'Dim table As String = "Disk_Volume"
            'TODO Falta abstraer esto
            Dim strUpdate As String
            If path = True Then
                strUpdate = "UPDATE Disk_Volume SET Disk_Vol_Name = '" & NewName & "', Disk_Vol_Size = " & NewSize & ", Disk_Vol_Type = " & Volume.VolumeType & ", Disk_Vol_Copy = " & Volume.copy & ", " _
            & "Disk_Vol_Path = '" & NewPath & "', Disk_Vol_Size_Len = " & Newsizelen & ", Disk_Vol_State = " & Volume.state & ", Disk_Vol_LstOffSet = " & Volume.offset & " where(Disk_Vol_Id = " & Volume.ID & ")"
            Else
                strUpdate = "UPDATE Disk_Volume SET Disk_Vol_Name = '" & NewName & "', Disk_Vol_Size = " & NewSize & ", Disk_Vol_Type = " & Volume.VolumeType & ", Disk_Vol_Copy = " & Volume.copy & ", " _
            & "Disk_Vol_Size_Len = " & Newsizelen & ", Disk_Vol_State = " & Volume.state & ", Disk_Vol_LstOffSet = " & Volume.offset & " where Disk_Vol_Id = " & Volume.ID
            End If
            Server.Con.ExecuteNonQuery(CommandType.Text, strUpdate)
        Catch ex As Exception
            Throw New Exception("Ocurrio un problema al modificar el Volumen: " & Volume.Name)
        End Try
    End Sub

#End Region
    Public Shared Function IsValidPath(ByVal Path As String) As Boolean
        'TODO Falta verificar si el path existe
        If Path.Trim = "" Then
            Return False
        Else
            If Not IO.Directory.Exists(Path) Then
                Return False
            Else

                If Not IO.File.Exists(Path & "\volid.txt") Then
                    Return False
                End If
            End If
            Return True
        End If
    End Function
    Public Shared Function isvolumepathInUse(ByVal VolPath As String) As Boolean
        Dim i As Int32
        Dim strselect As String = "SELECT count(1) FROM disk_volume WHERE disk_vol_path='" & VolPath & "'"
        i = Server.Con.ExecuteScalar(CommandType.Text, strselect)
        If i > 0 Then Return True Else Return False
    End Function

    Public Shared Function IsValidSize(ByVal Size As String) As Boolean
        Try
            If CDec(Size) = 0 Then
                Return False
            ElseIf CDec(Size) > 0 Then
                Return True
            End If
        Catch
            Return False
        End Try
    End Function
    Public Shared Function IsValidType(ByVal Type As String) As Boolean
        If Trim(Type) = "" Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Shared Function VolIsEmpty(ByVal Volume As IVolume) As Boolean
        If Volume.Files > 0 Then
            Return False
        Else
            Return True
        End If
    End Function
#Region "GetVolumeData"

    Public Shared Function GetVolumes(Optional ByVal TemporalVolumes As Boolean = False, Optional ByVal VolName As String = "") As ArrayList
        Dim Strselect As String
        If TemporalVolumes = False Then
            Strselect = "select Disk_Vol_Name,Disk_Vol_Size,Disk_Vol_Type,Disk_Vol_Copy,Disk_Vol_Path," _
    & " Disk_Vol_Size_Len,Disk_Vol_State,Disk_Vol_LstOffset,Disk_Vol_Files,Disk_Vol_Id from Disk_Volume  WHERE Disk_Vol_id > 0 order by Disk_Vol_Name"
        Else
            Strselect = "select Disk_Vol_Name,Disk_Vol_Size,Disk_Vol_Type,Disk_Vol_Copy,Disk_Vol_Path," _
     & " Disk_Vol_Size_Len,Disk_Vol_State,Disk_Vol_LstOffset,Disk_Vol_Files,Disk_Vol_Id from Disk_Volume WHERE Disk_Vol_id < 0 and DISK_VOL_NAME = '" & VolName & "' order by Disk_Vol_Name"
            End If

        Dim Volumes As New ArrayList

        Dim DS As DataSet
        DS = Server.Con.ExecuteDataset(CommandType.Text, Strselect)
        DS.Tables(0).TableName = "Disk_Volume"

        Dim Qrows As Int32 = Ds.Tables(0).Rows.Count
        Dim i As Int32
        For i = 0 To Qrows - 1
            Dim Vol As New Volume
            Vol.Name = DS.Tables(0)(i)("DISK_VOL_NAME")
            Vol.Size = DS.Tables(0)(i)("Disk_Vol_Size")
            Vol.VolumeType = DS.Tables(0)(i)("Disk_Vol_Type")
            Vol.copy = DS.Tables(0)(i)("Disk_Vol_Copy")
            Vol.path = DS.Tables(0)(i)("Disk_Vol_Path")
            Vol.sizelen = DS.Tables(0)(i)("Disk_Vol_Size_Len")
            Vol.state = DS.Tables(0)(i)("Disk_Vol_State")
            Vol.offset = DS.Tables(0)(i)("Disk_Vol_LstOffset")
            Vol.Files = DS.Tables(0)(i)("Disk_Vol_Files")
            Vol.ID = DS.Tables(0)(i)("Disk_Vol_Id")
            Volumes.Add(Vol)
        Next
        Return Volumes
    End Function
    Public Shared Function GetAllVolumes() As DataSet
        Dim Strselect As String
        Strselect = "select Disk_Vol_Name,Disk_Vol_Size,Disk_Vol_Type,Disk_Vol_Copy,Disk_Vol_Path," _
    & " Disk_Vol_Size_Len,Disk_Vol_State,Disk_Vol_LstOffset,Disk_Vol_Files,Disk_Vol_Id from Disk_Volume  WHERE Disk_Vol_id > 0 order by Disk_Vol_Name"
        Dim Dstemp As DataSet
        Dstemp = Server.Con.ExecuteDataset(CommandType.Text, Strselect)
        Dstemp.Tables(0).TableName = "Disk_Volume"
        Return Dstemp
    End Function





    Public Shared Function GetRealSizeLen(ByVal DocTypeId As Integer, ByVal Path As String, ByVal Offset As Integer) As Long
        Dim dir As New DirectoryInfo(Path & "\" & DocTypeId & "\" & Offset)
        'Dim f As FileInfo
        Dim Size As Long
        Dim fiArr As FileInfo() = dir.GetFiles()
        Dim fri As FileInfo
        For Each fri In fiArr
            Dim fsize As Decimal = fri.Length
            Size += fsize
        Next fri
        Return Size
    End Function

    Public Shared Function GetRealSizeLen(ByVal Path As String) As Long
        Dim dir As New DirectoryInfo(Path)
        Dim Size As Long
        Dim fiArr As FileInfo() = dir.GetFiles()
        Dim fri As FileInfo
        For Each fri In fiArr
            Dim fsize As Decimal = fri.Length
            Size += fsize
        Next fri
        Return Size
    End Function
    Public Shared Function GetRealFiles(ByVal Path As String) As Long
        Dim dir As New DirectoryInfo(Path)
        Return dir.GetFiles.Length
    End Function

    Public Shared Function ValidateOffSet(ByVal Volume As IVolume) As Boolean
        Try
            Dim DSize As Decimal = CDec(Volume.sizelen) / 1024
            If (DSize / (Volume.Size / 20)) + 1 <= Volume.offset Then
                Return True
            Else
                If Volume.offset < 20 Then
                    Volume.offset += 1
                    SetLastOffSetUsed(Volume.offset, Volume.Id)
                    Return True
                Else
                    SetStateFull(Volume.Id)
                    Return False
                End If
            End If
        Catch
            Return False
        End Try
    End Function
    Public Shared Function RetrieveVolumePath(ByVal VolumeId As Integer) As String
        Dim strselect As String = "SELECT DISK_VOL_ID, DISK_VOL_STATE, DISK_VOL_PATH FROM DISK_VOLUME WHERE DISK_VOL_ID = " & VolumeId
        Dim DS As DataSet
        DS = Server.Con.ExecuteDataset(CommandType.Text, strselect)
        DS.Tables(0).TableName = "Disk_Volume"
        Dim qrows As Integer = DS.Tables("Disk_Volume").Rows.Count
        If qrows > 0 Then
            Dim i As Integer
            For i = 0 To qrows - 1
                Dim VolState As Integer = DS.Tables("Disk_Volume").Rows(i).Item("Disk_Vol_State")
                If VolState = 0 Then
                    Dim VolPath As String = Trim(DS.Tables("Disk_Volume").Rows(i).Item("Disk_Vol_Path"))
                    Return VolPath
                Else
                    'TODO Falta ver si este es el indicador de volumen lleno para el insertar
                    Dim VolPath As String = Trim(DS.Tables("Disk_Volume").Rows(i).Item("Disk_Vol_Path"))
                    Return VolPath
                End If
            Next
        Else
            Throw New Exception("El volumen no se encuentra disponible")
            Return Nothing
        End If
        Return Nothing
    End Function
    Public Shared Function GetTemporalVolume(ByVal DocTypeId As Int32) As IVolume
        Dim Strselect As String = "select Disk_Vol_Name,Disk_Vol_Size,Disk_Vol_Type,Disk_Vol_Copy,Disk_Vol_Path, Disk_Vol_Size_Len,Disk_Vol_State,Disk_Vol_LstOffset,Disk_Vol_Files,Disk_Vol_Id from Disk_Volume WHERE Disk_Vol_ID < 0 and DISK_VOL_NAME = '" & System.Environment.MachineName & "' order by Disk_Vol_Name"
            Dim con As IConnection = Nothing
        Dim Dr As IDataReader = Nothing
        Dim Vol As New Volume
        Try
            con = Server.Con
            Dr = con.ExecuteReader(CommandType.Text, Strselect)

            While Dr.Read
                Vol.Name = Dr.GetString(0)
                Vol.Size = Dr.GetDecimal(1)
                Vol.VolumeType = Dr.GetInt32(2)
                Vol.copy = Dr.GetInt32(3)
                Vol.path = Dr.GetString(4)
                Vol.sizelen = Dr.GetDouble(5)
                Vol.state = Dr.GetInt32(6)
                Vol.offset = Dr.GetInt32(7)
                Vol.Files = Dr.GetDecimal(8)
                Vol.ID = Dr.GetInt32(9)
            End While
        Finally
            If IsNothing(Dr) = False Then
                Dr.Close()
                Dr.Dispose()
                Dr = Nothing
            End If
            If Not IsNothing(con) Then
                con.Close()
                con.dispose()
                con = Nothing
            End If
        End Try
        If IsNothing(Vol.path) OrElse String.IsNullOrEmpty(Vol.path.Trim) Then
            Throw New Exception("El Volumen es incorrecto")
        End If
        Dim ZOPTB As New ZOptBusiness

        Dim forceBlob As String = ZOPTB.GetValue("ForceBlob")
        ZOPTB = Nothing
        If Not String.IsNullOrEmpty(forceBlob) AndAlso Boolean.Parse(forceBlob) Then
            CreateDirVolume(Vol.path, DocTypeId)
        End If

        Return Vol
    End Function
    'Public Shared Sub CheckVolumesPath()
    '    '          if Server.IsOracle then
    '    'Dim strselect As String = "select substr(disk_vol_Path,3, instr(substr(disk_vol_Path,3,length(disk_vol_Path)),'\',1,1 )-1 ) , disk_vol_path from (disk_volume) where substr(disk_vol_Path,1,2)='\\' order by 1"
    '    '              Dim ds As DataSet = Server.Con(True).ExecuteDataset(CommandType.Text, strselect)
    '    '              Dim i As Integer
    '    '              Dim last As String = ""
    '    '              For i = 0 To ds.Tables(0).Rows.Count - 1

    '    '                  If ds.Tables(0).Rows(0).Item(0) <> last Then
    '    '                      If IO.File.Exists(ds.Tables(0).Rows(0).Item(1)) Then
    '    '                      End If
    '    '                      last = ds.Tables(0).Rows(0).Item(0)
    '    '                  End If
    '    '              Next
    '    '          End If
    'End Sub

#End Region
    Public Shared Function GetVolumenPathByVolId(ByVal volid As Int32) As String
        Dim strsql As String = "select Disk_Vol_Path from Disk_Volume where Disk_Vol_ID=" & volid
        Dim volpath As String
        volpath = Server.Con.ExecuteScalar(CommandType.Text, strsql)
        If volpath Is Nothing Then
            Return Nothing
        Else
            Return volpath
        End If
    End Function

    Public Shared Function CreateDirVolume(ByVal Volume As IVolume, ByVal DocTypeId As Int64) As Boolean
        Try
            'chequeo el directorio del entidad
            ZTrace.WriteLineIf(ZTrace.IsVerbose, Volume.path.Trim & "\" & DocTypeId)
            Dim DocDirPath As String = (Volume.path.Trim & "\" & DocTypeId)
            If Directory.Exists(DocDirPath) = False Then
                'creo el directorio del entidad dentro del volumen
                ZTrace.WriteLineIf(ZTrace.IsInfo, Directory.Exists(DocDirPath))
                Directory.CreateDirectory(DocDirPath)
            End If
            'chequeo la existencia de los offset
            Dim i As Integer
            For i = Volume.offset To Volume.offset + 1
                ZTrace.WriteLineIf(ZTrace.IsInfo, Volume.path.Trim & "\" & DocTypeId & "\" & i)
                Dim OffSetDirPath As String = (Volume.path.Trim & "\" & DocTypeId & "\" & i)
                If Directory.Exists(OffSetDirPath) = False Then
                    'creo la carpeta offset
                    ZTrace.WriteLineIf(ZTrace.IsInfo, Directory.Exists(OffSetDirPath))
                    Directory.CreateDirectory(OffSetDirPath)
                End If
            Next
            Return True
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al crear volumen " & ex.ToString)
            Return False
        End Try
    End Function
    Public Shared Function CreateDirVolume(ByVal VolPath As String, ByVal DocTypeId As Int64) As Boolean
        Try
            'chequeo el directorio del entidad
            ZTrace.WriteLineIf(ZTrace.IsInfo, VolPath.Trim & "\" & DocTypeId)
            Dim DocDirPath As String = VolPath.Trim & "\" & DocTypeId
            If Directory.Exists(DocDirPath) = False Then
                'creo el directorio del entidad dentro del volumen
                ZTrace.WriteLineIf(ZTrace.IsInfo, Directory.Exists(DocDirPath))
                Directory.CreateDirectory(DocDirPath)
            End If
            'chequeo la existencia de los offset
            Dim i As Integer
            For i = 1 To 99
                ZTrace.WriteLineIf(ZTrace.IsInfo, VolPath.Trim & "\" & DocTypeId & "\" & i)
                Dim OffSetDirPath As String = (VolPath.Trim & "\" & DocTypeId & "\" & i)
                If Directory.Exists(OffSetDirPath) = False Then
                    'creo la carpeta offset
                    ZTrace.WriteLineIf(ZTrace.IsInfo, Directory.Exists(OffSetDirPath))
                    Directory.CreateDirectory(OffSetDirPath)
                End If
            Next
            Return True
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al crear volumen " & ex.ToString)
            Return False
        End Try
    End Function
    Private Sub New()
    End Sub

#Region "Document Insert"

    Public Shared ReadOnly Property VolumePath(ByVal volume As IVolume, ByVal docTypeId As Int32) As String
        Get
            If volume.ID = 0 Then
                Return String.Empty
            Else
                Return volume.path & "\" & docTypeId & "\" & volume.offset
            End If
        End Get
    End Property



    Public Shared Function GetVolumeListId(ByVal Entityid As Int64) As Integer
        Try
            If Not Cache.Volumes.hsVolumesListIdbyEntityId.Contains(Entityid) Then
                SyncLock (Cache.Volumes.hsVolumesListIdbyEntityId)
                    If Not Cache.Volumes.hsVolumesListIdbyEntityId.Contains(Entityid) Then
                        Cache.Volumes.hsVolumesListIdbyEntityId.Add(Entityid, VolumesFactory.GetVolumeListIdByEntityId(Entityid))
                    End If
                End SyncLock
            End If
            Return Cache.Volumes.hsVolumesListIdbyEntityId.Item(Entityid)
        Catch ex As Exception
            Throw New Exception("No hay un volumen disponible para este entidad, por favor contactese con su administrador de sistema")
        End Try
    End Function


    Public Shared Function GetVolumeData(ByRef volumeID As Int64, Optional ByVal Transaction As Zamba.Data.Transaction = Nothing) As IVolume
        'obtengo la data para el volumen seleccionado
        Dim Vol As Volume
        If Cache.Volumes.hsVolumesbyVolumeId.ContainsKey(volumeID) = False Then
            Vol = VolumesFactory.GetVolumeData(volumeID, Transaction)
            SyncLock Cache.Volumes.hsVolumesbyVolumeId
                Cache.Volumes.hsVolumesbyVolumeId.Add(volumeID, Vol)
            End SyncLock
        End If
        Return Cache.Volumes.hsVolumesbyVolumeId(volumeID)
    End Function



    Public Shared Function LoadVolume(ByVal docTypeId As Int32, ByVal dsVols As DataSet) As IVolume
        Dim Volume As IVolume = New Volume()
        Dim ErrorBuilder As New StringBuilder()
        Try
            Dim i As Int32
            For i = 0 To dsVols.Tables(0).Rows.Count - 1
                Try
                    Dim VolumeID As Int64 = Int64.Parse(dsVols.Tables(0).Rows(i).Item("DISK_VOLUME_ID").ToString())
                    Volume = GetVolumeData(VolumeID)

                    Dim ZOPTB As New ZOptBusiness

                    Dim forceBlob As String = ZOPTB.GetValue("ForceBlob")
                    ZOPTB = Nothing

                    If String.IsNullOrEmpty(forceBlob) OrElse Not Boolean.Parse(forceBlob) Then
                        If IsNothing(Volume) Then
                            ErrorBuilder.Append("Volumen no disponible")
                        ElseIf Volume.VolumeState <> IVolume.VolumeStates.VolumenListo And Volume.VolumeState <> IVolume.VolumeStates.VolumenEnPreparacion Then
                            ErrorBuilder.Append("Volumen no disponible")
                        ElseIf VerificoPath(Volume) = False Then
                            ErrorBuilder.Append("PATH del Volumen Incorrecto")
                        ElseIf CreateDirsDocTypeOffset(Volume, docTypeId) = False Then
                            ErrorBuilder.Append("No se pueden crear los OFFSET del Volumen")
                        ElseIf GetOffSet(Volume) = Nothing Then
                            ErrorBuilder.Append("Volumen Lleno")
                        Else
                            Return Volume
                        End If
                    Else
                        Return Volume
                    End If

                Catch ex As Exception
                    If (String.IsNullOrEmpty(ErrorBuilder.ToString())) Then
                        Throw New Exception(ex.ToString())
                    Else
                        Throw New Exception("Volumen No Disponible -- " + ErrorBuilder.ToString)
                    End If
                End Try
            Next

            Volume = GetTemporalVolume(docTypeId)

            If IsNothing(Volume.path) = False Then
                Return Volume
            Else
                ErrorBuilder.Append("PATH del Volumen Incorrecto")
            End If
        Catch ex As Exception
            If (String.IsNullOrEmpty(ErrorBuilder.ToString())) Then
                Throw New Exception(ex.ToString())
            Else
                Throw New Exception("Volumen No Disponible -- " + ErrorBuilder.ToString)
            End If
        End Try

        ErrorBuilder.Remove(0, ErrorBuilder.Length)
        ErrorBuilder = Nothing

        Return Volume
    End Function

    Public Shared Function VerificoPath(ByVal Volume As IVolume) As Boolean
        'verifico el path del volumen
        If IsNothing(Volume.path) Or Volume.path = "" Then
            Return False
        End If
        Return True
    End Function
    Private Shared Function CreateDirsDocTypeOffset(ByVal Volume As IVolume, ByVal DocTypeId As Int32) As Boolean
        Try
            'verifico el offset y directorio de doctype en el volumen
            CreateDirVolume(Volume, DocTypeId)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Shared Function GetOffSet(ByRef Volume As IVolume) As Int32
        Try
            If Volume.VolumeType = VolumeType.DataBase Then 'Volumen tipo base de datos
                Volume.offset = 1
                SetLastOffSetUsed(Volume.offset, Volume.ID)
                Return Volume.offset
            Else
                Dim DSize As Decimal = CDec(Volume.sizelen) / 1024
                If (DSize / (Volume.Size / 20)) + 1 <= Volume.offset Then

                    Return Volume.offset
                Else
                    If Volume.offset < 20 Then
                        Volume.offset += 1
                        SetLastOffSetUsed(Volume.offset, Volume.ID)
                        Return Volume.offset
                    Else
                        VolumesBusiness.SetStateFull(Volume.ID)
                        Return Nothing
                    End If
                End If
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function ObtengoElSiguienteVolumen(ByVal DsVols As DataSet, ByVal DocTypeId As Int32) As Volume
        Dim Volume As New Volume
        Try
            Dim i As Int32
            Dim FlagNextVolumeFinded As Boolean
            For i = 0 To DsVols.Tables(0).Rows.Count - 1
                If FlagNextVolumeFinded = False Then
                    If Volume.Id = DsVols.Tables(0).Rows(i).Item("DISK_VOLUME_ID") Then
                        Volume.Id = DsVols.Tables(0).Rows(i + 1).Item("DISK_VOLUME_ID")
                        FlagNextVolumeFinded = True
                    End If
                End If
            Next
            If FlagNextVolumeFinded = False Then
                Return Nothing
            Else
                Return Volume
            End If
        Catch ex As Exception
            Try
                Volume = GetTemporalVolume(DocTypeId)
                If IsNothing(Volume.path) Then
                    Return Nothing
                Else
                    Return Volume
                End If
            Catch exc As Exception
                Return Nothing
            End Try
        End Try
    End Function


#End Region

    Public Shared Sub DeleteFile(ByVal path As String)
        Dim file As New IO.FileInfo(path)
        If file.Exists = True Then
            file.Delete()
        End If
    End Sub
    'Private Shared Function Ruta(ByVal pathvolumen As String, ByVal docTypeId As Int32, ByVal ofset As Int32, ByVal docfile As String) As String
    '    Dim path As String
    '    path = pathvolumen & "\" & docTypeId.ToString & "\" & ofset.ToString & "\" & docfile
    '    Return path
    'End Function

    Public Shared Sub BorrarArchivos(ByVal DocTypeId As Int64)
        Dim sql As String = "Delete from doc_I" & DocTypeId
        Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        UpdateFilesinVol(DocTypeId)
    End Sub
    Private Shared Sub UpdateFilesinVol(ByVal DoctypeId As Int64)
        Dim sql As String = "Select disk_group_id from doc_type where doc_type_id=" & DoctypeId
        Dim volgroup As Int32 = Server.Con.ExecuteScalar(CommandType.Text, sql)
        'TODO Revisar
        'Dim volgroup As Int32
        'if Server.IsOracle then
        '    'Dim parNames() As String = {"DocTypeId"}
        '    '' Dim parTypes() As Object = {13}
        '    Dim parValues() As Object = {DoctypeId}
        '    'volgroup = Server.Con.ExecuteScalar("ZDtGetDoctypes_pkg.ZDtGetDgIdByDtId", parValues)
        '    volgroup = Server.Con.ExecuteScalar("zsp_doctypes_100.GetDiskGroupId", parValues)
        'Else
        '    Dim parValues() As Object = {DoctypeId}
        '    Try
        '        volgroup = Server.Con.ExecuteScalar("ZDtGetDOC_TYPE_R_DOC_TYPE", parvalues)
        '    Catch
        '    End Try
        'End If

        'sql = "Select disk_volume_id from disk_group_r_disk_volume where disk_group_id=" & volgroup
        'Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)
        Dim ds As DataSet = Nothing
        If Server.isOracle Then
            'Dim parNames() As String = {"volgroup", "io_cursor"}
            '' Dim parTypes() As Object = {13, 5}
            Dim parValues() As Object = {volgroup, 2}
            'ds = Server.Con.ExecuteDataset("ZDgRDvGet_pkg.ZDgRDvGetDvIdByDgId", parValues)
            ds = Server.Con.ExecuteDataset("zsp_volume_100.GetDocGroupRDocVolByDgId", parValues)
        Else
            Dim parValues() As Object = {volgroup}
            Try
                'ds = Server.Con.ExecuteDataset("ZDgRDvGetDvIdByDgId", parvalues)
                ds = Server.Con.ExecuteDataset("zsp_volume_100_GetDocGroupRDocVolByDgId", parValues)
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End If
        Dim i As Int16
        For i = 0 To ds.Tables(0).Rows.Count - 1
            'sql = "Update disk_volume set Disk_vol_files=0 where disk_vol_id=" & Integer.Parse(ds.Tables(0).Rows(i).Item(0))
            'Server.Con.ExecuteNonQuery(CommandType.Text, sql)
            If Server.isOracle Then
                'Dim parNames() As String = {"Archs", "DiskVolId"}
                '' Dim parTypes() As Object = {13, 13}
                Dim parValues() As Object = {0, Integer.Parse(ds.Tables(0).Rows(i).Item(0))}
                'Server.Con.ExecuteNonQuery("ZDtUpdDiskVolume_pkg.ZDvUpdFilesByVId", parValues)
                Server.Con.ExecuteNonQuery("zsp_volume_100.UpdFilesByVolId", parValues)
            Else
                Dim parValues() As Object = {0, Integer.Parse(ds.Tables(0).Rows(i).Item(0))}
                Try
                    'Server.Con.ExecuteNonQuery("ZDvUpdFilesByVId", parvalues)
                    Server.Con.ExecuteNonQuery("zsp_volume_100_UpdFilesByVolId", parValues)
                Catch
                End Try
            End If
        Next
        'sql = "Update Doc_type set Doccount=0 where doc_type_id=" & DoctypeId
        'Server.Con.ExecuteNonQuery(CommandType.Text, sql)

        If Server.isOracle Then
            'Dim parNames() As String = {"DocCount", "DocTypeId"}
            '' Dim parTypes() As Object = {13, 13}
            Dim parValues() As Object = {0, DoctypeId}
            'Server.Con.ExecuteNonQuery("ZDtUpdDoctypes_pkg.ZDtUpdDoccountByDtId", parValues)
            Server.Con.ExecuteNonQuery("zsp_doctypes_100.UpdDocCountById", parValues)
        Else
            Dim parValues() As Object = {0, DoctypeId}
            Try
                'Server.Con.ExecuteNonQuery("ZDtUpdDoccountByDtId", parvalues)
                Server.Con.ExecuteNonQuery("zsp_doctypes_100_UpdDocCountById", parValues)
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End If
    End Sub
    Public Shared Sub DeleteVolume(ByVal Volume As IVolume)
        Dim strDelete As String = "DELETE FROM Disk_Volume WHERE Disk_Vol_Id = " & Volume.Id
        Server.Con.ExecuteNonQuery(CommandType.Text, strDelete)
    End Sub
    Public Overrides Sub Dispose()

    End Sub


    ''' <summary>
    ''' Obtiene el tipo de volumen
    ''' </summary>
    ''' <param name="volumeId"></param>
    ''' <returns>(Int32) 1: DISCO RIGIDO, 2: DISCO OPTICO, 3: , 4: , 5: BASE DE DATOS</returns>
    ''' <history>
    '''     [Tomas] 17/03/2011  Created
    ''' </history>
    ''' <remarks></remarks>
    Public Shared Function GetVolumeType(ByVal volumeId As Int32) As Int32

        If Not Cache.Volumes.hsVolumeTypes.Contains(volumeId) Then
            SyncLock Cache.Volumes.hsVolumeTypes
                If Not Cache.Volumes.hsVolumeTypes.Contains(volumeId) Then

                    Cache.Volumes.hsVolumeTypes.Add(volumeId, VolumesFactory.GetVolumeType(volumeId))

                End If
            End SyncLock
        End If
        Return CInt(Cache.Volumes.hsVolumeTypes.Item(volumeId))
    End Function


End Class
