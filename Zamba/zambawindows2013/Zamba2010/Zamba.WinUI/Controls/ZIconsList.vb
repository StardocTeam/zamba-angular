Public Class ZIconsList
    Inherits System.ComponentModel.Component
    Implements IDisposable

#Region " Código generado por el Diseñador de componentes "

    Public Sub New(Container As System.ComponentModel.IContainer)
        MyClass.New()

        'Requerido para la compatibilidad con el Diseñador de composiciones de clases Windows.Forms
        Container.Add(Me)
    End Sub

    Public Sub New()
        MyBase.New()

        'El Diseñador de componentes requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub

    'Component reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de componentes
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de componentes requiere el siguiente procedimiento
    'Se puede modificar utilizando el Diseñador de componentes.
    'No lo modifique con el editor de código.
    Public WithEvents ZIconList As ImageList
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(ZIconsList))
        ZIconList = New ImageList(components)
        '
        'ZIconList
        '
        ZIconList.ImageStream = CType(resources.GetObject("ZIconList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        ZIconList.TransparentColor = System.Drawing.Color.Transparent
        ZIconList.Images.SetKeyName(0, "appbar.box.layered.png")
        ZIconList.Images.SetKeyName(1, "appbar.image.landscape.png")
        ZIconList.Images.SetKeyName(2, "appbar.page.word.png")
        ZIconList.Images.SetKeyName(3, "appbar.page.excel.png")
        ZIconList.Images.SetKeyName(4, "appbar.page.file.pdf.tag.png")
        ZIconList.Images.SetKeyName(5, "")
        ZIconList.Images.SetKeyName(6, "appbar.ie.png")
        ZIconList.Images.SetKeyName(7, "")
        ZIconList.Images.SetKeyName(8, "appbar.page.excel.png")
        ZIconList.Images.SetKeyName(9, "appbar.ie.png")
        ZIconList.Images.SetKeyName(10, "")
        ZIconList.Images.SetKeyName(11, "")
        ZIconList.Images.SetKeyName(12, "")
        ZIconList.Images.SetKeyName(13, "")
        ZIconList.Images.SetKeyName(14, "")
        ZIconList.Images.SetKeyName(15, "")
        ZIconList.Images.SetKeyName(16, "")
        ZIconList.Images.SetKeyName(17, "Icon 405.ico")
        ZIconList.Images.SetKeyName(18, "appbar.axis.x.png")
        ZIconList.Images.SetKeyName(19, "")
        ZIconList.Images.SetKeyName(20, "")
        ZIconList.Images.SetKeyName(21, "")
        ZIconList.Images.SetKeyName(22, "")
        ZIconList.Images.SetKeyName(23, "")
        ZIconList.Images.SetKeyName(24, "")
        ZIconList.Images.SetKeyName(25, "")
        ZIconList.Images.SetKeyName(26, "")
        ZIconList.Images.SetKeyName(27, "")
        ZIconList.Images.SetKeyName(28, "")
        ZIconList.Images.SetKeyName(29, "")
        ZIconList.Images.SetKeyName(30, "appbar.form.basic.png")
        ZIconList.Images.SetKeyName(31, "appbar.debug.step.into.png")
        ZIconList.Images.SetKeyName(32, "appbar.user.png")
        ZIconList.Images.SetKeyName(33, "appbar.text.align.right.png")
        ZIconList.Images.SetKeyName(34, "appbar.debug.step.out.png")
        ZIconList.Images.SetKeyName(35, "appbar.debug.step.into.png")
        ZIconList.Images.SetKeyName(36, "appbar.debug.step.out.png")
        ZIconList.Images.SetKeyName(37, "error.gif")
        ZIconList.Images.SetKeyName(38, "075793-3d-glossy-blue-orb-icon-business-gears-sc37.png")
        ZIconList.Images.SetKeyName(39, "appbar.email.hardedge.png")
        ZIconList.Images.SetKeyName(40, "appbar.refresh.png")
        ZIconList.Images.SetKeyName(41, "appbar.folder.open.png")
        ZIconList.Images.SetKeyName(42, "businessman_preferences.png")
        ZIconList.Images.SetKeyName(43, "warning.gif")
        ZIconList.Images.SetKeyName(44, "appbar.page.search.png")
        ZIconList.Images.SetKeyName(45, "appbar.add.png")
        ZIconList.Images.SetKeyName(46, "appbar.user.delete.png")
        ZIconList.Images.SetKeyName(47, "appbar.user.minus.png")
        ZIconList.Images.SetKeyName(48, "appbar.user.tie.png")
        ZIconList.Images.SetKeyName(49, "reject-512.png")
        ZIconList.Images.SetKeyName(50, "user_ok_man_male_profile_account_person_people-512.png")
        ZIconList.Images.SetKeyName(51, "server.png")
        ZIconList.Images.SetKeyName(52, "user_lock_man_male_profile_account_person-512.png")
        ZIconList.Images.SetKeyName(53, "appbar.arrow.right.png")
        ZIconList.Images.SetKeyName(54, "2000px-Yes_Check_Circle.svg.png")
        ZIconList.Images.SetKeyName(55, "716964-alarm-512.png")
        ZIconList.Images.SetKeyName(56, "1482803089_SEO_search.png")
        ZIconList.Images.SetKeyName(57, "appbar.adobe.acrobat.png")
        ZIconList.Images.SetKeyName(58, "appbar.alert.png")
        ZIconList.Images.SetKeyName(59, "appbar.barcode.png")
        ZIconList.Images.SetKeyName(60, "appbar.billing.png")
        ZIconList.Images.SetKeyName(61, "appbar.chat.png")
        ZIconList.Images.SetKeyName(62, "appbar.check.png")
        ZIconList.Images.SetKeyName(63, "appbar.delete.png")
        ZIconList.Images.SetKeyName(64, "appbar.disk.png")
        ZIconList.Images.SetKeyName(65, "appbar.globe.png")
        ZIconList.Images.SetKeyName(66, "appbar.link.png")
        ZIconList.Images.SetKeyName(67, "appbar.list.check.png")
        ZIconList.Images.SetKeyName(68, "appbar.image.landscape.png")
        ZIconList.Images.SetKeyName(69, "appbar.inbox.in.png")
        ZIconList.Images.SetKeyName(70, "appbar.inbox.out.png")
        ZIconList.Images.SetKeyName(71, "appbar.information.png")
        ZIconList.Images.SetKeyName(72, "appbar.input.pen.png")
        ZIconList.Images.SetKeyName(73, "appbar.input.question.png")
        ZIconList.Images.SetKeyName(74, "appbar.key.old.png")
        ZIconList.Images.SetKeyName(75, "appbar.location.checkin.png")
        ZIconList.Images.SetKeyName(76, "appbar.lock.png")
        ZIconList.Images.SetKeyName(77, "appbar.microphone.google.png")
        ZIconList.Images.SetKeyName(78, "appbar.office.365.png")
        ZIconList.Images.SetKeyName(79, "appbar.office.access.png")
        ZIconList.Images.SetKeyName(80, "appbar.office.excel.png")
        ZIconList.Images.SetKeyName(81, "appbar.office.infopath.png")
        ZIconList.Images.SetKeyName(82, "appbar.office.lync.png")
        ZIconList.Images.SetKeyName(83, "appbar.office.onenote.png")
        ZIconList.Images.SetKeyName(84, "appbar.office.outlook.png")
        ZIconList.Images.SetKeyName(85, "appbar.office.png")
        ZIconList.Images.SetKeyName(86, "appbar.office.powerpoint.png")
        ZIconList.Images.SetKeyName(87, "appbar.office.project.png")
        ZIconList.Images.SetKeyName(88, "appbar.office.publisher.png")
        ZIconList.Images.SetKeyName(89, "appbar.office.sharepoint.png")
        ZIconList.Images.SetKeyName(90, "appbar.office.visio.png")
        ZIconList.Images.SetKeyName(91, "appbar.office.word.png")
        ZIconList.Images.SetKeyName(92, "appbar.page.add.png")
        ZIconList.Images.SetKeyName(93, "appbar.page.check.png")
        ZIconList.Images.SetKeyName(94, "appbar.page.delete.png")
        ZIconList.Images.SetKeyName(95, "appbar.page.download.png")
        ZIconList.Images.SetKeyName(96, "appbar.page.edit.png")
        ZIconList.Images.SetKeyName(97, "appbar.page.file.gif.tag.png")
        ZIconList.Images.SetKeyName(98, "appbar.page.file.pdf.tag.png")
        ZIconList.Images.SetKeyName(99, "appbar.page.heart.png")
        ZIconList.Images.SetKeyName(100, "appbar.page.heartbreak.png")
        ZIconList.Images.SetKeyName(101, "appbar.page.image.png")
        ZIconList.Images.SetKeyName(102, "appbar.page.jpg.png")
        ZIconList.Images.SetKeyName(103, "appbar.page.location.add.png")
        ZIconList.Images.SetKeyName(104, "appbar.page.location.png")
        ZIconList.Images.SetKeyName(105, "appbar.paperclip.png")
        ZIconList.Images.SetKeyName(106, "appbar.people.checkbox.png")
        ZIconList.Images.SetKeyName(107, "appbar.people.magnify.png")
        ZIconList.Images.SetKeyName(108, "appbar.people.multiple.magnify.png")
        ZIconList.Images.SetKeyName(109, "appbar.pie.png")
        ZIconList.Images.SetKeyName(110, "appbar.pin.png")
        ZIconList.Images.SetKeyName(111, "appbar.power.png")
        ZIconList.Images.SetKeyName(112, "appbar.printer.text.png")
        ZIconList.Images.SetKeyName(113, "appbar.qr.png")
        ZIconList.Images.SetKeyName(114, "appbar.redo.png")
        ZIconList.Images.SetKeyName(115, "appbar.refresh.png")
        ZIconList.Images.SetKeyName(116, "appbar.reply.email.png")
        ZIconList.Images.SetKeyName(117, "appbar.settings.png")
        ZIconList.Images.SetKeyName(118, "appbar.social.dropbox.download.png")
        ZIconList.Images.SetKeyName(119, "appbar.social.dropbox.upload.png")
        ZIconList.Images.SetKeyName(120, "appbar.social.facebook.variant.png")
        ZIconList.Images.SetKeyName(121, "appbar.social.linkedin.variant.png")
        ZIconList.Images.SetKeyName(122, "appbar.social.microsoft.png")
        ZIconList.Images.SetKeyName(123, "appbar.social.picasa.png")
        ZIconList.Images.SetKeyName(124, "appbar.social.pinterest.png")
        ZIconList.Images.SetKeyName(125, "appbar.social.skype.png")
        ZIconList.Images.SetKeyName(126, "appbar.social.twitter.png")
        ZIconList.Images.SetKeyName(127, "appbar.social.whatsapp.png")
        ZIconList.Images.SetKeyName(128, "appbar.stock.up.png")
        ZIconList.Images.SetKeyName(129, "appbar.timer.check.png")
        ZIconList.Images.SetKeyName(130, "appbar.timer.pause.png")
        ZIconList.Images.SetKeyName(131, "appbar.timer.play.png")
        ZIconList.Images.SetKeyName(132, "appbar.timer.stop.png")
        ZIconList.Images.SetKeyName(133, "images.png")
        ZIconList.Images.SetKeyName(134, "next-512.png")
        ZIconList.Images.SetKeyName(135, "Untitled-11-512.png")
        ZIconList.Images.SetKeyName(136, "user_lock_man_male_profile_account_person-512.png")
        ZIconList.Images.SetKeyName(137, "user_ok_man_male_profile_account_person_people-512.png")
        ZIconList.Images.SetKeyName(138, "user_remove_man_male_profile_account_person-512.png")
        ZIconList.Images.SetKeyName(139, "appbar.thumb.up.add.png")
        ZIconList.Images.SetKeyName(140, "appbar.thumb.up.delete.png")
        ZIconList.Images.SetKeyName(141, "appbar.thumb.up.minus.png")
        ZIconList.Images.SetKeyName(142, "appbar.thumbs.down.png")
        ZIconList.Images.SetKeyName(143, "appbar.thumbs.up.png")
        ZIconList.Images.SetKeyName(144, "appbar.printer.blank.png")
        ZIconList.Images.SetKeyName(145, "appbar.people.arrow.right.png")
        ZIconList.Images.SetKeyName(146, "appbar.new.png")
        ZIconList.Images.SetKeyName(147, "appbar.edit.png")

    End Sub

#End Region

End Class
