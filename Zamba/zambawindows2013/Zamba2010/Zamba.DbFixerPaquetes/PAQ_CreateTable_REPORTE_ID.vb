'listo
Imports Zamba.Servers
Imports Zamba.AppBlock
Public Class PAQ_CreateTable_REPORTE_ID
    Inherits ZPaq
    Implements IPAQ

    Public Overrides Sub Dispose()

    End Sub

#Region "Propiedades"

    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Crea una tabla que contiene los id de reportes, descripicion y si puede ser automatico"
        End Get
    End Property


    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "PAQ_CreateTableREPORTE_ID"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_CreateTable_REPORTE_ID
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("01/01/2006")
        End Get
    End Property

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
        End Get
    End Property

    Dim _instaled As Boolean = False
    Public Property installed() As Boolean Implements IPAQ.installed
        Get
            Return _instaled
        End Get
        Set(ByVal value As Boolean)
            _instaled = value
        End Set
    End Property
    Public ReadOnly Property orden() As Int64 Implements IPAQ.orden
        Get
            Return 6
        End Get
    End Property
#End Region

#Region "Ejecución"

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute

        Dim sql As String = String.Empty
        Dim flagError As Boolean = False
        Dim exBuilder As New System.Text.StringBuilder()

        'Try
        '    sql = "DROP TABLE REPORTE_ID"
        '    If GenerateScripts = False Then
        '        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
        '    Else
        '        Dim sw As New IO.StreamWriter(application.StartupPath & "\scripts.txt", True)
        '        sw.WriteLine("")
        '        sw.WriteLine(sql.ToString)
        '        sw.WriteLine("")
        '        sw.Close()
        '        sw = Nothing
        '    End If
        'Catch
        'End Try

        Try

            If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
                sql = "create table REPORTE_ID (ID int,DES nvarchar(100),AUTOMATICO int)"
            Else
                sql = "create table REPORTE_ID (ID number(10),DES nvarchar2(100),AUTOMATICO number(10))"
            End If

            If GenerateScripts = False Then
                If ZPaq.ExisteTabla("REPORTE_ID") = True Then
                    Throw New Exception(Me.name & ": La tabla REPORTE_ID ya existe en la base de datos.")
                End If
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If

        Catch ex As Exception
            flagError = True
            exBuilder.AppendLine(sql)
            exBuilder.Append("Error: ")
            exBuilder.AppendLine(ex.ToString())
            exBuilder.AppendLine()
        End Try


        Try
            LoadData(GenerateScripts)
        Catch ex As Exception
            flagError = True
            exBuilder.AppendLine(ex.ToString())
        End Try

        If flagError Then
            Throw New Exception(exBuilder.ToString())
        Else
            Return True
        End If

    End Function

    Private Sub LoadData(ByVal GenerateScripts As Boolean)

        Dim sql As String = ""
        Dim flagError As Boolean = False
        Dim exBuilder As New System.Text.StringBuilder()

        Try
            sql = "Delete from Reporte_id"
            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        Catch
        End Try
        Try
            sql = "insert into Reporte_id values(1,'Exportados',1)"
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            sql = "insert into Reporte_id values(2,'Exportar',0)"
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            sql = "insert into Reporte_id values(3,'Publicos a Exportar',0)"
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            sql = "insert into Reporte_id values(4,'Usuarios Activos',1)"
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            sql = "insert into Reporte_id values(5,'Permisos',1)"
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            sql = "insert into Reporte_id values(6,'Procesos',0)"
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            sql = "insert into Reporte_id values(7,'Documentos',1)"
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            sql = "insert into Reporte_id values(8,'Inactive User',1)"
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            sql = "insert into Reporte_id values(9,'Documentos más consultados',1)"
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            sql = "insert into Reporte_id values(10,'Documentos Impresos',1)"
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            sql = "insert into Reporte_id values(11,'Documentos Eliminados',1)"
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            sql = "insert into Reporte_id values(12,'Documentos Enviados',1)"
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            sql = "insert into Reporte_id values(13,'Documentos sin extensión',1)"
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            sql = "insert into Reporte_id values(14,'Usuarios con Zamba',1)"
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            sql = "insert into Reporte_id values(15,'Documentos con índices',1)"
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            sql = "insert into Reporte_id values(16,'Solo Mails',1)"
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            sql = "insert into Reporte_id values(17,'Documentos sin mails',1)"
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            sql = "insert into Reporte_id values(18,'Mails públicos por usuario',1)"
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            sql = "insert into Reporte_id values(19,'Historial usuario',0)"
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            sql = "insert into Reporte_id values(20,'Historial Documento',0)"
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            sql = "insert into Reporte_id values(21,'Documentos por fechas',0)"
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            sql = "insert into Reporte_id values(22,'Documentos por fechas sin mails',0)"
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            sql = "insert into Reporte_id values(23,'Permisos por usuarios',1)"
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            sql = "insert into Reporte_id values(24,'Documentos impresos por fechas',0)"
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            sql = "insert into Reporte_id values(25,'Logins fallidos',1)"
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            sql = "insert into Reporte_id values(26,'Scanner Barcode by Date',1)"
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            sql = "insert into Reporte_id values(27,'Scanner Barcode by Batche',1)"
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            sql = "insert into Reporte_id values(28,'All Barcodes',0)"
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            sql = "insert into Reporte_id values(29,'Usuarios Bloqueados',1)"
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            sql = "insert into Reporte_id values(30,'Documentos en volúmenes',1)"
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            sql = "insert into Reporte_id values(31,'Volúmenes',1)"
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            sql = "insert into Reporte_id values(32,'Historial de Acción',0)"
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            sql = "insert into Reporte_id values(33,'Carátulas Ingresadas',0)"
            If GenerateScripts = False Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
        Catch ex As Exception
            flagError = True
            exBuilder.AppendLine(sql)
            exBuilder.Append("Error: ")
            exBuilder.AppendLine(ex.ToString())
            exBuilder.AppendLine()
        End Try
    End Sub

#End Region

End Class
