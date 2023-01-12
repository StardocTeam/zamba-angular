using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web
{
    public partial class WFFilters : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

      

        protected void CHKLSTEPS_SelectedIndexChanged1(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
        
              var StatesIds = new List<Int64>();
            
            foreach (var i in this.CHKLSTATES.CheckedItems)
            {
            
            }
        }
    }
}