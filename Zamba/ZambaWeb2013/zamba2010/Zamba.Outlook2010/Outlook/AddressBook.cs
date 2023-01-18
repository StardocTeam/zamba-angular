using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Outlook;
using Zamba.Outlook.Interfaces;

namespace Zamba.Outlook.Outlook
{
    /// <summary>
    /// Represents an Outlook Address Book
    /// </summary>
    public sealed class AddressBook
        : IAddressBook
    {
        #region Constantes
        private const String MAPI_NAMESPACE = "MAPI";
        #endregion

        #region Atributos
        private Boolean _disposed = false;
        private String _name;
        private List<IOutlookContact> _contacts;
        #endregion

        #region Propiedades
        public String Name
        {
            get { return _name; }
        }
        public List<IOutlookContact> Contacts
        {
            get { return _contacts; }
        }
        #endregion

        #region Constructores
        private AddressBook()
        {
            _contacts = new List<IOutlookContact>();
        }
        private AddressBook(String name)
            : this()
        {
            _name = name;
        }
        #endregion

        #region Get
        /// <summary>
        /// Returns an Address Book by its name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IAddressBook GetAddressBook(String name)
        {
            Application OutlookApplication = Office.Outlook.SharedOutlook.GetOutlookApp();

            _NameSpace NameSpace = OutlookApplication.GetNamespace(MAPI_NAMESPACE);

            NameSpace.Logon(null, null, true, true);

            if (null == NameSpace.AddressLists || NameSpace.AddressLists.Count == 0)
                return null;

            IAddressBook MyBook = null;

            foreach (AddressList CurrentBook in NameSpace.AddressLists)
            {
                if (String.Compare(name, CurrentBook.Name) == 0)
                {
                    MyBook = new AddressBook(CurrentBook.Name);

                    foreach (AddressEntry CurrentEntry in CurrentBook.AddressEntries)
                        MyBook.Contacts.Add(new OutlookContact(CurrentEntry.Name, CurrentEntry.Address));
                }
            }

            return MyBook;
        }
        /// <summary>
        /// Returns the List of Adress Books
        /// </summary>
        /// <returns></returns>
        public List<IAddressBook> GetAddressBooks()
        {
            Application OutlookApplication = Office.Outlook.SharedOutlook.GetOutlookApp();

            _NameSpace NameSpace = OutlookApplication.GetNamespace(MAPI_NAMESPACE);

            NameSpace.Logon(null, null, true, true);

            if (null == NameSpace.AddressLists)
                return null;
            else if (NameSpace.AddressLists.Count == 0)
                return new List<IAddressBook>(0);

            List<IAddressBook> MyBooks = new List<IAddressBook>(NameSpace.AddressLists.Count);

            IAddressBook MyBook = null;

            foreach (AddressList CurrentBook in NameSpace.AddressLists)
            {
                MyBook = new AddressBook(CurrentBook.Name);

                foreach (AddressEntry CurrentEntry in CurrentBook.AddressEntries)
                    MyBook.Contacts.Add(new OutlookContact(CurrentEntry.Name, CurrentEntry.Address));

                MyBooks.Add(MyBook);
            }

            return MyBooks;
        }
        #endregion

        /// <summary>
        /// Returns an Instance of the Outlook Application
        /// </summary>
        /// <returns></returns>
        //private static Application GetOutlook()
        //{
        //    //TODO: Validar si existe una sesion de Outlook y tomarla 
        //    return new Application();
        //}

        public void Dispose()
        {
            if (!_disposed)
            {
                _name = null;
                foreach (OutlookContact CurrentContact in _contacts)
                    CurrentContact.Dispose();

                _contacts.Clear();
                _contacts = null;
            }
            _disposed = true;
        }
    }
}
