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
    public partial class UserInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Marsh Intranet - Quién es quién?";
            //gvQuienEsQuien.RowCreated += new GridViewRowEventHandler(gvQuienEsQuien_RowCreated);
            if (Page.IsPostBack != true)
            {
                DataSet dsUsers = UserBusiness.GetAllMarshUsers();
                DataSet dsdptos = UserBusiness.GetDistinctMarshDptos();
            
                if (dsdptos.Tables.Count > 0)
                {
                    foreach (DataRow CurrentRow in dsdptos.Tables[0].Rows)
                    {
                        drlstDepartamento.Items.Add(CurrentRow["Sector"].ToString());
                    }
                    drlstDepartamento.Items.Add("Todos");
                    LoadgvQuienEsQuien(dsUsers);
                }
            }
            //else
            //{
            //    gvQuienEsQuien_SelectedIndexChanged();
            //}
        }



        private void LoadgvQuienEsQuien(DataSet dsUsers)
        {

    
            //dv.on
            //dv.Table = dsUsers.Tables[0];
            //gvQuienEsQuien.RowCreated += new GridViewRowEventHandler(gvQuienEsQuien_RowCreated);

            gvQuienEsQuien.DataSource = dsUsers ;
            //gvQuienEsQuien.Columns[1].Visible = false;
            gvQuienEsQuien.DataBind();
            
           

        }

        //void gvQuienEsQuien_RowCreated(object sender, GridViewRowEventArgs e)
        //{
        //    e.Row.Cells[1].Visible = false;
        //}

        protected void btnTodos_click(object sender, EventArgs e)
        { 
            DataSet dsusers= new DataSet();

            dsusers = UserBusiness.GetAllMarshUsers();
            //gvQuienEsQuien.RowCreated += new GridViewRowEventHandler(gvQuienEsQuien_RowCreated);
            gvQuienEsQuien.DataSource = dsusers;
            gvQuienEsQuien.DataBind();
        }


        /// <summary>
        /// [sebasitan 12-02-2009]Obtengo la info completa de un usuario especifico
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvQuienEsQuien_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int64 UserId, Id;

            if (Int64.TryParse(gvQuienEsQuien.SelectedRow.Cells[1].Text , out Id) == true)
            {
                UserId = Id;
                Response.Redirect("CompleteUserInfo.aspx?UserId=" + UserId);
            }
   

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataSet dsUsers = new DataSet ();

            try
            {

                dsUsers = UserBusiness.GetSpecificMarshUsers(txtNombre.Text, txtApellido.Text, drlstDepartamento.SelectedValue);
                //gvQuienEsQuien.RowCreated += new GridViewRowEventHandler(gvQuienEsQuien_RowCreated);
                gvQuienEsQuien.DataSource = dsUsers;
                gvQuienEsQuien.DataBind();
            }
            catch (Exception Ex)
            {
                btnTodos_click(null, null);//en caso de exception muestro todos
                Zamba.Core.ZClass.raiseerror(Ex);                
            }
                
                
        }

        protected void gvQuienEsQuien_RowCreated1(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
        }

     

        protected void gvQuienEsQuien_Sorting(object sender, GridViewSortEventArgs e)
        {

    

        }

    }
}
