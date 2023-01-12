Imports Zamba.Servers
Imports Zamba.AppBlock
Imports System.Data

Public Class PAQ_CreateTables_Mensajes
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub
    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("02/05/2006")
        End Get
    End Property

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
        End Get
    End Property

    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Crea tablas MSG_ATTACH,MSG_DEST,MSG_DEST_TYPE"
        End Get
    End Property

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        Dim sql As New System.Text.StringBuilder
        Dim flagException As Boolean = False
        Dim exBuilder As New System.Text.StringBuilder()

        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            'SQL
            'TABLA MSG_ATTACH
            sql.Append("CREATE TABLE [MSG_ATTACH] ([MSG_ID] [numeric](10, 0) NOT NULL ,[DOC_ID] [numeric](10, 0) NOT NULL ,[DOC_TYPE_ID] [numeric](10, 0) NOT NULL ,[FOLDER_ID] [numeric](10, 0) NOT NULL ,[INDEX_ID] [numeric](10, 0) NOT NULL ,[NAME] [varchar] (100) NOT NULL ,[ICON] [numeric](10, 0) NOT NULL ,[volumelistid] [numeric](10, 0) NULL ,[DOC_FILE] [nvarchar] (50) NULL ,[Offset] [numeric](10, 0) NULL ,[Disk_Vol_Path] [nvarchar] (250) NULL) ON [PRIMARY]")
            Try
                If Not ZPaq.ExisteTabla("MSG_ATTACH") Then
                    If Not GenerateScripts Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString())
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If
                Else
                    Throw New Exception(Me.name & ": la tabla MSG_ATTACH  ya existe en la base de datos.")
                End If
            Catch ex As Exception
                flagException = True
                exBuilder.AppendLine(sql.ToString())
                exBuilder.Append("Error: ")
                exBuilder.Append(ex.ToString())
                exBuilder.AppendLine()
                exBuilder.AppendLine()
            End Try

            sql.Remove(0, sql.Length)



            sql.Append("CREATE TABLE [MSG_DEST] ([MSG_ID] [numeric](10, 0) NOT NULL ,[USER_ID] [numeric](10, 0) NOT NULL ,[DEST_TYPE] [numeric](18, 0) NOT NULL ,[READ] [bit] NOT NULL ,[DELETED] [bit] NOT NULL ,[USER_NAME] [varchar] (40) NOT NULL) ON [PRIMARY]")
            Try
                If Not ZPaq.ExisteTabla("MSG_DEST") Then
                    If Not GenerateScripts Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString())
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If
                Else
                    Throw New Exception(Me.name & ": la tabla MSG_DEST  ya existe en la base de datos.")
                End If
            Catch ex As Exception
                flagException = True
                exBuilder.AppendLine(sql.ToString())
                exBuilder.Append("Error: ")
                exBuilder.Append(ex.ToString())
                exBuilder.AppendLine()
                exBuilder.AppendLine()
            End Try

            sql.Remove(0, sql.Length)


            sql.Append("CREATE TABLE [MSG_DEST_TYPE] ([MSG_TYPE] [numeric](18, 0) NOT NULL ,[TYPE_TEXT] [nvarchar] (20) NOT NULL) ON [PRIMARY]")
            Try
                If Not ZPaq.ExisteTabla("MSG_DEST_TYPE") Then
                    If Not GenerateScripts Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString())
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If
                Else
                    Throw New Exception(Me.name & ": la tabla MSG_DEST_TYPE  ya existe en la base de datos.")
                End If
            Catch ex As Exception
                flagException = True
                exBuilder.AppendLine(sql.ToString())
                exBuilder.Append("Error: ")
                exBuilder.Append(ex.ToString())
                exBuilder.AppendLine()
                exBuilder.AppendLine()
            End Try

        Else

            'ORACLE


            sql.Append("CREATE TABLE MSG_ATTACH (MSG_ID NUMBER(10) NOT NULL ,DOC_ID NUMBER(10) NOT NULL ,DOC_TYPE_ID NUMBER(10) NOT NULL ,FOLDER_ID NUMBER(10) NOT NULL ,INDEX_ID NUMBER(10) NOT NULL ,NAME VARCHAR2 (100) NOT NULL ,ICON NUMBER(10) NOT NULL ,volumelistid NUMBER(10) NULL ,DOC_FILE VARCHAR2 (50) NULL ,Offset NUMBER(10) NULL ,Disk_Vol_Path VARCHAR2 (250) NULL)")
            Try
                If Not ZPaq.ExisteTabla("MSG_ATTACH") Then
                    If Not GenerateScripts Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString())
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If
                Else
                    Throw New Exception(Me.name & ": la tabla MSG_ATTACH  ya existe en la base de datos.")
                End If
            Catch ex As Exception
                flagException = True
                exBuilder.AppendLine(sql.ToString())
                exBuilder.Append("Error: ")
                exBuilder.Append(ex.ToString())
                exBuilder.AppendLine()
                exBuilder.AppendLine()
            End Try

            sql.Append("CREATE TABLE MSG_DEST (MSG_ID NUMBER(10) NOT NULL ,USER_ID NUMBER(10) NOT NULL ,DEST_TYPE NUMBER(18) NOT NULL ,READ bit NOT NULL ,DELETED bit NOT NULL ,USER_NAME VARCHAR2 (40) NOT NULL)")
            Try
                If Not ZPaq.ExisteTabla("MSG_DEST") Then
                    If Not GenerateScripts Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString())
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If
                Else
                    Throw New Exception(Me.name & ": la tabla MSG_DEST  ya existe en la base de datos.")
                End If
            Catch ex As Exception
                flagException = True
                exBuilder.AppendLine(sql.ToString())
                exBuilder.Append("Error: ")
                exBuilder.Append(ex.ToString())
                exBuilder.AppendLine()
                exBuilder.AppendLine()
            End Try

            sql.Append("CREATE TABLE MSG_DEST_TYPE (MSG_TYPE NUMBER(18) NOT NULL ,TYPE_TEXT VARCHAR2 (20) NOT NULL)")
            Try
                If Not ZPaq.ExisteTabla("MSG_DEST_TYPE") Then
                    If Not GenerateScripts Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString())
                    Else
                        Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                        sw.WriteLine("")
                        sw.WriteLine(sql)
                        sw.WriteLine("")
                        sw.Close()
                        sw = Nothing
                    End If
                Else
                    Throw New Exception(Me.name & ": la tabla MSG_DEST_TYPE  ya existe en la base de datos.")
                End If
            Catch ex As Exception
                flagException = True
                exBuilder.AppendLine(sql.ToString())
                exBuilder.Append("Error: ")
                exBuilder.Append(ex.ToString())
                exBuilder.AppendLine()
                exBuilder.AppendLine()
            End Try
        End If


        sql.Remove(0, sql.Length)
        sql = Nothing

        Return True
    End Function

    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_CreateTables"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_CreateTables_Mensajes
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
            Return 27
        End Get
    End Property
End Class
