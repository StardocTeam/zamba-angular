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
    Public Class DocTypesWF
        Private Shared _singleton As DocTypesWF

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
        Public Shared Function GetInstance() As DocTypesWF
            If Membership.MembershipHelper.isWeb Then
                If _singleton Is Nothing Then
                    _singleton = New DocTypesWF()
                End If
                Return _singleton
            Else
                Return New DocTypesWF()
            End If
        End Function

        ''' <summary>
        ''' Remueve la instancia actual de ZCore
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RemoveCurrentInstance() As ZCore
            If Membership.MembershipHelper.isWeb Then
                _singleton = Nothing
            End If
        End Function

        Private _hsDocTypesWF As Dictionary(Of String, DataTable)

        Public ReadOnly Property hsDocTypesWF As Dictionary(Of String, DataTable)
            Get
                If _hsDocTypesWF Is Nothing Then
                    _hsDocTypesWF = New Dictionary(Of String, DataTable)
                End If

                Return _hsDocTypesWF
            End Get
        End Property
    End Class
End Namespace
