using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.Data
{
    public static class CacheDirectory
    {
        public static string GetFileName(string name)
            => Path.Combine(
                GetCacheDirectoryName(),
                name
                );

        public static string GetCacheDirectoryName()
            => Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Temp",
                "Keesh.Cache"
                );

        public static void CreateCacheDirectory()
        {
            if (!Directory.Exists(GetCacheDirectoryName()))
                Directory.CreateDirectory(GetCacheDirectoryName());
        }
    }
}
