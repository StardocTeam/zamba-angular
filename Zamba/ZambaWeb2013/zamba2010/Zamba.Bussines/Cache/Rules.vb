Imports System.Collections.Generic

Namespace Cache
    '[Ezequiel] 02/05/2011 - Clase la cual guarda las reglas
    Public Class Rules
        Private Shared _hsSingletonZCoreInstances As New SynchronizedHashtable

        Public Shared lsStepDSRules As New SynchronizedHashtable

        Public Shared lsRulesDSRules As New SynchronizedHashtable

        ''' <summary>
        ''' El constructor es privado
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
        End Sub

        ''' <summary>
        ''' Obtiene la instancia actual de ZCore
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetInstance() As Rules
            If Membership.MembershipHelper.isWeb Then
                Dim zCoreKey As String = Membership.MembershipHelper.CurrentUser.ID
                If Not _hsSingletonZCoreInstances.ContainsKey(zCoreKey) Then
                    _hsSingletonZCoreInstances.Add(zCoreKey, New Rules())
                End If
                Return _hsSingletonZCoreInstances.Item(zCoreKey)
            Else
                Return New Rules()
            End If

        End Function

        ''' <summary>
        ''' Remueve la instancia actual de ZCore
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RemoveCurrentInstance() As ZCore
            If Membership.MembershipHelper.isWeb AndAlso Membership.MembershipHelper.CurrentUser IsNot Nothing Then
                Dim zCoreKey As String = Membership.MembershipHelper.CurrentUser.ID
                If _hsSingletonZCoreInstances.ContainsKey(zCoreKey) Then
                    _hsSingletonZCoreInstances.Item(zCoreKey).ClearAll()
                    _hsSingletonZCoreInstances.Remove(zCoreKey)
                End If
            End If
        End Function









        Public Shared Sub ClearAll()
            lsStepDSRules.Clear()
            lsRulesDSRules.Clear()

        End Sub
    End Class
End Namespace