using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Stardoc.HtmlEditor.Enumerators;
using Stardoc.HtmlEditor.HtmlControls;

internal partial class ListHtmlRadioButton
    : Form
{

    #region Atributos
    private List<RadioButtonItem> _radioButtons;
    private HtmlFormState _state = HtmlFormState.Insert;
    #endregion

    #region Propiedades
    public List<RadioButtonItem> RadioButtons
    {
        get { return _radioButtons; }
    }
    #endregion

    #region Constructores
    public ListHtmlRadioButton()
    {
        InitializeComponent();
    }
    public ListHtmlRadioButton(List<RadioButtonItem> radioButtons, HtmlFormState state)
    {
        InitializeComponent();

        _radioButtons = radioButtons;
        _state = state;
        foreach (RadioButtonItem CurrentRadio in _radioButtons)
            lstItems.Items.Add(CurrentRadio.ToString());
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
        using (HtmlRadioButtonEditor editor = new HtmlRadioButtonEditor())
        {
            if (editor.ShowDialog() == DialogResult.OK)
            {
                lstItems.Items.Add(editor.Radio.ToString());
                _radioButtons.Add(editor.Radio);
            }
        }
    }
    /// <summary>
    /// Modifica 1 Radio
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btUpdate_Click(object sender, EventArgs e)
    {
        if (-1 != lstItems.SelectedIndex)
        {
            RadioButtonItem UpdatedRadioButton = _radioButtons[lstItems.SelectedIndex];
            using (HtmlRadioButtonEditor Dialog = new HtmlRadioButtonEditor(UpdatedRadioButton, HtmlFormState.Update))
            {
                if (Dialog.ShowDialog() == DialogResult.OK)
                {
                    UpdatedRadioButton = Dialog.Radio;

                    lstItems.Items[lstItems.SelectedIndex] = UpdatedRadioButton.ToString();
                    _radioButtons[lstItems.SelectedIndex] = UpdatedRadioButton;
                }
            }
        }
    }
    /// <summary>
    /// Borrar 1 RadioButton del listado
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btDelete_Click(object sender, EventArgs e)
    {
        if (lstItems.SelectedItems.Count > 0)
        {
            Int32 CurrentIndex = -1;
            for (int i = lstItems.SelectedIndices.Count - 1; i >= 0; i--)
            {
                CurrentIndex = lstItems.SelectedIndices[i];

                lstItems.Items.RemoveAt(CurrentIndex);
                _radioButtons.RemoveAt(CurrentIndex);
            }
        }
    }
    /// <summary>
    /// Cambio el RadioButton seleccionado
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void lstItems_SelectedIndexChanged(object sender, EventArgs e)
    {
        ValidateButtons();
    }
    private void ListHtmlRadioButton_Load(object sender, EventArgs e)
    {
        if (null == _radioButtons)
            _radioButtons = new List<RadioButtonItem>();

        ActiveControl = btAdd;
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

