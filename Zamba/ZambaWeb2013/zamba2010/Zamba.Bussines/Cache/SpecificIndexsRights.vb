Imports System.Collections.Generic

'Hash que guarda los permisos por indices especificos
Namespace Cache

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <history>
    '''             [Ezequiel] 10/09/2009
    '''             Marcelo    29/04/2013 
    ''' </history>
    ''' <remarks></remarks>
    Public Class SpecificIndexsRights

        ''' <summary>
        ''' El constructor es privado
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub New()

        End Sub


        ''' <summary>
        ''' Remueve la instancia actual de ZCore
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RemoveCurrentInstance() As ZCore
            hsSpecificIndexsRights.Clear()
        End Function

        Public Shared hsSpecificIndexsRights As New SynchronizedHashtable

    End Class
End Namespace

