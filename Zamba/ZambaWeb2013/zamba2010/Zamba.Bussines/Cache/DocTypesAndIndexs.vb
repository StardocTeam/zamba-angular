Imports Zamba.Membership
Imports System.Collections.Generic

Namespace Cache
    ''' <summary>
    ''' Clase de cache que contiene los indices y tipos de documentos
    ''' </summary>
    ''' <history>
    '''  Marcelo    Modified    13/08/2009
    '''  Ezequiel   Modified    09/09/09
    '''  Javier     Modified    18/10/2010
    '''</history>
    ''' <remarks></remarks>
    Public Class DocTypesAndIndexs
        'Hash que guarda los valores de los indices de substitucion
        'todo unificar en un solo hash
        '****************************************************************
        Public Shared hsIndexsDT As New SynchronizedHashtable
        Public Shared hsIndexsArray As New SynchronizedHashtable
        '****************************************************************
        'Hash que guarda los nombres de los indices
        Public Shared hsIndexsName As New SynchronizedHashtable

        Friend Shared Sub RemoveCurrentInstance()
            hsIndexsDT.Clear()
            hsIndexsArray.Clear()
            hsIndexsName.Clear()
            hsAutocompleteKeys.Clear()
            hsAutocompleteIndexs.Clear()
            hsSustIndex.Clear()
            hsDocTypesByName.Clear()
            hsWFDocTypes.Clear()
            hsIndexsProperties.Clear()
            hsRestrictionsIndexs.Clear()
            hsForms.Clear()
            hsDocAssociatedCount.Clear()
            hsDocTypeIndexsDS.Clear()
            hsHierarchicalTableByParentValue.Clear()
            hsDocAsociations.Clear()
            hsDocTypeAsociations.Clear()
            hsRestrictionsIndexsByUserId.Clear()
            hsAssociatedIndexsRights.Clear()
            hsIndexs.Clear()
            hsIndexsChilds.Clear()
            hsSearchEntities.Clear()
            hsFiltersEntities.Clear()
            hsSearchIndexs.Clear()
            hsReferenceIndexsByDoctype.Clear()
        End Sub
        'Hash que guarda las keys del autocomplete
        Public Shared hsIndexs As New SynchronizedHashtable
        Public Shared hsIndexsChilds As New SynchronizedHashtable
        Public Shared hsSearchEntities As New SynchronizedHashtable
        Public Shared hsFiltersEntities As New SynchronizedHashtable
        Public Shared hsSearchIndexs As New SynchronizedHashtable

        'Hash que guarda las keys del autocomplete
        Public Shared hsAutocompleteKeys As New SynchronizedHashtable
        Public Shared hsAutocompleteIndexs As New SynchronizedHashtable
        'Hash que guarda las descripciones de los indices de sustitucion ya utilizado
        Public Shared hsSustIndex As New SynchronizedHashtable

        'Hash que guarda los tipos de documentos por nombre
        Public Shared hsDocTypesByName As New SynchronizedHashtable
        'Hash que guarda los wf de los tipos de documentos
        Public Shared hsWFDocTypes As New SynchronizedHashtable

        'Hash que guarda las propiedades de atributos asociados de la tabla index_r_doc_type 
        Public Shared hsIndexsProperties As New SynchronizedHashtable

        Public Shared hsRestrictionsIndexs As New SynchronizedHashtable
        'Hash que guarda los forms de los doctypes
        Public Shared hsForms As New SynchronizedHashtable

        Public Shared hsDocAssociatedCount As New SynchronizedHashtable

        'Hash para guardar por doctypeid los indices asociadso(en dataset)
        Public Shared hsDocTypeIndexsDS As New SynchronizedHashtable

        'Hash para guardar las tablas de opciones por valor jerarquico
        Public Shared hsHierarchicalTableByParentValue As New SynchronizedHashtable

        'Hash para guardar los indices, con su tabla de jerarquia de indices hijos
        '  Public Shared hsIndexWithChildsById As New SynchronizedHashtable
        'NO SE PUEDE GUARDAR LOS INDICES EN CACHE PORQUE COMPARTIRIAN EL DATA QUE ES DIFERENTE PARA CADA DOCUMENTO

        'Hash que guarda las asociaciones entre 2 tipos de documentos.
        Public Shared hsDocAsociations As New SynchronizedHashtable
        'Hash que guarda las asociaciones entre 2 tipos de documentos, solo las entidades sin atributos.
        Public Shared hsDocTypeAsociations As New SynchronizedHashtable

        'Hash para guardar las restriccionesdeindicesporusuario
        Public Shared hsRestrictionsIndexsByUserId As New SynchronizedHashtable

        Public Shared hsAssociatedIndexsRights As New SynchronizedHashtable

        'Almacena los indices referecniales para cada entidad.
        Public Shared hsReferenceIndexsByDoctype As New SynchronizedHashtable
    End Class
End Namespace