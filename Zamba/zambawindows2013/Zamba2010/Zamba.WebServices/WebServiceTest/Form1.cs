using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Configuration;
using System.IO;
using WebServiceTest.ar.com.stardoc.www;
using WebServiceTest;
using System.Xml;
using System.Text;



public partial class frmTest
    : Form
{
    private const String FILE_EXTENSION_NODE_NAME = "extension";
    private const String FILE_VALUE_NODE_NAME = "value";
    private const String WEB_SERVICE_URL_NODE = "WebServiceUrl";
    private static readonly String TEMP_FILE_PATH = Application.StartupPath + @"/temp";

    private static ServiceIndex _ws = null;
    public frmTest()
    {
        InitializeComponent();
    }

    private ServiceIndex GetWebService()
    {
        if (null == _ws)
        {
            _ws = new ServiceIndex();

            if (!String.IsNullOrEmpty(tbWSDL.Text))
                _ws.Url = tbWSDL.Text;
        }

        return _ws;
    }

    private void btQuerySearch_Click(object sender, EventArgs e)
    {
        tbQueryResult.Text = String.Empty;

        try
        {
            if (String.IsNullOrEmpty(tbWSDL.Text))
                MessageBox.Show("Ingrese una direccion WSDL!");

            ServiceIndex Ws = GetWebService();
            tbQueryResult.Text = Ws.QueryDocuments(@tbQuery.Text);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void btQueryXML_Click(object sender, EventArgs e)
    {
        tbIndexes.Text = String.Empty;
        try
        {
            if (String.IsNullOrEmpty(tbWSDL.Text))
                MessageBox.Show("Ingrese una direccion WSDL!");
            else
            {
                ServiceIndex Ws = GetWebService();
                tbIndexes.Text = Ws.GetIndexs();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void btQueryImage_Click(object sender, EventArgs e)
    {
        try
        {
            if (String.IsNullOrEmpty(tbWSDL.Text))
                MessageBox.Show("Ingrese una direccion WSDL!");
            else
            {
                ServiceIndex Ws = GetWebService();
                String OuterXmlValue = Ws.GetDocumentImage(Int64.Parse(tbImageId.Text));

                XmlDocument XmlImage = new XmlDocument();
                XmlImage.LoadXml(OuterXmlValue);
                String FileExtension = string.Empty;
                String SerializedValue = GetSerializedDocument(XmlImage, out FileExtension);

                if (!String.IsNullOrEmpty(SerializedValue) && !String.IsNullOrEmpty(FileExtension))
                {
                    if (File.Exists(TEMP_FILE_PATH + FileExtension))
                        File.Delete(TEMP_FILE_PATH + FileExtension);

                    using (FileStream reader = File.Create(TEMP_FILE_PATH + FileExtension))
                    {
                        byte[] buffer = Convert.FromBase64String(SerializedValue);
                        reader.Write(buffer, 0, buffer.Length);
                        reader.Close();
                    }

                    wbrFile.Navigate(TEMP_FILE_PATH + FileExtension);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }
    }

    private static string GetSerializedDocument(XmlDocument xml, out string fileExtension)
    {
        String SerializedDocument = null;
        fileExtension = string.Empty;

        if (null != xml && xml.HasChildNodes)
        {
            foreach (XmlAttribute CurrentAttribute in xml.FirstChild.FirstChild.Attributes)
            {
                if (string.Compare(CurrentAttribute.Name, FILE_EXTENSION_NODE_NAME) == 0)
                    fileExtension = CurrentAttribute.Value;
            }

            SerializedDocument = xml.InnerText;
        }

        return SerializedDocument;
    }

    private void frmTest_Load_1(object sender, EventArgs e)
    {
        try
        {
            tbQuery.Text = @"<Indexs> <Index name="""" operator="""" value="""" userId=""""/></Indexs>";
            tbWSDL.Text = @"http://localhost/ZambaWebServices/Service.asmx";
            tbWSDLDocTypes.Text = @"http://localhost:/ZambaWebServices/Service.asmx"; // @"http://ARAS-DE012/ZambaIntegration/service.asmx";
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void btDocTypes_Click(object sender, EventArgs e)
    {
        try
        {
            if (String.IsNullOrEmpty(tbWSDLDocTypes.Text))
                MessageBox.Show("Ingrese una direccion WSDL!");
            else
            {
                WebServiceTest.DocTypesWebService.Service CurrentService = new WebServiceTest.DocTypesWebService.Service();

                if (!String.IsNullOrEmpty(tbWSDLDocTypes.Text))
                    CurrentService.Url = tbWSDLDocTypes.Text;

                tbDocTypes.Text = CurrentService.GetDocTypes();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        wbrFile.Navigate(String.Empty);
    }

  
}