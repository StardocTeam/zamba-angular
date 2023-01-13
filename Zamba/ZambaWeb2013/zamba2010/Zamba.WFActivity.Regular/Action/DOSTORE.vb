Imports Zamba.Core
Imports System.Xml.Serialization


<RuleCategory("Base de Datos"), RuleDescription("Ejecutar Procedimiento Desde Archivo"), RuleHelp("Ejecuta un procedimiento almacenado"), RuleFeatures(False)> <Serializable()> _
Public Class DOSTORE

    '    Dim ReplaceIndexs As New ArrayList
    '    Dim Partypes As New ArrayList
    '    Dim parNames As New ArrayList
    '    Dim TipoResultado As String
    '    Dim Stored As String


    Inherits WFRuleParent
    Private _isLoaded As Boolean
    Private _isFull As Boolean

    Public Overrides Sub Dispose()

    End Sub
    Private Sub New()
        MyBase.New(0, "", 0)
    End Sub

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64)
        MyBase.New(Id, Name, wfstepid)
    End Sub

    'Public Overloads Overrides Function Play(ByVal Results As SortedList) As SortedList
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Try
            Dim playRule As New Zamba.WFExecution.PlayDOSTORE
            Return playRule.Play(results, Me)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Return results
    End Function

    Public Overrides ReadOnly Property MaskName() As String
        Get
            Return "Valido o Realizo"
        End Get
    End Property

    Public Overrides Sub FullLoad()

    End Sub

    Public Overrides ReadOnly Property IsFull() As Boolean
        Get

        End Get
    End Property

    Public Overrides ReadOnly Property IsLoaded() As Boolean
        Get

        End Get
    End Property

    Public Overrides Sub Load()

    End Sub
End Class

'    Public Overrides Function PrivateCheck() As Boolean
'        Try
'            Dim i As Int32
'            ' If Document.DocTypeId = DocTypeId Then
'            Task.Indexs = Zamba.Core.Results_Factory.GetIndexs(Task, True)
'            For i = 0 To Me.ReplaceIndexs.Count - 1
'                Dim h As Int32
'                For h = 0 To Task.Indexs.Count - 1
'                    If Me.ReplaceIndexs(i) = Task.Indexs(h).Name Then
'                        Dim IndexData As String = Task.Indexs(h).data
'                        ReplaceIndexs(i) = ReplaceIndexs(i).Replace("<<" & Me.ReplaceIndexs(i) & ">>", Task.Indexs(h).data)
'                    End If
'                Next
'            Next

'            If TipoResultado.ToUpper = "CONDATOS" Then
'                Dim ds As DataSet
'                ds = Server.Con.ExecuteDataset(Me.Stored, parNames, Partypes, ReplaceIndexs)
'                Me.HashTable.Add("CONDATOS", ds)
'                Return True
'            Else
'                Server.Con.ExecuteNonQuery(Me.Stored, parNames, Partypes, ReplaceIndexs)
'                Return True
'            End If
'            'Else
'            'Return False
'            ' End If
'        Catch ex As Exception
'           zamba.core.zclass.raiseerror(ex)
'            Return False
'        End Try
'    End Function

'#End Region

'End Class
