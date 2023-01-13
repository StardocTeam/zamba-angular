using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stardoc.HtmlEditor.Scripts
{
    public sealed class Calendar
        : BaseValidation
    {
        private const string CALENDAR_ATTRIBUTE = "onfocus=\"showCalendarControl(this)\"";
        public override string ToString()
        {
            return CALENDAR_ATTRIBUTE;
        }
    }
}
