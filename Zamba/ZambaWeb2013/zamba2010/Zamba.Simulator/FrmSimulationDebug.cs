using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Zamba.Simulator
{
    public partial class FrmSimulationDebug : Form
    {
        public FrmSimulationDebug()
        {
            InitializeComponent();
        }

        private void FrmSimulationDebug_Load(object sender, EventArgs e)
        {
            //REGLA
            dgvRegla.Rows.Add(new object[] { "zvar(idreclamo)", "25687", "NUMERICO" });
            dgvRegla.Rows.Add(new object[] { "zvar(estudio)" , "26", "NUMERICO"});
            dgvRegla.Rows.Add(new object[] { "zvar(tipo)" , "3" , "NUMERICO"});

            //TAREA
            dgvTarea.Rows.Add(new object[] { "Nro Juicio", "106227", "NUMERICO" });
            dgvTarea.Rows.Add(new object[] { "Juicio o Mediación", "0", "NUMERICO" });
            dgvTarea.Rows.Add(new object[] { "Carátula", "GLORIA MELILLO C/ MARINA GUTIERREZ", "TEXTO" });
            dgvTarea.Rows.Add(new object[] { "ID Reclamo", "25687", "NUMERICO" });
            dgvTarea.Rows.Add(new object[] { "Nro de Siniestro", "94561", "NUMERICO" });
            dgvTarea.Rows.Add(new object[] { "Nro de Subsiniestro", "1", "NUMERICO" });
            dgvTarea.Rows.Add(new object[] { "Fecha Notificación", "05/06/2014", "FECHA" });
            dgvTarea.Rows.Add(new object[] { "Rama", "21", "NUMERICO" });
            //relleno de tarea
            dgvTarea.Rows.Add(new object[] { "Rama", "21", "NUMERICO" });
            dgvTarea.Rows.Add(new object[] { "Rama", "21", "NUMERICO" });
            dgvTarea.Rows.Add(new object[] { "Rama", "21", "NUMERICO" });
            dgvTarea.Rows.Add(new object[] { "Rama", "21", "NUMERICO" });
            dgvTarea.Rows.Add(new object[] { "Rama", "21", "NUMERICO" });
            dgvTarea.Rows.Add(new object[] { "Rama", "21", "NUMERICO" });
            dgvTarea.Rows.Add(new object[] { "Rama", "21", "NUMERICO" });
            dgvTarea.Rows.Add(new object[] { "Rama", "21", "NUMERICO" });
            dgvTarea.Rows.Add(new object[] { "Rama", "21", "NUMERICO" });
            dgvTarea.Rows.Add(new object[] { "Rama", "21", "NUMERICO" });
            dgvTarea.Rows.Add(new object[] { "Rama", "21", "NUMERICO" });
            dgvTarea.Rows.Add(new object[] { "Rama", "21", "NUMERICO" });

            //GLOBAL
            dgvGlobales.Rows.Add(new object[] { "zvar(idReclamoAsociado)", "29541", "NUMERICO" });
            dgvGlobales.Rows.Add(new object[] { "zvar(flagNotificar)", "SI", "SI/NO" });
            dgvGlobales.Rows.Add(new object[] { "zvar(demandado)", "96", "NUMERICO" });
            dgvGlobales.Rows.Add(new object[] { "zvar(rutaCarta)", @"C:\Shared Folder\Printer\96846464684.PNG", "TEXTO" });
            dgvGlobales.Rows.Add(new object[] { "zvar(idreclamo)", "25687", "NUMERICO" });
            dgvGlobales.Rows.Add(new object[] { "zvar(estudio)", "26", "NUMERICO" });
            dgvGlobales.Rows.Add(new object[] { "zvar(tipo)", "3", "NUMERICO" });
        }
    }
}
