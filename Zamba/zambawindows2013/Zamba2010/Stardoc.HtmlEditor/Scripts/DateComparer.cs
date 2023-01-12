using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stardoc.HtmlEditor.Scripts
{
    public sealed class DateComparer
        : BaseValidation
    {
        private String _firstElementName;
        private String _triggeredElementName;
        private String _secondElementName;

        public String FirstElementName
        {
            get { return _firstElementName; }
            set { _firstElementName = value; }
        }
        public String SecondElementName
        {
            get { return _secondElementName; }
            set { _secondElementName = value; }
        }
        public String TriggeredElementName
        {
            get { return _triggeredElementName; }
            set { _triggeredElementName = value; }
        }

        public DateComparer(String firstElementName, String secondElementName, String triggeredName)
        {
            _firstElementName = firstElementName;
            _secondElementName = secondElementName;
            _triggeredElementName = triggeredName;
        }

        public override string ToString()
        {
            StringBuilder ParsedValueBuilder = new StringBuilder();
            ParsedValueBuilder.Append("onblur=\"ValidateDates('");
            ParsedValueBuilder.Append(_firstElementName);
            ParsedValueBuilder.Append("','");
            ParsedValueBuilder.Append(_secondElementName);
            ParsedValueBuilder.Append("','");
            ParsedValueBuilder.Append(_triggeredElementName);
            ParsedValueBuilder.Append("')");

            return base.ToString();
        }
    }
}
