using Stardoc.HtmlEditor.Enumerators;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using Stardoc.HtmlEditor.HtmlControls;

namespace Stardoc.HtmlEditor
{
    internal partial class HtmlSelectEditor
        : Form
    {
        #region Constatens
        private const String TITLE_STATE_VIEW = "Resumen elemento SELECT";
        private const String TITLE_STATE_UPDATE = "Edicion elemento SELECT";
        private const String TITLE_STATE_INSERT = "Creacion elemento SELECT";
        #endregion

        #region Atributos
        private List<OptionItem> _options;
        private HtmlFormState _state;
        #endregion

        #region Propiedades
        public SelectItem SelectItem
        {
            get { return new SelectItem(tbName.Text, _options, chkEnabled.Checked); }
            set
            {
                _options = value.Options;
                tbName.Text = value.Name;
                LoadOptions();
                chkEnabled.Checked = value.Enabled;
            }
        }
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
        #endregion

        #region Constructores
        public HtmlSelectEditor()
        {
            InitializeComponent();
            _options = new List<OptionItem>();
        }
        public HtmlSelectEditor(SelectItem item, HtmlFormState state)
            : this()
        {
            State = state;
            SelectItem = item;
        } 
        #endregion

        #region Eventos
        private void SelectEditor_Load(object sender, EventArgs e)
        {
            ActiveControl = btAdd;
            ValidateButtons();

            if (_state == HtmlFormState.Insert)
                chkEnabled.Checked = true;
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

