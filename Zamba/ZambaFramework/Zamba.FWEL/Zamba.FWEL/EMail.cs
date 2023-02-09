
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zamba.Core
{
    public class ListEmail : IListEmail
    {

        public string UniqueId { get; set; }
        public bool IsAnswered { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDraft { get; set; }
        public bool IsFlagged { get; set; }
        public bool IsRecent { get; set; }
        public bool IsRead { get; set; }
        public long Size { get; set; }
        public int SequenceNumber { get; set; }
        public string Subject { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string Cc { get; set; }

        public string ReplyTo { get; set; }

        public string Sender { get; set; }
        public string Body { get; set; }

        public string Date { get; set; }
        public object Attachments_Count { get; set; }

        private int _Attachments_Count;
    }

    //public class Attach {

    //    //
    //    // Summary:
    //    //     Gets or sets data.
    //    public Stream Data { get; set; }
    //    //
    //    // Summary:
    //    //     Gets or sets content id.
    //    public string ContentId { get; set; }
    //    //
    //    // Summary:
    //    //     Gets or sets content type.
    //    public ContentType ContentType { get; set; }
    //    //
    //    // Summary:
    //    //     Gets or sets dispostion type.
    //    public string DispositionType { get; set; }
    //    //
    //    // Summary:
    //    //     Gets or sets file name for an attachment.
    //    public string FileName { get; set; }
    //    //
    //    // Summary:
    //    //     Gets or sets file size for an attachment.
    //    public long Size { get; set; }
    //}

   
  
}
