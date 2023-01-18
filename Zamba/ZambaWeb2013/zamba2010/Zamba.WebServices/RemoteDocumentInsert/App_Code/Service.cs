using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Xml;
using Zamba.Core;
using System.Collections.Generic;
using System.Text;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

public class Service : System.Web.Services.WebService
{
    #region Constantes
    private const string DOC_TYPE_ID_ROW_NAME = "DocTypeId";
    private const string DOC_TYPE_NAME_ROW_NAME = "DocTypeName";
    private const string INDEX_ID_ROW_NAME = "IndexId";
    private const string INDEX_NAME_ROW_NAME = "IndexName";
    private const string INDEX_LENGHT_ROW_NAME = "IndexLength";

    private const string DOC_TYPES_NODE_NAME = "DocTypes";
    private const string INDEXS_NODE_NAME = "Indexs";
    private const string INDEX_NODE_NAME = "Index";
    private const string INDEX_NAME_NODE_NAME = "name";

    #endregion

    public Service()
    {
    }

    #region GetDocTypes
    /// <summary>
    /// Returns a list of all DocTypes with their Indexs in an XML
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public string GetDocTypes()
    {
        StringBuilder strLog = new StringBuilder();
        DataTable Dt = null;
        String Value = string.Empty;
        try
        {
            strLog.AppendLine("Ejecutando Busqueda de Tipos de Documento");
            strLog.AppendLine(DateTime.Now.ToString());
             Dt = Zamba.Services.DocType.GetAllDocTypes();
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
            if (withLog() == true)
                writeLog(ex.ToString(), "Error");
            Value = Value + Environment.NewLine + "Error al obtener los tipos de documento: " + ex.Message + "Tipo: " + ex.Source;
            strLog.AppendLine(Environment.NewLine + "Error al obtener los tipos de documento: " + ex.Message + "Tipo: " + ex.Source);
        }

        if (null == Dt)
        {
            strLog.AppendLine("No se han encontrador Tipos de Documento");
            return string.Empty;
        }
        else
        {
            XmlDocument XmlDoc = new XmlDocument();

            XmlElement DocTypesNode = XmlDoc.CreateElement(DOC_TYPES_NODE_NAME);

            XmlElement IndexsNode = null;
            XmlElement CurrentDocTypeNode = null;

            Int64 CurrentDocTypeId = -1;
            String CurrentDocTypeName;
            Int64 TempDocTypeId;
            Int64 CurrentIndexId;
            String CurrentIndexName;
            Int32 CurrentIndexLength;

            try
            {
                foreach (DataRow CurrentRow in Dt.Rows)
                {
                    TempDocTypeId = Int64.Parse(CurrentRow[DOC_TYPE_ID_ROW_NAME].ToString());

                    if (TempDocTypeId != CurrentDocTypeId)
                    {
                        if (null != CurrentDocTypeNode)
                        {
                            if (null != IndexsNode)
                            {
                                CurrentDocTypeNode.AppendChild(IndexsNode);
                                IndexsNode = null;
                            }

                            DocTypesNode.AppendChild(CurrentDocTypeNode);
                            CurrentDocTypeNode = null;
                        }

                        CurrentDocTypeId = TempDocTypeId;
                        CurrentDocTypeName = CurrentRow[DOC_TYPE_NAME_ROW_NAME].ToString().Trim();

                        CurrentDocTypeNode = CreateDocType(CurrentDocTypeId, CurrentDocTypeName, ref XmlDoc);
                    }

                    if (null == IndexsNode)
                        IndexsNode = XmlDoc.CreateElement(INDEXS_NODE_NAME);

                    CurrentIndexId = Int64.Parse(CurrentRow[INDEX_ID_ROW_NAME].ToString().Trim());
                    CurrentIndexName = CurrentRow[INDEX_NAME_ROW_NAME].ToString().Trim();

                    CurrentIndexLength = Int32.Parse(CurrentRow[INDEX_LENGHT_ROW_NAME].ToString());

                    IndexsNode.AppendChild(CreateIndexNode(CurrentIndexId, CurrentIndexName, CurrentIndexLength, ref XmlDoc));
                }

                Value = DocTypesNode.OuterXml;
                strLog.AppendLine("Retorna :" + Value);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                if (withLog() == true)
                    writeLog(ex.ToString(), "Error");
                Value = Value + Environment.NewLine + "Error: " + ex.Message + "Tipo: " + ex.Source;
                strLog.AppendLine(Environment.NewLine + "Error: " + ex.Message + "Tipo: " + ex.Source);
            }
            finally
            {

                if (null != XmlDoc)
                {
                    XmlDoc.RemoveAll();
                    XmlDoc = null;
                }

                if (null != DocTypesNode)
                {
                    DocTypesNode.RemoveAll();
                    DocTypesNode = null;
                }
                if (null != IndexsNode)
                {
                    IndexsNode.RemoveAll();
                    IndexsNode = null;
                }
                if (null != CurrentDocTypeNode)
                {
                    CurrentDocTypeNode.RemoveAll();
                    CurrentDocTypeNode = null;
                }

                if (null != strLog)
                {
                    if (withLog() == true)
                        writeLog(strLog.ToString(), "GetDocTypes");

                    strLog.Remove(0, strLog.Length);
                    strLog = null;
                }

                CurrentDocTypeName = null;
                CurrentIndexName = null;
            }
        }

        return Value;
    }

    /// <summary>
    /// Creates an Index XmlNode 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="xmlDoc"></param>
    /// <returns></returns>
    private static XmlElement CreateIndexNode(Int64 id, String name,Int32 length, ref XmlDocument xmlDoc)
    {
        XmlElement IndexElement = xmlDoc.CreateElement("Index");
        XmlAttribute IdAttribute = xmlDoc.CreateAttribute("id");
        IdAttribute.Value = id.ToString();

        XmlAttribute NameAttribute = xmlDoc.CreateAttribute("name");
        NameAttribute.Value = name;

        XmlAttribute LenghtAttribute = xmlDoc.CreateAttribute("lenght");
        LenghtAttribute.Value = length.ToString();

        IndexElement.Attributes.Append(IdAttribute);
        IndexElement.Attributes.Append(NameAttribute);
        IndexElement.Attributes.Append(LenghtAttribute);

        return IndexElement;
    }

    /// <summary>
    /// Creates a DocType Node
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="xmlDoc"></param>
    /// <returns></returns>
    private static XmlElement CreateDocType(Int64 id, String name, ref XmlDocument xmlDoc)
    {
        XmlElement IndexElement = xmlDoc.CreateElement("DocType");

        XmlAttribute IdAttribute = xmlDoc.CreateAttribute("id");
        IdAttribute.Value = id.ToString();

        XmlAttribute NameAttribute = xmlDoc.CreateAttribute("name");
        NameAttribute.Value = name;

        IndexElement.Attributes.Append(IdAttribute);
        IndexElement.Attributes.Append(NameAttribute);

        return IndexElement;
    }
    #endregion

    /// <summary>
    /// Si se quiere utilizar el log o no
    /// </summary>
    /// <returns></returns>
    private Boolean withLog()
    {
        try
        {
            Boolean useLog = false;
            string lg = System.Web.Configuration.WebConfigurationManager.AppSettings["Log"];
            Boolean.TryParse(lg, out useLog);
            return useLog;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Escribe un log con el los datos del mensaje y el titulo como nombre del archivo
    /// </summary>
    /// <param name="message"></param>
    private void writeLog(String message, String Title)
    {
        try
        {
            //Obtengo el path desde el web.config
            string path = System.Web.Configuration.WebConfigurationManager.AppSettings["ExportPath"];
            if (System.IO.Directory.Exists(path) == false)
            {
                System.IO.Directory.CreateDirectory(path);
            }
            if (path.EndsWith("\\") == false)
            {
                path += "\\";
            }

            path += Title;
            path += " ";
            path += System.DateTime.Now.ToString().Replace(":", "");
            path = path.Replace("/", "-");
            path = path.Replace(".", "");
            path += ".txt";

            //Escribo el archivo
            System.IO.StreamWriter writer = new System.IO.StreamWriter(path);
            try
            {
                writer.Write(message);
            }
            finally
            {
                writer.Close();
                writer.Dispose();
                writer = null;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
//[WebMethod]
//public void InsertDocument()
//{
//    try
//    {
//        const String FilePath = @"D:\RemoteInsert.JPG";

//        System.IO.FileStream Fb = new System.IO.FileStream(FilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);

//        byte[] file = new byte[Fb.Length];

//        Fb.Read(file, 0, (Int32)Fb.Length);
//        Fb.Close();

//        Dictionary<Int64, String> Indexs = new Dictionary<long, string>(3);
//        Indexs.Add(135, "Iron Maiden");
//        Indexs.Add(136, "Dickinson");
//        Indexs.Add(137, "Heavy Metal");

//        Results_Business.Insert("asdasd", file, 217, Indexs);

//        Indexs.Clear();
//        Indexs = null;

//    }
//    catch (Exception ex)
//    {
//        ZClass.raiseerror(ex);
//    }
//}
