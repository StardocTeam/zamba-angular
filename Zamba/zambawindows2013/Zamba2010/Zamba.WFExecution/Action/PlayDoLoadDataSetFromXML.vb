Imports System.IO

Public Class PlayDoLoadDataSetFromXML

    Private _myRule As IDoLoadDataSetFromXML
    Private strStarTag As String
    Private strEndTag As String
    Private strXMLSource As String
    Private nombreVar As String
    Private Ds As DataSet

    Sub New(ByVal rule As IDoLoadDataSetFromXML)
        _myRule = rule
        Ds = New DataSet()
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            If results.Count > 0 Then
                Ds = GetDataSetFromText(_myRule.XMLSource, _myRule.StartTag, _myRule.EndTag, results(0))
            Else
                Ds = GetDataSetFromText(_myRule.XMLSource, _myRule.StartTag, _myRule.EndTag, Nothing)
            End If
            If VariablesInterReglas.ContainsKey(_myRule.DataSetName) = False Then
                VariablesInterReglas.Add(_myRule.DataSetName, Ds, False)
            Else
                VariablesInterReglas.Item(_myRule.DataSetName) = Ds
            End If
        Finally
        End Try
        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    Private Function GetDataSetFromText(ByVal Text As String, ByVal StartTag As String, ByVal EndTag As String, ByVal result As ITaskResult) As DataSet
        Text = WFRuleParent.ReconocerVariables(Text)
        If Not IsNothing(Result) Then
            Text = TextoInteligente.ReconocerCodigo(Text, result)
        End If

        StartTag = WFRuleParent.ReconocerVariables(StartTag)
        If Not IsNothing(result) Then
            StartTag = TextoInteligente.ReconocerCodigo(StartTag, result)
        End If

        EndTag = WFRuleParent.ReconocerVariables(EndTag)

        If Not IsNothing(result) Then
            EndTag = TextoInteligente.ReconocerCodigo(EndTag, result)
        End If

        Text = Text.Trim()
        StartTag = StartTag.Trim()
        EndTag = EndTag.Trim()

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Tag inicio " & StartTag)
        If Text.Contains(StartTag) Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Ingresando a la separacion tag inicio")
            Dim separator() As String = {StartTag}
            Text = Text.Split(separator, StringSplitOptions.RemoveEmptyEntries)(1)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto: " & Text)
        Else
            ZTrace.WriteLineIf(ZTrace.IsInfo, "No se encontro el tag de inicio")
        End If

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Tag fin " & EndTag)
        If Text.Contains(EndTag) Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Ingresando a la separacion tag finalizacion")
            Dim separator2() As String = {EndTag}
            Text = Text.Split(separator2, StringSplitOptions.RemoveEmptyEntries)(0)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Texto: " & Text)
        Else
            ZTrace.WriteLineIf(ZTrace.IsInfo, "No se encontro el tag de finalizacion")
        End If

        'Text = "<?xml version=""1.0"" encoding=""utf-8""?>" + Text

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Creando objeto XML en memoria.")
        Dim encoderText As Text.UTF8Encoding = New Text.UTF8Encoding()
        Dim bytes() As Byte = encoderText.GetBytes(Text)
        Dim ms As New MemoryStream(bytes)
        Dim xmlToRead As Xml.XmlReader = Xml.XmlReader.Create(ms)

        Dim Ds As New DataSet()
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Leyendo XML en memoria para cargar el DataSet.")

        Try
            Ds.ReadXml(xmlToRead)
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al leer XML:" & ex.Message)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Se retornará DataSet vacío.")
            ZClass.raiseerror(ex)
        End Try

        Return Ds
    End Function

End Class