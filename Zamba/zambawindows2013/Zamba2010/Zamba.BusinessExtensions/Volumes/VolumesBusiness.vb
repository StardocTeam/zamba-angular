Imports System.IO
Imports System.Text
Imports Zamba.Servers
Imports Zamba.Data
Imports System.Net
Imports Zamba.PreLoad

'Imports Zamba.Volumes.Core

Public Class VolumesBusiness
    Inherits ZClass
#Region "ABM"
    Public Shared Sub AddVolume(ByRef Volume As IVolume)

        Volume.ID = CoreData.GetNewID(IdTypes.VOLUMEID)

        Dim StrInsert As String = "INSERT INTO Disk_Volume (DISK_VOL_ID,Disk_Vol_Name,Disk_Vol_Size,Disk_Vol_Type,Disk_Vol_Copy,Disk_Vol_Path,Disk_Vol_Size_Len,Disk_Vol_State,Disk_Vol_LstOffSet, Disk_Vol_Files) Values (" _
           & Volume.ID & ",'" & Volume.Name & "'," & Volume.Size & "," & Volume.Type & "," & Volume.copy & ",'" & Volume.path & "'," & Volume.sizelen & "," & Volume.state & "," & Volume.offset & "," & Volume.Files & ")"
        Server.Con.ExecuteNonQuery(CommandType.Text, StrInsert)
    End Sub

    Public Shared Function IsVolumeDuplicated(ByVal VolumeName As String) As Boolean
        Try
            Dim strSelect As String = "SELECT COUNT(Disk_Vol_id) from Disk_Volume where Disk_Vol_Name = '" & VolumeName.Trim & "'"
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

    Public Shared Sub SetStateFull(ByVal volumeId As Integer)
        If Server.isOracle Then
            Dim StrUpdate As String = "UPDATE DISK_VOLUME SET DISK_VOL_STATE = 1 WHERE DISK_VOL_ID = " & volumeId
            Server.Con.ExecuteNonQuery(CommandType.Text, StrUpdate)
        Else
            Server.Con.ExecuteNonQuery("ZSP_VOLUME_100_SetStateFull", New Object() {volumeId})
        End If
    End Sub

    Public Shared Sub SetLastOffSetUsed(ByVal lastOffsetUsed As Integer, ByVal volumeId As Integer)
        If Server.isOracle Then
            Dim StrUpdate As String = "UPDATE DISK_VOLUME SET DISK_VOL_LSTOFFSET = " & lastOffsetUsed & " WHERE DISK_VOL_ID = " & volumeId
            Server.Con.ExecuteNonQuery(CommandType.Text, StrUpdate)
        Else
            Server.Con.ExecuteNonQuery("ZSP_VOLUME_100_SetLastOffSetUsed", New Object() {lastOffsetUsed, volumeId})
        End If
    End Sub

    Public Shared Sub UpdateVolume(ByRef Volume As IVolume, ByVal NewName As String, ByVal NewPath As String, ByVal NewSize As Decimal, ByVal NewType As String, ByVal NewState As Int32)


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
                strUpdate = "UPDATE Disk_Volume SET Disk_Vol_Name = '" & NewName & "', Disk_Vol_Size = " & NewSize & ", Disk_Vol_Type = " & Volume.Type & ", Disk_Vol_Copy = " & Volume.copy & ", " _
            & "Disk_Vol_Path = '" & NewPath & "', Disk_Vol_Size_Len = " & Newsizelen & ", Disk_Vol_State = " & Volume.state & ", Disk_Vol_LstOffSet = " & Volume.offset & " where(Disk_Vol_Id = " & Volume.ID & ")"
            Else
                strUpdate = "UPDATE Disk_Volume SET Disk_Vol_Name = '" & NewName & "', Disk_Vol_Size = " & NewSize & ", Disk_Vol_Type = " & Volume.Type & ", Disk_Vol_Copy = " & Volume.copy & ", " _
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
            If Not Directory.Exists(Path) Then
                Return False
            Else
                Return True
            End If
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

    Public Shared Function GetVolumeData(ByVal VolumeId As Integer) As IVolume
        'Dim DsVolume As New DsVolume

        Dim Ds As DataSet = Nothing
        Dim CurrentVolume As IVolume = Nothing
        If Server.isOracle Then
            Dim Strselect As String = "select Disk_Vol_Name,Disk_Vol_Size,Disk_Vol_Type,Disk_Vol_Copy,Disk_Vol_Path, Disk_Vol_Size_Len,Disk_Vol_State,Disk_Vol_LstOffset,Disk_Vol_Files from Disk_Volume where disk_vol_id = " & VolumeId.ToString()
            Ds = Server.Con.ExecuteDataset(CommandType.Text, Strselect)
        Else
            Dim parameters() As Object = {(VolumeId)}
            Ds = Server.Con.ExecuteDataset("zsp_volumes_100_GetVolumeData", parameters)
        End If

        CurrentVolume = New Volume()
        CurrentVolume.Name = Ds.Tables(0).Rows(0)("DISK_VOL_NAME")
        CurrentVolume.Size = Ds.Tables(0).Rows(0)("Disk_Vol_Size")
        CurrentVolume.Type = Ds.Tables(0).Rows(0)("Disk_Vol_Type")
        CurrentVolume.copy = Ds.Tables(0).Rows(0)("Disk_Vol_Copy")
        CurrentVolume.path = Ds.Tables(0).Rows(0)("Disk_Vol_Path")
        CurrentVolume.sizelen = Ds.Tables(0).Rows(0)("Disk_Vol_Size_Len")
        CurrentVolume.state = Ds.Tables(0).Rows(0)("Disk_Vol_State")
        CurrentVolume.offset = Ds.Tables(0).Rows(0)("Disk_Vol_LstOffset")
        CurrentVolume.Files = Ds.Tables(0).Rows(0)("Disk_Vol_Files")
        CurrentVolume.ID = VolumeId

        Return CurrentVolume
    End Function

    Public Shared Function GetActiveDiskGroupVolumes(ByVal VolumeListId As Int32) As DataSet
        SyncLock (Cache.Volumes.hsVolumesbyVolumeListId)
            If Not Cache.Volumes.hsVolumesbyVolumeListId.Contains(VolumeListId) Then
                Cache.Volumes.hsVolumesbyVolumeListId.Add(VolumeListId, VolumeListsFactory.GetActiveDiskGroupVolumes(VolumeListId))
            End If
            Return Cache.Volumes.hsVolumesbyVolumeListId.Item(VolumeListId)
        End SyncLock
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
            Dim DSize As Decimal = Volume.sizelen / 1024
            If (DSize / (Volume.Size / 20)) + 1 <= Volume.offset Then
                Return True
            Else
                If Volume.offset < 20 Then
                    Volume.offset += 1
                    SetLastOffSetUsed(Volume.offset, Volume.ID)
                    Return True
                Else
                    SetStateFull(Volume.ID)
                    Return False
                End If
            End If
        Catch
            Return False
        End Try
    End Function
#End Region
    Public Shared NetworkConnection As NetworkConnection

    Public Shared Function CreateDirVolume(ByVal Volume As IVolume, ByVal DocTypeId As Int64) As Boolean
        Try
            'chequeo el directorio de la entidad
            ZTrace.WriteLineIf(ZTrace.IsInfo, Volume.path.Trim & "\" & DocTypeId)
            Dim DocDirPath As String = (Volume.path.Trim & "\" & DocTypeId)

            If Directory.Exists(DocDirPath) = False Then
                'creo el directorio de la entidad dentro del volumen
                ZTrace.WriteLineIf(ZTrace.IsInfo, Directory.Exists(DocDirPath))
                Directory.CreateDirectory(DocDirPath)
            End If

            'chequeo la existencia de los offset
            Dim i As Integer
            For i = 1 To 20
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
            'chequeo el directorio de la entidad
            ZTrace.WriteLineIf(ZTrace.IsInfo, VolPath.Trim & "\" & DocTypeId)
            Dim DocDirPath As String = VolPath.Trim & "\" & DocTypeId
            If Directory.Exists(DocDirPath) = False Then
                'creo el directorio de la entidad dentro del volumen
                ZTrace.WriteLineIf(ZTrace.IsInfo, Directory.Exists(DocDirPath))
                Directory.CreateDirectory(DocDirPath)
            End If
            'chequeo la existencia de los offset
            Dim i As Integer
            For i = 1 To 20
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
    Public Sub New()
    End Sub

#Region "Document Insert"

    Public ReadOnly Property VolumePath(ByVal volume As IVolume, ByVal docTypeId As Int64) As String
        Get
            If volume.ID = 0 Then
                Return String.Empty
            Else
                Return volume.path & "\" & docTypeId & "\" & volume.offset
            End If
        End Get
    End Property

    Public Shared Function GetVolumeListId(ByVal DocTypeId As Integer) As Integer
        Try
            SyncLock (Cache.Volumes.hsVolumesListIdbyEntityId)
                If Not Cache.Volumes.hsVolumesListIdbyEntityId.Contains(DocTypeId) Then
                    Cache.Volumes.hsVolumesListIdbyEntityId.Add(DocTypeId, VolumesFactory.GetVolumeListIdByEntityId(DocTypeId))
                End If
                Return Cache.Volumes.hsVolumesListIdbyEntityId.Item(DocTypeId)
            End SyncLock
        Catch ex As Exception
            Throw New Exception("No hay un volumen disponible para este entidad, por favor contactese con su administrador de sistema")
        End Try
    End Function

    Public Shared Function GetVolumeData(ByVal volumeId As Int64) As IVolume
        SyncLock (Cache.Volumes.hsVolumesbyVolumeId)
            If Not Cache.Volumes.hsVolumesbyVolumeId.Contains(volumeId) Then
                Dim Volume As Volume = VolumesFactory.GetVolumeData(volumeId)
                Cache.Volumes.hsVolumesbyVolumeId.Add(volumeId, Volume)
                Return Volume
            Else
                Return Cache.Volumes.hsVolumesbyVolumeId.Item(volumeId)
            End If
        End SyncLock
    End Function

    Public Shared Function VerificoPath(ByVal Volume As IVolume) As Boolean
        Return Not String.IsNullOrEmpty(Volume.path)
    End Function

    Private Shared Function CreateDirsDocTypeOffset(ByVal Volume As IVolume, ByVal docTypeId As Int64) As Boolean
        Try
            'verifico el offset y directorio de doctype en el volumen

            CreateDirVolume(Volume, docTypeId)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Shared Function GetOffSet(ByRef Volume As IVolume) As Int32
        Dim DSize As Decimal
        Try
            DSize = Volume.sizelen / 1024
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
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    'Public Shared Function GetOffSet(ByRef Volume As IVolume, Optional ByRef t As Transaction = Nothing) As Int32
    '    Try
    '        Dim DSize As Decimal = CDec(Volume.sizelen) / 1024
    '        If (DSize / (Volume.Size / 20)) + 1 <= Volume.offset Then
    '            Return Volume.offset
    '        Else
    '            If Volume.offset < 20 Then
    '                Volume.offset += 1
    '                SetLastOffSetUsed(Volume.offset, Volume.ID, t)
    '                Return Volume.offset
    '            Else
    '                VolumesBusiness.SetStateFull(Volume.ID, t)
    '                Return Nothing
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Return Nothing
    '    End Try
    'End Function
    Public Shared Function ObtengoElSiguienteVolumen(ByVal DsVols As DataSet, ByVal docTypeId As Int64) As Volume
        Dim Volume As New Volume
        Try
            Dim i As Int32
            Dim FlagNextVolumeFinded As Boolean
            For i = 0 To DsVols.Tables(0).Rows.Count - 1
                If FlagNextVolumeFinded = False Then
                    If Volume.ID = DsVols.Tables(0).Rows(i).Item("DISK_VOLUME_ID") Then
                        Volume.ID = DsVols.Tables(0).Rows(i + 1).Item("DISK_VOLUME_ID")
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
            raiseerror(ex)
            Return Nothing
        End Try
    End Function

    Public Shared Function LoadVolume(ByVal docTypeId As Int64, ByVal dsVols As DataSet) As IVolume
        Dim Volume As IVolume = New Volume()
        Dim ErrorBuilder As New StringBuilder()
        Dim i As Int32

        Try
            For i = 0 To dsVols.Tables(0).Rows.Count - 1
                Try
                    Volume.ID = dsVols.Tables(0).Rows(i).Item("DISK_VOLUME_ID")
                    Volume = GetVolumeData(Volume.ID)
                    If IsNothing(Volume) Then
                    ElseIf Volume.VolumeState <> VolumeStates.VolumenListo And Volume.VolumeState <> VolumeStates.VolumenEnPreparacion Then
                        ErrorBuilder.Append("Volumen no disponible")
                    ElseIf Volume.Type <> VolumeTypes.DataBase Then
                        If VerificoPath(Volume) = False Then
                            ErrorBuilder.Append("PATH del Volumen Incorrecto")
                            'ElseIf CreateDirsDocTypeOffset(Volume, docTypeId) = False Then
                            ' ErrorBuilder.Append("No se pueden crear los OFFSET del Volumen")
                        ElseIf GetOffSet(Volume) = Nothing Then
                            ErrorBuilder.Append("Volumen Lleno")
                        Else
                            Return Volume
                        End If
                    Else
                        Return Volume
                    End If
                    Return Nothing
                Catch ex As Exception
                    If (String.IsNullOrEmpty(ErrorBuilder.ToString())) Then
                        Throw New Exception(ex.ToString())
                    Else
                        UserBusiness.Rights.SaveAction(Volume.ID, ObjectTypes.ErrorLog, RightsType.Use, ErrorBuilder.ToString())
                        Throw New Exception("Volumen No Disponible -- " + ErrorBuilder.ToString)
                    End If
                End Try
            Next


        Catch ex As Exception
            If (String.IsNullOrEmpty(ErrorBuilder.ToString())) Then
                Throw New Exception(ex.ToString())
            Else
                UserBusiness.Rights.SaveAction(Volume.ID, ObjectTypes.ErrorLog, RightsType.Use, ErrorBuilder.ToString())
                Throw New Exception("Volumen No Disponible -- " + ErrorBuilder.ToString)
            End If
        End Try

        ErrorBuilder.Remove(0, ErrorBuilder.Length)
        ErrorBuilder = Nothing

        Return Volume
    End Function

    'Public Shared Function LoadVolume(ByVal docTypeId As Int64, ByVal dsVols As DataSet, Optional ByRef t As Transaction = Nothing) As IVolume
    '    Dim Volume As IVolume = New Volume()
    '    'Dim ErrorMsg As String = ""
    '    Dim ErrorBuilder As New StringBuilder()

    '    Try
    '        Dim i As Int32
    '        For i = 0 To dsVols.Tables(0).Rows.Count - 1
    '            Try
    '                Volume.ID = dsVols.Tables(0).Rows(i).Item("DISK_VOLUME_ID")
    '                Volume = GetVolumeData(Volume, t)
    '                If IsNothing(Volume) Then
    '                ElseIf Volume.VolumeState <> VolumeStates.VolumenListo And Volume.VolumeState <> VolumeStates.VolumenEnPreparacion Then
    '                    ErrorBuilder.Append("Volumen no disponible")
    '                ElseIf Volume.Type <> VolumeTypes.DataBase Then
    '                    If VerificoPath(Volume) = False Then
    '                        ErrorBuilder.Append("PATH del Volumen Incorrecto")
    '                    ElseIf CreateDirsDocTypeOffset(Volume, docTypeId) = False Then
    '                        ErrorBuilder.Append("No se pueden crear los OFFSET del Volumen")
    '                    ElseIf GetOffSet(Volume, t) = Nothing Then
    '                        ErrorBuilder.Append("Volumen Lleno")
    '                    Else
    '                        Return Volume
    '                    End If
    '                Else
    '                    Return Volume
    '                End If

    '            Catch ex As Exception
    '                If (String.IsNullOrEmpty(ErrorBuilder.ToString())) Then
    '                    Throw New Exception(ex.ToString())
    '                Else
    '                    UserBusiness.Rights.SaveAction(Volume.ID, ObjectTypes.ErrorLog, RightsType.Use, ErrorBuilder.ToString(), Membership.MembershipHelper.CurrentUser.ID, t)
    '                    Throw New Exception("Volumen No Disponible -- " + ErrorBuilder.ToString)
    '                End If
    '            End Try
    '        Next

    '        Volume = GetTemporalVolume(docTypeId, t)

    '        If IsNothing(Volume.path) = False Then
    '            Return Volume
    '        End If

    '        ' Para que tira una exception si llega a esta linea de codigo? Si ocurre una Exception
    '        ' la agarra el try-catch
    '        ' Throw New Exception("Volumen No Disponible -- " & ErrorBuilder.ToString)
    '    Catch ex As Exception
    '        If (String.IsNullOrEmpty(ErrorBuilder.ToString())) Then
    '            Throw New Exception(ex.ToString())
    '        Else
    '            UserBusiness.Rights.SaveAction(Volume.ID, ObjectTypes.ErrorLog, RightsType.Use, ErrorBuilder.ToString(), Membership.MembershipHelper.CurrentUser.ID, t)
    '            Throw New Exception("Volumen No Disponible -- " + ErrorBuilder.ToString)
    '        End If
    '    End Try


    '    ErrorBuilder.Remove(0, ErrorBuilder.Length)
    '    ErrorBuilder = Nothing
    '    'Volume = VerificoCantidadVolumenesYCargoelprimero(DsVols)
    '    'Volume = ObtengoElSiguienteVolumen(DsVols, DocTypeId)

    '    Return Volume
    'End Function
#End Region

    Public Shared Sub DeleteFile(ByVal path As String)
        Dim file As New FileInfo(path)
        If file.Exists = True Then
            file.Delete()
        End If
    End Sub

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
        '    ''Dim parNames() As String = {"DocTypeId"}
        '    'Dim parTypes() As Object = {13}
        '    Dim parValues() As Object = {DoctypeId}
        '    'volgroup = Server.Con.ExecuteScalar("ZDtGetDoctypes_pkg.ZDtGetDgIdByDtId",  parValues)
        '    volgroup = Server.Con.ExecuteScalar("zsp_doctypes_100.GetDiskGroupId",  parValues)
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
            ''Dim parNames() As String = {"volgroup", "io_cursor"}
            'Dim parTypes() As Object = {13, 5}
            Dim parValues() As Object = {volgroup, 2}
            'ds = Server.Con.ExecuteDataset("ZDgRDvGet_pkg.ZDgRDvGetDvIdByDgId",  parValues)
            ds = Server.Con.ExecuteDataset("zsp_volume_100.GetDocGroupRDocVolByDgId", parValues)
        Else
            Dim parValues() As Object = {volgroup}
            Try
                'ds = Server.Con.ExecuteDataset("ZDgRDvGetDvIdByDgId", parvalues)
                ds = Server.Con.ExecuteDataset("zsp_volume_100_GetDocGroupRDocVolByDgId", parValues)
            Catch ex As Exception
                raiseerror(ex)
            End Try
        End If
        Dim i As Int16
        For i = 0 To ds.Tables(0).Rows.Count - 1
            'sql = "Update disk_volume set Disk_vol_files=0 where disk_vol_id=" & Integer.Parse(ds.Tables(0).Rows(i).Item(0))
            'Server.Con.ExecuteNonQuery(CommandType.Text, sql)
            If Server.isOracle Then
                ''Dim parNames() As String = {"Archs", "DiskVolId"}
                'Dim parTypes() As Object = {13, 13}
                Dim parValues() As Object = {0, Integer.Parse(ds.Tables(0).Rows(i).Item(0))}
                'Server.Con.ExecuteNonQuery("ZDtUpdDiskVolume_pkg.ZDvUpdFilesByVId",  parValues)
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
            ''Dim parNames() As String = {"DocCount", "DocTypeId"}
            'Dim parTypes() As Object = {13, 13}
            Dim parValues() As Object = {0, DoctypeId}
            'Server.Con.ExecuteNonQuery("ZDtUpdDoctypes_pkg.ZDtUpdDoccountByDtId",  parValues)
            Server.Con.ExecuteNonQuery("zsp_doctypes_100.UpdDocCountById", parValues)
        Else
            Dim parValues() As Object = {0, DoctypeId}
            Try
                'Server.Con.ExecuteNonQuery("ZDtUpdDoccountByDtId", parvalues)
                Server.Con.ExecuteNonQuery("zsp_doctypes_100_UpdDocCountById", parValues)
            Catch ex As Exception
                raiseerror(ex)
            End Try
        End If
    End Sub
    Public Shared Sub DeleteVolume(ByVal Volume As IVolume)
        Dim strDelete As String = "DELETE FROM Disk_Volume WHERE Disk_Vol_Id = " & Volume.ID
        Server.Con.ExecuteNonQuery(CommandType.Text, strDelete)
    End Sub

    Public Overrides Sub Dispose()

    End Sub

    Public Shared Function GetVolumeType(ByVal volumeId As Int32) As Int32
        If Not Cache.Volumes.hsVolumeTypes.Contains(volumeId) Then
            Cache.Volumes.hsVolumeTypes.Add(volumeId, VolumesFactory.GetVolumeType(volumeId))
        End If
        Return Cache.Volumes.hsVolumeTypes.Item(volumeId)
    End Function

    Public Shared Sub ClearHashTables()
        If Not IsNothing(Cache.Volumes.hsVolumeTypes) Then
            Cache.Volumes.hsVolumeTypes.Clear()
            Cache.Volumes.hsVolumeTypes = Nothing
            Cache.Volumes.hsVolumeTypes = New Hashtable()
        End If
    End Sub

End Class