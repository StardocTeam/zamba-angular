namespace DbToXML
{
    partial class CompareAttributes
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
            this.gridSource = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.insert = new System.Windows.Forms.RadioButton();
            this.UpdateSource = new System.Windows.Forms.RadioButton();
            this.UpdateDestiny = new System.Windows.Forms.RadioButton();
            this.RegenerateSource = new System.Windows.Forms.RadioButton();
            this.RegenrateDestiny = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btncancel = new System.Windows.Forms.Button();
            this.btnok = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridSource)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridSource
            // 
            this.gridSource.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSource.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridSource.Location = new System.Drawing.Point(0, 0);
            this.gridSource.Name = "gridSource";
            this.gridSource.Size = new System.Drawing.Size(903, 116);
            this.gridSource.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.insert);
            this.groupBox1.Controls.Add(this.UpdateSource);
            this.groupBox1.Controls.Add(this.UpdateDestiny);
            this.groupBox1.Controls.Add(this.RegenerateSource);
            this.groupBox1.Controls.Add(this.RegenrateDestiny);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btncancel);
            this.groupBox1.Controls.Add(this.btnok);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 259);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(903, 220);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // insert
            // 
            this.insert.AutoSize = true;
            this.insert.Location = new System.Drawing.Point(31, 187);
            this.insert.Name = "insert";
            this.insert.Size = new System.Drawing.Size(75, 17);
            this.insert.TabIndex = 11;
            this.insert.TabStop = true;
            this.insert.Text = "Insertar en";
            this.insert.UseVisualStyleBackColor = true;
            // 
            // UpdateSource
            // 
            this.UpdateSource.AutoSize = true;
            this.UpdateSource.Location = new System.Drawing.Point(31, 151);
            this.UpdateSource.Name = "UpdateSource";
            this.UpdateSource.Size = new System.Drawing.Size(105, 17);
            this.UpdateSource.TabIndex = 10;
            this.UpdateSource.TabStop = true;
            this.UpdateSource.Text = "Actualizar Origen";
            this.UpdateSource.UseVisualStyleBackColor = true;
            // 
            // UpdateDestiny
            // 
            this.UpdateDestiny.AutoSize = true;
            this.UpdateDestiny.Location = new System.Drawing.Point(31, 128);
            this.UpdateDestiny.Name = "UpdateDestiny";
            this.UpdateDestiny.Size = new System.Drawing.Size(110, 17);
            this.UpdateDestiny.TabIndex = 9;
            this.UpdateDestiny.TabStop = true;
            this.UpdateDestiny.Text = "Actualizar Destino";
            this.UpdateDestiny.UseVisualStyleBackColor = true;
            // 
            // RegenerateSource
            // 
            this.RegenerateSource.AutoSize = true;
            this.RegenerateSource.Location = new System.Drawing.Point(31, 95);
            this.RegenerateSource.Name = "RegenerateSource";
            this.RegenerateSource.Size = new System.Drawing.Size(404, 17);
            this.RegenerateSource.TabIndex = 8;
            this.RegenerateSource.TabStop = true;
            this.RegenerateSource.Text = "Mantener Atributo de Destino con Id y Generar Nuevo Id para Atributo de Origen";
            this.RegenerateSource.UseVisualStyleBackColor = true;
            // 
            // RegenrateDestiny
            // 
            this.RegenrateDestiny.AutoSize = true;
            this.RegenrateDestiny.Location = new System.Drawing.Point(31, 72);
            this.RegenrateDestiny.Name = "RegenrateDestiny";
            this.RegenrateDestiny.Size = new System.Drawing.Size(404, 17);
            this.RegenrateDestiny.TabIndex = 7;
            this.RegenrateDestiny.TabStop = true;
            this.RegenrateDestiny.Text = "Mantener Atributo de Origen con Id y Generar Nuevo Id para Atributo de Destino";
            this.RegenrateDestiny.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 6;
            // 
            // btncancel
            // 
            this.btncancel.Location = new System.Drawing.Point(740, 128);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(75, 23);
            this.btncancel.TabIndex = 5;
            this.btncancel.Text = "Cancelar";
            this.btncancel.UseVisualStyleBackColor = true;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // btnok
            // 
            this.btnok.Location = new System.Drawing.Point(593, 128);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(75, 23);
            this.btnok.TabIndex = 4;
            this.btnok.Text = "Guardar";
            this.btnok.UseVisualStyleBackColor = true;
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 116);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(903, 143);
            this.dataGridView1.TabIndex = 3;
            // 
            // CompareAttributes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(903, 479);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.gridSource);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "CompareAttributes";
            this.Text = "Resolver Conflicto";
            this.Load += new System.EventHandler(this.CompareSchema_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridSource)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridSource;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btncancel;
        private System.Windows.Forms.Button btnok;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RadioButton RegenerateSource;
        private System.Windows.Forms.RadioButton RegenrateDestiny;
        private System.Windows.Forms.RadioButton UpdateDestiny;
        private System.Windows.Forms.RadioButton insert;
        private System.Windows.Forms.RadioButton UpdateSource;
    }
}