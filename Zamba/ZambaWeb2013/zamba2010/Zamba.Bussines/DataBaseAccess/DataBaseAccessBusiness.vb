Imports Zamba.data
Imports System.Collections.Generic
Imports Zamba.Data.DataBaseAccessFactory

Public Class DataBaseAccessBusiness

    Public Shared Function GetQuerys() As DataSet
        Return DataBaseAccessFactory.GetQuerys
    End Function


    Private Shared Function GetTabla(ByVal IDConsulta As Int32) As String
        Return DataBaseAccessFactory.GetTabla(IDConsulta)
    End Function

    Private Shared Function GetDscolumns(ByVal IDConsulta As Int32) As DataSet
        Return DataBaseAccessFactory.getdscolumns(IDConsulta)
    End Function
    Private Shared Function GetDsClaves(ByVal IDConsulta As Int32) As DataSet
        Return DataBaseAccessFactory.getdsclaves(IDConsulta)
    End Function

    Public Shared Function MakeSelect(ByVal IDConsulta As Int32, ByVal columnasclave As ArrayList) As String
        '  Dim getcolumns As New ArrayList
        Dim sql As New System.Text.StringBuilder
        Try
            Dim Tabla As String = GetTabla(IDConsulta)
            Dim Dscolumns As DataSet = GetDscolumns(IDConsulta)
            Dim dsClaves As DataSet = GetDsClaves(IDConsulta)

            Dim i As Int32
            sql.Append("Select")
            Try
                For i = 0 To Dscolumns.Tables(0).Rows.Count - 1
                    sql.Append(Dscolumns.Tables(0).Rows(i).Item(0))
                    sql.Append(", ")
                Next
            Catch ex As Exception
                Throw New Exception("La cantidad de claves no coincide con la cantidad de columnas")
            End Try
            If sql.ToString.EndsWith(", ") Then sql.Replace(sql.ToString, sql.ToString.Substring(0, sql.Length - 2))
            sql.Append(" from ")
            sql.Append(Tabla)
            sql.Append(" Where ")
            For i = 0 To dsClaves.Tables(0).Rows.Count - 1
                sql.Append(dsClaves.Tables(0).Rows(i).Item(0))
                sql.Append("=")
                If IsNumeric(columnasclave(i)) = False Then
                    sql.Append("'")
                    sql.Append(columnasclave(i))
                    sql.Append("' and ")
                Else
                    sql.Append(columnasclave(i))
                    sql.Append(" and ")
                End If
                If sql.ToString.EndsWith(" and ") Then sql.Replace(sql.ToString, sql.ToString.Substring(0, sql.Length - 5))
            Next
            Return sql.ToString
        Finally
            sql = Nothing
        End Try
    End Function

    Public Shared Function ExecuteAndGetDs(ByVal ServerType As String, ByVal ServerName As String, ByVal DBName As String, ByVal DBUser As String, ByVal DBPassword As String, ByVal sql As String) As DataSet
        Return DataBaseAccessFactory.ExecuteAndGetDs(ServerType, ServerName, DBName, DBUser, DBPassword, sql)
    End Function
    Public Shared Function ExecuteAndGetDs(ByVal sql As String) As DataSet
        Return DataBaseAccessFactory.ExecuteAndGetDs(sql)
    End Function

    Public Shared Function ExecuteDataset(ByVal WhereData As ArrayList, ByVal SqlId As Int32, ByVal ServerType As String, ByVal ServerName As String, ByVal DBName As String, ByVal DBUser As String, ByVal DBPassword As String) As DataSet
        Dim sql As String = DataBaseAccessBusiness.MakeSelect(SqlId, WhereData)

        Dim dstemp As DataSet = DataBaseAccessBusiness.ExecuteAndGetDs(ServerType, ServerName, DBName, DBUser, DBPassword, sql)
        Return dstemp

    End Function
    Public Shared Function ExecuteDataset(ByVal WhereData As ArrayList, ByVal SqlId As Int32) As DataSet
        Dim sql As String = DataBaseAccessBusiness.MakeSelect(SqlId, WhereData)

        Dim dstemp As DataSet = DataBaseAccessBusiness.ExecuteAndGetDs(sql)
        Return dstemp

    End Function


#Region "UCWizard"

    'Se crea una clase (que luego deberá ser borrada) ya que existen
    'métodos de igual nombre en la clase DataBaseAccessBusiness
    Public Class UCWizard

        Public Shared Function MakeSelect(ByVal IDConsulta As Int32, ByVal columnasclave As ArrayList) As String


            ' Dim getcolumns As New ArrayList
            Dim sql As New System.Text.StringBuilder
            sql.Append("select SelectTable from ZQColumns where ID=" & IDConsulta)
            Try
                ' Dim server As New server
                ' server.MakeConnection()
                '  con1 = server.Con
                '  server.dispose()
                ' Dim Tabla As String = con1.ExecuteScalar(CommandType.Text, sql)
                Dim Dscolumns As DataSet = GetDscolumns(IDConsulta)

                Dim dsClaves As DataSet = GetDsClaves(IDConsulta)


                Dim i As Int32
                sql.Append("Select ")
                Try
                    For i = 0 To Dscolumns.Tables(0).Rows.Count - 1
                        sql.Append(Dscolumns.Tables(0).Rows(i).Item(0))
                        sql.Append(", ")
                    Next
                Catch ex As Exception
                    Throw New Exception("La cantidad de claves no coincide con la cantidad de columnas")
                End Try

                If sql.ToString.EndsWith(", ") Then sql.Replace(sql.ToString, sql.ToString.Substring(0, sql.Length - 2))
                sql.Append(" from ")
                sql.Append("Tabla")
                sql.Append(" Where ")
                For i = 0 To dsClaves.Tables(0).Rows.Count - 1
                    sql.Append(dsClaves.Tables(0).Rows(i).Item(0))
                    sql.Append("=")
                    If IsNumeric(columnasclave(i)) = False Then
                        sql.Append("'")
                        sql.Append(columnasclave(i))
                        sql.Append("' and ")
                    Else
                        sql.Append(columnasclave(i))
                        sql.Append(" and ")
                    End If
                    If sql.ToString.EndsWith(" and ") Then sql.Replace(sql.ToString, sql.ToString.Substring(0, sql.Length - 5))
                Next i
                Return sql.ToString
            Finally
                sql = Nothing
            End Try
        End Function
    End Class


#End Region

#Region "UCUpdate"

    Public Class UCUpdate


        Public Shared Sub LoadConsultas(ByRef claves As Hashtable, ByRef columnasDevueltas As Hashtable, ByRef tabla As String, ByVal id As Int32, ByVal mensajesError As List(Of String))

            Try
                GetClavesFromZqkeys(claves, columnasDevueltas, tabla, id)
            Catch ex As Exception
                mensajesError.Add("No se pudo obtener las claves para la consulta seleccionada")
            End Try
            Try
                GetAllFromZqcolumns(claves, columnasDevueltas, tabla, id)
            Catch ex As Exception
                mensajesError.Add("No se pudo obtener las columnas a actualizar para la consulta seleccionada")
            End Try

        End Sub


        Public Shared Sub LoadConsultas(ByRef claves As Hashtable, ByRef columnasDevueltas As Hashtable, ByRef tabla As String, ByVal id As Int32)

            Try
                GetClavesFromZqkeys(claves, columnasDevueltas, tabla, id)
            Catch ex As Exception
                'mensajesError.Add("No se pudo obtener las claves para la consulta seleccionada")
            End Try
            Try
                GetAllFromZqcolumns(claves, columnasDevueltas, tabla, id)
            Catch ex As Exception
                'mensajesError.Add("No se pudo obtener las columnas a actualizar para la consulta seleccionada")
            End Try

        End Sub


        Public Shared Sub GetClavesFromZqkeys(ByRef claves As Hashtable, ByRef columnasdevueltas As Hashtable, ByVal tabla As String, ByVal id As Int32)
            Dim i As Int32
            Dim ds As DataSet = GetDsClaves(id)
            For i = 0 To ds.Tables(0).Rows.Count - 1
                claves.Add(ds.Tables(0).Rows(i).Item(0), ds.Tables(0).Rows(i).Item(0))
            Next
        End Sub


        Public Shared Sub GetAllFromZqcolumns(ByRef claves As Hashtable, ByRef columnasdevueltas As Hashtable, ByVal tabla As String, ByVal id As Int32)

            Dim i As Int32


            Dim ds As DataSet = GetAllZQColumnsDs(id)
            tabla = CType(ds.Tables(0).Rows(0).Item(1), String)
            For i = 0 To ds.Tables(0).Rows.Count - 1
                columnasdevueltas.Add(ds.Tables(0).Rows(i).Item(2), ds.Tables(0).Rows(i).Item(2))
            Next

        End Sub


        Public Shared Function UpdateString(ByVal Where As Hashtable, ByVal valoresNuevos As Hashtable, ByRef claves As Hashtable, ByRef columnasdevueltas As Hashtable, ByRef tabla As String, ByRef id As Int32, Optional ByRef mensajeError As String = "") As String
            Dim sql As New System.Text.StringBuilder
            Try
                sql.Append("Update ")
                sql.Append(tabla)
                sql.Append(" set ")

                If id <> 0 And valoresNuevos.Count = columnasdevueltas.Count Then
                    Dim i As Int32
                    Try
                        For i = 0 To columnasdevueltas.Count - 1
                            sql.Append(columnasdevueltas(i))
                            sql.Append("=")
                            If IsNumeric(valoresNuevos(i)) Then
                                sql.Append(valoresNuevos(i))
                                sql.Append(",")
                            Else
                                sql.Append("'")
                                sql.Append(valoresNuevos(i))
                                sql.Append("',")
                            End If
                        Next
                        If sql.ToString.EndsWith(",") Then sql.Replace(sql.ToString, sql.ToString.Substring(0, sql.Length - 1))
                        sql.Append(" Where ")
                        For i = 0 To claves.Count - 1
                            sql.Append(claves(i))
                            sql.Append("=")
                            If IsNumeric(Where(i)) Then
                                sql.Append(Where(i))
                            Else
                                sql.Append("'")
                                sql.Append(Where(i))
                                sql.Append("'")

                            End If
                        Next
                    Catch ex As Exception
                        mensajeError = ex.ToString()
                    End Try
                Else
                    mensajeError = "La cantidad de parámetros no coincide con los necesarios para la consulta " & id
                End If
                Return sql.ToString
            Finally
                sql = Nothing
            End Try
        End Function


        Public Shared Function UpdateString(ByVal Where As ArrayList, ByVal valoresNuevos As ArrayList, ByRef claves As Hashtable, ByRef columnasdevueltas As Hashtable, ByRef tabla As String, ByRef id As Int32, Optional ByRef mensajeError As String = "") As String
            Dim sql As New System.Text.StringBuilder
            Try
                sql.Append("Update ")
                sql.Append(tabla)
                sql.Append(" set ")
                If id <> 0 And valoresNuevos.Count = columnasdevueltas.Count Then
                    Dim i As Int32
                    Try
                        For i = 0 To columnasdevueltas.Count - 1
                            sql.Append(columnasdevueltas(i).ToString)
                            sql.Append("=")
                            If IsNumeric(valoresNuevos(i)) Then
                                sql.Append(valoresNuevos(i).ToString)
                                sql.Append(",")
                            Else
                                sql.Append("'")
                                sql.Append(valoresNuevos(i).ToString)
                                sql.Append("',")
                            End If
                        Next
                        'revisar esta linea, porque se cambio con el stringbuilder
                        If sql.ToString.EndsWith(",") Then sql.ToString.TrimEnd(Char.Parse(",")) ' = sql.ToString.Substring(0, sql.ToString.Length - 1)
                        sql.Append(" Where ")
                        For i = 0 To claves.Count - 1
                            sql.Append(claves(i).ToString())
                            sql.Append("=")
                            If IsNumeric(Where(i)) Then
                                sql.Append(Where(i))
                            Else
                                sql.Append("'")
                                sql.Append(Where(i).ToString())
                                sql.Append("'")
                            End If
                        Next
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                Else
                    MessageBox.Show("La cantidad de parámetros no coincide con los necesarios para la consulta " & id, "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                Return sql.ToString
            Finally
                sql = Nothing
            End Try
        End Function


    End Class

#End Region

End Class
