using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace Zamba.DBPortableDiagram
{
	public sealed partial class DBPortableDiagram: SequentialWorkflowActivity
	{
		public DBPortableDiagram()
		{
			InitializeComponent();
		}

        public Hashtable hsObjects;
        public Hashtable hsObjectsType;
        private Boolean blnExisteID;
        private Boolean blnExisteNombre;

        private void CargarBD_ExecuteCode(object sender, EventArgs e)
        {

        }

        private void ObtenerTipoObjeto_ExecuteCode(object sender, EventArgs e)
        {

        }

        private void ObtenerObjeto_ExecuteCode(object sender, EventArgs e)
        {

        }

        private void ConsultarUsuario_ExecuteCode(object sender, EventArgs e)
        {

        }

        private void SaltearObjeto_ExecuteCode(object sender, EventArgs e)
        {

        }

        private void Reemplazar_ExecuteCode(object sender, EventArgs e)
        {

        }

        private void ReemplazarRelacionesyParametros_ExecuteCode(object sender, EventArgs e)
        {

        }

        private void GenerarNuevoID_ExecuteCode(object sender, EventArgs e)
        {

        }

        private void InsertarObjeto_ExecuteCode(object sender, EventArgs e)
        {

        }

        private void ObtenerExistenciaID_ExecuteCode(object sender, EventArgs e)
        {

        }

        private void ObtenerExistenciaNombre_ExecuteCode(object sender, EventArgs e)
        {

        }

        private void ActualizarObjLastId_ExecuteCode(object sender, EventArgs e)
        {

        }
	}

}
