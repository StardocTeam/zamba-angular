using System;
using System.Windows.Forms;
namespace Stardoc.HtmlEditor
{
    internal partial class HtmlOptionEditor : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btAccept = new System.Windows.Forms.Button();
            this.tbValue = new System.Windows.Forms.TextBox();
            this.btCancel = new System.Windows.Forms.Button();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lbName = new System.Windows.Forms.Label();
            this.lbValue = new System.Windows.Forms.Label();
            this.chkSelected = new System.Windows.Forms.CheckBox();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btAccept
            // 
            this.btAccept.Location = new System.Drawing.Point(168, 92);
            this.btAccept.Name = "btAccept";
            this.btAccept.Size = new System.Drawing.Size(75, 23);
            this.btAccept.TabIndex = 6;
            this.btAccept.Text = "Aceptar";
            this.btAccept.UseVisualStyleBackColor = true;
            this.btAccept.Click += new System.EventHandler(this.btAccept_Click);
            // 
            // tbValue
            // 
            this.tbValue.Location = new System.Drawing.Point(89, 41);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(235, 20);
            this.tbValue.TabIndex = 4;
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(249, 92);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 7;
            this.btCancel.Text = "Cancelar";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(89, 15);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(235, 20);
            this.tbName.TabIndex = 2;
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Location = new System.Drawing.Point(13, 18);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(47, 13);
            this.lbName.TabIndex = 1;
            this.lbName.Text = "Nombre:";
            // 
            // lbValue
            // 
            this.lbValue.AutoSize = true;
            this.lbValue.Location = new System.Drawing.Point(13, 44);
            this.lbValue.Name = "lbValue";
            this.lbValue.Size = new System.Drawing.Size(34, 13);
            this.lbValue.TabIndex = 3;
            this.lbValue.Text = "Valor:";
            // 
            // chkSelected
            // 
            this.chkSelected.AutoSize = true;
            this.chkSelected.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSelected.Location = new System.Drawing.Point(12, 67);
            this.chkSelected.Name = "chkSelected";
            this.chkSelected.Size = new System.Drawing.Size(91, 17);
            this.chkSelected.TabIndex = 5;
            this.chkSelected.Text = "Seleccionado";
            this.chkSelected.UseVisualStyleBackColor = true;
            // 
            // chkEnabled
            // 
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkEnabled.Location = new System.Drawing.Point(109, 67);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(73, 17);
            this.chkEnabled.TabIndex = 8;
            this.chkEnabled.Text = "Habilitado";
            this.chkEnabled.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkEnabled.UseVisualStyleBackColor = true;
            // 
            // HtmlOptionEditor
            // 
            this.AcceptButton = this.btAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(345, 128);
            this.Controls.Add(this.chkEnabled);
            this.Controls.Add(this.chkSelected);
            this.Controls.Add(this.lbValue);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.tbValue);
            this.Controls.Add(this.btAccept);
            this.Name = "HtmlOptionEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edición elemento OPTION";
            this.Load += new System.EventHandler(this.AddSelectItem_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btAccept;
        private TextBox tbValue;
        private Button btCancel;
        private TextBox tbName;
        private Label lbName;
        private Label lbValue;
        private CheckBox chkSelected;
        private CheckBox chkEnabled;
    }
}