using System;

namespace Zamba.Outlook.Interfaces
{
    interface IAccount
    {
        void Create(string name);
        void Delete(string name);
    }
}
