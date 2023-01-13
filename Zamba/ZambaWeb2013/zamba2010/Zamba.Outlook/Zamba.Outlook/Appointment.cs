using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Outlook;
using Zamba.Outlook;

namespace Zamba.Outlook
{
    /// <summary>
    /// Represents an Outlook Appointment
    /// </summary>
    public sealed class Appointment
        :IDisposable 
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
            set { _subject = value; }
        }
        public DateTime Start
        {
            set { _start = value; }
        }
        public DateTime End
        {
            set { _end = value; }
        }
        public String Body
        {
            set { _body = value; }
        }
        public String Location
        {
            set { _location = value; }
        }
        public AppointmentPriority Priority
        {
            set { _priority = value; }
        }

        #endregion

        #region Constructores
        public Appointment(String body, List<String> toAddresses, String subject, DateTime start, DateTime end, String location, AppointmentPriority priority)
        {
            _body = body;
            _toAddresses = toAddresses;
            _subject = subject;
            _start = start;
            _end = end;
            _location = location;
            _priority = priority;
        }
        #endregion

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
                Application OutlookApplication = GetOutlook();

                _NameSpace NameSpace = OutlookApplication.GetNamespace(MAPI_NAMESPACE);

                NameSpace.Logon(null, null, true, true);
                MAPIFolder ContactsFolder = NameSpace.GetDefaultFolder(OlDefaultFolders.olFolderContacts);

                AppointmentItem Appointment = (AppointmentItem)OutlookApplication.CreateItem(OlItemType.olAppointmentItem);
                Appointment.Body = _body;
                Appointment.Start = _start;
                Appointment.End = _end;
                Appointment.Subject = _subject;
                Appointment.Location = _location;

                switch (_priority)
                {
                    case AppointmentPriority.Low:
                        Appointment.Importance = OlImportance.olImportanceLow;
                        break;
                    case AppointmentPriority.Medium:
                        Appointment.Importance = OlImportance.olImportanceNormal;
                        break;
                    case AppointmentPriority.High:
                        Appointment.Importance = OlImportance.olImportanceHigh;
                        break;
                }

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

        /// <summary>
        /// Returns an Instance of the Outlook Application
        /// </summary>
        /// <returns></returns>
        private static Application GetOutlook()
        {
            //TODO: Validar si existe una sesion de Outlook y tomarla 
            return new Application();
        }

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
