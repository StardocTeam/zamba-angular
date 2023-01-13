Imports Zamba.Data
Imports System.Collections.Generic

''' <summary>
''' Se encarga de manejar los botones dinámicos de Zamba.
''' Son botones que se pueden configurar desde el administrador y 
''' se pueden ubicar en diferentes partes de Zamba. 
''' </summary>
''' <remarks></remarks>
Public Class DynamicButtonBusiness
    Private Shared _hsSingletonInstances As New Dictionary(Of String, DynamicButtonBusiness)
    Private _lsHomeButtons As List(Of IDynamicButton)
    Private _lsHeaderButtons As List(Of IDynamicButton)

    Private Sub New()
        _lsHomeButtons = Nothing
        _lsHeaderButtons = Nothing
    End Sub

    ''' <summary>
    ''' Obtiene la instancia del modelo de botones del usuario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetInstance() As DynamicButtonBusiness
        If Membership.MembershipHelper.isWeb Then
            Dim zCoreKey As String = Membership.MembershipHelper.CurrentUser.ID
            If Not _hsSingletonInstances.ContainsKey(zCoreKey) Then
                _hsSingletonInstances.Add(zCoreKey, New DynamicButtonBusiness())
            End If
            Return _hsSingletonInstances.Item(zCoreKey)
        Else
            Return New DynamicButtonBusiness()
        End If
    End Function

    ''' <summary>
    ''' Remueve la instancia del modelo de botones del usuario
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub RemoveCurrentInstance()
        If Membership.MembershipHelper.isWeb AndAlso Membership.MembershipHelper.CurrentUser IsNot Nothing Then

            Dim zCoreKey As String = Membership.MembershipHelper.CurrentUser.ID
            If _hsSingletonInstances.ContainsKey(zCoreKey) Then
                _hsSingletonInstances.Remove(zCoreKey)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Obtiene los botones dinámicos. Se puede filtrar por tipo de botón.
    ''' </summary>
    ''' <param name="filter">Filtro para obtener unicamente los botones requeridos</param>
    ''' <returns>DataTable con los botones</returns>
    ''' <remarks></remarks>
    Public Function GetButtons(Optional ByVal filter As ButtonType = ButtonType.All, _
                                      Optional ByVal wfId As Int64 = 0) As List(Of IDynamicButton)

        Dim dt As DataTable = DynamicButtonFactory.GetButtons(filter, wfId)
        Dim rowCount As Integer = dt.Rows.Count
        Dim list As New List(Of IDynamicButton)

        For i As Integer = 0 To rowCount - 1
            list.Add(New ZDynamicButton(dt.Rows(i)))
        Next

        Return list
    End Function

    ''' <summary>
    ''' Obtiene los botones para un lugar específico
    ''' </summary>
    ''' <param name="filter"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetButtons(ByVal filter As ButtonType,
                                      ByVal place As ButtonPlace, ByVal user As IUser) As List(Of IDynamicButton)

        Dim dt As DataTable = DynamicButtonFactory.GetButtons(filter, place)
        Dim rowCount As Integer = dt.Rows.Count
        Dim list As New List(Of IDynamicButton)
        Dim currButton As IDynamicButton

        For i As Integer = 0 To rowCount - 1
            currButton = New ZDynamicButton(dt.Rows(i))

            currButton.PlaceId = place
            If currButton.TypeId <> ButtonType.Rule Then
                list.Add(currButton)
            Else
                If Not currButton.NeedRights Then
                    list.Add(currButton)
                Else
                    Dim WFB As New WFBusiness
                    If WFB.CanExecuteRules(currButton.RuleId, user.ID, Nothing, Nothing) Then
                        list.Add(currButton)
                    End If
                End If
            End If

        Next

        Return list
    End Function

    ''' <summary>
    ''' Obtiene los botones de tipo regla para un lugar específico
    ''' </summary>
    ''' <param name="filter"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRuleButtons(ByVal place As ButtonPlace, _
                                          ByVal wfId As Int64, ByVal user As IUser) As List(Of IDynamicButton)
        Dim dt As DataTable
        Dim list As List(Of IDynamicButton)
        Try
            list = New List(Of IDynamicButton)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "* Buscando botones dinámicos en " & place.ToString & " para el WF=" & wfId & " y el usuario " & user.Name)
            dt = DynamicButtonFactory.GetRuleButtons(place, wfId)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "* Se han encontrado " & dt.Rows.Count & " botones dinámicos. Verificando habilitación y permisos sobre las reglas.")

            Dim rowCount As Integer = dt.Rows.Count
            Dim currButton As IDynamicButton

            Dim stepid As Int64
            Dim ruleId As Int64
            Dim ruleEnabled As Boolean = False

            For i As Integer = 0 To rowCount - 1
                currButton = New ZDynamicButton(dt.Rows(i))

                If currButton.NeedRights Then
                    Dim WFB As New WFBusiness
                    If WFB.CanExecuteRules(currButton.RuleId, user.ID, Nothing, Nothing) Then
                        list.Add(currButton)
                    End If
                Else
                    list.Add(currButton)
                End If
            Next

            Return list
        Catch ex As Exception
            ZClass.raiseerror(ex)
            If list IsNot Nothing Then
                list.Clear()
                list = Nothing
            End If
            Return Nothing
        Finally
            If dt IsNot Nothing Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Function

    ''' <summary>
    ''' Obtiene los botones de en la home, utilizando cache
    ''' </summary>
    ''' <param name="user"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetHomeButtons(ByVal user As IUser) As List(Of IDynamicButton)
        If _lsHomeButtons Is Nothing Then
            _lsHomeButtons = GetButtons(ButtonType.All, ButtonPlace.WebHome, user)
        End If

        Return _lsHomeButtons
    End Function

    ''' <summary>
    ''' Obtiene los botones del header, utilizando cache
    ''' </summary>
    ''' <param name="user"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetHeaderButtons(ByVal user As IUser) As List(Of IDynamicButton)
        'If _lsHeaderButtons Is Nothing Then
        '    _lsHeaderButtons = GetButtons(ButtonType.All, ButtonPlace.WebHeader, user)
        'End If
        'Necesita refrescarse los botones por si surge un cambio del administrador
        Return GetButtons(ButtonType.All, ButtonPlace.WebHeader, user) '_lsHeaderButtons
    End Function

    ''' <summary>
    ''' Obtiene las ubicaciones de los botones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetPlaces() As DataTable
        Return DynamicButtonFactory.GetPlaces()
    End Function

    ''' <summary>
    ''' Obtiene los tipos de botones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetTypes() As DataTable
        Return DynamicButtonFactory.GetTypes()
    End Function

End Class