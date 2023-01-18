using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Stardoc.HtmlEditor;
using Stardoc.HtmlEditor.HtmlControls;

namespace TestEditorHtml
{
    static class Program
    {

        static VirtualFormsBuilder _mainForm = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                _mainForm = new VirtualFormsBuilder();
                _mainForm.OnSelectedDocTypeChanged += new VirtualFormsBuilder.SelectedDocTypeChanged(MainForm_OnSelectedDocTypeChanged);

                Dictionary<Int64, String> DocTypes = new Dictionary<Int64, String>(1);
                DocTypes.Add(1, "Cheques");
                DocTypes.Add(2, "Vales");

                _mainForm.DocTypes = DocTypes;

                Application.Run(_mainForm);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        static void MainForm_OnSelectedDocTypeChanged(long? docTypeId)
        {
            if (docTypeId.HasValue)
            {
                List<IHtmlElement> Elements = new List<IHtmlElement>(5);

                if (docTypeId.Value == 1)
                {

                    Elements.Add(new CheckBoxItem("CheckBox normal", true, true));
                    Elements.Add(new RadioButtonItem("RadioButton normal", "zamba", true, true));
                    Elements.Add(new RadioButtonItem("RadioButton normal 2", "zamba", false, true));

                    List<OptionItem> ops = new List<OptionItem>(5);
                    for (int i = 1; i < 6; i++)
                        ops.Add(new OptionItem("Opcion " + i.ToString(), i.ToString(), false));

                    SelectItem Select = new SelectItem(ops, true);
                    Select.Required = true;
                    Elements.Add(Select);

                    Elements.Add(new TextAreaItem(10, 10, "Ipsen Loren", true));
                    Elements.Add(new TextBoxItem("TextBox normal", "", true, true));


                    Elements.Add(new ZCheckBoxItem("CheckBox zamba", true, true, true, 1));
                    Elements.Add(new ZRadioButtonItem("RadioButton zamba", "zamba", true, true, 2));
                    Elements.Add(new ZRadioButtonItem("RadioButton zamba 2", "zamba", false, true, 2));

                    List<OptionItem> Zops = new List<OptionItem>(5);
                    for (int i = 1; i < 6; i++)
                        Zops.Add(new OptionItem("Opcion " + i.ToString(), i.ToString(), false));

                    ZSelectItem ZSelect = new ZSelectItem(Zops, true, true, 3);
                    ZSelect.Required = true;
                    Elements.Add(ZSelect);

                    Elements.Add(new ZTextAreaItem(10, 10, "Ipsen Loren", true, 4));
                    Elements.Add(new ZTextBoxItem("TextBox zamba", "", true, true, true, 5));


                    _mainForm.LoadHtmlElements(Elements);
                }
                else if(docTypeId.Value == 2)
                {
                    Elements.Add(new CheckBoxItem("CheckBox ", true, true));
                    Elements.Add(new RadioButtonItem("RadioButton ", "zamba", true, true));
                    Elements.Add(new RadioButtonItem("RadioButton ", "zamba", false, true));

                    List<OptionItem> ops = new List<OptionItem>(5);
                    for (int i = 1; i < 6; i++)
                        ops.Add(new OptionItem("Opcion " + i.ToString(), i.ToString(), false));

                    SelectItem Select = new SelectItem(ops, true);
                    Select.Required = true;
                    Elements.Add(Select);

                    Elements.Add(new TextAreaItem(10, 10, "Ipsen Loren", true));
                    Elements.Add(new TextBoxItem("TextBox ", "", true, true));


                    Elements.Add(new ZCheckBoxItem("CheckBox ", true, true, true, 1));
                    Elements.Add(new ZRadioButtonItem("RadioButton ", "zamba", true, true, 2));
                    Elements.Add(new ZRadioButtonItem("RadioButton ", "zamba", false, true, 2));

                    List<OptionItem> Zops = new List<OptionItem>(5);
                    for (int i = 1; i < 6; i++)
                        Zops.Add(new OptionItem("Opcion " + i.ToString(), i.ToString(), false));

                    ZSelectItem ZSelect = new ZSelectItem(Zops, true, true, 3);
                    ZSelect.Required = true;
                    Elements.Add(ZSelect);

                    Elements.Add(new ZTextAreaItem(10, 10, "Ipsen Loren", true, 4));
                    Elements.Add(new ZTextBoxItem("TextBox", "", true, true, true, 5));


                    _mainForm.LoadHtmlElements(Elements);
                }
            }
        }
    }
}

