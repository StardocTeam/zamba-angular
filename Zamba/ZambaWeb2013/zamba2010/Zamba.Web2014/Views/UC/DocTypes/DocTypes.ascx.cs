using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Collections;
using System.Text;

using Zamba.Services;

public delegate void DocTypesSelected(List<Int64> docTypesIds);

public partial class DocTypes: UserControl 
{
    private DocTypesSelected _dDocTypesSelected;

    private Zamba.Core.IUser user;

    //Instanciamos el User config para extraer la última selección de usuario
    private SUserPreferences uConfig = new SUserPreferences();
    public long LastSectionId
    {
        set { Session["LastDocId"] = value; }
        get { return ((Session["LastDocId"] == null) ? 0 : (long)Session["LastDocId"]); }
    }

    public string LastSavedEntitiesIds 
    {
        set 
        {
            uConfig.setValue("WebSearchLastEntityIdsSelection", value, Zamba.Core.Sections.Viewer);
        }
        get
        {
            return uConfig.getValue("WebSearchLastEntityIdsSelection", Zamba.Core.Sections.Viewer, String.Empty);
        }
    }

    private List<Int64> LastEntitiesIdsUsed
    {
        get
        {
            if (Session["SelectedsDocTypesIds"] != null)
                return (List<Int64>)Session["SelectedsDocTypesIds"];

            List<Int64> sdtl = new List<Int64>();

            Session["SelectedsDocTypesIds"] = sdtl;
            return (List<Int64>)Session["SelectedsDocTypesIds"];
        }
        set
        {
            Session["SelectedsDocTypesIds"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        user = (Zamba.Core.IUser)Session["User"];
    }

    private class clsDocTypetoArray 
    {
        public String ID { get; set; }
        public String Name { get; set; }
    }

    public void LoadDocTypes(long UserId, int SelectedSectionId)
    {
        try
        {
            DataList1.DataSource = new SRights().GetDocTypeUserRightFromArchive(SelectedSectionId).Tables[0];
            DataList1.DataBind();

            //Refresca el ultimo tipo de archivo
            if (LastSectionId != 0 && LastSectionId != SelectedSectionId)
            {
                LastSectionId = SelectedSectionId;
            }


            //Siempre checkeará si hay un entidad seleccionado.
            CheckEntitiesToBeChecked();

            LoadIndexs();
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex );
        }
    }


  
    protected void Dt_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            e.Item.Attributes["onclick"] = "CheckClick";
        }
        catch (Exception ex)
        {
           Zamba.AppBlock.ZException.Log(ex);
        }
    }

    public void SelectDocTypeId(Int64 DocTypeId)
    {
        try
        {
            foreach (Control docTypes in DataList1.Controls)
            {
                if (DocTypeId ==  Convert.ToInt64(((TextBox)docTypes.Controls[1]).Text))
                    ((CheckBox)docTypes.Controls[3]).Checked = true;
            }
        }
        catch (Exception ex)
        {
           Zamba.AppBlock.ZException.Log(ex);
        }
    
    }

    protected void Check(object sender, EventArgs e)
    {
        try
        {
            string id = ((WebControl)sender).Attributes["dtid"].ToString();

            if (((CheckBox)sender).Checked)
            {
                if (LastEntitiesIdsUsed.Contains(Int64.Parse(id)) != true)
                {
                    LastEntitiesIdsUsed.Add(Int64.Parse(id));
                }
            }
            else
            {
                LastEntitiesIdsUsed.Remove(Int64.Parse(id));
            }
            SaveLastEntitiesidsUsed();

            LoadIndexs();
            ((UpdatePanel)((ContentPlaceHolder)this.Parent.Parent.Parent).FindControl("UpdatePanel2")).Update();
        }
        catch (Exception ex)
        {
           Zamba.AppBlock.ZException.Log(ex);
        }        
    }
    //Método creado para cargar los checks usados.
    private void SaveLastEntitiesidsUsed()
    {
        StringBuilder idsToLoad = new StringBuilder();
        foreach (long idToSave in LastEntitiesIdsUsed)
        {
            idsToLoad.Append(idToSave);
            idsToLoad.Append(',');
        }

        string idsToSave = idsToLoad.ToString();

        if (LastSavedEntitiesIds != idsToSave)
        {
            LastSavedEntitiesIds = idsToSave;
        }
    }

    public event DocTypesSelected OnDocTypesSelected
    {
        add
        {
            _dDocTypesSelected += value;
        }
        remove
        {
            _dDocTypesSelected -= value;
        }
    }

    private void LoadIndexs()
    {
        try
        {
            if (_dDocTypesSelected != null && LastEntitiesIdsUsed.Count > 0)
            { 
                _dDocTypesSelected(LastEntitiesIdsUsed);
            }
        }
        catch (Exception ex)
        {
           Zamba.AppBlock.ZException.Log(ex);
        }
    }

    /// <summary>
    /// Método que sirve para marcar los documentos seleccionados al volver a la pagina de busqueda.
    /// </summary>
    /// <history>
    /// </history>
    private void CheckDocsSelected()
    {
        try
        {
            foreach (Control docTypes in DataList1.Controls)
            {
                if (LastEntitiesIdsUsed.Contains(Convert.ToInt32(((TextBox)docTypes.Controls[1]).Text)))
                    ((CheckBox)docTypes.Controls[3]).Checked = true;
            }
        }
        catch(Exception ex)
        {
           Zamba.AppBlock.ZException.Log(ex);
        }
    }

    /// <summary>
    /// Método que sirve para marcar los documentos seleccionados al volver a la pagina de busqueda y recordar los previos checks.
    /// </summary>
    /// <history>
    /// </history>
    private void CheckEntitiesToBeChecked()
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (!String.IsNullOrEmpty(LastSavedEntitiesIds))
                {
                    LastEntitiesIdsUsed.Clear();
                    string[] previusIds = LastSavedEntitiesIds.Split(new char[] { ',' });
                    foreach (string idToSelect in previusIds)
                    {
                        if (!string.IsNullOrEmpty(idToSelect))
                        {
                            LastEntitiesIdsUsed.Add(long.Parse(idToSelect));
                        }
                    }


                    foreach (Control docTypes in DataList1.Controls)
                    {
                        if (LastEntitiesIdsUsed.Contains(Convert.ToInt32(((TextBox)docTypes.Controls[1]).Text)))
                            ((CheckBox)docTypes.Controls[3]).Checked = true;

                        if (((CheckBox)docTypes.Controls[3]).Checked == false)
                            if (LastEntitiesIdsUsed.Contains(Convert.ToInt32(((TextBox)docTypes.Controls[1]).Text)))
                                LastEntitiesIdsUsed.Remove(Convert.ToInt32(((TextBox)docTypes.Controls[1]).Text));
                    }

                }
                else
                {
                    Boolean first = true;
                    foreach (Control docTypes in DataList1.Controls)
                    {
                        if (first)
                        {
                            first = false;
                            ((CheckBox)docTypes.Controls[3]).Checked = true;

                            string id = ((WebControl)((CheckBox)docTypes.Controls[3])).Attributes["dtid"].ToString();

                            if (((CheckBox)docTypes.Controls[3]).Checked)
                            {
                                if (LastEntitiesIdsUsed.Contains(Int64.Parse(id)) != true)
                                {
                                    LastEntitiesIdsUsed.Add(Int64.Parse(id));
                                }
                            }
                            else
                            {
                                LastEntitiesIdsUsed.Remove(Int64.Parse(id));
                            }
                        }
                    }
                                            SaveLastEntitiesidsUsed();

                }
            }
            else
            {

                if (LastEntitiesIdsUsed != null)
                {
                    Boolean first = true;
                    foreach (Control docTypes in DataList1.Controls)
                    {
                        if (first)
                        {
                            first = false;
                            if (LastEntitiesIdsUsed.Count == 0)
                            {
                                ((CheckBox)docTypes.Controls[3]).Checked = true;

                            }
                        }

                        if (LastEntitiesIdsUsed.Contains(Convert.ToInt32(((TextBox)docTypes.Controls[1]).Text)))
                            ((CheckBox)docTypes.Controls[3]).Checked = true;

                        if (((CheckBox)docTypes.Controls[3]).Checked == false)
                            if (LastEntitiesIdsUsed.Contains(Convert.ToInt32(((TextBox)docTypes.Controls[1]).Text)))
                                LastEntitiesIdsUsed.Remove(Convert.ToInt32(((TextBox)docTypes.Controls[1]).Text));
                    }

                    SaveLastEntitiesidsUsed();

                }
                }
        
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }
    }

    /// <summary>
    /// Metodo que notifica si hay indices seleccionados.
    /// </summary>
    /// <returns>Devuelve true si hay, de lo contrario false.</returns>
    /// <history>
    /// </history>
    public bool GotSelectedIndexs()
    {
        bool ret = false;
        foreach (Control docTypes in DataList1.Controls)
        {
            if (((CheckBox) docTypes.Controls[3]).Checked != true) continue;
            ret = true;
            break;
        }
        return ret;
    }
}