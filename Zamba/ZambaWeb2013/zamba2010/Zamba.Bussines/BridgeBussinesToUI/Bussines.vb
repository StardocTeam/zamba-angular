Imports Zamba.Core
Imports Zamba.Data
Imports Zamba.Servers
Imports System.IO
Imports System.Text
Imports Zamba.Data.MonitorFactory
Imports Zamba.Membership

Namespace Access
    Public Class Server
        'Evento a ConnectionTerminated
        Public Shared Event ConnectionTerminated()
        Public Shared Event SessionTimeOut()

        'TODO ERROR: REFERENCIAR DE Business A SERVER NO SE PUEDE< DEBE SER A TRAVEZ DE DATA
        Public Shared WithEvents server As Zamba.Servers.Server

        Private Shared Sub Event_ConnectionTerminated() Handles server.ConnectionTerminated
            RaiseEvent ConnectionTerminated()
        End Sub
        Private Shared Sub Event_SessionTimeOut() Handles server.SessionTimeOut
            RaiseEvent SessionTimeOut()
        End Sub


    End Class
    Public Class utilities
        Public Shared Sub SetLotusNotesDefault()
            Zamba.Membership.MembershipHelper.CurrentUser.eMail.Type = MailTypes.LotusNotesMail
        End Sub

    End Class
End Namespace
Namespace Users


    Public Class User
        Public Shared Function GetUserId() As Int64
            Return Zamba.Membership.MembershipHelper.CurrentUser.ID
        End Function
        Public Shared Function GetUserName() As String
            If Not IsNothing(Zamba.Membership.MembershipHelper.CurrentUser) Then
                Return Zamba.Membership.MembershipHelper.CurrentUser.Name
            Else
                Return String.Empty
            End If
        End Function
        Public Shared Function IsNothingUser() As Boolean
            Return IsNothing(Zamba.Membership.MembershipHelper.CurrentUser)
        End Function
        Public Shared Function factoryRight() As RightFactory
            Return New RightFactory
        End Function

        Public Shared Function IsWfLic() As Boolean
            If (Zamba.Membership.MembershipHelper.CurrentUser IsNot Nothing) Then
                Return Zamba.Membership.MembershipHelper.CurrentUser.WFLic
            End If
        End Function
    End Class
End Namespace
Namespace Rights
    Public Class Factory
        Public Shared Sub SaveAction(ByVal ObjectId As Int64, ByVal ObjectType As ObjectTypes,
                                     ByVal ActionType As Zamba.Core.RightsType, Optional ByVal S_Object_ID As String = "",
                                     Optional ByVal _userid As Int32 = 0)
            RightFactory.SaveAction(ObjectId, ObjectType, ActionType, S_Object_ID)
        End Sub
    End Class
End Namespace
Namespace Indexs
    Public Class Schema
        Public Shared Function GetIndexSchema(ByVal DocTypeId As Int32) As DataSet
            Return Core.IndexsBusiness.GetIndexSchema(DocTypeId)
        End Function
    End Class
    Public Class DataBase
        Public Shared Sub Delete_Index_TableFactory(ByVal doc_type_id As Int64)
            DataBaseIndexsFactory.Delete_Index_Table(doc_type_id)
        End Sub
        Public Shared Sub Delete_Index_Column(ByVal doc_type_id As Integer, ByVal indexes As ArrayList)
            DataBaseIndexsFactory.Delete_Index_Column(doc_type_id, indexes)
        End Sub
        Public Shared Function Create_AllDocDIndex(ByVal doc_id As Integer, ByVal indices As DataTable) As ArrayList
            Return DataBaseIndexsFactory.Create_AllDocDIndex(doc_id, indices)
        End Function
        Public Shared Function Create_New_Index(ByVal DocD_Id As Integer, ByVal Name As String, ByVal indices As DataTable) As DocD
            Return DataBaseIndexsFactory.Create_New_Index(DocD_Id, Name, indices)
        End Function
        Public Shared Sub Drop_Database_Index(ByVal index_name As String, ByVal docd_index As Integer)
            DataBaseIndexsFactory.Drop_Database_Index(index_name, docd_index)
        End Sub
        Public Shared Function Update_DocD(ByVal docd_obj As DocD, ByVal doc_dindex As Integer, ByVal indices As DataTable) As Integer
            Return DataBaseIndexsFactory.Update_DocD(docd_obj, doc_dindex, indices)
        End Function
        Public Shared Sub Create_Database_Index(ByVal docd_obj As DocD, ByVal doc_dindex As Integer, ByVal Cluster As Boolean, ByVal Unico As Boolean)
            DataBaseIndexsFactory.Create_Database_Index(docd_obj, doc_dindex, Cluster, Unico)
        End Sub
        Public Shared Function VerificarServer() As Boolean
            Return DataBaseIndexsFactory.VerificarServer()
        End Function
    End Class
    Public Class DocType
        Public Shared Sub RemoveIndex(ByVal Doctype As Zamba.Core.DocType, ByVal IndexId As Int32)
            DocTypesFactory.RemoveIndex(Doctype, IndexId)
        End Sub
        Public Shared Sub SetIndexRequired(ByVal DocTypeId As Int32, ByVal IndexId As Int32)
            IndexsBusiness.SetIndexRequired(DocTypeId, IndexId)
        End Sub
        Public Shared Sub Delete_DocD(ByVal docd_name As String, ByVal doc_dindex As Integer)
            DataBaseIndexsFactory.Delete_DocD(docd_name, doc_dindex)
        End Sub
        Public Shared Function FillIndexDocType(ByVal docTypeId As Int64) As DataSet
            Return DataBaseIndexsFactory.FillIndexDocType(docTypeId)
        End Function
    End Class
End Namespace
Namespace DocTypes
    Public Class Volumen


        ''' <summary>
        ''' Verifica si el grupo de volumenes es válido.
        ''' </summary>
        ''' <param name="DiskGroupId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function IsValidDiskVolume(ByVal DiskGroupId As Int32) As Boolean
            Dim dtVols As DataTable = Nothing

            Try
                'Se obtienen los volumenes asociados a un grupo de volumenes
                dtVols = VolumeListsBusiness.GetDiskGroupData(DiskGroupId)

                'Se busca por algún volumen en base de datos
                For Each dr As DataRow In dtVols.Rows
                    If String.Compare(dr(1).ToString(), "5") = 0 Then
                        Return True
                    End If
                Next

                'Si no se encuentra se busca la existencia de algún volumen de tipo 
                'diferente a base de datos y que tenga una ruta configurada existente
                Dim volPath As String
                For Each dr As DataRow In dtVols.Rows
                    If Directory.Exists(dr(2).ToString()) Then
                        Return True
                    End If
                Next

                Return False
            Finally
                If dtVols IsNot Nothing Then
                    dtVols.Dispose()
                End If
            End Try
        End Function
        Public Shared Function GetDiskGroupVolumes(ByVal DiskGroupId As Int32) As DataSet
            Return VolumeListsBusiness.GetDiskGroupVolumes(DiskGroupId)
        End Function
        Public Shared Function RetrieveVolumePath(ByVal VolumeId As Integer) As String
            Return VolumesBusiness.RetrieveVolumePath(VolumeId)
        End Function
        Public Shared Function DocumentsCount(ByVal DocTypeId As Int32) As Int64
            Return DocTypesFactory.DocumentsCount(DocTypeId)
        End Function
    End Class
    Public Class DocTypeDataBase
        Public Shared Sub removecolumn(ByVal doctype As Zamba.Core.DocType, ByVal indexidarray As ArrayList)
            DocTypesFactory.Removecolumn(doctype, indexidarray)
        End Sub

        Public Shared Sub AddColumn(ByVal doctype As Zamba.Core.DocType, ByVal IndexIdArray As ArrayList, ByVal IndexTypeArray As ArrayList, ByVal IndexLenArray As ArrayList)
            DocTypesFactory.AddColumn(doctype, IndexIdArray, IndexTypeArray, IndexLenArray)
        End Sub
        Public Shared Sub adddoctyperelationindex(ByVal indexid As Int32, ByVal doctypeid As Int32, ByVal order As Int32, ByVal Required As Boolean, ByVal IsReferenced As Boolean)
            DocTypesFactory.adddoctyperelationindex(indexid, doctypeid, order, Required, IsReferenced)
        End Sub
        Public Shared Sub AddColumnTextindex(ByVal DocTypeId As Int32, ByVal IndexId As Int32)
            DocTypesFactory.AddColumnTextindex(DocTypeId, IndexId)
        End Sub
    End Class

    Public Class DocType
        Public Shared Function getDocTypesFactory() As ArrayList
            Return VolumeListsBusiness.GetDiskGroupsList
        End Function
        Public Shared Sub Remove_DocType_FromAll_DocTypesGroup(ByVal doctypeid As Int32)
            DocTypesFactory.Remove_DocType_FromAll_DocTypesGroup(doctypeid)
        End Sub
        Public Shared Sub DelDocType(ByVal DocType As Zamba.Core.DocType)
            DocTypesFactory.DelDocType(DocType)
        End Sub
        Public Shared Sub DeleteTables(ByVal DocType As Zamba.Core.DocType)
            DocTypesFactory.DeleteTables(DocType)
        End Sub
        Public Shared Sub deleteRights(ByVal DocType As Zamba.Core.DocType)
            DocTypesFactory.DeleteRights(DocType)
        End Sub


        Public Shared Function CopyDoc(ByVal DocIDOrigen As Int32, ByVal DocNameDestino As String) As Integer
            DocTypesFactory.CopyDoc(DocIDOrigen, DocNameDestino)
        End Function



    End Class
End Namespace
Namespace BusinessControls
    Public Class Barcodes

    End Class

    Public Class Histories
        ''' <summary>
        ''' Borra valores del historial
        ''' </summary>
        ''' <param name="pRbBorrartodo"></param>
        ''' <param name="pRbBorradoVer"></param>
        ''' <param name="prbborrarFechas"></param>
        ''' <param name="pDTFecha1"></param>
        ''' <param name="pDTFecha2"></param>
        ''' <param name="psWhere"></param>
        '''<history>
        ''' 	[Marcelo]	22/05/2008	Modified
        '''</history>
        ''' <remarks></remarks>
        Public Shared Sub Borrar(ByVal pRbBorrartodo As Boolean, ByVal pRbBorradoVer As Boolean, ByVal prbborrarFechas As Boolean, ByVal pDTFecha1 As Date, ByVal pDTFecha2 As Date, ByVal psWhere As String)
            Dim sql As String = String.Empty

            Dim UB As New UserBusiness
            Try
                If pRbBorrartodo = True Then
                    sql = "Delete from USER_HST Where Action_type <> 4 and action_type <> 12 " & psWhere
                End If
                If prbborrarFechas = True Then
                    'Detalle: Si se selecciona borrar entre 1/1/2006 y 2/1/2006, solo borra todos los registros del 1/1/2006
                    'por que la consulta es ... WHERE ACTION_DATE BETWEEN '2006-1-1 00:00:000' AND '2006-1-2 00:00:000'
                    sql = "Delete from USER_HST Where ACTION_DATE between " & Server.Con.ConvertDate(pDTFecha1.ToString) & " and " & Server.Con.ConvertDate(pDTFecha2.ToString) & " and Action_type<>4 and action_type<>12 " & psWhere
                End If
                If pRbBorradoVer = True Then
                    sql = "Delete from USER_HST Where Action_type=2 " & psWhere
                End If
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.Trim)

                'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
                UB.SaveAction(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.HistorialDeUsuario, RightsType.Delete, "Se depuro el historial")
            Catch ex As Exception
                Throw New ArgumentException(ex.Message)
            Finally
                UB = Nothing
            End Try
        End Sub
    End Class
End Namespace
Namespace Scheduler
    Public Class SchedulerBusiness
        Public Shared Sub UpdateFolderConfig(ByVal sIntervalo As String, ByVal sAlarmTimeDisp As String, ByVal tipoConfig As Int32, ByVal idCarpeta As Decimal, ByVal lDays() As Object)
            SchedulerFactory.UpdateFolderConfig(sIntervalo, sAlarmTimeDisp, tipoConfig, idCarpeta, lDays)
        End Sub

        Public Shared Function GetFolderConfig(ByVal idTarea As Decimal) As DataSet
            Return SchedulerFactory.GetFolderConfig(idTarea)
        End Function
    End Class
End Namespace
Namespace MonitorBusiness
    Public Class MonitorProcessBusiness
        Public Shared Function GetFolders(ByVal UserConfigMachineName As String) As DataSet
            Return MonitorProcessFactory.GetFolders(UserConfigMachineName)
        End Function
    End Class
End Namespace