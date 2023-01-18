using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using Zamba.Services;
using Zamba.Core;

namespace Zamba.Framework
{
    public class WFExecution
    {

        public delegate void _continueExecution(long RuleId, ref List<Zamba.Core.ITaskResult> results, ref Zamba.Core.RulePendingEvents PendigEvent, ref Zamba.Core.RuleExecutionResult ExecutionResult, ref List<Int64> ExecutedIDs, ref Hashtable Params, ref List<Int64> PendingChildRules, ref Boolean RefreshRule, List<long> TaskIdsToRefresh, Boolean IsAsync);

        Zamba.Core.IUser _user;

        public delegate void _hacealgo(long RuleId, ref List<Zamba.Core.ITaskResult> results, ref Zamba.Core.RulePendingEvents PendigEvent, ref Zamba.Core.RuleExecutionResult ExecutionResult, ref List<Int64> ExecutedIDs, ref Hashtable Params, ref List<Int64> PendingChildRules, ref Boolean RefreshRule, List<long> TaskIdsToRefresh, Boolean IsAsync);
        public event _hacealgo _haceralgoEvent;

        public WFExecution(Zamba.Core.IUser currentUser)
        {
            _user = currentUser;
        }

        public void ExecuteRule(long RuleId,
                                ref List<ITaskResult> results,
                                ref RulePendingEvents PendigEvent,
                                ref RuleExecutionResult ExecutionResult,
                                ref List<Int64> ExecutedIDs,
                                ref Hashtable Params,
                                ref List<Int64> PendingChildRules,
                                ref Boolean RefreshRule,
                                List<long> TaskIdsToRefresh, Boolean IsAsync)
        {
            if (PendigEvent == RulePendingEvents.CancelExecution)
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "CancelExecution RuleId" + RuleId.ToString());
                _haceralgoEvent(RuleId, ref results, ref PendigEvent, ref ExecutionResult, ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, TaskIdsToRefresh,  IsAsync);
            }
            else
            {
                SRules Rules = new SRules();
                Int64 idtoexecute;
                if (ExecutedIDs != null && ExecutedIDs.Count > 0)
                {
                   // ZTrace.WriteLineIf(ZTrace.IsVerbose, "ExecutedIDs.Count" + ExecutedIDs.Count.ToString());
                    idtoexecute = ExecutedIDs[ExecutedIDs.Count - 1];
                   // ZTrace.WriteLineIf(ZTrace.IsVerbose, "idtoexecute" + idtoexecute.ToString());
                    if (idtoexecute < 0) idtoexecute *= -1;
                }
                else
                {
                    idtoexecute = RuleId;
                   // ZTrace.WriteLineIf(ZTrace.IsVerbose, "idtoexecute" + idtoexecute.ToString());
                }
                results = Rules.ExecuteWebRule(idtoexecute, results, ref PendigEvent, ref ExecutionResult, ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, ref TaskIdsToRefresh,  IsAsync);

                try
                {
                    if (RefreshRule)
                    {
                        if (results != null && results.Count > 0)
                        {
                            if (TaskIdsToRefresh != null && !TaskIdsToRefresh.Contains(results[0].TaskId))
                            {
                                TaskIdsToRefresh.Add(results[0].TaskId);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                }


                switch (ExecutionResult)
                {
                    case Zamba.Core.RuleExecutionResult.NoExecution:
                        if (PendingChildRules.Count > 0)
                        {
                            Int64 PendingRule = PendingChildRules[0];
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "NoExecution PendingChildRules[0]" + PendingChildRules[0].ToString());

                            ExecutedIDs.Clear();
                            PendingChildRules.Remove(PendingRule);
                            ExecuteRule(PendingRule, ref results, ref PendigEvent, ref ExecutionResult, ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, TaskIdsToRefresh, IsAsync);
                            return;
                        }
                        else
                        {
                            PendigEvent = RulePendingEvents.ValidateDistribute;
                            ExecutionResult = RuleExecutionResult.PendingEventExecution;
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "NoExecution RuleId" + RuleId.ToString());

                            _haceralgoEvent(RuleId, ref results, ref PendigEvent, ref ExecutionResult, ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, TaskIdsToRefresh, IsAsync);

                            return;
                        }
                    case Zamba.Core.RuleExecutionResult.CorrectExecution:
                        if (PendingChildRules.Count > 0)
                        {
                            Int64 PendingRule = PendingChildRules[0];
                           // ZTrace.WriteLineIf(ZTrace.IsVerbose, "CorrectExecution PendingChildRules[0]" + PendingChildRules[0].ToString());
                            ExecutedIDs.Clear();
                            //  ExecutedIDs.Add(PendingRule);
                            PendingChildRules.Remove(PendingRule);
                            ExecuteRule(PendingRule, ref results, ref PendigEvent, ref ExecutionResult, ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, TaskIdsToRefresh, IsAsync);
                            return;
                        }
                        else
                        {
                            PendigEvent = RulePendingEvents.ValidateDistribute;
                            ExecutionResult = RuleExecutionResult.PendingEventExecution;
                          //  ZTrace.WriteLineIf(ZTrace.IsVerbose, "CorrectExecution RuleId" + RuleId.ToString());

                            _haceralgoEvent(RuleId, ref results, ref PendigEvent, ref ExecutionResult, ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, TaskIdsToRefresh, IsAsync);

                            return;
                        }
                    case Zamba.Core.RuleExecutionResult.FailedExecution:
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "FailedExecution RuleId" + RuleId.ToString());
                        _haceralgoEvent(RuleId, ref results, ref PendigEvent, ref ExecutionResult, ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, TaskIdsToRefresh, IsAsync);
                        return;

                    default:
                       // ZTrace.WriteLineIf(ZTrace.IsVerbose, "default RuleId" + RuleId.ToString());
                        _haceralgoEvent(RuleId, ref results, ref PendigEvent, ref ExecutionResult, ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, TaskIdsToRefresh, IsAsync);
                        return;
                }
            }
        }
    }
}