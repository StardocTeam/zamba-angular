Imports Zamba.Servers
Imports Zamba.Core

''' <sumary>
''' Facilita el uso de utilización de transacciones con Zamba.Server
''' </sumary>
Public Class Transaction

#Region "Atributos"
    Private _con As IConnection
    Private _transaction As IDbTransaction
#End Region

#Region "Propiedades"
    Public Property Con() As IConnection
        Get
            Return _con
        End Get
        Set(ByVal value As IConnection)
            _con = value
        End Set
    End Property

    Public Property Transaction() As IDbTransaction
        Get
            Return _transaction
        End Get
        Set(ByVal value As IDbTransaction)
            _transaction = value
        End Set
    End Property
#End Region

#Region "Constructores"

    ''' <summary>
    ''' Por defecto utilizara ReadUncommitted como nivel de Isolation
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        Try
            'Abre la conexión y crea la transacción
            _con = Server.Con(False, False)
            _con.Open()

            If Server.isOracle Then
                _transaction = _con.CN.BeginTransaction()
            Else
                _transaction = _con.CN.BeginTransaction(IsolationLevel.ReadCommitted)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Throw New Exception("Error al crear la transacción", ex)
            Dispose()
        End Try
    End Sub


    ''' <summary>
    ''' Recibe el nivel de Isolation como parametro
    ''' </summary>
    ''' <param name="level"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal level As IsolationLevel)
        Try
            'Abre la conexión y crea la transacción
            _con = Server.Con(False, False)
            _con.Open()
            _transaction = _con.CN.BeginTransaction(level)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Throw New Exception("Error al crear la transacción", ex)
            Dispose()
        End Try
    End Sub
#End Region

#Region "Métodos"
    ''' <summary>
    ''' Realiza rollback de la transacción
    ''' </summary>
    ''' <history>Tomas created 27/08/2009</history>
    Public Sub Rollback()
        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Aplicando Rollback")
            _transaction.Rollback()
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Rollback aplicado con éxito")
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al hacer rollback de las modificaciones")
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Realiza commit de la transacción
    ''' </summary>
    ''' <history>Tomas created 27/08/2009</history>
    Public Sub Commit()
        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Aplicando Commit")
            _transaction.Commit()
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Commit aplicado con éxito")

        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al intentar aplicar los cambios")
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Cierra la conexion que se encuentre abierta y realiza el dispose del objeto
    ''' </summary>
    ''' <history>Tomas created 27/08/2009</history>
    Public Sub Dispose()
        Try
            'Dispose de la transacción
            If Not IsNothing(_transaction) Then
                _transaction.Dispose()
                _transaction = Nothing
            End If

            'Cierra la conexión
            If Not IsNothing(Con) Then
                _con.Close()
                _con.dispose()
                _con = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

End Class
