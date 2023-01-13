using System;
using Zamba.Core;

public interface ITaskViewer
{
    Int64 Task_ID { get; set; }
    ITaskResult TaskResult { get; set; }
}