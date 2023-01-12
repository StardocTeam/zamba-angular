using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using Zamba.Services;
using Zamba.Core;


public class WFExecution
{

    public delegate void _continueExecution(long RuleId, ref List<Zamba.Core.ITaskResult> results, Zamba.Core.RulePendingEvents PendigEvent, Zamba.Core.RuleExecutionResult ExecutionResult, List<Int64> ExecutedIDs, Hashtable Params, List<Int64> PendingChildRules, ref  Boolean RefreshRule, List<long> TaskIdsToRefresh);

    Zamba.Core.IUser _user;

    public delegate void _hacealgo(long RuleId, ref List<Zamba.Core.ITaskResult> results, ref Zamba.Core.RulePendingEvents PendigEvent, ref Zamba.Core.RuleExecutionResult ExecutionResult, ref List<Int64> ExecutedIDs, ref Hashtable Params, ref List<Int64> PendingChildRules, ref  Boolean RefreshRule, List<long> TaskIdsToRefresh);
    public event _hacealgo _haceralgoEvent;
    
    public WFExecution(Zamba.Core.IUser currentUser)
	{
        _user = currentUser;
	}

    public void ExecuteRule(long RuleId, ref List<Zamba.Core.ITaskResult> results, 
        RulePendingEvents PendigEvent, RuleExecutionResult ExecutionResult, List<Int64> ExecutedIDs,
        Hashtable Params, List<Int64> PendingChildRules, ref  Boolean RefreshRule, List<long> TaskIdsToRefresh)
    {
        if (PendigEvent == RulePendingEvents.CancelExecution)
        {
            _haceralgoEvent(RuleId, ref results, ref PendigEvent, ref ExecutionResult, ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, TaskIdsToRefresh);
        }
        else
        {
            SRules Rules = new SRules();
            Int64 idtoexecute;
            if (ExecutedIDs != null && ExecutedIDs.Count > 0)
            {
                idtoexecute = ExecutedIDs[ExecutedIDs.Count - 1];
                if (idtoexecute < 0) idtoexecute *= -1;
            }
            else
            {
                idtoexecute = RuleId;
            }
                results = Rules.ExecuteWebRule(idtoexecute, results, ref PendigEvent, ref ExecutionResult, ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, ref TaskIdsToRefresh);

                if (RefreshRule)
                {
                    if (results != null && results.Count > 0)
                    {
                        if (!TaskIdsToRefresh.Contains(results[0].TaskId))
                        {
                            TaskIdsToRefresh.Add(results[0].TaskId);
                        }
                    }                
                }

            switch (ExecutionResult)
            {
                case Zamba.Core.RuleExecutionResult.NoExecution:
                    if (PendingChildRules.Count > 0)
                    {
                        Int64 PendingRule = PendingChildRules[0];
                        ExecutedIDs.Clear();
                        PendingChildRules.Remove(PendingRule);
                        ExecuteRule(PendingRule, ref results, PendigEvent, ExecutionResult, ExecutedIDs, Params, PendingChildRules, ref RefreshRule, TaskIdsToRefresh);
                        return;
                    }
                    else
                    {
                        PendigEvent = RulePendingEvents.ValidateDistribute;
                        ExecutionResult = RuleExecutionResult.PendingEventExecution;
                        _haceralgoEvent(RuleId, ref results, ref PendigEvent, ref ExecutionResult, ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, TaskIdsToRefresh);

                        return;
                    }
                case Zamba.Core.RuleExecutionResult.CorrectExecution:
                    if (PendingChildRules.Count > 0)
                    {
                        Int64 PendingRule = PendingChildRules[0];
                        ExecutedIDs.Clear();
                        //  ExecutedIDs.Add(PendingRule);
                        PendingChildRules.Remove(PendingRule);
                        ExecuteRule(PendingRule, ref results, PendigEvent, ExecutionResult, ExecutedIDs, Params, PendingChildRules, ref RefreshRule, TaskIdsToRefresh);
                        return;
                    }
                    else
                    {
                        PendigEvent = RulePendingEvents.ValidateDistribute;
                        ExecutionResult = RuleExecutionResult.PendingEventExecution;
                        _haceralgoEvent(RuleId, ref results, ref PendigEvent, ref ExecutionResult, ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, TaskIdsToRefresh);

                        return;
                    }
                case Zamba.Core.RuleExecutionResult.FailedExecution:
                    _haceralgoEvent(RuleId, ref results, ref PendigEvent, ref ExecutionResult, ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, TaskIdsToRefresh);
                    return;

                default:
                    _haceralgoEvent(RuleId, ref results, ref PendigEvent, ref ExecutionResult, ref ExecutedIDs, ref Params, ref  PendingChildRules, ref RefreshRule, TaskIdsToRefresh);
                    return;
            }
        }
    }
}