Imports Zamba.Data
Imports Zamba.Tools
Imports System.IO

Public Class ResultBusinessExt

    Public Sub InsertIntoDOCB(ByVal res As IResult)
        Dim rsF As New ResultFactoryExt
        rsF.InsertResIntoDOCB(res, False)
    End Sub

    ''' <summary>
    ''' Obtiene todos los documentos que no estan en blob
    ''' </summary>
    ''' <param name="docTypeID"></param>
    ''' <remarks></remarks>
    Public Function GetNotInBlobDocuments(ByVal docTypeID As Int64) As DataSet
        Dim rsF As New ResultFactoryExt
        Return rsF.GetNotInBlobDocuments(docTypeID)
    End Function

    ''' <summary>
    ''' Verifica si el result posee un documento encriptado, verificando si existe su registro en ZSer
    ''' </summary>
    ''' <param name="docId"></param>
    ''' <param name="docTypeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function IsDocumentEncrypted(ByVal docId As Long, ByVal docTypeId As Long) As Boolean
        Dim sKey As String = docId & "|" & docTypeId
        If Not Cache.Results.CacheEncryptedDocumets.ContainsKey(sKey) Then
            Dim resF As New ResultFactoryExt
            Dim encResult As Long = resF.GetEnccryptionCount(docId, docTypeId)
            Cache.Results.CacheEncryptedDocumets(sKey) = encResult > 0
        End If

        Return Cache.Results.CacheEncryptedDocumets(sKey)
    End Function

    ''' <summary>
    ''' Obtiene la password de encriptacion/decriptacion, ya decodificada.
    ''' </summary>
    ''' <param name="docId"></param>
    ''' <param name="docTypeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetDecryptionPassword(ByVal docId As Long, ByVal docTypeId As Long) As String
        Dim sKey As String = docId & "|" & docTypeId

        If Not Cache.Results.CacheDecryptPassword.ContainsKey(sKey) Then
            Dim resF As New ResultFactoryExt
            Dim sPasswordDB As String = resF.GetFilePassword(docId, docTypeId)
            Dim decodedPass As String = Encryption.DecryptString(sPasswordDB)

            Cache.Results.CacheDecryptPassword(sKey) = decodedPass
        End If

        Return Cache.Results.CacheDecryptPassword(sKey)
    End Function

    ''' <summary>
    ''' Copia el documento a un temporal y lo desencripta si la password de parametro coincide con la de la DB.
    ''' </summary>
    ''' <param name="result"></param>
    ''' <param name="decrypPass"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CopyAndDecrypt(ByVal result As IResult, ByVal decrypPass As String) As String
        If String.IsNullOrEmpty(result.FullPath) Then
            Throw New NotImplementedException("Encriptacion no soportada para archivos blob")
        End If

        'Password con la que fue encriptado el archivo
        Dim docDBPassword As String = GetDecryptionPassword(result.ID, result.DocTypeId)
        'Password ingresada
        Dim decryptionPassword As String = result.ID & decrypPass & result.DocTypeId

        If docDBPassword = decryptionPassword Then

            Dim encTool As New CryptoFileManager()
            Dim fileKey As Byte()
            Dim fileKeyIV As Byte()
            encTool.getKeysFromPassword(decryptionPassword, fileKey, fileKeyIV)
            Dim dir As IO.DirectoryInfo
            dir = Zamba.Tools.EnvironmentUtil.GetTempDir("\OfficeTemp")
            If dir.Exists = False Then dir.Create()
            Dim name As String = result.ID.ToString()
            Dim fTemp As FileInfo

            If result.FullPath(result.FullPath.Length - 4) = "." Then
                name = name & result.FullPath.Substring(result.FullPath.Length - 4)
            ElseIf result.FullPath(result.FullPath.Length - 5) = "." Then
                name = name & result.FullPath.Substring(result.FullPath.Length - 5)
            End If
            If result.IsExcel OrElse result.IsWord Then
                'Esto evita el error de abrir 2 excel con el mismo nombre (abrirlo en resultado y tareas)
                fTemp = New FileInfo(FileBusiness.GetUniqueFileName(dir.FullName, name))
            Else
                fTemp = New FileInfo(dir.FullName & "\" & name)
            End If
            name = Nothing

            encTool.DecryptData(result.FullPath, fTemp.FullName, fileKey, fileKeyIV)

            UserBusiness.Rights.SaveAction(result.ID, ObjectTypes.Documents, RightsType.Decrypt, "El usuario: " & Membership.MembershipHelper.CurrentUser.Name & " ha desencriptado el documento")
            Return fTemp.FullName
        Else
            Return String.Empty
        End If
    End Function

    ''' <summary>
    ''' Encripta un documento y lo copia al full path del result
    ''' </summary>
    ''' <param name="result"></param>
    ''' <param name="localPath"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function EncryptAndCopy(ByVal result As IResult, ByVal localPath As String) As Boolean
        If String.IsNullOrEmpty(result.FullPath) OrElse String.IsNullOrEmpty(localPath) Then
            Throw New NotImplementedException("Encriptacion no soportada para archivos blob")
        End If

        Dim docDBPassword As String = GetDecryptionPassword(result.ID, result.DocTypeId)
        Dim fileKey As Byte()
        Dim fileKeyIV As Byte()
        Dim encTool As New CryptoFileManager()
        encTool.getKeysFromPassword(docDBPassword, fileKey, fileKeyIV)

        encTool.EncryptData(localPath, result.FullPath, fileKey, fileKeyIV)

        Return True
    End Function

    ''' <summary>
    ''' Actualiza un documento copiandolo a su repositorio siempre y cuando se detecten modificaciones
    ''' </summary>
    ''' <param name="res">Result</param>
    ''' <param name="rootPath">Ruta de origen</param>
    ''' <param name="destPath">Ruta de destino</param>
    ''' <remarks>El volumen puede ser físico o digital</remarks>
    Public Sub UpdateDocument(ByVal res As Result, ByVal rootPath As String, ByVal destPath As String)
        'Verifica si el volumen es de tipo base de datos o si todavia no se inserto el documento
        If res.Disk_Group_Id <> 0 AndAlso ZCore.filterVolumes(res.Disk_Group_Id).Type = VolumeTypes.DataBase Then
            'Se codifica el temporal
            Dim tempDocument As Byte() = FileEncode.Encode(rootPath)
            If Not tempDocument.SequenceEqual(res.EncodedFile) Then
                res.EncodedFile = tempDocument
                'Se actualiza el documento en la base
                Results_Factory.UpdateDOCB(res)
            End If
            tempDocument = Nothing
        Else
            If File.GetLastWriteTime(rootPath) <> File.GetLastWriteTime(destPath) Then
                'Si el volumen es en disco rigido simplemente lo copia
                Dim fa As FileInfo = New FileInfo(res.FullPath)
                fa.Attributes = FileAttributes.Archive

                'Si el documento esta encriptado, lo encripto y copio, sino lo copie normalmente
                If IsDocumentEncrypted(res.ID, res.DocTypeId) Then
                    EncryptAndCopy(res, rootPath)
                Else
                    File.Copy(rootPath, destPath, True)
                End If
            End If
        End If
    End Sub

    Public Function GetBlobDocument(ByVal docTypeId As Int64, ByVal docId As Int64) As BlobDocument
        Dim rfe As New ResultFactoryExt

        Dim blob As Byte() = rfe.GetBinaryDoc(docTypeId, docId)
        rfe = Nothing

        Dim extension As String = Results_Factory.GetFullName(docId, docTypeId)
        extension = Path.GetExtension(extension)

        Dim blobDoc As New BlobDocument
        blobDoc.BlobFile = blob
        blobDoc.Extension = extension
        blobDoc.ID = docId

        Return blobDoc
    End Function



    Public Function GetPreviewDocument(EntityId As Int64, DocumentId As Int64, UserId As Int64, ConvertToPDf As Boolean) As String
        Try
            Dim res As Result = Zamba.Core.Results_Business.GetResult(DocumentId, EntityId)

            If res IsNot Nothing Then
                Dim file__1 As Byte()
                Dim filename As String
                filename = res.FullPath

                If res.IsMsg Then
                    filename = filename.Replace(".msg", ".html")
                    res.Doc_File = res.Doc_File.ToLower().Replace(".msg", ".html")
                End If

                'Verifica que el volumen sea de tipo blob o que se encuentre forzada la opción de volumenes del mismo tipo
                If res.Disk_Group_Id > 0 AndAlso (VolumesBusiness.GetVolumeType(res.Disk_Group_Id) = VolumeTypes.DataBase OrElse (Not String.IsNullOrEmpty(ZOptBusiness.GetValue("ForceBlob")) AndAlso Boolean.Parse(ZOptBusiness.GetValue("ForceBlob")))) Then

                    Results_Business.LoadFileFromDB(res)
                End If

                'Verifica si el result contiene el documento guardado
                If res.EncodedFile IsNot Nothing Then
                    file__1 = res.EncodedFile
                Else
                    Dim sUseWebService As String = ZOptBusiness.GetValue("UseWebService")
                    'Verifica si debe utilizar el webservice para obtener el documento
                    If Not [String].IsNullOrEmpty(sUseWebService) AndAlso Boolean.Parse(sUseWebService) Then
                        Dim RB As New Results_Business
                        file__1 = RB.GetWebDocFileWS(res.DocTypeId, res.ID, UserId)
                        RB = Nothing
                    Else
                        file__1 = GetFileInBytes(res)
                    End If
                End If
                If file__1 IsNot Nothing Then
                    If res.IsWord AndAlso ConvertToPDf Then
                        Dim newFile As String = Path.Combine(Membership.MembershipHelper.AppTempPath, res.Doc_File)
                        Dim newPDFFile As String = Path.Combine(Membership.MembershipHelper.AppTempPath, res.Doc_File.ToLower().Replace(".docx", ".pdf").Replace(".doc", ".pdf").Replace(".dotx", ".pdf").Replace(".dot", ".pdf"))
                        If File.Exists(newPDFFile) = False Then
                            FileEncode.Decode(newFile, file__1)
                            Dim ST As New Zamba.FileTools.SpireTools()
                            ST.ConvertWordToPDF(newFile, newPDFFile)
                        End If
                        file__1 = FileEncode.Encode(newPDFFile)
                        filename = newPDFFile
                    End If

                    If (res.IsHTML OrElse res.IsRTF OrElse res.IsText OrElse res.IsXoml) AndAlso ConvertToPDf Then
                        Dim newFile As String = Path.Combine(Membership.MembershipHelper.AppTempPath, res.Doc_File)
                        Dim newPDFFile As String = Path.Combine(Membership.MembershipHelper.AppTempPath, res.Doc_File.ToLower().Replace(".html", ".pdf").Replace(".htm", ".pdf").Replace(".rtf", ".pdf").Replace(".txt", ".pdf").Replace(".xml", ".pdf").Replace(".xoml", ".pdf"))
                        If File.Exists(newPDFFile) = False Then
                            FileEncode.Decode(newFile, file__1)
                            Dim ST As New Zamba.FileTools.SpireTools()
                            ST.ConvertWordToPDF(newFile, newPDFFile)
                        End If
                        file__1 = FileEncode.Encode(newPDFFile)
                        filename = newPDFFile
                    End If

                    If (res.IsExcel) AndAlso ConvertToPDf Then
                        Dim newFile As String = Path.Combine(Membership.MembershipHelper.AppTempPath, res.Doc_File)
                        Dim newPDFFile As String = Path.Combine(Membership.MembershipHelper.AppTempPath, res.Doc_File.ToLower().Replace(".xlsx", ".pdf").Replace(".xls", ".pdf"))
                        If File.Exists(newPDFFile) = False Then
                            FileEncode.Decode(newFile, file__1)
                            Dim ST As New Zamba.FileTools.SpireTools()
                            ST.ConvertExcelToPDF(newFile, newPDFFile)
                        End If
                        file__1 = FileEncode.Encode(newPDFFile)
                        filename = newPDFFile
                    End If

                    If (res.IsImage) AndAlso ConvertToPDf AndAlso res.IsTif = False Then
                        Dim newFile As String = Path.Combine(Membership.MembershipHelper.AppTempPath, res.Doc_File)
                        Dim newPDFFile As String = Path.Combine(Membership.MembershipHelper.AppTempPath, res.Doc_File.ToLower().Replace(".jpg", ".pdf").Replace(".bmp", ".pdf").Replace(".png", ".pdf").Replace(".gif", ".pdf").Replace(".jpeg", ".pdf").Replace(".tif", ".pdf").Replace(".ico", ".pdf"))
                        If File.Exists(newPDFFile) = False Then
                            FileEncode.Decode(newFile, file__1)
                            Dim ST As New Zamba.FileTools.SpireTools()
                            ST.ConvertImageToPDF(newFile, newPDFFile)
                        End If
                        file__1 = FileEncode.Encode(newPDFFile)
                        filename = newPDFFile
                    End If

                    If (res.IsTif) AndAlso ConvertToPDf Then
                        Dim newFile As String = Path.Combine(Membership.MembershipHelper.AppTempPath, res.Doc_File)
                        Dim newPDFFile As String = Path.Combine(Membership.MembershipHelper.AppTempPath, res.Doc_File.ToLower().Replace(".tiff", ".pdf").Replace(".tif", ".pdf"))
                        If File.Exists(newPDFFile) = False Then
                            FileEncode.Decode(newFile, file__1)
                            Dim ST As New Zamba.FileTools.SpireTools()
                            ST.ConvertTIFFToPDF(newFile, newPDFFile)
                        End If
                        file__1 = FileEncode.Encode(newPDFFile)
                        filename = newPDFFile
                    End If

                    If (res.IsPowerpoint) AndAlso ConvertToPDf Then
                        Dim newFile As String = Path.Combine(Membership.MembershipHelper.AppTempPath, res.Doc_File)
                        Dim newPDFFile As String = Path.Combine(Membership.MembershipHelper.AppTempPath, res.Doc_File.ToLower().Replace(".pptx", ".pdf").Replace(".ppt", ".pdf"))
                        If File.Exists(newPDFFile) = False Then
                            FileEncode.Decode(newFile, file__1)
                            Dim ST As New Zamba.FileTools.SpireTools()
                            ST.ConvertPowerPointToPDF(newFile, newPDFFile)
                        End If
                        file__1 = FileEncode.Encode(newPDFFile)
                        filename = newPDFFile

                    End If
                End If

                'Verifica la existencia del documento buscado 
                If Not (file__1 IsNot Nothing AndAlso file__1.Length > 0) Then
                    Return Nothing
                End If

                Return filename
            End If
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Return Nothing
        End Try
    End Function


    Public Shared Function GetFileInBytes(res As IResult) As [Byte]()
        'Verifica si el volumen es de tipo base de datos o si todavia no se inserto el documento
        If (res.Disk_Group_Id <> 0 AndAlso DirectCast(VolumesBusiness.GetVolumeType(res.Disk_Group_Id), VolumeTypes) = VolumeTypes.DataBase) OrElse File.Exists(res.FullPath) = False Then
            'traer el archivo desde la base
            Results_Business.LoadFileFromDB(res)

            'Verifica si se debe codificar (en caso de ser la primera vez en abrirse)
            If (res.EncodedFile Is Nothing AndAlso res.FullPath IsNot Nothing) Then
                'Lo codifica
                res.EncodedFile = FileEncode.Encode(res.FullPath)
                'Se guarda en la base el archivo
                Results_Factory.InsertIntoDOCB(res, False)
            End If

            Return res.EncodedFile
        Else
            'Lo codifica
            res.EncodedFile = FileEncode.Encode(res.FullPath)
            Return res.EncodedFile
        End If
    End Function
End Class
