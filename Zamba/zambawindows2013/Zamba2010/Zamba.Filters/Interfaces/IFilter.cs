using System;

namespace Zamba.Filters.Interfaces
{
    public interface IFilter
    {
        IFiltersComponent Fc { get; set; }

        void ShowTaskOfDT();

        Int32 LastPage { get; set; }


    } 
}
