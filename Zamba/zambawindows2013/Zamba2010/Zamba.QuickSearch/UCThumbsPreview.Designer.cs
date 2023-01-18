using System.Data;
using System.Windows.Forms;

namespace Zamba.QuickSearch
{
    partial class UCThumbsPreview
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
            Telerik.WinControls.UI.RadListDataItem radListDataItem1 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem2 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem3 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem4 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem5 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem6 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem7 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem8 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem9 = new Telerik.WinControls.UI.RadListDataItem();
            this.dt = new System.Data.DataTable();
            this.radCardView1 = new Telerik.WinControls.UI.RadCardView();
            this.DocIdItem = new Telerik.WinControls.UI.CardViewItem();
            this.TaskItem = new Telerik.WinControls.UI.CardViewItem();
            this.StepItem = new Telerik.WinControls.UI.CardViewItem();
            this.Stateitem = new Telerik.WinControls.UI.CardViewItem();
            this.AsignedItem = new Telerik.WinControls.UI.CardViewItem();
            this.layoutControlLabelItem1 = new Telerik.WinControls.UI.LayoutControlLabelItem();
            this.cardViewGroupItem1 = new Telerik.WinControls.UI.CardViewGroupItem();
            this.radCommandBar1 = new Telerik.WinControls.UI.RadCommandBar();
            this.commandBarRowElement1 = new Telerik.WinControls.UI.CommandBarRowElement();
            this.commandBarStripElement1 = new Telerik.WinControls.UI.CommandBarStripElement();
            this.commandBarSeparator4 = new Telerik.WinControls.UI.CommandBarSeparator();
            this.commandBarLabel1 = new Telerik.WinControls.UI.CommandBarLabel();
            this.commandBarDropDownSort = new Telerik.WinControls.UI.CommandBarDropDownList();
            this.commandBarSeparator1 = new Telerik.WinControls.UI.CommandBarSeparator();
            this.commandBarLabel2 = new Telerik.WinControls.UI.CommandBarLabel();
            this.commandBarDropDownGroup = new Telerik.WinControls.UI.CommandBarDropDownList();
            this.commandBarSeparator2 = new Telerik.WinControls.UI.CommandBarSeparator();
            this.commandBarSeparator3 = new Telerik.WinControls.UI.CommandBarSeparator();
            this.commandBarLabel3 = new Telerik.WinControls.UI.CommandBarLabel();
            this.commandBarTextBoxFilter = new Telerik.WinControls.UI.CommandBarTextBox();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radCardView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radCardView1.CardTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radCommandBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.SuspendLayout();
            // 
            // radCardView1
            // 
            this.radCardView1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            // 
            // 
            // 
            this.radCardView1.CardTemplate.HiddenItems.AddRange(new Telerik.WinControls.RadItem[] { this.Stateitem, this.layoutControlLabelItem1 }); this.radCardView1.CardTemplate.Items.AddRange(new Telerik.WinControls.RadItem[] { this.TaskItem, this.StepItem, this.AsignedItem, this.cardViewGroupItem1, this.layoutControlLabelItem2 });
            this.radCardView1.CardTemplate.Location = new System.Drawing.Point(635, 299);
            this.radCardView1.CardTemplate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radCardView1.CardTemplate.Name = "radCardView1Template";
            this.radCardView1.CardTemplate.Size = new System.Drawing.Size(100, 80);
            this.radCardView1.CardTemplate.TabIndex = 0;
            this.radCardView1.Controls.Add(this.radCardView1.CardTemplate);
            this.radCardView1.DisplayMember = "Tarea";
            this.radCardView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radCardView1.ItemSize = new System.Drawing.Size(100, 80);
            this.radCardView1.KeyboardSearchEnabled = true;
            this.radCardView1.Location = new System.Drawing.Point(0, 30);
            this.radCardView1.Name = "radCardView1";
            // 
            // 
            // 
            this.radCardView1.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 200, 250);
            this.radCardView1.ShowGroups = true;
            this.radCardView1.Size = new System.Drawing.Size(1511, 887);
            this.radCardView1.TabIndex = 2;
            this.radCardView1.Text = "radCardView1";
            this.radCardView1.ValueMember = "DOC_ID";

            // 
            // cardViewItem16
            // 
            this.DocIdItem.Bounds = new System.Drawing.Rectangle(0, 390, 192, 26);
            this.DocIdItem.FieldName = "DOC_ID";
            this.DocIdItem.Name = "DocIdItem";
            this.DocIdItem.Text = "DOC_ID";
            // 
            // cardViewItem1
            // 
            this.IconIdItem.Bounds = new System.Drawing.Rectangle(0, 0, 192, 26);
            this.IconIdItem.FieldName = "ICON_ID";
            this.IconIdItem.Name = "IconIdItem";
            this.IconIdItem.Text = "ICON_ID";
            // 
            // cardViewItem7
            // 
            this.TaskItem.Bounds = new System.Drawing.Rectangle(0, 156, 192, 26);
            this.TaskItem.FieldName = "Tarea";
            this.TaskItem.Name = "TaskItem";
            this.TaskItem.Text = "Tarea";
            this.TaskItem.TextProportionalSize = 0F;
            this.TaskItem.MaxSize = new System.Drawing.Size(0, 0);
            this.TaskItem.MinSize = new System.Drawing.Size(0, 30);
            // 
            // cardViewItem8
            // 
            this.StepItem.Bounds = new System.Drawing.Rectangle(0, 182, 192, 26);
            this.StepItem.FieldName = "Etapa";
            this.StepItem.Name = "StepItem";
            this.StepItem.Text = "Etapa";
            this.StepItem.TextProportionalSize = 0F;
            this.StepItem.MaxSize = new System.Drawing.Size(0, 0);
            this.StepItem.MinSize = new System.Drawing.Size(0, 30);
            // 
            // cardViewItem9
            // 
            this.Stateitem.Bounds = new System.Drawing.Rectangle(0, 208, 192, 26);
            this.Stateitem.FieldName = "Estado";
            this.Stateitem.Name = "Stateitem";
            this.Stateitem.Text = "Estado";
            this.Stateitem.TextProportionalSize = 0F;
            this.Stateitem.MaxSize = new System.Drawing.Size(0, 0);
            this.Stateitem.MinSize = new System.Drawing.Size(0, 30);
            // 
            // layoutControlLabelItem1
            // 
            this.layoutControlLabelItem1.Bounds = new System.Drawing.Rectangle(0, 0, 360, 26);
            this.layoutControlLabelItem1.Name = "layoutControlLabelItem1";
            this.layoutControlLabelItem1.Text = "layoutControlLabelItem1";
            // 
            // cardViewItem2
            // 
            this.AsignedItem.Bounds = new System.Drawing.Rectangle(0, 160, 360, 30);
            this.AsignedItem.FieldName = "Asignado";
            this.AsignedItem.Name = "AsignedItem";
            this.AsignedItem.Text = "Asignado";
            this.AsignedItem.TextProportionalSize = 0F;
            this.AsignedItem.MaxSize = new System.Drawing.Size(0, 0);
            this.AsignedItem.MinSize = new System.Drawing.Size(0, 30);
            // 
            // cardViewGroupItem1
            // 
            this.cardViewGroupItem1.Bounds = new System.Drawing.Rectangle(0, 230, 360, 190);
            this.cardViewGroupItem1.DrawText = true;
            //this.cardViewGroupItem1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            //this.cardViewItem10,
            //this.cardViewItem11,
            //this.cardViewItem12,
            //this.cardViewItem13,
            //this.cardViewItem14,
            //this.cardViewItem15});
            this.cardViewGroupItem1.Name = "cardViewGroupItem1";            
            // 
            // layoutControlLabelItem2
            // 
            this.layoutControlLabelItem2.Bounds = new System.Drawing.Rectangle(0, 220, 360, 10);
            this.layoutControlLabelItem2.DrawText = false;
            this.layoutControlLabelItem2.MaxSize = new System.Drawing.Size(0, 10);
            this.layoutControlLabelItem2.MinSize = new System.Drawing.Size(46, 10);
            this.layoutControlLabelItem2.Name = "layoutControlLabelItem2";
            // 
            // radCommandBar1
            // 
            this.radCommandBar1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.radCommandBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radCommandBar1.Location = new System.Drawing.Point(0, 0);
            this.radCommandBar1.Name = "radCommandBar1";
            // 
            // 
            // 
            this.radCommandBar1.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 25, 25);
            this.radCommandBar1.Rows.AddRange(new Telerik.WinControls.UI.CommandBarRowElement[] {
            this.commandBarRowElement1});
            this.radCommandBar1.Size = new System.Drawing.Size(1511, 30);
            this.radCommandBar1.TabIndex = 4;
            // 
            // commandBarRowElement1
            // 
            this.commandBarRowElement1.DisplayName = null;
            this.commandBarRowElement1.MinSize = new System.Drawing.Size(25, 25);
            this.commandBarRowElement1.Strips.AddRange(new Telerik.WinControls.UI.CommandBarStripElement[] {
            this.commandBarStripElement1});
            // 
            // commandBarStripElement1
            // 
            this.commandBarStripElement1.DisplayName = "commandBarStripElement1";
            this.commandBarStripElement1.EnableDragging = false;
            // 
            // 
            // 
            this.commandBarStripElement1.Grip.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarStripElement1.Grip.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarStripElement1.Items.AddRange(new Telerik.WinControls.UI.RadCommandBarBaseItem[] {
            this.commandBarSeparator4,
            this.commandBarLabel1,
            this.commandBarDropDownSort,
            this.commandBarSeparator1,
            this.commandBarLabel2,
            this.commandBarDropDownGroup,
            this.commandBarSeparator2,
            this.commandBarSeparator3,
            this.commandBarLabel3,
            this.commandBarTextBoxFilter});
            this.commandBarStripElement1.Name = "commandBarStripElement1";
            // 
            // 
            // 
            this.commandBarStripElement1.OverflowButton.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarStripElement1.OverflowButton.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarStripElement1.StretchHorizontally = true;
            this.commandBarStripElement1.Text = "";
            // 
            // commandBarSeparator4
            // 
            this.commandBarSeparator4.AccessibleDescription = "commandBarSeparator4";
            this.commandBarSeparator4.AccessibleName = "commandBarSeparator4";
            this.commandBarSeparator4.DisplayName = "commandBarSeparator4";
            this.commandBarSeparator4.Name = "commandBarSeparator4";
            this.commandBarSeparator4.VisibleInOverflowMenu = false;
            // 
            // commandBarLabel1
            // 
            this.commandBarLabel1.DisplayName = "commandBarLabel1";
            this.commandBarLabel1.Name = "commandBarLabel1";
            this.commandBarLabel1.Text = "Sort By:";
            // 
            // commandBarDropDownSort
            // 
            this.commandBarDropDownSort.DisplayName = "commandBarDropDownList1";
            this.commandBarDropDownSort.DropDownAnimationEnabled = true;
            this.commandBarDropDownSort.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            radListDataItem1.Text = "None";
            radListDataItem2.Text = "Make";
            radListDataItem3.Text = "Model";
            radListDataItem4.Text = "Category";
            radListDataItem5.Text = "Year";
            this.commandBarDropDownSort.Items.Add(radListDataItem1);
            this.commandBarDropDownSort.Items.Add(radListDataItem2);
            this.commandBarDropDownSort.Items.Add(radListDataItem3);
            this.commandBarDropDownSort.Items.Add(radListDataItem4);
            this.commandBarDropDownSort.Items.Add(radListDataItem5);
            this.commandBarDropDownSort.MaxDropDownItems = 0;
            this.commandBarDropDownSort.Name = "commandBarDropDownSort";
            this.commandBarDropDownSort.Text = "None";
            // 
            // commandBarSeparator1
            // 
            this.commandBarSeparator1.AccessibleDescription = "commandBarSeparator1";
            this.commandBarSeparator1.AccessibleName = "commandBarSeparator1";
            this.commandBarSeparator1.DisplayName = "commandBarSeparator1";
            this.commandBarSeparator1.Name = "commandBarSeparator1";
            this.commandBarSeparator1.VisibleInOverflowMenu = false;
            // 
            // commandBarLabel2
            // 
            this.commandBarLabel2.DisplayName = "commandBarLabel2";
            this.commandBarLabel2.Name = "commandBarLabel2";
            this.commandBarLabel2.Text = "Group By:";
            // 
            // commandBarDropDownGroup
            // 
            this.commandBarDropDownGroup.DisplayName = "commandBarDropDownList2";
            this.commandBarDropDownGroup.DropDownAnimationEnabled = true;
            this.commandBarDropDownGroup.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            radListDataItem6.Text = "None";
            radListDataItem7.Text = "Make";
            radListDataItem8.Text = "Category";
            radListDataItem9.Text = "Year";
            this.commandBarDropDownGroup.Items.Add(radListDataItem6);
            this.commandBarDropDownGroup.Items.Add(radListDataItem7);
            this.commandBarDropDownGroup.Items.Add(radListDataItem8);
            this.commandBarDropDownGroup.Items.Add(radListDataItem9);
            this.commandBarDropDownGroup.MaxDropDownItems = 0;
            this.commandBarDropDownGroup.Name = "commandBarDropDownGroup";
            this.commandBarDropDownGroup.Text = "";
            // 
            // commandBarSeparator2
            // 
            this.commandBarSeparator2.AccessibleDescription = "commandBarSeparator2";
            this.commandBarSeparator2.AccessibleName = "commandBarSeparator2";
            this.commandBarSeparator2.DisplayName = "commandBarSeparator2";
            this.commandBarSeparator2.Name = "commandBarSeparator2";
            this.commandBarSeparator2.VisibleInOverflowMenu = false;
            // 
            // commandBarSeparator3
            // 
            this.commandBarSeparator3.AccessibleDescription = "commandBarSeparator3";
            this.commandBarSeparator3.AccessibleName = "commandBarSeparator3";
            this.commandBarSeparator3.DisplayName = "commandBarSeparator3";
            this.commandBarSeparator3.Name = "commandBarSeparator3";
            this.commandBarSeparator3.VisibleInOverflowMenu = false;
            // 
            // commandBarLabel3
            // 
            this.commandBarLabel3.DisplayName = "commandBarLabel3";
            this.commandBarLabel3.Name = "commandBarLabel3";
            this.commandBarLabel3.Text = "Filter:";
            // 
            // commandBarTextBoxFilter
            // 
            this.commandBarTextBoxFilter.DisplayName = "commandBarTextBox1";
            this.commandBarTextBoxFilter.MinSize = new System.Drawing.Size(200, 0);
            this.commandBarTextBoxFilter.Name = "commandBarTextBoxFilter";
            this.commandBarTextBoxFilter.StretchHorizontally = true;
            this.commandBarTextBoxFilter.Text = "";
            // 
            // radPanel1
            // 
            this.radPanel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.radPanel1.Controls.Add(this.radCardView1);
            this.radPanel1.Controls.Add(this.radCommandBar1);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPanel1.Location = new System.Drawing.Point(0, 0);
            this.radPanel1.Name = "radPanel1";
            // 
            // 
            // 
            this.radPanel1.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 200, 100);
            this.radPanel1.Size = new System.Drawing.Size(1511, 917);
            this.radPanel1.TabIndex = 5;
            this.radPanel1.Text = "radPanel1";
            // 
            // UCThumbsPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.radPanel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "UCThumbsPreview";
            this.Size = new System.Drawing.Size(1511, 917);
            ((System.ComponentModel.ISupportInitialize)(this.dt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radCardView1.CardTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radCardView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radCommandBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Telerik.WinControls.UI.RadCardView radCardView1;
        private Telerik.WinControls.UI.LayoutControlLabelItem layoutControlLabelItem1;
        private Telerik.WinControls.UI.LayoutControlLabelItem layoutControlLabelItem2;
        private Telerik.WinControls.UI.RadCommandBar radCommandBar1;
        private Telerik.WinControls.UI.CommandBarRowElement commandBarRowElement1;
        private Telerik.WinControls.UI.CommandBarStripElement commandBarStripElement1;
        private Telerik.WinControls.UI.CommandBarLabel commandBarLabel1;
        private Telerik.WinControls.UI.CommandBarDropDownList commandBarDropDownSort;
        private Telerik.WinControls.UI.CommandBarSeparator commandBarSeparator1;
        private Telerik.WinControls.UI.CommandBarLabel commandBarLabel2;
        private Telerik.WinControls.UI.CommandBarDropDownList commandBarDropDownGroup;
        private Telerik.WinControls.UI.CommandBarSeparator commandBarSeparator2;
        private Telerik.WinControls.UI.CommandBarTextBox commandBarTextBoxFilter;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.CommandBarSeparator commandBarSeparator3;
        private Telerik.WinControls.UI.CommandBarSeparator commandBarSeparator4;
        private Telerik.WinControls.UI.CommandBarLabel commandBarLabel3;
        private DataTable dt;
        private Telerik.WinControls.UI.CardViewGroupItem cardViewGroupItem1;
        private Telerik.WinControls.UI.CardViewItem DocIdItem;
        private Telerik.WinControls.UI.CardViewItem IconIdItem;
        private Telerik.WinControls.UI.CardViewItem TaskItem;
        private Telerik.WinControls.UI.CardViewItem StepItem;
        private Telerik.WinControls.UI.CardViewItem Stateitem;
        private Telerik.WinControls.UI.CardViewItem AsignedItem;


    }
}