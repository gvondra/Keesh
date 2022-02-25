using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.Data
{
    public class DataSerializer : IDataSerializer
    {
        public async Task<T> LoadData<T>(string fileName)
        {
            return await Task.Run<T>(() =>
            {
                T result = default;
                if (File.Exists(fileName))
                {
                    using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        using (StreamReader reader = new StreamReader(fileStream))
                        {
                            using (JsonTextReader jsonReader = new JsonTextReader(reader))
                            {
                                JsonSerializer serializer = new JsonSerializer()
                                {
                                    ContractResolver = new DefaultContractResolver()
                                };
                                result = serializer.Deserialize<T>(jsonReader);
                            }
                        }
                    }
                }
                return result;
            });
        }

        public async Task SaveData(string fileName, object data)
        {
            await Task.Run(() =>
            {
                using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                {
                    using (StreamWriter writer = new StreamWriter(fileStream))
                    {
                        using (JsonTextWriter jsonWriter = new JsonTextWriter(writer))
                        {
                            JsonSerializer serializer = new JsonSerializer()
                            {
                                ContractResolver = new DefaultContractResolver(),
                                Formatting = Formatting.Indented
                            };
                            serializer.Serialize(jsonWriter, data);
                        }
                    }
                }
            });
        }
    }
}
