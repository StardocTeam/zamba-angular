'ver
Imports Zamba.Servers
Public Class PAQ_CreateTables_Exportacion_Mails
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub
    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Crea las tablas necesarias para la exportación y clasificación de mails de Lotus Notes."
        End Get
    End Property

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        Dim sql As String = String.Empty
        Dim flagException As Boolean = False
        Dim exBuilder As New System.Text.StringBuilder()

        If Server.ServerType = Server.DBTYPES.Oracle OrElse Server.ServerType = Server.DBTYPES.Oracle9 OrElse Server.ServerType = Server.DBTYPES.OracleClient Then

            Try
                sql = "CREATE TABLE USRNOTES (ID NUMBER(10), NOMBRE NVARCHAR2(50), CONF_PATHREMOTOARCH NVARCHAR2(50), CONF_MAILSERVER NVARCHAR2(50), CONF_BASEMAIL NVARCHAR2(50), CONF_PATHARCH NVARCHAR2(50), CONF_VISTAEXPORTACION NVARCHAR2(50), CONF_PAPELERA NVARCHAR2(50), CONF_NOMARCHTXT NVARCHAR2(50), CONF_SEQMSG NUMBER(10), CONF_SEQATT NUMBER(10), CONF_LOCKEO NUMBER(10), CONF_ACUMIMG NUMBER(10), CONF_LIMIMG NUMBER(10), CONF_DESTEXT NUMBER(10), CONF_TEXTOSUBJECT NVARCHAR2(50), CONF_BORRAR NVARCHAR2(50), CONF_ARCHCTRL NVARCHAR2(50), CONF_SCHEDULESEL NVARCHAR2(50), CONF_SCHEDULEFIJO NVARCHAR2(50), CONF_SCHEDULEVAR NVARCHAR2(50), CONF_EJECUTABLE NVARCHAR2(50), CONF_NOMUSERNOTES NVARCHAR2(50), CONF_NOMUSERRED NVARCHAR2(50), CONF_CHARSREEMPSUBJ NVARCHAR2(50), LASTRUNTIME DATE, CONF_REINTENTO NUMBER(10), ACTIVO NUMBER(4), CONF_SEQIMG NUMBER(10))"
                If Not ZPaq.ExisteTabla("USRNOTES") Then
                    If Not GenerateScripts Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If
                Else
                    Throw New Exception(Me.name & ": la tabla USRNOTES  ya existe en la base de datos.")
                End If
            Catch EX As Exception
                flagException = True
                exBuilder.AppendLine(sql)
                exBuilder.Append("Error: ")
                exBuilder.Append(EX.ToString())
                exBuilder.AppendLine()
                exBuilder.AppendLine()
            End Try

            Try
                sql = "CREATE TABLE NOTESINDEX_R_DOC_INDEX (INDEXID NUMBER(10) NOT NULL, CARGAR NUMBER(1) NOT NULL)"
                If Not ZPaq.ExisteTabla("NOTESINDEX_R_DOC_INDEX") Then
                    If Not GenerateScripts Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If
                Else
                    Throw New Exception(Me.name & ": la tabla NOTESINDEX_R_DOC_INDEX ya existe en la base de datos.")
                End If
            Catch ex As Exception
                flagException = True
                exBuilder.AppendLine(sql)
                exBuilder.Append("Error: ")
                exBuilder.Append(ex.ToString())
                exBuilder.AppendLine()
                exBuilder.AppendLine()
            End Try

            Try
                sql = "ALTER TABLE INDEX_R_DOC_TYPE ADD (MustComplete NUMBER(1))"
                If GenerateScripts = False Then
                    If ZPaq.ExisteTabla("") Then
                        If Not GenerateScripts Then
                            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                        Else
                            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                            sw.WriteLine("")
                            sw.WriteLine(sql)
                            sw.WriteLine("")
                            sw.Close()
                            sw = Nothing
                        End If
                    Else
                        Throw New Exception(Me.name & ": la tabla INDEX_R_DOC_TYPE no existe en la base de datos.")
                    End If
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(sql.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch EX As Exception
                exBuilder.AppendLine(sql)
                exBuilder.Append("Error: ")
                flagException = True
                exBuilder.Append(EX.ToString())
                exBuilder.AppendLine()
                exBuilder.AppendLine()
            End Try

            Try
                sql = "ALTER TABLE INDEX_R_DOC_TYPE ADD (LoadLotus NUMBER(1))"
                If GenerateScripts = False Then
                    If ZPaq.ExisteTabla("INDEX_R_DOC_TYPE") Then
                        If Not GenerateScripts Then
                            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                        Else
                            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                            sw.WriteLine("")
                            sw.WriteLine(sql)
                            sw.WriteLine("")
                            sw.Close()
                            sw = Nothing
                        End If
                    Else
                        Throw New Exception(Me.name & ": la tabla INDEX_R_DOC_TYPE no existe en la base de datos.")
                    End If
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(sql.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch EX As Exception
                exBuilder.AppendLine(sql)
                exBuilder.Append("Error: ")
                flagException = True
                exBuilder.Append(EX.ToString())
                exBuilder.AppendLine()
                exBuilder.AppendLine()
            End Try

            Try
                sql = "ALTER TABLE INDEX_R_DOC_TYPE ADD (ShowLotus NUMBER(1))"
                If GenerateScripts = False Then
                    If ZPaq.ExisteTabla("") Then
                        If Not GenerateScripts Then
                            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                        Else
                            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                            sw.WriteLine("")
                            sw.WriteLine(sql)
                            sw.WriteLine("")
                            sw.Close()
                            sw = Nothing
                        End If
                    Else
                        Throw New Exception(Me.name & ": la tabla INDEX_R_DOC_TYPE no existe en la base de datos.")
                    End If
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(sql.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch EX As Exception
                exBuilder.AppendLine(sql)
                exBuilder.Append("Error: ")
                flagException = True
                exBuilder.Append(EX.ToString())
                exBuilder.AppendLine()
                exBuilder.AppendLine()
            End Try

            Try
                sql = "UPDATE INDEX_R_DOC_TYPE SET MustComplete=0"
                If GenerateScripts = False Then
                    If ZPaq.ExisteTabla("INDEX_R_DOC_TYPE") Then
                        If Not GenerateScripts Then
                            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                        Else
                            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                            sw.WriteLine("")
                            sw.WriteLine(sql)
                            sw.WriteLine("")
                            sw.Close()
                            sw = Nothing
                        End If
                    Else
                        Throw New Exception(Me.name & ": la tabla INDEX_R_DOC_TYPE no existe en la base de datos.")
                    End If
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(sql.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch EX As Exception
                exBuilder.AppendLine(sql)
                exBuilder.Append("Error: ")
                flagException = True
                exBuilder.Append(EX.ToString())
                exBuilder.AppendLine()
                exBuilder.AppendLine()
            End Try

            Try
                sql = "UPDATE INDEX_R_DOC_TYPE SET LoadLotus=0"
                If GenerateScripts = False Then
                    If ZPaq.ExisteTabla("INDEX_R_DOC_TYPE") Then
                        If Not GenerateScripts Then
                            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                        Else
                            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                            sw.WriteLine("")
                            sw.WriteLine(sql)
                            sw.WriteLine("")
                            sw.Close()
                            sw = Nothing
                        End If
                    Else
                        Throw New Exception(Me.name & ": la tabla INDEX_R_DOC_TYPE no existe en la base de datos.")
                    End If
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(sql.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch EX As Exception
                exBuilder.AppendLine(sql)
                exBuilder.Append("Error: ")
                flagException = True
                exBuilder.Append(EX.ToString())
                exBuilder.AppendLine()
                exBuilder.AppendLine()
            End Try

            Try
                sql = "UPDATE INDEX_R_DOC_TYPE SET ShowLotus=0"
                If GenerateScripts = False Then
                    If ZPaq.ExisteTabla("INDEX_R_DOC_TYPE") Then
                        If Not GenerateScripts Then
                            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                        Else
                            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                            sw.WriteLine("")
                            sw.WriteLine(sql)
                            sw.WriteLine("")
                            sw.Close()
                            sw = Nothing
                        End If
                    Else
                        Throw New Exception(Me.name & ": la tabla INDEX_R_DOC_TYPE no existe en la base de datos.")
                    End If
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(sql.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch EX As Exception
                exBuilder.AppendLine(sql)
                exBuilder.Append("Error: ")
                flagException = True
                exBuilder.Append(EX.ToString())
                exBuilder.AppendLine()
                exBuilder.AppendLine()
            End Try

            Try
                sql = "CREATE TABLE USR_R_FORM (IDFORM NUMBER(10) NOT NULL,IDUSR NUMBER(10) NOT NULL)"
                If GenerateScripts = False Then
                    If Not ZPaq.ExisteTabla("USR_R_FORM") Then
                        If Not GenerateScripts Then
                            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                        Else
                            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                            sw.WriteLine("")
                            sw.WriteLine(sql)
                            sw.WriteLine("")
                            sw.Close()
                            sw = Nothing
                        End If
                    Else
                        Throw New Exception(Me.name & ": la tabla USR_R_FORM ya existe en la base de datos.")
                    End If
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(sql.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                exBuilder.AppendLine(sql)
                exBuilder.Append("Error: ")
                flagException = True
                exBuilder.Append(ex.ToString())
                exBuilder.AppendLine()
            End Try

            Try
                sql = "CREATE TABLE FRMNOTES (ID NUMBER(10) NOT NULL, NOMBRE NVARCHAR2(50) NOT NULL)"
                If GenerateScripts = False Then
                    If Not ZPaq.ExisteTabla("FRMNOTES") Then
                        If Not GenerateScripts Then
                            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                        Else
                            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                            sw.WriteLine("")
                            sw.WriteLine(sql)
                            sw.WriteLine("")
                            sw.Close()
                            sw = Nothing
                        End If
                    Else
                        Throw New Exception(Me.name & ": la tabla FRMNOTES ya existe en la base de datos.")
                    End If
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(sql.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                exBuilder.AppendLine(sql)
                exBuilder.Append("Error: ")
                flagException = True
                exBuilder.Append(ex.ToString())
                exBuilder.AppendLine()
            End Try

            Try
                sql = "CREATE TABLE USERFORM (ID NUMBER, IDFORM NUMBER NOT NULL, NOMBRE VARCHAR2(50))"
                If GenerateScripts = False Then
                    If Not ZPaq.ExisteTabla("USERFORM") Then
                        If Not GenerateScripts Then
                            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                        Else
                            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                            sw.WriteLine("")
                            sw.WriteLine(sql)
                            sw.WriteLine("")
                            sw.Close()
                            sw = Nothing
                        End If
                    Else
                        Throw New Exception(Me.name & ": la tabla USERFORM ya existe en la base de datos.")
                    End If
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(sql.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch EX As Exception
                exBuilder.AppendLine(sql)
                exBuilder.Append("Error: ")
                flagException = True
                exBuilder.Append(EX.ToString())
                exBuilder.AppendLine()
            End Try

            Try
                sql = "CREATE TABLE USERADMIN (ID NUMBER, IDADMIN NUMBER NOT NULL, NOMBRE VARCHAR2(50))"
                If GenerateScripts = False Then
                    If Not ZPaq.ExisteTabla("USERADMIN") Then
                        If Not GenerateScripts Then
                            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                        Else
                            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                            sw.WriteLine("")
                            sw.WriteLine(sql)
                            sw.WriteLine("")
                            sw.Close()
                            sw = Nothing
                        End If
                    Else
                        Throw New Exception(Me.name & ": la tabla USERADMIN ya existe en la base de datos.")
                    End If
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(sql.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch EX As Exception
                exBuilder.AppendLine(sql)
                exBuilder.Append("Error: ")
                flagException = True
                exBuilder.Append(EX.ToString())
                exBuilder.AppendLine()
            End Try

            Try
                sql = "CREATE TABLE USERPARAM (ID NUMBER, IDPARAM NUMBER NOT NULL, NOMBRE VARCHAR2(50))"
                If GenerateScripts = False Then
                    If Not ZPaq.ExisteTabla("USERPARAM") Then
                        If Not GenerateScripts Then
                            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                        Else
                            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                            sw.WriteLine("")
                            sw.WriteLine(sql)
                            sw.WriteLine("")
                            sw.Close()
                            sw = Nothing
                        End If
                    Else
                        Throw New Exception(Me.name & ": la tabla USERPARAM ya existe en la base de datos.")
                    End If
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(sql.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch EX As Exception
                exBuilder.AppendLine(sql)
                exBuilder.Append("Error: ")
                flagException = True
                exBuilder.Append(EX.ToString())
                exBuilder.AppendLine()
            End Try

        Else   'SQL

            Try
                sql = "CREATE TABLE USERPARAM (ID NUMERIC(10, 0) NOT NULL, IDPARAM NUMERIC(10,0) NOT NULL, NOMBRE VARCHAR(100) NOT NULL)"
                If GenerateScripts = False Then
                    If Not ZPaq.ExisteTabla("USERPARAM") Then
                        If Not GenerateScripts Then
                            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                        Else
                            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                            sw.WriteLine("")
                            sw.WriteLine(sql)
                            sw.WriteLine("")
                            sw.Close()
                            sw = Nothing
                        End If
                    Else
                        Throw New Exception(Me.name & ": la tabla USERPARAM ya existe en la base de datos.")
                    End If
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(sql.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch EX As Exception
                exBuilder.AppendLine(sql)
                exBuilder.Append("Error: ")
                flagException = True
                exBuilder.Append(EX.ToString())
                exBuilder.AppendLine()
            End Try

            Try
                sql = "CREATE TABLE USERADMIN (ID numeric (18,0), IDADMIN numeric (18,0), NOMBRE CHAR(50))"
                If GenerateScripts = False Then
                    If Not ZPaq.ExisteTabla("USERADMIN") Then
                        If Not GenerateScripts Then
                            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                        Else
                            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                            sw.WriteLine("")
                            sw.WriteLine(sql)
                            sw.WriteLine("")
                            sw.Close()
                            sw = Nothing
                        End If
                    Else
                        Throw New Exception(Me.name & ": la tabla USERADMIN ya existe en la base de datos.")
                    End If
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(sql.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch EX As Exception
                exBuilder.AppendLine(sql)
                exBuilder.Append("Error: ")
                flagException = True
                exBuilder.Append(EX.ToString())
                exBuilder.AppendLine()
            End Try

            Try
                sql = "CREATE TABLE USERFORM (ID NUMERIC(38), IDFORM NUMERIC(38) NOT NULL, NOMBRE CHAR(50))"
                If GenerateScripts = False Then
                    If Not ZPaq.ExisteTabla("USERFORM") Then
                        If Not GenerateScripts Then
                            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                        Else
                            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                            sw.WriteLine("")
                            sw.WriteLine(sql)
                            sw.WriteLine("")
                            sw.Close()
                            sw = Nothing
                        End If
                    Else
                        Throw New Exception(Me.name & ": la tabla USERFORM ya existe en la base de datos.")
                    End If
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(sql.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch EX As Exception
                exBuilder.AppendLine(sql)
                exBuilder.Append("Error: ")
                flagException = True
                exBuilder.Append(EX.ToString())
                exBuilder.AppendLine()
                exBuilder.AppendLine()
            End Try

            Try
                sql = "CREATE TABLE NOTESINDEX_R_DOC_INDEX (INDEXID INT NOT NULL, CARGAR INT NOT NULL)"
                If GenerateScripts = False Then
                    If Not ZPaq.ExisteTabla("NOTESINDEX_R_DOC_INDEX") Then
                        If Not GenerateScripts Then
                            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                        Else
                            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                            sw.WriteLine("")
                            sw.WriteLine(sql)
                            sw.WriteLine("")
                            sw.Close()
                            sw = Nothing
                        End If
                    Else
                        Throw New Exception(Me.name & ": la tabla NOTESINDEX_R_DOC_INDEX ya existe en la base de datos.")
                    End If
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(sql.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch EX As Exception
                exBuilder.AppendLine(sql)
                exBuilder.Append("Error: ")
                flagException = True
                exBuilder.Append(EX.ToString())
                exBuilder.AppendLine()
                exBuilder.AppendLine()
            End Try

            Try
                sql = "CREATE TABLE USRNOTES (ID NUMERIC(10,0), NOMBRE CHAR(100), CONF_PATHREMOTOARCH CHAR(100), CONF_MAILSERVER CHAR(100), CONF_BASEMAIL CHAR(100), CONF_PATHARCH CHAR(100), CONF_VISTAEXPORTACION CHAR(100), CONF_PAPELERA CHAR(100), CONF_NOMARCHTXT CHAR(100), CONF_SEQMSG NUMERIC(10,0), CONF_SEQATT NUMERIC(10,0), CONF_LOCKEO NUMERIC(10,0), CONF_ACUMIMG NUMERIC(10,0), CONF_LIMIMG NUMERIC(10,0), CONF_DESTEXT NUMERIC(10,0), CONF_TEXTOSUBJECT CHAR(100), CONF_BORRAR CHAR(100), CONF_ARCHCTRL CHAR(255), CONF_SCHEDULESEL CHAR(100), CONF_SCHEDULEFIJO CHAR(100), CONF_SCHEDULEVAR CHAR(100), CONF_EJECUTABLE CHAR(255), CONF_NOMUSERNOTES CHAR(255), CONF_NOMUSERRED CHAR(100), CONF_CHARSREEMPSUBJ CHAR(100), LASTRUNTIME DATETIME, CONF_REINTENTO NUMERIC(10,0), ACTIVO NUMERIC(4,0), CONF_SEQIMG NUMERIC(10,0))"
                If GenerateScripts = False Then
                    If Not ZPaq.ExisteTabla("USRNOTES") Then
                        If Not GenerateScripts Then
                            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                        Else
                            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                            sw.WriteLine("")
                            sw.WriteLine(sql)
                            sw.WriteLine("")
                            sw.Close()
                            sw = Nothing
                        End If
                    Else
                        Throw New Exception(Me.name & ": la tabla USRNOTES ya existe en la base de datos.")
                    End If
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(sql.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch EX As Exception
                exBuilder.AppendLine(sql)
                exBuilder.Append("Error: ")
                flagException = True
                exBuilder.Append(EX.ToString())
                exBuilder.AppendLine()
                exBuilder.AppendLine()
            End Try

            Try
                sql = "CREATE TABLE FRMNOTES (ID INT NOT NULL, NOMBRE CHAR(50) NOT NULL)"
                If GenerateScripts = False Then
                    If Not ZPaq.ExisteTabla("FRMNOTES") Then
                        If Not GenerateScripts Then
                            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                        Else
                            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                            sw.WriteLine("")
                            sw.WriteLine(sql)
                            sw.WriteLine("")
                            sw.Close()
                            sw = Nothing
                        End If
                    Else
                        Throw New Exception(Me.name & ": la tabla FRMNOTES ya existe en la base de datos.")
                    End If
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(sql.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch EX As Exception
                exBuilder.AppendLine(sql)
                exBuilder.Append("Error: ")
                flagException = True
                exBuilder.Append(EX.ToString())
                exBuilder.AppendLine()
                exBuilder.AppendLine()
            End Try

            Try
                sql = "CREATE TABLE USR_R_FORM (IDFORM NUMERIC(10) NOT NULL,IDUSR NUMERIC(10) NOT NULL)"
                If GenerateScripts = False Then
                    If Not ZPaq.ExisteTabla("USR_R_FORM") Then
                        If Not GenerateScripts Then
                            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                        Else
                            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                            sw.WriteLine("")
                            sw.WriteLine(sql)
                            sw.WriteLine("")
                            sw.Close()
                            sw = Nothing
                        End If
                    Else
                        Throw New Exception(Me.name & ": la tabla USR_R_FORM ya existe en la base de datos.")
                    End If
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(sql.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch EX As Exception
                exBuilder.AppendLine(sql)
                exBuilder.Append("Error: ")
                flagException = True
                exBuilder.Append(EX.ToString())
                exBuilder.AppendLine()
                exBuilder.AppendLine()
            End Try

            Try
                sql = "ALTER TABLE INDEX_R_DOC_TYPE ADD MustComplete NUMERIC(4) DEFAULT 0"
                If GenerateScripts = False Then
                    If ZPaq.ExisteTabla("INDEX_R_DOC_TYPE") Then
                        If Not GenerateScripts Then
                            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                        Else
                            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                            sw.WriteLine("")
                            sw.WriteLine(sql)
                            sw.WriteLine("")
                            sw.Close()
                            sw = Nothing
                        End If
                    Else
                        Throw New Exception(Me.name & ": la tabla INDEX_R_DOC_TYPE no existe en la base de datos.")
                    End If
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(sql.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch EX As Exception
                exBuilder.AppendLine(sql)
                exBuilder.Append("Error: ")
                flagException = True
                exBuilder.Append(EX.ToString())
                exBuilder.AppendLine()
                exBuilder.AppendLine()
            End Try

            Try
                sql = "ALTER TABLE INDEX_R_DOC_TYPE ADD LoadLotus NUMERIC(4) DEFAULT 0"
                If GenerateScripts = False Then
                    If ZPaq.ExisteTabla("INDEX_R_DOC_TYPE") Then
                        If Not GenerateScripts Then
                            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                        Else
                            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                            sw.WriteLine("")
                            sw.WriteLine(sql)
                            sw.WriteLine("")
                            sw.Close()
                            sw = Nothing
                        End If
                    Else
                        Throw New Exception(Me.name & ": la tabla INDEX_R_DOC_TYPE no existe en la base de datos.")
                    End If
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(sql.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch EX As Exception
                exBuilder.AppendLine(sql)
                exBuilder.Append("Error: ")
                flagException = True
                exBuilder.Append(EX.ToString())
                exBuilder.AppendLine()
                exBuilder.AppendLine()
            End Try

            Try
                sql = "ALTER TABLE INDEX_R_DOC_TYPE ADD ShowLotus NUMERIC(4) DEFAULT 0"
                If GenerateScripts = False Then
                    If ZPaq.ExisteTabla("INDEX_R_DOC_TYPE") Then
                        If Not GenerateScripts Then
                            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                        Else
                            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                            sw.WriteLine("")
                            sw.WriteLine(sql)
                            sw.WriteLine("")
                            sw.Close()
                            sw = Nothing
                        End If
                    Else
                        Throw New Exception(Me.name & ": la tabla INDEX_R_DOC_TYPE no existe en la base de datos.")
                    End If
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(sql.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch EX As Exception
                exBuilder.AppendLine(sql)
                exBuilder.Append("Error: ")
                flagException = True
                exBuilder.Append(EX.ToString())
                exBuilder.AppendLine()
                exBuilder.AppendLine()
            End Try

            Try
                sql = "UPDATE INDEX_R_DOC_TYPE SET MustComplete=0"
                If GenerateScripts = False Then
                    If ZPaq.ExisteTabla("INDEX_R_DOC_TYPE") Then
                        If Not GenerateScripts Then
                            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                        Else
                            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                            sw.WriteLine("")
                            sw.WriteLine(sql)
                            sw.WriteLine("")
                            sw.Close()
                            sw = Nothing
                        End If
                    Else
                        Throw New Exception(Me.name & ": la tabla INDEX_R_DOC_TYPE no existe en la base de datos.")
                    End If
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(sql.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch EX As Exception
                exBuilder.AppendLine(sql)
                exBuilder.Append("Error: ")
                flagException = True
                exBuilder.Append(EX.ToString())
                exBuilder.AppendLine()
                exBuilder.AppendLine()
            End Try

            Try
                sql = "UPDATE INDEX_R_DOC_TYPE SET LoadLotus=0"
                If GenerateScripts = False Then
                    If ZPaq.ExisteTabla("INDEX_R_DOC_TYPE") Then
                        If Not GenerateScripts Then
                            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                        Else
                            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                            sw.WriteLine("")
                            sw.WriteLine(sql)
                            sw.WriteLine("")
                            sw.Close()
                            sw = Nothing
                        End If
                    Else
                        Throw New Exception(Me.name & ": la tabla INDEX_R_DOC_TYPE no existe en la base de datos.")
                    End If
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(sql.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch EX As Exception
                exBuilder.AppendLine(sql)
                exBuilder.Append("Error: ")
                flagException = True
                exBuilder.Append(EX.ToString())
                exBuilder.AppendLine()
                exBuilder.AppendLine()
            End Try

            Try
                sql = "UPDATE INDEX_R_DOC_TYPE SET ShowLotus=0"
                If GenerateScripts = False Then
                    If ZPaq.ExisteTabla("INDEX_R_DOC_TYPE") Then
                        If Not GenerateScripts Then
                            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
                        Else
                            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                            sw.WriteLine("")
                            sw.WriteLine(sql)
                            sw.WriteLine("")
                            sw.Close()
                            sw = Nothing
                        End If
                    Else
                        Throw New Exception(Me.name & ": la tabla INDEX_R_DOC_TYPE no existe en la base de datos.")
                    End If
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(sql.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch EX As Exception
                exBuilder.AppendLine(sql)
                exBuilder.Append("Error: ")
                flagException = True
                exBuilder.Append(EX.ToString())
                exBuilder.AppendLine()
                exBuilder.AppendLine()
            End Try

            Try
                Actualizar()
            Catch EX As Exception
                exBuilder.AppendLine(sql)
                exBuilder.Append("Error: ")
                flagException = True
                exBuilder.Append(EX.ToString())
                exBuilder.AppendLine()
                exBuilder.AppendLine()
            End Try

            If flagException Then
                Throw New Exception(Me.name + exBuilder.ToString())
            End If
        End If
        Return True
    End Function

    'Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute
    '    Dim sql As String
    '    Dim flagException As Boolean = False
    '    Dim exBuilder As New System.Text.StringBuilder()

    '    If Server.ServerType = Server.DBTYPES.Oracle OrElse Server.ServerType = Server.DBTYPES.Oracle9 OrElse Server.ServerType = Server.DBTYPES.OracleClient Then

    '        Try
    '            sql = "CREATE TABLE USRNOTES (ID NUMBER(10), NOMBRE NVARCHAR2(50), CONF_PATHREMOTOARCH NVARCHAR2(50), CONF_MAILSERVER NVARCHAR2(50), CONF_BASEMAIL NVARCHAR2(50), CONF_PATHARCH NVARCHAR2(50), CONF_VISTAEXPORTACION NVARCHAR2(50), CONF_PAPELERA NVARCHAR2(50), CONF_NOMARCHTXT NVARCHAR2(50), CONF_SEQMSG NUMBER(10), CONF_SEQATT NUMBER(10), CONF_LOCKEO NUMBER(10), CONF_ACUMIMG NUMBER(10), CONF_LIMIMG NUMBER(10), CONF_DESTEXT NUMBER(10), CONF_TEXTOSUBJECT NVARCHAR2(50), CONF_BORRAR NVARCHAR2(50), CONF_ARCHCTRL NVARCHAR2(50), CONF_SCHEDULESEL NVARCHAR2(50), CONF_SCHEDULEFIJO NVARCHAR2(50), CONF_SCHEDULEVAR NVARCHAR2(50), CONF_EJECUTABLE NVARCHAR2(50), CONF_NOMUSERNOTES NVARCHAR2(50), CONF_NOMUSERRED NVARCHAR2(50), CONF_CHARSREEMPSUBJ NVARCHAR2(50), LASTRUNTIME DATE, CONF_REINTENTO NUMBER(10), ACTIVO NUMBER(4), CONF_SEQIMG NUMBER(10))"
    '            If Not ZPaq.ExisteTabla("USRNOTES") Then
    '                Server.Con.ExecuteNonQuery(CommandType.Text, sql)
    '            Else
    '                Throw New Exception(Me.name & ": la tabla USRNOTES  ya existe en la base de datos.")
    '            End If
    '        Catch EX As Exception
    '            flagException = True
    '            exBuilder.Append(EX.ToString())
    '            exBuilder.AppendLine()
    '        End Try

    '        Try
    '            sql = "CREATE TABLE NOTESINDEX_R_DOC_INDEX (INDEXID NUMBER(10) NOT NULL, CARGAR NUMBER(1) NOT NULL)"
    '            If Not ZPaq.ExisteTabla("NOTESINDEX_R_DOC_INDEX") Then
    '                Server.Con.ExecuteNonQuery(CommandType.Text, sql)
    '            Else
    '                Throw New Exception(Me.name & ": la tabla NOTESINDEX_R_DOC_INDEX ya existe en la base de datos.")
    '            End If
    '        Catch ex As Exception
    '            flagException = True
    '            exBuilder.Append(ex.ToString())
    '            exBuilder.AppendLine()
    '        End Try

    '        Try
    '            sql = "ALTER TABLE INDEX_R_DOC_TYPE ADD (MustComplete NUMBER(1))"
    '            If GenerateScripts = False Then
    '                If ZPaq.ExisteTabla("") Then
    '                    Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
    '                Else
    '                    Throw New Exception(Me.name & ": la tabla INDEX_R_DOC_TYPE no existe en la base de datos.")
    '                End If
    '            Else
    '                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
    '                sw.WriteLine("")
    '                sw.WriteLine(sql.ToString)
    '                sw.WriteLine("")
    '                sw.Close()
    '                sw = Nothing
    '            End If
    '            sql = "Update INDEX_R_DOC_TYPE set MustComplete=0"
    '            If GenerateScripts = False Then
    '                If ZPaq.ExisteTabla("INDEX_R_DOC_TYPE") Then
    '                    Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
    '                Else
    '                    Throw New Exception(Me.name & ": la tabla INDEX_R_DOC_TYPE no existe en la base de datos.")
    '                End If
    '            Else
    '                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
    '                sw.WriteLine("")
    '                sw.WriteLine(sql.ToString)
    '                sw.WriteLine("")
    '                sw.Close()
    '                sw = Nothing
    '            End If
    '        Catch EX As Exception
    '            flagException = True
    '            exBuilder.Append(EX.ToString())
    '            exBuilder.AppendLine()
    '        End Try

    '        Try
    '            sql = "CREATE TABLE USR_R_FORM (IDFORM NUMBER(10) NOT NULL,IDUSR NUMBER(10) NOT NULL)"
    '            If GenerateScripts = False Then
    '                If Not ZPaq.ExisteTabla("USR_R_FORM") Then
    '                    Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
    '                Else
    '                    Throw New Exception(Me.name & ": la tabla USR_R_FORM ya existe en la base de datos.")
    '                End If
    '            Else
    '                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
    '                sw.WriteLine("")
    '                sw.WriteLine(sql.ToString)
    '                sw.WriteLine("")
    '                sw.Close()
    '                sw = Nothing
    '            End If
    '        Catch ex As Exception
    '            flagException = True
    '            exBuilder.Append(ex.ToString())
    '            exBuilder.AppendLine()
    '        End Try

    '        Try
    '            sql = "ALTER TABLE INDEX_R_DOC_TYPE ADD (LoadLotus NUMBER(1))"
    '            If GenerateScripts = False Then
    '                If ZPaq.ExisteTabla("INDEX_R_DOC_TYPE") Then
    '                    Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
    '                Else
    '                    Throw New Exception(Me.name & ": la tabla INDEX_R_DOC_TYPE no existe en la base de datos.")
    '                End If
    '            Else
    '                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
    '                sw.WriteLine("")
    '                sw.WriteLine(sql.ToString)
    '                sw.WriteLine("")
    '                sw.Close()
    '                sw = Nothing
    '            End If
    '            sql = "Update INDEX_R_DOC_TYPE set LoadLotus=0"
    '            If GenerateScripts = False Then
    '                If ZPaq.ExisteTabla("") Then
    '                    Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
    '                Else
    '                    Throw New Exception(Me.name & ": la tabla INDEX_R_DOC_TYPE no existe en la base de datos.")
    '                End If
    '            Else
    '                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
    '                sw.WriteLine("")
    '                sw.WriteLine(sql.ToString)
    '                sw.WriteLine("")
    '                sw.Close()
    '                sw = Nothing
    '            End If
    '        Catch EX As Exception
    '            flagException = True
    '            exBuilder.Append(EX.ToString())
    '            exBuilder.AppendLine()
    '        End Try

    '        Try
    '            sql = "CREATE TABLE FRMNOTES (ID NUMBER(10) NOT NULL, NOMBRE NVARCHAR2(50) NOT NULL)"
    '            If GenerateScripts = False Then
    '                If Not ZPaq.ExisteTabla("FRMNOTES") Then
    '                    Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
    '                Else
    '                    Throw New Exception(Me.name & ": la tabla FRMNOTES ya existe en la base de datos.")
    '                End If
    '            Else
    '                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
    '                sw.WriteLine("")
    '                sw.WriteLine(sql.ToString)
    '                sw.WriteLine("")
    '                sw.Close()
    '                sw = Nothing
    '            End If
    '        Catch ex As Exception
    '            flagException = True
    '            exBuilder.Append(ex.ToString())
    '            exBuilder.AppendLine()
    '        End Try

    '        Try
    '            sql = "ALTER TABLE INDEX_R_DOC_TYPE ADD (ShowLotus NUMBER(1))"
    '            If GenerateScripts = False Then
    '                If ZPaq.ExisteTabla("INDEX_R_DOC_TYPE") Then
    '                    Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
    '                Else
    '                    Throw New Exception(Me.name & ": la tabla INDEX_R_DOC_TYPE no existe en la base de datos.")
    '                End If
    '            Else
    '                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
    '                sw.WriteLine("")
    '                sw.WriteLine(sql.ToString)
    '                sw.WriteLine("")
    '                sw.Close()
    '                sw = Nothing
    '            End If
    '            sql = "Update INDEX_R_DOC_TYPE set ShowLotus=0"
    '            If GenerateScripts = False Then
    '                If ZPaq.ExisteTabla("INDEX_R_DOC_TYPE") Then
    '                    Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
    '                Else
    '                    Throw New Exception(Me.name & ": la tabla INDEX_R_DOC_TYPE no existe en la base de datos.")
    '                End If
    '            Else
    '                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
    '                sw.WriteLine("")
    '                sw.WriteLine(sql.ToString)
    '                sw.WriteLine("")
    '                sw.Close()
    '                sw = Nothing
    '            End If
    '        Catch EX As Exception
    '            flagException = True
    '            exBuilder.Append(EX.ToString())
    '            exBuilder.AppendLine()
    '        End Try

    '        Try
    '            sql = "CREATE TABLE USERFORM (ID NUMBER, IDFORM NUMBER NOT NULL, NOMBRE VARCHAR2(50))"
    '            If GenerateScripts = False Then
    '                If Not ZPaq.ExisteTabla("USERFORM") Then
    '                    Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
    '                Else
    '                    Throw New Exception(Me.name & ": la tabla USERFORM ya existe en la base de datos.")
    '                End If
    '            Else
    '                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
    '                sw.WriteLine("")
    '                sw.WriteLine(sql.ToString)
    '                sw.WriteLine("")
    '                sw.Close()
    '                sw = Nothing
    '            End If
    '        Catch EX As Exception
    '            flagException = True
    '            exBuilder.Append(EX.ToString())
    '            exBuilder.AppendLine()
    '        End Try

    '        Try
    '            sql = "CREATE TABLE USERADMIN (ID NUMBER, IDADMIN NUMBER NOT NULL, NOMBRE VARCHAR2(50))"
    '            If GenerateScripts = False Then
    '                If Not ZPaq.ExisteTabla("USERADMIN") Then
    '                    Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
    '                Else
    '                    Throw New Exception(Me.name & ": la tabla USERADMIN ya existe en la base de datos.")
    '                End If
    '            Else
    '                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
    '                sw.WriteLine("")
    '                sw.WriteLine(sql.ToString)
    '                sw.WriteLine("")
    '                sw.Close()
    '                sw = Nothing
    '            End If
    '        Catch EX As Exception
    '            flagException = True
    '            exBuilder.Append(EX.ToString())
    '            exBuilder.AppendLine()
    '        End Try

    '        Try
    '            sql = "CREATE TABLE USERPARAM (ID NUMBER, IDPARAM NUMBER NOT NULL, NOMBRE VARCHAR2(50))"
    '            If GenerateScripts = False Then
    '                If Not ZPaq.ExisteTabla("USERPARAM") Then
    '                    Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
    '                Else
    '                    Throw New Exception(Me.name & ": la tabla USERPARAM ya existe en la base de datos.")
    '                End If
    '            Else
    '                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
    '                sw.WriteLine("")
    '                sw.WriteLine(sql.ToString)
    '                sw.WriteLine("")
    '                sw.Close()
    '                sw = Nothing
    '            End If
    '        Catch EX As Exception
    '            flagException = True
    '            exBuilder.Append(EX.ToString())
    '            exBuilder.AppendLine()
    '        End Try

    '    Else   'SQL

    '        Try
    '            sql = "CREATE TABLE USERPARAM (ID INT, IDPARAM INT NOT NULL, NOMBRE CHAR(50))"
    '            If GenerateScripts = False Then
    '                If Not ZPaq.ExisteTabla("USERPARAM") Then
    '                    Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
    '                Else
    '                    Throw New Exception(Me.name & ": la tabla USERPARAM ya existe en la base de datos.")
    '                End If
    '            Else
    '                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
    '                sw.WriteLine("")
    '                sw.WriteLine(sql.ToString)
    '                sw.WriteLine("")
    '                sw.Close()
    '                sw = Nothing
    '            End If
    '        Catch EX As Exception
    '            flagException = True
    '            exBuilder.Append(EX.ToString())
    '            exBuilder.AppendLine()
    '        End Try

    '        Try
    '            sql = "CREATE TABLE USERADMIN (ID int, IDADMIN int NOT NULL, NOMBRE CHAR(50))"
    '            If GenerateScripts = False Then
    '                If Not ZPaq.ExisteTabla("USERADMIN") Then
    '                    Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
    '                Else
    '                    Throw New Exception(Me.name & ": la tabla USERADMIN ya existe en la base de datos.")
    '                End If
    '            Else
    '                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
    '                sw.WriteLine("")
    '                sw.WriteLine(sql.ToString)
    '                sw.WriteLine("")
    '                sw.Close()
    '                sw = Nothing
    '            End If
    '        Catch EX As Exception
    '            flagException = True
    '            exBuilder.Append(EX.ToString())
    '            exBuilder.AppendLine()
    '        End Try

    '        Try
    '            sql = "CREATE TABLE USERFORM (ID INT, IDFORM INT NOT NULL, NOMBRE CHAR(50))"
    '            If GenerateScripts = False Then
    '                If Not ZPaq.ExisteTabla("USERFORM") Then
    '                    Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
    '                Else
    '                    Throw New Exception(Me.name & ": la tabla USERFORM ya existe en la base de datos.")
    '                End If
    '            Else
    '                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
    '                sw.WriteLine("")
    '                sw.WriteLine(sql.ToString)
    '                sw.WriteLine("")
    '                sw.Close()
    '                sw = Nothing
    '            End If
    '        Catch EX As Exception
    '            flagException = True
    '            exBuilder.Append(EX.ToString())
    '            exBuilder.AppendLine()
    '        End Try

    '        Try
    '            sql = "CREATE TABLE NOTESINDEX_R_DOC_INDEX (INDEXID INT NOT NULL, CARGAR INT NOT NULL)"
    '            If GenerateScripts = False Then
    '                If Not ZPaq.ExisteTabla("NOTESINDEX_R_DOC_INDEX") Then
    '                    Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
    '                Else
    '                    Throw New Exception(Me.name & ": la tabla NOTESINDEX_R_DOC_INDEX ya existe en la base de datos.")
    '                End If
    '            Else
    '                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
    '                sw.WriteLine("")
    '                sw.WriteLine(sql.ToString)
    '                sw.WriteLine("")
    '                sw.Close()
    '                sw = Nothing
    '            End If
    '        Catch EX As Exception
    '            flagException = True
    '            exBuilder.Append(EX.ToString())
    '            exBuilder.AppendLine()
    '        End Try

    '        Try
    '            sql = "CREATE TABLE USRNOTES (ID INT, NOMBRE CHAR(50), CONF_PATHREMOTOARCH CHAR(50), CONF_MAILSERVER CHAR(50), CONF_BASEMAIL CHAR(50), CONF_PATHARCH CHAR(50), CONF_VISTAEXPORTACION CHAR(50), CONF_PAPELERA CHAR(50), CONF_NOMARCHTXT CHAR(50), CONF_SEQMSG INT, CONF_SEQATT INT, CONF_LOCKEO INT, CONF_ACUMIMG INT, CONF_LIMIMG INT, CONF_DESTEXT INT, CONF_TEXTOSUBJECT CHAR(50), CONF_BORRAR CHAR(50), CONF_ARCHCTRL CHAR(50), CONF_SCHEDULESEL CHAR(50), CONF_SCHEDULEFIJO CHAR(50), CONF_SCHEDULEVAR CHAR(50), CONF_EJECUTABLE CHAR(50), CONF_NOMUSERNOTES CHAR(50), CONF_NOMUSERRED CHAR(50), CONF_CHARSREEMPSUBJ CHAR(50), LASTRUNTIME DATE, CONF_REINTENTO INT, ACTIVO INT, CONF_SEQIMG INT)"
    '            If GenerateScripts = False Then
    '                If Not ZPaq.ExisteTabla("USRNOTES") Then
    '                    Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
    '                Else
    '                    Throw New Exception(Me.name & ": la tabla USRNOTES ya existe en la base de datos.")
    '                End If
    '            Else
    '                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
    '                sw.WriteLine("")
    '                sw.WriteLine(sql.ToString)
    '                sw.WriteLine("")
    '                sw.Close()
    '                sw = Nothing
    '            End If
    '        Catch EX As Exception
    '            flagException = True
    '            exBuilder.Append(EX.ToString())
    '            exBuilder.AppendLine()
    '        End Try

    '        Try
    '            sql = "CREATE TABLE FRMNOTES (ID INT NOT NULL, NOMBRE CHAR(50) NOT NULL)"
    '            If GenerateScripts = False Then
    '                If Not ZPaq.ExisteTabla("FRMNOTES") Then
    '                    Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
    '                Else
    '                    Throw New Exception(Me.name & ": la tabla FRMNOTES ya existe en la base de datos.")
    '                End If
    '            Else
    '                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
    '                sw.WriteLine("")
    '                sw.WriteLine(sql.ToString)
    '                sw.WriteLine("")
    '                sw.Close()
    '                sw = Nothing
    '            End If
    '        Catch EX As Exception
    '            flagException = True
    '            exBuilder.Append(EX.ToString())
    '            exBuilder.AppendLine()
    '        End Try

    '        Try
    '            sql = "CREATE TABLE USR_R_FORM (IDFORM INT NOT NULL,IDUSR INT NOT NULL)"
    '            If GenerateScripts = False Then
    '                If Not ZPaq.ExisteTabla("USR_R_FORM") Then
    '                    Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
    '                Else
    '                    Throw New Exception(Me.name & ": la tabla USR_R_FORM ya existe en la base de datos.")
    '                End If
    '            Else
    '                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
    '                sw.WriteLine("")
    '                sw.WriteLine(sql.ToString)
    '                sw.WriteLine("")
    '                sw.Close()
    '                sw = Nothing
    '            End If
    '        Catch EX As Exception
    '            flagException = True
    '            exBuilder.Append(EX.ToString())
    '            exBuilder.AppendLine()
    '        End Try

    '        Try
    '            sql = "ALTER TABLE INDEX_R_DOC_TYPE ADD MustComplete int default 0"
    '            If GenerateScripts = False Then
    '                If ZPaq.ExisteTabla("INDEX_R_DOC_TYPE") Then
    '                    Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
    '                Else
    '                    Throw New Exception(Me.name & ": la tabla INDEX_R_DOC_TYPE no existe en la base de datos.")
    '                End If
    '            Else
    '                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
    '                sw.WriteLine("")
    '                sw.WriteLine(sql.ToString)
    '                sw.WriteLine("")
    '                sw.Close()
    '                sw = Nothing
    '            End If
    '            sql = "Update INDEX_R_DOC_TYPE set MustComplete=0"
    '            If GenerateScripts = False Then
    '                If ZPaq.ExisteTabla("INDEX_R_DOC_TYPE") Then
    '                    Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
    '                Else
    '                    Throw New Exception(Me.name & ": la tabla INDEX_R_DOC_TYPE no existe en la base de datos.")
    '                End If
    '            Else
    '                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
    '                sw.WriteLine("")
    '                sw.WriteLine(sql.ToString)
    '                sw.WriteLine("")
    '                sw.Close()
    '                sw = Nothing
    '            End If
    '        Catch EX As Exception
    '            flagException = True
    '            exBuilder.Append(EX.ToString())
    '            exBuilder.AppendLine()
    '        End Try

    '        Try
    '            sql = "ALTER TABLE INDEX_R_DOC_TYPE ADD LoadLotus int default 0"
    '            If GenerateScripts = False Then
    '                If ZPaq.ExisteTabla("INDEX_R_DOC_TYPE") Then
    '                    Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
    '                Else
    '                    Throw New Exception(Me.name & ": la tabla INDEX_R_DOC_TYPE no existe en la base de datos.")
    '                End If
    '            Else
    '                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
    '                sw.WriteLine("")
    '                sw.WriteLine(sql.ToString)
    '                sw.WriteLine("")
    '                sw.Close()
    '                sw = Nothing
    '            End If
    '            sql = "Update INDEX_R_DOC_TYPE set LoadLotus=0"
    '            If GenerateScripts = False Then
    '                If ZPaq.ExisteTabla("INDEX_R_DOC_TYPE") Then
    '                    Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
    '                Else
    '                    Throw New Exception(Me.name & ": la tabla INDEX_R_DOC_TYPE no existe en la base de datos.")
    '                End If
    '            Else
    '                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
    '                sw.WriteLine("")
    '                sw.WriteLine(sql.ToString)
    '                sw.WriteLine("")
    '                sw.Close()
    '                sw = Nothing
    '            End If
    '        Catch EX As Exception
    '            flagException = True
    '            exBuilder.Append(EX.ToString())
    '            exBuilder.AppendLine()
    '        End Try

    '        Try
    '            sql = "ALTER TABLE INDEX_R_DOC_TYPE ADD ShowLotus int default 0"
    '            If GenerateScripts = False Then
    '                If ZPaq.ExisteTabla("INDEX_R_DOC_TYPE") Then
    '                    Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
    '                Else
    '                    Throw New Exception(Me.name & ": la tabla INDEX_R_DOC_TYPE no existe en la base de datos.")
    '                End If
    '            Else
    '                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
    '                sw.WriteLine("")
    '                sw.WriteLine(sql.ToString)
    '                sw.WriteLine("")
    '                sw.Close()
    '                sw = Nothing
    '            End If
    '            sql = "Update INDEX_R_DOC_TYPE set ShowLotus=0"
    '            If GenerateScripts = False Then
    '                If Not ZPaq.ExisteTabla("INDEX_R_DOC_TYPE") Then
    '                    Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
    '                Else
    '                    Throw New Exception(Me.name & ": la tabla INDEX_R_DOC_TYPE no existe en la base de datos.")
    '                End If
    '            Else
    '                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
    '                sw.WriteLine("")
    '                sw.WriteLine(sql.ToString)
    '                sw.WriteLine("")
    '                sw.Close()
    '                sw = Nothing
    '            End If
    '        Catch EX As Exception
    '            flagException = True
    '            exBuilder.Append(EX.ToString())
    '            exBuilder.AppendLine()
    '        End Try

    '        Try
    '            Actualizar()
    '        Catch EX As Exception
    '            flagException = True
    '            exBuilder.Append(EX.ToString())
    '            exBuilder.AppendLine()
    '        End Try

    '        If flagException Then
    '            Throw New Exception(exBuilder.ToString())
    '        End If
    '    End If
    '    Return True
    'End Function

    Private Sub Actualizar()
        'TODO store: SPGetIndex_ID()
        Dim ds As New DataSet
        Dim sql As String = "Select Index_ID from doc_Index where Dropdown=2"

        If ZPaq.ExisteTabla("doc_Index") Then
            ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
        Else
            Throw New Exception(Me.name & ": la tabla doc_Index no existe en la base de datos.")
        End If
        Dim i As Int32
        For i = 0 To ds.Tables(0).Rows.Count - 1
            sql = "Update INDEX_R_DOC_TYPE set LoadLotus=1 where Index_ID=" & CInt(ds.Tables(0).Rows(i).Item(0))
            If ZPaq.ExisteTabla("INDEX_R_DOC_TYPE") Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql)
            Else
                Throw New Exception(Me.name & ": la tabla INDEX_R_DOC_TYPE no existe en la base de datos.")
            End If
        Next
    End Sub

    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_CreateTables_Exportacion_Mails"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_CreateTables_Exportacion_Mails
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("01/05/2006")
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
            Return 20
        End Get
    End Property
End Class
