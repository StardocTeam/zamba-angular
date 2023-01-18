using Zamba.AppBlock;

namespace Zamba.Shapes.Views
{
    partial class UCRuleDetail
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblAction = new ZLabel();
            this.lblWorkflow = new ZLabel();
            this.lblStep = new ZLabel();
            this.lblName = new ZLabel();
            this.lblId = new ZLabel();
            this.SuspendLayout();
            // 
            // lblAction
            // 
            this.lblAction.AutoSize = true;
            this.lblAction.Location = new System.Drawing.Point(20, 20);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(43, 13);
            this.lblAction.TabIndex = 0;
            this.lblAction.Text = "Acción:";
            // 
            // lblWorkflow
            // 
            this.lblWorkflow.AutoSize = true;
            this.lblWorkflow.Location = new System.Drawing.Point(20, 45);
            this.lblWorkflow.Name = "lblWorkflow";
            this.lblWorkflow.Size = new System.Drawing.Size(49, 13);
            this.lblWorkflow.TabIndex = 1;
            this.lblWorkflow.Text = "Proceso:";
            // 
            // lblStep
            // 
            this.lblStep.AutoSize = true;
            this.lblStep.Location = new System.Drawing.Point(20, 70);
            this.lblStep.Name = "lblStep";
            this.lblStep.Size = new System.Drawing.Size(38, 13);
            this.lblStep.TabIndex = 2;
            this.lblStep.Text = "Etapa:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(20, 95);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(47, 13);
            this.lblName.TabIndex = 3;
            this.lblName.Text = "Nombre:";
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(20, 120);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(21, 13);
            this.lblId.TabIndex = 4;
            this.lblId.Text = "ID:";
            // 
            // UCRuleDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblStep);
            this.Controls.Add(this.lblWorkflow);
            this.Controls.Add(this.lblAction);
            this.Name = "UCRuleDetail";
            this.Size = new System.Drawing.Size(437, 396);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZLabel lblAction;
        private ZLabel lblWorkflow;
        private ZLabel lblStep;
        private ZLabel lblName;
        private ZLabel lblId;
    }
}
