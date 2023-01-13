Imports ZAMBA.Servers
Public Class ClsActiveLic
    Private Datos As New DsActiveLic


#Region "Propiedades"
    'Public Shared ReadOnly Property ConectionTime() As DateTime
    '    Get
    '        Return
    '    End Get
    'End Property
    Public ReadOnly Property LastAction() As Date
        Get
            Return Datos.DsActiveLic.Rows(0).Item(1)
        End Get
    End Property

    Public ReadOnly Property TimeOut() As Int32
        Get
            Return Datos.DsActiveLic.Rows(0).Item(2)
        End Get
    End Property
    Public ReadOnly Property ExcedioTimeOut() As Boolean
        Get
            ' Dim X As Decimal
            If DateDiff(DateInterval.Minute, Now, LastAction) > TimeOut Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

#End Region
#Region "Metodos Publicos"
    Public Sub SetLastAction()
        Datos.DsActiveLic(0).U_Time = Now
        Datos.AcceptChanges()
    End Sub
#End Region
#Region "Metodos Privados"
    Private Sub Conect(ByVal ID As Int32)
        Dim sql As String = "select C_Time,U_Time, TIME_OUT from ucm where User_ID=" & ID
        Dim Ds As New DataSet
        Try
		Ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
            Ds.Tables(0).TableName = Datos.Tables(0).TableName
            Datos.Merge(Ds)
        Catch ex As Exception
        Finally
            sql = Nothing
            Ds.Dispose()
        End Try
    End Sub
#End Region
#Region "New"
    Public Sub New(ByVal id As Int32)
        Conect(id)
    End Sub
#End Region

End Class
