using System.Windows.Forms;
internal partial class HtmlSelectEditor
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
        this.lstItems = new ListBox();
        this.btAdd = new Button();
        this.btDelete = new Button();
        this.btAccept = new Button();
        this.btUpdate = new Button();
        this.lbTitle = new Label();
        this.btCancel = new Button();
        this.chkEnabled = new CheckBox();
        this.chkRequired = new CheckBox();
        this.SuspendLayout();
        // 
        // lstItems
        // 
        this.lstItems.FormattingEnabled = true;
        this.lstItems.Location = new System.Drawing.Point(12, 35);
        this.lstItems.Name = "lstItems";
        this.lstItems.Size = new System.Drawing.Size(438, 134);
        this.lstItems.TabIndex = 0;
        this.lstItems.SelectedIndexChanged += new System.EventHandler(this.lstItems_SelectedIndexChanged);
        // 
        // btAdd
        // 
        this.btAdd.Location = new System.Drawing.Point(456, 35);
        this.btAdd.Name = "btAdd";
        this.btAdd.Size = new System.Drawing.Size(75, 23);
        this.btAdd.TabIndex = 1;
        this.btAdd.Text = "Agregar";
        this.btAdd.UseVisualStyleBackColor = true;
        this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
        // 
        // btDelete
        // 
        this.btDelete.Location = new System.Drawing.Point(456, 90);
        this.btDelete.Name = "btDelete";
        this.btDelete.Size = new System.Drawing.Size(75, 23);
        this.btDelete.TabIndex = 2;
        this.btDelete.Text = "Borrar";
        this.btDelete.UseVisualStyleBackColor = true;
        this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
        // 
        // btAccept
        // 
        this.btAccept.Location = new System.Drawing.Point(375, 198);
        this.btAccept.Name = "btAccept";
        this.btAccept.Size = new System.Drawing.Size(75, 23);
        this.btAccept.TabIndex = 4;
        this.btAccept.Text = "Aceptar";
        this.btAccept.UseVisualStyleBackColor = true;
        this.btAccept.Click += new System.EventHandler(this.btAccept_Click);
        // 
        // btUpdate
        // 
        this.btUpdate.Location = new System.Drawing.Point(457, 61);
        this.btUpdate.Name = "btUpdate";
        this.btUpdate.Size = new System.Drawing.Size(75, 23);
        this.btUpdate.TabIndex = 5;
        this.btUpdate.Text = "Modificar";
        this.btUpdate.UseVisualStyleBackColor = true;
        this.btUpdate.Click += new System.EventHandler(this.btUpdate_Click);
        // 
        // lbTitle
        // 
        this.lbTitle.AutoSize = true;
        this.lbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lbTitle.Location = new System.Drawing.Point(12, 9);
        this.lbTitle.Name = "lbTitle";
        this.lbTitle.Size = new System.Drawing.Size(99, 13);
        this.lbTitle.TabIndex = 6;
        this.lbTitle.Text = "Listado de items";
        // 
        // btCancel
        // 
        this.btCancel.DialogResult = DialogResult.Cancel;
        this.btCancel.Location = new System.Drawing.Point(456, 198);
        this.btCancel.Name = "btCancel";
        this.btCancel.Size = new System.Drawing.Size(75, 23);
        this.btCancel.TabIndex = 7;
        this.btCancel.Text = "Cancelar";
        this.btCancel.UseVisualStyleBackColor = true;
        this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
        // 
        // chkEnabled
        // 
        this.chkEnabled.AutoSize = true;
        this.chkEnabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.chkEnabled.Location = new System.Drawing.Point(12, 175);
        this.chkEnabled.Name = "chkEnabled";
        this.chkEnabled.Size = new System.Drawing.Size(73, 17);
        this.chkEnabled.TabIndex = 10;
        this.chkEnabled.Text = "Habilitado";
        this.chkEnabled.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.chkEnabled.UseVisualStyleBackColor = true;
        // 
        // chkRequired
        // 
        this.chkRequired.AutoSize = true;
        this.chkRequired.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.chkRequired.Location = new System.Drawing.Point(91, 175);
        this.chkRequired.Name = "chkRequired";
        this.chkRequired.Size = new System.Drawing.Size(75, 17);
        this.chkRequired.TabIndex = 11;
        this.chkRequired.Text = "Requerido";
        this.chkRequired.UseVisualStyleBackColor = true;
        // 
        // HtmlSelectEditor
        // 
        this.AcceptButton = this.btAccept;
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.CancelButton = this.btCancel;
        this.ClientSize = new System.Drawing.Size(554, 233);
        this.Controls.Add(this.chkRequired);
        this.Controls.Add(this.chkEnabled);
        this.Controls.Add(this.btCancel);
        this.Controls.Add(this.lbTitle);
        this.Controls.Add(this.btUpdate);
        this.Controls.Add(this.btAccept);
        this.Controls.Add(this.btDelete);
        this.Controls.Add(this.btAdd);
        this.Controls.Add(this.lstItems);
        this.Name = "HtmlSelectEditor";
        this.StartPosition = FormStartPosition.CenterParent;
        this.Text = "Edición elemento SELECT";
        this.Load += new System.EventHandler(this.SelectEditor_Load);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    protected ListBox lstItems;
    protected Button btAdd;
    protected Button btDelete;
    protected Button btAccept;
    protected Button btUpdate;
    protected Label lbTitle;
    protected Button btCancel;
    protected CheckBox chkEnabled;
    protected CheckBox chkRequired;
}