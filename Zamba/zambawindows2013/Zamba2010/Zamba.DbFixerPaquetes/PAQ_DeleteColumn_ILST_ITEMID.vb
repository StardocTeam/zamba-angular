Imports zamba.servers
Imports Zamba.Core
Imports Zamba.AppBlock
Imports Microsoft.Win32

Public Class PAQ_DeleteColumn_ILST_ITEMID
    Inherits ZPaq
    Implements IPAQ
#Region "Propiedades"
    Public ReadOnly Property name() As String Implements IPAQ.name
        Get
            Return "PAQ_DeleteColumn_ILST_ITEMID"
        End Get
    End Property
    Public ReadOnly Property number() As EnumPaquetes Implements IPAQ.number
        Get
            Return EnumPaquetes.PAQ_DeleteColumn_ILST_ITEMID
        End Get
    End Property
    Public Overrides ReadOnly Property CanDrop() As Boolean
        Get
            Return True
        End Get
    End Property
    Public Overrides ReadOnly Property CreateDate() As Date
        Get
            Return Date.Parse("11/12/2006")
        End Get
    End Property
    Public Overrides ReadOnly Property ZVersion() As String
        Get
            Return "2.0.0"
        End Get
    End Property
    Public ReadOnly Property description() As String Implements IPAQ.description
        Get
            Return "Elimina la columna ITEMID de las tablas ILST_I###"
        End Get
    End Property
    Public Overrides ReadOnly Property EditDate() As Date
        Get
            Return Date.Parse("11/12/2006")
        End Get
    End Property
#End Region
    Public Overrides Sub Dispose()

    End Sub
    Public Function execute(Optional ByVal GenerateScripts As Boolean = False) As Boolean Implements IPAQ.execute
        Try
            Dim sql As New System.Text.StringBuilder
            Dim j As Int32
            Dim tabla As DataTable = GetIDS()
            If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then

                'Dim tabla As DataTable = GetIDS()
                If GenerateScripts = False Then
                    For j = 1 To tabla.Rows.Count - 1

                        sql.Append("IF EXISTS(SELECT name FROM sysobjects WHERE name like 'PK_ILST_I" & tabla.Rows(j).Item(0).ToString() & "')")
                        sql.Append(ControlChars.NewLine)
                        sql.Append("	BEGIN")
                        sql.Append(ControlChars.NewLine)
                        sql.Append("	        ALTER TABLE ILST_I" & tabla.Rows(j).Item(0).ToString() & "  DROP CONSTRAINT PK_ILST_I" & tabla.Rows(j).Item(0).ToString())
                        sql.Append(ControlChars.NewLine)
                        sql.Append("		    ALTER TABLE ILST_I" & tabla.Rows(j).Item(0).ToString() & " DROP COLUMN ITEMID")
                        sql.Append(ControlChars.NewLine)
                        sql.Append("	END")
                        sql.Append(ControlChars.NewLine)
                        sql.Append(ControlChars.NewLine)

                        Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)

                    Next
                Else
                    Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                    sw.WriteLine("")
                    sw.WriteLine(sql.ToString)
                    sw.WriteLine("")
                    sw.Close()
                    sw = Nothing
                End If

                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                sql.Remove(0, sql.Length)

            Else
                'ORACLE
                'todo: (marcos), POR CADA TABLA ilst_i que no existe genera excep. falta corregirlo
                For j = 1 To tabla.Rows.Count - 1

                    Try
                        sql.Append(" ALTER TABLE " & Chr(34) & "ILST_I" & tabla.Rows(j).Item(0).ToString() & Chr(34) & " DROP COLUMN " & Chr(34) & "ITEMID" & Chr(34))
                        'If generatescipts = False Then

                        '    Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)

                        'End If
                        If GenerateScripts = False Then
                            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
                        Else
                            Dim sw As New IO.StreamWriter(Application.StartupPath & "\scripts.txt", True)
                            sw.WriteLine("")
                            sw.WriteLine(sql.ToString)
                            sw.WriteLine("")
                            sw.Close()
                            sw = Nothing
                        End If
                    Catch ex As Exception
                        Zamba.Core.ZClass.raiseerror(ex)
                    End Try
                    sql.Remove(0, sql.Length)
                Next

            End If


        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Return True
    End Function
    Private Function GetIDS() As DataTable
        'TODO store: SPGetDoc_type_id
        Try
            Dim sql As String = "select index_id from doc_index order by index_id"
            Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)
            sql = Nothing
            If Not IsNothing(ds) Then
                Return ds.Tables(0)
            Else
                Return Nothing
            End If
        Catch
            Return Nothing
        End Try
    End Function

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
            Return 42
        End Get
    End Property
End Class
