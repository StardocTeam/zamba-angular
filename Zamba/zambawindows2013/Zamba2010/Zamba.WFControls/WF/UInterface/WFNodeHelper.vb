Imports Zamba.Core.Enumerators
Imports Zamba.Core

Public Class WFNodeHelper
    Implements IWFNodeHelper

    Private cbd As IntPtr
    Private Property CD As IntPtr

    Public Function GetnodeImage(NodeType As NodeWFTypes, Optional RuleNodeType As TypesofRules = TypesofRules.Regla) As Image Implements IWFNodeHelper.GetnodeImage
        Dim currentImage As Image
        Select Case NodeType
            Case NodeWFTypes.Busqueda
                currentImage = Global.Zamba.Controls.My.Resources.Resources.appbar_list_check.GetThumbnailImage(32, 32, CB, CD)
            Case NodeWFTypes.Comienzo
                currentImage = Global.Zamba.Controls.My.Resources.Resources.appbar_edit_add.GetThumbnailImage(32, 32, CB, CD)
            Case NodeWFTypes.Etapa
                currentImage = Global.Zamba.Controls.My.Resources.Resources.appbar_list_add.GetThumbnailImage(32, 32, CB, CD)
            Case NodeWFTypes.FloatingRule
                currentImage = Global.Zamba.Controls.My.Resources.Resources.appbar_edit_add.GetThumbnailImage(32, 32, CB, CD)
            Case NodeWFTypes.Inicio
                currentImage = Global.Zamba.Controls.My.Resources.Resources.appbar_axis_x.GetThumbnailImage(32, 32, CB, CD)
            Case NodeWFTypes.insercion
                currentImage = Global.Zamba.Controls.My.Resources.Resources.appbar_edit_add.GetThumbnailImage(32, 32, CB, CD)
            Case NodeWFTypes.nodoBusqueda
                currentImage = Global.Zamba.Controls.My.Resources.Resources.appbar_page_search.GetThumbnailImage(32, 32, CB, CD)
            Case NodeWFTypes.nodoInsercion
                currentImage = Global.Zamba.Controls.My.Resources.Resources.appbar_add_multiple.GetThumbnailImage(32, 32, CB, CD)
            Case NodeWFTypes.Permiso
                currentImage = Global.Zamba.Controls.My.Resources.Resources.appbar_key.GetThumbnailImage(32, 32, CB, CD)
            Case NodeWFTypes.Regla
                currentImage = Global.Zamba.Controls.My.Resources.Resources.appbar_edit_add.GetThumbnailImage(32, 32, CB, CD)
            Case NodeWFTypes.Tarea
                currentImage = Global.Zamba.Controls.My.Resources.Resources.appbar_edit_add.GetThumbnailImage(32, 32, CB, CD)
            Case NodeWFTypes.TipoDeRegla
                Select Case RuleNodeType
                    Case TypesofRules.AbrirDocumento
                        currentImage = Global.Zamba.Controls.My.Resources.Resources.appbar_book_hardcover_open_writing.GetThumbnailImage(32, 32, CB, CD)
                    Case TypesofRules.AbrirZamba
                        currentImage = Global.Zamba.Controls.My.Resources.Resources.appbar_axis_z.GetThumbnailImage(32, 32, CB, CD)
                    Case TypesofRules.AccionUsuario
                        currentImage = Global.Zamba.Controls.My.Resources.Resources.user_tick_profile_login_approve_verified_512.GetThumbnailImage(32, 32, CB, CD)
                    Case TypesofRules.Actualizacion
                        currentImage = Global.Zamba.Controls.My.Resources.Resources.appbar_refresh.GetThumbnailImage(32, 32, CB, CD)
                    Case TypesofRules.Asignar
                        currentImage = Global.Zamba.Controls.My.Resources.Resources.appbar_people_right.GetThumbnailImage(32, 32, CB, CD)
                    Case TypesofRules.Condicion
                        currentImage = Global.Zamba.Controls.My.Resources.Resources.appbar_checkmark_pencil_top.GetThumbnailImage(32, 32, CB, CD)
                    Case TypesofRules.Derivar
                        currentImage = Global.Zamba.Controls.My.Resources.Resources.appbar_people_arrow_right.GetThumbnailImage(32, 32, CB, CD)
                    Case TypesofRules.Entrada
                        currentImage = Global.Zamba.Controls.My.Resources.Resources.appbar_arrow_down.GetThumbnailImage(32, 32, CB, CD)
                    Case TypesofRules.Estado
                        currentImage = Global.Zamba.Controls.My.Resources.Resources.appbar_list_add_below.GetThumbnailImage(32, 32, CB, CD)
                    Case TypesofRules.Eventos
                        currentImage = Global.Zamba.Controls.My.Resources.Resources.appbar_alert.GetThumbnailImage(32, 32, CB, CD)
                    Case TypesofRules.Floating
                        currentImage = Global.Zamba.Controls.My.Resources.Resources.appbar_list_gear.GetThumbnailImage(32, 32, CB, CD)
                    Case TypesofRules.GuardarDocumento
                        currentImage = Global.Zamba.Controls.My.Resources.Resources.appbar_save.GetThumbnailImage(32, 32, CB, CD)
                    Case TypesofRules.Indices
                        currentImage = Global.Zamba.Controls.My.Resources.Resources.appbar_edit_box.GetThumbnailImage(32, 32, CB, CD)
                    Case TypesofRules.Iniciar
                        currentImage = Global.Zamba.Controls.My.Resources.Resources.appbar_arrow_right.GetThumbnailImage(32, 32, CB, CD)
                    Case TypesofRules.Insertar
                        currentImage = Global.Zamba.Controls.My.Resources.Resources.appbar_add.GetThumbnailImage(32, 32, CB, CD)
                    Case TypesofRules.Planificada
                        currentImage = Global.Zamba.Controls.My.Resources.Resources.appbar_timer.GetThumbnailImage(32, 32, CB, CD)
                    Case TypesofRules.Regla
                        currentImage = Global.Zamba.Controls.My.Resources.Resources.appbar_list_gear.GetThumbnailImage(32, 32, CB, CD)
                    Case TypesofRules.Salida
                        currentImage = Global.Zamba.Controls.My.Resources.Resources.appbar_arrow_up.GetThumbnailImage(32, 32, CB, CD)
                    Case TypesofRules.TareaDerivada
                        currentImage = Global.Zamba.Controls.My.Resources.Resources.appbar_people_checkbox.GetThumbnailImage(32, 32, CB, CD)
                    Case TypesofRules.TareaFinalizada
                        currentImage = Global.Zamba.Controls.My.Resources.Resources.appbar_checkmark_pencil.GetThumbnailImage(32, 32, CB, CD)
                    Case TypesofRules.TareaIniciada
                        currentImage = Global.Zamba.Controls.My.Resources.Resources.appbar_checkmark_pencil_top.GetThumbnailImage(32, 32, CB, CD)
                    Case TypesofRules.TareaRechazada
                        currentImage = Global.Zamba.Controls.My.Resources.Resources.appbar_thumbs_down.GetThumbnailImage(32, 32, CB, CD)
                    Case TypesofRules.Terminar
                        currentImage = Global.Zamba.Controls.My.Resources.Resources.appbar_checkmark_pencil.GetThumbnailImage(32, 32, CB, CD)
                    Case TypesofRules.CerrarTarea
                        currentImage = Global.Zamba.Controls.My.Resources.Resources.appbar_checkmark_pencil.GetThumbnailImage(32, 32, CB, CD)
                    Case TypesofRules.ValidacionEntrada
                        currentImage = Global.Zamba.Controls.My.Resources.Resources.appbar_list_check.GetThumbnailImage(32, 32, CB, CD)
                    Case TypesofRules.ValidacionSalida
                        currentImage = Global.Zamba.Controls.My.Resources.Resources.appbar_list_check.GetThumbnailImage(32, 32, CB, CD)
                End Select
            Case NodeWFTypes.WorkFlow
                currentImage = Global.Zamba.Controls.My.Resources.Resources.appbar_list_add_above.GetThumbnailImage(32, 32, CB, CD)
            Case NodeWFTypes.Estado
                currentImage = Global.Zamba.Controls.My.Resources.Resources.appbar_list_add_below.GetThumbnailImage(32, 32, CB, CD)
        End Select

        Return currentImage
    End Function


    Private Function CB() As Image.GetThumbnailImageAbort
    End Function

End Class
