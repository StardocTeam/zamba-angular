using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Stardoc.HtmlEditor.Enumerators;
using Stardoc.HtmlEditor.HtmlControls;

internal partial class ListHtmlCheckbox 
    : Form
{
    #region Atributos
    private HtmlFormState _state = HtmlFormState.Insert;
    private List<CheckBoxItem> _checkBoxes = null;
    #endregion

    #region Propiedades
    public List<CheckBoxItem> CheckBoxes
    {
        get { return _checkBoxes; }
    }
    #endregion

    #region Constructores
    public ListHtmlCheckbox()
    {
        InitializeComponent();
    }
    public ListHtmlCheckbox(List<CheckBoxItem> checkBoxes, HtmlFormState state)
    {
        InitializeComponent();
        _checkBoxes = checkBoxes;
        _state = state;

        foreach (CheckBoxItem CurrentItem in _checkBoxes)
            lstItems.Items.Add(CurrentItem.ToString());
    }
    #endregion

    #region Eventos
    private void btAccept_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
        Close();
    }
    private void btCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
    private void btAdd_Click(object sender, EventArgs e)
    {
        using (HtmlCheckbox dialog = new HtmlCheckbox())
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                lstItems.Items.Add(dialog.CheckedBox.ToString());
                _checkBoxes.Add(dialog.CheckedBox);
            }
        }
    }
    private void btUpdate_Click(object sender, EventArgs e)
    {
        if (-1 != lstItems.SelectedIndex)
        {
            CheckBoxItem UpdatedCheckBox = _checkBoxes[lstItems.SelectedIndex];
            using (HtmlCheckbox Dialog = new HtmlCheckbox(UpdatedCheckBox, HtmlFormState.Update))
            {
                if (Dialog.ShowDialog() == DialogResult.OK)
                {
                    UpdatedCheckBox = Dialog.CheckedBox;

                    lstItems.Items[lstItems.SelectedIndex] = UpdatedCheckBox.ToString();
                    _checkBoxes[lstItems.SelectedIndex] = UpdatedCheckBox;
                }
            }
        }
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
                _checkBoxes.RemoveAt(CurrentIndex);
            }
        }
    }

    private void ListHtmlCheckbox_Load(object sender, EventArgs e)
    {
        if (null == _checkBoxes)
            _checkBoxes = new List<CheckBoxItem>();

        ActiveControl = btAdd;
        ValidateButtons();
    }


    private void lstItems_SelectedIndexChanged(object sender, EventArgs e)
    {
        ValidateButtons();
    }
    #endregion

    /// <summary>
    /// Valida el estado de los botones de acuerdo al estado del formulario
    /// </summary>
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
}
