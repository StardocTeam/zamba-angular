using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamba.Core;

namespace Zamba.Web
{
    public partial class Gsearch : System.Web.UI.Page
    {


        // [WebMethod]
        public void Page_Load(object sender, EventArgs e)
        {

            try
            {
                string urlcondicion = Request.Url.Query;

                string newurl;

                string where;
                where = Request.Url.OriginalString.Replace("%7C","|").Split('=').Last();

                if (urlcondicion != "?where=" & urlcondicion != "")
                {


                    System.Collections.Hashtable listaindices = new System.Collections.Hashtable();


                    listaindices.Add("26", "102");
                    listaindices.Add("1", "17");
                    listaindices.Add("4", "5");
                    listaindices.Add("8", "11");
                    listaindices.Add("5", "7");
                    listaindices.Add("7", "118");

                    System.Collections.Hashtable listaentidades = new System.Collections.Hashtable();

                    listaentidades.Add("1", "15");
                    listaentidades.Add("26", "17");
                    listaentidades.Add("5", "17");
                    listaentidades.Add("3", "12");
                    listaentidades.Add("4", "17");

                    string query = where;

                    string[] querypipes = query.Split(char.Parse("|"));


                    string userstring = querypipes[0];
                    string searchstring = querypipes[1];
                    string typesstring = querypipes[2];


                    userstring = userstring.Replace("user", "user=");


                    string[] searchstringdetails = searchstring.Split(char.Parse(";"));

                    string filenetindexid = searchstringdetails[0];
                    string filenetdato = searchstringdetails[1].Replace("$", "");

                    string ZambaINdexId = string.Empty;
                    string[] indexsstringdetails = filenetindexid.Split(char.Parse(","));
                    foreach (string index in indexsstringdetails)
                    {
                        if (listaindices.ContainsKey(index))
                            ZambaINdexId = ZambaINdexId + listaindices[index].ToString() + ",";
                    }
                    if (ZambaINdexId.Length > 0)
                    ZambaINdexId = ZambaINdexId.Remove(ZambaINdexId.Length - 1, 1);


                    string ZambaTypeIds = string.Empty;
                    string[] typesstringdetails = typesstring.Split(char.Parse(";"));
                    List<String> EntityUsed = new List<string>();
                    foreach (string type in typesstringdetails)
                    {
                        if (listaentidades.ContainsKey(type) && !EntityUsed.Contains(listaentidades[type].ToString()))
                        {
                            EntityUsed.Add(listaentidades[type].ToString());
                            ZambaTypeIds = ZambaTypeIds + listaentidades[type].ToString() + ",";
                        }
                    }
                    ZambaTypeIds = ZambaTypeIds.Remove(ZambaTypeIds.Length - 1, 1);


                    newurl = Page.Request.Url.Scheme + "://" + Page.Request.Url.Host + ":" + Page.Request.Url.Port +  Page.Request.ApplicationPath + "/" + "globalsearch/search/search.html?" + userstring + (ZambaINdexId != string.Empty ? "&attr=" + ZambaINdexId : string.Empty) + (ZambaTypeIds != string.Empty ? "&types=" + ZambaTypeIds : string.Empty) + "&search=" + filenetdato;

                }

                else

                    newurl = Page.Request.Url.Scheme + "://" + Page.Request.Url.Host + ":" + Page.Request.Url.Port + Page.Request.ApplicationPath + "/" + "globalsearch/search/search.html";


                Response.Redirect(newurl, false);
                return;


            }
            catch (Exception ex)
            {

                ZClass.raiseerror(ex);
            }





        }
    }
}