Imports System.io
Imports zamba.data
'Imports zamba.DocTypes.Factory
<Ipreprocess.PreProcessName("Inserta Datos de Inbroker en ORD"), Ipreprocess.PreProcessHelp("Trae de sg_operacion_out y sg_operacionconsulta los valores de inbroker que van a ir en la ORD")> _
Public Class ippSetORD_Inbroker
    Implements Ipreprocess

    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Trae de sg_operacion_out y sg_operacionconsulta los valores de inbroker que van a ir en la ORD"
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
                    While line <> ""
                        ZTrace.WriteLineIf(ZTrace.IsInfo, line.Split("|")(3))
                        If IsNumeric(line.Split("|")(3)) Then
                            PostProcess_Factory.SetORD_InbrokerProcess(line.Split("|")(3))
                        End If
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
                While line <> ""
                    ZTrace.WriteLineIf(ZTrace.IsInfo, line.Split("|")(3))
                    If IsNumeric(line.Split("|")(3)) Then
                        PostProcess_Factory.SetORD_InbrokerProcessFile(line.Split("|")(3))
                    End If
                    line = strReader.ReadLine
                End While
            End If
            Return File
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
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
            Return "Inserta Datos de Inbroker en ORD"
        End Get
    End Property
End Class
