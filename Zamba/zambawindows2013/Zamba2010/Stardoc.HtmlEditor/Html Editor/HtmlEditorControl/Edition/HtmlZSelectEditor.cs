using System;
using Stardoc.HtmlEditor.Enumerators;
using Stardoc.HtmlEditor.HtmlControls;

internal partial class HtmlZSelectEditor
    : HtmlSelectEditor
{
    #region Atributos
    private Int64 _indexId;
    #endregion

    #region Propiedades
    public ZSelectItem SelectItem
    {
        get
        {
            return new ZSelectItem(base.SelectItem, _indexId);
        }
        set
        {
            base.SelectItem = (SelectItem)value;
            _indexId = value.IndexId;
        }
    }
    #endregion

    #region Constructores
    public HtmlZSelectEditor()
        : base()
    { }
    public HtmlZSelectEditor(ZSelectItem item, HtmlFormState state)
        : this()
    {
        base.State = state;
        this.SelectItem = item;
    }
    #endregion
}