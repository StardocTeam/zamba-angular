using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Xml.Serialization;

namespace Zamba.Grid.PageGroupGrid
{
        public class BarraPaginaClickEventArgs : 
                        EventArgs, 
                        IBarraPaginaClickEventArgs {
            protected ArrayList lista;


            public BarraPaginaClickEventArgs(ArrayList lista) {
                this.lista = lista; }

            public Object ItemSelectedPage {
                get { return this.lista; } }
        }

        public class DataNavigatorPageBarClickEventArgs : 
                        BarraPaginaClickEventArgs, 
                        IBarraPaginaClickEventArgs
        {
            protected IDataSource pageList;

            public DataNavigatorPageBarClickEventArgs(
                 ArrayList lista, 
                 TemplateDataPageGroupList pageList 
                ) : base(lista) 
            {
                    this.pageList = pageList;
            }

            public Object ItemSelectedPage {
                get {
                    return this.getOriginalCollection();
                }
            }

            protected Type getItemType() {
                if (null == this.lista ||
                    0 == this.lista.Count)
                    return null;

                return this.lista[0].GetType();
            }

            protected Object getOriginalCollection() {
                if (this.getItemType() == typeof(DataRow)) {
                    DataTable table =
                        ((DataTable)this.pageList.DataSource).Clone();
                       
                    foreach (DataRow row in this.lista)
                        table.ImportRow(row);

                    return table;

                }
                
                return this.lista;
            }

        }

        public delegate void BarraPaginaClickEventHandler(
                    object sender,
                    IBarraPaginaClickEventArgs e);
    }

