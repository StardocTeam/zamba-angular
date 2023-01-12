Imports Zamba.Servers
Imports zamba.AppBlock

Public Class PAQ_CreateView_ZviewShowInsertsHistory
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub
    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Crea vista ZviewShowInsertsHistory"
        End Get
    End Property

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        Dim sql As New System.Text.StringBuilder

        Try

            If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
                'SQL
                If ZPaq.IfExists("ZviewShowInsertsHistory", ZPaq.Tipo.View, False) = True Then
                    sql.Append("drop VIEW ZviewShowInsertsHistory")
                    Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                    sql.Remove(0, sql.Length)
                End If
                sql.Append("CREATE VIEW ZviewShowInsertsHistory AS")
                sql.Append(ControlChars.NewLine)
                sql.Append("    SELECT  USRTABLE.NAME AS Usuario, USER_HST.ACTION_DATE AS [Fecha de Insertado], ")
                sql.Append(ControlChars.NewLine)
                sql.Append("            USER_HST.S_OBJECT_ID AS [Nombre del Documento Insertado],RIGHTSTYPE.RIGHTSTYPE AS ACCION")
                sql.Append(ControlChars.NewLine)
                sql.Append("    FROM    USER_HST INNER JOIN")
                sql.Append(ControlChars.NewLine)
                sql.Append("            USRTABLE ON USER_HST.USER_ID = USRTABLE.ID INNER JOIN")
                sql.Append(ControlChars.NewLine)
                sql.Append("            RIGHTSTYPE ON USER_HST.ACTION_TYPE = RIGHTSTYPE.RIGHTSTYPEID ")
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)


            Else

                If Server.ServerType = Server.DBTYPES.Oracle Or Server.ServerType = Server.DBTYPES.Oracle9 Or Server.ServerType = Server.DBTYPES.OracleClient Then
                    sql.Append("CREATE OR REPLACE VIEW ZviewShowInsertsHistory AS ")
                    sql.Append(" SELECT USRTABLE.NAME AS Usuario, USER_HST.ACTION_DATE AS FechaInsertado,")
                    sql.Append(" USER_HST.S_OBJECT_ID AS NombreDocumento,RIGHTSTYPE.RIGHTSTYPE AS ACCION")
                    sql.Append(" FROM  USER_HST INNER JOIN USRTABLE ON")
                    sql.Append(" USER_HST.USER_ID = USRTABLE.ID INNER JOIN RIGHTSTYPE ON")
                    sql.Append(" USER_HST.ACTION_TYPE = RIGHTSTYPE.RIGHTSTYPEID")
                    Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)

                End If
            End If
            Return True
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Function

    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "Zview_ShowInsertsHistory"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_CreateView_ZviewShowInsertsHistory
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("23/05/07")
        End Get
    End Property

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "3.0.0"
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
            Return 64
        End Get
    End Property


End Class
