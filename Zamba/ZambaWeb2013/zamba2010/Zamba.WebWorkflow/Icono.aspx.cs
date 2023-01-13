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
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
//TODO: COMENTARIO --EMILIANO ---esta pagina fue hecha solo para mostra el icono de los documentos asociados en la grilla de resultados de busqueda
public partial class Icono : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null)
        {
            Zamba.AppBlock.ZIconsList IL= new Zamba.AppBlock.ZIconsList();            
            string id = Request.QueryString[0] as string;
            //aca va el metodo que obtiene la imagen del icono.
            //-->            
            //System.Drawing.IconConverter   ico_converter = new System.Drawing.IconConverter();
            // Tamaño de la imagen
            // [Gaston] 02/02/2009  Tamaño modificado a 15,15. Prácticamente el mismo tamaño que la imagen
            Bitmap bmp = new Bitmap(15, 15);
            Graphics g = Graphics.FromImage(bmp);            

            using (g)
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(50,Color.White)),0, 0, 25, 25);
                IL.ZIconList.Draw(g,0,0,Convert.ToInt32(id));                  
            } 
            
            //<--                                        
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "image/jpeg";                    
            bmp.Save(Response.OutputStream, ImageFormat.Jpeg);
            Response.End();
        }
    }
}
