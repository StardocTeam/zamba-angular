Imports System.Collections.Generic

Namespace Cache
    Public Class StepsOpt

        Private Const KEY As string = "StepsOpt"

        Private Shared _singleton As StepsOpt

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
        Public Shared Function GetInstance() As StepsOpt
            If Membership.MembershipHelper.isWeb Then
                If _singleton Is Nothing Then
                    _singleton = New StepsOpt()
                End If
                Return _singleton
            Else
                Return New StepsOpt()
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
        Private _hsStepsOpt As Dictionary(Of String, DataTable)

        Public ReadOnly Property hsStepsOpt As Dictionary(Of String, DataTable)
            Get
                If _hsStepsOpt Is Nothing Then
                    _hsStepsOpt = New Dictionary(Of String, DataTable)
                End If

                Return _hsStepsOpt
            End Get
        End Property
    End Class
End Namespace