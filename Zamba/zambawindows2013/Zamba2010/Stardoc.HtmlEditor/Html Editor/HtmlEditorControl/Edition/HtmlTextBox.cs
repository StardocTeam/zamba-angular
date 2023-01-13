using System;
using System.Windows.Forms;
using Stardoc.HtmlEditor.Enumerators;
using Stardoc.HtmlEditor.HtmlControls;

internal partial class HtmlTextBox
    : Form
{
    #region Constantes
    /// <summary>
    /// Titulo del Form cuando esta en estado View
    /// </summary>
    private const String TITLE_STATE_VIEW = "Resumen elemento TEXTBOX";
    /// <summary>
    /// Titulo del Form cuando esta en estado Edit
    /// </summary>
    private const String TITLE_STATE_UPDATE = "Editar elemento TEXTBOX";
    /// <summary>
    /// Titulo del Form cuando esta en estado Insert
    /// </summary>
    private const String TITLE_STATE_INSERT = "Insertar elemento TEXTBOX";
    #endregion

    #region Atributos
    private HtmlFormState _state = HtmlFormState.Insert;
    #endregion

    #region Propiedades
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
    public TextBoxItem TextBox
    {
        get
        {
            return new TextBoxItem(tbName.Text, tbValue.Text, chkReadOnly.Checked, chkEnabled.Checked, chkRequired.Checked, GetSelectedDataType());
        }
        set
        {
            tbName.Text = value.Name;
            tbValue.Text = value.Value;
            chkEnabled.Checked = value.Enabled;
            chkReadOnly.Checked = value.ReadOnly;
            chkRequired.Checked = value.Required;
        }
    }
    #endregion

    #region Constructores
    internal HtmlTextBox()
    {
        InitializeComponent();
        LoadDataTypes();
    }
    internal HtmlTextBox(TextBoxItem item, HtmlFormState state)
        : this()
    {
        State = state;
        chkEnabled.Checked = item.Enabled;
        chkReadOnly.Checked = item.ReadOnly;
        chkRequired.Checked = item.Required;
        tbValue.Text = item.Value;
        tbName.Text = item.Name;
        SelectDataType(item.DataType);
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

    private void HtmlComboBox_Load(object sender, EventArgs e)
    {
        ActiveControl = tbName;
    }
    #endregion

    /// <summary>
    /// Se cargan los tipos de datos de los textbox en el combobox correspondientes
    /// </summary>
    private void LoadDataTypes()
    {
        cbDataTypes.Items.Clear();

        foreach (String DataTypeValue in Enum.GetNames(typeof(DataType)))
            cbDataTypes.Items.Add(DataTypeValue);

        if (cbDataTypes.Items.Count > 0)
            cbDataTypes.SelectedIndex = 0;


    }

    private void SelectDataType(DataType dataType)
    {
        String DataTypeName = Enum.GetName(typeof(DataType), dataType);
        String CurrentDataType = null;
        for (Int32 i = 0; i < cbDataTypes.Items.Count; i++)
        {
            CurrentDataType = cbDataTypes.Items[i].ToString();

            if (string.Compare(DataTypeName, CurrentDataType) == 0)
            {
                cbDataTypes.SelectedIndex = i;
                break;
            }
        }
    }

    private DataType GetSelectedDataType()
    {
        return (DataType)Enum.Parse(typeof(DataType), cbDataTypes.SelectedItem.ToString());
    }
}
