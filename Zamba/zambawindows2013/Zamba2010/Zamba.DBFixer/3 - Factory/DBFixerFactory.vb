Imports Zamba.Servers
Imports System.Text

''' <summary>
''' Clase que se encarga de las ejecucion de las consultas para los arreglos de la base de datos
''' </summary>
''' <history>
''' Clase que se encarga de la creacion y modificacion de los archivos de la base de datos
''' Cada archivo tiene su propia clase (TAble, Column, etc.)
''' </history>
''' <remarks></remarks>
Public Class DBFixerFactory
    Public Class FixTable

        ''' <summary>
        ''' Agrega una Columna a una Tabla.
        ''' </summary>
        ''' <param name="tableName">Nombre de la Tabla.</param>
        ''' <param name="columnName">Nombre de la Columna.</param>
        ''' <param name="columnType">Tipo de la Columna.</param>
        ''' <param name="columnIsNull">Definición NULL/NOT NULL de la Columna.</param>
        ''' <param name="sQuery">String por referencia donde se almacenará la consulta.</param>
        ''' <param name="columnPrecition"></param>
        ''' <param name="columnScale"></param>
        ''' <param name="defaultData"></param>
        ''' <history> [Alejandro]	02/2008	Created 
        '''           [Marcelo]     28/04/2008 Modified
        '''           [Gaston]      24/06/2008 Modified
        ''' </history>
        ''' 
        Public Shared Sub AddColumn(ByVal tableName As String, ByVal columnName As String, ByVal columnType As String, ByVal columnIsNull As Boolean, ByRef sQuery As String, Optional ByVal columnPrecition As Int32 = -1, Optional ByVal columnScale As Integer = -1, Optional ByVal defaultData As String = "")
            Dim sqlBuilder As New StringBuilder

            If Server.isOracle Then
                sqlBuilder.Append("ALTER TABLE ")
                sqlBuilder.Append(Chr(34))
                sqlBuilder.Append(tableName)
                sqlBuilder.Append(Chr(34))
                sqlBuilder.Append(" ADD ")
                sqlBuilder.Append(Chr(34))
                sqlBuilder.Append(columnName)
                sqlBuilder.Append(Chr(34))
                sqlBuilder.Append(" ")
                sqlBuilder.Append(columnType)

                If columnPrecition <> -1 AndAlso Not columnType.StartsWith("DATE") _
                AndAlso Not columnType.Contains("(") AndAlso Not columnType.Contains(")") Then

                    sqlBuilder.Append("(" & columnPrecition)

                    If columnScale <> -1 Then
                        sqlBuilder.Append(",")
                        sqlBuilder.Append(columnScale)
                    End If

                    sqlBuilder.Append(")")

                End If

                If String.IsNullOrEmpty(defaultData) = False Then
                    sqlBuilder.Append(" DEFAULT ")
                    If columnType.ToUpper = "VARCHAR" Or columnType.ToUpper = "NVARCHAR" Or columnType.ToUpper = "VARCHAR2" Or columnType.ToUpper = "NVARCHAR2" Then
                        sqlBuilder.Append("'")
                        sqlBuilder.Append(defaultData)
                        sqlBuilder.Append("'")
                    Else
                        sqlBuilder.Append(defaultData)
                    End If
                ElseIf columnIsNull Then
                    sqlBuilder.Append(" NULL")
                Else
                    sqlBuilder.Append(" NOT NULL")
                End If
            Else
                sqlBuilder.Append("ALTER TABLE ")
                'sqlBuilder.Append(frmZDBFixer.userName & ".")
                sqlBuilder.Append("[")
                sqlBuilder.Append(tableName)
                sqlBuilder.Append("] ADD [")
                sqlBuilder.Append(columnName)
                sqlBuilder.Append("] ")
                sqlBuilder.Append(columnType)
                If columnPrecition <> -1 AndAlso columnType.ToUpper <> "DATETIME" _
                                         AndAlso columnType.ToUpper <> "INT" _
                                         AndAlso columnType.ToUpper <> "BIT" _
                                         AndAlso columnType.ToUpper <> "MONEY" _
                                         AndAlso columnType.ToUpper <> "BIGINT" _
                                         AndAlso columnType.ToUpper <> "SMALLINT" _
                                         AndAlso columnType.ToUpper <> "FLOAT" _
                                         AndAlso columnType.ToUpper <> "IMAGE" _
                                         AndAlso columnType.ToUpper <> "TINYINT" _
                                         AndAlso columnType.ToUpper <> "SMALLDATETIME" _
                                         AndAlso columnType.ToUpper <> "DOUBLE" _
                                         AndAlso columnType.ToUpper <> "NTEXT" _
                                         AndAlso columnType.ToUpper <> "UNIQUEIDENTIFIER" Then
                    sqlBuilder.Append("(" & columnPrecition.ToString)
                    If columnScale <> -1 Then
                        sqlBuilder.Append(",")
                        sqlBuilder.Append(columnScale)
                    End If
                    sqlBuilder.Append(")")
                End If

                If columnIsNull Then
                    sqlBuilder.Append(" NULL")
                Else
                    sqlBuilder.Append(" NOT NULL")
                    If String.IsNullOrEmpty(defaultData) = False Then
                        sqlBuilder.Append(" Default ")
                        If columnType.ToUpper = "VARCHAR" Or columnType.ToUpper = "NVARCHAR" Or columnType.ToUpper = "VARCHAR2" Or columnType.ToUpper = "NVARCHAR2" Then
                            sqlBuilder.Append("'")
                            sqlBuilder.Append(defaultData)
                            sqlBuilder.Append("'")
                        Else
                            sqlBuilder.Append(defaultData)
                        End If

                    End If
                End If
            End If

            sQuery = sqlBuilder.ToString()

            Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
        End Sub

        ''' <summary>
        ''' Crea una Tabla en la Base.
        ''' </summary>
        ''' <param name="tblName">Nombre de la Tabla.</param>
        ''' <param name="tblColumns">Colección de Columnas de la Tabla.</param>
        ''' <param name="sQuery">String por referencia donde se almacenará la consulta.</param>
        ''' <history> [Alejandro]	02/2008	Created </history>
        Public Shared Sub CreateTable(ByVal tblName As String, ByVal tblColumns As Generic.List(Of Column), ByRef sQuery As String)

            Dim sqlBuilder As New StringBuilder()
            Dim isOneColumnTable As Boolean = Not (tblColumns.Count <> 1)
            If Server.isOracle Then
                sqlBuilder.Append("CREATE TABLE ")
                sqlBuilder.Append(Chr(34))
                sqlBuilder.Append(tblName)
                sqlBuilder.Append(Chr(34))
                sqlBuilder.Append(" (")
                For Each col As Column In tblColumns
                    sqlBuilder.Append(Chr(34))
                    sqlBuilder.Append(col.Name)
                    sqlBuilder.Append(Chr(34))
                    sqlBuilder.Append(" ")
                    sqlBuilder.Append(col.Type(True))
                    If Not col.Type(True).ToUpper.Contains("DATE") _
                    AndAlso Not col.Type(True).ToUpper.Contains("BLOB") _
                    AndAlso Not col.Type(True).ToUpper.Contains("CLOB") _
                    AndAlso Not col.Type(True).ToUpper.Contains("NUMBER") Then
                        sqlBuilder.Append("(")
                        If col.Precition <> 0 Then sqlBuilder.Append(col.Precition)
                        If col.Scale <> -1 Then
                            sqlBuilder.Append(",")
                            sqlBuilder.Append(col.Scale)
                        End If
                        sqlBuilder.Append(")")
                    Else
                        If col.Type(True).ToUpper.Contains("NUMBER") AndAlso Not col.Type(True).Contains("(") AndAlso Not col.Type(True).Contains(")") Then
                            sqlBuilder.Append("(")
                            If col.Precition <> 0 Then sqlBuilder.Append(col.Precition)
                            If col.Scale <> -1 Then
                                sqlBuilder.Append(",")
                                sqlBuilder.Append(col.Scale)
                            End If
                            sqlBuilder.Append(")")
                        End If
                    End If
                    If Not String.IsNullOrEmpty(col.DefaultData) Then
                        sqlBuilder.Append(" DEFAULT ")
                        sqlBuilder.Append(GetDefaultValue(col.DefaultData))
                    End If
                    If col.getIsNull() Then
                        sqlBuilder.Append(" NULL")
                    Else
                        sqlBuilder.Append(" NOT NULL")
                    End If

                    'If its not one column AndAlso its not last column to append
                    If Not isOneColumnTable AndAlso tblColumns.IndexOf(col) <> tblColumns.Count - 1 Then
                        sqlBuilder.Append(", ")
                    End If
                Next
                sqlBuilder.Append(")")
            Else
                sqlBuilder.Append("CREATE TABLE [")
                sqlBuilder.Append(tblName)
                sqlBuilder.Append("] (")
                Dim columnType As String
                For Each col As Column In tblColumns
                    columnType = col.Type(False)

                    sqlBuilder.Append("[")
                    sqlBuilder.Append(col.Name)
                    sqlBuilder.Append("] ")
                    sqlBuilder.Append(columnType)
                    If col.Precition <> -1 AndAlso columnType.ToUpper <> "DATETIME" _
                    AndAlso columnType.ToUpper <> "INT" _
                    AndAlso columnType.ToUpper <> "BIT" _
                    AndAlso columnType.ToUpper <> "MONEY" _
                    AndAlso columnType.ToUpper <> "BIGINT" _
                    AndAlso columnType.ToUpper <> "SMALLINT" _
                    AndAlso columnType.ToUpper <> "FLOAT" _
                    AndAlso columnType.ToUpper <> "IMAGE" _
                    AndAlso columnType.ToUpper <> "TINYINT" _
                    AndAlso columnType.ToUpper <> "SMALLDATETIME" _
                    AndAlso columnType.ToUpper <> "DOUBLE" _
                    AndAlso columnType.ToUpper <> "NTEXT" _
                    AndAlso columnType.ToUpper <> "UNIQUEIDENTIFIER" Then
                        sqlBuilder.Append("(")
                        sqlBuilder.Append(col.Precition)
                        If col.Scale <> -1 Then
                            sqlBuilder.Append(",")
                            sqlBuilder.Append(col.Scale)
                        End If
                        sqlBuilder.Append(")")
                    End If

                    ' Se coloca (1,1) porque por lo general es (1,1). Sin embargo, puede existir la posibilidad de que el identity tenga otros 
                    ' valores...
                    If (col.Identity = True) Then
                        sqlBuilder.Append(" IDENTITY (1, 1)")
                    End If

                    If (col.Identity_replication = True) Then
                        sqlBuilder.Append(" NOT FOR REPLICATION")
                    End If

                    If col.getIsNull() Then
                        sqlBuilder.Append(" NULL")
                    Else
                        sqlBuilder.Append(" NOT NULL")
                    End If
                    If Not IsNothing(col.DefaultData) Then
                        sqlBuilder.Append(" DEFAULT ")
                        If String.Compare(GetDefaultValue(col.DefaultData), String.Empty) = 0 Then
                            sqlBuilder.Append("('')")
                        Else
                            sqlBuilder.Append(GetDefaultValue(col.DefaultData))
                        End If
                    End If
                    'If its not one column AndAlso its not last column to append
                    If Not isOneColumnTable AndAlso tblColumns.IndexOf(col) <> tblColumns.Count - 1 Then
                        sqlBuilder.Append(", ")
                    End If
                Next
                sqlBuilder.Append(")")
            End If

            sQuery = sqlBuilder.ToString()

            Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
        End Sub

    End Class

    ''' <summary>
    ''' Método que sirve para arreglar la columna
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	23/06/2008	Modified    Se agrego validación para detectar DATE(0)
    ''' </history>
    Public Class FixColumn

        ''' <summary>
        ''' Modifica el Tipo de una Columna en la Base.
        ''' </summary>
        ''' <param name="columnName">Nombre de la Columna.</param>
        ''' <param name="columnType">Tipo de la Columna.</param>
        ''' <param name="columnPrecition">Longitud de la Columna.</param>
        ''' <param name="tableName">Nombre de la Tabla que posee la Columna.</param>
        ''' <param name="sQuery">String por referencia donde se almacenará la consulta.</param>
        ''' <returns>Cantidad de Filas afectadas.</returns>
        ''' <history> [Alejandro]	02/2008	Created </history>
        Public Shared Function AlterType(ByVal columnName As String, ByVal columnType As String, ByVal columnPrecition As Int32, ByVal columnScale As Int32, ByVal tableName As String, ByRef sQuery As String) As Int32
            Dim sqlBuilder As New StringBuilder

            If Server.isOracle Then
                sqlBuilder.Append("ALTER TABLE ")
                sqlBuilder.Append(Chr(34))
                sqlBuilder.Append(tableName)
                sqlBuilder.Append(Chr(34))
                sqlBuilder.Append(" MODIFY ")
                sqlBuilder.Append(Chr(34))
                sqlBuilder.Append(columnName)
                sqlBuilder.Append(Chr(34))
                sqlBuilder.Append(" ")
                sqlBuilder.Append(columnType)
                If Not columnType.Contains("(") AndAlso Not columnType.Contains(")") Then
                    If columnPrecition <> -1 Then
                        sqlBuilder.Append("(")
                        sqlBuilder.Append(columnPrecition)
                        If columnScale <> -1 Then
                            sqlBuilder.Append(",")
                            sqlBuilder.Append(columnScale)
                        End If
                        sqlBuilder.Append(")")
                    End If
                End If
            Else
                ' Hay que agregar el Nombre de usuario
                sqlBuilder.Append("ALTER TABLE ")
                'sqlBuilder.Append(frmZDBFixer.userName & ".")
                sqlBuilder.Append("[")
                sqlBuilder.Append(tableName)
                sqlBuilder.Append("] ALTER COLUMN [")
                sqlBuilder.Append(columnName)
                sqlBuilder.Append("] ")
                sqlBuilder.Append(columnType)
                If columnPrecition <> -1 AndAlso columnType.ToUpper <> "DATETIME" _
AndAlso columnType.ToUpper <> "INT" _
AndAlso columnType.ToUpper <> "BIT" _
AndAlso columnType.ToUpper <> "MONEY" _
AndAlso columnType.ToUpper <> "BIGINT" _
AndAlso columnType.ToUpper <> "SMALLINT" _
AndAlso columnType.ToUpper <> "FLOAT" _
AndAlso columnType.ToUpper <> "IMAGE" _
AndAlso columnType.ToUpper <> "TINYINT" _
AndAlso columnType.ToUpper <> "SMALLDATETIME" _
AndAlso columnType.ToUpper <> "DOUBLE" _
AndAlso columnType.ToUpper <> "NTEXT" _
AndAlso columnType.ToUpper <> "UNIQUEIDENTIFIER" Then
                    sqlBuilder.Append("(")
                    sqlBuilder.Append(columnPrecition)
                    If columnScale <> -1 Then
                        sqlBuilder.Append(",")
                        sqlBuilder.Append(columnScale)
                    End If
                    sqlBuilder.Append(")")
                End If
            End If

            If (sqlBuilder.ToString().Contains("DATE(0)")) Then
                sQuery = sqlBuilder.ToString()
                sQuery = sQuery.Remove(sQuery.IndexOf("(0)"))
                sqlBuilder.Remove(0, sqlBuilder.ToString.Length)
                sqlBuilder.Append(sQuery)
            End If

            Return Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())

        End Function

        ''' <summary>
        ''' Modifica la Longitud de una columna.
        ''' </summary>
        ''' <param name="columnName">Nombre de la Columna.</param>
        ''' <param name="columnType">Tipo de la Columna.</param>
        ''' <param name="columnPrecition">Longitud de la Columna.</param>
        ''' <param name="columnScale"></param>
        ''' <param name="columnIsNull">Definición NULL/NOT NULL de la columna.</param>
        ''' <param name="tableName">Nombre de la Tabla que posee la Columna.</param>
        ''' <param name="sQuery">String por referencia donde se almacenará la consulta.</param>
        ''' <returns>Cantidad de Filas afectadas.</returns>
        ''' <history> [Alejandro]	02/2008	Created </history>
        Public Shared Function AlterLength(ByVal columnName As String, ByVal columnType As String, ByVal columnPrecition As Int32, ByVal columnScale As Int32, ByVal columnIsNull As Boolean, ByVal tableName As String, ByRef sQuery As String) As Int32

            Dim sqlBuilder As New StringBuilder()

            If Server.isOracle Then

                sqlBuilder.Append("ALTER TABLE ")
                sqlBuilder.Append(Chr(34))
                sqlBuilder.Append(tableName)
                sqlBuilder.Append(Chr(34))
                sqlBuilder.Append(" MODIFY ")
                sqlBuilder.Append(Chr(34))
                sqlBuilder.Append(columnName)
                sqlBuilder.Append(Chr(34))
                sqlBuilder.Append(" ")
                sqlBuilder.Append(columnType)
                If Not columnType.Contains("(") AndAlso Not columnType.Contains(")") Then
                    sqlBuilder.Append("(")
                    '                    If columnPrecition <> 0 AndAlso Not columnType.Contains("NUMBER") Then
                    If columnPrecition <> 0 Then
                        sqlBuilder.Append(columnPrecition)
                        If columnScale <> -1 Then
                            sqlBuilder.Append(",")
                            sqlBuilder.Append(columnScale)
                        End If
                    End If
                    sqlBuilder.Append(")")
                End If

                'Comente esto porque tira exception, igual no me parece que sea necesario modificar los null en el metodo para
                'modificar longitud de columna, de cualquier forma se mantiene el null o not null de la columna

                'If columnIsNull Then
                '    sqlBuilder.Append(" NULL")
                'Else
                '    sqlBuilder.Append(" NOT NULL")
                'End If

            Else
                sqlBuilder.Append("ALTER TABLE ")
                'sqlBuilder.Append(frmZDBFixer.userName & ".")
                sqlBuilder.Append("[")
                sqlBuilder.Append(tableName)
                sqlBuilder.Append("] ALTER COLUMN [")
                sqlBuilder.Append(columnName)
                sqlBuilder.Append("] ")
                sqlBuilder.Append(columnType)
                If columnPrecition <> -1 AndAlso columnType.ToUpper <> "DATETIME" _
    AndAlso columnType.ToUpper <> "INT" _
    AndAlso columnType.ToUpper <> "BIT" _
    AndAlso columnType.ToUpper <> "MONEY" _
    AndAlso columnType.ToUpper <> "BIGINT" _
    AndAlso columnType.ToUpper <> "SMALLINT" _
    AndAlso columnType.ToUpper <> "FLOAT" _
    AndAlso columnType.ToUpper <> "IMAGE" _
    AndAlso columnType.ToUpper <> "TINYINT" _
    AndAlso columnType.ToUpper <> "SMALLDATETIME" _
    AndAlso columnType.ToUpper <> "DOUBLE" _
    AndAlso columnType.ToUpper <> "NTEXT" _
    AndAlso columnType.ToUpper <> "UNIQUEIDENTIFIER" Then


                    sqlBuilder.Append("(")
                    sqlBuilder.Append(columnPrecition)
                    If columnScale <> -1 Then
                        sqlBuilder.Append(",")
                        sqlBuilder.Append(columnScale)
                    End If
                    sqlBuilder.Append(")")
                End If
                If columnIsNull Then
                    sqlBuilder.Append(" NULL")
                Else
                    sqlBuilder.Append(" NOT NULL")
                End If
            End If

            sQuery = sqlBuilder.ToString()

            Return Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())

        End Function

        ''' <summary>
        ''' Modifica la definición NULL/NOT NULL de una Columna en la Base.
        ''' </summary>
        ''' <param name="columnName">Nombre de la Columna.</param>
        ''' <param name="columnType">Tipo de la Columna.</param>
        ''' <param name="columnPrecition">Precision de la Columna.</param>
        ''' <param name="columnIsNull">Definición NULL/NOT NULL de la Columna.</param>
        ''' <param name="tableName">Nombre de la Tabla que posee la Columna.</param>
        ''' <param name="sQuery">String por referencia donde se almacenará la consulta.</param>
        ''' <param name="columnDefaultValue">Valor por Default de la Columna.</param>
        ''' <returns>Cantidad de Filas afectadas.</returns>
        ''' <history> [Alejandro]	02/2008	Created 
        ''' 	      [Marcelo] 	15/05/2008	Modified
        ''' </history>
        Public Shared Function AlterIsNull(ByVal columnName As String, ByVal columnType As String, ByVal columnPrecition As Int32, ByVal columnScale As Int32, ByVal columnIsNull As Boolean, ByVal tableName As String, ByRef sQuery As String, Optional ByVal columnDefaultValue As String = "") As Int32
            Dim sqlBuilder As New StringBuilder()

            If Server.isOracle Then

                columnName = columnName
                columnType = columnType
                tableName = tableName

                sqlBuilder.Append("ALTER TABLE ")
                sqlBuilder.Append(Chr(34))
                sqlBuilder.Append(tableName)
                sqlBuilder.Append(Chr(34))
                sqlBuilder.Append(" MODIFY ")
                sqlBuilder.Append(Chr(34))
                sqlBuilder.Append(columnName)
                sqlBuilder.Append(Chr(34))
                sqlBuilder.Append(" ")
                sqlBuilder.Append(columnType)
                If Not columnType.StartsWith("DATE") _
                 AndAlso Not columnType.Contains("(") AndAlso Not columnType.Contains(")") Then
                    If columnPrecition <> -1 Then
                        sqlBuilder.Append("(")
                        sqlBuilder.Append(columnPrecition)
                        If columnScale <> -1 Then
                            sqlBuilder.Append(",")
                            sqlBuilder.Append(columnScale)
                        End If
                        sqlBuilder.Append(")")
                    End If
                End If
                If columnIsNull Then
                    sqlBuilder.Append(" NULL")
                Else
                    sqlBuilder.Append(" NOT NULL")
                End If
            Else
                sqlBuilder.Append("ALTER TABLE ")
                'sqlBuilder.Append(frmZDBFixer.userName & ".")
                sqlBuilder.Append("[")
                sqlBuilder.Append(tableName)
                sqlBuilder.Append("] ALTER COLUMN [")
                sqlBuilder.Append(columnName)
                sqlBuilder.Append("] ")
                sqlBuilder.Append(columnType)
                If columnPrecition <> -1 AndAlso columnType.ToUpper <> "DATETIME" _
AndAlso columnType.ToUpper <> "INT" _
AndAlso columnType.ToUpper <> "BIT" _
AndAlso columnType.ToUpper <> "MONEY" _
AndAlso columnType.ToUpper <> "BIGINT" _
AndAlso columnType.ToUpper <> "SMALLINT" _
AndAlso columnType.ToUpper <> "FLOAT" _
AndAlso columnType.ToUpper <> "IMAGE" _
AndAlso columnType.ToUpper <> "TINYINT" _
AndAlso columnType.ToUpper <> "SMALLDATETIME" _
AndAlso columnType.ToUpper <> "DOUBLE" _
AndAlso columnType.ToUpper <> "NTEXT" _
AndAlso columnType.ToUpper <> "UNIQUEIDENTIFIER" Then
                    sqlBuilder.Append("(")
                    sqlBuilder.Append(columnPrecition)
                    If columnScale <> -1 Then
                        sqlBuilder.Append(",")
                        sqlBuilder.Append(columnScale)
                    End If
                    sqlBuilder.Append(")")
                End If

                If columnIsNull Then
                    sqlBuilder.Append(" NULL")
                Else
                    sqlBuilder.Append(" NOT NULL")
                End If
            End If


            'sQuery = sqlBuilder.ToString()
            Return Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
        End Function

        ''' <summary>
        ''' Modifica el Valor Default de una Columna en la Base.
        ''' </summary>
        ''' <param name="columnName">Nombre de la Columna.</param>
        ''' <param name="columnDefaultValue">Valor por Default de la columna.</param>
        ''' <param name="tableName">Nombre de la Tabla donde se encuentra la Columna.</param>
        ''' <param name="sQuery">String por referencia donde se almacenará la consulta.</param>
        ''' <returns>Cantidad de Filas afectadas.</returns>
        ''' <history> [Alejandro]	02/2008	Created </history>
        Public Shared Function AlterDefault(ByVal columnName As String, ByVal columnDefaultValue As String, ByVal tableName As String, ByRef sQuery As String) As Int32
            Dim sqlBuilder As New StringBuilder()

            If Server.isOracle Then
                sqlBuilder.Append("ALTER TABLE ")
                sqlBuilder.Append(Chr(34))
                sqlBuilder.Append(tableName)
                sqlBuilder.Append(Chr(34))
                sqlBuilder.Append(" MODIFY (")
                sqlBuilder.Append(Chr(34))
                sqlBuilder.Append(columnName)
                sqlBuilder.Append(Chr(34))
                sqlBuilder.Append(" DEFAULT ")
                sqlBuilder.Append(GetDefaultValue(columnDefaultValue))
                sqlBuilder.Append(" )")
            Else
                sqlBuilder.Append("ALTER TABLE ")
                'sqlBuilder.Append(frmZDBFixer.userName & ".")
                sqlBuilder.Append("[")
                sqlBuilder.Append(tableName)
                sqlBuilder.Append("] ADD CONSTRAINT DF_")
                sqlBuilder.Append(tableName)
                sqlBuilder.Append("_")
                sqlBuilder.Append(columnName)
                sqlBuilder.Append(" DEFAULT (")
                sqlBuilder.Append(GetDefaultValue(columnDefaultValue))
                sqlBuilder.Append(")")
                sqlBuilder.Append(" FOR [")
                sqlBuilder.Append(columnName)
                sqlBuilder.Append("]")
            End If

            sQuery = sqlBuilder.ToString()

            Return Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
        End Function
    End Class

    Public Class FixGeneral

        ''' <summary>
        ''' Crea una PrimaryKey en la Base.
        ''' </summary>
        ''' <param name="tableName">Nombre de la Tabla donde se creara la clave.</param>
        ''' <param name="columnName">Nombre de la Columna donde se creará la clave.</param>
        ''' <param name="sQuery">String por referencia donde se almacenará la consulta.</param>
        ''' <history> [Alejandro]	02/2008	Created </history>
        Public Shared Sub CreatePrimaryKey(ByVal tableName As String, ByVal columnName As List(Of String), ByRef sQuery As String)
            Dim sqlBuilder As New StringBuilder()
            If Server.isOracle Then
                sqlBuilder.AppendLine("ALTER TABLE ")
                sqlBuilder.Append(Chr(34))
                sqlBuilder.Append(tableName)
                sqlBuilder.Append(Chr(34))
                sqlBuilder.Append(" ADD CONSTRAINT PK_")
                sqlBuilder.Append(tableName)

                'Agregado Diego: Para poder agregar mas de una PK
                sqlBuilder.Append(" primary key  (")
                For Each column As String In columnName
                    sqlBuilder.Append(Chr(34))
                    sqlBuilder.Append(column)
                    sqlBuilder.Append(Chr(34))
                    sqlBuilder.Append(",")
                Next
                sqlBuilder.Remove(sqlBuilder.Length - 1, 1)
                sqlBuilder.Append(")")
            Else
                sqlBuilder.AppendLine("ALTER TABLE ")
                'sqlBuilder.Append(frmZDBFixer.userName & ".")
                sqlBuilder.Append("[" & tableName & "]")
                sqlBuilder.AppendLine(" ADD")
                sqlBuilder.Append("CONSTRAINT PK_")
                sqlBuilder.Append(tableName)


                'Agregado Diego: Para poder agregar mas de una PK

                sqlBuilder.Append(" primary key  (")
                For Each column As String In columnName
                    sqlBuilder.Append(column)
                    sqlBuilder.Append(",")
                Next
                sqlBuilder.Remove(sqlBuilder.Length - 1, 1)
                sqlBuilder.Append(")")
            End If

            sQuery = sqlBuilder.ToString()
            Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString)
        End Sub

        ''' <summary>
        ''' Crea una ForeignKey en la Base.
        ''' </summary>
        ''' <param name="baseTableName">Nombre de la Tabla Base de la relación.</param>
        ''' <param name="baseColumnNames">Nombres de la Columnas Base de la relación.</param>
        ''' <param name="refTableName">Nombre de la Tabla de Referencia de la relación.</param>
        ''' <param name="refColumnNames">Nombres de la Columnas de Referencia de la relación.</param>
        ''' <param name="sQuery">String por referencia donde se almacenará la consulta.</param>
        ''' <history> [Alejandro]	02/2008	Created </history>
        Public Shared Sub CreateForeingKey(ByVal objname As String, ByVal baseTableName As String, ByVal baseColumnNames As List(Of String), ByVal refTableName As String, ByVal refColumnNames As List(Of String), ByVal _OnUpdateCascade As Boolean, ByVal _OnDeleteCascade As Boolean, ByVal _OnCheckForReplication As Boolean, ByRef sQuery As String)
            Dim sqlBuilder As New StringBuilder()
            If Server.isOracle Then
                sqlBuilder.Append("FK_")
                sqlBuilder.Append(baseTableName)
                sqlBuilder.Append("_")
                sqlBuilder.Append(refTableName)
                sqlBuilder.Append("_")
                sqlBuilder.Append(baseColumnNames(0))
                Dim constraintName As String = sqlBuilder.ToString
                If constraintName.Length > 28 Then
                    'cortando el nombre de objeto pueden quedar objetos con el mismo nombre
                    constraintName = constraintName.Remove(26, constraintName.Length - 26)
                    constraintName = constraintName & frmZDBFixer.repeatedObject
                    frmZDBFixer.repeatedObject += 1
                End If


                sqlBuilder.Remove(0, sqlBuilder.Length)
                sqlBuilder.Append("ALTER TABLE ")
                sqlBuilder.Append(Chr(34))
                sqlBuilder.Append(baseTableName)
                sqlBuilder.Append(Chr(34))
                sqlBuilder.AppendLine(" ADD")
                sqlBuilder.Append("CONSTRAINT ")
                sqlBuilder.AppendLine(constraintName)
                sqlBuilder.Append("foreign key (")


                For Each bcolumname As String In baseColumnNames
                    If baseColumnNames.Count > 1 Then
                        sqlBuilder.Append(Chr(34))
                        sqlBuilder.Append(bcolumname)
                        sqlBuilder.Append(Chr(34))
                        sqlBuilder.Append(",")
                    Else
                        sqlBuilder.Append(Chr(34))
                        sqlBuilder.Append(bcolumname)
                        sqlBuilder.Append(Chr(34))
                    End If
                Next
                If baseColumnNames.Count > 1 Then
                    sqlBuilder.Remove(sqlBuilder.Length - 1, 1)
                End If

                sqlBuilder.AppendLine(")")
                sqlBuilder.Append("references ")
                sqlBuilder.Append(Chr(34))
                sqlBuilder.Append(refTableName)
                sqlBuilder.Append(Chr(34))
                sqlBuilder.Append(" (")
                For Each rcolumname As String In refColumnNames
                    If refColumnNames.Count > 1 Then
                        sqlBuilder.Append(Chr(34))
                        sqlBuilder.Append(rcolumname)
                        sqlBuilder.Append(Chr(34))
                        sqlBuilder.Append(",")
                    Else
                        sqlBuilder.Append(Chr(34))
                        sqlBuilder.Append(rcolumname)
                        sqlBuilder.Append(Chr(34))
                    End If
                Next
                If refColumnNames.Count > 1 Then
                    sqlBuilder.Remove(sqlBuilder.Length - 1, 1)
                End If
                sqlBuilder.Append(")")

                If _OnDeleteCascade Then sqlBuilder.Append(" ON DELETE CASCADE")

            Else
                '******************** SQL *************************
                If Not String.IsNullOrEmpty(objname) Then
                    'Es una foreign key de tipo constraint
                    sqlBuilder.Append("ALTER TABLE ")
                    'sqlBuilder.Append(frmZDBFixer.userName & ".")
                    sqlBuilder.Append("[")
                    sqlBuilder.Append(baseTableName)
                    sqlBuilder.AppendLine("] ADD")
                    sqlBuilder.Append("CONSTRAINT ")
                    sqlBuilder.Append(objname)

                    sqlBuilder.Append(" foreign key (")

                    For Each bcolumname As String In baseColumnNames
                        If baseColumnNames.Count > 1 Then
                            sqlBuilder.Append(bcolumname)
                            sqlBuilder.Append(",")
                        Else
                            sqlBuilder.Append(bcolumname)
                        End If
                    Next
                    If baseColumnNames.Count > 1 Then
                        sqlBuilder.Remove(sqlBuilder.Length - 1, 1)
                    End If

                    sqlBuilder.AppendLine(")")
                    sqlBuilder.Append("references ")
                    'sqlBuilder.Append(frmZDBFixer.userName & ".")
                    sqlBuilder.Append(refTableName)
                    sqlBuilder.Append(" (")

                    For Each rcolumname As String In refColumnNames
                        If refColumnNames.Count > 1 Then
                            sqlBuilder.Append(rcolumname)
                            sqlBuilder.Append(",")
                        Else
                            sqlBuilder.Append(rcolumname)
                        End If
                    Next
                    If refColumnNames.Count > 1 Then
                        sqlBuilder.Remove(sqlBuilder.Length - 1, 1)
                    End If

                    sqlBuilder.Append(")")

                    If _OnDeleteCascade Then sqlBuilder.Append(" ON DELETE CASCADE")
                    If _OnUpdateCascade Then sqlBuilder.Append(" ON UPDATE CASCADE")
                    If _OnCheckForReplication = False Then sqlBuilder.Append(" NOT FOR REPLICATION ")

                Else
                    'Es una foreign key a nivel tabla
                    'Es una foreign key de tipo constraint
                    sqlBuilder.Append("ALTER TABLE ")
                    'sqlBuilder.Append(frmZDBFixer.userName & ".")
                    sqlBuilder.Append("[")
                    sqlBuilder.Append(baseTableName)
                    sqlBuilder.AppendLine("] ADD")
                    sqlBuilder.Append(" foreign key (")

                    For Each bcolumname As String In baseColumnNames
                        If baseColumnNames.Count > 1 Then
                            sqlBuilder.Append(bcolumname)
                            sqlBuilder.Append(",")
                        Else
                            sqlBuilder.Append(bcolumname)
                        End If
                    Next
                    If baseColumnNames.Count > 1 Then
                        sqlBuilder.Remove(sqlBuilder.Length - 1, 1)
                    End If

                    sqlBuilder.AppendLine(")")
                    sqlBuilder.Append("references ")
                    'sqlBuilder.Append(frmZDBFixer.userName & ".")
                    sqlBuilder.Append(refTableName)
                    sqlBuilder.Append(" (")

                    For Each rcolumname As String In refColumnNames
                        If refColumnNames.Count > 1 Then
                            sqlBuilder.Append(rcolumname)
                            sqlBuilder.Append(",")
                        Else
                            sqlBuilder.Append(rcolumname)
                        End If
                    Next
                    If refColumnNames.Count > 1 Then
                        sqlBuilder.Remove(sqlBuilder.Length - 1, 1)
                    End If

                    sqlBuilder.Append(")")

                    If _OnDeleteCascade Then sqlBuilder.Append(" ON DELETE CASCADE")
                    If _OnUpdateCascade Then sqlBuilder.Append(" ON UPDATE CASCADE")
                    If _OnCheckForReplication = False Then sqlBuilder.Append(" NOT FOR REPLICATION ")

                End If
            End If

            Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
        End Sub

        ''' <summary>
        ''' Crea una Vista en la Base.
        ''' </summary>
        ''' <param name="vText">Script de la Vista.</param>
        ''' <history> [Alejandro]	02/2008	Created </history>
        Public Shared Sub CreateView(ByVal vText As String)
            'If Not Server.isOracle Then
            Server.Con.ExecuteNonQuery(CommandType.Text, vText)
            'Else
            ' PARA ORACLE HAY QUE CAMBIAR LA SINTAXIS DE LOS SCRIPTS EN PALABRAS RESERVADAS TALES COMO 
            ' GETDATE Y TOP, 
            '    Dim viewtext As String = String.Empty
            '    Dim rownumexpression As String = String.Empty

            '    For Each item As String In vText.ToUpper.Split(" ")
            '        If item.ToUpper.StartsWith("GETDATE()") Then
            '            If item.Contains(Chr(13)) Then item = item.ToUpper.Split(Chr(13))(0)
            '            If item.ToUpper.Replace("GETDATE()", String.Empty).Trim.Contains(Chr(3)) Then
            '                If IsNumeric(item.ToUpper.Replace("GETDATE()", String.Empty).Trim.Split(Chr(13))(0)) Then
            '                    item = item.ToUpper.Replace("GETDATE()", "(GETDATE " & item.ToUpper.Replace("GETDATE()", String.Empty).Trim.Split(Chr(13))(0) & ")")
            '                Else
            '                    item = item.ToUpper.Replace("GETDATE()", "SYSDATE")
            '                End If
            '            End If
            '        End If
            '        viewtext += item
            '    Next

            '    Server.Con.ExecuteNonQuery(CommandType.Text, vText)
            'End If
        End Sub

        ''' <summary>
        ''' Se crea un procedimiento almacenado, paquete o cuerpo de paquete en la base de datos que corresponda (SQL o Oracle)
        ''' </summary>
        ''' <param name="elemText"></param>
        ''' <remarks></remarks>
        ''' <history>
        ''' 	[Gaston]	13/06/2008	Created
        ''' </history>
        Public Shared Sub createStoredProcedure(ByVal elemText As String)
            Server.Con.ExecuteNonQuery(CommandType.Text, elemText)
        End Sub

        '''' <summary>
        '''' Crea un Stored Procedure en la base.
        '''' </summary>
        '''' <param name="spText">SQL: Texto del SP. Oracle: Encabezado del Paquete.</param>
        '''' <param name="spText2">Solo Oracle: parametro que contempla el cuerpo del Paquete.</param>
        '''' <history> [Alejandro]	02/2008	Created </history>
        'Public Shared Sub CreateStoredProcedure(ByVal spText As String, Optional ByVal spText2 As String = "")
        '    If Server.isSQLServer Then
        '        Server.Con.ExecuteNonQuery(CommandType.Text, spText)
        '    Else
        '        Server.Con.ExecuteNonQuery(CommandType.Text, "CREATE " & spText)
        '        If Not String.IsNullOrEmpty(spText2) Then
        '            Server.Con.ExecuteNonQuery(CommandType.Text, "CREATE " & spText2)
        '        End If
        '    End If
        'End Sub

        ''' <summary>
        ''' Crea un trigger en base
        ''' </summary>
        ''' <param name="tgName">Nombre del trigger</param>
        ''' <param name="tableTo">Tabla a la cual se aplica</param>
        ''' <param name="tgAplication">En que Evento se aplica</param>
        ''' <param name="tgText">El texto contenido en el Trigger</param>
        ''' <remarks>Diego</remarks>
        Public Shared Sub CreateTrigger(ByVal tgName As String, ByVal tableTo As String, ByVal tgAplication As String, ByVal tgText As String)
            Dim query As New StringBuilder
            If Server.isOracle Then
                'query.Append("CREATE TRIGGER ")
                'query.Append(tgName)
                'query.Append(" AFTER (")
                'query.Append(tgAplication)
                'query.Append(") ON ")
                'query.Append(tableTo)
                'query.AppendLine(" ")
                'query.Append(tgText)
            Else
                query.Append("CREATE TRIGGER ")
                query.Append(tgName)
                query.Append(" ON ")
                query.Append(tableTo)
                query.Append(" FOR ")
                query.Append(tgAplication)
                query.Append(" AS ")
                query.Append(tgText)
            End If
            Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString())
        End Sub


        ''' <summary>
        ''' Crea una restriccion unique en la base
        ''' </summary>
        ''' <param name="uqName"></param>
        ''' <param name="uqColumnsNames"></param>
        ''' <remarks></remarks>
        Public Shared Sub CreateUnique(ByVal tableName As String, ByVal uqName As String, ByVal uqColumnsNames As List(Of String))
            Dim query As New StringBuilder
            If Server.isOracle Then
                query.Append("ALTER TABLE ")
                query.Append(tableName)
                query.AppendLine(" ADD")
                query.Append("CONSTRAINT ")
                query.Append(uqName)
                query.Append(" UNIQUE (")

                For Each bcolumname As String In uqColumnsNames
                    If uqColumnsNames.Count > 1 Then
                        query.Append(bcolumname)
                        query.Append(",")
                    Else
                        query.Append(bcolumname)
                    End If
                Next
                If uqColumnsNames.Count > 1 Then query.Remove(query.Length - 1, 1)
                query.Append(")")
            Else
                query.Append("ALTER TABLE ")
                query.Append("[")
                query.Append(tableName)
                query.AppendLine("] ADD")
                query.Append("CONSTRAINT ")
                query.Append(uqName)
                query.Append(" UNIQUE (")

                For Each bcolumname As String In uqColumnsNames
                    If uqColumnsNames.Count > 1 Then
                        query.Append(bcolumname)
                        query.Append(",")
                    Else
                        query.Append(bcolumname)
                    End If
                Next
                If uqColumnsNames.Count > 1 Then query.Remove(query.Length - 1, 1)
                query.Append(")")
            End If
            Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString())
        End Sub


        Public Shared Sub CreateCheck(ByVal tableName As String, ByVal CkName As String, ByVal CheckExpression As String, ByVal NotForReplication As Boolean)
            Dim query As New StringBuilder
            If Server.isOracle Then
                If Not String.IsNullOrEmpty(CkName) Then
                    CkName = CkName.Replace("[", String.Empty)
                    CkName = CkName.Replace("]", String.Empty)
                End If
                CheckExpression = CheckExpression.Replace("[", String.Empty)
                CheckExpression = CheckExpression.Replace("]", String.Empty)
                If Not String.IsNullOrEmpty(CkName) Then

                    'Es un check de tipo Constraint
                    query.Append("ALTER TABLE ")
                    query.Append(tableName)
                    query.AppendLine(" ADD")
                    query.Append("CONSTRAINT ")
                    query.Append(CkName)
                    query.Append(" CHECK ")
                    If NotForReplication Then
                        query.Append(" NOT FOR REPLICATION (")
                    Else
                        query.Append("(")
                    End If
                    query.Append(CheckExpression)
                    query.Append(")")
                Else
                    'Es un check de tipo Tabla ( no tiene nombre en el script y no es constraint)
                    query.Append("ALTER TABLE ")
                    query.Append(tableName)
                    query.AppendLine(" ADD CHECK ")
                    If NotForReplication Then
                        query.Append(" NOT FOR REPLICATION (")
                    Else
                        query.Append("(")
                    End If
                    query.Append(CheckExpression)
                    query.Append(")")
                End If

            Else
                If Not String.IsNullOrEmpty(CkName) Then
                    'Es un check de tipo Constraint
                    'ALTER TABLE [dbo].[DISK_GROUP] ADD 
                    'CONSTRAINT [CK__DISK_GROU] CHECK ([DISK_GROUP_ID] is not null),
                    query.Append("ALTER TABLE ")
                    query.Append("[")
                    query.Append(tableName)
                    query.AppendLine("] ADD")
                    query.Append("CONSTRAINT ")
                    query.Append(CkName)
                    query.Append(" CHECK ")
                    If NotForReplication Then
                        query.Append(" NOT FOR REPLICATION (")
                    Else
                        query.Append("(")
                    End If
                    query.Append(CheckExpression)
                    query.Append(")")
                Else
                    'Es un check de tipo Tabla ( no tiene nombre en el script y no es constraint)
                    'ALTER TABLE [dbo].[VERREG] ADD 
                    'CHECK ([ID] is not null)
                    query.Append("ALTER TABLE ")
                    query.Append("[")
                    query.Append(tableName)
                    query.AppendLine("] ADD CHECK ")

                    If NotForReplication Then
                        query.Append(" NOT FOR REPLICATION (")
                    Else
                        query.Append("(")
                    End If
                    query.Append(CheckExpression)
                    query.Append(")")
                End If
            End If
            'cuando se quiere agregar un Check en una columna not null se producen exceptions
            Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString())
        End Sub


    End Class

    Private Shared FunctionNames As String = "CURRENT_DATE;DBTIMEZONE"

    ''' <summary>
    ''' Obtiene la declaración correcta para el Valor Default (caso numérico, caso no numérico)
    ''' </summary>
    ''' <param name="sDefaultValue">Valor a Parsear</param>
    ''' <history> [Alejandro]	02/2008	Created </history>
    Public Shared Function GetDefaultValue(ByVal sDefaultValue As String) As String
        Dim sdefaultTemp As String = sDefaultValue
        sdefaultTemp = sdefaultTemp.Replace("(", String.Empty)
        sdefaultTemp = sdefaultTemp.Replace(")", String.Empty)
        'Si no es un String vacío: caso erróneo
        If String.IsNullOrEmpty(sDefaultValue) Then
            Return sDefaultValue
        End If

        'Si no termina con "()": caso de funciones en T-SQL, en el cual se declara sin apóstrofos
        If sDefaultValue.EndsWith("()") Then
            Return sDefaultValue
        End If

        'Si no es numérico: caso en el cual se declara sin apóstrofos
        If IsNumeric(sDefaultValue) Or IsNumeric(sdefaultTemp) Then
            Return sDefaultValue
        End If

        'Si no es numérico negativo: caso en el cual se declara sin apóstrofos
        If sDefaultValue.StartsWith("-") AndAlso IsNumeric(sDefaultValue.Remove(0, 1)) Then
            Return sDefaultValue
        End If

        'Por cada función en la constante FunctionNames, si no es una función (caso en el cual se declara sin apóstrofos)
        For Each functionN As String In FunctionNames.Split(";")
            If String.Compare(functionN, sDefaultValue) = 0 Then
                Return sDefaultValue
            End If
        Next

        'Entonces se devuelve con apóstrofos porque es una constante
        Return "'" & sDefaultValue & "'"

    End Function

    ''' <summary>
    ''' Borra los valores de una tabla que no correspondan con la creacion de una FK 
    ''' </summary>
    ''' <param name="tableName">La tabla de la que se borraran los datos</param>
    ''' <param name="columnName">Columna de la tabla por la que se van a filtrar los datos que no correspondan</param>
    ''' <param name="fkTableName">Tabla FK de que se van a buscar los valores que deberian estar en la tabla original</param>
    ''' <param name="fkColumnName">Columna FK de la tabla original por la que se van a buscar los valores que no deberian estar en la tabla</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteForeignValues(ByVal tableName As String, ByVal columnName As String, ByVal fkTableName As String, ByVal fkColumnName As String)

        Dim sqlBuilder As New StringBuilder()
        'If Server.isOracle Then

        'ElseIf Server.isSQLServer Then
        sqlBuilder.Append("delete from ")
        sqlBuilder.Append(fkTableName)
        sqlBuilder.Append(" where ")
        sqlBuilder.Append(fkColumnName)
        sqlBuilder.Append(" not in(select ")
        sqlBuilder.Append(columnName)
        sqlBuilder.Append(" from ")
        sqlBuilder.Append(tableName)
        sqlBuilder.Append(")")

        If fkColumnName.ToUpper() <> "INITIALSTEPID" Then
            Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
        End If
        'End If

    End Sub

    ''' <summary>
    ''' Obtiene todos los contraints de una tabla
    ''' </summary>
    ''' <param name="tableName"></param>
    ''' <returns>Dataset</returns>
    ''' <remarks></remarks>
    Public Shared Function GetTableConstraints(ByVal tableName As String) As DataSet
        Dim sqlBuilder As New StringBuilder()
        If Server.isOracle Then

        Else
            sqlBuilder.Append("select id,name from sysobjects where parent_obj =")
            sqlBuilder.Append("(select id from sysobjects where name = '")
            sqlBuilder.Append(tableName)
            sqlBuilder.Append("' and xtype = 'U')")
        End If
        Return Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())
    End Function

    'Public Shared Sub DeleteTableConstraints(ByVal tableName As String)
    '    Dim sqlBuilder As New StringBuilder()
    '    If Server.isOracle Then
    '        'TODO DIEGO; FALTA PARA ORACLE
    '        Throw New Exception("Falta Implementar para ORACLE")
    '    Else
    '        Dim DS As DataSet = GetTableConstraints(tableName)
    '        For Each Item As DataRow In DS.Tables(0).Rows
    '            DBFixerFactory.DeleteContraint(tableName, Item(1).ToString)
    '        Next
    '    End If
    'End Sub


    ''' <summary>
    ''' Borra las constraints que puedan tener las columnas 
    ''' </summary>
    ''' <param name="tablename"></param>
    ''' <param name="ColumnsNames"></param>
    '''  <history>
    ''' </history>
    ''' <remarks>
    ''' [Andres]	17/07/2008	Created
    '''</remarks>
    Public Shared Sub DeleteColumnsConstraints(ByVal tablename As String, ByVal ColumnsNames As List(Of String))
        For Each CurrentColumnName As String In ColumnsNames
            DeleteColumnConstraints(tablename, CurrentColumnName)
        Next
    End Sub
    ''' <summary>
    ''' Borra las constraints que pueda contener una columna
    ''' </summary>
    ''' <param name="tablename"></param>
    ''' <param name="ColumnName"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Marcelo]	15/05/2008	Modified - Se modificaron las consultas y se agrego un filtro por columna
    '''                                        Se estaban borrando todas las constraints de la tabla
    ''' </history>
    Public Shared Sub DeleteColumnConstraints(ByVal tablename As String, ByVal ColumnName As String)
        Dim sqlBuilder As New StringBuilder()
        Dim cName As String
        If Not Server.isOracle Then



            '***************************************Default Values*******************************************
            sqlBuilder.Append("SELECT name FROM sysobjects WHERE parent_obj in (select id from sysobjects where name like '")
            sqlBuilder.Append(tablename)
            sqlBuilder.Append("') and info in (SELECT colid FROM syscolumns WHERE Id in (select id from sysobjects where name like '")
            sqlBuilder.Append(tablename)
            sqlBuilder.Append("') and name = '")
            sqlBuilder.Append(ColumnName)
            sqlBuilder.Append("')")

            For Each Item As DataRow In Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString).Tables(0).Rows
                If Not IsDBNull(Item.Item(0)) Then
                    cName = Item.Item(0).ToString
                    If String.IsNullOrEmpty(cName) = False Then DeleteContraint(tablename, cName)
                End If
            Next


            '***************************************Foreign Keys*******************************************
            cName = String.Empty
            sqlBuilder.Remove(0, sqlBuilder.Length)
            sqlBuilder.Append("select object_name(sysobjects.id) as nombre from sysforeignkeys inner join sysobjects on sysforeignkeys.constid = sysobjects.id ")
            sqlBuilder.Append("inner join sysreferences on sysforeignkeys.constid = sysreferences.constid inner join syscolumns source ")
            sqlBuilder.Append("on sysforeignkeys.fkeyid = source.id and sysforeignkeys.fkey = source.colid inner join syscolumns dest ")
            sqlBuilder.Append("on sysforeignkeys.rkeyid = dest.id and sysforeignkeys.rkey = dest.colid where sysobjects.xtype = 'F' and object_name(source.id) in ('")
            sqlBuilder.Append(tablename)
            sqlBuilder.Append("') and (source.name in ('")
            sqlBuilder.Append(ColumnName)
            sqlBuilder.Append("') or dest.name in ('")
            sqlBuilder.Append(ColumnName)
            sqlBuilder.Append("'))")

            For Each Item As DataRow In Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString).Tables(0).Rows
                If Not IsDBNull(Item.Item(0)) Then
                    cName = Item.Item(0).ToString
                    If String.IsNullOrEmpty(cName) = False Then DeleteContraint(tablename, cName)
                End If
            Next


            '***************************************Primary Keys*******************************************
            Dim tableId As Int32 = DBFixerBusiness.GetTableId(tablename)

            sqlBuilder.Remove(0, sqlBuilder.Length)
            sqlBuilder.Append("select sysindexes.name from sysindexes where id in  ")
            sqlBuilder.Append(" (select sysindexKeys.id from sysindexKeys  inner join syscolumns on sysindexKeys.colid = syscolumns.colid  ")
            sqlBuilder.Append(" WHERE sysindexKeys.id = ")
            sqlBuilder.Append(tableId)
            sqlBuilder.Append(" AND syscolumns.id = ")
            sqlBuilder.Append(tableId)
            sqlBuilder.Append(" AND syscolumns.name = '")
            sqlBuilder.Append(ColumnName)
            sqlBuilder.Append("') ")
            sqlBuilder.Append(" and indid IN (select sysindexKeys.indid from sysindexKeys  inner join syscolumns on sysindexKeys.colid = syscolumns.colid  ")
            sqlBuilder.Append(" WHERE sysindexKeys.id =")
            sqlBuilder.Append(tableId)
            sqlBuilder.Append(" AND syscolumns.id = ")
            sqlBuilder.Append(tableId)
            sqlBuilder.Append(" AND syscolumns.name = '")
            sqlBuilder.Append(ColumnName)
            sqlBuilder.Append("')")

            For Each Item As DataRow In Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString).Tables(0).Rows
                If Not IsDBNull(Item.Item(0)) Then
                    cName = Item.Item(0).ToString
                    If Not String.IsNullOrEmpty(cName) AndAlso Not cName.StartsWith("_WA_") Then DeleteContraint(tablename, cName)
                End If
            Next








            'Dim tableId As Int32 = DBFixerBusiness.GetTableId(tablename)

            'sqlBuilder.Remove(0, sqlBuilder.Length)
            'sqlBuilder.Append("select id, name from sysobjects where parent_obj = ")
            'sqlBuilder.Append(tableId)
            'Dim dsConstraints As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString)

            'Dim cname As String
            'Dim rConstId As Int32
            'For Each r As DataRow In dsConstraints.Tables(0).Rows
            'cname = r.Item(1)
            'rConstId = r.Item(0)
            'If cname.StartsWith("PK") Then
            'sqlBuilder.Remove(0, sqlBuilder.Length)
            'sqlBuilder.Append("select count(*) from sysindexKeys ")
            'sqlBuilder.Append(" inner join syscolumns on sysindexKeys.colid = syscolumns.colid ")
            'sqlBuilder.Append(" WHERE sysindexKeys.id =")
            'sqlBuilder.Append(tableId)
            'sqlBuilder.Append(" AND syscolumns.id = ")
            'sqlBuilder.Append(tableId)
            'sqlBuilder.Append(" AND syscolumns.name = '")
            'sqlBuilder.Append(ColumnName)
            'sqlBuilder.Append("'")
            'If Server.Con.ExecuteScalar(CommandType.Text, sqlBuilder.ToString) > 0 Then
            '    'DeleteContraint(tablename, cname)
            'End If

            'ElseIf cname.StartsWith("FK") Then
            'sqlBuilder.Remove(0, sqlBuilder.Length)
            'sqlBuilder.Append("select count(*) from sysforeignkeys ")
            'sqlBuilder.Append(" inner join syscolumns on sysforeignkeys.fkey = syscolumns.colid ")
            'sqlBuilder.Append(" WHERE sysforeignkeys.constid =")
            'sqlBuilder.Append(rConstId)
            'sqlBuilder.Append(" AND syscolumns.id = ")
            'sqlBuilder.Append(tableId)
            'sqlBuilder.Append(" AND syscolumns.name = '")
            'sqlBuilder.Append(ColumnName)
            'sqlBuilder.Append("'")
            'If Server.Con.ExecuteScalar(CommandType.Text, sqlBuilder.ToString) > 0 Then
            '    'DeleteContraint(tablename, cname)
            'End If

            'ElseIf cname.StartsWith("DF") Then
            'sqlBuilder.Remove(0, sqlBuilder.Length)
            'sqlBuilder.Append("select count(*) from sysobjects inner join syscolumns on sysobjects.info = syscolumns.colid  where sysobjects.parent_obj =")
            'sqlBuilder.Append(tableId)
            'sqlBuilder.Append(" and syscolumns.id = ")
            'sqlBuilder.Append(tableId)
            'sqlBuilder.Append(" AND syscolumns.name = '")
            'sqlBuilder.Append(ColumnName)
            'sqlBuilder.Append("'")

            'If Server.Con.ExecuteScalar(CommandType.Text, sqlBuilder.ToString) > 0 Then
            '    DeleteContraint(tablename, cname)
            'End If
            'ElseIf cname.StartsWith("IX") Then
            ''Con indices no puedo filtrar por la columna actual asi que se elimina si se encuentra en la tabla
            'DeleteContraint(tablename, cname)
            'End If
            'Next
        Else
            'TODO ORACLE

            sqlBuilder.Append("select constraint_name  from user_cons_columns where table_name = '")
            sqlBuilder.Append(tablename)
            sqlBuilder.Append("' and column_name = '")
            sqlBuilder.Append(ColumnName)
            sqlBuilder.Append("'")


            Dim TempDs As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())
            sqlBuilder.Remove(0, sqlBuilder.Length)
            If Not IsNothing(TempDs) Then

                Dim CurrentConstraint As String
                For Each CurrentRow As DataRow In TempDs.Tables(0).Rows
                    CurrentConstraint = CurrentRow.Item(0).ToString()

                    Try
                        DeleteContraint(tablename, CurrentConstraint)
                    Catch ex As Exception
                        Dim constraint_type As String = Server.Con.ExecuteScalar(CommandType.Text, "select CONSTRAINT_TYPE from dba_constraints  where OWNER = '" & DBFixerValidations.oracleOwner & "' and  CONSTRAINT_NAME = '" & CurrentConstraint & "'")
                        If ex.Message.ToUpper.StartsWith("ORA-02273") AndAlso constraint_type = "P" Then
                            'se trata de eliminar una pk pero tiene fk referenciando la pk
                            'se va a eliminar todas las fk que referencien la constraint que se quiere eliminar
                            If Not IsNothing(GetRefConstraints(tablename, CurrentConstraint)) Then
                                For Each r As DataRow In GetRefConstraints(tablename, CurrentConstraint).Tables(0).Rows
                                    DeleteContraint(r.Item("TABLE_NAME"), r.Item("CONSTRAINT_NAME"))
                                Next
                            End If
                        End If
                    End Try
                    sqlBuilder.Remove(0, sqlBuilder.Length)
                Next
            End If
        End If
    End Sub

    Public Shared Function GetRefConstraints(ByVal basetableName As String, ByVal ConstraintName As String) As DataSet
        Dim query As New StringBuilder
        query.Append("select CONSTRAINT_NAME, TABLE_NAME  from dba_constraints  where OWNER = '")
        query.Append(DBFixerValidations.oracleOwner)
        query.Append("' and  R_CONSTRAINT_NAME = '")
        query.Append(ConstraintName)
        query.Append("'")
        Return Server.Con.ExecuteDataset(CommandType.Text, query.ToString)
    End Function



    ''' <summary>
    ''' Borra una constraint segun el nombre
    ''' </summary>
    ''' <param name="tableName"></param>
    ''' <param name="ConstraintName"></param>
    ''' <remarks></remarks>
    Private Shared Sub DeleteContraint(ByVal tableName As String, ByVal ConstraintName As String)
        Dim sqlBuilder As New StringBuilder()
        If Not Server.isOracle Then
            Try
                sqlBuilder.Append("alter table ")
                'sqlBuilder.Append(frmZDBFixer.userName & ".")
                sqlBuilder.Append(tableName)
                sqlBuilder.Append(" drop constraint ")
                sqlBuilder.Append(ConstraintName)
                Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString)
            Catch ex As System.Data.SqlClient.SqlException
                ' Si la constraint es un indice y no una primaryKey
                If ex.Number = 3728 Then
                    sqlBuilder.Remove(0, sqlBuilder.Length)
                    sqlBuilder.Append("DROP INDEX ")
                    sqlBuilder.Append(tableName)
                    sqlBuilder.Append(".")
                    sqlBuilder.Append(ConstraintName)
                    Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString)
                End If
                If ex.Number = 3725 Then
                    'Hay una referencia a la restricción 'PK__DOC_TYPE__6D823440'
                    'en la tabla 'WFDocument', restricción FOREIGN KEY 'FK_WFDocument_DOC_TYPE'
                    '. No se puede quitar la restricción. Consulte los errores anteriores.
                    Dim fkname As String = ex.Message
                    Dim TBLNAME As String = fkname.Split("'")(3)
                    fkname = fkname.Split("'")(5)
                    sqlBuilder.Remove(0, sqlBuilder.Length)
                    sqlBuilder.Append("alter table ")
                    sqlBuilder.Append(TBLNAME)
                    sqlBuilder.Append(" drop constraint ")
                    sqlBuilder.Append(fkname)
                    Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString)
                    DeleteContraint(tableName, ConstraintName)
                End If
            End Try
        Else
            'ORACLE
            sqlBuilder.Append("ALTER TABLE ")
            sqlBuilder.Append(tableName)
            sqlBuilder.Append(" DROP CONSTRAINT ")
            sqlBuilder.Append(ConstraintName)
            Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString)

        End If
    End Sub

    ''' <summary>
    ''' Obtiene el Id de la tabla Actual
    ''' </summary>
    ''' <param name="tablename"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetTableId(ByVal tablename As String) As Int32
        Dim query As New StringBuilder
        query.Append("select id from sysobjects where name = '")
        query.Append(tablename)
        query.Append("'")
        'query.Append("' and uid = ")
        'query.Append(frmZDBFixer.userId)
        'If Not IsDBNull(Server.Con.ExecuteScalar(CommandType.Text, query.ToString)) AndAlso Not IsNothing(Server.Con.ExecuteScalar(CommandType.Text, query.ToString)) Then
        Return Server.Con.ExecuteScalar(CommandType.Text, query.ToString)
        'Else
        'query.Remove(0, query.Length)
        'query.Append("select id from sysobjects where name = '")
        'query.Append(tablename)
        'query.Append("' and uid = 1")
        'Return Server.Con.ExecuteScalar(CommandType.Text, query.ToString)
        'End If
    End Function


    Friend Shared Sub ExecuteQuery(ByVal query As String)
        Server.Con.ExecuteNonQuery(CommandType.Text, query)
    End Sub

    ''' <summary>
    ''' Obtiene los indices segun una columna
    ''' </summary>
    ''' <param name="tablename">Nombre de la tabla</param>
    ''' <param name="columname">Nombre de la columna utilizada para busqueda</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>23-6-2008 Diego[Created]</history>
    Shared Function GetIndexsByColumn(ByVal tablename As String, ByVal columname As String) As List(Of Index)
        Dim query As New StringBuilder
        Dim _Index As Index = Nothing
        Dim IndexList As List(Of Index) = Nothing
        Dim dsIndex As DataSet = Nothing
        Dim columns As List(Of Column) = Nothing
        Dim column As Column = Nothing

        'Obtiene los nombres de los indices
        query.Append("select index_name from ALL_IND_COLUMNS where table_owner  = '")
        query.Append(DBFixerValidations.oracleOwner)
        query.Append("' and table_name = '")
        query.Append(tablename)
        query.Append("' AND Column_name = '")
        query.Append(columname)
        query.Append("' and index_name not like 'PK_%'")

        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, query.ToString)
        If IsNothing(ds) AndAlso ds.Tables(0).Rows.Count < 1 Then Return Nothing

        'Por cada Indice Busco sus columnas
        For Each r As DataRow In ds.Tables(0).Rows
            columns = Nothing
            query.Remove(0, query.Length)
            query.Append("select COLUMN_NAME,DESCEND from ALL_IND_COLUMNS where table_owner  = '")
            query.Append(DBFixerValidations.oracleOwner)
            query.Append("' and table_name = '")
            query.Append(tablename)
            query.Append("' AND Index_name = '")
            query.Append(r.Item("INDEX_NAME"))
            query.Append("' AND column_name not like 'SYS_%'")
            dsIndex = Server.Con.ExecuteDataset(CommandType.Text, query.ToString)

            'Agrego las columnas as una coleccion
            For Each r2 As DataRow In dsIndex.Tables(0).Rows
                column = New Column(r2.Item(0))
                'Orden (ASC,DESC)
                column.IndexOrder = r2.Item(1)
                If IsNothing(columns) Then columns = New List(Of Column)
                columns.Add(column)
            Next
            'Creo El indice
            _Index = New Index(columns)
            _Index.ObjName = r.Item("INDEX_NAME")
            If IsNothing(IndexList) Then IndexList = New List(Of Index)
            'Lo Agrego a una coleccion de indices
            IndexList.Add(_Index)

        Next
        Return IndexList
    End Function


    ''' <summary>
    ''' Borra Un indice, No Necesita el nombre de tabla
    ''' </summary>
    ''' <param name="IndexName">Nombre del Indice a borrar</param>
    ''' <remarks></remarks>
    ''' <history>23-6-2008 Diego[Created]</history>
    Public Shared Sub DeleteIndex(ByVal IndexName As String)
        Server.Con.ExecuteNonQuery(CommandType.Text, "DROP INDEX " & IndexName)
    End Sub

 
    ''' <summary>
    ''' Crea un Indice en Base
    ''' </summary>
    ''' <param name="ColumnsNamesAndSortOrder">Nombres de Columnas y Orden</param>
    ''' <param name="tableName">Nombre de Tabla</param>
    ''' <remarks></remarks>
    ''' <history>23-6-2008 Diego[Created]</history>
    Public Shared Sub CreateIndex(ByVal ColumnsNamesAndSortOrder As Dictionary(Of String, String), ByVal tableName As String)
        Dim query As New StringBuilder
        query.Append("CREATE INDEX IX_")
        query.Append(tableName)
        query.Append(" ON ")
        query.Append(tableName & "(")

        For Each columnName As String In ColumnsNamesAndSortOrder.Keys
            query.Append(columnName)
            query.Append(" " & ColumnsNamesAndSortOrder.Item(columnName))
            query.Append(",")
        Next
        query.Remove(query.Length - 1, 1)
        query.Append(")")
        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
    End Sub

End Class


