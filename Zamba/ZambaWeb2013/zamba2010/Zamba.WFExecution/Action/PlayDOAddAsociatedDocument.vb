Imports System

Public Class PlayDOAddAsociatedDocument

    Private _myRule As IDoAddAsociatedDocument

    Sub New(ByVal rule As IDoAddAsociatedDocument)
        Me._myRule = rule
    End Sub


    Public Function Play(ByVal results As List(Of ITaskResult), ByVal rule As IDoAddAsociatedDocument) As List(Of ITaskResult)
        Return PlayWeb(results, _myRule, Nothing)
    End Function

    '04/07/11: Sumada la funcionalidad del la regla DoAddAsociatedDocument
    'Quitado este parámetro: ByVal rule As IDoAddAsociatedDocument, 
    Public Function PlayWeb(ByVal results As List(Of ITaskResult), ByVal rule As IDoAddAsociatedDocument, ByRef params As Hashtable) As List(Of ITaskResult)
        Dim RiB As New RightsBusiness
        Try

            If RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.DocTypes, RightsType.Create, rule.AsociatedDocType.ID) Then
                params.Add("DocID", results(0).ID)
                params.Add("DocTypeId", rule.AsociatedDocType.ID)
                If (rule.ChildRulesIds IsNot Nothing) Then
                    params.Add("NextRuleIds", String.Join(",", rule.ChildRulesIds))
                End If

                '19/07/11: Sumado un parámetro mas para poder obtener los indices del documento que llama la regla
                If Not IsNothing(results(0).DocType) Then
                        params.Add("FillIndxDocTypeID", results(0).DocType.ID)
                    End If

                params.Add("DontOpenTaskIfIsAsociatedToWF", rule.DontOpenTaskIfIsAsociatedToWF)
                params.Add("HaveSpecificAttributes", rule.HaveSpecificAttributes)
                If rule.HaveSpecificAttributes Then
                        'Si hay configuracion especifica, obtenemos el diccionario con los valores
                        params.Add("SpecificAttrubutes", GetHashFromSpecificAttributes(rule.SpecificAttrubutes, results))
                    End If

                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "No se pudo crear un entidad asociado porque no posee los permisos requeridos.")
            End If

        Catch ex As UriFormatException
            ZClass.raiseerror(ex)
        Finally
            RiB = Nothing
        End Try
        Return results
    End Function

    ''' <summary>
    ''' Obtiene la configuracion de los atributos, retornandolos en un diccionario
    ''' </summary>
    ''' <param name="configuration"></param>
    ''' <param name="results"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
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
