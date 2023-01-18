using System.Data;
using System;
using System.Web.UI.WebControls;
using System.Collections;

public interface IGrid
{
    long DocTypeId { get; set; }
    Int32 WFId { get; set; }
    Int32 StepId { get; set; }
    Int32 RowCount { get; set; }
    DropDownList cmbDocTypes {get; set; }
    string GridScript { get; set; }
    string GridType { get; set; }
    string GridKey { get; set; }
    string Height { get; set; }
    string Width { get; set; }
    string ToolbarContent { get; set; }
    ArrayList ColumnsToHide { get; set; }
    string ColInd { get; set; }
    string Url { get; set; }

}