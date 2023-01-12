using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Zamba.Core;
using Zamba.Filters;

namespace ZambaWeb.RestApi.ViewModels
{
    public class LoadTasksVM
    {
        public DataTable Data { get; set; }
        public int TaskCount { get; set; }
        public LoadTasksVM(DataTable dt)
        {
            Data = dt;
            TaskCount = dt.Rows.Count;
        }
    }
    public class LoadTasksParamVM
    {
        public LoadTasksParamVM()
        {
            DocTypeIds = new List<long>();         
        }
        public long StepId { get; set; }
        public List<long> DocTypeIds { get; set; }
        public int LastPage { get; set; } = 1;
        public int PageSize { get; set; } = 50;
        public List<FilterElem> FiltersElem { get; set; }
    }
}