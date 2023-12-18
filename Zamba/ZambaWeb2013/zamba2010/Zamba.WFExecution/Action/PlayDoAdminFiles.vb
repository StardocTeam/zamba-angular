﻿Imports Zamba.Core

Public Class PlayDoAdminFiles

    Private myRule As IDoAdminFiles
    Private rootPaths As New List(Of String)
    Private source As Object
    Private targetPath As String
    Private errorList As New List(Of String)

    Sub New(ByVal rule As IDoAdminFiles)
        Me.myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Cantidad de results a ejecutar: " & results.Count)
        Dim exitsub As Boolean = False

        Try
            If Not String.IsNullOrEmpty(myRule.SourceVar) AndAlso Not (myRule.Action <> FileActions.Delete AndAlso String.IsNullOrEmpty(myRule.TargetPath)) Then

                For Each r As ITaskResult In results
                    'Se traduce la ruta de origen
                    source = WFRuleParent.ObtenerValorVariableObjeto(myRule.SourceVar)

                    'Se traduce la ruta de destino
                    Dim VarInterReglas As New VariablesInterReglas()
                    targetPath = VarInterReglas.ReconocerVariablesValuesSoloTexto(myRule.TargetPath).Trim
                    VarInterReglas = Nothing

                    If Not String.IsNullOrEmpty(targetPath) Then
                        targetPath = TextoInteligente.ReconocerCodigo(targetPath, r).Trim
                    Else
                        targetPath = TextoInteligente.ReconocerCodigo(myRule.TargetPath, r).Trim
                    End If

                    'Verifica el tipo de datos de la variable de origen.
                    Select Case source.GetType.ToString
                        Case "System.String"
                            'Si es de tipo string, busca por el caracter | para separar las diferentes rutas y las pasa todas a un list(of string).
                            Dim sep() As String = {"|"}
                            For Each root As String In TextoInteligente.ReconocerCodigo(source.ToString, r).Split(sep, StringSplitOptions.RemoveEmptyEntries)
                                FormatAndAddPaths(root, r)
                            Next
                        Case "System.String[]"
                            For Each root As String In DirectCast(source, System.Array)
                                FormatAndAddPaths(root, r)
                            Next
                        Case "System.Data.DataSet"
                            For Each row As DataRow In DirectCast(source, DataSet).Tables(0).Rows
                                FormatAndAddPaths(row.Item(0).ToString, r)
                            Next
                        Case "System.Data.DataTable"
                            For Each row As DataRow In DirectCast(source, DataTable).Rows
                                FormatAndAddPaths(row.Item(0).ToString, r)
                            Next
                        Case Else
                            exitsub = True
                            Throw New Exception("El tipo de dato de la variable de origen no es reconocido")
                    End Select

                    'Verifica si debe continuar o no.
                    If Not exitsub Then
                        'Dependiendo de la accion copia, mueve o borra.
                        Select Case myRule.Action
                            Case FileActions.Copy
                                Copy()

                            Case FileActions.Delete
                                Delete()
                            Case FileActions.Move
                                Move()
                        End Select
                    End If
                Next
            Else
                Throw New Exception("La variable de origen y/o destino no se encuentra configurada")
            End If
        Finally
            If errorList IsNot Nothing Then
                SaveErrors()
                errorList.Clear()
            End If
            If rootPaths IsNot Nothing Then
                rootPaths.Clear()
            End If
        End Try

        Return results
    End Function

    Private Sub Copy()
        For Each root As String In rootPaths
            CopyFullDirectory(root, targetPath)
        Next
    End Sub

    Private Sub Move()
        For Each root As String In rootPaths
            MoveFullDirectory(root, targetPath)
        Next
    End Sub

    Private Sub Delete()
        If myRule.DeleteVarFiles Then
            For Each root As String In rootPaths
                DeleteFullDirectory(root)
            Next
        Else
            DeleteFullDirectory(targetPath)
        End If
    End Sub


#Region "Metodos adaptados de la clase FileBusiness"
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
    Public Sub CopyFullDirectory(ByVal rutaOrigen As String, ByVal rutaDestino As String)
        'Si el directorio de origen no existe lanza exception
        If Not IO.Directory.Exists(rutaOrigen) Then
            errorList.Add("La ruta de origen """ & rutaOrigen & """ no existe.")
        Else
            Try
                'Si el directorio de destino no existe se crea
                If Not IO.Directory.Exists(rutaDestino) Then
                    IO.Directory.CreateDirectory(rutaDestino)
                End If

                If IO.File.GetAttributes(rutaOrigen).Directory = IO.FileAttributes.Directory Then
                    'Copia recursiva de todo el contenido de origen a destino.
                    CopyDirectory(rutaOrigen, rutaDestino)
                Else
                    IO.File.Copy(rutaOrigen, rutaDestino & "\" & IO.Path.GetFileName(rutaOrigen), myRule.Overwrite)
                End If

            Catch ex As Exception
                ZCore.raiseerror(ex)
                errorList.Add("Error al crear el directorio " & rutaDestino & vbCrLf & ex.Message)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Copia todo el contenido de un directorio a otro.
    ''' </summary>
    ''' <param name="rutaOrigen">Ruta de origen donde se buscará la existencia de más directorios o finalmente los archivos físicos</param>
    ''' <param name="rutaDestino">Ruta de destino</param>
    ''' <history>
    ''' [Tomás] - 06/05/2010 - Created
    '''</history>
    Private Sub CopyDirectory(ByVal rutaOrigen As String, ByVal rutaDestino As String)

        'Se realiza la copia de los archivos.
        For Each archivo As String In IO.Directory.GetFiles(rutaOrigen)
            Try
                If myRule.Overwrite Then
                    IO.File.SetAttributes(archivo, IO.FileAttributes.Normal)
                End If

                IO.File.Copy(archivo, rutaDestino + "\" + IO.Path.GetFileName(archivo), myRule.Overwrite)
            Catch ex As Exception
                ZCore.raiseerror(ex)
                errorList.Add(ex.Message)
            End Try
        Next

        'Se verifica que la ruta de origen contenga directorios dentro.
        Dim rutas As String() = IO.Directory.GetDirectories(rutaOrigen)
        If rutas.Length > 0 Then
            Dim rutaD As String
            For Each rutaO As String In rutas
                Try
                    'Se arma la ruta de destino.
                    rutaD = rutaDestino + rutaO.Substring(rutaO.LastIndexOf("\"))
                    'Si el directorio de destino no existe se crea.
                    If Not IO.Directory.Exists(rutaD) Then
                        IO.Directory.CreateDirectory(rutaD)
                    End If
                    'Se verifica por más directorios o archivos dentro de la carpeta.
                    CopyDirectory(rutaO, rutaD)
                Catch ex As Exception
                    ZCore.raiseerror(ex)
                    errorList.Add(ex.Message)
                End Try
            Next
        End If
    End Sub

    ''' <summary>
    ''' MUEVE todo el contenido de un directorio a otro.
    ''' TODO: LEER REMARKS
    ''' </summary>
    ''' <param name="rutaOrigen">Ruta de origen desde donde se copiarán todos los directorios y archivos</param>
    ''' <param name="rutaDestino">Ruta de destino</param>
    ''' <remarks>Por el momento está diseñado para encontrar nada más directorios o archivos dentro de un directorio pero no ambos.</remarks>
    ''' <history>
    ''' [Tomás] - 06/05/2010 - Created
    '''</history>
    Public Sub MoveFullDirectory(ByVal rutaOrigen As String, ByVal rutaDestino As String)
        'Si el directorio de origen no existe lanza exception
        If Not IO.Directory.Exists(rutaOrigen) Then
            errorList.Add("La ruta de origen """ & rutaOrigen & """ no existe.")
        Else
            Try
                'Si el directorio de destino no existe se crea
                If Not IO.Directory.Exists(rutaDestino) Then
                    IO.Directory.CreateDirectory(rutaDestino)
                End If

                Dim cantErrors As Int32 = errorList.Count

                'Copia recursiva de todo el contenido de origen a destino.
                CopyFullDirectory(rutaOrigen, rutaDestino)

                If cantErrors = errorList.Count Then
                    DeleteFullDirectory(rutaOrigen)
                Else
                    Throw New IO.IOException("Han ocurrido errores en la copia, por lo tanto los archivos de origen no se eliminaran")
                End If

            Catch ex As Exception
                ZCore.raiseerror(ex)
                errorList.Add(ex.Message)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' ELIMINA todo el contenido de un directorio.
    ''' TODO: LEER REMARKS
    ''' </summary>
    ''' <param name="rutaOrigen">Ruta de origen desde donde se copiarán todos los directorios y archivos</param>
    ''' <param name="rutaDestino">Ruta de destino</param>
    ''' <remarks>Por el momento está diseñado para encontrar nada más directorios o archivos dentro de un directorio pero no ambos.</remarks>
    ''' <history>
    ''' [Tomás] - 06/05/2010 - Created
    '''</history>
    Public Sub DeleteFullDirectory(ByVal rutaOrigen As String)
        Try
            If IO.File.GetAttributes(rutaOrigen) = IO.FileAttributes.Directory Then
                IO.Directory.Delete(rutaOrigen, True)
            Else
                IO.File.Delete(rutaOrigen)
            End If
        Catch ex As Exception
            ZCore.raiseerror(ex)
            errorList.Add(ex.Message)
        End Try
    End Sub
#End Region

    ''' <summary>
    ''' Guarda un error especifico en una variable de zamba
    ''' </summary>
    ''' <param name="description"></param>
    ''' <param name="cleanErrors"></param>
    ''' <remarks></remarks>
    Private Sub SaveErrors()
        Select Case myRule.OutputDataType
            Case FWDataTypes.Cadena 'El error sale en formato de cadena.
                Dim sb As New System.Text.StringBuilder()
                For Each logEr As String In errorList
                    sb.AppendLine(logEr)
                Next

                If VariablesInterReglas.ContainsKey(Me.myRule.ErrorVar) = False Then
                    VariablesInterReglas.Add(Me.myRule.ErrorVar, sb.ToString)
                Else
                    VariablesInterReglas.Item(Me.myRule.ErrorVar) = sb.ToString
                End If

            Case FWDataTypes.ListOfString 'El error sale en formato de lista.
                If VariablesInterReglas.ContainsKey(Me.myRule.ErrorVar) = False Then
                    VariablesInterReglas.Add(Me.myRule.ErrorVar, errorList)
                Else
                    VariablesInterReglas.Item(Me.myRule.ErrorVar) = errorList
                End If
        End Select
    End Sub
    Private Sub FormatAndAddPaths(ByVal root As String, ByVal r As ITaskResult)
        root = root.Trim
        If root.EndsWith("\") Then
            root = root.Remove(root.Length - 1, 1)
        End If
        rootPaths.Add(TextoInteligente.ReconocerCodigo(root, r).Trim)
    End Sub
End Class