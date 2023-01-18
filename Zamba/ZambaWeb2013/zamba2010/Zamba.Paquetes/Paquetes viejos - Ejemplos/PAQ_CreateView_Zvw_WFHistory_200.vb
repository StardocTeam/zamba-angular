Imports Zamba.Servers
Imports zamba.AppBlock

Public Class PAQ_CreateView_Zvw_WFHistory_200
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub
    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Crea vista Zvw_WFHistory_200"
        End Get
    End Property

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        Dim sql As New System.Text.StringBuilder
        Dim var As Boolean

        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            'SQL
            var = False
            Try
                If ZPaq.IfExists("Zvw_WFHistory_200", ZPaq.Tipo.View, False) = True Then
                    If MessageBox.Show("La vista Zvw_WFHistory_200 ya existe, desea borrarla?", "Creación de Vista", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly) Then
                        sql.Append("drop VIEW Zvw_WFHistory_200")
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                        sql.Remove(0, sql.Length)
                        var = True
                    End If
                Else
                    var = True
                End If

                If var = True Then
                    sql.Append("CREATE VIEW Zvw_WFHistory_200 AS SELECT ")
                    sql.Append(ControlChars.NewLine)
                    sql.Append("fecha AS Fecha, ")
                    sql.Append(ControlChars.NewLine)
                    sql.Append("Step_Name AS Etapa, ")
                    sql.Append(ControlChars.NewLine)
                    sql.Append("State AS Estado, ")
                    sql.Append(ControlChars.NewLine)
                    sql.Append("UserName AS Usuario, ")
                    sql.Append(ControlChars.NewLine)
                    sql.Append("Accion AS Accion, ")
                    sql.Append(ControlChars.NewLine)
                    sql.Append("Doc_Name AS Tarea, ")
                    sql.Append(ControlChars.NewLine)
                    sql.Append("Doc_Type_Name AS [Tipo Documento], ")
                    sql.Append(ControlChars.NewLine)
                    sql.Append("Doc_Id ")
                    sql.Append(ControlChars.NewLine)
                    sql.Append("FROM WFStepHst")
                    sql.Append(ControlChars.NewLine)
                    Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                End If
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
            sql.Remove(0, sql.Length)
        End If
        sql = Nothing
        Return True

    End Function

    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_Zvw_WFHistory_200"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_CreateView_Zvw_WFHistory_200
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("18/01/07")
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
            Return 66
        End Get
    End Property
End Class
