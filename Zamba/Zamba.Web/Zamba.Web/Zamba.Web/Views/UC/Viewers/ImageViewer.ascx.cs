using System;
using Zamba.Core;
using Telerik.Web.UI;
using System.Net;
using System.IO;
using Zamba.Membership;

public partial class Views_ImageViewer : System.Web.UI.UserControl
{
    const string _URLFORMAT = "{5}{0}{1}/Services/GetDocFile.ashx?DocTypeId={2}&DocId={3}&UserID={4}";

    public IResult Result
    {
        get;
        set;
    }

    public RadImageEditor ImageVisualizer
    {
        get;
        set;
    }

    bool _VisualizerSetted = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Si es primera carga generar el control
        if (!IsPostBack)
        {
            SetVisualizer();
        }
    }

    /// <summary>
    /// Genera por primera vez el visualizador
    /// </summary>
    public void SetVisualizer()
    {
        //Si no fue generado ya el control
        if (!_VisualizerSetted)
        {
            try
            {
                //Se crea el visor de imagenes
                RadImageEditor rImg = new RadImageEditor();
                this.Controls.Add(rImg);
                rImg.ImageUrl = CreateTempImage();
                //Setear botones
                SetVisualizerOptions(rImg);

                ImageVisualizer = rImg;
                _VisualizerSetted = true;
            }
            catch (Exception ex)
            {
                lblError.Text = "Ha ocurrido un error al iniciar el viualizador de imagenes";
                lblError.Visible = true;
                ZClass.raiseerror(ex);
            }
        }
    }

    /// <summary>
    /// Seteara todas las opciones del imageeditor dado
    /// </summary>
    /// <param name="rImg"></param>
    private void SetVisualizerOptions(RadImageEditor rImg)
    {
        ImageEditorToolGroup group = new ImageEditorToolGroup();
        string buttonClassName = "zradButton";
        
        ImageEditorTool tool = new ImageEditorTool();
        tool.CommandName = "RotateRight";
        tool.CssClass = buttonClassName;
        group.Tools.Add(tool);

        tool = new ImageEditorTool();
        tool.CommandName = "RotateLeft";
        tool.CssClass = buttonClassName;
        group.Tools.Add(tool);

        tool = new ImageEditorTool();
        tool.CommandName = "ZoomIn";
        tool.CssClass = buttonClassName;
        group.Tools.Add(tool);

        tool = new ImageEditorTool();
        tool.CommandName = "ZoomOut";
        tool.CssClass = buttonClassName;
        group.Tools.Add(tool);

        tool = new ImageEditorTool();
        tool.CommandName = "Reset";
        tool.CssClass = buttonClassName;
        group.Tools.Add(tool);
        
        rImg.Tools.Clear();
        rImg.Tools.Add(group);

        rImg.LocalizationPath = "~/Localization/Resources";
        rImg.Language = "es-AR";

        rImg.EnableResize = false;
    }

    /// <summary>
    /// Descaga la imagen y genera un temporal para setear de url al visualizador de imagen
    /// </summary>
    /// <returns></returns>
    private string CreateTempImage()
    {
        //Se genera la url del getDocFile para descargar la imagen
        string url = string.Format(_URLFORMAT, Request.ServerVariables["HTTP_HOST"], Request.ApplicationPath, Result.DocTypeId, Result.ID, Zamba.Membership.MembershipHelper.CurrentUser.ID, MembershipHelper.Protocol);
        HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
        httpRequest.Method = "GET";

        string tempFileFormat = "temp_{0}{1}";
        FileInfo fi = new FileInfo(Result.Doc_File);
        string fileName = string.Format(tempFileFormat, DateTime.Now.Ticks, fi.Extension);

        string imgUrl = string.Concat(MembershipHelper.AppTempDir("\\ImgTemp"), "\\" + fileName);

        //Se genera una peticion http para descargar la imagen y se captura la respuesta
        using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
        {
            //Se crea un stream para esa respuesta
            using (Stream responseStream = httpResponse.GetResponseStream())
            {
                //Se genera un stream de archivo para bajar la respuesta
                using (FileStream localFileStream =
                    new FileStream(imgUrl, FileMode.Create))
                {
                    //Se utiliza un buffer para ir escribiendo de 4kb
                    var buffer = new byte[4096];
                    long totalBytesRead = 0;
                    int bytesRead;

                    //Mientras se sigan leyendo bytes de la respuesta
                    while ((bytesRead = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        //Se escribe el buffer leido en 
                        totalBytesRead += bytesRead;
                        localFileStream.Write(buffer, 0, bytesRead);
                    }
                }
            }
        }

        //Se devuelve la locacion del archivo, en ruta uri
        return string.Concat("~/Log/ImgTemp/", fileName);
    }
}