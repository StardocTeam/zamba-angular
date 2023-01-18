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
using Zamba.Data;
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //[sebastian 10-02-2009] obtengo un data set con todo los valores para generar el formulario
            DataSet dsForm = FormBussines.GetDynamicFormValues();
            //[sebastian 10-02-2009] genero la tabla que hace el formulario.
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
                    string header = "<html><head></head><body>";
                    return header;

                case "footer":

                    string footer = "</body></html>";
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
                string NewColumn = string.Empty;
                Int32 LastControlOrder = 1;
                bool CloseRow = false;
                Int32 ControlOrder;
                Int32 LastSection=1;
                Int32 IdSection;
                //[sebastian 10-02-2009] genero el header html y obtengo el encabezado de la tabla
                strgHtml.AppendLine(GetDynamicFormHeaderAndFooter("header"));
                strgHtml.AppendLine(GetTableHeader((dsFormTable.Tables[0].Rows[0]["IdSeccion"].ToString())));

                //[sebastian 10-02-2009] recorro el data set para ir generando las diferentes tablas y celdas
                //del formulario.
                foreach (DataRow CurrentRow in dsFormTable.Tables[0].Rows)
                {
                    //[sebastian 10-02-2009] numero de orden del formulario
                    ControlOrder = Int32.Parse(CurrentRow["NroOrden"].ToString());
                    //[sebastian 10-02-2009] id de la seccion: que se utiliza para saber cuando es otra tabla
                    //dentro del formulario
                    IdSection = Int32.Parse(CurrentRow["IdSeccion"].ToString());

                    if (LastSection == IdSection)
                    {
                        if (ControlOrder == LastControlOrder)
                        {
                            NewColumn = "true";
                            CloseRow = false;
                            //[sebastian 10-02-2009] agrego una nueva celda a la tabla con el control correspondiente
                            strgHtml.AppendLine(GetNewTableCell(CurrentRow, NewColumn, CloseRow).ToString());

                        }
                        else
                        {
                            NewColumn = "false";
                            CloseRow = true;
                            //[sebastian 10-02-2009] agrego una nueva fila a la tabla con su control
                            strgHtml.AppendLine(GetNewTableCell(CurrentRow, NewColumn, CloseRow).ToString());
                        }
                        //[sebastian 10-02-2009] asigno el numero de orden del control actual para saber cuando 
                        //cambio de fila para ubicar el control siguiente
                        LastControlOrder = ControlOrder;

                    }

                    else
                    {
                        //[sebastian 10-02-2009]cierro la tabla para comenzar una nueva
                        strgHtml.AppendLine("</tr>");
                        strgHtml.AppendLine("</table>");
                        //[sebastian 10-02-2009] obtengo el nuevo encabezado de la nueva tabla para generarla 
                        //dentro del mismo formulario
                        strgHtml.AppendLine(GetTableHeader(CurrentRow["IdSeccion"].ToString()));

                    }
                    //[sebastian 10-02-2009] guardo la última seccion (tabla) que se genero.
                    LastSection = IdSection;
                }
                //[sebastian 10-02-2009] cierro la última tabla del formulario para luego cerrar el formulario
                //para ser mostrado y guardado en un archivo html
                strgHtml.AppendLine("</tr>");
                strgHtml.AppendLine("</table>");
                //[sebastian 10-02-2009] obtengo el footer del formulario(de la pagina web), para cerrar el 
                //formulario definitivamente
                strgHtml.AppendLine(GetDynamicFormHeaderAndFooter("footer"));

                //[sebastian 10-02-2009] guardo el formulario en un archivo html para ser mostrado
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
        /// Genera el encabezado de la tabla con titulo y formato
        /// </summary>
        /// <param name="SectionId">id de la seccion para obtener el nombre (titulo) de la nueva tabla</param>
        /// <returns></returns>
        public string GetTableHeader(string SectionId)
        {
            StringBuilder title = new StringBuilder();

           title.AppendLine("<table id=\"table1\" border=\"2\" width=\"100%\">");
           title.AppendLine("<caption  style=\"background-color:#527F76\">");
           title.AppendLine(FormFactory.GetDynamicFormSectionName(SectionId));
           title.AppendLine("</caption>");
           title.AppendLine("<tr>");
           

           return title.ToString();
        }

        /// <summary>
        /// Genera una nueva celda con el control dentro y formato
        /// </summary>
        /// <param name="CellValue">controles que van dentro</param>
        /// <param name="NewColumn">Indica si se genera una  nueva columna o fila</param>
        /// <param name="CloseRow">Cierra el tag de la fila para general una nueva fila luego</param>
        /// <returns>sebastian 10-02-2009</returns>
        public string  GetNewTableCell(DataRow CellValue, string NewColumn, bool CloseRow)
        {
            StringBuilder strHTML = new StringBuilder();

               if (CloseRow == true)
                   strHTML.AppendLine("</tr>");

               if(string.Compare(NewColumn, "true") == 0)
               {
                   //[sebastian 10-02-2009] genera una nueva celda con el control dentro 
                   strHTML.AppendLine("<td style=\"font-size:smaller; background-color:#C0D9D9\">");
                    strHTML.AppendLine(CellValue["index_name"].ToString().Trim());
                    strHTML.AppendLine(GetControlHtml(CellValue["dropdown"].ToString().Trim(), CellValue["index_id"].ToString().Trim()));
                    strHTML.AppendLine("</td>");
               }

               else
               {
                   //[sebastian 10-02-2009] genera un a nueva fila con su control dentro
                    strHTML.AppendLine("<tr>");
                    strHTML.AppendLine("<td style=\"font-size:smaller; background-color:#C0D9D9\">");
                    strHTML.AppendLine(CellValue["index_name"].ToString().Trim());
                    strHTML.AppendLine(GetControlHtml(CellValue["dropdown"].ToString().Trim(), CellValue["index_name"].ToString().Trim()));
                    strHTML.AppendLine("</td>");
               }



               //[sebastian 10-02-2009] devuelvo el strig con el control dentro.
                return strHTML.ToString();
        }
        /// <summary>
        /// [sebastian 06-02-2009]Obtiene el control de html para ser insertado en la pagina
        /// </summary>
        /// <param name="Control">tipo de control</param>
        /// <param name="IdValue">valor del id del control</param>
        /// <returns></returns>
        public string GetControlHtml(string ControlType, string ControlName)
        {
            switch (ControlType)
            {

                case "0":

                    return "<input type=\"text\" name=\"" + ControlName + "\"/>";

                case "1":
                    StringBuilder SelectControl = new StringBuilder();
                    SelectControl.AppendLine("<select id=\"" + ControlName + "\">");

                    Int32 IndexId;
                    ArrayList SustList = new ArrayList();
                    if (Int32.TryParse(ControlName, out IndexId) == true)
                    {
                        SustList = IndexsBussines.GetDropDownList(IndexId);
                        foreach (string ListValue in SustList)
                        {
                            SelectControl.AppendLine("<option value=\"" + ListValue +"\">");
                            SelectControl.AppendLine(ListValue);
                            SelectControl.AppendLine("</option>");
                        }
                    }
                    SelectControl.Append("</select>");
                    return SelectControl.ToString();
                case "2":
                    //[sebastian 10-02-2009] tabla de sustitucion
                    StringBuilder SustitutionList = new StringBuilder();
                    SustitutionList.AppendLine("<select \"" + ControlName + "\">");
                    SustitutionList.AppendLine("<option value=\"\"></option></select>");
                    DataTable SustLisTable = new DataTable();
                    Int32 Indice; 
                    
                    if (Int32.TryParse(ControlName, out Indice) == true)
                    {
                    SustLisTable=AutoSubstitutionBussines.GetIndexData(Indice, false, false); 
                    }
                    return SustitutionList.ToString();

                case "radio":

                    return "<input type=\"radio\" name=\"" + ControlName + "\"/>";


                case "chekbox":
                    return "input type=\"checkbox\"name=\"" + ControlName + "\"/>"; ;

                default:
                    break;
            }

            return string.Empty;
        }


        /// <summary>
        /// Genera el archivo de exceptions [sebastian 10-02-2009]
        /// </summary>
        /// <param name="message">exception</param>
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

