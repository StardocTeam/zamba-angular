using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using Zamba.ExportaOutlook.RServices;
using Zamba.Services.RemoteEntities;
using Zamba.Services.RemoteInterfaces;
using Zamba.Services.RemoteEnumerators;
using Office = Microsoft.Office.Core;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Collections;
using System.Diagnostics;
using Zamba.Services.Remoting;
using Zamba.Core;

namespace ExportaOutlook.Forms
{
    /// <summary>
    /// Formulario encargado de configurar el enlace o mapeo entre las propiedades de un mail y los atributos de una entidad de Zamba.
    /// </summary>
    public partial class FrmMapEntityIndexs : Form
    {
        Dictionary<long, List<IAttributeMap>> eMaps = new Dictionary<long, List<IAttributeMap>>();
      
        ZambaRemoteClass ZRC = new ZambaRemoteClass();

        public FrmMapEntityIndexs()
        {
            InitializeComponent();
            this.cboEntity.SelectedIndexChanged -= new System.EventHandler(this.cboEntity_SelectedIndexChanged);
            this.cboEntity.SelectedIndexChanged += new System.EventHandler(this.cboEntity_SelectedIndexChanged);
        }

      

        /// <summary>
        /// Carga inicial de las entidades
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMapIndexs_Load(object sender, EventArgs e)
        {
            try
            {
                //Se cargan las entidades en el combo.
                DataTable dtDocTypes = ZRC.GetDocTypeNamesAndIds();
                cboEntity.DataSource = dtDocTypes;
                cboEntity.ValueMember = dtDocTypes.Columns[0].ColumnName;
                cboEntity.DisplayMember = dtDocTypes.Columns[1].ColumnName;

                this.lstMappedIndexs.View = View.SmallIcon;
                
                foreach (DataRow r in dtDocTypes.Rows)
                {
                    var EntityId = r[dtDocTypes.Columns[0].ColumnName];
                    var EntityName = r[dtDocTypes.Columns[1].ColumnName];


                    var Attrs = GetIndexs(Int64.Parse(EntityId.ToString()));

                    List<AttributeIndexMap> MappedIndexs =  ZRC.GetMappedIndexs(Int64.Parse(EntityId.ToString()));

                  
                    if (MappedIndexs != null &&  MappedIndexs.Count > 0)
                    {
                        var newgroup = new ListViewGroup(EntityName.ToString().Trim());
                        this.lstMappedIndexs.Groups.Add(newgroup);

                        foreach(AttributeIndexMap A in MappedIndexs)
                        {
                            var newitem = new ListViewItem(((MailAttributeType)A.AttributeId).ToString().Trim() + " - " + Zamba.Core.IndexsBusiness.GetIndexNameById(A.IndexId).Trim(), newgroup);
                            this.lstMappedIndexs.Items.Add(newitem);
                        }
                     
                    }
                }            

            }
            catch (Exception ex)
           

            {
                MessageBox.Show("Error al cargar las entidades de Zamba", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ZTrace.WriteLineIf(ZTrace.IsError, "Error al cargar las entidades de Zamba.\n" + ex.ToString());
                ZClass.raiseerror(ex);
                this.Close();
            }
        }

        /// <summary>
        /// Carga dinámica de los atributos de la entidad seleccionada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboEntity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cboEntity.ValueMember))
            {
                List<AttributeIndexMap> hshMaps = null;
                List<IAttributeMap> indexs;

                try
                {
                    //Se obtienen los atributos de la entidad
                    Int64 docTypeId = Int64.Parse(cboEntity.SelectedValue.ToString());
                    indexs = GetIndexs(docTypeId);

                    //Se agrega un vacío para dejar sin configurar
                    indexs.Insert(0, new AttributeMap(0, "Sin configurar"));

                    //Se completa el combo de todos los campos del mail. Deben ser instancias diferentes.
                    FillComboData(ref cboSender, indexs);
                    FillComboData(ref cboTo, indexs);
                    FillComboData(ref cboCC, indexs);
                    FillComboData(ref cboBCC, indexs);
                    FillComboData(ref cboSubject, indexs);
                    FillComboData(ref cboMessage, indexs);
                    FillComboData(ref cboDate, indexs);
                    FillComboData(ref cboCode, indexs);
                    FillComboData(ref cboWindows, indexs);
                    FillComboData(ref cboZamba, indexs);
                    FillComboData(ref cboExportType, indexs);
                    FillComboData(ref cboAutoincremental, indexs);

                    //Se buscan atributos mapeados
                    hshMaps = ZRC.GetMappedIndexs(docTypeId);
                    if (hshMaps != null && hshMaps.Count > 0)
                    {
                        //Se cargan los combos con los atributos
                        SetComboValue(ref cboSender, hshMaps, 0);
                        SetComboValue(ref cboTo, hshMaps, 1);
                        SetComboValue(ref cboCC, hshMaps, 2);
                        SetComboValue(ref cboBCC, hshMaps, 3);
                        SetComboValue(ref cboDate, hshMaps, 4);
                        SetComboValue(ref cboSubject, hshMaps, 5);
                        SetComboValue(ref cboMessage, hshMaps, 6);
                        SetComboValue(ref cboWindows, hshMaps, 7);
                        SetComboValue(ref cboZamba, hshMaps, 8);
                        SetComboValue(ref cboCode, hshMaps, 9);
                        SetComboValue(ref cboExportType, hshMaps, 10);
                        SetComboValue(ref cboAutoincremental, hshMaps, 11);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los atributos de la entidad seleccionada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ZTrace.WriteLineIf(ZTrace.IsError, "Error al cargar los atributos de la entidad seleccionada.\n" + ex.ToString());
                    ZClass.raiseerror(ex);
                    this.Close();
                }
               
            }
        }

        /// <summary>
        /// Carga un valor previamente configurado en un combo
        /// </summary>
        /// <param name="cbo"></param>
        /// <param name="hshMaps"></param>
        /// <param name="key"></param>
        private void SetComboValue(ref ComboBox cbo, List<AttributeIndexMap> hshMaps, int key)
        {
            foreach (AttributeIndexMap A in hshMaps)
            {
                if (A.AttributeId == key)
                {
                    cbo.SelectedValue = A.IndexId;
                }
            }
        }

        /// <summary>
        /// Obtiene el listado de atributos de una entidad
        /// </summary>
        /// <param name="docTypeId"></param>
        /// <returns></returns>
        private List<IAttributeMap> GetIndexs(long docTypeId)
        {
            if (!eMaps.ContainsKey(docTypeId))
                eMaps.Add(docTypeId, ZRC.GetEntityAttributes(docTypeId).Attributes);

            return eMaps[docTypeId];
        }

        /// <summary>
        /// Carga los datos de un combo desde una lista de atributos
        /// </summary>
        /// <param name="cbo"></param>
        /// <param name="data"></param>
        private void FillComboData(ref ComboBox cbo, List<IAttributeMap> data)
        {
            cbo.DataSource = data.ToList<IAttributeMap>();
            cbo.DisplayMember = "Name";
            cbo.ValueMember = "Id";
        }

        /// <summary>
        /// Cierra el formulario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Se impactan las configuraciones de los mapeos realizados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            EntityMap eMap = new EntityMap();

            try
            {
                eMap.DocTypeId = Int64.Parse(cboEntity.SelectedValue.ToString());

                //Se recupera el valor de cada combo y se completa en el objeto eMap
                RecoverComboValue(ref cboAutoincremental, ref eMap, MailAttributeType.Autoincremental);
                RecoverComboValue(ref cboBCC, ref eMap, MailAttributeType.BCC);
                RecoverComboValue(ref cboCC, ref eMap, MailAttributeType.CC);
                RecoverComboValue(ref cboDate, ref eMap, MailAttributeType.Date);
                RecoverComboValue(ref cboExportType, ref eMap, MailAttributeType.ExportType);
                RecoverComboValue(ref cboCode, ref eMap, MailAttributeType.Code);
                RecoverComboValue(ref cboMessage, ref eMap, MailAttributeType.Message);
                RecoverComboValue(ref cboSender, ref eMap, MailAttributeType.Sender);
                RecoverComboValue(ref cboSubject, ref eMap, MailAttributeType.Subject);
                RecoverComboValue(ref cboTo, ref eMap, MailAttributeType.To);
                RecoverComboValue(ref cboWindows, ref eMap, MailAttributeType.WindowsUser);
                RecoverComboValue(ref cboZamba, ref eMap, MailAttributeType.ZambaUser);

                //Se impactan las modificaciones realizadas
                ZRC.MapEntity((IEntityMap)eMap);

                //Se actualiza la cache
                eMaps.Remove(eMap.DocTypeId);

                //Por defecto el form se cierra al guardar
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar los atributos de la entidad", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ZTrace.WriteLineIf(ZTrace.IsError, "Error al guardar los atributos de la entidad.\n" + ex.ToString());
                ZClass.raiseerror(ex);
            }
            finally
            {
                eMap = null;
                this.Close();
            }
        }

        /// <summary>
        /// Recupera el valor de un combo y lo guarda en el objeto eMap
        /// </summary>
        /// <param name="cbo"></param>
        /// <param name="eMap"></param>
        /// <param name="mailType"></param>
        private void RecoverComboValue(ref ComboBox cbo, ref EntityMap eMap, MailAttributeType mailType)
        {
            if (cbo.SelectedIndex > 0)
            {
                eMap.Add((long)cbo.SelectedValue, cbo.Text, mailType);
            }
        }
    }
}
