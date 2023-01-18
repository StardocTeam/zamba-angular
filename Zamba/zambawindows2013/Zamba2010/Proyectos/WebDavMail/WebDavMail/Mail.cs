using System;
using System.Text;
using System.Xml;
using System.IO;
using System.Net;
using System.Xml.XPath;

namespace WebDavMail
{
    class Mail
    {
        public string p_strUserName;
        public string p_strPassword;
        public string p_strAlias;
        public string p_strInboxURL;
        public string p_strServer;
        public string p_strDrafts;
        
        /// <summary>
        /// Gets an XMLDocument containing a list of attachments, found in an email
        /// </summary>
        /// <param name="strMailUrl"></param>
        /// <returns></returns>
        public XmlDocument GetAttachmentsListXML(string strMailUrl)
        {
            XmlDocument loXmlDoc = new XmlDocument();
            try
            {
                MSXML2.XMLHTTP40 HttpWebRequest = default(MSXML2.XMLHTTP40);
                HttpWebRequest = new MSXML2.XMLHTTP40();
                HttpWebRequest.open("X-MS-ENUMATTS", strMailUrl, false, p_strUserName, p_strPassword);
                HttpWebRequest.setRequestHeader("Depth", "1");
                HttpWebRequest.setRequestHeader("Content-type", "xml");
                HttpWebRequest.send("");
                loXmlDoc.LoadXml(HttpWebRequest.responseText);
                HttpWebRequest = null;
            }
            catch (Exception ex)
            {
                throw;
            }
            return loXmlDoc;
        }

        /// <summary>
        /// Extracts an attachment from an email
        /// </summary>
        /// <param name="sAttachmentUrl"></param>
        /// <returns></returns>
        public string getAttachmentFromMail(string sAttachmentUrl)
        {
            string strResult = "";
            try
            {
                MSXML2.XMLHTTP40 HttpWebRequest = default(MSXML2.XMLHTTP40);
                HttpWebRequest = new MSXML2.XMLHTTP40();
                HttpWebRequest.open("GET", sAttachmentUrl, false, p_strUserName, p_strPassword);
                HttpWebRequest.send("");
                strResult = HttpWebRequest.responseText;
                HttpWebRequest = null;
            }
            catch
            {
                throw;
            }
            return strResult;
        }

        /// <summary>
        /// Gets all unread email messages, containing at least one attachment, from an email account on an exchange server
        /// </summary>
        /// <returns></returns>
        public XmlDocument GetUnreadMailWithAttachments()
        {
            HttpWebRequest loRequest = default(HttpWebRequest);
            HttpWebResponse loResponse = default(HttpWebResponse);
            string lsRootUri = null;
            string lsQuery = null;
            byte[] laBytes = null;

            Stream loRequestStream = default(Stream);
            Stream loResponseStream = default(Stream);
            XmlDocument loXmlDoc = default(XmlDocument);
            loXmlDoc = new XmlDocument();
            try
            {
                lsRootUri = p_strServer + "/Exchange/" + p_strAlias + "/" + p_strInboxURL;
                lsQuery = "<?xml version=\"1.0\"?>"
                            + "<D:searchrequest xmlns:D = \"DAV:\" xmlns:m=\"urn:schemas:httpmail:\">"
                            + "<D:sql>SELECT \"urn:schemas:httpmail:hasattachment\", \"DAV:displayname\", "
                            + "\"urn:schemas:httpmail:from\", \"urn:schemas:httpmail:subject\", "
                            + "\"urn:schemas:httpmail:htmldescription\" FROM \"" + lsRootUri 
                            + "\" WHERE \"DAV:ishidden\" = false AND \"DAV:isfolder\" = false AND "
                            + "\"urn:schemas:httpmail:hasattachment\" = true AND \"urn:schemas:httpmail:read\" = false"
                            + "</D:sql></D:searchrequest>";
                loRequest = (HttpWebRequest)WebRequest.Create(lsRootUri);
                loRequest.Credentials = new NetworkCredential(p_strUserName, p_strPassword);
                loRequest.Method = "SEARCH";
                laBytes = System.Text.Encoding.UTF8.GetBytes(lsQuery);
                loRequest.ContentLength = laBytes.Length;
                loRequestStream = loRequest.GetRequestStream();
                loRequestStream.Write(laBytes, 0, laBytes.Length);
                loRequestStream.Close();
                loRequest.ContentType = "text/xml";
                loRequest.Headers.Add("Translate", "F");
                loResponse = (HttpWebResponse)loRequest.GetResponse();
                loResponseStream = loResponse.GetResponseStream();
                loXmlDoc.Load(loResponseStream);
                loResponseStream.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            return loXmlDoc;
        }

        /// <summary>
        /// Gets all unread email messages from an email account on an exchange server
        /// </summary>
        /// <returns></returns>
        public XmlDocument GetUnreadMailAll()
        {
            HttpWebRequest loRequest = default(HttpWebRequest);
            HttpWebResponse loResponse = default(HttpWebResponse);
            string lsRootUri = null;
            string lsQuery = null;
            byte[] laBytes = null;

            Stream loRequestStream = default(Stream);
            Stream loResponseStream = default(Stream);
            XmlDocument loXmlDoc = default(XmlDocument);
            loXmlDoc = new XmlDocument();
            try
            {
                lsRootUri = p_strServer + "/Exchange/" + p_strAlias + "/" + p_strInboxURL;
                lsQuery = "<?xml version=\"1.0\"?>"
                            + "<D:searchrequest xmlns:D = \"DAV:\" xmlns:m=\"urn:schemas:httpmail:\">"
                            + "<D:sql>SELECT \"urn:schemas:httpmail:hasattachment\", \"DAV:displayname\", "
                            + "\"urn:schemas:httpmail:from\", \"urn:schemas:httpmail:subject\", "
                            + "\"urn:schemas:httpmail:htmldescription\" FROM \"" + lsRootUri
                            + "\" WHERE \"DAV:ishidden\" = false "
                            + "AND \"DAV:isfolder\" = false " 
                            //+ "AND \"urn:schemas:httpmail:hasattachment\" = true "
                            + "AND \"urn:schemas:httpmail:read\" = false"
                            + "</D:sql></D:searchrequest>";
                loRequest = (HttpWebRequest)WebRequest.Create(lsRootUri);
                loRequest.Credentials = new NetworkCredential(p_strUserName, p_strPassword);
                loRequest.Method = "SEARCH";
                laBytes = System.Text.Encoding.UTF8.GetBytes(lsQuery);
                loRequest.ContentLength = laBytes.Length;
                loRequestStream = loRequest.GetRequestStream();
                loRequestStream.Write(laBytes, 0, laBytes.Length);
                loRequestStream.Close();
                loRequest.ContentType = "text/xml";
                loRequest.Headers.Add("Translate", "F");
                loResponse = (HttpWebResponse)loRequest.GetResponse();
                loResponseStream = loResponse.GetResponseStream();
                loXmlDoc.Load(loResponseStream);
                loResponseStream.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            return loXmlDoc;
        }

        /// <summary>
        /// Returns information about all mailboxes
        /// </summary>
        /// <returns></returns>
        public XmlDocument GetAllMailboxInfo()
        {
            XmlDocument loXmlDoc = new XmlDocument();
            
            string lsRootUri = p_strServer + "/Exchange/" + p_strAlias + "/" + p_strInboxURL;

            byte[] buffer = GetFolderSizeRequest(lsRootUri);
            var request = (HttpWebRequest)WebRequest.Create(lsRootUri);
            request.Method = "SEARCH";
            request.ContentType = "text/xml";
            request.Credentials = new NetworkCredential(p_strUserName, p_strPassword);
            request.Headers.Add("Translate", "f");
            request.Headers.Add("Depth", "1");
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(buffer, 0, buffer.Length);
            }
            HttpWebResponse loResponse = (HttpWebResponse)request.GetResponse();
            Stream loResponseStream = loResponse.GetResponseStream();
            loXmlDoc.Load(loResponseStream);

            return loXmlDoc;
        }

        /// <summary>
        /// Helper class
        /// </summary>
        /// <returns></returns>
        public long GetMailboxSize()
        {
            return GetMailboxSize(p_strServer + "/Exchange/" + p_strAlias + "/" + p_strInboxURL);
        }

        /// <summary>
        /// Returns the total size of all mailboxes within the root node of a mailbox url
        /// </summary>
        /// <param name="lsRootUri"></param>
        /// <returns></returns>
        private long GetMailboxSize(string lsRootUri)
        {
            XmlReader reader;
            byte[] buffer = GetFolderSizeRequest(lsRootUri);
            var request = (HttpWebRequest) WebRequest.Create(lsRootUri);
            request.Method = "SEARCH";
            request.ContentType = "text/xml";
            request.Credentials = new NetworkCredential(p_strUserName, p_strPassword);
            request.Headers.Add("Translate", "f");  
            request.Headers.Add("Depth", "1");
            using (Stream stream = request.GetRequestStream()) 
            {
                stream.Write(buffer, 0, buffer.Length); 
            }  
            using (WebResponse response = request.GetResponse()) 
            {  
                string content = new StreamReader(response.GetResponseStream()).ReadToEnd();  
                reader = XmlReader.Create(new StringReader(content));  
                var nsmgr = new XmlNamespaceManager(reader.NameTable);
                nsmgr.AddNamespace("dav", "DAV:");
                nsmgr.AddNamespace("e", "http://schemas.microsoft.com/mapi/proptag/");  
                var doc = new XPathDocument(reader);  
                long result = 0;  
                foreach (XPathNavigator element in doc.CreateNavigator().Select("//dav:response[dav:propstat/dav:status = 'HTTP/1.1 200 OK']", nsmgr))  
                {  
                    var size = element.SelectSingleNode("dav:propstat/dav:prop/e:x0e080014", nsmgr).ValueAsLong; 
                    string folderUrl = element.SelectSingleNode("dav:href", nsmgr).Value;  
                    result += size; 
                    bool hasSubs = element.SelectSingleNode("dav:propstat/dav:prop/dav:hassubs", nsmgr).ValueAsBoolean; 
                    if (hasSubs)  
                    {
                        result += GetMailboxSize(folderUrl);  
                    }  
                }  
                return result;  
            }  
        }

        /// <summary>
        /// Returns the size of one mail folder
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private byte[] GetFolderSizeRequest(string sUrl)  
        { 
            var settings = new XmlWriterSettings {Encoding = Encoding.UTF8}; 
            using (var stream = new MemoryStream()) 
            using (XmlWriter writer = XmlWriter.Create(stream, settings)) 
            {
                writer.WriteStartElement("searchrequest", "DAV:");  
                var searchRequest = new StringBuilder();  
                searchRequest.AppendFormat("SELECT \"http://schemas.microsoft.com/mapi/proptag/x0e080014\", \"DAV:hassubs\" FROM SCOPE ('HIERARCHICAL TRAVERSAL OF \"{0}\"')", sUrl);  
                writer.WriteElementString("sql", searchRequest.ToString());  
                writer.WriteEndElement();  
                writer.WriteEndDocument();  
                writer.Flush();  
                return stream.ToArray(); 
            }  
        }

        /// <summary>
        /// Marks an email an read
        /// </summary>
        /// <param name="strMailUrl"></param>
        internal string MarkAsRead(string strMailUrl)
        {
            string strResult = "";
            HttpWebRequest loRequest = default(HttpWebRequest);
            HttpWebResponse loResponse = default(HttpWebResponse);
            string lsQuery = null;
            byte[] laBytes = null;

            Stream loRequestStream = default(Stream);
            XmlDocument loXmlDoc = default(XmlDocument);
            loXmlDoc = new XmlDocument();
            try
            {
                lsQuery = "<?xml version=\"1.0\"?>"
                        + "<a:propertyupdate xmlns:a=\"DAV:\" xmlns:d=\"urn:schemas-microsoft-com:exch-data:\" "
                        + "xmlns:b=\"urn:schemas:httpmail:\" xmlns:c=\"xml:\">"
                        + "<a:set><a:prop><b:read>" + 1
                        + "</b:read></a:prop>"
                        + "</a:set></a:propertyupdate>";

                loRequest = (HttpWebRequest)HttpWebRequest.Create(strMailUrl);
                loRequest.Credentials = new NetworkCredential(p_strUserName, p_strPassword);
                loRequest.Method = "PROPPATCH";
                laBytes = Encoding.UTF8.GetBytes((string)lsQuery);
                loRequest.ContentLength = laBytes.Length;
                loRequestStream = loRequest.GetRequestStream();
                loRequestStream.Write(laBytes, 0, laBytes.Length);
                loRequestStream.Close();
                loRequest.ContentType = "text/xml";
                loResponse = (HttpWebResponse)loRequest.GetResponse();
                strResult = loResponse.StatusCode.ToString();
                loRequest = null;
                loResponse = null;

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                loXmlDoc = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            return strResult;
        }

        /// <summary>
        /// Sending an email
        /// </summary>
        /// <param name="strSendTo"></param>
        /// <param name="strSendSubject"></param>
        /// <param name="strSendBody"></param>
        internal string SendMail(string strSendTo, string strSendSubject, string strSendBody)
        {
            HttpWebRequest PUTRequest = default(HttpWebRequest);
            WebResponse PUTResponse = default(WebResponse);
            HttpWebRequest MOVERequest = default(HttpWebRequest);
            WebResponse MOVEResponse = default(WebResponse);
            string strMailboxURI = "";
            string strSubURI = "";
            string strTempURI = "";
            string strTo = strSendTo;
            string strSubject = strSendSubject;
            string strText = strSendBody;
            string strBody = "";
            byte[] bytes = null;
            Stream PUTRequestStream = null;

            try
            {
                strMailboxURI = p_strServer + "/exchange/" + p_strAlias;

                strSubURI = p_strServer + "/exchange/" + p_strAlias
                          + "/##DavMailSubmissionURI##/";

                strTempURI = p_strServer + "/exchange/" + p_strAlias
                           + "/" + p_strDrafts + "/" + strSubject + ".eml";

                strBody = "To: " + strTo + "\n" +
                "Subject: " + strSubject + "\n" +
                "Date: " + System.DateTime.Now +
                "X-Mailer: test mailer" + "\n" +
                "MIME-Version: 1.0" + "\n" +
                "Content-Type: text/plain;" + "\n" +
                "Charset = \"iso-8859-1\"" + "\n" +
                "Content-Transfer-Encoding: 7bit" + "\n" +
                "\n" + strText;

                PUTRequest = (HttpWebRequest)HttpWebRequest.Create(strTempURI);
                PUTRequest.Credentials = new NetworkCredential(p_strUserName, p_strPassword);
                PUTRequest.Method = "PUT";
                bytes = Encoding.UTF8.GetBytes((string)strBody);
                PUTRequest.ContentLength = bytes.Length;
                PUTRequestStream = PUTRequest.GetRequestStream();
                PUTRequestStream.Write(bytes, 0, bytes.Length);
                PUTRequestStream.Close();
                PUTRequest.ContentType = "message/rfc822";
                PUTResponse = (HttpWebResponse)PUTRequest.GetResponse();
                MOVERequest = (HttpWebRequest)HttpWebRequest.Create(strTempURI);
                MOVERequest.Credentials = new NetworkCredential(p_strUserName, p_strPassword);
                MOVERequest.Method = "MOVE";
                MOVERequest.Headers.Add("Destination", strSubURI);
                MOVEResponse = (HttpWebResponse)MOVERequest.GetResponse();
                Console.WriteLine("Message successfully sent.");

                // Clean up.
                PUTResponse.Close();
                MOVEResponse.Close();

            }
            catch (Exception ex)
            {
                throw;
            }
            return strBody;
        }

        /// <summary>
        /// Returns all contacts where firstname or lastname starts with a certain character or string
        /// </summary>
        /// <returns></returns>
        internal string PrintContactsUsingExchangeWebDAV(string strZoekString)
        {
            string sResult = "";
            NetworkCredential credentials = new NetworkCredential(p_strUserName, p_strPassword);
            string uri = p_strServer + "/exchange/" + p_strAlias;

            string sRequest = string.Format(
                @"<?xml version=""1.0""?>
                <g:searchrequest xmlns:g=""DAV:"">
                    <g:sql>
                        SELECT
                            ""urn:schemas:contacts:sn"", ""urn:schemas:contacts:givenName"",
                            ""urn:schemas:contacts:email1"", ""urn:schemas:contacts:telephoneNumber"", 
                            ""urn:schemas:contacts:bday"", ""urn:schemas:contacts:nickname"",
                            ""urn:schemas:contacts:o"", ""    urn:schemas:contacts:profession""
                        FROM
                            Scope('SHALLOW TRAVERSAL OF ""{0}/exchange/{1}/contacts""')
                        WHERE
                            ""urn:schemas:contacts:givenName"" LIKE '{2}%'
                        OR
                            ""urn:schemas:contacts:sn"" LIKE '{2}%'
                    </g:sql>
                </g:searchrequest>",
                p_strServer, p_strAlias, strZoekString);
            // For more contact information look up urn:schemas:contacts on MSDN
            byte[] contents = Encoding.UTF8.GetBytes(sRequest);

            HttpWebRequest request = HttpWebRequest.Create(uri) as HttpWebRequest;
            request.Credentials = credentials;
            request.Method = "SEARCH";
            request.ContentLength = contents.Length;
            request.ContentType = "text/xml";
            
            using (Stream requestStream = request.GetRequestStream())
                requestStream.Write(contents, 0, contents.Length);

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            using (Stream responseStream = response.GetResponseStream())
           {
                XmlDocument document = new XmlDocument();
                document.Load(responseStream);

                foreach (XmlElement element in document.GetElementsByTagName("a:prop"))
               {
                   if (element.InnerText.Length > 0)
                   {
                       sResult = sResult + string.Format("Name:  {0} {1}\nNickname:  {2}\nBirthday: {3}\nEmail: {4}\nPhone: {5}\nProfession:  {6}\nCompany:  {7}",
                           (element["d:givenName"] != null ? element["d:givenName"].InnerText : ""),
                           (element["d:sn"] != null ? element["d:sn"].InnerText : ""),
                           (element["d:nickname"] != null ? element["d:nickname"].InnerText : ""),
                           (element["d:bday"] != null ? element["d:bday"].InnerText : ""),
                           (element["d:email1"] != null ? element["d:email1"].InnerText : ""),
                           (element["d:telephoneNumber"] != null ? element["d:telephoneNumber"].InnerText : ""),
                           (element["d:profession"] != null ? element["d:profession"].InnerText : ""),
                           (element["d:o"] != null ? element["d:o"].InnerText : "")
                           ) + Environment.NewLine + Environment.NewLine;
                   }
                }
            }
            return sResult;
        }

        /// <summary>
        /// Gets all unread email messages from an email account on an exchange server
        /// </summary>
        /// <returns></returns>
        public XmlDocument DownloadAttachs(string strMailUrl)
        {
            // Variables.
            System.Net.HttpWebRequest Request;
            System.Net.WebResponse Response;
            System.Net.CredentialCache MyCredentialCache;
            string strMessageURI = strMailUrl;
            string strUserName = this.p_strUserName;
            string strPassword = this.p_strPassword;
            string strDomain = "";
            System.IO.Stream ResponseStream = null;
            System.Xml.XmlDocument ResponseXmlDoc = null;
            System.Xml.XmlNode root = null;
            System.Xml.XmlNamespaceManager nsmgr = null;
            System.Xml.XmlNodeList PropstatNodes = null;
            System.Xml.XmlNodeList HrefNodes = null;
            System.Xml.XmlNode StatusNode = null;
            System.Xml.XmlNode PropNode = null;

            try
            {
                // Create a new CredentialCache object and fill it with the network
                // credentials required to access the server.
                MyCredentialCache = new System.Net.CredentialCache();
                MyCredentialCache.Add(new System.Uri(strMessageURI),
                   "NTLM",
                   new System.Net.NetworkCredential(strUserName, strPassword, strDomain)
                   );

                // Create the HttpWebRequest object.
                Request = (System.Net.HttpWebRequest)HttpWebRequest.Create(strMessageURI);

                // Add the network credentials to the request.
                Request.Credentials = MyCredentialCache;

                // Specify the method.
                Request.Method = "X-MS-ENUMATTS";

                // Send the X-MS-ENUMATTS method request and get the
                // response from the server.
                Response = (HttpWebResponse)Request.GetResponse();

                // Get the XML response stream.
                ResponseStream = Response.GetResponseStream();

                // Create the XmlDocument object from the XML response stream.
                ResponseXmlDoc = new System.Xml.XmlDocument();

                // Load the XML response stream.
                ResponseXmlDoc.Load(ResponseStream);

                // Get the root node.
                root = ResponseXmlDoc.DocumentElement;

                // Create a new XmlNamespaceManager.
                nsmgr = new System.Xml.XmlNamespaceManager(ResponseXmlDoc.NameTable);

                // Add the DAV: namespace, which is typically assigned the a: prefix
                // in the XML response body.  The namespaceses and their associated
                // prefixes are listed in the attributes of the DAV:multistatus node
                // of the XML response.
                nsmgr.AddNamespace("a", "DAV:");

                // Add the http://schemas.microsoft.com/mapi/proptag/ namespace, which
                // is typically assigned the d: prefix in the XML response body.
                nsmgr.AddNamespace("d", "http://schemas.microsoft.com/mapi/proptag/");

                // Use an XPath query to build a list of the DAV:propstat XML nodes,
                // corresponding to the returned status and properties of
                // the file attachment(s).
                PropstatNodes = root.SelectNodes("//a:propstat", nsmgr);

                // Use an XPath query to build a list of the DAV:href nodes,
                // corresponding to the URIs of the attachement(s) on the message.
                // For each DAV:href node in the XML response, there is an
                // associated DAV:propstat node.
                HrefNodes = root.SelectNodes("//a:href", nsmgr);


                string urlAttach = string.Empty;

                // Attachments found?
                if (HrefNodes.Count > 0)
                {
                    // Display the number of attachments on the message.
                    Console.WriteLine(HrefNodes.Count + " attachments found...");

                    // Iterate through the attachment properties.
                    for (int i = 0; i < HrefNodes.Count; i++)
                    {
                        // Use an XPath query to get the DAV:status node from the DAV:propstat node.
                        StatusNode = PropstatNodes[i].SelectSingleNode("a:status", nsmgr);

                        // Check the status of the attachment properties.
                        if (StatusNode.InnerText != "HTTP/1.1 200 OK")
                        {
                            urlAttach = HrefNodes[i].InnerText;
                            Console.WriteLine("Attachment: " + HrefNodes[i].InnerText);
                            Console.WriteLine("Status: " + StatusNode.InnerText);
                            Console.WriteLine("");
                        }
                        else
                        {
                            urlAttach = HrefNodes[i].InnerText;
                            Console.WriteLine("Attachment: " + HrefNodes[i].InnerText);
                            Console.WriteLine("Status: " + StatusNode.InnerText);

                            // Get the CdoPR_ATTACH_FILENAME_W MAPI property tag,
                            // corresponding to the attachment file name.  The
                            // http://schemas.microsoft.com/mapi/proptag/ namespace is typically
                            // assigned the d: prefix in the XML response body.
                            PropNode = PropstatNodes[i].SelectSingleNode("a:prop/d:x3704001f", nsmgr);
                            Console.WriteLine("Attachment name: " + PropNode.InnerText);
                            string FileName = PropNode.InnerText;

                            // Get the CdoPR_ATTACH_EXTENSION_W MAPI property tag,
                            // corresponding to the attachment file extension.
                            PropNode = PropstatNodes[i].SelectSingleNode("a:prop/d:x3703001f", nsmgr);
                            Console.WriteLine("File extension: " + PropNode.InnerText);

                            // Get the CdoPR_ATTACH_SIZE MAPI property tag,
                            // corresponding to the attachment file size.
                            PropNode = PropstatNodes[i].SelectSingleNode("a:prop/d:x0e200003", nsmgr);
                            Console.WriteLine("Attachment size: " + PropNode.InnerText);

                            Console.WriteLine("");

                            try
                            {
                                downloadFile(this.p_strUserName, this.p_strPassword, urlAttach, FileName);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Errores al Descargar el archivo.");
                            }
                            
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No attachments found.");
                }

                Console.ReadLine();

              

                // Clean up.
                ResponseStream.Close();
                Response.Close();

                

            }
            catch (Exception ex)
            {
                // Catch any exceptions. Any error codes from the X-MS-ENUMATTS
                // method request on the server will be caught here, also.
                Console.WriteLine(ex.Message);
            }
            return ResponseXmlDoc;
        }

        public bool downloadFile(string userName, string password, string attachUrl, string fname)
        {

            System.Net.HttpWebRequest myreq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(attachUrl);
            myreq.Credentials = new System.Net.NetworkCredential(userName, password);
            WebResponse o_resp = myreq.GetResponse();
            Stream instream = o_resp.GetResponseStream();
            FileStream fs = new FileStream(@"C:\" + fname, FileMode.Create);
            StreamWriter w = new StreamWriter(fs);
            //StreamReader sr= new StreamReader(instream);
            //String sResp= sr.ReadToEnd();

            byte[] buffer = new byte[8192];
            int bytesRead;
            do
            {
                bytesRead = instream.Read(buffer, 0, buffer.Length);
                if (bytesRead == 0) break;
                fs.Write(buffer, 0, bytesRead);

            } while (bytesRead > 0);


            //w.Write(sResp);
            w.Close();
            fs.Close();

            return true;
        }
    }
}
