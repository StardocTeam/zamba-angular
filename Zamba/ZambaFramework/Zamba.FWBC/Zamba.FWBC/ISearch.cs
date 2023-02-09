using System.Collections.Generic;
using Zamba;
using Zamba.Framework;
using Zamba.Searchs;

public interface ISearch
{
    List<IDocType> Doctypes   { get; set; }
List<IIndex>  Indexs   { get; set; }
   bool CaseSensitive  { get; set; }
    bool UseVersion  { get; set; }
    bool ShowIndexOnGrid  { get; set; }
    string Textsearch { get; set; }
    string ParentName  { get; set; }
    string Name { get; set; }
    int MaxResults  { get; set; }
    long UserId  { get; set; }
    long WorkflowId  { get; set; }
    long StepId  { get; set; }
    long StepStateId  { get; set; }
    long TaskStateId  { get; set; }
     List< string> SQL  { get; set; }
     List< string> SQLCount  { get; set; }
    SearchTypes SearchType { get; set; }
    List<IEntityEnabledForQuickSearch> EntitiesEnabledForQuickSearch  { get; set; }
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
}