Imports Zamba.Core
''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Templates
''' Class	 : Templates.FactoryTemplates
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Fabrica de Templates
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	26/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class FactoryTemplates


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un nuevo ID para guardar el Template.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetNewTemplateId() As Int32
        Return CoreBusiness.GetNewID(IdTypes.TEMPLATE)
    End Function
End Class
