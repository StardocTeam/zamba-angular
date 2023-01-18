using System;
using System.Windows.Forms;
using Stardoc.HtmlEditor.Enumerators;
using Stardoc.HtmlEditor.HtmlControls;

internal partial class HtmlCheckbox : Form
{
    #region Constantes
    /// <summary>
    /// Titulo del Form cuando esta en estado View
    /// </summary>
    private const String TITLE_STATE_VIEW = "Resumen elemento CHECKBOX";
    /// <summary>
    /// Titulo del Form cuando esta en estado Edit
    /// </summary>
    private const String TITLE_STATE_UPDATE = "Editar elemento CHECKBOX";
    /// <summary>
    /// Titulo del Form cuando esta en estado Insert
    /// </summary>
    private const String TITLE_STATE_INSERT = "Insertar elemento CHECKBOX";
    #endregion

    #region Atributos
    private HtmlFormState _state = HtmlFormState.Insert;
    #endregion

    #region Propiedades
    public CheckBoxItem CheckedBox
    {
        get
        {
            return new CheckBoxItem(tbInput.Text, chkChecked.Checked, chkEnabled.Checked, chkRequired.Checked);
        }
        set
        {
            tbInput.Text = value.Text;
            chkChecked.Checked = value.Checked;
            chkEnabled.Checked = value.Enabled;
            chkRequired.Checked = value.Required;
        }
    }
    public HtmlFormState State
    {
        get { return _state; }
        set
        {
            _state = value;
            switch (value)
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
    public HtmlCheckbox()
    {
        InitializeComponent();
    }

    public HtmlCheckbox(CheckBoxItem checkBox, HtmlFormState state)
        : this()
    {
        tbInput.Text = checkBox.Text;
        chkChecked.Checked = checkBox.Checked;
        chkRequired.Checked = checkBox.Required;
        chkEnabled.Checked = checkBox.Enabled;
        State = state;
    }
    #endregion

    #region Eventos
    private void btAccept_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
        Close();
    }
    private void HtmlCheckbox_Load(object sender, EventArgs e)
    {
        ActiveControl = tbInput;

        //Pongo opciones por default
        if (_state == HtmlFormState.Insert)
            chkEnabled.Checked = true;
    }
    private void btCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
    #endregion
}
