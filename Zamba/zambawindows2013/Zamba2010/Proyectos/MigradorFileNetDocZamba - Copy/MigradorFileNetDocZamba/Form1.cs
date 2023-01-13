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
using System.Xml.Serialization;
using Zamba;

namespace MigradorEasyDocZamba
{

    public partial class Form1 : Form
    {
        List<Entity> Entities { get; set; } = new List<Entity>();
        List<Index> Indexs { get; set; } = new List<Index>();
        List<IndexList> IndexLists { get; set; } = new List<IndexList>();

        Hashtable hsVols { get; set; } = new Hashtable();
        Hashtable hsEntities { get; set; } = new Hashtable();
        Hashtable hsEntitiesAndAtributes { get; set; } = new Hashtable();
        Hashtable hsAtributes { get; set; } = new Hashtable();


        public Form1()
        {
            InitializeComponent();

            Entities.Add(new Entity("Expedientes de Clientes", "saros.element e inner join saros.VERSION v on e.e_name = v_e_name inner join saros.STACK s on s.st_id = v.v_stack_id", "v_e_name", 10, "Antecedentes Clientes                                       ", "e_INA01", @"''\\svrimage\zamba\' || replace(s.st_path,':','') || '\' || e.e_org_filename || '''", @"''\\svrimage\FILENET\' || replace(s.st_path,':','') || '\' || v.v_stack_file_id || '''"));
            Entities.Add(new Entity("Balances", "saros.element e inner join saros.VERSION v on e.e_name = v_e_name inner join saros.STACK s on s.st_id = v.v_stack_id", "v_e_name", 83, "Balances                                                    ", "e_INA01", @"''\\svrimage\zamba\' || replace(s.st_path,':','') || '\' || e.e_org_filename || '''", @"''\\svrimage\FILENET\' || replace(s.st_path,':','') || '\' || v.v_stack_file_id || '''"));
            Entities.Add(new Entity("", "saros.element e inner join saros.VERSION v on e.e_name = v_e_name inner join saros.STACK s on s.st_id = v.v_stack_id", "v_e_name", 25, "Documentos Generales                                        ", "e_INA01", @"''\\svrimage\zamba\' || replace(s.st_path,':','') || '\' || e.e_org_filename || '''", @"''\\svrimage\FILENET\' || replace(s.st_path,':','') || '\' || v.v_stack_file_id || '''"));
            Entities.Add(new Entity("Emision", "saros.element e inner join saros.VERSION v on e.e_name = v_e_name inner join saros.STACK s on s.st_id = v.v_stack_id", "v_e_name", 17, "Emision                                                     ", "e_INA01", @"''\\svrimage\zamba\' || replace(s.st_path,':','') || '\' || e.e_org_filename || '''", @"''\\svrimage\FILENET\' || replace(s.st_path,':','') || '\' || v.v_stack_file_id || '''"));
            Entities.Add(new Entity("Seguros de Vida", "saros.element e inner join saros.VERSION v on e.e_name = v_e_name inner join saros.STACK s on s.st_id = v.v_stack_id", "v_e_name", 17, "", "e_INA01", @"''\\svrimage\zamba\' || replace(s.st_path,':','') || '\' || e.e_org_filename || '''", @"''\\svrimage\FILENET\' || replace(s.st_path,':','') || '\' || v.v_stack_file_id || '''"));
            Entities.Add(new Entity("Seguros Vida Obligatorio", "saros.element e inner join saros.VERSION v on e.e_name = v_e_name inner join saros.STACK s on s.st_id = v.v_stack_id", "v_e_name", 17, "", "e_INA01", @"''\\svrimage\zamba\' || replace(s.st_path,':','') || '\' || e.e_org_filename || '''", @"''\\svrimage\FILENET\' || replace(s.st_path,':','') || '\' || v.v_stack_file_id || '''"));
            Entities.Add(new Entity("Accidentes Personales", "saros.element e inner join saros.VERSION v on e.e_name = v_e_name inner join saros.STACK s on s.st_id = v.v_stack_id", "v_e_name", 17, "", "e_INA01", @"''\\svrimage\zamba\' || replace(s.st_path,':','') || '\' || e.e_org_filename || '''", @"''\\svrimage\FILENET\' || replace(s.st_path,':','') || '\' || v.v_stack_file_id || '''"));
            Entities.Add(new Entity("Fotos Insp Prev Seguros Grales", "saros.element e inner join saros.VERSION v on e.e_name = v_e_name inner join saros.STACK s on s.st_id = v.v_stack_id", "v_e_name", 17, "", "e_INA01", @"''\\svrimage\zamba\' || replace(s.st_path,':','') || '\' || e.e_org_filename || '''", @"''\\svrimage\FILENET\' || replace(s.st_path,':','') || '\' || v.v_stack_file_id || '''"));
            Entities.Add(new Entity("Exp de Compras y Contrataciones", "saros.element e inner join saros.VERSION v on e.e_name = v_e_name inner join saros.STACK s on s.st_id = v.v_stack_id", "v_e_name", 11, "Expediente de Compras                                       ", "e_INA01", @"''\\svrimage\zamba\' || replace(s.st_path,':','') || '\' || e.e_org_filename || '''", @"''\\svrimage\FILENET\' || replace(s.st_path,':','') || '\' || v.v_stack_file_id || '''"));
            Entities.Add(new Entity("", "saros.element e inner join saros.VERSION v on e.e_name = v_e_name inner join saros.STACK s on s.st_id = v.v_stack_id", "v_e_name", 26, "Factura                                                     ", "e_INA01", @"''\\svrimage\zamba\' || replace(s.st_path,':','') || '\' || e.e_org_filename || '''", @"''\\svrimage\FILENET\' || replace(s.st_path,':','') || '\' || v.v_stack_file_id || '''"));
            Entities.Add(new Entity("Impuestos", "saros.element e inner join saros.VERSION v on e.e_name = v_e_name inner join saros.STACK s on s.st_id = v.v_stack_id", "v_e_name", 84, "Impuestos                                                   ", "e_INA01", @"''\\svrimage\zamba\' || replace(s.st_path,':','') || '\' || e.e_org_filename || '''", @"''\\svrimage\FILENET\' || replace(s.st_path,':','') || '\' || v.v_stack_file_id || '''"));
            Entities.Add(new Entity("Legales", "saros.element e inner join saros.VERSION v on e.e_name = v_e_name inner join saros.STACK s on s.st_id = v.v_stack_id", "v_e_name", 12, "Legales                                                     ", "e_INA01", @"''\\svrimage\zamba\' || replace(s.st_path,':','') || '\' || e.e_org_filename || '''", @"''\\svrimage\FILENET\' || replace(s.st_path,':','') || '\' || v.v_stack_file_id || '''"));
            Entities.Add(new Entity("Media", "saros.element e inner join saros.VERSION v on e.e_name = v_e_name inner join saros.STACK s on s.st_id = v.v_stack_id", "v_e_name", 13, "Mediaciones                                                 ", "e_INA01", @"''\\svrimage\zamba\' || replace(s.st_path,':','') || '\' || e.e_org_filename || '''", @"''\\svrimage\FILENET\' || replace(s.st_path,':','') || '\' || v.v_stack_file_id || '''"));
            Entities.Add(new Entity("Mediacion", "saros.element e inner join saros.VERSION v on e.e_name = v_e_name inner join saros.STACK s on s.st_id = v.v_stack_id", "v_e_name", 13, "", "e_INA01", @"''\\svrimage\zamba\' || replace(s.st_path,':','') || '\' || e.e_org_filename || '''", @"''\\svrimage\FILENET\' || replace(s.st_path,':','') || '\' || v.v_stack_file_id || '''"));
            Entities.Add(new Entity("", "saros.element e inner join saros.VERSION v on e.e_name = v_e_name inner join saros.STACK s on s.st_id = v.v_stack_id", "v_e_name", 22, "Orden de Compras                                            ", "e_INA01", @"''\\svrimage\zamba\' || replace(s.st_path,':','') || '\' || e.e_org_filename || '''", @"''\\svrimage\FILENET\' || replace(s.st_path,':','') || '\' || v.v_stack_file_id || '''"));
            Entities.Add(new Entity("Registro de Proveedores", "saros.element e inner join saros.VERSION v on e.e_name = v_e_name inner join saros.STACK s on s.st_id = v.v_stack_id", "v_e_name", 14, "Proveedores                                                 ", "e_INA01", @"''\\svrimage\zamba\' || replace(s.st_path,':','') || '\' || e.e_org_filename || '''", @"''\\svrimage\FILENET\' || replace(s.st_path,':','') || '\' || v.v_stack_file_id || '''"));
            Entities.Add(new Entity("", "saros.element e inner join saros.VERSION v on e.e_name = v_e_name inner join saros.STACK s on s.st_id = v.v_stack_id", "v_e_name", 18, "Requisitoria de Compra                                      ", "e_INA01", @"''\\svrimage\zamba\' || replace(s.st_path,':','') || '\' || e.e_org_filename || '''", @"''\\svrimage\FILENET\' || replace(s.st_path,':','') || '\' || v.v_stack_file_id || '''"));
            Entities.Add(new Entity("Inspecciones de Siniestros", "saros.element e inner join saros.VERSION v on e.e_name = v_e_name inner join saros.STACK s on s.st_id = v.v_stack_id", "v_e_name", 15, "Siniestro                                                   ", "e_INA01", @"''\\svrimage\zamba\' || replace(s.st_path,':','') || '\' || e.e_org_filename || '''", @"''\\svrimage\FILENET\' || replace(s.st_path,':','') || '\' || v.v_stack_file_id || '''"));
            Entities.Add(new Entity("Terceros Lesiones", "saros.element e inner join saros.VERSION v on e.e_name = v_e_name inner join saros.STACK s on s.st_id = v.v_stack_id", "v_e_name", 15, "", "e_INA01", @"''\\svrimage\zamba\' || replace(s.st_path,':','') || '\' || e.e_org_filename || '''", @"''\\svrimage\FILENET\' || replace(s.st_path,':','') || '\' || v.v_stack_file_id || '''"));
            Entities.Add(new Entity("InspeccionesSegGral", "saros.element e inner join saros.VERSION v on e.e_name = v_e_name inner join saros.STACK s on s.st_id = v.v_stack_id", "v_e_name", 15, "", "e_INA01", @"''\\svrimage\zamba\' || replace(s.st_path,':','') || '\' || e.e_org_filename || '''", @"''\\svrimage\FILENET\' || replace(s.st_path,':','') || '\' || v.v_stack_file_id || '''"));
            Entities.Add(new Entity("Siniestros", "saros.element e inner join saros.VERSION v on e.e_name = v_e_name inner join saros.STACK s on s.st_id = v.v_stack_id", "v_e_name", 15, "", "e_INA01", @"''\\svrimage\zamba\' || replace(s.st_path,':','') || '\' || e.e_org_filename || '''", @"''\\svrimage\FILENET\' || replace(s.st_path,':','') || '\' || v.v_stack_file_id || '''"));
            Entities.Add(new Entity("Inspecciones", "saros.element e inner join saros.VERSION v on e.e_name = v_e_name inner join saros.STACK s on s.st_id = v.v_stack_id", "v_e_name", 15, "", "e_INA01", @"''\\svrimage\zamba\' || replace(s.st_path,':','') || '\' || e.e_org_filename || '''", @"''\\svrimage\FILENET\' || replace(s.st_path,':','') || '\' || v.v_stack_file_id || '''"));
            Entities.Add(new Entity("", "saros.element e inner join saros.VERSION v on e.e_name = v_e_name inner join saros.STACK s on s.st_id = v.v_stack_id", "v_e_name", 19, "ZB - Clasificacion de Documentos from Multifuncion  ", "e_INA01", @"''\\svrimage\zamba\' || replace(s.st_path,':','') || '\' || e.e_org_filename || '''", @"''\\svrimage\FILENET\' || replace(s.st_path,':','') || '\' || v.v_stack_file_id || '''"));
            Entities.Add(new Entity("", "saros.element e inner join saros.VERSION v on e.e_name = v_e_name inner join saros.STACK s on s.st_id = v.v_stack_id", "v_e_name", 20, "ZB - Matriz de Derivacion                                   ", "e_INA01", @"''\\svrimage\zamba\' || replace(s.st_path,':','') || '\' || e.e_org_filename || '''", @"''\\svrimage\FILENET\' || replace(s.st_path,':','') || '\' || v.v_stack_file_id || '''"));
            Entities.Add(new Entity("", "saros.element e inner join saros.VERSION v on e.e_name = v_e_name inner join saros.STACK s on s.st_id = v.v_stack_id", "v_e_name", 79, "ZB - Tabla Derivacion de Legales                            ", "e_INA01", @"''\\svrimage\zamba\' || replace(s.st_path,':','') || '\' || e.e_org_filename || '''", @"''\\svrimage\FILENET\' || replace(s.st_path,':','') || '\' || v.v_stack_file_id || '''"));
            Entities.Add(new Entity("", "saros.element e inner join saros.VERSION v on e.e_name = v_e_name inner join saros.STACK s on s.st_id = v.v_stack_id", "v_e_name", 81, "ZB - Tabla Derivacion Emision                               ", "e_INA01", @"''\\svrimage\zamba\' || replace(s.st_path,':','') || '\' || e.e_org_filename || '''", @"''\\svrimage\FILENET\' || replace(s.st_path,':','') || '\' || v.v_stack_file_id || '''"));
            Entities.Add(new Entity("", "saros.element e inner join saros.VERSION v on e.e_name = v_e_name inner join saros.STACK s on s.st_id = v.v_stack_id", "v_e_name", 80, "ZB - Tabla Derivacion Mediacion                             ", "e_INA01", @"''\\svrimage\zamba\' || replace(s.st_path,':','') || '\' || e.e_org_filename || '''", @"''\\svrimage\FILENET\' || replace(s.st_path,':','') || '\' || v.v_stack_file_id || '''"));
            Entities.Add(new Entity("", "saros.element e inner join saros.VERSION v on e.e_name = v_e_name inner join saros.STACK s on s.st_id = v.v_stack_id", "v_e_name", 82, "ZB - Tabla Derivacion Siniestro                             ", "e_INA01", @"''\\svrimage\zamba\' || replace(s.st_path,':','') || '\' || e.e_org_filename || '''", @"''\\svrimage\FILENET\' || replace(s.st_path,':','') || '\' || v.v_stack_file_id || '''"));
            Entities.Add(new Entity("", "saros.element e inner join saros.VERSION v on e.e_name = v_e_name inner join saros.STACK s on s.st_id = v.v_stack_id", "v_e_name", 21, "ZB - Tracking                                               ", "e_INA01", @"''\\svrimage\zamba\' || replace(s.st_path,':','') || '\' || e.e_org_filename || '''", @"''\\svrimage\FILENET\' || replace(s.st_path,':','') || '\' || v.v_stack_file_id || '''"));


            Indexs.Add(new Index("Test", "Test", 1,"Test",  IndexDataType.Alfanumerico));
            IndexLists.Add(new IndexList(1,"Test", "Test", "Test", "Test"));

            RefreshLists();
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

                                IConnection con = Zamba.Servers.Server.get_Con(DBTYPES.OracleClient, txtServer.Text, txtDB.Text, txtUser.Text, txtPassword.Text, false, true);
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
                                                string indexName = ((Zamba.Core.Index)newRes.Indexs[i]).Name;
                                                if (((List<string>)hsEntitiesAndAtributes[txtTemplate.Text.Trim()]).Contains(indexName))
                                                {
                                                    ((Zamba.Core.Index)newRes.Indexs[i]).DataTemp = dr.GetValue(dr.GetOrdinal(indexName)).ToString().Trim().Replace("'", "").Replace("´", "");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            ((Zamba.Core.Index)newRes.Indexs[0]).DataTemp = dr.GetValue(ordinalNroOrden).ToString().Trim();
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
        private DataSet executeDatasetSource(String query)
        {
            return Zamba.Servers.Server.get_Con(DBTYPES.OracleClient, txtServer.Text, txtDB.Text, txtUser.Text, txtPassword.Text, false, true).ExecuteDataset(CommandType.Text, query);
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

                            DataSet dsFile = executeDatasetSource(query.ToString());

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
                                            string indexName = ((Zamba.Core.Index)newRes.Indexs[i]).Name;
                                            if (hsIndexs.Contains(indexName))
                                            {
                                                ((Zamba.Core.Index)newRes.Indexs[i]).DataTemp = drFile[indexName].ToString().Trim().Replace("'", ""); ;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        ((Zamba.Core.Index)newRes.Indexs[0]).DataTemp = drFile["nro_orden"].ToString().Trim();
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
                DataSet dsAux = executeDatasetSource("select DESCRIPCIO, tmplate from easydoc.tmplates");

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
                dsAux = executeDatasetSource("select tmplate, campo from easydoc.zona order by tmplate");

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
                dsAux = executeDatasetSource("select volumen, mount from easydoc.montaje");

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
                    dsAux = executeDatasetSource("select DESCRIPCIO, tmplate from easydoc.tmplates order by DESCRIPCIO");

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

        private void button7_Click(object sender, EventArgs e)
        {
            SaveEntities(Entities, Path.Combine(Zamba.Membership.MembershipHelper.AppTempPath, "Entities.xml"));
        }

        private List<Entity> LoadEntities(string filepath)
        {
            string file = filepath;
            List<Entity> listofa = new List<Entity>();
            XmlSerializer formatter = new XmlSerializer(typeof(List<Entity>));
            FileStream aFile = new FileStream(file, FileMode.Open);
            byte[] buffer = new byte[aFile.Length];
            aFile.Read(buffer, 0, (int)aFile.Length);
            MemoryStream stream = new MemoryStream(buffer);
            aFile.Close();
            aFile.Dispose();
            return (List<Entity>)formatter.Deserialize(stream);
        }
        
        private void SaveEntities(List<Entity> listofa, string filepath)
        {
            string path = filepath;
            FileStream outFile = File.Create(path);
            XmlSerializer formatter = new XmlSerializer(typeof(List<Entity>));
            formatter.Serialize(outFile, listofa);
            outFile.Flush();
            outFile.Close();
            outFile.Dispose();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Entities = LoadEntities(Path.Combine(Zamba.Membership.MembershipHelper.AppTempPath, "Entities.xml"));
            RefreshLists();
        }

        private void RefreshLists()
        {
            this.radGridView1.DataSource = null;
            this.radGridView2.DataSource = null;
            this.radGridView3.DataSource = null;

            radGridView1.AutoGenerateColumns = true;
            radGridView2.AutoGenerateColumns = true;
            radGridView3.AutoGenerateColumns = true;

            this.radGridView1.DataSource = Entities;
            this.radGridView2.DataSource = Indexs;
            this.radGridView3.DataSource = IndexLists;
        }

        const string sourcequery = "select {0} from {1}";
        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Entity E in Entities)
                {
                    string query = string.Format("select *, {2} as File from {0} where {1}",E.SourceEntityTable, E.SourceCondition,E.SourceQueryFileColumns);
                    DataSet dssource = executeDatasetSource(query);
                    ProcessSourceEntity(dssource, E);
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

        }

        private void ProcessSourceEntity(DataSet dssource, Entity e)
        {
            try
            {
                IDocType DocType = Zamba.Data.DocTypesFactory.GetDocType(e.EntityId);
                if (DocType != null)
                {
                    Boolean FirstRow = true;
                    foreach (DataRow r in dssource.Tables[0].Rows)
                    {
                        if (FirstRow)
                        {
                            foreach (IIndex I in DocType.Indexs)
                            {
                                foreach(Index EI in Indexs)
                                {
                                    if (I.ID == EI.IndexId)
                                    {
                                        int SourceColumnIndex = r.Table.Columns.IndexOf(EI.SourceIndexColumn);
                                        EI.SourceIndexColumnOrdinal = SourceColumnIndex;
                                        break;
                                    }
                                }

                            }
                            //Ya tengo la definicion de los indices y el orden de las columnas.
                            FirstRow = false;
                        }

                        NewResult result = Results_Business.GetNewNewResult(e.EntityId);
                        result.File = r["File"].ToString();

                        foreach (IIndex I in result.Indexs)
                        {
                            foreach (Index IE in Indexs)
                            {
                                if (I.ID == IE.IndexId)
                                {
                                    I.Data = r[IE.SourceIndexColumnOrdinal].ToString();
                                    I.DataTemp = I.Data;
                                    break;
                                }
                            }
                        }
                        Results_Business.InsertDocument(ref result,false,false,true,false,false,false,false,false,false,false,0,false, e.RealFileExtension);
                    }
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }
    }

    public class Entity
    {
        public string SourceEntityName { get; set; }
        public string SourceEntityTable { get; set; }
        public string SourceKeyColumnName { get; set; }
        public Int64 EntityId { get; set; }
        public string EntityName { get; set; }
 
        public string SourceQueryFileColumns { get; set; }
        public string QueryOriginalFileColumn { get; set; }
        public object SourceCondition { get; internal set; }
        public string RealFileExtension { get; internal set; }

        public Entity() { }
        public Entity(string SourceEntityName, string SourceEntityTable, string SourceKeyColumnName, Int64 EntityId, string EntityName,string SourceFilterEntity
, string QueryFileColumns, string QueryOriginalFileColumn)
        {
            this.SourceEntityName = SourceEntityName;
            this.SourceEntityTable = SourceEntityTable;
            this.SourceKeyColumnName = SourceKeyColumnName;
            this.EntityId = EntityId;
            this.EntityName = EntityName;
            this.SourceQueryFileColumns = QueryFileColumns;
            this.SourceCondition = SourceFilterEntity + " = '" + SourceEntityName + "'";
            this.QueryOriginalFileColumn = QueryOriginalFileColumn;
        }

    }

    public class Index
    {
        public string SourceIndexName { get; set; }
        public string SourceIndexColumn { get; set; }
        public Int64 IndexId { get; set; }
        public string IndexName { get; set; }
    public IndexDataType Type { get; set; }
    public List<string> TransformValues { get; set; }
    public int SourceIndexColumnOrdinal { get; internal set; }

        public Index() { }

        public Index(string SourceIndexName, string SourceIndexColumn, Int64 IndexId, string IndexName, IndexDataType Type)
        {
            this.SourceIndexName = SourceIndexName;
            this.SourceIndexColumn = SourceIndexColumn;
            this.IndexId = IndexId;
            this.IndexName = IndexName;
            this.Type = Type;
        }
    }

    public class IndexList
    {
        public Int64 IndexId { get; set; }
        public string SourceIndexCode { get; set; }
        public string SourceIndexDescription { get; set; }
        public string IndexCode { get; set; }
        public string IndexDescription { get; set; }
        public IndexList() { }

        public IndexList(Int64 IndexId,
         string SourceIndexCode,
         string SourceIndexDescription,
         string IndexCode,
         string IndexDescription)
        {
            this.IndexId = IndexId;
            this.SourceIndexCode = SourceIndexCode;
            this.SourceIndexDescription = SourceIndexDescription;
            this.IndexCode = IndexCode;
            this.IndexDescription = IndexDescription;
        }
    }

}
