Imports Zamba.Servers
Imports zamba.AppBlock

Public Class PAQ_CreateTable_WorkFlow
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub

    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Crea las tablas de Worflow para Oracle"
        End Get
    End Property

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute
        Dim sql As New System.Text.StringBuilder()
        Dim var As Boolean
        Dim exBuilder As New System.Text.StringBuilder()
        Dim flagException As Boolean = False

        If Server.ServerType = Server.DBTYPES.OracleClient OrElse Server.ServerType = Server.DBTYPES.Oracle9 OrElse Server.ServerType = Server.DBTYPES.Oracle Then
            'SQL
            var = False

            sql.Append("CREATE TABLE WF_DT (WFId number(10) NOT NULL, DocTypeId number(10) NOT NULL)")
            Try
                If GenerateScripts = False Then
                    If ZPaq.ExisteTabla("WF_DT") = True Then
                        Throw New Exception(Me.name & ": La tabla WF_DT ya existe en la base de datos.")
                    End If
                    Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(sql.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                exBuilder.AppendLine(sql.ToString())
                exBuilder.Append("Error: ")
                flagException = True
                exBuilder.AppendLine(ex.ToString())
                exBuilder.AppendLine()
            End Try
            sql.Remove(0, sql.Length)
            sql.Append("CREATE TABLE WFWorkflow (")
            sql.Append("work_id number (10) NOT NULL ,")
            sql.Append("Wstat_id number(10) NOT NULL ,")
            sql.Append("Name nvarchar2 (50) NOT NULL ,")
            sql.Append("Description nvarchar2 (100) NULL ,")
            sql.Append("Help nvarchar2 (200) NULL ,")
            sql.Append("CreateDate date NOT NULL ,")
            sql.Append("EditDate date NOT NULL ,")
            sql.Append("Refreshrate number (10) NOT NULL ,")
            sql.Append("InitialStepId number (10) NOT NULL )")
            Try
                If GenerateScripts = False Then
                    If ZPaq.ExisteTabla("WFWorkflow") = True Then
                        Throw New Exception(Me.name & ": La tabla WFWorkflow ya existe en la base de datos.")
                    End If
                    Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(sql.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If
            Catch ex As Exception
                exBuilder.AppendLine(sql.ToString())
                exBuilder.Append("Error: ")
                flagException = True
                exBuilder.AppendLine(ex.ToString())
                exBuilder.AppendLine()
            End Try
            sql = Nothing
        End If

        If flagException Then
            Throw New Exception(Me.name & exBuilder.ToString())
        End If

        Return True

    End Function

    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "PAQ_CreateTableWorkflow"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_CreateTable_WorkFlow
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("17/07/06")
        End Get
    End Property

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
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
            Return 25
        End Get
    End Property
End Class
