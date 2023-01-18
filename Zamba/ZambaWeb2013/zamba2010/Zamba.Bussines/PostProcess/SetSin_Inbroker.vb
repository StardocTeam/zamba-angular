Imports ZAMBA.AppBlock
Imports System.io
Imports zamba.data
Imports Zamba.Servers.Server
Imports Zamba.Core
'Imports zamba.DocTypes.Factory
<Ipreprocess.PreProcessName("Inserta Datos de Inbroker en Doc_I58"), Ipreprocess.PreProcessHelp("Trae de SGSINIESTROS los valores de inbroker que van a ir en la DOC_I58")> _
Public Class ippSetDoc_I58Inbroker
    Implements Ipreprocess

    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Trae de SGSINIESTROS los valores de inbroker que van a ir en la DOC_I58"
    End Function

    Public Function GetXml(Optional ByVal xml As String = Nothing) As String Implements Ipreprocess.GetXml
        Return xml
    End Function

    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError

    Public Event PreprocessMessage(ByVal msg As String) Implements Ipreprocess.PreprocessMessage

    Public Function process(ByVal Files As System.Collections.ArrayList, Optional ByVal param As System.Collections.ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As System.Collections.ArrayList Implements Ipreprocess.process
        Try
            For Each file As String In Files
                Dim strReader As New StreamReader(file)
                If Not IsNothing(strReader) Then
                    Dim line As String = strReader.ReadLine
                    While line <> String.Empty
                        ZTrace.WriteLineIf(ZTrace.IsInfo, line.Split("|")(4))
                        Dim idSiniestro As String = line.Split("|")(4)
                        idSiniestro = idSiniestro.Insert(4, "-")
                        PostProcess_Factory.SetSin_InbrokerProcess(idSiniestro)
                        line = strReader.ReadLine
                    End While
                End If
            Next
            Return Files
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return Files
        End Try
    End Function

    Public Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Ipreprocess.processFile
        Try
            Dim strReader As New StreamReader(File)
            If Not IsNothing(strReader) Then
                Dim line As String = strReader.ReadLine
                While line <> String.Empty
                    Try
                        If IsNumeric(line.Split("|")(4)) Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, line.Split("|")(4))
                            Dim idSiniestro As String = line.Split("|")(4)
                            If idSiniestro.Length >= 7 Then
                                idSiniestro = idSiniestro.Insert(4, "-")
                            End If
                            PostProcess_Factory.SetSin_InbrokerProcessFile(idSiniestro)
                        End If
                    Catch ex As IndexOutOfRangeException
                        ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString)
                    End Try
                    line = strReader.ReadLine
                End While
            End If
            Return File
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return File
        End Try
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
            Return "Inserta Datos de Inbroker en Doc_I58"
        End Get
    End Property
End Class
