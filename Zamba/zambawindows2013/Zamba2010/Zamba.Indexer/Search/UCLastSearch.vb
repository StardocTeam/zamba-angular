Imports Zamba.Core
Imports System.Collections.Generic
Imports Zamba.Core.Searchs
Imports Zamba.Core.Search

Public Class UCLastSearch
    Inherits ZControl

#Region " Código generado por el Diseñador de Windows Forms "
    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If components IsNot Nothing Then
                components.Dispose()
            End If
            If lastSearchs IsNot Nothing Then
                lastSearchs.Clear()
                lastSearchs = Nothing
            End If
            lsb = Nothing
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    '  Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents ContextMenu1 As ContextMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu()
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.SuspendLayout()
        '
        'ContextMenu1
        '
        Me.ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.Text = "Borrar Historial"
        '
        'UCLastSearch
        '
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "UCLastSearch"
        Me.Size = New System.Drawing.Size(224, 272)
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Atributos y Propiedades"
    Private lastSearchs As List(Of LastSearch) = Nothing
    Private lsb As New LastSearchBusiness
#End Region

#Region "Constructores y Carga"
    Public Sub New()
        MyBase.New()
        InitializeComponent()

    End Sub

    Private Sub UCLastSearch_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadLastSearchs()
    End Sub
#End Region

#Region "Métodos y Eventos"
    Public Sub LoadLastSearchs()
        Try
            lastSearchs = lsb.GetLastSearchs()

            Controls.Clear()
            If lastSearchs.Count = 0 Then
                Dim Lnk As New ZLinkLabel
                Lnk.LinkBehavior = LinkBehavior.HoverUnderline
                Lnk.Tag = 0
                Lnk.AutoSize = True
                Lnk.Text = "No hay búsquedas en el historial"
                Lnk.Font = Font
                Lnk.Location = New Drawing.Point(15, (Controls.Count * 25) + 5)
                Controls.Add(Lnk)
            Else
                'Agregamos un label como mensaje 16-09-2011 - Cristian Collazos
                Dim lblMensaje As New ZLabel
                lblMensaje.Text = "Listado de las últimas 20 búsquedas realizadas. Haga click en una para volver a ejecutarla."
                lblMensaje.Dock = DockStyle.Top
                lblMensaje.Font = Font
                lblMensaje.AutoSize = True
                Controls.Add(lblMensaje)

                Dim i As Int32
                For i = 0 To lastSearchs.Count - 1
                    Dim link As New LastSearchLink(lastSearchs(i))
                    link.Location = New Drawing.Point(3, (Controls.Count * 25) + 4)
                    RemoveHandler link.LinkClicked, AddressOf SearchClicked
                    AddHandler link.LinkClicked, AddressOf SearchClicked
                    Controls.Add(link)
                Next

                Dim zLinkDeleteAll As New ZLinkLabel
                zLinkDeleteAll.LinkBehavior = LinkBehavior.HoverUnderline
                zLinkDeleteAll.AutoSize = True
                zLinkDeleteAll.Text = "Limpiar historial"
                zLinkDeleteAll.Font = Font
                zLinkDeleteAll.Dock = DockStyle.Right
                zLinkDeleteAll.TextAlign = ContentAlignment.BottomRight
                RemoveHandler zLinkDeleteAll.LinkClicked, AddressOf zLinkDeleteAll_LinkClicked
                AddHandler zLinkDeleteAll.LinkClicked, AddressOf zLinkDeleteAll_LinkClicked
                Controls.Add(zLinkDeleteAll)

            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
            MessageBox.Show("Error al mostrar el historial de búsquedas", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SearchClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs)
        Try
            Dim _lastSearchLink As LastSearchLink = DirectCast(sender, LastSearchLink)
            Dim _lastSearch As New LastSearch(_lastSearchLink.Id, _lastSearchLink.Text)
            Dim _search As Search = LastSearchBusiness.GetSerializedSearchObject(_lastSearch.Id)
            _search.Name = _lastSearch.Name
            ModDocuments.ReLoad(_lastSearch, _search)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub zLinkDeleteAll_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        Try
            LastSearchBusiness.DeleteAll()
            LoadLastSearchs()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

End Class

Class LastSearchLink
    Inherits LinkLabel

    Public Property Id As Long

    Public Sub New(ByVal lastSearch As LastSearch)
        MyBase.New()
        Text = lastSearch.Name
        Id = lastSearch.Id

        LinkBehavior = LinkBehavior.HoverUnderline
        AutoSize = True
    End Sub
End Class
