using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Outlook;

namespace Zamba.Outlook
{
    /// <summary>
    /// Represents and Outlook Contact
    /// </summary>
    public sealed class OutlookContact
        : IDisposable
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
        public List<String> EmailAddress
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

        #region Get
        /// <summary>
        /// Returns every Contact 
        /// </summary>
        /// <returns></returns>
        public static List<OutlookContact> GetContacts()
        {
            Application OutlookApplication = GetOutlook();

            _NameSpace NameSpace = OutlookApplication.GetNamespace(MAPI_NAMESPACE);

            NameSpace.Logon(null, null, true, true);
            MAPIFolder ContactsFolder = NameSpace.GetDefaultFolder(OlDefaultFolders.olFolderContacts);

            List<OutlookContact> ContactList = new List<OutlookContact>(ContactsFolder.Items.Count);

            ContactItem CurrentContactItem = null;
            OutlookContact CurrentContact = null;

            foreach (Object CurrentItem in ContactsFolder.Items)
            {
                if (CurrentItem is ContactItem)
                {
                    CurrentContactItem = (ContactItem)CurrentItem;
                    CurrentContact = new OutlookContact(CurrentContactItem.FullName, CurrentContactItem.Email1Address);
                    ContactList.Add(CurrentContact);
                }
            }

            CurrentContactItem = null;
            CurrentContact = null;

            return ContactList;
        }

        /// <summary>
        /// Returns a list of Contacts that has a specific email account
        /// </summary>
        /// <param name="email">The email to search for</param>
        /// <returns></returns>
        public static List<OutlookContact> GetContacts(String email)
        {
            Application OutlookApplication = GetOutlook();

            _NameSpace NameSpace = OutlookApplication.GetNamespace(MAPI_NAMESPACE);

            NameSpace.Logon(null, null, true, true);
            MAPIFolder ContactsFolder = NameSpace.GetDefaultFolder(OlDefaultFolders.olFolderContacts);

            List<OutlookContact> ContactList = new List<OutlookContact>(ContactsFolder.Items.Count);

            ContactItem CurrentContactItem = null;
            OutlookContact CurrentContact = null;

            foreach (Object CurrentItem in ContactsFolder.Items)
            {
                if (CurrentItem is ContactItem)
                {
                    CurrentContactItem = (ContactItem)CurrentItem;

                    if (String.Compare(CurrentContactItem.Email1Address, email) == 0 ||
                        String.Compare(CurrentContactItem.Email2Address, email) == 0 ||
                        String.Compare(CurrentContactItem.Email3Address, email) == 0)
                    {
                        CurrentContact = new OutlookContact(CurrentContactItem.FullName, CurrentContactItem.Email1Address,
                            CurrentContactItem.Email2Address, CurrentContactItem.Email3Address);

                        ContactList.Add(CurrentContact);
                    }
                }
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
            Application OutlookApplication = GetOutlook();

            _NameSpace NameSpace = OutlookApplication.GetNamespace(MAPI_NAMESPACE);

            NameSpace.Logon(null, null, true, true);
            MAPIFolder ContactsFolder = NameSpace.GetDefaultFolder(OlDefaultFolders.olFolderContacts);

            List<OutlookContact> ContactList = new List<OutlookContact>(ContactsFolder.Items.Count);

            ContactItem CurrentContactItem = null;
            OutlookContact CurrentContact = null;

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

            CurrentContactItem = null;
            CurrentContact = null;

            return ContactList;
        } 
        #endregion

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
        private static Application GetOutlook()
        {
            //TODO: Validar si existe una sesion de Outlook y tomarla 
            return new Application();
        }
    }
}
