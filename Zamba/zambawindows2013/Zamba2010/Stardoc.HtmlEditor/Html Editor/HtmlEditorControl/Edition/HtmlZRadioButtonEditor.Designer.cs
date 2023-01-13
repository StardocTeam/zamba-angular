namespace Stardoc.HtmlEditor.Edition
{
    internal partial class HtmlZRadioButtonEditor
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
            this.lbText = new System.Windows.Forms.Label();
            this.lbCategoria = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbCategory = new System.Windows.Forms.TextBox();
            this.btCancel = new System.Windows.Forms.Button();
            this.btAccept = new System.Windows.Forms.Button();
            this.chkChecked = new System.Windows.Forms.CheckBox();
            this.lbYesNo = new System.Windows.Forms.Label();
            this.rdbYes = new System.Windows.Forms.RadioButton();
            this.rdbNo = new System.Windows.Forms.RadioButton();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.chkRequired = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lbText
            // 
            this.lbText.AutoSize = true;
            this.lbText.Location = new System.Drawing.Point(16, 15);
            this.lbText.Name = "lbText";
            this.lbText.Size = new System.Drawing.Size(37, 13);
            this.lbText.TabIndex = 0;
            this.lbText.Text = "Texto:";
            // 
            // lbCategoria
            // 
            this.lbCategoria.AutoSize = true;
            this.lbCategoria.Location = new System.Drawing.Point(16, 41);
            this.lbCategoria.Name = "lbCategoria";
            this.lbCategoria.Size = new System.Drawing.Size(55, 13);
            this.lbCategoria.TabIndex = 2;
            this.lbCategoria.Text = "Categoria:";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(95, 12);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(225, 20);
            this.tbName.TabIndex = 1;
            // 
            // tbCategory
            // 
            this.tbCategory.Location = new System.Drawing.Point(95, 38);
            this.tbCategory.Name = "tbCategory";
            this.tbCategory.Size = new System.Drawing.Size(225, 20);
            this.tbCategory.TabIndex = 2;
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(245, 111);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 4;
            this.btCancel.Text = "Cancelar";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btAccept
            // 
            this.btAccept.Location = new System.Drawing.Point(164, 111);
            this.btAccept.Name = "btAccept";
            this.btAccept.Size = new System.Drawing.Size(75, 23);
            this.btAccept.TabIndex = 3;
            this.btAccept.Text = "Aceptar";
            this.btAccept.UseVisualStyleBackColor = true;
            this.btAccept.Click += new System.EventHandler(this.btAccept_Click);
            // 
            // chkChecked
            // 
            this.chkChecked.AutoSize = true;
            this.chkChecked.Location = new System.Drawing.Point(19, 87);
            this.chkChecked.Name = "chkChecked";
            this.chkChecked.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkChecked.Size = new System.Drawing.Size(91, 17);
            this.chkChecked.TabIndex = 5;
            this.chkChecked.Text = "Seleccionado";
            this.chkChecked.UseVisualStyleBackColor = true;
            // 
            // lbYesNo
            // 
            this.lbYesNo.AutoSize = true;
            this.lbYesNo.Location = new System.Drawing.Point(16, 66);
            this.lbYesNo.Name = "lbYesNo";
            this.lbYesNo.Size = new System.Drawing.Size(31, 13);
            this.lbYesNo.TabIndex = 12;
            this.lbYesNo.Text = "Valor";
            // 
            // rdbYes
            // 
            this.rdbYes.AutoSize = true;
            this.rdbYes.Checked = true;
            this.rdbYes.Location = new System.Drawing.Point(95, 64);
            this.rdbYes.Name = "rdbYes";
            this.rdbYes.Size = new System.Drawing.Size(34, 17);
            this.rdbYes.TabIndex = 13;
            this.rdbYes.TabStop = true;
            this.rdbYes.Text = "Si";
            this.rdbYes.UseVisualStyleBackColor = true;
            // 
            // rdbNo
            // 
            this.rdbNo.AutoSize = true;
            this.rdbNo.Location = new System.Drawing.Point(135, 64);
            this.rdbNo.Name = "rdbNo";
            this.rdbNo.Size = new System.Drawing.Size(39, 17);
            this.rdbNo.TabIndex = 14;
            this.rdbNo.TabStop = true;
            this.rdbNo.Text = "No";
            this.rdbNo.UseVisualStyleBackColor = true;
            // 
            // chkEnabled
            // 
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkEnabled.Location = new System.Drawing.Point(116, 87);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(73, 17);
            this.chkEnabled.TabIndex = 15;
            this.chkEnabled.Text = "Habilitado";
            this.chkEnabled.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkEnabled.UseVisualStyleBackColor = true;
            // 
            // chkRequired
            // 
            this.chkRequired.AutoSize = true;
            this.chkRequired.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkRequired.Location = new System.Drawing.Point(195, 87);
            this.chkRequired.Name = "chkRequired";
            this.chkRequired.Size = new System.Drawing.Size(75, 17);
            this.chkRequired.TabIndex = 16;
            this.chkRequired.Text = "Requerido";
            this.chkRequired.UseVisualStyleBackColor = true;
            // 
            // HtmlZRadioButtonEditor
            // 
            this.AcceptButton = this.btAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(332, 145);
            this.Controls.Add(this.chkRequired);
            this.Controls.Add(this.chkEnabled);
            this.Controls.Add(this.rdbNo);
            this.Controls.Add(this.rdbYes);
            this.Controls.Add(this.lbYesNo);
            this.Controls.Add(this.chkChecked);
            this.Controls.Add(this.btAccept);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.tbCategory);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.lbCategoria);
            this.Controls.Add(this.lbText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "HtmlZRadioButtonEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edición del elemento Radio Button";
            this.Load += new System.EventHandler(this.HtmlZRadioButtonEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbText;
        private System.Windows.Forms.Label lbCategoria;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbCategory;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btAccept;
        private System.Windows.Forms.CheckBox chkChecked;
        private System.Windows.Forms.Label lbYesNo;
        private System.Windows.Forms.RadioButton rdbYes;
        private System.Windows.Forms.RadioButton rdbNo;
        private System.Windows.Forms.CheckBox chkEnabled;
        private System.Windows.Forms.CheckBox chkRequired;
    }
}