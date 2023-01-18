Imports zamba.Core

Public Class UCStepCtrl
    Inherits ZControl

#Region " Código generado por el Diseñador de Windows Forms "

    Private Sub New()
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

    Private WithEvents TxtDescription As TextBox
    Friend WithEvents Label3 As ZLabel
    Private WithEvents txtId As ZLabel
    Friend WithEvents Label8 As ZLabel

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents BtnSave As ZButton
    Private WithEvents TextBox1 As TextBox
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents BtnAdd As ZButton
    Friend WithEvents BtnEdit As ZButton
    Friend WithEvents BtnDel As ZButton
    Friend WithEvents BtnInitial As ZButton
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents Label5 As ZLabel
    Friend WithEvents txtexpire As TextBox
    Friend WithEvents txtmaxdocs As TextBox
    Friend WithEvents Label6 As ZLabel
    Friend WithEvents Label7 As ZLabel
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents btnEditDescription As ZButton
    Friend WithEvents TxtName As TextBox
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents PanelTop As ZLabel
    Friend WithEvents txtdesc As TextBox
    Friend WithEvents btnSave2 As ZButton
    Friend WithEvents btnRemove As ZButton
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents cbColors As ComboBox
    Friend WithEvents rdbMonth As System.Windows.Forms.RadioButton
    Friend WithEvents rdbDay As System.Windows.Forms.RadioButton
    Friend WithEvents lstNumbersTypesAndColors As ListBox
    Friend WithEvents lblPrueba As ZLabel
    Friend WithEvents cbNumbers As ComboBox
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
    Friend WithEvents ZLabel1 As ZLabel
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UCStepCtrl))
        Me.TxtName = New System.Windows.Forms.TextBox()
        Me.TxtDescription = New System.Windows.Forms.TextBox()
        Me.Label3 = New Zamba.AppBlock.ZLabel()
        Me.txtId = New Zamba.AppBlock.ZLabel()
        Me.Label8 = New Zamba.AppBlock.ZLabel()
        Me.BtnSave = New Zamba.AppBlock.ZButton()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.btnSave2 = New Zamba.AppBlock.ZButton()
        Me.btnRemove = New Zamba.AppBlock.ZButton()
        Me.Label4 = New Zamba.AppBlock.ZLabel()
        Me.txtdesc = New System.Windows.Forms.TextBox()
        Me.Label2 = New Zamba.AppBlock.ZLabel()
        Me.cbColors = New System.Windows.Forms.ComboBox()
        Me.rdbMonth = New System.Windows.Forms.RadioButton()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.rdbDay = New System.Windows.Forms.RadioButton()
        Me.txtexpire = New System.Windows.Forms.TextBox()
        Me.lstNumbersTypesAndColors = New System.Windows.Forms.ListBox()
        Me.txtmaxdocs = New System.Windows.Forms.TextBox()
        Me.lblPrueba = New Zamba.AppBlock.ZLabel()
        Me.cbNumbers = New System.Windows.Forms.ComboBox()
        Me.Label7 = New Zamba.AppBlock.ZLabel()
        Me.Label6 = New Zamba.AppBlock.ZLabel()
        Me.Label1 = New Zamba.AppBlock.ZLabel()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.btnEditDescription = New Zamba.AppBlock.ZButton()
        Me.BtnInitial = New Zamba.AppBlock.ZButton()
        Me.BtnDel = New Zamba.AppBlock.ZButton()
        Me.BtnEdit = New Zamba.AppBlock.ZButton()
        Me.BtnAdd = New Zamba.AppBlock.ZButton()
        Me.Label5 = New Zamba.AppBlock.ZLabel()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.PanelTop = New Zamba.AppBlock.ZLabel()
        Me.ZLabel1 = New Zamba.AppBlock.ZLabel()
        Me.SuspendLayout()
        '
        'TxtName
        '
        Me.TxtName.BackColor = System.Drawing.Color.White
        Me.TxtName.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TxtName.Location = New System.Drawing.Point(96, 15)
        Me.TxtName.MaxLength = 0
        Me.TxtName.Name = "TxtName"
        Me.TxtName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtName.Size = New System.Drawing.Size(256, 23)
        Me.TxtName.TabIndex = 0
        Me.TxtName.WordWrap = False
        '
        'TxtDescription
        '
        Me.TxtDescription.BackColor = System.Drawing.Color.White
        Me.TxtDescription.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TxtDescription.Location = New System.Drawing.Point(96, 43)
        Me.TxtDescription.MaxLength = 0
        Me.TxtDescription.Name = "TxtDescription"
        Me.TxtDescription.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtDescription.Size = New System.Drawing.Size(256, 23)
        Me.TxtDescription.TabIndex = 1
        Me.TxtDescription.WordWrap = False
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.FontSize = 9.75!
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label3.Location = New System.Drawing.Point(9, 11)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(56, 16)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Nombre"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtId
        '
        Me.txtId.BackColor = System.Drawing.Color.Transparent
        Me.txtId.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtId.FontSize = 9.75!
        Me.txtId.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.txtId.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtId.Location = New System.Drawing.Point(38, 155)
        Me.txtId.Name = "txtId"
        Me.txtId.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtId.Size = New System.Drawing.Size(102, 24)
        Me.txtId.TabIndex = 0
        Me.txtId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.FontSize = 9.75!
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label8.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label8.Location = New System.Drawing.Point(9, 43)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(81, 24)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Descripcion"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'BtnSave
        '
        Me.BtnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSave.ForeColor = System.Drawing.Color.White
        Me.BtnSave.Location = New System.Drawing.Point(269, 230)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(80, 24)
        Me.BtnSave.TabIndex = 6
        Me.BtnSave.Text = "Guardar"
        Me.BtnSave.UseVisualStyleBackColor = False
        '
        'Panel
        '
        Me.AutoScroll = True
        Me.Controls.Add(Me.ZLabel1)
        Me.Controls.Add(Me.CheckBox3)
        Me.Controls.Add(Me.CheckBox2)
        Me.Controls.Add(Me.btnSave2)
        Me.Controls.Add(Me.btnRemove)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtdesc)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cbColors)
        Me.Controls.Add(Me.rdbMonth)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.rdbDay)
        Me.Controls.Add(Me.txtexpire)
        Me.Controls.Add(Me.lstNumbersTypesAndColors)
        Me.Controls.Add(Me.txtmaxdocs)
        Me.Controls.Add(Me.lblPrueba)
        Me.Controls.Add(Me.cbNumbers)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.BtnSave)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtDescription)
        Me.Controls.Add(Me.TxtName)
        Me.Controls.Add(Me.txtId)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.btnEditDescription)
        Me.Controls.Add(Me.BtnInitial)
        Me.Controls.Add(Me.BtnDel)
        Me.Controls.Add(Me.BtnEdit)
        Me.Controls.Add(Me.BtnAdd)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.ListView1)
        Me.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        '
        'CheckBox3
        '
        Me.CheckBox3.BackColor = System.Drawing.Color.Transparent
        Me.CheckBox3.Location = New System.Drawing.Point(12, 222)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(100, 64)
        Me.CheckBox3.TabIndex = 111
        Me.CheckBox3.Text = "No asignar a usuario al iniciar tarea si el usuario pertenece a otro grupo"
        Me.CheckBox3.UseVisualStyleBackColor = False
        '
        'CheckBox2
        '
        Me.CheckBox2.BackColor = System.Drawing.Color.Transparent
        Me.CheckBox2.Location = New System.Drawing.Point(12, 196)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(100, 64)
        Me.CheckBox2.TabIndex = 110
        Me.CheckBox2.Text = "No asignar a usuario al iniciar tarea asignada a grupo de usuarios"
        Me.CheckBox2.UseVisualStyleBackColor = False
        '
        'btnSave2
        '
        Me.btnSave2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnSave2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave2.ForeColor = System.Drawing.Color.White
        Me.btnSave2.Location = New System.Drawing.Point(227, 676)
        Me.btnSave2.Name = "btnSave2"
        Me.btnSave2.Size = New System.Drawing.Size(80, 24)
        Me.btnSave2.TabIndex = 109
        Me.btnSave2.Text = "Guardar"
        Me.btnSave2.UseVisualStyleBackColor = False
        '
        'btnRemove
        '
        Me.btnRemove.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemove.ForeColor = System.Drawing.Color.White
        Me.btnRemove.Location = New System.Drawing.Point(227, 646)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(80, 24)
        Me.btnRemove.TabIndex = 108
        Me.btnRemove.Text = "Remover"
        Me.btnRemove.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.FontSize = 9.75!
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(9, 561)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(235, 16)
        Me.Label4.TabIndex = 106
        Me.Label4.Text = "vencido la tarea, el color debe ser"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtdesc
        '
        Me.txtdesc.BackColor = System.Drawing.Color.White
        Me.txtdesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtdesc.Location = New System.Drawing.Point(9, 426)
        Me.txtdesc.Name = "txtdesc"
        Me.txtdesc.ReadOnly = True
        Me.txtdesc.Size = New System.Drawing.Size(343, 23)
        Me.txtdesc.TabIndex = 12
        Me.txtdesc.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.FontSize = 9.75!
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(9, 155)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(23, 24)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Id"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbColors
        '
        Me.cbColors.Enabled = False
        Me.cbColors.FormattingEnabled = True
        Me.cbColors.Location = New System.Drawing.Point(249, 558)
        Me.cbColors.Name = "cbColors"
        Me.cbColors.Size = New System.Drawing.Size(100, 24)
        Me.cbColors.TabIndex = 104
        '
        'rdbMonth
        '
        Me.rdbMonth.AutoSize = True
        Me.rdbMonth.Enabled = False
        Me.rdbMonth.Location = New System.Drawing.Point(204, 532)
        Me.rdbMonth.Name = "rdbMonth"
        Me.rdbMonth.Size = New System.Drawing.Size(73, 20)
        Me.rdbMonth.TabIndex = 103
        Me.rdbMonth.TabStop = True
        Me.rdbMonth.Text = "mes/es"
        Me.rdbMonth.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.BackColor = System.Drawing.Color.Transparent
        Me.CheckBox1.Location = New System.Drawing.Point(146, 155)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(100, 64)
        Me.CheckBox1.TabIndex = 5
        Me.CheckBox1.Text = "Inicia Tarea al abrir el documento"
        Me.CheckBox1.UseVisualStyleBackColor = False
        '
        'rdbDay
        '
        Me.rdbDay.AutoSize = True
        Me.rdbDay.Enabled = False
        Me.rdbDay.Location = New System.Drawing.Point(144, 532)
        Me.rdbDay.Name = "rdbDay"
        Me.rdbDay.Size = New System.Drawing.Size(58, 20)
        Me.rdbDay.TabIndex = 102
        Me.rdbDay.TabStop = True
        Me.rdbDay.Text = "día/s"
        Me.rdbDay.UseVisualStyleBackColor = True
        '
        'txtexpire
        '
        Me.txtexpire.BackColor = System.Drawing.Color.White
        Me.txtexpire.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtexpire.Location = New System.Drawing.Point(96, 107)
        Me.txtexpire.MaxLength = 0
        Me.txtexpire.Name = "txtexpire"
        Me.txtexpire.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtexpire.Size = New System.Drawing.Size(48, 23)
        Me.txtexpire.TabIndex = 3
        Me.txtexpire.WordWrap = False
        '
        'lstNumbersTypesAndColors
        '
        Me.lstNumbersTypesAndColors.Enabled = False
        Me.lstNumbersTypesAndColors.FormattingEnabled = True
        Me.lstNumbersTypesAndColors.ItemHeight = 16
        Me.lstNumbersTypesAndColors.Location = New System.Drawing.Point(12, 588)
        Me.lstNumbersTypesAndColors.Name = "lstNumbersTypesAndColors"
        Me.lstNumbersTypesAndColors.Size = New System.Drawing.Size(183, 132)
        Me.lstNumbersTypesAndColors.TabIndex = 101
        '
        'txtmaxdocs
        '
        Me.txtmaxdocs.BackColor = System.Drawing.Color.White
        Me.txtmaxdocs.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtmaxdocs.Location = New System.Drawing.Point(288, 104)
        Me.txtmaxdocs.MaxLength = 0
        Me.txtmaxdocs.Name = "txtmaxdocs"
        Me.txtmaxdocs.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtmaxdocs.Size = New System.Drawing.Size(64, 23)
        Me.txtmaxdocs.TabIndex = 4
        Me.txtmaxdocs.WordWrap = False
        '
        'lblPrueba
        '
        Me.lblPrueba.AutoSize = True
        Me.lblPrueba.BackColor = System.Drawing.Color.Transparent
        Me.lblPrueba.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrueba.FontSize = 9.75!
        Me.lblPrueba.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblPrueba.Location = New System.Drawing.Point(9, 530)
        Me.lblPrueba.Name = "lblPrueba"
        Me.lblPrueba.Size = New System.Drawing.Size(84, 16)
        Me.lblPrueba.TabIndex = 100
        Me.lblPrueba.Text = "Después de"
        Me.lblPrueba.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbNumbers
        '
        Me.cbNumbers.FormattingEnabled = True
        Me.cbNumbers.Location = New System.Drawing.Point(93, 528)
        Me.cbNumbers.Name = "cbNumbers"
        Me.cbNumbers.Size = New System.Drawing.Size(45, 24)
        Me.cbNumbers.TabIndex = 99
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.FontSize = 9.75!
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label7.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label7.Location = New System.Drawing.Point(169, 104)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(113, 32)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Cupo de Documentos"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.FontSize = 9.75!
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label6.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label6.Location = New System.Drawing.Point(9, 107)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(72, 32)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Vencimiento en Horas"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.FontSize = 9.75!
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(9, 72)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(48, 24)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Ayuda"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.White
        Me.TextBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TextBox1.Location = New System.Drawing.Point(96, 73)
        Me.TextBox1.MaxLength = 0
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TextBox1.Size = New System.Drawing.Size(254, 23)
        Me.TextBox1.TabIndex = 2
        Me.TextBox1.WordWrap = False
        '
        'btnEditDescription
        '
        Me.btnEditDescription.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnEditDescription.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEditDescription.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.btnEditDescription.ForeColor = System.Drawing.Color.White
        Me.btnEditDescription.Location = New System.Drawing.Point(169, 477)
        Me.btnEditDescription.Name = "btnEditDescription"
        Me.btnEditDescription.Size = New System.Drawing.Size(183, 24)
        Me.btnEditDescription.TabIndex = 5
        Me.btnEditDescription.Text = "Cambiar Descripcion"
        Me.btnEditDescription.UseVisualStyleBackColor = False
        '
        'BtnInitial
        '
        Me.BtnInitial.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BtnInitial.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnInitial.ForeColor = System.Drawing.Color.White
        Me.BtnInitial.Location = New System.Drawing.Point(242, 453)
        Me.BtnInitial.Name = "BtnInitial"
        Me.BtnInitial.Size = New System.Drawing.Size(110, 24)
        Me.BtnInitial.TabIndex = 4
        Me.BtnInitial.Text = " Inicial"
        Me.BtnInitial.UseVisualStyleBackColor = False
        '
        'BtnDel
        '
        Me.BtnDel.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BtnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnDel.ForeColor = System.Drawing.Color.White
        Me.BtnDel.Location = New System.Drawing.Point(124, 453)
        Me.BtnDel.Name = "BtnDel"
        Me.BtnDel.Size = New System.Drawing.Size(115, 24)
        Me.BtnDel.TabIndex = 3
        Me.BtnDel.Text = "Eliminar"
        Me.BtnDel.UseVisualStyleBackColor = False
        '
        'BtnEdit
        '
        Me.BtnEdit.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BtnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnEdit.ForeColor = System.Drawing.Color.White
        Me.BtnEdit.Location = New System.Drawing.Point(9, 477)
        Me.BtnEdit.Name = "BtnEdit"
        Me.BtnEdit.Size = New System.Drawing.Size(158, 24)
        Me.BtnEdit.TabIndex = 2
        Me.BtnEdit.Text = "Cambiar Nombre"
        Me.BtnEdit.UseVisualStyleBackColor = False
        '
        'BtnAdd
        '
        Me.BtnAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BtnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAdd.ForeColor = System.Drawing.Color.White
        Me.BtnAdd.Location = New System.Drawing.Point(9, 453)
        Me.BtnAdd.Name = "BtnAdd"
        Me.BtnAdd.Size = New System.Drawing.Size(112, 24)
        Me.BtnAdd.TabIndex = 1
        Me.BtnAdd.Text = "Agregar"
        Me.BtnAdd.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.FontSize = 9.75!
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(6, 271)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(343, 23)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Estados"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ListView1
        '
        Me.ListView1.BackColor = System.Drawing.Color.White
        Me.ListView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.ListView1.Location = New System.Drawing.Point(9, 297)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(343, 121)
        Me.ListView1.TabIndex = 2
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.List
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = ""
        Me.ColumnHeader1.Width = 343
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "")
        Me.ImageList1.Images.SetKeyName(1, "")
        Me.ImageList1.Images.SetKeyName(2, "")
        Me.ImageList1.Images.SetKeyName(3, "")
        Me.ImageList1.Images.SetKeyName(4, "")
        Me.ImageList1.Images.SetKeyName(5, "")
        Me.ImageList1.Images.SetKeyName(6, "")
        Me.ImageList1.Images.SetKeyName(7, "")
        Me.ImageList1.Images.SetKeyName(8, "")
        Me.ImageList1.Images.SetKeyName(9, "")
        Me.ImageList1.Images.SetKeyName(10, "")
        Me.ImageList1.Images.SetKeyName(11, "")
        Me.ImageList1.Images.SetKeyName(12, "")
        '
        'PanelTop
        '
        Me.PanelTop.BackColor = System.Drawing.Color.White
        Me.PanelTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelTop.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PanelTop.FontSize = 12.0!
        Me.PanelTop.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.PanelTop.Location = New System.Drawing.Point(0, 0)
        Me.PanelTop.Name = "PanelTop"
        Me.PanelTop.Size = New System.Drawing.Size(507, 32)
        Me.PanelTop.TabIndex = 98
        Me.PanelTop.Text = "  Etapa"
        Me.PanelTop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ZLabel1
        '
        Me.ZLabel1.AutoSize = True
        Me.ZLabel1.BackColor = System.Drawing.Color.Transparent
        Me.ZLabel1.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.ZLabel1.FontSize = 9.75!
        Me.ZLabel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.ZLabel1.Location = New System.Drawing.Point(283, 534)
        Me.ZLabel1.Name = "ZLabel1"
        Me.ZLabel1.Size = New System.Drawing.Size(66, 16)
        Me.ZLabel1.TabIndex = 112
        Me.ZLabel1.Text = "de haber"
        Me.ZLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'UCStepCtrl
        '
        Me.AutoScroll = True
        Me.Controls.Add(Me.PanelTop)
        Me.Name = "UCStepCtrl"
        Me.Size = New System.Drawing.Size(607, 779)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim WFStep As WFStep

    Public Sub New(ByRef WFStep As WFStep, ByVal isreadonly As Boolean)
        Me.New()
        RefreshStep(WFStep, isreadonly)
    End Sub

    ''' <summary>
    ''' Método que muestra los datos de una etapa
    ''' </summary>
    ''' <param name="WFStep"></param>
    ''' <param name="isreadonly"></param>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Gaston]    29/09/2008  Modified        Llamada al método loadNumbersAndColors
    ''' </history>
    Public Sub RefreshStep(ByRef WFStep As WFStep, ByVal isreadonly As Boolean)

        Me.WFStep = WFStep
        TxtName.Text = WFStep.Name
        TextBox1.Text = WFStep.Help
        TxtDescription.Text = WFStep.Description
        txtId.Text = WFStep.ID.ToString
        txtexpire.Text = WFStep.MaxHours.ToString()
        txtmaxdocs.Text = WFStep.MaxDocs.ToString()
        CheckBox1.Checked = WFStep.StartAtOpenDoc
        Enabled = isreadonly

        LoadStates()
        loadNumbersAndColors()

        Dim dt As DataTable = WFStepBusiness.getTypesOfPermit(WFStep.ID, False, TypesofPermits.DontAsignTaskAsignedToGroup)

        If dt.Rows.Count > 0 Then
            CheckBox2.Checked = dt.Rows(0)(2)
        Else
            CheckBox2.Checked = False
        End If

        dt = WFStepBusiness.getTypesOfPermit(WFStep.ID, False, TypesofPermits.DontAsignTaskOnInitIfAllowExecuteAllUserTask)

        If dt.Rows.Count > 0 Then
            CheckBox3.Checked = dt.Rows(0)(2)
        Else
            CheckBox3.Checked = False
        End If


    End Sub

    Public Event NameChanged(ByVal Name As String, ByRef wfstep As WFStep)

    Private Sub TxtDescription_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles TxtDescription.TextChanged
        Try
            WFStep.Description = TxtDescription.Text.Trim
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub TxtName_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles TxtName.TextChanged
        Try
            WFStep.Name = TxtName.Text.Trim
            RaiseEvent NameChanged(TxtName.Text.Trim, WFStep)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Event SaveChanges(ByRef wfstep As WFStep)

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnSave.Click
        Try
            WFStepBusiness.removeTypeOfPermit(WFStep.ID, TypesofPermits.DontAsignTaskAsignedToGroup)
            WFStepBusiness.addTypeOfPermit(WFStep.ID, TypesofPermits.DontAsignTaskAsignedToGroup, CheckBox2.Checked, "0")

            WFStepBusiness.removeTypeOfPermit(WFStep.ID, TypesofPermits.DontAsignTaskOnInitIfAllowExecuteAllUserTask)
            WFStepBusiness.addTypeOfPermit(WFStep.ID, TypesofPermits.DontAsignTaskOnInitIfAllowExecuteAllUserTask, CheckBox3.Checked, "0")

            WFStepBusiness.UpdateStep(WFStep)
            MessageBox.Show("Cambios Guardados", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
            RaiseEvent SaveChanges(WFStep)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles TextBox1.TextChanged
        Try
            WFStep.Help = TextBox1.Text
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub txtexpire_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles txtexpire.TextChanged
        Try
            If IsNumeric(txtexpire.Text) Then
                'todo wf step: falta ver que en la base el tipo de dato y todo su tratamiento sea decimal, para poder especificar medias horas
                WFStep.MaxHours = CDec(txtexpire.Text)
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub txtmaxdocs_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles txtmaxdocs.TextChanged
        Try
            WFStep.MaxDocs = Integer.Parse(txtmaxdocs.Text)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles CheckBox1.CheckedChanged
        Try
            WFStep.StartAtOpenDoc = CheckBox1.Checked
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

#Region "Estados"
    Private Sub LoadStates()
        Try
            ListView1.Clear()
            For Each s As WFStepState In WFStep.States
                If Not s.ID = 0 Then
                    ListView1.Items.Add(New LStateItem(s, s.Initial))
                End If
            Next
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Class LStateItem
        Inherits ListViewItem
        Public State As WFStepState
        Public description As String
        Private _Initial As Boolean
        Sub New(ByVal State As WFStepState, ByVal Initial As Boolean)
            Me.State = State
            Me.Initial = Initial
            Text = Me.State.Name + " [" + Me.State.ID.ToString + "]"
        End Sub
        Public WriteOnly Property Initial() As Boolean
            Set(ByVal Value As Boolean)
                _Initial = Value
                If _Initial = True Then
                    ForeColor = Color.Blue
                Else
                    ForeColor = Color.FromArgb(76, 76, 76)
                End If
            End Set
        End Property
    End Class

    ''' <summary>
    ''' Evento que se ejecuta cuando se agrega un estado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Tomás] - 08/05/2009 - Modified - Se realiza una validación en el ingreso del nombre y descripción
    '''                                       del evento para que no se duplique.
    ''' </history>
    Private Sub BtnAdd_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnAdd.Click
        Try
            'Obtiene el nombre
            Dim name As String = InputBox("Ingrese el nombre del Estado", "Nombre del Estado")
            'Valida que los datos sean correctos
            If isValidName(name) Then
                'Ingresa la descripción
                Dim description As String = InputBox("Ingrese la descripción del Estado", "Descripción del Estado")
                'Valida los datos de la descripción
                If isValidDescription(description) Then
                    Dim NewState As New WFStepState(ToolsBusiness.GetNewID(IdTypes.WFSTEPSTATE), name.Trim, description.Trim, False)

                    WFStep.States.Add(NewState)
                    WFStepStatesBusiness.AddState(NewState, WFStep)
                    'Se agrega el estado a la lista
                    ListView1.Items.Add(New LStateItem(NewState, NewState.Initial))
                End If
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se presiona el botón Cambiar Nombre
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	10/06/2008	Modified    Se cambio la llamada al método UpdateState
    ''' 	[Tomás] - 08/05/2009 - Modified - Se realiza una validación en el ingreso del nombre del evento para que no se duplique.
    ''' </history>
    Private Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnEdit.Click
        Try
            'Valida que haya uno seleccionado
            If ListView1.SelectedItems.Count > 0 Then
                'Obtiene el nuevo nombre
                Dim LStateItem As LStateItem = DirectCast(ListView1.SelectedItems(0), LStateItem)
                Dim oldName As String = LStateItem.State.Name
                Dim name As String = InputBox("Modifique el nombre del Estado", "Nombre del Estado", LStateItem.State.Name.Trim)
                'Valida que sea correcto
                If isValidName(name) Then
                    'Guarda y muestra los cambios
                    LStateItem.State.Name = name.Trim
                    WFStepStatesBusiness.UpdateState("Name", LStateItem.State.Name, CInt(LStateItem.State.ID), oldName)
                    LStateItem.Text = name.Trim
                End If
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se presiona el botón Cambiar Descripcion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	10/06/2008	Modified    Se cambio la llamada al método UpdateState y se agrego txtDesc.Text para la actualización
    ''' 	[Tomás] - 08/05/2009 - Modified - Se realiza una validación en el ingreso de la descripción del evento para que no se duplique.
    ''' </history>
    Private Sub btnEditDescription_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnEditDescription.Click
        Try
            'Valida que haya uno seleccionado
            If ListView1.SelectedItems.Count > 0 Then
                'Obtiene la descripcion
                Dim LStateItem As LStateItem = DirectCast(ListView1.SelectedItems(0), LStateItem)
                Dim description As String = InputBox("Modifique la descripción del Estado", "Descripción del Estado", LStateItem.State.Description)
                'Valida que sea correcta
                If isValidDescription(description) AndAlso Not String.Compare(description, LStateItem.State.Description, True) = 0 Then
                    'Guarda y muestra los cambios
                    LStateItem.State.Description = description
                    WFStepStatesBusiness.UpdateState("Description", LStateItem.State.Description, CInt(LStateItem.State.ID), LStateItem.State.Name)
                    txtdesc.Text = description
                End If
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub BtnDel_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnDel.Click
        Try
            If ListView1.SelectedItems.Count > 0 Then
                Dim LStateItem As LStateItem = DirectCast(ListView1.SelectedItems(0), LStateItem)
                WFStepStatesBusiness.RemoveState(LStateItem.State)
                WFStep.States.Remove(LStateItem.State)
                ListView1.Items.Remove(LStateItem)
                txtdesc.Clear()
                If WFStepStatesBusiness.GetInitialStateExistance(WFStep.ID) = 0 Then
                    MsgBox("La etapa se encuentra sin estado inicial. Configure un nuevo estado inicial por favor.", , "ATENCION")
                End If
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub BtnInitial_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnInitial.Click
        If ListView1.SelectedItems.Count > 0 Then
            Try
                If ListView1.SelectedItems.Count > 0 Then
                    Dim LStateItem As LStateItem = DirectCast(ListView1.SelectedItems(0), LStateItem)
                    WFStepStatesBusiness.SetInitialState(LStateItem.State, WFStep)

                    For Each item As LStateItem In ListView1.Items
                        If item Is LStateItem Then
                            item.Initial = CBool(1)
                            item.State.Initial = True
                        Else
                            item.Initial = CBool(0)
                            item.State.Initial = False
                        End If
                    Next
                End If
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Valida que el nombre del estado sea correcto
    ''' </summary>
    ''' <param name="name">Nombre del estado</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Tomas] - 08/05/2009 - Created
    ''' </history>
    Private Function isValidName(ByVal name As String) As Boolean
        'Valido que existan datos
        If String.IsNullOrEmpty(name) Then
            Return False
        End If
        'Valido la capacidad
        If name.Trim.Length = 0 Then
            Return False
        End If
        If name.Trim.Length > 50 Then
            MessageBox.Show("El tamaño máximo permitido para el nombre es de 50 caracteres", "Error de ingreso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If
        'Valido si ya existe en el listbox
        For Each itemName As LStateItem In ListView1.Items
            If String.Compare(name.Trim, itemName.Text.Trim, True) = 0 Then
                MessageBox.Show("El nombre del estado que desea ingresar ya existe.", "Error de ingreso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If
        Next
        Return True
    End Function

    ''' <summary>
    ''' Valida que el nombre de la descripción sea correcto
    '''
    ''' </summary>
    ''' <param name="name">Nombre de la descripción</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Tomas] - 08/05/2009 - Created
    '''     [Sebastian] 21/05/2009 MODIFIED se comento la validacion de esta vacio el comentario.
    ''' </history>
    Private Function isValidDescription(ByVal description As String) As Boolean
        'Valido que existan datos
        If String.IsNullOrEmpty(description) = True Then
            If MessageBox.Show("El campo de desripción está vacio. ¿Desea Continuar?", "Zamba Administrador", MessageBoxButtons.YesNo) = DialogResult.No Then
                Return False
            End If
        End If
        'Valido la capacidad
        If description.Trim.Length > 250 Then
            MessageBox.Show("El tamaño máximo permitido para la descripción es de 250 caracteres", "Error de ingreso", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        Return True
    End Function

#End Region

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles ListView1.SelectedIndexChanged
        Try
            If ListView1.SelectedItems.Count > 0 Then
                Dim LStateItem As LStateItem = DirectCast(ListView1.SelectedItems(0), LStateItem)
                txtdesc.Text = LStateItem.State.Description
                LStateItem = Nothing
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ' -------------------------------------------------------------------------------------------------------------------------------------
    ' -------------------------------------------------------------------------------------------------------------------------------------
    ' Configuración para mostrar el color de una tarea según la cantidad de días o meses que pasaron con respecto a su fecha de vencimiento

    ' [Gaston]    29/09/2008  Created
    ' -------------------------------------------------------------------------------------------------------------------------------------
    ' -------------------------------------------------------------------------------------------------------------------------------------
#Region "Vencimiento de Tareas"

    Private numbersCompleted As New ArrayList

#Region "Eventos"

    ''' <summary>
    ''' Evento que se ejecuta al cambiar el valor del Atributo seleccionado en el combobox Numbers
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cbNumbers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles cbNumbers.SelectedIndexChanged

        If Not IsNothing(cbNumbers.SelectedItem) Then

            For Each elem As NumberTypeAndColor In lstNumbersTypesAndColors.Items

                ' Si el número ya se encuentra en la lista que muestra número, tipo y color
                If (elem.Number = cbNumbers.SelectedItem.ToString()) Then

                    ' Si es tipo día entonces se deshabilita el radioButton "día/s" porque ya se encuentra en la lista
                    If (elem.Type = "día/s") Then
                        rdbDay.Enabled = False
                        rdbMonth.Enabled = True
                    Else
                        rdbDay.Enabled = True
                        rdbMonth.Enabled = False
                    End If

                    Exit Sub

                End If

            Next

            ' Si el elemento no se encuentra en la lista que muestra número, tipo y color entonces habilitar ambos radioButton
            rdbDay.Enabled = True
            rdbMonth.Enabled = True

        End If

    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al hacer click sobre el radioButton "día/s" o "mes/es"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rdbDay_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles rdbDay.CheckedChanged, rdbMonth.CheckedChanged
        cbColors.Enabled = True
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al cambiar el valor del Atributo seleccionado en el combobox Colors
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Gaston]    29/09/2008  Created        
    ''' </history>
    Private Sub cbColors_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles cbColors.SelectedIndexChanged

        If Not (IsNothing(cbColors.SelectedItem)) Then

            Dim dc As NumberTypeAndColor

            If (rdbDay.Checked = True) Then
                dc = New NumberTypeAndColor(cbNumbers.SelectedItem.ToString(), "día/s", cbColors.SelectedItem.ToString())
            Else
                dc = New NumberTypeAndColor(cbNumbers.SelectedItem.ToString(), "mes/es", cbColors.SelectedItem.ToString())
            End If

            lstNumbersTypesAndColors.Items.Add(dc)

            ' Si alguno de los radioButton es distinto a true entonces el número ya está como día y mes. Por lo tanto, se agrega en una colección
            ' el número que ya se encuentra en su totalidad en la lista (día y mes) y elimina del combobox los números 
            If ((rdbDay.Enabled = False) Or (rdbMonth.Enabled = False)) Then
                numbersCompleted.Add(cbNumbers.SelectedItem.ToString())
                cbNumbers.Items.Remove(cbNumbers.SelectedItem)
            End If

            cbColors.Items.Remove(cbColors.SelectedItem.ToString())

            rdbDay.Enabled = False
            rdbMonth.Enabled = False
            rdbDay.Checked = False
            rdbMonth.Checked = False
            cbColors.Enabled = False
            lstNumbersTypesAndColors.Enabled = True

            cbNumbers.SelectedItem = Nothing
            cbColors.SelectedItem = Nothing

            btnSave2.Enabled = True

        End If

    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al cambiar el valor del Atributo seleccionado en el listbox NumbersTypesAndColors
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Gaston]    29/09/2008  Created        
    ''' </history>
    Private Sub lstNumbersTypesAndColors_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles lstNumbersTypesAndColors.SelectedIndexChanged

        If Not IsNothing(lstNumbersTypesAndColors.SelectedItem) Then
            btnRemove.Enabled = True
        End If

    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al hacer un click sobre el botón Remover
    ''' [Sebastian 17-09-09] Modified Remove from Data Base and Hash Table
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Gaston]    29/09/2008  Created        
    ''' </history>
    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnRemove.Click

        If Not IsNothing(lstNumbersTypesAndColors.SelectedItem) Then

            ' Se agrega el color al combobox de colores
            cbColors.Items.Add(CType(lstNumbersTypesAndColors.SelectedItem, NumberTypeAndColor).Color)
            cbColors.Sorted = True

            ' Si el número no se encuentra en el combobox que muestra los números entonces se agrega al correspondiente combobox
            If Not (cbNumbers.Items.Contains(CType(lstNumbersTypesAndColors.SelectedItem, NumberTypeAndColor).Number)) Then

                cbNumbers.Items.Clear()

                ' Se agrega al combobox los números 
                For counter As Integer = 1 To 31
                    cbNumbers.Items.Add(counter)
                Next

                numbersCompleted.Remove(CType(lstNumbersTypesAndColors.SelectedItem, NumberTypeAndColor).Number)

                ' Se eliminan del combobox los números que se encuentran ya en la lista con día y mes
                For Each elem As Integer In numbersCompleted
                    cbNumbers.Items.Remove(elem)
                Next

            End If

            ' Se elimina el elemento seleccionado perteneciente a la lista que muestra número, tipo y color
            lstNumbersTypesAndColors.Items.Remove(lstNumbersTypesAndColors.SelectedItem)
            'Remover color form Data Base
            WFStepBusiness.removeTypeOfPermit(WFStep.ID, TypesofPermits.Expired)
            SyncLock (Cache.Workflows.hsStepsOpt)
                'Remove color from hash
                Cache.Workflows.hsStepsOpt.Remove(WFStep.ID)
            End SyncLock
        End If

        btnRemove.Enabled = False

    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al hacer un click sobre el botón Guardar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Gaston]    29/09/2008  Created        
    ''' </history>
    Private Sub btnSave2_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnSave2.Click

        If (lstNumbersTypesAndColors.Items.Count > 0) Then

            Try

                ' Se remueve de la base de datos, para remover antiguos valores
                WFStepBusiness.removeTypeOfPermit(WFStep.ID, TypesofPermits.Expired)

                For Each elem As NumberTypeAndColor In lstNumbersTypesAndColors.Items

                    Dim numberOfDays As Integer = CType(elem.Number, Integer)

                    If (elem.Type = "mes/es") Then

                        ' Los meses se reemplazan por la cantidad de días que corresponden con un mes de 30 días
                        Select Case numberOfDays
                            Case 1
                                numberOfDays = 30
                            Case 2
                                numberOfDays = 60
                            Case 3
                                numberOfDays = 90
                            Case 4
                                numberOfDays = 120
                            Case 5
                                numberOfDays = 150
                            Case 6
                                numberOfDays = 180
                            Case 7
                                numberOfDays = 210
                            Case 8
                                numberOfDays = 240
                            Case 9
                                numberOfDays = 270
                            Case 10
                                numberOfDays = 300
                            Case 11
                                numberOfDays = 330
                            Case 12
                                numberOfDays = 360
                            Case Else
                                numberOfDays = 400
                        End Select

                    End If

                    ' Se guarda en la base de datos
                    ' Parámetros : Id de etapa || Vencimiento || cantidad de días || color 
                    WFStepBusiness.addTypeOfPermit(WFStep.ID, TypesofPermits.Expired, numberOfDays, elem.Color)

                Next

                RaiseEvent SaveChanges(WFStep)

            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try

        Else
            WFStepBusiness.removeTypeOfPermit(WFStep.ID, TypesofPermits.Expired)
        End If

    End Sub

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Método que sirve para cargar en los respectivos combobox los números y colores
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Gaston]    29/09/2008  Created        
    '''                 23/10/2008  Modified    Se recupera de la base de datos
    ''' </history>
    Private Sub loadNumbersAndColors()

        lstNumbersTypesAndColors.DisplayMember = "NumberTypeAndColor"

        For counter As Integer = 1 To 31
            cbNumbers.Items.Add(counter)
        Next

        cbColors.Items.Add("Rojo")
        cbColors.Items.Add("Verde")
        cbColors.Items.Add("Amarillo")
        cbColors.Items.Add("Azul")
        cbColors.Items.Add("Violeta")
        cbColors.Items.Add("Gris")

        cbColors.Sorted = True

        Dim dt As DataTable = WFStepBusiness.getTypesOfPermit(WFStep.ID, False, TypesofPermits.Expired)
        Dim ban As Boolean = False

        If Not (IsNothing(dt)) Then

            Dim temp As New ArrayList

            If (dt.Rows.Count > 0) Then

                For Each dr As DataRow In dt.Rows

                    ban = False

                    Select Case (dr("ObjTwo").ToString())
                        Case "30"
                            dr("ObjTwo") = "1"
                        Case "60"
                            dr("ObjTwo") = "2"
                        Case "90"
                            dr("ObjTwo") = "3"
                        Case "120"
                            dr("ObjTwo") = "4"
                        Case "150"
                            dr("ObjTwo") = "5"
                        Case "180"
                            dr("ObjTwo") = "6"
                        Case "210"
                            dr("ObjTwo") = "7"
                        Case "240"
                            dr("ObjTwo") = "8"
                        Case "270"
                            dr("ObjTwo") = "9"
                        Case "300"
                            dr("ObjTwo") = "10"
                        Case "330"
                            dr("ObjTwo") = "11"
                        Case "360"
                            dr("ObjTwo") = "12"
                        Case Else
                            Dim dcDay As New NumberTypeAndColor(dr("ObjTwo").ToString(), "día/s", dr("ObjExtraData").ToString())
                            lstNumbersTypesAndColors.Items.Add(dcDay)
                            ban = True
                    End Select

                    If (ban = False) Then
                        Dim dcMonth As New NumberTypeAndColor(dr("ObjTwo").ToString(), "mes/es", dr("ObjExtraData").ToString())
                        lstNumbersTypesAndColors.Items.Add(dcMonth)
                    End If

                    cbColors.Items.Remove(dr("ObjExtraData").ToString())

                    ' Si el número no se encuentra en la colección entonces se agrega
                    If Not (temp.Contains(dr("ObjTwo").ToString())) Then
                        temp.Add(dr("ObjTwo").ToString())
                        ' de lo contrario si ya se encuentra entonces significa que el mismo número esta como día y mes, y por lo tanto
                        ' hay que eliminarlo del combobox Numbers
                    Else
                        numbersCompleted.Add(dr("ObjTwo").ToString())
                    End If

                Next

                If (numbersCompleted.Count > 0) Then
                    ' Se eliminan del combobox los números que se encuentran ya en la lista con día y mes
                    For Each elem As Integer In numbersCompleted
                        cbNumbers.Items.Remove(elem)
                    Next
                End If

                dt.Dispose()
                temp.Clear()
                lstNumbersTypesAndColors.Enabled = True
                btnRemove.Enabled = True

            End If

        End If

    End Sub

#End Region

#Region "Clase NumberTypeAndColor"

    Private Class NumberTypeAndColor

        Private m_number, m_color, m_type As String

        ' Contructor
        Public Sub New(ByVal number As String, ByVal type As String, ByVal color As String)
            m_number = number
            m_type = type
            m_color = color
        End Sub

        ' Propiedades
        Public ReadOnly Property NumberTypeAndColor() As String

            Get

                If (m_type.Contains("día/s")) Then

                    If (m_number.Length = 1) Then
                        Return (m_number & "    " & m_type & "         " & m_color)
                    Else
                        Return (m_number & "  " & m_type & "         " & m_color)
                    End If

                Else

                    If (m_number.Length = 1) Then
                        Return (m_number & "    " & m_type & "     " & m_color)
                    Else
                        Return (m_number & "  " & m_type & "     " & m_color)
                    End If

                End If

            End Get

        End Property

        Public Property Number() As String

            Get
                Return (m_number)
            End Get

            Set(ByVal value As String)
                m_number = value
            End Set

        End Property

        Public ReadOnly Property Type() As String

            Get
                Return (m_type)
            End Get

        End Property

        Public ReadOnly Property Color() As String

            Get
                Return (m_color)
            End Get

        End Property

    End Class

    Private Sub ZLabel1_Click(sender As Object, e As EventArgs) Handles ZLabel1.Click

    End Sub

#End Region

#End Region
End Class