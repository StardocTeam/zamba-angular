Imports Zamba.Core
Imports System.Text

Public Class DynamicButtonFactory

    ''' <summary>
    ''' Obtiene los botones dinámicos. Se puede filtrar por tipo de botón.
    ''' </summary>
    ''' <param name="filter">Filtro para obtener unicamente los botones requeridos</param>
    ''' <returns>DataTable con los botones</returns>
    ''' <remarks></remarks>
    Public Shared Function GetButtons(Optional ByVal filter As ButtonType = ButtonType.All, _
                                      Optional ByVal wfId As Int64 = 0) As DataTable

        Dim query As String = "SELECT B.ID, B.CAPTION, B.BUTTONID, P.PLACEDESC, T.TYPEDESC, B.PARAMS, B.NEEDRIGHTS, B.VIEWCLASS, B.BUTTONORDER, W.NAME AS WFNAME, B.CREATEDATE, B.MODIFIEDDATE, B.ICONID, B.GroupName, B.GroupClass "
        query &= "FROM ZBUTTONS B "
        query &= "INNER JOIN ZBUTTONSPLACE P ON B.PLACEID = P.PLACEID "
        query &= "INNER JOIN ZBUTTONSTYPE T ON B.TYPEID = T.TYPEID "
        query &= "LEFT JOIN WFWORKFLOW W ON B.WFID = W.WORK_ID "

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
    Public Shared Function GetButtons(ByVal filter As ButtonType, _
                                      ByVal place As ButtonPlace) As DataTable

        Dim query As String = "SELECT b.BUTTONID, b.PARAMS, b.NEEDRIGHTS, b.CAPTION, b.BUTTONORDER, b.WFID, b.ICONID,b.VIEWCLASS, b.GroupName, b.GroupClass,r.step_id FROM ZBUTTONS B inner join wfrules r on r.id = replace(b.BUTTONID,'zamba_rule_','') WHERE PLACEID= " & CInt(place)


        If filter <> ButtonType.All Then
            query &= " AND TYPEID=" & CInt(filter)
        End If
        query &= " order by BUTTONORDER"

        Return Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)

    End Function

    ''' <summary>
    ''' Obtiene los botones de tipo regla para un lugar específico
    ''' </summary>
    ''' <param name="filter"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetRuleButtons(ByVal place As ButtonPlace, _
                                          Optional ByVal wfId As Int64 = 0) As DataTable

        Dim query As String = "SELECT REPLACE(BUTTONID, 'zamba_rule_', '') AS RULEID, BUTTONID, PARAMS, NEEDRIGHTS, CAPTION, BUTTONORDER, WFID, z.ICONID, step_id, z.GroupName, z.GroupClass FROM ZBUTTONS z inner join wfrules r on REPLACE(z.BUTTONID, 'zamba_rule_', '') = r.id WHERE TYPEID = 0 And PLACEID = " & CInt(place)

        If wfId > 0 Then
            query &= " AND (WFID = " & wfId.ToString & " OR WFID = 0)"
        End If

        Return Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)

    End Function

    ''' <summary>
    ''' Obtiene las ubicaciones de los botones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetPlaces() As DataTable

        'se agrega una condicion para no mostrar el item 
        '"Pantalla de Inicio Web" ya que todavia no tiene
        'funcionalidad, quitar el where una vez creada

        Dim query As String = "SELECT * FROM ZBUTTONSPLACE  WHERE placeid <> 4"
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
    Public Shared Sub AddButton(ByVal buttonId As String, _
                            ByVal placeId As ButtonPlace, _
                            ByVal typeId As ButtonType, _
                            ByVal params As String, _
                            ByVal needRights As Boolean, _
                            ByVal viewClass As String, _
                            ByVal caption As String, _
                            ByVal buttonOrder As String, _
                            ByVal wfId As Int64, _
                            ByVal idicon As Int32)

        Dim id As Int32 = CoreData.GetNewID(Zamba.Core.IdTypes.ZButtons)
        Dim query As New StringBuilder()

        query.Append("INSERT INTO ZBUTTONS(ID, ButtonID,PlaceID,TypeID,Params,NeedRights,ViewClass,Caption,ButtonOrder,CreateDate,ModifiedDate,WFID,iconId) VALUES(")
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
        query.Append(")")

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
    Public Shared Sub UpdateButton(ByVal id As Int64, _
                                   ByVal buttonId As String, _
                                    ByVal placeId As ButtonPlace, _
                                    ByVal typeId As ButtonType, _
                                    ByVal params As String, _
                                    ByVal needRights As Boolean, _
                                    ByVal viewClass As String, _
                                    ByVal caption As String, _
                                    ByVal buttonOrder As String, _
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
    Public Shared Sub UpdateRuleButton(ByVal id As Int64, _
                                        ByVal buttonId As String, _
                                        ByVal place As ButtonPlace, _
                                        ByVal order As Int32, _
                                        ByVal caption As String, _
                                        ByVal params As String, _
                                        ByVal needRights As Boolean, _
                                        ByVal wfId As Int64,
                                        ByVal idIcon As Int32)

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