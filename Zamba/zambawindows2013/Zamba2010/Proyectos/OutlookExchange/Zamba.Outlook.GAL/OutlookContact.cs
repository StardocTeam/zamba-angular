using System;
using System.Collections.Generic;

namespace Zamba.Outlook.GAL
{
    [Serializable]
    public class OutlookContact
    {
        public enum olAddressTypes
        {
            SMTP, 
            EXCHANGE           
        }

        public enum olContactTypes
        {
            SINGLE,
            DISTRIBUTION_LIST
        }

        private String _fullName = null;
        private String _emailAddress = null;
        private String _addressBookName = null;
        private olContactTypes _contactType;
        private olAddressTypes _addressType;
        private bool _resolved = false;
        private List<OutlookContact> _members = null;

        public String EmailAddress
        {
            set { _emailAddress = value.Replace("'", "''"); }
            get { return _emailAddress; }
        }

        public string FullName
        {
            set { _fullName = value.Replace("'", "''"); }
            get { return _fullName; }
        }

        public string AddressBookName
        {
            set { _addressBookName = value.Replace("'", "''"); }
            get { return _addressBookName; }
        }

        public bool Resolved
        {
            set { _resolved = value; }
            get { return _resolved; }
        }

        public List<OutlookContact> Members
        {
            set { _members = value; }
            get { return _members; }
        }

        public olContactTypes ContactType
        {
            set { _contactType = value; }
            get { return _contactType; }
        }

        public olAddressTypes AddressType
        {
            set 
            { 
                _addressType = value;

                if (_addressType == olAddressTypes.SMTP)
                    _resolved = true;
            }
            get { return _addressType; }
        }

        public OutlookContact() { }

        public OutlookContact(String fullName, String emailAddress, olContactTypes type, olAddressTypes addType)
        {
            _fullName = fullName.Replace("'", "''");
            _emailAddress = emailAddress.Replace("'", "''");

            _contactType = type;

            _members = new List<OutlookContact>();

            _addressType = addType;

            if (addType == olAddressTypes.SMTP)
                _resolved = true;
        }
    }
}