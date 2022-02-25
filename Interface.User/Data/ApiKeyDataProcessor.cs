using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.Data
{
    public class ApiKeyDataProcessor : IApiKeyDataProcessor
    {
        private const string FILE_NAME = "api-key.dat";

        public string GetKey()
        {
            string key = null;
            string fileName = CacheDirectory.GetFileName(FILE_NAME);
            if (File.Exists(fileName))
            {
                using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (BinaryReader reader = new BinaryReader(stream, Encoding.Unicode))
                    {
                        key = reader.ReadString();
                    }
                }
            }
            return key;
        }

        public void SetKey(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                CacheDirectory.CreateCacheDirectory();
                using (FileStream stream = new FileStream(CacheDirectory.GetFileName(FILE_NAME), FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    using (BinaryWriter writer = new BinaryWriter(stream, Encoding.Unicode))
                    {
                        writer.Write(key);
                    }
                }
            }
            else if (File.Exists(CacheDirectory.GetFileName(FILE_NAME)))
                File.Delete(CacheDirectory.GetFileName(FILE_NAME));
        }
    }
}
