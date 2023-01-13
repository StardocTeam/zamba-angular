using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
        #region Constantes
        private const String DOC_TYPE_NAME_PREFIX = "Entidad: ";
        private const String COLUMN_INDEX_NAME = "Tipo";
        private const String COLUMN_INDEX_TYPE = "Nombre";
        #endregion

        #region Atributos
        private List<Int64> _docTypes = new List<Int64>();
        private String _docTypeName;
        #endregion

        #region Propiedades
        /// <summary>
        /// Establece u obtiene el nombre de la entidad
        /// </summary>
        public String DocTypeName
        {
            set
            {
                _docTypeName = value;
                lbDocTypeName.Text = DOC_TYPE_NAME_PREFIX + _docTypeName;
            }
            get
            {
                return _docTypeName;
            }
        }
        #endregion

        #region Constructores
        public HtmlControlsList()
        {
            InitializeComponent();
        }
        #endregion

        #region Eventos
        internal event SelectedHtmlControl OnSelectedHtmlControl;
        internal delegate void SelectedHtmlControl(IHtmlWritable selectedItem);


        internal delegate void ReloadIndexes();
        internal event ReloadIndexes OnReloadIndexes;

        private void HtmlControlsList_Load(object sender, EventArgs e)
        {
            try
            {
                ActiveControl = lvControls;
                LoadGroups();

                if (lvControls.Items.Count == 0)
                {
                    lvControls.Enabled = false;
                    lbDocTypeName.Text = "No hay atributos para mostrar";
                }
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
        /// que tipo de control es y llamar _frmRules su metodo que devuelve el html.
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
            {
                lvControls.Items.RemoveAt(lvControls.SelectedIndices[0]);
            }
        }

        public void LoadHtmlElements(List<BaseHtmlElement> elements)
        {
            lvControls.Items.Clear();
            ListViewItem item = null;


            elements.Sort(new ElementsSorter());


            foreach (BaseHtmlElement CurrentElement in elements)
            {
                switch (CurrentElement.Type())
                {
                    case HtmlControlType.CheckBox:
                        if (CurrentElement is ZCheckBoxItem)
                            item = (ListViewItem)new ListViewItems.ZCheckBox(0, CurrentElement.ToString(), (ZCheckBoxItem)CurrentElement);
                        else if (CurrentElement is CheckBoxItem)
                            item = (ListViewItem)new ListViewItems.CheckBox(0, CurrentElement.ToString(), (CheckBoxItem)CurrentElement);
                        break;
                    case HtmlControlType.CheckBoxes:
                        break;
                    case HtmlControlType.Select:
                        if (CurrentElement is ZSelectItem)
                            item = (ListViewItem)new ZSelect(3, CurrentElement.ToString(), (ZSelectItem)CurrentElement);
                        else if (CurrentElement is SelectItem)
                            item = (ListViewItem)new Select(3, CurrentElement.ToString(), (SelectItem)CurrentElement);
                        break;
                    case HtmlControlType.Selects:
                        break;
                    case HtmlControlType.TextArea:
                        if (CurrentElement is ZTextAreaItem)
                            item = new ZTextArea(2, CurrentElement.ToString(), (ZTextAreaItem)CurrentElement);
                        else if (CurrentElement is TextAreaItem)
                            item = new TextArea(2, CurrentElement.ToString(), (TextAreaItem)CurrentElement);
                        break;
                    case HtmlControlType.RadioButton:
                        if (CurrentElement is ZRadioButtonItem)
                            item = (ListViewItem)new ListViewItems.ZRadioButton(4, CurrentElement.ToString(), (ZRadioButtonItem)CurrentElement);
                        else if (CurrentElement is RadioButtonItem)
                            item = (ListViewItem)new ListViewItems.RadioButton(4, CurrentElement.ToString(), (RadioButtonItem)CurrentElement);
                        break;
                    case HtmlControlType.RadioButtons:
                        break;
                    case HtmlControlType.TextBox:
                        if (CurrentElement is ZTextBoxItem)
                            item = (ListViewItem)new ListViewItems.ZTextBox(1, CurrentElement.ToString(), (ZTextBoxItem)CurrentElement);
                        else if (CurrentElement is TextBoxItem)
                            item = (ListViewItem)new ListViewItems.TextBox(1, CurrentElement.ToString(), (TextBoxItem)CurrentElement);
                        break;
                    case HtmlControlType.TextBoxes:
                        break;
                    case HtmlControlType.Option:
                        break;
                    case HtmlControlType.Options:
                        break;
                    case HtmlControlType.Table:
                        item = (ListViewItem)new ListViewItems.Table(1, CurrentElement.ToString(), (TableItem)CurrentElement);
                        break;
                    case HtmlControlType.Button:
                        item = (ListViewItem)new ListViewItems.Button(1, CurrentElement.ToString(), (ButtonItem)CurrentElement);
                        break;
                    default:
                        break;
                }

                lvControls.Items.Add(item);
            }



            lvControls.Enabled = lvControls.Items.Count != 0;
        }

        private void btRefreshIndexes_Click(object sender, EventArgs e)
        {
            if (null != OnReloadIndexes)
                OnReloadIndexes();
        }


        private class ElementsSorter
            : IComparer<BaseHtmlElement>
        {
            public int Compare(BaseHtmlElement x, BaseHtmlElement y)
            {
                return x.Name.CompareTo(y.Name);
            }
        }
    }
}