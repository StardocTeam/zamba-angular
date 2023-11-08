using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zamba.Framework
{

    public class Filter
    {
        public long UserId { get; set; }
        public SizePage SizePage { get; set; }
        public List<Parameters> Parameters { get; set; }
    }
    public class SizePage
    {
        public int LastPage { get; set; } = 0;
        public int PageSize { get; set; } = 25;
    }

    public enum SearchFilterType{
    All = 0,
    Entity = 1,
    Attribute = 2,
    WorkflowId = 3,
    StepId = 4,
    StepStateId = 5,
    TaskStateId = 6,
    UserId = 7
}


    public class Parameters
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Value2 { get; set; }
        public string Operator { get; set; }
        public SearchFilterType Type { get; set; }
    }

    public class genericRequest
    {
        public long UserId { get; set; }
        public Dictionary<string, string> Params { get; set; } = new Dictionary<string, string>();
    };


    public class genericRequestParam
    {
        public string key { get; set; }
        public string value { get; set; }
    };

    public class ResquestToDictionry
    {
        public Dictionary<string, object> Params { get; set; }
    };

    public class ReportDto
    {
        public string varx { get; set; }
        public string vary { get; set; }
        public string varz { get; set; }
        public string variables { get; set; }


    };

    public class TimeLineDto
    {
        public long id { get; set; }
        public long resultId { get; set; }
        public long entityId { get; set; }
        public long UserId { get; set; }
        public string UsrAprobacion { get; set; }
        public string title { get; set; }
        public string subtitle { get; set; }
        public string description { get; set; }
        public string urlpost { get; set; }
        public string date { get; set; }
        public string avatar { get; set; }
        public string subdescription { get; set; }
        public string btnsubdescription { get; set; }
        public string thum { get; set; }
        // Reportes
        public string UsuarioZamba { get; set; }
        public string FechaConexion { get; set; }
        public string UltimaActividad { get; set; }
        public string UsuarioWindows { get; set; }
        public string NombrePC { get; set; }

    };
    public class CalendarEvents
    {

        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
    };

    public class ObservacionesDto
    {
        public string id { get; set; }
        public string docId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string dateobs { get; set; }
        public string value { get; set; }
        public string Foto { get; set; }


    };
    public class EditDto
    {
        public List<Object> Indexs { get; set; } = new List<object>();
        public string ExternUserID { get; set; }
        public string Id { get; set; }
    }

}
