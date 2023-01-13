using System.Windows.Forms;
internal partial class ListHtmlCheckbox 
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
        this.btCancel = new System.Windows.Forms.Button();
        this.btAccept = new System.Windows.Forms.Button();
        this.lbTitle = new System.Windows.Forms.Label();
        this.lstItems = new System.Windows.Forms.ListBox();
        this.btAdd = new System.Windows.Forms.Button();
        this.btDelete = new System.Windows.Forms.Button();
        this.btUpdate = new System.Windows.Forms.Button();
        this.SuspendLayout();
        // 
        // btCancel
        // 
        this.btCancel.Location = new System.Drawing.Point(456, 198);
        this.btCancel.Name = "btCancel";
        this.btCancel.Size = new System.Drawing.Size(75, 23);
        this.btCancel.TabIndex = 14;
        this.btCancel.Text = "Cancelar";
        this.btCancel.UseVisualStyleBackColor = true;
        this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
        // 
        // btAccept
        // 
        this.btAccept.Location = new System.Drawing.Point(375, 198);
        this.btAccept.Name = "btAccept";
        this.btAccept.Size = new System.Drawing.Size(75, 23);
        this.btAccept.TabIndex = 11;
        this.btAccept.Text = "Aceptar";
        this.btAccept.UseVisualStyleBackColor = true;
        this.btAccept.Click += new System.EventHandler(this.btAccept_Click);
        // 
        // lbTitle
        // 
        this.lbTitle.AutoSize = true;
        this.lbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lbTitle.Location = new System.Drawing.Point(12, 9);
        this.lbTitle.Name = "lbTitle";
        this.lbTitle.Size = new System.Drawing.Size(99, 13);
        this.lbTitle.TabIndex = 13;
        this.lbTitle.Text = "Listado de items";
        // 
        // lstItems
        // 
        this.lstItems.FormattingEnabled = true;
        this.lstItems.Location = new System.Drawing.Point(12, 35);
        this.lstItems.Name = "lstItems";
        this.lstItems.Size = new System.Drawing.Size(438, 134);
        this.lstItems.TabIndex = 8;
        this.lstItems.SelectedIndexChanged += new System.EventHandler(this.lstItems_SelectedIndexChanged);
        // 
        // btAdd
        // 
        this.btAdd.Location = new System.Drawing.Point(456, 35);
        this.btAdd.Name = "btAdd";
        this.btAdd.Size = new System.Drawing.Size(75, 23);
        this.btAdd.TabIndex = 9;
        this.btAdd.Text = "Agregar";
        this.btAdd.UseVisualStyleBackColor = true;
        this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
        // 
        // btDelete
        // 
        this.btDelete.Location = new System.Drawing.Point(456, 90);
        this.btDelete.Name = "btDelete";
        this.btDelete.Size = new System.Drawing.Size(75, 23);
        this.btDelete.TabIndex = 10;
        this.btDelete.Text = "Borrar";
        this.btDelete.UseVisualStyleBackColor = true;
        this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
        // 
        // btUpdate
        // 
        this.btUpdate.Location = new System.Drawing.Point(457, 61);
        this.btUpdate.Name = "btUpdate";
        this.btUpdate.Size = new System.Drawing.Size(75, 23);
        this.btUpdate.TabIndex = 12;
        this.btUpdate.Text = "Modificar";
        this.btUpdate.UseVisualStyleBackColor = true;
        this.btUpdate.Click += new System.EventHandler(this.btUpdate_Click);
        // 
        // ListHtmlCheckbox
        // 
        this.AcceptButton = this.btAccept;
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.CancelButton = this.btCancel;
        this.ClientSize = new System.Drawing.Size(554, 233);
        this.Controls.Add(this.btCancel);
        this.Controls.Add(this.lbTitle);
        this.Controls.Add(this.btUpdate);
        this.Controls.Add(this.btAccept);
        this.Controls.Add(this.btDelete);
        this.Controls.Add(this.btAdd);
        this.Controls.Add(this.lstItems);
        this.Name = "ListHtmlCheckbox";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "Listado de elementos CheckBox";
        this.Load += new System.EventHandler(this.ListHtmlCheckbox_Load);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private Button btCancel;
    private Button btAccept;
    private Label lbTitle;
    private ListBox lstItems;
    private Button btAdd;
    private Button btDelete;
    private Button btUpdate;

}