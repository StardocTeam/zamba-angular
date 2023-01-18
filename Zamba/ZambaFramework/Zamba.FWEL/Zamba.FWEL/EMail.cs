
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;
using System.Runtime.InteropServices;
using System.ComponentModel.Design.Serialization;
using System.Reflection;

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

    //public class attach
    //{

    //    //
    //    // summary:
    //    //     gets or sets data.
    //    public stream data { get; set; }
    //    //
    //    // summary:
    //    //     gets or sets content id.
    //    public string contentid { get; set; }
    //    //
    //    // summary:
    //    //     gets or sets content type.
    //    public contenttype contenttype { get; set; }
    //    //
    //    // summary:
    //    //     gets or sets dispostion type.
    //    public string dispositiontype { get; set; }
    //    //
    //    // summary:
    //    //     gets or sets file name for an attachment.
    //    public string filename { get; set; }
    //    //
    //    // summary:
    //    //     gets or sets file size for an attachment.
    //    public long size { get; set; }
    //}


    public class attach {

        public Stream Data { get; set; }
        public string ContentId { get; set; }
        public ContentType ContentType { get; set; }
        public string DispositionType { get; set; }
        public string FileName { get; set; }
        public long Size { get; set; }

    }

    public class ContentType
    {
       

        public string Boundary { get; set; }
        public string CharSet { get; set; }
        public string MediaType { get; set; }
        public string Name { get; set; }
        public StringDictionary Parameters { get; }

     
    }

    public class StringDictionary 
    {
        //public virtual string this[string key] { get; set; }
        public virtual int Count { get; }
        public virtual bool IsSynchronized { get; }
        public virtual ICollection Keys { get; }
        public virtual object SyncRoot { get; }
        public virtual ICollection Values { get; }

        
    }

    //public interface ICollection : IEnumerable
    //{
    //    int Count { get; }
    //    object SyncRoot { get; }
    //    bool IsSynchronized { get; }

    //    void CopyTo(Array array, int index);
    //}

    //public interface IEnumerable
    //{
    //    [DispId(-4)]
    //    IEnumerator GetEnumerator();
    //}

    //public interface IEnumerator
    //{
        
    //    object Current { get; }
    //    bool MoveNext();
    //    void Reset();


    //}
}
