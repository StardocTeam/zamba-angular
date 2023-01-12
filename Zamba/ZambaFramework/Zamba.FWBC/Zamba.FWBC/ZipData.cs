using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zamba.Framework
{
    public class Zipdata
    {
        public List<string> docidList;
        [Required]
        public List<string> DocidList
        {
            get { return docidList; }
            set { docidList = value; }

        }

        public List<string> mailto;
        [Required]
        public List<string> MailTo
        {

            get { return mailto; }
            set { mailto = value; }
        }

        public List<string> cc;
        [Required]
        public List<string> CC
        {

            get { return cc; }
            set { cc = value; }
        }


        public string asunto;
        [Required]
        public string Asunto
        {

            get { return asunto; }
            set { asunto = value; }
        }


        public string zipname;
        [Required]
        public string ZipName
        {

            get { return zipname; }
            set { zipname = value; }
        }


        public string zippassword;
        [Required]
        public string ZipPassword
        {

            get { return zippassword; }
            set { zippassword = value; }
        }

        public string messageBody;
        [Required]
        public string MessageBody
        {

            get { return messageBody; }
            set { zippassword = value; }
        }


         public List<IdInfo> Idinfo { get; set; }
        public class IdInfo
        {
            public int DocTypeid { get; set; }
            public int DocId { get; set; }

        }
    }
}
