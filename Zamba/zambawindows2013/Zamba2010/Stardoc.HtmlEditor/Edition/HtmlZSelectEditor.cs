using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Stardoc.HtmlEditor.Enumerators;
using Stardoc.HtmlEditor.HtmlControls;

namespace Stardoc.HtmlEditor.Edition
{
    internal partial class HtmlZSelectEditor : Form
    {
        #region Constantes
        private const String TITLE_STATE_VIEW = "Resument elemento SELECT";
        private const String TITLE_STATE_UPDATE = "Edicion elemento SELECT";
        private const String TITLE_STATE_INSERT = "Creacion elemento SELECT"; 
        #endregion

        #region Atributos
        private List<OptionItem> _options;
        private HtmlFormState _state;
        private Int64 _indexId;
        #endregion

        #region Propiedades
        public HtmlFormState State
        {
            get { return _state; }
            set
            {
                _state = value;

                switch (_state)
                {
                    case HtmlFormState.View:
                        this.Text = TITLE_STATE_VIEW;
                        break;
                    case HtmlFormState.Update:
                        this.Text = TITLE_STATE_UPDATE;
                        break;
                    case HtmlFormState.Insert:
                        this.Text = TITLE_STATE_INSERT;
                        break;
                    default:
                        break;
                }
            }
        }
        public ZSelectItem SelectItem
        {
            get
            {
                return new ZSelectItem(tbName.Text, _options, chkEnabled.Checked, _indexId);
            }
            set
            {
                _options = value.Options;
                LoadOptions();
                _indexId = value.IndexId;
                chkEnabled.Checked = value.Enabled;
                tbName.Text = value.Name;
            }
        } 
        #endregion

        #region Constructores
        public HtmlZSelectEditor()
        {
            InitializeComponent();
        }
        public HtmlZSelectEditor(ZSelectItem item, HtmlFormState state)
            :this()
        {
            State = state;
            SelectItem = item;
        } 
        #endregion

        #region Eventos
        private void SelectEditor_Load(object sender, EventArgs e)
        {
            ActiveControl = btAdd;
            if (_state == HtmlFormState.Insert)
                chkEnabled.Checked = true;

            ValidateButtons();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            using (HtmlOptionEditor Dialog = new HtmlOptionEditor())
            {
                if (Dialog.ShowDialog() == DialogResult.OK)
                {
                    OptionItem InsertedOption = Dialog.Option;
                    lstItems.Items.Add(InsertedOption.ToString());
                    _options.Add(InsertedOption);

                    if (InsertedOption.Default)
                        ValidateDefault(lstItems.Items.Count - 1);
                }
            }
        }
        private void btUpdate_Click(object sender, EventArgs e)
        {
            if (-1 != lstItems.SelectedIndex)
            {
                OptionItem UpdatedOption = _options[lstItems.SelectedIndex];
                using (HtmlOptionEditor Dialog = new HtmlOptionEditor(UpdatedOption))
                {
                    if (Dialog.ShowDialog() == DialogResult.OK)
                    {
                        UpdatedOption = Dialog.Option;

                        lstItems.Items[lstItems.SelectedIndex] = UpdatedOption.ToString();
                        _options[lstItems.SelectedIndex] = UpdatedOption;

                        if (UpdatedOption.Default)
                            ValidateDefault(lstItems.SelectedIndex);
                    }
                }
            }
        }
        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        private void btAccept_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
        private void btDelete_Click(object sender, EventArgs e)
        {
            if (lstItems.SelectedItems.Count > 0)
            {
                Int32 CurrentIndex = -1;
                for (int i = lstItems.SelectedIndices.Count - 1; i >= 0; i--)
                {
                    CurrentIndex = lstItems.SelectedIndices[i];

                    lstItems.Items.RemoveAt(CurrentIndex);
                    _options.RemoveAt(CurrentIndex);
                }
            }
        }
        private void lstItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateButtons();
        }
        #endregion

        private void ValidateDefault(Int32 editedIndex)
        {
            for (int i = 0; i <= _options.Count - 1; i++)
            {
                if (i != editedIndex && _options[i].Default)//Valido todos los otros options
                {
                    _options[i].Default = false;
                    lstItems.Items[i] = _options[i].ToString();
                }
            }
        }
        private void ValidateButtons()
        {
            if ((lstItems.Items.Count == 0) || (lstItems.SelectedIndex == -1))
            {
                btUpdate.Enabled = false;
                btDelete.Enabled = false;
            }
            else
            {
                btUpdate.Enabled = true;
                btDelete.Enabled = true;
            }
        }
        private void LoadOptions()
        {
            lstItems.Items.Clear();

            foreach (OptionItem CurrentOption in _options)
                lstItems.Items.Add(CurrentOption.ToString());
        }
    }
}