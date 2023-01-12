<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UCDoDismemberObject
    Inherits Zamba.WFUserControl.ZRuleControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Shadows Sub InitializeComponent()
        Me.btPath = New Zamba.AppBlock.ZButton()
        Me.tbPath = New System.Windows.Forms.TextBox()
        Me.btSave = New Zamba.AppBlock.ZButton()
        Me.lbPropertyValue = New Zamba.AppBlock.ZLabel()
        Me.lbPropertyType = New Zamba.AppBlock.ZLabel()
        Me.lsbClasses = New System.Windows.Forms.ListBox()
        Me.lsbProperties = New System.Windows.Forms.ListBox()
        Me.Label2 = New Zamba.AppBlock.ZLabel()
        Me.tbObjectName = New System.Windows.Forms.TextBox()
        Me.lsbValues = New System.Windows.Forms.ListBox()
        Me.btDelete = New Zamba.AppBlock.ZButton()
        Me.btAdd = New Zamba.AppBlock.ZButton()
        Me.btDeleteAll = New Zamba.AppBlock.ZButton()
        Me.lbPropertiesList = New Zamba.AppBlock.ZLabel()
        Me.lbClassList = New Zamba.AppBlock.ZLabel()
        Me.Label4 = New Zamba.AppBlock.ZLabel()
        Me.Label5 = New Zamba.AppBlock.ZLabel()
        Me.tbPropertyValue = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.btnEdit = New Zamba.AppBlock.ZButton()
        Me.lblType = New Zamba.AppBlock.ZLabel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.ZPanel1 = New Zamba.AppBlock.ZPanel()
        Me.ZPanel2 = New Zamba.AppBlock.ZPanel()
        Me.tbRule.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.ZPanel1.SuspendLayout()
        Me.ZPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbState
        '
        Me.tbState.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbState.Size = New System.Drawing.Size(824, 781)
        '
        'tbHabilitation
        '
        Me.tbHabilitation.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbHabilitation.Size = New System.Drawing.Size(824, 781)
        '
        'tbConfiguration
        '
        Me.tbConfiguration.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbConfiguration.Size = New System.Drawing.Size(824, 781)
        '
        'tbAlerts
        '
        Me.tbAlerts.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbAlerts.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbAlerts.Size = New System.Drawing.Size(824, 781)
        '
        'tbRule
        '
        Me.tbRule.Controls.Add(Me.lsbValues)
        Me.tbRule.Controls.Add(Me.Label5)
        Me.tbRule.Controls.Add(Me.ZPanel2)
        Me.tbRule.Controls.Add(Me.SplitContainer1)
        Me.tbRule.Controls.Add(Me.ZPanel1)
        Me.tbRule.Controls.Add(Me.tbObjectName)
        Me.tbRule.Controls.Add(Me.Label2)
        Me.tbRule.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbRule.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbRule.Size = New System.Drawing.Size(635, 605)
        '
        'tbctrMain
        '
        Me.tbctrMain.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbctrMain.Size = New System.Drawing.Size(643, 634)
        '
        'btPath
        '
        Me.btPath.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btPath.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btPath.ForeColor = System.Drawing.Color.White
        Me.btPath.Location = New System.Drawing.Point(454, 65)
        Me.btPath.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btPath.Name = "btPath"
        Me.btPath.Size = New System.Drawing.Size(99, 28)
        Me.btPath.TabIndex = 1
        Me.btPath.Text = "Buscar..."
        Me.btPath.UseVisualStyleBackColor = True
        '
        'tbPath
        '
        Me.tbPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbPath.Location = New System.Drawing.Point(7, 34)
        Me.tbPath.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbPath.Name = "tbPath"
        Me.tbPath.Size = New System.Drawing.Size(651, 23)
        Me.tbPath.TabIndex = 0
        '
        'btSave
        '
        Me.btSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btSave.ForeColor = System.Drawing.Color.White
        Me.btSave.Location = New System.Drawing.Point(6, 192)
        Me.btSave.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btSave.Name = "btSave"
        Me.btSave.Size = New System.Drawing.Size(99, 28)
        Me.btSave.TabIndex = 11
        Me.btSave.Text = "Guardar"
        Me.btSave.UseVisualStyleBackColor = True
        '
        'lbPropertyValue
        '
        Me.lbPropertyValue.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbPropertyValue.AutoSize = True
        Me.lbPropertyValue.BackColor = System.Drawing.Color.Transparent
        Me.lbPropertyValue.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbPropertyValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lbPropertyValue.Location = New System.Drawing.Point(7, 185)
        Me.lbPropertyValue.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbPropertyValue.Name = "lbPropertyValue"
        Me.lbPropertyValue.Size = New System.Drawing.Size(46, 16)
        Me.lbPropertyValue.TabIndex = 7
        Me.lbPropertyValue.Text = "Valor:"
        Me.lbPropertyValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbPropertyType
        '
        Me.lbPropertyType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbPropertyType.BackColor = System.Drawing.Color.Transparent
        Me.lbPropertyType.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbPropertyType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lbPropertyType.Location = New System.Drawing.Point(60, 161)
        Me.lbPropertyType.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbPropertyType.Name = "lbPropertyType"
        Me.lbPropertyType.Size = New System.Drawing.Size(270, 16)
        Me.lbPropertyType.TabIndex = 9
        Me.lbPropertyType.Text = "lbPropertyType"
        Me.lbPropertyType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lbPropertyType.Visible = False
        '
        'lsbClasses
        '
        Me.lsbClasses.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.lsbClasses.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lsbClasses.FormattingEnabled = True
        Me.lsbClasses.ItemHeight = 16
        Me.lsbClasses.Location = New System.Drawing.Point(8, 25)
        Me.lsbClasses.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.lsbClasses.Name = "lsbClasses"
        Me.lsbClasses.Size = New System.Drawing.Size(275, 180)
        Me.lsbClasses.TabIndex = 3
        '
        'lsbProperties
        '
        Me.lsbProperties.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lsbProperties.FormattingEnabled = True
        Me.lsbProperties.ItemHeight = 16
        Me.lsbProperties.Location = New System.Drawing.Point(11, 25)
        Me.lsbProperties.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.lsbProperties.Name = "lsbProperties"
        Me.lsbProperties.Size = New System.Drawing.Size(318, 132)
        Me.lsbProperties.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(4, 4)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(150, 16)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Nombre de la variable"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbObjectName
        '
        Me.tbObjectName.Dock = System.Windows.Forms.DockStyle.Top
        Me.tbObjectName.Location = New System.Drawing.Point(4, 20)
        Me.tbObjectName.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbObjectName.Name = "tbObjectName"
        Me.tbObjectName.Size = New System.Drawing.Size(627, 23)
        Me.tbObjectName.TabIndex = 2
        '
        'lsbValues
        '
        Me.lsbValues.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lsbValues.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lsbValues.FormattingEnabled = True
        Me.lsbValues.HorizontalScrollbar = True
        Me.lsbValues.ItemHeight = 16
        Me.lsbValues.Location = New System.Drawing.Point(4, 382)
        Me.lsbValues.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.lsbValues.Name = "lsbValues"
        Me.lsbValues.Size = New System.Drawing.Size(502, 219)
        Me.lsbValues.TabIndex = 6
        '
        'btDelete
        '
        Me.btDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btDelete.ForeColor = System.Drawing.Color.White
        Me.btDelete.Location = New System.Drawing.Point(6, 76)
        Me.btDelete.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btDelete.Name = "btDelete"
        Me.btDelete.Size = New System.Drawing.Size(99, 28)
        Me.btDelete.TabIndex = 8
        Me.btDelete.Text = "Quitar"
        Me.btDelete.UseVisualStyleBackColor = True
        '
        'btAdd
        '
        Me.btAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btAdd.ForeColor = System.Drawing.Color.White
        Me.btAdd.Location = New System.Drawing.Point(6, 4)
        Me.btAdd.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btAdd.Name = "btAdd"
        Me.btAdd.Size = New System.Drawing.Size(99, 28)
        Me.btAdd.TabIndex = 7
        Me.btAdd.Text = "Agregar"
        Me.btAdd.UseVisualStyleBackColor = True
        '
        'btDeleteAll
        '
        Me.btDeleteAll.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btDeleteAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btDeleteAll.ForeColor = System.Drawing.Color.White
        Me.btDeleteAll.Location = New System.Drawing.Point(6, 112)
        Me.btDeleteAll.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btDeleteAll.Name = "btDeleteAll"
        Me.btDeleteAll.Size = New System.Drawing.Size(99, 28)
        Me.btDeleteAll.TabIndex = 10
        Me.btDeleteAll.Text = "Borrar todo"
        Me.btDeleteAll.UseVisualStyleBackColor = True
        '
        'lbPropertiesList
        '
        Me.lbPropertiesList.AutoSize = True
        Me.lbPropertiesList.BackColor = System.Drawing.Color.Transparent
        Me.lbPropertiesList.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbPropertiesList.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lbPropertiesList.Location = New System.Drawing.Point(11, 5)
        Me.lbPropertiesList.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbPropertiesList.Name = "lbPropertiesList"
        Me.lbPropertiesList.Size = New System.Drawing.Size(87, 16)
        Me.lbPropertiesList.TabIndex = 19
        Me.lbPropertiesList.Text = "Propiedades"
        Me.lbPropertiesList.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbClassList
        '
        Me.lbClassList.AutoSize = True
        Me.lbClassList.BackColor = System.Drawing.Color.Transparent
        Me.lbClassList.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbClassList.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lbClassList.Location = New System.Drawing.Point(4, 5)
        Me.lbClassList.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbClassList.Name = "lbClassList"
        Me.lbClassList.Size = New System.Drawing.Size(50, 16)
        Me.lbClassList.TabIndex = 18
        Me.lbClassList.Text = "Clases"
        Me.lbClassList.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(4, 14)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(192, 16)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "Ubicación de la librería (.dll)"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(4, 366)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(345, 16)
        Me.Label5.TabIndex = 21
        Me.Label5.Text = "Lista de propiedades con sus valores de la variable"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbPropertyValue
        '
        Me.tbPropertyValue.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbPropertyValue.Location = New System.Drawing.Point(64, 181)
        Me.tbPropertyValue.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.tbPropertyValue.MaxLength = 4000
        Me.tbPropertyValue.Name = "tbPropertyValue"
        Me.tbPropertyValue.Size = New System.Drawing.Size(265, 24)
        Me.tbPropertyValue.TabIndex = 5
        Me.tbPropertyValue.Text = ""
        '
        'btnEdit
        '
        Me.btnEdit.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEdit.ForeColor = System.Drawing.Color.White
        Me.btnEdit.Location = New System.Drawing.Point(6, 40)
        Me.btnEdit.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(99, 28)
        Me.btnEdit.TabIndex = 9
        Me.btnEdit.Text = "Editar"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'lblType
        '
        Me.lblType.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblType.AutoSize = True
        Me.lblType.BackColor = System.Drawing.Color.Transparent
        Me.lblType.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblType.Location = New System.Drawing.Point(7, 161)
        Me.lblType.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(42, 16)
        Me.lblType.TabIndex = 24
        Me.lblType.Text = "Tipo:"
        Me.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Top
        Me.SplitContainer1.Location = New System.Drawing.Point(4, 143)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.lbClassList)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lsbClasses)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.tbPropertyValue)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lblType)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbPropertyValue)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbPropertyType)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lsbProperties)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbPropertiesList)
        Me.SplitContainer1.Size = New System.Drawing.Size(627, 223)
        Me.SplitContainer1.SplitterDistance = 288
        Me.SplitContainer1.SplitterWidth = 5
        Me.SplitContainer1.TabIndex = 25
        '
        'ZPanel1
        '
        Me.ZPanel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ZPanel1.Controls.Add(Me.Label4)
        Me.ZPanel1.Controls.Add(Me.tbPath)
        Me.ZPanel1.Controls.Add(Me.btPath)
        Me.ZPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.ZPanel1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ZPanel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.ZPanel1.Location = New System.Drawing.Point(4, 43)
        Me.ZPanel1.Name = "ZPanel1"
        Me.ZPanel1.Size = New System.Drawing.Size(627, 100)
        Me.ZPanel1.TabIndex = 26
        '
        'ZPanel2
        '
        Me.ZPanel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ZPanel2.Controls.Add(Me.btAdd)
        Me.ZPanel2.Controls.Add(Me.btnEdit)
        Me.ZPanel2.Controls.Add(Me.btDelete)
        Me.ZPanel2.Controls.Add(Me.btDeleteAll)
        Me.ZPanel2.Controls.Add(Me.btSave)
        Me.ZPanel2.Dock = System.Windows.Forms.DockStyle.Right
        Me.ZPanel2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ZPanel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.ZPanel2.Location = New System.Drawing.Point(506, 366)
        Me.ZPanel2.Name = "ZPanel2"
        Me.ZPanel2.Size = New System.Drawing.Size(125, 235)
        Me.ZPanel2.TabIndex = 27
        '
        'UCDoDismemberObject
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "UCDoDismemberObject"
        Me.Size = New System.Drawing.Size(643, 634)
        Me.tbRule.ResumeLayout(False)
        Me.tbRule.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ZPanel1.ResumeLayout(False)
        Me.ZPanel1.PerformLayout()
        Me.ZPanel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btPath As ZButton
    Friend WithEvents tbPath As System.Windows.Forms.TextBox
    Friend WithEvents btSave As ZButton
    Friend WithEvents lbPropertyValue As ZLabel
    Friend WithEvents lbPropertyType As ZLabel
    Friend WithEvents lsbProperties As System.Windows.Forms.ListBox
    Friend WithEvents lsbClasses As System.Windows.Forms.ListBox
    Friend WithEvents tbObjectName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents btAdd As ZButton
    Friend WithEvents btDelete As ZButton
    Friend WithEvents lsbValues As System.Windows.Forms.ListBox
    Friend WithEvents btDeleteAll As ZButton
    Friend WithEvents lbPropertiesList As ZLabel
    Friend WithEvents lbClassList As ZLabel
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents Label5 As ZLabel
    Friend WithEvents tbPropertyValue As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents btnEdit As ZButton
    Friend WithEvents lblType As ZLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents ZPanel2 As ZPanel
    Friend WithEvents ZPanel1 As ZPanel
End Class