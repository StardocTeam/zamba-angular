
Public Class Column

#Region "Atributos"
    Private _name As String
    Private _type As String
    Private _length As Int32 = -1
    Private _table As Table
    Private _IsNull As String
    Private _default As String
    Private _precition As Integer
    Private _scale As Integer = -1
    Private _identity As Boolean = False
    Private _identity_replication As Boolean = False
    'Variable Utilizada para el SortOrder en un indice ASC-DESC
    Private _IndexOrder As String
#End Region

#Region "Propiedades"
    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property
    Public Property Type(ByVal isOracle As Boolean) As String
        Get
            'Diferencia si es oracle o sql
            If isOracle = True Then
                'Todo hacer la conversion de sql a oracle
                Return _type
            Else
                Return _type
            End If
        End Get
        Set(ByVal value As String)
            _type = value
        End Set
    End Property


    Public Property Scale() As Integer
        Get
            Return _scale
        End Get
        Set(ByVal value As Integer)
            _scale = value
        End Set
    End Property


    Public Property Precition() As Integer
        Get
            Return _precition
        End Get
        Set(ByVal value As Integer)
            _precition = value
        End Set
    End Property

    Public Property length() As Integer
        Get
            Return _length
        End Get
        Set(ByVal value As Integer)
            _length = value
        End Set
    End Property
    'Propiedad Utilizada para el SortOrder en un indice ASC-DESC
    Public Property IndexOrder() As String
        Get
            Return _IndexOrder
        End Get
        Set(ByVal value As String)
            _IndexOrder = value
        End Set
    End Property


    Public Property Table() As Table
        Get
            Return _table
        End Get
        Set(ByVal value As Table)
            _table = value
        End Set
    End Property
    Public ReadOnly Property getIsNull() As Boolean
        Get
            If _IsNull.ToUpper() = "IS NULL" Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property
    Public WriteOnly Property setIsNull() As String
        Set(ByVal value As String)
            _IsNull = value
        End Set
    End Property
    Public Property DefaultData() As String
        Get
            Return _default
        End Get
        Set(ByVal value As String)
            _default = value
        End Set
    End Property
    ''' <summary>
    ''' Se agrego la propiedad Identity
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	17/06/2008	Created
    ''' </history>
    Public Property Identity() As Boolean
        Get
            Return _identity
        End Get
        Set(ByVal value As Boolean)
            _identity = value
        End Set
    End Property

    ''' <summary>
    ''' Se agrego la propiedad Identity_replication
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	17/06/2008	Created
    ''' </history>
    Public Property Identity_replication() As Boolean
        Get
            Return _identity_replication
        End Get
        Set(ByVal value As Boolean)
            _identity_replication = value
        End Set
    End Property
#End Region

#Region "Constructores"
    Public Sub New(ByVal name As String)
        'Si es oracle se evalua si la columna es una palabra reservada
        ' En caso de que sea se le agrega una string constante cosa de salvar esa cuestion
        If Zamba.Servers.Server.isOracle Then
            If Not IsExlusiveWord(name) Then
                _name = name
            Else
                _name = "C_" & name
            End If
        Else
            _name = name
        End If

    End Sub
    Public Sub New(ByVal name As String, ByVal table As Table)
        'Si es oracle se evalua si la columna es una palabra reservada
        ' En caso de que sea se le agrega una string constante cosa de salvar esa cuestion
        If Zamba.Servers.Server.isOracle Then
            If Not IsExlusiveWord(name) Then
                _name = name
            Else
                _name = "C_" & name
            End If
        Else
            _name = name
        End If

        _table = table
    End Sub
    Public Sub New(ByVal sName As String, ByVal bIsNull As Boolean, ByVal sType As String, ByVal iLength As Int16, Optional ByVal colTable As Table = Nothing)
        'Si es oracle se evalua si la columna es una palabra reservada
        ' En caso de que sea se le agrega una string constante cosa de salvar esa cuestion
        If Zamba.Servers.Server.isOracle Then
            If Not IsExlusiveWord(Name) Then
                _name = Name
            Else
                _name = "C_" & Name
            End If
        Else
            _name = Name
        End If

        _IsNull = bIsNull
        _type = sType
        _length = iLength
        _precition = iLength
        If Not IsNothing(colTable) Then
            _table = colTable
        End If
    End Sub
#End Region


    ''' <summary>
    ''' Obtiene el tipo de datos para oracle a partir de su equivalente en SQL Server.
    ''' </summary>
    ''' <param name="sSqlType">Tipo de datos Compatible en T-SQL.</param>
    ''' <param name="noLength">False si se desea incluír la longitud.</param>
    ''' <history> 
    '''    [Alejandro] 02/2008     Created 
    '''    [Gaston]    20/06/2008  Modified    Se agrego validación para el tipo de dato decimal
    ''' </history>
    Public Shared Function GetOracleType(ByVal sSqlType As String, ByVal length As Int16, ByRef col As Column, Optional ByVal noLength As Boolean = False) As String

        If 0 = String.Compare(sSqlType, "binary") Then
            If noLength Then
                Return "NUMBER"
            End If
            col.Precition = 1
            col.length = 0
            col.Scale = 0
            Return "NUMBER(1)"
        ElseIf 0 = String.Compare(sSqlType, "varbinary") Then
            If noLength Then
                Return "NUMBER"
            End If
            col.Precition = 1
            col.length = 1
            col.Scale = 0
            Return "NUMBER(1)"
        ElseIf 0 = String.Compare(sSqlType, "tinyint") Then
            If noLength Then
                Return "NUMBER"
            End If
            col.Precition = 3
            col.length = 3
            col.Scale = 0
            Return "NUMBER(3,0)"
        ElseIf 0 = String.Compare(sSqlType, "int") Then
            If noLength Then
                Return "NUMBER"
            End If
            col.Precition = 10
            col.length = 10
            col.Scale = 0
            Return "NUMBER(10,0)"
        ElseIf 0 = String.Compare(sSqlType, "bigint") Then
            If noLength Then
                Return "NUMBER"
            End If
            col.Precition = 19
            col.length = 19
            col.Scale = 0
            Return "NUMBER(19,0)"
        ElseIf 0 = String.Compare(sSqlType, "bit") Then
            If noLength Then
                Return "NUMBER"
            End If
            col.Precition = 1
            col.length = 1
            col.Scale = 0
            Return "NUMBER(1)"
        ElseIf 0 = String.Compare(sSqlType, "real") Then
            Return "FLOAT"
        ElseIf 0 = String.Compare(sSqlType, "datetime") Then
            Return "DATE"
        ElseIf 0 = String.Compare(sSqlType, "date") Then
            Return "DATE"
        ElseIf 0 = String.Compare(sSqlType, "smalldatetime") Then
            Return "DATE"
        ElseIf 0 = String.Compare(sSqlType, "image") Then
            Return "BLOB"
        ElseIf 0 = String.Compare(sSqlType, "money") Then
            If noLength Then
                Return "NUMBER"
            End If
            Return "NUMBER(12,0)"
        ElseIf 0 = String.Compare(sSqlType, "ntext") Then
            If noLength Then
                Return "VARCHAR2"
            End If
            Return "VARCHAR2(250)"
        ElseIf 0 = String.Compare(sSqlType, "varchar") Then
            'VARCHAR FUNCIONA UNICAMENTE HASTA 4000 CARACTERES
            If noLength Then
                Return "VARCHAR2"
            End If
            Return "VARCHAR2(250)"
        ElseIf 0 = String.Compare(sSqlType, "nvarchar") Then
            If noLength Then
                Return "VARCHAR2"
            End If
            Return "VARCHAR2(250)"
        ElseIf 0 = String.Compare(sSqlType, "smallint") Then
            col.Precition = 5
            col.length = 5
            col.Scale = 3
            Return "NUMBER(5,3)"
        ElseIf 0 = String.Compare(sSqlType, "smallmoney") Then
            Return "NUMBER"
        ElseIf 0 = String.Compare(sSqlType, "sql_variant") Then
            If noLength Then
                Return "VARCHAR2"
            End If
            Return "VARCHAR2(250)"
        ElseIf 0 = String.Compare(sSqlType, "sysname") Then
            If noLength Then
                Return "VARCHAR2"
            End If
            Return "VARCHAR2(250)"
        ElseIf 0 = String.Compare(sSqlType, "text") Then
            If noLength Then
                Return "VARCHAR2"
            End If
            Return "VARCHAR2(250)"
        ElseIf 0 = String.Compare(sSqlType, "numeric") Then
            If noLength Then
                Return "NUMBER"
            End If
            col.Precition = 12
            col.length = 12
            col.Scale = 0
            Return "NUMBER(12,0)"
        ElseIf 0 = String.Compare(sSqlType, "timestamp") Then
            Return "DATE"
        ElseIf 0 = String.Compare(sSqlType, "uniqueidentifier") Then
            Return "ROWID"
        ElseIf 0 = String.Compare(sSqlType, "rowguid") Then
            Return "ROWID"

            ' Si el tipo de dato es decimal
        ElseIf 0 = String.Compare(sSqlType, "decimal") Then
            If (noLength) Then
                Return ("NUMBER")
            Else
                Return ("NUMBER(" & col.Precition & "," & col.Scale & ")")
            End If

        Else
            Return sSqlType
        End If

        'No se utiliza el tipo NVARCHAR2 ya que reserva el doble de memoria que 
        'VARCHAR2 y su aplicación en la base de Zamba 

        'ElseIf 0 = String.Compare(sSqlType, "decimal") Then
        '    Return "NUMBER"

    End Function

    Private Function IsExlusiveWord(ByVal columnName As String) As Boolean
        Select Case columnName.ToUpper

            Case "EXCLUSIVE"
                Return True
            Case "VALUE"
                Return True
            Case "INITIAL"
                Return True

        End Select

        Return False
    End Function


End Class