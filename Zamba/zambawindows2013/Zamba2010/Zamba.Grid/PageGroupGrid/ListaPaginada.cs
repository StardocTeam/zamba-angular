using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using zamba.collections;
namespace Zamba.Grid.PageGroupGrid
{
    /// <summary>
    /// Pagina los items 
    /// que se agreguen a esta 
    /// segun numero de items x pagina
    /// </summary>
    /// <remarks>
    ///  Es una linked list de dos niveles.
    ///  "lista paginada" (0)..(1)....(n) "Nivel 1"
    ///                    |    |      .
    ///                   (0)  (0)     .
    ///                    |    |      .  "Nivel 2"
    ///                   (1)   .
    ///                    .    .
    ///                    .
    ///                   (n)
    /// 
    /// </remarks>
    public class ListaPaginada : IPageList
    {
        #region Atributos
        protected int pageSize;
        protected int currentPage;

        protected ArrayList pages;
        #endregion End Atributos

        #region Propiedades

        public int ItemsCount
        {
            get
            {
                int cantidad = 0;
                if (this.isEmpty()) return ++cantidad;

                foreach (ArrayList page in this.pages)
                    cantidad += page.Count;
                return cantidad;
            }
        }


        /// <summary>
        /// Devuelve el numero
        /// de paginas
        /// </summary>
        public int PageCount
        {
            get
            {
                if (isEmpty())
                    return 0;

                return this.pages.Count;
            }
        }


        /// <summary>
        /// Devuelve el numero maximo de 
        /// objetos por pagina
        /// </summary>
        public int PageSize
        {
            get
            {
                return this.pageSize;
            }
        }

        /// <summary>
        /// Devuelve el atributo de la pagina actual
        /// devuelta por los metodos Home,Bask,Next,End
        /// </summary>
        public int CurrentPageIndex
        {
            get
            {
                return this.currentPage + 1;
            }
        }

        /// <summary>
        /// Devuelve indice del primer elemento
        /// de la pagina
        /// </summary>
        public int CurrentPageFirtItemIndex
        {
            get
            {
                // Si es la primara pagina...
                if (1 == this.CurrentPageIndex)
                    return 1;

                return ((this.CurrentPageIndex - 1) *
                          this.pageSize) + 1;
            }
        }

        /// <summary>
        /// Devuelve indice del primer elemento
        /// de la pagina
        /// </summary>
        public int CurrentPageLastItemIndex
        {
            get
            {
                return this.CurrentPageIndex *
                        this.getCurrentPage().Count;
            }
        }




        #endregion Propiedades

        #region Constructores

        /// <summary>
        /// Constructor
        /// </summary>
        public ListaPaginada()
        {
            this.inicializar(
            Constant.ListaPaginada.PAGE_SIZE);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pageSize">
        ///     numero de items por pagina
        /// </param>
        public ListaPaginada(int pageSize)
        {
            this.inicializar(pageSize);
        }

        #endregion End Constructores

        #region Metodos Publicos




        /// <summary>
        /// Pregunta si existe el objeto
        /// en la listaPaginada
        /// </summary>
        /// <param name="value">objeto</param>
        /// <returns>booleano</returns>
        public bool existThis(Object value)
        {
            if (this.isEmpty()) return false;

            foreach (ArrayList page in this.pages)
            {
                foreach (object item in page)
                {
                    if (item.Equals(value))
                        return true;
                }
            }

            return false;
        }


        /// <summary>
        /// Devuelve los item de la pagina
        /// actualmente seleccionada
        /// </summary>
        /// <returns>Lista de items</returns>
        public ArrayList getCurrentPage()
        {
            return this.getPage(this.currentPage);
        }






        /// <summary>
        /// Agrega una lista de objeto a 
        /// la coleccion
        /// </summary>
        /// <param name="value">lista de objeto</param>
        public virtual void AddRange(ArrayList value)
        {
            if (null == value) return;

            foreach( object item in value )
                if (!this.existThis(item)) this.Add(item);
        }

        /// <summary>
        /// Vacia la lista de paginas
        /// </summary>
        public virtual void clear()
        {
            this.inicializar(this.pageSize);
        }


        /// <summary>
        /// Devuelde una lista de objetos 
        /// perteneciente a la primera pagina
        /// </summary>
        /// <param name="value">lista de objeto</param>
        public virtual ArrayList home()
        {
            this.currentPage = 0;
            return this.getPage(this.currentPage);
        }


        /// <summary>
        /// Devuelde una lista de objetos 
        /// perteneciente a la ultima pagina
        /// </summary>
        /// <param name="value">lista de objeto</param>
        public virtual ArrayList end()
        {
            if (isEmpty())
                this.currentPage = 0;
            else
                this.currentPage = this.pages.Count - 1;

            return this.getPage(this.currentPage);
        }


        /// <summary>
        /// Devuelde una lista de objetos 
        /// perteneciente a la pagina anterior
        /// a la cargada actualmente
        /// </summary>
        /// <param name="value">lista de objeto</param>
        public virtual ArrayList back()
        {
            if (isEmpty()) return null;

            if (this.currentPage > 0)
                return this.getPage(--this.currentPage);
            else
                return this.getCurrentPage();
        }



        /// <summary>
        /// Devuelde una lista de objetos 
        /// perteneciente a la pagina siguiente
        /// a la cargada actualmente
        /// </summary>
        /// <param name="value">lista de objeto</param>
        public virtual ArrayList next()
        {
            if (isEmpty()) return null;

            if (this.currentPage < this.pages.Count - 1)
                return this.getPage(++this.currentPage);
            else
                return this.getCurrentPage();
        }

        /// <summary>
        /// Devuelve una pagina
        /// </summary>
        /// <param name="index">indice de pagina</param>
        /// <param name="value">lista de objeto</param>
        public virtual ArrayList getPage(int index)
        {
            if (isEmpty())
                return null;

            if (index > (this.pages.Count - 1)
                || index < 0)
                return null;


            this.currentPage = index;
            return (ArrayList)this.pages[index];
        }


        /// <summary>
        /// Devuelve la pagina por indice
        /// </summary>
        /// <param name="index">numero de pagina</param>
        /// <returns>Lista de objetos</returns>
        public virtual ArrayList this[int index]
        {
            get { return this.getPage(index); }
        }


        /// <summary>
        /// Pregunta si esta vacia la lista
        /// </summary>
        /// <returns>booleano</returns>
        public virtual bool isEmpty()
        {
            if (null == this.pages)
                return true;

            if (0 == this.pages.Count)
                return true;

            return false;
        }




        #endregion Metodos Publicos

        #region Metodos Protegidos
        /// <summary>
        /// Toma la pagina actua a llenar
        /// </summary>
        /// <remarks> 
        ///  si esta llena o no hay paginas
        ///  => agrega una nueva pagina 
        ///  y devuelve una ref a esta.
        /// </remarks>
        /// <returns>pagina actual a llenar</returns>
        protected virtual ArrayList getCurrentAddPage()
        {
            // Si no hay paginas...
            if (this.isEmpty())
                return this.AddEmptyPage();

            // Tomo la ultima pagina...
            ArrayList page =
                (ArrayList)this.pages[this.pages.Count - 1];

            // si la pagina esta llena...
            if (page.Count >= this.pageSize)
                return this.AddEmptyPage();

            return page;
        }


        /// <summary>
        /// Devuelve una pagina nueva
        /// </summary>
        /// <returns>nueva pagina</returns>
        protected virtual ArrayList AddEmptyPage()
        {
            // Si no hay una ref. a pages...
            if (this.isEmpty())
                this.pages = new ArrayList();

            ArrayList newPage = new ArrayList();
            this.pages.Add(newPage);
            return newPage;
        }

        #endregion End Metodos Protegidos

        #region Metodos Privados
        /// <summary>
        /// Agrega un objeto a la coleccion
        /// </summary>
        /// <param name="value">objeto</param>
        private void Add(object value)
        {
            this.getCurrentAddPage().Add(value);
        }

        /// <summary>
        /// Inicializa la lista
        /// </summary>
        /// <param name="pageSize">
        ///     numero de items por pagina
        /// </param>
        private void inicializar(int pageSize)
        {
            this.pageSize = pageSize;
            this.currentPage = 0;

            if (null != this.pages)
                this.pages.Clear();
        }






        #endregion End Metodos Privados

    }
}
