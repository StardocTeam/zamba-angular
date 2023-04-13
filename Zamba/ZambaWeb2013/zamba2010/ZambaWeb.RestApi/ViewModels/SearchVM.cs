using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zamba.Framework;

namespace ZambaWeb.RestApi.ViewModels
{
    public class SearchVM
    {
    }

    public class searchList
    {
        public Int64 IndexId { get; set; }
        public string Value { get; set; }
        public Int64 LimitTo { get; set; }

    }
    public class TaskModel
    {
        public string Name { get; set; }
        public string StateName { get; set; }
        public long TaskId { get; set; }
        public string TaskStateName { get; set; }
        public string WfStepName { get; set; }
        public string AsignedTo { get; set; }
        public string WorkFlowName { get; set; }
        public List<TaskIndexModel> Indexs { get; set; }
        // public List<Zamba.Core.Index> Indexs { get; set; }
    }
    public class TaskIndexModel
    {
        public Int64 Id { get; set; }
        public string name { get; set; }
        public string value { get; set; }
    }

}

public class EntityDto
{
    public string name { get; set; }
    public long id { get; set; }
    public bool enabled { get; set; } = true;

    public long ResultsCount { get; set; }
    public Boolean UserCanRemove { get; set; }
    
}

public class ViewDto
{
    public string name { get; set; }
    public long id { get; set; }
    public bool enabled { get; set; } = true;
    public long reportId { get; set; }
    public long ResultsCount { get; set; }
    public string viewClass { get; set; }

}

public class SearchDto
{
    public Int64 SearchId { get; set; }
    public Int64 OrganizationId { get; set; }
    public List<Int64> DoctypesIds { get; set; } = new List<long>();
    public List<EntityDto> entities { get; set; } = new List<EntityDto>();
    public List<Object> Indexs { get; set; } = new List<object>();
    public List<Object> UsedFilters { get; set; } = new List<object>();
    public Boolean blnSearchInAllDocsType { get; set; }
    public string TextSearchInAllIndexs { get; set; }
    public Boolean RaiseResults { get; set; }
    public string ParentName { get; set; }
    public Boolean CaseSensitive { get; set; }
    public Int64 MaxResults { get; set; }
    public Boolean ShowIndexOnGrid { get; set; }
    public Boolean UseVersion { get; set; }
    public Int64 UserId { get; set; }
    public Int64 StepId { get; set; }
    public Int64 StepStateId { get; set; }
    public Int64 TaskStateId { get; set; }
    public Int64 WorkflowId { get; set; }
    public long UserAssignedId { get; set; }
    public UsedFilter UserAssignedFilter { get; set; }
    public UsedFilter StepFilter { get; set; }
    public string NotesSearch { get; set; }
    public string Textsearch { get; set; }

    public long LastPage { get; set; } = 0;
    public int PageSize { get; set; } = 100;

    public string OrderBy { get; set; }

    public List<Object> Filters { get; set; } = new List<object>();

    public bool AsignedTasks { get; set; }

    public string View { get; set; }

    public bool columnFiltering { get; set; }

    public string ExternUserID { get; set; }

    public List<string> Lista_ColumnasFiltradas{ get; set; } = new List<string>();

    public bool FiltersResetables { get; set; }

    public Dictionary<string, string> Params { get; set; } = new Dictionary<string, string>();

    public List<kendoFilter> crdateFilters { get; set; } = new List<kendoFilter>();

    public List<kendoFilter> lupdateFilters { get; set; } = new List<kendoFilter>();

    public List<kendoFilter> nameFilters { get; set; } = new List<kendoFilter>();

    public List<kendoFilter> originalFilenameFilters { get; set; } = new List<kendoFilter>();

    public List<kendoFilter> stateFilters { get; set; } = new List<kendoFilter>();
    

}

public class UsedFilter
{
    public long ID { get; set; }
    public string Name { get; set; }
    public string Data { get; set; }
    public string dataDescription { get; set; }
    public string CompareOperator { get; set; }
    public string CurrentUserId { get; set; }
    public bool IsChecked { get; set; }
    public int StepId { get; set; }
    public string EntitiesIds { get; set; }
    public long zFilterWebID { get; set; }
}

public class SearchDtoIndex
{

}


public class LastNodeObj
{
    public LastNodeObj()
    {
    }
    public string LastNodes { get; set; }
    public int UserId { get; set; }
}

public class ResultDto
{
    public Int64 DOC_ID { get; set; }//Id
    public Int64 DOC_TYPE_ID { get; set; }//EntityId
    public Int64 UserId { get; set; }
}

public class WorkflowDTO
{
    public long ID { get; set; }
    public string Name { get; set; }
    public List<StepDTO> Steps { get; set; } = new List<StepDTO>();
}

public class StepDTO
{
    public long ID { get; set; }
    public string Name { get; set; }
    public long WFID { get; set; }
    public string WFName { get; set; }
    public int Count { get; set; }
}

public class StateDTO
{
    public long ID { get; set; }
    public string Name { get; set; }
    public long StepID { get; set; }
    public string StepName { get; set; }
    public int Count { get; set; }
}
public class UserAsignedDTO
{
    public long ID { get; set; }
    public string Name { get; set; }
    public int Count { get; set; }
}
