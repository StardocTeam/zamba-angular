'ver
Imports Zamba.Servers
Imports Zamba.Core
Public Class PAQ_CreateTable_PRO
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub


#Region "Propiedades"
    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_CreateTable_PRO"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_CreateTable_PRO
        End Get
    End Property
    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Creacion de Tabla PRO - Version 1.0.0.0"
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
            Return 4
        End Get
    End Property
#End Region

#Region "Ejecución"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        'CREA LA TABLA PRO
        Dim strcreate As String = String.Empty
        Dim flagError As Boolean = False
        Dim exBuilder As New System.Text.StringBuilder()

        Try
            If Server.ServerType = Server.DBTYPES.Oracle OrElse Server.ServerType = Server.DBTYPES.Oracle9 OrElse Server.ServerType = Server.DBTYPES.OracleClient Then
                strcreate = "CREATE TABLE PRO (Pro_Id number(4) NOT NULL, Pro_Query nvarchar2(200) NOT NULL, Pro_Executed number (1) NOT NULL, Pro_Type number (1) NOT NULL, Pro_Resultado nvarchar2(50))"
            Else
                strcreate = "CREATE TABLE PRO (Pro_Id int NOT NULL, Pro_Query nvarchar(200) NOT NULL, Pro_Executed bit NOT NULL, Pro_Type smallint NOT NULL, Pro_Resultado nvarchar (50) NOT NULL)"
            End If

            If GenerateScripts = False Then
                If ZPaq.ExisteTabla("PRO") = True Then
                    Throw New Exception(Me.name & ": la tabla PRO ya existe en la base de datos")
                End If
                Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(strcreate.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
        Catch ex As Exception
            flagError = True
            exBuilder.AppendLine(strcreate)
            exBuilder.Append("Error: ")
            exBuilder.AppendLine(ex.ToString())
            exBuilder.AppendLine()
        End Try

        Try
            If Server.ServerType = Server.DBTYPES.Oracle OrElse Server.ServerType = Server.DBTYPES.Oracle9 OrElse Server.ServerType = Server.DBTYPES.OracleClient Then
                strcreate = "ALTER TABLE PRO ADD CONSTRAINT pk_PRO PRIMARY KEY (Pro_Id)"
            Else
                strcreate = "ALTER TABLE PRO ADD CONSTRAINT pk_PRO PRIMARY KEY (Pro_Id)"
            End If

            If GenerateScripts = False Then
                If ZPaq.ExisteTabla("PRO") Then
                    Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
                Else
                    Throw New Exception(Me.name & ": la tabla PRO no existe en la base de datos.")
                End If
            Else
                Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                sw.WriteLine("")
                sw.WriteLine(strcreate.ToString)
                sw.WriteLine("")
                sw.Close()
                sw = Nothing
            End If
        Catch ex As Exception
            flagError = True
            exBuilder.AppendLine(strcreate)
            exBuilder.Append("Error: ")
            exBuilder.AppendLine(ex.ToString())
            exBuilder.AppendLine()
        End Try

        If flagError Then
            Throw New Exception(exBuilder.ToString())
        Else
            Return True
        End If
    End Function
#End Region

End Class
