Imports Zamba.Servers
Imports Zamba.AppBlock

Public Class PAQ_CreateStore_zsp_messages_100_GetMyMessagesNew
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub

#Region "Propiedades"
    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Crea procedimiento zsp_messages_100_GetMyMessagesNew "
        End Get
    End Property
    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property
    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("04/05/2006")
        End Get
    End Property
    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "1.5.9"
        End Get
    End Property
    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "PAQ_CreateStore_zsp_messages_100_GetMyMessagesNew"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_CreateStore_zsp_messages_100_GetMyMessagesNew
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
            Return 75
        End Get
    End Property
#End Region

#Region "Ejecución"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute
        Dim sql As New System.Text.StringBuilder
        Dim var As Boolean
        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            'SQL
            If ZPaq.IfExists("zsp_GetMyMessagesNew", ZPaq.Tipo.StoredProcedure, False) = False Then
                If MessageBox.Show("La tabla MSG_ATTACH ya existe desea borrarla?", "Creacion de Tabla", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly) = DialogResult.Yes Then
                    sql.Append("DROP procedure zsp_GetMyMessagesNew")
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
                    var = True
                    sql.Remove(0, sql.Length)
                End If
            Else
                var = True
            End If
            If var = True Then
                sql.Append("CREATE procedure zsp_GetMyMessagesNew @my_id INT AS Select msg_id from MSG_DEST where [msg_dest].[read] = 0 and user_id =@my_id and msg_dest.deleted=0")
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            End If
            Try
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Catch ex As Exception
                MsgBox("Error al ejecutar el paquete")
            End Try

        Else
            'ORACLE
            sql.Append("CREATE OR REPLACE PACKAGE zsp_GetMyMessagesNew_Pkg AS type t_cursor is ref cursor;PROCEDURE zsp_GetMyMessagesNew(my_id IN MSG_DEST.user_id%type,io_cursor OUT t_cursor);END zsp_GetMyMessagesNew_Pkg;")
            Try
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Catch ex As Exception
                MsgBox("Error al ejecutar el paquete")
            End Try
            sql.Remove(0, sql.Length)
            sql.Append("CREATE OR REPLACE PACKAGE BODY zsp_GetMyMessagesNew_Pkg AS PROCEDURE zsp_GetMyMessagesNew(my_id IN MSG_DEST.user_id%type,io_cursor OUT t_cursor) IS v_cursor t_cursor;BEGIN OPEN v_cursor for Select msg_id from MSG_DEST where MSG_DEST.READ='0' and ")
            sql.Append("user_id = my_id and MSG_DEST.deleted='0';io_cursor := v_cursor; END zsp_GetMyMessagesNew;	End zsp_GetMyMessagesNew_Pkg;")
            Try
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Catch ex As Exception
                MsgBox("Error al ejecutar el paquete")
            End Try
        End If

        sql = Nothing
        Return True
    End Function
#End Region



End Class
