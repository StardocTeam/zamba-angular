Imports Zamba.Servers
Imports Zamba.Core
Imports Microsoft.Win32

Public Class PAQ_CreateTable_WFStepHst
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub

#Region "Propiedades"
    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_CreateTablaWFStepHst"
        End Get
    End Property
    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_CreateTable_WFStepHst
        End Get
    End Property
    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Crea la Tabla WFStepHst haciendo un drop y un create nuevamente"
        End Get
    End Property
    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return True
        End Get
    End Property
    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("18/01/2007")
        End Get
    End Property
    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
        End Get
    End Property
    Public Overrides ReadOnly Property EditDate() As Date
        Get
            Return Date.Parse("18/01/2007")
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
            Return 24
        End Get
    End Property
#End Region

#Region "Ejecución"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute

        Dim sql As New System.Text.StringBuilder

        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            Try
                sql.Remove(0, sql.Length)
                sql.Append("DROP TABLE WFSTEPHST")
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Catch e As Exception
                Zamba.Core.ZClass.raiseerror(e)
            End Try

            sql.Remove(0, sql.Length)
            sql.Append("CREATE TABLE [WFStepHst] (")
            sql.Append(ControlChars.NewLine)
            sql.Append("Doc_Id [numeric](18, 0) NOT NULL ,")
            sql.Append(ControlChars.NewLine)
            sql.Append("FOLDER_Id [numeric](18, 0) NOT NULL ,")
            sql.Append(ControlChars.NewLine)
            sql.Append("Step_Name [nvarchar] (50) COLLATE Modern_Spanish_CI_AS NOT NULL ,")
            sql.Append(ControlChars.NewLine)
            sql.Append("State [nvarchar] (50) COLLATE Modern_Spanish_CI_AS NOT NULL ,")
            sql.Append(ControlChars.NewLine)
            sql.Append("UserName [nvarchar] (50) COLLATE Modern_Spanish_CI_AS NOT NULL ,")
            sql.Append(ControlChars.NewLine)
            sql.Append("Accion [nvarchar] (250) COLLATE Modern_Spanish_CI_AS NOT NULL ,")
            sql.Append(ControlChars.NewLine)
            sql.Append("Fecha [datetime] NOT NULL ,")
            sql.Append(ControlChars.NewLine)
            sql.Append("Doc_Name [nvarchar] (250) COLLATE Modern_Spanish_CI_AS NOT NULL ,")
            sql.Append(ControlChars.NewLine)
            sql.Append("Doc_Type_Name [nvarchar] (50) COLLATE Modern_Spanish_CI_AS NOT NULL ")
            sql.Append(ControlChars.NewLine)
            sql.Append(")")


        Else
            sql.Append("CREATE TABLE ZAMBA.WFSTEPHST (DOC_ID NUMBER(9) NOT NULL,")
            sql.Append(" FOLDER_ID NUMBER(9) NOT NULL, STEP_NAME NVARCHAR2(50) NOT")
            sql.Append(" NULL, STATE NVARCHAR2(50) NOT NULL, USERNAME")
            sql.Append(" NVARCHAR2(50) NOT NULL, ACTION NVARCHAR2(250) NOT NULL,")
            sql.Append(" FECHA DATE NOT NULL, DOC_NAME NVARCHAR2(250) NOT NULL,")
            sql.Append(" DOC_TYPE_NAME NVARCHAR2(50) NOT NULL)")
        End If
        If GenerateScripts = False Then
            If ZPaq.ExisteTabla("WFStepHst") = True Then
                Throw New Exception(Me.name & " La tabla WFStepHst ya existe en la base de datos")
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
    End Function
#End Region

End Class
