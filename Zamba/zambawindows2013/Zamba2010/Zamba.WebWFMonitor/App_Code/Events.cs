using System;
using System.Data;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using Zamba;
using Zamba.Core;
using Zamba.Data;
using Zamba.Servers;
using Zamba.Users.Factory;
using Zamba.WFBusiness;

public class Eventos
{
    public SortedList<long, TaskResult> GetTasks(Int32 wfId, Int32 stepId)
    {
        DsWF DsAllWfs = WFBusiness.GetAllWorkflows();

        WorkFlow CurrentWorkflow = null;
        foreach (DsWF.WFRow Row in DsAllWfs.WF)
        {
            if ((int)Row[0] == wfId)
            {
                CurrentWorkflow = WFBusiness.GetWf(Row);
                WFBusiness.GetFullMonitorWF(CurrentWorkflow);
            }
        }

        if (null == CurrentWorkflow)
            return null;

        SortedList<long, TaskResult> TaskResultsList = new SortedList<long, TaskResult>();
        foreach (WFStep CurrentStep in CurrentWorkflow.Steps.Values)
        {
            if (CurrentStep.ID == stepId)
            {
                foreach (TaskResult taskResult in CurrentStep.Tasks.Values)
                    TaskResultsList.Add(taskResult.ID, taskResult);

                break;
            }
        }

        return TaskResultsList;
    }
    public List<Evento> GetEventos(Int32 wfId, Int32 stepId)
    {
        List<Evento> Events = new List<Evento>();
        SortedList<long, TaskResult> Tasks = GetTasks(wfId, stepId);

        foreach (TaskResult taskResult in Tasks.Values)
            Events.Add(new Evento(taskResult));

        return Events;
    }
}