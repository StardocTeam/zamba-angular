using System.Windows.Forms;
internal partial class HtmlTextBox
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
        this.lbValor = new System.Windows.Forms.Label();
        this.tbValue = new System.Windows.Forms.TextBox();
        this.btCancel = new System.Windows.Forms.Button();
        this.btAccept = new System.Windows.Forms.Button();
        this.chkEnabled = new System.Windows.Forms.CheckBox();
        this.chkReadOnly = new System.Windows.Forms.CheckBox();
        this.lbName = new System.Windows.Forms.Label();
        this.tbName = new System.Windows.Forms.TextBox();
        this.chkRequired = new System.Windows.Forms.CheckBox();
        this.lbDataType = new System.Windows.Forms.Label();
        this.cbDataTypes = new System.Windows.Forms.ComboBox();
        this.SuspendLayout();
        // 
        // lbValor
        // 
        this.lbValor.AutoSize = true;
        this.lbValor.Location = new System.Drawing.Point(12, 36);
        this.lbValor.Name = "lbValor";
        this.lbValor.Size = new System.Drawing.Size(34, 13);
        this.lbValor.TabIndex = 8;
        this.lbValor.Text = "Valor:";
        // 
        // tbValue
        // 
        this.tbValue.Location = new System.Drawing.Point(71, 33);
        this.tbValue.Name = "tbValue";
        this.tbValue.Size = new System.Drawing.Size(260, 20);
        this.tbValue.TabIndex = 2;
        // 
        // btCancel
        // 
        this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.btCancel.Location = new System.Drawing.Point(256, 122);
        this.btCancel.Name = "btCancel";
        this.btCancel.Size = new System.Drawing.Size(75, 23);
        this.btCancel.TabIndex = 6;
        this.btCancel.Text = "Cancelar";
        this.btCancel.UseVisualStyleBackColor = true;
        this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
        // 
        // btAccept
        // 
        this.btAccept.Location = new System.Drawing.Point(175, 122);
        this.btAccept.Name = "btAccept";
        this.btAccept.Size = new System.Drawing.Size(75, 23);
        this.btAccept.TabIndex = 5;
        this.btAccept.Text = "Aceptar";
        this.btAccept.UseVisualStyleBackColor = true;
        this.btAccept.Click += new System.EventHandler(this.btAccept_Click);
        // 
        // chkEnabled
        // 
        this.chkEnabled.AutoSize = true;
        this.chkEnabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.chkEnabled.Location = new System.Drawing.Point(12, 90);
        this.chkEnabled.Name = "chkEnabled";
        this.chkEnabled.Size = new System.Drawing.Size(73, 17);
        this.chkEnabled.TabIndex = 3;
        this.chkEnabled.Text = "Habilitado";
        this.chkEnabled.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.chkEnabled.UseVisualStyleBackColor = true;
        // 
        // chkReadOnly
        // 
        this.chkReadOnly.AutoSize = true;
        this.chkReadOnly.CheckAlign = System.Drawing.ContentAlignment.BottomRight;
        this.chkReadOnly.Location = new System.Drawing.Point(91, 90);
        this.chkReadOnly.Name = "chkReadOnly";
        this.chkReadOnly.Size = new System.Drawing.Size(86, 17);
        this.chkReadOnly.TabIndex = 4;
        this.chkReadOnly.Text = "Solo Lectura";
        this.chkReadOnly.UseVisualStyleBackColor = true;
        // 
        // lbName
        // 
        this.lbName.AutoSize = true;
        this.lbName.Location = new System.Drawing.Point(12, 10);
        this.lbName.Name = "lbName";
        this.lbName.Size = new System.Drawing.Size(47, 13);
        this.lbName.TabIndex = 7;
        this.lbName.Text = "Nombre:";
        // 
        // tbName
        // 
        this.tbName.Location = new System.Drawing.Point(71, 7);
        this.tbName.Name = "tbName";
        this.tbName.Size = new System.Drawing.Size(260, 20);
        this.tbName.TabIndex = 1;
        // 
        // chkRequired
        // 
        this.chkRequired.AutoSize = true;
        this.chkRequired.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.chkRequired.Location = new System.Drawing.Point(183, 90);
        this.chkRequired.Name = "chkRequired";
        this.chkRequired.Size = new System.Drawing.Size(75, 17);
        this.chkRequired.TabIndex = 14;
        this.chkRequired.Text = "Requerido";
        this.chkRequired.UseVisualStyleBackColor = true;
        // 
        // lbDataType
        // 
        this.lbDataType.AutoSize = true;
        this.lbDataType.Location = new System.Drawing.Point(12, 62);
        this.lbDataType.Name = "lbDataType";
        this.lbDataType.Size = new System.Drawing.Size(31, 13);
        this.lbDataType.TabIndex = 15;
        this.lbDataType.Text = "Tipo:";
        // 
        // cbDataTypes
        // 
        this.cbDataTypes.FormattingEnabled = true;
        this.cbDataTypes.Location = new System.Drawing.Point(71, 59);
        this.cbDataTypes.Name = "cbDataTypes";
        this.cbDataTypes.Size = new System.Drawing.Size(106, 21);
        this.cbDataTypes.TabIndex = 16;
        // 
        // HtmlTextBox
        // 
        this.AcceptButton = this.btAccept;
        this.CancelButton = this.btCancel;
        this.ClientSize = new System.Drawing.Size(343, 158);
        this.Controls.Add(this.cbDataTypes);
        this.Controls.Add(this.lbDataType);
        this.Controls.Add(this.chkRequired);
        this.Controls.Add(this.tbName);
        this.Controls.Add(this.lbName);
        this.Controls.Add(this.chkReadOnly);
        this.Controls.Add(this.chkEnabled);
        this.Controls.Add(this.btCancel);
        this.Controls.Add(this.btAccept);
        this.Controls.Add(this.tbValue);
        this.Controls.Add(this.lbValor);
        this.Name = "HtmlTextBox";
        this.Text = "Insertar elemento TEXTBOX";
        this.Load += new System.EventHandler(this.HtmlComboBox_Load);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private Label lbValor;
    protected TextBox tbValue;
    protected Button btCancel;
    protected Button btAccept;
    protected CheckBox chkEnabled;
    protected CheckBox chkReadOnly;
    protected Label lbName;
    protected TextBox tbName;
    protected CheckBox chkRequired;
    private Label lbDataType;
    private ComboBox cbDataTypes;
}