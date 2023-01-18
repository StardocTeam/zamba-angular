using System.Collections.Generic;
using Marsh.Data;
using System.Data;
using Zamba.Core;
using System;
namespace Marsh.Bussines
{
    public class ArticuloLibreriaBussines
    {
        int _id;        
        int _cantidad;
        string _unidad;
        string _articulo;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Articulo
        {
            get { return _articulo; } 
            set { _articulo = value; } 
        }
        public int Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }

        public string Unidad
        {
            get { return _unidad; }
            set { _unidad = value; } 
        }

        public ArticuloLibreriaBussines()
        {
        }

        public List<ArticuloLibreriaBussines> Listar()
        {
            ArticuloLibreriaData arti = new ArticuloLibreriaData();
            List<ArticuloLibreriaBussines> lista = new List<ArticuloLibreriaBussines>();

            try
            {
                DataSet ds = arti.Listar();

                foreach (DataRow row in ds.Tables[0].Rows)
                    lista.Add(DataRowToArticulo(row));
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            return lista;
        }

        private ArticuloLibreriaBussines DataRowToArticulo(DataRow row)
        {
            ArticuloLibreriaBussines arti = new ArticuloLibreriaBussines();

            arti.Id = int.Parse(row["codigo"].ToString());
            arti.Articulo= row["descripcion"].ToString();

            return arti;
        }
    }
}