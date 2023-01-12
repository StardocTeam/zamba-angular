''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'CLASE QUE PERMITE REALIZAR UN ABM DE ROWS DE LA TABLA IP_FOLDER                       '
'''''''''''''''''''

Imports Zamba.Data

Public Class ImportsBusiness
    Inherits ZClass


    'METODO QUE PERMITE LLENAR UN DATASET CON LAS CONFIGURACIONES
    Public Shared Function GetConfigurations() As DataSet
        Return ImportsFactory.GetConfigurations()
    End Function

    Public Shared Function GetRowByID(ByVal Path As String, ByVal IPFolder As DataSet) As dataset
        Return ImportsFactory.GetRowByID(Path, IPFolder)
    End Function

    ''' <summary>
    ''' METODO QUE PERMITE BORRAR UNA ROW DE LA TABLA DE LA BASE DE DATOS
    ''' </summary>
    ''' <param name="Row"></param>
    ''' <returns></returns>
    ''' <history>
    ''' 	[Marcelo]	22/05/2008	Modified
    ''' </history>
    ''' <remarks></remarks>
    Public Shared Function DeleteRow(ByVal currentFolder As Folder) As Boolean
        Dim deleted As Boolean = ImportsFactory.DeleteRow(currentFolder)
        If deleted = True Then
            UserBusiness.Rights.SaveAction(currentFolder.ID, ObjectTypes.ModuleMonitor, RightsType.Delete, "Se eliminó el monitoreo: " & currentFolder.Nombre & "(" & currentFolder.ID & ")")
        End If
        Return deleted
    End Function

    ''' <summary>
    ''' METODO QUE PERMITE MODIFICAR UNA ROW DE LA TABLA DE LA BASE DE DATOS
    ''' </summary>
    ''' <param name="Row"></param>
    ''' <returns></returns>
    ''' <history>
    ''' 	[Marcelo]	22/05/2008	Modified
    ''' </history>
    ''' <remarks></remarks>
    Public Shared Function UpdateRow(ByVal currentFolder As Folder) As Boolean
        Dim updated As Boolean = ImportsFactory.UpdateRow(currentFolder)
        If updated = True Then
            'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
            UserBusiness.Rights.SaveAction(currentFolder.ID, ObjectTypes.ModuleMonitor, RightsType.Edit, "Se actualizó el monitoreo: " & currentFolder.Nombre & "(" & currentFolder.ID & ")")
        End If

        Return updated
    End Function

    ''' <summary>
    ''' METODO QUE PERMITE GUARDAR UNA NUEVA ROW EN LA TABLA DE LA BASE DE DATOS
    ''' </summary>
    ''' <param name="Row"></param>
    ''' <returns></returns>
    ''' <history>
    ''' 	[Marcelo]	22/05/2008	Modified
    ''' </history>
    ''' <remarks></remarks>
    Public Shared Function StoreRow(ByVal Row As DataRow) As Boolean
        Dim iID As Int32 = CoreBusiness.GetNewID(IdTypes.FOLDERID)
        Dim saved As Boolean = ImportsFactory.StoreRow(Row, iID)

        If saved Then
            'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
            UserBusiness.Rights.SaveAction(iID, ObjectTypes.ModuleMonitor, RightsType.insert, "Se creó el monitoreo: " & Row("NOMBRE") & "(" & Row("ID") & ")")
        End If

        Return saved
    End Function

    ''' <summary>
    ''' METODO QUE PERMITE GUARDAR UNA NUEVA ROW EN LA TABLA DE LA BASE DE DATOS
    ''' </summary>
    ''' <param name="Row"></param>
    ''' <returns></returns>
    ''' <history>
    ''' 	[Marcelo]	22/05/2008	Modified
    ''' </history>
    ''' <remarks></remarks>
    Public Shared Function StoreIpFolderRow(ByVal currentFolder As Folder) As Decimal
        Dim iId As Int32 = CoreBusiness.GetNewID(IdTypes.FOLDERID)
        Dim saved As Boolean = ImportsFactory.StoreIpFolderRow(currentFolder, iId)

        If saved = True Then
            'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
            UserBusiness.Rights.SaveAction(iId, ObjectTypes.ModuleMonitor, RightsType.insert, "Se creó el monitoreo: " & currentFolder.Nombre & "(" & currentFolder.ID & ")")
        End If

        Return saved
    End Function
    Public Overrides Sub Dispose()
    End Sub


    Public Shared Function GetFolderData(ByVal FolderPath As String) As DataSet
        Return ImportsFactory.GetFolderData(FolderPath)
    End Function


    Public Shared Function GetIPFOLDERCONFByFolderId(ByVal FolderId As Int32) As DataSet
        Return ImportsFactory.GetIPFOLDERCONFByFolderId(FolderId)
    End Function

End Class
