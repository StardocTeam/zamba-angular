using System;
using System.Data;
using System.Collections.Generic;
using Marsh.Data;
using Zamba.Core;

namespace Marsh.Bussines
{
    public class UsuarioBussines
    {
        private string _nombre;
        private string _apellido;
        private string _description;
        private string _email;
        private string _interno;
        private string _sector;
        private string _cargo;
        private string _imagen;

        public string Nombre 
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public string Apellido
        {
            get { return _apellido; }
            set { _apellido = value; }
        }

        public string NombreApellido
        {
            get { return _description; }
            set { _description = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Interno 
        {
            get { return _interno; }
            set { _interno = value; }
        }

        public string Sector 
        {
            get { return _sector; }
            set { _sector = value; }
        }

        public string Cargo
        {
            get { return _cargo; }
            set { _cargo = value; }
        }

        public string Imagen
        {
            get { return _imagen; }
            set { _imagen = value; }
        }

        /// <summary>
        /// Obtiene una lista de usuarios cuyo apellido comience con el string indicado
        /// </summary>
        /// <param name="buscar">String a buscar como parte del nombre o apellido del usuario</param>
        /// <returns></returns>
        public List<UsuarioBussines> Buscar(string buscar, string buscaren)
        {
            List<UsuarioBussines> lista = new List<UsuarioBussines>();

            string[] palabras = buscar.Split(' ');

            // buscar el nombre tal cual lo ingreso el usuario
            lista = doSearch(buscar, buscaren);

            // si no hay resultados invertir las palabras y volver a buscar
            if (lista.Count == 0)
            {
                // si hay al menos dos palabras, invertirlas y buscar
                if (palabras.Length >= 2)
                {
                    lista = doSearch(palabras[1] + " " + palabras[0], buscaren);
                }
            }

            // si todavia no hay resultados buscar de a palabras sueltas 
            // y cruzar los datos
            if (lista.Count == 0)
            {
                List<UsuarioBussines> lista1 = new List<UsuarioBussines>();
                List<UsuarioBussines> lista2 = new List<UsuarioBussines>();

                lista1 = doSearch(palabras[0], buscaren);

                if (palabras.Length >= 2)
                    lista2 = doSearch(palabras[1], buscaren);

                //foreach (UsuarioBussines u1 in lista1)
                //{
                //    foreach (UsuarioBussines u2 in lista2)
                //    {
                //        if (u1.Nombre == u2.Nombre || u1.Apellido == u2.Apellido)
                //        {
                //            lista.Add(u1);
                //        }
                //    }
                //}

                foreach (UsuarioBussines u1 in lista1)
                    lista.Add(u1);
                
                foreach (UsuarioBussines u2 in lista2)
                    lista.Add(u2);
            }

            return lista;
        }

        /// <summary>
        /// Metodo privado que realiza la busqueda en la base
        /// </summary>
        /// <param name="buscar"></param>
        /// <param name="buscaren"></param>
        /// <returns></returns>
        private List<UsuarioBussines> doSearch(string buscar, string buscaren)
        {
            UsuariosData usuarios = new UsuariosData();
            List<UsuarioBussines> lista = new List<UsuarioBussines>();

            try
            {
                DataSet ds = usuarios.searchUsuarios(buscar, buscaren);

                foreach (DataRow row in ds.Tables[0].Rows)
                    lista.Add(DataRowToUsuario(row));
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            return lista;
        }

        /// <summary>
        /// Obtiene una lista de usuarios cuya inicial del apellido coincida con el char indicado
        /// </summary>
        /// <param name="inicial">Inicial del apellido a buscar</param>
        /// <returns></returns>
        public List<UsuarioBussines> ListarPorInicial(char inicial)
        {
            UsuariosData usuarios = new UsuariosData();
            List<UsuarioBussines> lista = new List<UsuarioBussines>();

            try
            {
                DataSet ds = usuarios.getUsuariosPorInicial(inicial);

                foreach (DataRow row in ds.Tables[0].Rows)
                    lista.Add(DataRowToUsuario(row));
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            return lista;
        }

        /// <summary>
        /// Obtiene los datos completos de un usuario
        /// </summary>
        /// <param name="nombre_apellido">Nombre y apellido del usuario a buscar</param>
        /// <returns></returns>
        public UsuarioBussines getUserData(string nombre_apellido)
        {
            UsuariosData usuarios = new UsuariosData();
            UsuarioBussines user = new UsuarioBussines();

            DataSet ds = usuarios.getUserData(nombre_apellido);

            user = DataRowToUsuario(ds.Tables[0].Rows[0]);

            return user;
        }

        /// <summary>
        /// Retorna una lista con todos los usuarios de la base
        /// </summary>
        /// <returns></returns>
        public List<UsuarioBussines> getUsersList()
        {
            UsuariosData usuarios = new UsuariosData();
            List<UsuarioBussines> lista = new List<UsuarioBussines>();

            try
            {
                DataSet ds = usuarios.getUsersList();

                foreach (DataRow row in ds.Tables[0].Rows)
                    lista.Add(DataRowToUsuario(row));
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            return lista;
        }

        private UsuarioBussines DataRowToUsuario(DataRow row)
        {
            UsuarioBussines usuario = new UsuarioBussines();

            usuario.Apellido = row["apellido"].ToString();
            usuario.Nombre = row["nombres"].ToString();
            usuario.NombreApellido = row["nombrecompleto"].ToString();
            usuario.Email = row["correo"].ToString();
            usuario.Sector = row["puesto"].ToString();
            usuario.Interno = row["telefono"].ToString();
            usuario.Imagen = row["foto"].ToString();
            usuario.Cargo = row["puesto"].ToString();

            if (usuario.Imagen != "")
                usuario.Imagen = IconsBussines.GetServerImagesPath() + @"\" + usuario.Imagen;

            return usuario;
        }
    }
}