Public Class IndexServiceBusiness
    Implements IService

#Region "Atributo y Constructor"
    Dim serviceId As Int64
    Public Sub New(ByVal serviceId As Int64, RefreshRate As Int64)
        Me.serviceId = serviceId
        Me.RefreshRate = RefreshRate
    End Sub
#End Region

    Private Property RefreshRate As Long

    Public Sub StartService() Implements IService.StartService
        Dim QMB As New Zamba.Core.IndexFileBusiness
        Try
            QMB.PeekQuequedFile(IndexedState.Pendiente)
            QMB.PeekQuequedFile(IndexedState.Erroneo)
            QMB.PeekQuequedFile(IndexedState.NoEncontrado)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            QMB = Nothing
        End Try
    End Sub

    Public ReadOnly Property ProcessQuequedFiles As Boolean
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public Sub StopService() Implements IService.StopService
        Try
            'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
            UserBusiness.Rights.SaveAction(ServiceTypes.IndexService, ObjectTypes.Services, RightsType.Terminar, "Deteniendo servicio de Indexacion con ID " & serviceId.ToString)
        Catch
        End Try
    End Sub
End Class
