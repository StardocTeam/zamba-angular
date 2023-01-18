using Zamba.Core;
using Zamba.Data;
using System.Collections.Generic;
using System;
using System.Web.UI.WebControls;
using System.Web.UI;

public partial class WebUserControl
    : UserControl
{
    public List<Index> Indices
    {
        get
        {
            List<Int64> Indices = new List<Int64>(gvCompletarIndice.Rows.Count);           
            List<Index> ListOfIndex =  DocTypesBusiness.GetIndexsListByDocTypeId(Int64.Parse(hdnDoctypeIdRecibe.Value));
            Int64 rowIndex;


           foreach (GridViewRow CurrentRow in gvCompletarIndice.Rows)
           {
               rowIndex=Int64.Parse(CurrentRow.Cells[0].Text);

               foreach (Index IndexOfDt in ListOfIndex )
               {    
                   if (rowIndex  == IndexOfDt.ID)
                   {

                       IndexOfDt.DataTemp = ((TextBox)CurrentRow.Cells[2].Controls[1]).Text ;
                       IndexOfDt.Data = ((TextBox)CurrentRow.Cells[2].Controls[1]).Text;
                       break;
                   }
               }         
               
           }        

            return ListOfIndex;
        }
        set
        {
            gvCompletarIndice.DataSource = null;

            gvCompletarIndice.DataSource = value;
            gvCompletarIndice.DataBind();

            Int32 BaseIndex = gvCompletarIndice.PageSize * gvCompletarIndice.PageIndex;
            Int32 CurrentIndex;
            Zamba.Core.Index Index = null;
            GridViewRow CurrentRow = null;
            if (value.Count > 0)

                for (int i = 0; i < gvCompletarIndice.PageSize; i++)
                {
                    CurrentIndex = BaseIndex + i;
                    if (CurrentIndex <= value.Count && i < gvCompletarIndice.Rows.Count)
                    {
                        Index = value[CurrentIndex];
                        CurrentRow = gvCompletarIndice.Rows[i];

                        CurrentRow.Cells[0].Text = Index.ID.ToString();
                        CurrentRow.Cells[1].Text = Index.Name;
                        ((TextBox)CurrentRow.FindControl("txtIndex_value")).Text = Index.DataTemp;
                    }
                }
        }
    }


    public string DocTypeId
    {

        get
        {
            return hdnDoctypeIdRecibe.Value;
        }

        set
        {

            hdnDoctypeIdRecibe.Value  = value;
            
        }

    }


    protected void gvCompletarIndice_PageIndexChanged(object sender, EventArgs e)
    {

    }

    protected void gvCompletarIndice_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCompletarIndice.PageIndex = e.NewPageIndex;

        List<Index> ListOfIndex = DocTypesBusiness.GetIndexsListByDocTypeId(Int64.Parse(hdnDoctypeIdRecibe.Value));
        gvCompletarIndice.DataSource = ListOfIndex ;
        gvCompletarIndice.DataBind();


        Int32 BaseIndex = gvCompletarIndice.PageSize * gvCompletarIndice.PageIndex;
        Int32 CurrentIndex;
        Zamba.Core.Index Index = null;
        GridViewRow CurrentRow = null;
        if (ListOfIndex.Count > 0)

            for (int i = 0; i < gvCompletarIndice.PageSize; i++)
            {
                CurrentIndex = BaseIndex + i;
                if (CurrentIndex <= ListOfIndex.Count  && i < gvCompletarIndice.Rows.Count)
                {
                    Index = ListOfIndex [CurrentIndex];
                    CurrentRow = gvCompletarIndice.Rows[i];

                    CurrentRow.Cells[0].Text = Index.ID.ToString();
                    CurrentRow.Cells[1].Text = Index.Name;
                    ((TextBox)CurrentRow.FindControl("txtIndex_value")).Text = Index.DataTemp;
                }
            }
    }


}
