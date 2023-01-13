Imports Zamba.Servers
Imports Zamba.Core
Imports Microsoft.Win32

Public Class PAQ_AlterTable_WFStep
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub

#Region "Propiedades"
    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_AlterTablaWFStep"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_AlterTable_WFStep
        End Get
    End Property

    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Modifica la Tabla WFStep agregandole las columnas Color,Width y Height."
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("25/10/2006")
        End Get
    End Property

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
        End Get
    End Property

    Public Overrides ReadOnly Property EditDate() As Date
        Get
            Return Date.Parse("25/10/2006")
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
            Return 55
        End Get
    End Property
#End Region

#Region "Ejecución"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        Dim strcreate As String
        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            strcreate = "ALTER TABLE WFStep ADD Color VARCHAR(50) NOT NULL DEFAULT '',  " & _
                        "Width numeric NOT NULL DEFAULT 150, " & _
                        "Height numeric NOT NULL DEFAULT 50"
        Else
            strcreate = "ALTER TABLE WFStep ADD (Color VARCHAR2(50) NOT NULL, Width NUMBER(10) NOT NULL, Height NUMBER(10) NOT NULL)"
        End If
        If GenerateScripts = False Then
            If ZPaq.ExisteTabla("WFStep") Then
                Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
            End If
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

#End Region


End Class
