Imports Zamba.Servers

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Paquetes
''' Class	 : Paquetes.ZPaq
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase Abstracta para paquetes. Todos los paquetes deben heredar de esta clase.
''' Hereda de Zclass
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan] 29/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public MustInherit Class ZPaq
    Inherits Zamba.Core.ZClass
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Listado de objetos posibles para cada motor de base de datos
    ''' </summary>
    ''' <remarks>
    ''' Los objetos pueden diferir para cada motor.
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Enum Tipo
        StoredProcedure = 0
        View = 1
        Table = 2
        Package = 3
        Package_Body = 4
        UserFunction = 5
        Trigger = 6
        DefaultValue = 7
        PrimaryKey = 8
        ForeingKey = 9
        Index = 10
        Check = 11
    End Enum
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Funcion que devuelve el nombre del objeto en base al motor con el que se este trabajando
    ''' </summary>
    ''' <param name="Tipo">Objetos disponibles</param>
    ''' <returns>Cadena con el nombre que utiliza el motor para referirce al objeto</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Shared Function GetobjectName(ByVal Tipo As Tipo) As String
        'Dim sTipo As String
        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            Select Case Tipo
                Case Tipo.DefaultValue
                    Return "D"
                Case Tipo.ForeingKey
                    Return "F"
                Case Tipo.PrimaryKey
                    Return "PK"
                Case Tipo.StoredProcedure
                    Return "P"
                Case Tipo.Table
                    Return "U"
                Case Tipo.Trigger
                    Return "TR"
                Case Tipo.UserFunction
                    Return "FN"
                Case Tipo.View
                    Return "V"
                Case Tipo.Check
                    Return "C"
                Case Else
                    Return Nothing
            End Select
        Else
            Select Case Tipo
                Case Tipo.Index
                    Return "INDEX"
                Case Tipo.Package
                    Return "PACKAGE"
                Case Tipo.Package_Body
                    Return "PACKAGE BODY"
                Case Tipo.StoredProcedure
                    Return "PROCEDURE"
                Case Tipo.Table
                    Return "TABLE"
                Case Tipo.Trigger
                    Return "TRIGGER"
                Case Tipo.UserFunction
                    Return "FUNCTION"
                Case Tipo.View
                    Return "VIEW"
                Case Else
                    Return Nothing
            End Select
        End If
    End Function
    Private Shared Function ObtenerNombreObjeto(ByVal Tipo As Tipo) As String
        Select Case Tipo
            Case Tipo.Index
                Return "INDEX"
            Case Tipo.Package
                Return "PACKAGE"
            Case Tipo.Package_Body
                Return "PACKAGE BODY"
            Case Tipo.StoredProcedure
                Return "PROCEDURE"
            Case Tipo.Table
                Return "TABLE"
            Case Tipo.Trigger
                Return "TRIGGER"
            Case Tipo.UserFunction
                Return "FUNCTION"
            Case Tipo.View
                Return "VIEW"
            Case Else
                Return Nothing
        End Select
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Funcion para conocer si un objeto existe o no en la base de datos
    ''' Si existe y CanDrop es True, entonces lo elimina
    ''' </summary>
    ''' <param name="name">Nombre del objeto</param>
    ''' <param name="Objeto">Tipo de objeto: Tabla, Vista, SP, trigger, etc.</param>
    ''' <param name="Candrop">Propiedad que determina si el objeto se debe eliminar</param>
    ''' <returns>True existe</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function IfExists(ByVal name As String, ByVal Objeto As Tipo, Optional ByVal Candrop As Boolean = False) As Boolean
        Dim sql As String
        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            sql = "Select count(*) from sysobjects Where lower(name)='" & name.Trim.ToLower & "' and xType='" & GetobjectName(Objeto) & "'"
        Else
            'sql = "Select count(*) from dba_objects Where object_type='" & Me.GetobjectName(Objeto) & "' and OBJECT_NAME='" & name.Trim & "'"
            sql = "Select count(*) from user_objects Where object_type='" & GetobjectName(Objeto) & "' and lower(OBJECT_NAME)='" & name.Trim.ToLower & "'"
        End If
        If Server.Con.ExecuteScalar(CommandType.Text, sql) > 0 Then
            If Candrop = True Then
                'sql = "DROP " & Me.GetobjectName(Objeto) & " " & name.Trim
                sql = "DROP " & ObtenerNombreObjeto(Objeto) & " " & name.Trim
                Server.Con.ExecuteNonQuery(CommandType.Text, sql)
            End If
            Return True
        Else
            Return False
        End If
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Esta propiedad dice si el Objeto se puede Destruir.
    ''' Por ejemplo las vistas, los Stored Procedure, Triggers, etc en el caso que existan se pueden destruir
    ''' ya que no eliminan datos.
    ''' Las Tablas NO se pueden destruir porque se perderian los datos.
    ''' Si el paquete crea una tabla, esta propiedad DEBE SER FALSE
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	28/06/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public MustOverride ReadOnly Property CanDrop() As Boolean
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Fecha en la que se creo el paquete
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	28/06/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public MustOverride ReadOnly Property CreateDate() As Date
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Version de Zamba para la cual se crea el paquete.
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	28/06/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public MustOverride ReadOnly Property ZVersion() As String
    Public Overridable ReadOnly Property EditDate() As Date
        Get
            Return Date.Parse("01/01/01")
        End Get
    End Property


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Verifica la existencia de una Tabla
    ''' </summary>
    ''' <param name="tabla">Nombre de la tabla que se desea verificar</param>
    ''' <returns>True Existe</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function ExisteTabla(ByVal tabla As String) As Boolean
        Dim str As New System.Text.StringBuilder, i As Int32
        tabla = tabla.ToUpper()
        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            str.Append("select count(*) from sysobjects where name = '")
            str.Append(tabla)
            str.Append("'")
            i = Servers.Server.Con.ExecuteScalar(CommandType.Text, str.ToString)
            If i = 0 Then Return False
            Return True
        Else
            str.Append("select count(*) from dba_tables where TABLE_NAME = '")
            str.Append(tabla)
            str.Append("'")
            i = Servers.Server.Con.ExecuteScalar(CommandType.Text, str.ToString)
            If i = 0 Then Return False
            Return True
        End If
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Verifica la existencia de una columna dentro de una tabla
    ''' </summary>
    ''' <param name="columna">Nombre de la columna</param>
    ''' <param name="tabla">Nombre de la Tabla</param>
    ''' <returns>True Existe</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function ExisteColumna(ByVal columna As String, ByVal tabla As String) As Boolean
        Dim str As New System.Text.StringBuilder, i As Int32
        columna = columna.ToUpper
        tabla = tabla.ToUpper
        If Server.ServerType = Server.DBTYPES.MSSQLServer OrElse Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
            str.Append("select count(*) from syscolumns where id = (select id from sysobjects where name = '")
            str.Append(tabla)
            str.Append("') and name = '")
            str.Append(columna)
            str.Append("'")
            i = Servers.Server.Con.ExecuteScalar(CommandType.Text, str.ToString)
            If i = 0 Then Return False
            Return True
        Else
            str.Append("select count(*) from dba_tab_columns where COLUMN_NAME = '")
            str.Append(columna)
            str.Append("' and TABLE_NAME='")
            str.Append(tabla)
            str.Append("'")
            i = Servers.Server.Con.ExecuteScalar(CommandType.Text, str.ToString)
            If i = 0 Then Return False
            Return True
        End If
    End Function

End Class
