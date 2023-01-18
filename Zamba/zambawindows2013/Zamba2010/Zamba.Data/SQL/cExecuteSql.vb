''' <summary>
''' Objeto que ejecuta sentencias SQL
''' </summary>
''' <remarks></remarks>
''' <example>Dim obj as Zamba.Data.cExecuteSql
''' Dim DocTypeCount as int32
''' DocTypeCount = obj.ExecuteSQL(Scalar,"Select count(1) from doc_type)
''' obj.Dispose()
''' </example>
Public NotInheritable Class cExecuteSql
    Inherits Zamba.Core.ZClass


    Public Enum ReturnType
        Table = 1
        Scalar = 2
        NoValue = 3
    End Enum

    Private _sql As String
    Private _returntype As ReturnType
    Private _ds As DataSet

    Public Property SQL() As String
        Get
            Return _sql
        End Get
        Set(ByVal value As String)
            _sql = value
        End Set
    End Property
    Public Property QueryType() As ReturnType
        Get
            Return _returntype
        End Get
        Set(ByVal value As ReturnType)
            _returntype = value
        End Set
    End Property

    ''' <summary>
    ''' Metodo para ejecutar una sentencia SQL
    ''' </summary>
    ''' <param name="rettype">Tipo de consulta</param>
    ''' <param name="sql">Sentencia que se desea ejecutar</param>
    ''' <returns>Depende del tipo de consulta, puede devolver un dataset o un numero que representa la cantidad de filas actualizalas/borradas, o un escalar en caso de que la consulta devuelva un solo valor</returns>
    ''' <remarks></remarks>
    Public Function ExecuteSQL(ByVal rettype As ReturnType, ByVal sql As String) As Object
        Dim i As Int16 = 0
        _returntype = rettype
        _sql = sql
        Select Case QueryType
            Case ReturnType.NoValue
                i = Server.Con.ExecuteNonQuery(CommandType.Text, Me.SQL)
                Return i
            Case ReturnType.Scalar
                i = Server.Con.ExecuteNonQuery(CommandType.Text, Me.SQL)
                Return i
            Case ReturnType.Table
                _ds = New DataSet
                _ds = Server.Con.ExecuteDataset(CommandType.Text, Me.SQL)
        End Select
        Return _ds
    End Function


#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Overrides Sub Dispose()
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overloads Sub Dispose(ByVal disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _sql = Nothing
                _ds.Dispose()
                _ds = Nothing
            End If
            ' TODO: free shared unmanaged resources
        End If
        disposedValue = True
    End Sub

#End Region

End Class
