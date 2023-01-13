using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zamba.Core;
using Zamba.Framework;

namespace ZambaWeb.RestApi.Models
{
    public class Word
    {
        public long id;
        public string word;
        public SearchFilterType type;
        public string name;
        public long parent;
        public long parentIndex;
        public Word(long id, string word, long parent)
        {
            this.id = id;
            this.word = word;
            this.name = word;
            this.parent = parent;
            this.parentIndex = 0;
            this.type = SearchFilterType.All;
        }
        public Word(long id, string word, long parent, long parentIndex)
        {
            this.id = id;
            this.word = word;
            this.name = word;
            this.parent = parent;
            this.parentIndex = parentIndex;
            this.type = SearchFilterType.All;
        }
    }
}