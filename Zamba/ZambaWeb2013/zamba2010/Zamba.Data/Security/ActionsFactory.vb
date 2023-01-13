Imports Zamba.Core
Imports Zamba.Data
Imports Zamba.Servers

Public Class ActionsFactory

    Public Event LogError(ByVal ex As Exception)

    Public Shared Function GetDocumentActions(ByVal documentId As Int64) As DataSet 'DSActions
        Dim Table As String = "VisHistory"
        Dim dstemp As DataSet = Nothing

        If Server.IsOracle Then
            Dim parNames() As String = {"DocumentId", "io_cursor"}
            ' Dim parTypes() As Object = {13, 5}
            Dim parValues() As Object = {documentId, 2}

            dstemp = Server.Con(True).ExecuteDataset("zsp_doctypes_100.GetDocumentActions", parValues)
            dstemp.Tables(0).TableName = Table
        Else
            Dim parameters() As Object = {documentId}
            'dstemp = Server.Con(True).ExecuteDataset("ClsActions_GetDocumentActions", parameters)
            dstemp = Server.Con(True).ExecuteDataset("zsp_users_100_GetDocumentAction", parameters)
            dstemp.Tables(0).TableName = Table
            'DsActions.Merge(dstemp)

        End If

        Return dstemp
    End Function

    ''' <summary>
    ''' Devuelve la fecha de la ultima modificacion del documento
    ''' </summary>
    ''' <param name="TaskId">Id de la tarea</param>
    ''' <history>Marcelo created 26/02/2009</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetLastModifiedDocumentHistoryById(ByVal documentId As Int64) As DateTime 'DSActions
        Dim lastModDate As Nullable(Of DateTime)
        If Server.isOracle Then
            Dim parNames() As String = {"DocumentId", "io_cursor"}
            ' Dim parTypes() As Object = {13, 5}
            Dim parValues() As Object = {documentId, 2}

            'todo crear este store en oracle
            lastModDate = Server.Con(True).ExecuteScalar("zsp_doctypes_200.GetLastDocumentAction", parValues)
        Else
            Dim parameters() As Object = {documentId}
            'dstemp = Server.Con(True).ExecuteDataset("ClsActions_GetDocumentActions", parameters)
            lastModDate = Server.Con(True).ExecuteScalar("zsp_users_200_GetLastDocumentAction", parameters)
        End If
        If lastModDate.HasValue = True Then
            Return lastModDate.Value
        Else
            Return New DateTime(1987, 5, 20)
        End If
    End Function

    Public Shared Function GetUserActions(ByVal UserId As Int64) As DataSet
        Dim Ds As DataSet = Nothing

        If Server.isOracle Then
            Dim parNames() As String = {"UserId", "io_cursor"}
            ' Dim parTypes() As Object = {13, 5}
            Dim parValues() As Object = {UserId, 2}

            Ds = Server.Con(True).ExecuteDataset("zsp_users_100.GetUserActions", parValues)
        Else
            Dim parvalues() As Object = {UserId}
            Ds = Server.Con(True).ExecuteDataset("zsp_users_100_GetUserAction", parvalues)
        End If

        Return Ds
    End Function

    'Public Shared Function GetDocumentActions(ByVal DocumentId As Integer) As DataSet 'DSActions
    '    Dim DsActions As New DsActions
    '    Dim Table As String = "VisHistory"
    '    if Server.IsOracle then
    '        Dim dstemp As DataSet
    '        Dim parNames() As String = {"DocumentId", "io_cursor"}
    '        Dim parTypes() = {13, 5}
    '        Dim parValues() = {DocumentId, 2}

    '        'dstemp = Server.Con(True).ExecuteDataset("GETDOCUMENTACTIONS_PKG.GetDocumentActions", parValues)
    '        dstemp = Server.Con(True).ExecuteDataset("zsp_doctypes_100.GetDocumentActions", parValues)
    '        dstemp.Tables(0).TableName = Table

    '        '--------------------LINEAS AGREGADAS------------------
    '        'dstemp.Tables(0).Columns(0).ColumnName = "NOOOMBRE"
    '        '------------------------------------------------------

    '        DsActions.Merge(dstemp)
    '    Else
    '        Dim dstemp As DataSet
    '        Dim parameters() = {DocumentId}
    '        'dstemp = Server.Con(True).ExecuteDataset("ClsActions_GetDocumentActions", parameters)
    '        dstemp = Server.Con(True).ExecuteDataset("zsp_users_100_GetDocumentAction", parameters)
    '        dstemp.Tables(0).TableName = Table
    '        'DsActions.Merge(dstemp)
    '        Return dstemp
    '    End If
    '    Return DsActions
    'End Function

    'Public Shared Function GetUserActions(ByVal UserId As Integer) As DataSet

    '    Try
    '        'Dim Table As String = "VisHistory"
    '        Dim Ds As DataSet

    '        if Server.IsOracle then
    '            Dim parNames() As String = {"UserId", "io_cursor"}
    '            Dim parTypes() = {13, 5}
    '            Dim parValues() = {UserId, 2}
    '            'Ds = Server.Con(True).ExecuteDataset("CLSACTIONS_GETUSERACTIONS_PKG.getUserActions", parValues)
    '            Ds = Server.Con(True).ExecuteDataset("zsp_users_100.GetUserActions", parValues)
    '            'Ds.Tables(0).TableName = Table
    '        Else
    '            Dim parvalues() = {UserId}
    '            'Ds = Server.Con(True).ExecuteDataset("ClsActions_GetUserActions", parvalues)
    '            Ds = Server.Con(True).ExecuteDataset("zsp_users_100_GetUserAction", parvalues)
    '            ' Ds.Tables(0).TableName = Table
    '        End If
    '        Return Ds
    '    Catch
    '    End Try
    'End Function

    Public Shared Sub SaveActioninDB(ByVal ObjectId As Int64, ByVal ObjectType As ObjectTypes, ByVal ActionType As RightsType, ByVal S_Object_ID As String, ByVal _userid As Int64, ByVal ConnectionId As Int32)
        Dim ObjectTypeId As Integer
        Dim ActionTypeId As Integer
        ActionTypeId = ActionType
        ObjectTypeId = ObjectType

        If String.IsNullOrEmpty(S_Object_ID) Then S_Object_ID = " "
        If Server.isOracle Then
            Dim parnames() As String = {"AID", "AUSRID", "AOBJID", "AOBJTID", "ATYPE", "ACONID", "SOBJECTID"}
            ' Dim parTypes() As Object = {13, 13, 13, 13, 13, 13, 13}
            Dim parvalues() As Object = {CoreData.GetNewID(IdTypes.USERHSTID), _userid, ObjectId, ObjectTypeId, ActionTypeId, ConnectionId, S_Object_ID}
            'Server.Con(True).ExecuteNonQuery("ACTIONS_PKG.Save_Action", parValues)
            Server.Con(True).ExecuteNonQuery("zsp_imports_100.InsertUserAction", parvalues)
        Else
            Dim parValues() As Object = {CoreData.GetNewID(IdTypes.USERHSTID), _userid, ObjectId, ObjectTypeId, ActionTypeId, ConnectionId, S_Object_ID}
            'Server.Con(True).ExecuteNonQuery("SAVE_ACTION", parvalues)
            Server.Con(True).ExecuteNonQuery("zsp_imports_100_InsertUserAction", parValues)
        End If
        Server.U_Time = Now
    End Sub

    ''' <summary>
    ''' Devuelve la fecha de la ultima modificacion del documento
    ''' </summary>
    ''' <returns>
    '''     Devuelve 0 en caso de no existir licencias disponibles
    '''     Devuelve 1 en caso de que las consultas se hayan realizado con éxito
    '''     Devuelve 2 en caso de tener máximo de licencias conectadas
    ''' </returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Tomas] 29/06/2009  Created    Se adapta el método para un nuevo manejo de logueo de acciones del usuario  
    '''</history>
    Public Shared Function SaveActioninDB(ByVal ObjectId As Int64, ByVal ObjectType As ObjectTypes, ByVal ActionType As RightsType, ByVal S_Object_ID As String, ByVal _userid As Int64, ByVal ConnectionId As Int32, ByVal machineName As String) As Integer
        Dim ObjectTypeId As Integer
        Dim ActionTypeId As Integer
        Dim ActionTypeDesc As String
        Dim res As Integer
        Dim ds As DataSet
        ActionTypeId = ActionType
        ActionTypeDesc = ActionType.ToString()
        ObjectTypeId = ObjectType

        If String.IsNullOrEmpty(S_Object_ID) Then S_Object_ID = " "

        If Server.isOracle Then
            Dim parnames() As String = {"ACONID", "WIN_PC", "AUSRID", "AOBJID", "AOBJTID", "ATYPE", "ATYPEDESC", "SOBJECTID", "io_cursor"}
            ' Dim parTypes() As Object = {OracleType.Number, OracleType.VarChar, OracleType.Number, OracleType.Number, OracleType.Number,             OracleType.Number, OracleType.VarChar, OracleType.VarChar, OracleType.Cursor}
            Dim parvalues() As Object = {ConnectionId, machineName, _userid, ObjectId, ObjectTypeId, ActionTypeId, ActionTypeDesc, S_Object_ID, 2}
            ds = Server.Con.ExecuteDataset("ZSP_IMPORTS_300.InsertUserAction", parvalues)
            If Not IsNothing(ds) AndAlso ds.Tables(0).Rows.Count > 0 Then
                res = CInt(ds.Tables(0).Rows(0)(0))
            End If
        Else
            Dim parValues() As Object = {ConnectionId, machineName, _userid, ObjectId, ObjectTypeId, ActionTypeId, ActionTypeDesc, S_Object_ID}
            res = Server.Con(True).ExecuteScalar("zsp_imports_300_InsertUserActionWeb", parValues)
        End If

        Server.U_Time = Now
        Return res
    End Function

    Public Shared Sub SaveActionWebViewInDB(ByVal ObjectId As Int64, ByVal ObjectType As ObjectTypes, ByVal ActionType As RightsType, ByVal S_Object_ID As String, ByVal _userid As Int64)
        Dim SQL As String

        SQL = "INSERT INTO USER_HST "
        SQL &= "(ACTION_ID,USER_ID,ACTION_DATE,OBJECT_ID,OBJECT_TYPE_ID,ACTION_TYPE,S_OBJECT_ID) "
        SQL &= "VALUES ("
        SQL &= CoreData.GetNewID(IdTypes.USERHSTID) & ", "
        SQL &= _userid & ", "

        If Server.isOracle Then
            SQL &= " sysdate, "
        Else
            SQL &= " getdate(), "
        End If

        SQL &= ObjectId & ", "
        SQL &= ObjectType & ", "
        SQL &= ActionType & ", '"
        SQL &= Replace(S_Object_ID, "'", "''") & "') "

        Server.Con(True).ExecuteNonQuery(CommandType.Text, SQL)

    End Sub


    ''' <summary>
    ''' Devuelve la fecha de la ultima modificacion del documento
    ''' </summary>
    ''' <returns>
    '''     Devuelve 0 en caso de no existir licencias disponibles
    '''     Devuelve 1 en caso de que las consultas se hayan realizado con éxito
    '''     Devuelve 2 en caso de tener máximo de licencias conectadas
    ''' </returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Tomas] 29/06/2009  Created    Se adapta el método para un nuevo manejo de logueo de acciones del usuario  
    '''</history>
    Public Shared Function SaveActioninDB(ByVal ObjectId As Int64, ByVal ObjectType As ObjectTypes, ByVal ActionType As RightsType, ByVal S_Object_ID As String, ByVal _userid As Int64, ByVal ConnectionId As Int32, ByVal machineName As String, ByRef t As Transaction) As Integer
        Dim ObjectTypeId As Integer
        Dim ActionTypeId As Integer
        Dim res As Integer
        Dim ds As DataSet
        ActionTypeId = ActionType
        ObjectTypeId = ObjectType

        If String.IsNullOrEmpty(S_Object_ID) Then S_Object_ID = " "

        If Server.IsOracle Then
            Dim parvalues() As Object = {ConnectionId, machineName, _userid, ObjectId, ObjectTypeId, ActionTypeId, S_Object_ID, 2}
            ds = t.Con.ExecuteDataset(t.Transaction, "zsp_imports_200.InsertUserAction", parvalues)
            If Not IsNothing(ds) AndAlso ds.Tables(0).Rows.Count > 0 Then
                res = CInt(ds.Tables(0).Rows(0)(0))
            End If
        Else
            Dim parValues() As Object = {ConnectionId, machineName, _userid, ObjectId, ObjectTypeId, ActionTypeId, S_Object_ID}
            res = t.Con.ExecuteScalar(t.Transaction, "zsp_imports_200_InsertUserAction", parValues)
        End If

        Server.U_Time = Now
        Return res
    End Function

    Private Sub New()
    End Sub
  

End Class