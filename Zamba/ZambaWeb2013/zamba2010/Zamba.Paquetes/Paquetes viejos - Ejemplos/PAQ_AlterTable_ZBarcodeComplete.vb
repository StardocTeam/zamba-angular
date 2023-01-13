Imports zamba.servers
Imports Zamba.Core
Imports Zamba.AppBlock
Imports Microsoft.Win32
' CREADO : 24/11/2006 - 

Public Class PAQ_AlterTable_ZBarcodeComplete

    Inherits ZPaq
    Implements IPAQ

    Public Overrides Sub Dispose()

    End Sub


#Region "Propiedades"
    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_AlterTable_ZBarcodeComplete"
        End Get
    End Property
    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_AlterTable_ZBarcodeComplete
        End Get
    End Property
    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return True
        End Get
    End Property
    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("24/11/2006")
        End Get
    End Property
    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
        End Get
    End Property
    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Agrega columnas nuevas y cambia el tipo de datos de las PK a numeric(18) "
        End Get
    End Property
    Public Overrides ReadOnly Property EditDate() As Date
        Get
            Return Date.Parse("24/11/2006")
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
            Return 58
        End Get
    End Property

#End Region
    
#Region "Ejecución"

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        Dim Sql As New System.Text.StringBuilder
        Try

            If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
                'Sql.Append("/*SACO las PK  */")
                'Sql.Append(ControlChars.NewLine)
                Sql.Append("ALTER TABLE ZBarcodeComplete  DROP CONSTRAINT PK_ZBarcodeComplete")
                Sql.Append(ControlChars.NewLine)
                'Sql.Append("/*Cambio el tipo de datos */")
                'Sql.Append(ControlChars.NewLine)
                Sql.Append("ALTER TABLE ZBarcodeComplete ALTER COLUMN  DocTypeId numeric(18,0) NOT NULL")
                Sql.Append(ControlChars.NewLine)
                Sql.Append("ALTER TABLE ZBarcodeComplete ALTER COLUMN  IndexId numeric(18,0) NOT NULL")
                Sql.Append(ControlChars.NewLine)
                'Sql.Append("/*AGREGO NUEVAMENTE LAS PK */")
                'Sql.Append(ControlChars.NewLine)
                Sql.Append("ALTER TABLE ZBarcodeComplete  ADD CONSTRAINT PK_ZBarcodeComplete PRIMARY KEY(DocTypeId,IndexId) ")
                Sql.Append(ControlChars.NewLine)
                'Sql.Append("/*AGREGO LOS NUEVOS CAMPOS RESTANTES */")
                'Sql.Append(ControlChars.NewLine)
                Sql.Append("ALTER TABLE ZBarcodeComplete ADD  Tabla varchar(50) COLLATE Modern_Spanish_CI_AS NULL")
                Sql.Append(ControlChars.NewLine)
                Sql.Append("ALTER TABLE ZBarcodeComplete ADD Columna varchar(50) COLLATE Modern_Spanish_CI_AS NULL")
                Sql.Append(ControlChars.NewLine)
                Sql.Append("ALTER TABLE ZBarcodeComplete ADD Clave numeric(18, 0) NULL")
                Sql.Append(ControlChars.NewLine)
                Sql.Append("ALTER TABLE ZBarcodeComplete ADD Orden numeric(18, 0) NULL")
                Sql.Append(ControlChars.NewLine)
                Sql.Append("ALTER TABLE ZBarcodeComplete ADD [Conditions] [varchar] (255) DEFAULT '=' NOT NULL ")


            End If
            If GenerateScripts = False Then
                If ZPaq.ExisteTabla("ZBarcodeComplete") Then
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
            Return True
            strSql = Nothing
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

#End Region





End Class
