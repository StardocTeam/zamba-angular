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
	partial class DgSearchIndexDelete
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
            this.SearchIndexDelete = new System.Workflow.Activities.CodeActivity();
            // 
            // SearchIndexDelete
            // 
            this.SearchIndexDelete.Description = "Borro todos los items de la ZSearchValues_DT por el id del documento";
            this.SearchIndexDelete.Name = "SearchIndexDelete";
            // 
            // DgSearchIndexDelete
            // 
            this.Activities.Add(this.SearchIndexDelete);
            this.Name = "DgSearchIndexDelete";
            this.CanModifyActivities = false;

		}

		#endregion

        private CodeActivity SearchIndexDelete;

    }
}
