Imports Zamba.AppBlock
Public Class ucNewWinForm
    Inherits ZControl

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub

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
    Friend WithEvents PanelTop As System.Windows.Forms.Panel
    Friend WithEvents PanelFill As System.Windows.Forms.Panel
    Friend WithEvents Splitter3 As System.Windows.Forms.Splitter
    Friend WithEvents PanelPropertyGrid As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents PanelForm As System.Windows.Forms.Panel
    Friend WithEvents PanelComponents As System.Windows.Forms.Panel
    Friend WithEvents TextBox As System.Windows.Forms.TextBox
    Friend WithEvents CheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents ComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Button As System.Windows.Forms.Button
    Friend WithEvents LinkLabel As System.Windows.Forms.LinkLabel
    Friend WithEvents Label As System.Windows.Forms.Label
    Friend WithEvents PropertyGrid1 As System.Windows.Forms.PropertyGrid
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents PictureBox As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        'Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(ucNewWinForm))
        Me.PanelTop = New System.Windows.Forms.Panel
        Me.PanelFill = New System.Windows.Forms.Panel
        Me.PanelForm = New System.Windows.Forms.Panel
        Me.Splitter2 = New System.Windows.Forms.Splitter
        Me.PanelComponents = New System.Windows.Forms.Panel
        Me.TextBox = New System.Windows.Forms.TextBox
        Me.CheckBox = New System.Windows.Forms.CheckBox
        Me.ComboBox = New System.Windows.Forms.ComboBox
        Me.Button = New System.Windows.Forms.Button
        Me.LinkLabel = New System.Windows.Forms.LinkLabel
        Me.Label = New System.Windows.Forms.Label
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.PanelPropertyGrid = New System.Windows.Forms.Panel
        Me.PropertyGrid1 = New System.Windows.Forms.PropertyGrid
        Me.Splitter3 = New System.Windows.Forms.Splitter
        Me.PictureBox = New System.Windows.Forms.Label
        Me.PanelFill.SuspendLayout()
        Me.PanelComponents.SuspendLayout()
        Me.PanelPropertyGrid.SuspendLayout()
        Me.SuspendLayout()
        '
        'ZIconList
        '

        '
        'PanelTop
        '
        Me.PanelTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelTop.Location = New System.Drawing.Point(0, 0)
        Me.PanelTop.Name = "PanelTop"
        Me.PanelTop.Size = New System.Drawing.Size(608, 64)
        Me.PanelTop.TabIndex = 1
        '
        'PanelFill
        '
        Me.PanelFill.Controls.Add(Me.PanelForm)
        Me.PanelFill.Controls.Add(Me.Splitter2)
        Me.PanelFill.Controls.Add(Me.PanelComponents)
        Me.PanelFill.Controls.Add(Me.Splitter1)
        Me.PanelFill.Controls.Add(Me.PanelPropertyGrid)
        Me.PanelFill.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelFill.Location = New System.Drawing.Point(0, 68)
        Me.PanelFill.Name = "PanelFill"
        Me.PanelFill.Size = New System.Drawing.Size(608, 452)
        Me.PanelFill.TabIndex = 2
        '
        'PanelForm
        '
        Me.PanelForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelForm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelForm.Location = New System.Drawing.Point(196, 0)
        Me.PanelForm.Name = "PanelForm"
        Me.PanelForm.Size = New System.Drawing.Size(412, 360)
        Me.PanelForm.TabIndex = 54
        '
        'Splitter2
        '
        Me.Splitter2.BackColor = System.Drawing.Color.FromArgb(CType(213, Byte), CType(213, Byte), CType(255, Byte))
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter2.Enabled = False
        Me.Splitter2.Location = New System.Drawing.Point(196, 360)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(412, 4)
        Me.Splitter2.TabIndex = 56
        Me.Splitter2.TabStop = False
        '
        'PanelComponents
        '
        Me.PanelComponents.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelComponents.Controls.Add(Me.PictureBox)
        Me.PanelComponents.Controls.Add(Me.TextBox)
        Me.PanelComponents.Controls.Add(Me.CheckBox)
        Me.PanelComponents.Controls.Add(Me.ComboBox)
        Me.PanelComponents.Controls.Add(Me.Button)
        Me.PanelComponents.Controls.Add(Me.LinkLabel)
        Me.PanelComponents.Controls.Add(Me.Label)
        Me.PanelComponents.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelComponents.Location = New System.Drawing.Point(196, 364)
        Me.PanelComponents.Name = "PanelComponents"
        Me.PanelComponents.Size = New System.Drawing.Size(412, 88)
        Me.PanelComponents.TabIndex = 55
        '
        'TextBox
        '
        Me.TextBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TextBox.Location = New System.Drawing.Point(248, 16)
        Me.TextBox.Name = "TextBox"
        Me.TextBox.Size = New System.Drawing.Size(104, 21)
        Me.TextBox.TabIndex = 5
        Me.TextBox.Text = "TextBox"
        '
        'CheckBox
        '
        Me.CheckBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CheckBox.Location = New System.Drawing.Point(136, 48)
        Me.CheckBox.Name = "CheckBox"
        Me.CheckBox.Size = New System.Drawing.Size(96, 32)
        Me.CheckBox.TabIndex = 4
        Me.CheckBox.Text = "CheckBox"
        '
        'ComboBox
        '
        Me.ComboBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ComboBox.Items.AddRange(New Object() {"ComboBox"})
        Me.ComboBox.Location = New System.Drawing.Point(32, 48)
        Me.ComboBox.Name = "ComboBox"
        Me.ComboBox.Size = New System.Drawing.Size(88, 21)
        Me.ComboBox.TabIndex = 3
        Me.ComboBox.Text = "ComboBox"
        '
        'Button
        '
        Me.Button.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button.Location = New System.Drawing.Point(168, 16)
        Me.Button.Name = "Button"
        Me.Button.Size = New System.Drawing.Size(64, 24)
        Me.Button.TabIndex = 2
        Me.Button.Text = "Button"
        '
        'LinkLabel
        '
        Me.LinkLabel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.LinkLabel.Location = New System.Drawing.Point(96, 16)
        Me.LinkLabel.Name = "LinkLabel"
        Me.LinkLabel.Size = New System.Drawing.Size(64, 23)
        Me.LinkLabel.TabIndex = 1
        Me.LinkLabel.TabStop = True
        Me.LinkLabel.Text = "LinkLabel"
        '
        'Label
        '
        Me.Label.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label.Location = New System.Drawing.Point(32, 16)
        Me.Label.Name = "Label"
        Me.Label.Size = New System.Drawing.Size(56, 23)
        Me.Label.TabIndex = 0
        Me.Label.Text = "Label"
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.FromArgb(CType(213, Byte), CType(213, Byte), CType(255, Byte))
        Me.Splitter1.Location = New System.Drawing.Point(192, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(4, 452)
        Me.Splitter1.TabIndex = 53
        Me.Splitter1.TabStop = False
        '
        'PanelPropertyGrid
        '
        Me.PanelPropertyGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelPropertyGrid.Controls.Add(Me.PropertyGrid1)
        Me.PanelPropertyGrid.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelPropertyGrid.Location = New System.Drawing.Point(0, 0)
        Me.PanelPropertyGrid.Name = "PanelPropertyGrid"
        Me.PanelPropertyGrid.Size = New System.Drawing.Size(192, 452)
        Me.PanelPropertyGrid.TabIndex = 52
        '
        'PropertyGrid1
        '
        Me.PropertyGrid1.CommandsBackColor = System.Drawing.Color.White
        Me.PropertyGrid1.CommandsForeColor = System.Drawing.Color.Black
        Me.PropertyGrid1.CommandsVisibleIfAvailable = True
        Me.PropertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PropertyGrid1.LargeButtons = False
        Me.PropertyGrid1.LineColor = System.Drawing.SystemColors.ScrollBar
        Me.PropertyGrid1.Location = New System.Drawing.Point(0, 0)
        Me.PropertyGrid1.Name = "PropertyGrid1"
        Me.PropertyGrid1.Size = New System.Drawing.Size(190, 450)
        Me.PropertyGrid1.TabIndex = 0
        Me.PropertyGrid1.Text = "PropertyGrid1"
        Me.PropertyGrid1.ViewBackColor = System.Drawing.SystemColors.Window
        Me.PropertyGrid1.ViewForeColor = System.Drawing.SystemColors.WindowText
        '
        'Splitter3
        '
        Me.Splitter3.BackColor = System.Drawing.Color.FromArgb(CType(213, Byte), CType(213, Byte), CType(255, Byte))
        Me.Splitter3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter3.Enabled = False
        Me.Splitter3.Location = New System.Drawing.Point(0, 64)
        Me.Splitter3.Name = "Splitter3"
        Me.Splitter3.Size = New System.Drawing.Size(608, 4)
        Me.Splitter3.TabIndex = 50
        Me.Splitter3.TabStop = False
        '
        'PictureBox
        '
        Me.PictureBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox.Location = New System.Drawing.Point(240, 56)
        Me.PictureBox.Name = "PictureBox"
        Me.PictureBox.Size = New System.Drawing.Size(112, 23)
        Me.PictureBox.TabIndex = 6
        Me.PictureBox.Text = "PictureBox"
        '
        'ucNewWinForm
        '
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.PanelFill)
        Me.Controls.Add(Me.Splitter3)
        Me.Controls.Add(Me.PanelTop)
        Me.Name = "ucNewWinForm"
        Me.Size = New System.Drawing.Size(608, 520)
        Me.PanelFill.ResumeLayout(False)
        Me.PanelComponents.ResumeLayout(False)
        Me.PanelPropertyGrid.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "PanelForm"
    Private Sub SelectComp(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Me.PropertyGrid1.SelectedObject = sender
            Me.PanelForm.CreateControl()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "AddComp"
    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button.Click
        Try
            Dim b As New Button
            b.Location = New Point(20, 20)
            b.BackColor = Color.Crimson
            Me.PanelForm.Controls.Add(b)
            b.BringToFront()
            RemoveHandler b.Click, AddressOf SelectComp
            AddHandler b.Click, AddressOf SelectComp
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Sub Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label.Click
        Try
            Dim l As New Label
            Me.PanelForm.Controls.Add(l)
            l.BringToFront()
            RemoveHandler l.Click, AddressOf SelectComp
            AddHandler l.Click, AddressOf SelectComp
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Sub LinkLabel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinkLabel.Click
        Try
            Dim l As New LinkLabel
            Me.PanelForm.Controls.Add(l)
            l.BringToFront()
            RemoveHandler l.Click, AddressOf SelectComp
            AddHandler l.Click, AddressOf SelectComp
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Sub TextBox_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox.Click
        Try
            Dim t As New TextBox
            Me.PanelForm.Controls.Add(t)
            t.BringToFront()
            RemoveHandler t.Click, AddressOf SelectComp
            AddHandler t.Click, AddressOf SelectComp
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Sub ComboBox_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox.Click
        Try
            Dim c As New ComboBox
            Me.PanelForm.Controls.Add(c)
            c.BringToFront()
            RemoveHandler c.Click, AddressOf SelectComp
            AddHandler c.Click, AddressOf SelectComp
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Sub CheckBox_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox.Click
        Try
            Dim c As New CheckBox
            Me.PanelForm.Controls.Add(c)
            c.BringToFront()
            RemoveHandler c.Click, AddressOf SelectComp
            AddHandler c.Click, AddressOf SelectComp
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Sub PictureBox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox.Click
        Try
            Dim p As New PictureBox
            Me.PanelForm.Controls.Add(p)
            p.BringToFront()
            RemoveHandler p.Click, AddressOf SelectComp
            AddHandler p.Click, AddressOf SelectComp
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
#End Region


End Class
