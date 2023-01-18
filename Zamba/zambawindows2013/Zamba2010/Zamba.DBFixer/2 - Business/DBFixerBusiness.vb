Imports Zamba.Servers
Imports System.Text

Public Class DBFixerBusiness

    ''' <summary>
    ''' Esta clase agrupa los fixeos que inciden mayoritariamente
    ''' en los objetos Tabla.
    ''' </summary>
    ''' <history>created [Alejandro]</history>
    Public Class FixTable
        ''' <summary>
        ''' Agrega una columna en una tabla.
        ''' </summary>
        ''' <param name="tableName">Nombre de la Tabla.</param>
        ''' <param name="col">Columna a agregar.</param>
        ''' <history> [Alejandro]	02/2008	Created </history>
        Public Shared Sub AddColumn(ByVal tableName As String, ByVal col As Column, Optional ByRef sQuery As String = "")
            DBFixerFactory.FixTable.AddColumn(tableName, col.Name, col.Type(Server.isOracle), col.getIsNull, sQuery, col.Precition, col.Scale, col.DefaultData)
        End Sub

        ''' <summary>
        ''' Crea una tabla en la Base de Datos.
        ''' </summary>
        ''' <param name="tbl">Objeto Tabla a crear.</param>
        ''' <history> [Alejandro] 02/2008 Created </history>
        Public Shared Sub CreateTable(ByVal tbl As Table, Optional ByRef sQuery As String = "")
            Dim qBuilder As New StringBuilder
            DBFixerFactory.FixTable.CreateTable(tbl.Name, tbl.Columns, sQuery)
            qBuilder.AppendLine(sQuery)
            If Not IsNothing(tbl.PrimaryKeyColumnNames) AndAlso tbl.PrimaryKeyColumnNames.Count > 0 Then
                Dim pkcols As New List(Of Column)
                For Each columnname As String In tbl.PrimaryKeyColumnNames
                    For Each c As Column In tbl.Columns
                        If columnname.ToUpper = c.Name.ToUpper Then
                            pkcols.Add(c)

                            Exit For
                        End If
                    Next
                Next

                Dim pkey As New PrimaryKey(pkcols, tbl)
                If Not DBFixerValidations.DBContainsPrimaryKey(pkey) Then
                    DBFixerBusiness.FixGeneral.CreatePrimaryKey(pkey, sQuery)
                End If
            End If
            qBuilder.AppendLine(sQuery)
            sQuery = qBuilder.ToString()
        End Sub

    End Class

    ''' <summary>
    ''' Esta clase agrupa los fixeos que inciden mayoritariamente
    ''' en los objetos Columna.
    ''' </summary>
    ''' <history> [Alejandro] 02/2008 Created </history>
    Public Class FixColumn

        ''' <summary>
        ''' Modifica el tipo de una Columna en la Base de Datos.
        ''' </summary>
        ''' <param name="col">Objeto columna a replicar en la Base de Datos.</param>
        ''' <param name="tableName">Nombre de la Tabla donde se encuentra la Columna.</param>
        ''' <returns>Cantidad de Filas afectadas.</returns>
        ''' <history> [Alejandro] 02/2008 Created </history>
        Public Shared Function AlterType(ByVal col As Column, ByVal tableName As String, Optional ByRef sQuery As String = "") As Int32
            Dim colname As New List(Of String)
            colname.Add(col.Name)
            'Se buscan y eliminan constraints de esa columna 
            DBFixerBusiness.DeleteColumnConstraints(tableName, col.Name)

            'EXEC sp_pkeys @table_name = N'USRTABLE' para las primary keys
            'EXEC sp_fkeys @pktable_name = N'USRTABLE' para las foreign keys

            'Obtengo los Indices[Ver de pasar a Bussines ya que trabaja con entidades]
            If Server.isOracle = True Then
                Dim Indexs As List(Of Index) = DBFixerFactory.GetIndexsByColumn(tableName, col.Name)
                'Borro los indices
                If Not IsNothing(Indexs) Then
                    For Each _Index As Index In Indexs
                        DeleteIndexs(_Index)
                    Next
                End If
                DBFixerFactory.FixColumn.AlterType(col.Name, col.Type(Server.isOracle), col.Precition, col.Scale, tableName, sQuery)

                If Not IsNothing(Indexs) Then
                    For Each _Index As Index In Indexs
                        FixGeneral.CreateIndex(_Index, tableName)
                    Next
                End If
            Else
                DBFixerFactory.FixColumn.AlterType(col.Name, col.Type(Server.isOracle), col.Precition, col.Scale, tableName, sQuery)
            End If
        End Function

        ''' <summary>
        ''' Modifica la longitud de una Columna en la Base de Datos.
        ''' </summary>
        ''' <param name="col">Objeto columna a replicar en la Base de Datos.</param>
        ''' <param name="tableName">Nombre de la Tabla donde se encuentra la Columna.</param>
        ''' <returns>Cantidad de Filas afectadas.</returns>
        ''' <history> [Alejandro] 02/2008 Created </history>
        Public Shared Function AlterLength(ByVal col As Column, ByVal tableName As String, Optional ByRef sQuery As String = "") As Int32
            'Se buscan y eliminan constraints de esa columna 
            DBFixerBusiness.DeleteColumnConstraints(tableName, col.Name)
            Dim Indexs As List(Of Index) = Nothing
            If Server.isOracle
            'Obtengo los Indices[Ver de pasar a Bussines ya que trabaja con entidades]
                Indexs = DBFixerFactory.GetIndexsByColumn(tableName, col.Name)
            'Borro los indices
            If Not IsNothing(Indexs) Then
                For Each _Index As Index In Indexs
                    DeleteIndexs(_Index)
                Next
            End If
            End If
            DBFixerFactory.FixColumn.AlterLength(col.Name, col.Type(Server.isOracle), col.Precition, col.Scale, col.getIsNull, tableName, sQuery)

            If Not IsNothing(Indexs) AndAlso Server.isOracle Then
                For Each _Index As Index In Indexs
                    FixGeneral.CreateIndex(_Index, tableName)
                Next
            End If
        End Function



        ''' <summary>
        ''' Modifica la característica 'Nulleable' de una Columna en la Base de Datos.
        ''' </summary>
        ''' <param name="col">Objeto columna a replicar en la Base de Datos.</param>
        ''' <param name="tableName">Nombre de la Tabla donde se encuentra la Columna.</param>
        ''' <returns>Cantidad de Filas afectadas.</returns>
        ''' <history>created [Alejandro]</history>
        Public Shared Function AlterIsNull(ByVal col As Column, ByVal tableName As String, Optional ByRef sQuery As String = "") As Int32
            'Se buscan y eliminan constraints de esa columna 
            DBFixerBusiness.DeleteColumnConstraints(tableName, col.Name)
            Return DBFixerFactory.FixColumn.AlterIsNull(col.Name, col.Type(Server.isOracle), col.Precition, col.Scale, col.getIsNull, tableName, sQuery)
        End Function

        ''' <summary>
        ''' Modifica el Default de una Columna en la Base de Datos.
        ''' </summary>
        ''' <param name="col">Objeto columna a replicar en la Base de Datos.</param>
        ''' <param name="tableName">Nombre de la Tabla donde se encuentra la Columna.</param>
        ''' <returns>Cantidad de Filas afectadas.</returns>
        ''' <history>created [Alejandro]</history>
        Public Shared Function AlterDefault(ByVal col As Column, ByVal tableName As String, Optional ByRef sQuery As String = "") As Int32
            'Se buscan y eliminan constraints de esa columna 
            DBFixerBusiness.DeleteColumnConstraints(tableName, col.Name)
            Return DBFixerFactory.FixColumn.AlterDefault(col.Name, col.DefaultData, tableName, sQuery)
        End Function
    End Class

    ''' <summary>
    ''' Esta clase agrupa los fixeos generales de una 
    ''' Base de Datos.
    ''' </summary>
    ''' <history>created [Alejandro]</history>
    Public Class FixGeneral

        ''' <summary>
        ''' Crea un PrimaryKey en la Base.
        ''' </summary>
        ''' <param name="pKey">Objeto PrimaryKey a crear.</param>
        ''' <param name="sQuery">String por referencia donde se almacenará la consulta.</param>
        ''' <remarks></remarks>
        Public Shared Sub CreatePrimaryKey(ByVal pKey As PrimaryKey, Optional ByRef sQuery As String = "")
            Dim _columnsNames As New List(Of String)
            For Each Columns As Column In pKey.BaseColumns
                _columnsNames.Add(Columns.Name)
            Next

            DBFixerFactory.DeleteColumnsConstraints(pKey.Table.Name, _columnsNames)

            DBFixerFactory.FixGeneral.CreatePrimaryKey(pKey.Table.Name, _columnsNames, sQuery)
        End Sub

        ''' <summary>
        ''' Crea una ForeignKey en la Base.
        ''' </summary>
        ''' <param name="fKey">Objeto ForeignKey a crear.</param>
        ''' <param name="sQuery">String por referencia donde se almacenará la consulta.</param>
        ''' <remarks></remarks>
        Public Shared Sub CreateForeignKey(ByVal fKey As ForeignKey, Optional ByRef sQuery As String = "")
            Dim bcolumnsNames As New List(Of String)
            Dim rcolumnsNames As New List(Of String)
            For Each baseColumn As Column In fKey.BaseColumn
                bcolumnsNames.Add(baseColumn.Name)
            Next
            For Each refColumn As Column In fKey.RefColumn
                rcolumnsNames.Add(refColumn.Name)
            Next

            DBFixerFactory.DeleteForeignValues(fKey.RefColumn(0).Table.Name, fKey.RefColumn(0).Name, fKey.BaseColumn(0).Table.Name, fKey.BaseColumn(0).Name)
            DBFixerFactory.FixGeneral.CreateForeingKey(fKey.ObjName, fKey.BaseColumn(0).Table.Name, bcolumnsNames, fKey.RefColumn(0).Table.Name, rcolumnsNames, fKey.OnUpdateCascade, fKey.OnDeleteCascade, fKey.OnCheckForReplication, sQuery)

        End Sub

        ''' <summary>
        ''' Crea una Vista en la Base.
        ''' </summary>
        ''' <param name="v">Objeto Vista a crear.</param>
        ''' <remarks></remarks>
        Public Shared Sub CreateView(ByVal v As View)
            DBFixerFactory.FixGeneral.CreateView(v.Text)
        End Sub

        ''' <summary>
        ''' Se crea un procedimiento almacenado, paquete o cuerpo de paquete en la base de datos de corresponda (SQL o Oracle)
        ''' </summary>
        ''' <param name="elemText"></param>
        ''' <remarks></remarks>
        Public Shared Sub createStoredProcedure(ByVal elemText As String)
            DBFixerFactory.FixGeneral.createStoredProcedure(elemText)
        End Sub

        '''' <summary>
        '''' Crea un StoredProcedure en la Base. 
        '''' </summary>
        '''' <param name="sp">Objeto Stored Procedure a crea.</param>
        '''' <remarks></remarks>
        'Public Shared Sub CreateStoredProcedure(ByVal sp As StoredProcedure)
        '    If Not String.IsNullOrEmpty(sp.Text2) Then
        '        DBFixerFactory.FixGeneral.createStoredProcedure(sp.Text, sp.Text2)
        '    Else
        '        DBFixerFactory.FixGeneral.createStoredProcedure(sp.Text)
        '    End If
        'End Sub

        ''' <summary>
        ''' Crea un trigger en base
        ''' </summary>
        ''' <param name="tg">trigger a crear</param>
        ''' <remarks></remarks>
        Public Shared Sub CreateTrigger(ByVal tg As Trigger)
            DBFixerFactory.FixGeneral.CreateTrigger(tg.Name, tg.TableTo, tg.Aplication, tg.Text)
        End Sub



        ''' <summary>
        ''' Crea un unique en la base
        ''' </summary>
        ''' <param name="uq"></param>
        ''' <remarks></remarks>
        Public Shared Sub CreateUnique(ByVal uq As Unique)
            Dim columnsNames As New List(Of String)
            For Each Item As Column In uq.BaseColumns
                columnsNames.Add(Item.Name)
            Next

            DBFixerFactory.FixGeneral.CreateUnique(uq.Table.Name, uq.ObjName, columnsNames)
        End Sub


        Public Shared Sub CreateCheck(ByVal ck As Check)
            DBFixerFactory.FixGeneral.CreateCheck(ck.BaseTable.Name, ck.ObjName, ck.CheckExpression, ck.NotForReplication)
        End Sub


        ''' <summary>
        ''' Crea un indice en base
        ''' </summary>
        ''' <param name="_index">Indice a crear</param>
        ''' <param name="tableName">Nombre de la tabla</param>
        ''' <remarks></remarks>
        ''' <history>23-6-2008 Diego[Created]</history>
        Public Shared Sub CreateIndex(ByVal _index As Index, ByVal tableName As String)
            Dim ColumnsNamesAndSortOrder As New Dictionary(Of String, String)
            For Each c As Column In _index.BaseColumns
                ColumnsNamesAndSortOrder.Add(c.Name, c.IndexOrder)
            Next

            DBFixerFactory.CreateIndex(ColumnsNamesAndSortOrder, tableName)
        End Sub
    End Class


    '''' <summary>
    '''' Elimina todas las contraints de una tabla
    '''' </summary>
    '''' <param name="tableName">Nombre de la tabla</param>
    '''' <remarks></remarks>
    'Public Shared Sub DeleteTableConstraints(ByVal tableName As String)
    '    DBFixerFactory.DeleteTableConstraints(tableName)
    'End Sub

    ''' <summary>
    ''' Borra las constraints de la columna
    ''' </summary>
    ''' <param name="tablename">Nombre de la tabla</param>
    ''' <param name="ColumnName">Nombre de la columna</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Marcelo]	15/05/2008	Modified - Se agrego el comentario
    ''' </history>
    Public Shared Sub DeleteColumnConstraints(ByVal tablename As String, ByVal ColumnName As String)
        DBFixerFactory.DeleteColumnConstraints(tablename, ColumnName)
    End Sub



    ''' <summary>
    ''' Borra Un indice
    ''' </summary>
    ''' <param name="_index">Entidad a Borrar</param>
    ''' <remarks></remarks>
    ''' <history>23-6-2008 Diego[Created]</history>
    Public Shared Sub DeleteIndexs(ByVal _index As Index)
        DBFixerFactory.DeleteIndex(_index.ObjName)
    End Sub


    ''' <summary>
    ''' Obtiene el Id de la tabla Actual Segun usuario
    ''' </summary>
    ''' <param name="tablename"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetTableId(ByVal tablename As String) As Int32
        Return DBFixerFactory.GetTableId(tablename)
    End Function

    Friend Shared Sub ExecuteQuery(ByVal query As String)
        DBFixerFactory.ExecuteQuery(query)
    End Sub

End Class
