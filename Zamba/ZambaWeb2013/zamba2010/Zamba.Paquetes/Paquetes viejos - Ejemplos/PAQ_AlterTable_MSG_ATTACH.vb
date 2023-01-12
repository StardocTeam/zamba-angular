Imports Zamba.Servers
Imports Zamba.Core
Imports Microsoft.Win32

Public Class PAQ_AlterTable_MSG_ATTACH
    Inherits ZPaq
    Implements IPAQ

    Public Overrides Sub Dispose()

    End Sub

#Region "Propiedades"
    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_AlterTable_MSG_ATTACH"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_AlterTable_MSG_ATTACH
        End Get
    End Property

    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Modifica las columnas DOC_FILE y NAME a VARCHAR(255)."
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("13/09/2006")
        End Get
    End Property
    Public Overrides ReadOnly Property EditDate() As Date
        Get
            Return Date.Parse("22/09/2006")
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
            Return 48
        End Get
    End Property

#End Region

#Region "Ejecución"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        Dim strcreate As String

        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            strcreate = "ALTER TABLE MSG_ATTACH ALTER COLUMN DOC_FILE VARCHAR(255) NULL " & _
                        "ALTER TABLE MSG_ATTACH ALTER COLUMN NAME VARCHAR(255) NULL"
        Else
            strcreate = "ALTER TABLE MSG_ATTACH MODIFY ( DOC_FILE VARCHAR2(255),NAME VARCHAR2(255))"
        End If
        If GenerateScripts = False Then
            If ZPaq.ExisteTabla("MSG_ATTACH") AndAlso ExisteColumna("DOC_FILE", "MSG_ATTACH") Then
                Server.Con.ExecuteNonQuery(CommandType.Text, strcreate.ToString)
            Else
                MessageBox.Show("La columna DOC_FILE o NAME no existe en la tabla MSG_ATTACH", "Zamba Paquetes", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
