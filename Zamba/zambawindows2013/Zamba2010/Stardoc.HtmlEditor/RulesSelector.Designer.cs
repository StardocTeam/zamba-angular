namespace Stardoc.HtmlEditor
{
    public partial class RulesSelector
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbWorkflows = new System.Windows.Forms.ComboBox();
            this.cmbSteps = new System.Windows.Forms.ComboBox();
            this.lbWorkflows = new System.Windows.Forms.Label();
            this.lbSteps = new System.Windows.Forms.Label();
            this.lvRules = new System.Windows.Forms.ListView();
            this.lbRules = new System.Windows.Forms.Label();
            this.btClose = new System.Windows.Forms.Button();
            this.btReload = new System.Windows.Forms.Button();
            this.chName = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // cmbWorkflows
            // 
            this.cmbWorkflows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbWorkflows.FormattingEnabled = true;
            this.cmbWorkflows.Location = new System.Drawing.Point(75, 6);
            this.cmbWorkflows.Name = "cmbWorkflows";
            this.cmbWorkflows.Size = new System.Drawing.Size(279, 21);
            this.cmbWorkflows.TabIndex = 0;
            this.cmbWorkflows.SelectedIndexChanged += new System.EventHandler(this.cmbWorkflows_SelectedIndexChanged);
            // 
            // cmbSteps
            // 
            this.cmbSteps.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSteps.FormattingEnabled = true;
            this.cmbSteps.Location = new System.Drawing.Point(75, 39);
            this.cmbSteps.Name = "cmbSteps";
            this.cmbSteps.Size = new System.Drawing.Size(279, 21);
            this.cmbSteps.TabIndex = 1;
            this.cmbSteps.SelectedIndexChanged += new System.EventHandler(this.cmbSteps_SelectedIndexChanged);
            // 
            // lbWorkflows
            // 
            this.lbWorkflows.AutoSize = true;
            this.lbWorkflows.Location = new System.Drawing.Point(12, 9);
            this.lbWorkflows.Name = "lbWorkflows";
            this.lbWorkflows.Size = new System.Drawing.Size(57, 13);
            this.lbWorkflows.TabIndex = 2;
            this.lbWorkflows.Text = "Workflows";
            // 
            // lbSteps
            // 
            this.lbSteps.AutoSize = true;
            this.lbSteps.Location = new System.Drawing.Point(12, 42);
            this.lbSteps.Name = "lbSteps";
            this.lbSteps.Size = new System.Drawing.Size(40, 13);
            this.lbSteps.TabIndex = 3;
            this.lbSteps.Text = "Etapas";
            // 
            // lvRules
            // 
            this.lvRules.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lvRules.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName});
            this.lvRules.Location = new System.Drawing.Point(75, 78);
            this.lvRules.Name = "lvRules";
            this.lvRules.Size = new System.Drawing.Size(279, 268);
            this.lvRules.TabIndex = 4;
            this.lvRules.UseCompatibleStateImageBehavior = false;
            this.lvRules.View = System.Windows.Forms.View.Details;
            this.lvRules.SelectedIndexChanged += new System.EventHandler(this.lvRules_SelectedIndexChanged);
            // 
            // lbRules
            // 
            this.lbRules.AutoSize = true;
            this.lbRules.Location = new System.Drawing.Point(12, 78);
            this.lbRules.Name = "lbRules";
            this.lbRules.Size = new System.Drawing.Size(40, 13);
            this.lbRules.TabIndex = 5;
            this.lbRules.Text = "Reglas";
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.Location = new System.Drawing.Point(279, 352);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(75, 23);
            this.btClose.TabIndex = 6;
            this.btClose.Text = "Cerrar";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // btReload
            // 
            this.btReload.Location = new System.Drawing.Point(75, 352);
            this.btReload.Name = "btReload";
            this.btReload.Size = new System.Drawing.Size(75, 23);
            this.btReload.TabIndex = 7;
            this.btReload.Text = "Refrescar";
            this.btReload.UseVisualStyleBackColor = true;
            this.btReload.Click += new System.EventHandler(this.btReload_Click);
            // 
            // chName
            // 
            this.chName.Text = "Nombre";
            this.chName.Width = 200;
            // 
            // RulesSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 391);
            this.Controls.Add(this.btReload);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.lbRules);
            this.Controls.Add(this.lvRules);
            this.Controls.Add(this.lbSteps);
            this.Controls.Add(this.lbWorkflows);
            this.Controls.Add(this.cmbSteps);
            this.Controls.Add(this.cmbWorkflows);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(382, 425);
            this.MinimumSize = new System.Drawing.Size(382, 425);
            this.Name = "RulesSelector";
            this.Text = "Añadir reglas";
            this.Load += new System.EventHandler(this.RulesSelector_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbWorkflows;
        private System.Windows.Forms.ComboBox cmbSteps;
        private System.Windows.Forms.Label lbWorkflows;
        private System.Windows.Forms.Label lbSteps;
        private System.Windows.Forms.ListView lvRules;
        private System.Windows.Forms.Label lbRules;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btReload;
        private System.Windows.Forms.ColumnHeader chName;
    }
}