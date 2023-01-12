using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Stardoc.HtmlEditor.Enumerators;
using Stardoc.HtmlEditor.HtmlControls;
using Stardoc.HtmlEditor.ListViewItems;
using Stardoc.HtmlEditor.Edition;

namespace Stardoc.HtmlEditor
{
    internal partial class HtmlControlsList
        : UserControl
    {
        #region Atributos
        private List<Int64> _docTypes = new List<Int64>();
        #endregion

        #region Propiedades
        public Dictionary<Int64, String> DocTypes
        {
            set
            {
                if (null == value || value.Count == 0)
                    EnableDocTypeList(false);
                else
                    EnableDocTypeList(true);

                LoadDocTypes(value);
            }
        }
        #endregion

        #region Constructores
        public HtmlControlsList()
        {
            InitializeComponent();
            EnableDocTypeList(false);
        }
        #endregion

        #region Eventos
        public event SelectedHtmlControl OnSelectedHtmlControl;
        public delegate void SelectedHtmlControl(IHtmlWritable selectedItem);

        public event SelectedDocTypeChanged OnSelectedDocTypeChanged;
        public delegate void SelectedDocTypeChanged(Int64? docTypeId);

        private void cmbDocTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            lvControls.Items.Clear();

            Int32 SelectedIndex = cmbDocTypes.SelectedIndex;

            if (null != OnSelectedDocTypeChanged)
                OnSelectedDocTypeChanged((Int64?)_docTypes[SelectedIndex]);
        }

        private void HtmlControlsList_Load(object sender, EventArgs e)
        {
            try
            {
                if (cmbDocTypes.Items.Count == 0)
                    EnableDocTypeList(false);

                ActiveControl = lvControls;
                LoadGroups();

                //ListViewItem Item = null;

                //#region Checkbox
                //ZCheckBoxItem zcb = new ZCheckBoxItem("Item 1", false);
                //zcb.Text = "CheckBox con Informacion de Zamba";
                //zcb.IndexId = 4654;
                //Item = new ListViewItems.ZCheckBox(0, "CheckBox con Informacion de Zamba", zcb);
                //Item.Group = GetItemGroup(HtmlControlType.CheckBox);
                //lvControls.Items.Add(Item);
                //#endregion

                //#region RadiobButton
                //ZRadioButtonItem zrb = new ZRadioButtonItem("False", "Items");
                //zrb.Checked = false;
                //zrb.SelectionValue = false;
                //zrb.IndexId = 123456;
                //Item = new ListViewItems.ZRadioButton(0, "False", zrb);
                //Item.Group = GetItemGroup(HtmlControlType.RadioButton);
                //lvControls.Items.Add(Item);


                //zrb = new ZRadioButtonItem("True", "Items");
                //zrb.Checked = true;
                //zrb.SelectionValue = true;
                //zrb.IndexId = 123457;
                //Item = new ListViewItems.ZRadioButton(0, "True", zrb);
                //Item.Group = GetItemGroup(HtmlControlType.RadioButton);
                //lvControls.Items.Add(Item);
                //#endregion

                //#region Select
                //ZSelectItem Select = new ZSelectItem();
                //Select.IndexId = 123456;
                //for (int i = 0; i < 10; i++)
                //{
                //    if (i == 4)
                //        Select.Options.Add(new OptionItem("Option " + i.ToString(), i.ToString(), true));
                //    else
                //        Select.Options.Add(new OptionItem("Option " + i.ToString(), i.ToString(), false));
                //}

                //Item = new ListViewItems.ZSelect(0, "SelectItem 123456", Select);
                //Item.Group = GetItemGroup(HtmlControlType.Select);
                //lvControls.Items.Add(Item);
                //#endregion

                //#region TextArea
                //ZTextAreaItem zta = new ZTextAreaItem(20, 12, "asd");
                //zta.IndexId = 45678;
                //zta.InnerText = "Texto del TextArea";
                //Item = new ListViewItems.ZTextArea(0, "TextArea con Informacion de Zamba", zta);
                //Item.Group = GetItemGroup(HtmlControlType.TextArea);
                //lvControls.Items.Add(Item);
                //#endregion

                //#region TextBox
                //ZTextBoxItem ztb = new ZTextBoxItem("Textbox Con informacion de Zamba", "sarasasa");
                //ztb.ReadOnly = false;
                //ztb.IndexId = 54654654;

                //Item = new ListViewItems.ZTextBox(0, "Textbox Con informacion de Zamba", ztb);
                //Item.Group = GetItemGroup(HtmlControlType.TextBox);
                //lvControls.Items.Add(Item);

                //ztb = new ZTextBoxItem("Textbox Con informacion de Zamba READONLY", "sarasasa");
                //ztb.ReadOnly = true;
                //ztb.IndexId = 45;

                //Item = new ListViewItems.ZTextBox(0, "Textbox Con informacion de Zamba READONLY", ztb);
                //Item.Group = GetItemGroup(HtmlControlType.TextBox);
                //lvControls.Items.Add(Item);

                //#endregion

                //Item = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void comboBoxesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ListHtmlCheckbox dialog = new ListHtmlCheckbox())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ListViewItem Item = null;
                    foreach (CheckBoxItem CurrentCheckBox in dialog.CheckBoxes)
                    {
                        Item = new ListViewItems.CheckBox(0, CurrentCheckBox.ToString(), CurrentCheckBox);
                        Item.Group = GetItemGroup(HtmlControlType.CheckBox);
                        lvControls.Items.Add(Item);
                    }
                }
            }
        }

        private void radioButtonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ListHtmlRadioButton dialog = new ListHtmlRadioButton())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ListViewItem Item = null;
                    foreach (RadioButtonItem CurrentRadioButton in dialog.RadioButtons)
                    {
                        Item = new ListViewItems.RadioButton(0, CurrentRadioButton.ToString(), CurrentRadioButton);
                        Item.Group = GetItemGroup(HtmlControlType.RadioButton);
                        lvControls.Items.Add(Item);
                    }
                }
            }
        }

        private void selectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (HtmlSelectEditor dialog = new HtmlSelectEditor())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    SelectItem CurrentSelect = dialog.SelectItem;
                    ListViewItem Item = new ListViewItems.Select(0, CurrentSelect.ToString(), CurrentSelect);
                    Item.Group = GetItemGroup(HtmlControlType.Select);
                    lvControls.Items.Add(Item);
                }
            }
        }

        private void textAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (HtmlTextAreaEditor dialog = new HtmlTextAreaEditor())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    TextAreaItem CurrentTextArea = dialog.TextArea;
                    ListViewItem Item = new ListViewItems.TextArea(0, CurrentTextArea.ToString(), CurrentTextArea);
                    Item.Group = GetItemGroup(HtmlControlType.TextArea);
                    lvControls.Items.Add(Item);
                }
            }
        }

        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvControls.SelectedItems.Count > 0)
                EditSelectedControl();
        }

        private void EditSelectedControl()
        {
            IHtmlWritable SelectedItem = GetSelectedItem();

            if (null != SelectedItem)
            {
                switch (SelectedItem.Type)
                {
                    case HtmlControlType.CheckBox:

                        if (lvControls.SelectedItems[0] is ZCheckBox)
                        {
                            ZCheckBox CurrentItem = (ZCheckBox)lvControls.SelectedItems[0];

                            HtmlZCheckbox Form = new HtmlZCheckbox(CurrentItem.InnerControl, HtmlFormState.Update);
                            if (Form.ShowDialog() == DialogResult.OK)
                                CurrentItem.InnerControl = Form.CheckedBox;
                        }
                        else if (lvControls.SelectedItems[0] is ListViewItems.CheckBox)
                        {
                            ListViewItems.CheckBox CurrentItem = (ListViewItems.CheckBox)lvControls.SelectedItems[0];

                            HtmlCheckbox Form = new HtmlCheckbox(CurrentItem.InnerControl, HtmlFormState.Update);
                            if (Form.ShowDialog() == DialogResult.OK)
                                CurrentItem.InnerControl = Form.CheckedBox;
                        }

                        break;
                    case HtmlControlType.CheckBoxes:
                        break;
                    case HtmlControlType.Select:
                        if (lvControls.SelectedItems[0] is ZSelect)
                        {
                            ZSelect CurrentItem = (ZSelect)lvControls.SelectedItems[0];

                            HtmlZSelectEditor Form = new HtmlZSelectEditor(CurrentItem.InnerControl, HtmlFormState.Update);
                            if (Form.ShowDialog() == DialogResult.OK)
                                CurrentItem.InnerControl = Form.SelectItem;
                        }
                        else if (lvControls.SelectedItems[0] is ListViewItems.Select)
                        {
                            Select CurrentItem = (Select)lvControls.SelectedItems[0];

                            HtmlSelectEditor Form = new HtmlSelectEditor(CurrentItem.InnerControl, HtmlFormState.Update);
                            if (Form.ShowDialog() == DialogResult.OK)
                                CurrentItem.InnerControl = Form.SelectItem;
                        }
                        break;
                    case HtmlControlType.Selects:
                        break;
                    case HtmlControlType.TextArea:
                        if (lvControls.SelectedItems[0] is ZTextArea)
                        {
                            ZTextArea CurrentItem = (ZTextArea)lvControls.SelectedItems[0];

                            HtmlZTextAreaEditor Form = new HtmlZTextAreaEditor(CurrentItem.InnerControl, HtmlFormState.Update);
                            if (Form.ShowDialog() == DialogResult.OK)
                                CurrentItem.InnerControl = Form.TextArea;
                        }
                        else if (lvControls.SelectedItems[0] is ListViewItems.TextArea)
                        {
                            TextArea CurrentItem = (TextArea)lvControls.SelectedItems[0];

                            HtmlTextAreaEditor Form = new HtmlTextAreaEditor(CurrentItem.InnerControl, HtmlFormState.Update);
                            if (Form.ShowDialog() == DialogResult.OK)
                                CurrentItem.InnerControl = Form.TextArea;
                        }
                        break;
                    case HtmlControlType.RadioButton:
                        if (lvControls.SelectedItems[0] is ZRadioButton)
                        {
                            ZRadioButton CurrentItem = (ZRadioButton)lvControls.SelectedItems[0];

                            HtmlZRadioButtonEditor Form = new HtmlZRadioButtonEditor(CurrentItem.InnerControl, HtmlFormState.Update);
                            if (Form.ShowDialog() == DialogResult.OK)
                                CurrentItem.InnerControl = Form.Radio;
                        }
                        else if (lvControls.SelectedItems[0] is ListViewItems.RadioButton)
                        {
                            ListViewItems.RadioButton CurrentItem = (ListViewItems.RadioButton)lvControls.SelectedItems[0];

                            HtmlRadioButtonEditor Form = new HtmlRadioButtonEditor(CurrentItem.InnerControl, HtmlFormState.Update);
                            if (Form.ShowDialog() == DialogResult.OK)
                                CurrentItem.InnerControl = Form.Radio;
                        }
                        break;
                    case HtmlControlType.RadioButtons:
                        break;
                    case HtmlControlType.TextBox:
                        if (lvControls.SelectedItems[0] is ZTextBox)
                        {
                            ZTextBox CurrentItem = (ZTextBox)lvControls.SelectedItems[0];

                            HtmlZTextBox Form = new HtmlZTextBox(CurrentItem.InnerControl, HtmlFormState.Update);
                            if (Form.ShowDialog() == DialogResult.OK)
                                CurrentItem.InnerControl = Form.TextBox;
                        }
                        else if (lvControls.SelectedItems[0] is ListViewItems.TextBox)
                        {
                            ListViewItems.TextBox CurrentItem = (ListViewItems.TextBox)lvControls.SelectedItems[0];

                            HtmlTextBox Form = new HtmlTextBox(CurrentItem.InnerControl, HtmlFormState.Update);
                            if (Form.ShowDialog() == DialogResult.OK)
                                CurrentItem.InnerControl = Form.TextBox;
                        }
                        break;
                    case HtmlControlType.TextBoxes:
                        break;
                    default:
                        break;
                }
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvControls.SelectedItems.Count > 0)
            {
                Int32 SelectedIndex = lvControls.SelectedIndices[0];
                lvControls.Items.RemoveAt(SelectedIndex);
            }
        }

        private void zTextAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (HtmlZTextAreaEditor dialog = new HtmlZTextAreaEditor())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ZTextAreaItem CurrentTextArea = dialog.TextArea;
                    ListViewItem Item = new ListViewItems.ZTextArea(0, CurrentTextArea.ToString(), CurrentTextArea);
                    Item.Group = GetItemGroup(HtmlControlType.TextArea);
                    lvControls.Items.Add(Item);
                }
            }
        }

        private void zComboBoxesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (HtmlZCheckbox dialog = new HtmlZCheckbox())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ZCheckBoxItem CurrentCheckBox = dialog.CheckedBox;
                    ListViewItem Item = new ListViewItems.ZCheckBox(0, CurrentCheckBox.ToString(), CurrentCheckBox);
                    Item.Group = GetItemGroup(HtmlControlType.CheckBox);
                    lvControls.Items.Add(Item);
                }
            }
        }

        private void zRadioButtosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (HtmlZRadioButtonEditor dialog = new HtmlZRadioButtonEditor())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ZRadioButtonItem CurrentRadioButton = dialog.Radio;
                    ListViewItem Item = new ListViewItems.ZRadioButton(0, CurrentRadioButton.ToString(), CurrentRadioButton);
                    Item.Group = GetItemGroup(HtmlControlType.RadioButton);
                    lvControls.Items.Add(Item);
                }
            }
        }

        private void zSelectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (HtmlZSelectEditor dialog = new HtmlZSelectEditor())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ZSelectItem CurrentRadioButton = dialog.SelectItem;
                    ListViewItem Item = new ListViewItems.ZSelect(0, CurrentRadioButton.ToString(), CurrentRadioButton);
                    Item.Group = GetItemGroup(HtmlControlType.Select);
                    lvControls.Items.Add(Item);
                }
            }
        }

        private void lvControls_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
        }

        private void lvControls_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (null != lvControls.SelectedItems && lvControls.SelectedItems.Count == 1)
            {
                lvControls.SelectedItems[0].BackColor = Color.Red;
                lvControls.SelectedItems[0].ForeColor = Color.Red;
            }

            OnSelectedHtmlControl(GetSelectedItem());
        }

        private void cmsControls_Opening(object sender, CancelEventArgs e)
        {
            if (null == GetSelectedItem())
            {
                cmsControls.Items[0].Visible = true;
                cmsControls.Items[1].Visible = false;
                cmsControls.Items[2].Visible = false;
            }
            else
            {
                cmsControls.Items[0].Visible = false;
                cmsControls.Items[1].Visible = true;
                cmsControls.Items[2].Visible = true;
            }
        }

        private void lvControls_DoubleClick(object sender, EventArgs e)
        {
            EditSelectedControl();
        }
        #endregion

        /// <summary>
        /// Carga todos los grupos en el listview. Cada grupo es 1 tipo de control HTML.
        /// </summary>
        private void LoadGroups()
        {
            ListViewGroup Group = null;
            try
            {
                foreach (String ControlsCategory in GetControlTypesNames())
                {
                    Group = new ListViewGroup(ControlsCategory);
                    Group.HeaderAlignment = HorizontalAlignment.Left;
                    Group.Name = ControlsCategory;

                    lvControls.Groups.Add(Group);
                }
            }
            finally
            {
                Group = null;
            }
        }

        /// <summary>
        /// Devuelve un listado de todos los tipos de control HTML
        /// </summary>
        /// <returns></returns>
        private String[] GetControlTypesNames()
        {
            return Enum.GetNames(typeof(HtmlControlType));
        }

        /// <summary>
        /// Devuelve el ListViewGroup correspondiente al tipo del control HTML.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private ListViewGroup GetItemGroup(HtmlControlType type)
        {
            String TypeName = GetControlTypeName(type);
            return lvControls.Groups[TypeName];
        }

        /// <summary>
        /// Devuelve el nombre del tipo de control
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private String GetControlTypeName(HtmlControlType type)
        {
            String ControlTypeName = null;

            switch (type)
            {
                case HtmlControlType.CheckBox:
                    ControlTypeName = "CheckBox";
                    break;
                case HtmlControlType.CheckBoxes:
                    ControlTypeName = "CheckBoxes";
                    break;
                case HtmlControlType.Select:
                    ControlTypeName = "Select";
                    break;
                case HtmlControlType.Selects:
                    ControlTypeName = "Selects";
                    break;
                case HtmlControlType.TextArea:
                    ControlTypeName = "TextArea";
                    break;
                case HtmlControlType.RadioButton:
                    ControlTypeName = "RadioButton";
                    break;
                case HtmlControlType.RadioButtons:
                    ControlTypeName = "RadioButtons";
                    break;
                case HtmlControlType.TextBox:
                    ControlTypeName = "TextBox";
                    break;
                case HtmlControlType.TextBoxes:
                    ControlTypeName = "TextBoxes";
                    break;
                default:
                    break;
            }

            return ControlTypeName;
        }

        /// <summary>
        /// Devuelve el control seleccionado. Se usa la interfaz de IHtmlWritable que sirve para saber
        /// que tipo de control es y llamar a su metodo que devuelve el html.
        /// </summary>
        /// <returns></returns>
        public IHtmlWritable GetSelectedItem()
        {
            if (null != lvControls.SelectedItems && lvControls.SelectedItems.Count == 1)
                return (IHtmlWritable)lvControls.SelectedItems[0];
            else
                return null;
        }

        /// <summary>
        /// Borra del listado el control seleccionado. Se usa cuando ya se agrego 1 control al editor de 
        /// Html.
        /// </summary>
        public void DeleteSelectedControl()
        {
            if (lvControls.SelectedIndices.Count == 1)
                lvControls.Items.RemoveAt(lvControls.SelectedIndices[0]);
        }

        /// <summary>
        /// Habilita o desabilita el combobox y el label del listado de DocTypes. Se desahibilita cuando
        /// no tiene items cargados.
        /// </summary>
        /// <param name="enable"></param>
        private void EnableDocTypeList(Boolean enable)
        {
            cmbDocTypes.Visible = enable;
            lbDocTypesList.Visible = enable;
        }

        public void LoadHtmlElements(List<IHtmlElement> elements)
        {
            lvControls.Items.Clear();
            LoadGroups();

            foreach (IHtmlElement CurrentElement in elements)
            {
                switch (CurrentElement.Type)
                {
                    case HtmlControlType.CheckBox:
                        ListViewItems.CheckBox Item = new ListViewItems.CheckBox(0, CurrentElement.ToString(),(CheckBoxItem)  CurrentElement);
                        Item.Group = GetItemGroup(HtmlControlType.CheckBox);
                        lvControls.Items.Add(Item);
                        break;
                    case HtmlControlType.CheckBoxes:
                        break;
                    case HtmlControlType.Select:
                        Select SelectItem = new Select(0, CurrentElement.ToString(), (SelectItem)CurrentElement);
                        SelectItem.Group = GetItemGroup(HtmlControlType.Select);
                        lvControls.Items.Add(SelectItem);
                        break;
                    case HtmlControlType.Selects:
                        break;
                    case HtmlControlType.TextArea:
                        TextArea TextAreaItem = new TextArea(0, CurrentElement.ToString(), (TextAreaItem)CurrentElement);
                        TextAreaItem.Group = GetItemGroup(HtmlControlType.TextArea);
                        lvControls.Items.Add(TextAreaItem);
                        break;
                    case HtmlControlType.RadioButton:
                        ListViewItems.RadioButton RadioItem = new ListViewItems.RadioButton(0, CurrentElement.ToString(), (RadioButtonItem)CurrentElement);
                        RadioItem.Group = GetItemGroup(HtmlControlType.RadioButton);
                        lvControls.Items.Add(RadioItem);
                        break;
                    case HtmlControlType.RadioButtons:
                        break;
                    case HtmlControlType.TextBox:
                        ListViewItems.TextBox TextBoxItem = new ListViewItems.TextBox(0, CurrentElement.ToString(), (TextBoxItem)CurrentElement);
                        TextBoxItem.Group = GetItemGroup(HtmlControlType.TextBox);
                        lvControls.Items.Add(TextBoxItem);
                        break;
                    case HtmlControlType.TextBoxes:
                        break;
                    case HtmlControlType.Option:
                        break;
                    case HtmlControlType.Options:
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Carga todos los tipos de documentos en cmboDocTypes
        /// </summary>
        private void LoadDocTypes(Dictionary<Int64, String> docTypes)
        {
            _docTypes.Clear();
            foreach (KeyValuePair<Int64, String> CurrentDocType in docTypes)
            {
                cmbDocTypes.Items.Add(CurrentDocType.Value);
                _docTypes.Add(CurrentDocType.Key);
            }

            lvControls.Items.Clear();

            if (cmbDocTypes.Items.Count > 0)
                cmbDocTypes.SelectedIndex = 0;
        }
    }
}
