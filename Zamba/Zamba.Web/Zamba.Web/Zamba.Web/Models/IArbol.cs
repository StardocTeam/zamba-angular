using System.Web.UI.WebControls;
using System.Collections;

namespace Zamba.Web
{
    public partial interface IArbol
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

}