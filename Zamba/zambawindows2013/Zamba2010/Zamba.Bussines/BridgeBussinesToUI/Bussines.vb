Imports Zamba.Core
Imports Zamba.Data
Imports Zamba.Servers
Imports System.IO
Imports System.Text
Imports Zamba.Data.MonitorFactory

Namespace Access
    Public Class Server
      
        Public Shared Event SessionTimeOut()

        'TODO ERROR: REFERENCIAR DE Business A SERVER NO SE PUEDE< DEBE SER A TRAVEZ DE DATA
        Public Shared WithEvents server As Zamba.Servers.Server

      
        Private Shared Sub Event_SessionTimeOut() Handles server.SessionTimeOut
            RaiseEvent SessionTimeOut()
        End Sub

        Public Shared Sub RemoveConnection()
            Ucm.RemoveConnection()
        End Sub
        Public Shared Sub RemoveWFConnections()
            Ucm.RemoveConnection()
        End Sub
    End Class
    Public Class utilities
        Public Shared Sub SetLotusNotesDefault()
            Membership.MembershipHelper.CurrentUser.eMail.Type = MailTypes.LotusNotesMail
        End Sub

    End Class
End Namespace
Namespace Users
    Public Class Actions
        Public Shared Sub CleanExceptions()
            ActionsBusiness.CleanExceptions()
        End Sub
    End Class
    'Public Class Rights
    '    Public Shared Function GetUserRights(ByVal ObjectId As ObjectTypes, ByVal RType As Zamba.Core.RightsType, Optional ByVal AditionalParam As Integer = -1) As Boolean
    '        Return RightFactory.GetUserRights(ObjectId, RType, AditionalParam)
    '    End Function
    'End Class
    Public Class User
        
        Public Shared Function GetUserName() As String
            If Not IsNothing(Membership.MembershipHelper.CurrentUser) Then
                Return Membership.MembershipHelper.CurrentUser.Name
            Else
                Return String.Empty
            End If
        End Function
        Public Shared Function IsNothingUser() As Boolean
            Return IsNothing(Membership.MembershipHelper.CurrentUser)
        End Function
        Public Shared Function factoryRight() As RightFactory
            Return New RightFactory
        End Function

        Public Shared Function IsWfLic() As Boolean
            If (Membership.MembershipHelper.CurrentUser IsNot Nothing) Then
                Return Membership.MembershipHelper.CurrentUser.WFLic
            End If
        End Function
    End Class
End Namespace

Namespace Indexs
    Public Class Schema
        Public Shared Function GetIndexSchema(ByVal docTypeId As Int64) As DSIndex
            Return Core.IndexsBusiness.GetIndexSchema(DocTypeId)
        End Function
    End Class
    Public Class DataBase
        'Public Shared Sub Delete_Index_TableFactory(ByVal doc_type_id As Int64)
        '    DataBaseIndexsFactory.Delete_Index_Table(doc_type_id)
        'End Sub
        'Public Shared Sub Delete_Index_Column(ByVal doc_type_id As Integer, ByVal indexes As ArrayList)
        '    DataBaseIndexsFactory.Delete_Index_Column(doc_type_id, indexes)
        'End Sub
        'Public Shared Function Create_AllDocDIndex(ByVal doc_id As Integer, ByVal atributos As DataTable) As ArrayList
        '    Return DataBaseIndexsFactory.Create_AllDocDIndex(doc_id, atributos)
        'End Function
        'Public Shared Function Create_New_Index(ByVal DocD_Id As Integer, ByVal Name As String, ByVal atributos As DataTable) As DocD
        '    Return DataBaseIndexsFactory.Create_New_Index(DocD_Id, Name, atributos)
        'End Function
        Public Shared Sub Drop_Database_Index(ByVal index_name As String, ByVal docd_index As Integer)
            DataBaseIndexsFactory.Drop_Database_Index(index_name, docd_index)
        End Sub
        'Public Shared Function Update_DocD(ByVal docd_obj As DocD, ByVal doc_dindex As Integer, ByVal atributos As DataTable) As Integer
        '    Return DataBaseIndexsFactory.Update_DocD(docd_obj, doc_dindex, atributos)
        'End Function
        Public Shared Sub Create_Database_Index(ByVal docd_obj As DocD, ByVal doc_dindex As Integer, ByVal Cluster As Boolean, ByVal Unico As Boolean)
            DataBaseIndexsFactory.Create_Database_Index(docd_obj, doc_dindex, Cluster, Unico)
        End Sub
        Public Shared Function VerificarServer() As Boolean
            Return DataBaseIndexsFactory.VerificarServer()
        End Function
    End Class
    Public Class DocType
        Public Shared Sub RemoveIndex(ByVal DoctypeID As Int64, ByVal IndexId As Int64)
            DocTypesFactory.RemoveIndex(DoctypeID, IndexId)
        End Sub
        Public Shared Sub SetIndexRequired(ByVal DocTypeId As Int64, ByVal IndexId As Int64)
            IndexsBusiness.SetIndexRequired(DocTypeId, IndexId)
        End Sub
        'Public Shared Sub Delete_DocD(ByVal docd_name As String, ByVal doc_dindex As Integer)
        '    DataBaseIndexsFactory.Delete_DocD(docd_name, doc_dindex)
        'End Sub
        Public Shared Function FillIndexDocType(ByVal docTypeId As Int64) As DataSet
            Return DataBaseIndexsFactory.FillIndexDocType(docTypeId)
        End Function
    End Class
End Namespace
Namespace DocTypes
    Public Class Volumen
        Public Shared Function IsValidDiskVolume(ByVal DiskGroupId As Int32) As Boolean
            Dim Vols As New DataSet
            Dim volPath As String
            Try
                Vols = VolumeListsBusiness.GetDiskGroupVolumes(DiskGroupId)
                Dim i As Int32
                Dim FlagAlmostOneVolGood As Boolean
                For i = 0 To Vols.Tables(0).Rows.Count - 1
                    Try
                        volPath = VolumesBusiness.RetrieveVolumePath(Vols.Tables(0).Rows(i).Item("DISK_VOLUME_ID"))
                        'chequeo el volumen
                        If Directory.Exists(volPath) Then
                            FlagAlmostOneVolGood = True
                        End If
                    Catch ex As Exception
                        zamba.core.zclass.raiseerror(ex)
                    End Try
                Next
                Return FlagAlmostOneVolGood
            Finally
                Vols.Dispose()
            End Try
        End Function
        Public Shared Function GetDiskGroupVolumes(ByVal DiskGroupId As Int32) As DataSet
            Return VolumeListsBusiness.GetDiskGroupVolumes(DiskGroupId)
        End Function
        Public Shared Function RetrieveVolumePath(ByVal VolumeId As Integer) As String
            Return VolumesBusiness.RetrieveVolumePath(VolumeId)
        End Function
        Public Shared Function DocumentsCount(ByVal docTypeId As Int64) As Int64
            Return DocTypesFactory.DocumentsCount(DocTypeId)
        End Function
    End Class
    Public Class DocTypeDataBase
        Public Shared Sub removecolumn(ByVal doctypeID As Int64, ByVal indexidarray As ArrayList)
            DocTypesFactory.Removecolumn(doctypeID, indexidarray)
        End Sub
        Public Shared Sub CreateView(ByVal DocType As Zamba.Core.DocType)
            DocTypesBusiness.CreateView(DocType)
        End Sub
        Public Shared Sub AddColumn(ByVal doctype As Zamba.Core.DocType, ByVal IndexIdArray As ArrayList, ByVal IndexTypeArray As ArrayList, ByVal IndexLenArray As ArrayList)
            DocTypesFactory.AddColumn(doctype, IndexIdArray, IndexTypeArray, IndexLenArray)
        End Sub
        Public Shared Sub adddoctyperelationindex(ByVal indexid As Int32, ByVal docTypeId As Int64, ByVal order As Int32, ByVal Required As Boolean, ByVal IsReferenced As Boolean)
            DocTypesFactory.adddoctyperelationindex(indexid, doctypeid, order, Required, IsReferenced)
        End Sub
        Public Shared Sub AddColumnTextindex(ByVal docTypeId As Int64, ByVal IndexId As Int32)
            DocTypesFactory.AddColumnTextindex(DocTypeId, IndexId)
        End Sub
    End Class

    Public Class DocType
        Public Shared Function getDocTypesFactory() As ArrayList
            Return VolumeListsBusiness.GetDiskGroupsList
        End Function
        Public Shared Sub Remove_DocType_FromAll_DocTypesGroup(ByVal docTypeId As Int64)
            DocTypesFactory.Remove_DocType_FromAll_DocTypesGroup(doctypeid)
        End Sub
        Public Shared Sub DelDocType(ByVal DocTypeID As Int64)
            DocTypesFactory.DelDocType(DocTypeID)
        End Sub
        Public Shared Sub DeleteTables(ByVal DocTypeID As Int64)
            DocTypesFactory.DeleteTables(DocTypeID)
        End Sub
        Public Shared Sub deleteRights(ByVal DocTypeID As Int64)
            DocTypesFactory.DeleteRights(DocTypeID)
        End Sub

        Public Shared Function GetDocTypesDsDocType() As DSDOCTYPE
            Return DocTypesFactory.GetDocTypesDsDocType()
        End Function

        Public Shared Function GetDocTypesDataSet() As DataSet
            Return DocTypesFactory.GetDocTypesDataSet()
        End Function

        Public Shared Function CopyDoc(ByVal DocIDOrigen As Int32, ByVal DocNameDestino As String) As Integer
            DocTypesFactory.CopyDoc(DocIDOrigen, DocNameDestino)
        End Function

        Shared Sub CreateTables(DocTypeId As Long)
            Dim Cr As CreateTables = Server.CreateTables
            Try
                'agrego la tabla del documento
                Cr.AddDocsTables(DocTypeId)

            Catch ex As Exception
                Throw New Exception("Ocurrió un error al crear las tablas. " & ex.ToString, ex)
            End Try
        End Sub

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
                UserBusiness.Rights.SaveAction(Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.HistorialDeUsuario, RightsType.Delete, "Se depuro el historial")
            Catch ex As Exception
                Throw New ArgumentException(ex.Message)
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