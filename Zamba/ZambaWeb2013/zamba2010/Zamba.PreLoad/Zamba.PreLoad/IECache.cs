using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using Zamba.Core;
namespace Zamba.IETools
{
    public class IECache
    {
        private static string InternetCache = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);

        private enum Mode
        {
            All,
            All_Except_Selected,
            Only_Selected
        }


        #region Definitions/DLL Imports
        /// <summary> 
        /// For PInvoke: Contains information about an entry in the Internet cache 
        /// </summary> 
        [StructLayout(LayoutKind.Explicit, Size = 80)]
        public struct INTERNET_CACHE_ENTRY_INFOA
        {
            [FieldOffset(0)]
            public uint dwStructSize;
            [FieldOffset(4)]
            public IntPtr lpszSourceUrlName;
            [FieldOffset(8)]
            public IntPtr lpszLocalFileName;
            [FieldOffset(12)]
            public uint CacheEntryType;
            [FieldOffset(16)]
            public uint dwUseCount;
            [FieldOffset(20)]
            public uint dwHitRate;
            [FieldOffset(24)]
            public uint dwSizeLow;
            [FieldOffset(28)]
            public uint dwSizeHigh;
            [FieldOffset(32)]
            public System.Runtime.InteropServices.ComTypes.FILETIME LastModifiedTime;
            [FieldOffset(40)]
            public System.Runtime.InteropServices.ComTypes.FILETIME ExpireTime;
            [FieldOffset(48)]
            public System.Runtime.InteropServices.ComTypes.FILETIME LastAccessTime;
            [FieldOffset(56)]
            public System.Runtime.InteropServices.ComTypes.FILETIME LastSyncTime;
            [FieldOffset(64)]
            public IntPtr lpHeaderInfo;
            [FieldOffset(68)]
            public uint dwHeaderInfoSize;
            [FieldOffset(72)]
            public IntPtr lpszFileExtension;
            [FieldOffset(76)]
            public uint dwReserved;
            [FieldOffset(76)]
            public uint dwExemptDelta;
        }

        // For PInvoke: Initiates the enumeration of the cache groups in the Internet cache 
        [DllImport(@"wininet",
            SetLastError = true,
            CharSet = CharSet.Auto,
            EntryPoint = "FindFirstUrlCacheGroup",
            CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindFirstUrlCacheGroup(
            int dwFlags,
            int dwFilter,
            IntPtr lpSearchCondition,
            int dwSearchCondition,
            ref long lpGroupId,
            IntPtr lpReserved);

        // For PInvoke: Retrieves the next cache group in a cache group enumeration 
        [DllImport(@"wininet",
            SetLastError = true,
            CharSet = CharSet.Auto,
            EntryPoint = "FindNextUrlCacheGroup",
            CallingConvention = CallingConvention.StdCall)]
        public static extern bool FindNextUrlCacheGroup(
            IntPtr hFind,
            ref long lpGroupId,
            IntPtr lpReserved);

        // For PInvoke: Releases the specified GROUPID and any associated state in the cache index file 
        [DllImport(@"wininet",
            SetLastError = true,
            CharSet = CharSet.Auto,
            EntryPoint = "DeleteUrlCacheGroup",
            CallingConvention = CallingConvention.StdCall)]
        public static extern bool DeleteUrlCacheGroup(
            long GroupId,
          int dwFlags,
          IntPtr lpReserved);

        // For PInvoke: Begins the enumeration of the Internet cache 
        [DllImport(@"wininet",
            SetLastError = true,
            CharSet = CharSet.Auto,
            EntryPoint = "FindFirstUrlCacheEntryA",
            CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindFirstUrlCacheEntry(
            [MarshalAs(UnmanagedType.LPTStr)] string lpszUrlSearchPattern,
            IntPtr lpFirstCacheEntryInfo,
            ref int lpdwFirstCacheEntryInfoBufferSize);

        // For PInvoke: Retrieves the next entry in the Internet cache 
        [DllImport(@"wininet",
            SetLastError = true,
            CharSet = CharSet.Auto,
            EntryPoint = "FindNextUrlCacheEntryA",
            CallingConvention = CallingConvention.StdCall)]
        public static extern bool FindNextUrlCacheEntry(
            IntPtr hFind,
            IntPtr lpNextCacheEntryInfo,
            ref int lpdwNextCacheEntryInfoBufferSize);

        // For PInvoke: Removes the file that is associated with the source name from the cache, if the file exists 
        [DllImport(@"wininet",
            SetLastError = true,
            CharSet = CharSet.Auto,
            EntryPoint = "DeleteUrlCacheEntryA",
            CallingConvention = CallingConvention.StdCall)]
        public static extern bool DeleteUrlCacheEntry(
            IntPtr lpszUrlName);
        #endregion

        #region Public Static Functions

        /// <summary> 
        /// Clears the cache of the web browser 
        /// </summary> 
        public static void ClearCache()
        {
            try
            {
                // Indicates that all of the cache groups in the user's system should be enumerated 
                const int CACHEGROUP_SEARCH_ALL = 0x0;
                // Indicates that all the cache entries that are associated with the cache group 
                // should be deleted, unless the entry belongs to another cache group. 
                const int CACHEGROUP_FLAG_FLUSHURL_ONDELETE = 0x2;
                // File not found. 
                const int ERROR_FILE_NOT_FOUND = 0x2;
                // No more items have been found. 
                const int ERROR_NO_MORE_ITEMS = 259;
                // Pointer to a GROUPID variable 
                long groupId = 0;
                // keep track of the previous groupId, if it matches the current one, break the loop. 
                long lastGroupId = 0;
                // keep track of the previous internetCacheEntry LocalFileName, if it matches the current one, break the loop.
                string lastIntCacheEntry = "";

                // Local variables "
                int cacheEntryInfoBufferSizeInitial = 0;
                int cacheEntryInfoBufferSize = 0;
                IntPtr cacheEntryInfoBuffer = IntPtr.Zero;
                INTERNET_CACHE_ENTRY_INFOA internetCacheEntry;
                IntPtr enumHandle = IntPtr.Zero;
                bool returnValue = false;

                ZTrace.WriteLineIf(true, "Obteniendo version de Sistema Operativo.");
                //Variable to hold our return value
                string operatingSystem = getOSVersion();
                ZTrace.WriteLineIf(true, "Version de Sistema Operativo. Windows:" + operatingSystem);

                ZTrace.WriteLineIf(true, "Eliminando agrupaciones de cache");
                if (String.Compare(operatingSystem, "XP") == 0)
                {                    
                    // Loop through Cache Group, and then delete entries. 
                    while (true)
                    {
                        if (ERROR_NO_MORE_ITEMS == Marshal.GetLastWin32Error() || ERROR_FILE_NOT_FOUND == Marshal.GetLastWin32Error()) { break; }
                        // Delete a particular Cache Group. 
                        returnValue = DeleteUrlCacheGroup(groupId, CACHEGROUP_FLAG_FLUSHURL_ONDELETE, IntPtr.Zero);
                        if (!returnValue && ERROR_FILE_NOT_FOUND == Marshal.GetLastWin32Error())
                        {

                            returnValue = FindNextUrlCacheGroup(enumHandle, ref groupId, IntPtr.Zero);
                        }

                        if (!returnValue && (ERROR_NO_MORE_ITEMS == Marshal.GetLastWin32Error() || ERROR_FILE_NOT_FOUND == Marshal.GetLastWin32Error()))
                            break;
                    }

                    // Start to delete URLs that do not belong to any group. 
                    enumHandle = FindFirstUrlCacheEntry(null, IntPtr.Zero, ref cacheEntryInfoBufferSizeInitial);
                    if (enumHandle != IntPtr.Zero && ERROR_NO_MORE_ITEMS == Marshal.GetLastWin32Error())
                        return;

                    cacheEntryInfoBufferSize = cacheEntryInfoBufferSizeInitial;
                    cacheEntryInfoBuffer = Marshal.AllocHGlobal(cacheEntryInfoBufferSize);
                    enumHandle = FindFirstUrlCacheEntry(null, cacheEntryInfoBuffer, ref cacheEntryInfoBufferSizeInitial);
                    ZTrace.WriteLineIf(true, "Agrupaciones de cache eliminadas");

                    ZTrace.WriteLineIf(true, "Eliminando entradas de cache");
                    while (true)
                    {                        
                        internetCacheEntry = (INTERNET_CACHE_ENTRY_INFOA)Marshal.PtrToStructure(cacheEntryInfoBuffer, typeof(INTERNET_CACHE_ENTRY_INFOA));
                        if (ERROR_NO_MORE_ITEMS == Marshal.GetLastWin32Error()) { break; }

                        cacheEntryInfoBufferSizeInitial = cacheEntryInfoBufferSize;

                        if (internetCacheEntry.CacheEntryType != 1048577)
                        {
                            returnValue = DeleteUrlCacheEntry(internetCacheEntry.lpszSourceUrlName);
                        }
                        else
                        {
                            returnValue = false;
                        }
                        if (!returnValue)
                        {
                            returnValue = FindNextUrlCacheEntry(enumHandle, cacheEntryInfoBuffer, ref cacheEntryInfoBufferSizeInitial);
                        }
                        if (!returnValue && ERROR_NO_MORE_ITEMS == Marshal.GetLastWin32Error())
                        {
                            break;
                        }
                        if (!returnValue && cacheEntryInfoBufferSizeInitial > cacheEntryInfoBufferSize)
                        {
                            cacheEntryInfoBufferSize = cacheEntryInfoBufferSizeInitial;
                            cacheEntryInfoBuffer = Marshal.ReAllocHGlobal(cacheEntryInfoBuffer, (IntPtr)cacheEntryInfoBufferSize);
                            returnValue = FindNextUrlCacheEntry(enumHandle, cacheEntryInfoBuffer, ref cacheEntryInfoBufferSizeInitial);
                        }


                    }
                    Marshal.FreeHGlobal(cacheEntryInfoBuffer);
                    ZTrace.WriteLineIf(true, "Entradas de cache eliminadas");
                }
                else if (String.Compare(operatingSystem, "7") == 0)
                {                   
                    // Delete the groups first. 
                    // Groups may not always exist on the system. 
                    // For more information, visit the following Microsoft Web site: 
                    // http://msdn.microsoft.com/library/?url=/workshop/networking/wininet/overview/cache.asp             
                    // By default, a URL does not belong to any group. Therefore, that cache may become 
                    // empty even when the CacheGroup APIs are not used because the existing URL does not belong to any group.             
                    enumHandle = FindFirstUrlCacheGroup(0, CACHEGROUP_SEARCH_ALL, IntPtr.Zero, 0, ref groupId, IntPtr.Zero);
                    // If there are no items in the Cache, you are finished. 
                    //if (enumHandle != IntPtr.Zero && ERROR_NO_MORE_ITEMS == Marshal.GetLastWin32Error())
                       // return;
                    
                    // Loop through Cache Group, and then delete entries. 
                    while (true)
                    {
                        //if (ERROR_NO_MORE_ITEMS == Marshal.GetLastWin32Error() || ERROR_FILE_NOT_FOUND == Marshal.GetLastWin32Error()) { break; }
                        // Delete a particular Cache Group. 
                        returnValue = DeleteUrlCacheGroup(groupId, CACHEGROUP_FLAG_FLUSHURL_ONDELETE, IntPtr.Zero);
                        if (!returnValue && lastGroupId == groupId)
                        {
                            lastGroupId = groupId;
                            returnValue = FindNextUrlCacheGroup(enumHandle, ref groupId, IntPtr.Zero);
                        }

                        if (lastGroupId == groupId)
                            break;

                        lastGroupId = groupId;
                    }

                    // Start to delete URLs that do not belong to any group. 
                    enumHandle = FindFirstUrlCacheEntry(null, IntPtr.Zero, ref cacheEntryInfoBufferSizeInitial);
                    //if (enumHandle != IntPtr.Zero)
                       // return;

                    cacheEntryInfoBufferSize = cacheEntryInfoBufferSizeInitial;
                    cacheEntryInfoBuffer = Marshal.AllocHGlobal(cacheEntryInfoBufferSize);
                    enumHandle = FindFirstUrlCacheEntry(null, cacheEntryInfoBuffer, ref cacheEntryInfoBufferSizeInitial);
                    ZTrace.WriteLineIf(true, "CacheGrupos eliminados");

                    ZTrace.WriteLineIf(true, "Eliminando entradas de cache");
                    while (true)
                    {

                        internetCacheEntry = (INTERNET_CACHE_ENTRY_INFOA)Marshal.PtrToStructure(cacheEntryInfoBuffer, typeof(INTERNET_CACHE_ENTRY_INFOA));
                        if (String.Compare(lastIntCacheEntry, internetCacheEntry.lpszLocalFileName.ToString()) == 0) { break; }

                        cacheEntryInfoBufferSizeInitial = cacheEntryInfoBufferSize;

                        if (internetCacheEntry.CacheEntryType != 1048577)
                        {
                            returnValue = DeleteUrlCacheEntry(internetCacheEntry.lpszLocalFileName);
                        }
                        else
                        {
                            returnValue = false;
                        }
                        if (!returnValue)
                        {
                            returnValue = FindNextUrlCacheEntry(enumHandle, cacheEntryInfoBuffer, ref cacheEntryInfoBufferSizeInitial);
                        }


                        if (!returnValue && String.Compare(lastIntCacheEntry, internetCacheEntry.lpszLocalFileName.ToString()) == 0)
                        {
                            break;
                        }
                        if (!returnValue && cacheEntryInfoBufferSizeInitial > cacheEntryInfoBufferSize)
                        {
                            cacheEntryInfoBufferSize = cacheEntryInfoBufferSizeInitial;
                            cacheEntryInfoBuffer = Marshal.ReAllocHGlobal(cacheEntryInfoBuffer, (IntPtr)cacheEntryInfoBufferSize);
                            returnValue = FindNextUrlCacheEntry(enumHandle, cacheEntryInfoBuffer, ref cacheEntryInfoBufferSizeInitial);
                        }

                        lastIntCacheEntry = internetCacheEntry.lpszLocalFileName.ToString();
                    }
                    Marshal.FreeHGlobal(cacheEntryInfoBuffer);
                    ZTrace.WriteLineIf(true, "Entradas de cache eliminadas");
                }
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(true, ex.ToString());
            }
        }


        private static string getOSVersion()
        {
            OperatingSystem os = Environment.OSVersion;
            Version vs = os.Version;
            string operatingSystem = "";

            if (os.Platform == PlatformID.Win32Windows)
            {
                switch (vs.Minor)
                {
                    case 0:
                        operatingSystem = "95";
                        break;
                    case 10:
                        if (vs.Revision.ToString() == "2222A")
                            operatingSystem = "98SE";
                        else
                            operatingSystem = "98";
                        break;
                    case 90:
                        operatingSystem = "Me";
                        break;
                    default:
                        break;
                }
            }
            else if (os.Platform == PlatformID.Win32NT)
            {
                switch (vs.Major)
                {
                    case 3:
                        operatingSystem = "NT 3.51";
                        break;
                    case 4:
                        operatingSystem = "NT 4.0";
                        break;
                    case 5:
                        if (vs.Minor == 0)
                            operatingSystem = "2000";
                        else
                            operatingSystem = "XP";
                        break;
                    case 6:
                        if (vs.Minor == 0)
                            operatingSystem = "Vista";
                        else
                            operatingSystem = "7";
                        break;
                    default:
                        break;
                }
            }
            return operatingSystem;
        }

        private static int getLastError()
        {
            return 1;
        }
        #endregion

        /// <summary> 
        /// Method for clearing internet cache through code 
        /// </summary> 
        /// <param name="folder">Directory to empty</param> 
        /// 
        private static void EmptyCacheFolder(DirectoryInfo folder, Mode mode, List<string> Files)
        {


            foreach (FileInfo f in folder.GetFiles())
            {
                try
                {
                    switch (mode)
                    {
                        case Mode.All:

                            try
                            {
                                f.IsReadOnly = false;
                                f.Attributes = FileAttributes.Normal;
                            }
                            catch
                            {
                            }

                            //la eliminacion esta separada en otro try para que si falla lo anterior 
                            //de todas formas se internte eliminar el archivo 
                            try
                            {
                                f.Delete();
                            }
                            catch (Exception ex)
                            {
                                Zamba.Core.ZClass.raiseerror(ex);
                            }

                            break;

                        case Mode.All_Except_Selected:

                            if (Files != null)
                            {
                                foreach (String PF in Files)
                                {
                                    if (!f.Name.Contains(PF))
                                    {
                                        try
                                        {
                                            f.IsReadOnly = false;
                                            f.Attributes = FileAttributes.Normal;
                                        }
                                        catch
                                        {
                                        }

                                        //la eliminacion esta separada en otro try para que si falla lo anterior 
                                        //de todas formas se internte eliminar el archivo 
                                        try
                                        {
                                            f.Delete();
                                        }
                                        catch (Exception ex)
                                        {
                                            Zamba.Core.ZClass.raiseerror(ex);
                                        }
                                    }
                                }
                            }

                            break;

                        case Mode.Only_Selected:

                            if (Files != null)
                            {
                                foreach (String PF in Files)
                                {
                                    if (f.Name.Contains(PF))
                                    {
                                        try
                                        {
                                            f.IsReadOnly = false;
                                            f.Attributes = FileAttributes.Normal;
                                        }
                                        catch
                                        {
                                        }

                                        //la eliminacion esta separada en otro try para que si falla lo anterior 
                                        //de todas formas se internte eliminar el archivo 
                                        try
                                        {
                                            f.Delete();
                                        }
                                        catch (Exception ex)
                                        {
                                            Zamba.Core.ZClass.raiseerror(ex);
                                        }
                                    }
                                }
                            }

                            break;
                    }
                }
                catch
                {
                }
            }

            foreach (DirectoryInfo subfolder in folder.GetDirectories())
                EmptyCacheFolder(subfolder, mode, Files);
        }

        /// <summary> 
        /// Method for emptying IE cache, keeps files of FilesToKeep list 
        /// </summary> 
        /// <returns></returns> 
        public static void ClearCacheFilesExceptSelected(List<string> FilesToKeep)
        {
            EmptyCacheFolder(new DirectoryInfo(InternetCache), Mode.All_Except_Selected, FilesToKeep);
        }

        /// <summary> 
        /// Method for emptying IE cache, only deletes files of FilesToDelete list 
        /// </summary> 
        /// <returns></returns> 
        public static void ClearCacheFilesOnlySelected(List<string> FilesToDelete)
        {
            EmptyCacheFolder(new DirectoryInfo(InternetCache), Mode.Only_Selected, FilesToDelete);
        }

        /// <summary> 
        /// Method for emptying IE cache, deletes all files 
        /// </summary> 
        /// <returns></returns> 
        public static void ClearCacheFilesFull()
        {
            EmptyCacheFolder(new DirectoryInfo(InternetCache), Mode.All, null);
        }
    }
}
