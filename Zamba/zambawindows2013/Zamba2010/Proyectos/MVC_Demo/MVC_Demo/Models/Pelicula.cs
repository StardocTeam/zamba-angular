using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace MVC_Demo.Models
{
    public class Pelicula
    {
        private int _id;
        private string _titulo;
        private DateTime _fecha;
        private int _puntaje;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Titulo
        {
            get { return _titulo; }
            set { _titulo = value; }
        }
        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }
        public int Puntaje
        {
            get { return _puntaje; }
            set { _puntaje = value; }
        }
    }
}