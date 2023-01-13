using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using Marsh.Data;
using Zamba.Core;
using System.IO;
using System.Security.Principal;
using System.Runtime.InteropServices;

namespace Marsh.Bussines
{
    public class NoticiaBussines
    {
        private int _id;
        private string _titulo;
        private string _noti_corta;
        private string _noti_larga;
        private string _noti_categoria;
        private string _noti_imagen;
        private string _noti_imagen_filename;
        private string _noti_imagen_path;
        private DateTime _fecha;

        private long _id_doc;
        private long _ind_titulo;
        private long _ind_descr_corta;
        private long _ind_descr_larga;
        private long _ind_categ;
        private long _ind_imagen;

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

        public string Imagen
        {
            get { return _noti_imagen; }
            set { _noti_imagen = value; }
        }

        public string ImagenFileName
        {
            get { return _noti_imagen_filename; }
            set { _noti_imagen_filename = value; }
        }

        public string ImagenPath
        {
            get { return _noti_imagen_path; }
            set { _noti_imagen_path = value; }
        }

        public string NoticiaCorta
        {
            get { return _noti_corta; }
            set { _noti_corta = value; }
        }

        public string NoticiaLarga
        {
            get { return _noti_larga; }
            set { _noti_larga = value; }
        }

        public string Categoria
        {
            get { return _noti_categoria; }
            set { _noti_categoria = value; }
        }

        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        public NoticiaBussines()
        {
            try
            {
                long.TryParse(ConfigurationSettings.AppSettings["noticia_doc_id"].ToString(), out _id_doc);
                long.TryParse(ConfigurationSettings.AppSettings["noticia_ind_titulo"].ToString(), out _ind_titulo);
                long.TryParse(ConfigurationSettings.AppSettings["noticia_ind_descr_corta"].ToString(), out _ind_descr_corta);
                long.TryParse(ConfigurationSettings.AppSettings["noticia_ind_descr_larga"].ToString(), out _ind_descr_larga);
                long.TryParse(ConfigurationSettings.AppSettings["noticia_ind_categ"].ToString(), out _ind_categ);
                long.TryParse(ConfigurationSettings.AppSettings["noticia_ind_imagen"].ToString(), out _ind_imagen);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return;
            }
        }

        /// <summary>
        /// Obtiene un listado con las ultimas noticias publicadas
        /// </summary>
        /// <param name="cant">Cantidad de noticias a obtener</param>
        /// <returns></returns>
        public List<Marsh.Bussines.NoticiaBussines> Listar()
        {
            List<Marsh.Bussines.NoticiaBussines> lista = new List<Marsh.Bussines.NoticiaBussines>();
            NoticiasData noticias = new NoticiasData(_id_doc, _ind_categ);

            try
            {
                DataSet ds = noticias.getNoticias();

                foreach (DataRow row in ds.Tables[0].Rows)
                    lista.Add(DataRowToNoticia(row));
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            return lista;
        }

        /// <summary>
        /// Obtiene una noticia
        /// </summary>
        /// <param name="id">Id de la noticia a obtener</param>
        /// <returns></returns>
        public Marsh.Bussines.NoticiaBussines getNoticiaById(int id)
        {
            Marsh.Bussines.NoticiaBussines noti = new Marsh.Bussines.NoticiaBussines();
            NoticiasData noticias = new NoticiasData(_id_doc, _ind_categ);

            try
            {
                DataSet ds = noticias.getNoticiaById(id);

                if (ds != null)
                    noti = DataRowToNoticia(ds.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            return noti;
        }

        private NoticiaBussines DataRowToNoticia(DataRow row)
        {
            string file_name = "";
            string file_path = "";

            NoticiaBussines noti = new NoticiaBussines();

            noti.Id = int.Parse(row["doc_id"].ToString());

            if (row["DOC_FILE"].ToString() != "")
            {
                // path del archivo
                file_path = row["DISK_VOL_PATH"].ToString();
                file_path += @"\";
                file_path += row["DOC_TYPE_ID"].ToString();
                file_path += @"\";
                file_path += row["OFFSET"].ToString();

                // nombre del archivo
                file_name = row["DOC_FILE"].ToString();

                noti.ImagenFileName = file_name;
                noti.ImagenPath = file_path;

                noti.Imagen = noti.ImagenPath + @"\" + noti.ImagenFileName;
            }

            // copia la imagen desde el volumen a una carpeta local
            // noti.Imagen = CopiaLocal(noti);

            noti.Fecha = Convert.ToDateTime(row["crdate"].ToString());
            noti.Titulo = row["I" + _ind_titulo].ToString();

            if(row["I" + _ind_descr_larga].ToString() != "")
                noti.NoticiaLarga = row["I" + _ind_descr_larga].ToString().Replace("\n", "<br>");

            if (row["I" + _ind_descr_corta].ToString() != "")
                noti.NoticiaCorta = row["I" + _ind_descr_corta].ToString().Replace("\n", "<br>");

            noti.Categoria = row["Categoria"].ToString();

            return noti;
        }

        /// <summary>
        /// Copia la imagen a una carpeta local de modo que no se enlacen al volumen
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private string CopiaLocal(NoticiaBussines noticia)
        {
            string img_folder;
            string newfile = "";

            img_folder = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"imgs\noticias";

            // si no existe la carpeta destino la crea
            if (!Directory.Exists(img_folder))
            {
                try
                {
                    Directory.CreateDirectory(img_folder);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                }
            }

            try
            {
                newfile = img_folder + @"\" + noticia.ImagenFileName;

                if (!File.Exists(newfile))
                {
                    System.Security.SecureString password = new System.Security.SecureString();

                    // se utiliza una herramienta externa (robocopy.exe)
                    // para copiar los archivos dado que los metodos de .net
                    // fallaban por problemas con los permisos cuando el archivo
                    // esta compartido mediante la red
                    System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();

                    info.FileName = AppDomain.CurrentDomain.BaseDirectory.ToString() + "robocopy.exe";
                    info.Arguments = noticia.ImagenPath + " " + img_folder + " " + noticia.ImagenFileName + @" > c:\" + noticia.ImagenFileName + ".txt";
                    info.CreateNoWindow = true;
                    info.UseShellExecute = false;

                    System.Diagnostics.Process.Start(info);
                }
    
                // la nueva imagen esta copiada localmente
                newfile = @"/imgs/noticias/" + noticia.ImagenFileName;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);

                // si no se pudo copiar el archivo devuelve la imagen desde el volumen
                newfile = "file:///" + noticia.ImagenPath + @"\" + noticia.ImagenFileName;
            }

            return newfile;
        }
    }
}