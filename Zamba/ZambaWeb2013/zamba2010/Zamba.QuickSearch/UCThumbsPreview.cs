using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Data;
using Zamba.Data;
using Zamba.Core;
using Zamba.Framework;
using Zamba.AppBlock;

namespace Zamba.QuickSearch
{
    public partial class UCThumbsPreview : UserControl
    {
        Font makeFont = new Font("Segoe UI Light", 14f);
        Font yearFont = new Font("Segoe UI", 11.5f);
        Font checkBoxFont = new Font("Segoe UI", 9.5f);

        public UCThumbsPreview()
        {
            InitializeComponent();
        }
        public UCThumbsPreview(DataTable ResultData)
        {
            this.dt = ResultData;
            InitializeComponent();

            ImagePrimitive searchIcon = new ImagePrimitive();
            //searchIcon.Image = global::Zamba.QuickSearch.Properties.Resources.appbar_magnify;
            searchIcon.Alignment = ContentAlignment.MiddleRight;
            this.commandBarTextBoxFilter.TextBoxElement.StretchHorizontally = true;
            this.commandBarTextBoxFilter.TextBoxElement.Alignment = ContentAlignment.MiddleRight;
            this.commandBarTextBoxFilter.TextBoxElement.Children.Add(searchIcon);
            this.commandBarTextBoxFilter.TextBoxElement.TextBoxItem.Alignment = ContentAlignment.MiddleLeft;
            this.commandBarTextBoxFilter.TextBoxElement.TextBoxItem.PropertyChanged += TextBoxItem_PropertyChanged;
            this.radCardView1.AllowEdit = false;

            this.radCardView1.CardViewItemCreating += radCardView1_CardViewItemCreating;
            this.radCardView1.CardViewItemFormatting += radCardView1_CardViewItemFormatting;
            this.radCardView1.SortDescriptors.CollectionChanged += SortDescriptors_CollectionChanged;
            this.commandBarTextBoxFilter.TextChanged += commandBarTextBoxFilter_TextChanged;
            this.commandBarDropDownSort.SelectedIndexChanged += commandBarDropDownSort_SelectedIndexChanged;
            this.commandBarDropDownGroup.SelectedIndexChanged += commandBarDropDownGroup_SelectedIndexChanged;
            this.radCardView1.DataSource = null;
            this.radCardView1.DataSource = ResultData;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.commandBarDropDownGroup.SelectedIndex = 1;

        }


        private bool ContainsFeature(ListViewDataItem item, string feature)
        {
            return item[feature] != null && Convert.ToInt32(item[feature]) != 0;
        }

        Font checkBoxItemsFont = new Font("Segoe UI", 10.5f);
        void radCardView1_CardViewItemCreating(object sender, CardViewItemCreatingEventArgs e)
        {
            CardViewGroupItem groupItem = e.NewItem as CardViewGroupItem;
            if (groupItem != null) {
                groupItem.DrawText = true;
                groupItem.Font = this.checkBoxItemsFont;
                groupItem.Text = "Entidad";
            }

            CardViewItem cardViewItem = e.NewItem as CardViewItem;
            if (cardViewItem == null || string.IsNullOrEmpty(cardViewItem.FieldName))
            {
                return;
            }


            //if (this.features.Contains(cardViewItem.FieldName))
            //{
            //    CheckBoxCardViewItem checkBoxItem = new CheckBoxCardViewItem();
            //    checkBoxItem.FieldName = cardViewItem.FieldName;
            //    e.NewItem = checkBoxItem;
            //}
        }

        void radCardView1_CardViewItemFormatting(object sender, CardViewItemFormattingEventArgs e)
        {
            try
            {

                CardViewItem cardViewItem = e.Item as CardViewItem;
                if (cardViewItem == null || string.IsNullOrEmpty(cardViewItem.FieldName))
                {
                    return;
                }

                if (cardViewItem.FieldName == "ICON_ID")
                {

                    cardViewItem.TextSizeMode = LayoutItemTextSizeMode.Fixed;
                    cardViewItem.TextFixedSize = 0;
                    cardViewItem.EditorItem.DrawText = false;
                    cardViewItem.EditorItem.DrawImage = true;
                    ZThumbsBusiness TB = new ZThumbsBusiness();
                    Image image = TB.GetThumb(Int64.Parse(e.VisualItem.Data["Doc_Id"].ToString()));
                    if (image != null)
                    {                        
                        float factor = 160 / (float)image.Height;
                        Bitmap resizedImage = new Bitmap(image, new SizeF(factor * image.Width, factor * image.Height).ToSize());
                        cardViewItem.EditorItem.Image = resizedImage;
                    }
                    else
                    {
                        ZIconsList IL = new ZIconsList();
                        image = IL.ZIconList.Images[int.Parse(e.VisualItem.Data["ICON_ID"].ToString())];
                        float factor = 160 / (float)image.Height;
                        Bitmap resizedImage = new Bitmap(image, new SizeF(factor * image.Width, factor * image.Height).ToSize());
                        cardViewItem.EditorItem.Image = resizedImage;
                    }
                }
                else if (cardViewItem.FieldName == "Tarea")
                {
                    cardViewItem.EditorItem.Font = makeFont;
                    cardViewItem.EditorItem.Text = e.VisualItem.Data["Tarea"].ToString().Trim();
                }
                else if (cardViewItem.FieldName == "Entidad")
                {
                    cardViewItem.EditorItem.Font = makeFont;
                    cardViewItem.EditorItem.Text = e.VisualItem.Data["Entidad"].ToString().Trim() + "  " + e.VisualItem.Data["Etapa"];
                }
                else if (cardViewItem.FieldName == "Estado")
                {
                    cardViewItem.EditorItem.Font = yearFont;
                    cardViewItem.EditorItem.Text = e.VisualItem.Data["Estado"].ToString().Trim();
                }
                else if (!GridColumns.ColumnsVisibility.ContainsKey("cardViewItem.FieldName"))
                {
                    this.radCardView1.CardTemplate.HiddenItems.Add(cardViewItem);
                    cardViewItem.Visibility = Telerik.WinControls.ElementVisibility.Hidden;
                }
                else
                {
                    this.radCardView1.CardTemplate.HiddenItems.Add(cardViewItem);
                    cardViewItem.Visibility = Telerik.WinControls.ElementVisibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

        }


        private void commandBarDropDownSort_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            this.radCardView1.SortDescriptors.CollectionChanged -= SortDescriptors_CollectionChanged;

            this.radCardView1.SortDescriptors.Clear();
            switch (this.commandBarDropDownSort.Text)
            {
                case "Entidad":
                    this.radCardView1.SortDescriptors.Add(new SortDescriptor("Entidad", ListSortDirection.Ascending));
                    this.radCardView1.EnableSorting = true;
                    break;
                case "Etapa":
                    this.radCardView1.SortDescriptors.Add(new SortDescriptor("Etapa", ListSortDirection.Ascending));
                    this.radCardView1.EnableSorting = true;
                    break;
                case "Category":
                    this.radCardView1.SortDescriptors.Add(new SortDescriptor("Estado", ListSortDirection.Ascending));
                    this.radCardView1.EnableSorting = true;
                    break;
                case "Year":
                    this.radCardView1.SortDescriptors.Add(new SortDescriptor("Proceso", ListSortDirection.Ascending));
                    this.radCardView1.EnableSorting = true;
                    break;
            }

            this.radCardView1.SortDescriptors.CollectionChanged += SortDescriptors_CollectionChanged;
        }

        private void commandBarDropDownGroup_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            this.radCardView1.GroupDescriptors.Clear();
            switch (this.commandBarDropDownGroup.Text)
            {
                case "None":
                    this.radCardView1.EnableGrouping = false;
                    this.radCardView1.ShowGroups = false;
                    break;
                case "Entidad":
                    this.radCardView1.GroupDescriptors.Add(new GroupDescriptor(
                        new SortDescriptor[] { new SortDescriptor("Entidad", ListSortDirection.Ascending) }));
                    this.radCardView1.EnableGrouping = true;
                    this.radCardView1.ShowGroups = true;
                    break;
                case "Estado":
                    this.radCardView1.GroupDescriptors.Add(new GroupDescriptor(
                        new SortDescriptor[] { new SortDescriptor("Estado", ListSortDirection.Ascending) }));
                    this.radCardView1.EnableGrouping = true;
                    this.radCardView1.ShowGroups = true;
                    break;
                case "Proceso":
                    this.radCardView1.GroupDescriptors.Add(new GroupDescriptor(
                        new SortDescriptor[] { new SortDescriptor("Proceso", ListSortDirection.Ascending) }));
                    this.radCardView1.EnableGrouping = true;
                    this.radCardView1.ShowGroups = true;
                    break;
            }
        }

        private void commandBarTextBoxFilter_TextChanged(object sender, EventArgs e)
        {
            this.radCardView1.FilterDescriptors.Clear();

            if (String.IsNullOrEmpty(this.commandBarTextBoxFilter.Text))
            {
                this.radCardView1.EnableFiltering = false;
            }
            else
            {
                this.radCardView1.FilterDescriptors.LogicalOperator = FilterLogicalOperator.Or;
                this.radCardView1.FilterDescriptors.Add("Entidad", FilterOperator.Contains, this.commandBarTextBoxFilter.Text);
                this.radCardView1.FilterDescriptors.Add("Etapa", FilterOperator.Contains, this.commandBarTextBoxFilter.Text);
                this.radCardView1.EnableFiltering = true;
            }
        }

        private void TextBoxItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Bounds")
            {
                commandBarTextBoxFilter.TextBoxElement.TextBoxItem.HostedControl.MaximumSize = new Size((int)commandBarTextBoxFilter.DesiredSize.Width - 28, 0);
            }
        }

        private void SortDescriptors_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (this.radCardView1.SortDescriptors.Count == 0)
            {
                this.commandBarDropDownSort.SelectedIndex = 0;
                return;
            }

            string columnName = this.radCardView1.Columns[this.radCardView1.SortDescriptors[0].PropertyName].HeaderText;
            if (columnName == "Proceso")
            {
                columnName = "Proceso";
            }
            RadListDataItem item = this.commandBarDropDownSort.ListElement.FindItemExact(columnName, false);
            if (item != null)
            {
                this.commandBarDropDownSort.SelectedItem = item;
            }
        }
    }

    public class CheckBoxCardViewItem : CardViewItem
    {
        protected override void CreateChildElements()
        {
            base.CreateChildElements();
            this.TextSizeMode = LayoutItemTextSizeMode.Proportional;
            this.TextProportionalSize = 0;
        }

        protected override CardViewEditorItem CreateEditorItem()
        {
            return new CheckBoxEditorItem();
        }

        public override void Synchronize()
        {
            CardListViewVisualItem cardVisualItem = this.FindAncestor<CardListViewVisualItem>();
            if (this.CardField == null || cardVisualItem == null || cardVisualItem.Data == null)
            {
                return;
            }

            RadCheckBoxElement checkBox = (this.EditorItem as CheckBoxEditorItem).Checkbox;
            checkBox.Text = this.CardField.HeaderText;
            object data = cardVisualItem.Data[this.CardField];
            checkBox.Checked = this.ContainsFeature(cardVisualItem.Data, this.FieldName);
        }

        private bool ContainsFeature(ListViewDataItem item, string feature)
        {
            return item[feature] != null && Convert.ToInt32(item[feature]) != 0;
        }
    }

    public class CheckBoxEditorItem : CardViewEditorItem
    {
        private RadCheckBoxElement checkbox;

        public RadCheckBoxElement Checkbox
        {
            get { return this.checkbox; }
            set { this.checkbox = value; }
        }

        protected override void CreateChildElements()
        {
            base.CreateChildElements();
            this.checkbox = new RadCheckBoxElement();
            this.Children.Add(this.checkbox);
            this.checkbox.ToggleStateChanged += checkbox_ToggleStateChanged;
        }

        void checkbox_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            // on check box value changed we need to change the value in DataSource
        }
    }
}
