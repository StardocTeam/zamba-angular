using System;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace Zamba
{
    namespace Thumbnails
    {
        public abstract class ListaImagenAbstracta : ListView
        {
            #region Atributos
            // Lista de path imagen agregados a Listaimagen...
            protected ArrayList listaPathImagenesRegistradas;

            // Lista de Tareas lanzadas al agregar imagenes..
            protected ListaTarea poolTareaAgrearImagenes;

            protected bool checkDuplicatedImage;

            #endregion End Atributos

            #region Propiedades
            /// <summary>
            /// Devuelve las imagenes 
            /// seleccionadas
            /// </summary>
            public object[] SelectedItems_
            {
                get
                {
                    object[] datos = new object[this.SelectedItems.Count];
                    int i = 0;
                    // Ordena..
                    this.listaPathImagenesRegistradas.Sort();
                    foreach (ListViewItem item in SelectedItems)
                    {
                        // Busca...
                        int index =
                        this.listaPathImagenesRegistradas
                        .BinarySearch(
                            new KeyReg(item.ImageKey, null, null,null));
                        datos[i] =
                          ((KeyReg)this.listaPathImagenesRegistradas[index]).Dato;
                        i++;
                    }
                    return datos;
                }
            }

            /// <summary>
            /// Asigna o devuelve si se chequean 
            /// imagenes duplicadas
            /// </summary>
            public virtual bool CheckDuplicateImage
            {
                set
                {
                    this.checkDuplicatedImage = value;
                }
                get
                {
                    return this.checkDuplicatedImage;
                }
            }




            #endregion End Propiedades


            public ListaImagenAbstracta(int maxNumeroPedidoAdd)
            {
                this.listaPathImagenesRegistradas
                    = new ArrayList();
                this.poolTareaAgrearImagenes =
                        new ListaTarea(maxNumeroPedidoAdd);

                this.checkDuplicatedImage = true;
            }


            /// <summary>
            /// Metodo llamado por un thread, para,
            /// agregar imagenes a la lista con un cierto
            /// delay de carga
            /// </summary>
            protected abstract void agregarImagenes(
                Tarea tarea, ArrayList parametros);

            /// <summary>
            /// Toma el texto de una imagen
            /// </summary>
            /// <remarks>
            ///     Segun que tipo de objeto sea
            ///     topa el texto corespondiente
            ///     a su imagen.
            /// </remarks>
            /// <param name="item">object</param>
            /// <returns>texto de la imagen</returns>
            protected abstract KeyReg getKeyReg(object item);



            #region Metodos Publicos

            public ListaImagenAbstracta()
            {
                this.listaPathImagenesRegistradas
                    = new ArrayList();
                this.poolTareaAgrearImagenes =
                new ListaTarea(
                 Constante.ListaTarea.DEFAULT_MAX_COUNT_LISTA_TAREAS);

                this.Sorting = SortOrder.Ascending;
            }
            /// <summary>
            /// Agrega imagenes segun path/Result
            /// </summary>
            /// <param name="list">ArrayList path/Result</param>
            public virtual void add(ArrayList list)
            {
                // valida la referencia...
                if (null == list || 0 == list.Count) return;

                // registrar imagenes que no esten en la lista...                      
                ArrayList listaNuenosPath =
                     this.registrarPathImagen(list);

                // agrega el texto de la imagen
                this.addItemText(listaNuenosPath);

                /* Lanza un thread... hata 10 max..
                 * nota: Estos threads solo agregan 
                 * imagenes nuevas */
                if (!this.poolTareaAgrearImagenes.isFill())
                {
                    Tarea tarea = new Tarea(
                        (ArrayList)listaNuenosPath.Clone(),
                        this.agregarImagenes
                    );
                    tarea.start();
                    this.poolTareaAgrearImagenes.Add(tarea);
                }
            }

            public virtual new void Clear()
            {
                try {
                    if (this.poolTareaAgrearImagenes.Count > 0) {
                        this.poolTareaAgrearImagenes.abortAll();
                        this.poolTareaAgrearImagenes.Clear();
                    }
                } catch { }
                
                if (null != this.LargeImageList &&
                    this.LargeImageList.Images.Count > 0)
                    this.LargeImageList.Images.Clear();
                this.Items.Clear();
                this.listaPathImagenesRegistradas.Clear();                                             
            }
            #endregion End Metodos Publicos





            #region  Metodos Protegidos

            /// <summary>
            /// Selecciona un item de la lista
            /// </summary>
            /// <param name="value">
            ///     objeto contemplado por el metodo
            ///     getKeyReg
            /// </param>
            /// <returns>true si pudo se seleccionarlo</returns>
            protected virtual bool SelectThis(object value)
            {
                // Ordena..
                this.listaPathImagenesRegistradas.Sort();

                // Busca...
                int index =
                    this.listaPathImagenesRegistradas
                       .BinarySearch(this.getKeyReg(value));

                if (0 > index) return false;

                this.Focus();
                this.Items[index].Selected = true;

                return true;
            }


            /// <summary>
            /// Agrega path para cada imagen
            /// Sebastian 15-07-09 se agrego condicion para salvar exception
            /// </summary>
            /// <param name="path">path</param>
            protected virtual void addItemText(ArrayList list)
            {
                //list.Sort();
                foreach (KeyReg _item in list)
                {
                 //TODO exception para verificar,null reference.Comentado por marcos  
                    //[Sebstian 15-07-09] se agrego condición para salvar exception
                    //[Tomas]   24-07-2009  Modified    Se modifica la validación del objeto _item
                    //                                  ya que no funcionaba como debía.
                    if (_item != null)
                        //if(!string.IsNullOrEmpty(_item.Text))
                        this.Items.Add(_item.Text, _item.Key);
                }
            }



            /// <summary>
            /// Registra las imagenes agregadas con el metodo
            /// addPathList por el usuario
            /// </summary>
            /// <param name="lista">lista</param>
            /// <returns>Imagenes ya registradas</returns>
            protected virtual ArrayList
                registrarPathImagen(ArrayList lista)
            {
                /* Si la lista es la lista de imagenes registradas 
                 * actual... */
                if (lista.Equals(this.listaPathImagenesRegistradas))
                    return this.listaPathImagenesRegistradas;

                ArrayList ListaImagenesNoRegitradas = new ArrayList();
                IEnumerator iterador = lista.GetEnumerator();
                iterador.Reset();
                while (iterador.MoveNext())
                {
                    KeyReg reg = this.getKeyReg(iterador.Current);

                    // si no esta registrada la imagen...
                    if (0 > this.listaPathImagenesRegistradas.
                        IndexOf(reg))
                    {
                        this.listaPathImagenesRegistradas.Add(reg);
                        ListaImagenesNoRegitradas.Add(reg);
                    }
                }
                return ListaImagenesNoRegitradas;
            }


            /// <summary>
            /// Libera los recursos tomados por LsitaImagen
            /// </summary>
            /// <param name="disposing">booleano</param>
            protected override void Dispose(bool disposing)
            {
                this.poolTareaAgrearImagenes.abortAll();
                this.poolTareaAgrearImagenes.Clear();
                base.Dispose(disposing);
            }



            /// <summary>
            /// Solo borra las imagenes de la vista
            /// solamente
            /// </summary>
            protected virtual void borrarVista()
            {
                this.poolTareaAgrearImagenes.abortAll();
                this.poolTareaAgrearImagenes.Clear();
                this.LargeImageList.Images.Clear();
                this.Items.Clear();
            }


            /// <summary>
            /// Inicializa la lista de imagenes
            /// </summary>
            protected virtual void inicializarImageList(Size size)
            {
                this.LargeImageList = new ImageList();
                this.LargeImageList.ImageSize = size;
                this.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
            }

            #endregion End Metodos Protegidos


        }
    }
}