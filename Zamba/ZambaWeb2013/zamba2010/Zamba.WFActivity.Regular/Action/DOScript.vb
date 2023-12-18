Imports Zamba.Data
Imports Zamba.Core
Imports System.Xml.Serialization

<RuleMainCategory("Archivos y Aplicaciones"), RuleCategory("Aplicaciones"), RuleSubCategory("Web"), RuleDescription("Ejecutar Script"), RuleHelp("Permite ejecutar un Script"), RuleFeatures(False)> <Serializable()> _
Public Class DOScript
    Inherits WFRuleParent
    Implements IDOSCRIPT, IRuleValidate
    Private playRule As Zamba.WFExecution.PlayDOScript
    Public Overrides Sub Dispose()

    End Sub
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _isValid As Boolean
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

    Public Property IsValid As Boolean Implements IRuleValidate.isValid
        Get
            Return _isValid
        End Get
        Set(ByVal value As Boolean)
            _isValid = value
        End Set
    End Property

    Public Overrides ReadOnly Property MaskName As String
        Get
              Return String.Empty
        End Get
    End Property

    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub


    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64)
        MyBase.New(Id, Name, wfstepid)

        Me.playRule = New WFExecution.PlayDOScript(Me)
    End Sub

    'Public Overloads Overrides Function Play(ByVal Results As SortedList) As SortedList
    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)

        Return playRule.Play(results)
    End Function

    Public Overloads Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
    End Function
End Class

'    Public Overrides Function PrivateCheck() As Boolean
'        Try
'            Dim i, h As Int32
'            Dim sw As New IO.StreamWriter("C:\tmp.txt")
'            Dim sr As New IO.StreamReader(file.FullName)
'            Dim linea As String

'            'If Document.DocTypeId = DoctypeId Then
'            Task.Indexs = Zamba.Core.Results_Factory.GetIndexs(Task, True)
'            While sr.Peek <> -1
'                linea = sr.ReadLine.Replace("<<" & Me.ReplaceIndexs(i) & ">>", Task.Indexs(h).data)
'                sw.WriteLine(linea)
'            End While
'            sw.Close()
'            sr.Close()
'            linea = String.Empty

'            Dim fio As New System.IO.FileInfo("c:\tmp.txt")
'            fio.CopyTo(file.FullName, True)
'            fio.Delete()
'            Dim Proceso As New System.Diagnostics.Process
'            Proceso.StartInfo.FileName = file.FullName
'            Proceso.Start()
'            Return True
'        Catch ex As Exception
'           zamba.core.zclass.raiseerror(ex)
'            Return False
'        End Try
'    End Function
'#End Region
'#Region "Propietary"
'    Dim ReplaceIndexs As New ArrayList
'    Dim file As IO.FileInfo
'    Dim docTypeId as Int64
'    Dim DocTypeName As String
'    Dim Atributos As Zamba.Core.Result
'#End Region

'End Class