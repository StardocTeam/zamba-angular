using System.Windows.Forms;

internal partial class HtmlCheckbox 
    : Form
{
    protected Button btAccept = null;
    protected TextBox tbInput = null;
    protected Button btCancel = null;
    protected Label lbTitle = null;
    protected CheckBox chkChecked = null;
    protected CheckBox chkEnabled = null;
    protected CheckBox chkRequired = null;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.btAccept = new Button();
        this.btCancel = new Button();
        this.tbInput = new TextBox();
        this.lbTitle = new Label();
        this.chkChecked = new CheckBox();
        this.chkEnabled = new CheckBox();
        this.chkRequired = new CheckBox();
        this.SuspendLayout();
        // 
        // btAccept
        // 
        this.btAccept.Location = new System.Drawing.Point(185, 62);
        this.btAccept.Name = "btAccept";
        this.btAccept.Size = new System.Drawing.Size(75, 23);
        this.btAccept.TabIndex = 3;
        this.btAccept.Text = "Aceptar";
        this.btAccept.UseVisualStyleBackColor = true;
        this.btAccept.Click += new System.EventHandler(this.btAccept_Click);
        // 
        // btCancel
        // 
        this.btCancel.DialogResult = DialogResult.Cancel;
        this.btCancel.Location = new System.Drawing.Point(266, 62);
        this.btCancel.Name = "btCancel";
        this.btCancel.Size = new System.Drawing.Size(75, 23);
        this.btCancel.TabIndex = 4;
        this.btCancel.Text = "Cancelar";
        this.btCancel.UseVisualStyleBackColor = true;
        this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
        // 
        // tbInput
        // 
        this.tbInput.Location = new System.Drawing.Point(52, 12);
        this.tbInput.Name = "tbInput";
        this.tbInput.Size = new System.Drawing.Size(289, 20);
        this.tbInput.TabIndex = 1;
        // 
        // lbTitle
        // 
        this.lbTitle.AutoSize = true;
        this.lbTitle.Location = new System.Drawing.Point(13, 15);
        this.lbTitle.Name = "lbTitle";
        this.lbTitle.Size = new System.Drawing.Size(33, 13);
        this.lbTitle.TabIndex = 3;
        this.lbTitle.Text = "Titulo";
        // 
        // chkChecked
        // 
        this.chkChecked.AutoSize = true;
        this.chkChecked.Location = new System.Drawing.Point(90, 38);
        this.chkChecked.Name = "chkChecked";
        this.chkChecked.RightToLeft = RightToLeft.Yes;
        this.chkChecked.Size = new System.Drawing.Size(91, 17);
        this.chkChecked.TabIndex = 2;
        this.chkChecked.Text = "Seleccionado";
        this.chkChecked.UseVisualStyleBackColor = true;
        // 
        // chkEnabled
        // 
        this.chkEnabled.AutoSize = true;
        this.chkEnabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.chkEnabled.Location = new System.Drawing.Point(187, 38);
        this.chkEnabled.Name = "chkEnabled";
        this.chkEnabled.Size = new System.Drawing.Size(73, 17);
        this.chkEnabled.TabIndex = 5;
        this.chkEnabled.Text = "Habilitado";
        this.chkEnabled.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.chkEnabled.UseVisualStyleBackColor = true;
        // 
        // chkRequired
        // 
        this.chkRequired.AutoSize = true;
        this.chkRequired.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.chkRequired.Location = new System.Drawing.Point(265, 38);
        this.chkRequired.Name = "chkRequired";
        this.chkRequired.Size = new System.Drawing.Size(75, 17);
        this.chkRequired.TabIndex = 6;
        this.chkRequired.Text = "Requerido";
        this.chkRequired.UseVisualStyleBackColor = true;
        // 
        // HtmlCheckbox
        // 
        this.AcceptButton = this.btAccept;
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.CancelButton = this.btCancel;
        this.ClientSize = new System.Drawing.Size(352, 100);
        this.Controls.Add(this.chkRequired);
        this.Controls.Add(this.chkEnabled);
        this.Controls.Add(this.chkChecked);
        this.Controls.Add(this.lbTitle);
        this.Controls.Add(this.btCancel);
        this.Controls.Add(this.tbInput);
        this.Controls.Add(this.btAccept);
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.Name = "HtmlCheckbox";
        this.StartPosition = FormStartPosition.CenterParent;
        this.Text = "Edición elemento CHECKBOX";
        this.Load += new System.EventHandler(this.HtmlCheckbox_Load);
        this.ResumeLayout(false);
        this.PerformLayout();

    }
}