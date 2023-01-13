Public Interface IAdvanceDocTypeNode

    ''' <summary>
    ''' ID del tipo de documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property docTypeID() As Int64

    ''' <summary>
    ''' IndexKey del nodo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IndexID() As String

    ''' <summary>
    ''' ID del indice relacion del padre
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RelationIndexID2() As Int64

    ''' <summary>
    ''' ID del indice relacion del hijo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RelationIndexID1() As Int64

    ''' <summary>
    ''' Nodo padre
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property ParentNode() As IAdvanceDocTypeNode
End Interface
