using System.Windows.Forms;
namespace Stardoc.HtmlEditor.Edition
{
  internal  partial class HtmlZTextAreaEditor
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
            this.tbText = new System.Windows.Forms.TextBox();
            this.lbRows = new System.Windows.Forms.Label();
            this.lbColumns = new System.Windows.Forms.Label();
            this.gbSize = new System.Windows.Forms.GroupBox();
            this.nmColumns = new System.Windows.Forms.NumericUpDown();
            this.nmRows = new System.Windows.Forms.NumericUpDown();
            this.btAccept = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.gbSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmColumns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmRows)).BeginInit();
            this.SuspendLayout();
            // 
            // tbText
            // 
            this.tbText.AcceptsReturn = true;
            this.tbText.AcceptsTab = true;
            this.tbText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbText.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbText.Location = new System.Drawing.Point(4, 32);
            this.tbText.Multiline = true;
            this.tbText.Name = "tbText";
            this.tbText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbText.Size = new System.Drawing.Size(576, 261);
            this.tbText.TabIndex = 1;
            this.tbText.WordWrap = false;
            this.tbText.TextChanged += new System.EventHandler(this.tbText_TextChanged);
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
            this.gbSize.Location = new System.Drawing.Point(8, 299);
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
            this.btAccept.Location = new System.Drawing.Point(420, 361);
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
            this.btCancel.Location = new System.Drawing.Point(505, 361);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 8;
            this.btCancel.Text = "Cancelar";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // chkEnabled
            // 
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkEnabled.Location = new System.Drawing.Point(214, 309);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(73, 17);
            this.chkEnabled.TabIndex = 14;
            this.chkEnabled.Text = "Habilitado";
            this.chkEnabled.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkEnabled.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Nombre";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(55, 6);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(153, 20);
            this.tbName.TabIndex = 16;
            // 
            // HtmlZTextAreaEditor
            // 
            this.AcceptButton = this.btAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(592, 395);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkEnabled);
            this.Controls.Add(this.tbText);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btAccept);
            this.Controls.Add(this.gbSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "HtmlZTextAreaEditor";
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

        private System.Windows.Forms.TextBox tbText;
        private System.Windows.Forms.Label lbRows;
        private System.Windows.Forms.Label lbColumns;
        private System.Windows.Forms.GroupBox gbSize;
        private System.Windows.Forms.Button btAccept;
        private System.Windows.Forms.Button btCancel;
        private NumericUpDown nmColumns;
        private NumericUpDown nmRows;
        private CheckBox chkEnabled;
        private Label label1;
        private TextBox tbName;

    }
}