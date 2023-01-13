using System;
using System.Collections.Generic;
using Zamba.Outlook.Enumerators;

namespace Zamba.Outlook.Interfaces
{
    public interface IAppointment
        : IDisposable
    {
        String Body { set; }

        DateTime End { set; }
        string Location { set; }
        AppointmentPriority Priority { set; }
        DateTime Start { set; }
        String Subject { set; }
        List<String> ToAddresses { get; }

        void Send();
        List<IAppointment> GetAppointments(AppointmentTimeSpam timeSpam);
    }
}
