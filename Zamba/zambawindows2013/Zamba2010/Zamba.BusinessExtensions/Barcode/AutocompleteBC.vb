<Serializable()> Public Class AutocompleteBC
    Inherits AutocompleteBCBusiness

    Public Sub New(ByVal Dtid As Int32)
        MyBase.New(Dtid)
    End Sub


    Public Overrides Sub Dispose()

    End Sub
End Class

Public Class AutoCompleteBarcode_Factory
    Private Shared AC As AutocompleteBC
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un Objeto AutoCompleteBC si hay configurado autocompletar en base al DocTypeID y al Atributo seleccionado
    ''' </summary>
    ''' <param name="DocTypeID"></param>
    ''' <param name="IndexId"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Se debe evaluar si el resultado de esto es NOTHING
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetComplete(ByVal docTypeId As Int64, ByVal IndexId As Int32) As AutocompleteBC
        If IsNothing(BarcodesBusiness.GetAutoIndexs(DocTypeID, IndexId)) Then
            Return Nothing
        Else
            AC = New AutocompleteBC(DocTypeID)
            Return AC
        End If
    End Function

End Class
