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
using System.Text;
using System.IO;
using Zamba.Core;

    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            DataSet dsForm = FormBussines.GetDynamicFormValues();
            CreateTable(dsForm);
        }


        /// <summary>
        /// [sebastian 06-02-2009]Obtiene la cabecera o pie del codigo html del formulario.
        /// </summary>
        /// <param name="HtmlHeaderFooter">valor que indice si se quiere la cabecera o pie</param>
        /// <returns>cabecera o pie como string</returns>
        public string GetDynamicFormHeaderAndFooter(string HtmlHeaderFooter)
        {
            switch (HtmlHeaderFooter)
            {
                case "header":
                    string header = "<html><head>CabeceraHtml</head><body><form>";
                    return header;

                case "footer":

                    string footer = "</form></body></html>";
                    return footer;
                
            }

            return string.Empty;
        }

       
/// <summary>
/// Crea la tabla del formulario con los datos del data set
/// </summary>
/// <param name="dsFormTable">data set con los datos para crear el formulario</param>
        
        public void CreateTable(DataSet dsFormTable)
        {
            try
            {
                
                StringBuilder strgHtml = new StringBuilder();
                Int64 FormRows = dsFormTable.Tables[0].Rows.Count;
                Int64 FormColumns = dsFormTable.Tables[0].Columns.Count;

                strgHtml.AppendLine(GetDynamicFormHeaderAndFooter("header"));
                strgHtml.AppendLine("<table>");

                for (int i = 0; i < FormRows; i++)
                {
                    strgHtml.Append("<tr>");
                    for (int j = 0; j < FormColumns; j++)
                    {
                        strgHtml.AppendLine("<td>");
                        strgHtml.AppendLine("hola: ");
                        strgHtml.AppendLine(GetControlHtml("textbox", "hola"));
                        strgHtml.AppendLine("</td>");
                    }
                    strgHtml.AppendLine("</tr>");
                }

                strgHtml.AppendLine("</table>");
                strgHtml.AppendLine(GetDynamicFormHeaderAndFooter("footer"));

                StreamWriter write = new StreamWriter(System.Web.HttpRuntime.AppDomainAppPath + "\\DynamicForm.html");
                write.AutoFlush = true;
                write.Write(strgHtml);
                write.Close();
            }
            catch (Exception ex)
            {
                
                writeLog("Error: " + ex.Message + " Trace: " + ex.StackTrace);
            }
        }

        /// <summary>
        /// [sebastian 06-02-2009]Obtiene el control de html para ser insertado en la pagina
        /// </summary>
        /// <param name="Control">tipo de control</param>
        /// <param name="IdValue">valor del id del control</param>
        /// <returns></returns>
        public string GetControlHtml(string Control, string IdValue)
        {
            switch (Control.ToLower())
            {
                             
                case "textbox":

                    return "<input type=\"text\" name=\"" + IdValue +"\"/>";
                    
                case "select":

                    return "<input type=\"select\" name=\"" + IdValue +"\"/>";
                    
                case "radio":

                    return "<input type=\"radio\" name=\"" + IdValue + "\"/>";
                    

                case "chekbox":
                    return "input type=\"checkbox\"name=\"" + IdValue + "\"/>"; ;
                    
                default:
                    break;
            }

            return string.Empty;
        }



        private void writeLog(String message)
        {
            try
            {
                string path = System.Web.HttpRuntime.AppDomainAppPath + "Exceptions\\";

                if (System.IO.Directory.Exists(path) == false)
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                path += "Exception ";
                path += System.DateTime.Now.ToString().Replace(".", "").Replace(":", "");
                path = path.Replace("/", "-");
                path += ".txt";

                System.IO.StreamWriter writer = new System.IO.StreamWriter(path);
                writer.Write(message);
                writer.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      

    }

