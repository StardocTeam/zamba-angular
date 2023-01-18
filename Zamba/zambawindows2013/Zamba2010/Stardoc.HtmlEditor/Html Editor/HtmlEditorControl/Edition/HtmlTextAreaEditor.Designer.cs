using System.Windows.Forms;

internal partial class HtmlTextAreaEditor
    : Form
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
        this.lbRows = new Label();
        this.lbColumns = new Label();
        this.gbSize = new GroupBox();
        this.nmColumns = new NumericUpDown();
        this.nmRows = new NumericUpDown();
        this.btAccept = new Button();
        this.btCancel = new Button();
        this.tbText = new TextBox();
        this.chkEnabled = new CheckBox();
        this.chkRequired = new CheckBox();
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
        this.nmColumns.Value = (decimal)DEFAULT_COLUMN_COUNT;
        this.nmColumns.Size = new System.Drawing.Size(42, 20);
        this.nmColumns.TabIndex = 4;
        // 
        // nmRows
        // 
        this.nmRows.Location = new System.Drawing.Point(134, 24);
        this.nmRows.Value = (decimal)DEFAULT_ROW_COUNT;
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
        this.btCancel.DialogResult = DialogResult.Cancel;
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
        this.tbText.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left)
                    | AnchorStyles.Right)));
        this.tbText.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.tbText.Location = new System.Drawing.Point(8, 12);
        this.tbText.Multiline = true;
        this.tbText.Name = "tbText";
        this.tbText.ScrollBars = ScrollBars.Vertical;
        this.tbText.Size = new System.Drawing.Size(594, 327);
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
        // chkRequired
        // 
        this.chkRequired.AutoSize = true;
        this.chkRequired.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.chkRequired.Location = new System.Drawing.Point(293, 354);
        this.chkRequired.Name = "chkRequired";
        this.chkRequired.Size = new System.Drawing.Size(75, 17);
        this.chkRequired.TabIndex = 13;
        this.chkRequired.Text = "Requerido";
        this.chkRequired.UseVisualStyleBackColor = true;
        // 
        // HtmlTextAreaEditor
        // 
        this.AcceptButton = this.btAccept;
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.CancelButton = this.btCancel;
        this.ClientSize = new System.Drawing.Size(610, 441);
        this.Controls.Add(this.chkRequired);
        this.Controls.Add(this.chkEnabled);
        this.Controls.Add(this.tbText);
        this.Controls.Add(this.btCancel);
        this.Controls.Add(this.btAccept);
        this.Controls.Add(this.gbSize);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.Name = "HtmlTextAreaEditor";
        this.StartPosition = FormStartPosition.CenterParent;
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

    protected Label lbRows;
    protected Label lbColumns;
    protected GroupBox gbSize;
    protected Button btAccept;
    protected Button btCancel;
    protected NumericUpDown nmColumns;
    protected NumericUpDown nmRows;
    protected TextBox tbText;
    protected CheckBox chkEnabled;
    protected CheckBox chkRequired;
}
