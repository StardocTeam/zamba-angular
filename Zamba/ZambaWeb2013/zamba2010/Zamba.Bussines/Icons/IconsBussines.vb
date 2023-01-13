Imports zamba.Data
Imports Zamba.Core
Public Class IconsBusiness
    Inherits ZClass

    Public Shared Function GetIconsPath(ByVal IconsType As IconsType) As String
        Return IconsFactory.GetIconsPath(IconsType)
    End Function
    Public Shared Function GetIconsPathString(ByVal IconKey As String) As String
        Return IconsFactory.GetIconsPathString(IconKey)
    End Function
    Public Shared Function GetServerImages() As DsImageServer
        Return IconsFactory.GetServerImages()
    End Function
    Public Enum IconsType
        WorkFlow
    End Enum

    Public Shared Sub SetIconspath(ByVal Type As String, ByVal Path As String)
        IconsFactory.SetIconspath(Type, Path)
    End Sub
    Public Shared Sub UpdateIconspath(ByVal Type As String, ByVal Path As String)
        IconsFactory.UpdateIconspath(Type, Path)
    End Sub

    Public Shared Function GetPicturesPath(ByVal Type As String) As String
        Return IconsFactory.GetPicturesPath(Type)
    End Function
    Public Shared Function GetServerImagesPath() As String
        Return IconsFactory.GetServerImagesPath
    End Function



    Public Overrides Sub Dispose()

    End Sub

#Region "Public Methods"

    ' Se agrega a zamba un nuevo icono...
    Public Shared Function InsertIcon(ByVal Type As String, _
    ByVal Image As String, _
    ByVal Description As String, ByVal dsicons As DsIcons) As String
        Try
            ' Tomo el path del archivo de configuracion xml del icono...
            Dim Dir As New IO.DirectoryInfo(IconsBusiness.GetIconsPath(Type))

            ' Toma el path completo del archivo de configuracion xml 
            ' del icono + nombre archivo...
            Dim fi As New IO.FileInfo(Dir.FullName & "\Icons" & Type & ".xml")

            ' Si no existe lo crea....
            If fi.Exists = False Then
                dsicons.WriteXmlSchema(fi.FullName)
            End If

            ' Se copia la imagen a el path anterior...
            Dim Fa As New IO.FileInfo(Image)
            Fa.CopyTo(fi.Directory.FullName & "\" & Fa.Name, True)


            ' Se crea una fila para el icono...
            Dim IconRow As DsIcons.IconsRow = dsicons.Icons.NewIconsRow

            ' Se setean datos...
            IconRow.Id = CoreData.GetNewID(IdTypes.IConId)
            IconRow.File = fi.Directory.FullName & "\" & Fa.Name
            IconRow.Description = Description

            ' Se agrega...
            dsicons.Icons.AddIconsRow(IconRow)
            dsicons.Icons.AcceptChanges()

            dsicons.WriteXml(fi.FullName)

            Return fi.Directory.FullName & "\" & Fa.Name
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
        Return Nothing
    End Function
    Public Shared Sub DeleteIcon(ByVal Type As String, ByVal IconId As Int32, ByVal dsicons As DsIcons)
        Try
            Dim Dir As New IO.DirectoryInfo(IconsBusiness.GetIconsPath(Type))
            If Dir.Exists = False Then
                Throw New Exception("No se puede encontrar el directorio de los Iconos")
            End If
            Dim i As Int32
            For i = 0 To DsIcons.Icons.Count - 1
                If DsIcons.Icons(i).Id = IconId Then
                    Dim fi As New IO.FileInfo(DsIcons.Icons(i).File)
                    If fi.Exists Then fi.Delete()
                    DsIcons.Icons(i).Delete()
                    DsIcons.AcceptChanges()
                End If
            Next
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Public Shared Sub EditIcon(ByVal Type As String, ByVal IconId As Int32, ByVal NewImage As String, ByVal dsicons As DsIcons)
        Try
            Dim Dir As New IO.DirectoryInfo(IconsBusiness.GetIconsPath(Type))
            If Dir.Exists = False Then
                Throw New Exception("No se puede encontrar el directorio de los Iconos")
            End If
            Dim i As Int32
            For i = 0 To dsicons.Icons.Count - 1
                If dsicons.Icons(i).Id = IconId Then
                    Dim Fa As New IO.FileInfo(NewImage)
                    Fa.CopyTo(dsicons.Icons(i).File, True)
                End If
            Next
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
#End Region


    Public Shared Function InsertPicture(ByVal Type As String, ByVal Image As String, ByVal Description As String, ByVal dspictures As DsIcons) As String
        Try
            Dim Dir As New IO.DirectoryInfo(IconsBusiness.GetPicturesPath(Type))
            Dim fi As New IO.FileInfo(Dir.FullName & "\Pictures" & Type & ".xml")
            If fi.Exists = False Then
                dspictures.WriteXmlSchema(fi.FullName)
            End If
            Dim Fa As New IO.FileInfo(Image)
            Fa.CopyTo(fi.Directory.FullName & "\" & Fa.Name, True)
            Dim PictureRow As DsIcons.IconsRow = dspictures.Icons.NewIconsRow
            PictureRow.Id = CoreData.GetNewID(IdTypes.IConId)
            PictureRow.File = fi.Directory.FullName & "\" & Fa.Name
            PictureRow.Description = Description
            dspictures.Icons.AddIconsRow(PictureRow)
            dspictures.Icons.AcceptChanges()
            dspictures.WriteXml(fi.FullName)
            Return fi.Directory.FullName & "\" & Fa.Name
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
        Return Nothing
    End Function
    Public Shared Sub DeletePicture(ByVal Type As String, ByVal PictureId As Int32, ByVal dspictures As DsIcons)
        Try
            Dim Dir As New IO.DirectoryInfo(IconsBusiness.GetPicturesPath(Type))
            If Dir.Exists = False Then
                Throw New Exception("No se puede encontrar el directorio de los Pictureos")
            End If
            Dim i As Int32
            For i = 0 To dspictures.Icons.Count - 1
                If dspictures.Icons(i).Id = PictureId Then
                    Dim fi As New IO.FileInfo(dspictures.Icons(i).File)
                    If fi.Exists Then fi.Delete()
                    dspictures.Icons(i).Delete()
                    dspictures.AcceptChanges()
                End If
            Next
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Public Shared Sub EditPicture(ByVal Type As String, ByVal PictureId As Int32, ByVal NewImage As String, ByVal dspictures As DsIcons)
        Try
            Dim Dir As New IO.DirectoryInfo(IconsBusiness.GetPicturesPath(Type))
            If Dir.Exists = False Then
                Throw New IO.FileNotFoundException("No se puede encontrar el directorio de los Pictures")
            End If
            Dim i As Int32
            For i = 0 To dspictures.Icons.Count - 1
                If dspictures.Icons(i).Id = PictureId Then
                    Dim Fa As New IO.FileInfo(NewImage)
                    Fa.CopyTo(dspictures.Icons(i).File, True)
                End If
            Next
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub


End Class
