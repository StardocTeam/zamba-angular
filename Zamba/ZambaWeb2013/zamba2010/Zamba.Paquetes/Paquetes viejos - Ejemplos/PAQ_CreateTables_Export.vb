Imports Zamba.Servers

Public Class PAQ_CreateTables_Export
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub
    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("22/02/2005")
        End Get
    End Property
    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
        End Get
    End Property
    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Crea la tabla ZExportControl y ZExportErrors para el control de la exportación e importación de mails a Lotus Notes"
        End Get
    End Property
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        Try
            Zpaq.IfExists("ZExportControl", ZPaq.Tipo.Table, Me.CanDrop)
            If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
                CreateSQL(GenerateScripts)
            Else
                CreateOracle(GenerateScripts)
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
        Try
            Zpaq.IfExists("ZExportErrors", ZPaq.Tipo.Table, Me.CanDrop)
            If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
                CreateErrorsSQL(GenerateScripts)
            Else
                CreateErrorsOracle(GenerateScripts)
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
        Return True
    End Function
    Private Sub CreateSQL(ByVal Generatescripts As Boolean)
        Dim sql As String = "Create Table ZExportControl(Fecha smalldatetime,UserId nvarchar(30),Line nvarchar(1000),Insertado char(1),Codigo nvarchar(50))"
        If Generatescripts = False Then
            If Not ZPaq.ExisteTabla("ZExportControl") Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Throw New Exception(Me.name & ": La tabla ZExportControl ya existe en la base de datos.")
            End If
        Else
            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
            sw.WriteLine("")
            sw.WriteLine(sql.ToString)
            sw.WriteLine("")
            sw.Close()
            sw = Nothing
        End If
    End Sub
    Private Sub CreateOracle(ByVal generatescripts As Boolean)
        Dim sql As String = "Create Table ZExportControl(Fecha date,UserId nvarchar2(30),Line nvarchar2(1000),Insertado char(1),Codigo nvarchar2(50))"
        If generatescripts = False Then
            If Not ZPaq.ExisteTabla("ZExportControl") Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Throw New Exception(Me.name & ": La tabla ZExportControl ya existe en la base de datos.")
            End If
        Else
            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
            sw.WriteLine("")
            sw.WriteLine(sql.ToString)
            sw.WriteLine("")
            sw.Close()
            sw = Nothing
        End If
    End Sub

    Private Sub CreateErrorsSQL(ByVal gen As Boolean)
        Dim sql As String = "Create Table ZExportErrors(Fecha smalldatetime,WinUser nvarchar(15),MachineName nvarchar(30),Line nvarchar(1000),ErrorType nvarchar(100),Codigo nvarchar(40))"
        If gen = False Then
            If Not ZPaq.ExisteTabla("ZExportErrors") Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Throw New Exception(Me.name & ": La tabla ZExportControl ya existe en la base de datos.")
            End If
        Else
            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
            sw.WriteLine("")
            sw.WriteLine(sql.ToString)
            sw.WriteLine("")
            sw.Close()
            sw = Nothing
        End If
    End Sub
    Private Sub CreateErrorsOracle(ByVal generatescripts As Boolean)
        Dim sql As String = "Create Table ZExportErrors(Fecha Date,WinUser nvarchar2(15),MachineName nvarchar2(30),Line nvarchar2(1000),ErrorType nvarchar2(100),Codigo nvarchar2(40))"
        If generatescripts = False Then
            If Not ZPaq.ExisteTabla("ZExportErrors") Then
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Else
                Throw New Exception(Me.name & ": La tabla ZExportControl ya existe en la base de datos.")
            End If
        Else
            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
            sw.WriteLine("")
            sw.WriteLine(sql.ToString)
            sw.WriteLine("")
            sw.Close()
            sw = Nothing
        End If
    End Sub

    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_CreateTables_Export"
        End Get
    End Property
    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_CreateTables_Export
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
            Return 19
        End Get
    End Property
End Class
