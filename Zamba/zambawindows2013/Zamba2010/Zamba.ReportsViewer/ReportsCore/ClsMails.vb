Imports ZAMBA.AppBlock
Imports ZAMBA.Servers
Imports System.Windows.Forms

Public Class RptMails
#Region "Eventos de Error"
    Public Event LogError(ByVal ex As Exception)
    Public Event LogDebug(ByVal clase As String, ByVal linea As Int32, ByVal obs1 As String, ByVal obs2 As String)
#End Region
    Dim Directorio As String
    Dim mailspublicos As Int32
    Dim mailsprivados As Int32
#Region "Propiedades"
    Public Property Countprivados() As Int32
        Get
            Return mailsprivados
        End Get
        Set(ByVal Value As Int32)
            mailsprivados = Value
        End Set
    End Property
    Public Property CountPublic() As Int32
        Get
            Return mailspublicos
        End Get
        Set(ByVal Value As Int32)
            mailspublicos = Value
        End Set
    End Property
#End Region
#Region "NEW"
    Public Sub New(ByVal Carpeta As String)
        If Not Carpeta.EndsWith("\") Then
            Carpeta &= "\"
        End If
        Directorio = Carpeta
    End Sub
    Public Sub New()

    End Sub
#End Region
#Region "Metodos Privados"
    Private Function CountAttach(ByVal File As IO.FileInfo) As Int32
        Dim Cantidad As Int32 = 0
        Dim privado As Boolean = True
        Try
            mailspublicos = 0
            mailsprivados = 0
            Dim sr As New IO.StreamReader(File.FullName)
            Dim I As Int32
            Dim str As String
            Dim Cad As String
            Dim subcampo() As String
            While sr.Peek <> -1
                str = sr.ReadLine
                If str <> "" Then
                    Dim campos() As String = str.Split("|"c)
                    campos = str.Split("|")
                    For I = 11 To campos.Length - 1
                        If campos(I).ToString.Trim <> String.Empty Then
                            privado = False
                            Exit For
                        End If
                    Next
                    If privado Then
                        Me.Countprivados += 1
                    Else
                        Me.CountPublic += 1
                    End If
                    privado = True
                    Cad = Nothing
                    Cad = campos(10).ToString
                    subcampo = Cad.Split(",")
                    Cantidad += subcampo.Length
                End If
            End While
        Catch ex As Exception
            RaiseEvent LogError(ex)
        End Try
        Cantidad = (Cantidad - CountMails(File))
        Return Cantidad
    End Function
    Private Function CountMails(ByVal file As IO.FileInfo) As Int32
        Dim Count As Int32 = 0
        Try
            Dim str As New IO.StreamReader(file.OpenRead)
            While str.Peek <> -1
                Count += 1
                str.ReadLine()

            End While
        Catch ex As Exception
            RaiseEvent LogError(ex)
        End Try
        Return Count
    End Function
#End Region
#Region "Metodos Publicos"
    Public Shared Function CountDocI(ByVal username As String) As Int32
        Dim Count As Int32
        Try
            Dim docname As String = "Mail de " & username
            Dim strdocI As String = "Select Doc_type_ID from Doc_Type where doc_Type_name='" & docname & "'"
            Dim DocTypeID As Int32
            DocTypeID = Server.Con.ExecuteScalar(Server.Con(True).ConString, CommandType.Text, strdocI)
            Dim strCount As String = "Select Count(*) from Doc_I" & DocTypeID
            Count = Server.Con(True).ExecuteScalar(Server.Con.ConString, CommandType.Text, strCount)
        Catch ex As Exception
            Count = -1
        End Try
        Return Count
    End Function
    Public Function MailsToImport() As DsMails
        Dim Folders As IO.DirectoryInfo
        Dim I As Int32
        ' Dim Mails As New ArrayList
        Dim FileMaestro As IO.FileInfo
        Dim Ds As New DsMails

        Try
            Folders = New IO.DirectoryInfo(Directorio)
            Dim dirs As IO.DirectoryInfo() = Folders.GetDirectories()
            For I = 0 To Folders.GetDirectories().Length - 1
                Try

                    FileMaestro = New IO.FileInfo(Directorio & dirs(I).ToString & "\Maestro.txt")
                Catch
                End Try

                If (FileMaestro.Exists) AndAlso (FileMaestro.Length) > 0 Then
                    Dim Row As DsMails.DsMailsRow = Ds.DsMails.NewDsMailsRow
                    Me.mailsprivados = 0
                    Me.mailspublicos = 0
                    Row.Usuario = dirs(I).ToString
                    Row.Cantidad = CountMails(FileMaestro)
                    'Row.Adjuntos = CountAttach(Directorio & dirs(I).ToString, FileMaestro)
                    Row.Adjuntos = Me.CountAttach(FileMaestro)
                    Row.Privados = Me.mailsprivados
                    Row.Publicos = Me.mailspublicos
                    Ds.DsMails.Rows.Add(Row)
                    Ds.AcceptChanges()
                End If
            Next
        Catch ex As Exception
            RaiseEvent LogError(ex)
        End Try
        Return Ds
    End Function
    Public Function MailsPublicToImport() As DsMails
        Dim FileMaestro As IO.FileInfo
        Dim Ds As New DsMails
        Try
            FileMaestro = New IO.FileInfo(Directorio & "\MaestroPublic.txt")
            If (FileMaestro.Exists) AndAlso (FileMaestro.Length) > 0 Then
                Dim Row As DsMails.DsMailsRow = Ds.DsMails.NewDsMailsRow
                Row.Fecha = Now.Date.ToString
                Row.Cantidad = CountMails(FileMaestro)
                Row.Adjuntos = Me.CountAttach(FileMaestro)
                Ds.DsMails.Rows.Add(Row)
                Ds.AcceptChanges()
            End If
        Catch ex As Exception
        End Try
        Return Ds
    End Function
    Public Function MailsImported() As DsMails
        Dim Usuario As IO.DirectoryInfo
        Dim I As Int32
        Dim j As Int32
        'Dim Mails As New ArrayList
        Dim FileMaestro As IO.FileInfo
        Dim BackupFolders() As IO.DirectoryInfo
        Dim Ds As New DsMails
        Try
            Usuario = New IO.DirectoryInfo(Directorio)
            Dim dirs As IO.DirectoryInfo() = Usuario.GetDirectories()
            For I = 0 To dirs.Length - 1
                BackupFolders = New IO.DirectoryInfo(Usuario.FullName.ToString & dirs(I).ToString).GetDirectories
                For j = 0 To BackupFolders.Length - 1

                    FileMaestro = New IO.FileInfo(BackupFolders(j).FullName & "\Maestro.txt")


                    If (FileMaestro.Exists) AndAlso (FileMaestro.Length) > 0 Then
                        Dim Row As DsMails.DsMailsImportadosRow = Ds.DsMailsImportados.NewDsMailsImportadosRow()
                        Row.Usuario = dirs(I).ToString
                        Row.Cantidad = CountMails(FileMaestro)
                        Row.Fecha = BackupFolders(j).Name.Substring(6)
                        'Row.Adjuntos = CountAttach(BackupFolders(j).FullName, FileMaestro)
                        Row.Adjuntos = Me.CountAttach(FileMaestro)
                        Row.Publicos = Me.CountPublic
                        Row.Privados = Me.Countprivados
                        Row.DocI = CountDocI(dirs(I).ToString)
                        Ds.DsMailsImportados.Rows.Add(Row)
                        Ds.AcceptChanges()
                    End If
                Next
            Next
        Catch ex As Exception
            RaiseEvent LogError(ex)
        End Try
        Return Ds
    End Function
    Public Shared Function BackupFiles(ByVal path As String) As Boolean
        'Realiza un backup de todos los archivos existentes 
        Dim OK As Boolean = True
        Try
            Dim FoOrigen As New IO.DirectoryInfo(path)
            Dim foDestino As New IO.DirectoryInfo(FoOrigen.ToString & "\Backup " & Now.Day.ToString & "-" & Now.Hour.ToString & "-" & Now.Minute.ToString & "-" & Now.Second.ToString)
            'Dim nuevopath As String = path & "\Backup " & Now.Date.ToString
            If Not foDestino.Exists Then
                foDestino.Create()
            End If
            Dim files() As IO.FileInfo = FoOrigen.GetFiles
            Dim file As IO.FileInfo

            For Each file In files
                file.CopyTo(foDestino.FullName + "\" + file.Name)
            Next

        Catch ex As Exception
            OK = False
        End Try
        Return OK
    End Function
    Public Shared Function MailsImportados() As DataSet
        Dim Datos As New DsMailsCount
        Try
            Dim sql As String = "Select doc_type_id,Doc_type_name from doc_type where doc_type_name like '%Mail%' or doc_type_name like '%mail%' order by Doc_type_name"
            Dim ds As New DataSet
            ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
            Dim i As Int16
            Dim count As Int32
            For i = 0 To ds.Tables(0).Rows.Count - 1
                sql = "Select Count(*) from doc_t" & CInt(ds.Tables(0).Rows(i).Item(0))
                count = Server.Con.ExecuteScalar(CommandType.Text, sql)
                Dim row As DsMailsCount.dsMailsCountRow = Datos.dsMailsCount.NewdsMailsCountRow
                row.Usuario = CType(ds.Tables(0).Rows(i).Item(1), String)
                row.Cantidad = count
                Datos.Tables(0).Rows.Add(row)
                Datos.AcceptChanges()
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return Datos
    End Function
    Public Shared Function MailsPublicosporUsuarios() As DataSet
        Dim ds As New DsMailspublicos
        Dim dstemp As DataSet
        Try
            dstemp = Server.Con.ExecuteDataset(CommandType.Text, "Select I51 as Usuario, Count(*)as Cantidad from doc_I55 group by I51 order by Usuario")
            ds.Tables(0).TableName = dstemp.Tables(0).TableName
            ds.Merge(dstemp)
        Catch ex As Exception
        End Try
        Return ds
    End Function

#End Region

End Class