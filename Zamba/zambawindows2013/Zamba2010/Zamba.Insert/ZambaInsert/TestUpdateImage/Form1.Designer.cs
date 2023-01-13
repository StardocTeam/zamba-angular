namespace TestUpdateImage
{
    partial class Form1
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
            this.btnInsert = new System.Windows.Forms.Button();
            this.txtDocTypeId = new System.Windows.Forms.TextBox();
            this.lbDocTypeId = new System.Windows.Forms.Label();
            this.lbInsertedId = new System.Windows.Forms.Label();
            this.txtIndexId = new System.Windows.Forms.TextBox();
            this.txtIndexValue = new System.Windows.Forms.TextBox();
            this.txtDocId = new System.Windows.Forms.TextBox();
            this.lbDocumentId = new System.Windows.Forms.Label();
            this.lbIndexId = new System.Windows.Forms.Label();
            this.lbValue = new System.Windows.Forms.Label();
            this.btnInsertIndex = new System.Windows.Forms.Button();
            this.gbDocument = new System.Windows.Forms.GroupBox();
            this.gbInsertIndex = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDocIdUpd = new System.Windows.Forms.TextBox();
            this.txtIndexIdUpd = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtIndexValueUpd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnUpdateState = new System.Windows.Forms.Button();
            this.gbDocument.SuspendLayout();
            this.gbInsertIndex.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnInsert
            // 
            this.btnInsert.Location = new System.Drawing.Point(301, 23);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(109, 23);
            this.btnInsert.TabIndex = 4;
            this.btnInsert.Text = "Insertar Documento";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btInsert_Click);
            // 
            // txtDocTypeId
            // 
            this.txtDocTypeId.Location = new System.Drawing.Point(183, 26);
            this.txtDocTypeId.Name = "txtDocTypeId";
            this.txtDocTypeId.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtDocTypeId.Size = new System.Drawing.Size(72, 20);
            this.txtDocTypeId.TabIndex = 2;
            // 
            // lbDocTypeId
            // 
            this.lbDocTypeId.AutoSize = true;
            this.lbDocTypeId.Location = new System.Drawing.Point(24, 28);
            this.lbDocTypeId.Name = "lbDocTypeId";
            this.lbDocTypeId.Size = new System.Drawing.Size(98, 13);
            this.lbDocTypeId.TabIndex = 6;
            this.lbDocTypeId.Text = "Id Tipo Documento";
            // 
            // lbInsertedId
            // 
            this.lbInsertedId.AutoSize = true;
            this.lbInsertedId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbInsertedId.Location = new System.Drawing.Point(13, 114);
            this.lbInsertedId.Name = "lbInsertedId";
            this.lbInsertedId.Size = new System.Drawing.Size(0, 13);
            this.lbInsertedId.TabIndex = 7;
            // 
            // txtIndexId
            // 
            this.txtIndexId.Location = new System.Drawing.Point(170, 45);
            this.txtIndexId.Name = "txtIndexId";
            this.txtIndexId.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtIndexId.Size = new System.Drawing.Size(72, 20);
            this.txtIndexId.TabIndex = 6;
            // 
            // txtIndexValue
            // 
            this.txtIndexValue.Location = new System.Drawing.Point(170, 72);
            this.txtIndexValue.Name = "txtIndexValue";
            this.txtIndexValue.Size = new System.Drawing.Size(240, 20);
            this.txtIndexValue.TabIndex = 7;
            // 
            // txtDocId
            // 
            this.txtDocId.Location = new System.Drawing.Point(170, 19);
            this.txtDocId.Name = "txtDocId";
            this.txtDocId.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtDocId.Size = new System.Drawing.Size(72, 20);
            this.txtDocId.TabIndex = 5;
            // 
            // lbDocumentId
            // 
            this.lbDocumentId.AutoSize = true;
            this.lbDocumentId.Location = new System.Drawing.Point(22, 19);
            this.lbDocumentId.Name = "lbDocumentId";
            this.lbDocumentId.Size = new System.Drawing.Size(68, 13);
            this.lbDocumentId.TabIndex = 11;
            this.lbDocumentId.Text = "Document Id";
            // 
            // lbIndexId
            // 
            this.lbIndexId.AutoSize = true;
            this.lbIndexId.Location = new System.Drawing.Point(22, 45);
            this.lbIndexId.Name = "lbIndexId";
            this.lbIndexId.Size = new System.Drawing.Size(45, 13);
            this.lbIndexId.TabIndex = 12;
            this.lbIndexId.Text = "Index Id";
            // 
            // lbValue
            // 
            this.lbValue.AutoSize = true;
            this.lbValue.Location = new System.Drawing.Point(22, 72);
            this.lbValue.Name = "lbValue";
            this.lbValue.Size = new System.Drawing.Size(31, 13);
            this.lbValue.TabIndex = 13;
            this.lbValue.Text = "Valor";
            // 
            // btnInsertIndex
            // 
            this.btnInsertIndex.Location = new System.Drawing.Point(319, 108);
            this.btnInsertIndex.Name = "btnInsertIndex";
            this.btnInsertIndex.Size = new System.Drawing.Size(91, 23);
            this.btnInsertIndex.TabIndex = 8;
            this.btnInsertIndex.Text = "Insertar Indice";
            this.btnInsertIndex.UseVisualStyleBackColor = true;
            this.btnInsertIndex.Click += new System.EventHandler(this.btInsertIndex_Click);
            // 
            // gbDocument
            // 
            this.gbDocument.Controls.Add(this.txtDocTypeId);
            this.gbDocument.Controls.Add(this.lbDocTypeId);
            this.gbDocument.Controls.Add(this.btnInsert);
            this.gbDocument.Location = new System.Drawing.Point(19, 12);
            this.gbDocument.Name = "gbDocument";
            this.gbDocument.Size = new System.Drawing.Size(428, 61);
            this.gbDocument.TabIndex = 15;
            this.gbDocument.TabStop = false;
            this.gbDocument.Text = "Insertar Documento";
            // 
            // gbInsertIndex
            // 
            this.gbInsertIndex.Controls.Add(this.txtDocId);
            this.gbInsertIndex.Controls.Add(this.txtIndexId);
            this.gbInsertIndex.Controls.Add(this.btnInsertIndex);
            this.gbInsertIndex.Controls.Add(this.txtIndexValue);
            this.gbInsertIndex.Controls.Add(this.lbValue);
            this.gbInsertIndex.Controls.Add(this.lbDocumentId);
            this.gbInsertIndex.Controls.Add(this.lbIndexId);
            this.gbInsertIndex.Location = new System.Drawing.Point(21, 79);
            this.gbInsertIndex.Name = "gbInsertIndex";
            this.gbInsertIndex.Size = new System.Drawing.Size(428, 146);
            this.gbInsertIndex.TabIndex = 16;
            this.gbInsertIndex.TabStop = false;
            this.gbInsertIndex.Text = "Insertar Indice Clave";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDocIdUpd);
            this.groupBox1.Controls.Add(this.txtIndexIdUpd);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.txtIndexValueUpd);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(23, 243);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(428, 146);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Insertar Indice a Actualizar";
            // 
            // txtDocIdUpd
            // 
            this.txtDocIdUpd.Location = new System.Drawing.Point(170, 19);
            this.txtDocIdUpd.Name = "txtDocIdUpd";
            this.txtDocIdUpd.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtDocIdUpd.Size = new System.Drawing.Size(72, 20);
            this.txtDocIdUpd.TabIndex = 5;
            // 
            // txtIndexIdUpd
            // 
            this.txtIndexIdUpd.Location = new System.Drawing.Point(170, 45);
            this.txtIndexIdUpd.Name = "txtIndexIdUpd";
            this.txtIndexIdUpd.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtIndexIdUpd.Size = new System.Drawing.Size(72, 20);
            this.txtIndexIdUpd.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(319, 108);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Insertar Indice";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtIndexValueUpd
            // 
            this.txtIndexValueUpd.Location = new System.Drawing.Point(170, 72);
            this.txtIndexValueUpd.Name = "txtIndexValueUpd";
            this.txtIndexValueUpd.Size = new System.Drawing.Size(240, 20);
            this.txtIndexValueUpd.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Valor";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Document Id";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Index Id";
            // 
            // btnUpdateState
            // 
            this.btnUpdateState.Location = new System.Drawing.Point(138, 409);
            this.btnUpdateState.Name = "btnUpdateState";
            this.btnUpdateState.Size = new System.Drawing.Size(136, 23);
            this.btnUpdateState.TabIndex = 15;
            this.btnUpdateState.Text = "Marcar como Cargado";
            this.btnUpdateState.UseVisualStyleBackColor = true;
            this.btnUpdateState.Click += new System.EventHandler(this.btnUpdateState_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 444);
            this.Controls.Add(this.btnUpdateState);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbInsertIndex);
            this.Controls.Add(this.gbDocument);
            this.Controls.Add(this.lbInsertedId);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "Test Actualizar Indices Externos";
            this.gbDocument.ResumeLayout(false);
            this.gbDocument.PerformLayout();
            this.gbInsertIndex.ResumeLayout(false);
            this.gbInsertIndex.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.TextBox txtDocTypeId;
        private System.Windows.Forms.Label lbDocTypeId;
        private System.Windows.Forms.Label lbInsertedId;
        private System.Windows.Forms.TextBox txtIndexId;
        private System.Windows.Forms.TextBox txtIndexValue;
        private System.Windows.Forms.TextBox txtDocId;
        private System.Windows.Forms.Label lbDocumentId;
        private System.Windows.Forms.Label lbIndexId;
        private System.Windows.Forms.Label lbValue;
        private System.Windows.Forms.Button btnInsertIndex;
        private System.Windows.Forms.GroupBox gbDocument;
        private System.Windows.Forms.GroupBox gbInsertIndex;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDocIdUpd;
        private System.Windows.Forms.TextBox txtIndexIdUpd;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtIndexValueUpd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnUpdateState;
    }
}

