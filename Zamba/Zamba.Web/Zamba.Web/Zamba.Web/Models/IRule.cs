using Zamba.Core;
using System.Collections;
using System.Collections.Generic;
using System;
using Zamba.Web.App_Code.Helpers;
using Zamba.Framework;

/// <summary>
/// Summary description for IRule
/// </summary>
public interface IRule
{
    event WFExecution._continueExecution ContinueExecution;
    RuleExecutionResult ExecutionResult { get; set; }
    RulePendingEvents PendigEvent { get; set; }
    List<Int64> ExecutedIDs { get; set; }
    Hashtable Params { get; set; }
    Int64 RuleID { get; set; }
    string Path { get; set; }
    List<Zamba.Core.ITaskResult> results { get; set; }
    Int64 TaskID { get; set; }
    List<long> PendingChildRules { get; set; }
    void LoadOptions();
    void _btnOk_Click(object sender, EventArgs e);
    void _btnCancel_Click(object sender, EventArgs e);
    bool NonVisibleTaskWithGuiRules { get; set; }
    void ClearCurrentExecutionSession();
}
