using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.IO;
using System;
using System.Collections.Generic;
using Zamba.Core.Pop3Utilities;
using System.Text;
using System.Net.Mail;

namespace Zamba.Pop3Utilities
{
    public class MailUtilities
    {
        protected static Regex BoundaryRegex = new Regex("Content-Type: multipart(?:/\\S+;)" + "\\s+[^\r\n]*boundary=\"?(?<boundary>" + "[^\"\r\n]+)\"?\r\n", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        protected static Regex UtcDateTimeRegex = new Regex(@"^(?:\w+,\s+)?(?<day>\d+)\s+(?<month>\w+)\s+(?<year>\d+)\s+(?<hour>\d{1,2})" + @":(?<minute>\d{1,2}):(?<second>\d{1,2})\s+(?<offsetsign>\-|\+)(?<offsethours>" + @"\d{2,2})(?<offsetminutes>\d{2,2})(?:.*)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        protected static Regex UrlRegex = new Regex("(?<url>https?://[^\\s\"]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        public static Regex CharsetRegex = new Regex("charset=\"?(?<charset>[^\\s\"]+)\"?", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        protected static Regex QuotedPrintableRegex = new Regex("=(?<hexchars>[0-9a-fA-F]{2,2})", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        protected static Regex FilenameRegex = new Regex("filename=\"?(?<filename>[^\\s\"]+)\"?", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        protected static Regex NameRegex = new Regex("name=\"?(?<filename>[^\\s\"]+)\"?", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static NameValueCollection ParseHeaders(string headerText)
        {
            NameValueCollection headers = new NameValueCollection();
            StringReader reader = new StringReader(headerText);
            string line;
            string headerName = null, headerValue;
            int colonIndx;
            while ((line = reader.ReadLine()) != null)
            {
                if (line == "")
                    break;
                if (Char.IsLetterOrDigit(line[0]) && (colonIndx = line.IndexOf(':')) != -1)
                {
                    headerName = line.Substring(0, colonIndx);
                    headerValue = line.Substring(colonIndx + 1).Trim();
                    headers.Add(headerName, headerValue);
                }
                else if (headerName != null)
                    headers[headerName] += " " + line.Trim();
                else
                    throw new FormatException("Could not parse headers");
            }
            return headers;
        }

        public static List<IDownloadedEmailBody> ParseMessageParts(string emailText)
        {
            List<IDownloadedEmailBody> messageParts = new List<IDownloadedEmailBody>();
            int newLinesIndx = emailText.IndexOf("\r\n\r\n");
            Match m = BoundaryRegex.Match(emailText);
            IDownloadedEmailBody newBody;
            if (m.Index < emailText.IndexOf("\r\n\r\n") && m.Success)
            {
                string boundary = m.Groups["boundary"].Value;
                string startingBoundary = "\r\n--" + boundary;
                int startingBoundaryIndx = -1;
                while (true)
                {
                    if (startingBoundaryIndx == -1)
                        startingBoundaryIndx = emailText.IndexOf(startingBoundary);
                    if (startingBoundaryIndx != -1)
                    {
                        int nextBoundaryIndx = emailText.IndexOf(startingBoundary, startingBoundaryIndx + startingBoundary.Length);
                        if (nextBoundaryIndx != -1 && nextBoundaryIndx != startingBoundaryIndx)
                        {
                            string multipartMsg = emailText.Substring(startingBoundaryIndx + startingBoundary.Length, (nextBoundaryIndx - startingBoundaryIndx - startingBoundary.Length));
                            int headersIndx = multipartMsg.IndexOf("\r\n\r\n");
                            if (headersIndx == -1)
                                throw new FormatException("Incompatible multipart message format");
                            string bodyText = multipartMsg.Substring(headersIndx).Trim();
                            NameValueCollection headers = MailUtilities.ParseHeaders(multipartMsg.Trim());

                            newBody = new DownloadedEmailBody(headers, bodyText);
                            newBody.ContentType = headers["Content-Type"];
                            newBody.Charset = MailUtilities.GetCharset(headers, newBody.ContentType);
                            newBody.ContentTransferEncoding = MailUtilities.GetContentTransferEncoding(headers);

                            messageParts.Add(newBody);
                        }
                        else
                            break;
                        startingBoundaryIndx = nextBoundaryIndx;
                    }
                    else
                        break;
                }
                if (newLinesIndx != -1)
                {
                    string emailBodyText = emailText.Substring(newLinesIndx + 1);
                }
            }
            else
            {
                int headersIndx = emailText.IndexOf("\r\n\r\n");
                if (headersIndx == -1)
                    throw new FormatException("Incompatible multipart message format");
                string bodyText = emailText.Substring(headersIndx).Trim();
                NameValueCollection headers = MailUtilities.ParseHeaders(emailText);

                newBody = new DownloadedEmailBody(headers, bodyText);
                newBody.ContentType = headers["Content-Type"];
                newBody.Charset = MailUtilities.GetCharset(headers,newBody.ContentType);
                newBody.ContentTransferEncoding = MailUtilities.GetContentTransferEncoding(headers);

                messageParts.Add(newBody);
            }
            return messageParts;
        }

        private static string GetContentTransferEncoding(NameValueCollection headers)
        {
            return headers["Content-Transfer-Encoding"]; 
        }

        private static string GetCharset(NameValueCollection headers, string contentType)
        {
            string charset = "us-ascii";
            Match m = MailUtilities.CharsetRegex.Match(contentType);

            if (m.Success)
                charset = m.Groups["charset"].Value;

            return charset;
        }

        public static DateTime ConvertStrToUtcDateTime(string str)
        {
            Match m = UtcDateTimeRegex.Match(str);
            int day, month, year, hour, min, sec;
            if (m.Success)
            {
                day = Convert.ToInt32(m.Groups["day"].Value);
                year = Convert.ToInt32(m.Groups["year"].Value);
                hour = Convert.ToInt32(m.Groups["hour"].Value);
                min = Convert.ToInt32(m.Groups["minute"].Value);
                sec = Convert.ToInt32(m.Groups["second"].Value);
                switch (m.Groups["month"].Value)
                {
                    case "Jan":
                        month = 1;
                        break;
                    case "Feb":
                        month = 2;
                        break;
                    case "Mar":
                        month = 3;
                        break;
                    case "Apr":
                        month = 4;
                        break;
                    case "May":
                        month = 5;
                        break;
                    case "Jun":
                        month = 6;
                        break;
                    case "Jul":
                        month = 7;
                        break;
                    case "Aug":
                        month = 8;
                        break;
                    case "Sep":
                        month = 9;
                        break;
                    case "Oct":
                        month = 10;
                        break;
                    case "Nov":
                        month = 11;
                        break;
                    case "Dec":
                        month = 12;
                        break;
                    default:
                        throw new FormatException("Unknown month.");
                }
                string offsetSign = m.Groups["offsetsign"].Value;
                int offsetHours = Convert.ToInt32(m.Groups["offsethours"].Value);
                int offsetMinutes = Convert.ToInt32(m.Groups["offsetminutes"].Value);
                DateTime dt = new DateTime(year, month, day, hour, min, sec);
                if (offsetSign == "+")
                {
                    dt.AddHours(offsetHours);
                    dt.AddMinutes(offsetMinutes);
                }
                else if (offsetSign == "-")
                {
                    dt.AddHours(-offsetHours);
                    dt.AddMinutes(-offsetMinutes);
                }
                return dt;
            }
            throw new FormatException("Incompatible date/time string format");
        }

        public static IDownloadedEmailHeader ParseHeader(string emailText, int emailId__1)
        {
            DownloadedEmailHeader headerToReturn = new DownloadedEmailHeader();

            headerToReturn.EmailId = emailId__1;
            headerToReturn.Headers = MailUtilities.ParseHeaders(emailText);
            headerToReturn.ContentType = headerToReturn.Headers["Content-Type"];
            headerToReturn.From = MailUtilities.DecodeSimpleString(headerToReturn.Headers["From"]);
            headerToReturn.To = MailUtilities.DecodeSimpleString(headerToReturn.Headers["To"]);
            headerToReturn.Subject = MailUtilities.DecodeSimpleString(headerToReturn.Headers["Subject"]);
            if (headerToReturn.Headers["Date"] != null)
            {
                try
                {
                    headerToReturn.UtcDateTime = MailUtilities.ConvertStrToUtcDateTime(headerToReturn.Headers["Date"]);
                }
                catch (FormatException generatedExceptionName)
                {
                    headerToReturn.UtcDateTime = DateTime.MinValue;
                }
            }
            else
            {
                headerToReturn.UtcDateTime = DateTime.MinValue;
            }

            return headerToReturn;
        }

        public static IDownloadedEmailBody FindMessagePart(List<IDownloadedEmailBody> msgParts, string contentType)
        {
            foreach (IDownloadedEmailBody p in msgParts)
                if (p.ContentType != null && p.ContentType.IndexOf(contentType) != -1)
                    return p;
            return null;
        }

        public static string FormatUrls(string plainText)
        {
            string replacementLink = "<a href=\"${url}\">${url}</a>";
            return UrlRegex.Replace(plainText, replacementLink);
        }

        public static string DecodeBase64String(string encodedString,string charset)
        {
            Decoder decoder = MailUtilities.GetDecoder(charset);
            byte[] buffer = Convert.FromBase64String(encodedString);
            char[] chararr = new char[decoder.GetCharCount(buffer, 0, buffer.Length)];
            decoder.GetChars(buffer, 0, buffer.Length, chararr, 0);
            return new string(chararr);
        }

        public static string DecodeQuotedPrintableString(string input, string charset)
        {
            var i = 0;
            var output = new List<byte>();
            while (i < input.Length)
            {
                if (input[i] == '=' && input[i + 1] == '\r' && input[i + 2] == '\n')
                {
                    //Skip
                    i += 3;
                }
                else if (input[i] == '=')
                {
                    string sHex = input;
                    sHex = sHex.Substring(i + 1, 2);
                    int hex = Convert.ToInt32(sHex, 16);
                    byte b = Convert.ToByte(hex);
                    output.Add(b);
                    i += 3;
                }
                else
                {
                    output.Add((byte)input[i]);
                    i++;
                }
            }
            if (String.IsNullOrEmpty(charset))
                return Encoding.UTF8.GetString(output.ToArray());
            else
                return Encoding.GetEncoding(charset).GetString(output.ToArray());
        }

        public static Decoder GetDecoder(string charset)
        {
            Decoder decoder;
            switch (charset.ToLower())
            {
                case "utf-7":
                    decoder = Encoding.UTF7.GetDecoder();
                    break;
                case "utf-8":
                    decoder = Encoding.UTF8.GetDecoder();
                    break;
                case "us-ascii":
                    decoder = Encoding.ASCII.GetDecoder();
                    break;
                case "iso-8859-1":
                    decoder = Encoding.ASCII.GetDecoder();
                    break;
                default:
                    decoder = Encoding.ASCII.GetDecoder();
                    break;
            }
            return decoder;
        }

        public static string DecodeSimpleString(string str)
        {
            int index = str.IndexOf("=?");

            if (index > -1)
            {
                StringBuilder sb = new StringBuilder();
                string[] stringParts = str.Split(new string[] { "?Q?" }, StringSplitOptions.RemoveEmptyEntries);
                List<string> parts = new List<string>();
                string[] subParts;
                string replacedText;

                if (stringParts.Length < 2)
                {
                    return str;
                }

                foreach (string part in stringParts)
                {
                    subParts = part.Split(new string[] { "?=" }, StringSplitOptions.RemoveEmptyEntries);
                    if (subParts.Length > 1)
                        parts.AddRange(subParts);
                    else
                        parts.Add(part);
                }

                int max = parts.Count / 2 + 1;
                for (int i = 0; i < max; i += 2)
                {
                    if (i + 1 < parts.Count)
                    {
                        sb.AppendLine(MailUtilities.DecodeQuotedPrintableString(parts[i + 1].Replace("=?", string.Empty).Replace("?=", string.Empty), parts[i].Replace("=?", string.Empty).Replace("?=", string.Empty).Trim()));
                    }
                }

                return sb.ToString();
            }
            return str;
        }

        internal static IDownloadedEmailBody GetOnlyHtmlMultiPart(string messagePart)
        {
            int nextPartIndex = messagePart.IndexOf("------=_NextPart");
            int nextPartIndex2;
            string splittedPart;
            bool partFinded = false;
            int contentTypeIndex;
            IDownloadedEmailBody body = null;
            NameValueCollection headers;
            string sHeaders;
            string sBody;

            while (nextPartIndex == -1 || !partFinded)
            {
                nextPartIndex2 = messagePart.ToLower().IndexOf("------=_nextpart",nextPartIndex + 1);

                splittedPart = messagePart.Substring(nextPartIndex, nextPartIndex2 - nextPartIndex);

                contentTypeIndex = splittedPart.ToLower().IndexOf("content-type: text/html");

                if (contentTypeIndex > -1)
                {
                    // + "content-type: text/html".Length
                    //contentTypeIndex, splittedPart.Length -  
                    sHeaders = splittedPart.Substring(contentTypeIndex, splittedPart.IndexOf("<") - contentTypeIndex);
                    sBody = splittedPart.Substring(splittedPart.IndexOf("<"));
                    headers = ParseHeaders(sHeaders);
                    body = new DownloadedEmailBody(headers, sBody);

                    body.ContentType = headers["Content-Type"];
                    body.Charset = MailUtilities.GetCharset(headers, body.ContentType);
                    body.ContentTransferEncoding = MailUtilities.GetContentTransferEncoding(headers);
                    partFinded = true;
                }
                else
                {
                    nextPartIndex = nextPartIndex2;
                }
            }

            return body;
        }
    }
}