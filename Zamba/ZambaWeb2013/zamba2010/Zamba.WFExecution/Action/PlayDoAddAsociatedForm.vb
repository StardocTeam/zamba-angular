Public Class PlayDoAddAsociatedForm
    Private _myRule As IDoAddAsociatedForm

    Sub New(ByVal rule As IDoAddAsociatedForm)
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As List(Of ITaskResult)) As List(Of ITaskResult)
        Return PlayWeb(results, _myRule, Nothing)
    End Function
    ''' <summary>
    ''' Obtiene la configuracion de los atributos, retornandolos en un diccionario
    ''' </summary>
    ''' <param name="configuration"></param>
    ''' <param name="results"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PlayWeb(ByVal results As List(Of ITaskResult), ByVal rule As IDoAddAsociatedForm, ByRef params As Hashtable) As List(Of ITaskResult)
        Try
            params.Add("DocID", results(0).ID)
            params.Add("DocTypeId", results(0).DocTypeId)
            params.Add("FormID", rule.FormID)
            params.Add("ContinueWithCurrentTasks", rule.ContinueWithCurrentTasks)
            params.Add("DontOpenTaskAfterInsert", rule.DontOpenTaskAfterInsert)
            params.Add("FillCommonAttributes", rule.FillCommonAttributes)
            params.Add("HaveSpecificAttributes", rule.HaveSpecificAttributes)
            If rule.HaveSpecificAttributes Then
                'Si hay configuracion especifica, obtenemos el diccionario con los valores
                params.Add("SpecificAttrubutes", GetHashFromSpecificAttributes(rule.SpecificAttrubutes, results))
            End If
        Catch ex As UriFormatException
            ZClass.raiseerror(ex)
        End Try
        Return results
    End Function

    Private Function GetHashFromSpecificAttributes(ByVal configuration As String, ByVal results As List(Of ITaskResult)) As Dictionary(Of Long, String)
        Dim hash As New Dictionary(Of Long, String)
        Dim strIndex As String = configuration.Replace("//", "§")
        Dim value As String
        Dim id As Int64
        Dim strItem As String

        If Not String.IsNullOrEmpty(configuration) Then
            Dim varInterReglas As New VariablesInterReglas()

            'Separa cada item
            While Not String.IsNullOrEmpty(strIndex)
                'Obtengo el item (// separa por items y | separa por valor y no completar)

                strItem = strIndex.Split("§")(0)
                id = Int(strItem.Split("|")(0))
                value = strItem.Substring(strItem.IndexOf("|", StringComparison.Ordinal) + 1)

                'Si no esta configurado para heredarse se agrega al diccionario
                If value.Contains("[no_completar]") Then
                    value = value.Split("|")(0)

                    value = TextoInteligente.ReconocerCodigo(value, results(0))
                    value = varInterReglas.ReconocerVariablesValuesSoloTexto(value)

                    hash.Add(Int64.Parse(id), value)
                End If

                strIndex = strIndex.Remove(0, strIndex.Split("§")(0).Length)
                If strIndex.Length > 0 Then
                    strIndex = strIndex.Remove(0, 1)
                End If
            End While
        End If

        Return hash
    End Function
End Class
