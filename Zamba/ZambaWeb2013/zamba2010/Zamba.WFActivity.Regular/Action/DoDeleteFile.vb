Imports Zamba.Core
Imports System.Xml.Serialization

<RuleCategory("Secciones"), RuleDescription("Eliminar Archivo"), RuleHelp("Permite eliminar un archivo especifico"), RuleFeatures(False)> <Serializable()> _
Public Class DoDeleteFile
    Inherits WFRuleParent
    Implements IDoDeleteFile
    Public Overrides Sub Dispose()

    End Sub
    Private Sub New()
        MyBase.New(0, "", 0)
    End Sub
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Public Overrides ReadOnly Property IsFull() As Boolean
        Get
            Return _isFull
        End Get
    End Property
    Public Overrides ReadOnly Property IsLoaded() As Boolean
        Get
            Return _isLoaded
        End Get
    End Property
    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64)
        MyBase.New(Id, Name, wfstepid)
    End Sub

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDoDeleteFile()
        Return playRule.Play(results, Me)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDoDeleteFile()
        Return playRule.Play(results, Me)
    End Function
End Class

'#Region "Override Methods"
'    'Friend Overrides Sub AnalizeParams()
'    '    Try
'    '        'Asigno los parametros propietarios
'    '        If RuleParams.Count > 0 Then OldParamId = RuleParams(0).Param_Id

'    '        Dim i As Int32
'    '        For i = 0 To RuleParams.Count - 1
'    '            If OldParamId <> RuleParams(i).Param_Id AndAlso OldParamId <> BaseParamId Then
'    '                '           Me.AddResultRow(OldParamId, SEPARADOR)
'    '            End If

'    '            Select Case RuleParams(i).Type.ToUpper
'    '                Case "INDEX"
'    '                    Replaceindexs.Add(RuleParams(i).Value)
'    '                Case "DELETEFILE"
'    '                    If RuleParams(i).Value <> "" Then Me.archivo = RuleParams(i).Value
'    '            End Select
'    '            OldParamId = RuleParams(i).Param_Id
'    '        Next
'    '        Me.AddResultRow(OldParamId, "AND")
'    '    Catch ex As Exception
'    '       zamba.core.zclass.raiseerror(ex)
'    '    End Try
'    'End Sub
'    Public Overrides Function PrivateCheck() As Boolean
'        Try
'            Dim i As Int32
'            '    If Document.DocTypeId = DocTypeId Then
'            Task.Indexs = Zamba.Core.Results_Factory.GetIndexs(Task, True)
'            If archivo = "" Then
'                For i = 0 To Me.Replaceindexs.Count - 1
'                    Dim h As Int32
'                    For h = 0 To Task.Indexs.Count - 1
'                        If Me.Replaceindexs(i) = "<<" & Task.Indexs(h).Name & ">>" Then
'                            Dim IndexData As String = Task.Indexs(h).data
'                            archivo &= IndexData 'ReplaceIndexs(i).Replace("<<" & Me.ReplaceIndexs(i) & ">>", Me.Document.Indexs(h).data)
'                        End If
'                    Next
'                Next
'            End If
'            Dim file As New IO.FileInfo(archivo)
'            If file.Exists Then
'                file.Delete()
'                Return True
'            Else
'                Return False
'            End If

'        Catch ex As Exception
'           zamba.core.zclass.raiseerror(ex)
'            Return False
'        End Try
'        '   Else
'        '     Return False
'        '  End If
'    End Function
'#End Region
'#Region "Propietary"
'    Dim Replaceindexs As New ArrayList
'    Dim archivo As String
'#End Region
'End Class
