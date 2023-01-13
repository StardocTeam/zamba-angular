using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Outlook;
using System.Diagnostics;
using Zamba.Core;
using System.Configuration;
using System.Runtime.InteropServices;

namespace Zamba.Outlook.GAL
{
    public class Outlook
    {
        private const String MAPI_NAMESPACE = "MAPI";        

        private Application _appOutlook = null;
        private bool _debug = false;

        public Outlook()
        {
            newApp();
        }

        private void newApp()
        {
            if (_appOutlook == null)
            {
                _appOutlook = new Application();

                bool.TryParse(ConfigurationManager.AppSettings["debug"].ToString(), out _debug);

                Log("Outlook version: " + _appOutlook.Version.ToString());
            }
        }

        // devuelve una lista de contactos pertenecientes a la libreta especificada
        public List<OutlookContact> GetAdressBookContacts(string AddressBook)
        {
            long max_contactos = 0, i = 0;

            long.TryParse(ConfigurationManager.AppSettings["max_contactos"], out max_contactos);

            OutlookContact CurrentContact = null;
            
            List<OutlookContact> ContactList = new List<OutlookContact>();

            // cuando intente leer la libreta de exchange va a fallar
            // si outlook esta trabajando offline
            try
            {
                if (_debug)
                    Log("OutlookApplication.GetNamespace(MAPI_NAMESPACE)");

                _NameSpace NameSpace = _appOutlook.GetNamespace(MAPI_NAMESPACE);

                NameSpace.Logon(null, null, true, true);

                AddressList list = NameSpace.AddressLists[AddressBook];

                if (_debug)
                    Log("  Recorriendo contactos de la libreta");

                foreach (AddressEntry entry in list.AddressEntries)
                {
                    CurrentContact = CreateContact(entry);

                    if (CurrentContact != null)
                        ContactList.Add(CurrentContact);

                    // cortar si se llega al maximo (y si lo hay)
                    if (max_contactos > 0)
                    {
                        i++;

                        if (i == max_contactos)
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

            try
            {
                // usuario normal
                if (entry.DisplayType == OlDisplayType.olUser)
                {
                    if (entry.Address != null)
                    {
                        if (entry.Type.ToString() == "EX")
                            contact = new OutlookContact(entry.Name, entry.Address, OutlookContact.olContactTypes.SINGLE, OutlookContact.olAddressTypes.EXCHANGE);
                        else
                            contact = new OutlookContact(entry.Name, entry.Address, OutlookContact.olContactTypes.SINGLE, OutlookContact.olAddressTypes.SMTP);
                    }
                }

                // lista de distribucion
                if (entry.DisplayType == OlDisplayType.olDistList || entry.DisplayType == OlDisplayType.olPrivateDistList)
                {
                    if (entry.Type.ToString() == "EX")
                        contact = new OutlookContact(entry.Name, entry.Address, OutlookContact.olContactTypes.DISTRIBUTION_LIST, OutlookContact.olAddressTypes.EXCHANGE);
                    else
                    {
                        string addresses = string.Empty;

                        foreach (AddressEntry member in entry.Members)
                            addresses = addresses + member.Address.Trim() + ";";

                        addresses = addresses.Remove(addresses.Length - 1, 1);

                        contact = new OutlookContact(entry.Name, addresses, OutlookContact.olContactTypes.DISTRIBUTION_LIST, OutlookContact.olAddressTypes.SMTP);                        
                    }
                }
            }
            catch (System.Exception ex)
            {
                Log("Exception: " + ex.ToString());
                ZClass.raiseerror(ex);
            }

            return contact;
        }

        // transforma la direccion de email de formato exchange a smtp
        public string ResolveExchangeAddress(OutlookContact contact)
        {
            string email = string.Empty;
            
            if (!contact.Resolved)
                email = GetSMTPAddress(contact.EmailAddress);

            return email;
        }

        // metodo interno para convertir la direccion de exchange a smtp
        private string GetSMTPAddress(string strAddress)
        {
            string strRet = string.Empty;

            try
            {
                if (_appOutlook == null)
                    newApp();

                if (_debug)
                    Log("    GetSMTPAddress()");

                //    //IF OUTLOOK VERSION IS >= 2007 THEN USES NATIVE OOM PROPERTIES AND METHODS             
                //    if (int.Parse((_appOutlook.Version.ToString().Substring(0, 2))) >= 12)
                //    {
                //        Recipient oRec;

                //        if (_debug)
                //            Log("    OutlookApplication.Session.CreateRecipient(" + strAddress + ")");
                        
                //        oRec = _appOutlook.Session.CreateRecipient(strAddress);

                //        if (_debug)
                //            Log("    oRec.Resolve()");

                //        if (oRec.Resolve())
                //        {
                //            Microsoft.Office.Interop.Outlook.ExchangeUser oEU;
                //            Microsoft.Office.Interop.Outlook.ExchangeDistributionList oEDL;

                //            switch (oRec.AddressEntry.AddressEntryUserType)
                //            {
                //                case Microsoft.Office.Interop.Outlook.OlAddressEntryUserType.olExchangeUserAddressEntry:

                //                    if (_debug)
                //                        Log("    oRec.AddressEntry.GetExchangeUser()");

                //                    oEU = oRec.AddressEntry.GetExchangeUser();
                                    
                //                    if (oEU != null)
                //                        strRet = oEU.PrimarySmtpAddress;
                                    
                //                    break;

                //                case Microsoft.Office.Interop.Outlook.OlAddressEntryUserType.olExchangeDistributionListAddressEntry:

                //                    if (_debug)
                //                        Log("    oRec.AddressEntry.GetExchangeDistributionList(");

                //                    oEDL = oRec.AddressEntry.GetExchangeDistributionList();
                                    
                //                    if (oEDL != null)
                //                        strRet = oEDL.PrimarySmtpAddress;

                //                    break;
                //            }
                //        }
                //        else
                //            if (_debug)
                //                Log("    Direccion NO resuelta");
                //    }

                // si no puede usar los objetos de O2007 lo hace de la forma complicada
                if (strRet == string.Empty)
                {
                    ContactItem oCon;
                    string strKey;

                    if (_debug)
                        Log("    CreateItem(OlItemType.olContactItem)");                    

                    oCon = (ContactItem)_appOutlook.CreateItem(OlItemType.olContactItem);
                    oCon.Email1Address = strAddress;

                    strKey = "_" + DateTime.Now.ToString("hh:mm:ss.fff").Replace(".", "").Replace(":", "");
                    oCon.FullName = strKey;

                    if (_debug)
                    {
                        Log("    oCon.FullName = " + oCon.FullName);
                        Log("    oCon.Save()");
                    }

                    oCon.Save();

                    strRet = oCon.Email1DisplayName.Replace("(", "").Replace(")", "").Replace(strKey, "").Trim();

                    if (_debug)
                    {
                        Log("    strRet = " + strRet);
                        Log("    oCon.Delete()");
                    }

                    oCon.Delete();
                    oCon = null;

                    oCon = (ContactItem)_appOutlook.Session.GetDefaultFolder(OlDefaultFolders.olFolderDeletedItems).Items.Find("[Subject]=" + strKey);

                    if (oCon != null)
                        oCon.Delete();
                }
            }
            catch(System.Exception ex)
            {
                strRet = "-";
                Log("Exception: " + ex.ToString() + " - " + ex.Source + " - " + ex.StackTrace);
                ZClass.raiseerror(ex);
            }

            return strRet;
        }

        // devuelve una lista con los nombres de las libretas de direcciones disponibles
        public List<string> GetAddressLists()
        {
            List<string> AddressList = new List<string>();

            try
            {
                if (_debug)
                    Log("OutlookApplication.GetNamespace(MAPI_NAMESPACE)");

                _NameSpace NameSpace = _appOutlook.GetNamespace(MAPI_NAMESPACE);

                NameSpace.Logon(null, null, true, true);

                if (_debug)
                {
                    Log("Libretas encontradas: " + NameSpace.AddressLists.Count);
                    Log("Recorriendo libretas");
                }

                foreach (AddressList list in NameSpace.AddressLists)
                    AddressList.Add(list.Name);
            }
            catch (System.Exception ex)
            {
                Log("Exception: " + ex.ToString());
                ZClass.raiseerror(ex);
            }

            if (_debug)
                Log("Libretas agregadas a la lista");

            return AddressList;
        }

        private void Log(string mensaje)
        {
            Trace.WriteLineIf(ZTrace.IsVerbose, DateTime.Now.ToString("hh:mm:ss") + ": " + mensaje);
        }

        public void Dispose()
        {            
            Marshal.ReleaseComObject(_appOutlook);

            if(_appOutlook != null)
                _appOutlook = null;
        }
    }
}