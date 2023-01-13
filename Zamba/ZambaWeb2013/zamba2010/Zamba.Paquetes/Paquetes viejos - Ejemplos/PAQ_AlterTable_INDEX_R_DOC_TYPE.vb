Imports Zamba.Servers
Imports Zamba.Core
Imports Microsoft.Win32
Public Class PAQ_AlterTable_INDEX_R_DOC_TYPE
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub

#Region "Propiedades"
    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_AlterTable_INDEX_R_DOC_TYPE"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_AlterTable_INDEX_R_DOC_TYPE
        End Get
    End Property

    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Agrega la columna IndexSearch a la tabla INDEX_R_DOC_TYPE con las siguientes definiciones: char(10) default '0'."
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("04/05/2007")
        End Get
    End Property

    Public Overrides ReadOnly Property EditDate() As Date
        Get
            Return Date.Parse("04/05/2007")
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
            Return 45
        End Get
    End Property

#End Region

#Region "Ejecución"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        Dim strSql As New System.Text.StringBuilder
        Dim strSql2 As New System.Text.StringBuilder
        Dim sqlOracle As String = ""
        Dim banderaArgentina As Boolean = True
        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            strSql.Append("if not exists (select * from information_schema.columns where table_name='index_r_doc_type' and column_name='IndexSearch')")
            strSql.Append(ControlChars.NewLine)
            strSql.Append("	alter table index_r_doc_type add IndexSearch  char(10)  null constraint DF_IndexSearch default 0;")
            strSql2.Append("update index_r_doc_type set IndexSearch='0' where IndexSearch is null; ")
        Else
            banderaArgentina = False
            sqlOracle = "ALTER TABLE INDEX_R_DOC_TYPE ADD IndexSearch CHAR(10) DEFAULT 0 NOT NULL"

        End If

        If GenerateScripts = False Then
            If ExisteColumna("IndexSearch", "INDEX_R_DOC_TYPE") = False Then

                If banderaArgentina = False Then
                    If ZPaq.ExisteTabla("index_r_doc_type") Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, sqlOracle.ToString)
                    End If
                Else
                    If ZPaq.ExisteTabla("index_r_doc_type") Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, strSql.ToString)
                        Server.Con.ExecuteNonQuery(CommandType.Text, strSql2.ToString)
                    End If
                End If
            Else
                MessageBox.Show("La columna ya existe ", "Zamba Paquetes", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Else

            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
            sw.WriteLine("")
            sw.WriteLine(strSql.ToString)
            sw.WriteLine("")
            sw.Close()
            sw = Nothing
        End If
        Return True
        strSql = Nothing
    End Function
#End Region

End Class
