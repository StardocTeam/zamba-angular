using System.Web.UI.WebControls;
using System.Collections;

public interface IArbol
{
    //System.Collections.Generic.List<ZambaCore> Combotipoform 
    //{
       
    //    set;
    //}

    TreeView WFTreeView
    {
        get;
        set;
    }

    DropDownList CmbFormType
    {
        get;
        set;
    }
}