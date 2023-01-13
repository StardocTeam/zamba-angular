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
    Public Class DocTypes
        Public Shared hsBaseDocTypesWithoutIndexs As SynchronizedHashtable = New SynchronizedHashtable
        Public Shared DocTypesByUserIdAndRightType As SynchronizedHashtable = New SynchronizedHashtable

        Private Shared _dicSingletonZCoreInstances As New Dictionary(Of String, DocTypes)
        Private _dicDocTypes As Dictionary(Of Long, DocType)


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
        Public Shared Function GetInstance() As DocTypes
            If Membership.MembershipHelper.isWeb Then
                Dim zCoreKey As String = Membership.MembershipHelper.CurrentUser.ID
                If Not _dicSingletonZCoreInstances.ContainsKey(zCoreKey) Then
                    SyncLock _dicSingletonZCoreInstances
                        If Not _dicSingletonZCoreInstances.ContainsKey(zCoreKey) Then
                            _dicSingletonZCoreInstances.Add(zCoreKey, New DocTypes())
                        End If
                    End SyncLock
                End If
                Return _dicSingletonZCoreInstances.Item(zCoreKey)
            Else
                Return New DocTypes()
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
                If _dicSingletonZCoreInstances.ContainsKey(zCoreKey) Then
                    _dicSingletonZCoreInstances.Remove(zCoreKey)
                End If
            End If

            hsBaseDocTypesWithoutIndexs.Clear()
            DocTypesByUserIdAndRightType.Clear()
            _dicSingletonZCoreInstances.Clear()

        End Function

        ''' <summary>
        ''' Cache de entidades de Zamba
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property hsDocTypes As Dictionary(Of Long, DocType)
            Get
                If _dicDocTypes Is Nothing Then
                    _dicDocTypes = New Dictionary(Of Long, DocType)
                End If

                Return _dicDocTypes
            End Get
        End Property

    End Class
End Namespace
