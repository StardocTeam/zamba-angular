Imports Zamba.Servers
Imports Zamba.Core
Imports Microsoft.Win32

Public Class PAQ_CreateTable_PreProcessIDs
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub
    
#Region "Propiedades"

    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_CreateTablePreProcessIDs"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_CreateTable_PreProcessIDs
        End Get
    End Property
    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Crea la Tabla PreProcessIDs"
        End Get
    End Property
    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return True
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("12/02/2007")
        End Get
    End Property

    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
        End Get
    End Property

    Public Overrides ReadOnly Property EditDate() As Date
        Get
            Return Date.Parse("12/02/2007")
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
            Return 3
        End Get
    End Property
#End Region

#Region "Ejecución"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        Try

            Dim sql As New System.Text.StringBuilder

            If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then

                sql.Append("CREATE TABLE [PreProcessIDs](")
                sql.Append(ControlChars.NewLine)
                sql.Append("[PreprocessName] [nvarchar](50) NOT NULL,")
                sql.Append(ControlChars.NewLine)
                sql.Append("[PreprocessID] [numeric](18, 0) NOT NULL,")
                sql.Append(ControlChars.NewLine)
                sql.Append("CONSTRAINT [PK_PreProcessIDs] PRIMARY KEY CLUSTERED ([PreprocessName] ASC))")

            Else
                sql.Append("CREATE TABLE PreProcessIDs (")
                sql.Append("PreprocessName varchar2(250) NOT NULL,")
                sql.Append("PreprocessID number(18,0) NOT NULL,")
                sql.Append("CONSTRAINT PK_PreProcessIDs PRIMARY KEY (PreprocessName))")

            End If
            If GenerateScripts = False Then
                If ZPaq.ExisteTabla("PreProcessIDs") = True Then
                    Throw New Exception(Me.name & ": La tabla PreProcessIDs ya existe en la base de datos.")
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
            Return True
        Catch ex As Exception
            Throw New Exception(Me.name & ": error en la creación de la tabla 'PreProcessIDs' (" & ex.ToString() & ")")
        End Try
    End Function
#End Region
End Class
