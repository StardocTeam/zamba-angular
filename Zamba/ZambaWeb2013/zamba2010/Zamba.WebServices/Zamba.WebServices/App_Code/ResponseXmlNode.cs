using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

/// <summary>
/// Summary description for ResponseXml
/// </summary>
public class ResponseXml
    : XmlDocument
{


    public ResponseXml()
    {
        AppendChild(base.CreateElement("ReturnedQuery"));
    }


    public void SetErrorState(String errorMessage)
    {
        XmlElement ErrorNode = CreateElement("State");
        XmlAttribute ValueNode = CreateAttribute("value");
        ValueNode.Value = "1";

        ErrorNode.Attributes.Append(ValueNode);
        ErrorNode.InnerText = errorMessage;

        FirstChild.AppendChild(ErrorNode);
    }
    public void SetErrorState(Exception ex)
    {
        SetErrorState(ex.Message);
    }

    public void SetSuccesState()
    {
        XmlElement Success = CreateElement("State");
        XmlAttribute value = CreateAttribute("value");
        value.Value = "0";

        Success.Attributes.Append(value);

        FirstChild.AppendChild(Success);
    }
}

