using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.IO;

namespace Stardoc.HtmlEditor
{
    internal static class ConfigurationManager
    {
        private static StreamReader _reader;
        //private static readonly String _configFilePath = Application.StartupPath + "\\editor.ini";
        private static Boolean _loaded;
        private const String CONFIG_FUNCTION_NAME_ASSOCIATED_DOC = "SetAsociatedDocumentFunctionName";
        private const String CONFIG_FUNCTION_NAME_RULE_ID = "SetRuleIdFunctionName";
        private const String CONFIG_SCRIPT_SET_ASOCIATED_DOC = "ScriptSetAsociatedDocument";
        private const String CONFIG_SCRIPT_SET_RULE_ID = "ScriptSetRule";
        private const String CONFIG_ELEMENT_RULE_ID = "HiddenFieldRuleId";
        private const String CONFIG_ELEMENT_ASOCIATED_DOC = "HiddenFieldAsociatedDocumentId";



        private static String _setAsociatedDocumentFunctionName = String.Empty;
        private static String _setRuleIdFunctionName = String.Empty;
        private static String _scriptSetAsociatedDocument = String.Empty;
        private static String _hiddenFieldAsociatedDocumentId = String.Empty;
        private static String _scriptSetRule = String.Empty;
        private static String _hiddenFieldRuleId = String.Empty;


        private static String _configFilePath
        {
            get
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Zamba Software\dateconfig.ini"))
                    return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString() + @"\Zamba Software\dateconfig.ini";
                else
                    return Application.StartupPath + "\\editor.ini";
            }
        }

        private static StreamReader Reader
        {
            get
            {
                if (null == _reader)
                    _reader = new StreamReader(_configFilePath);

                return _reader;
            }
            //_loaded  = true;
        }

        private static void LoadValues()
        {
            if (File.Exists(_configFilePath))
            {
                String Line;
                String ItemName;
                String ItemValue;

                while (!String.IsNullOrEmpty(Line = Reader.ReadLine()))
                {
                    ItemName = Line.Substring(0, Line.IndexOf('='));
                    ItemValue = Line.Substring(Line.IndexOf('=') + 1, Line.Length - Line.IndexOf('=') - 1);

                    switch (ItemName.Trim())
                    {
                        case CONFIG_ELEMENT_ASOCIATED_DOC :
                            _hiddenFieldAsociatedDocumentId = ItemValue;
                            break;
                        case CONFIG_ELEMENT_RULE_ID :
                            _hiddenFieldRuleId = ItemValue;
                            break;
                        case CONFIG_FUNCTION_NAME_ASSOCIATED_DOC :
                            _setAsociatedDocumentFunctionName  = ItemValue;
                            break;
                        case CONFIG_FUNCTION_NAME_RULE_ID :
                            _setRuleIdFunctionName = ItemValue;
                            break;
                        case CONFIG_SCRIPT_SET_ASOCIATED_DOC :
                            _scriptSetAsociatedDocument = ItemValue;
                            break;
                        case CONFIG_SCRIPT_SET_RULE_ID   :
                            _scriptSetRule = ItemValue;
                            break;
                        default:
                            break;
                    }
                }
            }

            _loaded = true;
        }


        public static String SetAsociatedDocumentFunctionName
        {
            get
            {
                if (!_loaded)
                    LoadValues();

                return _setAsociatedDocumentFunctionName;
            }
        }
        public static String SetRuleIdFunctionName
        {
            get
            {
                if (!_loaded)
                    LoadValues();
                return _setRuleIdFunctionName;
            }
        }
        public static String ScriptSetAsociatedDocument
        {
            get
            {
                if (!_loaded)
                    LoadValues();
                return _scriptSetAsociatedDocument;
            }
        }
        public static String HiddenFieldAsociatedDocumentId
        {
            get
            {
                if (!_loaded)
                    LoadValues();
                return _hiddenFieldAsociatedDocumentId;
            }
        }
        public static String ScriptSetRule
        {
            get
            {
                if (!_loaded)
                    LoadValues();
                return _scriptSetRule;
            }
        }
        public static String HiddenFieldRuleId
        {
            get
            {
                if (!_loaded)
                    LoadValues();
                return _hiddenFieldRuleId;
            }
        }
    }
}
