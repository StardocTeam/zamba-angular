Imports Zamba.Core
<Ipreprocess.PreProcessName("Autocompletar"), Ipreprocess.PreProcessHelp("Realiza un Select con los valores del Where recibidos como parametros separados por comas y un backup del archivo en la carpeta enviada como parámetro, en caso contrario la guarda en la carpeta de backup default")> _
Public Class ippAutocomplete
    Implements Ipreprocess

    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError

    Public Event PreprocessMessage(ByVal msg As String) Implements Ipreprocess.PreprocessMessage

    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Realiza un Select con los valores del Where recibidos como parametros separados por comas y un backup del archivo en la carpeta enviada como parámetro, en caso contrario la guarda en la carpeta de backup default"
    End Function

    Public Function GetXml(Optional ByVal xml As String = Nothing) As String Implements Ipreprocess.GetXml
        Return ""
    End Function

    Public Function process(ByVal Files As ArrayList, Optional ByVal param As ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As ArrayList Implements Ipreprocess.process

    End Function

    Public Shared Function ProcessDocument(ByRef Result As Result, ByVal WhereIndexsname As ArrayList, ByVal UpdateIndexsName As ArrayList, ByVal SqlId As Int32) As Object
        Dim WhereColsData As New ArrayList
        Dim i As Int32
        For i = 0 To WhereIndexsname.Count - 1
            Dim b As Int32
            For b = 0 To Result.Indexs.Count - 1
                If String.Compare(DirectCast(Result.Indexs(b).name, String), DirectCast(WhereIndexsname(i), String)) = 0 Then
                    WhereColsData.Add(Result.Indexs(b).data)
                    Exit For
                End If
            Next
        Next

        Dim ds As DataSet = Zamba.Core.DataBaseAccessBusiness.ExecuteDataset(WhereColsData, SqlId)

        For i = 0 To UpdateIndexsName.Count - 1
            Dim b As Int32
            For b = 0 To Result.Indexs.Count - 1
                If String.Compare(DirectCast(Result.Indexs(b).name, String), DirectCast(UpdateIndexsName(i), String)) = 0 Then
                    Result.Indexs(b).data = ds.Tables(0).Rows(0).Item(i)
                    Exit For
                End If
            Next
        Next
    End Function

    Public Shared Function ProcessScalar(ByVal WhereData As ArrayList, ByVal SqlId As Int32) As Object
        Dim dstemp As DataSet = Zamba.Core.DataBaseAccessBusiness.ExecuteDataset(WhereData, SqlId)
        Return dstemp.Tables(0).Rows(0).Item(0)
    End Function

    Public Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Ipreprocess.processFile
    End Function

    Public Sub SetXml(Optional ByVal xml As String = Nothing) Implements Ipreprocess.SetXml
    End Sub

    Public Shared ReadOnly Property Id() As Integer
        Get
            Return ProcessFactory.GetProcessIDByName(Name())
        End Get
    End Property

    Public Shared ReadOnly Property Name() As String
        Get
            Return "Auto Completar"
        End Get
    End Property
End Class