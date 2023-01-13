Imports ZAMBA.AppBlock
Public Class RptProceso
    Dim Ds As New DsRptProcess
    Dim Ruta As String

#Region "Metodos Publicos"
    Public Function Datos() As DsRptProcess
        Dim Folders As New IO.DirectoryInfo(Me.Path)
        Dim i As Int32
        Dim file As IO.FileInfo
        Dim carpetas() As IO.DirectoryInfo = Folders.GetDirectories
        Dim cantidadCarpetas As Int32 = Folders.GetDirectories().Length
        Try
            For i = 0 To cantidadCarpetas
                file = New IO.FileInfo(carpetas(i).FullName)
                For Each file In carpetas(i).GetFiles("*.txt")
                    If file.Extension.ToUpper = ".TXT" Then
                        GuardarDato(file)
                    End If
                Next
            Next
        Catch ex As Exception
            ZException.Log(ex)
        End Try
        Return Ds
    End Function
#End Region
#Region "Metodos Privados"
    Private Shared Function CountLines(ByVal File As IO.FileInfo) As Int32
        Dim I As Int32 = 0
        Try
            Dim sr As New IO.StreamReader(File.FullName)
            While sr.Peek <> -1
                sr.ReadLine()
                I += 1
            End While
        Catch ex As Exception
            ZException.Log(ex)
        End Try
        Return I
    End Function
    Private Sub GuardarDato(ByVal File As IO.FileInfo)
        Dim Row As DsRptProcess.DsRptProcesRow = Ds.DsRptProces.NewDsRptProcesRow
        Row.Carpeta = File.DirectoryName
        Row.Archivo = File.Name
        Row.Cantidad = CountLines(File)
        Ds.Tables(0).Rows.Add(Row)
        Ds.AcceptChanges()
    End Sub
#End Region
#Region "Propiedades"
    Public Property Path() As String
        Get
            Return Ruta
        End Get
        Set(ByVal Value As String)
            Ruta = Value
        End Set
    End Property
#End Region
#Region "New"
    Public Sub New(ByVal Path As String)
        Me.Path = Path
    End Sub
#End Region
End Class