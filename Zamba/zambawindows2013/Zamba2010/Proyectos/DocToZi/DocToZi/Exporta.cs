using System.Windows.Forms;
using System;
using System.IO;
using Zamba.Servers;
using System.Text;
using System.Data;
using Zamba.Core;
using System.Collections.Generic;


namespace DocToZi
{
    class Exporta : IDisposable 
    {
        #region Atributos
        private List<Int64> _docTypes = new List<Int64>();          // Lista con todos los tipos de documento.
        private List<String> specialDocTypes = new List<String>();  // Lista de documentos que se tratarán de manera especial.
        private List<String> backUpDocIds = new List<String>();     // Documentos a insertar en las tablas de backup.
        private DataTable docIdsToZI = new DataTable();             // Documentos a insertar en la ZI.
        private bool isSpecialDocType = false;                      // Los documentos especiales serán aquellos que se "cuelguen" o tarden demasiado tiempo en ser procesados. 
        private bool firstTimeOpened = true;
        private Int64 _docId;
        private Int64 _folderID;
        private DateTime _date;

        //Controlan el minimizado de la consola
        //[System.Runtime.InteropServices.DllImport("user32.dll")]
        //private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        #endregion

        #region Constantes
        private const String SELECT_QUERY = "SELECT crdate, DOC_ID, FOLDER_ID FROM DOC";
        private const String WHERE_QUERY = " where doc_id not in (select docid from zi where dtid = ";
        private const String BKPTABLE_QUERY = "Old (DOC_ID VARCHAR2(200 byte) NULL, FOLDER_ID VARCHAR2(200 byte) NULL, DISK_GROUP_ID VARCHAR2(200 byte) NULL, PLATTER_ID VARCHAR2(200 byte) NULL, VOL_ID VARCHAR2(200 byte) NULL, DOC_FILE VARCHAR2(200 byte) NULL, OFFSET VARCHAR2(200 byte) NULL, DOC_TYPE_ID VARCHAR2(200 byte) NULL, NAME VARCHAR2(255 byte) NULL, ICON_ID NUMBER(2) NULL, SHARED VARCHAR2(200 byte) NULL, ORIGINAL_FILENAME VARCHAR2(255 byte) NULL, NUMEROVERSION VARCHAR2(255 byte) NULL, ROOTID VARCHAR2(200 byte) NULL, VERSION VARCHAR2(255 byte) NULL, VER_PARENT_ID VARCHAR2(200 byte) NULL, ISVIRTUAL VARCHAR2(255 byte) NULL, FILESIZE VARCHAR2(255 byte)  NULL)";
        private const String WHEREDOCT_QUERY = " where doc_id not in (select doc_id from doc_i";
        private bool isOracle;
        #endregion

        public void execute()
        {
            Console.WriteLine("********************************************************************************");
            Console.WriteLine("* DocToZi - Comienzo de ejecución: " + DateTime.Now.ToString());
            Console.WriteLine("********************************************************************************");
            try
            {
                // Minimiza la consola. El parámetro 6 es el que minimiza.
                //IntPtr winHandle = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
                //ShowWindow( winHandle, 6);

                // Verifica el tipo de base de datos contra la que se esta trabajando.
                if (Server.isOracle)
                    isOracle = true;
                else
                    isOracle = false;

                // Verifica si utilizará tipos de documentos especiales
                if (Boolean.Parse(UserPreferences.getValue("UseSpecialDocType", Sections.DocToZI, false)))
                {
                    String doctypes = UserPreferences.getValue("SpecialDocTypesIds", Sections.DocToZI, 0);
                    foreach (String docTypeId in doctypes.Split(char.Parse(",")))
                    {
                        specialDocTypes.Add(docTypeId.Trim());
                    }
                    if (String.Compare(doctypes, "0") != 0)
                    {
                        Console.WriteLine("________________________________________________________");
                        Console.WriteLine("Existen seleccionados documentos especiales.");
                        Console.WriteLine("Su procesamiento demora más de lo normal.");
                        Console.WriteLine("Si desea desactivar esta opción abra el UserConfig y");
                        Console.WriteLine("configure la opción UseSpecialDocType en False.");
                        Console.WriteLine("________________________________________________________" + "\n");
                    }
                }

                // Verifica si utilizará tipos de documentos específicos
                if (Boolean.Parse(UserPreferences.getValue("UseSpecificDocType", Sections.DocToZI, false)) == false)
                {
                    LoadDocTypes();

                    foreach (Int64 CurrentDocType in _docTypes)
                        try
                        {
                            InsertIntoZI(CurrentDocType.ToString());
                        }
                        catch (Exception ex)
                        {
                            ZClass.raiseerror(ex);
                        }
                        finally
                        {
                            backUpDocIds.Clear();
                            docIdsToZI.Clear();
                        }
                }
                else
                {
                    String doctypes = UserPreferences.getValue("SpecificDocTypesIds", Sections.DocToZI, 0);
                    foreach (String docTypeId in doctypes.Split(char.Parse(",")))
                    {
                        try
                        {
                            InsertIntoZI(docTypeId.Trim());
                        }
                        catch (Exception ex)
                        {
                            ZClass.raiseerror(ex);
                        }
                        finally
                        {
                            backUpDocIds.Clear();
                            docIdsToZI.Clear();
                        }
                    }
                }

                Console.WriteLine("Finalizado");
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            finally
            {
                specialDocTypes.Clear();
                backUpDocIds.Clear();
                docIdsToZI.Clear();
            }
            //Console.ReadLine();
            Console.WriteLine("********************************************************************************");
            Console.WriteLine("* DocToZi - Fin de ejecución: " + DateTime.Now.ToString());
            Console.WriteLine("********************************************************************************");
            Application.Exit();
        }

        /// <summary>
        /// Cargo los tipos de documento 
        /// </summary>
        private void LoadDocTypes()
        {
            Console.WriteLine("Obteniendo doctpes desde BD");
            DataSet ds = GetDocTypes();

            Console.WriteLine("Cargando Doctypes");
            Int64 DocTypeId;
            foreach (DataRow CurrentRow in ds.Tables[0].Rows)
            {
                if (Int64.TryParse(CurrentRow[0].ToString(), out DocTypeId))
                {
                    Console.WriteLine("   " + DocTypeId.ToString());
                    _docTypes.Add(DocTypeId);
                }
                else
                    Console.WriteLine("   ERROR cargando " + CurrentRow[0].ToString());
            }

            Console.WriteLine("Fin carga Doctypes");
        }

        /// <summary>
        /// Inserta los valores de la tablas doc_i correspondiente en ZI
        /// </summary>
        /// <param name="docTypeId"></param>
        private void InsertIntoZI(String docTypeId)
        {
            Console.WriteLine("Procesando documento " + docTypeId);

            IDataReader dr = null;
            try
            {
                //Actualizo los valores faltantes de las crdate 
                dr = Server.get_Con(true, true, true).ExecuteReader(CommandType.Text, "SELECT  crdate,doc_i" + docTypeId + ".DOC_ID, FOLDER_ID FROM DOC_t" + docTypeId + " inner join doc_i" + docTypeId + " on doc_t" + docTypeId + ".doc_id=doc_i" + docTypeId + ".doc_id where doc_i" + docTypeId + ".doc_id not in (select docid from zi where dtid = " + docTypeId + ")");
                if (null != dr)
                {
                    while (dr.Read())
                    {
                        try
                        {
                            _docId = Int64.Parse(dr[1].ToString());

                            if (String.IsNullOrEmpty(dr[0].ToString()))
                                _date = DateTime.Now;
                            else
                                _date = DateTime.Parse(dr[0].ToString());

                            _folderID = Int64.Parse(dr[2].ToString());
                            //CheckTime();
                            Insert(CoreBusiness.GetNewID(IdTypes.Inserts), docTypeId, _docId, _folderID, _date);
                        }
                        catch (Exception ex)
                        {
                            Exception ex2 = new Exception(ex.Message + "§§§El docId que falla es el " + _docId + " y el docTypeId es " + docTypeId, ex);
                            //Si el error ocurre 5 veces se evita el bucle de error y se loguea la exception no insertando el documento en la zi.
                            ZClass.raiseerror(ex2);
                            Console.WriteLine(ex2.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Exception ex2 = new Exception(ex.Message + "§§§El el docTypeId es " + docTypeId, ex);
                //Si el error ocurre 5 veces se evita el bucle de error y se loguea la exception no insertando el documento en la zi.
                ZClass.raiseerror(ex2);
                Console.WriteLine(ex2.ToString());
            }
            finally
            {
                if (dr != null)
                {
                    if (dr.IsClosed == false)
                        dr.Close();
                    dr.Dispose();
                    dr = null;
                }
            }
        }

        /// <summary>
        /// Inserta los valores en ZI
        /// </summary>
        /// <param name="insertId"></param>
        /// <param name="docTypeId"></param>
        /// <param name="DocumentId"></param>
        /// <param name="folderID"></param>
        /// <param name="date"></param>
        /// <history>
        /// [Tomas] 14/08/2009  Modified    Se agrega verificacion de errores. Es posible que si se corren varios doctozi a la vez al 
        ///                                 obtener los ids genere excepciones de id duplicada.
        /// </history>
        private void Insert(Int64 insertId, String docTypeId, Int64 DocumentId, Int64 folderID, DateTime date)
        {
            // Sirven en caso de que el id del docid se repita (en los casos de estar corriendo mas de 1 doctozi)
            Int16 errorCount = 0;
            bool errors = false;
            do
            {
                try
                {
                    if (isOracle)
                    {
                        string[] parNames = { "INSERT_ID", "DOCTYPEID", "DOC_ID", "FOLDER_ID", "I_DATE" };
                        Object[] parTypes = { 13, 13, 13, 13, 13 };
                        Object[] parValues = { insertId, docTypeId, DocumentId, folderID, date };
                        Server.get_Con(true, true, true).ExecuteNonQuery("zsp_doctozi_100.AddDocToZI", parNames, parTypes, parValues);
                    }
                    else
                    {
                        //Object[] parameters = {insertId, docTypeId, DocumentId, folderID, Server.get_Con(true, true, true).ConvertDateTime(date.ToString())};
                        Object[] parameters = { insertId, docTypeId, DocumentId, folderID, date };
                        Server.get_Con(true, true, true).ExecuteNonQuery("ZSP_DOCTOZI_100_AddDocToZI", parameters);
                    }
                    errors = false;
                    Console.WriteLine("Insertado DocId " + DocumentId.ToString());
                }
                catch (Exception ex)
                {
                    if (errorCount < 2)
                    {
                        //En caso de error se obtiene un nuevo id y se intenta insertar nuevamente
                        insertId++;
                        errors = true;
                        errorCount++;
                    }
                    else
                    {
                        Exception ex2 = new Exception(ex.Message + "§§§El docId que falla es el " + DocumentId + " y el id de insercion es " + insertId + " y el docTypeId es " + docTypeId + " - Hora:" + DateTime.Now.ToString(), ex);
                        //Si el error ocurre 5 veces se evita el bucle de error y se loguea la exception no insertando el documento en la zi.
                        ZClass.raiseerror(ex2);
                        Console.WriteLine("ERROR Insertado DocId " + DocumentId.ToString());
                        errors = false;
                    }
                }
            } while (errors == true);
        }

        /// <summary>
        /// Obtiene todos los ID de los doctypes
        /// </summary>
        /// <returns>Un Dataset con todos los IDs</returns>
        private DataSet GetDocTypes()
        {
            DataSet Ds = null;
            byte ascendente = 0; // 0 = Ascendente, 1 = Descendente

            try
            {
                // Selección de ascendente o descendente
                if (Boolean.Parse(UserPreferences.getValue("OrderByDocTypeDesc", Sections.DocToZI, false)))
                {
                    ascendente = 1;
                }
            }
            catch (Exception)
            {
                ascendente = 0;
            }

            if (isOracle)
            {
                string[] parNames = { "orderByDesc", "io_cursor" };
                Object[] parTypes = { 13, 5 };
                Object[] parValues = { ascendente, 2 };
                Ds = Server.get_Con(true, true, true).ExecuteDataset("zsp_doctozi_100.GetDocTypes", parNames, parTypes, parValues);
            }
            else
            {
                Object[] parameters = { ascendente };
                Ds = Server.get_Con(true, true, true).ExecuteDataset("ZSP_DOCTOZI_100_GetDocTypes", parameters);
            }
            return Ds;
        }


        private void CheckTime()
        {
            if (DateTime.Now.Hour >= Int16.Parse(Zamba.Core.UserPreferences.getValue("DocToZIStart", Sections.DocToZI, "0")) && DateTime.Now.Hour <= Int16.Parse(Zamba.Core.UserPreferences.getValue("DocToZIEnd", Sections.DocToZI, "24")))
            {
            }
            else
            {
                System.Threading.Thread.Sleep(3600000);
                CheckTime();
            }
        }

        /// <summary>
        /// Devuelve si la tabla tiene o no FK
        /// </summary>
        /// <param name="docTypeId"></param>
        /// <returns></returns>
        //private Boolean tableHasFK(String docTypeId)
        //{
        //    Int32 cantFK = 0;
        //    StringBuilder sqlBuilder = new System.Text.StringBuilder();

        //    if (isOracle)
        //    {
        //        sqlBuilder.Append("select count(COLUMN_NAME) from(DBA_CONS_COLUMNS) WHERE TABLE_NAME = 'DOC_I");
        //        sqlBuilder.Append(docTypeId);
        //        sqlBuilder.Append("'  AND COLUMN_NAME = 'DOC_ID");
        //        sqlBuilder.Append("' AND CONSTRAINT_NAME IN (select CONSTRAINT_NAME ");
        //        sqlBuilder.Append(" from(DBA_CONSTRAINTS) WHERE TABLE_NAME = 'DOC_I");
        //        sqlBuilder.Append(docTypeId);
        //        sqlBuilder.Append("' and CONSTRAINT_TYPE = 'R')");

        //        cantFK = Int32.Parse(Server.get_Con(true, true, true).ExecuteScalar(CommandType.Text, sqlBuilder.ToString()).ToString());
        //    }
        //    else
        //    {
        //        sqlBuilder.Append("select count(*) from sysforeignkeys inner join sysobjects on sysforeignkeys.constid = sysobjects.id ");
        //        sqlBuilder.Append("inner join sysreferences on sysforeignkeys.constid = sysreferences.constid ");
        //        sqlBuilder.Append("inner join syscolumns source on sysforeignkeys.fkeyid = source.id and ");
        //        sqlBuilder.Append("sysforeignkeys.fkey = source.colid inner join syscolumns dest ");
        //        sqlBuilder.Append("on sysforeignkeys.rkeyid = dest.id and sysforeignkeys.rkey = dest.colid where ");
        //        sqlBuilder.Append("sysobjects.xtype = 'F' and (object_name(source.id) = 'DOC_I166') and (source.name = 'DOC_ID' ");
        //        sqlBuilder.Append("or dest.name = 'DOC_ID')");

        //        cantFK = Int32.Parse(Server.get_Con(true, true, true).ExecuteScalar(CommandType.Text, sqlBuilder.ToString()).ToString());
        //    }

        //    if (cantFK == 0)
        //        return false;
        //    else
        //        return true;
        //}

        /// <summary>
        /// Informa si existen diferencias entre los doc_id de la doc_t y la doc_i de un documento específico.
        /// </summary>
        /// <param name="docTypeId"></param>
        /// <returns>1 Si existen diferencias</returns>
        /// <history>
        /// [Tomas] 10/08/2009 Created: Este método es creado para resolver la tarea nº2681 
        ///                             ya que el origen del problema no pudo ser encontrado.
        /// </history>
        private Int32 GetDoctAndDociDifferences(String docTypeId)
        {
            Console.WriteLine("Buscando diferencias entre tablas...");
             //Obtiene los ids por separado
            //DataTable doc_i = Server.get_Con(true, true, true).ExecuteDataset(CommandType.Text, "select doc_id from doc_i" + docTypeId).Tables[0];
            DataTable doc_t = Server.get_Con(true, true, true).ExecuteDataset(CommandType.Text, "SELECT  crdate,doc_i" + docTypeId + ".DOC_ID, FOLDER_ID FROM DOC_t" + docTypeId + " inner join doc_i" + docTypeId + " on doc_t" + docTypeId + ".doc_id=doc_i" + docTypeId + ".doc_id").Tables[0];
            DataTable doc_zi = Server.get_Con(true, true, true).ExecuteDataset(CommandType.Text, "select docid from zi where dtid=" + docTypeId).Tables[0];
            DataRow[] encontrados;
            String docId;
            //Int32 cantDifDocIds = 0;

            if (firstTimeOpened)
            {
                docIdsToZI.Columns.Add("DOC_ID");
                docIdsToZI.Columns.Add("CRDATE");
                docIdsToZI.Columns.Add("FOLDER_ID");
                firstTimeOpened = false;
            }

             //Por cada docId de la tabla Doc_T valida si no existe en la Doc_I
            foreach (DataRow docIdT in doc_t.Rows)
            {
                docId = docIdT[1].ToString();

                 //Busca si ese docId existe en la doc_i
                //encontrados = doc_i.Select("DOC_ID=" + docId);

                 //Si no tiene elementos es porque el docId es diferente.
                 //Lo agrego a la lista de docIds para hacer backup
                //if (encontrados.Length == 0)
                //{
                    //cantDifDocIds++;
                    //backUpDocIds.Add(docId);
                //}
                //else
                //{
                     //Busca si ese docId existe en la doc_zi
                    encontrados = doc_zi.Select("DOCID=" + docId);

                     //Si no tiene elementos es porque el docId es diferente
                     //Agrego al dataSet los registros a insertar
                    if (encontrados.Length == 0)
                        foreach (DataRow dr in doc_t.Select("DOC_ID=" + docId))
                        {
                            docIdsToZI.Rows.Add(new Object[] { dr[0].ToString(), dr[1].ToString(), dr[2].ToString() });
                        }
                //}
            }
            encontrados = null;
            //doc_i.Clear();
            doc_t.Clear();
            doc_zi.Clear();
            //doc_i.Dispose();
            doc_t.Dispose();
            doc_zi.Dispose();
            return 0;
        }

        //private void CreateTableBackUp(String docTypeId)
        //{
        //    try
        //    {
        //        Console.WriteLine("Creando tabla backup");
        //        if (isOracle)
        //        {
        //            Server.get_Con(true, true, true).ExecuteNonQuery(CommandType.Text, "CREATE TABLE DOC_T" + docTypeId + BKPTABLE_QUERY);
        //        }
        //        else
        //        {
        //            Server.get_Con(true, true, true).ExecuteNonQuery(CommandType.Text, "SELECT TOP 0 * INTO DOC_T" + docTypeId + "OLD FROM DOC_T" + docTypeId);
        //            Server.get_Con(true, true, true).ExecuteNonQuery(CommandType.Text, "SELECT TOP 0 * INTO DOC_T" + docTypeId + " FROM DOC_T" + docTypeId + "OLD");
        //        }
        //    }
        //    catch
        //    {
        //        try
        //        {
        //            Server.get_Con(true, true, true).ExecuteNonQuery(CommandType.Text, "drop table doc_t" + docTypeId + "old");
        //            Server.get_Con(true, true, true).ExecuteNonQuery(CommandType.Text, "CREATE TABLE DOC_T" + docTypeId + BKPTABLE_QUERY);
        //        }
        //        catch (Exception ex)
        //        {
        //            ZClass.raiseerror(ex);
        //        }
        //    }

        //    Inserto los datos del backup
        //    try
        //    {
        //        InsertBackUpData(docTypeId);
        //    }
        //    catch
        //    {
        //        Server.get_Con(true, true, true).ExecuteNonQuery(CommandType.Text, "alter table doc_t" + docTypeId + " add ISVIRTUAL NUMBER(1) NULL");
        //        InsertBackUpData(docTypeId);
        //    }

        //    Borrando registros incorrectos
        //    DeleteIncorrectRows(docTypeId);

        //    Console.WriteLine("Verificando Foreign Key");
        //    if (tableHasFK(docTypeId) == false)
        //    {
        //        Console.WriteLine("Creando Foreign Key");
        //        Server.get_Con(true, true, true).ExecuteNonQuery(CommandType.Text, "ALTER TABLE doc_I" + docTypeId + " ADD CONSTRAINT fk" + docTypeId + " FOREIGN KEY(doc_id) REFERENCES DOC_T" + docTypeId + " (DOC_ID)");
        //    }
        //}

        /// <summary>
        /// Realiza el insert del backup
        /// </summary>
        /// <param name="docTypeId"></param>
        //private void InsertBackUpData(String docTypeId)
        //{
        //    Console.WriteLine("Realizando backup de los registros");

        //    if (isSpecialDocType)
        //    {
        //        foreach (String docId in backUpDocIds)
        //        {
        //            Server.get_Con(true, true, true).ExecuteNonQuery(CommandType.Text, "INSERT INTO DOC_T" + docTypeId + "OLD SELECT * FROM DOC_T" + docTypeId + " where doc_id = " + docId);
        //        }             
        //    }
        //    else
        //    {
        //        Server.get_Con(true, true, true).ExecuteNonQuery(CommandType.Text, "INSERT INTO DOC_T" + docTypeId + "OLD SELECT * FROM DOC_T" + docTypeId + WHEREDOCT_QUERY + docTypeId + ")");
        //    }
        //}

        //private void DeleteIncorrectRows(String docTypeId)
        //{
        //    Console.WriteLine("Borrando registros incorrectos");

        //    if (isSpecialDocType)
        //    {
        //        foreach (String docId in backUpDocIds)
        //        {
        //            Server.get_Con(true, true, true).ExecuteNonQuery(CommandType.Text, "delete from doc_t" + docTypeId + " where doc_id = " + docId);
        //        }
        //    }
        //    else
        //    {
        //        Server.get_Con(true, true, true).ExecuteNonQuery(CommandType.Text, "update doc_t" + docTypeId + " set platter_id = 123456 where doc_id not in (select doc_id from doc_i" + docTypeId + ")");
        //        Server.get_Con(true, true, true).ExecuteNonQuery(CommandType.Text, "delete from doc_t" + docTypeId + " where platter_id = 123456");
        //    }
        //}

        #region IDisposable Members

        public void Dispose()
        {
            if (_docTypes != null)
            {
                _docTypes.Clear();
                _docTypes = null;
            }
            if (specialDocTypes != null)
            {
                specialDocTypes.Clear();
                specialDocTypes = null;
            }
            if (backUpDocIds != null)
            {
                backUpDocIds.Clear();
                backUpDocIds = null;
            }
            if (docIdsToZI != null)
            {
                docIdsToZI.Clear();
                docIdsToZI = null;
            }
        }

        #endregion
    }
}
