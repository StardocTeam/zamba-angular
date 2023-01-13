

public interface ITaskHeader
{
    Zamba.Core.ITaskResult TaskResult { get; set; }

    void FillHeader();
}
