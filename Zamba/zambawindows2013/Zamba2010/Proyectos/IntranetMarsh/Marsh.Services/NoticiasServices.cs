using System.Collections.Generic;
using System.Configuration;
using Marsh.Bussines;
using Zamba.Services;

namespace Marsh.Services
{
    public class NoticiasServices
    {
        private int _cant_paginas;

        public int TotalPaginas 
        {
            get { return _cant_paginas; }
            set { _cant_paginas = value; }
        }

        public NoticiasServices()
        {
            string a = Zamba.Core.ServersBusiness.BuildExecuteScalar(System.Data.CommandType.Text, "SELECT '1' FROM dual").ToString();
            Rights.ValidateLogIn(int.Parse(ConfigurationSettings.AppSettings["zamba_user_id"].ToString()));
        }

        /// <summary>
        /// Obtiene la lista completa de noticias
        /// </summary>
        /// <returns>Lista de noticias</returns>
        public List<NoticiaBussines> Listar()
        {
            return new NoticiaBussines().Listar();
        }

        /// <summary>
        /// Obtiene una lista de noticias paginada
        /// </summary>
        /// <param name="pagina">Numero de pagina a obtener</param>
        /// <param name="cant_por_pagina">Cantidad de items por pagina</param>
        /// <returns>Lista de noticias</returns>
        public List<NoticiaBussines> Listar(int pagina, int cant_por_pagina)
        {
            List<NoticiaBussines> lista = new List<NoticiaBussines>();

            lista = new NoticiaBussines().Listar();
            
            Paginador<NoticiaBussines> paginador = new Paginador<NoticiaBussines>(lista);

            paginador.CantidadPagina = cant_por_pagina;
            paginador.Pagina = pagina;
            
            lista = paginador.Paginar();

            _cant_paginas = paginador.TotalPaginas;

            return lista;
        }       

        /// <summary>
        /// Obtiene una noticia en particular
        /// </summary>
        /// <param name="id">Id de la noticia a obtener</param>
        /// <returns></returns>
        public NoticiaBussines getNoticiaById(int id)
        {
            int _zamba_user_Id = int.Parse(ConfigurationSettings.AppSettings["zamba_user_id"].ToString());

            Rights.ValidateLogIn(_zamba_user_Id);

            return new NoticiaBussines().getNoticiaById(id);
        }
    }
}
