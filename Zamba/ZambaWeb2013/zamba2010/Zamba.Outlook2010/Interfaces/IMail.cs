using System;

namespace Zamba.Outlook.Interfaces
{
    public interface IMail
    {
        void SendMail(string body, bool isBodyHTML, string subject, System.Collections.Generic.List<string> toMailAdresses, System.Collections.Generic.List<string> ccMailAddresses, System.Collections.Generic.List<string> bccMailAddresses);
        void SendMail(string body, bool isBodyHTML, string subject, System.Collections.Generic.List<string> toMailAdresses, System.Collections.Generic.List<string> ccMailAddresses, System.Collections.Generic.List<string> bccMailAddresses, System.Collections.Generic.List<string> AttachmentsFilePaths);
        void SendMail(string body, bool isBodyHTML, string subject, System.Collections.Generic.List<string> toMailAdresses);
        void SendMail(string body, bool isBodyHTML, string subject, System.Collections.Generic.List<string> toMailAdresses, System.Collections.Generic.List<string> ccMailAddresses);
    }
}
