using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Reflection;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace Zamba.DBPortableDiagram
{
	partial class DBPortableDiagram
	{
		#region Designer generated code
		
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
        [System.Diagnostics.DebuggerNonUserCode]
		private void InitializeComponent()
		{
            this.CanModifyActivities = true;
            System.Workflow.Activities.Rules.RuleConditionReference ruleconditionreference1 = new System.Workflow.Activities.Rules.RuleConditionReference();
            System.Workflow.Activities.Rules.RuleConditionReference ruleconditionreference2 = new System.Workflow.Activities.Rules.RuleConditionReference();
            System.Workflow.Activities.Rules.RuleConditionReference ruleconditionreference3 = new System.Workflow.Activities.Rules.RuleConditionReference();
            System.Workflow.Activities.Rules.RuleConditionReference ruleconditionreference4 = new System.Workflow.Activities.Rules.RuleConditionReference();
            System.Workflow.Activities.Rules.RuleConditionReference ruleconditionreference5 = new System.Workflow.Activities.Rules.RuleConditionReference();
            this.EjecutarReemplazarRelacionesyParametros = new System.Workflow.Activities.CodeActivity();
            this.GenerarNuevoID = new System.Workflow.Activities.CodeActivity();
            this.ReemplazarRelacionesyParametros = new System.Workflow.Activities.CodeActivity();
            this.Reemplazar = new System.Workflow.Activities.CodeActivity();
            this.EjecutarSaltearObjeto = new System.Workflow.Activities.CodeActivity();
            this.EjecutarObtenerObjeto = new System.Workflow.Activities.CodeActivity();
            this.GenerarNuevoObjeto = new System.Workflow.Activities.SequenceActivity();
            this.ReemplazarObjeto = new System.Workflow.Activities.SequenceActivity();
            this.SaltearSoloObjeto = new System.Workflow.Activities.SequenceActivity();
            this.SaltearObjectoYRelaciones = new System.Workflow.Activities.SequenceActivity();
            this.InsertarObjeto = new System.Workflow.Activities.CodeActivity();
            this.EjecutarConsultarUsuario = new System.Workflow.Activities.CodeActivity();
            this.parallelActivity1 = new System.Workflow.Activities.ParallelActivity();
            this.ConsultarUsuario = new System.Workflow.Activities.CodeActivity();
            this.SaltearObjeto = new System.Workflow.Activities.CodeActivity();
            this.NoExisteNombre = new System.Workflow.Activities.IfElseBranchActivity();
            this.SiExisteNombre = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifElseBranchActivity2 = new System.Workflow.Activities.IfElseBranchActivity();
            this.SiEsIgual = new System.Workflow.Activities.IfElseBranchActivity();
            this.ExisteNombre = new System.Workflow.Activities.IfElseActivity();
            this.ObtenerExistenciaNombre = new System.Workflow.Activities.CodeActivity();
            this.EsIgualElObjeto = new System.Workflow.Activities.IfElseActivity();
            this.NoExisteID = new System.Workflow.Activities.IfElseBranchActivity();
            this.SiExisteID = new System.Workflow.Activities.IfElseBranchActivity();
            this.ExisteID = new System.Workflow.Activities.IfElseActivity();
            this.ObtenerExistenciaID = new System.Workflow.Activities.CodeActivity();
            this.ObtenerObjeto = new System.Workflow.Activities.CodeActivity();
            this.sequenceActivity1 = new System.Workflow.Activities.SequenceActivity();
            this.whileActivity2 = new System.Workflow.Activities.WhileActivity();
            this.ObtenerTipoObjeto = new System.Workflow.Activities.CodeActivity();
            this.sequenceActivity2 = new System.Workflow.Activities.SequenceActivity();
            this.ActualizarObjLastId = new System.Workflow.Activities.CodeActivity();
            this.whileActivity1 = new System.Workflow.Activities.WhileActivity();
            this.CargarBD = new System.Workflow.Activities.CodeActivity();
            // 
            // EjecutarReemplazarRelacionesyParametros
            // 
            this.EjecutarReemplazarRelacionesyParametros.Name = "EjecutarReemplazarRelacionesyParametros";
            this.EjecutarReemplazarRelacionesyParametros.ExecuteCode += new System.EventHandler(this.ReemplazarRelacionesyParametros_ExecuteCode);
            // 
            // GenerarNuevoID
            // 
            this.GenerarNuevoID.Description = "Generar un nuevo ID y cambiar ID Objeto y ID en relaciones o Parametros";
            this.GenerarNuevoID.Name = "GenerarNuevoID";
            this.GenerarNuevoID.ExecuteCode += new System.EventHandler(this.GenerarNuevoID_ExecuteCode);
            // 
            // ReemplazarRelacionesyParametros
            // 
            this.ReemplazarRelacionesyParametros.Name = "ReemplazarRelacionesyParametros";
            this.ReemplazarRelacionesyParametros.ExecuteCode += new System.EventHandler(this.ReemplazarRelacionesyParametros_ExecuteCode);
            // 
            // Reemplazar
            // 
            this.Reemplazar.Name = "Reemplazar";
            this.Reemplazar.ExecuteCode += new System.EventHandler(this.Reemplazar_ExecuteCode);
            // 
            // EjecutarSaltearObjeto
            // 
            this.EjecutarSaltearObjeto.Name = "EjecutarSaltearObjeto";
            this.EjecutarSaltearObjeto.ExecuteCode += new System.EventHandler(this.SaltearObjeto_ExecuteCode);
            // 
            // EjecutarObtenerObjeto
            // 
            this.EjecutarObtenerObjeto.Name = "EjecutarObtenerObjeto";
            this.EjecutarObtenerObjeto.ExecuteCode += new System.EventHandler(this.ObtenerObjeto_ExecuteCode);
            // 
            // GenerarNuevoObjeto
            // 
            this.GenerarNuevoObjeto.Activities.Add(this.GenerarNuevoID);
            this.GenerarNuevoObjeto.Activities.Add(this.EjecutarReemplazarRelacionesyParametros);
            this.GenerarNuevoObjeto.Name = "GenerarNuevoObjeto";
            // 
            // ReemplazarObjeto
            // 
            this.ReemplazarObjeto.Activities.Add(this.Reemplazar);
            this.ReemplazarObjeto.Activities.Add(this.ReemplazarRelacionesyParametros);
            this.ReemplazarObjeto.Name = "ReemplazarObjeto";
            // 
            // SaltearSoloObjeto
            // 
            this.SaltearSoloObjeto.Activities.Add(this.EjecutarSaltearObjeto);
            this.SaltearSoloObjeto.Name = "SaltearSoloObjeto";
            // 
            // SaltearObjectoYRelaciones
            // 
            this.SaltearObjectoYRelaciones.Activities.Add(this.EjecutarObtenerObjeto);
            this.SaltearObjectoYRelaciones.Name = "SaltearObjectoYRelaciones";
            // 
            // InsertarObjeto
            // 
            this.InsertarObjeto.Description = "Crea el objeto en la BD";
            this.InsertarObjeto.Name = "InsertarObjeto";
            this.InsertarObjeto.ExecuteCode += new System.EventHandler(this.InsertarObjeto_ExecuteCode);
            // 
            // EjecutarConsultarUsuario
            // 
            this.EjecutarConsultarUsuario.Description = "Ejecutar la regla de consulta al usuario";
            this.EjecutarConsultarUsuario.Name = "EjecutarConsultarUsuario";
            this.EjecutarConsultarUsuario.ExecuteCode += new System.EventHandler(this.ConsultarUsuario_ExecuteCode);
            // 
            // parallelActivity1
            // 
            this.parallelActivity1.Activities.Add(this.SaltearObjectoYRelaciones);
            this.parallelActivity1.Activities.Add(this.SaltearSoloObjeto);
            this.parallelActivity1.Activities.Add(this.ReemplazarObjeto);
            this.parallelActivity1.Activities.Add(this.GenerarNuevoObjeto);
            this.parallelActivity1.Name = "parallelActivity1";
            // 
            // ConsultarUsuario
            // 
            this.ConsultarUsuario.Description = "Se consulta al usuario sobre como continuar";
            this.ConsultarUsuario.Name = "ConsultarUsuario";
            this.ConsultarUsuario.ExecuteCode += new System.EventHandler(this.ConsultarUsuario_ExecuteCode);
            // 
            // SaltearObjeto
            // 
            this.SaltearObjeto.Name = "SaltearObjeto";
            this.SaltearObjeto.ExecuteCode += new System.EventHandler(this.SaltearObjeto_ExecuteCode);
            // 
            // NoExisteNombre
            // 
            this.NoExisteNombre.Activities.Add(this.InsertarObjeto);
            this.NoExisteNombre.Name = "NoExisteNombre";
            // 
            // SiExisteNombre
            // 
            this.SiExisteNombre.Activities.Add(this.EjecutarConsultarUsuario);
            ruleconditionreference1.ConditionName = "Condition4";
            this.SiExisteNombre.Condition = ruleconditionreference1;
            this.SiExisteNombre.Name = "SiExisteNombre";
            // 
            // ifElseBranchActivity2
            // 
            this.ifElseBranchActivity2.Activities.Add(this.ConsultarUsuario);
            this.ifElseBranchActivity2.Activities.Add(this.parallelActivity1);
            this.ifElseBranchActivity2.Description = "NoEsDiferente";
            this.ifElseBranchActivity2.Name = "ifElseBranchActivity2";
            // 
            // SiEsIgual
            // 
            this.SiEsIgual.Activities.Add(this.SaltearObjeto);
            ruleconditionreference2.ConditionName = "Condition3";
            this.SiEsIgual.Condition = ruleconditionreference2;
            this.SiEsIgual.Name = "SiEsIgual";
            // 
            // ExisteNombre
            // 
            this.ExisteNombre.Activities.Add(this.SiExisteNombre);
            this.ExisteNombre.Activities.Add(this.NoExisteNombre);
            this.ExisteNombre.Description = "Verifica la existencia del nombre";
            this.ExisteNombre.Name = "ExisteNombre";
            // 
            // ObtenerExistenciaNombre
            // 
            this.ObtenerExistenciaNombre.Name = "ObtenerExistenciaNombre";
            this.ObtenerExistenciaNombre.ExecuteCode += new System.EventHandler(this.ObtenerExistenciaNombre_ExecuteCode);
            // 
            // EsIgualElObjeto
            // 
            this.EsIgualElObjeto.Activities.Add(this.SiEsIgual);
            this.EsIgualElObjeto.Activities.Add(this.ifElseBranchActivity2);
            this.EsIgualElObjeto.Description = "Verifico si el objeto es igual al ya existente";
            this.EsIgualElObjeto.Name = "EsIgualElObjeto";
            // 
            // NoExisteID
            // 
            this.NoExisteID.Activities.Add(this.ObtenerExistenciaNombre);
            this.NoExisteID.Activities.Add(this.ExisteNombre);
            this.NoExisteID.Name = "NoExisteID";
            // 
            // SiExisteID
            // 
            this.SiExisteID.Activities.Add(this.EsIgualElObjeto);
            ruleconditionreference3.ConditionName = "Condition2";
            this.SiExisteID.Condition = ruleconditionreference3;
            this.SiExisteID.Name = "SiExisteID";
            // 
            // ExisteID
            // 
            this.ExisteID.Activities.Add(this.SiExisteID);
            this.ExisteID.Activities.Add(this.NoExisteID);
            this.ExisteID.Description = "Se verifica la existencia del ID";
            this.ExisteID.Name = "ExisteID";
            // 
            // ObtenerExistenciaID
            // 
            this.ObtenerExistenciaID.Name = "ObtenerExistenciaID";
            this.ObtenerExistenciaID.ExecuteCode += new System.EventHandler(this.ObtenerExistenciaID_ExecuteCode);
            // 
            // ObtenerObjeto
            // 
            this.ObtenerObjeto.Name = "ObtenerObjeto";
            this.ObtenerObjeto.ExecuteCode += new System.EventHandler(this.ObtenerObjeto_ExecuteCode);
            // 
            // sequenceActivity1
            // 
            this.sequenceActivity1.Activities.Add(this.ObtenerObjeto);
            this.sequenceActivity1.Activities.Add(this.ObtenerExistenciaID);
            this.sequenceActivity1.Activities.Add(this.ExisteID);
            this.sequenceActivity1.Name = "sequenceActivity1";
            // 
            // whileActivity2
            // 
            this.whileActivity2.Activities.Add(this.sequenceActivity1);
            ruleconditionreference4.ConditionName = "Condition2";
            this.whileActivity2.Condition = ruleconditionreference4;
            this.whileActivity2.Name = "whileActivity2";
            // 
            // ObtenerTipoObjeto
            // 
            this.ObtenerTipoObjeto.Name = "ObtenerTipoObjeto";
            this.ObtenerTipoObjeto.ExecuteCode += new System.EventHandler(this.ObtenerTipoObjeto_ExecuteCode);
            // 
            // sequenceActivity2
            // 
            this.sequenceActivity2.Activities.Add(this.ObtenerTipoObjeto);
            this.sequenceActivity2.Activities.Add(this.whileActivity2);
            this.sequenceActivity2.Name = "sequenceActivity2";
            // 
            // ActualizarObjLastId
            // 
            this.ActualizarObjLastId.Description = "Actualizar los objects last ID";
            this.ActualizarObjLastId.Name = "ActualizarObjLastId";
            this.ActualizarObjLastId.ExecuteCode += new System.EventHandler(this.ActualizarObjLastId_ExecuteCode);
            // 
            // whileActivity1
            // 
            this.whileActivity1.Activities.Add(this.sequenceActivity2);
            ruleconditionreference5.ConditionName = "Condition1";
            this.whileActivity1.Condition = ruleconditionreference5;
            this.whileActivity1.Name = "whileActivity1";
            // 
            // CargarBD
            // 
            this.CargarBD.Description = "Creacion y migracion de Datos";
            this.CargarBD.Name = "CargarBD";
            this.CargarBD.ExecuteCode += new System.EventHandler(this.CargarBD_ExecuteCode);
            // 
            // DBPortableDiagram
            // 
            this.Activities.Add(this.CargarBD);
            this.Activities.Add(this.whileActivity1);
            this.Activities.Add(this.ActualizarObjLastId);
            this.Name = "DBPortableDiagram";
            this.CanModifyActivities = false;

		}

		#endregion

        private CodeActivity ActualizarObjLastId;
        private CodeActivity ObtenerExistenciaID;
        private CodeActivity ObtenerExistenciaNombre;
        private IfElseBranchActivity NoExisteID;
        private IfElseBranchActivity SiExisteID;
        private IfElseActivity ExisteID;
        private CodeActivity ObtenerObjeto;
        private SequenceActivity sequenceActivity1;
        private WhileActivity whileActivity2;
        private CodeActivity ObtenerTipoObjeto;
        private SequenceActivity sequenceActivity2;
        private WhileActivity whileActivity1;
        private SequenceActivity SaltearSoloObjeto;
        private SequenceActivity SaltearObjectoYRelaciones;
        private CodeActivity InsertarObjeto;
        private CodeActivity EjecutarConsultarUsuario;
        private ParallelActivity parallelActivity1;
        private CodeActivity ConsultarUsuario;
        private CodeActivity SaltearObjeto;
        private IfElseBranchActivity NoExisteNombre;
        private IfElseBranchActivity SiExisteNombre;
        private IfElseBranchActivity ifElseBranchActivity2;
        private IfElseBranchActivity SiEsIgual;
        private IfElseActivity ExisteNombre;
        private IfElseActivity EsIgualElObjeto;
        private CodeActivity GenerarNuevoID;
        private CodeActivity Reemplazar;
        private CodeActivity EjecutarSaltearObjeto;
        private CodeActivity EjecutarObtenerObjeto;
        private SequenceActivity GenerarNuevoObjeto;
        private SequenceActivity ReemplazarObjeto;
        private CodeActivity EjecutarReemplazarRelacionesyParametros;
        private CodeActivity ReemplazarRelacionesyParametros;
        private CodeActivity CargarBD;




























    }
}
