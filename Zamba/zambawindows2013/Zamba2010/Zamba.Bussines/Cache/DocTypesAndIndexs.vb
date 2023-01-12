Imports System.Collections.Generic

Namespace Cache
    ''' <summary>
    ''' Clase de cache que contiene los atributos y entidades
    ''' </summary>
    ''' <history>
    '''  Marcelo    Modified    13/08/2009
    '''  Ezequiel   Modified    09/09/09
    '''  Javier     Modified    18/10/2010
    '''</history>
    ''' <remarks></remarks>
    Public Class DocTypesAndIndexs
        Public Shared Sub clearAll()
            Try
                hsIndexsDT.Clear()
                hsIndexs.Clear()
                hsIndexsArray.Clear()
                hsIndexsName.Clear()
                hsAutocompleteKeys.Clear()
                hsAutocompleteIndexs.Clear()
                hsSustIndex.Clear()
                hsDocTypes.Clear()
                hsDocTypesByName.Clear()
                hsWFDocTypes.Clear()
                hsDocTypesWF.Clear()
                hsIndexsSchemaOfDT.Clear()
                hsSpecificIndexsRights.Clear()
                hsIndexsProperties.Clear()
                hsDocAsociations.Clear()
                hsAssociatedIndexsRights.Clear()
                hsRestrictionsStrings.Clear()
                hsRestrictionsIndexs.Clear()
                hsForms.Clear()
                hsDocAssociatedCount.Clear()
                dicDocTypeExistance.Clear()
                dicDocTypeByRight.Clear()

                arIndexs.clear()
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        'Hash que guarda los valores de los atributos de substitucion
        'todo unificar en un solo hash
        '****************************************************************
        Public Shared hsIndexsDT As New Hashtable
        Public Shared hsIndexsArray As New Hashtable
        '****************************************************************
        'Hash que guarda los nombres de los atributos
        Public Shared hsIndexsName As New Hashtable
        'Hash que guarda los atributos por id de atributo
        Public Shared hsIndexs As New Hashtable
        'Hash que guarda las keys del autocomplete
        Public Shared hsAutocompleteKeys As New Hashtable
        Public Shared hsAutocompleteIndexs As New Hashtable
        'Hash que guarda las descripciones de los atributos de sustitucion ya utilizado
        Public Shared hsSustIndex As New Hashtable
        'Hash que guarda los entidades
        Public Shared hsDocTypes As New Hashtable
        'Hash que guarda los entidades por nombre
        Public Shared hsDocTypesByName As New Hashtable
        'Hash que guarda los wf de los entidades
        Public Shared hsWFDocTypes As New Hashtable
        'Hash que guarda los los entidades de un wf
        Public Shared hsDocTypesWF As New Hashtable
        'Hash que guarda el esquema de atributos de un entidad
        Public Shared hsIndexsSchemaOfDT As New Hashtable
        'Hash que guarda los permisos por atributos especificos
        Public Shared hsSpecificIndexsRights As New Hashtable
        'Hash que guarda las propiedades de atributos asociados de la tabla index_r_doc_type 
        Public Shared hsIndexsProperties As New Hashtable
        'Hash que guarda las asociaciones entre 2 entidades.
        Public Shared hsDocAsociations As New Hashtable
        'Hash que guarda las asociaciones entre 2 entidades y sus permisos.
        Public Shared hsAssociatedIndexsRights As New Hashtable
        'Hash que guarda las restricciones del usuario para un doctype
        Public Shared hsRestrictionsStrings As New Hashtable
        Public Shared hsRestrictionsIndexs As New Hashtable
        'Hash que guarda todos los forms por ID
        Public Shared hsForms As New Hashtable
        'Hash que guarda los forms de los doctypes
        Public Shared hsFormsByEntityId As New Hashtable
        'Lista que guarda todos los forms
        Public Shared hsAllForms As new  List(Of zwebform)

        Public Shared hsDocAssociatedCount As New Hashtable
        'Guarda la verificacion de la existencia de una entidad. Se utiliza en Threadpool.
        Public Shared dicDocTypeExistance As New Dictionary(Of Long, Boolean)()
        'Guarda las entidades que el usuario tiene permisos de algun tipo
        Public Shared dicDocTypeByRight As New Dictionary(Of Zamba.Core.RightsType, ArrayList)()

        'Hash que guarda los atributos por id de atributo
        Public Shared arIndexs As New Hashtable
    End Class
End Namespace