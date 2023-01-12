Imports Zamba.Data
Imports Zamba.Core.WF.WF

''' <summary>
''' Se encarga de manejar los botones dinámicos de Zamba.
''' Son botones que se pueden configurar desde el administrador y 
''' se pueden ubicar en diferentes partes de Zamba. 
''' </summary>
''' <remarks></remarks>
Public Class GenericButtonBusiness
    ''' <summary>
    ''' Obtiene los botones dinámicos. Se puede filtrar por tipo de botón.
    ''' </summary>
    ''' <param name="filter">Filtro para obtener unicamente los botones requeridos</param>
    ''' <returns>DataTable con los botones</returns>
    ''' <remarks></remarks>
    Public Shared Function GetButtons(Optional ByVal filter As ButtonType = ButtonType.All, _
                                      Optional ByVal wfId As Int64 = 0) As DataTable
        Return GenericButtonFactory.GetButtons(filter, wfId)
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
        Return GenericButtonFactory.GetButtons(filter, place)
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
        Return GenericButtonFactory.GetRuleButtons(place, wfId)
    End Function

    ''' <summary>
    ''' Obtiene las ubicaciones de los botones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetPlaces() As DataTable
        Return GenericButtonFactory.GetPlaces()
    End Function

    ''' <summary>
    ''' Obtiene los tipos de botones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetTypes() As DataTable
        Return GenericButtonFactory.GetTypes()
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
    ''' <remarks>TODO: PASAR LOS PARAMETROS A UN OBJETO UNICO</remarks>
    Public Shared Sub AddButton(ByVal buttonId As String, _
                                ByVal buttonPlace As ButtonPlace, _
                                ByVal actionName As ButtonType, _
                                ByVal params As String, _
                                ByVal needRights As Boolean, _
                                ByVal viewClass As String, _
                                ByVal caption As String, _
                                ByVal buttonOrder As String, _
                                ByVal wfId As Int64, _
                                ByVal idIcon As Int32, _
                                ByVal groupName As String, _
                                ByVal groupClass As String)

        'Valida la longitud de los campos
        If buttonId.Length > 100 Then Throw New ArgumentOutOfRangeException(buttonId, "El ID no puede superar los 100 caracteres.")
        If params.Length > 500 Then Throw New ArgumentOutOfRangeException(params, "Los parámetros no pueden superar los 500 caracteres.")
        If viewClass.Length > 50 Then Throw New ArgumentOutOfRangeException(viewClass, "La clase no puede superar los 50 caracteres.")
        If caption.Length > 100 Then Throw New ArgumentOutOfRangeException(caption, "La leyenda no puede superar los 100 caracteres.")

        GenericButtonFactory.AddButton(buttonId, buttonPlace, actionName, params, needRights, viewClass, caption, buttonOrder, wfId, idIcon, groupName, groupClass)

    End Sub

    ''' <summary>
    ''' Agrega un botón de tipo regla.
    ''' </summary>
    ''' <param name="ruleID"></param>
    ''' <param name="place"></param>
    ''' <param name="order"></param>
    ''' <param name="caption"></param>
    ''' <param name="params"></param>
    ''' <remarks>TODO: PASAR LOS PARAMETROS A UN OBJETO UNICO</remarks>
    Public Shared Sub AddRuleButton(ByVal ruleID As Int64, _
                                    ByVal place As ButtonPlace, _
                                    ByVal order As Int32, _
                                    ByVal caption As String, _
                                    ByVal params As String, _
                                    ByVal needRights As Boolean, _
                                    ByVal wfId As Int64,
                                    ByVal idIcon As Int32, _
                                    ByRef cssClass As String, _
                                    ByRef groupName As String, _
                                    ByRef groupClass As String)
        AddButton("zamba_rule_" & ruleID.ToString, place, ButtonType.Rule, params, needRights, cssClass, caption, order, wfId, idIcon, groupName, groupClass)
    End Sub

    ''' <summary>
    ''' Actualiza un botón dinámico.
    ''' </summary>
    ''' <param name="ruleID"></param>
    ''' <param name="place"></param>
    ''' <param name="order"></param>
    ''' <param name="caption"></param>
    ''' <param name="params"></param>
    ''' <remarks>TODO: PASAR LOS PARAMETROS A UN OBJETO UNICO</remarks>
    Public Shared Sub UpdateButton(ByVal id As Int64, _
                                    ByVal buttonId As String, _
                                    ByVal place As ButtonPlace, _
                                    ByVal actionName As ButtonType, _
                                    ByVal params As String, _
                                    ByVal needRights As Boolean, _
                                    ByVal viewClass As String, _
                                    ByVal caption As String, _
                                    ByVal buttonOrder As String, _
                                    ByVal wfId As Int64,
                                    ByVal idIcon As Int32)

        'Valida la longitud de los campos
        If params.Length > 500 Then Throw New ArgumentOutOfRangeException(params, "Los parámetros no pueden superar los 500 caracteres.")
        If viewClass.Length > 50 Then Throw New ArgumentOutOfRangeException(viewClass, "La clase no puede superar los 50 caracteres.")
        If caption.Length > 100 Then Throw New ArgumentOutOfRangeException(caption, "La leyenda no puede superar los 100 caracteres.")

        GenericButtonFactory.UpdateButton(id, buttonId, place, actionName, params, needRights, viewClass, caption, buttonOrder, wfId, idIcon)

    End Sub

    ''' <summary>
    ''' Actualiza un botón de tipo regla.
    ''' </summary>
    ''' <param name="ruleID"></param>
    ''' <param name="place"></param>
    ''' <param name="order"></param>
    ''' <param name="caption"></param>
    ''' <param name="params"></param>
    ''' <remarks></remarks>
    Public Shared Sub UpdateRuleButton(ByVal id As Int64, _
                                        ByVal ruleID As Int64, _
                                        ByVal place As ButtonPlace, _
                                        ByVal order As Int32, _
                                        ByVal caption As String, _
                                        ByVal params As String, _
                                        ByVal needRights As Boolean, _
                                        ByVal wfId As Int64, _
                                        ByVal idIcon As Int32, _
                                        ByRef cssClass As String, _
                                    ByRef groupName As String, _
                                    ByRef groupClass As String)
        GenericButtonFactory.UpdateRuleButton(id, "zamba_rule_" & ruleID.ToString, place, order, caption, params, needRights, wfId, idIcon, cssClass, groupName, groupClass)
    End Sub

    ''' <summary>
    ''' Elimina un botón dinámico.
    ''' </summary>
    ''' <param name="buttonId"></param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteButton(ByVal id As Int64)

        GenericButtonFactory.DeleteButton(id)

    End Sub

    ''' <summary>
    ''' Verifica la existencia de un botón dinámico
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns>True si existe</returns>
    ''' <remarks></remarks>
    Public Shared Function CheckExistance(ByVal id As Int64) As Boolean

        Return GenericButtonFactory.CheckExistance(id)

    End Function

  

End Class
