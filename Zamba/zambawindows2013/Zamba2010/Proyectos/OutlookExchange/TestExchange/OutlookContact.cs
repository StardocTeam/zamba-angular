using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestExchange
{
    [Serializable]
    public class OutlookContact
    {
        public enum AddressTypes
        {
            SMTP, 
            EXCHANGE           
        }

        public enum ContactTypes
        {
            SINGLE,
            DISTRIBUTION_LIST
        }

        private String _fullName = null;
        private String _emailAddress = null;
        private ContactTypes _contactType;
        private AddressTypes _addressType;
        private bool _resolved = false;
        private List<OutlookContact> _members = null;

        public String EmailAddress
        {
            set { _emailAddress = value; }
            get { return _emailAddress; }
        }

        public string FullName
        {
            set { _fullName = value; }
            get { return _fullName; }
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

        public ContactTypes ContactType
        {
            set { _contactType = value; }
            get { return _contactType; }
        }

        public AddressTypes AddressType
        {
            set 
            { 
                _addressType = value;

                if (_addressType == AddressTypes.SMTP)
                    _resolved = true;
            }
            get { return _addressType; }
        }

        public OutlookContact(String fullName, String emailAddress, ContactTypes type, AddressTypes addType)
        {
            _fullName = fullName;
            _emailAddress = emailAddress;
            _contactType = type;

            _members = new List<OutlookContact>();

            if (addType == AddressTypes.SMTP)
                _resolved = true;
        }
    }
}
