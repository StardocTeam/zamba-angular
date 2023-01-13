using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Outlook;
using System.Diagnostics;
using Zamba.Core;
using System.Reflection;
using System.Collections;

namespace TestExchange
{
    public class Outlook
    {
        private const String MAPI_NAMESPACE = "MAPI";        

        private Application _appOutlook = null;

        public Outlook()
        {
            _appOutlook = new Application();

            Log("Outlook version: " + _appOutlook.Version.ToString());
        }

        // devuelve una lista de contactos pertenecientes a la libreta especificada
        public List<OutlookContact> GetAdressBookContacts(string AddressBook)
        {
            OutlookContact CurrentContact = null;
            
            Log("OutlookApplication.GetNamespace(MAPI_NAMESPACE)");
            _NameSpace NameSpace = _appOutlook.GetNamespace(MAPI_NAMESPACE);

            NameSpace.Logon(null, null, true, true);

            List<OutlookContact> ContactList = new List<OutlookContact>();

            // cuando intente leer la libreta de exchange va a fallar
            // si outlook esta trabajando offline
            try
            {
                Log("Buscando libreta seleccionada");

                foreach (AddressList list in NameSpace.AddressLists)
                {
                    if (list.Name == AddressBook)
                    {
                        Log("  Libreta encontrada");
                        Log("  Recorriendo contactos de la libreta");

                        foreach (AddressEntry entry in list.AddressEntries)
                        {
                            CurrentContact = CreateContact(entry);

                            if (CurrentContact != null)
                                ContactList.Add(CurrentContact);
                        }
                        break;
                    }
                }
            }
            catch (System.Exception ex)
            {
                Log("Exception: " + ex.ToString());
                ZClass.raiseerror(ex);
            }
            return ContactList;
        }

        // crea un contacto a partir de una entrada en una libreta
        private OutlookContact CreateContact(AddressEntry entry)
        {
            OutlookContact contact = null;

            // usuario normal
            if (entry.DisplayType == OlDisplayType.olUser)
            {
                if (entry.Address != null)
                {
                    if (entry.Type.ToString() == "EX")
                        contact = new OutlookContact(entry.Name, entry.Address, OutlookContact.ContactTypes.SINGLE, OutlookContact.AddressTypes.EXCHANGE);
                    else
                        contact = new OutlookContact(entry.Name, entry.Address, OutlookContact.ContactTypes.SINGLE, OutlookContact.AddressTypes.SMTP);

                    Log("    Contacto: " + contact.FullName + " - Tipo: " + contact.ContactType.ToString());
                }
            }
            
            // lista de distribucion
            if (entry.DisplayType == OlDisplayType.olDistList || 
                entry.DisplayType == OlDisplayType.olPrivateDistList)
            {
                Log("    Lista de distribucion: " + entry.Name);

                if (entry.Type.ToString() == "EX")
                    contact = new OutlookContact(entry.Name, entry.Address, OutlookContact.ContactTypes.DISTRIBUTION_LIST, OutlookContact.AddressTypes.EXCHANGE);
                else
                    contact = new OutlookContact(entry.Name, entry.Address, OutlookContact.ContactTypes.DISTRIBUTION_LIST, OutlookContact.AddressTypes.SMTP);
            }

            return contact;
        }

        // transforma la direccion de email de formato exchange a smtp
        public void ResolveExchangeAddress(ref OutlookContact contact)
        {
            Log("    ResolveExchangeAddress()");
            
            if (!contact.Resolved)
            {
                Log("        Exchange: " + contact.EmailAddress);

                contact.EmailAddress = GetSMTPAddress(contact.EmailAddress);
                contact.AddressType = OutlookContact.AddressTypes.SMTP;
                contact.Resolved = true;
                
                Log("        SMTP: " + contact.EmailAddress);
            }
        }

        // metodo interno para convertir la direccion de exchange a smtp
        private string GetSMTPAddress(string strAddress)
        {
            string strRet = string.Empty;
            
            Log("    GetSMTPAddress()");

            try
            {
                //IF OUTLOOK VERSION IS >= 2007 THEN USES NATIVE OOM PROPERTIES AND METHODS             
                if (int.Parse((_appOutlook.Version.ToString().Substring(0, 2))) >= 12)
                {
                    Recipient oRec;

                    Log("    OutlookApplication.Session.CreateRecipient(" + strAddress + ")");
                    oRec = _appOutlook.Session.CreateRecipient(strAddress);

                    Log("    oRec.Resolve()");

                    if (oRec.Resolve())
                    {
                        Microsoft.Office.Interop.Outlook.ExchangeUser oEU;
                        Microsoft.Office.Interop.Outlook.ExchangeDistributionList oEDL;

                        switch (oRec.AddressEntry.AddressEntryUserType)
                        {
                            case Microsoft.Office.Interop.Outlook.OlAddressEntryUserType.olExchangeUserAddressEntry:
                                
                                Log("    oRec.AddressEntry.GetExchangeUser()");
                                oEU = oRec.AddressEntry.GetExchangeUser();
                                
                                if (oEU != null)
                                    strRet = oEU.PrimarySmtpAddress;
                                
                                break;

                            case Microsoft.Office.Interop.Outlook.OlAddressEntryUserType.olExchangeDistributionListAddressEntry:
                         
                                Log("    oRec.AddressEntry.GetExchangeDistributionList(");
                                oEDL = oRec.AddressEntry.GetExchangeDistributionList();
                                
                                if (oEDL != null)
                                    strRet = oEDL.PrimarySmtpAddress;

                                break;
                        }
                    }
                    else
                        Log("    Direccion NO resuelta");
                }

                // si no puede usar los objetos de O2007 lo hace de la forma complicada
                if (strRet == string.Empty)
                {
                    ContactItem oCon;
                    string strKey;

                    Log("    CreateItem(OlItemType.olContactItem)");                    

                    oCon = (ContactItem)_appOutlook.CreateItem(OlItemType.olContactItem);
                    oCon.Email1Address = strAddress;

                    strKey = "_" + DateTime.Now.ToString("hh:mm:ss.fff").Replace(".", "").Replace(":", "");
                    oCon.FullName = strKey;

                    Log("    oCon.FullName = " + oCon.FullName);
                    Log("    oCon.Save()");

                    oCon.Save();

                    strRet = oCon.Email1DisplayName.Replace("(", "").Replace(")", "").Replace(strKey, "").Trim();

                    Log("    strRet = " + strRet);
                    Log("    oCon.Delete()");

                    oCon.Delete();
                    oCon = null;

                    oCon = (ContactItem)_appOutlook.Session.GetDefaultFolder(OlDefaultFolders.olFolderDeletedItems).Items.Find("[Subject]=" + strKey);

                    if (oCon != null)
                        oCon.Delete();
                }
            }
            catch(System.Exception ex)
            {
                Log("Exception: " + ex.ToString());
                ZClass.raiseerror(ex);
            }

            return strRet;
        }

        // devuelve una lista con los nombres de las libretas de direcciones disponibles
        public List<string> GetAddressLists()
        {
            Log("OutlookApplication.GetNamespace(MAPI_NAMESPACE)");
            _NameSpace NameSpace = _appOutlook.GetNamespace(MAPI_NAMESPACE);

            NameSpace.Logon(null, null, true, true);

            List<string> AddressList = new List<string>();

            Log("Libretas encontradas: " + NameSpace.AddressLists.Count);
            Log("Recorriendo libretas");

            try
            {
                foreach (AddressList list in NameSpace.AddressLists)
                    AddressList.Add(list.Name);
            }
            catch (System.Exception ex)
            {
                Log("Exception: " + ex.ToString());
                ZClass.raiseerror(ex);
            }
            
            Log("Libretas agregadas a la lista");

            return AddressList;
        }

        private void Log(string mensaje)
        {
            Trace.WriteLineIf(ZTrace.IsVerbose, DateTime.Now.ToString("hh:mm:ss.fff") + ": " + mensaje);
        }
    }
}