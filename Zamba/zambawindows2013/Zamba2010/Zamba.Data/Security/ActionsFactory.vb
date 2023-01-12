Imports Zamba.Core

Public Class ActionsFactory

    Public Event LogError(ByVal ex As Exception)

    Public Shared Function GetDocumentActions(ByVal documentId As Int64) As DataSet 'DSActions
        Dim Table As String = "VisHistory"
        Dim dstemp As DataSet = Nothing

        If Server.IsOracle Then
            ''Dim parNames() As String = {"DocumentId", "io_cursor"}
            'Dim parTypes() As Object = {13, 5}
            Dim parValues() As Object = {documentId, 2}

            dstemp = Server.Con(True).ExecuteDataset("zsp_doctypes_100.GetDocumentActions", parValues)
            dstemp.Tables(0).TableName = Table
        Else
            Dim parameters() As Object = {documentId}
            'dstemp = Server.Con(True).ExecuteDataset("ClsActions_GetDocumentActions", parameters)
            dstemp = Server.Con(True).ExecuteDataset("zsp_users_100_GetDocumentAction", parameters)
            dstemp.Tables(0).TableName = Table
            'DsActions.Merge(DSTEMP)

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
            ''Dim parNames() As String = {"DocumentId", "io_cursor"}
            'Dim parTypes() As Object = {13, 5}
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
            ''Dim parNames() As String = {"UserId", "io_cursor"}
            'Dim parTypes() As Object = {13, 5}
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
    '        ''Dim parNames() As String = {"DocumentId", "io_cursor"}
    '        Dim parTypes() = {13, 5}
    '        Dim parValues() = {DocumentId, 2}

    '        'dstemp = Server.Con(True).ExecuteDataset("GETDOCUMENTACTIONS_PKG.GetDocumentActions",  parValues)
    '        dstemp = Server.Con(True).ExecuteDataset("zsp_doctypes_100.GetDocumentActions",  parValues)
    '        dstemp.Tables(0).TableName = Table

    '        '--------------------LINEAS AGREGADAS------------------
    '        'dstemp.Tables(0).Columns(0).ColumnName = "NOOOMBRE"
    '        '------------------------------------------------------

    '        DsActions.Merge(DSTEMP)
    '    Else
    '        Dim dstemp As DataSet
    '        Dim parameters() = {DocumentId}
    '        'dstemp = Server.Con(True).ExecuteDataset("ClsActions_GetDocumentActions", parameters)
    '        dstemp = Server.Con(True).ExecuteDataset("zsp_users_100_GetDocumentAction", parameters)
    '        dstemp.Tables(0).TableName = Table
    '        'DsActions.Merge(DSTEMP)
    '        Return dstemp
    '    End If
    '    Return DsActions
    'End Function

    'Public Shared Function GetUserActions(ByVal UserId As Integer) As DataSet

    '    Try
    '        'Dim Table As String = "VisHistory"
    '        Dim Ds As DataSet

    '        if Server.IsOracle then
    '            ''Dim parNames() As String = {"UserId", "io_cursor"}
    '            Dim parTypes() = {13, 5}
    '            Dim parValues() = {UserId, 2}
    '            'Ds = Server.Con(True).ExecuteDataset("CLSACTIONS_GETUSERACTIONS_PKG.getUserActions",  parValues)
    '            Ds = Server.Con(True).ExecuteDataset("zsp_users_100.GetUserActions",  parValues)
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
        Dim ActionTypeDesc As String
        Dim res As Integer
        Dim ds As DataSet
        ActionTypeId = ActionType
        ActionTypeDesc = ActionType.ToString()
        ObjectTypeId = ObjectType

        If String.IsNullOrEmpty(S_Object_ID) Then S_Object_ID = " "

        If Server.isOracle Then
            Dim parvalues() As Object = {ConnectionId, machineName, _userid, ObjectId, ObjectTypeId, ActionTypeId, ActionTypeDesc, S_Object_ID, 2}
            ds = Server.Con(True).ExecuteDataset("ZSP_IMPORTS_300.InsertUserAction", parvalues)
            If Not IsNothing(ds) AndAlso ds.Tables(0).Rows.Count > 0 Then
                res = CInt(ds.Tables(0).Rows(0)(0))
            End If
        Else
            Dim parValues() As Object = {ConnectionId, machineName, _userid, ObjectId, ObjectTypeId, ActionTypeId, ActionTypeDesc, S_Object_ID}

            If t Is Nothing Then
                res = Server.Con(True).ExecuteScalar("zsp_imports_400_InsertUserAction", parValues)
            Else
                res = Server.Con(True).ExecuteScalar(t.Transaction, "zsp_imports_400_InsertUserAction", parValues)
            End If

        End If

        Server.U_Time = Now
        '''Server.Con.ExecuteNonQuery(CommandType.Text, String.Format("update usrtable set LUPDATE = {1} where id = {0}", _userid, If(Server.isOracle, "sysdate", "getdate()")))

        Return res
    End Function

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


    Private Sub New()
    End Sub
    Public Shared Sub CleanExceptions()
        'Try
        '    if Server.IsOracle then
        '        'Server.Con.ExecuteNonQuery(CommandType.StoredProcedure, "DelExcepTable_PKG.DelExcepTable")
        '        Server.Con.ExecuteNonQuery(CommandType.StoredProcedure, "zsp_exception_100.DeleteExceptionTable")
        '    Else
        '        'Server.Con.ExecuteNonQuery(CommandType.StoredProcedure, "DelExcepTable")
        '        Server.Con.ExecuteNonQuery(CommandType.StoredProcedure, "zsp_exception_100_DeleteExceptionTable")
        '    End If
        'Catch ex As Exception
        'End Try
        Dim I As Int32
        Dim Dir As IO.DirectoryInfo
        Try
            Dir = New IO.DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Zamba Software\Exceptions")
            If Dir.Exists = False Then
                Dir.Create()
            End If
        Catch
            Dir = New IO.DirectoryInfo(System.Windows.Forms.Application.StartupPath & "\Exceptions")
            If Dir.Exists = False Then
                Dir.Create()
            End If
        End Try
        Dim archivos() As IO.FileInfo = Dir.GetFiles

        If archivos.Length = 0 Then
            Exit Sub
        Else
            For I = 0 To archivos.Length - 1
                If archivos(I).CreationTime.Month < Now.Month Then
                    archivos(I).Delete()
                End If
            Next
        End If
    End Sub

End Class