Imports System.Collections.Generic

Namespace Cache

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <history>
    '''             [Ezequiel] 10/09/2009
    '''             Marcelo    29/04/2013 
    ''' </history>
    ''' <remarks></remarks>
    Public Class RestrictionsStrings
        Private Shared _hsSingletonZCoreInstances As New Dictionary(Of String, RestrictionsStrings)

        ''' <summary>
        ''' El constructor es privado
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub New()

        End Sub

        ''' <summary>
        ''' Obtiene la instancia actual de ZCore
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetInstance() As RestrictionsStrings
            If Membership.MembershipHelper.isWeb Then
                Dim zCoreKey As String = Membership.MembershipHelper.CurrentUser.ID
                If Not _hsSingletonZCoreInstances.ContainsKey(zCoreKey) Then
                    _hsSingletonZCoreInstances.Add(zCoreKey, New RestrictionsStrings())
                End If
                Return _hsSingletonZCoreInstances.Item(zCoreKey)
            Else
                Return New RestrictionsStrings()
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
                    _hsSingletonZCoreInstances.Remove(zCoreKey)
                End If
            End If
        End Function

        Private _hsRestrictionsStrings As Dictionary(Of String, Generic.List(Of IIndex))

        Public ReadOnly Property hsRestrictionsStrings As Dictionary(Of String, Generic.List(Of IIndex))
            Get
                If _hsRestrictionsStrings Is Nothing Then
                    _hsRestrictionsStrings = New Dictionary(Of String, Generic.List(Of IIndex))
                End If

                Return _hsRestrictionsStrings
            End Get
        End Property
    End Class
End Namespace
