Imports Zamba.Data

Public Class Autoname_Business

    ''' <summary>
    ''' Metodo utilizado por una aplicacion externa para realizar el proceso de autonombre
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[AlejandroR] - 02/03/2010 -	Created - (WI 4419)
    ''' </history>
    Public Shared Sub ExecuteUpdate(ByVal docTypeId As Int64, ByVal DocTypeName As String, ByVal AutoName As String)

        AutonameFactory.ExecuteUpdate(DocTypeId, DocTypeName, AutoName)

    End Sub

End Class
