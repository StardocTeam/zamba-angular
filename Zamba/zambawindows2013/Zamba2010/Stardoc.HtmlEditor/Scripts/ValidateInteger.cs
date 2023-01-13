using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stardoc.HtmlEditor.Scripts
{
    public sealed class ValidateInteger
        : BaseValidation
    {
        private const String VALIDATE_INTEGET_ATTRIBUTE = "onfocus=\"SetValueOnFocus(this);\" onchange=\"ValidateInteger(this);\"\":\"onfocus=\"SetValueOnFocus(this);\" onchange=\"ValidateInteger(this);\"";

        public override string ToString()
        {
            return VALIDATE_INTEGET_ATTRIBUTE;
        }
    }
}
