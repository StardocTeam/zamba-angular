Imports Zamba.Servers
Imports Zamba.Core
Imports Microsoft.Win32
Public Class PAQ_CreateTable_WFStep
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        Dim strcreate As String
        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            strcreate = "CREATE TABLE WFStep (STEP_ID numeric NOT NULL,WORK_ID numeric NOT NULL, [NAME] VARCHAR(50) NOT NULL,DESCRIPTION VARCHAR(100),HELP VARCHAR(100), CREATEDATE DATEtime NOT NULL, IMAGEINDEX numeric NOT NULL, EDITDATE DATEtime NOT NULL, LOCATIONX numeric NOT NULL, LOCATIONY numeric NOT NULL, MAX_HOURS numeric NOT NULL, MAX_DOCS numeric NOT NULL, STARTATOPENDOC numeric NOT NULL,Color VARCHAR(50) NOT NULL DEFAULT '',Width numeric NOT NULL DEFAULT 150,Height numeric NOT NULL DEFAULT 50)"
        Else
            strcreate = "CREATE TABLE WFStep (STEP_ID NUMBER(10) NOT NULL, WORK_ID NUMBER(10) NOT NULL, " & Chr(34) & "NAME" & Chr(34) & " VARCHAR2(50) NOT NULL, DESCRIPTION VARCHAR2(100), HELP VARCHAR2(100), CREATEDATE DATE NOT NULL, IMAGEINDEX NUMBER(10) NOT NULL, EDITDATE DATE NOT NULL, LOCATIONX NUMBER(10) NOT NULL, LOCATIONY NUMBER(10) NOT NULL, MAX_HOURS NUMBER(10) NOT NULL, MAX_DOCS NUMBER(10) NOT NULL, STARTATOPENDOC NUMBER(10) NOT NULL,Color VARCHAR2(50) NOT NULL,Width numeric NOT NULL,Height numeric NOT NULL)"
        End If
        If GenerateScripts = False Then
            If ZPaq.ExisteTabla("WFStep") = True Then
                Throw New Exception(Me.name & " La tabla WFStep ya existe en la base de datos")
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
        Return True
    End Function

    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_CreateTableWFStep"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_CreateTable_WFStep
        End Get
    End Property
    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Crea la Tabla WFStep"
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
            Return 23
        End Get
    End Property
End Class
