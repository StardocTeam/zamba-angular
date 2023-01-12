using Zamba.AppBlock;

partial class UCForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    #region Component Designer generated code

    //NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    //Puede modificarse utilizando el Diseñador de Windows Forms. 
    //No lo modifique con el editor de código.


    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.components = new System.ComponentModel.Container();
            this.Panel1 = new Zamba.AppBlock.ZPanel();
            this.ContextMenu1 = new System.Windows.Forms.ContextMenu();
            this.MenuItem2 = new System.Windows.Forms.MenuItem();
            this.MenuItem1 = new System.Windows.Forms.MenuItem();
            this.Panel2 = new Zamba.AppBlock.ZPanel();
            this.txtEntity = new System.Windows.Forms.TextBox();
            this.txtType = new System.Windows.Forms.TextBox();
            this.Label5 = new ZLabel();
            this.Label4 = new ZLabel();
            this.Label3 = new ZLabel();
            this.Label2 = new ZLabel();
            this.txtname = new System.Windows.Forms.TextBox();
            this.txtpath = new System.Windows.Forms.TextBox();
            this.Label1 = new ZLabel();
            this.Splitter1 = new Zamba.AppBlock.ZSplitter();
            this.ErrorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btncond = new Zamba.AppBlock.ZButton();
            this.btnAttributeCondition = new Zamba.AppBlock.ZButton();
            this.btnOpenTestCases = new Zamba.AppBlock.ZButton();
            this.Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Panel1.ForeColor = System.Drawing.Color.FromArgb(76,76,76);
            this.Panel1.Location = new System.Drawing.Point(286, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(466, 640);
            this.Panel1.TabIndex = 0;
            // 
            // ContextMenu1
            // 
            this.ContextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItem2,
            this.MenuItem1});
            // 
            // MenuItem2
            // 
            this.MenuItem2.Index = 0;
            this.MenuItem2.Text = "Actualizar";
            // 
            // MenuItem1
            // 
            this.MenuItem1.Index = 1;
            this.MenuItem1.Text = "Eliminar";
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.btnOpenTestCases);
            this.Panel2.Controls.Add(this.btnAttributeCondition);
            this.Panel2.Controls.Add(this.btncond);
            this.Panel2.Controls.Add(this.txtEntity);
            this.Panel2.Controls.Add(this.txtType);
            this.Panel2.Controls.Add(this.Label5);
            this.Panel2.Controls.Add(this.Label4);
            this.Panel2.Controls.Add(this.Label3);
            this.Panel2.Controls.Add(this.Label2);
            this.Panel2.Controls.Add(this.txtname);
            this.Panel2.Controls.Add(this.txtpath);
            this.Panel2.Controls.Add(this.Label1);
            this.Panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.Panel2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Panel2.ForeColor = System.Drawing.Color.FromArgb(76,76,76);
            this.Panel2.Location = new System.Drawing.Point(0, 0);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(281, 640);
            this.Panel2.TabIndex = 1;
            // 
            // txtEntity
            // 
            this.txtEntity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEntity.BackColor = System.Drawing.Color.White;
            this.txtEntity.Enabled = false;
            this.txtEntity.Location = new System.Drawing.Point(16, 201);
            this.txtEntity.Name = "txtEntity";
            this.txtEntity.Size = new System.Drawing.Size(256, 21);
            this.txtEntity.TabIndex = 12;
            //this.txtEntity.TextChanged += new System.EventHandler(this.txtEntity_TextChanged);
            // 
            // txtType
            // 
            this.txtType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtType.BackColor = System.Drawing.Color.White;
            this.txtType.Enabled = false;
            this.txtType.Location = new System.Drawing.Point(16, 153);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(256, 21);
            this.txtType.TabIndex = 11;
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.Color.Transparent;
            this.Label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.Location = new System.Drawing.Point(0, 0);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(279, 22);
            this.Label5.TabIndex = 10;
            this.Label5.Text = "Propiedades";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label4
            // 
            this.Label4.BackColor = System.Drawing.Color.Transparent;
            this.Label4.Location = new System.Drawing.Point(16, 182);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(152, 16);
            this.Label4.TabIndex = 9;
            this.Label4.Text = "Entidad";
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.Color.Transparent;
            this.Label3.Location = new System.Drawing.Point(16, 134);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(152, 16);
            this.Label3.TabIndex = 8;
            this.Label3.Text = "Tipo de Formulario";
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Location = new System.Drawing.Point(16, 86);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(100, 16);
            this.Label2.TabIndex = 7;
            this.Label2.Text = "Nombre";
            // 
            // txtname
            // 
            this.txtname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtname.BackColor = System.Drawing.Color.White;
            this.txtname.Enabled = false;
            this.txtname.Location = new System.Drawing.Point(16, 102);
            this.txtname.Name = "txtname";
            this.txtname.Size = new System.Drawing.Size(256, 21);
            this.txtname.TabIndex = 3;
            // 
            // txtpath
            // 
            this.txtpath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtpath.BackColor = System.Drawing.Color.White;
            this.txtpath.Enabled = false;
            this.txtpath.Location = new System.Drawing.Point(16, 56);
            this.txtpath.Name = "txtpath";
            this.txtpath.Size = new System.Drawing.Size(256, 21);
            this.txtpath.TabIndex = 1;
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Location = new System.Drawing.Point(16, 40);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(160, 16);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Formulario";
            // 
            // Splitter1
            // 
            this.Splitter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(182)))), ((int)(((byte)(9)))));
            this.Splitter1.Location = new System.Drawing.Point(281, 0);
            this.Splitter1.Name = "Splitter1";
            this.Splitter1.Size = new System.Drawing.Size(5, 640);
            this.Splitter1.TabIndex = 2;
            this.Splitter1.TabStop = false;
            // 
            // ErrorProvider1
            // 
            this.ErrorProvider1.ContainerControl = this;
            // 
            // btncond
            // 
            this.btncond.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btncond.Location = new System.Drawing.Point(19, 248);
            this.btncond.Name = "btncond";
            this.btncond.Size = new System.Drawing.Size(115, 27);
            this.btncond.TabIndex = 22;
            this.btncond.Text = "Condiciones";
            this.btncond.Click += new System.EventHandler(this.btncond_Click);
            // 
            // btnAttributeCondition
            // 
            this.btnAttributeCondition.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnAttributeCondition.Location = new System.Drawing.Point(16, 288);
            this.btnAttributeCondition.Margin = new System.Windows.Forms.Padding(0);
            this.btnAttributeCondition.Name = "btnAttributeCondition";
            this.btnAttributeCondition.Size = new System.Drawing.Size(256, 27);
            this.btnAttributeCondition.TabIndex = 26;
            this.btnAttributeCondition.Text = "Condiciones de atributos";
            this.btnAttributeCondition.Click += new System.EventHandler(this.btnAttributeCondition_Click);
            // 
            // btnOpenTestCases
            // 
            this.btnOpenTestCases.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOpenTestCases.Location = new System.Drawing.Point(16, 326);
            this.btnOpenTestCases.Margin = new System.Windows.Forms.Padding(0);
            this.btnOpenTestCases.Name = "btnOpenTestCases";
            this.btnOpenTestCases.Size = new System.Drawing.Size(256, 27);
            this.btnOpenTestCases.TabIndex = 27;
            this.btnOpenTestCases.Text = "Ir a casos de prueba";
            this.btnOpenTestCases.Click += new System.EventHandler(this.btnOpenTestCases_Click);
            // 
            // UCForm
            // 
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.Splitter1);
            this.Controls.Add(this.Panel2);
            this.Name = "UCForm";
            this.Size = new System.Drawing.Size(752, 640);
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider1)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion

    internal ZLabel Label1;
    internal System.Windows.Forms.TextBox txtpath;
    internal System.Windows.Forms.TextBox txtname;
    internal ZSplitter Splitter1;
    internal System.Windows.Forms.ErrorProvider ErrorProvider1;
    internal System.Windows.Forms.ContextMenu ContextMenu1;
    internal System.Windows.Forms.MenuItem MenuItem1;
    internal ZLabel Label2;
    internal ZLabel Label3;
    internal ZLabel Label4;
    internal ZLabel Label5;
    internal System.Windows.Forms.MenuItem MenuItem2;
    internal Zamba.AppBlock.ZPanel Panel1;
    internal Zamba.AppBlock.ZPanel Panel2;
    internal System.Windows.Forms.TextBox txtType;
    internal System.Windows.Forms.TextBox txtEntity;
    internal ZButton btncond;
    internal ZButton btnAttributeCondition;
    internal ZButton btnOpenTestCases;
}