Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Runtime.Serialization
Imports System.IO
Imports Zamba.Tools
Imports Zamba.Core

Public Class ZPrinting
    Private Shared _PrinterSettings As System.Drawing.Printing.PrinterSettings

    Public Shared Property PrintConfig() As System.Drawing.Printing.PrinterSettings
        Get
            If Not _PrinterSettings Is Nothing Then
                Return _PrinterSettings
            End If

            _PrinterSettings = New System.Drawing.Printing.PrinterSettings
            LoadPageSettings(_PrinterSettings.DefaultPageSettings)
            Return _PrinterSettings

        End Get
        Set(ByVal Value As System.Drawing.Printing.PrinterSettings)
            _PrinterSettings = Value
            SavePageSettings(_PrinterSettings.DefaultPageSettings)
        End Set
    End Property

    Public Shared Sub SavePrintConfig(ByVal pc As System.Drawing.Printing.PrinterSettings)
        PrintConfig = pc
    End Sub

    Public Shared Sub FileSerialize(ByVal ps As Object, ByVal filename As String)
        Dim stream As IO.Stream = IO.File.Open(filename, IO.FileMode.Create)
        Dim bf As New BinaryFormatter
        Try
            bf.Serialize(stream, ps)
            stream.Close()
        Catch ex As Exception
            stream.Close()
            IO.File.Delete(filename)
        End Try
    End Sub
    Public Shared Function GetSerializationFromFile(ByVal filename As String) As Object
        Dim obj As Object
        Dim file As IO.Stream = IO.File.Open(filename, FileMode.Open)
        Dim bf As New BinaryFormatter
        obj = bf.Deserialize(file)
        file.Close()
        Return obj
    End Function

    Private Shared Sub SavePageSettings(ByVal ps As System.Drawing.Printing.PageSettings)
        Try
            UserPreferences.setValue("BottomMargin", ps.Margins.Bottom.ToString, Sections.PrintPreferences)
            UserPreferences.setValue("LeftMargin", ps.Margins.Left.ToString, Sections.PrintPreferences)
            UserPreferences.setValue("RightMargin", ps.Margins.Right.ToString, Sections.PrintPreferences)
            UserPreferences.setValue("TopMargin", ps.Margins.Top.ToString, Sections.PrintPreferences)
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex, True)
        End Try
    End Sub

    Private Shared Sub LoadPageSettings(ByVal prsett As System.Drawing.Printing.PageSettings)
        Try
            prsett.Margins.Bottom = UserPreferences.getValue("BottomMargin", Sections.PrintPreferences, "0")
            prsett.Margins.Left = UserPreferences.getValue("LeftMargin", Sections.PrintPreferences, "0")
            prsett.Margins.Right = UserPreferences.getValue("RightMargin", Sections.PrintPreferences, "0")
            prsett.Margins.Top = UserPreferences.getValue("TopMargin", Sections.PrintPreferences, "0")
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex, True)
        End Try
    End Sub

End Class
