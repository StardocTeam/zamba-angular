Imports System.Text.RegularExpressions
Imports System.IO
Imports Zamba.Core
Imports System.Collections.Generic
Imports System.Web

Namespace HelperForm
    ''' <summary>
    ''' Clase helper para procesar documentos anidados de
    ''' formularios virtuales
    ''' Ej. <iframe id=Zamba_InnerDocType_7 width="100%" height="100%" frameborder="0"/>
    ''' </summary>
    ''' <history>
    ''' 	[osanchez]	07/04/2009	Created    
    ''' </history>
    Public Class HelperFormVirtual
#Region "Constantes"
        Const tagHtmlSRC As String = "src"
        Const tagHtmlIFRAME As String = "iframe"
        Const tagHtmlZamba_InnerDocType As String = "zamba_innerdoctype"
        Const tagHtmlZamba_Doc_ As String = "zamba_doc_"
        Const replazarAtributoIdRegex As String = "\s+?(id=\w*)"
        Const replazarAtributoSrcRegex As String = "src=[^\s=]*"
        Const parseHtmlRegex As String = "\<\/?(\w+)((\s+(\w+)(\s*=\s*(?:\"".*?\""|'.*?'|[^'\"">\s]+)?))+\s*|\s*)\/?\>"
        Const ParseHtmlError As String = "No existe el archivo"
#End Region

        ''' <summary>
        ''' Reemplaza el atributo id de un tag html
        ''' </summary>
        ''' <param name="tag">tag html</param>
        ''' <param name="id">valor de retorno del atributo id</param>
        ''' <history>
        ''' 	[osanchez]	07/04/2009	Created    
        ''' </history>
        Public Shared Sub replazarAtributoId(ByRef tag As String, ByVal id As Int64)
            'Reemplaza el atributo id
            Dim regex As String = replazarAtributoIdRegex
            Dim options As RegexOptions = ((RegexOptions.IgnorePatternWhitespace Or RegexOptions.Multiline) _
                        Or RegexOptions.IgnoreCase)
            Dim reg As Regex = New Regex(regex, options)

            tag = reg.Replace(tag, " id=Zamba_Doc_" & id.ToString() & " ")
        End Sub

        ''' <summary>
        ''' Reemplaza el atributo src de un tag html
        ''' </summary>
        ''' <param name="tag">tag html</param>
        ''' <param name="path">ruta del archivo a visualizar</param>
        ''' <history>
        ''' 	[osanchez]	07/04/2009	Created    
        ''' </history>
        Public Shared Sub replazarAtributoSrc(ByRef tag As String, ByVal path As String)
            Dim formatPath As String
            If Not IsNothing(tag) Then
                formatPath = " src=""file:///" & HttpUtility.HtmlEncode(path) & """ "

                'Reemplaza el atributo src
                If tag.Contains(tagHtmlSRC) Then
                    ''Se encuentra la primer comilla de la ruta
                    'Dim inicio As Int32 = tag.IndexOf("""", tag.IndexOf(tagHtmlSRC))
                    ''Se encuentra la cantidad de caracteres a remover. Se suma 1 para no contar la comilla inicial.
                    'Dim cantidad As Int32 = tag.IndexOf("""", inicio + 1) - inicio
                    ''Se remueve la ruta antigua. Se suma uno para contar la comilla final.
                    'tag = tag.Remove(inicio, cantidad + 1)
                    'inicio = Nothing
                    'cantidad = Nothing
                    tag = RemoveValueFromProperty(tag, tagHtmlSRC)

                    Dim regex As String = replazarAtributoSrcRegex
                    Dim options As System.Text.RegularExpressions.RegexOptions = ((System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace Or System.Text.RegularExpressions.RegexOptions.Multiline) _
                                Or System.Text.RegularExpressions.RegexOptions.IgnoreCase)
                    Dim reg As System.Text.RegularExpressions.Regex = New System.Text.RegularExpressions.Regex(regex, options)
                    'Se reemplaza la propiedad src del iframe
                    tag = reg.Replace(tag, formatPath)
                Else
                    'determina si el tag  esta escrito de esta forma < /> o
                    '<></>
                    If tag.Substring(tag.Length - 2, 1) = "/" Then
                        'El tag tiene el siguiente formato "</>"
                        tag = tag.Replace("/", " " & formatPath & " /")
                    Else
                        'El tag tiene el siguiente formato "<></>"
                        tag = tag.Replace(">", " " & formatPath & " >")
                    End If
                End If
            End If
        End Sub

        ''' <summary>
        ''' Remueve el tag 'src' y su valor de un string.
        ''' </summary>
        ''' <param name="tag">Tag a formatear</param>
        ''' <returns>Tag formateado sin el src</returns>
        ''' <history>
        ''' 	[Tomas] 08/03/2010  Created   
        ''' </history>
        Public Shared Function RemoveSrcTag(ByVal tag As String) As String
            Dim src As String = "src="
            If tag.Contains(src) Then
                'Posición inicial a remover.
                Dim iStart As Int32 = tag.IndexOf(src)
                'Posición final a remover.
                Dim iEnd As Int32 = tag.IndexOf(ControlChars.Quote, tag.IndexOf(src) + src.Length + 1)
                'Se remueve el tag src y su valor.
                tag = tag.Remove(iStart, iEnd - iStart + 1)
            End If
            Return tag
        End Function

        ''' <summary>
        ''' Busca la existencia de la propiedad y le remueve el valor 
        ''' que tiene entre comillas si es que lo tiene.
        ''' </summary>
        ''' <param name="html">html con propiedades</param>
        ''' <param name="id">propiedad a remover el valor</param>
        ''' <returns></returns>
        ''' <history>
        ''' 	[Tomas] 07/10/2009  Created   
        ''' </history>
        Public Shared Function RemoveValueFromProperty(ByVal html As String, ByVal propertyName As String) As String
            Dim propPosition As Int32 = html.IndexOf(propertyName)

            'Se encuentra la primer comilla de la ruta
            Dim inicio As Int32 = html.IndexOf("""", propPosition)

            ''Si luego del nombre de la propiedad no se encuentra en igual lo agrega
            'If html.Chars(inicio - 1).CompareTo(Char.Parse("=")) = 0 Then
            '    html = html.Replace(propertyName, propertyName & "=")
            'End If

            'Verifica que la posición de la comilla encontrada sea la de la propiedad, ya que
            'puede darse el caso en que la propiedad se encuentra sin valores (sin comillas).
            'El +1 es para contar el caracter del igual.
            If inicio = (propPosition + propertyName.Length + 1) Then
                'Se encuentra la cantidad de caracteres a remover. Se suma 1 para no contar la comilla inicial.
                Dim cantidad As Int32 = html.IndexOf("""", inicio + 1) - inicio
                'Se remueve la ruta antigua. Se suma uno para contar la comilla final.
                html = html.Remove(inicio, cantidad + 1)

                'ElseIf propPosition < inicio Then
                '   'Caso de que la propiedad no tenga algun valor cargado y no existan otras comillas por delante.    
                'ElseIf propPosition > inicio Then
                '   'Caso de que la propiedad no tenga algun valor cargado y el inicio sea el indice pero de otra propiedad.    
            End If

            Return html
        End Function

        ''' <summary>
        ''' Busca el tag html zamba_innerdoctype_ 
        ''' Si existe retorna true y el id del documento asociado
        ''' por medio del parametro id
        ''' </summary>
        ''' <param name="item">coincidencia de expresion regular</param>
        ''' <param name="id">atributo id del tag</param>
        ''' <returns>True si existe el  tag zamba_innerdoctype_</returns>
        ''' <history>
        ''' 	[osanchez]	07/04/2009	Created    
        ''' 	[Marcelo]	28/08/2009	Modified    
        ''' </history>
        Public Shared Function buscarTagZamba(ByVal item As Match) As Int64
            Dim isOk As Boolean = False
            Dim id As Int64 = 0
            '---Falta probar si el tag html tiene 1 solo elemento
            For Each tag As Capture In item.Groups(3).Captures
                Dim auxtag As String
                If Not IsNothing(tag.Value) Then
                    auxtag = tag.Value.ToLower()
                    If auxtag.Contains(tagHtmlZamba_InnerDocType) Then
                        Dim index As Int32
                        Dim aux As String
                        index = auxtag.IndexOf(tagHtmlZamba_InnerDocType)
                        aux = auxtag.Replace(tagHtmlZamba_InnerDocType, String.Empty)
                        aux = aux.Replace("/", String.Empty).Replace("_", String.Empty)
                        If aux.Substring(index, aux.Length - index).ToLower().Contains("original") Then
                            Return -1
                        ElseIf Int64.TryParse(aux.Substring(index, aux.Length - index), id) Then
                            Return id
                        End If
                        Exit For
                    End If
                End If
            Next
            Return id
        End Function

        ''' <summary>
        ''' Retorna una coleccion de tag html
        ''' </summary>
        ''' <param name="path">ruta del archivo a visualizar</param>
        ''' <returns>Coleccion con los tag html</returns>
        ''' <history>
        ''' 	[osanchez]	07/04/2009	Created    
        ''' </history>
        Public Shared Function ParseHtml(ByVal path As String, ByVal item As String) As MatchCollection
            If File.Exists(path) Then
                Dim html As String
                Using reader As New StreamReader(path)
                    html = reader.ReadToEnd()
                End Using

                If html.Contains(item) = False Then
                    Return Nothing
                End If

                Dim regex As String = parseHtmlRegex

                Dim options As RegexOptions = ((System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace Or System.Text.RegularExpressions.RegexOptions.Multiline) _
                            Or RegexOptions.IgnoreCase)

                Dim reg As System.Text.RegularExpressions.Regex = New System.Text.RegularExpressions.Regex(regex, options)

                Dim matches As MatchCollection
                matches = reg.Matches(html)
                Return matches
            Else
                Throw New FileNotFoundException(ParseHtmlError, path)
            End If
        End Function

        ''' <summary>
        ''' Modifica el contenido de un archivo html segun el 
        ''' contenido de una lista de modificaciones y guarda
        ''' el archivo
        ''' </summary>
        ''' <param name="tags">lista con los tag a modificar</param>
        ''' <param name="path">ruta del archivo a visualizar</param>
        ''' <history>
        ''' 	[osanchez]	07/04/2009	Created    
        ''' </history>
        Public Shared Sub actualizarHtml(ByVal tags As List(Of DtoTag), ByVal path As String)
            Dim html As String

            Using reader As New StreamReader(path)
                html = reader.ReadToEnd()
                reader.Close()
            End Using
            'Reemplaza el html original
            For Each dto As DtoTag In tags
                html = html.Replace(dto.oldTag, dto.newTag)
                Trace.WriteLineIf(ZTrace.IsVerbose, "Reemplazando " & dto.oldTag & " por " & dto.newTag)
            Next

            Try
                'Escribe el archivo en disco
                Using wr As New StreamWriter(path, False)
                    wr.Write(html)
                    wr.Flush()
                    wr.Close()
                End Using
            Catch ex As Exception
                If TypeOf (ex) Is System.UnauthorizedAccessException Then
                    Dim fl As FileInfo
                    Try
                        fl = New FileInfo(path)
                        fl.Attributes = FileAttributes.Normal
                    Finally
                        fl = Nothing
                    End Try
                    Using wr As New StreamWriter(path, False)
                        wr.Write(html)
                        wr.Flush()
                        wr.Close()
                    End Using
                Else
                    raiseerror(ex)
                End If
            End Try
        End Sub

        ''' <summary>
        ''' helper para generar una instancia de la clase 
        ''' DtoTag
        ''' </summary>
        ''' <param name="oldTag">Valor original</param>
        ''' <param name="newTag">Valor modificado</param>
        ''' <returns>Instancia de la clase DtoTag</returns>
        ''' <history>
        ''' 	[osanchez]	07/04/2009	Created    
        ''' </history>
        Public Shared Function instanceDtoTag(ByVal oldTag As String, ByVal newTag As String) As DtoTag
            Dim item As New DtoTag

            item.oldTag = oldTag
            item.newTag = newTag

            Return item
        End Function

        ''' <summary>
        ''' Busca el tag html iframe, Si existe retorna true 
        ''' </summary>
        ''' <param name="item">coincidencia de expresion regular</param>
        ''' <returns>True si existe el tag iframe</returns>
        ''' <history>
        ''' 	[osanchez]	07/04/2009	Created    
        ''' </history>
        Public Shared Function buscarHtmlIframe(ByVal item As Match) As Boolean
            If item.Groups(1).Value.ToLower() = tagHtmlIFRAME Then
                Return True
            Else
                Return False
            End If
        End Function

        ''' <summary>
        ''' Busca el tag html zamba_doc_ 
        ''' Si existe retorna true y el id del documento por 
        ''' medio del parametro id
        ''' </summary>
        ''' <param name="tag">tag html</param>
        ''' <param name="id">atributo id del tag html</param>
        ''' <returns>True si existe el tag Zamba_Doc_Id</returns>
        ''' <history>
        ''' 	[osanchez]	07/04/2009	Created    
        ''' </history>
        Public Shared Function getZamba_Doc_Id(ByVal tag As String, ByRef id As Int64) As Boolean
            Dim isOk As Boolean = False
            Dim myTag As String
            If Not IsNothing(tag) Then
                myTag = tag.ToLower()
                'Evalua el formato 
                If myTag.Contains(tagHtmlZamba_Doc_) Then
                    Dim index As Int32
                    Dim aux As String
                    index = myTag.IndexOf(tagHtmlZamba_Doc_)
                    aux = myTag.Replace(tagHtmlZamba_Doc_, "")
                    If Int64.TryParse(aux.Substring(index, aux.Length - index), id) Then
                        isOk = True
                    End If
                End If
            End If
            Return isOk
        End Function


        Public Shared Function EncodeHtml(ByVal s As String) As String
            Return HttpUtility.HtmlEncode(s)
        End Function

    End Class

    ''' <summary>
    ''' Clase utilizada como transporte. Encapsula una unidad
    ''' logica de una modificacion de un tag html
    ''' </summary>   
    ''' <history>
    ''' 	[osanchez]	07/04/2009	Created    
    ''' </history>
    Public Class DtoTag
        Public oldTag As String
        Public newTag As String
    End Class

End Namespace
