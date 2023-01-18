using System;
using System.Collections.Generic;
using Zamba.Core;
using System.Configuration;
using Marsh.Data;
using System.Data;

namespace Marsh.Bussines
{
    public class FormularioBussines
    {
        private string _titulo;
        private string _descripcion;
        private string _categoria;
        private string _file;
        private DateTime _fecha;
        private int _id;

        private long _id_doc;
        private long _ind_titulo;
        private long _ind_descr;
        private long _ind_fecha;
        private long _ind_categ;

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

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        public string Categoria
        {
            get { return _categoria; }
            set { _categoria = value; }
        }

        public string File
        {
            get { return _file; }
            set { _file = value; }
        }

        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        public FormularioBussines()
        {
            try
            {
                long.TryParse(ConfigurationSettings.AppSettings["formularios_doc_id"].ToString(), out _id_doc);
                long.TryParse(ConfigurationSettings.AppSettings["formularios_ind_titulo"].ToString(), out _ind_titulo);
                long.TryParse(ConfigurationSettings.AppSettings["formularios_ind_descr"].ToString(), out _ind_descr);
                long.TryParse(ConfigurationSettings.AppSettings["formularios_ind_fecha"].ToString(), out _ind_fecha);
                long.TryParse(ConfigurationSettings.AppSettings["formularios_ind_categ"].ToString(), out _ind_categ);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return;
            }
        }

        /// <summary>
        /// Obtiene todas los formularios y solicitudes publicados
        /// </summary>
        /// <returns></returns>
        public List<FormularioBussines> Listar()
        {
            List<FormularioBussines> lista = new List<FormularioBussines>();
            FormulariosData formulario = new FormulariosData(_id_doc, _ind_categ);

            try
            {
                DataSet ds = formulario.getFormularios();

                foreach (DataRow row in ds.Tables[0].Rows)
                    lista.Add(DataRowToFormulario(row));
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            return lista;
        }
        
        /// <summary>
        /// Busca formularios
        /// </summary>
        /// <param name="buscar">String a buscar en el indice del formulario</param>
        /// <param name="indice">Indice en el cual realizar la busqueda</param>
        /// <returns></returns>
        public List<FormularioBussines> Buscar(string buscar, string categ)
        {
            List<FormularioBussines> lista = new List<FormularioBussines>();
            FormulariosData formulario = new FormulariosData(_id_doc, _ind_categ);

            try
            {
                DataSet ds = formulario.Buscar(buscar, categ, _ind_titulo);

                foreach (DataRow row in ds.Tables[0].Rows)
                    lista.Add(DataRowToFormulario(row));
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            return lista;
        }

        /// <summary>
        /// Obtiene una lista filtrada de formularios
        /// </summary>
        /// <param name="categ">Categoria por la cual filtrar</param>
        /// <param name="_ind_categ">Indice donde se guarda la categoria</param>
        /// <returns></returns>
        public List<FormularioBussines> Filtrar(string categ)
        {
            List<FormularioBussines> lista = new List<FormularioBussines>();
            FormulariosData formulario = new FormulariosData(_id_doc, _ind_categ);

            try
            {
                DataSet ds = formulario.Filtrar(categ);

                foreach (DataRow row in ds.Tables[0].Rows)
                    lista.Add(DataRowToFormulario(row));
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            return lista;
        }

        /// <summary>
        /// Obtiene un formulario
        /// </summary>
        /// <param name="id">Id del formulario a obtener</param>
        /// <returns></returns>
        public FormularioBussines getFormularioById(int id)
        {
            FormularioBussines form = new FormularioBussines();
            FormulariosData forms = new FormulariosData(_id_doc, _ind_categ);

            try
            {
                DataSet ds = forms.getFormularioById(id);

                if (ds != null)
                    form = DataRowToFormulario(ds.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            return form;
        }

        /// <summary>
        /// Obtiene una lista con todas las categorias de los formularios
        /// </summary>
        /// <returns></returns>
        public List<string> ListarCategorias()
        {
            FormulariosData forms = new FormulariosData(_id_doc, _ind_categ);
            List<string> lista = new List<string>();

            try
            {
                DataSet ds = forms.ListarCategorias();

                foreach (DataRow row in ds.Tables[0].Rows)
                    lista.Add(row["categoria"].ToString());
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            return lista;
        }

        private FormularioBussines DataRowToFormulario(DataRow row)
        {
            string file;

            FormularioBussines form = new FormularioBussines();

            file = row["DISK_VOL_PATH"].ToString();
            file += @"\";
            file += row["DOC_TYPE_ID"].ToString();
            file += @"\";
            file += row["OFFSET"].ToString();
            file += @"\";
            file += row["DOC_FILE"].ToString();

            form.Id = int.Parse(row["DOC_ID"].ToString());
            form.Titulo = row["I" + _ind_titulo].ToString();
            form.Descripcion = row["I" + _ind_descr].ToString();
            form.Categoria = row["Categoria"].ToString();
            form.File = file;
            form.Fecha = Convert.ToDateTime(row["crdate"].ToString());

            return form;
        }
    }
}
