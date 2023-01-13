'Imports Zamba.Foro.Factory
'Imports zamba.foro.Core
Imports Zamba.Core
''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Foro
''' Class	 : Foro.ZForo_Factory
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Factory para trabajar con elementos del foro
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class ForoBussines
    Inherits Zamba.Core.ZClass
    Public Overrides Sub Dispose()

    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Elimina un mensaje
    ''' </summary>
    ''' <param name="DocT">Id del Tipo de Documento</param>
    ''' <param name="DocId">ID del documento especifico</param>
    ''' <param name="ParentId">Id que representa el comentario original</param>
    ''' <param name="IdMensaje">Id del Mensaje que se desea eliminar</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub DeleteMessage(ByVal DocId As Int64, ByVal ParentId As Int32, ByVal IdMensaje As Int32)
        ZForoBusiness.DeleteMessage(DocId, ParentId, IdMensaje)
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Agrega un mensaje al foro
    ''' </summary>
    ''' <param name="DocT">Id del Tipo de Documento</param>
    ''' <param name="DocId">ID del documento especifico</param>
    ''' <param name="IdMensaje">Id del mensaje</param>
    ''' <param name="ParentId">Id que representa el comentario original, si no es respuesta a un comentario, entonces es 0</param>
    ''' <param name="LinkName">... ,maximo 60 caracteres</param>
    ''' <param name="Mensaje">Mensaje que se desea persistir, maximo 300 caracteres</param>
    ''' <param name="Fecha">Fecha de creacion</param>
    ''' <param name="UserId"></param>
    ''' <param name="StateId"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub InsertMessage(ByVal DocId As Int64, ByVal Doctypeid As Int64, ByVal IdMensaje As Int32, ByVal ParentId As Int32, ByVal LinkName As String, ByVal Mensaje As String, ByVal Fecha As Date, ByVal UserId As Int32, ByVal StateId As Integer)
        ZForoBusiness.InsertMessage(DocId, Doctypeid, IdMensaje, ParentId, LinkName, Mensaje, Fecha, UserId, StateId)
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un nuevo ID para un mensaje
    ''' </summary>
    ''' <param name="DocT">Id del Tipo de Documento</param>
    ''' <param name="DocId">ID del documento especifico</param>
    ''' <returns>Numero entero que representa el nuevo ID</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function SiguienteId(ByVal DocId As Int64) As Integer
        Return ZForoBusiness.SiguienteId(DocId)
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un nuevo ParentID
    ''' </summary>
    ''' <param name="DocT">Id del Tipo de Documento</param>
    ''' <param name="DocId">ID del documento especifico</param>
    ''' <param name="IdMensaje">Id del mensaje</param>
    ''' <returns>ID que representa el Nuevo Parent ID</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function SiguienteParent(ByVal DocId As Int64, ByVal IdMensaje As Int32) As Int32
        Return ZForoBusiness.SiguienteParent(DocId, IdMensaje)
    End Function
End Class
