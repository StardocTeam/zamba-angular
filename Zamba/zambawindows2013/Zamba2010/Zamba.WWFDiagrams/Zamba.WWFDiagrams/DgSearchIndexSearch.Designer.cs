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
	partial class DgSearchIndexSearch
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
            this.EjecutoBusqueda = new System.Workflow.Activities.CodeActivity();
            this.ObtengoResultsIds = new System.Workflow.Activities.CodeActivity();
            this.ObtengoWordId = new System.Workflow.Activities.CodeActivity();
            this.SiNoEsta = new System.Workflow.Activities.IfElseBranchActivity();
            this.SiEsta = new System.Workflow.Activities.IfElseBranchActivity();
            this.SearchIndexSearch = new System.Workflow.Activities.IfElseActivity();
            // 
            // EjecutoBusqueda
            // 
            this.EjecutoBusqueda.Description = "Ejecuto la busqueda de zamba por ids";
            this.EjecutoBusqueda.Name = "EjecutoBusqueda";
            // 
            // ObtengoResultsIds
            // 
            this.ObtengoResultsIds.Description = "Obtengo los ids de los documentos";
            this.ObtengoResultsIds.Name = "ObtengoResultsIds";
            // 
            // ObtengoWordId
            // 
            this.ObtengoWordId.Description = "Obtengo el id de la palabra";
            this.ObtengoWordId.Name = "ObtengoWordId";
            // 
            // SiNoEsta
            // 
            this.SiNoEsta.Name = "SiNoEsta";
            // 
            // SiEsta
            // 
            this.SiEsta.Activities.Add(this.ObtengoWordId);
            this.SiEsta.Activities.Add(this.ObtengoResultsIds);
            this.SiEsta.Activities.Add(this.EjecutoBusqueda);
            this.SiEsta.Name = "SiEsta";
            // 
            // SearchIndexSearch
            // 
            this.SearchIndexSearch.Activities.Add(this.SiEsta);
            this.SearchIndexSearch.Activities.Add(this.SiNoEsta);
            this.SearchIndexSearch.Description = "Si esta la palabra";
            this.SearchIndexSearch.Name = "SearchIndexSearch";
            // 
            // DgSearchIndexSearch
            // 
            this.Activities.Add(this.SearchIndexSearch);
            this.Name = "DgSearchIndexSearch";
            this.CanModifyActivities = false;

		}

		#endregion

        private CodeActivity EjecutoBusqueda;
        private CodeActivity ObtengoResultsIds;
        private CodeActivity ObtengoWordId;
        private IfElseBranchActivity SiNoEsta;
        private IfElseBranchActivity SiEsta;
        private IfElseActivity SearchIndexSearch;


    }
}
