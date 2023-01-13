<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UCDoSignPDF
    Inherits Zamba.WFUserControl.ZRuleControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Overloads Sub InitializeComponent()
        Me.contact = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.label11 = New Zamba.AppBlock.ZLabel()
        Me.reason = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.groupBox4 = New System.Windows.Forms.GroupBox()
        Me.writePDF = New System.Windows.Forms.CheckBox()
        Me.label13 = New Zamba.AppBlock.ZLabel()
        Me.location = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.label12 = New Zamba.AppBlock.ZLabel()
        Me.btn_Aceptar = New Zamba.AppBlock.ZButton()
        Me.DebugBox = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.groupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label17 = New Zamba.AppBlock.ZLabel()
        Me.button5 = New Zamba.AppBlock.ZButton()
        Me.fullPath = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.Label5 = New Zamba.AppBlock.ZLabel()
        Me.button4 = New Zamba.AppBlock.ZButton()
        Me.fileName = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.label9 = New Zamba.AppBlock.ZLabel()
        Me.label8 = New Zamba.AppBlock.ZLabel()
        Me.label7 = New Zamba.AppBlock.ZLabel()
        Me.label6 = New Zamba.AppBlock.ZLabel()
        Me.Label10 = New Zamba.AppBlock.ZLabel()
        Me.Label14 = New Zamba.AppBlock.ZLabel()
        Me.producer = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.creator = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.keywords = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.subject = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.title = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.author = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.button2 = New Zamba.AppBlock.ZButton()
        Me.Label15 = New Zamba.AppBlock.ZLabel()
        Me.password = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.groupBox3 = New System.Windows.Forms.GroupBox()
        Me.lblCert = New Zamba.AppBlock.ZLabel()
        Me.certificate = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.Label16 = New Zamba.AppBlock.ZLabel()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.groupBox4.SuspendLayout()
        Me.groupBox2.SuspendLayout()
        Me.groupBox1.SuspendLayout()
        Me.groupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.groupBox4)
        Me.tbRule.Controls.Add(Me.DebugBox)
        Me.tbRule.Controls.Add(Me.groupBox2)
        Me.tbRule.Controls.Add(Me.groupBox1)
        Me.tbRule.Controls.Add(Me.groupBox3)
        Me.tbRule.Size = New System.Drawing.Size(873, 629)
        '
        'tbctrMain
        '
        Me.tbctrMain.Size = New System.Drawing.Size(881, 658)
        '
        'contact
        '
        Me.contact.Location = New System.Drawing.Point(94, 56)
        Me.contact.Name = "contact"
        Me.contact.Size = New System.Drawing.Size(353, 21)
        Me.contact.TabIndex = 30
        Me.contact.Text = ""
        '
        'label11
        '
        Me.label11.AutoSize = True
        Me.label11.BackColor = System.Drawing.Color.Transparent
        Me.label11.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.label11.Location = New System.Drawing.Point(15, 37)
        Me.label11.Name = "label11"
        Me.label11.Size = New System.Drawing.Size(52, 16)
        Me.label11.TabIndex = 29
        Me.label11.Text = "Motivo"
        Me.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'reason
        '
        Me.reason.Location = New System.Drawing.Point(94, 30)
        Me.reason.Name = "reason"
        Me.reason.Size = New System.Drawing.Size(353, 21)
        Me.reason.TabIndex = 28
        Me.reason.Text = ""
        '
        'groupBox4
        '
        Me.groupBox4.Controls.Add(Me.writePDF)
        Me.groupBox4.Controls.Add(Me.label13)
        Me.groupBox4.Controls.Add(Me.location)
        Me.groupBox4.Controls.Add(Me.label12)
        Me.groupBox4.Controls.Add(Me.contact)
        Me.groupBox4.Controls.Add(Me.label11)
        Me.groupBox4.Controls.Add(Me.reason)
        Me.groupBox4.Controls.Add(Me.btn_Aceptar)
        Me.groupBox4.Location = New System.Drawing.Point(389, 207)
        Me.groupBox4.Name = "groupBox4"
        Me.groupBox4.Size = New System.Drawing.Size(453, 182)
        Me.groupBox4.TabIndex = 47
        Me.groupBox4.TabStop = False
        Me.groupBox4.Text = "4- Firma"
        '
        'writePDF
        '
        Me.writePDF.AutoSize = True
        Me.writePDF.Checked = True
        Me.writePDF.CheckState = System.Windows.Forms.CheckState.Checked
        Me.writePDF.Location = New System.Drawing.Point(189, 108)
        Me.writePDF.Name = "writePDF"
        Me.writePDF.Size = New System.Drawing.Size(213, 20)
        Me.writePDF.TabIndex = 34
        Me.writePDF.Text = "Dejar firma en hojas del PDF"
        Me.writePDF.UseVisualStyleBackColor = True
        '
        'label13
        '
        Me.label13.AutoSize = True
        Me.label13.BackColor = System.Drawing.Color.Transparent
        Me.label13.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.label13.Location = New System.Drawing.Point(15, 89)
        Me.label13.Name = "label13"
        Me.label13.Size = New System.Drawing.Size(44, 16)
        Me.label13.TabIndex = 33
        Me.label13.Text = "Lugar"
        Me.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'location
        '
        Me.location.Location = New System.Drawing.Point(94, 82)
        Me.location.Name = "location"
        Me.location.Size = New System.Drawing.Size(353, 21)
        Me.location.TabIndex = 32
        Me.location.Text = ""
        '
        'label12
        '
        Me.label12.AutoSize = True
        Me.label12.BackColor = System.Drawing.Color.Transparent
        Me.label12.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.label12.Location = New System.Drawing.Point(15, 63)
        Me.label12.Name = "label12"
        Me.label12.Size = New System.Drawing.Size(69, 16)
        Me.label12.TabIndex = 31
        Me.label12.Text = "Contacto"
        Me.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btn_Aceptar
        '
        Me.btn_Aceptar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btn_Aceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Aceptar.ForeColor = System.Drawing.Color.White
        Me.btn_Aceptar.Location = New System.Drawing.Point(253, 145)
        Me.btn_Aceptar.Name = "btn_Aceptar"
        Me.btn_Aceptar.Size = New System.Drawing.Size(126, 31)
        Me.btn_Aceptar.TabIndex = 27
        Me.btn_Aceptar.Text = "Guardar"
        Me.btn_Aceptar.UseVisualStyleBackColor = True
        '
        'DebugBox
        '
        Me.DebugBox.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DebugBox.Location = New System.Drawing.Point(8, 395)
        Me.DebugBox.Name = "DebugBox"
        Me.DebugBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.DebugBox.Size = New System.Drawing.Size(828, 109)
        Me.DebugBox.TabIndex = 45
        Me.DebugBox.Text = ""
        Me.DebugBox.Visible = False
        '
        'groupBox2
        '
        Me.groupBox2.Controls.Add(Me.Label17)
        Me.groupBox2.Controls.Add(Me.button5)
        Me.groupBox2.Controls.Add(Me.fullPath)
        Me.groupBox2.Controls.Add(Me.Label5)
        Me.groupBox2.Controls.Add(Me.button4)
        Me.groupBox2.Controls.Add(Me.fileName)
        Me.groupBox2.Location = New System.Drawing.Point(8, 24)
        Me.groupBox2.Name = "groupBox2"
        Me.groupBox2.Size = New System.Drawing.Size(375, 177)
        Me.groupBox2.TabIndex = 44
        Me.groupBox2.TabStop = False
        Me.groupBox2.Text = "1- Documento PDF"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label17.Location = New System.Drawing.Point(7, 95)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(57, 16)
        Me.Label17.TabIndex = 18
        Me.Label17.Text = "Destino"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'button5
        '
        Me.button5.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.button5.ForeColor = System.Drawing.Color.White
        Me.button5.Location = New System.Drawing.Point(272, 92)
        Me.button5.Name = "button5"
        Me.button5.Size = New System.Drawing.Size(74, 20)
        Me.button5.TabIndex = 13
        Me.button5.Text = "Seleccionar"
        Me.button5.UseVisualStyleBackColor = True
        '
        'fullPath
        '
        Me.fullPath.Location = New System.Drawing.Point(60, 51)
        Me.fullPath.Name = "fullPath"
        Me.fullPath.Size = New System.Drawing.Size(201, 21)
        Me.fullPath.TabIndex = 12
        Me.fullPath.Text = ""
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(7, 58)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(71, 16)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "Ubicación"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'button4
        '
        Me.button4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.button4.ForeColor = System.Drawing.Color.White
        Me.button4.Location = New System.Drawing.Point(272, 51)
        Me.button4.Name = "button4"
        Me.button4.Size = New System.Drawing.Size(74, 20)
        Me.button4.TabIndex = 14
        Me.button4.Text = "Seleccionar"
        Me.button4.UseVisualStyleBackColor = True
        '
        'fileName
        '
        Me.fileName.Location = New System.Drawing.Point(60, 92)
        Me.fileName.Name = "fileName"
        Me.fileName.Size = New System.Drawing.Size(201, 21)
        Me.fileName.TabIndex = 17
        Me.fileName.Text = ""
        '
        'label9
        '
        Me.label9.AutoSize = True
        Me.label9.BackColor = System.Drawing.Color.Transparent
        Me.label9.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.label9.Location = New System.Drawing.Point(15, 155)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(66, 16)
        Me.label9.TabIndex = 7
        Me.label9.Text = "Producer"
        Me.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label8
        '
        Me.label8.AutoSize = True
        Me.label8.BackColor = System.Drawing.Color.Transparent
        Me.label8.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.label8.Location = New System.Drawing.Point(24, 129)
        Me.label8.Name = "label8"
        Me.label8.Size = New System.Drawing.Size(57, 16)
        Me.label8.TabIndex = 7
        Me.label8.Text = "Creator"
        Me.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label7
        '
        Me.label7.AutoSize = True
        Me.label7.BackColor = System.Drawing.Color.Transparent
        Me.label7.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.label7.Location = New System.Drawing.Point(13, 103)
        Me.label7.Name = "label7"
        Me.label7.Size = New System.Drawing.Size(71, 16)
        Me.label7.TabIndex = 7
        Me.label7.Text = "Keywords"
        Me.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label6
        '
        Me.label6.AutoSize = True
        Me.label6.BackColor = System.Drawing.Color.Transparent
        Me.label6.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.label6.Location = New System.Drawing.Point(22, 77)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(59, 16)
        Me.label6.TabIndex = 6
        Me.label6.Text = "Subject"
        Me.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(38, 51)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(37, 16)
        Me.Label10.TabIndex = 5
        Me.Label10.Text = "Title"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label14.Location = New System.Drawing.Point(28, 26)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(52, 16)
        Me.Label14.TabIndex = 4
        Me.Label14.Text = "Author"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'producer
        '
        Me.producer.Location = New System.Drawing.Point(93, 148)
        Me.producer.Name = "producer"
        Me.producer.Size = New System.Drawing.Size(354, 21)
        Me.producer.TabIndex = 3
        Me.producer.Text = ""
        '
        'creator
        '
        Me.creator.Location = New System.Drawing.Point(93, 122)
        Me.creator.Name = "creator"
        Me.creator.Size = New System.Drawing.Size(354, 21)
        Me.creator.TabIndex = 3
        Me.creator.Text = ""
        '
        'groupBox1
        '
        Me.groupBox1.Controls.Add(Me.label9)
        Me.groupBox1.Controls.Add(Me.label8)
        Me.groupBox1.Controls.Add(Me.label7)
        Me.groupBox1.Controls.Add(Me.label6)
        Me.groupBox1.Controls.Add(Me.Label10)
        Me.groupBox1.Controls.Add(Me.Label14)
        Me.groupBox1.Controls.Add(Me.producer)
        Me.groupBox1.Controls.Add(Me.creator)
        Me.groupBox1.Controls.Add(Me.keywords)
        Me.groupBox1.Controls.Add(Me.subject)
        Me.groupBox1.Controls.Add(Me.title)
        Me.groupBox1.Controls.Add(Me.author)
        Me.groupBox1.Location = New System.Drawing.Point(389, 24)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(453, 177)
        Me.groupBox1.TabIndex = 43
        Me.groupBox1.TabStop = False
        Me.groupBox1.Text = "2- PDF MetaData (Información de archivo)"
        '
        'keywords
        '
        Me.keywords.Location = New System.Drawing.Point(93, 96)
        Me.keywords.Name = "keywords"
        Me.keywords.Size = New System.Drawing.Size(354, 21)
        Me.keywords.TabIndex = 3
        Me.keywords.Text = ""
        '
        'subject
        '
        Me.subject.Location = New System.Drawing.Point(93, 70)
        Me.subject.Name = "subject"
        Me.subject.Size = New System.Drawing.Size(354, 21)
        Me.subject.TabIndex = 2
        Me.subject.Text = ""
        '
        'title
        '
        Me.title.Location = New System.Drawing.Point(93, 44)
        Me.title.Name = "title"
        Me.title.Size = New System.Drawing.Size(354, 21)
        Me.title.TabIndex = 1
        Me.title.Text = ""
        '
        'author
        '
        Me.author.Location = New System.Drawing.Point(93, 19)
        Me.author.Name = "author"
        Me.author.Size = New System.Drawing.Size(354, 21)
        Me.author.TabIndex = 0
        Me.author.Text = ""
        '
        'button2
        '
        Me.button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.button2.ForeColor = System.Drawing.Color.White
        Me.button2.Location = New System.Drawing.Point(284, 39)
        Me.button2.Name = "button2"
        Me.button2.Size = New System.Drawing.Size(74, 20)
        Me.button2.TabIndex = 22
        Me.button2.Text = "Seleccionar"
        Me.button2.UseVisualStyleBackColor = True
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label15.Location = New System.Drawing.Point(12, 80)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(83, 16)
        Me.Label15.TabIndex = 23
        Me.Label15.Text = "Contraseña"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'password
        '
        Me.password.Location = New System.Drawing.Point(97, 80)
        Me.password.Name = "password"
        Me.password.Size = New System.Drawing.Size(181, 21)
        Me.password.TabIndex = 25
        Me.password.Text = ""
        '
        'groupBox3
        '
        Me.groupBox3.Controls.Add(Me.lblCert)
        Me.groupBox3.Controls.Add(Me.button2)
        Me.groupBox3.Controls.Add(Me.certificate)
        Me.groupBox3.Controls.Add(Me.Label15)
        Me.groupBox3.Controls.Add(Me.password)
        Me.groupBox3.Controls.Add(Me.Label16)
        Me.groupBox3.Location = New System.Drawing.Point(8, 207)
        Me.groupBox3.Name = "groupBox3"
        Me.groupBox3.Size = New System.Drawing.Size(375, 182)
        Me.groupBox3.TabIndex = 46
        Me.groupBox3.TabStop = False
        Me.groupBox3.Text = "3- Certificado"
        '
        'lblCert
        '
        Me.lblCert.AutoSize = True
        Me.lblCert.BackColor = System.Drawing.Color.Transparent
        Me.lblCert.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCert.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblCert.Location = New System.Drawing.Point(124, 17)
        Me.lblCert.Name = "lblCert"
        Me.lblCert.Size = New System.Drawing.Size(15, 16)
        Me.lblCert.TabIndex = 26
        Me.lblCert.Text = "-"
        Me.lblCert.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'certificate
        '
        Me.certificate.Location = New System.Drawing.Point(97, 39)
        Me.certificate.Name = "certificate"
        Me.certificate.Size = New System.Drawing.Size(181, 21)
        Me.certificate.TabIndex = 21
        Me.certificate.Text = ""
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label16.Location = New System.Drawing.Point(12, 46)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(108, 16)
        Me.Label16.TabIndex = 24
        Me.Label16.Text = "Certificado S/N"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'UCDoSignPDF
        '
        Me.Name = "UCDoSignPDF"
        Me.Size = New System.Drawing.Size(881, 658)
        Me.tbRule.ResumeLayout(False)
        Me.tbctrMain.ResumeLayout(False)
        Me.groupBox4.ResumeLayout(False)
        Me.groupBox4.PerformLayout()
        Me.groupBox2.ResumeLayout(False)
        Me.groupBox2.PerformLayout()
        Me.groupBox1.ResumeLayout(False)
        Me.groupBox1.PerformLayout()
        Me.groupBox3.ResumeLayout(False)
        Me.groupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents txtMensaje As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents lblSecondaryValue As ZLabel
    Friend WithEvents txtVariable As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents btnAceptar As ZButton
    Friend WithEvents Label3 As ZLabel
    Friend WithEvents txtValorPorDefecto As Zamba.AppBlock.TextoInteligenteTextBox
    Private WithEvents groupBox4 As GroupBox
    Private WithEvents writePDF As CheckBox
    Private WithEvents location As Zamba.AppBlock.TextoInteligenteTextBox
    Private WithEvents contact As Zamba.AppBlock.TextoInteligenteTextBox
    Private WithEvents reason As Zamba.AppBlock.TextoInteligenteTextBox
    Private WithEvents DebugBox As Zamba.AppBlock.TextoInteligenteTextBox
    Private WithEvents groupBox2 As GroupBox
    Private WithEvents fullPath As Zamba.AppBlock.TextoInteligenteTextBox
    Private WithEvents fileName As Zamba.AppBlock.TextoInteligenteTextBox
    Private WithEvents groupBox1 As GroupBox
    Private WithEvents producer As Zamba.AppBlock.TextoInteligenteTextBox
    Private WithEvents creator As Zamba.AppBlock.TextoInteligenteTextBox
    Private WithEvents keywords As Zamba.AppBlock.TextoInteligenteTextBox
    Private WithEvents subject As Zamba.AppBlock.TextoInteligenteTextBox
    Private WithEvents title As Zamba.AppBlock.TextoInteligenteTextBox
    Private WithEvents author As Zamba.AppBlock.TextoInteligenteTextBox
    Private WithEvents groupBox3 As GroupBox
    Private WithEvents certificate As Zamba.AppBlock.TextoInteligenteTextBox
    Private WithEvents password As Zamba.AppBlock.TextoInteligenteTextBox
    Private WithEvents label13 As ZLabel
    Private WithEvents label12 As ZLabel
    Private WithEvents label11 As ZLabel
    Private WithEvents btn_Aceptar As ZButton
    Private WithEvents button5 As ZButton
    Private WithEvents Label5 As ZLabel
    Private WithEvents button4 As ZButton
    Private WithEvents label9 As ZLabel
    Private WithEvents label8 As ZLabel
    Private WithEvents label7 As ZLabel
    Private WithEvents label6 As ZLabel
    Private WithEvents Label10 As ZLabel
    Private WithEvents Label14 As ZLabel
    Private WithEvents button2 As ZButton
    Private WithEvents Label15 As ZLabel
    Private WithEvents Label16 As ZLabel
    Private WithEvents Label17 As ZLabel
    Private WithEvents lblCert As ZLabel
End Class
