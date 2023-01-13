using System.Collections.Generic;
using System.Configuration;
using Marsh.Bussines;
using Zamba.Services;

namespace Marsh.Services
{
    public class FormulariosServices
    {
        private int _cant_paginas;

        public int TotalPaginas 
        {
            get { return _cant_paginas; }
            set { _cant_paginas = value; }
        }

        public FormulariosServices()
        {
            Rights.ValidateLogIn(int.Parse(ConfigurationSettings.AppSettings["zamba_user_id"].ToString()));
        }

        /// <summary>
        /// Obtiene una lista de formularios paginada
        /// </summary>
        /// <param name="pagina">Numero de pagina a obtener</param>
        /// <param name="cant_por_pagina">Cantidad de items por pagina</param>
        /// <returns>Lista de formularios</returns>
        public List<string> ListarCategorias()
        {
            return new FormularioBussines().ListarCategorias();
        }

        /// <summary>
        /// Obtiene la lista completa de formularios
        /// </summary>
        /// <returns>Lista de formularios</returns>
        public List<FormularioBussines> Listar()
        {
            return new FormularioBussines().Listar();
        }

        /// <summary>
        /// Obtiene una lista de formularios paginada
        /// </summary>
        /// <param name="pagina">Numero de pagina a obtener</param>
        /// <param name="cant_por_pagina">Cantidad de items por pagina</param>
        /// <returns>Lista de formularios</returns>
        public List<FormularioBussines> Listar(int pagina, int cant_por_pagina)
        {
            List<FormularioBussines> lista = new List<FormularioBussines>();

            lista = new FormularioBussines().Listar();

            lista = Paginar(lista, pagina, cant_por_pagina);

            return lista;
        }

        /// <summary>
        /// Buscar formularios y obtiene una lista paginada
        /// </summary>
        /// <param name="buscar">Palabra a buscar en el titulo de los formularios</param>
        /// <param name="pagina">Numero de pagina a obtener</param>
        /// <param name="cant_por_pagina">Cantidad de items por pagina</param>
        /// <returns>Lista de formularios</returns>
        public List<FormularioBussines> Buscar(string buscar, string categ, int pagina, int cant_por_pagina)
        {
            List<FormularioBussines> lista = new List<FormularioBussines>();

            lista = new FormularioBussines().Buscar(buscar, categ);
            
            lista = Paginar(lista, pagina, cant_por_pagina);

            return lista;
        }

        /// <summary>
        /// Obtiene una lista paginada de formularios en una categoria
        /// </summary>
        /// <param name="categ">Categoria del formulario</param>
        /// <param name="pagina">Numero de pagina a obtener</param>
        /// <param name="cant_por_pagina">Cantidad de items por pagina</param>
        /// <returns>Lista de formularios</returns>
        public List<FormularioBussines> Filtrar(string categ, int pagina, int cant_por_pagina)
        {
            List<FormularioBussines> lista = new List<FormularioBussines>();

            lista = new FormularioBussines().Filtrar(categ);

            lista = Paginar(lista, pagina, cant_por_pagina);

            return lista;
        }

        /// <summary>
        /// Obtiene un formulario
        /// </summary>
        /// <param name="id">id del formulario a obtener</param>
        /// <returns></returns>
        public FormularioBussines getFormularioById(int id)
        {
            return new FormularioBussines().getFormularioById(id);
        }

        private List<FormularioBussines> Paginar(List<FormularioBussines> lista, int pagina, int cant_por_pagina)
        {
            Paginador<FormularioBussines> paginador = new Paginador<FormularioBussines>(lista);

            paginador.CantidadPagina = cant_por_pagina;
            paginador.Pagina = pagina;

            lista = paginador.Paginar();

            _cant_paginas = paginador.TotalPaginas;

            return lista;
        }
    }
}