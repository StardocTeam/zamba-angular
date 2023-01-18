using System.Windows.Forms;

namespace Stardoc.HtmlEditor
{
    internal partial class HtmlTextAreaEditor : Form
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
            this.lbRows = new System.Windows.Forms.Label();
            this.lbColumns = new System.Windows.Forms.Label();
            this.gbSize = new System.Windows.Forms.GroupBox();
            this.nmColumns = new System.Windows.Forms.NumericUpDown();
            this.nmRows = new System.Windows.Forms.NumericUpDown();
            this.btAccept = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.tbText = new System.Windows.Forms.TextBox();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.lbName = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.gbSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmColumns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmRows)).BeginInit();
            this.SuspendLayout();
            // 
            // lbRows
            // 
            this.lbRows.AutoSize = true;
            this.lbRows.Location = new System.Drawing.Point(6, 26);
            this.lbRows.Name = "lbRows";
            this.lbRows.Size = new System.Drawing.Size(85, 13);
            this.lbRows.TabIndex = 3;
            this.lbRows.Text = "Cantidad de filas";
            // 
            // lbColumns
            // 
            this.lbColumns.AutoSize = true;
            this.lbColumns.Location = new System.Drawing.Point(6, 50);
            this.lbColumns.Name = "lbColumns";
            this.lbColumns.Size = new System.Drawing.Size(112, 13);
            this.lbColumns.TabIndex = 5;
            this.lbColumns.Text = "Cantidad de columnas";
            // 
            // gbSize
            // 
            this.gbSize.Controls.Add(this.nmColumns);
            this.gbSize.Controls.Add(this.nmRows);
            this.gbSize.Controls.Add(this.lbRows);
            this.gbSize.Controls.Add(this.lbColumns);
            this.gbSize.Location = new System.Drawing.Point(8, 345);
            this.gbSize.Name = "gbSize";
            this.gbSize.Size = new System.Drawing.Size(200, 85);
            this.gbSize.TabIndex = 2;
            this.gbSize.TabStop = false;
            this.gbSize.Text = "Tamaño";
            // 
            // nmColumns
            // 
            this.nmColumns.Location = new System.Drawing.Point(134, 50);
            this.nmColumns.Name = "nmColumns";
            this.nmColumns.Size = new System.Drawing.Size(42, 20);
            this.nmColumns.TabIndex = 4;
            // 
            // nmRows
            // 
            this.nmRows.Location = new System.Drawing.Point(134, 24);
            this.nmRows.Name = "nmRows";
            this.nmRows.Size = new System.Drawing.Size(42, 20);
            this.nmRows.TabIndex = 3;
            // 
            // btAccept
            // 
            this.btAccept.Location = new System.Drawing.Point(442, 407);
            this.btAccept.Name = "btAccept";
            this.btAccept.Size = new System.Drawing.Size(75, 23);
            this.btAccept.TabIndex = 7;
            this.btAccept.Text = "Aceptar";
            this.btAccept.UseVisualStyleBackColor = true;
            this.btAccept.Click += new System.EventHandler(this.btAccept_Click);
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(527, 407);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 8;
            this.btCancel.Text = "Cancelar";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // tbText
            // 
            this.tbText.AcceptsReturn = true;
            this.tbText.AcceptsTab = true;
            this.tbText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbText.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbText.Location = new System.Drawing.Point(8, 32);
            this.tbText.Multiline = true;
            this.tbText.Name = "tbText";
            this.tbText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbText.Size = new System.Drawing.Size(594, 307);
            this.tbText.TabIndex = 1;
            this.tbText.WordWrap = false;
            this.tbText.TextChanged += new System.EventHandler(this.tbText_TextChanged);
            // 
            // chkEnabled
            // 
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkEnabled.Location = new System.Drawing.Point(214, 354);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(73, 17);
            this.chkEnabled.TabIndex = 12;
            this.chkEnabled.Text = "Habilitado";
            this.chkEnabled.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkEnabled.UseVisualStyleBackColor = true;
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Location = new System.Drawing.Point(5, 9);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(47, 13);
            this.lbName.TabIndex = 13;
            this.lbName.Text = "Nombre:";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(58, 6);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(150, 20);
            this.tbName.TabIndex = 14;
            // 
            // HtmlTextAreaEditor
            // 
            this.AcceptButton = this.btAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(610, 441);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.chkEnabled);
            this.Controls.Add(this.tbText);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btAccept);
            this.Controls.Add(this.gbSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "HtmlTextAreaEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edición elemento TEXTAREA";
            this.Load += new System.EventHandler(this.TextAreaEditor_Load);
            this.gbSize.ResumeLayout(false);
            this.gbSize.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmColumns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmRows)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbRows;
        private System.Windows.Forms.Label lbColumns;
        private System.Windows.Forms.GroupBox gbSize;
        private System.Windows.Forms.Button btAccept;
        private System.Windows.Forms.Button btCancel;
        private NumericUpDown nmColumns;
        private NumericUpDown nmRows;
        private TextBox tbText;
        private CheckBox chkEnabled;
        private Label lbName;
        private TextBox tbName;


    }
}