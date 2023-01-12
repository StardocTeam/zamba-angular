using System;
using System.Collections;
using Zamba.Core;
using Zamba.Diagrams.Shapes;
using MindFusion.Diagramming.Layout;
using System.Drawing;
using Diagram = Zamba.Shapes.Views.Diagram;
using System.Collections.Generic;
using MindFusion.Drawing;

using ColumnStyle = MindFusion.Diagramming.ColumnStyle;
using MindFusion.Diagramming;
using MindFusion.Diagramming.WinForms;
using System.Data;

namespace Zamba.Shapes.Controllers
{
    class DERController : IDiagramController, IRefresh
    {
        private TreeLayout _treeLayout = null;


        public IDiagram GetDiagram(Object[] parameters)
        {
            return fillDiagram(parameters,false);

        }

        private IDiagram fillDiagram(Object[] parameters, bool isRefresh)
        {

            //Se inicializa el diagrama donde se va a dibujar 
            Diagram diagDER = new Diagram();
            diagDER.DiagramType = DiagramType.DER;
            diagDER.Bounds = new RectangleF(0, 0, 500, 1000);
            //Se crea un objeto ROOT para comenzar el diagrama
            ZCoreView rootObject = new ZCoreView(0, "DER");
            GenericShape shpRoot = new GenericShape(diagDER, rootObject);
            shpRoot.Visible = false;
            //Se agrega el primer shape. Este seria como un titulo, el inicio de todo
            diagDER.Nodes.Add(shpRoot);


            //Se obtiene la Entidad del listado (DocType)
            IDocType docType = DocTypesBusiness.GetDocType((Int64)((TableNode)parameters[0]).Id, isRefresh);
            // DocTypeBusinessExt.GetDocTypeByID((Int64)((TableNode)parameters[0]).Id);
            if (docType != null)
            {
                //Se completan los atributos de la entidad (Index)
                List<IIndex> indexs = IndexsBussinesExt.getAllIndexesByDocTypeID(docType.ID);
                docType.Indexs = indexs;

                //se crea la tabla , en la especificaciones se dice la posicion donde se dibuja.
                //Se inicializan las variables con la cual se van a trabajar para el generado automatico de las tablas.
                float createdWidth = 0;
                float createdHeight = 0;
                float actualx = 0;
                float actualy = 0;
                float spaceBetweenTables = 25;
                //Se crea la tabla para la entidad
                TableNode DocTypeTable = CreateDocTypeTable(diagDER, docType, indexs, actualx, 100, ref createdWidth, ref createdHeight);
                //actualx indica la posición x en donde se va a dibujar el diagrama .
                actualx += spaceBetweenTables + createdWidth;
                //se crean las DOC_I DOC_B DOC_T DOC_D relacionadas.
                //actualy indica la posición y en donde se va a dibujar el diagrama .
                TableNode DocITable = CreateDocITable(diagDER, docType, actualx, actualy, ref createdWidth, ref createdHeight);
                if (DocITable != null)
                {
                    actualy += spaceBetweenTables + createdHeight;
                    diagDER.Links.Add(new DiagramLink(diagDER, DocITable, DocTypeTable));
                }

                TableNode DocBTable = CreateDocBTable(diagDER, docType, actualx, actualy, ref createdWidth, ref createdHeight);
                if (DocBTable != null)
                {
                    actualy += spaceBetweenTables + createdHeight;
                    diagDER.Links.Add(new DiagramLink(diagDER, DocBTable, DocTypeTable));
                }

                TableNode DocDTable = CreateDocDTable(diagDER, docType, actualx, actualy, ref createdWidth, ref createdHeight);
                if (DocDTable != null)
                {
                    actualy += spaceBetweenTables + createdHeight;
                    diagDER.Links.Add(new DiagramLink(diagDER, DocDTable, DocTypeTable));
                }


                TableNode DocTTable = CreateDocTTable(diagDER, docType, actualx, actualy, ref createdWidth, ref createdHeight);
                if (DocTTable != null)
                {

                    actualy += spaceBetweenTables + createdHeight;
                    diagDER.Links.Add(new DiagramLink(diagDER, DocTTable, DocTypeTable));
                }


                //Se organizan los objetos del diagrama
                SetLayout(diagDER, shpRoot);

                //Se devuelve el diagrama

                return diagDER;
            }
            return null;
        }


        private TableNode CreateDocTTable(Diagram diagDER, IDocType docType, float x, float y, ref float tableCreatedWidth, ref float tableCreatedHeight)
        {
            DataTable dt = DocTypeBusinessExt.GetSchemeFromDoc_TByDocTypeID(docType.ID);
            if (dt != null)
            {
                TableNode tbl = diagDER.Factory.CreateTableNode(x, y, 50, 60);


                tbl.ColumnCount = 4;
                tbl.RowCount = dt.Rows.Count + 1;
                tbl.Caption = "DOC_T" + docType.ID;
                tbl.ColumnWidth = 20;
                tbl.RowHeight = 6;
               tbl.Font = new Font("Microsoft Sans Serif",8);
                tableCreatedWidth = tbl.ColumnCount * tbl.ColumnWidth;
                tableCreatedHeight = tbl.RowHeight + tbl.RowCount * tbl.RowHeight;
                tbl.Resize(tableCreatedWidth, tableCreatedHeight);
                tbl.Shape = SimpleShape.Rectangle;
                for (int i = 0; i < tbl.ColumnCount; i++)
                {
                    tbl.Columns[i].Width = tbl.ColumnWidth;
                }
                
                tbl[0, 0].Text = "Atributo";
                tbl[1, 0].Text = "Tipo";
                tbl[2, 0].Text = "Longitud";
                tbl[3, 0].Text = "Precisión";
                tbl.Id = docType.ID;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    tbl[0, j + 1].Text = dt.Rows[j].ItemArray[0].ToString();
                    tbl[1, j + 1].Text = dt.Rows[j].ItemArray[1].ToString();
                    tbl[2, j + 1].Text = dt.Rows[j].ItemArray[3].ToString();
                    tbl[3, j + 1].Text = dt.Rows[j].ItemArray[4].ToString();
                }

                return tbl;
            }
            return null;

        }

        private TableNode CreateDocDTable(Diagram diagDER, IDocType docType, float x, float y, ref float tableCreatedWidth, ref float tableCreatedHeight)
        {
            DataTable dt = DocTypeBusinessExt.GetSchemeFromDoc_DByDocTypeID(docType.ID);
            if (dt != null)
            {
                TableNode tbl = diagDER.Factory.CreateTableNode(x, y, 50, 60);


                tbl.ColumnCount = 4;
                tbl.RowCount = dt.Rows.Count + 1;
                tbl.Caption = "DOC_D" + docType.ID;
                tbl.ColumnWidth = 20;
                tbl.RowHeight = 6;
               tbl.Font = new Font("Microsoft Sans Serif",8);
                tableCreatedWidth = tbl.ColumnCount * tbl.ColumnWidth;
                tableCreatedHeight = tbl.RowHeight + tbl.RowCount * tbl.RowHeight;
                tbl.Resize(tableCreatedWidth, tableCreatedHeight);
                tbl.Shape = SimpleShape.Rectangle;
                for (int i = 0; i < tbl.ColumnCount; i++)
                {
                    tbl.Columns[i].Width = tbl.ColumnWidth;
                }
                
                tbl[0, 0].Text = "Atributo";
                tbl[1, 0].Text = "Tipo";
                tbl[2, 0].Text = "Longitud";
                tbl[3, 0].Text = "Precisión";
                tbl.Id = docType.ID;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    tbl[0, j + 1].Text = dt.Rows[j].ItemArray[0].ToString();
                    tbl[1, j + 1].Text = dt.Rows[j].ItemArray[1].ToString();
                    tbl[2, j + 1].Text = dt.Rows[j].ItemArray[3].ToString();
                    tbl[3, j + 1].Text = dt.Rows[j].ItemArray[4].ToString();
                }

                return tbl;
            }
            return null;
        }

        private TableNode CreateDocBTable(Diagram diagDER, IDocType docType, float x, float y, ref float tableCreatedWidth, ref float tableCreatedHeight)
        {
            DataTable dt = DocTypeBusinessExt.GetSchemeFromDoc_BByDocTypeID(docType.ID);
            if (dt != null)
            {
                TableNode tbl = diagDER.Factory.CreateTableNode(x, y, 50, 60);


                tbl.ColumnCount = 4;
                tbl.RowCount = dt.Rows.Count + 1;
                tbl.Caption = "DOC_B" + docType.ID;
                tbl.ColumnWidth = 20;
                tbl.RowHeight = 6;
               tbl.Font = new Font("Microsoft Sans Serif",8);
                tableCreatedWidth = tbl.ColumnCount * tbl.ColumnWidth;
                tableCreatedHeight = tbl.RowHeight + tbl.RowCount * tbl.RowHeight;
                tbl.Resize(tableCreatedWidth, tableCreatedHeight);
                tbl.Shape = SimpleShape.Rectangle;
                for (int i = 0; i < tbl.ColumnCount; i++)
                {
                    tbl.Columns[i].Width = tbl.ColumnWidth;
                }
                
                tbl[0, 0].Text = "Atributo";
                tbl[1, 0].Text = "Tipo";
                tbl[2, 0].Text = "Longitud";
                tbl[3, 0].Text = "Precisión";
                tbl.Id = docType.ID;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    tbl[0, j + 1].Text = dt.Rows[j].ItemArray[0].ToString();
                    tbl[1, j + 1].Text = dt.Rows[j].ItemArray[1].ToString();
                    tbl[2, j + 1].Text = dt.Rows[j].ItemArray[3].ToString();
                    tbl[3, j + 1].Text = dt.Rows[j].ItemArray[4].ToString();
                }
                return tbl;
            }
            return null;
        }

        private TableNode CreateDocITable(Diagram diagDER, IDocType docType, float x, float y, ref float tableCreatedWidth, ref float tableCreatedHeight)
        {
            DataTable dt = DocTypeBusinessExt.GetSchemeFromDoc_IByDocTypeID(docType.ID);
            if (dt != null)
            {
                TableNode tbl = diagDER.Factory.CreateTableNode(x, y, 50, 60);


                tbl.ColumnCount = 4;
                tbl.RowCount = dt.Rows.Count + 1;
                tbl.Caption = "DOC_I" + docType.ID;

                tbl.ColumnWidth = 20;
                tbl.RowHeight = 6;
               tbl.Font = new Font("Microsoft Sans Serif",8);
                tableCreatedWidth = tbl.ColumnCount * tbl.ColumnWidth;
                tableCreatedHeight = tbl.RowHeight + tbl.RowCount * tbl.RowHeight;
                tbl.Resize(tableCreatedWidth, tableCreatedHeight);
                tbl.Shape = SimpleShape.Rectangle;
                for (int i = 0; i < tbl.ColumnCount; i++)
                {
                    tbl.Columns[i].Width = tbl.ColumnWidth;
                }

                tbl[0, 0].Text = "Atributo";
                tbl[1, 0].Text = "Tipo";
                tbl[2, 0].Text = "Longitud";
                tbl[3, 0].Text = "Precisión";
                tbl.Id = docType.ID;

                float auxtableCreatedWidth = 0;
                float auxtableCreatedHeight = 0;
                float auxX = 250;
                float auxY = y;

                for (int j = 0; j < dt.Rows.Count; j++)
                {

                    tbl[0, j + 1].Text = dt.Rows[j].ItemArray[0].ToString();
                    tbl[1, j + 1].Text = dt.Rows[j].ItemArray[1].ToString();
                    tbl[2, j + 1].Text = dt.Rows[j].ItemArray[3].ToString();
                    tbl[3, j + 1].Text = dt.Rows[j].ItemArray[4].ToString();
                    Int32 indexid;
                    TableNode tbllst;
                    Int32.TryParse(dt.Rows[j].ItemArray[0].ToString().Replace("I", ""), out indexid);
                    Int32 spaceBetweenTables = 25;
                    
                    switch (IndexsBussinesExt.GetIndexDropDownType(indexid))
                    {
                        case IndexAdditionalType.DropDown:
                        case IndexAdditionalType.DropDownJerarquico:
                            tbllst = CreateIlstTable(diagDER, indexid, auxX, auxY, ref  auxtableCreatedWidth, ref  auxtableCreatedHeight);
                            auxY += auxtableCreatedHeight + spaceBetweenTables;
                            diagDER.Links.Add(new DiagramLink(diagDER, tbl, tbllst));
                            break;
                        case IndexAdditionalType.AutoSustitución:
                        case IndexAdditionalType.AutoSustituciónJerarquico:
                            tbllst = CreateSlstTable(diagDER, indexid, auxX, auxY, ref  auxtableCreatedWidth, ref  auxtableCreatedHeight);
                            auxY += auxtableCreatedHeight + spaceBetweenTables;
                            diagDER.Links.Add(new DiagramLink(diagDER, tbl, tbllst));
                            break;
                        default:
                            break;
                    }
                }

                return tbl;
            }
            return null;
        }


        private static TableNode CreateDocTypeTable(Diagram diagDER, IDocType docType, List<IIndex> indexs, float x, float y, ref float tableCreatedWidth, ref float tableCreatedHeight)
        {
            TableNode tbl = diagDER.Factory.CreateTableNode(x, y, 50, 60);


            tbl.ColumnCount = 3;
            tbl.RowCount = docType.Indexs.Count + 1;
            tbl.Caption = docType.Name;
            tbl.ColumnWidth = 25;
            tbl.RowHeight = 6;
           tbl.Font = new Font("Microsoft Sans Serif",8);
            tableCreatedWidth = tbl.ColumnCount * tbl.ColumnWidth;
            tableCreatedHeight = tbl.RowHeight + tbl.RowCount * tbl.RowHeight;
            tbl.Resize(tableCreatedWidth, tableCreatedHeight);
            tbl.Shape = SimpleShape.Rectangle;
            for (int i = 0; i < tbl.ColumnCount; i++)
            {
                tbl.Columns[i].Width = tbl.ColumnWidth;
            }
            
            tbl.Columns[0].Width = 17;
            tbl.Columns[1].Width = 25 + 7;
            tbl.Columns[2].Width = 25;
            tbl.Id = docType.ID;
            tbl[0, 0].Text = "ID";
            tbl[1, 0].Text = "Nombre";
            tbl[2, 0].Text = "Tipo";


            if (indexs != null && indexs.Count > 0)
            {
                for (int j = 0; j < indexs.Count; j++)
                {
                    //Se obtiene el atributo (Index) del listado
                    IIndex index = (IIndex)docType.Indexs[j];
                    tbl[0, j + 1].Text = index.ID.ToString();
                    tbl[1, j + 1].Text = index.Name.ToString();
                    tbl[2, j + 1].Text = index.Type.ToString();
                }
            }

            return tbl;
        }

        private void DocTypesRelations(MindFusion.Diagramming.Diagram diagram, Hashtable docTypes)
        {
            DataTable relations = DocTypesRDocTypesBusinessExt.GetAllDocTypesRDocTypes();

            foreach (DataRow row in relations.Rows)
            {
                diagram.Links.Add(new DiagramLink(diagram, (TableNode)docTypes[row[0].ToString()], (TableNode)docTypes[row[1].ToString()]));
            }
        }

        private void SetLayout(MindFusion.Diagramming.Diagram diagram, GenericShape shpRoot)
        {
            diagram.ResizeToFitItems(25, false);
        }
        private static TableNode CreateIlstTable(Diagram diagDER, Int32 indexid, float x, float y, ref float tableCreatedWidth, ref float tableCreatedHeight)
        {
         
            TableNode tbl = diagDER.Factory.CreateTableNode(x, y, 50, 60);
            DataTable dt = IndexsBussinesExt.GetSchemeIlst(indexid);

            tbl.ColumnCount = 4;
            tbl.RowCount = dt.Rows.Count + 1;
            tbl.Caption = "Ilst_I" + indexid;

            tbl.ColumnWidth = 20;
            tbl.RowHeight = 6;
           tbl.Font = new Font("Microsoft Sans Serif",8);
            tableCreatedWidth = tbl.ColumnCount * tbl.ColumnWidth;
            tableCreatedHeight = tbl.RowHeight + tbl.RowCount * tbl.RowHeight;
            tbl.Resize(tableCreatedWidth, tableCreatedHeight);
            tbl.Shape = SimpleShape.Rectangle;
            for (int i = 0; i < tbl.ColumnCount; i++)
            {
                tbl.Columns[i].Width = tbl.ColumnWidth;
            }

            tbl.Id = indexid;
            tbl[0, 0].Text = "Atributo";
            tbl[1, 0].Text = "Tipo";
            tbl[2, 0].Text = "Longitud";
            tbl[3, 0].Text = "Precisión";

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                tbl[0, j + 1].Text = dt.Rows[j].ItemArray[0].ToString();
                tbl[1, j + 1].Text = dt.Rows[j].ItemArray[1].ToString();
                tbl[2, j + 1].Text = dt.Rows[j].ItemArray[3].ToString();
                tbl[3, j + 1].Text = dt.Rows[j].ItemArray[4].ToString(); 

            }
            return tbl;
        }

        private static TableNode CreateSlstTable(Diagram diagDER, Int32 indexid, float x, float y, ref float tableCreatedWidth, ref float tableCreatedHeight)
        {

            TableNode tbl = diagDER.Factory.CreateTableNode(x, y, 50, 60);
            DataTable dt = IndexsBussinesExt.GetSchemeSlst(indexid);

            tbl.ColumnCount = 4;
            tbl.RowCount = dt.Rows.Count + 1;
            tbl.Caption = "Slst_S" + indexid;

            tbl.ColumnWidth = 20;
            tbl.RowHeight = 6;
           tbl.Font = new Font("Microsoft Sans Serif",8);
            tableCreatedWidth = tbl.ColumnCount * tbl.ColumnWidth;
            tableCreatedHeight = tbl.RowHeight + tbl.RowCount * tbl.RowHeight;
            tbl.Resize(tableCreatedWidth, tableCreatedHeight);
            tbl.Shape = SimpleShape.Rectangle;
            for (int i = 0; i < tbl.ColumnCount; i++)
            {
                tbl.Columns[i].Width = tbl.ColumnWidth;
            }

            tbl.Id = indexid;
            tbl[0, 0].Text = "Atributo";
            tbl[1, 0].Text = "Tipo";
            tbl[2, 0].Text = "Longitud";
            tbl[3, 0].Text = "Precisión";

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                tbl[0, j + 1].Text =  dt.Rows[j].ItemArray[0].ToString();
                tbl[1, j + 1].Text =  dt.Rows[j].ItemArray[1].ToString();
                tbl[2, j + 1].Text =  dt.Rows[j].ItemArray[3].ToString();
                tbl[3, j + 1].Text =  dt.Rows[j].ItemArray[4].ToString(); 

            }
            return tbl;
        }

        public IDiagram Refresh(object[] parameters)
        {
            return fillDiagram(parameters, true);
        }
    }
}