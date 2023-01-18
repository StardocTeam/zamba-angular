using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Xml.Serialization;
using Zamba.Grid;
using Zamba.Grid.PageGroupGrid;

namespace Zamba
{
    namespace Thumbnails 
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
                        ((DataSet)this.pageList.DataSource).Tables[0].Clone();
                       
                    foreach (DataRow row in this.lista)
                        table.ImportRow(row);

                    DataSet ds = new DataSet();
                    ds.Tables.Add(table);
                    return ds;

                }
                
                return this.lista;
            }
     
            //public T Clonar<T>(T e) {
            //    XmlSerializer ser = new XmlSerializer(typeof(T));                
            //    System.IO.MemoryStream m = new System.IO.MemoryStream();
            //    ser.Serialize(m, e);                                            
            //    return (T)ser.Deserialize(m);                                                        
            //}
        }

        public delegate void BarraPaginaClickEventHandler(
                    object sender,
                    IBarraPaginaClickEventArgs e);
    }
}
