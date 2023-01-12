Namespace Cache

    Public Class WebServices
        Public Shared hsWebServiceTypes As Hashtable = New Hashtable
        Public Shared hsWebServiceInstances As Hashtable = New Hashtable
        Public Shared hsWebMethods As Hashtable = New Hashtable
        Public Shared hsWebMethodsParameters As Hashtable = New Hashtable

        ''' <summary>
        ''' Limpia la cache de los webservices
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub ClearAll()
            hsWebServiceTypes.Clear()
            hsWebServiceInstances.Clear()
            hsWebMethods.Clear()
            hsWebMethodsParameters.Clear()
        End Sub
    End Class

End Namespace