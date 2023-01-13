using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stardoc.HtmlEditor.Scripts
{
    public sealed class RequiredField
        : BaseValidation
    {
        private const String REQUIRED_ATTRIBUTE = "required=\"true\"";

        private String _requiredMessage = null;
        public RequiredField(String requiredMessage)
        {
            _requiredMessage = requiredMessage;
        }
        public String ParsedRequiredMessage
        {
            get
            {
                StringBuilder ParsedValueBuilder = new StringBuilder();
                ParsedValueBuilder.Append("<strong style=\"display: none;\">");
                ParsedValueBuilder.Append(_requiredMessage);
                ParsedValueBuilder.Append("</strong>");

                return ParsedValueBuilder.ToString();
            }
        }

        public override string ToString()
        {
            return REQUIRED_ATTRIBUTE;
        }
    }
}
