using Zamba.AppBlock;

namespace Zamba.Query
{
    partial class FrmMain
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.WinControls.Data.GroupDescriptor groupDescriptor1 = new Telerik.WinControls.Data.GroupDescriptor();
            Telerik.WinControls.Data.SortDescriptor sortDescriptor1 = new Telerik.WinControls.Data.SortDescriptor();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.Tabs = new System.Windows.Forms.TabControl();
            this.txtQuery = new System.Windows.Forms.TextBox();
            this.btn_EjecutarQuery = new Zamba.AppBlock.ZButton();
            this.txtMensajes = new System.Windows.Forms.TextBox();
            this.tabOptions = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chkContinueIfError = new System.Windows.Forms.CheckBox();
            this.btnCleanQuery = new Zamba.AppBlock.ZButton();
            this.btnCleanLog = new Zamba.AppBlock.ZButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.cboServerType = new System.Windows.Forms.ComboBox();
            this.cmbODBC = new System.Windows.Forms.ComboBox();
            this.lblServerType = new System.Windows.Forms.LinkLabel();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblServerName = new System.Windows.Forms.LinkLabel();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.lblDatabase = new System.Windows.Forms.LinkLabel();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.lblUser = new System.Windows.Forms.LinkLabel();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.LinkLabel();
            this.chkUseAppIni = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.rgvLog = new Telerik.WinControls.UI.RadGridView();
            this.radSplitContainer1 = new Telerik.WinControls.UI.RadSplitContainer();
            this.splitPanel1 = new Telerik.WinControls.UI.SplitPanel();
            this.splitPanel2 = new Telerik.WinControls.UI.SplitPanel();
            this.splitPanel3 = new Telerik.WinControls.UI.SplitPanel();
            this.tabOptions.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgvLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvLog.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel3)).BeginInit();
            this.SuspendLayout();
            // 
            // Tabs
            // 
            this.Tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tabs.Location = new System.Drawing.Point(0, 0);
            this.Tabs.Margin = new System.Windows.Forms.Padding(4);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(1091, 234);
            this.Tabs.TabIndex = 0;
            // 
            // txtQuery
            // 
            this.txtQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtQuery.Location = new System.Drawing.Point(0, 0);
            this.txtQuery.Margin = new System.Windows.Forms.Padding(4);
            this.txtQuery.MaxLength = 5000000;
            this.txtQuery.Multiline = true;
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtQuery.Size = new System.Drawing.Size(1091, 234);
            this.txtQuery.TabIndex = 1;
            this.txtQuery.TextChanged += new System.EventHandler(this.txtQuery_TextChanged);
            // 
            // btn_EjecutarQuery
            // 
            this.btn_EjecutarQuery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(157)))), ((int)(((byte)(224)))));
            this.btn_EjecutarQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_EjecutarQuery.ForeColor = System.Drawing.Color.White;
            this.btn_EjecutarQuery.Location = new System.Drawing.Point(36, 22);
            this.btn_EjecutarQuery.Margin = new System.Windows.Forms.Padding(4);
            this.btn_EjecutarQuery.Name = "btn_EjecutarQuery";
            this.btn_EjecutarQuery.Size = new System.Drawing.Size(681, 33);
            this.btn_EjecutarQuery.TabIndex = 2;
            this.btn_EjecutarQuery.Text = "Ejecutar";
            this.btn_EjecutarQuery.UseVisualStyleBackColor = true;
            this.btn_EjecutarQuery.Click += new System.EventHandler(this.btn_EjecutarQuery_Click);
            // 
            // txtMensajes
            // 
            this.txtMensajes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMensajes.ForeColor = System.Drawing.Color.Red;
            this.txtMensajes.Location = new System.Drawing.Point(4, 4);
            this.txtMensajes.Margin = new System.Windows.Forms.Padding(4);
            this.txtMensajes.Multiline = true;
            this.txtMensajes.Name = "txtMensajes";
            this.txtMensajes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMensajes.Size = new System.Drawing.Size(1075, 196);
            this.txtMensajes.TabIndex = 4;
            this.txtMensajes.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // tabOptions
            // 
            this.tabOptions.Controls.Add(this.tabPage1);
            this.tabOptions.Controls.Add(this.tabPage2);
            this.tabOptions.Controls.Add(this.tabPage3);
            this.tabOptions.Controls.Add(this.tabPage4);
            this.tabOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabOptions.Location = new System.Drawing.Point(0, 0);
            this.tabOptions.Margin = new System.Windows.Forms.Padding(4);
            this.tabOptions.Name = "tabOptions";
            this.tabOptions.SelectedIndex = 0;
            this.tabOptions.Size = new System.Drawing.Size(1091, 233);
            this.tabOptions.TabIndex = 38;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chkContinueIfError);
            this.tabPage1.Controls.Add(this.btn_EjecutarQuery);
            this.tabPage1.Controls.Add(this.btnCleanQuery);
            this.tabPage1.Controls.Add(this.btnCleanLog);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(1083, 204);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Inicio";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // chkContinueIfError
            // 
            this.chkContinueIfError.AutoSize = true;
            this.chkContinueIfError.Location = new System.Drawing.Point(36, 76);
            this.chkContinueIfError.Margin = new System.Windows.Forms.Padding(4);
            this.chkContinueIfError.Name = "chkContinueIfError";
            this.chkContinueIfError.Size = new System.Drawing.Size(216, 17);
            this.chkContinueIfError.TabIndex = 9;
            this.chkContinueIfError.Text = "Continuar ejecución en caso de error";
            this.chkContinueIfError.UseVisualStyleBackColor = true;
            // 
            // btnCleanQuery
            // 
            this.btnCleanQuery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(157)))), ((int)(((byte)(224)))));
            this.btnCleanQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCleanQuery.ForeColor = System.Drawing.Color.White;
            this.btnCleanQuery.Location = new System.Drawing.Point(36, 123);
            this.btnCleanQuery.Margin = new System.Windows.Forms.Padding(4);
            this.btnCleanQuery.Name = "btnCleanQuery";
            this.btnCleanQuery.Size = new System.Drawing.Size(208, 28);
            this.btnCleanQuery.TabIndex = 7;
            this.btnCleanQuery.Text = "Limpiar consulta";
            this.btnCleanQuery.UseVisualStyleBackColor = true;
            this.btnCleanQuery.Click += new System.EventHandler(this.btnCleanQuery_Click);
            // 
            // btnCleanLog
            // 
            this.btnCleanLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(157)))), ((int)(((byte)(224)))));
            this.btnCleanLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCleanLog.ForeColor = System.Drawing.Color.White;
            this.btnCleanLog.Location = new System.Drawing.Point(272, 123);
            this.btnCleanLog.Margin = new System.Windows.Forms.Padding(4);
            this.btnCleanLog.Name = "btnCleanLog";
            this.btnCleanLog.Size = new System.Drawing.Size(208, 28);
            this.btnCleanLog.TabIndex = 8;
            this.btnCleanLog.Text = "Limpiar resultado";
            this.btnCleanLog.UseVisualStyleBackColor = true;
            this.btnCleanLog.Click += new System.EventHandler(this.btnCleanLog_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.cboServerType);
            this.tabPage2.Controls.Add(this.cmbODBC);
            this.tabPage2.Controls.Add(this.lblServerType);
            this.tabPage2.Controls.Add(this.txtPassword);
            this.tabPage2.Controls.Add(this.lblServerName);
            this.tabPage2.Controls.Add(this.txtUser);
            this.tabPage2.Controls.Add(this.lblDatabase);
            this.tabPage2.Controls.Add(this.txtDatabase);
            this.tabPage2.Controls.Add(this.lblUser);
            this.tabPage2.Controls.Add(this.txtServerName);
            this.tabPage2.Controls.Add(this.lblPassword);
            this.tabPage2.Controls.Add(this.chkUseAppIni);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(1083, 204);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Conexión";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // cboServerType
            // 
            this.cboServerType.FormattingEnabled = true;
            this.cboServerType.Location = new System.Drawing.Point(105, 7);
            this.cboServerType.Margin = new System.Windows.Forms.Padding(4);
            this.cboServerType.Name = "cboServerType";
            this.cboServerType.Size = new System.Drawing.Size(352, 24);
            this.cboServerType.TabIndex = 32;
            this.cboServerType.SelectedIndexChanged += new System.EventHandler(this.cboServerType_SelectedIndexChanged);
            // 
            // cmbODBC
            // 
            this.cmbODBC.FormattingEnabled = true;
            this.cmbODBC.Location = new System.Drawing.Point(105, 37);
            this.cmbODBC.Margin = new System.Windows.Forms.Padding(4);
            this.cmbODBC.Name = "cmbODBC";
            this.cmbODBC.Size = new System.Drawing.Size(352, 24);
            this.cmbODBC.TabIndex = 37;
            this.cmbODBC.Visible = false;
            this.cmbODBC.SelectedIndexChanged += new System.EventHandler(this.cmbODBC_SelectedIndexChanged);
            // 
            // lblServerType
            // 
            this.lblServerType.AutoSize = true;
            this.lblServerType.BackColor = System.Drawing.Color.Transparent;
            this.lblServerType.Location = new System.Drawing.Point(13, 15);
            this.lblServerType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblServerType.Name = "lblServerType";
            this.lblServerType.Size = new System.Drawing.Size(63, 13);
            this.lblServerType.TabIndex = 27;
            this.lblServerType.TabStop = true;
            this.lblServerType.Text = "Server Type";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(541, 74);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '@';
            this.txtPassword.Size = new System.Drawing.Size(185, 23);
            this.txtPassword.TabIndex = 36;
            // 
            // lblServerName
            // 
            this.lblServerName.AutoSize = true;
            this.lblServerName.BackColor = System.Drawing.Color.Transparent;
            this.lblServerName.Location = new System.Drawing.Point(13, 43);
            this.lblServerName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblServerName.Name = "lblServerName";
            this.lblServerName.Size = new System.Drawing.Size(70, 13);
            this.lblServerName.TabIndex = 28;
            this.lblServerName.TabStop = true;
            this.lblServerName.Text = "Server Name";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(541, 39);
            this.txtUser.Margin = new System.Windows.Forms.Padding(4);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(185, 23);
            this.txtUser.TabIndex = 35;
            // 
            // lblDatabase
            // 
            this.lblDatabase.AutoSize = true;
            this.lblDatabase.BackColor = System.Drawing.Color.Transparent;
            this.lblDatabase.Location = new System.Drawing.Point(13, 74);
            this.lblDatabase.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(55, 13);
            this.lblDatabase.TabIndex = 29;
            this.lblDatabase.TabStop = true;
            this.lblDatabase.Text = "Database";
            // 
            // txtDatabase
            // 
            this.txtDatabase.Location = new System.Drawing.Point(105, 70);
            this.txtDatabase.Margin = new System.Windows.Forms.Padding(4);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(352, 23);
            this.txtDatabase.TabIndex = 34;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.BackColor = System.Drawing.Color.Transparent;
            this.lblUser.Location = new System.Drawing.Point(467, 47);
            this.lblUser.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(30, 13);
            this.lblUser.TabIndex = 30;
            this.lblUser.TabStop = true;
            this.lblUser.Text = "User";
            // 
            // txtServerName
            // 
            this.txtServerName.Location = new System.Drawing.Point(105, 37);
            this.txtServerName.Margin = new System.Windows.Forms.Padding(4);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(352, 23);
            this.txtServerName.TabIndex = 33;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.BackColor = System.Drawing.Color.Transparent;
            this.lblPassword.Location = new System.Drawing.Point(463, 78);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.TabIndex = 31;
            this.lblPassword.TabStop = true;
            this.lblPassword.Text = "Password";
            // 
            // chkUseAppIni
            // 
            this.chkUseAppIni.BackColor = System.Drawing.Color.Transparent;
            this.chkUseAppIni.Location = new System.Drawing.Point(487, 11);
            this.chkUseAppIni.Margin = new System.Windows.Forms.Padding(4);
            this.chkUseAppIni.Name = "chkUseAppIni";
            this.chkUseAppIni.Size = new System.Drawing.Size(223, 25);
            this.chkUseAppIni.TabIndex = 26;
            this.chkUseAppIni.Text = "Usar Configuracion Existente";
            this.chkUseAppIni.UseVisualStyleBackColor = false;
            this.chkUseAppIni.CheckedChanged += new System.EventHandler(this.chkUseAppIni_CheckedChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txtMensajes);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage3.Size = new System.Drawing.Size(1083, 204);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Log: texto";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.rgvLog);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage4.Size = new System.Drawing.Size(1083, 204);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Log: grilla";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // rgvLog
            // 
            this.rgvLog.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.rgvLog.BeginEditMode = Telerik.WinControls.RadGridViewBeginEditMode.BeginEditOnEnter;
            this.rgvLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rgvLog.Location = new System.Drawing.Point(4, 4);
            this.rgvLog.Margin = new System.Windows.Forms.Padding(4);
            // 
            // 
            // 
            this.rgvLog.MasterTemplate.AllowAddNewRow = false;
            this.rgvLog.MasterTemplate.AllowDeleteRow = false;
            this.rgvLog.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.rgvLog.MasterTemplate.EnableAlternatingRowColor = true;
            this.rgvLog.MasterTemplate.EnableFiltering = true;
            sortDescriptor1.PropertyName = "ExecutionId";
            groupDescriptor1.GroupNames.AddRange(new Telerik.WinControls.Data.SortDescriptor[] {
            sortDescriptor1});
            this.rgvLog.MasterTemplate.GroupDescriptors.AddRange(new Telerik.WinControls.Data.GroupDescriptor[] {
            groupDescriptor1});
            this.rgvLog.MasterTemplate.MultiSelect = true;
            this.rgvLog.MasterTemplate.ShowGroupedColumns = true;
            this.rgvLog.MasterTemplate.ShowRowHeaderColumn = false;
            this.rgvLog.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.rgvLog.Name = "rgvLog";
            this.rgvLog.NewRowEnterKeyMode = Telerik.WinControls.UI.RadGridViewNewRowEnterKeyMode.None;
            // 
            // 
            // 
            this.rgvLog.RootElement.ControlBounds = new System.Drawing.Rectangle(4, 4, 240, 150);
            this.rgvLog.ShowCellErrors = false;
            this.rgvLog.ShowRowErrors = false;
            this.rgvLog.Size = new System.Drawing.Size(1075, 196);
            this.rgvLog.TabIndex = 0;
            this.rgvLog.GroupByChanged += new Telerik.WinControls.UI.GridViewCollectionChangedEventHandler(this.rgvLog_GroupByChanged);
            // 
            // radSplitContainer1
            // 
            this.radSplitContainer1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.radSplitContainer1.Controls.Add(this.splitPanel1);
            this.radSplitContainer1.Controls.Add(this.splitPanel2);
            this.radSplitContainer1.Controls.Add(this.splitPanel3);
            this.radSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radSplitContainer1.Location = new System.Drawing.Point(3, 2);
            this.radSplitContainer1.Name = "radSplitContainer1";
            this.radSplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // 
            // 
            this.radSplitContainer1.RootElement.ControlBounds = new System.Drawing.Rectangle(3, 2, 200, 200);
            this.radSplitContainer1.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.radSplitContainer1.Size = new System.Drawing.Size(1091, 709);
            this.radSplitContainer1.TabIndex = 39;
            this.radSplitContainer1.TabStop = false;
            this.radSplitContainer1.Text = "radSplitContainer1";
            // 
            // splitPanel1
            // 
            this.splitPanel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.splitPanel1.Controls.Add(this.txtQuery);
            this.splitPanel1.Location = new System.Drawing.Point(0, 0);
            this.splitPanel1.Name = "splitPanel1";
            // 
            // 
            // 
            this.splitPanel1.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 200, 200);
            this.splitPanel1.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.splitPanel1.Size = new System.Drawing.Size(1091, 234);
            this.splitPanel1.TabIndex = 0;
            this.splitPanel1.TabStop = false;
            this.splitPanel1.Text = "splitPanel1";
            // 
            // splitPanel2
            // 
            this.splitPanel2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.splitPanel2.Controls.Add(this.Tabs);
            this.splitPanel2.Location = new System.Drawing.Point(0, 238);
            this.splitPanel2.Name = "splitPanel2";
            // 
            // 
            // 
            this.splitPanel2.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 238, 200, 200);
            this.splitPanel2.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.splitPanel2.Size = new System.Drawing.Size(1091, 234);
            this.splitPanel2.TabIndex = 1;
            this.splitPanel2.TabStop = false;
            this.splitPanel2.Text = "splitPanel2";
            // 
            // splitPanel3
            // 
            this.splitPanel3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.splitPanel3.Controls.Add(this.tabOptions);
            this.splitPanel3.Location = new System.Drawing.Point(0, 476);
            this.splitPanel3.Name = "splitPanel3";
            // 
            // 
            // 
            this.splitPanel3.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 476, 200, 200);
            this.splitPanel3.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.splitPanel3.Size = new System.Drawing.Size(1091, 233);
            this.splitPanel3.TabIndex = 2;
            this.splitPanel3.TabStop = false;
            this.splitPanel3.Text = "splitPanel3";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1097, 713);
            this.Controls.Add(this.radSplitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmMain";
            this.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Text = "Zamba Query";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.tabOptions.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rgvLog.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainer1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl Tabs;
        private System.Windows.Forms.TextBox txtQuery;
        private ZButton btn_EjecutarQuery;
        private System.Windows.Forms.TextBox txtMensajes;
        private ZButton btnCleanLog;
        private ZButton btnCleanQuery;
        internal System.Windows.Forms.ComboBox cmbODBC;
        internal System.Windows.Forms.TextBox txtPassword;
        internal System.Windows.Forms.TextBox txtUser;
        internal System.Windows.Forms.TextBox txtDatabase;
        internal System.Windows.Forms.TextBox txtServerName;
        internal System.Windows.Forms.ComboBox cboServerType;
        internal System.Windows.Forms.CheckBox chkUseAppIni;
        internal System.Windows.Forms.LinkLabel lblPassword;
        internal System.Windows.Forms.LinkLabel lblUser;
        internal System.Windows.Forms.LinkLabel lblDatabase;
        internal System.Windows.Forms.LinkLabel lblServerName;
        internal System.Windows.Forms.LinkLabel lblServerType;
        private System.Windows.Forms.TabControl tabOptions;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private Telerik.WinControls.UI.RadGridView rgvLog;
        private Telerik.WinControls.UI.RadSplitContainer radSplitContainer1;
        private Telerik.WinControls.UI.SplitPanel splitPanel1;
        private Telerik.WinControls.UI.SplitPanel splitPanel2;
        private Telerik.WinControls.UI.SplitPanel splitPanel3;
        private System.Windows.Forms.CheckBox chkContinueIfError;
    }
}

