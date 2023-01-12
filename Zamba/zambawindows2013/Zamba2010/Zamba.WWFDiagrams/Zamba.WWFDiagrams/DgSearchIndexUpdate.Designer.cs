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
	partial class DgSearchIndexUpdate
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
            this.SearchIndexInsert = new System.Workflow.Activities.CodeActivity();
            this.SearchIndexDelete = new System.Workflow.Activities.CodeActivity();
            // 
            // SearchIndexInsert
            // 
            this.SearchIndexInsert.Description = "Llamo al metodo que inserta";
            this.SearchIndexInsert.Name = "SearchIndexInsert";
            // 
            // SearchIndexDelete
            // 
            this.SearchIndexDelete.Description = "Borro los indices actuales del documento";
            this.SearchIndexDelete.Name = "SearchIndexDelete";
            // 
            // DgSearchIndexUpdate
            // 
            this.Activities.Add(this.SearchIndexDelete);
            this.Activities.Add(this.SearchIndexInsert);
            this.Name = "DgSearchIndexUpdate";
            this.CanModifyActivities = false;

		}

		#endregion

        private CodeActivity SearchIndexInsert;
        private CodeActivity SearchIndexDelete;





    }
}
