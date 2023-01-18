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
using System.Collections.Generic;
using Zamba.Servers;
using Zamba.Core;

namespace MVC_Demo.Models
{
    public class PeliculaDB
    {
        private List<Pelicula> dsToList(DataSet ds)
        {
            List<Pelicula> lista = new List<Pelicula>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                Pelicula peli = new Pelicula();

                peli.Id = Convert.ToInt32(row["id"]);
                peli.Titulo = row["Titulo"].ToString();
                peli.Puntaje = Convert.ToInt32(row["Puntaje"]);
                peli.Fecha = Convert.ToDateTime(row["Fecha"]);

                lista.Add(peli);
            }

            return lista;
        }

        public List<Pelicula> GetAll()
        {
            DataSet ds;
            List<Pelicula> lista = new List<Pelicula>();
            
            try
            {
                ds = Zamba.Servers.Server.get_Con(false, true, false).ExecuteDataset(CommandType.StoredProcedure, "Peliculas_TT");
                lista = dsToList(ds);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            return lista;
        }

        public Pelicula GetById(int id)
        {
            DataSet ds;
            Pelicula peli = new Pelicula();

            try
            {
                ds = Zamba.Servers.Server.get_Con(false, true, false).ExecuteDataset("Peliculas_TxId", id);

                if(ds != null)
                {
                    peli.Id = Convert.ToInt32(ds.Tables[0].Rows[0]["id"]);
                    peli.Titulo = ds.Tables[0].Rows[0]["Titulo"].ToString();
                    peli.Puntaje = Convert.ToInt32(ds.Tables[0].Rows[0]["Puntaje"]);
                    peli.Fecha = Convert.ToDateTime(ds.Tables[0].Rows[0]["Fecha"]);
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            return peli;
        }

        public List<Pelicula> Search(string titulo)
        {
            DataSet ds;
            List<Pelicula> lista = new List<Pelicula>();

            try
            {
                ds = Zamba.Servers.Server.get_Con(false, true, false).ExecuteDataset("Peliculas_TxTitulo", titulo);
                lista = dsToList(ds);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            return lista;
        }

        public void Add(Pelicula peli)
        {
            try
            {
                Zamba.Servers.Server.get_Con(false, true, false).ExecuteDataset("Peliculas_A", peli.Titulo, peli.Fecha, peli.Puntaje);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        public void Update(Pelicula peli)
        {
            try
            {
                Zamba.Servers.Server.get_Con(false, true, false).ExecuteDataset("Peliculas_U", peli.Titulo, peli.Fecha, peli.Puntaje, peli.Id);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                if (Zamba.Servers.Server.get_Con(false, true, false).ExecuteNonQuery("Peliculas_B", id)>0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return false;
            }

            return true;
        }
    }
}