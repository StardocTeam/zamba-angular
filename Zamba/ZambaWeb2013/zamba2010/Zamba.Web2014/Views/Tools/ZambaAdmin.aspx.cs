using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Zamba.Data;
using Zamba.Core;
using Zamba.Servers;
using System.Text;

public partial class Views_Tools_ZambaAdmin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userTools"] == null)    
            Response.Redirect("../../Views/Security/Login_WebTools.aspx");

        if (!(Page.IsPostBack))
        {
            init_page();
        }
    }

    protected void init_page()
    {
        rdoReturnValues.Checked = true;
        try
        {
            ZOptBusiness zopt = new ZOptBusiness();
            string title = zopt.GetValue("WebViewTitle");
            zopt = null;
            this.Title = (string.IsNullOrEmpty(title)) ? "Zamba Software" : title + " - Zamba Software";
        }
        catch { } 
    }   

    private void executeScript(string script)
    {
        DataSet ds = new DataSet();
        try
        {
            String[] separator = { "\nGO" };
            String[] sentences = script.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            txtQueryWritter.Text = string.Empty;
            queryResult.Text = string.Empty;

            foreach (String S in sentences)
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(S);
                if (rdoReturnValues.Checked == true)
                    {
                        
                        //[Ezequiel] - 07/10/09 - Si el servidor es de oracle y la consulta comienza
                        //                        con exec entonces ejecuto un procedimiento     
                        if (Zamba.Servers.Server.isOracle && S.ToLower().Trim().StartsWith("exec "))
                        {
                            //[Ezequiel] - Guardo el nombre del store
                            string spname = S.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[1];
                            //[Ezequiel] - Guardo los parametros.
                            string param = S.Replace(S.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0] + " " + S.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[1], "");
                            //[Ezequiel] - Creo los arrays donde se van a guardar los parametros y valores
                            System.Collections.ArrayList parNames = new System.Collections.ArrayList();
                            System.Collections.ArrayList parValues = new System.Collections.ArrayList();

                            //[Ezequiel] - Recorro la lista de parametros para cargar los datos en los arraylist
                            while (param != string.Empty)
                            {
                                Boolean split = false;
                                Int32 pos = 0;
                                string par = string.Empty;
                                //[Ezequiel] - Bucle que separa el nombre del parametro del valor
                                while (!split)
                                {
                                    //[Ezequiel] - Obtengo la posicion de la primera coma y la guardo en pos,
                                    // en el caso de que pos sea diferente de 0 es porque el valor tenia dentro una coma
                                    // entonces en la proxima iteracion a esa posicion le sumo 1 asi toma la segunda coma
                                    pos = param.IndexOf(",", (pos == 0 ? pos : pos + 1));
                                    //[Ezequiel] - Si la posicion es -1 es porque era el ultimo parametro.
                                    if (pos == -1)
                                        par = param;
                                    else
                                        par = param.Substring(0, pos);
                                    //[Ezequiel] - Si el parametro termina con comilla simple es porque esta completo
                                    // caso contrario habia una coma dentro del valor y sigue la iteracion
                                    if (par.Trim().EndsWith("'"))
                                    {
                                        //[Ezequiel] - Guardo el nombre del parametro
                                        string parname = par.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0];
                                        parNames.Add(parname.Trim());
                                        //[Ezequiel] - Guardo el valor del parametro
                                        parValues.Add(par.Substring(par.IndexOf(parname) + parname.Length).Trim().Replace("'", ""));
                                        //[Ezequiel] - Si la longitud del parametro es igual a la lista de parametros
                                        // es porque ya no hay mas nada por recorrer
                                        if (param.Length == par.Length)
                                            param = "";
                                        else
                                            param = param.Substring(par.Length + 1);
                                        //[Ezequiel] - Pongo la bandera en true para que recorra el siguiente parametro.
                                        split = true;
                                    }
                                }

                            }

                            //[Ezequiel] - Genero el array para los tipos de parametros
                            string[] parTypes = new string[parNames.ToArray().Length];
                            for (Int32 i = parNames.ToArray().Length - 1; i > -1; i--)
                            {
                                parNames[i] = parNames[i].ToString().Trim();
                                parValues[i] = parValues[i].ToString().Trim();
                                if (parNames[i].ToString().ToLower().CompareTo("io_cursor") == 0)
                                    parTypes[i] = "2";
                                else
                                    parTypes[i] = "13";
                            }
                            ds = Zamba.Servers.Server.get_Con(false, true, false).ExecuteDataset(spname, parNames.ToArray(), parTypes, parValues.ToArray());
                        }
                        else
                            ds = Zamba.Servers.Server.get_Con(false, true, false).ExecuteDataset(CommandType.Text, sql.ToString());

                        gvResults.DataSource = ds;
                        gvResults.DataBind();
                        queryResult.Text = "Filas obtenidas: " + ds.Tables[0].Rows.Count.ToString();                     
                    }
                    else
                    {
                        queryResult.Visible = false;                    
                        Object pepe = Zamba.Servers.Server.get_Con(false,true, false).ExecuteNonQuery(CommandType.Text, sql.ToString().Replace("\r", ""));
                        if (pepe != null)
                        {
                            queryResult.Visible = true;
                            queryResult.Text += pepe.ToString() + " Filas Afectadas en la Consulta" + "\r\n";                           
                        }
                    }
                }                
            }

        catch (Exception ex)
        {
            queryResult.Text = ex.Message;
            queryResult.Visible = true;
        }
    }

#region Eventos

    protected void btnExecute_Click(object sender, EventArgs e)
    {
        gvResults.DataSource = null;
        gvResults.DataBind();

        string textEncoded = HttpUtility.HtmlEncode(txtQueryWritter.Text);
        string textDecoded = HttpUtility.HtmlDecode(textEncoded);
        executeScript(textDecoded);
    }

    protected void btnCleanValues_Click(object sender, EventArgs e)
    {
        queryResult.Text = string.Empty;
        queryResult.Visible = false;
        gvResults.DataSource = null;
        gvResults.DataBind();
    }

#endregion
}
