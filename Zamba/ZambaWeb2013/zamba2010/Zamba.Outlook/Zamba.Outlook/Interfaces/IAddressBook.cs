using System;
using System.Collections.Generic;

namespace Zamba.Outlook.Interfaces
{
    public interface IAddressBook
        : IDisposable
    {
        List<IOutlookContact> Contacts { get; }
        string Name { get; }
        IAddressBook GetAddressBook(String name);
        List<IAddressBook> GetAddressBooks();
    }
}
