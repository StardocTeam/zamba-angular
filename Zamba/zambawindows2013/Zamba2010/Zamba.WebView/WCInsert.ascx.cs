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
using Zamba.Core;
using System.Collections.Generic;


public partial class Controls_Insert_WCInsert : System.Web.UI.UserControl
{
    public event InsertEventHandler Inserted;
    public delegate void InsertEventHandler();    

    protected void btInsertar_Click(object sender, EventArgs e)
    {

        Page.Validate();
        if (Page.IsValid)
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    string pathTemp = Server.MapPath("~/temp");
                    string ext = string.Empty;
                    string file = string.Empty;
                    string originalName = string.Empty;
                    //Genera el nombre del archivo
                    ext = System.IO.Path.GetExtension(FileUpload1.FileName);
                    file = System.IO.Path.Combine(pathTemp, Guid.NewGuid().ToString() + ext);
                    originalName = System.IO.Path.GetFileName(FileUpload1.FileName);
                    //Sube el archivo
                    try
                    {
                        FileUpload1.SaveAs(file);
                    }
                    catch (System.IO.IOException ex)
                    {
                        throw ex;
                    }

                    DocType dt = null;
                    try
                    {
                        dt = DocTypesBusiness.GetDocType(Int64.Parse(DropDownList1.SelectedValue));
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    Int32 userId = Int32.MinValue;
                    if (Session["UserId"] != null)
                        userId = Int32.Parse(Session["UserId"].ToString());
                    else
                        throw new ArgumentException("El UserId debe ser distinto de nulo");

                    NewResult result = null;
                    //Agrega el archivo a zamba
                    try
                    {
                        result = Results_Business.GetNewNewResult(dt, userId, file);
                        
                        
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }


                    result.OriginalName = originalName;


                    int ubicacion=0;

                    foreach(Index  indice in completarindice.Indices)
                    {
                        result.Indexs[ubicacion] = indice;
                        ubicacion += 1;
                    }
                    
               
                    //Agrega el archivo a zamba
                    Zamba.Core.Results_Business.InsertDocument(ref result, false, false, false, false, false, false, false);
                    
                    //insertar la actualizacion de la grilla

                    this.Inserted();
                    //Luego eliminar el archivo
                    try
                    {
                        System.IO.File.Delete(file);
                    }
                    catch (System.IO.IOException ex)
                    {
                        throw ex;
                    }
                }
                else
                    throw new ArgumentException("El archivo no existe.");
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }
    }

    void Controls_Insert_WCInsert_inserted()
    {
        throw new NotImplementedException();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack  == false)
        {
            //DropDownList1.SelectedIndex = -1;
            //cargarIndices();
        }
    }
 



    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        cargarIndices();
    }

    private void cargarIndices()
    {
        NewResult CurrentResult = null;
        CurrentResult = Results_Business.GetNewNewResult(Int64.Parse(DropDownList1.SelectedValue));
        completarindice.DocTypeId = DropDownList1.SelectedValue;


        //le paso el result elegido del combo.

        List<Index> indexs = new List<Index>();

        foreach (Index currentindex in CurrentResult.Indexs)
        {
            indexs.Add(currentindex);
        }

        completarindice.Indices = indexs;  
    }





    protected void hdnDocTypeId_ValueChanged(object sender, EventArgs e)
    {

    }

   
}