using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamba.Core;

namespace Zamba.Web.GlobalSearch.search
{
    public partial class SearchConvert : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {

                string where = Page.RouteData.Values["where"].ToString();




                Hashtable listaindices = new Hashtable();

                listaindices.Add("26", "102");
                listaindices.Add("1", "17");
                listaindices.Add("4", "5");
                listaindices.Add("8", "11");
                listaindices.Add("5", "7");
                listaindices.Add("7", "19,14");

                string query = where;

                string[] querypipes = query.Split(char.Parse("|"));

                string userstring = querypipes[0];
                string searchstring = querypipes[1];
                string typesstring = querypipes[2];

                userstring = userstring.Replace("user", "user=");

                string[] searchstringdetails = searchstring.Split(char.Parse(";"));

                string filenetindexid = searchstringdetails[0];
                string filenetdato = searchstringdetails[1].Replace("$", "");

                string ZambaINdexId = listaindices[filenetindexid].ToString();


                string newurl = userstring + "&attr=" + ZambaINdexId + "&search=" + filenetdato;

                Response.Redirect("./globalsearch/search/search.html?" + newurl);

            }
            catch (Exception ex)
            {

                ZClass.raiseerror(ex);
            }
        }
    }


}
    
