using System;
using System.Collections.Generic;

namespace Zamba.Outlook.Interfaces
{
    public interface IOutlookContact
        :IDisposable
    {
        List<String> EmailAddresses { get; }
        string FullName { get; set; }
    }
}
