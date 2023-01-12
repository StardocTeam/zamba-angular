using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamba.Services.RemoteInterfaces;

namespace Zamba.ExportBusiness
{
   public class ExportBusiness
    {
        public void GetExportMailOptions(bool automatedExport, out bool HasToChangeMail, out bool HasToAddLink, out string LinkToExportedMailLocation, out bool MarkExportedMail)
        {
            if (automatedExport)
            {
                HasToChangeMail = Boolean.Parse(UserPreferences.getValueForMachine("ChangeExportedMail", UPSections.ExportaPreferences, true));
                HasToAddLink = Boolean.Parse(UserPreferences.getValueForMachine("AddLinkToExportedMail", UPSections.ExportaPreferences, true));
                MarkExportedMail = Boolean.Parse(UserPreferences.getValueForMachine("MarkExportedMail", UPSections.ExportaPreferences, true));

                LinkToExportedMailLocation = UserPreferences.getValueForMachine("LinkToExportedMailLocation", UPSections.ExportaPreferences, "Top");
            }
            else
            {
                HasToChangeMail = Boolean.Parse(UserPreferences.getValue("ChangeExportedMail", UPSections.ExportaPreferences, true));
                HasToAddLink = Boolean.Parse(UserPreferences.getValue("AddLinkToExportedMail", UPSections.ExportaPreferences, true));
                MarkExportedMail = Boolean.Parse(UserPreferences.getValue("MarkExportedMail", UPSections.ExportaPreferences, true));
                LinkToExportedMailLocation = UserPreferences.getValue("LinkToExportedMailLocation", UPSections.ExportaPreferences, "Top");
            }
        }

        public string GetNewBody(IMail mail, Microsoft.Office.Interop.Outlook.MailItem _mail)
        {
            return "<html><body><style type=\"text/css\"> .button a { margin:10px;border:2px solid;border-color:#A0B4E3 #000000 #000000 #A7BEDE;padding:0 3px;background:#8DADDA;font:bold 12px arial,verdana,sans-serif;color:#FFFFFF;text-decoration:none;} .button a:active { border:2px inset; } </style>"
             + "<h3>El mail se ha exportado a zamba</h3></br><span class=\"button\"><a href=\"" + mail.link + "\">   "
             + LinkTextDescription + "   </a></span>"
             + "<INPUT TYPE=hidden NAME=\"EntryId\" VALUE=\"" + _mail.EntryID + "\"></body></html>";
        }

        private string _linkTextDescription;
        public string LinkTextDescription
        {
            get
            {
                if (string.IsNullOrEmpty(_linkTextDescription))
                    _linkTextDescription = "Abrir Mail";
                return _linkTextDescription;
            }
        }
    }
}
