Imports System
Imports System.Collections.Generic
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports Zamba.AppBlock
Public Class TextoInteligente
    ''' <summary>
    ''' Asigna al indice a la propiedad data y datatemp el valor devuelto por la consulta sql que se ejecuta
    ''' </summary>
    ''' <history>Marcelo Modified 09/06/09</history>
    ''' <param name="StringSelect">consulta sql</param>
    ''' <param name="SmartText">texto inteligente completo</param>
    ''' <param name="Result">resulta actual de la regla o tarea</param>
    ''' <remarks>Sebasti�n</remarks>
    Public Shared Function AsignItemFromSmartText(ByVal SmartText As String, ByRef Result As ITaskResult, ByVal Resultado As String) As Boolean
        If SmartText.Contains("<<") Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Asignando valor a " & SmartText)

            If Resultado.Contains("<<") Then
                Resultado = ReconocerCodigo(Resultado, Result)
            End If

            Dim OnlySpecifiedIndexsids As Generic.List(Of Long) = AsignSearchSmartText(SmartText, Result, Resultado)
            If Not IsNothing(OnlySpecifiedIndexsids) Then
                If OnlySpecifiedIndexsids.Count > 0 Then
                    Dim rstBuss As New Results_Business()
                    rstBuss.SaveModifiedIndexData(Result, True, True, OnlySpecifiedIndexsids, Nothing)
                    rstBuss = Nothing
                End If
            End If
            Return True
        End If
        Return False
    End Function


    ''' <summary>
    ''' Search the value of the Texto Inteligente property
    ''' Se realizo la sobre carga cambiando solo set por get.
    ''' 03/11/2008 se realizo una modificacion para que en caso de ser un indice le pueda asiganar el valor
    ''' de todas formas.[sebastian]
    ''' </summary>
    ''' <param name="value">The property</param>
    ''' <returns></returns>
    ''' <remarks>Sebastian</remarks>
    Public Shared Function AsignSearchSmartText(ByVal codedtext As String, Optional ByRef InitialObject As Object = Nothing, Optional ByVal resultado As String = Nothing) As Generic.List(Of Int64)
        Dim OnlySpecifiedIndexsids As Generic.List(Of Int64) = New Generic.List(Of Int64)
        Dim values() As String = codedtext.Replace(">>.<<", "�").Replace("<<", "").Replace(">>", "").Split("�")
        Dim FinalValue As String = String.Empty

        Try
            Dim allObjects As New AllObjects
            Dim systemProperties As PropertyInfo()

            If IsNothing(InitialObject) Then
                'Busca la clase shared allobject que tiene los objectos que puedo utilizar
                For Each CurrentAssemblyType As Type In Assembly.GetExecutingAssembly().GetTypes()
                    If String.Compare(CurrentAssemblyType.Name, "ALLOBJECTS", True) = 0 Then

                        'pido las propiedades de allobjects
                        systemProperties = CurrentAssemblyType.GetProperties()

                        'por cada propiedad me fijo si coincide con el valor buscado
                        For Each systemProperty As PropertyInfo In systemProperties
                            If String.Compare(systemProperty.Name, values(0), True) = 0 Then
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Propiedad: " & systemProperty.Name)
                                systemProperty.SetValue(allObjects, resultado, Nothing)
                                Exit For
                            End If
                        Next

                        If IsNothing(InitialObject) Then
                            'no encontro la propiedad en AllObjects

                            For Each systemProperty As PropertyInfo In systemProperties
                                Try
                                    If IsNothing(systemProperty.GetValue(InitialObject, Nothing)) = False Then
                                        'Dim str1 As String = systemProperty.GetValue(InitialObject, Nothing)
                                        'str1 = str1.Substring(systemProperty.GetValue(InitialObject, Nothing))
                                        'Dim str2 As String = values(0)
                                        If String.Compare(systemProperty.GetValue(allObjects, Nothing).GetType.ToString.Substring(systemProperty.GetValue(allObjects, Nothing).GetType.ToString.LastIndexOf(".") + 1), values(0), True) = 0 Then
                                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Resultado: " & resultado)
                                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Objeto Inicial: " & InitialObject.ToString())
                                            systemProperty.SetValue(InitialObject, resultado, Nothing)
                                            Exit For
                                        End If
                                    End If
                                Catch ex As Exception
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
                                    Zamba.Core.ZClass.raiseerror(ex)
                                End Try
                            Next
                            systemProperties = Nothing
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

        Try
            Dim levels As Integer = values.Length - 1

            Dim varBoolean As Boolean
            For actualLevel As Integer = 1 To levels
                varBoolean = False

                Dim Parametro As String = Nothing
                Dim Propiedad As String = Nothing
                If values(levels).IndexOf("(") <> -1 Then
                    Parametro = values(levels).Substring(values(levels).IndexOf("(") + 1, values(levels).Length - values(levels).IndexOf("(") - 2)
                    Propiedad = values(levels).Substring(0, values(levels).IndexOf("("))
                Else
                    Propiedad = values(levels)
                End If

                If actualLevel = levels Then
                    For Each objectProperty As PropertyInfo In InitialObject.GetType.GetProperties
                        If String.Compare(objectProperty.Name, Propiedad, True) = 0 Then
                            'Si es un indice entra aca

                            If Not IsNothing(Parametro) Then
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Parametro: " & Parametro)
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor a asignar: " & resultado)
                                If String.Compare("VARIABLES", Propiedad.ToUpper) = 0 Then
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Es Variable")
                                    DirectCast(InitialObject, TaskResult).Variables(Parametro) = resultado
                                Else
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Es Indice")
                                    Dim indice As IIndex = DirectCast(InitialObject, TaskResult).IndexByName(Parametro)
                                    If indice IsNot Nothing Then
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Indice: " & indice.Name)
                                        If String.Compare(indice.Data, resultado) <> 0 Then
                                            DirectCast(InitialObject, TaskResult).IndexByName(Parametro).Data = resultado
                                            DirectCast(InitialObject, TaskResult).IndexByName(Parametro).DataTemp = resultado
                                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Id de Indice:" & indice.ID)
                                            OnlySpecifiedIndexsids.Add(indice.ID)
                                        End If
                                    End If
                                End If
                            Else
                                'Si es solo un propiedad entra aca
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Es Propiedad")
                                Select Case objectProperty.PropertyType.Name.ToLower
                                    Case "int16"
                                        objectProperty.SetValue(InitialObject, Int16.Parse(resultado), Nothing)
                                    Case "int32"
                                        objectProperty.SetValue(InitialObject, Int32.Parse(resultado), Nothing)
                                    Case "int64"
                                        objectProperty.SetValue(InitialObject, Int64.Parse(resultado), Nothing)
                                    Case "date"
                                        objectProperty.SetValue(InitialObject, Date.Parse(resultado), Nothing)
                                    Case "boolean"
                                        objectProperty.SetValue(InitialObject, Boolean.Parse(resultado), Nothing)
                                    Case Else
                                        objectProperty.SetValue(InitialObject, resultado, Nothing)
                                End Select
                            End If
                            varBoolean = True
                            Exit For
                        End If
                    Next
                    If varBoolean = False Then
                        For Each field As FieldInfo In InitialObject.GetType.GetFields
                            If String.Compare(field.Name, Propiedad, True) = 0 Then
                                field.SetValue(InitialObject, resultado)
                                Exit For
                            End If
                        Next
                    End If
                Else
                    For Each objectProperty As PropertyInfo In InitialObject.GetType.GetProperties
                        If String.Compare(objectProperty.Name, Propiedad, True) = 0 Then
                            objectProperty.SetValue(InitialObject, resultado, Nothing)
                            varBoolean = True
                            Exit For
                        End If
                    Next
                    If varBoolean = False Then
                        For Each objectField As FieldInfo In InitialObject.GetType.GetFields
                            If String.Compare(objectField.Name, Propiedad, True) = 0 Then
                                objectField.SetValue(InitialObject, resultado)
                                Exit For
                            End If
                        Next
                    End If
                End If
            Next
            Return OnlySpecifiedIndexsids
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

        Return OnlySpecifiedIndexsids
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Transforms the code from a TextoInteligente control and returns his proper value 
    ''' </summary>
    ''' <param name="Codigo">The text of TextoInteligente control</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' [Federico]	01/06/2006	Created
    ''' [Andres]24/11/2006 Updated
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function ReconocerCodigo(ByVal codedText As String, ByVal TaskResult As ITaskResult, Optional ByVal LogFull As Boolean = True, Optional HtmlDoc As String = Nothing) As String
        Dim DecodedText As New Text.StringBuilder(codedText.Length)
        Dim bolIndex As Boolean = False
        Dim partialText As String = String.Empty
        Dim allObjects As New AllObjects
        AllObjects.Tarea = TaskResult

        codedText = codedText.Replace("<<repetir>>", "�")
        codedText = codedText.Replace("<</repetir>>", "�")
        codedText = codedText.Replace("<<Repetir>>", "�")
        codedText = codedText.Replace("<</Repetir>>", "�")
        codedText = codedText.Replace("<<REPETIR>>", "�")
        codedText = codedText.Replace("<</REPETIR>>", "�")
        Dim separator As Char() = " "
        If codedText = String.Empty Or codedText = " " Then
            Return codedText
        End If
        For Each CurrentWord As String In codedText.Split(" ")

            ' Si el CurrentWord comienza con "<<", pero no termina con ">>"
            ' Ejemplo - "<<tarea>>.<<indice(A"
            Dim count1 As Int32 = CurrentWord.Replace("<<", "�").Split("�").Length
            Dim count2 As Int32 = CurrentWord.Replace(">>", "�").Split("�").Length
            If count1 <> count2 Then
                bolIndex = True
            End If

            If bolIndex = False Then
                If CurrentWord.Contains("<<") = True Then
                    If (LogFull) Then ZTrace.WriteLineIf(ZTrace.IsInfo, "Palabra a decodificar: " & CurrentWord)
                    Dim lista As System.Text.RegularExpressions.MatchCollection
                    lista = Regex.Matches(CurrentWord, "(\<<.+\>>\.\<<.+\>>)+", RegexOptions.ExplicitCapture)

                    If lista.Count > 0 Then
                        If (LogFull) Then ZTrace.WriteLineIf(ZTrace.IsInfo, "Expresion Regular: " & lista(0).Value)
                        DecodedText.Append(CurrentWord.Substring(0, lista(0).Index))
                        Dim values() As String = codedText.Replace(">>.<<", "�").Replace("<<", "").Replace(">>", "").Split("�")

                        If lista(0).Value.Split(".")(0).ToLower() = "<<tarea>>" Then
                            DecodedText.Append(Search(lista(0).Value, TaskResult))
                        Else
                            DecodedText.Append(Search(lista(0).Value, Nothing, HtmlDoc))
                        End If
                        DecodedText.Append(CurrentWord.Substring(lista(0).Index + lista(0).Length, CurrentWord.Length - (lista(0).Index + lista(0).Length)))
                        If (LogFull) Then ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto Decodificado: " & DecodedText.ToString())
                    End If
                    '     Dim lista As MatchCollection = Regex.Matches(Codigo, "(\<.+\>\.\<.+\>)+", RegexOptions.ExplicitCapture)
                    '   DecodedText.Append(Search(CurrentWord))
                Else
                    DecodedText.Append(CurrentWord)
                End If
                DecodedText.Append(" ")
            Else
                ' Concatenaci�n
                partialText &= CurrentWord

                count1 = partialText.Replace("<<", "�").Split("�").Length
                count2 = partialText.Replace(">>", "�").Split("�").Length

                ' Si partialText contiene ">>"
                If count1 = count2 Then
                    ' Si partialText comienza con "<<"
                    If partialText.Contains("<<") = True Then
                        If (LogFull) Then ZTrace.WriteLineIf(ZTrace.IsInfo, "Palabra a decodificar: " & partialText)
                        Dim lista As System.Text.RegularExpressions.MatchCollection
                        lista = Regex.Matches(partialText, "(\<<.+\>>\.\<<.+\>>)+", RegexOptions.ExplicitCapture)

                        If lista.Count > 0 Then
                            If (LogFull) Then ZTrace.WriteLineIf(ZTrace.IsInfo, "Expresion Regular: " & lista(0).Value)
                            DecodedText.Append(partialText.Substring(0, lista(0).Index))
                            If lista(0).Value.Split(".")(0).ToLower() = "<<tarea>>" Then
                                DecodedText.Append(Search(lista(0).Value, TaskResult))
                            Else
                                DecodedText.Append(Search(lista(0).Value))
                            End If
                            DecodedText.Append(partialText.Substring(lista(0).Index + lista(0).Length, partialText.Length - (lista(0).Index + lista(0).Length)))
                            If (LogFull) Then ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto Parcial: " & DecodedText.ToString())
                            partialText = String.Empty
                        End If
                        '     Dim lista As MatchCollection = Regex.Matches(Codigo, "(\<.+\>\.\<.+\>)+", RegexOptions.ExplicitCapture)
                        '   DecodedText.Append(Search(CurrentWord))
                    Else
                        DecodedText.Append(partialText)
                    End If
                    DecodedText.Append(" ")
                    bolIndex = False
                Else
                    ' Concatenarle al partialText un espacio
                    partialText &= " "
                End If
            End If
        Next
        Return DecodedText.ToString().Trim
    End Function


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Transforms the code from a TextoInteligente control and returns his proper value 
    ''' </summary>
    ''' <param name="Codigo">The text of TextoInteligente control</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' [Federico]	01/06/2006	Created
    ''' [Andres]24/11/2006 Updated
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function ReconocerCodigoAsHashTB(ByVal codedText As String, ByVal TaskResult As ITaskResult) As Hashtable
        Dim DecodedText As New Text.StringBuilder(codedText.Length)
        Dim bolIndex As Boolean = False
        Dim partialText As String = String.Empty
        Dim Cambios As New Hashtable()
        Dim allObjects As New AllObjects
        AllObjects.Tarea = TaskResult

        codedText = codedText.Replace("<<repetir>>", "�")
        codedText = codedText.Replace("<</repetir>>", "�")
        codedText = codedText.Replace("<<Repetir>>", "�")
        codedText = codedText.Replace("<</Repetir>>", "�")
        codedText = codedText.Replace("<<REPETIR>>", "�")
        codedText = codedText.Replace("<</REPETIR>>", "�")
        Dim separator As Char() = " "
        For Each CurrentWord As String In codedText.Split(" ")

            ' Si el CurrentWord comienza con "<<", pero no termina con ">>"
            ' Ejemplo - "<<tarea>>.<<indice(A"
            Dim count1 As Int32 = CurrentWord.Replace("<<", "�").Split("�").Length
            Dim count2 As Int32 = CurrentWord.Replace(">>", "�").Split("�").Length
            If count1 <> count2 Then
                bolIndex = True
            End If

            If bolIndex = False Then
                If CurrentWord.Contains("<<") = True Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Palabra a decodificar: " & CurrentWord)
                    Dim lista As System.Text.RegularExpressions.MatchCollection
                    lista = Regex.Matches(CurrentWord, "(\<<.+\>>\.\<<.+\>>)+", RegexOptions.ExplicitCapture)

                    If lista.Count > 0 Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Expresion Regular: " & lista(0).Value)
                        'DecodedText.Append(CurrentWord.Substring(0, lista(0).Index))
                        Dim values() As String = codedText.Replace(">>.<<", "�").Replace("<<", "").Replace(">>", "").Split("�")
                        If lista(0).Value.Split(".")(0).ToLower() = "<<tarea>>" Then
                            'DecodedText.Append(Search(lista(0).Value, TaskResult))
                            If Not Cambios.ContainsKey(lista(0).Value) Then
                                Cambios.Add(lista(0).Value, Search(lista(0).Value, TaskResult))
                            End If
                        Else
                            'DecodedText.Append(Search(lista(0).Value))
                            Cambios.Add(lista(0).Value, Search(lista(0).Value))
                        End If
                        'DecodedText.Append(CurrentWord.Substring(lista(0).Index + lista(0).Length, CurrentWord.Length - (lista(0).Index + lista(0).Length)))
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto Decodificado: " & DecodedText.ToString())
                    End If
                    '     Dim lista As MatchCollection = Regex.Matches(Codigo, "(\<.+\>\.\<.+\>)+", RegexOptions.ExplicitCapture)
                    '   DecodedText.Append(Search(CurrentWord))
                Else
                    'DecodedText.Append(CurrentWord)

                End If
                'DecodedText.Append(" ")
            Else
                ' Concatenaci�n
                partialText &= CurrentWord

                count1 = partialText.Replace("<<", "�").Split("�").Length
                count2 = partialText.Replace(">>", "�").Split("�").Length

                ' Si partialText contiene ">>"
                If count1 = count2 Then
                    ' Si partialText comienza con "<<"
                    If partialText.Contains("<<") = True Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Palabra a decodificar: " & partialText)
                        Dim lista As System.Text.RegularExpressions.MatchCollection
                        lista = Regex.Matches(partialText, "(\<<.+\>>\.\<<.+\>>)+", RegexOptions.ExplicitCapture)

                        If lista.Count > 0 Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Expresion Regular: " & lista(0).Value)
                            DecodedText.Append(partialText.Substring(0, lista(0).Index))
                            If lista(0).Value.Split(".")(0).ToLower() = "<<tarea>>" Then
                                'DecodedText.Append(Search(lista(0).Value, TaskResult))
                                Cambios.Add(lista(0).Value, Search(lista(0).Value, TaskResult))
                            Else
                                'DecodedText.Append(Search(lista(0).Value))
                                Cambios.Add(lista(0).Value, Search(lista(0).Value))
                            End If
                            'DecodedText.Append(partialText.Substring(lista(0).Index + lista(0).Length, partialText.Length - (lista(0).Index + lista(0).Length)))
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto Parcial: " & DecodedText.ToString())
                            partialText = String.Empty
                        End If
                        '     Dim lista As MatchCollection = Regex.Matches(Codigo, "(\<.+\>\.\<.+\>)+", RegexOptions.ExplicitCapture)
                        '   DecodedText.Append(Search(CurrentWord))
                    Else
                        'DecodedText.Append(partialText)
                    End If
                    DecodedText.Append(" ")
                    bolIndex = False
                Else
                    ' Concatenarle al partialText un espacio
                    partialText &= " "
                End If
            End If
        Next
        Return Cambios
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Transforms the code from a TextoInteligente control and returns his proper value 
    ''' </summary>
    ''' <param name="Codigo">The text of TextoInteligente control</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' [Federico]	01/06/2006	Created
    ''' [Andres]24/11/2006 Updated
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function ReconocerCodigoAsObject(ByVal codedText As String, ByVal TaskResult As IResult) As Object
        Dim DecodedText As New Text.StringBuilder(codedText.Length)
        Dim bolIndex As Boolean = False
        Dim partialText As String = String.Empty

        'AllObjects.Tarea = TaskResult

        codedText = codedText.Replace("<<repetir>>", "�")
        codedText = codedText.Replace("<</repetir>>", "�")
        codedText = codedText.Replace("<<Repetir>>", "�")
        codedText = codedText.Replace("<</Repetir>>", "�")
        codedText = codedText.Replace("<<REPETIR>>", "�")
        codedText = codedText.Replace("<</REPETIR>>", "�")
        Dim separator As Char() = " "
        For Each CurrentWord As String In codedText.Split(" ")

            ' Si el CurrentWord comienza con "<<", pero no termina con ">>"
            ' Ejemplo - "<<tarea>>.<<indice(A"
            Dim count1 As Int32 = CurrentWord.Replace("<<", "�").Split("�").Length
            Dim count2 As Int32 = CurrentWord.Replace(">>", "�").Split("�").Length
            If count1 <> count2 Then
                bolIndex = True
            End If

            If bolIndex = False Then
                If CurrentWord.Contains("<<") = True Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Palabra a decodificar: " & CurrentWord)
                    Dim lista As System.Text.RegularExpressions.MatchCollection
                    lista = Regex.Matches(CurrentWord, "(\<<.+\>>\.\<<.+\>>)+", RegexOptions.ExplicitCapture)

                    If lista.Count > 0 Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Expresion Regular: " & lista(0).Value)
                        DecodedText.Append(CurrentWord.Substring(0, lista(0).Index))
                        Dim values() As String = codedText.Replace(">>.<<", "�").Replace("<<", "").Replace(">>", "").Split("�")
                        If lista(0).Value.Split(".")(0).ToLower() = "<<tarea>>" Then
                            Return SearchAsObject(lista(0).Value, TaskResult)
                        Else
                            Return SearchAsObject(lista(0).Value)
                        End If
                        DecodedText.Append(CurrentWord.Substring(lista(0).Index + lista(0).Length, CurrentWord.Length - (lista(0).Index + lista(0).Length)))
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto Decodificado: " & DecodedText.ToString())
                    End If
                Else
                    DecodedText.Append(CurrentWord)
                End If
                DecodedText.Append(" ")
            Else
                ' Concatenaci�n
                partialText &= CurrentWord

                count1 = partialText.Replace("<<", "�").Split("�").Length
                count2 = partialText.Replace(">>", "�").Split("�").Length

                ' Si partialText contiene ">>"
                If count1 = count2 Then
                    ' Si partialText comienza con "<<"
                    If partialText.Contains("<<") = True Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Palabra a decodificar: " & partialText)
                        Dim lista As System.Text.RegularExpressions.MatchCollection
                        lista = Regex.Matches(partialText, "(\<<.+\>>\.\<<.+\>>)+", RegexOptions.ExplicitCapture)

                        If lista.Count > 0 Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Expresion Regular: " & lista(0).Value)
                            DecodedText.Append(partialText.Substring(0, lista(0).Index))
                            If lista(0).Value.Split(".")(0).ToLower() = "<<tarea>>" Then
                                Return SearchAsObject(lista(0).Value, TaskResult)
                            Else
                                Return SearchAsObject(Search(lista(0).Value))
                            End If
                        End If
                    Else
                        DecodedText.Append(partialText)
                    End If
                    DecodedText.Append(" ")
                    bolIndex = False
                Else
                    ' Concatenarle al partialText un espacio
                    partialText &= " "
                End If
            End If
        Next
        Return DecodedText.ToString()
    End Function


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Transforms the code from a TextoInteligente control and returns his proper value 
    ''' </summary>
    ''' <param name="Codigo">The text of TextoInteligente control</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' [Federico]	01/06/2006	Created
    ''' [Andres]24/11/2006 Updated
    ''' [Andres]21/08/2008 Updated
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function ReconocerCodigo(ByVal codedText As String, ByVal TaskResult As ITaskResult, ByVal prevtext As String) As String
        Dim texttemp As String = ReconocerCodigo(codedText, TaskResult)
        If Not String.IsNullOrEmpty(prevtext) AndAlso texttemp.Contains("�") AndAlso prevtext.Contains("�") Then
            Dim tmptxt As String() = texttemp.Split("�")
            Dim tmptxt2 As String() = prevtext.Split("�")
            texttemp = String.Empty

            For index As Integer = 0 To tmptxt.Length - 1
                If index Mod 2 Then
                    texttemp += "�"
                    texttemp += tmptxt2(index)
                    texttemp += ","
                    texttemp += tmptxt(index)
                    texttemp += "�"
                Else
                    texttemp += tmptxt(index)
                End If

                texttemp += " "
            Next
        End If
        Return texttemp
    End Function
    ''' <summary>
    ''' Search the value of the Texto Inteligente property
    ''' </summary>
    ''' <param name="value">The property</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Search(ByVal codedText As String, Optional ByVal InitialObject As Object = Nothing, Optional HtmlDoc As String = Nothing) As String
        Dim values() As String = codedText.Replace(">>.<<", "�").Replace("<<", "").Replace(">>", "").Split("�")
        Dim FinalValue As String = String.Empty

        Try
            Dim allObjects As New AllObjects
            Dim systemProperties As PropertyInfo()
            If IsNothing(InitialObject) Then
                'Busca la clase shared allobject que tiene los objectos que puedo utilizar
                For Each CurrentAssemblyType As Type In Assembly.GetExecutingAssembly().GetTypes()
                    If String.Compare(CurrentAssemblyType.Name, "ALLOBJECTS", True) = 0 Then

                        'pido las propiedades de allobjects
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Pido las propiedades de allobjects")
                        systemProperties = CurrentAssemblyType.GetProperties()

                        'por cada propiedad me fijo si coincide con el valor buscado
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Me fijo si en las propiedades se encuentra el valor buscado")
                        For Each systemProperty As PropertyInfo In systemProperties
                            If String.Compare(systemProperty.Name, values(0), True) = 0 Then
                                'si es coincidente el valor buscado con la propiedad, le pido el valor de la propiedad este puede 
                                'ser un objeto o el valor final, si es un objeto en el proximo bucle mas abajo
                                'buscare del mismo modo las subpropiedades del objeto para ver si coincide
                                'con el valor buscado que venia con .<
                                InitialObject = systemProperty.GetValue(allObjects, Nothing)
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se encontro el valor las propiedades de AllObjects")
                                Exit For
                            End If
                        Next

                        If IsNothing(InitialObject) Then
                            'no encontro la propiedad en AllObjects
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "No se encontro la propiedad en AllObjects")
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Me fijo si en las Subpropiedades se encuentra el valor buscado")
                            For Each systemProperty As PropertyInfo In systemProperties
                                Try
                                    If IsNothing(systemProperty.GetValue(allObjects, Nothing)) = False Then
                                        Dim str1 As String = systemProperty.GetValue(allObjects, Nothing).GetType.ToString '.Substring(systemProperty.GetValue(allObjects, Nothing).GetType.ToString.LastIndexOf(".") + 1)
                                        str1 = str1.Substring(systemProperty.GetValue(allObjects, Nothing).GetType.ToString.LastIndexOf(".") + 1)
                                        Dim str2 As String = values(0)
                                        If String.Compare(systemProperty.GetValue(allObjects, Nothing).GetType.ToString.Substring(systemProperty.GetValue(allObjects, Nothing).GetType.ToString.LastIndexOf(".") + 1), values(0), True) = 0 Then
                                            'If String.Compare(str1, str2, True) Then
                                            InitialObject = systemProperty.GetValue(allObjects, Nothing)
                                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Se encontro el valor las subpropiedades de AllObjects")
                                            Exit For
                                        End If
                                    End If
                                Catch ex As Exception
                                    Zamba.Core.ZClass.raiseerror(ex)
                                End Try
                            Next
                            systemProperties = Nothing
                        Else
                            Exit For
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Throw
        End Try

        If IsNothing(InitialObject) Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "No se encontro el valor, se devuelve un string empty.")
            Return String.Empty
        End If

        Try
            Dim levels As Integer = values.Length - 1

            Dim varBoolean As Boolean
            For actualLevel As Integer = 1 To levels
                varBoolean = False

                Dim Parametros As Object() = Nothing
                Dim Propiedad As String = Nothing
                If values(levels).IndexOf("(") <> -1 Then
                    Parametros = values(levels).Substring(values(levels).IndexOf("(") + 1, values(levels).Length - values(levels).IndexOf("(") - 2).Split(",")
                    For Each par As String In Parametros
                        If par.Contains("zvar") = True Then
                            Dim VarInterReglas As New VariablesInterReglas()
                            par = VarInterReglas.ReconocerVariables(par.Replace("zvar(", "").Replace(")", ""))
                            VarInterReglas = Nothing
                        End If
                    Next

                    Propiedad = values(levels).Substring(0, values(levels).IndexOf("("))
                Else
                    Propiedad = values(levels)
                End If

                If Not IsNothing(Propiedad) Then ZTrace.WriteLineIf(ZTrace.IsInfo, "Propiedad a buscar: " & Propiedad)

                If actualLevel = levels Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Recorro las propiedades de InitialObject asi tomar el valor de la propiedad que busco.")
                    For Each objectProperty As PropertyInfo In InitialObject.GetType.GetProperties
                        If String.Compare(objectProperty.Name, Propiedad, True) = 0 Then
                            'Si es in indice entra aca

                            If Not IsNothing(Parametros) Then
                                'Se agrego este if porque en el caso de la propiedad del texto inteligente para
                                'commmon funtion cuando recibia un id para buscar el nombre del usuario daba error 
                                'porque lo trataba como string y o como int32 [Sebastian 23/12/2008]
                                If Parametros.Length = 1 AndAlso IsNumeric(Parametros(0)) = True Then
                                    Dim intParam As Int64 = 0
                                    If (Int64.TryParse(Parametros(0), intParam) = True) Then
                                        Try
                                            FinalValue = objectProperty.GetValue(InitialObject, New Object() {Int32.Parse(Parametros(0))}).ToString
                                        Catch ex As Exception
                                            FinalValue = objectProperty.GetValue(InitialObject, New Object() {Parametros(0)}).ToString
                                        End Try
                                    Else
                                        FinalValue = objectProperty.GetValue(InitialObject, New Object() {Parametros(0)}).ToString
                                    End If
                                Else
                                    Dim paramscount As Int16 = objectProperty.GetIndexParameters.Length
                                    If paramscount > Parametros.Length Then
                                        Array.Resize(Parametros, Parametros.Length + 1)
                                        Parametros(Parametros.Length - 1) = HtmlDoc
                                    End If
                                    Dim value As Object = objectProperty.GetValue(InitialObject, Parametros)
                                    If Not IsNothing(value) Then
                                        FinalValue = value.ToString
                                        value = Nothing
                                    Else
                                        FinalValue = String.Empty
                                    End If
                                End If
                            Else
                                'Si es solo un propiedad entra aca
                                FinalValue = objectProperty.GetValue(InitialObject, Nothing).ToString
                            End If
                            varBoolean = True
                            If Not IsNothing(FinalValue) Then ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor obtenido: " & FinalValue.ToString)
                            Exit For
                        End If
                    Next
                    If varBoolean = False Then
                        For Each field As FieldInfo In InitialObject.GetType.GetFields
                            If String.Compare(field.Name, Propiedad, True) = 0 Then
                                FinalValue = field.GetValue(InitialObject).ToString.Trim
                                Exit For
                            End If
                        Next
                    End If
                Else
                    For Each objectProperty As PropertyInfo In InitialObject.GetType.GetProperties
                        If String.Compare(objectProperty.Name, Propiedad, True) = 0 Then
                            Dim paramscount As Int16 = objectProperty.GetIndexParameters.Length
                            If paramscount > Parametros.Length Then
                                Array.Resize(Parametros, Parametros.Length + 1)
                                Parametros(Parametros.Length - 1) = HtmlDoc
                                InitialObject = objectProperty.GetValue(InitialObject, Parametros)
                            Else
                                InitialObject = objectProperty.GetValue(InitialObject, Parametros)
                            End If
                            varBoolean = True
                            Exit For
                        End If
                    Next
                    If varBoolean = False Then
                        For Each objectField As FieldInfo In InitialObject.GetType.GetFields
                            If String.Compare(objectField.Name, Propiedad, True) = 0 Then
                                InitialObject = objectField.GetValue(InitialObject)
                                Exit For
                            End If
                        Next
                    End If
                End If
            Next
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Throw
        End Try
        Return FinalValue
    End Function

    ''' <summary>
    ''' Search the value of the Texto Inteligente property
    ''' </summary>
    ''' <param name="value">The property</param>wi
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function SearchAsObject(ByVal codedText As String, Optional ByVal InitialObject As Object = Nothing) As Object
        Dim values() As String = codedText.Replace(">>.<<", "�").Replace("<<", "").Replace(">>", "").Split("�")
        Dim FinalValue As Object = Nothing

        Try
            Dim allObjects As New AllObjects
            Dim systemProperties As PropertyInfo()
            If IsNothing(InitialObject) Then
                'Busca la clase shared allobject que tiene los objectos que puedo utilizar
                For Each CurrentAssemblyType As Type In Assembly.GetExecutingAssembly().GetTypes()
                    If String.Compare(CurrentAssemblyType.Name, "ALLOBJECTS", True) = 0 Then

                        'pido las propiedades de allobjects
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Pido las propiedades de allobjects")
                        systemProperties = CurrentAssemblyType.GetProperties()

                        'por cada propiedad me fijo si coincide con el valor buscado
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Me fijo si en las propiedades se encuentra el valor buscado")
                        For Each systemProperty As PropertyInfo In systemProperties
                            If String.Compare(systemProperty.Name, values(0), True) = 0 Then
                                'si es coincidente el valor buscado con la propiedad, le pido el valor de la propiedad este puede 
                                'ser un objeto o el valor final, si es un objeto en el proximo bucle mas abajo
                                'buscare del mismo modo las subpropiedades del objeto para ver si coincide
                                'con el valor buscado que venia con .<
                                InitialObject = systemProperty.GetValue(allObjects, Nothing)
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se encontro el valor las propiedades de AllObjects")
                                Exit For
                            End If
                        Next

                        If IsNothing(InitialObject) Then
                            'no encontro la propiedad en AllObjects
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "No se encontro la propiedad en AllObjects")
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Me fijo si en las Subpropiedades se encuentra el valor buscado")
                            For Each systemProperty As PropertyInfo In systemProperties
                                Try
                                    If IsNothing(systemProperty.GetValue(allObjects, Nothing)) = False Then
                                        Dim str1 As String = systemProperty.GetValue(allObjects, Nothing).GetType.ToString '.Substring(systemProperty.GetValue(allObjects, Nothing).GetType.ToString.LastIndexOf(".") + 1)
                                        str1 = str1.Substring(systemProperty.GetValue(allObjects, Nothing).GetType.ToString.LastIndexOf(".") + 1)
                                        Dim str2 As String = values(0)
                                        If String.Compare(systemProperty.GetValue(allObjects, Nothing).GetType.ToString.Substring(systemProperty.GetValue(allObjects, Nothing).GetType.ToString.LastIndexOf(".") + 1), values(0), True) = 0 Then
                                            'If String.Compare(str1, str2, True) Then
                                            InitialObject = systemProperty.GetValue(allObjects, Nothing)
                                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Se encontro el valor las subpropiedades de AllObjects")
                                            Exit For
                                        End If
                                    End If
                                Catch ex As Exception
                                    Zamba.Core.ZClass.raiseerror(ex)
                                End Try
                            Next
                            systemProperties = Nothing
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Throw
        End Try

        If IsNothing(InitialObject) Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "No se encontro el valor, se devuelve un valor nulo.")
            Return Nothing
        End If

        Try
            Dim levels As Integer = values.Length - 1

            Dim varBoolean As Boolean
            For actualLevel As Integer = 1 To levels
                varBoolean = False

                Dim Parametro As String = Nothing
                Dim Propiedad As String = Nothing
                If values(levels).IndexOf("(") <> -1 Then
                    Parametro = values(levels).Substring(values(levels).IndexOf("(") + 1, values(levels).Length - values(levels).IndexOf("(") - 2)
                    Propiedad = values(levels).Substring(0, values(levels).IndexOf("("))
                Else
                    Propiedad = values(levels)
                End If

                If Not IsNothing(Propiedad) Then ZTrace.WriteLineIf(ZTrace.IsInfo, "Propiedad a buscar: " & Propiedad)

                If actualLevel = levels Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Recorro las propiedades de InitialObject asi tomar el valor de la propiedad que busco.")
                    For Each objectProperty As PropertyInfo In InitialObject.GetType.GetProperties
                        If String.Compare(objectProperty.Name, Propiedad, True) = 0 Then
                            'Si es in indice entra aca

                            If Not IsNothing(Parametro) Then
                                'Se agrego este if porque en el caso de la propiedad del texto inteligente para
                                'commmon funtion cuando recibia un id para buscar el nombre del usuario daba error 
                                'porque lo trataba como string y o como int32 [Sebastian 23/12/2008]
                                If IsNumeric(Parametro) = True Then
                                    FinalValue = objectProperty.GetValue(InitialObject, New Object() {Int32.Parse(Parametro)})
                                Else
                                    FinalValue = objectProperty.GetValue(InitialObject, New Object() {Parametro})
                                End If
                            Else
                                'Si es solo un propiedad entra aca
                                FinalValue = objectProperty.GetValue(InitialObject, Nothing)
                            End If
                            varBoolean = True
                            If Not IsNothing(FinalValue) Then ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor obtenido: " & FinalValue.ToString)
                            Exit For
                        End If
                    Next
                    If varBoolean = False Then
                        For Each field As FieldInfo In InitialObject.GetType.GetFields
                            If String.Compare(field.Name, Propiedad, True) = 0 Then
                                FinalValue = field.GetValue(InitialObject).ToString.Trim
                                Exit For
                            End If
                        Next
                    End If
                Else
                    For Each objectProperty As PropertyInfo In InitialObject.GetType.GetProperties
                        If String.Compare(objectProperty.Name, Propiedad, True) = 0 Then
                            InitialObject = objectProperty.GetValue(InitialObject, New Object() {Parametro})
                            varBoolean = True
                            Exit For
                        End If
                    Next
                    If varBoolean = False Then
                        For Each objectField As FieldInfo In InitialObject.GetType.GetFields
                            If String.Compare(objectField.Name, Propiedad, True) = 0 Then
                                InitialObject = objectField.GetValue(InitialObject)
                                Exit For
                            End If
                        Next
                    End If
                End If
            Next
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Throw
        End Try
        Return FinalValue
    End Function

    Public Shared Function EsPublica(ByVal OBJ As Object) As Boolean
        Try
            If String.Compare(OBJ.GetType.ToString, "SYSTEM.REFLECTION.RUNTIMEPROPERTYINFO", True) = 0 Then

                If String.Compare(OBJ.GetCustomAttributes(True)(0).Propiedad.ToString, "PROPIEDADPUBLICA", True) = 0 Then
                    Return True
                Else
                    Return False
                End If
            ElseIf String.Compare(OBJ.GetType.ToString, "SYSTEM.REFLECTION.RUNTIMEFIELDINFO", True) = 0 Then
                If String.Compare(OBJ.GetCustomAttributes(True)(0).Propiedad.ToString, "PROPIEDADPUBLICA", True) = 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                If String.Compare(OBJ.GetCustomAttributes(True)(0).Propiedad.ToString, "PROPIEDADPUBLICA", True) = 0 Then
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString)
            Return (False)
        End Try

    End Function

    Public Shared Sub AddNewItems(ByRef txtTextBoxAEvaluar As TextBox, ByRef lstListaDeSugerencias As ListBox)
        Try
            'primer nivel
            If txtTextBoxAEvaluar.Text.EndsWith("<<") AndAlso txtTextBoxAEvaluar.Text.EndsWith(">>.<<") = False Then
                lstListaDeSugerencias.Items.Clear()
                AgregarAllFromAssembly(lstListaDeSugerencias)
                Exit Sub
            End If

            'Dim TextoAEvaluar As String
            Dim TextoHasta As String = txtTextBoxAEvaluar.Text.Substring(0, txtTextBoxAEvaluar.SelectionStart)
            Dim Desde As Int32 = TextoHasta.LastIndexOf("+")
            If Desde = -1 Then Desde = 0
            Dim TextoDesde As String = TextoHasta.Substring(Desde + 2).Trim

            If String.Compare(TextoDesde, "<<") = 0 Then
                lstListaDeSugerencias.Items.Clear()
                AgregarAllFromAssembly(lstListaDeSugerencias)
                Exit Sub
            End If
            'esto es para cuando es segundo nivel o mas con un objeto y una propiedad
            'Dim Objetos() As String = TextoDesde.Replace(">.<", "�").Replace("<", "�").Replace(">", "�").Replace(".", "").Split("�")
            Dim Objetos As MatchCollection = Regex.Matches(txtTextBoxAEvaluar.Text, "<<[^<<>>]+>>")
            '            Dim Objetos As MatchCollection = Regex.Matches(TextoDesde, "<<[^<<>>]+>>\.<<[^<<>>]+>>")
            If Objetos.Count = 0 Then
                If txtTextBoxAEvaluar.Text.EndsWith(">>.<<") Then
                    Objetos = Regex.Matches(txtTextBoxAEvaluar.Text, "<<[^<<>>]+>>\.<<")
                    '   Objetos = Regex.Matches(Text, "<<[^<<>>]+>>\.<<")
                    If Objetos.Count = 0 Then Exit Sub
                Else
                    Exit Sub
                End If
            End If
            lstListaDeSugerencias.Items.Clear()

            If Objetos.Count = 1 AndAlso String.IsNullOrEmpty(Objetos.Item(0).Value.Trim) Then
                AgregarAllFromAssembly(lstListaDeSugerencias)
                Exit Sub
            End If

            Dim Niveles As Int32 = Objetos.Count + 2
            Dim ZBusinesssAssembly As Assembly = Assembly.LoadWithPartialName("Zamba.Business")

            For Each SySType As Type In ZBusinesssAssembly.GetTypes

                If String.Compare(SySType.Name, "ALLOBJECTS", True) = 0 Then

                    Dim MiObjeto As Object = Nothing

                    Dim VarBoolean As Boolean
                    Dim NivelFinal As Int32 = Niveles - 2
                    For NivelActual As Int32 = 0 To NivelFinal
                        VarBoolean = False

                        Select Case NivelActual
                            Case NivelFinal
                                'ULTIMO NIVEL

                                If IsNothing(MiObjeto) Then
                                    For Each PP As PropertyInfo In SySType.GetProperties
                                        If EsPublica(PP) Then AgregarItem(lstListaDeSugerencias, PP.Name)
                                    Next

                                    For Each FF As FieldInfo In SySType.GetFields
                                        If EsPublica(FF) Then AgregarItem(lstListaDeSugerencias, FF.Name)
                                    Next
                                ElseIf String.Compare(MiObjeto.GetType.ToString.ToUpper, "SYSTEM.REFLECTION.RUNTIMEPROPERTYINFO", True) = 0 Then
                                    For Each PP As PropertyInfo In MiObjeto.PropertyType.GetProperties
                                        If EsPublica(PP) Then AgregarItem(lstListaDeSugerencias, PP.Name)
                                    Next
                                    For Each FF As FieldInfo In MiObjeto.PropertyType.GetFields
                                        If EsPublica(FF) Then AgregarItem(lstListaDeSugerencias, FF.Name)
                                    Next
                                ElseIf String.Compare(MiObjeto.GetType.ToString, "SYSTEM.REFLECTION.RUNTIMEFIELDINFO", True) = 0 Then
                                    For Each PP As PropertyInfo In MiObjeto.FieldType.GetProperties
                                        If EsPublica(PP) Then AgregarItem(lstListaDeSugerencias, PP.Name)
                                    Next
                                    For Each FF As FieldInfo In MiObjeto.FieldType.Getfields
                                        If EsPublica(FF) Then AgregarItem(lstListaDeSugerencias, FF.Name)
                                    Next
                                Else

                                    For Each PP As PropertyInfo In MiObjeto.GetType.GetProperties
                                        If EsPublica(PP) Then AgregarItem(lstListaDeSugerencias, PP.Name)
                                    Next

                                    For Each FF As FieldInfo In MiObjeto.FieldType.GetFields
                                        If EsPublica(FF) Then AgregarItem(lstListaDeSugerencias, FF.Name)
                                    Next


                                End If

                                'ULTIMO NIVEL

                            Case 0

                                'PRIMER NIVEL: ALLOBJECTS

                                If IsNothing(MiObjeto) Then
                                    For Each PP As PropertyInfo In SySType.GetProperties
                                        If String.Compare(PP.Name, Objetos.Item(NivelActual).Value.Replace("<<", "").Replace(">>.<<", "").Replace(">>", "").Replace(".", ""), True) = 0 Then
                                            MiObjeto = PP
                                            VarBoolean = True
                                            Exit For
                                        End If
                                    Next
                                    If VarBoolean = False Then
                                        For Each FF As FieldInfo In SySType.GetFields
                                            If String.Compare(FF.Name, Objetos(NivelActual).Value.Replace("<<", "").Replace(">>.<<", "").Replace(">>", "").Replace(".", ""), True) = 0 Then
                                                MiObjeto = FF
                                                Exit For
                                            End If
                                        Next
                                    End If
                                ElseIf String.Compare(MiObjeto.GetType.ToString, "SYSTEM.REFLECTION.RUNTIMEPROPERTYINFO", True) = 0 Then
                                    For Each PP As PropertyInfo In MiObjeto.PropertyType.GetProperties
                                        If String.Compare(PP.Name, Objetos(NivelActual).Value.Replace("<<", "").Replace(">>.<<", "").Replace(">>", "").Replace(".", ""), True) = 0 Then
                                            MiObjeto = PP
                                            VarBoolean = True
                                            Exit For
                                        End If
                                    Next
                                    If VarBoolean = False Then
                                        For Each FF As FieldInfo In MiObjeto.PropertyType.Getfields
                                            If String.Compare(FF.Name, Objetos(NivelActual).Value.Replace("<<", "").Replace(">>.<<", "").Replace(">>", "").Replace(".", ""), True) = 0 Then
                                                MiObjeto = FF
                                                Exit For
                                            End If
                                        Next
                                    End If
                                ElseIf String.Compare(MiObjeto.GetType.ToString, "SYSTEM.REFLECTION.RUNTIMEFIELDINFO", True) = 0 Then
                                    For Each PP As PropertyInfo In MiObjeto.FieldType.GetProperties
                                        If String.Compare(PP.Name, Objetos(NivelActual).Value.Replace("<<", "").Replace(">>.<<", "").Replace(">>", "").Replace(".", ""), True) = 0 Then
                                            MiObjeto = PP
                                            VarBoolean = True
                                            Exit For
                                        End If
                                    Next
                                    If VarBoolean = False Then
                                        For Each FF As FieldInfo In MiObjeto.FieldType.Getfields
                                            If String.Compare(FF.Name, Objetos(NivelActual).Value.Replace("<<", "").Replace(">>.<<", "").Replace(">>", "").Replace(".", ""), True) = 0 Then
                                                MiObjeto = FF
                                                Exit For
                                            End If
                                        Next
                                    End If
                                Else


                                    For Each PP As PropertyInfo In MiObjeto.GetType.GetProperties
                                        If String.Compare(PP.Name, Objetos(NivelActual).Value.Replace("<<", "").Replace(">>.<<", "").Replace(">>", "").Replace(".", ""), True) = 0 Then
                                            MiObjeto = PP
                                            VarBoolean = True
                                            Exit For
                                        End If
                                    Next
                                    If VarBoolean = False Then
                                        For Each FF As FieldInfo In MiObjeto.FieldType.GetFields
                                            If String.Compare(FF.Name, Objetos(NivelActual).Value.Replace("<<", "").Replace(">>.<<", "").Replace(">>", "").Replace(".", ""), True) = 0 Then
                                                MiObjeto = FF
                                                Exit For
                                            End If
                                        Next
                                    End If

                                End If

                                'PRIMER NIVEL: ALLOBJECTS


                            Case Else

                                'OTROS NIVELES

                                If IsNothing(MiObjeto) Then
                                    For Each PP As PropertyInfo In SySType.GetProperties
                                        If String.Compare(PP.Name, Objetos(NivelActual).Value.Replace("<<", "").Replace(">>.<<", "").Replace(">>", "").Replace(".", ""), True) = 0 Then
                                            MiObjeto = PP
                                            VarBoolean = True
                                            Exit For
                                        End If
                                    Next
                                    If VarBoolean = False Then
                                        For Each FF As FieldInfo In SySType.GetFields
                                            If String.Compare(FF.Name, Objetos(NivelActual).Value.Replace("<<", "").Replace(">>.<<", "").Replace(">>", "").Replace(".", ""), True) = 0 Then
                                                MiObjeto = FF
                                                Exit For
                                            End If
                                        Next
                                    End If
                                ElseIf String.Compare(MiObjeto.GetType.ToString, "SYSTEM.REFLECTION.RUNTIMEPROPERTYINFO", True) = 0 Then
                                    For Each PP As PropertyInfo In MiObjeto.PropertyType.GetProperties
                                        If String.Compare(PP.Name, Objetos(NivelActual).Value.Replace("<<", "").Replace(">>.<<", "").Replace(">>", "").Replace(".", ""), True) = 0 Then
                                            MiObjeto = PP
                                            VarBoolean = True
                                            Exit For
                                        End If
                                    Next
                                    If VarBoolean = False Then
                                        For Each FF As FieldInfo In MiObjeto.PropertyType.Getfields
                                            If String.Compare(FF.Name, Objetos(NivelActual).Value.Replace("<<", "").Replace(">>.<<", "").Replace(">>", "").Replace(".", ""), True) = 0 Then
                                                MiObjeto = FF
                                                Exit For
                                            End If
                                        Next
                                    End If
                                ElseIf String.Compare(MiObjeto.GetType.ToString, "SYSTEM.REFLECTION.RUNTIMEFIELDINFO", True) = 0 Then
                                    For Each PP As PropertyInfo In MiObjeto.FieldType.GetProperties
                                        If String.Compare(PP.Name, Objetos(NivelActual).Value.Replace("<<", "").Replace(">>.<<", "").Replace(">>", "").Replace(".", ""), True) = 0 Then
                                            MiObjeto = PP
                                            VarBoolean = True
                                            Exit For
                                        End If
                                    Next
                                    If VarBoolean = False Then
                                        For Each FF As FieldInfo In MiObjeto.FieldType.Getfields
                                            If String.Compare(FF.Name, Objetos(NivelActual).Value.Replace("<<", "").Replace(">>.<<", "").Replace(">>", "").Replace(".", ""), True) = 0 Then
                                                MiObjeto = FF
                                                Exit For
                                            End If
                                        Next
                                    End If
                                Else

                                    For Each PP As PropertyInfo In MiObjeto.GetType.GetProperties
                                        If String.Compare(PP.Name, Objetos(NivelActual).Value.Replace("<<", "").Replace(">>.<<", "").Replace(">>", "").Replace(".", ""), True) = 0 Then
                                            MiObjeto = PP
                                            VarBoolean = True
                                            Exit For
                                        End If
                                    Next
                                    If VarBoolean = False Then
                                        For Each FF As FieldInfo In MiObjeto.FieldType.GetFields
                                            If String.Compare(FF.Name, Objetos(NivelActual).Value.Replace("<<", "").Replace(">>.<<", "").Replace(">>", "").Replace(".", ""), True) = 0 Then
                                                MiObjeto = FF
                                                Exit For
                                            End If
                                        Next
                                    End If

                                End If

                                'OTROS NIVELES
                        End Select

                    Next

                    Exit For

                End If

            Next

        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString)
        End Try

    End Sub

    Public Shared Sub AgregarItem(ByRef lstListaDeSugerencias As ListBox, ByVal Item As String)
        lstListaDeSugerencias.Items.Add(Item)
    End Sub

    Public Shared Sub AgregarAllFromAssembly(ByRef lstListaDeSugerencias As ListBox)
        Try
            Dim SySAssem As Assembly = Assembly.LoadWithPartialName("Zamba.Bussines")

            For Each SySType As Type In SySAssem.GetTypes
                If String.Compare(SySType.Name, "ALLOBJECTS", True) = 0 Then
                    For Each P As PropertyInfo In SySType.GetProperties
                        AgregarItem(lstListaDeSugerencias, P.Name)
                    Next
                    Exit For
                End If
            Next
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString)
        End Try
    End Sub


    ''' <summary>
    ''' Funcionalidad que devuelve su valor ya sea un Zvar o un Texto Inteligente.
    ''' </summary>
    ''' <param name="index"></param>
    ''' <param name="taskResult"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetValueFromZvarOrSmartText(ByVal inputStr As String, ByVal taskResult As ITaskResult, Optional HtmlDoc As String = Nothing) As String

        'Ej.   {{Zamba.ObtenerLinkWeb("Acceso usuarios internos")}}
        'Ej.   <<FuncionesComunes>>.<<ObtenerLinkWeb(Acceso usuarios internos)>>
        If inputStr.Contains("{{") AndAlso inputStr.Contains("Zamba.") Then
            If inputStr.Contains("Zamba.Tarea") Then
                inputStr = inputStr.Replace("{{Zamba.Tarea", "<<Tarea>>.<<")
                inputStr = inputStr.Replace("}}", ">>")
            End If
            If inputStr.Contains("Zamba.Data") Then
                inputStr = inputStr.Replace("{{Zamba.Data", "<<ZData")
                inputStr = inputStr.Replace("}}", ">>")
            End If
            If inputStr.Contains("Zamba.") Then
                inputStr = inputStr.Replace("{{Zamba.", "<<FuncionesComunes>>.<<")
                inputStr = inputStr.Replace("}}", ">>")
            End If
        End If


        Dim stringToReturn As String = inputStr
        Dim blnVar As Boolean


        If (inputStr.ToLower().Contains("zvar(")) Then
            Dim VarInterReglas As VariablesInterReglas = New VariablesInterReglas()
            stringToReturn = VarInterReglas.ReconocerVariables(inputStr)
            blnVar = True
        End If

        If (inputStr.ToLower().Contains(">>.<<")) AndAlso taskResult IsNot Nothing Then
            stringToReturn = Zamba.Core.TextoInteligente.ReconocerCodigo(stringToReturn, taskResult, True, HtmlDoc).Trim()
        ElseIf (inputStr.ToLower().Contains(">>.<<")) AndAlso IsNothing(taskResult) Then
            If blnVar = False Then
                Return String.Empty
            End If
        End If

        Return stringToReturn
    End Function

End Class