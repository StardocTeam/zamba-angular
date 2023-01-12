Imports Zamba.Servers
Imports Zamba.AppBlock

Public Class PAQ_AlterTable_ZbarCode
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub

#Region "Ejecución"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute
        Dim sql As New System.Text.StringBuilder
        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            sql.Append("ALTER TABLE ZBarCode ALTER COLUMN ID numeric(18, 0) NOT NULL ")
            sql.Append(ControlChars.NewLine)
            sql.Append("ALTER TABLE ZBarCode ALTER COLUMN FECHA datetime NOT NULL")
            sql.Append(ControlChars.NewLine)
            sql.Append("ALTER TABLE ZBarCode ALTER COLUMN DOC_TYPE_ID numeric(18, 0) NOT NULL ")
            sql.Append(ControlChars.NewLine)
            sql.Append("ALTER TABLE ZBarCode ALTER COLUMN USERID numeric(18, 0) NOT NULL ")
            sql.Append(ControlChars.NewLine)
            sql.Append("ALTER TABLE ZBarCode ALTER COLUMN SCANNED varchar (2) NULL ")
            sql.Append(ControlChars.NewLine)
            sql.Append("ALTER TABLE ZBarCode ALTER COLUMN SCANNEDDATE datetime NULL ")
            sql.Append(ControlChars.NewLine)
            sql.Append("ALTER TABLE ZBarCode ADD DOC_ID numeric(18, 0) NULL ")
            sql.Append(ControlChars.NewLine)
            sql.Append("ALTER TABLE ZBarCode ADD BATCH varchar (10) NULL ")
            sql.Append(ControlChars.NewLine)
            sql.Append("ALTER TABLE ZBarCode ADD BOX numeric(18, 0) NULL ")

        End If

        Try
            If GenerateScripts = False Then
                If ZPaq.ExisteTabla("ZBarCode") Then
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
            'Dim exn As New Exception("ERROR | " & ex.ToString & " en " & strcreate)
            'ZException.Log(exn, False)
            'MessageBox.Show(exn.Message)
        End Try
        Return True
    End Function

#End Region

#Region "Propiedades"
    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "PAQ_AlterTableZbarCode"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_AlterTable_ZbarCode
        End Get
    End Property

    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Modifica la definicion de las columnas existentes,además agrega nuevas"
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("04/12/2006")
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
            Return 57
        End Get
    End Property
#End Region

End Class
