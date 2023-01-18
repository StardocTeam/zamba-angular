Imports Zamba.Servers
Imports Zamba.Core
Public Class PAQ_AlterTable_IP_FOLDER
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub

#Region "Propiedades"
    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_AlterTable_IP_FOLDER"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_AlterTable_IP_FOLDER
        End Get
    End Property

    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Modifica la columna 'NOMBRE' en la tabla IP_FOLDER como: VARCHAR2(100) NOT NULL, y setea la Primary Key."
        End Get
    End Property

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
            Return 46
        End Get
    End Property
#End Region

#Region "Ejecución"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        'AGREGA LA PK
        Dim StrPK As String

        If Server.ServerType = Server.DBTYPES.Oracle OrElse Server.ServerType = Server.DBTYPES.Oracle9 OrElse Server.ServerType = Server.DBTYPES.OracleClient Then
            Try
                StrPK = "ALTER TABLE IP_FOLDER MODIFY(NOMBRE VARCHAR2(100) NOT NULL)"
                If GenerateScripts = False Then
                    If ZPaq.ExisteTabla("IP_FOLDER") Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, StrPK.ToString)
                    End If
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(StrPK.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
            Try
                StrPK = "ALTER TABLE IP_FOLDER MODIFY (path,nombre CONSTRAINT 'IP_FOLDER_UK41134155798687' PRIMARY KEY())"
                If GenerateScripts = False Then
                    If ZPaq.ExisteTabla("IP_FOLDER") Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, StrPK.ToString)
                    End If
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(StrPK.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        Else
            StrPK = "alter table IP_FOLDER  drop constraint PK__IP_FOLDER__0C06BB60 "
            If GenerateScripts = False Then
                If ZPaq.ExisteTabla("IP_FOLDER") Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, StrPK.ToString)
                End If
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(StrPK.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            StrPK = "alter table ip_folder alter column NOMBRE nvarchar(100) not null"
            If GenerateScripts = False Then
                If ZPaq.ExisteTabla("ip_folder") Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, StrPK.ToString)
                End If
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(StrPK.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
            StrPK = "alter table ip_folder add Nombre,path CONSTRAINT pk PRIMARY KEY"
            If GenerateScripts = False Then
                If ZPaq.ExisteTabla("ip_folder") Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, StrPK.ToString)
                End If
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(StrPK.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
        End If
        Return True
    End Function
#End Region

End Class
