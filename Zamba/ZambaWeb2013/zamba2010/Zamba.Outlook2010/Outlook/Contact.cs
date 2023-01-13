using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Outlook;
using Zamba.Outlook.Interfaces;
using System.Data;
using Zamba.Core;
using Zamba.Data;

namespace Zamba.Outlook.Outlook
{
    /// <summary>
    /// Represents and Outlook Contact
    /// </summary>


    public sealed class OutlookContact
        : IOutlookContact
    {
        #region Constantes
        private const String MAPI_NAMESPACE = "MAPI";
        private const Int32 EMAIL_ADDRESSES_COUNT = 3;
        #endregion

        #region Atributos
        private String _fullName = null;
        private List<String> _emailAddress = null;
        private Boolean _disposed = false;

        #endregion

        #region Propiedades
        public List<String> EmailAddresses
        {
            get
            {
                return _emailAddress;
            }
        }
        public string FullName
        {
            set
            {
                _fullName = value;
            }
            get
            {
                return _fullName;
            }
        }
        #endregion

        #region Constructores
        public OutlookContact(String fullName, String emailAddress)
        {
            _fullName = fullName;
            _emailAddress = new List<String>(EMAIL_ADDRESSES_COUNT);
            _emailAddress.Add(emailAddress);
        }
        public OutlookContact(String fullName, String emailAddress, String secondEmailAddress)
            : this(fullName, emailAddress)
        {
            _emailAddress.Add(secondEmailAddress);
        }
        public OutlookContact(String fullName, String emailAddress, String secondEmailAddress, String thirdEmailAddress)
            : this(fullName, emailAddress, secondEmailAddress)
        {
            _emailAddress.Add(thirdEmailAddress);
        }

        #endregion

        ///// <summary>
        ///// Returns every Contact 
        ///// </summary>
        ///// <returns></returns>
        //public static List<OutlookContact> GetContacts()
        //{
        //    Application OutlookApplication = GetOutlook();

        //    _NameSpace NameSpace = OutlookApplication.GetNamespace(MAPI_NAMESPACE);

        //    NameSpace.Logon(null, null, true, true);
        //    MAPIFolder ContactsFolder = NameSpace.GetDefaultFolder(OlDefaultFolders.olFolderContacts);

        //    List<OutlookContact> ContactList = new List<OutlookContact>(ContactsFolder.Items.Count);

        //    ContactItem CurrentContactItem = null;
        //    OutlookContact CurrentContact = null;

        //    foreach (Object CurrentItem in ContactsFolder.Items)
        //    {
        //        if (CurrentItem is ContactItem)
        //        {
        //            CurrentContactItem = (ContactItem)CurrentItem;
        //            CurrentContact = new OutlookContact(CurrentContactItem.FullName, CurrentContactItem.Email1Address,CurrentContactItem.Email2Address ,CurrentContactItem.Email3Address);
        //            ContactList.Add(CurrentContact);
        //        }
        //    }

        //    CurrentContactItem = null;
        //    CurrentContact = null;

        //    return ContactList;
        //}

        /// <summary>
        /// Returns every Contact 
        /// [Alejandro] 23-11-09 updated
        /// </summary>
        /// <returns></returns>
        public static List<OutlookContact> GetContacts()
        {
            Application OutlookApplication = Office.Outlook.SharedOutlook.GetOutlookApp();

            List<OutlookContact> ContactList = new List<OutlookContact>();

            // cuando intente leer la libreta de exchange va a fallar
            // si outlook esta trabajando offline
            try
            {
                _NameSpace NameSpace = OutlookApplication.GetNamespace(MAPI_NAMESPACE);

                NameSpace.Logon(null, null, true, true);

                foreach (AddressList list in NameSpace.AddressLists)
                {
                    foreach (AddressEntry entry in list.AddressEntries)
                    {
                        if (entry.Address != null)
                        {
                            OutlookContact CurrentContact = null;

                            CurrentContact = new OutlookContact(entry.Name, entry.Address);
                            ContactList.Add(CurrentContact);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            return ContactList;
        }

        /// <summary>
        /// Returns every Contact in an AddressBook
        /// [Alejandro] 23-11-09 created
        /// </summary>
        /// <returns></returns>
        public static List<OutlookContact> GetAdressBookContacts(string AddressBook)
        {
            string Address;

            Application OutlookApplication = Office.Outlook.SharedOutlook.GetOutlookApp();

            List<OutlookContact> ContactList = new List<OutlookContact>();

            // cuando intente leer la libreta de exchange va a fallar
            // si outlook esta trabajando offline
            try
            {
                _NameSpace NameSpace = OutlookApplication.GetNamespace(MAPI_NAMESPACE);

                NameSpace.Logon(null, null, true, true);

                // libretas guardadas en la base
                List<string> DBAddressBooks = GAL_Factory.GetAddressBooks();

                // si la libreta esta en la base leer los contactos desde ahi
                if (DBAddressBooks.Contains(AddressBook))
                {
                    DataSet ds = GAL_Factory.GetAddressBookContacts(AddressBook);

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        if (row["Email"] != null)
                        {
                            OutlookContact CurrentContact = null;
                            CurrentContact = new OutlookContact(row["FullName"].ToString(), row["Email"].ToString());
                            ContactList.Add(CurrentContact);
                        }
                    }
                }
                else
                {
                    // obtener la libreta desde OL
                    AddressList list = NameSpace.AddressLists[AddressBook];

                    // leer contactos
                    foreach (AddressEntry entry in list.AddressEntries)
                    {
                        if (entry.Address != null)
                        {
                            OutlookContact CurrentContact = null;

                            if (entry.Type == "EX" && entry.Address.IndexOf("@") == -1)
                                Address = GetSMTPAddress(entry.Address);
                            else
                                Address = entry.Address;

                            CurrentContact = new OutlookContact(entry.Name, Address);
                            ContactList.Add(CurrentContact);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            return ContactList;
        }

        private static string GetSMTPAddress(string strAddress)
        {
            ContactItem oCon;
            string strKey;
            string strRet = string.Empty;

            Application OutlookApplication = Office.Outlook.SharedOutlook.GetOutlookApp();

            try
            {
                if (strRet == string.Empty)
                {
                    oCon = (ContactItem)OutlookApplication.CreateItem(OlItemType.olContactItem);
                    oCon.Email1Address = strAddress;

                    strKey = "_" + (new Random().Next() * 100000 + DateTime.Now.ToShortDateString());
                    strKey = strKey.Replace(".", "").Replace("/", "");
                    oCon.FullName = strKey;

                    oCon.Save();

                    strRet = oCon.Email1DisplayName.Replace("(", "").Replace(")", "").Replace(strKey, "").Trim();

                    oCon.Delete();
                    oCon = null;

                    oCon = (ContactItem)OutlookApplication.Session.GetDefaultFolder(OlDefaultFolders.olFolderDeletedItems).Items.Find("[Subject]=" + strKey);

                    if (oCon != null)
                        oCon.Delete();
                }
            }
            catch (System.Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            return strRet;
        }

        /// <summary>
        /// Returns every Address Book
        /// [Alejandro] 23-11-09 created
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAddressLists()
        {
            List<string> AddressList = new List<string>();

            Application OutlookApplication = Office.Outlook.SharedOutlook.GetOutlookApp();

            try
            {
                _NameSpace NameSpace = OutlookApplication.GetNamespace(MAPI_NAMESPACE);

                NameSpace.Logon(null, null, true, true);

                // lista de libretas guardadas en la base que deben mostrarse
                // si la libreta no esta en esta lista es ignorada
                List<string> DBAddressBooks = GAL_Factory.GetOutlookAddressBooksToShow();

                foreach (string libretaDB in DBAddressBooks)
                {
                    foreach(AddressList lib in NameSpace.AddressLists)
                    {
                        if(lib.Name.ToLower().Trim() == libretaDB.ToLower().Trim())
                        {
                            AddressList.Add(libretaDB);
                        }
                    }
                }

                //foreach (AddressList list in NameSpace.AddressLists)
                //    AddressList.Add(list.Name);
            }
            catch (System.Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            return AddressList;
        }

        /// <summary>
        /// Returns a list of Contacts that has a specific email account
        /// </summary>
        /// <param name="email">The email to search for</param>
        /// <returns></returns>
        public static List<OutlookContact> GetContacts(String email)
        {
            Application OutlookApplication = Office.Outlook.SharedOutlook.GetOutlookApp();

            List<OutlookContact> ContactList = null;
            ContactItem CurrentContactItem = null;
            OutlookContact CurrentContact = null;

            try
            {
                _NameSpace NameSpace = OutlookApplication.GetNamespace(MAPI_NAMESPACE);

                NameSpace.Logon(null, null, true, true);
                MAPIFolder ContactsFolder = NameSpace.GetDefaultFolder(OlDefaultFolders.olFolderContacts);

                ContactList = new List<OutlookContact>(ContactsFolder.Items.Count);

                foreach (Object CurrentItem in ContactsFolder.Items)
                {
                    if (CurrentItem is ContactItem)
                    {
                        CurrentContactItem = (ContactItem)CurrentItem;

                        if (String.Compare(CurrentContactItem.Email1Address, email) == 0 ||
                            String.Compare(CurrentContactItem.Email2Address, email) == 0 ||
                            String.Compare(CurrentContactItem.Email3Address, email) == 0)
                        {
                            CurrentContact = new OutlookContact(CurrentContactItem.FullName, CurrentContactItem.Email1Address, CurrentContactItem.Email2Address, CurrentContactItem.Email3Address);
                            ContactList.Add(CurrentContact);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            CurrentContactItem = null;
            CurrentContact = null;

            return ContactList;
        }

        /// <summary>
        /// Returns a list of Contacts that has a specific full Name
        /// </summary>
        /// <param name="fullName">The full Name to search for</param>
        /// <returns></returns>
        public static List<OutlookContact> GetContactsByName(String fullName)
        {
            Application OutlookApplication = Office.Outlook.SharedOutlook.GetOutlookApp();

            List<OutlookContact> ContactList = null;
            ContactItem CurrentContactItem = null;
            OutlookContact CurrentContact = null;

            try
            {
                _NameSpace NameSpace = OutlookApplication.GetNamespace(MAPI_NAMESPACE);

                NameSpace.Logon(null, null, true, true);
                MAPIFolder ContactsFolder = NameSpace.GetDefaultFolder(OlDefaultFolders.olFolderContacts);

                ContactList = new List<OutlookContact>(ContactsFolder.Items.Count);

                foreach (Object CurrentItem in ContactsFolder.Items)
                {
                    if (CurrentItem is ContactItem)
                    {
                        CurrentContactItem = (ContactItem)CurrentItem;

                        if (String.Compare(CurrentContactItem.FullName, fullName) == 0)
                        {
                            CurrentContact = new OutlookContact(CurrentContactItem.FullName, CurrentContactItem.Email1Address,
                                CurrentContactItem.Email2Address, CurrentContactItem.Email3Address);

                            ContactList.Add(CurrentContact);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            CurrentContactItem = null;
            CurrentContact = null;

            return ContactList;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _fullName = null;
                _emailAddress = null;
            }
            _disposed = true;
        }

        /// <summary>
        /// Returns an Instance of the Outlook Application
        /// </summary>
        /// <returns></returns>
        //private static Application GetOutlook()
        //{
        //    //TODO: Validar si existe una sesion de Outlook y tomarla 
        //    return new Application();
        //}
    }
}
