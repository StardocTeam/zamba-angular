Imports Zamba.Servers
Imports Zamba.Core
Public Class PAQ_AddColumns_DOC_T
    Inherits ZPaq
    Implements IPAQ

    Public Overrides Sub Dispose()

    End Sub

#Region "Propiedades"

    Public ReadOnly Property name() As String Implements IPAQ.Name
        Get
            Return "PAQ_AddColumns_DOC_T"
        End Get
    End Property

    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.Number
        Get
            Return EnumPaquetes.PAQ_AddColumns_DOC_T
        End Get
    End Property

    Public ReadOnly Property description() As String Implements IPAQ.Description
        Get
            Return "Verifica y agrega las columnas de la tabla DOC_T que esten faltando para la Version 2.0.0"
        End Get
    End Property

    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("26/08/2005")
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
            Return 39
        End Get
    End Property

#End Region

#Region "Ejecución"
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.Execute
        'TODO store: SPGetDoc_type_id 
        'TODO store: SPGetTop1FromDoc_t
        Dim dsIds As DataSet
        Dim dsDesc As DataSet
        Dim strSelect As String
        Dim strAlter As String
        Dim cols As Columns
        Dim doc_t As String
        Dim tablename As String

        'traigo ids de doc_type
        strSelect = "select doc_type_id from doc_type"
        dsIds = Server.Con.ExecuteDataset(CommandType.Text, strSelect)

        'Recuento de Columnas de cada doc_t
        For i As Integer = 0 To dsIds.Tables(0).Rows.Count - 1


            doc_t = dsIds.Tables(0).Rows(i).Item(0).ToString()
            tablename = "DOC_T" & doc_t
            If Server.ServerType = Server.DBTYPES.Oracle9 Or Server.ServerType = Server.DBTYPES.Oracle Or Server.ServerType = Server.DBTYPES.OracleClient Then
                strSelect = "select * from doc_t" & doc_t & " where rownum=1"
            Else
                strSelect = "select TOP 1 * from doc_t" & doc_t
            End If

            dsDesc = Server.Con.ExecuteDataset(CommandType.Text, strSelect)
            'Verifico Columnas  y las agrego
            If dsDesc.Tables(0).Columns.Count < 17 Then
                cols = New Columns
                For j As Int32 = 0 To dsDesc.Tables(0).Columns.Count - 1
                    Select Case dsDesc.Tables(0).Columns(j).ColumnName.ToUpper
                        Case "SHARED"
                            cols._shared = True
                            Exit Select
                        Case "ORIGINAL_FILENAME"
                            cols.original_filename = True
                            Exit Select
                        Case "VER_PARENT_ID"
                            cols.ver_parent_id = True
                            Exit Select
                        Case "VERSION"
                            cols.version = True
                            Exit Select
                        Case "ROOTID"
                            cols.rootid = True
                            Exit Select
                        Case "NUMEROVERSION"
                            cols.numero_version = True
                            Exit Select
                        Case "ISVIRTUAL"
                            cols.IsVirtual = True
                            Exit Select
                    End Select
                Next
                If cols._shared = False Then
                    If ZPaq.ExisteColumna("SHARED", tablename) = False Then
                        If Server.ServerType = Server.DBTYPES.Oracle9 Or Server.ServerType = Server.DBTYPES.Oracle Or Server.ServerType = Server.DBTYPES.OracleClient Then
                            strAlter = "ALTER TABLE DOC_T" & doc_t & " ADD (SHARED NUMBER(2))"
                        Else
                            strAlter = "ALTER TABLE DOC_T" & doc_t & " ADD SHARED NUMERIC (2)"
                        End If
                        If ZPaq.ExisteTabla("DOC_T" & doc_t) Then
                            Server.Con.ExecuteNonQuery(CommandType.Text, strAlter)
                        End If
                    End If
                End If
                If cols.original_filename = False Then
                    If ZPaq.ExisteColumna("ORIGINAL_FILENAME", tablename) = False Then
                        If Server.ServerType = Server.DBTYPES.Oracle9 Or Server.ServerType = Server.DBTYPES.Oracle Or Server.ServerType = Server.DBTYPES.OracleClient Then
                            strAlter = "ALTER TABLE DOC_T" & doc_t & " ADD (ORIGINAL_FILENAME VARCHAR2(255))"
                        Else
                            strAlter = "ALTER TABLE DOC_T" & doc_t & " ADD ORIGINAL_FILENAME NVARCHAR2 (255)"
                        End If
                        If ZPaq.ExisteTabla("DOC_T" & doc_t) Then
                            Server.Con.ExecuteNonQuery(CommandType.Text, strAlter)
                        End If
                    End If
                End If
                If cols.ver_parent_id = False Then
                    If ZPaq.ExisteColumna("VER_PARENT_ID", tablename) = False Then
                        If Server.ServerType = Server.DBTYPES.Oracle9 Or Server.ServerType = Server.DBTYPES.Oracle Or Server.ServerType = Server.DBTYPES.OracleClient Then
                            strAlter = "ALTER TABLE DOC_T" & doc_t & " ADD (VER_PARENT_ID NUMBER(10))"
                        Else
                            strAlter = "ALTER TABLE DOC_T" & doc_t & " ADD VER_PARENT_ID NUMERIC (10)"
                        End If
                        If ZPaq.ExisteTabla("DOC_T" & doc_t) Then
                            Server.Con.ExecuteNonQuery(CommandType.Text, strAlter)
                        End If
                    End If
                End If
                If cols.version = False Then
                    If ZPaq.ExisteColumna("VERSION", tablename) = False Then
                        If Server.ServerType = Server.DBTYPES.Oracle9 Or Server.ServerType = Server.DBTYPES.Oracle Or Server.ServerType = Server.DBTYPES.OracleClient Then
                            strAlter = "ALTER TABLE DOC_T" & doc_t & " ADD (VERSION NUMBER(2))"
                        Else
                            strAlter = "ALTER TABLE DOC_T" & doc_t & " ADD VERSION NUMERIC (2)"
                        End If
                        If ZPaq.ExisteTabla("DOC_T" & doc_t) Then
                            Server.Con.ExecuteNonQuery(CommandType.Text, strAlter)
                        End If
                    End If
                End If
                If cols.rootid = False Then
                    If Server.ServerType = Server.DBTYPES.Oracle9 OrElse Server.ServerType = Server.DBTYPES.Oracle OrElse Server.ServerType = Server.DBTYPES.OracleClient Then
                        strAlter = "ALTER TABLE DOC_T" & doc_t & " ADD (ROOTID NUMBER(10))"
                    Else
                        strAlter = "ALTER TABLE DOC_T" & doc_t & " ADD ROOTID NUMERIC (10)"
                    End If
                    If ZPaq.ExisteTabla("DOC_T" & doc_t) Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, strAlter)
                    End If
                End If
                If cols.numero_version = False Then
                    If Server.ServerType = Server.DBTYPES.Oracle9 OrElse Server.ServerType = Server.DBTYPES.Oracle OrElse Server.ServerType = Server.DBTYPES.OracleClient Then
                        strAlter = "ALTER TABLE DOC_T" & doc_t & " ADD (NUMEROVERSION NUMBER(2))"
                    Else
                        strAlter = "ALTER TABLE DOC_T" & doc_t & " ADD NUMEROVERSION NUMERIC (2)"
                    End If
                    If ZPaq.ExisteTabla("DOC_T" & doc_t) Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, strAlter)
                    End If
                End If
                If cols.IsVirtual = False Then
                    If Server.ServerType = Server.DBTYPES.Oracle9 OrElse Server.ServerType = Server.DBTYPES.Oracle OrElse Server.ServerType = Server.DBTYPES.OracleClient Then
                        strAlter = "ALTER TABLE DOC_T" & doc_t & " ADD (ISVIRTUAL NUMBER(1))"
                    Else
                        strAlter = "ALTER TABLE DOC_T" & doc_t & " ADD ISVIRTUAL NUMERIC (1)"
                    End If
                    If ZPaq.ExisteTabla("DOC_T" & doc_t) Then
                        Server.Con.ExecuteNonQuery(CommandType.Text, strAlter)
                    End If
                End If
            End If
        Next
        Return True
    End Function

    Private Class Columns
        Public _shared, original_filename, numero_version, rootid, version, ver_parent_id, IsVirtual As Boolean
    End Class
#End Region

End Class
