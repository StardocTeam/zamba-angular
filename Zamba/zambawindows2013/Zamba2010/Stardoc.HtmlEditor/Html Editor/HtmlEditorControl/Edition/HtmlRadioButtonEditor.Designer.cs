using System.Windows.Forms;
internal partial class HtmlRadioButtonEditor
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
        this.lbText = new Label();
        this.lbCategoria = new Label();
        this.tbName = new TextBox();
        this.tbCategory = new TextBox();
        this.btCancel = new Button();
        this.btAccept = new Button();
        this.chkChecked = new CheckBox();
        this.chkEnabled = new CheckBox();
        this.chkRequired = new CheckBox();
        this.SuspendLayout();
        // 
        // lbText
        // 
        this.lbText.AutoSize = true;
        this.lbText.Location = new System.Drawing.Point(12, 9);
        this.lbText.Name = "lbText";
        this.lbText.Size = new System.Drawing.Size(37, 13);
        this.lbText.TabIndex = 0;
        this.lbText.Text = "Texto:";
        // 
        // lbCategoria
        // 
        this.lbCategoria.AutoSize = true;
        this.lbCategoria.Location = new System.Drawing.Point(12, 35);
        this.lbCategoria.Name = "lbCategoria";
        this.lbCategoria.Size = new System.Drawing.Size(55, 13);
        this.lbCategoria.TabIndex = 2;
        this.lbCategoria.Text = "Categoria:";
        // 
        // tbName
        // 
        this.tbName.Location = new System.Drawing.Point(91, 6);
        this.tbName.Name = "tbName";
        this.tbName.Size = new System.Drawing.Size(225, 20);
        this.tbName.TabIndex = 1;
        // 
        // tbCategory
        // 
        this.tbCategory.Location = new System.Drawing.Point(91, 32);
        this.tbCategory.Name = "tbCategory";
        this.tbCategory.Size = new System.Drawing.Size(225, 20);
        this.tbCategory.TabIndex = 2;
        // 
        // btCancel
        // 
        this.btCancel.DialogResult = DialogResult.Cancel;
        this.btCancel.Location = new System.Drawing.Point(240, 77);
        this.btCancel.Name = "btCancel";
        this.btCancel.Size = new System.Drawing.Size(75, 23);
        this.btCancel.TabIndex = 4;
        this.btCancel.Text = "Cancelar";
        this.btCancel.UseVisualStyleBackColor = true;
        this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
        // 
        // btAccept
        // 
        this.btAccept.Location = new System.Drawing.Point(159, 77);
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
        this.chkChecked.Location = new System.Drawing.Point(15, 58);
        this.chkChecked.Name = "chkChecked";
        this.chkChecked.RightToLeft = RightToLeft.Yes;
        this.chkChecked.Size = new System.Drawing.Size(91, 17);
        this.chkChecked.TabIndex = 5;
        this.chkChecked.Text = "Seleccionado";
        this.chkChecked.UseVisualStyleBackColor = true;
        // 
        // chkEnabled
        // 
        this.chkEnabled.AutoSize = true;
        this.chkEnabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.chkEnabled.Location = new System.Drawing.Point(112, 58);
        this.chkEnabled.Name = "chkEnabled";
        this.chkEnabled.Size = new System.Drawing.Size(73, 17);
        this.chkEnabled.TabIndex = 9;
        this.chkEnabled.Text = "Habilitado";
        this.chkEnabled.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.chkEnabled.UseVisualStyleBackColor = true;
        // 
        // chkRequired
        // 
        this.chkRequired.AutoSize = true;
        this.chkRequired.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.chkRequired.Location = new System.Drawing.Point(191, 58);
        this.chkRequired.Name = "chkRequired";
        this.chkRequired.Size = new System.Drawing.Size(75, 17);
        this.chkRequired.TabIndex = 10;
        this.chkRequired.Text = "Requerido";
        this.chkRequired.UseVisualStyleBackColor = true;
        // 
        // HtmlRadioButtonEditor
        // 
        this.AcceptButton = this.btAccept;
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.CancelButton = this.btCancel;
        this.ClientSize = new System.Drawing.Size(332, 112);
        this.Controls.Add(this.chkRequired);
        this.Controls.Add(this.chkEnabled);
        this.Controls.Add(this.chkChecked);
        this.Controls.Add(this.btAccept);
        this.Controls.Add(this.btCancel);
        this.Controls.Add(this.tbCategory);
        this.Controls.Add(this.tbName);
        this.Controls.Add(this.lbCategoria);
        this.Controls.Add(this.lbText);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.Name = "HtmlRadioButtonEditor";
        this.StartPosition = FormStartPosition.CenterParent;
        this.Text = "Edición del elemento Radio Button";
        this.Load += new System.EventHandler(this.HtmlRadioButtonEditor_Load);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private Label lbText;
    private Label lbCategoria;
    private TextBox tbName;
    private TextBox tbCategory;
    private Button btCancel;
    private Button btAccept;
    private CheckBox chkChecked;
    private CheckBox chkEnabled;
    private CheckBox chkRequired;
}