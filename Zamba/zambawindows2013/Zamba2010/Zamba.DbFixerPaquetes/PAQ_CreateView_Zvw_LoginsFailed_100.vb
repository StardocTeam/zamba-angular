Imports Zamba.Servers
Imports Zamba.AppBlock
Public Class PAQ_CreateView_Zvw_LoginsFailed_100
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub
    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides ReadOnly Property EditDate() As Date
        Get
            Return Date.Parse("28/06/2006")
        End Get
    End Property
    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("08/05/2006")
        End Get
    End Property

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "1.5.9"
        End Get
    End Property

    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Crea vista de Fecha,Usuario y Descripción"
        End Get
    End Property

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute
        Dim sql As New System.Text.StringBuilder
        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            'SQL
            Try
                sql.Append("CREATE view Zvw_LoginsFailed_100 As Select TOP 100 percent User_hst.Action_date as Fecha,(UsrTable.Apellido + ' ' + usrtable.nombres) as Usuario,User_hst.s_object_id as Descripcion from User_hst inner join Usrtable on (user_hst.User_ID=UsrTable.Id) where user_hst.ACTION_TYPE=27 order by Usuario,fecha desc")
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        Else
            'ORACLE
            Try
                sql.Append("CREATE OR REPLACE VIEW ZVW_LOGINSFAILED_100 (FECHA,USUARIO,DESCRIPCION) AS SELECT USER_HST.ACTION_DATE Fecha,(USRTABLE.NAME ||' '||USRTABLE.APELLIDO) Usuario,USER_HST.S_OBJECT_ID Descripcion FROM USER_HST INNER JOIN USRTABLE ON(USRTABLE.ID =USER_HST.USER_ID) WHERE USER_HST.ACTION_TYPE=27 ORDER BY Usuario,fecha desc")
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End If
        sql = Nothing
        Return True
    End Function

    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "PAQ_CreateView_Zvw_LoginsFailed_100"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_CreateView_Zvw_LoginsFailed_100
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
            Return 65
        End Get
    End Property
End Class
