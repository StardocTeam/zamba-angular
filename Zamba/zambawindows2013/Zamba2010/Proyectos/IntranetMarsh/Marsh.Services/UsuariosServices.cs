using System.Collections.Generic;
using System.Configuration;
using Marsh.Bussines;
using Zamba.Services;

namespace Marsh.Services
{
    public class UsuariosServices
    {
        private int _cant_paginas;

        public int TotalPaginas
        {
            get { return _cant_paginas; }
            set { _cant_paginas = value; }
        }

        public UsuariosServices()
        {
            Rights.ValidateLogIn(int.Parse(ConfigurationSettings.AppSettings["zamba_user_id"].ToString()));
        }

        /// <summary>
        /// Obtiene una lista de usuarios cuyo apellido comience con el string indicado
        /// </summary>
        /// <param name="buscar">String a buscar como comiezo del apellido</param>
        /// <returns></returns>
        public List<UsuarioBussines> Buscar(string buscar, string buscaren, int pagina, int cant_por_pagina)
        {
            List<UsuarioBussines> lista = new List<UsuarioBussines>();

            lista = new UsuarioBussines().Buscar(buscar, buscaren);

            lista = Paginar(lista, pagina, cant_por_pagina);

            return lista;
        }

        /// <summary>
        /// Obtiene una lista de usuarios cuya inicial del apellido coincida con el char indicado
        /// </summary>
        /// <param name="inicial">Inicial del apellido a buscar</param>
        /// <returns></returns>
        public List<UsuarioBussines> ListarPorInicial(char inicial, int pagina, int cant_por_pagina)
        {
            List<UsuarioBussines> lista = new List<UsuarioBussines>();

            lista = new UsuarioBussines().ListarPorInicial(inicial);

            lista = Paginar(lista, pagina, cant_por_pagina);

            return lista;
        }

        /// <summary>
        /// Obtiene los datos de un usuario
        /// </summary>
        /// <param name="inicial">Nombre y apellido a buscar</param>
        /// <returns></returns>
        public UsuarioBussines getUserData(string nombre_apellido)
        {
            return new UsuarioBussines().getUserData(nombre_apellido);
        }

        /// <summary>
        /// Retorna una lista con todos los usuarios de la base
        /// </summary>
        /// <returns></returns>
        public List<UsuarioBussines> GetUsersArrayList()
        {
            return new UsuarioBussines().getUsersList();
        }

        private List<UsuarioBussines> Paginar(List<UsuarioBussines> lista, int pagina, int cant_por_pagina)
        {
            Paginador<UsuarioBussines> paginador = new Paginador<UsuarioBussines>(lista);

            paginador.CantidadPagina = cant_por_pagina;
            paginador.Pagina = pagina;

            lista = paginador.Paginar();

            _cant_paginas = paginador.TotalPaginas;

            return lista;
        }
    }
}