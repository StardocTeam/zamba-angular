namespace Zamba.FormBrowser.Razor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Text;
    using System.Collections;
    using Zamba.Core;
    using System.Web;
    using System.Net;

    public abstract class TemplateBase
    {
        [Browsable(false)]
        public StringBuilder Buffer { get; set; }
        
        [Browsable(false)]
        public StringWriter Writer { get; set; }

        public TemplateBase()
        {
            Buffer = new StringBuilder();
            Writer = new StringWriter(Buffer);
        }

        public abstract void Execute();

        public Hashtable DataSource { get; set; }

        // Writes the results of expressions like: "@foo.Bar"
        public virtual void Write(object value)
        {
            // For HTML output we'd probably want to HTMLEncode everything
            // But not for plain text templating
            WriteLiteral(value);
        }

        // Writes literals like markup: "<p>Foo</p>"
        public virtual void WriteLiteral(object value)
        {
            Buffer.Append(value);
        }

        /// <summary>
        /// WriteAttribute implementation lifted from ANurse's MicroRazor Implementation
        /// and the AspWebStack source.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="prefix"></param>
        /// <param name="suffix"></param>
        /// <param name="values"></param>
        public virtual void WriteAttribute(string name, PositionTagged<string> prefix,
                                           PositionTagged<string> suffix, params AttributeValue[] values)
        {
            bool first = true;
            bool wroteSomething = false;
            if (values.Length == 0)
            {
                // Explicitly empty attribute, so write the prefix and suffix
                WritePositionTaggedLiteral(prefix);
                WritePositionTaggedLiteral(suffix);
            }
            else
            {
                for (int i = 0; i < values.Length; i++)
                {
                    AttributeValue attrVal = values[i];
                    PositionTagged<object> val = attrVal.Value;
                    PositionTagged<string> next = i == values.Length - 1 ?
                        suffix : // End of the list, grab the suffix
                        values[i + 1].Prefix; // Still in the list, grab the next prefix

                    bool? boolVal = null;
                    if (val.Value is bool)
                    {
                        boolVal = (bool)val.Value;
                    }

                    if (val.Value != null && (boolVal == null || boolVal.Value))
                    {
                        string valStr = val.Value as string;
                        if (valStr == null)
                        {
                            valStr = val.Value.ToString();
                        }
                        if (boolVal != null)
                        {
                            valStr = name;
                        }

                        if (first)
                        {
                            WritePositionTaggedLiteral(prefix);
                            first = false;
                        }
                        else
                        {
                            WritePositionTaggedLiteral(attrVal.Prefix);
                        }

                        // Calculate length of the source span by the position of the next value (or suffix)
                        int sourceLength = next.Position - attrVal.Value.Position;

                        if (attrVal.Literal)
                        {
                            WriteLiteral(valStr);
                        }
                        else
                        {
                            Write(valStr); // Write value
                        }
                        wroteSomething = true;
                    }
                }
                if (wroteSomething)
                    WritePositionTaggedLiteral(suffix);
            }
        }

        private void WritePositionTaggedLiteral(string value, int position)
        {
            WriteLiteral(value);
        }

        private void WritePositionTaggedLiteral(PositionTagged<string> value)
        {
            WritePositionTaggedLiteral(value.Value, value.Position);
        }

        public virtual void WriteAttribute(string attr,
                                           Tuple<string, int> token1,
                                           Tuple<string, int> token2,
                                           Tuple<Tuple<string, int>,
                                           Tuple<object, int>, bool> token3)
        {
            object value = null;

            if (token3 != null)
                value = token3.Item2.Item1;
            else
                value = string.Empty;

            var output = token1.Item1 + value.ToString() + token2.Item1;

            Buffer.Append(output);
        }

        /// <summary>
        /// This method is used to write out attribute values using
        /// some funky nested tuple storage.
        /// 
        /// Handles situations like href="@(Model.Url)?parm1=1"
        /// where text and expressions mix in the attribute
        /// 
        /// This call comes in from the Razor runtime parser
        /// </summary>
        /// <param name="attr"></param>
        /// <param name="tokens"></param>
        public virtual void WriteAttribute(string attr,
                                   Tuple<string, int> token1,
                                   Tuple<string, int> token2,
                                   Tuple<Tuple<string, int>,
                                         Tuple<object, int>, bool> token3,
                                   Tuple<Tuple<string, int>,
                                         Tuple<string, int>, bool> token4)
        {         
            object value = null;
            object textval = null;
            if (token3 != null)
                value = token3.Item2.Item1;
            else
                value = string.Empty;

            if (token4 != null)
                textval = token4.Item2.Item1;
            else
                textval = string.Empty;

            var output = token1.Item1 + value.ToString() + textval.ToString() + token2.Item1;

            Buffer.Append(output);
        }
    }
}
