''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'CLASE QUE PERMITE REALIZAR UN ABM DE ROWS DE LA TABLA IP_FOLDER                       '
'''''''''''''''''''

Imports System.data
Imports Zamba.AppBlock
Imports Zamba.Core
Imports Zamba.Data

Public Class ImportsBusiness
    Inherits ZClass

    'Public Shared Sub Dispose()
    '    Server.Con.dispose()
    'End Sub
    'METODO QUE PERMITE LLENAR UN DATASET CON LAS CONFIGURACIONES
    Public Shared Function GetConfigurations(ByVal IPFolder As dsIPFolder, ByVal UserId As Int32) As dsIPFolder
        Return ImportsFactory.GetConfigurations(IPFolder, UserId)
    End Function

    'METODO QUE PERMITE LLENAR UN DATASET CON LOS USUARIOS Y SUS IDs
    'Public Function GetUsers(ByVal UserTable As dsUserTable) As dsUserTable
    '    Try
    '        Dim Strselect As String = "SELECT * FROM USRTABLE"
    '        Return Server.Con.ExecuteDataset(CommandType.Text, Strselect)
    '    Catch ex As Exception
    '       zamba.core.zclass.raiseerror(ex)
    '        Return UserTable
    '    End Try
    'End Function

    ''METODO QUE PERMITE BORRAR UNA ROW DE LA TABLA DE LA BASE DE DATOS
    'Public Function DeleteRow(ByVal Row As dsIPFolder.IP_FolderRow) As Boolean


    '    Try
    '        Dim strdelete As String = "DELETE FROM IP_FOLDER WHERE ID = " & Row.Item("Id")
    '        Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
    '    Catch ex As Exception
    '       zamba.core.zclass.raiseerror(ex)
    '        Return False
    '    End Try
    '    Return True
    'End Function

    'METODO QUE PERMITE MODIFICAR UNA ROW DE LA TABLA DE LA BASE DE DATOS
    Public Shared Function UpdateRow(ByVal row As DataRow) As Boolean
        Return ImportsFactory.UpdateRow(row)
    End Function

    ''METODO QUE PERMITE GUARDAR UNA NUEVA ROW EN LA TABLA DE LA BASE DE DATOS
    'Public Function StoreRow(ByVal Row As dsIPFolder.IP_FolderRow) As Boolean

    '    Try

    '        'ESTO HAY QUE REVISARLO
    '        Dim i As Byte
    '        If Row("Service") Then
    '            i = 1
    '        Else
    '            i = 0
    '        End If
    '        Dim strInsert As String = "INSERT INTO IP_FOLDER (Nombre,Path,Timer,Service,User_Id,NombreMaquina) VALUES ('" _
    '        & Row.Nombre & "', '" & Row.Path & "', " & Row("Timer") & ", " & i & ", " & Row("User_Id") & ", '" & Environment.MachineName & "')"
    '        Server.Con.ExecuteNonQuery(CommandType.Text, strInsert)
    '    Catch ex As Exception
    '       zamba.core.zclass.raiseerror(ex)

    '        Return False
    '    End Try
    '    Return True
    'End Function

    Public Shared Function GetConfigurations(ByVal IPFolder As dsIPFolder) As dsIPFolder
        Return ImportsFactory.GetConfigurations(IPFolder)
    End Function

    Public Shared Function GetRowByID(ByVal Path As String, ByVal IPFolder As dsIPFolder) As dsIPFolder
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
    Public Shared Function DeleteRow(ByVal Row As dsIPFolder.IP_FolderRow) As Boolean
        Dim deleted As Boolean = ImportsFactory.DeleteRow(Row)
        If deleted = True Then
            'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
            UserBusiness.Rights.SaveAction(Row.ID, ObjectTypes.ModuleMonitor, RightsType.Delete, "Se eliminó el monitoreo: " & Row.NOMBRE & "(" & Row.ID & ")")
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
    Public Shared Function UpdateRow(ByVal Row As dsIPFolder.IP_FolderRow) As Boolean
        Dim updated As Boolean = ImportsFactory.UpdateRow(Row)
        If updated = True Then
            'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
            UserBusiness.Rights.SaveAction(Row.ID, ObjectTypes.ModuleMonitor, RightsType.Edit, "Se actualizó el monitoreo: " & Row.NOMBRE & "(" & Row.ID & ")")
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
    Public Shared Function StoreRow(ByVal Row As dsIPFolder.IP_FolderRow) As Boolean
        Dim iID As Int32 = CoreBusiness.GetNewID(IdTypes.FOLDERID)
        Dim saved As Boolean = ImportsFactory.StoreRow(Row, iID)

        If saved Then
            'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
            UserBusiness.Rights.SaveAction(iID, ObjectTypes.ModuleMonitor, RightsType.insert, "Se creó el monitoreo: " & Row.NOMBRE & "(" & Row.ID & ")")
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
    Public Shared Function StoreIpFolderRow(ByVal Row As dsIPFolder.IP_FolderRow) As Decimal
        Dim iId As Int32 = CoreBusiness.GetNewID(IdTypes.FOLDERID)
        Dim saved As Boolean = ImportsFactory.StoreIpFolderRow(Row, iId)

        If saved = True Then
            'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
            UserBusiness.Rights.SaveAction(iId, ObjectTypes.ModuleMonitor, RightsType.insert, "Se creó el monitoreo: " & Row.NOMBRE & "(" & Row.ID & ")")
        End If

        Return saved
    End Function
    Public Overrides Sub Dispose()
    End Sub


    Public Shared Function GetFolderData(ByVal FolderPath As String) As dsIPFolder
        Return ImportsFactory.GetFolderData(FolderPath)
    End Function


    Public Shared Function GetIPFOLDERCONFByFolderId(ByVal FolderId As Int32) As dsIPFolderConf
        Return ImportsFactory.GetIPFOLDERCONFByFolderId(FolderId)
    End Function

End Class
