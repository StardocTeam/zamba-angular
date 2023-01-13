using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZambaWeb.RestApi.Models.ResponseModel
{
    public class CacheResponseModel<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
    }
}