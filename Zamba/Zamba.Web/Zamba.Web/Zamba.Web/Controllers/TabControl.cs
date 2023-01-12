using Zamba.Web.App_Code.Helpers;


namespace Zamba.Web
{
    public delegate void Focus(ResultTabs selected);

    /// <summary>
    /// Se encarga de agregar el manejo del foco de los tabs al momento de hacer postback.
    /// </summary>
    /// <history>
    /// Tomas   04/03/2011  Created     
    /// </history>
    public class ResultTab : System.Web.UI.UserControl
    {
        public Focus SetFocus = null;

        public event Focus OnFocus
        {
            add
            {
                SetFocus += value;
            }
            remove
            {
                SetFocus -= value;
            }
        }

        public ResultTab()
        {

        }
    }
}

