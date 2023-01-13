namespace TestInsertImage
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
            this.txtDocumentName = new System.Windows.Forms.TextBox();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.tbDocumentName = new System.Windows.Forms.Label();
            this.lbPath = new System.Windows.Forms.Label();
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
            this.btnUpdateState = new System.Windows.Forms.Button();
            this.gbDocument.SuspendLayout();
            this.gbInsertIndex.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnInsert
            // 
            this.btnInsert.Location = new System.Drawing.Point(301, 126);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(109, 23);
            this.btnInsert.TabIndex = 4;
            this.btnInsert.Text = "Insertar Documento";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btInsert_Click);
            // 
            // txtDocumentName
            // 
            this.txtDocumentName.Location = new System.Drawing.Point(170, 19);
            this.txtDocumentName.Name = "txtDocumentName";
            this.txtDocumentName.Size = new System.Drawing.Size(240, 20);
            this.txtDocumentName.TabIndex = 1;
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(170, 84);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(240, 20);
            this.txtPath.TabIndex = 3;
            // 
            // tbDocumentName
            // 
            this.tbDocumentName.AutoSize = true;
            this.tbDocumentName.Location = new System.Drawing.Point(23, 26);
            this.tbDocumentName.Name = "tbDocumentName";
            this.tbDocumentName.Size = new System.Drawing.Size(119, 13);
            this.tbDocumentName.TabIndex = 2;
            this.tbDocumentName.Text = "Nombre del Documento";
            // 
            // lbPath
            // 
            this.lbPath.AutoSize = true;
            this.lbPath.Location = new System.Drawing.Point(23, 91);
            this.lbPath.Name = "lbPath";
            this.lbPath.Size = new System.Drawing.Size(29, 13);
            this.lbPath.TabIndex = 4;
            this.lbPath.Text = "Path";
            // 
            // txtDocTypeId
            // 
            this.txtDocTypeId.Location = new System.Drawing.Point(170, 51);
            this.txtDocTypeId.Name = "txtDocTypeId";
            this.txtDocTypeId.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtDocTypeId.Size = new System.Drawing.Size(72, 20);
            this.txtDocTypeId.TabIndex = 2;
            // 
            // lbDocTypeId
            // 
            this.lbDocTypeId.AutoSize = true;
            this.lbDocTypeId.Location = new System.Drawing.Point(23, 58);
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
            this.gbDocument.Controls.Add(this.txtDocumentName);
            this.gbDocument.Controls.Add(this.txtPath);
            this.gbDocument.Controls.Add(this.tbDocumentName);
            this.gbDocument.Controls.Add(this.lbPath);
            this.gbDocument.Controls.Add(this.txtDocTypeId);
            this.gbDocument.Controls.Add(this.lbDocTypeId);
            this.gbDocument.Controls.Add(this.btnInsert);
            this.gbDocument.Location = new System.Drawing.Point(19, 12);
            this.gbDocument.Name = "gbDocument";
            this.gbDocument.Size = new System.Drawing.Size(428, 155);
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
            this.gbInsertIndex.Location = new System.Drawing.Point(19, 186);
            this.gbInsertIndex.Name = "gbInsertIndex";
            this.gbInsertIndex.Size = new System.Drawing.Size(428, 146);
            this.gbInsertIndex.TabIndex = 16;
            this.gbInsertIndex.TabStop = false;
            this.gbInsertIndex.Text = "Insertar Indice";
            // 
            // btnUpdateState
            // 
            this.btnUpdateState.Location = new System.Drawing.Point(158, 345);
            this.btnUpdateState.Name = "btnUpdateState";
            this.btnUpdateState.Size = new System.Drawing.Size(136, 23);
            this.btnUpdateState.TabIndex = 14;
            this.btnUpdateState.Text = "Marcar como Cargado";
            this.btnUpdateState.UseVisualStyleBackColor = true;
            this.btnUpdateState.Click += new System.EventHandler(this.btnUpdateState_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 380);
            this.Controls.Add(this.btnUpdateState);
            this.Controls.Add(this.gbInsertIndex);
            this.Controls.Add(this.gbDocument);
            this.Controls.Add(this.lbInsertedId);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "Test Insertar Imagenes";
            this.gbDocument.ResumeLayout(false);
            this.gbDocument.PerformLayout();
            this.gbInsertIndex.ResumeLayout(false);
            this.gbInsertIndex.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.TextBox txtDocumentName;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label tbDocumentName;
        private System.Windows.Forms.Label lbPath;
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
        private System.Windows.Forms.Button btnUpdateState;
    }
}

