using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Outlook;
using Zamba.Outlook.Interfaces;
using Zamba.Outlook.Enumerators;

namespace Zamba.Outlook.Outlook
{
    /// <summary>
    /// Represents an Outlook Appointment
    /// </summary>
    public sealed class Appointment
        :  IAppointment
    {
        #region Constantes
        private const String MAPI_NAMESPACE = "MAPI";
        private const String ADDRESSES_SEPARATOR = ";";
        private const Int32 ERRORS_TO_VALIDATE = 1;
        #endregion

        #region Atributos
        private List<String> _toAddresses = new List<String>();
        private String _subject = String.Empty;
        private DateTime _start;
        private DateTime _end;
        private String _body = String.Empty;
        private String _location = String.Empty;
        private AppointmentPriority _priority = AppointmentPriority.Low;
        private Boolean _disposed = false;
        #endregion

        #region Propiedades
        public List<String> ToAddresses
        {
            get { return _toAddresses; }
        }
        public String Subject
        {
            set
            {
                if (null != value)
                    _subject = value;
                else
                    _subject = String.Empty;
            }
            get { return _subject; }
        }
        public DateTime Start
        {
            set { _start = value; }
            get { return _start; }
        }
        public DateTime End
        {
            set { _end = value; }
            get { return _end; }
        }
        public String Body
        {
            set { _body = value; }
            get { return _body; }
        }
        public String Location
        {
            set
            {

                if (null != value)
                    _location = value;
                else
                    _location = String.Empty;
            }
            get { return _location; }
        }
        public AppointmentPriority Priority
        {
            set { _priority = value; }
            get { return _priority; }
        }

        #endregion

        #region Constructores


        private Appointment(String body, List<String> toAddresses, String subject, DateTime start, DateTime end, String location, OlImportance priority)
            : this(body, toAddresses, subject, start, end, location)
        {
            _priority = GetPriority(priority);
        }

        public Appointment(String body, List<String> toAddresses, String subject, DateTime start, DateTime end, String location)
        {
            _body = body;
            _toAddresses = toAddresses;
            _subject = subject;
            _start = start;
            _end = end;
            _location = location;
        }
        public Appointment(String body, List<String> toAddresses, String subject, DateTime start, DateTime end, String location, AppointmentPriority priority)
            : this(body, toAddresses, subject, start, end, location)
        {
            _priority = priority;
        }
        #endregion

        #region Send
        /// <summary>
        /// Validates wheather the Appointment has valid Data
        /// </summary>
        /// <returns></returns>
        private Boolean IsValid(List<String> errors)
        {
            Boolean IsValid = true;

            if (null == errors)
                errors = new List<String>(ERRORS_TO_VALIDATE);

            if (DateTime.Compare(_start, _end) > 0)
            {
                errors.Add("La fecha de comienzo de la cita es posterior a la fecha de finalización.");
                IsValid = false;
            }

            return IsValid;
        }

        /// <summary>
        /// Sends the Appointment 
        /// </summary>
        public void Send()
        {
            List<String> Errors = new List<String>();

            if (IsValid(Errors))
            {
                Application OutlookApplication = Office.Outlook.SharedOutlook.GetOutlookApp();

                _NameSpace NameSpace = OutlookApplication.GetNamespace(MAPI_NAMESPACE);

                NameSpace.Logon(null, null, true, true);
                MAPIFolder ContactsFolder = NameSpace.GetDefaultFolder(OlDefaultFolders.olFolderContacts);

                AppointmentItem Appointment = (AppointmentItem)OutlookApplication.CreateItem(OlItemType.olAppointmentItem);
                Appointment.Body = _body;
                Appointment.Start = _start;
                Appointment.End = _end;
                Appointment.Subject = _subject;
                Appointment.Location = _location;

                Appointment.Importance = GetPriority(_priority);

                foreach (String ToAdress in _toAddresses)
                    Appointment.Recipients.Add(ToAdress);

                Appointment.ReminderSet = true;

                Microsoft.Office.Interop.Outlook.MailItem mailItem = Appointment.ForwardAsVcal();

                StringBuilder AddressBuilder = new StringBuilder();
                foreach (String CurrentMailAddress in _toAddresses)
                {
                    AddressBuilder.Append(CurrentMailAddress);
                    AddressBuilder.Append(ADDRESSES_SEPARATOR);
                }

                AddressBuilder.Remove(AddressBuilder.Length - ADDRESSES_SEPARATOR.Length, ADDRESSES_SEPARATOR.Length);

                mailItem.To = AddressBuilder.ToString();
                AddressBuilder.Remove(0, AddressBuilder.Length);

                mailItem.Send();
            }
            else
            {

                if (null != Errors && Errors.Count > 0)
                {
                    StringBuilder ErrorBuilder = new StringBuilder();

                    foreach (String CurrentError in Errors)
                        ErrorBuilder.AppendLine(CurrentError);

                    String ErrorMessage = ErrorBuilder.ToString();

                    ErrorBuilder.Remove(0, ErrorBuilder.Length);
                    ErrorBuilder = null;

                    throw new System.Exception(ErrorMessage);
                }
            }
        }
        #endregion

        #region Priority
        private static OlImportance GetPriority(AppointmentPriority priority)
        {
            OlImportance ReturnValue;
            switch (priority)
            {
                case AppointmentPriority.Low:
                    ReturnValue = OlImportance.olImportanceLow;
                    break;
                case AppointmentPriority.Medium:
                    ReturnValue = OlImportance.olImportanceNormal;
                    break;
                case AppointmentPriority.High:
                    ReturnValue = OlImportance.olImportanceHigh;
                    break;
                default:
                    ReturnValue = OlImportance.olImportanceNormal;
                    break;
            }

            return ReturnValue;
        }
        private static AppointmentPriority GetPriority(OlImportance priority)
        {
            AppointmentPriority ReturnValue;

            switch (priority)
            {
                case OlImportance.olImportanceHigh:
                    ReturnValue = AppointmentPriority.High;
                    break;
                case OlImportance.olImportanceLow:
                    ReturnValue = AppointmentPriority.Low;
                    break;
                case OlImportance.olImportanceNormal:
                    ReturnValue = AppointmentPriority.Medium;
                    break;
                default:
                    ReturnValue = AppointmentPriority.Medium;
                    break;
            }

            return ReturnValue;
        }
        #endregion

        public List<IAppointment> GetAppointments(AppointmentTimeSpam timeSpam)
        {
            return null;
        }
        public List<IAppointment> GetAppointments( )
        {
            Application OutlookApplication = Office.Outlook.SharedOutlook.GetOutlookApp();

            _NameSpace NameSpace = OutlookApplication.GetNamespace(MAPI_NAMESPACE);

            NameSpace.Logon(null, null, true, true);
            MAPIFolder CalendarFolder = NameSpace.GetDefaultFolder(OlDefaultFolders.olFolderCalendar);
            if (null == CalendarFolder)
                return null;
            else if (CalendarFolder.Items.Count == 0)
                return new List<IAppointment>(0);

            List<IAppointment> Appointments = new List<IAppointment>(CalendarFolder.Items.Count);

            List<String> ToAddress = new List<String>();
            foreach (AppointmentItem CurrentItem in CalendarFolder.Items)
            {
                ToAddress.Clear();

                foreach (Recipient r in CurrentItem.Recipients)
                    ToAddress.Add(r.Address);

                Appointments.Add(new Appointment(CurrentItem.Body, ToAddress,
                    CurrentItem.Subject, CurrentItem.Start,
                    CurrentItem.End, CurrentItem.Location,
                    CurrentItem.Importance));
            }

            return Appointments;
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

        public void Dispose()
        {
            if (!_disposed)
            {
                if (null != _toAddresses)
                {
                    _toAddresses.Clear();
                    _toAddresses = null;
                }

                _subject = null;
                _subject = null;
                _body = null;
                _location = null;
            }
            _disposed = true;
        }
    }
}
