using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Zamba.Core;
using System.Collections.Generic;

namespace IntranetMarsh.Controls
{
    public partial class ProcList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    TreeNode docname = null;
                    TreeNode indexname = null;
                    List<string> nodes1 = null;
                    List<string> nodes2 = null;
                    //[Ezequiel] - Se realiza la carga de nombres de tipos de documentos a mostrar que estan en el webconfig.
                    foreach (string doct in System.Web.Configuration.WebConfigurationManager.AppSettings["ProceduresListIdDoc"].Split(','))
                    {
                        Int32 doctypeid = Int32.Parse(doct);
                        Zamba.Core.DocType[] arDocTypes = new DocType[1];
                        arDocTypes[0] = Zamba.Core.DocTypesBusiness.GetDocType(doctypeid);
                        Zamba.Core.Searchs.Search Search = new Zamba.Core.Searchs.Search(IndexsBussines.GetIndexsSchema(Convert.ToInt32(arDocTypes[0].ID)), "", true, "", "", 0, arDocTypes, true, "");
                        ArrayList mresults = new ArrayList();
                        //todo: arreglar que ahora no se recibe un arraylist es un datatable
                        //                       mresults = Zamba.Core.ModDocuments.DoSearch(Search, UserBusiness.CurrentUser(), true);

                        docname = new TreeNode(arDocTypes[0].Name, arDocTypes[0].ID.ToString());
                        docname.Expanded = false;
                        this.ProcTreeView.Nodes.Add(docname);
                        Int16 numindex = 0;
                        nodes1 = new List<string>();
                        nodes2 = new List<string>();

                        //[Ezequiel] - Se realiza la carga de los indices que se especificaron en el webconfig
                        foreach (string indexslst in System.Web.Configuration.WebConfigurationManager.AppSettings["ProceduresListIdIn"].Split(','))
                        {
                            numindex++;
                            //[Ezequiel] - Si es la primera iteracion se agregan los indices al nodo del documento
                            if (numindex == 1)
                                //[Ezequiel] - Recorro todos los result para agregar el valor del indice que quiero poner en el treeview
                                foreach (Result results in mresults)
                                    //[Ezequiel] - Recorro los indices del result
                                    foreach (Index indexs in results.Indexs)
                                    {
                                        //[Ezequiel] - Comparo que el indice sea del tipo al cual voy a agregar
                                        if (indexs.ID.ToString().CompareTo(indexslst) == 0 && !string.IsNullOrEmpty(indexs.Data) && !nodes1.Contains(indexs.Data))
                                        {
                                            //[Ezequiel] - Agrego el indice
                                            nodes1.Add(indexs.Data);
                                            indexname = new TreeNode(indexs.Data);
                                            indexname.Expanded = false;
                                            docname.ChildNodes.Add(indexname);
                                        }
                                    }
                            //[Ezequiel] - Si es la la segunda iteracion se agregan los indices a los nodos que se agregaron en la iteracion anterior.
                            else if (numindex == 2)
                                //[Ezequiel] - Recorro todos los nodos que se agregaron anteriormente
                                foreach (TreeNode nodaux in docname.ChildNodes)
                                    //[Ezequiel] - Recorro todos los result para agregar el valor del indice que quiero agregar
                                    foreach (Result results in mresults)
                                    //[Ezequiel] - Recorro los indices del result
                                    {
                                        List<string> indexsid = new List<string>();
                                        foreach (Index indexs in results.Indexs)
                                        {
                                            indexsid.Add(indexs.Data);
                                        }
                                        if (indexsid.Contains(nodaux.Text) && !nodes2.Contains(results.get_GetIndexById(Int32.Parse(indexslst)).Data))
                                        {
                                            nodes2.Add(results.get_GetIndexById(Int32.Parse(indexslst)).Data);
                                            indexname = new TreeNode(results.get_GetIndexById(Int32.Parse(indexslst)).Data);
                                            indexname.Expanded = false;
                                            nodaux.ChildNodes.Add(indexname);
                                        }
                                    }
                                        //    //[Ezequiel] - Comparo si la info del indice es la misma que la del nodo donde voy a agregar el nuevo indice
                                        //    if (nodaux.Text.CompareTo(indexs.Data) == 0 && nodaux.Value.CompareTo(indexs.ID.ToString()) == 0)
                                        //    {
                                        //        //[Ezequiel] - Vuelvo a recorrer los indices
                                        //        foreach (Index indexsaux in results.Indexs)
                                        //        {
                                        //            //[Ezequiel] - Comparo que el indice sea del tipo al cual voy a agregar
                                        //            if (indexsaux.ID.ToString().CompareTo(indexslst) == 0 && !string.IsNullOrEmpty(indexsaux.Data))
                                        //            {
                                        //                //[Ezequiel] - Agrego el indice
                                        //                indexname = new TreeNode(indexsaux.Data, indexsaux.ID.ToString());
                                        //                indexname.Expanded = false;
                                        //                nodaux.ChildNodes.Add(indexname);
                                        //            }
                                        //        }
                                        //    }
                                        //}
                            //[Ezequiel] - Si es la la segunda iteracion se agregan los indices a los nodos que se agregaron en la segunda iteracion
                            else
                                //[Ezequiel] - Recorro todos los nodos que se agregaron en la primer iteracion
                                foreach (TreeNode nodaux in docname.ChildNodes)
                                    foreach (TreeNode nodaux2 in nodaux.ChildNodes)
                                        //[Ezequiel] - Recorro todos los result para agregar el valor del indice que quiero agregar
                                        foreach (Result results in mresults)
                                        //[Ezequiel] - Recorro los indices del result
                                        {
                                            List<string> indexsid = new List<string>();
                                            foreach (Index indexs in results.Indexs)
                                            {
                                                indexsid.Add(indexs.Data);
                                            }
                                            if (indexsid.Contains(nodaux.Text) && indexsid.Contains(nodaux2.Text))
                                            {
                                                indexname = new TreeNode(results.get_GetIndexById(Int32.Parse(indexslst)).Data, results.ID.ToString());
                                                indexname.Expanded = false;
                                                nodaux2.ChildNodes.Add(indexname);
                                            }
                                        }
                                    ////[Ezequiel] - Recorro todos los result para agregar el valor del indice que quiero agregar
                                    //foreach (Result results in mresults)
                                    //    //[Ezequiel] - Recorro los indices del result
                                    //    foreach (Index indexs in results.Indexs)
                                    //    {
                                    //        //[Ezequiel] - Comparo si la info del indice es la misma que la del nodo de la primer iteracion
                                    //        if (nodaux.Text.CompareTo(indexs.Data) == 0 && nodaux.Value.CompareTo(indexs.ID.ToString()) == 0)
                                    //        {
                                    //            //[Ezequiel] - Recorro todos los nodos que se agregaron en la segunda iteracion
                                    //            foreach (TreeNode nodaux2 in nodaux.ChildNodes)
                                    //                foreach (Result results2 in mresults)
                                    //                    foreach (Index indexs2 in results2.Indexs)
                                    //                    {
                                    //                        if (nodaux2.Text.CompareTo(indexs2.Data) == 0 && nodaux2.Value.CompareTo(indexs2.ID.ToString()) == 0)
                                    //                        {

                                    //                            foreach (Index indexsaux in results2.Indexs)
                                    //                            {
                                    //                                if (indexsaux.ID.ToString().CompareTo(indexslst) == 0 && !string.IsNullOrEmpty(indexsaux.Data))
                                    //                                {
                                    //                                    indexname = new TreeNode(indexsaux.Data, results2.ID.ToString());
                                    //                                    indexname.Expanded = false;
                                    //                                    nodaux2.ChildNodes.Add(indexname);
                                    //                                }
                                    //                            }
                                    //                        }
                                    //                    }

                                    //        }
                                    //    }
                        }
                    }

                    //TreeNode proc0 = new TreeNode("Instructivos");
                    //TreeNode proc1 = new TreeNode("Diagramas");
                    //TreeNode proc2 = new TreeNode("Manuales de procedimientos");
                    //proc0.Expanded = false;
                    //proc1.Expanded = false;
                    //proc2.Expanded = false;
                    //this.ProcTreeView.Nodes.Add(proc0);
                    //this.ProcTreeView.Nodes.Add(proc1);
                    //this.ProcTreeView.Nodes.Add(proc2);

                    //TreeNode proc3 = new TreeNode("Mails");
                    //TreeNode proc4 = new TreeNode("Finpro & Surety");
                    //proc3.Expanded = false;
                    //proc4.Expanded = false;
                    //proc0.ChildNodes.Add(proc3);
                    //proc0.ChildNodes.Add(proc4);

                    //TreeNode doc5 = new TreeNode("Exportacion de Mails v 1.70");
                    //doc5.Value = 276207.ToString();
                    //TreeNode proc11 = new TreeNode("Exportacion");
                    //proc11.ChildNodes.Add(doc5);

                    //TreeNode proc10 = new TreeNode("Caucion");



                    //proc3.ChildNodes.Add(proc11);
                    //proc4.ChildNodes.Add(proc10);

                    //TreeNode doc4 = new TreeNode("Ordenes de baja de cauciones");
                    //doc4.Value = 276205.ToString();
                    //proc10.ChildNodes.Add(doc4);

                    //TreeNode proc5 = new TreeNode("Affinity");
                    //proc5.Expanded = false;
                    //proc1.ChildNodes.Add(proc5);

                    //TreeNode proc6 = new TreeNode("Autos");
                    //proc6.Expanded = false;
                    //proc5.ChildNodes.Add(proc6);


                    //TreeNode doc1 = new TreeNode("Control de la documentacion");
                    //doc1.Value = 276197.ToString();
                    //proc6.ChildNodes.Add(doc1);


                    //TreeNode proc7 = new TreeNode("Vida");
                    //proc7.Expanded = false;
                    //proc2.ChildNodes.Add(proc7);

                    //TreeNode proc8 = new TreeNode("Denuncias y demandas");
                    //proc8.Expanded = false;
                    //proc7.ChildNodes.Add(proc8);




                    //TreeNode proc9 = new TreeNode("Siniestros RDT");
                    //proc9.Expanded = false;
                    //proc2.ChildNodes.Add(proc9);

                    //TreeNode proc13 = new TreeNode("Denuncias y demandas");
                    //proc13.Expanded = false;
                    //proc9.ChildNodes.Add(proc13);

                    //TreeNode doc2 = new TreeNode("Gestion de siniestros");
                    //doc2.Value = 276199.ToString();
                    //proc13.ChildNodes.Add(doc2);

                    //TreeNode doc3 = new TreeNode("Reintegros de incapacidad");
                    //doc3.Value = 276201.ToString();
                    //proc8.ChildNodes.Add(doc3);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                }
            }
        }

        private void FillGrid()
        {

        }


        protected void ProcTreeView_SelectedNodeChanged1(object sender, EventArgs e)
        {
            Int64 DocId;
            if (this.ProcTreeView.SelectedNode != null && Int64.TryParse(this.ProcTreeView.SelectedNode.Value,out DocId))
            {
                EGetDocID(DocId);
            }
        }


        //[Ezequiel] 11/02/09 Created - Delegado y evento para lanzar la id del docuementos seleccionado.
        internal delegate void GetDocId(Int64 DocId);
        internal event GetDocId EGetDocID;
    }
}