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
    Public Class ZModule
        Private Shared _singleton As ZModule

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
        Public Shared Function GetInstance() As ZModule
            If Membership.MembershipHelper.isWeb Then
                If _singleton Is Nothing Then
                    _singleton = New ZModule()
                End If
                Return _singleton
            Else
                Return New ZModule()
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

        'Guarda las opciones de las etapas (WFStepOpt)
        Private _hsModules As Dictionary(Of ObjectTypes, Boolean)

        Public ReadOnly Property hsModule As Dictionary(Of ObjectTypes, Boolean)
            Get
                If _hsModules Is Nothing Then
                    _hsModules = New Dictionary(Of ObjectTypes, Boolean)
                End If

                Return _hsModules
            End Get
        End Property
    End Class
End Namespace
