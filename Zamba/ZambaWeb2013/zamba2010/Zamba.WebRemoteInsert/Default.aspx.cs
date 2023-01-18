using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using Zamba;


public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
      

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string destDir = Server.MapPath("./Upload");         
            string fileName = Path.GetFileName(Upload.PostedFile.FileName);         
            string destPath = Path.Combine(destDir, fileName);

            mUpload(destPath);
            List<Zamba.Core.Index> indexs = new List<Zamba.Core.Index>();
            int index_id=-1;
            string index_value="";
            string index_name="";
            int index_type=-1;
            int index_len=-1;
            short index_dropdown=-1;
            foreach(DataListItem item in this.DataList1.Items)
            {
                index_id = -1;
                index_value = "";
                index_name = "";
                index_type = -1;
                index_len = -1;
                index_dropdown = -1;

                foreach (Control c in item.Controls)
                {
                    if (c.ID != null)
                    {
                        try
                        {
                            switch (c.ID)
                            {
                                case "Index_Name":
                                    index_name = ((System.Web.UI.WebControls.Label)c).Text.Trim();
                                    break;
                                case "Index_Value":
                                    index_value = ((System.Web.UI.WebControls.TextBox)c).Text.Trim();
                                    break;
                                case "Index_Id":
                                    index_id = Convert.ToInt32(((System.Web.UI.WebControls.HiddenField)c).Value);
                                    break;
                                case "Index_Type":
                                    index_type = Convert.ToInt32(((System.Web.UI.WebControls.HiddenField)c).Value);
                                    break;
                                case "Index_Len":
                                    index_len = Convert.ToInt32(((System.Web.UI.WebControls.HiddenField)c).Value);
                                    break;
                                case "Index_DropDown":
                                    index_dropdown = Convert.ToInt16(((System.Web.UI.WebControls.HiddenField)c).Value);
                                    break;
                            }
                        }
                        catch
                        { 

                        }
                    }
                }
                if(index_id != -1)
                    indexs.Add(new Zamba.Core.Index(index_id,index_name,index_type,index_len,false,false,index_dropdown,false,false,0));
            }
            
            Zamba.WebLibrary.WebLibrary.AgregarAZamba(Int32.Parse(cboDocType.SelectedValue), destPath);

        }
        catch (Exception ex)
        { 
            lbStatus.Text = ex.Message;
        }
   }
    
    private void mUpload(String destPath)
    { 
        try
        {      
            if (Upload.PostedFile.ContentLength != 0)
            {
                try
                {   
                               
                    Upload.PostedFile.SaveAs(destPath);
                    lbStatus.Text = "Uploading....";
                }
                catch (Exception ex)
                {
                    lbStatus.Text = ex.Message;
                }
                lbStatus.Text = "Upload Complete."; 
            }            
        }
        catch (System.Exception ex)
        {
            lbStatus.Text = ex.Message;
        }
    }

    protected void DetailsView1_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
    {

    }
}
