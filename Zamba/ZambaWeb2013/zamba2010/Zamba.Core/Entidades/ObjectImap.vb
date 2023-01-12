Imports Zamba.Data
Imports Zamba.Core
Imports Zamba.FileTools

Public Class DTOObjectImap
    Inherits ZClass
    Implements IDTOObjectImap

    Public Property Id_proceso As Long Implements IDTOObjectImap.Id_proceso

    Public Property Is_Active As Long Implements IDTOObjectImap.Is_Active

    Public Property Nombre_usuario As String Implements IDTOObjectImap.Nombre_usuario

    Public Property Nombre_proceso As String Implements IDTOObjectImap.Nombre_proceso

    Public Property Correo_electronico As String Implements IDTOObjectImap.Correo_electronico

    Public Property Id_usuario As Long Implements IDTOObjectImap.Id_usuario

    Public Property Password As String Implements IDTOObjectImap.Password
    Public Property Direccion_servidor As String Implements IDTOObjectImap.Direccion_servidor

    Public Property Puerto As Long Implements IDTOObjectImap.Puerto

    Public Property Protocolo As String Implements IDTOObjectImap.Protocolo
    Public Property Filtrado As Long Implements IDTOObjectImap.Filtrado

    Public Property Filtro_campo As String Implements IDTOObjectImap.Filtro_campo

    Public Property Filtro_valor As String Implements IDTOObjectImap.Filtro_valor

    Public Property Filtro_recientes As Long Implements IDTOObjectImap.Filtro_recientes

    Public Property Filtro_noleidos As Long Implements IDTOObjectImap.Filtro_noleidos

    Public Property Exportar_adjunto_por_separado As Long Implements IDTOObjectImap.Exportar_adjunto_por_separado

    Public Property Carpeta As String Implements IDTOObjectImap.Carpeta

    Public Property CarpetaDest As String Implements IDTOObjectImap.CarpetaDest

    Public Property Entidad As Long Implements IDTOObjectImap.Entidad

    Public Property Enviado_por As Long Implements IDTOObjectImap.Enviado_por
    Public Property Para As Long Implements IDTOObjectImap.Para

    Public Property Cc As Long Implements IDTOObjectImap.Cc

    Public Property Cco As Long Implements IDTOObjectImap.Cco

    Public Property Asunto As Long Implements IDTOObjectImap.Asunto

    Public Property Body As Long Implements IDTOObjectImap.Body
    Public Property Fecha As Long Implements IDTOObjectImap.Fecha

    Public Property Usuario_zamba As Long Implements IDTOObjectImap.Usuario_zamba

    Public Property Codigo_mail As Long Implements IDTOObjectImap.Codigo_mail

    Public Property Tipo_exportacion As Long Implements IDTOObjectImap.Tipo_exportacion

    Public Property Autoincremento As Long Implements IDTOObjectImap.Autoincremento

    Public Property GenericInbox As Long Implements IDTOObjectImap.GenericInbox

    Public Overrides Sub Dispose() Implements IDTOObjectImap.Dispose
    End Sub

    Public Sub New()

    End Sub

    Public Sub New(Obj As DataRow)
        Id_proceso = Convert.ToInt32(Obj("PROCESS_ID"))
        Is_Active = Convert.ToInt32(Obj("IS_ACTIVE"))
        Nombre_usuario = Obj("USER_NAME")
        Nombre_proceso = Obj("PROCESS_NAME")
        Correo_electronico = Obj("EMAIL")
        Id_usuario = Convert.ToInt32(Obj("USER_ID"))
        Password = (Obj("USER_PASSWORD"))
        Direccion_servidor = Obj("IP_ADDRESS")
        Puerto = Convert.ToInt32(Obj("FIELD_PORT"))
        Protocolo = Obj("FIELD_PROTOCOL")
        Filtrado = Convert.ToInt32(Obj("HAS_FILTERS"))

        If IsDBNull(Obj("FILTER_FIELD")) Then
            Filtro_campo = ""
        Else
            Filtro_campo = Obj("FILTER_FIELD")
        End If

        If IsDBNull(Obj("FILTER_VALUE")) Then
            Filtro_valor = ""
        Else
            Filtro_valor = Obj("FILTER_VALUE")
        End If

        Filtro_recientes = Convert.ToInt32(Obj("FILTER_RECENTS"))
        Filtro_noleidos = Convert.ToInt32(Obj("FILTER_NOT_READS"))
        Exportar_adjunto_por_separado = Convert.ToInt32(Obj("EXPORT_ATTACHMENTS_SEPARATELY"))
        Carpeta = Obj("FOLDER_NAME")
        CarpetaDest = Obj("FOLDER_NAME_DEST")
        Entidad = Convert.ToInt32(Obj("ENTITY_ID"))
        Enviado_por = Convert.ToInt32(Obj("SENT_BY"))
        Para = Convert.ToInt32(Obj("FIELD_TO"))
        Cc = Convert.ToInt32(Obj("CC"))
        Cco = Convert.ToInt32(Obj("CCO"))
        Asunto = Convert.ToInt32(Obj("SUBJECT"))
        Body = Convert.ToInt32(Obj("FIELD_BODY"))
        Fecha = Convert.ToInt32(Obj("FIELD_DATE"))
        Usuario_zamba = Convert.ToInt32(Obj("Z_USER"))
        Codigo_mail = Convert.ToInt32(Obj("CODE_MAIL"))
        Tipo_exportacion = Convert.ToInt32(Obj("TYPE_OF_EXPORT"))
        Autoincremento = Convert.ToInt32(Obj("AUT_INCREMENT"))

        If IsDBNull(Obj("GENERIC_INBOX")) Then
            GenericInbox = 0
        Else
            GenericInbox = Convert.ToInt32(Obj("GENERIC_INBOX"))
        End If
    End Sub

End Class