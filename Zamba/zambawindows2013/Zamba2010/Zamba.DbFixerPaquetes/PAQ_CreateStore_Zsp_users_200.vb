Imports Zamba.Servers

Public Class PAQ_CreateStore_Zsp_users_200
    Inherits ZPaq
    Implements IPAQ

    Public Overrides Sub Dispose()

    End Sub

#Region "Propiedades"

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("07/07/2006")
        End Get
    End Property

    Public Overrides ReadOnly Property EditDate() As Date
        Get
            Return Date.Parse("21/07/2006 11:41")
        End Get
    End Property

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
        End Get
    End Property

    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Crea un procedimiento almacenado para obtener un usuario en base al nombre. Realiza la busqueda en UsrTable"
        End Get
    End Property

    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "PAQ_CreateStore_Zsp_users_200"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_CreateStore_Zsp_users_200
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
            Return 78
        End Get
    End Property

#End Region

#Region "Ejecución"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute
        If Server.ServerType = Server.DBTYPES.MSSQLServer7Up OrElse Server.ServerType = Server.DBTYPES.MSSQLServer Then
            Return Me.ExecuteSQL(GenerateScripts)
        Else
            Return Me.ExecuteOracle(GenerateScripts)
        End If
    End Function
    Private Function ExecuteOracle(ByVal generatescripts As Boolean) As Boolean
        Dim sql As New System.Text.StringBuilder
        Dim sw As IO.StreamWriter

        Try
            sql.Append("CREATE OR REPLACE PACKAGE Zsp_users_200 AS ")
            sql.Append("TYPE t_cursor IS REF CURSOR;")
            sql.Append("PROCEDURE GetUserByName (username IN Usrtable.Name%TYPE, io_cursor OUT t_cursor);")
            sql.Append("END Zsp_users_200;")

            If generatescripts = True Then
                sw = New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine(sql.ToString)
            Else
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            End If

            sql.Remove(0, sql.Length)

            sql.Append("CREATE OR REPLACE PACKAGE BODY Zsp_users_200 AS ")
            sql.Append("PROCEDURE GetUserByName (username IN Usrtable.Name%TYPE, io_cursor OUT t_cursor) IS ")
            sql.Append("v_cursor t_cursor;")
            sql.Append("BEGIN ")
            sql.Append("OPEN v_cursor FOR ")
            sql.Append("SELECT * FROM usrtable where name =username;")
            sql.Append("io_cursor := v_cursor;")
            sql.Append("END GetUserByName;")
            sql.Append("END Zsp_users_200;")

            If generatescripts = True Then
                sw.WriteLine(sql.ToString)
                Return True
            Else
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                Return True
            End If
        Catch ex As Exception
            Return False
        Finally
            sql.Remove(0, sql.Length)
            If Not IsNothing(sw) Then
                sw.Close()
            End If
        End Try
    End Function
    Private Function ExecuteSQL(ByVal generatescripts As Boolean) As Boolean
        'TODO store: Zsp_users_200_GetUserByName 
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append("Create Proc Zsp_users_200_GetUserByName")
            sql.Append(ControlChars.NewLine)
            sql.Append("@name nvarchar(20)")
            sql.Append(ControlChars.NewLine)
            sql.Append("as")
            sql.Append(ControlChars.NewLine)
            sql.Append("Select * from usrtable where name=@name")
            If generatescripts = True Then
                sql.Append(ControlChars.NewLine)
                sql.Append("GO")
                sql.Append(ControlChars.NewLine)
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine(sql.ToString)
                sw.Close()
                sw = Nothing
                Return True
            Else
                IfExists("Zsp_users_200_GetUserByName", ZPaq.Tipo.StoredProcedure, False)
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                Return True
            End If
            Return False
        Catch ex As Exception
            Return False
        Finally
            sql = Nothing
        End Try
    End Function

#End Region

End Class
