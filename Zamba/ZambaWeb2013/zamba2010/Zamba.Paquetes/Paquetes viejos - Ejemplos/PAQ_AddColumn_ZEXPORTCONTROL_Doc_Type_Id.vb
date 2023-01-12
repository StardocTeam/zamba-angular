Imports Zamba.Servers
Imports Zamba.Core
Imports Microsoft.Win32
Public Class PAQ_AddColumn_ZEXPORTCONTROL_Doc_Type_Id
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub

#Region "Propiedades"

    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_AddColumn_ZEXPORTCONTROL_Doc_Type_Id"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_AddColumn_ZEXPORTCONTROL_Doc_Type_Id
        End Get
    End Property

    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Agrega la columna DOC_TYPE_ID a la tabla ZEXPORTCONTROL "
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("30/05/1931")
        End Get
    End Property

    Public Overrides ReadOnly Property EditDate() As Date
        Get
            Return Date.Parse("30/05/1931")
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
            Return 36
        End Get
    End Property

#End Region

#Region "Ejecución"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        Dim Sql As New System.Text.StringBuilder
        Dim sql2 As New System.Text.StringBuilder
        Try
            If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then

            Else

                Sql.Append("ALTER TABLE ZEXPORTCONTROL ADD (DOC_TYPE_ID NUMBER);")
                'Server.Con.ExecuteNonQuery(CommandType.Text, Sql.ToString)
                'sql2.Append(Sql.ToString)
                'Sql.Remove(0, Sql.Length)



            End If

            If GenerateScripts = False Then
                If ZPaq.ExisteTabla("ZEXPORTCONTROL") Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, Sql.ToString)
                End If
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(Sql.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If

        Catch ex As Exception

        End Try
        Return True
        Sql = Nothing

    End Function
#End Region

End Class
