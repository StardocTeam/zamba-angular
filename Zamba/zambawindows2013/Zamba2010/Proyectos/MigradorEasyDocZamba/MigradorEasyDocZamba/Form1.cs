using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Zamba.Core;
using System.Diagnostics;
using Zamba.Data;
using System.IO;
using Zamba.Servers;

namespace MigradorEasyDocZamba
{
    public partial class Form1 : Form
    {
        Hashtable hsEntities = new Hashtable();
        Hashtable hsAtributes = new Hashtable();
        Hashtable hsEntitiesAndAtributes = new Hashtable();
        Hashtable hsVols = new Hashtable();

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Realiza la migracion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> Fallidos = new List<string>();
                for (int a = 0; a < dataGridView1.RowCount; a++)
                {
                    if (dataGridView1.Rows[a].Cells[0].Value != null && (string)dataGridView1.Rows[a].Cells[0].Value == "True")
                    {
                        txtTemplate.Text = (string)dataGridView1.Rows[a].Cells[2].Value;

                        Trace.WriteLine("Logueando Usuario");
                        if (UserBusiness.Rights.ValidateLogIn(Int32.Parse(txtUsrZamba.Text), ClientType.Service) != null)
                        {
                            Trace.WriteLine("Marcando entidad en ejecucion");
                            Zamba.Servers.Server.get_Con().ExecuteNonQuery("update MigracionEasyDoc set Estado='Ejecucion' where template='" + txtTemplate.Text.Trim() + "'");

                            Trace.WriteLine("Hora comienzo: " + DateTime.Now);

                            //Obtengo los atributos de la entidad
                            if (hsEntities.Contains(txtTemplate.Text.Trim()))
                            {
                                Int64 doctypeID = (Int64)hsEntities[txtTemplate.Text.Trim()];

                                StringBuilder query = new StringBuilder();

                                query.Append("Select VOLUMEN, cod_lote ");

                                if ((List<string>)hsEntitiesAndAtributes[txtTemplate.Text.Trim()] != null)
                                {
                                    foreach (string s in (List<string>)hsEntitiesAndAtributes[txtTemplate.Text.Trim()])
                                    {
                                        query.Append(",");
                                        query.Append(s);
                                    }
                                }
                                else
                                {
                                    Trace.WriteLine("No hay atributos para el template: " + txtTemplate.Text.Trim());
                                    query.Append(", nro_orden ");
                                }

                                query.Append(" from easydoc.");
                                query.Append(txtTemplate.Text.Trim());
                                query.Append("AB");

                                IConnection con = Zamba.Servers.Server.get_Con(Zamba.Servers.Server.DBTYPES.OracleClient, txtServer.Text, txtDB.Text, txtUser.Text, txtPassword.Text, false, true);
                                IDataReader dr = con.ExecuteReader(CommandType.Text, query.ToString());
                                query = null;

                                //Por cada entidad
                                while (dr.Read())
                                {
                                    String ruta = dr.GetValue(0).ToString().Trim();

                                    if (hsVols.Contains(ruta) == true)
                                    {
                                        Int32 ordinalNroOrden = dr.GetOrdinal("nro_orden");
                                        ruta = hsVols[ruta].ToString() + dr.GetValue(1) + "\\" + dr.GetValue(ordinalNroOrden) + ".TIF";
                                        NewResult newRes = Results_Business.GetNewNewResult(doctypeID);

                                        //Si no tiene indices, cargo solo el nro de orden
                                        if ((List<string>)hsEntitiesAndAtributes[txtTemplate.Text.Trim()] != null)
                                        {
                                            for (Int32 i = 0; i <= newRes.Indexs.Count - 1; i++)
                                            {
                                                string indexName = ((Index)newRes.Indexs[i]).Name;
                                                if (((List<string>)hsEntitiesAndAtributes[txtTemplate.Text.Trim()]).Contains(indexName))
                                                {
                                                    ((Index)newRes.Indexs[i]).DataTemp = dr.GetValue(dr.GetOrdinal(indexName)).ToString().Trim().Replace("'", "").Replace("´", ""); 
                                                }
                                            }
                                        }
                                        else
                                        {
                                            ((Index)newRes.Indexs[0]).DataTemp = dr.GetValue(ordinalNroOrden).ToString().Trim();
                                        }

                                        newRes.File = ruta;
                                        newRes.FolderId = CoreData.GetNewID(IdTypes.FOLDERSID);

                                        if (Results_Business.InsertDocument(ref newRes, false, false, false, false, true, false, false, false) != Zamba.Core.InsertResult.Insertado)
                                        {
                                            Fallidos.Add(txtTemplate.Text + "§" + dr.GetValue(1) + "§" + dr.GetValue(ordinalNroOrden));
                                            Trace.WriteLine("Ha ocurrido un error en la insercion. Lote: " + dr.GetValue(1) + " - Nro Orden: " + dr.GetValue(ordinalNroOrden));
                                        }
                                    }
                                    else
                                    {
                                        Trace.WriteLine("El volumen no esta disponible: " + ruta);
                                    }
                                }

                                Trace.WriteLine("Lote ingresado correctamente. Hora Finalizacion: " + DateTime.Now);
                                dataGridView1.Rows[a].Cells[0].Value = "False";
                                dr.Close();
                                dr.Dispose();
                                dr = null;
                                con.Close();
                                con.dispose();
                                con = null;
                            }
                            else
                            {
                                Trace.WriteLine("La entidad no esta disponible: " + txtTemplate.Text);
                            }

                            Trace.WriteLine("Marcando entidad finalizado");
                            Zamba.Servers.Server.get_Con().ExecuteNonQuery("update MIgracionEasyDoc set Estado='Migrado', cod_lote='', nro_orden='' where template='" + txtTemplate.Text.Trim() + "'");

                        }
                        else
                        {
                            MessageBox.Show("El usuario ingresado no es un usuario valido");
                        }
                    }
                }

                #region fallidos
                StreamWriter sw = new StreamWriter(Application.StartupPath + "\\Fallidos.txt");
                sw.AutoFlush = true;
                foreach (String fallido in Fallidos)
                {
                    sw.WriteLine(fallido);
                }
                sw.Close();
                sw.Dispose();
                sw = null;
                #endregion


                if (Fallidos.Count != 0)
                {
                    MessageBox.Show("Proceso finalizado con " + Fallidos.Count + " errores");
                }
                else
                    MessageBox.Show("Proceso finalizado correctamente");

                Fallidos = null;

            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                MessageBox.Show("Proceso finalizado con errores");
            }
        }

        /// <summary>
        /// Ejecuta una consulta que devuelve un dataset contra la base de easydoc
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private DataSet executeDatasetEasyDoc(String query)
        {
            return Zamba.Servers.Server.get_Con(Zamba.Servers.Server.DBTYPES.OracleClient, txtServer.Text, txtDB.Text, txtUser.Text, txtPassword.Text, false, true).ExecuteDataset(CommandType.Text, query);
        }

        /// <summary>
        /// Normaliza las doc_t para que se puedan ver correctamente los documentos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Int64 dtID in hsEntities.Values)
                {
                    Trace.WriteLine("Normalizando documento: " + dtID);
                    Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, "update doc_t" + dtID + " set doc_file=original_filename, disk_group_id=-1");
                    Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, "insert into wfdocument (doc_id,doc_type_id,task_id, step_id, folder_id, iconid, work_id,name,do_state_id, checkin, expiredate, user_asigned, c_exclusive, task_state_id)  select doc_id, doc_type_id, (select max(task_id) + 1 from wfdocument), " + txtStepID.Text + ", folder_id, icon_id, " + txtWFID.Text + ",name, " + txtStateId.Text + ",sysdate, sysdate + 30, 0,0,0 from doc_t" + dtID + " where doc_id not in (select doc_id from wfdocument)");
                    Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, "update objlastid set objectid = (select max(task_id) from wfdocument) where object_type_id = 55");
                    
                    if (dtID.ToString() == textBox1.Text)
                    {
                        Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, "update doc_t" + dtID + " set doc_file=replace(doc_file,'DGART','DGSeguros') where doc_file like '%893%'");
                        Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, "update doc_t" + dtID + " set doc_file=replace(doc_file,'DGART','DGSeguros') where doc_file like '%894%'");
                        Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, "update doc_t" + dtID + " set doc_file=replace(doc_file,'DGART','DGSeguros') where doc_file like '%895%'");
                        Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, "update doc_t" + dtID + " set doc_file=replace(doc_file,'DGART','DGSeguros') where doc_file like '%896%'");
                        Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, "update doc_t" + dtID + " set doc_file=replace(doc_file,'DGART','DGSeguros') where doc_file like '%897%'");
                        Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, "update doc_t" + dtID + " set doc_file=replace(doc_file,'DGART','DGSeguros') where doc_file like '%898%'");
                        Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, "update doc_t" + dtID + " set doc_file=replace(doc_file,'DGART','DGSeguros') where doc_file like '%899%'");
                        Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, "update doc_t" + dtID + " set doc_file=replace(doc_file,'DGART','DGSeguros') where doc_file like '%890%'");
                        Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, "update doc_t" + dtID + " set doc_file=replace(doc_file,'DGART','DGSeguros') where doc_file like '%1016%'");
                        Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, "update doc_t" + dtID + " set doc_file=replace(doc_file,'DGART','DGSeguros') where doc_file like '%1017%'");
                        Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, "update doc_t" + dtID + " set doc_file=replace(doc_file,'DGART','DGSeguros') where doc_file like '%1018%'");
                        Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, "update doc_t" + dtID + " set doc_file=replace(doc_file,'DGART','DGSeguros') where doc_file like '%1019%'");
                        Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, "update doc_t" + dtID + " set doc_file=replace(doc_file,'DGART','DGSeguros') where doc_file like '%1020%'");
                        Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, "update doc_t" + dtID + " set doc_file=replace(doc_file,'DGART','DGSeguros') where doc_file like '%1021%'");
                    }
                }

                MessageBox.Show("Proceso finalizado correctamente");
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                MessageBox.Show("Proceso finalizado con errores");
            }
        }

        /// <summary>
        /// Recarga la grilla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            cargarGrilla();
        }

        /// <summary>
        /// Carga la grilla con el estado de la migracion
        /// </summary>
        private void cargarGrilla()
        {
            dataGridView1.DataSource = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, "select * from migracionEasyDoc").Tables[0];
            dataGridView1.Update();
        }

        /// <summary>
        /// Vuelve a ejecutar los procesos marcados como fallidos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> Fallidos = new List<string>();
                StreamReader sr = new StreamReader(Application.StartupPath + "\\Fallidos.txt");
                Trace.WriteLine("Logueando Usuario");
                if (UserBusiness.Rights.ValidateLogIn(Int32.Parse(txtUsrZamba.Text), ClientType.Service) != null)
                {
                    String line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        String entidad = line.Split(char.Parse("§"))[0];
                        String codLote = line.Split(char.Parse("§"))[1];
                        String nroOrden = line.Split(char.Parse("§"))[2];

                        Trace.WriteLine("Hora comienzo: " + DateTime.Now);
                        //Obtengo los atributos de la entidad
                        List<string> hsIndexs = (List<string>)hsEntitiesAndAtributes[entidad];

                        if (hsEntities.Contains(entidad))
                        {
                            Int64 doctypeID = (Int64)hsEntities[entidad];

                            StringBuilder query = new StringBuilder();

                            query.Append("Select VOLUMEN ");

                            if (hsIndexs != null)
                            {
                                foreach (string s in hsIndexs)
                                {
                                    query.Append(",");
                                    query.Append(s);
                                }
                            }
                            else
                            {
                                Trace.WriteLine("No hay atributos para el template: " + entidad);
                                query.Append(", nro_orden ");
                            }

                            query.Append(" from easydoc.");
                            query.Append(entidad);
                            query.Append("AB where cod_lote ='");
                            query.Append(codLote);
                            query.Append("' and Nro_orden ='");
                            query.Append(nroOrden);
                            query.Append("'");

                            DataSet dsFile = executeDatasetEasyDoc(query.ToString());

                            query = null;

                            //Por cada entidad
                            foreach (DataRow drFile in dsFile.Tables[0].Rows)
                            {
                                String ruta = drFile["VOLUMEN"].ToString().Trim();

                                if (hsVols.Contains(ruta) == true)
                                {
                                    ruta = hsVols[ruta].ToString() + codLote + "\\" + nroOrden + ".TIF";

                                    NewResult newRes = Results_Business.GetNewNewResult(doctypeID);

                                    //Si no tiene indices, cargo solo el nro de orden
                                    if (hsIndexs != null)
                                    {
                                        for (Int32 i = 0; i <= newRes.Indexs.Count - 1; i++)
                                        {
                                            string indexName = ((Index)newRes.Indexs[i]).Name;
                                            if (hsIndexs.Contains(indexName))
                                            {
                                                ((Index)newRes.Indexs[i]).DataTemp = drFile[indexName].ToString().Trim().Replace("'", ""); ;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        ((Index)newRes.Indexs[0]).DataTemp = drFile["nro_orden"].ToString().Trim();
                                    }

                                    newRes.File = ruta;
                                    newRes.FolderId = CoreData.GetNewID(IdTypes.FOLDERSID);

                                    if (Results_Business.InsertDocument(ref newRes, false, false, false, false, true, false, false, false) != Zamba.Core.InsertResult.Insertado)
                                    {
                                        Fallidos.Add(entidad + "§" + codLote + "§" + nroOrden);
                                        Trace.WriteLine("Ha ocurrido un error en la insercion. Lote: " + codLote + " - Nro Orden: " + nroOrden);
                                    }
                                }
                                else
                                {
                                    Trace.WriteLine("El volumen no esta disponible: " + ruta);
                                }
                            }

                            Trace.WriteLine("Lote ingresado correctamente.Hora Finalizacion: " + DateTime.Now);
                            dsFile.Dispose();
                            dsFile = null;
                        }
                        else
                        {
                            Trace.WriteLine("La entidad no esta disponible: " + entidad);
                        }
                    }
                    
                }
                else
                {
                    MessageBox.Show("El usuario ingresado no es un usuario valido");
                }

                if (Fallidos.Count == 0)
                {
                    MessageBox.Show("Proceso finalizado correctamente");
                }
                else
                {
                    MessageBox.Show("Proceso finalizado con " + Fallidos.Count + "errores");
                }

                sr.Close();
                sr.Dispose();
                sr = null;

                StreamWriter sw = new StreamWriter(Application.StartupPath + "\\Fallidos.txt");
                sw.AutoFlush = true;
                foreach (String fallido in Fallidos)
                {
                    sw.WriteLine(fallido);
                }
                sw.Close();
                sw.Dispose();
                sw = null;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                MessageBox.Show("Proceso finalizado con errores");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = Application.StartupPath + "\\Fallidos.txt";
            proc.Start();
            proc.Dispose();
            proc = null;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                Zamba.Core.ZTrace.AddListener("MigradorEasyDocZamba");
                Trace.WriteLine("Iniciando migrador");

                #region HashTables
                //////////////////////Guardo las entidades en un hash//////////////////////////////
                #region entidades
                Trace.WriteLine("Obteniendo nombres entidades");
                DataSet dsAux = executeDatasetEasyDoc("select DESCRIPCIO, tmplate from easydoc.tmplates");

                Trace.WriteLine("Guardando entidades de Zamba");
                foreach (DataRow drEntity in dsAux.Tables[0].Rows)
                {
                    Int64 EntityID = Zamba.Data.DocTypesFactory.GetDocTypeIdByName(drEntity[0].ToString().Trim());
                    if (EntityID > 0)
                    {
                        hsEntities.Add(drEntity[1].ToString().Trim(), EntityID);
                    }
                    else
                        Trace.WriteLine("No se ha encontrado la entidad: " + drEntity[0].ToString().Trim());
                }

                dsAux.Dispose();
                Trace.WriteLine("Entidades guardadas");
                #endregion

                //////////////////////Guardo los atributos en un hash//////////////////////////////
                #region Atributos
                Trace.WriteLine("Obteniendo atributos");
                dsAux = Zamba.Data.Indexs_Factory.GetIndexsDsIdsAndNames();

                Trace.WriteLine("Guardando atributos de Zamba");
                foreach (DataRow drAtribute in dsAux.Tables[0].Rows)
                {
                    hsAtributes.Add(drAtribute[1].ToString().Trim(), drAtribute[0].ToString().Trim());
                }

                dsAux.Dispose();
                Trace.WriteLine("Atributos guardados");
                #endregion

                //////////////////////Guardo los atributos de las entidades en un hash/////////////
                #region atributosEntidades
                Trace.WriteLine("Obteniendo atributos de entidades de EasyDoc");
                dsAux = executeDatasetEasyDoc("select tmplate, campo from easydoc.zona order by tmplate");

                Trace.WriteLine("Guardando atributos de entidades de EasyDoc");
                String NombreEntidadAnterior = string.Empty;
                List<String> NombresAtributos = new List<string>();
                NombresAtributos.Add("nro_orden");
                foreach (DataRow drEntity in dsAux.Tables[0].Rows)
                {
                    if (NombreEntidadAnterior != drEntity[0].ToString().Trim())
                    {
                        if (string.IsNullOrEmpty(NombreEntidadAnterior) == false)
                            hsEntitiesAndAtributes.Add(NombreEntidadAnterior, NombresAtributos);
                        NombreEntidadAnterior = drEntity[0].ToString().Trim();
                        NombresAtributos = new List<string>();
                        NombresAtributos.Add("nro_orden");
                    }
                    NombresAtributos.Add(drEntity[1].ToString().Trim());
                }
                //Guardo el ultimo valor
                hsEntitiesAndAtributes.Add(NombreEntidadAnterior, NombresAtributos);
                dsAux.Dispose();
                Trace.WriteLine("Atributos de entidades guardados");
                #endregion

                //////////////////////Guardo las entidades en un hash//////////////////////////////
                #region Volumenes
                Trace.WriteLine("Obteniendo volumenes");
                dsAux = executeDatasetEasyDoc("select volumen, mount from easydoc.montaje");

                hsVols.Add("V171", "\\\\Ar-digital-uno\\imgdig\\DGART\\DGLIBERTY\\V171\\");
                hsVols.Add("V196", "\\\\Ar-digital-uno\\imgdig\\DGART\\DGLIBERTY\\V196\\");

                Trace.WriteLine("Guardando volumenes de EasyDoc");
                foreach (DataRow drVol in dsAux.Tables[0].Rows)
                {
                    if (drVol[1].ToString().Trim().EndsWith("\\"))
                    {
                        hsVols.Add(drVol[0].ToString().Trim(), drVol[1].ToString().Trim());
                    }
                    else
                        hsVols.Add(drVol[0].ToString().Trim(), drVol[1].ToString().Trim() + "\\");
                }

                dsAux.Dispose();
                dsAux = null;
                Trace.WriteLine("Volumenes guardados");
                #endregion
                #endregion

                Trace.WriteLine("Carga tabla de migracion");
                cargarGrilla();

                #region cargaDatosMigracionBasicos
                if (dataGridView1.Rows.Count == 0)
                {
                    dsAux = executeDatasetEasyDoc("select DESCRIPCIO, tmplate from easydoc.tmplates order by DESCRIPCIO");

                    foreach (DataRow drEntity in dsAux.Tables[0].Rows)
                    {
                        Int64 EntityID = Zamba.Data.DocTypesFactory.GetDocTypeIdByName(drEntity[0].ToString().Trim());
                        if (EntityID > 0)
                        {
                            Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, "Insert into MigracionEasyDoc(Entidad, Template, Estado) values ('" + drEntity[0].ToString().Trim() + "','" + drEntity[1].ToString().Trim() + "','Pendiente')");
                        }
                    }

                    dsAux.Dispose();
                    dsAux = null;
                    Trace.WriteLine("Entidades migracion generadas");
                }
                #endregion

                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
        }
    }
}
