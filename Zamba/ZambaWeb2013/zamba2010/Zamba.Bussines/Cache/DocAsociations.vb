Imports System.Collections.Generic

Namespace Cache

    ''' <summary>
    ''' Hash que guarda las asociaciones entre 2 tipos de documentos.
    ''' </summary>
    ''' <history>
    '''             [Ezequiel] 10/09/2009
    '''             Marcelo    29/04/2013 
    ''' </history>
    ''' <remarks></remarks>
    Public Class DocAsociations
        Private Shared _singleton As DocAsociations

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
        Public Shared Function GetInstance() As DocAsociations
            If Membership.MembershipHelper.isWeb Then
                If _singleton Is Nothing Then
                    _singleton = New DocAsociations()
                End If
                Return _singleton
            Else
                Return New DocAsociations()
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

        'Hash que guarda las asociaciones entre 2 tipos de documentos.
        Private _hsDocAsociations As Dictionary(Of String, Generic.List(Of Asociados))

        Public ReadOnly Property hsDocAsociations As Dictionary(Of String, Generic.List(Of Asociados))
            Get
                If _hsDocAsociations Is Nothing Then
                    _hsDocAsociations = New Dictionary(Of String, Generic.List(Of Asociados))
                End If

                Return _hsDocAsociations
            End Get
        End Property

        'Hash que guarda las asociaciones entre 2 tipos de documentos.
        Private _hsDocAsociationsIds As Dictionary(Of Int64, Generic.List(Of Int64))

        Public ReadOnly Property hsDocAsociationsIds As Dictionary(Of Int64, Generic.List(Of Int64))
            Get
                If _hsDocAsociationsIds Is Nothing Then
                    _hsDocAsociationsIds = New Dictionary(Of Int64, Generic.List(Of Int64))
                End If
                Return _hsDocAsociationsIds
            End Get
        End Property

    End Class
End Namespace
