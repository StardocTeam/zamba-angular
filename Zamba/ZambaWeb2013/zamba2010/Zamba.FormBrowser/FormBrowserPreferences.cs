namespace Zamba.FormBrowser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zamba.Core;

    public class FormBrowserPreferences
    {            
        bool? _useVersionNumber = null;
        bool? _useParentId = null;      
        bool? _useDocId = null;
        bool? _useDoctypeId = null;
        bool? _useDocPath = null;
        bool? _useOriginalName = null;


        UserPreferences UP = new UserPreferences();

        public bool UseVersionNumber
        {
            get
            {
                if (!_useVersionNumber.HasValue)
                {
                    _useVersionNumber = bool.Parse(UP.getValue("NumerodeVersion", UPSections.FormPreferences, "True"));
                }
                return _useVersionNumber.Value;
            }
            set
            {
                _useVersionNumber = value;
            }
        }

        public bool UseParentId
        {
            get
            {
                if (!_useParentId.HasValue)
                {
                    _useParentId = bool.Parse(UP.getValue("ParentId", UPSections.FormPreferences, "True"));
                }
                return _useParentId.Value;
            }
            set
            {
                _useParentId = value;
            }
        }

      
        public bool UseDocId
        {
            get
            {
                if (!_useDocId.HasValue)
                {
                    _useDocId = bool.Parse(UP.getValue("ResultId", UPSections.FormPreferences, "True"));
                }
                return _useDocId.Value;
            }
            set
            {
                _useDocId = value;
            }
        }

        public bool UseDoctypeId
        {
            get
            {
                if (!_useDoctypeId.HasValue)
                {
                    _useDoctypeId = bool.Parse(UP.getValue("DoctypeId", UPSections.FormPreferences, "True"));
                }
                return _useDoctypeId.Value;
            }
            set
            {
                _useDoctypeId = value;
            }
        }

        public bool UseDocPath
        {
            get
            {
                if (!_useDocPath.HasValue)
                {
                    _useDocPath = bool.Parse(UP.getValue("RutaDocumento", UPSections.FormPreferences, "True"));
                }
                return _useDocPath.Value;
            }
            set
            {
                _useDocPath = value;
            }
        }

        public bool UseOrignalName
        {
            get
            {
                if (!_useOriginalName.HasValue)
                {
                    _useOriginalName = bool.Parse(UP.getValue("NombreOriginal", UPSections.FormPreferences, "True"));
                }
                return _useOriginalName.Value;
            }
            set
            {
                _useOriginalName = value;
            }
        }
    }
}
