using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Zamba.Data;
using Zamba.Core;
using System.Drawing;

public partial class Controls_Related_WCDocumentsRelated : System.Web.UI.UserControl
{
    /// <summary>
    /// Evento que se ejecuta cuando se carga la página Web
    /// </summary>
    /// <history>
    /// 	[Gaston]	08/08/2008	Created
    /// </history>
    protected void Page_Load(object sender, EventArgs e)
    {
        TreeView1.Nodes.Clear();
        
    }

    public void getRelatedDocs(Int64 idDoc)
    {
        Result relatedResultFinal = new Result();

        Zamba.Core.Results_Business.getRelatedsResults(idDoc, ref relatedResultFinal);
        
        if (relatedResultFinal != null)
        {
            TreeNode rootFather = new TreeNode(relatedResultFinal.Name);

            if (relatedResultFinal.ChildsResults != null)
            {
                if (relatedResultFinal.ChildsResults.Count > 0)
                {
                    txtRel.Visible = false;
                    TreeView1.Nodes.Add(rootFather);
                    // Por cada hijo del padre raíz
                    foreach (Result relatedResult in relatedResultFinal.ChildsResults)
                    {
                        buildTree(relatedResult, rootFather, idDoc);
                    }
                    return;
                }
            }
            txtRel.Text = "No se han encontrado relaciones";
            txtRel.Visible = true;
        }
    }

    /// <summary>
    /// Método recursivo que sirve para construir el árbol que tendrá los results relacionados
    /// </summary>
    /// <history>
    /// 	[Gaston]	11/08/2008	Created
    /// </history>
    /// 
    private void buildTree(Zamba.Core.Result relatedResult, TreeNode parentNode, Int64 idDoc)
    {
        // Se agrega un nodo con el nombre del documento y el nombre de la relación 
        TreeNode resultNode = new TreeNode(relatedResult.AutoName + " - " + relatedResult.Name);

        if (relatedResult.ID == idDoc) 
        {
            
          // Corregir
           //Font  NodeFont = new Font("Times New Roman", 9, FontStyle.Bold, GraphicsUnit.Pixel);

           resultNode.Select();
        }

        // Se agrega el nodo a su padre
        parentNode.ChildNodes.Add(resultNode);

        foreach (Result relatedRes in relatedResult.ChildsResults)
        {
            buildTree(relatedRes, resultNode, idDoc);
        }
    }
}
