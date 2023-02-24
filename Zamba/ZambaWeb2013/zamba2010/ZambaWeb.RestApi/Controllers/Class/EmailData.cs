using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZambaWeb.RestApi.Controllers.Class
{
    public class EmailData
    {
        public List<IdInfo> Idinfo { get; set; }

        public string mailto;
        [Required]
        public string MailTo
        {

            get { return mailto; }
            set { mailto = value; }
        }

        public string cc;
        [Required]
        public string CC
        {

            get { return cc; }
            set { cc = value; }
        }

        public string cco;
        [Required]
        public string CCO
        {

            get { return cco; }
            set { cco = value; }
        }

        public string subject;
        [Required]
        public string Subject
        {

            get { return subject; }
            set { subject = value; }
        }

        public string messageBody;
        public string MessageBody
        {
            get { return messageBody; }
            set { messageBody = value; }
        }


        public long userid;
        public long Userid
        {
            get { return userid; }
            set { userid = value; }
        }
        private bool addlink;
        public bool AddLink
        {
            get { return addlink; }
            set { addlink = value; }
        }


        public class IdInfo
        {
            public Int64 DocTypeid { get; set; }
            public Int64 DocId { get; set; }

        }


        public bool fromType;
        public bool FromType{
            get { return fromType; }
            set { fromType = value; }
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
        public bool? SendDocument { get; set; }
        public Dictionary<string, string>[] Base64StringArray { get; set; }
        public string MailPathVariable { get; set; }

        public string isDomail { get; set; }
    }    
}