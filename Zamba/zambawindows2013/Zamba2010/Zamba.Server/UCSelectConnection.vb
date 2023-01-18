Imports System.IO
Imports System.Collections.Generic

Public Class UcSelectConnection
    Dim _file As FileInfo


    Public Sub New()
        MyBase.new()
        InitializeComponent()

    End Sub

    Public Event ConnectionChanged(File As String)


    Private Sub UcSelectConnectionLoad(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            _file = New FileInfo(Server.currentfile())

            GetAllFiles(_file.Directory)
            Server.AppConfig.Read(_file.FullName)
            RemoveHandler RadDropDownList1.SelectedIndexChanged, AddressOf RadDropDownList1_SelectedIndexChanged
            RadDropDownList1.SelectedIndex = RadDropDownList1.FindStringExact(_file.Name)
            AddHandler RadDropDownList1.SelectedIndexChanged, AddressOf RadDropDownList1_SelectedIndexChanged

        Catch ex As Exception
            RemoveHandler RadDropDownList1.SelectedIndexChanged, AddressOf RadDropDownList1_SelectedIndexChanged
            AddHandler RadDropDownList1.SelectedIndexChanged, AddressOf RadDropDownList1_SelectedIndexChanged
            Throw New Exception("No se encuentra una configuración correcta para la aplicación")
        End Try


    End Sub

    Public Files As New List(Of FileInfo)

    Private Sub GetAllFiles(ByVal Directory As IO.DirectoryInfo)
        RemoveHandler RadDropDownList1.SelectedIndexChanged, AddressOf RadDropDownList1_SelectedIndexChanged
        Files = Directory.GetFiles("*app*.INI").ToList()
        RadDropDownList1.DisplayMember = "Name"
        RadDropDownList1.ValueMember = "FullName"
        RadDropDownList1.DataSource = files
        If files.Count = 1 Then Enabled = False
        AddHandler RadDropDownList1.SelectedIndexChanged, AddressOf RadDropDownList1_SelectedIndexChanged

    End Sub


    Private Sub RadButton1Click(sender As Object, e As EventArgs)
        Try
            Dim specificfile As FileInfo = RadDropDownList1.SelectedValue
        Catch ex As Exception

        End Try
    End Sub


    Private Sub RadDropDownList1_SelectedIndexChanged(sender As System.Object, e As EventArgs) Handles RadDropDownList1.SelectedIndexChanged
        Server.ServerType = DBTYPES.SinDefinir
        Server._currentfile = RadDropDownList1.SelectedValue.ToString

        Server.AppConfig.Read(Server._currentfile)
        RaiseEvent ConnectionChanged(Server._currentfile)
        SetLastUsedConfigFile(Server._currentfile)
    End Sub



    Private Function SetLastUsedConfigFile(LastUsedFileName As String) As String
        Try
            Dim File As New FileInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Zamba Software\LastUsedConfigFile.ini")
            If (File.Exists) Then
                File.Delete()
            End If
            Dim sr As New StreamWriter(File.FullName)
            sr.Write(LastUsedFileName)
            sr.Flush()
            sr.Close()
            sr.Dispose()
        Catch ex As Exception
        End Try

    End Function

End Class
