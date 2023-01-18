'listo
Imports Zamba.Servers
Imports Zamba.AppBlock
Public Class PAQ_AddColumn_ZsecOption_RepeatPassword
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub

#Region "Propiedades"
    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_AddColumn_ZsecOption_RepeatPassword"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_AddColumn_ZsecOption_RepeatPassword
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get

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
            Return 38
        End Get
    End Property

    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Agrega la columna RepeatPassword a la tabla ZsecOption, tabla que contiene las opciones de seguridad."
        End Get
    End Property

#End Region

#Region "Ejecucion"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        Dim sql As String
        If ZPaq.ExisteColumna("RepeatPassword", "ZsecOption") = False Then
            If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
                sql = "Alter Table ZsecOption add RepeatPassword nvarchar(5)"
            Else
                sql = "Alter Table ZsecOption add RepeatPassword nvarchar2(5)"
            End If

            Try
                If GenerateScripts = False Then
                    If ZPaq.ExisteTabla("ZsecOption") Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
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
                Dim exn As New Exception("ERROR | " & ex.ToString & " en " & sql)
                'ZException.Log(exn, False)
                MessageBox.Show(exn.Message)
            End Try
        Else
            MessageBox.Show("Ya existía la columna previamente")
        End If
        Return True
    End Function
#End Region

End Class
