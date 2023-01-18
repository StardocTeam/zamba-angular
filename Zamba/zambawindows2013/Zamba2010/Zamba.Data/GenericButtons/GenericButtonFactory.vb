Imports Zamba.Core
Imports System.Text

Public Class GenericButtonFactory

    ''' <summary>
    ''' Obtiene los botones dinámicos. Se puede filtrar por tipo de botón.
    ''' </summary>
    ''' <param name="filter">Filtro para obtener unicamente los botones requeridos</param>
    ''' <returns>DataTable con los botones</returns>
    ''' <remarks></remarks>
    Public Shared Function GetButtons(Optional ByVal filter As ButtonType = ButtonType.All,
                                      Optional ByVal wfId As Int64 = 0) As DataTable
        Dim query As String = "SELECT B.ID, B.CAPTION, B.BUTTONID, P.PLACEDESC, T.TYPEDESC, B.PARAMS, B.NEEDRIGHTS, B.VIEWCLASS, B.BUTTONORDER, W.NAME AS WFNAME, B.CREATEDATE, B.MODIFIEDDATE, B.ICONID, B.GroupName , B.GroupClass "
        query &= "FROM ZBUTTONS B "
        query &= "INNER JOIN ZBUTTONSPLACE P ON B.PLACEID = P.PLACEID "
        query &= "INNER JOIN ZBUTTONSTYPE T ON B.TYPEID = T.TYPEID "
        query &= "LEFT JOIN WFWORKFLOW W ON B.WFID = W.WORK_ID AND W.WORK_ID > 0 "

        If filter <> ButtonType.All Then
            If filter = ButtonType.Rule AndAlso wfId > 0 Then
                query &= "WHERE (W.WORK_ID=" & wfId.ToString & " OR B.WFID=0) AND B.TYPEID=" & CInt(filter)
            Else
                query &= "WHERE B.TYPEID=" & CInt(filter)
            End If
        End If

        Return Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)
    End Function

    ''' <summary>
    ''' Obtiene los botones para un lugar específico
    ''' </summary>
    ''' <param name="filter"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetButtons(ByVal filter As ButtonType,
                                      ByVal place As ButtonPlace) As DataTable
        Dim query As String = "SELECT BUTTONID, PARAMS, NEEDRIGHTS, CAPTION, BUTTONORDER, WFID, ICONID FROM ZBUTTONS WHERE PLACEID=" & CInt(place) & " AND TYPEID=" & CInt(filter)
        Return Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)
    End Function

    ''' <summary>
    ''' Obtiene los botones de tipo regla para un lugar específico
    ''' </summary>
    ''' <param name="filter"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetRuleButtons(ByVal place As ButtonPlace,
                                          Optional ByVal wfId As Int64 = 0) As DataTable
        Dim query As String = "SELECT REPLACE(BUTTONID, 'zamba_rule_', '') AS RULEID, BUTTONID, PARAMS, NEEDRIGHTS, CAPTION, BUTTONORDER, WFID, z.ICONID, step_id, groupname, groupclass, viewclass FROM ZBUTTONS z inner join wfrules r on REPLACE(z.BUTTONID, 'zamba_rule_', '') = r.id WHERE TYPEID = 0 And PLACEID = " & CInt(place)
        If wfId > 0 Then
            query &= " AND (WFID = " & wfId.ToString & " OR WFID = 0)"
        End If

        Return Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)

    End Function

    Public Shared Function GetZbuttonsAndGroups() As DataTable

        Dim query As String = "Select * from ZButtonsAndGroups"

        Return Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)

    End Function

    Public Shared Function InsertZbuttonClassRule(ByVal idRule As Integer, ByVal idClass As Integer)

        Dim query As String = "Insert into BUTTONRULE_R_BTNANDGROUP values( " & idRule.ToString & "," & idClass.ToString & ")"
        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)


    End Function
    ''' <summary>
    ''' Obtiene las ubicaciones de los botones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetPlaces() As DataTable
        Dim query As String = "SELECT * FROM ZBUTTONSPLACE"
        Return Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)
    End Function

    ''' <summary>
    ''' Obtiene los tipos de botones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetTypes() As DataTable
        Dim query As String = "SELECT * FROM ZBUTTONSTYPE"
        Return Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)
    End Function

    ''' <summary>
    ''' Inserta un botón dinámico.
    ''' </summary>
    ''' <param name="buttonId"></param>
    ''' <param name="buttonPlace"></param>
    ''' <param name="actionName"></param>
    ''' <param name="params"></param>
    ''' <param name="needPermissions"></param>
    ''' <param name="viewClass"></param>
    ''' <param name="caption"></param>
    ''' <param name="buttonOrder"></param>
    ''' <remarks></remarks>
    Public Shared Sub AddButton(ByVal buttonId As String,
                            ByVal placeId As ButtonPlace,
                            ByVal typeId As ButtonType,
                            ByVal params As String,
                            ByVal needRights As Boolean,
                            ByVal viewClass As String,
                            ByVal caption As String,
                            ByVal buttonOrder As String,
                            ByVal wfId As Int64,
                            ByVal idicon As Int32,
                            ByVal groupName As String,
                            ByVal groupClass As String)

        Dim id As Int32 = 1

        Dim idO As Object = Server.Con.ExecuteScalar(CommandType.Text, "select max(id) + 1 from zbuttons")

        If idO Is Nothing OrElse Int32.TryParse(idO, id) = False Then
            id = CoreData.GetNewID(Zamba.Core.IdTypes.ZButtons)
        End If

        Dim query As New StringBuilder()

        query.Append("INSERT INTO ZBUTTONS(ID, ButtonID,PlaceID,TypeID,Params,NeedRights,ViewClass,Caption,ButtonOrder,CreateDate,ModifiedDate,WFID,iconId,GroupName,GroupClass) VALUES(")
        query.Append(id)
        query.Append(",'")
        query.Append(buttonId)
        query.Append("',")
        query.Append(placeId)
        query.Append(",")
        query.Append(typeId)
        query.Append(",'")
        query.Append(params)
        query.Append("',")
        query.Append(CInt(needRights))
        query.Append(",'")
        query.Append(viewClass)
        query.Append("','")
        query.Append(caption)
        query.Append("',")
        query.Append(buttonOrder)
        'Dependiendo de la base de datos, se usa una función distinta.
        If Server.isOracle Then
            query.Append(",sysdate,sysdate,")
        Else
            query.Append(",getdate(),getdate(),")
        End If
        query.Append(wfId)
        query.Append(",")
        query.Append(idicon)
        query.Append(",'")
        query.Append(groupName)
        query.Append("','")
        query.Append(groupClass)
        query.Append("')")

        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
        query = Nothing

    End Sub

    ''' <summary>
    ''' Actualiza un botón dinámico.
    ''' </summary>
    ''' <param name="buttonId"></param>
    ''' <param name="buttonPlace"></param>
    ''' <param name="actionName"></param>
    ''' <param name="params"></param>
    ''' <param name="needPermissions"></param>
    ''' <param name="viewClass"></param>
    ''' <param name="caption"></param>
    ''' <param name="buttonOrder"></param>
    ''' <remarks></remarks>
    Public Shared Sub UpdateButton(ByVal id As Int64,
                                   ByVal buttonId As String,
                                    ByVal placeId As ButtonPlace,
                                    ByVal typeId As ButtonType,
                                    ByVal params As String,
                                    ByVal needRights As Boolean,
                                    ByVal viewClass As String,
                                    ByVal caption As String,
                                    ByVal buttonOrder As String,
                                    ByVal wfId As Int64,
                                    ByVal idIcon As Int32)

        Dim query As New StringBuilder()

        'Se arma el Update para SQL SERVER
        query.Append("UPDATE ZBUTTONS SET BUTTONID='")
        query.Append(buttonId)
        query.Append("',PLACEID=")
        query.Append(placeId)
        query.Append(",TYPEID=")
        query.Append(typeId)
        query.Append(",PARAMS='")
        query.Append(params)
        query.Append("',NEEDRIGHTS=")
        query.Append(CInt(needRights))
        query.Append(",VIEWCLASS='")
        query.Append(viewClass)
        query.Append("',CAPTION='")
        query.Append(caption)
        query.Append("',BUTTONORDER=")
        query.Append(buttonOrder)
        query.Append(",WFID=")
        query.Append(wfId)
        query.Append(",iconId=")
        query.Append(idIcon)
        If Server.isOracle Then
            query.Append(",MODIFIEDDATE=sysdate WHERE ID=")
        Else
            query.Append(",MODIFIEDDATE=getdate() WHERE ID=")
        End If
        query.Append(id)

        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
        query = Nothing

    End Sub

    ''' <summary>
    ''' Actualiza un botón de tipo regla.
    ''' </summary>
    ''' <param name="buttonId"></param>
    ''' <param name="buttonPlace"></param>
    ''' <param name="actionName"></param>
    ''' <param name="params"></param>
    ''' <param name="needPermissions"></param>
    ''' <param name="viewClass"></param>
    ''' <param name="caption"></param>
    ''' <param name="buttonOrder"></param>
    ''' <remarks></remarks>
    Public Shared Sub UpdateRuleButton(ByVal id As Int64,
                                        ByVal buttonId As String,
                                        ByVal place As ButtonPlace,
                                        ByVal order As Int32,
                                        ByVal caption As String,
                                        ByVal params As String,
                                        ByVal needRights As Boolean,
                                        ByVal wfId As Int64,
                                        ByVal idIcon As Int32,
                                        ByRef cssClass As String,
                                    ByRef groupName As String,
                                    ByRef groupClass As String)

        Dim query As New StringBuilder()

        query.Append("UPDATE ZBUTTONS SET BUTTONID='")
        query.Append(buttonId)
        query.Append("',PLACEID=")
        query.Append(place)
        query.Append(",PARAMS='")
        query.Append(params)
        query.Append("',NEEDRIGHTS=")
        query.Append(CInt(needRights))
        query.Append(",CAPTION='")
        query.Append(caption)
        query.Append("',BUTTONORDER=")
        query.Append(order)
        query.Append(",WFID=")
        query.Append(wfId)
        query.Append(",iconId=")
        query.Append(idIcon)
        query.Append(",ViewClass='")
        query.Append(cssClass)
        query.Append("', GroupName='")
        query.Append(groupName)
        query.Append("', GroupClass='")
        query.Append(groupClass)
        query.Append("'")
        'Dependiendo de la base de datos, se utiliza una función distinta de fecha.
        If Server.isOracle Then
            query.Append(",MODIFIEDDATE=sysdate WHERE ID='")
        Else
            query.Append(",MODIFIEDDATE=getdate() WHERE ID='")
        End If
        query.Append(id)
        query.Append("'")

        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
        query = Nothing

    End Sub

    Public Shared Sub SetNewZButtonsAndGroups(text As String, v As Integer)
        Dim query As String = "Insert into ZButtonsAndGroups values(" & v.ToString & ",'" & text & "')"

        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)


    End Sub

    ''' <summary>
    ''' Elimina un botón dinámico.
    ''' </summary>
    ''' <param name="buttonId"></param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteButton(ByVal id As Int64)

        Dim query As String = "DELETE ZBUTTONS WHERE ID=" & id.ToString
        Server.Con.ExecuteNonQuery(CommandType.Text, query)

    End Sub

    ''' <summary>
    ''' Verifica la existencia de un botón dinámico
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns>True si existe</returns>
    ''' <remarks></remarks>
    Public Shared Function CheckExistance(ByVal id As Int64) As Boolean
        Return CBool(Server.Con.ExecuteScalar(CommandType.Text, "SELECT COUNT(1) FROM ZBUTTONS WHERE ID = " & id.ToString))
    End Function

End Class
