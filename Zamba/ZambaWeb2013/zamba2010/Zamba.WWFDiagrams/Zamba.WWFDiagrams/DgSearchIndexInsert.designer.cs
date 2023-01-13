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

namespace Zamba.WWFDiagrams
{
	partial class DgSearchIndexInsert
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
            this.Creo = new System.Workflow.Activities.CodeActivity();
            this.NoEsta = new System.Workflow.Activities.IfElseBranchActivity();
            this.Esta = new System.Workflow.Activities.IfElseBranchActivity();
            this.Inserto = new System.Workflow.Activities.CodeActivity();
            this.BuscarPalabra = new System.Workflow.Activities.IfElseActivity();
            this.SiNoEsta = new System.Workflow.Activities.IfElseBranchActivity();
            this.SiEsta = new System.Workflow.Activities.IfElseBranchActivity();
            this.ValidarPalabra = new System.Workflow.Activities.IfElseActivity();
            this.SearchIndexInsert = new System.Workflow.Activities.SequenceActivity();
            // 
            // Creo
            // 
            this.Creo.Description = "eo la palabra en la ZSeachValues";
            this.Creo.Name = "Creo";
            // 
            // NoEsta
            // 
            this.NoEsta.Activities.Add(this.Creo);
            this.NoEsta.Name = "NoEsta";
            // 
            // Esta
            // 
            this.Esta.Name = "Esta";
            // 
            // Inserto
            // 
            this.Inserto.Description = "Inserto la palabra en la ZSearchValues_DT";
            this.Inserto.Name = "Inserto";
            // 
            // BuscarPalabra
            // 
            this.BuscarPalabra.Activities.Add(this.Esta);
            this.BuscarPalabra.Activities.Add(this.NoEsta);
            this.BuscarPalabra.Description = "Buscar si la palabra ya esta en el indexado";
            this.BuscarPalabra.Name = "BuscarPalabra";
            // 
            // SiNoEsta
            // 
            this.SiNoEsta.Activities.Add(this.BuscarPalabra);
            this.SiNoEsta.Activities.Add(this.Inserto);
            this.SiNoEsta.Name = "SiNoEsta";
            // 
            // SiEsta
            // 
            this.SiEsta.Name = "SiEsta";
            // 
            // ValidarPalabra
            // 
            this.ValidarPalabra.Activities.Add(this.SiEsta);
            this.ValidarPalabra.Activities.Add(this.SiNoEsta);
            this.ValidarPalabra.Description = "Valido que la palabra no este en el diccionario de ZSearchDictionary";
            this.ValidarPalabra.Name = "ValidarPalabra";
            // 
            // SearchIndexInsert
            // 
            this.SearchIndexInsert.Activities.Add(this.ValidarPalabra);
            this.SearchIndexInsert.Name = "SearchIndexInsert";
            // 
            // DgSearchIndexInsert
            // 
            this.Activities.Add(this.SearchIndexInsert);
            this.Name = "DgSearchIndexInsert";
            this.CanModifyActivities = false;

		}

		#endregion

        private IfElseBranchActivity NoEsta;
        private IfElseBranchActivity Esta;
        private IfElseActivity BuscarPalabra;
        private SequenceActivity SearchIndexInsert;
        private CodeActivity Creo;
        private IfElseBranchActivity SiNoEsta;
        private IfElseBranchActivity SiEsta;
        private CodeActivity Inserto;
        private IfElseActivity ValidarPalabra;






    }
}
