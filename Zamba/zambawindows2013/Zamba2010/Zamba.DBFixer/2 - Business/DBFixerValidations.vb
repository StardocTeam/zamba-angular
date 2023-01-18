Imports Zamba
Imports Zamba.Servers
Imports System.Text


Public Class DBFixerValidations

    Public Shared Event ErrorOccurred(ByVal ex As Exception, ByVal element As String, ByVal elementName As String, ByVal tmpQuery As String, ByVal additional As String)

    ''' <summary>
    ''' Owner en oracle, necesario para algunas consultas
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property oracleOwner() As String
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

#Region "Exists"

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Verifica la existencia de una Tabla
    ''' </summary>
    ''' <param name="tableName">Nombre de la tabla que se desea verificar</param>
    ''' <returns>True si la Tabla  Existe</returns>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    '''  [Alejandro]       02/2008  Modified
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function DBContainsTable(ByVal tableName As String) As Boolean
        'TODO store: SPExistsTable

        Dim str As New System.Text.StringBuilder, i As Int32 = 0
        tableName = tableName
        If Server.isSQLServer Then
            str.Append("select count(*) from sysobjects where name = '")
            str.Append(tableName)
            str.Append("'")
            'Dim query As String = getQuery(tableName, str.ToString())
            Try
                i = Convert.ToInt32(Servers.Server.Con.ExecuteScalar(CommandType.Text, str.ToString()))
            Catch ex As Exception
                RaiseEvent ErrorOccurred(ex, "Tabla", tableName, str.ToString(), "[Error verificando existencia en Base.")
            End Try
            If i = 0 Then Return False
            Return True
        Else
            str.Append("select count(*) from dba_tables where TABLE_NAME = '")
            str.Append(tableName)
            str.Append("' and OWNER = '")
            str.Append(oracleOwner.ToUpper)
            str.Append("'")
            Try
                i = Convert.ToInt32(Servers.Server.Con.ExecuteScalar(CommandType.Text, str.ToString))
            Catch ex As Exception
                RaiseEvent ErrorOccurred(ex, "Tabla", tableName, str.ToString, "[Error verificando existencia en Base.")
            End Try
            If i = 0 Then Return False
            Return True
        End If
    End Function

    '''' <summary>
    '''' Arma con el query y el tablename la consulta dinamica
    '''' El query debe terminar con id = para que se adapte bien a la consulta
    '''' </summary>
    '''' <param name="tableName"></param>
    '''' <param name="query"></param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Public Shared Function getQuery(ByVal objName As String, ByVal query As String) As String
    '    Dim str As New System.Text.StringBuilder
    '    Try
    '        str.Append("if (select count(id) from sysobjects where name = '")
    '        str.Append(objName)
    '        str.Append("' and uid=")
    '        str.Append(frmZDBFixer.userId.ToString())
    '        str.Append(") > 0 Begin ")
    '        str.Append(query)
    '        str.Append("(select count(id) from sysobjects where name = '")
    '        str.Append(objName)
    '        str.Append("' and uid=")
    '        str.Append(frmZDBFixer.userId.ToString())
    '        str.Append(") End else Begin ")
    '        str.Append(query)
    '        str.Append("(select count(id) from sysobjects where name = '")
    '        str.Append(objName)
    '        str.Append("' and uid=1")
    '        str.Append(") End")
    '        Return str.ToString()
    '    Finally
    '        str = Nothing
    '    End Try
    'End Function


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Verifica la existencia de una columna dentro de una tabla
    ''' </summary>
    ''' <param name="columnName">Nombre de la columna</param>
    ''' <param name="tableName">Nombre de la Tabla</param>
    ''' <returns>True si la Columna Existe</returns>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    '''  [Alejandro]       02/2008  Modified
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function DBContainsColumn(ByVal columnName As String, ByVal tableName As String) As Boolean
        'TODO store: SPExistsColumn
        Dim str As New System.Text.StringBuilder, i As Int32 = 0
        columnName = columnName
        tableName = tableName
        If Server.isSQLServer Then
            str.Append("select count(*) from syscolumns where id = ")
            str.Append(DBFixerBusiness.GetTableId(tableName).ToString)
            str.Append(" and name = '")
            str.Append(columnName)
            str.Append("'")
            Try

                i = Convert.ToInt32(Servers.Server.Con.ExecuteScalar(CommandType.Text, str.ToString))
                'En caso q haya mas de una tabla
                'Catch
                '    Try
                '        str.Remove(0, str.Length)
                '        str.Append("select count(*) from syscolumns where name = '")
                '        str.Append(columnName)
                '        str.Append("' and id = ")
                '        'Dim query As String = getQuery(tableName, str.ToString())
                '        i = Convert.ToInt32(Servers.Server.Con.ExecuteScalar(CommandType.Text, str.ToString()))
            Catch ex As Exception
                RaiseEvent ErrorOccurred(ex, "Columna", columnName, str.ToString(), "[Error verificando existencia en Base.")
            End Try
            'End Try
            If i = 0 Then Return False
            Return True
        Else
            str.Append("select count(*) from dba_tab_columns where COLUMN_NAME = '")
            str.Append(columnName)
            str.Append("' and TABLE_NAME='")
            str.Append(tableName)
            str.Append("' and OWNER = '")
            str.Append(oracleOwner.ToUpper)
            str.Append("'")
            Try
                i = Convert.ToInt32(Servers.Server.Con.ExecuteScalar(CommandType.Text, str.ToString))
            Catch ex As Exception
                RaiseEvent ErrorOccurred(ex, "Columna", columnName, str.ToString(), "[Error verificando existencia en Base.")
            End Try
            If i = 0 Then Return False
            Return True
        End If
    End Function

    ''' <summary>
    ''' Obtiene el id del usuario
    ''' </summary>
    ''' <param name="userName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetSQLUserId(ByVal userName As String) As Int32
        Dim str As New System.Text.StringBuilder, i As Int32 = 0
        If Server.isSQLServer Then
            str.Append("select uid from sysusers where name = '")
            str.Append(userName)
            str.Append("'")
            Try
                If Convert.ToInt32(Servers.Server.Con.ExecuteScalar(CommandType.Text, str.ToString)) = 0 Then
                    Return 1
                Else
                    i = Convert.ToInt32(Servers.Server.Con.ExecuteScalar(CommandType.Text, str.ToString))
                End If
            Catch ex As Exception
                RaiseEvent ErrorOccurred(ex, "Usuario", userName, str.ToString(), "[Error al obtener id de usuario.")
            End Try
            Return i
        End If
    End Function


    ''' <summary>
    ''' Obtiene el nombre de usuario
    ''' </summary>
    ''' <param name="userid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetSQLUserName(ByVal userid As Int32) As String
        Dim str As New System.Text.StringBuilder, i As Int32 = 0
        If Server.isSQLServer Then
            str.Append("select name from sysusers where uid = ")
            str.Append(userid)
            Try
                If Not IsDBNull(Servers.Server.Con.ExecuteScalar(CommandType.Text, str.ToString)) AndAlso Not IsNothing(Servers.Server.Con.ExecuteScalar(CommandType.Text, str.ToString)) Then
                    Return Servers.Server.Con.ExecuteScalar(CommandType.Text, str.ToString)
                End If
            Catch ex As Exception
                RaiseEvent ErrorOccurred(ex, "Usuario", userid.ToString, str.ToString(), "[Error al obtener nombre de usuario.")
            End Try
        End If
        Return "DBO"
    End Function


    ''' <summary>
    ''' Valida si la vista es válida en la base.
    ''' </summary>
    ''' <param name="vName">Nombre de la vista.</param>
    ''' <param name="vText">Texto de la vista.</param>
    ''' <returns>True si la vista es válida en la base.</returns>
    ''' <history>
    ''' 	[Alejandro]	15/01/2007	Created
    ''' </history>
    Public Shared Function DBContainsView(ByVal vName As String, ByVal vText As String) As Boolean
        Try
            Dim tmpVText As String
            tmpVText = GetProcedureText(vName)
            If Not String.IsNullOrEmpty(tmpVText) Then
                If String.Compare(tmpVText, vText) = 0 Then
                    Return True
                End If
            End If
            Return False
        Catch
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Valida si la vista se encuentra en la base.
    ''' </summary>
    ''' <param name="vName">Nombre de la vista</param>
    ''' <returns>True si el nombre de la vista se encuentra en la base.</returns>
    ''' <history>
    ''' 	[Alejandro]	15/01/2007	Created
    ''' </history>
    Public Shared Function DBContainsView(ByVal vName As String) As Boolean
        Dim sqlBuilder As New StringBuilder()
        If Server.isOracle Then
            sqlBuilder.Append("select * from DBA_VIEWS where VIEW_NAME = '")
            sqlBuilder.Append(vName)
            sqlBuilder.Append("' AND OWNER = '")
            sqlBuilder.Append(oracleOwner)
            sqlBuilder.Append("'")
        Else
            sqlBuilder.Append("select name from sysobjects where xtype = 'V' and name = '")
            sqlBuilder.Append(vName)
            sqlBuilder.Append("'")
        End If
        Dim tmpO As Object = Nothing
        Try
            'tmpO = Server.Con.ExecuteScalar(CommandType.Text, DBFixerValidations.getQuery(vName, sqlBuilder.ToString()))
            tmpO = Server.Con.ExecuteScalar(CommandType.Text, sqlBuilder.ToString())
        Catch ex As Exception
            RaiseEvent ErrorOccurred(ex, "View", vName, sqlBuilder.ToString(), "[Error validando existencia en Base.]")
        End Try
        If Not IsNothing(tmpO) Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Valida si la Primary Key es correcta en la base.
    ''' </summary>
    ''' <param name="pKey">Primary Key a evaluar.</param>
    ''' <returns>True si la primary key es correcta en la base.</returns>
    ''' <history>
    ''' 	[Alejandro]	15/01/2007	Created
    ''' </history>
    Public Shared Function DBContainsPrimaryKey(ByVal pKey As PrimaryKey) As Boolean
        Dim ds As DataSet
        Dim columnsNames As New List(Of String)

        For Each c As Column In pKey.BaseColumns
            columnsNames.Add(c.Name)
        Next

        ds = GetPrimaryKey(pKey.Table.Name, columnsNames)
        If Not IsNothing(ds) AndAlso Not IsDBNull(ds) AndAlso Not IsNothing(ds.Tables) _
        AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then

            If pKey.BaseColumns.Count <> ds.Tables(0).Rows.Count Then
                'si la cantidad de columnas PK entre la base de datos y
                'la entidad difieren , algo esta mal
                Return False
            End If

            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Valida si el procedimiento es correcto en la base.
    ''' </summary>
    ''' <param name="spName">Nombre del procedimiento.</param>
    ''' <param name="spText">Texto del procedimiento.</param>
    ''' <returns>True si el procedimiento es correcto en la base.</returns>
    ''' <history>
    ''' 	[Alejandro]	15/01/2007	Created
    ''' </history>
    Public Shared Function DBContainsStoredProcedure(ByVal spName As String, ByVal spText As String) As Boolean
        Try
            Dim tmpSPText As String = GetProcedureText(spName)
            If Not String.IsNullOrEmpty(tmpSPText) Then
                If String.Compare(tmpSPText, spText) = 0 Then
                    Return True
                End If
            End If
            Return False
        Catch
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Valida si el procedimiento existe en la base de datos de SQL
    ''' </summary>
    ''' <param name="spName">Nombre del procedimiento.</param>
    ''' <returns>True si el procedimiento es correcto en la base.</returns>
    ''' <history>
    ''' 	[Alejandro]	15/01/2007	Created
    '''     [Gaston]    13/06/2008  Modified   Este código estaba en un mismo método junto con la validación de Oracle, lo que se hizo fue seperar
    '''                                        SQL de Oracle y modificar parte del código
    ''' </history>
    Public Shared Function DBContainsStoredProcedure(ByVal spName As String) As Boolean

        Dim sqlBuilder As New StringBuilder()

        sqlBuilder.Append("select name from sysobjects where xtype = 'P' and name = '")
        sqlBuilder.Append(spName)
        sqlBuilder.Append("'")

        Return (validateStoreProcedure(spName, sqlBuilder, "Stored Procedure"))

    End Function

    ''' <summary>
    ''' Se valida si el paquete o cuerpo del paquete existe en la base de datos de Oracle
    ''' </summary>
    ''' <param name="pkgName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Alejandro]	15/01/2007	Created
    '''     [Gaston]    13/06/2008  Modified   Este código estaba en un mismo método junto con la validación de SQL, lo que se hizo fue seperar
    '''                                        Oracle de SQL y modificar parte del código
    ''' </history>
    Public Shared Function DBContainsPackageOrBodyOfPackage(ByVal pkgName As String, ByVal typeElem As String) As Boolean

        Dim sqlBuilder As New StringBuilder()

        sqlBuilder.Append("SELECT NAME FROM DBA_SOURCE WHERE NAME = '")
        sqlBuilder.Append(pkgName)
        sqlBuilder.Append("' AND TYPE = '" & typeElem.ToUpper() & "' AND OWNER = '")
        sqlBuilder.Append(oracleOwner)
        sqlBuilder.Append("'")

        Return (validateStoreProcedure(pkgName, sqlBuilder, typeElem))

    End Function

    ''' <summary>
    ''' Se ejecuta una consulta y se verifica si ocurre un error, en caso contrario se devuelve un boolean
    ''' </summary>
    ''' <param name="elemName"></param>
    ''' <param name="sqlBuilder"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    '''     [Alejandro]	15/01/2007	Created
    '''     [Gaston]    13/06/2008  Modified   Este código estaba en un mismo método junto con la validación de Oracle y SQL, lo que se hizo fue 
    '''                                        que cada método especifico de SQL y Oracle llamase a este método modificando parte del código
    ''' </history>
    Private Shared Function validateStoreProcedure(ByVal elemName As String, ByVal sqlBuilder As StringBuilder, ByVal typeElem As String) As Boolean

        Dim tmpO As Object = Nothing

        Try
            'tmpO = Server.Con.ExecuteScalar(CommandType.Text, DBFixerValidations.getQuery(spName, sqlBuilder.ToString()))
            tmpO = Server.Con.ExecuteScalar(CommandType.Text, sqlBuilder.ToString())
        Catch ex As Exception
            RaiseEvent ErrorOccurred(ex, typeElem, elemName, sqlBuilder.ToString, "[Error validando existencia de " & typeElem & " en la Base]")
        End Try

        If Not IsNothing(tmpO) Then
            Return True
        Else
            Return False
        End If

    End Function

    '''' <summary>
    '''' Valida si el procedimiento existe en la base.
    '''' </summary>
    '''' <param name="spName">Nombre del procedimiento.</param>
    '''' <returns>True si el procedimiento es correcto en la base.</returns>
    '''' <history>
    '''' 	[Alejandro]	15/01/2007	Created
    '''' </history>
    'Public Shared Function DBContainsStoredProcedure(ByVal spName As String) As Boolean
    '    Dim sqlBuilder As New StringBuilder()
    '    If Server.isOracle Then
    '        sqlBuilder.Append("SELECT NAME FROM DBA_SOURCE WHERE NAME = '")
    '        sqlBuilder.Append(spName)
    '        sqlBuilder.Append("' AND TYPE = 'PACKAGE' AND OWNER = '")
    '        sqlBuilder.Append(oracleOwner)
    '        sqlBuilder.Append("'")
    '    Else
    '        sqlBuilder.Append("select name from sysobjects where xtype = 'P' and name = '")
    '        sqlBuilder.Append(spName)
    '        sqlBuilder.Append("'")
    '    End If
    '    Dim tmpO As Object = Nothing
    '    Try
    '        'tmpO = Server.Con.ExecuteScalar(CommandType.Text, DBFixerValidations.getQuery(spName, sqlBuilder.ToString()))
    '        tmpO = Server.Con.ExecuteScalar(CommandType.Text, sqlBuilder.ToString())
    '    Catch ex As Exception
    '        RaiseEvent ErrorOccurred(ex, "StoredProcedure", spName, sqlBuilder.ToString, "[Error validando existencia de StoredProcedure en la Base]")
    '    End Try
    '    If Not IsNothing(tmpO) Then
    '        Return True
    '    Else
    '        Return False
    '    End If
    'End Function

    ''' <summary>
    ''' Valida si la Foreign Key es válida.
    ''' </summary>
    ''' <param name="fKey">Foreign Key a validar.</param>
    ''' <returns>True si la ForeignKey es válida.</returns>
    ''' <history>
    ''' 	[Alejandro]	15/01/2007	Created
    ''' </history>
    Public Shared Function DBContainsForeignKey(ByVal fKey As ForeignKey) As Boolean
        'Valido con una de N columnas posibles
        '        Dim ds As DataSet = GetReference(fKey.BaseTable.Name, fKey.BaseColumn.Name, fKey.RefTable.Name, fKey.RefColumn.Name)
        Dim ds As DataSet = GetReference(fKey.BaseColumn(0).Table.Name, fKey.BaseColumn(0).Name, fKey.RefColumn(0).Table.Name, fKey.RefColumn(0).Name)
        If Not IsNothing(ds) AndAlso Not IsDBNull(ds) AndAlso Not IsNothing(ds.Tables) _
        AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function


    ''' <summary>
    ''' Valida que exista el trigger en la base
    ''' </summary>
    ''' <param name="tg"> Trigger a validar</param>
    ''' <returns></returns>
    ''' <remarks>Diego</remarks>
    Public Shared Function DBContainsTrigger(ByVal tg As Trigger) As Boolean
        Try
            Dim tmpTGText As String = GetTriggerText(tg.Name)
            If Not String.IsNullOrEmpty(tmpTGText) Then
                If tmpTGText.ToUpper.Contains(tg.Name.ToUpper) Then
                    Return True
                End If

            End If
            Return False
        Catch
            Return False
        End Try
    End Function



    ''' <summary>
    ''' Valida que exista la restriccion unique en la db
    ''' </summary>
    ''' <param name="uq">unique</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DBContainsUnique(ByVal uq As Unique) As Boolean
        Return ValidateUnique(uq.BaseColumns(0).Name, uq.Table.Name)
    End Function

    ''' <summary>
    ''' Valida que exista la restriccion check en la base
    ''' </summary>
    ''' <param name="ck">check</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DBContainsCheck(ByVal ck As Check) As Boolean
        If ck.CheckExpression.Split(" ")(0).ToUpper.StartsWith("NOT") Then
            Return GetCheckText(ck.BaseTable.Name, ck.CheckExpression.Split(" ")(1).ToUpper)
        Else
            Return GetCheckText(ck.BaseTable.Name, ck.CheckExpression.Split(" ")(0).ToUpper)
        End If

    End Function


#End Region

#Region "Get"

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene el Length de una Columna
    ''' </summary>
    ''' <param name="columnName">Nombre de la columna</param>
    ''' <param name="tableName">Nombre de la Tabla</param>
    ''' <returns>Length(Int32) de la Columna.</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Alejandro]	14/01/2007	Created
    ''' 	[Marcelo]	15/05/2008	Modified - Se agrego que filtre por columna
    '''                                      - Estaba trayendo el los valores de la primera columna siempre
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetColumnLength(ByVal columnName As String, ByVal tableName As String) As Dictionary(Of String, Int32)
        Dim str As New System.Text.StringBuilder
        columnName = columnName
        tableName = tableName
        If Server.isSQLServer Then
            str.Append("select prec,scale from syscolumns where id = ")
            str.Append(DBFixerBusiness.GetTableId(tableName))
            str.Append(" and name = '")
            str.Append(columnName)
            str.Append("'")

            Try
                Dim dict As New Dictionary(Of String, Int32)
                Dim precition As Int32 = -1
                Dim scale As Int32 = -1
                If Not IsDBNull(Server.Con.ExecuteDataset(CommandType.Text, str.ToString()).Tables(0).Rows(0).Item(0)) Then
                    precition = Server.Con.ExecuteDataset(CommandType.Text, str.ToString()).Tables(0).Rows(0).Item(0)
                End If

                If Not IsDBNull(Server.Con.ExecuteDataset(CommandType.Text, str.ToString()).Tables(0).Rows(0).Item(1)) Then
                    scale = Int32.Parse(Server.Con.ExecuteDataset(CommandType.Text, str.ToString()).Tables(0).Rows(0).Item(1).ToString)
                End If

                dict.Add("PRECITION", precition)
                dict.Add("SCALE", scale)
                Return dict
            Catch ex As Exception
                RaiseEvent ErrorOccurred(ex, "Columna", columnName, str.ToString, "[Error obteniendo logitud de la Columna.]")
            End Try
        Else

            str.Append("select DATA_PRECISION, DATA_SCALE, DATA_LENGTH from dba_tab_columns where COLUMN_NAME = '")
            str.Append(columnName)
            str.Append("' and TABLE_NAME='")
            str.Append(tableName)
            str.Append("' and OWNER = '")
            str.Append(oracleOwner.ToUpper)
            str.Append("'")

            Dim dict As New Dictionary(Of String, Int32)
            Dim Precision As Int32 = -1
            Dim Lenght As Int32 = -1
            Dim Scale As Int32 = -1
            Dim ds As DataSet = Nothing
            Try

                ds = Servers.Server.Con.ExecuteDataset(CommandType.Text, str.ToString)

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DATA_PRECISION")) Then
                    Precision = ds.Tables(0).Rows(0).Item("DATA_PRECISION")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DATA_LENGTH")) Then
                    Lenght = ds.Tables(0).Rows(0).Item("DATA_LENGTH")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DATA_SCALE")) Then
                    Scale = ds.Tables(0).Rows(0).Item("DATA_SCALE")
                End If

                dict.Add("SCALE", Scale)
                dict.Add("LENGHT", Lenght)
                dict.Add("PRECITION", Precision)
                Return dict

            Catch ex As Exception
                RaiseEvent ErrorOccurred(ex, "Columna", columnName, str.ToString, "[Error obteniendo logitud de la Columna.]")
            End Try
        End If
        Return Nothing
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene la declaración null/not-null de una Columna.
    ''' </summary>
    ''' <param name="columnName">Nombre de la columna</param>
    ''' <param name="tableName">Nombre de la Tabla</param>
    ''' <returns>True si la columna es nullable.</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Alejandro]	14/01/2007	Created
    ''' 	[Marcelo]	15/05/2008	Modified - Se agrego que filtre por columna
    '''                                      - Estaba trayendo el los valores de la primera columna siempre
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetColumnIsNullable(ByVal columnName As String, ByVal tableName As String) As Boolean
        'Public Shared Function GetColumnIsNullable(ByVal columnName As String, ByVal tableName As String, ByVal userid As Int32) As Boolean
        Dim str As New System.Text.StringBuilder
        columnName = columnName
        tableName = tableName
        If Server.isSQLServer Then
            Dim i As Int32 = 1
            str.Append("select isnullable from syscolumns where id = ")
            str.Append(DBFixerBusiness.GetTableId(tableName))
            str.Append(" and name = '")
            str.Append(columnName)
            str.Append("'")

            Try
                i = Convert.ToByte(Servers.Server.Con.ExecuteScalar(CommandType.Text, str.ToString()))
                'Catch
                '    Try
                '        str.Remove(0, str.Length)
                '        str.Append("select isnullable from syscolumns where id = (select id from sysobjects where name = '")
                '        str.Append(tableName)
                '        str.Append("' and uid = ")
                '        str.Append(userid)
                '        str.Append(" ) and name = '")
                '        str.Append(columnName)
                '        str.Append("'")
                '        i = Convert.ToByte(Servers.Server.Con.ExecuteScalar(CommandType.Text, str.ToString()))
            Catch ex As Exception
                RaiseEvent ErrorOccurred(ex, "Columna", columnName, str.ToString, "[Error obteniendo declaración Nullable de la Columna]")
            End Try
            'End Try
            If 0 = i Then
                Return False
            Else
                Return True
            End If
        Else
            str.Append("select NULLABLE from dba_tab_columns where COLUMN_NAME = '")
            str.Append(columnName)
            str.Append("' and TABLE_NAME='")
            str.Append(tableName)
            str.Append("' and OWNER = '")
            str.Append(oracleOwner)
            str.Append("'")
            Dim sIsNullable As String = "Y"
            Try
                sIsNullable = Servers.Server.Con.ExecuteScalar(CommandType.Text, str.ToString())
            Catch ex As Exception
                RaiseEvent ErrorOccurred(ex, "Columna", columnName, str.ToString, "[Error obteniendo declaración Nullable de la Columna]")
            End Try
            If String.Compare(sIsNullable, "Y") = 0 Then
                Return True
            Else
                Return False
            End If
        End If
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene el tipo de una columna.
    ''' </summary>
    ''' <param name="columnName">Nombre de la columna</param>
    ''' <param name="tableName">Nombre de la Tabla</param>
    ''' <returns>Tipo de la columna (String).</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Alejandro]	14/01/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetColumnType(ByVal columnName As String, ByVal tableName As String) As String
        Dim str As New System.Text.StringBuilder
        columnName = columnName
        tableName = tableName
        If Server.isSQLServer Then
            Dim i As Int32
            str.Append("select xtype from syscolumns where id = ")
            str.Append(DBFixerBusiness.GetTableId(tableName))
            str.Append(" and name = '")
            str.Append(columnName)
            str.Append("'")
            Try
                i = Convert.ToInt16(Servers.Server.Con.ExecuteScalar(CommandType.Text, str.ToString()))
            Catch
                'Try
                '    str.Remove(0, str.Length)
                '    str.Append("select xtype from syscolumns where id = ")
                '    str.Append(DBFixerBusiness.GetTableId(tableName))
                '    str.Append("  and name = '")
                '    str.Append(columnName)
                '    str.Append("'")
                '    i = Convert.ToInt16(Servers.Server.Con.ExecuteScalar(CommandType.Text, str.ToString()))
                'Catch ex As Exception
                '    RaiseEvent ErrorOccurred(ex, "Columna", columnName, str.ToString, "[Error obteniendo el Tipo de la Columna]")
                '    Return String.Empty
                'End Try
            End Try
            Try
                Dim eColumnXType As EnumColumnsTypesSQLServer = i
                Return eColumnXType.ToString().Replace("t_", "")
            Catch
                Return String.Empty
            End Try
        Else
            Dim returnValue As String = String.Empty
            str.Append("select DATA_TYPE from dba_tab_columns where COLUMN_NAME = '")
            str.Append(columnName)
            str.Append("' and TABLE_NAME='")
            str.Append(tableName)
            str.Append("' and owner = '")
            str.Append(oracleOwner.ToUpper)
            str.Append("'")
            Try
                returnValue = Server.Con.ExecuteScalar(CommandType.Text, str.ToString).ToString
            Catch ex As Exception
                RaiseEvent ErrorOccurred(ex, "Columna", columnName, str.ToString, "[Error obteniendo el tipo de la Columna]")
            End Try
            Return returnValue
        End If
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene el valor por defecto de una columna.
    ''' </summary>
    ''' <param name="columnName">Nombre de la columna</param>
    ''' <param name="tableName">Nombre de la Tabla</param>
    ''' <returns>Valor por defecto de la columna.</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Alejandro]	14/01/2007	Created
    '''     [Gaston]    23/06/2008  Modified    Se agrego la validación que verifica si es DBNull, si es, se devuelve un String.Empty
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetColumnDefaultData(ByVal columnName As String, ByVal tableName As String) As String
        'Public Shared Function GetColumnDefaultData(ByVal columnName As String, ByVal tableName As String, ByVal userid As Int32) As String

        Dim str As New System.Text.StringBuilder
        If Server.isSQLServer Then
            str.Append("select text from syscomments where id = (select cdefault from syscolumns where id = ")
            str.Append(DBFixerBusiness.GetTableId(tableName))
            str.Append(" and name = '")
            str.Append(columnName)
            str.Append("')")

            Dim cDataDefault As String = String.Empty
            Dim tmpObj As Object = Nothing
            Try
                tmpObj = Server.Con.ExecuteScalar(CommandType.Text, str.ToString())
                'Catch
                '    Try
                '        str.Remove(0, str.Length)
                '        str.Append("select text from syscomments where id = (select cdefault from syscolumns where id = ")
                '        str.Append(DBFixerBusiness.GetTableId(tableName))
                '        str.Append("  and name = '")
                '        str.Append(columnName)
                '        str.Append("')")
                '        tmpObj = Server.Con.ExecuteScalar(CommandType.Text, str.ToString())
            Catch ex As Exception
                RaiseEvent ErrorOccurred(ex, "Columna", columnName, str.ToString, "[Error obteniendo el ValorDefault de la Columna]")
            End Try
            'End Try
            If Not IsNothing(tmpObj) Then
                cDataDefault = tmpObj.ToString()
            Else
                Return String.Empty
            End If
            cDataDefault = cDataDefault.Remove(0, 1)
            cDataDefault = cDataDefault.Remove(cDataDefault.Length - 1, 1)
            If cDataDefault.StartsWith("'") AndAlso cDataDefault.Length > 2 Then
                cDataDefault = cDataDefault.Remove(0, 1)
            End If
            If cDataDefault.EndsWith("'") AndAlso cDataDefault.Length > 1 Then
                cDataDefault = cDataDefault.Remove(cDataDefault.Length - 1, 1)
            End If
            Return cDataDefault
        Else
            str.Append("select DATA_DEFAULT from dba_tab_columns where COLUMN_NAME = '")
            str.Append(columnName)
            str.Append("' and TABLE_NAME='")
            str.Append(tableName)
            str.Append("' and owner = '")
            str.Append(oracleOwner)
            str.Append("'")

            Try

                Dim obj As Object = Server.Con.ExecuteScalar(CommandType.Text, str.ToString())

                If (IsDBNull(obj)) Then
                    Return (String.Empty)
                Else
                    Return (DirectCast(obj, String))
                End If

            Catch ex As Exception
                RaiseEvent ErrorOccurred(ex, "Columna", columnName, str.ToString, "[Error obteniendo el ValorDefault de la Columna]")
                Return Nothing
            End Try

        End If
    End Function

    ''' <summary>
    ''' Obtiene las referencias para una tabla. Opcional: las referencias entre dos tablas.
    ''' </summary>
    ''' <param name="baseTableName">Nombre de la tabla base.</param>
    ''' <param name="refTableName">Nombre de la tabla a la que las referencias apuntan.</param>
    ''' <returns>/***********************************************************************************\
    '''          |                                SQL Server                                         |
    '''          | tabla_base: Nombre de la tabla base.                                              |
    '''          | tabla_ref: Nombre de la tabla a la que las referencias apuntan.                   |
    '''          | columna_base: Nombre de la columna de la referencia de la tabla_base.             |
    '''          | columna_ref: Nombre de la columna de la tabla_ref a la que apunta la referencia.  |
    '''          | nombre: Nombre de la referencia.                                                  |
    '''          |                                                                                   |                 
    '''          |                                  Oracle                                           |
    '''          | COLUMN_NAME: Nombre de la columna de la tabla a la que apunta la referencia.      |
    '''          |                                                                                   |
    '''          \***********************************************************************************/
    '''          </returns>
    ''' <history>
    ''' 	[Alejandro]	15/01/2007	Created
    ''' </history>
    Public Shared Function GetTableReferences(ByVal baseTableName As String, Optional ByVal refTableName As String = "") As DataSet
        Dim sqlBuilder As New System.Text.StringBuilder()
        If Server.isOracle Then
            sqlBuilder.AppendLine("select DBA_CONS_COLUMNS.COLUMN_NAME")
            sqlBuilder.AppendLine("  from(DBA_CONSTRAINTS)")
            sqlBuilder.AppendLine("inner join  DBA_CONS_COLUMNS")
            sqlBuilder.AppendLine("      on  DBA_CONS_COLUMNS.CONSTRAINT_NAME = DBA_CONSTRAINTS.CONSTRAINT_NAME")
            sqlBuilder.AppendLine("where  DBA_CONSTRAINTS.CONSTRAINT_NAME IN (select R_CONSTRAINT_NAME ")
            sqlBuilder.AppendLine("from(DBA_CONSTRAINTS)")
            sqlBuilder.AppendLine("where OWNER = 'ZAMBA'")
            sqlBuilder.Append("and TABLE_NAME = '")
            sqlBuilder.Append(baseTableName)
            sqlBuilder.AppendLine("'")
            sqlBuilder.AppendLine("and CONSTRAINT_TYPE = 'R')")
            If Not String.IsNullOrEmpty(refTableName) Then
                sqlBuilder.Append("and DBA_CONSTRAINTS.TABLE_NAME = '")
                sqlBuilder.Append(refTableName)
                sqlBuilder.AppendLine("'")
            End If
            sqlBuilder.AppendLine("';")
        Else
            sqlBuilder.AppendLine("select tabla_base = object_name(source.id),")
            sqlBuilder.AppendLine("tabla_ref = object_name(dest.id),")
            sqlBuilder.AppendLine("nombre = object_name(sysobjects.id),")
            sqlBuilder.AppendLine("columna_base = source.name,")
            sqlBuilder.AppendLine("columna_ref = dest.name,")
            sqlBuilder.AppendLine("keyno")
            sqlBuilder.AppendLine("from sysforeignkeys inner join sysobjects")
            sqlBuilder.AppendLine("on sysforeignkeys.constid = sysobjects.id")
            sqlBuilder.AppendLine("inner join sysreferences")
            sqlBuilder.AppendLine("on sysforeignkeys.constid = sysreferences.constid")
            sqlBuilder.AppendLine("inner join syscolumns source")
            sqlBuilder.AppendLine("on sysforeignkeys.fkeyid = source.id and ")
            sqlBuilder.AppendLine("sysforeignkeys.fkey = source.colid")
            sqlBuilder.AppendLine(" inner join syscolumns dest")
            sqlBuilder.AppendLine("on sysforeignkeys.rkeyid = dest.id and ")
            sqlBuilder.AppendLine("sysforeignkeys.rkey = dest.colid")
            sqlBuilder.AppendLine("where sysobjects.xtype = 'F'")
            sqlBuilder.Append("and (object_name(source.id) in ('")
            sqlBuilder.Append(baseTableName)
            sqlBuilder.AppendLine("'))")
            sqlBuilder.AppendLine("order by nombre,keyno")
        End If
        ''Obtengo todas las relaciones para esa tabla
        'sqlBuilder.AppendLine("select R_CONSTRAINT_NAME")
        'sqlBuilder.AppendLine(" from  DBA_CONSTRAINTS")
        'sqlBuilder.AppendLine("where OWNER = 'ZAMBA'")
        'sqlBuilder.Append("  and TABLE_NAME = '")
        'sqlBuilder.Append(tableName)
        'sqlBuilder.AppendLine("'")
        'sqlBuilder.Append("  and CONSTRAINT_TYPE = 'R'")
        ''Si la tabla destino esta especificada filtro las relaciones a la tabla destino
        'If Not String.IsNullOrEmpty(refTableName) Then
        '    ds = Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())
        '    If Not IsNothing(ds) AndAlso Not IsDBNull(ds) AndAlso Not IsNothing(ds.Tables) _
        '    AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
        '        'Por cada relación de esa tabla
        '        For Each r As DataRow In ds.Tables(0).Rows
        '            sqlBuilder.Append("select CONSTRAINT_NAME where CONSTRAINT_NAME = '")
        '            sqlBuilder.Append(r("R_CONSTRAINT_NAME"))
        '            sqlBuilder.Append("' AND TABLE_NAME '")
        '            sqlBuilder.Append(refTableName)
        '            sqlBuilder.Append("'")
        '            ds = Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())
        '            'Si el dataset viene con resultados significa que tal relación es entre
        '            'las dos tablas pasadas por parámetros
        '            If Not IsNothing(ds) AndAlso Not IsDBNull(ds) AndAlso Not IsNothing(ds.Tables) _
        '           AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then

        '            End If
        '        Next
        '    End If
        'End If
        Try
            Return Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())
        Catch ex As Exception
            RaiseEvent ErrorOccurred(ex, "Tabla", baseTableName, sqlBuilder.ToString, "[Error obteniendo las Relaciones de la Tabla]")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene todas las referencias entre la columna y la tabla base y la columna y la tabla de
    ''' referencia pasadas por parámetro.
    ''' </summary>
    ''' <param name="baseTableName">Nombre de la tabla base.</param>
    ''' <param name="baseColumnName">Nombre de la columna base.</param>
    ''' <param name="refTableName">Nombre de la tabla de referencia.</param>
    ''' <param name="refColumnName">Nombre de la columna de referencia.</param>
    ''' <returns>/***********************************************************************************\
    '''          |                                SQL Server                                         |
    '''          | tabla_base: Nombre de la tabla base.                                              |
    '''          | tabla_ref: Nombre de la tabla a la que las referencias apuntan.                   |
    '''          | columna_base: Nombre de la columna de la referencia de la tabla_base.             |
    '''          | columna_ref: Nombre de la columna de la tabla_ref a la que apunta la referencia.  |
    '''          | nombre: Nombre de la referencia.                                                  |
    '''          |                                                                                   |                 
    '''          |                                  Oracle                                           |
    '''          | COLUMN_NAME: Nombre de la columna de la tabla a la que apunta la referencia y     |
    '''          |              nombre de la columna de la tabla de la que parte la referencia.      |
    '''          |                                                                                   |
    '''          \***********************************************************************************/
    ''' </returns>
    ''' <history>
    ''' 	[Alejandro]	15/01/2007	Created
    ''' </history>
    Public Shared Function GetReference(ByVal baseTableName As String, ByVal baseColumnName As String, ByVal refTableName As String, ByVal refColumnName As String) As DataSet
        Dim sqlBuilder As New System.Text.StringBuilder()
        If Server.isOracle Then
            'ORACLE
            sqlBuilder.Append("select COLUMN_NAME from(DBA_CONS_COLUMNS) ")
            sqlBuilder.Append("WHERE TABLE_NAME = '")
            sqlBuilder.Append(baseTableName)
            sqlBuilder.Append("'  AND COLUMN_NAME = '")
            sqlBuilder.Append(baseColumnName)
            sqlBuilder.Append("' AND OWNER = '")
            sqlBuilder.Append(oracleOwner)
            sqlBuilder.Append("' AND CONSTRAINT_NAME IN (select CONSTRAINT_NAME ")
            sqlBuilder.Append(" from(DBA_CONSTRAINTS) where OWNER = '")
            sqlBuilder.Append(oracleOwner)
            sqlBuilder.Append("' and TABLE_NAME = '")
            sqlBuilder.Append(baseTableName)
            sqlBuilder.Append("' and CONSTRAINT_TYPE = 'R')")
            Try
                Return Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())

            Catch ex As Exception
                RaiseEvent ErrorOccurred(ex, "Tabla", baseTableName, sqlBuilder.ToString, "[Error obteniendo Relaciones de la Tabla]")
                Return Nothing
            End Try
        Else
            'SQL
            sqlBuilder.AppendLine("select tabla_base = object_name(source.id),")
            sqlBuilder.AppendLine("tabla_ref = object_name(dest.id),")
            sqlBuilder.AppendLine("nombre = object_name(sysobjects.id),")
            sqlBuilder.AppendLine("columna_base = source.name,")
            sqlBuilder.AppendLine("columna_ref = dest.name,")
            sqlBuilder.AppendLine("keyno")
            sqlBuilder.AppendLine("from sysforeignkeys inner join sysobjects")
            sqlBuilder.AppendLine("on sysforeignkeys.constid = sysobjects.id")
            sqlBuilder.AppendLine("inner join sysreferences")
            sqlBuilder.AppendLine("on sysforeignkeys.constid = sysreferences.constid")
            sqlBuilder.AppendLine("inner join syscolumns source")
            sqlBuilder.AppendLine("on sysforeignkeys.fkeyid = source.id and ")
            sqlBuilder.AppendLine("sysforeignkeys.fkey = source.colid")
            sqlBuilder.AppendLine(" inner join syscolumns dest")
            sqlBuilder.AppendLine("on sysforeignkeys.rkeyid = dest.id and ")
            sqlBuilder.AppendLine("sysforeignkeys.rkey = dest.colid")
            sqlBuilder.AppendLine("where sysobjects.xtype = 'F'")
            sqlBuilder.Append("and object_name(source.id) in ('")
            sqlBuilder.Append(baseTableName)
            sqlBuilder.AppendLine("')")
            sqlBuilder.Append("and object_name(dest.id) in ('")
            sqlBuilder.Append(refTableName)
            sqlBuilder.AppendLine("')")
            sqlBuilder.Append("and source.name in ('")
            sqlBuilder.Append(baseColumnName)
            sqlBuilder.AppendLine("')")
            sqlBuilder.Append("and dest.name in ('")
            sqlBuilder.Append(refColumnName)
            sqlBuilder.AppendLine("')")
            sqlBuilder.AppendLine("order by nombre,keyno")
            Try
                Return Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())
            Catch ex As Exception
                RaiseEvent ErrorOccurred(ex, "Tabla", baseTableName, sqlBuilder.ToString, "[Error obteniendo Relaciones de la Tabla]")
                Return Nothing
            End Try
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene la Primary Key para una tabla y una columna pasada por parámetros.
    ''' </summary>
    ''' <param name="tableName">Nombre de la tabla que contiene la Primary Key.</param>
    ''' <param name="columnNames">Nombres de la columna que contiene la Primary Key.</param>
    ''' <returns>Devuelve toda la información disponible en la base para esa relación.</returns>
    ''' <history>
    ''' 	[Alejandro]	15/01/2007	Created
    ''' </history>
    Public Shared Function GetPrimaryKey(ByVal tableName As String, ByVal columnNames As List(Of String)) As DataSet
        Dim sqlBuilder As New StringBuilder()
        If Server.isOracle Then
            sqlBuilder.AppendLine("select DBA_CONS_COLUMNS.COLUMN_NAME")
            sqlBuilder.AppendLine("FROM(DBA_CONS_COLUMNS)")
            sqlBuilder.AppendLine("INNER JOIN DBA_CONSTRAINTS")
            sqlBuilder.AppendLine("ON DBA_CONSTRAINTS.CONSTRAINT_NAME = DBA_CONS_COLUMNS.CONSTRAINT_NAME")
            sqlBuilder.Append("WHERE DBA_CONSTRAINTS.TABLE_NAME = '")
            sqlBuilder.Append(tableName.ToUpper)
            sqlBuilder.AppendLine("'")
            sqlBuilder.AppendLine("AND DBA_CONSTRAINTS.CONSTRAINT_TYPE = 'P'")
            sqlBuilder.Append("AND (")

            For Each column As String In columnNames
                sqlBuilder.Append(" DBA_CONS_COLUMNS.COLUMN_NAME = '")
                sqlBuilder.Append(column)
                sqlBuilder.Append("'")
                sqlBuilder.Append(" OR ")
            Next
            sqlBuilder.Remove(sqlBuilder.Length - 3, 3)
            sqlBuilder.Append(")")
            sqlBuilder.Append(" AND DBA_CONS_COLUMNS.OWNER = '")
            sqlBuilder.Append(oracleOwner)
            sqlBuilder.Append("' AND DBA_CONSTRAINTS.OWNER = '")
            sqlBuilder.Append(oracleOwner)
            sqlBuilder.Append("'")
        Else
            sqlBuilder.Append("SELECT  c.name, c.colid FROM sysindexes i INNER JOIN sysobjects t ON i.id = t.id INNER JOIN sysindexkeys k ON i.indid = k.indid AND i.id = k.ID INNER JOIN syscolumns c	ON c.id = t.id AND c.colid = k.colid WHERE  i.id = t.id AND i.indid BETWEEN 1 And 254 AND (i.status & 2048) = 2048 AND t.id = OBJECT_ID('")
            sqlBuilder.Append(tableName)
            sqlBuilder.AppendLine("') ORDER BY k.KeyNo")
        End If
        Try
            Return Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())
        Catch ex As Exception
            RaiseEvent ErrorOccurred(ex, "Tabla", tableName, sqlBuilder.ToString(), "[Error obteniendo PrimaryKey]")
        End Try
        Return Nothing
    End Function



    ''' <summary>
    ''' Obtiene el texto de un procedimiento para un nombre de procedimiento
    ''' pasado por parámetro.
    ''' </summary>
    ''' <param name="spName">Nombre del procedimiento.</param>
    ''' <returns>Devuelve el texto del procedimiento o (Null/Empty) si el procedimiento
    ''' no se encuentra en la base.</returns>
    ''' <history>
    ''' 	[Alejandro]	15/01/2007	Created
    ''' </history>
    Public Shared Function GetProcedureText(ByVal spName As String) As String
        Dim sqlBuilder As New StringBuilder()
        sqlBuilder.Append("select [text] from syscomments where id = (select id from sysobjects where xtype = 'P' and name = '")
        sqlBuilder.Append(spName)
        sqlBuilder.Append("')")
        Try
            Return Server.Con.ExecuteScalar(CommandType.Text, sqlBuilder.ToString()).ToString()
        Catch ex As Exception
            RaiseEvent ErrorOccurred(ex, "StoredProcedure", spName, sqlBuilder.ToString(), "[Error verificando existencia en Base: no se puede obtener el texto del Stored.]")
        End Try
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene el texto de una vista para un nombre de vista pasado por parámetro.
    ''' </summary>
    ''' <param name="vName">Nombre de la vista a buscar el texto.</param>
    ''' <returns>Devuelve el texto de la vista si la vista existe o (Empty/Null)
    ''' si la vista no se encuentra en la base.</returns>
    ''' <history>
    ''' 	[Alejandro]	15/01/2007	Created
    ''' </history>
    Public Shared Function GetViewText(ByVal vName As String) As String
        Dim sqlBuilder As New StringBuilder()
        sqlBuilder.Append("select [text] from syscomments where id = (select id from sysobjects where xtype = 'V' and name = '")
        sqlBuilder.Append(vName)
        sqlBuilder.Append("'")
        Try
            Return Server.Con.ExecuteScalar(CommandType.Text, sqlBuilder.ToString()).ToString()
        Catch ex As Exception
            RaiseEvent ErrorOccurred(ex, "Vista", vName, sqlBuilder.ToString, "[Error al obtener el Script de la Vista]")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Valida existencia de un trigger en base
    ''' </summary>
    ''' <param name="tgName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetTriggerText(ByVal tgName As String) As String
        Dim sqlBuilder As New StringBuilder()
        sqlBuilder.Append("select [text] from syscomments where id = (select id from sysobjects where xtype = 'TR' and name = '")
        sqlBuilder.Append(tgName)
        sqlBuilder.Append("')")
        Try
            Return Server.Con.ExecuteScalar(CommandType.Text, sqlBuilder.ToString)
        Catch ex As Exception
            RaiseEvent ErrorOccurred(ex, "Trigger", tgName, sqlBuilder.ToString, "[Error al obtener el Script del Trigger]")
            Return Nothing
        End Try
    End Function


    ''' <summary>
    ''' Obtiene el nombre de la restriccion unique, para validar
    ''' </summary>
    ''' <param name="ColumnName">Nombre de la columna</param>
    ''' <param name="TableName">Nombre de la tabla</param>
    ''' <returns></returns>
    ''' <history>
    ''' Marcelo Modified 03/07/08
    ''' </history>
    ''' <remarks></remarks>
    Public Shared Function ValidateUnique(ByVal ColumnName As String, ByVal TableName As String) As Boolean
        Dim sqlBuilder As New StringBuilder()
        Try
            If Not Server.isOracle Then

                sqlBuilder.Append("select id from sysobjects where xtype = 'UQ' and parent_obj =  (select id from sysobjects where xtype = 'U' and name =  '")
                sqlBuilder.Append(TableName)
                sqlBuilder.Append("')")


                If Not String.IsNullOrEmpty(Server.Con.ExecuteScalar(CommandType.Text, sqlBuilder.ToString)) Then
                    Return True
                Else
                    Return False
                End If
            Else
                'ORACLE
                sqlBuilder.Append("select COLUMN_NAME  FROM DBA_CONS_COLUMNS WHERE  COLUMN_NAME = '")
                sqlBuilder.Append(ColumnName)
                sqlBuilder.Append("' AND OWNER = '")
                sqlBuilder.Append(oracleOwner)
                sqlBuilder.Append("' AND TABLE_NAME = '")
                sqlBuilder.Append(TableName)
                sqlBuilder.Append("' AND CONSTRAINT_NAME IN ")

                sqlBuilder.Append("(SELECT CONSTRAINT_NAME FROM DBA_CONSTRAINTS WHERE OWNER = '")
                sqlBuilder.Append(oracleOwner)
                sqlBuilder.Append("' AND CONSTRAINT_TYPE = 'U' AND TABLE_NAME = '")
                sqlBuilder.Append(TableName)
                sqlBuilder.Append("')")
                If Not String.IsNullOrEmpty(Server.Con.ExecuteScalar(CommandType.Text, sqlBuilder.ToString)) Then
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            RaiseEvent ErrorOccurred(ex, "Unique", String.Empty, sqlBuilder.ToString, "[Error al obtener el Script del Unique]")
            Return False

        End Try
    End Function


    ''' <summary>
    ''' Obtiene El nombre de Check si existe en base
    ''' </summary>
    ''' <param name="CkName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetCheckText(ByVal TableName As String, ByVal ColumnName As String) As Boolean
        Dim sqlBuilder As New StringBuilder()
        ColumnName = ColumnName.Replace("[", String.Empty)
        ColumnName = ColumnName.Replace("]", String.Empty)

        If Not Server.isOracle Then
            sqlBuilder.Append("select text from syscomments where id in ")
            sqlBuilder.Append("(")
            sqlBuilder.Append(" select id from sysobjects where xtype = 'C' and parent_obj = ")
            sqlBuilder.Append(" (select id from sysobjects where xtype = 'U' and name =  '")
            sqlBuilder.Append(TableName)
            sqlBuilder.Append("')")
            sqlBuilder.Append(" )")

            Try
                Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString)

                For Each r As DataRow In ds.Tables(0).Rows
                    If r.Item(0).ToString.ToUpper.Split("[")(1).StartsWith(ColumnName.ToUpper) Then Return True
                Next
                Return False
            Catch ex As Exception
                RaiseEvent ErrorOccurred(ex, "Unique", String.Empty, sqlBuilder.ToString, "[Error al obtener el Script del Check]")
                Return False
            End Try
        Else
            'ORACLE
            sqlBuilder.Append("SELECT CONSTRAINT_NAME FROM DBA_CONSTRAINTS WHERE OWNER = '")
            sqlBuilder.Append(oracleOwner)
            sqlBuilder.Append("' AND CONSTRAINT_TYPE = 'C' AND TABLE_NAME = '")
            sqlBuilder.Append(TableName)
            sqlBuilder.Append("'")
            Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString)
            Dim Constraints As New List(Of String)

            For Each r As DataRow In ds.Tables(0).Rows
                If Not r.Item(0).ToString.ToUpper.StartsWith("SYS") Then
                    Constraints.Add(r.Item(0).ToString)
                End If
            Next

            If Constraints.Count = 0 Then Return False

            sqlBuilder.Remove(0, sqlBuilder.Length)
            sqlBuilder.Append("select COLUMN_NAME  FROM DBA_CONS_COLUMNS WHERE  COLUMN_NAME = '")
            sqlBuilder.Append(ColumnName)
            sqlBuilder.Append("' AND OWNER = '")
            sqlBuilder.Append(oracleOwner)
            sqlBuilder.Append("' AND TABLE_NAME = '")
            sqlBuilder.Append(TableName)
            sqlBuilder.Append("' AND (")

            For Each Item As String In Constraints
                sqlBuilder.Append(" CONSTRAINT_NAME = '" & Item & "'")
                sqlBuilder.Append(" OR ")
            Next
            sqlBuilder.Remove(sqlBuilder.Length - 3, 3)
            sqlBuilder.Append(")")




            Try
                If Not String.IsNullOrEmpty(Server.Con.ExecuteScalar(CommandType.Text, sqlBuilder.ToString)) Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                RaiseEvent ErrorOccurred(ex, "Unique", String.Empty, sqlBuilder.ToString, "[Error al obtener el Script del Check]")
                Return False
            End Try
        End If
    End Function

#End Region

#Region "Validate"

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Verifica el tipo de una Columna contra uno pasado por parámetro.
    ''' </summary>
    ''' <param name="columnName">Nombre de la columna</param>
    ''' <param name="tableName">Nombre de la Tabla</param>
    '''<param name="columnType">Tipo a comparar de la columna</param>
    ''' <returns>True si el tipo de la Columna coincide con el pasado por parámetro.</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Alejandro]	14/01/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function IsColumnType(ByVal columnName As String, ByVal tableName As String, ByVal columnType As String) As Boolean

        Dim tmpColumnType As String = GetColumnType(columnName, tableName)
        'If Server.isOracle Then
        '    columnType = Column.GetOracleType(columnType, True)
        'End If
        If columnType.Contains("(") Then
            columnType = columnType.Remove(columnType.IndexOf("("), columnType.Length - columnType.IndexOf("("))
        End If

        If String.Compare(tmpColumnType, columnType, True) = 0 Then
            Return True
        End If

        If String.Compare(columnType, "uniqueidentifier ROWGUIDCOL") = 0 AndAlso String.Compare(tmpColumnType, "uniqueidentifier") = 0 Then

            Return True
            '[Gaston] Codigo comentado
            'ElseIf (Server.isOracle) Then

            '    If ((tmpColumnType.ToUpper() = "FLOAT") AndAlso (columnType.ToUpper() = "NUMBER")) Then
            '        precition = -2
            '        Return True
            '        ' Si el tipo de dato de la columna de Oracle es NVARCHAR2 y el tipo de dato de la columna de SQL es VARCHAR2 no hacer la conversión
            '        ' de la columna de Oracle de NVARCHAR2 A VARCHAR2
            '    ElseIf ((tmpColumnType.ToUpper() = "NVARCHAR2") AndAlso (columnType.ToUpper() = "VARCHAR2")) Then
            '        precition = -2
            '        Return True
            '    End If

            'End If

        End If

        Return False

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Compara la declaración null/not-null de una columna contra una declaración pasada por parámetro
    ''' </summary>
    ''' <param name="columnName">Nombre de la columna</param>
    ''' <param name="tableName">Nombre de la Tabla</param>
    ''' <param name="columnIsNullable">Declaración null/not-null a comparar de la Columna</param>
    ''' <returns>True si la declaración de la columna y la pasada por parámetro coinciden.</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Alejandro]	14/01/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function IsColumnNullable(ByVal columnName As String, ByVal tableName As String, ByVal columnIsNullable As Boolean) As Boolean
        'Public Shared Function IsColumnNullable(ByVal columnName As String, ByVal tableName As String, ByVal columnIsNullable As Boolean, ByVal userId As Int32) As Boolean
        'Dim tmpIsColumnNullable As Boolean = GetColumnIsNullable(columnName, tableName, userId)
        Dim tmpIsColumnNullable As Boolean = GetColumnIsNullable(columnName, tableName)
        If tmpIsColumnNullable = columnIsNullable Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Valida si el valor por defecto de la columna y el valor pasado por parámetro son coincidentes.
    ''' </summary>
    ''' <param name="columnName">Nombre de la columna</param>
    ''' <param name="tableName">Nombre de la Tabla</param>
    ''' <param name="columnDefaultData">Valor por defecto esperado de la columna.</param>
    ''' <returns>True si los valores son coincidentes.</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Alejandro]	14/01/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function IsColumnDefaultData(ByVal columnName As String, ByVal tableName As String, ByVal columnDefaultData As String) As Boolean
        'Public Shared Function IsColumnDefaultData(ByVal columnName As String, ByVal tableName As String, ByVal columnDefaultData As String, ByVal userId As Int32) As Boolean
        'Dim tmpColumnDefaultData As String = GetColumnDefaultData(columnName, tableName, userId)
        Dim tmpColumnDefaultData As String = GetColumnDefaultData(columnName, tableName)
        If Not IsNumeric(tmpColumnDefaultData) Then
            GetCorrectDefaultData(tmpColumnDefaultData)
        End If
        If String.Compare(tmpColumnDefaultData.TrimEnd, columnDefaultData.TrimEnd) = 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Compara el Length de una Columna contra un Length pasado por parametro.
    ''' </summary>
    ''' <param name="column">Columna</param>
    ''' <param name="tableName">Nombre de la Tabla</param>
    ''' <returns>True si los Lentgh coinciden</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Alejandro]	14/01/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function IsColumnLength(ByVal column As Column, ByVal tableName As String) As Boolean
        Dim Precition_Scale As Dictionary(Of String, Int32)
        Precition_Scale = GetColumnLength(column.Name, tableName)
        If Server.isOracle = False Then
            Dim ColumnType As String = column.Type(Server.isOracle)
            If ColumnType.ToUpper <> "DATETIME" _
    AndAlso ColumnType.ToUpper <> "INT" _
    AndAlso ColumnType.ToUpper <> "BIT" _
    AndAlso ColumnType.ToUpper <> "MONEY" _
    AndAlso ColumnType.ToUpper <> "BIGINT" _
    AndAlso ColumnType.ToUpper <> "SMALLINT" _
    AndAlso ColumnType.ToUpper <> "FLOAT" _
    AndAlso ColumnType.ToUpper <> "IMAGE" _
    AndAlso ColumnType.ToUpper <> "TINYINT" _
    AndAlso ColumnType.ToUpper <> "SMALLDATETIME" _
    AndAlso ColumnType.ToUpper <> "DOUBLE" _
    AndAlso ColumnType.ToUpper <> "NTEXT" _
    AndAlso ColumnType.ToUpper <> "UNIQUEIDENTIFIER" Then

                If Precition_Scale.Item("PRECITION") >= column.Precition Then
                    If Precition_Scale.Item("SCALE") = column.Scale Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Else
                Return True
            End If
        Else
            'ORACLE
            Dim ColumnType As String = column.Type(Server.isOracle)

            If ColumnType.ToUpper <> "DATE" AndAlso ColumnType.ToUpper <> "CLOB" AndAlso ColumnType.ToUpper <> "BLOB" Then

                If ColumnType.ToUpper = "VARCHAR2" Or ColumnType.ToUpper = "CHAR" Then
                    If Precition_Scale.Item("LENGHT") = column.length Then Return True
                End If

                ' Si el tipo de columna es number y Precition_Scale es -1 se coloca un number 18
                If (ColumnType.Contains("NUMBER") AndAlso (Precition_Scale.Item("PRECITION") = -1)) Then
                    Precition_Scale.Item("PRECITION") = 18
                End If

                If Precition_Scale.Item("PRECITION") >= column.length Then
                    If Precition_Scale.Item("SCALE") = column.Scale Then Return True
                End If

            Else
                'Es un tipo de datos sin longitud
                Return True
            End If
        End If

        Return False
    End Function

#End Region

#Region "General"
    Private Shared Sub GetCorrectDefaultData(ByRef sDefaultData As String)
        DBFixerParser.TrimStartString(sDefaultData)
        sDefaultData = sDefaultData.Trim
        If sDefaultData.StartsWith("'") Then
            sDefaultData = sDefaultData.Remove(0, 1)
        End If
        If sDefaultData.EndsWith("'") Then
            sDefaultData = sDefaultData.Remove(sDefaultData.Length - 1, 1)
        End If
    End Sub

#End Region

End Class


Public Enum EnumColumnsTypesSQLServer
    t_bigint = 127
    t_binary = 173
    t_bit = 104
    t_char = 175
    t_datetime = 61
    t_decimal = 106
    t_float = 62
    t_image = 34
    t_int = 56
    t_money = 60
    t_nchar = 239
    t_ntext = 99
    t_numeric = 108
    t_nvarchar = 231
    t_real = 59
    t_smalldatetime = 58
    t_smallint = 52
    t_smallmoney = 122
    t_sql_variant = 98
    'Se saca este tipo porque tiene el mismo numero que nvarchar y trae
    'conflictos en el proceso de comparación
    't_sysname = 231
    t_text = 35
    t_timestamp = 189
    t_tinyint = 48
    t_uniqueidentifier = 36
    t_varbinary = 165
    t_varchar = 167
End Enum

