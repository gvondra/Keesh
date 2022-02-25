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

        public string GetKey(ISettings settings)
        {
            string key = null;
            string fileName = GetFileName(settings);
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

        public void SetKey(ISettings settings, string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                using (FileStream stream = new FileStream(GetFileName(settings), FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    using (BinaryWriter writer = new BinaryWriter(stream, Encoding.Unicode))
                    {
                        writer.Write(key);
                    }
                }
            }
            else if (File.Exists(GetFileName(settings)))
                File.Delete(GetFileName(settings));
        }

        private string GetFileName(ISettings settings)
            => Path.Combine(settings.CacheFolderName, FILE_NAME);
    }
}
