using System;
using System.Collections.Generic;
using System.Text;
using Zamba.Core;

public class Evento
{
    private Result _result;
    public string Name
    {
        get
        {
            return Result.Name;
        }
    }
    public long Id
    {
        get
        {
            return Result.ID;
        }
    }

    public Result Result
    {
        get
        {
            return _result;
        }
        set
        {
            _result = value;
        }
    }
    public DateTime StartTime
    {
        get
        {
            return Result.Fecha_Inicio;
        }
    }
    public DateTime EndTime
    {
        get
        {
            return Result.Fecha_Fin;
        }
    }
    public Evento(TaskResult T)
    {
        this.Result = T;
    }
}