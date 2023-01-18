Imports Zamba.Core

<Ipreprocess.PreProcessName("Mover a Carpeta Temporaria"), Ipreprocess.PreProcessHelp("Mueve los archivos especificados a la carpeta temporal")> _
Public Class ippMoveToTemp
    Inherits ZClass
    Implements Ipreprocess
    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Mueve los archivos monitoreados a un directorio temporal. Debe pasarse por parámetro dicho directorio."
    End Function
    Public Overrides Sub Dispose()

    End Sub
    Public Function GetXml(Optional ByVal xml As String = Nothing) As String Implements Ipreprocess.GetXml
        Return ""
    End Function

    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError

    Public Event PreprocessMessage(ByVal msg As String) Implements Ipreprocess.PreprocessMessage
    'Private Sub SetFolder(ByRef fm As ArrayList, ByVal param As ArrayList)
    Private Sub SetFolder(ByVal Files As ArrayList, ByVal param As ArrayList)
        'If param <> Nothing Then
        If Not IsNothing(param) Then
            tempFolder = param(0)
        Else
            'Me.tempFolder = FM.TempPath
            tempFolder = Files(2)
        End If

        If tempFolder.IndexOf("\") <> tempFolder.Length - 1 Then
            tempFolder = tempFolder & "\"
        End If
    End Sub

    Private tempFolder As String
    Private Const MAXIMODEINTENTOS As Int16 = 1000
    Public Function process(ByVal Files As ArrayList, Optional ByVal param As ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As ArrayList Implements Ipreprocess.process
        'If FM.FullFilename <> Nothing Then
        If Files(0) <> Nothing Then
            Dim targetFile As String
            'Me.SetFolder(FM, param)
            SetFolder(Files, param)
            'Dim fi As New IO.FileInfo(FM.FullFilename)
            Dim fi As New IO.FileInfo(Files(0))
            If IO.Directory.Exists(tempFolder) Then
                targetFile = tempFolder & fi.Name

                ' Dim flag As Boolean = True
                Try
                    'IO.File.Move(FM.FullFilename, targetFile)
                    If IO.File.Exists(Files(0)) = True Then
                        IO.File.Move(Files(0), targetFile)
                        'FM.FullFilename = targetFile
                        Files(0) = targetFile
                    Else
                        Files(0) = Nothing
                    End If
                Catch ex As IO.IOException
                    Try
                        'IO.File.Copy(FM.FullFilename, targetFile, True)
                        IO.File.Copy(Files(0), targetFile, True)
                        IO.File.Delete(Files(0))
                        'FM.FullFilename = targetFile
                        Files(0) = targetFile
                    Catch
                        raiseerror(ex)
                    End Try
                Catch ex As Exception
                    raiseerror(ex)
                End Try
            Else
                Files(0) = Nothing
            End If
        End If
        Return Files
    End Function

    Public Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Ipreprocess.processFile
        Return String.Empty
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
            Return "Mover a Temporal"
        End Get
    End Property
End Class
