using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Stardoc.HtmlEditor;
using Stardoc.HtmlEditor.HtmlControls;
using Zamba.Core;
using Zamba.HTMLEditor.Controller;

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
                //Index ElementsSorter = IndexsBussines.GetIndexByIdAsIndex(2348,"");
                //Index asd2 = IndexsBussines.GetIndexByIdAsIndex(2349,"");

                IDocType CurrentDocType = new DocType();
                CurrentDocType.Name = "Tipo de Documento de Prueba";
                CurrentDocType.Indexs.AddRange(GetAllIndexes());

                IZwebForm Form = new ZwebForm();

                FormsEditor target = new FormsEditor(Form, CurrentDocType);
                target.Show();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private static List<IIndex> GetAllIndexes()
        {
            List<IIndex> Indexes = new List<IIndex>(9);

            IIndex Alfanumerico = new Index("Alfanumerico", 1, IndexDataType.Alfanumerico, 20, IndexAdditionalType.DropDown);
            Alfanumerico.DropDown = IndexAdditionalType.LineText ;
            Indexes.Add(Alfanumerico);

            IIndex AlfanumericoLargo = new Index("AlfanumericoLargo", 2, IndexDataType.Alfanumerico_Largo, 20, IndexAdditionalType.DropDown);
            AlfanumericoLargo.DropDown = IndexAdditionalType.LineText;
            Indexes.Add(AlfanumericoLargo);

            IIndex Fecha = new Index("Fecha", 3, IndexDataType.Fecha, 20, IndexAdditionalType.DropDown);
            Fecha.DropDown = IndexAdditionalType.LineText;
            Indexes.Add(Fecha);

            IIndex FechaHora = new Index("Fecha_Hora", 4, IndexDataType.Fecha_Hora, 20, IndexAdditionalType.DropDown);
            FechaHora.DropDown = IndexAdditionalType.LineText;
            Indexes.Add(FechaHora );

            IIndex Moneda = new Index("Moneda", 5, IndexDataType.Moneda, 20, IndexAdditionalType.DropDown);
            Moneda.DropDown = IndexAdditionalType.LineText;
            Indexes.Add(Moneda);

            IIndex Numerico = new Index("Numerico", 6, IndexDataType.Numerico, 20, IndexAdditionalType.DropDown);
            Numerico.DropDown = IndexAdditionalType.LineText;
            Indexes.Add(Numerico);

            IIndex NumericoDecimales = new Index("Numerico_Decimales", 6, IndexDataType.Numerico_Decimales, 20, IndexAdditionalType.DropDown);
            NumericoDecimales.DropDown = IndexAdditionalType.LineText;
            Indexes.Add(NumericoDecimales);

            IIndex NumericoLargo = new Index("Numerico_Largo", 7, IndexDataType.Numerico_Largo, 20, IndexAdditionalType.DropDown);
            NumericoDecimales.DropDown = IndexAdditionalType.LineText;
            Indexes.Add(NumericoDecimales);

            IIndex SiNo = new Index("Si_No", 8, IndexDataType.Si_No, 20, IndexAdditionalType.DropDown);
            SiNo.DropDown = IndexAdditionalType.LineText;
            Indexes.Add(SiNo);

            Index Listado = new Index("Listado", 9, IndexDataType.Alfanumerico, 20, IndexAdditionalType.DropDown);
            Listado.DropDown = IndexAdditionalType.DropDown;
            for (int i = 0; i < 10; i++)
                Listado.DropDownList.Add(i);

            Indexes.Add(Listado);

            return Indexes;
        }
    }
}
