Imports System.Text
Imports Zamba.Core

''' <summary>
''' Esta clase se encarga de ejecutar consultas respecto a la base de datos
''' </summary>
''' <remarks></remarks>
Public Class DBFactory
    ''' <summary>
    ''' Return the columns names
    ''' </summary>
    ''' <param name="name">Nombre de la tabla</param>
    ''' <returns></returns>
    ''' <history>Marcelo 01/10/08 Created</history>
    ''' <remarks></remarks>
    Public Shared Function GetColumns(ByVal strServer As String, ByVal strDatabase As String, ByVal strUser As String, ByVal tableName As String) As DataSet
        Dim ds As DataSet

        Dim strRuta As StringBuilder = New StringBuilder()
        If Not String.IsNullOrEmpty(strServer) Then
            strRuta.Append(strServer)
            strRuta.Append(".")
        End If
        If Not String.IsNullOrEmpty(strDatabase) Then
            strRuta.Append(strDatabase)
            strRuta.Append(".")
        End If
        If Not String.IsNullOrEmpty(strUser) Then
            strRuta.Append(strUser)
            strRuta.Append(".")
        End If

        If Server.ServerType = DBTypes.MSSQLServer7Up OrElse Server.ServerType = DBTypes.MSSQLServer Then
            Dim sql As String = "exec " & strRuta.ToString() & "sp_columns '" & tableName & "'"
            ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
            If Not IsNothing(ds) AndAlso ds.Tables.Count > 0 Then
                ds.Tables(0).Columns.Remove("Table_owner")
                ds.Tables(0).Columns.Remove("Table_qualifier")
                ds.Tables(0).Columns.Remove("Table_name")
            End If
        Else
            Dim sql As String = "Desc " & strRuta.ToString() & tableName
            ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
        End If
        Return ds
    End Function

    ''' <summary>
    ''' Devuelve una lista con todas las tablas y vistas de la base de datos
    ''' </summary>
    ''' <returns></returns>
    ''' <history>Marcelo 01/10/08 Created</history>
    ''' <remarks></remarks>
    Public Shared Function GetTablesAndViews(ByVal strServer As String, ByVal strDatabase As String, ByVal strUser As String) As DataSet
        Dim ds As DataSet
        Dim strRuta As StringBuilder = New StringBuilder()
        If Not String.IsNullOrEmpty(strServer) Then
            strRuta.Append(strServer)
            strRuta.Append(".")
        End If
        If Not String.IsNullOrEmpty(strDatabase) Then
            strRuta.Append(strDatabase)
            strRuta.Append(".")
        End If
        If Not String.IsNullOrEmpty(strUser) Then
            strRuta.Append(strUser)
            strRuta.Append(".")
        End If

        If Server.ServerType = DBTypes.MSSQLServer7Up OrElse Server.ServerType = DBTypes.MSSQLServer Then
            Dim sql As String = "select name from " & strRuta.ToString() & "sysobjects where xtype = 'V' or xtype = 'U' order by name"
            ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
        Else
            Dim sql As String = "select table_name from " & strRuta.ToString() & "dba_views where OWNER = '" & oracleOwner & "';"
            Dim dsaux As DataSet
            ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
            sql = "select table_name from " & strRuta.ToString() & "dba_tables where OWNER = '" & oracleOwner & "';"
            dsaux = Server.Con.ExecuteDataset(CommandType.Text, sql)
            If ds.Tables.Count > 0 Then
                ds.Tables(0).Merge(dsaux.Tables(0))
            Else
                ds = dsaux
            End If
        End If

        Return ds
    End Function

    ''' <summary>
    ''' Owner en oracle, necesario para algunas consultas
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared ReadOnly Property oracleOwner() As String
        Get
            Try
                Dim tmpValue As String = Server.Con.ConString.Split(";")(1)
                tmpValue = tmpValue.Replace("User Id=", "")
                Return tmpValue.ToUpper
            Catch ex As Exception
                Return String.Empty
            End Try
        End Get
    End Property
End Class