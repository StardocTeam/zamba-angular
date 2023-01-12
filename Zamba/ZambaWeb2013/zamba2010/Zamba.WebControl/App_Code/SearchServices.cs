using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;


/// <summary>
/// Descripción breve de SearchServices
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class SearchServices : System.Web.Services.WebService {

    public SearchServices () {

        //Eliminar la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    [WebMethod]
    public Zamba.Core.Index[] GetIndexsSchema()
    {
        Int32 IdDocType = 533;

        Zamba.Core.Index[] array = Zamba.Core.Indexs_Factory.GetIndexsSchema(IdDocType);
        return array;

    }

    [WebMethod]
    public ArrayList GetComboList()
    {
        Int32 IndexId = 0;
        ArrayList list;
        list = new ArrayList();
        Object SessionIndexId = Session["IndexId"];
        if (SessionIndexId != null)
        {
            IndexId = Int32.Parse(Session["IndexId"].ToString());
             list = Zamba.Core.Indexs_Factory.GetDropDownList(IndexId);
        }
        return list;
    }
}

