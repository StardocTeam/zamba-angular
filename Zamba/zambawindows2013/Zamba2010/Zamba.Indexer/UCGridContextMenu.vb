Imports System.Collections.Generic
Imports Telerik.WinControls.UI
Imports Zamba.Core

Public Class UCGridContextMenu

    Public WithEvents contextMenu As RadContextMenu
    Dim ItemEditTif As RadMenuItem
    Dim ItemShowResultProperties As RadMenuItem
    Dim ItemPrintIndexs As RadMenuItem
    Dim ItemAddToWF As RadMenuItem
    Dim ItemExportToPDF As RadMenuItem
    Dim ItemDeleteResult As RadMenuItem

    Dim ItemShowHistory As RadMenuItem
    Dim ItemGenerateWindowsLink As RadMenuItem
    Dim ItemGenerateWebLink As RadMenuItem
    Public Property ContextMenuContainer As IMenuContextContainer
    ' Public Event ItemClicked(Action As String, listResults As List(Of IResult), ContextMenuContainer As IMenuContextContainer)

    Public Sub New(ByRef ContextMenuContainer As IMenuContextContainer)
        MyBase.New
        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        Me.ContextMenuContainer = ContextMenuContainer
        contextMenu = New RadContextMenu

        ItemEditTif = New RadMenuItem
        ItemShowResultProperties = New RadMenuItem
        ItemPrintIndexs = New RadMenuItem
        ItemAddToWF = New RadMenuItem
        ItemExportToPDF = New RadMenuItem
        ItemDeleteResult = New RadMenuItem

        ItemShowHistory = New RadMenuItem
        ItemGenerateWindowsLink = New RadMenuItem
        ItemGenerateWebLink = New RadMenuItem
        '
        'AgregarAWorkFlowToolStripMenuItem
        '
        ItemAddToWF.Name = "AgregarAWorkFlowToolStripMenuItem"
        ItemAddToWF.Size = New System.Drawing.Size(219, 22)
        ItemAddToWF.Tag = "AddToWF"
        ItemAddToWF.Text = "Agregar a WorkFlow"
        AddHandler ItemAddToWF.Click, AddressOf _itemClicked
        '
        'ExportarAPDFToolStripMenuItem
        '
        ItemExportToPDF.Name = "ExportarAPDFToolStripMenuItem"
        ItemExportToPDF.Size = New System.Drawing.Size(219, 22)
        ItemExportToPDF.Tag = "ExportToPDF"
        ItemExportToPDF.Text = "Exportar a PDF"
        AddHandler ItemExportToPDF.Click, AddressOf _itemClicked
        '
        'EliminarToolStripMenuItem
        '
        ItemDeleteResult.Name = "EliminarToolStripMenuItem"
        ItemDeleteResult.Size = New System.Drawing.Size(219, 22)
        ItemDeleteResult.Tag = "Delete"
        ItemDeleteResult.Text = "Eliminar"
        AddHandler ItemDeleteResult.Click, AddressOf _itemClicked


        '
        'btnEditar
        '
        ItemEditTif.Name = "btnEditar"
        ItemEditTif.Size = New System.Drawing.Size(219, 22)
        ItemEditTif.Tag = "EditTIF"
        ItemEditTif.Text = "Editor de TIF"
        AddHandler ItemEditTif.Click, AddressOf _itemClicked

        '
        'btHistorial
        '
        ItemShowHistory.Name = "btHistorial"
        ItemShowHistory.Size = New System.Drawing.Size(219, 22)
        ItemShowHistory.Tag = "History"
        ItemShowHistory.Text = "Historial"
        AddHandler ItemShowHistory.Click, AddressOf _itemClicked

        '
        'PropiedadesToolStripMenuItem
        '
        ItemShowResultProperties.Name = "PropiedadesToolStripMenuItem"
        ItemShowResultProperties.Size = New System.Drawing.Size(219, 22)
        ItemShowResultProperties.Tag = "Property"
        ItemShowResultProperties.Text = "Propiedades"
        AddHandler ItemShowResultProperties.Click, AddressOf _itemClicked

        '
        'GenerarLinkAResultadoToolStripMenuItem
        '
        ItemGenerateWindowsLink.Name = "GenerarLinkAResultadoToolStripMenuItem"
        ItemGenerateWindowsLink.Size = New System.Drawing.Size(219, 22)
        ItemGenerateWindowsLink.Tag = "GenerateLink"
        ItemGenerateWindowsLink.Text = "Generar Link a Resultado"
        AddHandler ItemGenerateWindowsLink.Click, AddressOf _itemClicked

        'GenerarLinkAResultadoWebToolStripMenuItem
        '
        ItemGenerateWebLink.Name = "GenerarLinkAResultadoWebToolStripMenuItem"
        ItemGenerateWebLink.Size = New System.Drawing.Size(219, 22)
        ItemGenerateWebLink.Tag = "GenerateLinkWeb"
        ItemGenerateWebLink.Text = "Generar Link a Resultado Web"
        AddHandler ItemGenerateWebLink.Click, AddressOf _itemClicked

        '
        'ImprimirIndicesToolStripMenuItem
        '
        ItemPrintIndexs.Name = "ImprimirIndicesToolStripMenuItem"
        ItemPrintIndexs.Size = New System.Drawing.Size(219, 22)
        ItemPrintIndexs.Tag = "PrintIndexs"
        ItemPrintIndexs.Text = "Imprimir Atributos"
        AddHandler ItemPrintIndexs.Click, AddressOf _itemClicked

        '
        'UCContextResult
        '
        contextMenu.Items.AddRange(New RadMenuItem() {ItemAddToWF, ItemExportToPDF, ItemPrintIndexs, ItemDeleteResult, ItemEditTif, ItemShowHistory, ItemShowResultProperties, ItemGenerateWindowsLink, ItemGenerateWebLink})
    End Sub

    Private Sub _itemClicked(sender As Object, e As EventArgs)
        Dim listResults As List(Of IResult)
        listResults = ContextMenuContainer.GetSelectedResults()
        ContextMenuContainer.currentContextMenuClick(sender.tag, listResults, ContextMenuContainer)
    End Sub

    Public Sub loadRights(ByRef Result As IResult)

        If Result.ID <> 0 Then

            'EIMINAR
            ItemDeleteResult.Enabled = UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.Delete, Result.DocTypeId)


            'HISTORIAL
            ItemShowHistory.Enabled = UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.FrmDocHistory, RightsType.View)

            'EDITOR TIF
            ItemEditTif.Enabled = UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.Edit, Result.DocTypeId) AndAlso Result.IsTif

            'EXOPORT TO PDF
            ItemExportToPDF.Enabled = Result.IsImage Or Result.IsWord

            'PROPIEDADES
            ItemShowResultProperties.Enabled = UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.FrmDocProperty, RightsType.View)

            'WORKFLOW
            ItemAddToWF.Enabled = UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.ModuleWorkFlow, RightsType.Use)

            'Se habilita el resto por defecto que no se encuentra controlado por permisos.
            ItemGenerateWebLink.Enabled = True
            ItemGenerateWindowsLink.Enabled = True
            ItemPrintIndexs.Enabled = True
        Else
            'Si doc_id = 0 entonces es una insercion y no deberia cargarse ninguna opcion.
            ItemDeleteResult.Enabled = False

            ItemShowHistory.Enabled = False
            ItemEditTif.Enabled = False
            ItemExportToPDF.Enabled = False
            ItemAddToWF.Enabled = False
            ItemGenerateWebLink.Enabled = False
            ItemGenerateWindowsLink.Enabled = False
            ItemPrintIndexs.Enabled = False
            ItemShowResultProperties.Enabled = False
        End If
    End Sub

End Class
