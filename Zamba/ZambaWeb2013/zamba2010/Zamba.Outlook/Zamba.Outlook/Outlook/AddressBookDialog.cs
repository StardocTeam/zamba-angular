using System.Collections.Generic;
using Microsoft.Office.Interop.Outlook;
using Exception = System.Exception;

namespace Zamba.Outlook.Outlook
{
    public class AddressBookDialog
    {
        private Application oOutlook;
        private SelectNamesDialog oDialog;

        private List<OutlookContact> _TO;
        private List<OutlookContact> _CC;
        private List<OutlookContact> _BCC;

        public List<OutlookContact> TO 
        { 
            get { return _TO; }
            set { _TO = value; }
        }

        public List<OutlookContact> CC
        {
            get { return _CC; }
            set { _CC = value; }
        }

        public List<OutlookContact> BCC
        {
            get { return _BCC; }
            set { _BCC = value; }
        }

        public AddressBookDialog()
        {
            oOutlook = new Application();

            _TO = new List<OutlookContact>();
            _CC = new List<OutlookContact>();
            _BCC = new List<OutlookContact>();
        }

        public int Show()
        {
            int resul = 1;

            try
            {
                string Address;
                object window;

                oDialog = oOutlook.Session.GetSelectNamesDialog();
                oDialog.ForceResolution = true;

                //if (_TO.Count > 0)
                //{
                //    List<OL.ContactItem> lista = new List<OL.ContactItem>();
                //    OL.ContactItem or = (OL.ContactItem)oOutlook.CreateItem(OL.OlItemType.olContactItem);

                //    or.FullName = _TO[0].FullName;
                //    or.Email1Address = _TO[0].EmailAddresses[0];
                    
                //    lista.Add(or);

                //    oDialog.Recipients = (OL.Recipients)or;
                //}

                if (oDialog.Display())
                {
                    foreach (Recipient recip in oDialog.Recipients)
                    {
                        //si es null es una lista
                        if (recip.Address == null)
                        {
                            //recorrer los miembros de la lista
                            foreach (AddressEntry recipLista in recip.AddressEntry.Members)
                            {
                                Address = GetContactAddress(recipLista);
                                AddressToList(Address, recip.Name, recip.Type.ToString());
                            }
                        }
                        else
                        {
                            Address = GetContactAddress(recip.AddressEntry);
                            AddressToList(Address, recip.Name, recip.Type.ToString());
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                resul = 0;
            }

            return resul;
        }

        // Devuelve la dirección de email del contacto
        private string GetContactAddress(AddressEntry Entry)
        {
            string Address = Entry.Address;

            if (Entry.Type.ToString() == "EX")
                Address = ResolveDisplayNameToSMTP(Entry);

            return Address;
        }

        // Obtiene la dirección SMTP a partir del nombre de usuario en Exchange
        private string ResolveDisplayNameToSMTP(AddressEntry Entry)
        {
            ExchangeUser oEU;
            ExchangeDistributionList oEDL;
            Recipient oRecip;
            string EmailAddress = Entry.Address;

            oRecip = oOutlook.Session.CreateRecipient(Entry.Name);
            oRecip.Resolve();

            if(oRecip.Resolved)
            {
                switch(oRecip.AddressEntry.AddressEntryUserType)
                {
                    case OlAddressEntryUserType.olExchangeUserAddressEntry:
                        oEU = oRecip.AddressEntry.GetExchangeUser();
                        if(!(oEU == null))
                            EmailAddress = oEU.PrimarySmtpAddress;
                        break;
                    case OlAddressEntryUserType.olExchangeDistributionListAddressEntry:
                        oEDL = oRecip.AddressEntry.GetExchangeDistributionList();
                        if(!(oEDL == null))
                            EmailAddress = oEDL.PrimarySmtpAddress;
                        break;    
                }
            }

            return EmailAddress;
        }

        //agrega el contacto a la coleccion segun el tipo
        private void AddressToList(string Address, string FullName, string TypeAddress)
        {
            Outlook.OutlookContact contact = new OutlookContact(FullName, Address);

            switch(TypeAddress)
            {
                case "1":
                    _TO.Add(contact);
                    break;
                case "2":
                    _CC.Add(contact);
                    break;
                case "3":
                    _BCC.Add(contact);
                    break;
            }
        }
    }
}