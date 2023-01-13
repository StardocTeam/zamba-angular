using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Xml;
using Zamba.Core;

/// <summary>
/// Servicio que tiene 3 tareas recibe consultas , las parsea y devuelve un listado d
/// <list type="">
/// <item>Devolver un listado de todos los indices de Zamba</item>
/// <item>Devolver un listado de documentos de zamba que satisfagan 1 consulta recibida</item>
/// <item>Devolver una imagen serializada de acuerdo a un ID de documento</item>
/// </list>
/// <history>
/// Andres [Modified] 16/05/08
/// </history>
/// </summary>
public class ServiceIndex :
    WebService
{
    #region Constants
    #region Query
    private const String QUERIES_NODE_NAME = "Queries";
    private const String QUERY_NODE_NAME = "Query";
    private const String QUERY_DOC_TYPES_NODE_NAME = "Doctypes";
    private const String QUERY_DOC_TYPE_NODE_NAME = "Doctype";
    private const String QUERY_INDEXS_NODE_NAME = "Indexs";
    private const String QUERY_INDEX_NODE_NAME = "Index";
    //No se usan mas los nombres de los indices
    //private const String QUERY_INDEX_NAME = "name";
    //private const String QUERY_INDEX_NAME_ABN = "Name";
    private const String QUERY_INDEX_ID_NAME = "id";
    private const String QUERY_OPERATOR_NAME = "operator";
    private const String QUERY_VALUE_NAME = "value";
    private const String QUERY_USER_ID_NAME = "userId"; 
    #endregion

    private const String FILE_NODE_NAME = "File";
    private const String FILE_EXTENSION_NODE_NAME = "extension";

    #region Response
    private const String RESPOSE_DOC_TYPES_NODE = "DocTypes";
    private const String RESPOSE_DOC_TYPE_NODE = "DocType";
    private const String RESPOSE_DOC_TYPE_ID_NODE = "id";
    private const String RESPOSE_DOCUMENTS_NODE = "Documents";
    private const String RESPOSE_DOCUMENT_NODE = "Document";
    private const String RESPOSE_DOCUMENT_ID_NODE = "id";
    private const String RESPOSE_INDEXES_NODE = "Indexes";
    private const String RESPOSE_INDEX_NODE = "Index";
    private const String RESPOSE_INDEX_ID_NODE = "id";
    private const String RESPOSE_INDEX_VALUE_NODE = "value"; 
    #endregion

    private const String DOCUMENT_ID_NODE_NAME = "id";
    private const String DOCUMENT_NAME_NODE_NAME = "name";

    private const String INDEXS_NODE_NAME = "Indexs";
    private const String INDEX_NODE_NAME = "Index";
    private const String INDEX_ID_NODE_NAME = "id";
    private const String INDEX_NAME_NODE_NAME = "name";
    private const String INDEX_LENGHT_NODE_NAME = "lenght";
    private const String INDEX_REQUIRED_NODE_NAME = "required";
    /// <summary>
    /// 
    /// </summary>
    private const String INDEX_TYPE_NODE_NAME = "type";
    private const string DOC_TYPE_ID_ROW_NAME = "DocTypeId";
    private const string DOC_TYPE_NAME_ROW_NAME = "DocTypeName";
    private const string INDEX_ID_ROW_NAME = "IndexId";
    private const string INDEX_NAME_ROW_NAME = "IndexName";
    private const string INDEX_LENGHT_ROW_NAME = "IndexLength";

    private const string DOC_TYPES_NODE_NAME = "DocTypes";
    #endregion

    #region GetIndexs
    /// <summary>
    /// Obtengo todos lo indices de Zamba
    /// <returns></returns>
    [WebMethod]
    public String GetIndexs()
    {
        StringBuilder strLog = new StringBuilder();
        Dictionary<Int64, string> IndexsIdAndName = null;
        String ReturnValue = String.Empty;

        try
        {
            strLog.AppendLine("Ejecutando Busqueda de Indices");
            strLog.AppendLine(DateTime.Now.ToString());
            IndexsIdAndName = Zamba.Services.Index.GetAllIndexsIdAndName();

            ReturnValue = BuildXml(IndexsIdAndName);
            strLog.AppendLine("Valor Retorno: " + ReturnValue);
        }
        catch (Exception ex)
        {
            //En caso de error, lo guardo en el log, en la variable de retorno
            //Lo escribo en una exception y hago un raise de la misma
            strLog.AppendLine("Error" + ex.Message + "Tipo:" + ex.Source);
            ReturnValue = "Error: " + ex.Message + "Tipo: " + ex.Source;
            if (withLog() == true)
                writeLog(ex.ToString(), "Error");
            ZClass.raiseerror(ex);

            ResponseXml Response = new ResponseXml();
            XmlElement DocumentsNode = Response.CreateElement(RESPOSE_DOCUMENTS_NODE);
            Response.AppendChild(DocumentsNode);
            Response.SetErrorState(ex);

            ReturnValue = Response.OuterXml;
        }
        finally
        {
            if (null != IndexsIdAndName)
            {
                IndexsIdAndName.Clear();
                IndexsIdAndName = null;
            }

            if (null != strLog)
            {
                if (withLog() == true)
                    writeLog(strLog.ToString(), "LogGetIndex");
                strLog = null;
            }
        }

        return ReturnValue;
    }

    /// <summary>
    /// Creates a XML with a list of documents Ids and its name
    /// </summary>
    /// <param name="documents"></param>
    /// <returns></returns>
    private static String BuildXml(Dictionary<Int64, String> documents)
    {
        ResponseXml XmlDoc = null;
        XmlElement DocumentsNode = null;
        XmlElement DocumentNode = null;
        XmlAttribute IdAtributte = null;
        XmlAttribute NameAtributte = null;

        try
        {
            XmlDoc = new ResponseXml();

            DocumentsNode = XmlDoc.CreateElement(RESPOSE_DOCUMENTS_NODE);

            foreach (Int64 CurrentId in documents.Keys)
            {
                DocumentNode = XmlDoc.CreateElement(RESPOSE_DOCUMENT_NODE);

                IdAtributte = XmlDoc.CreateAttribute(DOCUMENT_ID_NODE_NAME);
                IdAtributte.Value = CurrentId.ToString();
                DocumentNode.Attributes.Append(IdAtributte);

                NameAtributte = XmlDoc.CreateAttribute(DOCUMENT_NAME_NODE_NAME);
                NameAtributte.Value = documents[CurrentId].Trim();
                DocumentNode.Attributes.Append(NameAtributte);

                DocumentsNode.AppendChild(DocumentNode);
            }

            XmlDoc.FirstChild.AppendChild(DocumentsNode);
            XmlDoc.SetSuccesState();
        }
        catch (Exception ex)
        {
            XmlDoc = new ResponseXml();
            DocumentsNode = XmlDoc.CreateElement(RESPOSE_DOCUMENTS_NODE);
            XmlDoc.AppendChild(DocumentsNode);
            XmlDoc.SetErrorState(ex);
        }

        return XmlDoc.OuterXml;
    }
    #endregion

    #region QueryDocuments
    /// <summary>
    /// Execute Queries in Zamba and parse the results into an XML.
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [WebMethod]
    public String QueryDocuments(String query)
    {
        //String que contendra un log del metodo
        StringBuilder strLog = new StringBuilder();
        XmlDocument XmlQuery = null;
        List<Query> ParsedQueries = null;
        String ReturnValue = String.Empty;

        try
        {
            strLog.AppendLine("Ejecutando Busqueda de Documentos");
            strLog.AppendLine(DateTime.Now.ToString());
            strLog.AppendLine("Consulta Original: " + query);

            query = HttpUtility.HtmlDecode(query);
            strLog.AppendLine("Consulta Decodificada: " + query);

            //Transformo el xml en querys
            XmlQuery = new XmlDocument();

            XmlQuery.LoadXml(query);
            ParsedQueries = ParseQuerys(XmlQuery);

            //Ejecuto las consultas
            ReturnValue = DoQuery(ParsedQueries, ref strLog);
            strLog.AppendLine("Valor Retorno: " + ReturnValue);
        }
        catch (Exception ex)
        {
            //En caso de error, lo guardo en el log, en la variable de retorno
            //Lo escribo en una exception y hago un raise de la misma
            strLog.AppendLine("Error" + ex.Message + "Tipo:" + ex.Source);
            ReturnValue = "Error: " + ex.Message + "Tipo: " + ex.Source;
            if (withLog() == true)
                writeLog(ex.ToString(), "Error");
            ZClass.raiseerror(ex);

            ResponseXml XmlDoc = new ResponseXml();
            //XmlElement DocTypesNode = XmlDoc.CreateElement(RESPOSE_DOC_TYPES_NODE);
            //XmlDoc.AppendChild(DocTypesNode);
            XmlDoc.SetErrorState(ex);

            ReturnValue = XmlDoc.OuterXml;
        }
        finally
        {
            if (null != XmlQuery)
            {
                XmlQuery.RemoveAll();
                XmlQuery = null;
            }

            if (null != ParsedQueries)
            {
                ParsedQueries.Clear();
                ParsedQueries = null;
            }

            if (null != strLog)
            {
                if (withLog() == true)
                    writeLog(strLog.ToString(), "LogDoQuery");
                strLog = null;
            }
        }

        return ReturnValue;
    }

    /// <summary>
    /// Parsea un XML y crea una lista de Queries a partir de esta.
    /// </summary>
    /// <param name="xmlQuery"></param>
    /// <returns></returns>
    private static List<Query> ParseQuerys(XmlDocument xmlQuery)
    {
        List<Query> ParsedQueries = null;

        if (null != xmlQuery.ChildNodes && xmlQuery.FirstChild.HasChildNodes)
        {
            if (string.Compare(xmlQuery.FirstChild.Name, QUERIES_NODE_NAME) == 0 && xmlQuery.FirstChild.HasChildNodes)
            {
                ParsedQueries = new List<Query>(xmlQuery.FirstChild.ChildNodes.Count);
                Query MyQuery = null;

                foreach (XmlNode CurrentNode in xmlQuery.FirstChild.ChildNodes)
                {
                    MyQuery = new Query();

                    foreach (XmlNode QueryNode in xmlQuery.FirstChild.ChildNodes)
                    {
                        if (String.Compare(QueryNode.Name, QUERY_NODE_NAME) == 0)
                        {
                            foreach (XmlNode Node in QueryNode.ChildNodes)
                            {
                                if (String.Compare(Node.Name, QUERY_DOC_TYPES_NODE_NAME) == 0)
                                    MyQuery.DocTypes.AddRange(ParseDocTypes(Node));
                                else if (String.Compare(Node.Name, QUERY_INDEXS_NODE_NAME) == 0)
                                    MyQuery.Indexs.AddRange(ParseIndexs(Node));
                            }
                        }
                        if (MyQuery.IsValid())
                            ParsedQueries.Add(MyQuery);
                    }
                }
            }
        }

        return ParsedQueries;
    }

    /// <summary>
    /// Parsea un nodo XML y devuelve el listado de indices encontrado
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    private static List<Index> ParseIndexs(XmlNode node)
    {
        if (null == node || String.Compare(node.Name, QUERY_INDEXS_NODE_NAME) != 0 || !node.HasChildNodes)
            return null;

        List<Index> Indexs = new List<Index>();

        String Name = String.Empty;
        String Value = String.Empty;
        QueryOperators Operator = QueryOperators.Ninguno;
        Int64 PosibleIndexUserId;
        Int64? UserId = null;
        Int64 PosibleIndexId;
        Int64? IndexId = null;

        foreach (XmlNode QueryNode in node.ChildNodes)
        {
            if (String.Compare(QueryNode.Name, QUERY_INDEX_NODE_NAME) == 0)
            {
                if (null != QueryNode.Attributes.GetNamedItem(QUERY_OPERATOR_NAME))
                    Operator = ParseValue(QueryNode.Attributes.GetNamedItem(QUERY_OPERATOR_NAME).Value);

                if (null != QueryNode.Attributes.GetNamedItem(QUERY_VALUE_NAME))
                    Value = QueryNode.Attributes.GetNamedItem(QUERY_VALUE_NAME).Value;

                if (null != QueryNode.Attributes.GetNamedItem(QUERY_USER_ID_NAME))
                {
                    if (Int64.TryParse(QueryNode.Attributes.GetNamedItem(QUERY_USER_ID_NAME).Value, out PosibleIndexUserId))
                        UserId = (Int64?)PosibleIndexUserId;
                    else
                        UserId = null;
                }
                if (null != QueryNode.Attributes.GetNamedItem(QUERY_INDEX_ID_NAME))
                {
                    if (Int64.TryParse(QueryNode.Attributes.GetNamedItem(QUERY_INDEX_ID_NAME).Value, out PosibleIndexId))
                        IndexId = (Int64?)PosibleIndexId;
                    else
                        IndexId = null;
                }

                if (!UserId.HasValue || !IndexId.HasValue)
                    throw new Exception("El id de indice o usuario es invalido");

                Indexs.Add(new Index(IndexId.Value, Operator, Value, UserId.Value));
            }
        }

        return Indexs;
    }

    /// <summary>
    /// Parsea un nodo XML y devuelve el listado de ID de tipo de documentos encontrado
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    private static List<Int64> ParseDocTypes(XmlNode node)
    {
        List<Int64> DocTypes = new List<Int64>();

        if (null == node || String.Compare(node.Name , QUERY_DOC_TYPES_NODE_NAME) != 0 || !node.HasChildNodes)
            return DocTypes;

        Int64 DocTypeId;
        foreach (XmlNode DocTypeNode in node.ChildNodes)
        {
            if (String.Compare(DocTypeNode.Name, QUERY_DOC_TYPE_NODE_NAME) == 0)
            {
                if (Int64.TryParse(DocTypeNode.InnerText, out DocTypeId))
                    DocTypes.Add(DocTypeId);
            }
        }

        return DocTypes;
    }

    /// <summary>
    /// Consulta contra zamba N queries y devuelve el resultado en forma de XML
    /// </summary>
    /// <param name="consultas"></param>
    /// <returns></returns>
    private static String DoQuery(List<Query> consultas, ref StringBuilder strLog)
    {
        Dictionary<Int64, Int64> tmpDocsDictionary;
        DataTable tmpDTDocIDs = null;
        List<Result> QueriedResults = new List<Result>();
        String ValuesNode = null;

        if (null == consultas || consultas.Count ==  0)
        {
            strLog.AppendLine("No hay consultas para ejecutar");
            return ValuesNode;
        }
        try
        {
            foreach (Query CurrentQuery in consultas)
            {
                strLog.AppendLine("Consulta");
                foreach (Index index in CurrentQuery.Indexs)
                {
                    strLog.AppendLine("Indice");
                    strLog.AppendLine("Id: " + index.Id.ToString());
                    strLog.AppendLine("Operador: " + index.Operator);
                    strLog.AppendLine("Valor Busqueda: " + index.Value);
                    strLog.AppendLine("Id del Usuario:" + index.UserId.ToString());
                    strLog.AppendLine();

                    String Operator = ParseValue(index.Operator);
                    if (string.IsNullOrEmpty(Operator))
                        strLog.Append("El operador es incorrecto");

                    IIndex MyIndex = IndexsBussines.GetIndexByIdAsIndex(index.Id, String.Empty);

                    if (null == MyIndex)
                        strLog.Append("El indice ingresado no existe en Zamba");

                    Boolean IsDocumentValid;
                    Result Document = null;
                    tmpDocsDictionary = Results_Business.SearchIndexByUserIdForWebServices(MyIndex.ID, MyIndex.Type, Operator, index.Value, index.UserId);

                    if (null != tmpDocsDictionary && tmpDocsDictionary.Count > 0)
                    {
                        foreach (KeyValuePair<Int64, Int64> pair in tmpDocsDictionary)
                        {
                            IsDocumentValid = true;

                            // se filtran por tipos de documentos
                            if (CurrentQuery.DocTypes.Count > 0 && !CurrentQuery.DocTypes.Contains(pair.Value))
                                IsDocumentValid = false;

                            //si este documento ya esta agregado a la lista no lo vuelvo a agregar.
                            if (QueriedResults.Exists(delegate(Result r) { return r.ID == pair.Key; }))
                                IsDocumentValid = false;

                            if (IsDocumentValid)
                            {
                                Document = Results_Business.GetResult(pair.Key, pair.Value);
                                strLog.AppendLine("Nombre Tipo de Documento:" + Document.Name);
                                QueriedResults.Add(Document);
                                strLog.AppendLine("Id del Documento:" + Document.ID.ToString());
                            }
                        }
                    }
                    else
                        strLog.Append("No se han encontrado resultados");
                }
            }

            ValuesNode = BuildXml(QueriedResults);
        }
        finally
        {
            if (null != tmpDTDocIDs)
            {
                tmpDTDocIDs.Dispose();
                tmpDTDocIDs = null;
            }

            if (null != QueriedResults)
            {
                QueriedResults.Clear();
                QueriedResults = null;
            }
        }
      
        return ValuesNode;
    }

    /// <summary>
    ///  Crea un XML con el listado de documentos agrupados por tipo de documento.
    /// </summary>
    /// <param name="results"></param>
    /// <returns></returns>
    private static String BuildXml(List<Result> results)
    {
        ResponseXml XmlDoc = null;
        XmlElement DocTypesNode = null;
        XmlElement DocumentNode = null;
        XmlElement DocTypeNode = null;
        XmlElement IndexesNode = null;
        XmlElement IndexNode = null;
        XmlAttribute IndexIdAttribute = null;
        XmlAttribute IndexValueAttribute = null;
        XmlAttribute DocTypeIdAttribute = null;
        XmlAttribute DocumentIdAttribute = null;

        try
        {
            XmlDoc = new ResponseXml();

            DocTypesNode = XmlDoc.CreateElement(RESPOSE_DOC_TYPES_NODE);
            if (null == results || results.Count == 0)
                return XmlDoc.OuterXml;

            List<Int64> DocTypes = new List<Int64>();

            foreach (Result document in results)
            {
                if (!DocTypes.Contains(document.DocType.ID))
                    DocTypes.Add(document.DocType.ID);
            }

            List<Result> TempResults = null;
            foreach (Int64 DocTypeId in DocTypes)
            {
                TempResults = results.FindAll(delegate(Result document)
                {
                    return document.DocType.ID == DocTypeId;
                });

                DocTypeNode = XmlDoc.CreateElement(RESPOSE_DOC_TYPE_NODE);

                DocTypeIdAttribute = XmlDoc.CreateAttribute(RESPOSE_DOC_TYPE_ID_NODE);
                DocTypeIdAttribute.Value = DocTypeId.ToString();
                DocTypeNode.Attributes.Append(DocTypeIdAttribute);

                foreach (Result CurrentResult in TempResults)
                {
                    DocumentNode = XmlDoc.CreateElement(RESPOSE_DOCUMENT_NODE);

                    DocumentIdAttribute = XmlDoc.CreateAttribute(DOCUMENT_ID_NODE_NAME);
                    DocumentIdAttribute.Value = CurrentResult.ID.ToString();

                    DocumentNode.Attributes.Append(DocumentIdAttribute);

                    IndexesNode = XmlDoc.CreateElement(RESPOSE_INDEXES_NODE);
                    foreach (IIndex index in CurrentResult.Indexs)
                    {
                        IndexNode = XmlDoc.CreateElement(RESPOSE_INDEX_NODE);

                        IndexIdAttribute = XmlDoc.CreateAttribute(RESPOSE_INDEX_ID_NODE);
                        IndexIdAttribute.Value = index.ID.ToString();
                        IndexNode.Attributes.Append(IndexIdAttribute);

                        IndexValueAttribute = XmlDoc.CreateAttribute(RESPOSE_INDEX_VALUE_NODE);
                        IndexValueAttribute.Value = index.Data;
                        IndexNode.Attributes.Append(IndexValueAttribute);

                        IndexesNode.AppendChild(IndexNode);
                    }
                    DocumentNode.AppendChild(IndexesNode);
                    DocTypeNode.AppendChild(DocumentNode);
                }

                DocTypesNode.AppendChild(DocTypeNode);
            }

            XmlDoc.FirstChild.AppendChild(DocTypesNode);
            XmlDoc.SetSuccesState();
        }
        catch (Exception ex)
        {
            XmlDoc = new ResponseXml();
            XmlElement DocTypes = XmlDoc.CreateElement(RESPOSE_DOC_TYPES_NODE);

            XmlDoc.FirstChild.AppendChild(DocTypes);
            XmlDoc.SetErrorState(ex);
        }
                               
        return XmlDoc.OuterXml;
    }
    #endregion

    #region GetDocumentImage
    /// <summary>
    /// Searchs a document by its ID and returns its image serialized
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [WebMethod()]
    public String GetDocumentImage(Int64 id)
    {
        StringBuilder strLog = new StringBuilder();
        StringBuilder QueryBuilder = new StringBuilder();
        String SerializedDocument = String.Empty;
        String OuterXmlValue = null;

        try
        {
            strLog.AppendLine("Ejecutando Busqueda de Imagen");
            strLog.AppendLine(DateTime.Now.ToString());
            strLog.AppendLine("Id del Documento: " + id);

            QueryBuilder.Append("select dtid from zi where docid = ");
            QueryBuilder.Append(id.ToString());

            Object DocTypeID = Zamba.Servers.Server.get_Con(false, false, false).ExecuteScalar(CommandType.Text, QueryBuilder.ToString());
            if (null != DocTypeID)
            {
                Int64 CurrentDocTypeId = Int64.Parse(DocTypeID.ToString());

                QueryBuilder.Remove(0, QueryBuilder.Length);

                QueryBuilder.Append("select RTRIM(DISK_VOLUME.DISK_VOL_PATH + '\\' + RTRIM(CONVERT(char, DOC");
                QueryBuilder.Append(CurrentDocTypeId.ToString());
                QueryBuilder.Append(".DOC_TYPE_ID)) + '\\' + RTRIM(CONVERT(char, DOC");
                QueryBuilder.Append(CurrentDocTypeId.ToString());
                QueryBuilder.Append(".OFFSET)) + '\\' + DOC");
                QueryBuilder.Append(CurrentDocTypeId.ToString());
                QueryBuilder.Append(".DOC_FILE) AS fullpath from doc");
                QueryBuilder.Append(CurrentDocTypeId.ToString());
                QueryBuilder.Append(" INNER JOIN DISK_VOLUME ON DOC");
                QueryBuilder.Append(CurrentDocTypeId.ToString());
                QueryBuilder.Append(".VOL_ID = DISK_VOLUME.DISK_VOL_ID  where doc_id = ");
                QueryBuilder.Append(id.ToString());

                Object Path = Zamba.Servers.Server.get_Con(false, false, false).ExecuteScalar(CommandType.Text, QueryBuilder.ToString());

                if (null != Path && File.Exists(Path.ToString()))
                {
                    FileInfo DocumentInformation = new FileInfo(Path.ToString());
                    try
                    {
                        SerializedDocument = SerializeDocument(Path.ToString());

                        OuterXmlValue = BuildXml(SerializedDocument, DocumentInformation.Extension);
                        strLog.AppendLine("Retorno: " + OuterXmlValue);
                    }
                    finally
                    {
                        DocumentInformation = null;
                    }
                }
                else
                {
                    if (Path == null)
                    {
                        strLog.AppendLine("No se ha encontrado el documento");
                        OuterXmlValue = "No se ha encontrado el documento";

                        ResponseXml XmlDoc = new ResponseXml();
                        XmlElement FileNode = XmlDoc.CreateElement(FILE_NODE_NAME);
                        XmlDoc.FirstChild.AppendChild(FileNode);
                        XmlDoc.SetErrorState("No se ha encontrado el documento");

                        OuterXmlValue = FileNode.OuterXml;
                    }
                    else
                    {
                        strLog.AppendLine("No existe el documento: " + Path);
                        OuterXmlValue = "No existe el documento: " + Path;

                        ResponseXml XmlDoc = new ResponseXml();
                        XmlElement FileNode = XmlDoc.CreateElement(FILE_NODE_NAME);
                        XmlDoc.FirstChild.FirstChild.AppendChild(FileNode);
                        XmlDoc.SetErrorState("No existe el documento: " + Path);

                        OuterXmlValue = XmlDoc.OuterXml;
                    }
                }
            }
            else
            {
                strLog.AppendLine("El documento no se encuentra cargado en la tabla de insercion");

                ResponseXml XmlDoc = new ResponseXml();
                XmlElement FileNode = XmlDoc.CreateElement(FILE_NODE_NAME);
                XmlDoc.FirstChild.AppendChild(FileNode);
                XmlDoc.SetErrorState("El documento no se encuentra cargado en la tabla de insercion");

                OuterXmlValue = XmlDoc.OuterXml;
            }
        }
        catch (Exception ex)
        {
            //En caso de error, lo guardo en el log, en la variable de retorno
            //Lo escribo en una exception y haog un raise de la misma
            strLog.AppendLine("Error" + ex.Message + "Tipo:" + ex.Source);
            OuterXmlValue = "Error: " + ex.Message + "Tipo: " + ex.Source;
            if (withLog() == true)
                writeLog(ex.ToString(), "Error");
            ZClass.raiseerror(ex);

            ResponseXml XmlDoc = new ResponseXml();
            XmlElement FileNode = XmlDoc.CreateElement(FILE_NODE_NAME);
            XmlDoc.FirstChild.AppendChild(FileNode);
            XmlDoc.SetErrorState(ex);

            OuterXmlValue = XmlDoc.OuterXml;
        }
        finally
        {
            if (null != QueryBuilder)
                QueryBuilder = null;
            if (null != strLog)
            {
                if (withLog() == true)
                    writeLog(strLog.ToString(), "LogGetDocumentoImage");
                strLog = null;
            }
        }

        return OuterXmlValue;
    }

    /// <summary>
    /// Serializa una imagen de un path
    /// </summary>
    /// <param name="strPathImagen"></param>
    /// <returns></returns>
    private static string SerializeDocument(String pathImagen)
    {
        FileStream fStream = null;
        String sBase64 = String.Empty;
        byte[] bytes;
        BinaryReader bReader = null;

        try
        {
            if (File.Exists(pathImagen))
            {
                using (fStream = new FileStream(pathImagen, FileMode.Open))
                {
                    // Declaramos fs para tener acceso a la imagen residente en la maquina cliente.
                    //fStream = new FileStream(strPathImagen, FileMode.Open);

                    // Declaramos un Leector Binario para accesar a los datos de la imagen pasarlos a un arreglo de bytes
                    bReader = new BinaryReader(fStream);
                    bytes = new byte[Convert.ToInt32(fStream.Length)];

                    bReader.Read(bytes, 0, bytes.Length);
                    // base64 es la cadena en donde se guarda el arreglo de bytes ya convertido
                    sBase64 = Convert.ToBase64String(bytes);
                }
            }
        }
        finally
        {
            bReader = null;
            bytes = null;

            if (null != fStream)
            {
                fStream.Dispose();
                fStream = null;
            }
        }

        return sBase64;
    }
    /// <summary>
    /// Creates a XML with the bynary of a file and its file extension
    /// </summary>
    /// <param name="bynaryString"></param>
    /// <param name="fileExtension"></param>
    /// <returns></returns>
    private static string BuildXml(string bynaryString, string fileExtension)
    {
        ResponseXml XmlDoc = new ResponseXml();
        XmlElement FileNode = null;
        try
        {
            FileNode = XmlDoc.CreateElement(FILE_NODE_NAME);
            FileNode.InnerText = bynaryString;

            XmlAttribute ExtensionAtributte = XmlDoc.CreateAttribute(FILE_EXTENSION_NODE_NAME);
            ExtensionAtributte.Value = fileExtension;
            FileNode.Attributes.Append(ExtensionAtributte);

            XmlDoc.FirstChild.AppendChild(FileNode);
            XmlDoc.SetSuccesState();
        }
        catch (Exception ex)
        {
            XmlDoc = new ResponseXml();
            FileNode = XmlDoc.CreateElement(FILE_NODE_NAME);
            XmlDoc.FirstChild.AppendChild(FileNode);

            XmlDoc.SetErrorState(ex);
        }

        return XmlDoc.OuterXml;
    }
    #endregion

    //private static XmlElement BuildErrorNode(String errorMessage, String nodeTag)
    //{
    //    XmlDocument XmlDoc = new XmlDocument();

    //    XmlElement SiblingNode = XmlDoc.CreateElement(nodeTag);        


    //    XmlElement Error = XmlDoc.CreateElement("State");
    //    XmlAttribute value = XmlDoc.CreateAttribute("value");
    //    value.Value = "1";


    //    Error.Attributes.Append(value);
    //    Error.InnerText = errorMessage;

    //    return Error;
    //}
    //private static XmlElement BuildErrorNode(String errorMessage)
    //{
    //    XmlDocument XmlDoc = new XmlDocument();
    //    XmlElement Error = XmlDoc.CreateElement("State");
    //    XmlAttribute value = XmlDoc.CreateAttribute("value");
    //    value.Value = "1";

    //    Error.Attributes.Append(value);
    //    Error.InnerText = errorMessage;

    //    return Error;
    //}
    ///// <summary>
    ///// Crea 1 nodo XML que representa el estado de ejecucion insatisfactoria de un metodo.
    ///// </summary>
    ///// <param name="ex"></param>
    ///// <returns></returns>
    //private static XmlElement BuildErrorNode(Exception ex)
    //{
    //    return BuildErrorNode(ex.Message);
    //}
    ///// <summary>
    ///// Crea 1 nodo XML que representa el estado de ejecucion satisfactoria de un metodo.
    ///// </summary>
    ///// <returns></returns>
    //private static XmlElement BuildSuccesfulNode()
    //{
    //    XmlDocument XmlDoc = new XmlDocument();
    //    XmlElement Success = XmlDoc.CreateElement("State");
    //    XmlAttribute value = XmlDoc.CreateAttribute("value");
    //    value.Value = "0";

    //    return Success;
    //}

    /// <summary>
    /// Representa una consulta realizada contra Zamba
    /// </summary>
    private sealed class Query
    {
        #region Atributos
        private List<Index> _indexs = null;
        private List<Int64> _docTypes = null;
        #endregion

        #region Propiedades
        public List<Int64> DocTypes
        {
            get { return _docTypes; }
        }
        public List<Index> Indexs
        {
            get { return _indexs; }
            set { _indexs = value; }
        }
        #endregion

        #region Constructores
        public Query()
        {
            _indexs = new List<Index>();
            _docTypes = new List<Int64>();
        }
        public Query(List<Int64> docTypes)
            : this()
        {
            _docTypes.AddRange(docTypes);
        }
        public Query(List<Index> indexs ,List<Int64> docTypes)
            : this()
        {
            _indexs = indexs;
            _docTypes.AddRange(docTypes);
        }
        #endregion

        /// <summary>
        /// Validates wheather the information from this instance is valid.
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            foreach (Index index in _indexs)
            {
                if (null == index || !index.IsValid())
                    return false;
            }

            return true;
        }
    }

    /// <summary>
    /// Representa un indice de una consulta
    /// </summary>
    private sealed class Index
    {
        #region Atributos
        private Int64 _id;
        private String _name;
        private QueryOperators _operator;
        private String _value;
        private Int64 _userId;
        #endregion

        #region Propiedades
        public Int64 Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public QueryOperators Operator
        {
            get { return _operator; }
            set { _operator = value; }
        }
        public String Value
        {
            get { return _value; }
            set { _value = value; }
        }
        public Int64 UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        #endregion

        #region Constructores
        public Index(Int64 id, QueryOperators indexOperator, String value, Int64 userId)
        {
            _id = id;
            _operator = indexOperator;
            _value = value;
            _userId = userId;
        }
        #endregion

        /// <summary>
        /// Validates wheather the information from this instance is valid.
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            bool IsValid = true;

            if (_operator == QueryOperators.Ninguno)
                IsValid = false;

            return IsValid;
        }
    }

    /// <summary>
    /// Representa los operadores pertenecientes a una consulta
    /// </summary>
    private enum QueryOperators
    {
        Mayor = 0,
        MayorOIgual = 1,
        Menor = 2,
        MenorOIgual = 3,
        Igual = 4,
        Diferente = 5,
        Ninguno = 6,
        Similar = 7
    }

    /// <summary>
    /// Parsea un String en un operador.
    /// </summary>
    /// <param name="ValueNode"></param>
    /// <returns></returns>
    private static QueryOperators ParseValue(String value)
    {
        QueryOperators ParsedValue = QueryOperators.Ninguno;

        if (String.Compare(value, "<>", 0) == 0)
            ParsedValue = QueryOperators.Diferente;
        else if (String.Compare(value, "=", 0) == 0)
            ParsedValue = QueryOperators.Igual;
        else if (String.Compare(value, ">", 0) == 0)
            ParsedValue = QueryOperators.Mayor;
        else if (String.Compare(value, ">=", 0) == 0)
            ParsedValue = QueryOperators.MayorOIgual;
        else if (String.Compare(value, "<", 0) == 0)
            ParsedValue = QueryOperators.Menor;
        else if (String.Compare(value, "<=", 0) == 0)
            ParsedValue = QueryOperators.MenorOIgual;
        else if (String.Compare(value, "Like", 0) == 0)
            ParsedValue = QueryOperators.Similar;

        return ParsedValue;
    }

    /// <summary>
    /// Parsea un operador en su String
    /// </summary>
    /// <param name="ValueNode"></param>
    /// <returns></returns>
    private static String ParseValue(QueryOperators value)
    {

        String ParsedValue = string.Empty;

        switch (value)
        {
            case QueryOperators.Diferente:
                ParsedValue = "<>";
                break;
            case QueryOperators.Igual:
                ParsedValue = "=";
                break;
            case QueryOperators.Mayor:
                ParsedValue = ">";
                break;
            case QueryOperators.MayorOIgual:
                ParsedValue = ">=";
                break;
            case QueryOperators.Menor:
                ParsedValue = "<";
                break;
            case QueryOperators.MenorOIgual:
                ParsedValue = "<=";
                break;
            case QueryOperators.Similar:
                ParsedValue = "Like";
                break;
            case QueryOperators.Ninguno:
                ParsedValue = String.Empty;
                break;
        }
        return ParsedValue;
    }

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
        //Obtengo el path desde el web.config
        //string path = System.Web.Configuration.WebConfigurationManager.AppSettings["ExportPath"];
        string path = Server.MapPath("~/Exceptions/");
        if (System.IO.Directory.Exists(path) == false)
        {
            System.IO.Directory.CreateDirectory(path);
        }
        if (path.EndsWith("\\") == false)
        {
            path += "\\";
        }

        String FileName = Title;
        FileName += " ";
        FileName += System.DateTime.Now.ToString().Replace(":", "");
        FileName = FileName.Replace("/", "-");
        FileName = FileName.Replace(".", "");
        FileName += ".txt";

        path = path + FileName;
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
    private static XmlElement CreateIndexNode(Int64 id, String name, Int32 length, ref XmlDocument xmlDoc)
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
}