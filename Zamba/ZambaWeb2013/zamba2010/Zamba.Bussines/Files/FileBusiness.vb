﻿Imports System.IO
Imports System.IO.Path

Public Class FileBusiness

    ''' <summary>
    ''' Obtiene un nombre único para un archivo. Si ya existe uno con el mismo nombre le asigna
    ''' como nombre el del archivo mas un contador entre paréntesis.
    ''' </summary>
    ''' <param name="filePath">Path del archivo</param>
    ''' <param name="fileName">Nombre del archivo</param>
    ''' <param name="fileExtension">Extensión del archivo</param>
    ''' <returns>Path completo del archivo con un nombre único</returns>
    ''' <remarks>Creado para resolver los conflictos de nombres de archivos repetidos.</remarks>
    ''' <history>
    ''' [Tomás] - 18/05/2009 - Created
    '''</history>
    Public Shared Function GetUniqueFileName(ByVal filePath As String, ByVal fileName As String, ByVal fileExtension As String) As String
        Try
            Dim identifier As String = String.Empty
            Dim cont As Int64 = 0

            'Valida que la extensión contenga el punto
            If Not fileExtension.StartsWith(".") AndAlso Not fileName.EndsWith(".") Then
                fileExtension.Insert(0, ".")
            End If

            'Valida que entre el path y el nombre del archivo exista una barra
            If Not filePath.EndsWith("\") AndAlso Not fileName.StartsWith("\") Then
                filePath &= "\"
            End If

            'Valida que el archivo no exista en el directorio. 
            'En caso de existir le agrega un contador entre paréntesis.
            While IO.File.Exists(filePath & fileName & identifier & fileExtension)
                identifier = "(" & cont.ToString & ")"
                cont += 1
            End While

            fileName = fileName & identifier
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Return (filePath & fileName & fileExtension)
    End Function

    ''' <summary>
    ''' Obtiene un nombre único para un archivo. Si ya existe uno con el mismo nombre le asigna
    ''' como nombre el del archivo mas un contador entre paréntesis.
    ''' </summary>
    ''' <param name="filePath">Path del archivo</param>
    ''' <param name="fileName">Nombre del archivo con su extensión</param>
    ''' <returns>Path completo del archivo con un nombre único</returns>
    ''' <remarks>Creado para resolver los conflictos de nombres de archivos repetidos.</remarks>
    ''' <history>
    ''' [Tomás] - 18/05/2009 - Created
    '''</history>
    Public Shared Function GetUniqueFileName(ByVal filePath As String, ByVal fileName As String) As String
        Dim extension As String = String.Empty
        Dim id As String = String.Empty
        Dim cont As Int64 = 0
        Try
            'Guarda la extension
            extension = GetExtension(fileName)

            'Guarda el nombre del archivo
            fileName = GetFileNameWithoutExtension(fileName)

            'Valida que entre el path y el nombre del archivo exista una barra
            If Not filePath.EndsWith("\") Then
                filePath &= "\"
            End If

            'Valida que el archivo no exista en el directorio. 
            'En caso de existir le agrega un contador entre paréntesis.
            While IO.File.Exists(filePath & fileName & id & extension)
                id = "(" & cont.ToString & ")"
                cont += 1
            End While

            fileName = fileName & id
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Return (filePath & fileName & extension)
    End Function

    ''' <summary>
    ''' Obtiene un nombre único para un archivo. Si ya existe uno con el mismo nombre le asigna
    ''' como nombre el del archivo mas un contador entre paréntesis.
    ''' </summary>
    ''' <param name="filePath">Path completo del archivo</param>
    ''' <returns>Path completo del archivo con un nombre único</returns>
    ''' <remarks>Creado para resolver los conflictos de nombres de archivos repetidos.</remarks>
    ''' <history>
    ''' [Tomás] - 05/05/2010 - Created
    '''</history>
    Public Shared Function GetUniqueFileName(ByVal filePath As String) As String
        If IO.File.Exists(filePath) Then
            Dim extension As String = String.Empty
            Dim id As String = String.Empty
            Dim cont As Int64 = 0
            Dim fileName As String = String.Empty
            Try
                'Guarda la extension
                extension = GetExtension(filePath)

                'Guarda el nombre del archivo
                fileName = GetFileNameWithoutExtension(filePath)

                'Remueve el archivo del path completo
                filePath = GetDirectoryName(filePath)

                'Valida que entre el path y el nombre del archivo exista una barra
                If Not filePath.EndsWith("\") Then
                    filePath &= "\"
                End If

                'Valida que el archivo no exista en el directorio. 
                'En caso de existir le agrega un contador entre paréntesis.
                While IO.File.Exists(filePath & fileName & id & extension)
                    id = "(" & cont.ToString & ")"
                    cont += 1
                End While

                fileName = fileName & id
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Return (filePath & fileName & extension)
        Else
            Return filePath
        End If
    End Function

    ''' <summary>
    ''' Copia todo el contenido de un directorio a otro.
    ''' TODO: LEER REMARKS
    ''' </summary>
    ''' <param name="rutaOrigen">Ruta de origen desde donde se copiarán todos los directorios y archivos</param>
    ''' <param name="rutaDestino">Ruta de destino</param>
    ''' <remarks>Por el momento está diseñado para encontrar nada más directorios o archivos dentro de un directorio pero no ambos.</remarks>
    ''' <history>
    ''' [Tomás] - 06/05/2010 - Created
    '''</history>
    Public Shared Sub CopyFullDirectory(ByVal rutaOrigen As String, ByVal rutaDestino As String)
        'Si el directorio de origen no existe lanza exception
        If Not IO.Directory.Exists(rutaOrigen) Then
            Throw New IO.IOException("La ruta de origen no existe.")
        End If

        'Si el directorio de destino no existe se crea
        If Not IO.Directory.Exists(rutaDestino) Then
            IO.Directory.CreateDirectory(rutaDestino)
        End If

        'Copia recursiva de todo el contenido de origen a destino.
        CopyDirectory(rutaOrigen, rutaDestino)
    End Sub

    ''' <summary>
    ''' Copia todo el contenido de un directorio a otro.
    ''' </summary>
    ''' <param name="rutaOrigen">Ruta de origen donde se buscará la existencia de más directorios o finalmente los archivos físicos</param>
    ''' <param name="rutaDestino">Ruta de destino</param>
    ''' <history>
    ''' [Tomás] - 06/05/2010 - Created
    '''</history>
    Private Shared Sub CopyDirectory(ByVal rutaOrigen As String, ByVal rutaDestino As String)
        Dim rutas As String() = IO.Directory.GetDirectories(rutaOrigen)

        'Se verifica que la ruta de origen contenga directorios dentro.
        If rutas.Length > 0 Then
            Dim rutaD As String
            For Each rutaO As String In rutas
                'Se arma la ruta de destino.
                rutaD = rutaDestino + rutaO.Substring(rutaO.LastIndexOf("\"))
                'Si el directorio de destino no existe se crea.
                If Not IO.Directory.Exists(rutaDestino) Then
                    IO.Directory.CreateDirectory(rutaDestino)
                End If
                'Se verifica por más directorios o archivos dentro de la carpeta.
                CopyDirectory(rutaO, rutaD)
            Next
        Else
            'Se realiza la copia de los archivos.
            For Each archivo As String In IO.Directory.GetFiles(rutaOrigen)
                IO.File.Copy(archivo, rutaDestino + "\" + IO.Path.GetFileName(archivo))
            Next
        End If
    End Sub

    ''' <summary>
    ''' Genera un archivo
    ''' </summary>
    ''' <param name="Path"></param>
    ''' <param name="content"></param>
    Public Shared Sub CreateFile(FilePath As String, content As String)
        Try
            Directory.CreateDirectory(GetDirectoryName(FilePath))
            File.WriteAllText(FilePath, content)
        Catch ex As Exception
        End Try
    End Sub

End Class