using System.Collections.Generic;
using Zamba;
using Zamba.Core;
using Zamba.Framework;
using Zamba.Searchs;

public interface ISearch
{
    IResult ParentEntity { get; set; }

    List<IDocType> Doctypes { get; set; }
    List<IIndex> Indexs { get; set; }
    bool CaseSensitive { get; set; }
    bool UseVersion { get; set; }
    bool ShowIndexOnGrid { get; set; }
    string Textsearch { get; set; }
    string ParentName { get; set; }
    string Name { get; set; }
    int MaxResults { get; set; }
    long UserId { get; set; }
    long WorkflowId { get; set; }
    long UserAssignedId { get; set; }
    bool UserAssignedEnabled { get; set; }
    long StepId { get; set; }
    long StepStateId { get; set; }
    long TaskStateId { get; set; }
    List<string> SQL { get; set; }
    List<string> SQLCount { get; set; }
    SearchTypes SearchType { get; set; }
    List<IEntityEnabledForQuickSearch> EntitiesEnabledForQuickSearch { get; set; }
    void SetOrderBy(string orderstring);
    void SetGroupBy(string orderstring);
    void AddIndex(IIndex index);
    void AddFilter(ikendoFilter filter);
    void AddDocType(IDocType docType);
    string OrderBy { get; set; }

    List<ikendoFilter> Filters { get; set; }
    string View { get; set; }
    string Restriction { get; set; }
    List<string> Lista_ColumnasFiltradas { get; set; }
    List<kendoFilter> crdateFilters { get; set; }
    List<kendoFilter> lupdateFilters { get; set; }
    List<kendoFilter> nameFilters { get; set; }
    List<kendoFilter> originalFilenameFilters { get; set; }
    List<kendoFilter> stateFilters { get; set; }

}