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
using System.Linq;


namespace Zamba.Shapes.Controllers
{
    class DocTypesController : IDiagramController, IRefresh
    {
        private TreeLayout _treeLayout = null;
        static int tableCount = 0;

        public IDiagram GetDiagram(Object[] parameters)
        {
            return FillDiagram(parameters);
        }

        private IDiagram FillDiagram(Object[] parameters)
        {
            try
            {
                //Se obtienen los datos a manipular
                //Se Obtienen las Entidadeds (DocType)
                List<IDocType> lstDocType = DocTypeBusinessExt.GetAllDocTypes();
                //Diagrama donde se comienzan a agregar los nodos
                Diagram diagDocType = new Diagram();
                //Se crea un objeto ROOT para comenzar el diagrama
                ZCoreView rootObject = new ZCoreView(0, "Entidades");
                GenericShape shpRoot = new GenericShape(diagDocType, rootObject);
                if (lstDocType.Count > 0)
                {
                    diagDocType.DiagramType = DiagramType.DocType;
                    diagDocType.Bounds = new RectangleF(0, 0, 500, 1000);
              
                    shpRoot.Visible = false;
                    //Se agrega el primer shape. Este seria como un titulo, el inicio de todo
                    diagDocType.Nodes.Add(shpRoot);

                    //Ahora comienza el agregado automatico de los nodos

                    //Se inicializa un HashTable de DocTypes para almacenarlos a todos y establecer la relacion entre los mismos
                    Hashtable docTypes = new Hashtable();
                    int leftColumnHeight = 0;
                    int rightColumnHeight = 0;
                    int x = 0;
                    for (int i = 0; i < lstDocType.Count; i++)
                    {
                        //Se obtiene la Entidad del listado (DocType)
                        IDocType docType = (IDocType)lstDocType[i];
                        TableNode tbl;
                        if (leftColumnHeight > rightColumnHeight)
                        {
                            x = 300;
                            tbl = CreateDocTypeTable(diagDocType, ref x, ref rightColumnHeight, docType);
                        }
                        else
                        {
                            x = 0;
                            tbl = CreateDocTypeTable(diagDocType, ref x, ref leftColumnHeight, docType);
                        }

                        docTypes.Add(tbl.Id.ToString(), tbl);

                    }


                    DocTypesRelations(diagDocType, docTypes);
                    //Se organizan los objetos del diagrama
                    SetLayout(diagDocType, shpRoot);

                    //Se devuelve el diagrama
                    return diagDocType;
                }
                else
                {
                 
                    ZCoreView rootObject2 = new ZCoreView(0, "El diagrama seleccionado no tiene nodos");
                    GenericShape shpRoot2 = new GenericShape(diagDocType, rootObject2);
                    shpRoot2.Transparent = true;
                    shpRoot2.Expandable = false;
                    shpRoot2.TextPadding = new MindFusion.Diagramming.Thickness(60f, 1f, 1f, 1f);
                    return diagDocType;
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            return null;
        }

        private static TableNode CreateDocTypeTable(Diagram diagDocType,ref int posx, ref int posy, IDocType docType)
        {
            //Se completan los atributos de la entidad (Index)
            List<IIndex> indexs = IndexsBussinesExt.getAllIndexesByDocTypeID(docType.ID);
            docType.Indexs = indexs;

            //Se crea el shape especifico por Entidad (DocType)

            //se crea la tabla , en la especificaciones se dice la posicion donde se dibuja.

            TableNode tbl = diagDocType.Factory.CreateTableNode(posx, posy, 50, 60);


            tbl.ColumnCount = 3;
            tbl.RowCount = docType.Indexs.Count + 1;
            tbl.Caption = docType.Name;
            tbl.Columns[0].ColumnStyle = ColumnStyle.FixedWidth;
            tbl.ColumnWidth = 25;
            tbl.RowHeight = 6;

            tbl.Font = new Font("Microsoft Sans Serif",8);
            tbl.Resize(tbl.ColumnCount * tbl.ColumnWidth, tbl.RowHeight + tbl.RowCount * tbl.RowHeight);
            posy += (int)(tbl.RowHeight + tbl.RowCount * tbl.RowHeight) + 25;
            tbl.Columns[0].Width = 17;
            tbl.Columns[1].Width = 25 + 7;
            tbl.Columns[2].Width = 25;
            tbl.Id = docType.ID;
            tbl[0, 0].Text = "ID";
            tbl[1, 0].Text = "Nombre";
            tbl[2, 0].Text = "Tipo";
            tableCount++;

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

        /// <summary>
        /// Setea las relaciones entre entidades, dejando un tablenode con los indices por los cuales se relaciona
        /// </summary>
        /// <param name="diagram"></param>
        /// <param name="docTypes"></param>
        private void DocTypesRelations(MindFusion.Diagramming.Diagram diagram, Hashtable docTypes)
        {
            DataTable relations = DocTypesRDocTypesBusinessExt.GetAllDocTypesRDocTypes();
            DataTable relAdded = relations.Clone();
            relAdded.Rows.Clear();

            TableNode nodeRelation;
            DiagramLink link;
            int y = 0;
            List<DataRow> rowsAdded = new List<DataRow>();
            DataRow[] invertedRow;
            string whereFormat = "DocTypeID1={0}";
            DataRow[] docTypeRelations;
            IEnumerable<long[]> rows;
            List<DataRow> addedRows;
            long currentAsocDoc;
            DataRow currRow;
            int i;
            int relIndex;

            //long docTypeId;
            //Por cada doctypeid que hay en el diagrama
            foreach (long docTypeId in DocTypesRDocTypesBusinessExt.GetAllDocTypesIDHaveAsociations())
            {
                //docTypeId = long.Parse(sDocTypeId);

                //Seleccionamos las relaciones.
                docTypeRelations = relations.Select(string.Format(whereFormat, docTypeId));

                //Si tiene alguna relacion
                if (docTypeRelations != null && docTypeRelations.Length > 0)
                {
                    addedRows = new List<DataRow>();                    
                    relIndex = 0;

                    //Mientras aun falten filas para mostrar en la relacion
                    while (docTypeRelations.Length > addedRows.Count)
                    {
                        //Generamos una nueva tabla
                        nodeRelation = diagram.Factory.CreateTableNode(130, y, 120, 50);
                        nodeRelation.ColumnCount = 2;
                        nodeRelation.Columns[0].Width = 60;
                        nodeRelation.Columns[1].Width = 60;
                        nodeRelation.Caption = "Relacion entre: " + ((TableNode)docTypes[docTypeRelations[relIndex][0].ToString()]).Caption + " - " + ((TableNode)docTypes[docTypeRelations[relIndex][1].ToString()]).Caption;

                        

                        currentAsocDoc = long.Parse(docTypeRelations[relIndex][1].ToString());
                        i = 0;
                        //Mientras no cambie el doctypeid del asociado
                        while (docTypeRelations.Length > relIndex &&currentAsocDoc == long.Parse(docTypeRelations[relIndex][1].ToString()))
                        {
                            nodeRelation.RowCount = i + 1;
                            currRow = docTypeRelations[relIndex];

                            //Generamos una nueva row y le ponemos los indices de la relacion actual
                            nodeRelation[0,i].Text = IndexsBusiness.GetIndexName(int.Parse(currRow[2].ToString()), true);
                            nodeRelation[1,i].Text = IndexsBusiness.GetIndexName(int.Parse(currRow[3].ToString()), true);

                            //Marcamos la fila como agregada
                            addedRows.Add(currRow);
                            currentAsocDoc = long.Parse(docTypeRelations[relIndex][1].ToString());
                            //Desplazamos el enumerador
                            relIndex++;
                            i++;
                        }

                        nodeRelation.SetBounds(new RectangleF(130, y, 120, (i + 1) * 6), true, true);

                        y += (i + 1) *  6 + 15;
                        
                        link = new DiagramLink(diagram, (TableNode)docTypes[docTypeRelations[relIndex - 1][0].ToString()], nodeRelation);
                        link.AutoRoute = true;
                        link.DrawCrossings = false;
                        diagram.Links.Add(link);

                        link = new DiagramLink(diagram, nodeRelation, (TableNode)docTypes[docTypeRelations[relIndex -1 ][1].ToString()]);
                        link.DrawCrossings = false;
                        link.AutoRoute = true;
                        diagram.Links.Add(link);
                    }
                }
            }            
        }

        private void SetLayout(MindFusion.Diagramming.Diagram diagram, GenericShape shpRoot)
        {
            diagram.LinkBrush = new MindFusion.Drawing.SolidBrush("#FFFFC0CB");
            diagram.LinkBaseShape = MindFusion.Diagramming.ArrowHeads.PointerArrow;
            diagram.LinkHeadShape = MindFusion.Diagramming.ArrowHeads.PointerArrow;
            diagram.LinkCascadeOrientation = MindFusion.Diagramming.Orientation.Horizontal;
            diagram.LinkShape = MindFusion.Diagramming.LinkShape.Bezier;
            diagram.LinkHitDistance = 10;
            diagram.RoundedLinks = true;
            diagram.RouteLinks = true;

            //Dibuja el diagrama acomodando todo lo modificado
            diagram.ResizeToFitItems(25, false);
        }


        public static void AddEntities(IEnumerable<ICore> lst, Diagram diagHome, GenericShape shpInsertar)
        {
            foreach (ICore item in lst)
            {
                IEntityDiagram entity = new EntityDiagram() { ID=item.ID,Name=item.Name };
                EntityShape shpEnt = new EntityShape(diagHome, entity, shpInsertar);
            }
        }

        /// <summary>
        /// Genera una tabla de los indices que son pasados en la lista
        /// La misma será retornada para poder ser trabajada.
        /// </summary>
        /// <param name="dg"></param>
        /// <param name="indexs"></param>
        /// <returns></returns>
        public TableNode GetIndexesAsTable(Diagram dg, List<IIndex> indexs)
        {
            TableNode tbl = dg.Factory.CreateTableNode(100,0,50,60);

            tbl.ColumnCount = 3;
            tbl.RowCount = indexs.Count + 1;
            tbl.Caption = "Atrtibutos asociados";
            tbl.Columns[0].ColumnStyle = ColumnStyle.FixedWidth;
            tbl.ColumnWidth = 25;
            tbl.RowHeight = 6;

            tbl.Font = new Font("Microsoft Sans Serif", 8);
            tbl.Resize(tbl.ColumnCount * tbl.ColumnWidth, tbl.RowHeight + tbl.RowCount * tbl.RowHeight);
            tbl.Columns[0].Width = 17;
            tbl.Columns[1].Width = 25 + 7;
            tbl.Columns[2].Width = 25;

            tbl.Id = "tblAttr";
            tbl[0, 0].Text = "ID";
            tbl[1, 0].Text = "Nombre";
            tbl[2, 0].Text = "Tipo";
            tableCount++;

            if (indexs != null && indexs.Count > 0)
            {
                for (int j = 0; j < indexs.Count; j++)
                {
                    //Se obtiene el atributo (Index) del listado
                    tbl[0, j + 1].Text = indexs[j].ID.ToString();
                    tbl[1, j + 1].Text = indexs[j].Name.ToString();
                    tbl[2, j + 1].Text = indexs[j].Type.ToString();
                }
            }

            return tbl;
        }

        public IDiagram Refresh(object[] parameters)
        {
            return FillDiagram(parameters);
        }
    }
}

