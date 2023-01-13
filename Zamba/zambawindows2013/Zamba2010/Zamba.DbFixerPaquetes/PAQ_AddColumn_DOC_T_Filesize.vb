'ver
Imports Zamba.Servers
Public Class PAQ_AddColumn_DOC_T_Filesize
    Inherits ZPaq
    Implements IPAQ
    Public Overrides Sub Dispose()

    End Sub

#Region "Propiedades"

    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "PAQ_AddColumn_DOC_T_Filesize"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_AddColumn_DOCT_Filesize
        End Get
    End Property

    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Agrega la columna FileSize a cada tabla DOC_T"
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("01/01/01")
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
            Return 32
        End Get
    End Property
#End Region

#Region "Ejecución"

    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute
        'TODO store: SPGetDoc_type_id()

        Dim dsdoctypes As DataSet = Server.Con.ExecuteDataset(CommandType.Text, "Select DOC_TYPE_ID from Doc_Type order by doc_type_ID")
        Dim i As Int16
        Dim sql As String
        For i = 0 To Convert.ToInt16(dsdoctypes.Tables(0).Rows.Count - 1)
            Try
                If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
                    sql = "Alter Table Doc_T" & dsdoctypes.Tables(0).Rows(i).Item(0).ToString() & " Add FileSize decimal(18,3) Null Default(0)"
                Else
                    sql = "ALTER TABLE DOC_T" & dsdoctypes.Tables(0).Rows(i).Item(0).ToString() & " ADD (FILESIZE NUMBER(10,4))"
                End If
                If GenerateScripts = False Then
                    If ZPaq.ExisteTabla("DOC_T" & dsdoctypes.Tables(0).Rows(i).Item(0).ToString()) Then
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
                Try
                    sql = "Update DOC_T" & dsdoctypes.Tables(0).Rows(i).Item(0).ToString() & " set FileSize=0"
                    If GenerateScripts = False Then
                        If ZPaq.ExisteTabla("DOC_T" & dsdoctypes.Tables(0).Rows(i).Item(0).ToString()) Then
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
                Catch
                End Try
            Catch
            End Try
        Next
        Return True
    End Function

#End Region

End Class
