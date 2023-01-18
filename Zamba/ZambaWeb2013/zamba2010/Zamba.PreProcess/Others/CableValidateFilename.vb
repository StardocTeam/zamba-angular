Imports ZAMBA.Core
Imports ZAMBA.Servers
<Ipreprocess.PreProcessName("Validar nombre archivos"), Ipreprocess.PreProcessHelp("Este proceso válida el nombre de los archivos que ingresan al sistema. Se Puede Poner por parámetro una ruta a donde poner los archivos rechazados")> _
Public Class ippCableValidateFilename
    Inherits ZClass
    Implements Ipreprocess
    Public Overrides Sub Dispose()
    End Sub

    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Este proceso válida el nombre de los archivos que ingresan al sistema. Se Puede Poner por parámetro una ruta a donde poner los archivos rechazados"
    End Function

    Public Function GetXml(Optional ByVal xml As String = Nothing) As String Implements Ipreprocess.GetXml
        Return Nothing
    End Function

    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError
    Public Event PreprocessMessage(ByVal msg As String) Implements Ipreprocess.PreprocessMessage

    Public Function process(ByVal Files As ArrayList, Optional ByVal param As ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As ArrayList Implements Ipreprocess.process
        Try
            'If Me.ValidateFileName(FM.FullFilename) = False Then
            If ValidateFileName(Files(0)) = False Then
                'moverarechazados(FM.FullFilename, param)
                Dim param1 As String = param(0)
                moverarechazados(Files(0), param1)
                'FM.FullFilename = Nothing
                Files(0) = Nothing
            End If
        Catch
            'FM.FullFilename = Nothing
            Files(0) = Nothing
        End Try
        Return Files
    End Function
    Private Shared Sub moverarechazados(ByVal Filename As String, ByVal param As String)
        Try
            If Not IsNothing(param) AndAlso param <> "" Then

                Dim dir As New IO.DirectoryInfo(param)
                If Not dir.Exists Then
                    dir.Create()
                End If
                Trace.WriteLineIf(ZTrace.IsInfo, "Moviendo a rechazados " & Filename)
                Dim fi As New IO.FileInfo(Filename)
                fi.MoveTo(dir.FullName & "\" & fi.Name)
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Public Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Ipreprocess.processFile
        Return Nothing
    End Function

    Public Sub SetXml(Optional ByVal xml As String = Nothing) Implements Ipreprocess.SetXml

    End Sub


    Private Function ValidateFileName(ByVal name As String) As Boolean
        'Dim fi As New IO.FileInfo(name)
        Dim Filename As String = name.Remove(0, name.LastIndexOf("\") + 1) 'fi.Name
        If Not IsNothing(Filename) AndAlso Filename.Length = 20 AndAlso Filename.IndexOf("_") = -1 AndAlso IsNumeric(Filename.Substring(0, 16)) Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Shared TiposDeParte As New ArrayList

    Private Shared Function ChequearTipoParte(ByRef tipoParte As String) As Boolean
        Try
            If TiposDeParte.Contains(tipoParte.ToUpper) = False Then
                Dim count As Integer = Server.Con.ExecuteScalar(CommandType.Text, "Select count(*) from iLST_i2 where UPPER(ITEM) = " & "'" & tipoParte.ToUpper & "'")
                If Not IsNothing(count) AndAlso count > 0 Then
                    TiposDeParte.Add(tipoParte.ToUpper)
                    Return True
                Else
                    Return False
                End If
            Else
                Return True
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
            Return False
        End Try
    End Function

    Public Shared ReadOnly Property Id() As Integer
        Get
            Return ProcessFactory.GetProcessIDByName(Name())
        End Get
    End Property

    Public Shared ReadOnly Property Name() As String
        Get
            Return "Validar Nombre de Archivo Cable"
        End Get
    End Property
End Class
