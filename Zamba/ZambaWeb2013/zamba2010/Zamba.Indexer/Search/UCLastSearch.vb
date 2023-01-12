Imports Zamba.AppBlock
Imports System.Collections
Imports Zamba.Core
Public Class UCLastSearch
    Inherits ZControl

#Region " Código generado por el Diseñador de Windows Forms "


    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    '  Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
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
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Name = "UCLastSearch"
        Me.Size = New System.Drawing.Size(224, 272)

    End Sub

#End Region

    Dim ls As New LastSearchs_Factory

    '    Dim Names As New ArrayList
    Public Sub New()
        MyBase.New()
        InitializeComponent()
        Try
            GLS()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
#Region "Metodos Privados"
    Public Sub RefreshLastSearchs()
        Try
            GLS()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub GLS()
        Try
            GetlastSearchs3()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    'Private Sub GetlastSearch()
    '    Names = ls.GetLastSearchNames()
    '    DAddLinks()
    'End Sub
    'Private Sub DAddLinks()
    '    Try
    '        Dim D1 As New DelegateAddLinks(AddressOf AddLinks)
    '        Me.Invoke(D1)
    '    Catch
    '    End Try
    'End Sub
    'Private Sub AddLinks()
    '    If Names.Count = 0 Then
    '        Dim Lnk As New ZLinkLabel
    '        '          Lnk.ImageList = Me.ImageList1
    '        '         Lnk.ImageIndex = 0
    '        Lnk.LinkBehavior = Windows.Forms.LinkBehavior.HoverUnderline
    '        Lnk.Tag = 0
    '        lnk.AutoSize = True
    '        Lnk.Text = "No hay busquedas en el historial"
    '        Lnk.Location = New Drawing.Point(3, (Me.Panel2.Controls.Count * 25) + 3)
    '        Me.Panel2.Controls.Add(Lnk)
    '    Else
    '        Dim i As Int32
    '        For i = 0 To Names.Count - 1
    '            Dim Lnk As New ZLinkLabel
    '            '                Lnk.ImageList = Me.ImageList1
    '            '               Lnk.ImageIndex = 0
    '            Lnk.LinkBehavior = Windows.Forms.LinkBehavior.HoverUnderline
    '            Lnk.Tag = i
    '            lnk.AutoSize = True
    '            Lnk.Text = Names(i)
    '            Dim name As String = Names(i)
    '            Lnk.Location = New Drawing.Point(3, (Me.Panel2.Controls.Count * 25) + 3)
    '            ', name.Length + 50, 25)
    '            lnk.AutoSize = True
    '            AddHandler Lnk.LinkClicked, AddressOf linkClicked
    '            Me.Panel2.Controls.Add(Lnk)
    '        Next
    '    End If
    'End Sub

    'Private Sub linkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
    '    Try
    '        Dim Link As Windows.Forms.LinkLabel
    '        Link = sender
    '        Dim Index As Int32 = Link.Tag
    '        ls.SetCurrentSearch(Index + 1)
    '        RaiseEvent LnkClicked(ls)
    '    Catch ex As Exception
    '        zclass.raiseerror(ex)
    '    End Try
    'End Sub

#End Region
    'Private Delegate Sub DelegateAddLinks()
    'Public Event LnkClicked(ByVal LastSearch As Zamba.Searchs.LastSearch)

    Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem1.Click
        Try
            Me.Controls.Clear()
            ls.DeleteLastSearch()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub


#Region "3"
    Private LastSearchsArray As New ArrayList
    Private Sub GetlastSearchs3()
        LastSearchsArray = LastSearchs_Factory.GetLastSearchs3()
        Me.Controls.Clear()
        DAddLinks3()
    End Sub
    Private Sub DAddLinks3()
        Try
            AddLinks3()
        Catch ex As Exception
            'oscar 2006-08-07 1701
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub AddLinks3()
        If LastSearchsArray.Count = 0 Then
            Dim Lnk As New ZLinkLabel
            Lnk.LinkBehavior = Windows.Forms.LinkBehavior.HoverUnderline
            Lnk.Tag = 0
            Lnk.AutoSize = True
            Lnk.Text = "No hay busquedas en el historial"
            Lnk.Location = New Drawing.Point(15, (Me.Controls.Count * 25) + 5)
            Dim ctx As New ContextMenu
            Dim mnu As New MenuItem("Ejecutar nuevamente")
            ctx.MenuItems.Add(mnu)
            RemoveHandler mnu.Click, AddressOf ExecuteSQL
            AddHandler mnu.Click, AddressOf ExecuteSQL
            Lnk.ContextMenu = ctx
            Me.Controls.Add(Lnk)
        Else
            Dim i As Int32
            For i = 0 To LastSearchsArray.Count - 1
                Dim Lnk As New ZLinkLabel
                '                Lnk.ImageList = Me.ImageList1
                '               Lnk.ImageIndex = 0
                Lnk.LinkBehavior = Windows.Forms.LinkBehavior.HoverUnderline
                Lnk.Tag = DirectCast(LastSearchsArray(i), Zamba.Core.Searchs.LastSearch).Id
                Lnk.AutoSize = True
                Lnk.Text = DirectCast(LastSearchsArray(i), Zamba.Core.Searchs.LastSearch).Name
                Lnk.Location = New Drawing.Point(3, (Me.Controls.Count * 25) + 3)
                Lnk.AutoSize = True
                RemoveHandler Lnk.LinkClicked, AddressOf linkClicked3
                AddHandler Lnk.LinkClicked, AddressOf linkClicked3
                Me.Controls.Add(Lnk)
            Next
        End If
    End Sub
    Private Sub ExecuteSQL(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim Link As Windows.Forms.LinkLabel
            Link = DirectCast(sender, LinkLabel)
            Dim Id As Int32 = CInt(Link.Tag)
            Dim name As String = Link.Text
            '            LS.SetCurrentSearch(Index + 1)
            Dim ls As New Searchs.LastSearch(Id, name)
            ls.SQL = LastSearchs_Factory.GetLastSearchSQL3(ls.Id)
            RaiseEvent LnkReloaded3(ls)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub
    Private Sub linkClicked3(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        Try
            Dim Link As Windows.Forms.LinkLabel
            Link = DirectCast(sender, LinkLabel)
            Dim Id As Int32 = Int32.Parse(Link.Tag.ToString)
            Dim name As String = Link.Text
            ' ls.SetCurrentSearch(Index + 1)
            Dim ls As New Searchs.LastSearch(Id, name)
            ls.Results = LastSearchs_Factory.GetLastSearchResults3(ls.Id)
            RaiseEvent LnkClicked3(ls)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Public Sub delete()
        Try
            Dim LS As New UCLastSearch
            Dim i As Int32
            For i = 0 To LS.LastSearchsArray.Count - 1
                LS.LastSearchsArray.RemoveAt(i)
            Next
            LS.RefreshLastSearchs()
        Catch ex As Exception
        End Try

    End Sub

    Public Event LnkClicked3(ByVal LastSearch As Searchs.LastSearch)
    Public Event LnkReloaded3(ByVal LastSearch As Searchs.LastSearch)


#End Region
End Class
