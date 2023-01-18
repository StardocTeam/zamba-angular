<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMAIN
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
    Private Sub InitializeComponent()
        Me.lblControlador = New System.Windows.Forms.Label
        Me.cboControlador = New System.Windows.Forms.ComboBox
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.ArchivoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.GenerarODBCToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SalirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.grupoOracle = New System.Windows.Forms.GroupBox
        Me.txtTranslationOption = New System.Windows.Forms.TextBox
        Me.txtTranslationDLL = New System.Windows.Forms.TextBox
        Me.txtSQLGetDataExtensions = New System.Windows.Forms.TextBox
        Me.txtResultSets = New System.Windows.Forms.TextBox
        Me.txtQueryTimeout = New System.Windows.Forms.TextBox
        Me.txtPrefetchCount = New System.Windows.Forms.TextBox
        Me.txtMetadataIdDefault = New System.Windows.Forms.TextBox
        Me.txtLongs = New System.Windows.Forms.TextBox
        Me.txtLobs = New System.Windows.Forms.TextBox
        Me.txtForceWCHAR = New System.Windows.Forms.TextBox
        Me.txtFailoverRetryCount = New System.Windows.Forms.TextBox
        Me.txtFailoverDelay = New System.Windows.Forms.TextBox
        Me.txtFailOver = New System.Windows.Forms.TextBox
        Me.txtEXECSyntax = New System.Windows.Forms.TextBox
        Me.txtEXECSchemaOpt = New System.Windows.Forms.TextBox
        Me.txtDriver = New System.Windows.Forms.TextBox
        Me.txtDisableMTS = New System.Windows.Forms.TextBox
        Me.txtDescription = New System.Windows.Forms.TextBox
        Me.txtCloseCursor = New System.Windows.Forms.TextBox
        Me.txtBatchAutocommitMode = New System.Windows.Forms.TextBox
        Me.txtAttributes = New System.Windows.Forms.TextBox
        Me.txtApplicationAttributes = New System.Windows.Forms.TextBox
        Me.txtUserId = New System.Windows.Forms.TextBox
        Me.txtDsn = New System.Windows.Forms.TextBox
        Me.txtServerName = New System.Windows.Forms.TextBox
        Me.lblUserID = New System.Windows.Forms.Label
        Me.lblLongs = New System.Windows.Forms.Label
        Me.lblTranslationOption = New System.Windows.Forms.Label
        Me.lblTranslationDLL = New System.Windows.Forms.Label
        Me.lblSQLGetDataExtensions = New System.Windows.Forms.Label
        Me.lblServerName = New System.Windows.Forms.Label
        Me.lblQueryTimeout = New System.Windows.Forms.Label
        Me.lblResultSets = New System.Windows.Forms.Label
        Me.lblPrefetchCount = New System.Windows.Forms.Label
        Me.lblLobs = New System.Windows.Forms.Label
        Me.lblMetadataIdDefault = New System.Windows.Forms.Label
        Me.lblFailoverRetryCount = New System.Windows.Forms.Label
        Me.ForceWCHAR = New System.Windows.Forms.Label
        Me.lblFailoverDelay = New System.Windows.Forms.Label
        Me.lblEXECSyntax = New System.Windows.Forms.Label
        Me.lblFailover = New System.Windows.Forms.Label
        Me.lblEXECSchemaOpt = New System.Windows.Forms.Label
        Me.lblDsn = New System.Windows.Forms.Label
        Me.lblDriver = New System.Windows.Forms.Label
        Me.lblDisableMTS = New System.Windows.Forms.Label
        Me.lblDescriptionOracle = New System.Windows.Forms.Label
        Me.lblCloseCursor = New System.Windows.Forms.Label
        Me.lblBatchAutocommitMode = New System.Windows.Forms.Label
        Me.lblAttributes = New System.Windows.Forms.Label
        Me.lblApplicationAttributes = New System.Windows.Forms.Label
        Me.grupoSqlServer = New System.Windows.Forms.GroupBox
        Me.txtDsnSQLServer = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtDescriptionSQL = New System.Windows.Forms.TextBox
        Me.txtDriverSQL = New System.Windows.Forms.TextBox
        Me.txtLastUser = New System.Windows.Forms.TextBox
        Me.txtDatabase = New System.Windows.Forms.TextBox
        Me.txtServer = New System.Windows.Forms.TextBox
        Me.lblServer = New System.Windows.Forms.Label
        Me.lblLastUser = New System.Windows.Forms.Label
        Me.lblDriverSQLServer = New System.Windows.Forms.Label
        Me.lblDescrptionSQLServer = New System.Windows.Forms.Label
        Me.lblDatabase = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.MenuStrip1.SuspendLayout()
        Me.grupoOracle.SuspendLayout()
        Me.grupoSqlServer.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblControlador
        '
        Me.lblControlador.AutoSize = True
        Me.lblControlador.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblControlador.Location = New System.Drawing.Point(11, 35)
        Me.lblControlador.Name = "lblControlador"
        Me.lblControlador.Size = New System.Drawing.Size(184, 15)
        Me.lblControlador.TabIndex = 2
        Me.lblControlador.Text = "Controlador de origen de datos :"
        '
        'cboControlador
        '
        Me.cboControlador.FormattingEnabled = True
        Me.cboControlador.Items.AddRange(New Object() {"Oracle en OraHome92", "SQL Server"})
        Me.cboControlador.Location = New System.Drawing.Point(201, 33)
        Me.cboControlador.Name = "cboControlador"
        Me.cboControlador.Size = New System.Drawing.Size(201, 21)
        Me.cboControlador.Sorted = True
        Me.cboControlador.TabIndex = 3
        Me.cboControlador.Text = "Oracle en OraHome92"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.MenuStrip1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArchivoToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(699, 24)
        Me.MenuStrip1.TabIndex = 4
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArchivoToolStripMenuItem
        '
        Me.ArchivoToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GenerarODBCToolStripMenuItem, Me.SalirToolStripMenuItem})
        Me.ArchivoToolStripMenuItem.Name = "ArchivoToolStripMenuItem"
        Me.ArchivoToolStripMenuItem.Size = New System.Drawing.Size(59, 20)
        Me.ArchivoToolStripMenuItem.Text = "&Archivo"
        '
        'GenerarODBCToolStripMenuItem
        '
        Me.GenerarODBCToolStripMenuItem.Name = "GenerarODBCToolStripMenuItem"
        Me.GenerarODBCToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.GenerarODBCToolStripMenuItem.Text = "Generar ODBC"
        '
        'SalirToolStripMenuItem
        '
        Me.SalirToolStripMenuItem.Name = "SalirToolStripMenuItem"
        Me.SalirToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.SalirToolStripMenuItem.Text = "Cerrar"
        '
        'grupoOracle
        '
        Me.grupoOracle.Controls.Add(Me.txtTranslationOption)
        Me.grupoOracle.Controls.Add(Me.txtTranslationDLL)
        Me.grupoOracle.Controls.Add(Me.txtSQLGetDataExtensions)
        Me.grupoOracle.Controls.Add(Me.txtResultSets)
        Me.grupoOracle.Controls.Add(Me.txtQueryTimeout)
        Me.grupoOracle.Controls.Add(Me.txtPrefetchCount)
        Me.grupoOracle.Controls.Add(Me.txtMetadataIdDefault)
        Me.grupoOracle.Controls.Add(Me.txtLongs)
        Me.grupoOracle.Controls.Add(Me.txtLobs)
        Me.grupoOracle.Controls.Add(Me.txtForceWCHAR)
        Me.grupoOracle.Controls.Add(Me.txtFailoverRetryCount)
        Me.grupoOracle.Controls.Add(Me.txtFailoverDelay)
        Me.grupoOracle.Controls.Add(Me.txtFailOver)
        Me.grupoOracle.Controls.Add(Me.txtEXECSyntax)
        Me.grupoOracle.Controls.Add(Me.txtEXECSchemaOpt)
        Me.grupoOracle.Controls.Add(Me.txtDriver)
        Me.grupoOracle.Controls.Add(Me.txtDisableMTS)
        Me.grupoOracle.Controls.Add(Me.txtDescription)
        Me.grupoOracle.Controls.Add(Me.txtCloseCursor)
        Me.grupoOracle.Controls.Add(Me.txtBatchAutocommitMode)
        Me.grupoOracle.Controls.Add(Me.txtAttributes)
        Me.grupoOracle.Controls.Add(Me.txtApplicationAttributes)
        Me.grupoOracle.Controls.Add(Me.txtUserId)
        Me.grupoOracle.Controls.Add(Me.txtDsn)
        Me.grupoOracle.Controls.Add(Me.txtServerName)
        Me.grupoOracle.Controls.Add(Me.lblUserID)
        Me.grupoOracle.Controls.Add(Me.lblLongs)
        Me.grupoOracle.Controls.Add(Me.lblTranslationOption)
        Me.grupoOracle.Controls.Add(Me.lblTranslationDLL)
        Me.grupoOracle.Controls.Add(Me.lblSQLGetDataExtensions)
        Me.grupoOracle.Controls.Add(Me.lblServerName)
        Me.grupoOracle.Controls.Add(Me.lblQueryTimeout)
        Me.grupoOracle.Controls.Add(Me.lblResultSets)
        Me.grupoOracle.Controls.Add(Me.lblPrefetchCount)
        Me.grupoOracle.Controls.Add(Me.lblLobs)
        Me.grupoOracle.Controls.Add(Me.lblMetadataIdDefault)
        Me.grupoOracle.Controls.Add(Me.lblFailoverRetryCount)
        Me.grupoOracle.Controls.Add(Me.ForceWCHAR)
        Me.grupoOracle.Controls.Add(Me.lblFailoverDelay)
        Me.grupoOracle.Controls.Add(Me.lblEXECSyntax)
        Me.grupoOracle.Controls.Add(Me.lblFailover)
        Me.grupoOracle.Controls.Add(Me.lblEXECSchemaOpt)
        Me.grupoOracle.Controls.Add(Me.lblDsn)
        Me.grupoOracle.Controls.Add(Me.lblDriver)
        Me.grupoOracle.Controls.Add(Me.lblDisableMTS)
        Me.grupoOracle.Controls.Add(Me.lblDescriptionOracle)
        Me.grupoOracle.Controls.Add(Me.lblCloseCursor)
        Me.grupoOracle.Controls.Add(Me.lblBatchAutocommitMode)
        Me.grupoOracle.Controls.Add(Me.lblAttributes)
        Me.grupoOracle.Controls.Add(Me.lblApplicationAttributes)
        Me.grupoOracle.Location = New System.Drawing.Point(14, 60)
        Me.grupoOracle.Name = "grupoOracle"
        Me.grupoOracle.Size = New System.Drawing.Size(665, 317)
        Me.grupoOracle.TabIndex = 31
        Me.grupoOracle.TabStop = False
        Me.grupoOracle.Text = "ODBC Oracle"
        '
        'txtTranslationOption
        '
        Me.txtTranslationOption.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtTranslationOption.Location = New System.Drawing.Point(371, 277)
        Me.txtTranslationOption.Name = "txtTranslationOption"
        Me.txtTranslationOption.Size = New System.Drawing.Size(50, 20)
        Me.txtTranslationOption.TabIndex = 80
        Me.txtTranslationOption.Text = "0"
        '
        'txtTranslationDLL
        '
        Me.txtTranslationDLL.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtTranslationDLL.Location = New System.Drawing.Point(126, 280)
        Me.txtTranslationDLL.Name = "txtTranslationDLL"
        Me.txtTranslationDLL.Size = New System.Drawing.Size(50, 20)
        Me.txtTranslationDLL.TabIndex = 79
        '
        'txtSQLGetDataExtensions
        '
        Me.txtSQLGetDataExtensions.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtSQLGetDataExtensions.Location = New System.Drawing.Point(371, 247)
        Me.txtSQLGetDataExtensions.Name = "txtSQLGetDataExtensions"
        Me.txtSQLGetDataExtensions.Size = New System.Drawing.Size(50, 20)
        Me.txtSQLGetDataExtensions.TabIndex = 78
        Me.txtSQLGetDataExtensions.Text = "F"
        '
        'txtResultSets
        '
        Me.txtResultSets.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtResultSets.Location = New System.Drawing.Point(126, 247)
        Me.txtResultSets.Name = "txtResultSets"
        Me.txtResultSets.Size = New System.Drawing.Size(50, 20)
        Me.txtResultSets.TabIndex = 77
        Me.txtResultSets.Text = "T"
        '
        'txtQueryTimeout
        '
        Me.txtQueryTimeout.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtQueryTimeout.Location = New System.Drawing.Point(517, 217)
        Me.txtQueryTimeout.Name = "txtQueryTimeout"
        Me.txtQueryTimeout.Size = New System.Drawing.Size(50, 20)
        Me.txtQueryTimeout.TabIndex = 76
        Me.txtQueryTimeout.Text = "T"
        '
        'txtPrefetchCount
        '
        Me.txtPrefetchCount.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtPrefetchCount.Location = New System.Drawing.Point(330, 217)
        Me.txtPrefetchCount.Name = "txtPrefetchCount"
        Me.txtPrefetchCount.Size = New System.Drawing.Size(50, 20)
        Me.txtPrefetchCount.TabIndex = 75
        Me.txtPrefetchCount.Text = "10"
        '
        'txtMetadataIdDefault
        '
        Me.txtMetadataIdDefault.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtMetadataIdDefault.Location = New System.Drawing.Point(126, 217)
        Me.txtMetadataIdDefault.Name = "txtMetadataIdDefault"
        Me.txtMetadataIdDefault.Size = New System.Drawing.Size(50, 20)
        Me.txtMetadataIdDefault.TabIndex = 74
        Me.txtMetadataIdDefault.Text = "F"
        '
        'txtLongs
        '
        Me.txtLongs.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtLongs.Location = New System.Drawing.Point(517, 185)
        Me.txtLongs.Name = "txtLongs"
        Me.txtLongs.Size = New System.Drawing.Size(50, 20)
        Me.txtLongs.TabIndex = 73
        Me.txtLongs.Text = "F"
        '
        'txtLobs
        '
        Me.txtLobs.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtLobs.Location = New System.Drawing.Point(330, 185)
        Me.txtLobs.Name = "txtLobs"
        Me.txtLobs.Size = New System.Drawing.Size(50, 20)
        Me.txtLobs.TabIndex = 72
        Me.txtLobs.Text = "T"
        '
        'txtForceWCHAR
        '
        Me.txtForceWCHAR.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtForceWCHAR.Location = New System.Drawing.Point(92, 185)
        Me.txtForceWCHAR.Name = "txtForceWCHAR"
        Me.txtForceWCHAR.Size = New System.Drawing.Size(50, 20)
        Me.txtForceWCHAR.TabIndex = 71
        Me.txtForceWCHAR.Text = "F"
        '
        'txtFailoverRetryCount
        '
        Me.txtFailoverRetryCount.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtFailoverRetryCount.Location = New System.Drawing.Point(517, 153)
        Me.txtFailoverRetryCount.Name = "txtFailoverRetryCount"
        Me.txtFailoverRetryCount.Size = New System.Drawing.Size(50, 20)
        Me.txtFailoverRetryCount.TabIndex = 70
        Me.txtFailoverRetryCount.Text = "10"
        '
        'txtFailoverDelay
        '
        Me.txtFailoverDelay.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtFailoverDelay.Location = New System.Drawing.Point(330, 153)
        Me.txtFailoverDelay.Name = "txtFailoverDelay"
        Me.txtFailoverDelay.Size = New System.Drawing.Size(50, 20)
        Me.txtFailoverDelay.TabIndex = 69
        Me.txtFailoverDelay.Text = "10"
        '
        'txtFailOver
        '
        Me.txtFailOver.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtFailOver.Location = New System.Drawing.Point(92, 153)
        Me.txtFailOver.Name = "txtFailOver"
        Me.txtFailOver.Size = New System.Drawing.Size(50, 20)
        Me.txtFailOver.TabIndex = 68
        Me.txtFailOver.Text = "T"
        '
        'txtEXECSyntax
        '
        Me.txtEXECSyntax.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtEXECSyntax.Location = New System.Drawing.Point(517, 121)
        Me.txtEXECSyntax.Name = "txtEXECSyntax"
        Me.txtEXECSyntax.Size = New System.Drawing.Size(50, 20)
        Me.txtEXECSyntax.TabIndex = 67
        Me.txtEXECSyntax.Text = "F"
        '
        'txtEXECSchemaOpt
        '
        Me.txtEXECSchemaOpt.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtEXECSchemaOpt.Location = New System.Drawing.Point(330, 121)
        Me.txtEXECSchemaOpt.Name = "txtEXECSchemaOpt"
        Me.txtEXECSchemaOpt.Size = New System.Drawing.Size(50, 20)
        Me.txtEXECSchemaOpt.TabIndex = 66
        '
        'txtDriver
        '
        Me.txtDriver.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtDriver.Location = New System.Drawing.Point(92, 121)
        Me.txtDriver.Name = "txtDriver"
        Me.txtDriver.Size = New System.Drawing.Size(121, 20)
        Me.txtDriver.TabIndex = 65
        'todo: y si esta instalado en otro lado
        Me.txtDriver.Text = "C:\oracle\ora92\BIN\SQORA32.DLL"
        '
        'txtDisableMTS
        '
        Me.txtDisableMTS.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtDisableMTS.Location = New System.Drawing.Point(517, 94)
        Me.txtDisableMTS.Name = "txtDisableMTS"
        Me.txtDisableMTS.Size = New System.Drawing.Size(50, 20)
        Me.txtDisableMTS.TabIndex = 64
        Me.txtDisableMTS.Text = "F"
        '
        'txtDescription
        '
        Me.txtDescription.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtDescription.Location = New System.Drawing.Point(287, 94)
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(149, 20)
        Me.txtDescription.TabIndex = 63
        Me.txtDescription.Text = "conexion a Oracle para exportacion de mails"
        '
        'txtCloseCursor
        '
        Me.txtCloseCursor.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtCloseCursor.Location = New System.Drawing.Point(92, 92)
        Me.txtCloseCursor.Name = "txtCloseCursor"
        Me.txtCloseCursor.Size = New System.Drawing.Size(50, 20)
        Me.txtCloseCursor.TabIndex = 62
        Me.txtCloseCursor.Text = "F"
        '
        'txtBatchAutocommitMode
        '
        Me.txtBatchAutocommitMode.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtBatchAutocommitMode.Location = New System.Drawing.Point(507, 66)
        Me.txtBatchAutocommitMode.Name = "txtBatchAutocommitMode"
        Me.txtBatchAutocommitMode.Size = New System.Drawing.Size(149, 20)
        Me.txtBatchAutocommitMode.TabIndex = 61
        Me.txtBatchAutocommitMode.Text = "IfAllSuccessful"
        '
        'txtAttributes
        '
        Me.txtAttributes.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtAttributes.Location = New System.Drawing.Point(287, 66)
        Me.txtAttributes.Name = "txtAttributes"
        Me.txtAttributes.Size = New System.Drawing.Size(50, 20)
        Me.txtAttributes.TabIndex = 60
        Me.txtAttributes.Text = "W"
        '
        'txtApplicationAttributes
        '
        Me.txtApplicationAttributes.ForeColor = System.Drawing.SystemColors.GrayText
        Me.txtApplicationAttributes.Location = New System.Drawing.Point(127, 66)
        Me.txtApplicationAttributes.Name = "txtApplicationAttributes"
        Me.txtApplicationAttributes.Size = New System.Drawing.Size(50, 20)
        Me.txtApplicationAttributes.TabIndex = 59
        Me.txtApplicationAttributes.Text = "T"
        '
        'txtUserId
        '
        Me.txtUserId.Location = New System.Drawing.Point(511, 19)
        Me.txtUserId.Name = "txtUserId"
        Me.txtUserId.Size = New System.Drawing.Size(97, 20)
        Me.txtUserId.TabIndex = 58
        Me.txtUserId.Text = "zamba"
        '
        'txtDsn
        '
        Me.txtDsn.Location = New System.Drawing.Point(324, 19)
        Me.txtDsn.Name = "txtDsn"
        Me.txtDsn.Size = New System.Drawing.Size(97, 20)
        Me.txtDsn.TabIndex = 57
        Me.txtDsn.Text = "ZAMBA"
        '
        'txtServerName
        '
        Me.txtServerName.AccessibleDescription = ""
        Me.txtServerName.Location = New System.Drawing.Point(92, 19)
        Me.txtServerName.Name = "txtServerName"
        Me.txtServerName.Size = New System.Drawing.Size(187, 20)
        Me.txtServerName.TabIndex = 56
        Me.txtServerName.Text = "ZAMBAPRD"
        '
        'lblUserID
        '
        Me.lblUserID.AutoSize = True
        Me.lblUserID.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUserID.Location = New System.Drawing.Point(457, 21)
        Me.lblUserID.Name = "lblUserID"
        Me.lblUserID.Size = New System.Drawing.Size(48, 15)
        Me.lblUserID.TabIndex = 54
        Me.lblUserID.Text = "UserID "
        '
        'lblLongs
        '
        Me.lblLongs.AutoSize = True
        Me.lblLongs.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLongs.Location = New System.Drawing.Point(470, 187)
        Me.lblLongs.Name = "lblLongs"
        Me.lblLongs.Size = New System.Drawing.Size(42, 15)
        Me.lblLongs.TabIndex = 55
        Me.lblLongs.Text = "Longs"
        '
        'lblTranslationOption
        '
        Me.lblTranslationOption.AutoSize = True
        Me.lblTranslationOption.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTranslationOption.Location = New System.Drawing.Point(251, 282)
        Me.lblTranslationOption.Name = "lblTranslationOption"
        Me.lblTranslationOption.Size = New System.Drawing.Size(108, 15)
        Me.lblTranslationOption.TabIndex = 53
        Me.lblTranslationOption.Text = "Translation Option"
        '
        'lblTranslationDLL
        '
        Me.lblTranslationDLL.AutoSize = True
        Me.lblTranslationDLL.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTranslationDLL.Location = New System.Drawing.Point(25, 282)
        Me.lblTranslationDLL.Name = "lblTranslationDLL"
        Me.lblTranslationDLL.Size = New System.Drawing.Size(95, 15)
        Me.lblTranslationDLL.TabIndex = 52
        Me.lblTranslationDLL.Text = "Translation DLL"
        '
        'lblSQLGetDataExtensions
        '
        Me.lblSQLGetDataExtensions.AutoSize = True
        Me.lblSQLGetDataExtensions.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSQLGetDataExtensions.Location = New System.Drawing.Point(220, 252)
        Me.lblSQLGetDataExtensions.Name = "lblSQLGetDataExtensions"
        Me.lblSQLGetDataExtensions.Size = New System.Drawing.Size(139, 15)
        Me.lblSQLGetDataExtensions.TabIndex = 51
        Me.lblSQLGetDataExtensions.Text = "SQLGetData extensions"
        '
        'lblServerName
        '
        Me.lblServerName.AutoSize = True
        Me.lblServerName.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServerName.Location = New System.Drawing.Point(8, 21)
        Me.lblServerName.Name = "lblServerName"
        Me.lblServerName.Size = New System.Drawing.Size(78, 15)
        Me.lblServerName.TabIndex = 50
        Me.lblServerName.Text = "ServerName"
        '
        'lblQueryTimeout
        '
        Me.lblQueryTimeout.AutoSize = True
        Me.lblQueryTimeout.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQueryTimeout.Location = New System.Drawing.Point(432, 217)
        Me.lblQueryTimeout.Name = "lblQueryTimeout"
        Me.lblQueryTimeout.Size = New System.Drawing.Size(90, 15)
        Me.lblQueryTimeout.TabIndex = 49
        Me.lblQueryTimeout.Text = "QueryTimeout  "
        '
        'lblResultSets
        '
        Me.lblResultSets.AutoSize = True
        Me.lblResultSets.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblResultSets.Location = New System.Drawing.Point(52, 252)
        Me.lblResultSets.Name = "lblResultSets"
        Me.lblResultSets.Size = New System.Drawing.Size(68, 15)
        Me.lblResultSets.TabIndex = 48
        Me.lblResultSets.Text = "ResultSets"
        '
        'lblPrefetchCount
        '
        Me.lblPrefetchCount.AutoSize = True
        Me.lblPrefetchCount.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrefetchCount.Location = New System.Drawing.Point(233, 217)
        Me.lblPrefetchCount.Name = "lblPrefetchCount"
        Me.lblPrefetchCount.Size = New System.Drawing.Size(91, 15)
        Me.lblPrefetchCount.TabIndex = 47
        Me.lblPrefetchCount.Text = "PrefetchCount  "
        '
        'lblLobs
        '
        Me.lblLobs.AutoSize = True
        Me.lblLobs.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLobs.Location = New System.Drawing.Point(284, 187)
        Me.lblLobs.Name = "lblLobs"
        Me.lblLobs.Size = New System.Drawing.Size(35, 15)
        Me.lblLobs.TabIndex = 46
        Me.lblLobs.Text = "Lobs"
        '
        'lblMetadataIdDefault
        '
        Me.lblMetadataIdDefault.AutoSize = True
        Me.lblMetadataIdDefault.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMetadataIdDefault.Location = New System.Drawing.Point(15, 217)
        Me.lblMetadataIdDefault.Name = "lblMetadataIdDefault"
        Me.lblMetadataIdDefault.Size = New System.Drawing.Size(112, 15)
        Me.lblMetadataIdDefault.TabIndex = 45
        Me.lblMetadataIdDefault.Text = "MetadataIdDefault  "
        '
        'lblFailoverRetryCount
        '
        Me.lblFailoverRetryCount.AutoSize = True
        Me.lblFailoverRetryCount.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFailoverRetryCount.Location = New System.Drawing.Point(405, 158)
        Me.lblFailoverRetryCount.Name = "lblFailoverRetryCount"
        Me.lblFailoverRetryCount.Size = New System.Drawing.Size(117, 15)
        Me.lblFailoverRetryCount.TabIndex = 44
        Me.lblFailoverRetryCount.Text = "FailoverRetryCount  "
        '
        'ForceWCHAR
        '
        Me.ForceWCHAR.AutoSize = True
        Me.ForceWCHAR.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForceWCHAR.Location = New System.Drawing.Point(8, 187)
        Me.ForceWCHAR.Name = "ForceWCHAR"
        Me.ForceWCHAR.Size = New System.Drawing.Size(89, 15)
        Me.ForceWCHAR.TabIndex = 43
        Me.ForceWCHAR.Text = "ForceWCHAR  "
        '
        'lblFailoverDelay
        '
        Me.lblFailoverDelay.AutoSize = True
        Me.lblFailoverDelay.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFailoverDelay.Location = New System.Drawing.Point(243, 158)
        Me.lblFailoverDelay.Name = "lblFailoverDelay"
        Me.lblFailoverDelay.Size = New System.Drawing.Size(81, 15)
        Me.lblFailoverDelay.TabIndex = 42
        Me.lblFailoverDelay.Text = "FailoverDelay"
        '
        'lblEXECSyntax
        '
        Me.lblEXECSyntax.AutoSize = True
        Me.lblEXECSyntax.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEXECSyntax.Location = New System.Drawing.Point(442, 126)
        Me.lblEXECSyntax.Name = "lblEXECSyntax"
        Me.lblEXECSyntax.Size = New System.Drawing.Size(80, 15)
        Me.lblEXECSyntax.TabIndex = 41
        Me.lblEXECSyntax.Text = "EXECSyntax  "
        '
        'lblFailover
        '
        Me.lblFailover.AutoSize = True
        Me.lblFailover.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFailover.Location = New System.Drawing.Point(30, 158)
        Me.lblFailover.Name = "lblFailover"
        Me.lblFailover.Size = New System.Drawing.Size(56, 15)
        Me.lblFailover.TabIndex = 40
        Me.lblFailover.Text = "Failover  "
        '
        'lblEXECSchemaOpt
        '
        Me.lblEXECSchemaOpt.AutoSize = True
        Me.lblEXECSchemaOpt.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEXECSchemaOpt.Location = New System.Drawing.Point(220, 126)
        Me.lblEXECSchemaOpt.Name = "lblEXECSchemaOpt"
        Me.lblEXECSchemaOpt.Size = New System.Drawing.Size(104, 15)
        Me.lblEXECSchemaOpt.TabIndex = 39
        Me.lblEXECSchemaOpt.Text = "EXECSchemaOpt"
        '
        'lblDsn
        '
        Me.lblDsn.AutoSize = True
        Me.lblDsn.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDsn.Location = New System.Drawing.Point(291, 21)
        Me.lblDsn.Name = "lblDsn"
        Me.lblDsn.Size = New System.Drawing.Size(37, 15)
        Me.lblDsn.TabIndex = 38
        Me.lblDsn.Text = "DSN  "
        '
        'lblDriver
        '
        Me.lblDriver.AutoSize = True
        Me.lblDriver.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDriver.Location = New System.Drawing.Point(41, 126)
        Me.lblDriver.Name = "lblDriver"
        Me.lblDriver.Size = New System.Drawing.Size(45, 15)
        Me.lblDriver.TabIndex = 37
        Me.lblDriver.Text = "Driver  "
        '
        'lblDisableMTS
        '
        Me.lblDisableMTS.AutoSize = True
        Me.lblDisableMTS.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDisableMTS.Location = New System.Drawing.Point(442, 96)
        Me.lblDisableMTS.Name = "lblDisableMTS"
        Me.lblDisableMTS.Size = New System.Drawing.Size(80, 15)
        Me.lblDisableMTS.TabIndex = 36
        Me.lblDisableMTS.Text = "DisableMTS  "
        '
        'lblDescriptionOracle
        '
        Me.lblDescriptionOracle.AutoSize = True
        Me.lblDescriptionOracle.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescriptionOracle.Location = New System.Drawing.Point(220, 97)
        Me.lblDescriptionOracle.Name = "lblDescriptionOracle"
        Me.lblDescriptionOracle.Size = New System.Drawing.Size(76, 15)
        Me.lblDescriptionOracle.TabIndex = 35
        Me.lblDescriptionOracle.Text = "Description  "
        '
        'lblCloseCursor
        '
        Me.lblCloseCursor.AutoSize = True
        Me.lblCloseCursor.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCloseCursor.Location = New System.Drawing.Point(8, 96)
        Me.lblCloseCursor.Name = "lblCloseCursor"
        Me.lblCloseCursor.Size = New System.Drawing.Size(78, 15)
        Me.lblCloseCursor.TabIndex = 34
        Me.lblCloseCursor.Text = "CloseCursor"
        '
        'lblBatchAutocommitMode
        '
        Me.lblBatchAutocommitMode.AutoSize = True
        Me.lblBatchAutocommitMode.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBatchAutocommitMode.Location = New System.Drawing.Point(368, 68)
        Me.lblBatchAutocommitMode.Name = "lblBatchAutocommitMode"
        Me.lblBatchAutocommitMode.Size = New System.Drawing.Size(133, 15)
        Me.lblBatchAutocommitMode.TabIndex = 33
        Me.lblBatchAutocommitMode.Text = "BatchAutocommitMode"
        '
        'lblAttributes
        '
        Me.lblAttributes.AutoSize = True
        Me.lblAttributes.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAttributes.Location = New System.Drawing.Point(225, 68)
        Me.lblAttributes.Name = "lblAttributes"
        Me.lblAttributes.Size = New System.Drawing.Size(58, 15)
        Me.lblAttributes.TabIndex = 32
        Me.lblAttributes.Text = "Attributes"
        '
        'lblApplicationAttributes
        '
        Me.lblApplicationAttributes.AutoSize = True
        Me.lblApplicationAttributes.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblApplicationAttributes.Location = New System.Drawing.Point(8, 68)
        Me.lblApplicationAttributes.Name = "lblApplicationAttributes"
        Me.lblApplicationAttributes.Size = New System.Drawing.Size(121, 15)
        Me.lblApplicationAttributes.TabIndex = 31
        Me.lblApplicationAttributes.Text = "Application Attributes"
        '
        'grupoSqlServer
        '
        Me.grupoSqlServer.Controls.Add(Me.txtDsnSQLServer)
        Me.grupoSqlServer.Controls.Add(Me.Label1)
        Me.grupoSqlServer.Controls.Add(Me.txtDescriptionSQL)
        Me.grupoSqlServer.Controls.Add(Me.txtDriverSQL)
        Me.grupoSqlServer.Controls.Add(Me.txtLastUser)
        Me.grupoSqlServer.Controls.Add(Me.txtDatabase)
        Me.grupoSqlServer.Controls.Add(Me.txtServer)
        Me.grupoSqlServer.Controls.Add(Me.lblServer)
        Me.grupoSqlServer.Controls.Add(Me.lblLastUser)
        Me.grupoSqlServer.Controls.Add(Me.lblDriverSQLServer)
        Me.grupoSqlServer.Controls.Add(Me.lblDescrptionSQLServer)
        Me.grupoSqlServer.Controls.Add(Me.lblDatabase)
        Me.grupoSqlServer.Enabled = False
        Me.grupoSqlServer.Location = New System.Drawing.Point(14, 398)
        Me.grupoSqlServer.Name = "grupoSqlServer"
        Me.grupoSqlServer.Size = New System.Drawing.Size(665, 132)
        Me.grupoSqlServer.TabIndex = 32
        Me.grupoSqlServer.TabStop = False
        Me.grupoSqlServer.Text = "ODBC SQL Server"
        '
        'txtDsnSQLServer
        '
        Me.txtDsnSQLServer.Location = New System.Drawing.Point(84, 97)
        Me.txtDsnSQLServer.Name = "txtDsnSQLServer"
        Me.txtDsnSQLServer.Size = New System.Drawing.Size(97, 20)
        Me.txtDsnSQLServer.TabIndex = 60
        Me.txtDsnSQLServer.Text = "ZAMBA"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(30, 99)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 15)
        Me.Label1.TabIndex = 62
        Me.Label1.Text = "DSN"
        '
        'txtDescriptionSQL
        '
        Me.txtDescriptionSQL.Location = New System.Drawing.Point(412, 49)
        Me.txtDescriptionSQL.Name = "txtDescriptionSQL"
        Me.txtDescriptionSQL.Size = New System.Drawing.Size(239, 20)
        Me.txtDescriptionSQL.TabIndex = 62
        '
        'txtDriverSQL
        '
        Me.txtDriverSQL.Location = New System.Drawing.Point(412, 21)
        Me.txtDriverSQL.Name = "txtDriverSQL"
        Me.txtDriverSQL.Size = New System.Drawing.Size(239, 20)
        Me.txtDriverSQL.TabIndex = 61
        Me.txtDriverSQL.Text = "C:\WINDOWS\System32\SQLSRV32.dll"
        '
        'txtLastUser
        '
        Me.txtLastUser.Location = New System.Drawing.Point(84, 73)
        Me.txtLastUser.Name = "txtLastUser"
        Me.txtLastUser.Size = New System.Drawing.Size(147, 20)
        Me.txtLastUser.TabIndex = 59
        Me.txtLastUser.Text = "sa"
        '
        'txtDatabase
        '
        Me.txtDatabase.Location = New System.Drawing.Point(84, 47)
        Me.txtDatabase.Name = "txtDatabase"
        Me.txtDatabase.Size = New System.Drawing.Size(147, 20)
        Me.txtDatabase.TabIndex = 58
        Me.txtDatabase.Text = "zambatst"
        '
        'txtServer
        '
        Me.txtServer.Location = New System.Drawing.Point(84, 21)
        Me.txtServer.Name = "txtServer"
        Me.txtServer.Size = New System.Drawing.Size(195, 20)
        Me.txtServer.TabIndex = 57
        Me.txtServer.Text = "MORDOR"
        '
        'lblServer
        '
        Me.lblServer.AutoSize = True
        Me.lblServer.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServer.Location = New System.Drawing.Point(24, 21)
        Me.lblServer.Name = "lblServer"
        Me.lblServer.Size = New System.Drawing.Size(45, 15)
        Me.lblServer.TabIndex = 4
        Me.lblServer.Text = "Server"
        '
        'lblLastUser
        '
        Me.lblLastUser.AutoSize = True
        Me.lblLastUser.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLastUser.Location = New System.Drawing.Point(24, 75)
        Me.lblLastUser.Name = "lblLastUser"
        Me.lblLastUser.Size = New System.Drawing.Size(62, 15)
        Me.lblLastUser.TabIndex = 3
        Me.lblLastUser.Text = " LastUser"
        '
        'lblDriverSQLServer
        '
        Me.lblDriverSQLServer.AutoSize = True
        Me.lblDriverSQLServer.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDriverSQLServer.Location = New System.Drawing.Point(338, 21)
        Me.lblDriverSQLServer.Name = "lblDriverSQLServer"
        Me.lblDriverSQLServer.Size = New System.Drawing.Size(39, 15)
        Me.lblDriverSQLServer.TabIndex = 2
        Me.lblDriverSQLServer.Text = "Driver"
        '
        'lblDescrptionSQLServer
        '
        Me.lblDescrptionSQLServer.AutoSize = True
        Me.lblDescrptionSQLServer.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescrptionSQLServer.Location = New System.Drawing.Point(338, 51)
        Me.lblDescrptionSQLServer.Name = "lblDescrptionSQLServer"
        Me.lblDescrptionSQLServer.Size = New System.Drawing.Size(70, 15)
        Me.lblDescrptionSQLServer.TabIndex = 1
        Me.lblDescrptionSQLServer.Text = "Description"
        '
        'lblDatabase
        '
        Me.lblDatabase.AutoSize = True
        Me.lblDatabase.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDatabase.Location = New System.Drawing.Point(25, 49)
        Me.lblDatabase.Name = "lblDatabase"
        Me.lblDatabase.Size = New System.Drawing.Size(61, 15)
        Me.lblDatabase.TabIndex = 0
        Me.lblDatabase.Text = "Database"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(279, 536)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(171, 26)
        Me.Button1.TabIndex = 33
        Me.Button1.Text = "Generar ODBC"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(597, 536)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(82, 26)
        Me.Button2.TabIndex = 34
        Me.Button2.Text = "Cerrar"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'frmMAIN
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(699, 574)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.grupoSqlServer)
        Me.Controls.Add(Me.grupoOracle)
        Me.Controls.Add(Me.cboControlador)
        Me.Controls.Add(Me.lblControlador)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "frmMAIN"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Generador ODBC"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.grupoOracle.ResumeLayout(False)
        Me.grupoOracle.PerformLayout()
        Me.grupoSqlServer.ResumeLayout(False)
        Me.grupoSqlServer.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblControlador As System.Windows.Forms.Label
    Friend WithEvents cboControlador As System.Windows.Forms.ComboBox
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents ArchivoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GenerarODBCToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SalirToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents grupoOracle As System.Windows.Forms.GroupBox
    Friend WithEvents lblUserID As System.Windows.Forms.Label
    Friend WithEvents lblLongs As System.Windows.Forms.Label
    Friend WithEvents lblTranslationOption As System.Windows.Forms.Label
    Friend WithEvents lblTranslationDLL As System.Windows.Forms.Label
    Friend WithEvents lblSQLGetDataExtensions As System.Windows.Forms.Label
    Friend WithEvents lblServerName As System.Windows.Forms.Label
    Friend WithEvents lblQueryTimeout As System.Windows.Forms.Label
    Friend WithEvents lblResultSets As System.Windows.Forms.Label
    Friend WithEvents lblPrefetchCount As System.Windows.Forms.Label
    Friend WithEvents lblLobs As System.Windows.Forms.Label
    Friend WithEvents lblMetadataIdDefault As System.Windows.Forms.Label
    Friend WithEvents lblFailoverRetryCount As System.Windows.Forms.Label
    Friend WithEvents ForceWCHAR As System.Windows.Forms.Label
    Friend WithEvents lblFailoverDelay As System.Windows.Forms.Label
    Friend WithEvents lblEXECSyntax As System.Windows.Forms.Label
    Friend WithEvents lblFailover As System.Windows.Forms.Label
    Friend WithEvents lblEXECSchemaOpt As System.Windows.Forms.Label
    Friend WithEvents lblDsn As System.Windows.Forms.Label
    Friend WithEvents lblDriver As System.Windows.Forms.Label
    Friend WithEvents lblDisableMTS As System.Windows.Forms.Label
    Friend WithEvents lblDescriptionOracle As System.Windows.Forms.Label
    Friend WithEvents lblCloseCursor As System.Windows.Forms.Label
    Friend WithEvents lblBatchAutocommitMode As System.Windows.Forms.Label
    Friend WithEvents lblAttributes As System.Windows.Forms.Label
    Friend WithEvents lblApplicationAttributes As System.Windows.Forms.Label
    Friend WithEvents grupoSqlServer As System.Windows.Forms.GroupBox
    Friend WithEvents lblServer As System.Windows.Forms.Label
    Friend WithEvents lblLastUser As System.Windows.Forms.Label
    Friend WithEvents lblDriverSQLServer As System.Windows.Forms.Label
    Friend WithEvents lblDescrptionSQLServer As System.Windows.Forms.Label
    Friend WithEvents lblDatabase As System.Windows.Forms.Label
    Friend WithEvents txtServerName As System.Windows.Forms.TextBox
    Friend WithEvents txtApplicationAttributes As System.Windows.Forms.TextBox
    Friend WithEvents txtUserId As System.Windows.Forms.TextBox
    Friend WithEvents txtDsn As System.Windows.Forms.TextBox
    Friend WithEvents txtCloseCursor As System.Windows.Forms.TextBox
    Friend WithEvents txtBatchAutocommitMode As System.Windows.Forms.TextBox
    Friend WithEvents txtAttributes As System.Windows.Forms.TextBox
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtDriver As System.Windows.Forms.TextBox
    Friend WithEvents txtDisableMTS As System.Windows.Forms.TextBox
    Friend WithEvents txtEXECSyntax As System.Windows.Forms.TextBox
    Friend WithEvents txtEXECSchemaOpt As System.Windows.Forms.TextBox
    Friend WithEvents txtLongs As System.Windows.Forms.TextBox
    Friend WithEvents txtLobs As System.Windows.Forms.TextBox
    Friend WithEvents txtForceWCHAR As System.Windows.Forms.TextBox
    Friend WithEvents txtFailoverRetryCount As System.Windows.Forms.TextBox
    Friend WithEvents txtFailoverDelay As System.Windows.Forms.TextBox
    Friend WithEvents txtFailOver As System.Windows.Forms.TextBox
    Friend WithEvents txtTranslationOption As System.Windows.Forms.TextBox
    Friend WithEvents txtTranslationDLL As System.Windows.Forms.TextBox
    Friend WithEvents txtSQLGetDataExtensions As System.Windows.Forms.TextBox
    Friend WithEvents txtResultSets As System.Windows.Forms.TextBox
    Friend WithEvents txtQueryTimeout As System.Windows.Forms.TextBox
    Friend WithEvents txtPrefetchCount As System.Windows.Forms.TextBox
    Friend WithEvents txtMetadataIdDefault As System.Windows.Forms.TextBox
    Friend WithEvents txtDescriptionSQL As System.Windows.Forms.TextBox
    Friend WithEvents txtDriverSQL As System.Windows.Forms.TextBox
    Friend WithEvents txtLastUser As System.Windows.Forms.TextBox
    Friend WithEvents txtDatabase As System.Windows.Forms.TextBox
    Friend WithEvents txtServer As System.Windows.Forms.TextBox
    Friend WithEvents txtDsnSQLServer As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
End Class
