using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Zamba.Core;

namespace IntranetMarsh
{
    public partial class Documents : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
          //  this.ProcedureList.EGetDocID += ShowDocSelected;
            this.LinkProcs.Click += new EventHandler(LinkProcs_Click);
            this.LinkResults.Click += new EventHandler(LinkResults_Click);
        }

        void LinkResults_Click(object sender, EventArgs e)
        {
            MView.ActiveViewIndex = 1;
            this.LinkResults.Font.Overline = true;
            this.LinkResults.Font.Underline = true;
            this.LinkProcs.Font.Overline = false;
            this.LinkProcs.Font.Underline = false;
        }

        void LinkProcs_Click(object sender, EventArgs e)
        {
            MView.ActiveViewIndex = 0;
            this.LinkResults.Font.Overline = false;
            this.LinkResults.Font.Underline = false;
            this.LinkProcs.Font.Overline = true;
            this.LinkProcs.Font.Underline = true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Search"] != null)
                SearchInDocs(Request.QueryString["Search"]);
            this.Title = "Marsh Intranet - Procedimientos";
            MakeListResult(this.CurrentResults);
            if (this.MView.ActiveViewIndex == 0)
            {
                this.LinkResults.Font.Overline = false;
                this.LinkResults.Font.Underline = false;
                this.LinkProcs.Font.Overline = true;
                this.LinkProcs.Font.Underline = true;
            }
            else
            {
                this.LinkResults.Font.Overline = true;
                this.LinkResults.Font.Underline = true;
                this.LinkProcs.Font.Overline = false;
                this.LinkProcs.Font.Underline = false;
            }
        }

        /// <summary>
        /// Metodo que recibe un DocId y lo muestra en el IFrame.
        /// </summary>
        /// <param name="DocId"></param>
        /// <history>[Ezequiel] 11/02/09 Created</history>
        private void ShowDocSelected(Int64 DocId, Int64 DocTypeId)
        {
            if (this.MView.ActiveViewIndex != 0)
            {
                this.MView.ActiveViewIndex = 0;
            }
            string asd = (Results_Business.GetResult(DocId, DocTypeId)).FullPath;
            this.PreviewDoc.Attributes.Add("src", asd);
        }


         ///<summary>
         ///Metodo que busca el texto dentro de los documentos
         ///</summary>
         ///<param name="DocId"></param>
         ///<history>[Ezequiel] 11/02/09 Created</history>
        protected void SearchInDocs(string SearchText)
        {
            Zamba.Core.DocType[] arDocTypes = new DocType[1];
            arDocTypes[0] = Zamba.Core.DocTypesBusiness.GetDocType(7240,false);
            Zamba.Core.Searchs.Search Search = new Zamba.Core.Searchs.Search(IndexsBussines.GetIndexsSchema(Convert.ToInt32(arDocTypes[0].ID)), SearchText, true, "", "", 0, arDocTypes, true, "");
            DataTable mresults  = Zamba.Core.ModDocuments.DoSearch(Search, UserBusiness.CurrentUser(), true);
            if (mresults != null && mresults.Rows.Count > 0)
            {
             //   System.Collections.Generic.List<Zamba.Core.IResult> ResultList = new System.Collections.Generic.List<Zamba.Core.IResult>();
                //foreach (DataRow R in mresults.Rows)
                //{
                //    ResultList.Add(R);
                //}
               // MakeListResult(ResultList);
               // CurrentResults = ResultList;
                MakeListResult(mresults);
                CurrentResults = mresults;
            }
            else
                CurrentResults = null;
            this.MView.ActiveViewIndex = 1;
            this.LinkResults.Font.Overline = true;
            this.LinkResults.Font.Underline = true;
            this.LinkProcs.Font.Overline = false;
            this.LinkProcs.Font.Underline = false;
        }



        /// <summary>
        /// Metodo que genera dinamicamente la tabla de resultados.
        /// </summary>
        /// <param name="DocId"></param>
        /// <history>[Ezequiel] 12/02/09 Created</history>
        private void MakeListResult(DataTable Res)
        {
            this.SearchTable.Controls.Clear();
            if (Res.Rows.Count > 0)
            {
            TableRow Srow;
            TableCell Scell;
            TableCell Scell2;
            Label Slabel;
            Button Sbutton;

            foreach (DataRow x in Res.Rows)
            {
                Srow = new TableRow();
                Scell = new TableCell();
                Scell2 = new TableCell();
                Slabel = new Label();
                Sbutton = new Button();

                Slabel.Text = x[0].ToString();
                Slabel.Font.Size = new FontUnit(FontSize.Small);
                Scell.Controls.Add(Slabel);
                Sbutton.ID = x.Table.Columns[0].ToString();
                Sbutton.Click += new EventHandler(ShowResultClicked);
                Sbutton.Font.Size = new FontUnit(FontSize.Small);
                Sbutton.Text = "Ver";
                Scell2.Controls.Add(Sbutton);
                Srow.Cells.Add(Scell);
                Srow.Cells.Add(Scell2);
                this.SearchTable.Rows.Add(Srow);
            }

            }
        }



        private void ShowResultClicked(object sender, EventArgs e)
        {
            Int64 DocTypeId = 0;
            ShowDocSelected(Convert.ToInt64(((Button)sender).ID),DocTypeId);
            this.MView.ActiveViewIndex = 0;
            this.LinkResults.Font.Overline = false;
            this.LinkResults.Font.Underline = false;
            this.LinkProcs.Font.Overline = true;
            this.LinkProcs.Font.Underline = true;
            this.PRevUPanel.Update();
        }


        private DataTable CurrentResults
        {
            get
            {
                if (Session["CurrentResults"] != null)
                    return (DataTable)Session["CurrentResults"];
                else
                {
                    DataTable mresults = new DataTable();
                    Session["CurrentResults"] = mresults;
                    return mresults;
                }
            }
            set
            {
                Session["CurrentResults"] = value;
            }
        }
    }
}
