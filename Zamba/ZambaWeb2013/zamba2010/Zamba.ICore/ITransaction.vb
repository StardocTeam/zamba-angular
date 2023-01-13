Public Interface ITransaction
    Property Con As IConnection
    Property Transaction As IDbTransaction
    Sub Commit()
    Sub Dispose()
    Sub Rollback()
End Interface
