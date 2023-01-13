'listo
Imports Zamba.Servers
Imports Zamba.AppBlock
Public Class PAQ_CreateTable_Schedule
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub

#Region "Propiedades"
    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Crea la tabla que almacena las tareas en el schedule"
        End Get
    End Property



    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "Schedule"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_CreateTable_Schedule
        End Get
    End Property

    'Private Sub insertRep()
    '    Dim strinsert = "insert As String"
    '    Try
    '        strinsert = "insert into REPORTE_ID (ID,DES,AUTOMATICO) values (1,'Exportados',1)"
    '        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    '        strinsert = "insert into REPORTE_ID (ID,DES,AUTOMATICO) values (2,'Exportar',0)"
    '        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    '        strinsert = "insert into REPORTE_ID (ID,DES,AUTOMATICO) values (3,'Publicos A Exportar',0)"
    '        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    '        strinsert = "insert into REPORTE_ID (ID,DES,AUTOMATICO) values (4,'Usuarios Activos',1) "
    '        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    '        strinsert = "insert into REPORTE_ID (ID,DES,AUTOMATICO) values (5,'Permisos',1) "
    '        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    '        strinsert = "insert into REPORTE_ID (ID,DES,AUTOMATICO) values (6,'Procesos',0) "
    '        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    '        strinsert = "insert into REPORTE_ID (ID,DES,AUTOMATICO) values (7,'Documentos',1) "
    '        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    '        strinsert = "insert into REPORTE_ID (ID,DES,AUTOMATICO) values (8,'Inactive Users',1)"
    '        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    '        strinsert = "insert into REPORTE_ID (ID,DES,AUTOMATICO) values (9,'Documentos Mas Consultados',1) "
    '        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    '        strinsert = "insert into REPORTE_ID (ID,DES,AUTOMATICO) values (10,'Documentos Impresos',1) "
    '        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    '        strinsert = "insert into REPORTE_ID (ID,DES,AUTOMATICO) values (11,'Documentos Eliminados',1)"
    '        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    '        strinsert = "insert into REPORTE_ID (ID,DES,AUTOMATICO) values (12,'Documentos Enviados',1)"
    '        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    '        strinsert = "insert into REPORTE_ID (ID,DES,AUTOMATICO) values (13,'Documentos Sin Extension',1)  "
    '        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    '        strinsert = "insert into REPORTE_ID (ID,DES,AUTOMATICO) values (14,'Usuarios Con Zamba',1)"
    '        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    '        strinsert = "insert into REPORTE_ID (ID,DES,AUTOMATICO) values (15,'Documentos Con Indices',1) "
    '        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    '        strinsert = "insert into REPORTE_ID (ID,DES,AUTOMATICO) values (16,'Solo Mails',1) "
    '        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    '        strinsert = "insert into REPORTE_ID (ID,DES,AUTOMATICO) values (17,'Documentos Sin Mails',1) "
    '        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    '        strinsert = "insert into REPORTE_ID (ID,DES,AUTOMATICO) values (18,'Mails Publicos Por Usuario',1)"
    '        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    '        strinsert = "insert into REPORTE_ID (ID,DES,AUTOMATICO) values (19,'Historial Usuario',0) "
    '        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    '        strinsert = "insert into REPORTE_ID (ID,DES,AUTOMATICO) values (20,'Historial Documento',0) "
    '        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    '        strinsert = "insert into REPORTE_ID (ID,DES,AUTOMATICO) values (21,'Documentos Por Fechas',0) "
    '        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    '        strinsert = "insert into REPORTE_ID (ID,DES,AUTOMATICO) values (22,'Documentos Por Fechas Sin Mails',0)"
    '        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    '        strinsert = "insert into REPORTE_ID (ID,DES,AUTOMATICO) values (23,'Permisos Por Usuarios',1)"
    '        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    '        strinsert = "insert into REPORTE_ID (ID,DES,AUTOMATICO) values (24,'Documentos Impresos Por Fechas',0)"
    '        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    '        strinsert = "insert into REPORTE_ID (ID,DES,AUTOMATICO) values (25,'Logins Fallidos',1)"
    '        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    '        strinsert = "insert into REPORTE_ID (ID,DES,AUTOMATICO) values (26,'Sacanned Barcode By Date',1)"
    '        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    '        strinsert = "insert into REPORTE_ID (ID,DES,AUTOMATICO) values (27,'Sacanned Barcode By Batche',1) "
    '        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    '        strinsert = "insert into REPORTE_ID (ID,DES,AUTOMATICO) values (28,'All Barcodes',0) "
    '        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    '        strinsert = "insert into REPORTE_ID (ID,DES,AUTOMATICO) values (29,'Usuarios Bloqueados',1)"
    '        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    '        strinsert = "insert into REPORTE_ID (ID,DES,AUTOMATICO) values (30,'Documentos EnVolumenes',1) "
    '        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    '        strinsert = "insert into REPORTE_ID (ID,DES,AUTOMATICO) values (31,'Volumenes',1) "
    '        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    '        strinsert = "insert into REPORTE_ID (ID,DES,AUTOMATICO) values (32,'Historial de Acciones',0)"
    '        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    '        strinsert = "insert into REPORTE_ID (ID,DES,AUTOMATICO) values (33,'Carátulas Ingresadas',0)"
    '        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString)
    '    End Try
    'End Sub

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
    Public Property installed() As Boolean Implements IPAQ.Installed
        Get
            Return _instaled
        End Get
        Set(ByVal value As Boolean)
            _instaled = value
        End Set
    End Property
    Public ReadOnly Property orden() As Int64 Implements IPAQ.Orden
        Get
            Return 7
        End Get
    End Property
#End Region

#Region "Ejecución"

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        Dim sql As String
        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            'sql = "create table Schedule (TASK_ID int NOT NULL,FECHA datetime,FRECUENCIA int,FECHA_FIN datetime,ACTIVO bit,REPORTE int)"
            sql = "create table Schedule (TASK_ID int NOT NULL ,FECHA datetime NOT NULL,FRECUENCIA int NOT NULL,FECHA_FIN datetime NOT NULL,ULTIMA datetime NOT NULL,ACTIVO bit NOT NULL,REPORTE int NOT NULL,FORMATO int NOT NULL,CARPETA varchar(100) NOT NULL,MAIL_TO varchar(255) NOT NULL,MAIL_CC varchar(255) NOT NULL,MAIL_CCO varchar(255) NOT NULL,IMPRIMIR int,MINUTOS int,DESCRIP nvarchar(100),ORIGEN int,REPET int)"
        Else
            'sql = "create table Schedule (TASK_ID number(10) NOT NULL ,FECHA timestamp NOT NULL,FRECUENCIA number(10) NOT NULL,FECHA_FIN timestamp NOT NULL,ULTIMA timestamp NOT NULL,ACTIVO number(1) NOT NULL,REPORTE number(10) NOT NULL,FORMATO number(10) NOT NULL,CARPETA nvarchar2(100) NOT NULL,MAIL_TO nvarchar2(255) NOT NULL,MAIL_CC nvarchar2(255) NOT NULL,MAIL_CCO nvarchar2(255) NOT NULL,IMPRIMIR number(10),MINUTOS number(10),DESCRIP nvarchar2(100),ORIGEN number(10),REPET number (10))"
            sql = "create table Schedule (TASK_ID number(10) NOT NULL ,FECHA timestamp NOT NULL,FRECUENCIA number(10) NOT NULL,FECHA_FIN timestamp NOT NULL,ULTIMA timestamp NOT NULL,ACTIVO number(1) NOT NULL,REPORTE number(10) NOT NULL,FORMATO number(10) NOT NULL,CARPETA nvarchar2(100) NOT NULL,MAIL_TO nvarchar2(255) ,MAIL_CC nvarchar2(255) ,MAIL_CCO nvarchar2(255),IMPRIMIR number(10),MINUTOS number(10),DESCRIP nvarchar2(100),ORIGEN number(10),REPET number (10))"
        End If

        'Try
        If ZPaq.ExisteTabla("Schedule") = True Then
            Throw New Exception(Me.name & ": La tabla Schedule ya existe en la base de datos.")
        End If
        Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        'Catch ex As Exception
        '    If MessageBox.Show("La tabla Schedule ya existe." & vbNewLine & "¿Desea Eliminarla?", "Atención!", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
        '        Server.Con.ExecuteNonQuery(CommandType.Text, "DROP TABLE Schedule")
        '        Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        '    End If
        'End Try
        Return True
    End Function

#End Region

End Class
