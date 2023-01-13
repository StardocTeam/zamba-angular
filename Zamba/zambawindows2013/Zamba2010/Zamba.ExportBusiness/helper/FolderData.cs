using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExportaOutlook.Helper
{
    internal class FolderInfo
    {
        public string Path { get; set; }
        public string EntryId { get; set; }

        public FolderInfo()
        { }

        public FolderInfo(string path, string entryId)
        {
            Path = path;
            EntryId = entryId;
        }
    }
}
