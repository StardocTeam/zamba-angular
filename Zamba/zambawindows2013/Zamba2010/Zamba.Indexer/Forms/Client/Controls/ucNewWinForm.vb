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
    Friend WithEvents TextBox As TextBox
    Friend WithEvents CheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents ComboBox As ComboBox
    Friend WithEvents Button As ZButton
    Friend WithEvents LinkLabel As System.Windows.Forms.LinkLabel
    Friend WithEvents Label As ZLabel
    Friend WithEvents PropertyGrid1 As System.Windows.Forms.PropertyGrid
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents PictureBox As ZLabel
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        'Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(ucNewWinForm))
        PanelTop = New System.Windows.Forms.Panel
        PanelFill = New System.Windows.Forms.Panel
        PanelForm = New System.Windows.Forms.Panel
        Splitter2 = New System.Windows.Forms.Splitter
        PanelComponents = New System.Windows.Forms.Panel
        TextBox = New TextBox
        CheckBox = New System.Windows.Forms.CheckBox
        ComboBox = New ComboBox
        Button = New ZButton
        LinkLabel = New System.Windows.Forms.LinkLabel
        Label = New ZLabel
        Splitter1 = New System.Windows.Forms.Splitter
        PanelPropertyGrid = New System.Windows.Forms.Panel
        PropertyGrid1 = New System.Windows.Forms.PropertyGrid
        Splitter3 = New System.Windows.Forms.Splitter
        PictureBox = New ZLabel
        PanelFill.SuspendLayout()
        PanelComponents.SuspendLayout()
        PanelPropertyGrid.SuspendLayout()
        SuspendLayout()
        '
        'ZIconList
        '

        '
        'PanelTop
        '
        PanelTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        PanelTop.Dock = System.Windows.Forms.DockStyle.Top
        PanelTop.Location = New System.Drawing.Point(0, 0)
        PanelTop.Name = "PanelTop"
        PanelTop.Size = New System.Drawing.Size(608, 64)
        PanelTop.TabIndex = 1
        '
        'PanelFill
        '
        PanelFill.Controls.Add(PanelForm)
        PanelFill.Controls.Add(Splitter2)
        PanelFill.Controls.Add(PanelComponents)
        PanelFill.Controls.Add(Splitter1)
        PanelFill.Controls.Add(PanelPropertyGrid)
        PanelFill.Dock = System.Windows.Forms.DockStyle.Fill
        PanelFill.Location = New System.Drawing.Point(0, 68)
        PanelFill.Name = "PanelFill"
        PanelFill.Size = New System.Drawing.Size(608, 452)
        PanelFill.TabIndex = 2
        '
        'PanelForm
        '
        PanelForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        PanelForm.Dock = System.Windows.Forms.DockStyle.Fill
        PanelForm.Location = New System.Drawing.Point(196, 0)
        PanelForm.Name = "PanelForm"
        PanelForm.Size = New System.Drawing.Size(412, 360)
        PanelForm.TabIndex = 54
        '
        'Splitter2
        '
        Splitter2.BackColor = System.Drawing.Color.FromArgb(CType(213, Byte), CType(213, Byte), CType(255, Byte))
        Splitter2.Dock = System.Windows.Forms.DockStyle.Bottom
        Splitter2.Enabled = False
        Splitter2.Location = New System.Drawing.Point(196, 360)
        Splitter2.Name = "Splitter2"
        Splitter2.Size = New System.Drawing.Size(412, 4)
        Splitter2.TabIndex = 56
        Splitter2.TabStop = False
        '
        'PanelComponents
        '
        PanelComponents.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        PanelComponents.Controls.Add(PictureBox)
        PanelComponents.Controls.Add(TextBox)
        PanelComponents.Controls.Add(CheckBox)
        PanelComponents.Controls.Add(ComboBox)
        PanelComponents.Controls.Add(Button)
        PanelComponents.Controls.Add(LinkLabel)
        PanelComponents.Controls.Add(Label)
        PanelComponents.Dock = System.Windows.Forms.DockStyle.Bottom
        PanelComponents.Location = New System.Drawing.Point(196, 364)
        PanelComponents.Name = "PanelComponents"
        PanelComponents.Size = New System.Drawing.Size(412, 88)
        PanelComponents.TabIndex = 55
        '
        'TextBox
        '
        TextBox.Cursor = System.Windows.Forms.Cursors.Hand
        TextBox.Location = New System.Drawing.Point(248, 16)
        TextBox.Name = "TextBox"
        TextBox.Size = New System.Drawing.Size(104, 21)
        TextBox.TabIndex = 5
        TextBox.Text = "TextBox"
        '
        'CheckBox
        '
        CheckBox.Cursor = System.Windows.Forms.Cursors.Hand
        CheckBox.Location = New System.Drawing.Point(136, 48)
        CheckBox.Name = "CheckBox"
        CheckBox.Size = New System.Drawing.Size(96, 32)
        CheckBox.TabIndex = 4
        CheckBox.Text = "CheckBox"
        '
        'ComboBox
        '
        ComboBox.Cursor = System.Windows.Forms.Cursors.Hand
        ComboBox.Items.AddRange(New Object() {"ComboBox"})
        ComboBox.Location = New System.Drawing.Point(32, 48)
        ComboBox.Name = "ComboBox"
        ComboBox.Size = New System.Drawing.Size(88, 21)
        ComboBox.TabIndex = 3
        ComboBox.Text = "ComboBox"
        '
        'Button
        '
        Button.Cursor = System.Windows.Forms.Cursors.Hand
        Button.Location = New System.Drawing.Point(168, 16)
        Button.Name = "Button"
        Button.Size = New System.Drawing.Size(64, 24)
        Button.TabIndex = 2
        Button.Text = "Button"
        '
        'LinkLabel
        '
        LinkLabel.Cursor = System.Windows.Forms.Cursors.Hand
        LinkLabel.Location = New System.Drawing.Point(96, 16)
        LinkLabel.Name = "LinkLabel"
        LinkLabel.Size = New System.Drawing.Size(64, 23)
        LinkLabel.TabIndex = 1
        LinkLabel.TabStop = True
        LinkLabel.Text = "LinkLabel"
        '
        'Label
        '
        Label.Cursor = System.Windows.Forms.Cursors.Hand
        Label.Location = New System.Drawing.Point(32, 16)
        Label.Name = "Label"
        Label.Size = New System.Drawing.Size(56, 23)
        Label.TabIndex = 0
        Label.Text = "Label"
        '
        'Splitter1
        '
        Splitter1.BackColor = System.Drawing.Color.FromArgb(CType(213, Byte), CType(213, Byte), CType(255, Byte))
        Splitter1.Location = New System.Drawing.Point(192, 0)
        Splitter1.Name = "Splitter1"
        Splitter1.Size = New System.Drawing.Size(4, 452)
        Splitter1.TabIndex = 53
        Splitter1.TabStop = False
        '
        'PanelPropertyGrid
        '
        PanelPropertyGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        PanelPropertyGrid.Controls.Add(PropertyGrid1)
        PanelPropertyGrid.Dock = System.Windows.Forms.DockStyle.Left
        PanelPropertyGrid.Location = New System.Drawing.Point(0, 0)
        PanelPropertyGrid.Name = "PanelPropertyGrid"
        PanelPropertyGrid.Size = New System.Drawing.Size(192, 452)
        PanelPropertyGrid.TabIndex = 52
        '
        'PropertyGrid1
        '
        PropertyGrid1.CommandsBackColor = System.Drawing.Color.White
        PropertyGrid1.CommandsForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        PropertyGrid1.CommandsVisibleIfAvailable = True
        PropertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill
        PropertyGrid1.LargeButtons = False
        PropertyGrid1.LineColor = System.Drawing.SystemColors.ScrollBar
        PropertyGrid1.Location = New System.Drawing.Point(0, 0)
        PropertyGrid1.Name = "PropertyGrid1"
        PropertyGrid1.Size = New System.Drawing.Size(190, 450)
        PropertyGrid1.TabIndex = 0
        PropertyGrid1.Text = "PropertyGrid1"
        PropertyGrid1.ViewBackColor = System.Drawing.SystemColors.Window
        PropertyGrid1.ViewForeColor = System.Drawing.SystemColors.WindowText
        '
        'Splitter3
        '
        Splitter3.BackColor = System.Drawing.Color.FromArgb(CType(213, Byte), CType(213, Byte), CType(255, Byte))
        Splitter3.Dock = System.Windows.Forms.DockStyle.Top
        Splitter3.Enabled = False
        Splitter3.Location = New System.Drawing.Point(0, 64)
        Splitter3.Name = "Splitter3"
        Splitter3.Size = New System.Drawing.Size(608, 4)
        Splitter3.TabIndex = 50
        Splitter3.TabStop = False
        '
        'PictureBox
        '
        PictureBox.Cursor = System.Windows.Forms.Cursors.Hand
        PictureBox.Location = New System.Drawing.Point(240, 56)
        PictureBox.Name = "PictureBox"
        PictureBox.Size = New System.Drawing.Size(112, 23)
        PictureBox.TabIndex = 6
        PictureBox.Text = "PictureBox"
        '
        'ucNewWinForm
        '
        BackColor = System.Drawing.Color.White
        Controls.Add(PanelFill)
        Controls.Add(Splitter3)
        Controls.Add(PanelTop)
        Name = "ucNewWinForm"
        Size = New System.Drawing.Size(608, 520)
        PanelFill.ResumeLayout(False)
        PanelComponents.ResumeLayout(False)
        PanelPropertyGrid.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

#Region "PanelForm"
    Private Sub SelectComp(ByVal sender As Object, ByVal e As EventArgs)
        Try
            PropertyGrid1.SelectedObject = sender
            PanelForm.CreateControl()
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "AddComp"
    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Button.Click
        Try
            Dim b As New Button
            b.Location = New Point(20, 20)
            b.BackColor = Color.Crimson
            PanelForm.Controls.Add(b)
            b.BringToFront()
            RemoveHandler b.Click, AddressOf SelectComp
            AddHandler b.Click, AddressOf SelectComp
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Sub Label_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Label.Click
        Try
            Dim l As New Label
            PanelForm.Controls.Add(l)
            l.BringToFront()
            RemoveHandler l.Click, AddressOf SelectComp
            AddHandler l.Click, AddressOf SelectComp
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Sub LinkLabel_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles LinkLabel.Click
        Try
            Dim l As New LinkLabel
            PanelForm.Controls.Add(l)
            l.BringToFront()
            RemoveHandler l.Click, AddressOf SelectComp
            AddHandler l.Click, AddressOf SelectComp
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Sub TextBox_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TextBox.Click
        Try
            Dim t As New TextBox
            PanelForm.Controls.Add(t)
            t.BringToFront()
            RemoveHandler t.Click, AddressOf SelectComp
            AddHandler t.Click, AddressOf SelectComp
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Sub ComboBox_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBox.Click
        Try
            Dim c As New ComboBox
            PanelForm.Controls.Add(c)
            c.BringToFront()
            RemoveHandler c.Click, AddressOf SelectComp
            AddHandler c.Click, AddressOf SelectComp
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Sub CheckBox_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CheckBox.Click
        Try
            Dim c As New CheckBox
            PanelForm.Controls.Add(c)
            c.BringToFront()
            RemoveHandler c.Click, AddressOf SelectComp
            AddHandler c.Click, AddressOf SelectComp
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Private Sub PictureBox_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles PictureBox.Click
        Try
            Dim p As New PictureBox
            PanelForm.Controls.Add(p)
            p.BringToFront()
            RemoveHandler p.Click, AddressOf SelectComp
            AddHandler p.Click, AddressOf SelectComp
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
#End Region


End Class
