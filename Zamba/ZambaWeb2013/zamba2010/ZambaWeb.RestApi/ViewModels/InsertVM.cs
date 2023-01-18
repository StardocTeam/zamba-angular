using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zamba;
using Zamba.Core;

namespace ZambaWeb.RestApi.ViewModels
{
    public class InsertParamVM
    {         
        //Los indices no se serializan correctamente y llegan vacios
        public List<object> Indexs { get; set; }
        public List<string> Filenames { get; set; }
        public long DocTypeId { get; set; }        
    }

    public class InsertVM
    {
        public InsertVM()
        {
            NewResult = new NewResultVM();
        }
        public bool OpenDoc { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
        public NewResultVM NewResult { get; set; }
    }

    public class NewResultVM
    {
        public NewResultVM() { }
        public NewResultVM(long id, string name, long docTypeId)
        {
            Id = id;
            Name = name;
            DocTypeId = docTypeId;
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public long DocTypeId { get; set; }
    }

}