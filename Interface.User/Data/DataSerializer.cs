using CsvHelper;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.Data
{
    public class DataSerializer : IDataSerializer
    {
        public async Task<List<T>> LoadCsvData<T>(string fileName)
        {
            return await Task.Run<List<T>>(() =>
            {
                List<T> result = null;
                if (File.Exists(fileName))
                {
                    using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                    using (StreamReader reader = new StreamReader(stream, Encoding.Unicode))
                    using (CsvReader csvReader = new CsvReader(reader, CultureInfo.CurrentCulture))
                    {
                        result = csvReader.GetRecords<T>().ToList();
                    }
                }
                return result;
            });
        }

        public async Task<T> LoadJsonData<T>(string fileName)
        {
            return await Task.Run<T>(() =>
            {
                T result = default;
                if (File.Exists(fileName))
                {
                    using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                    using (StreamReader reader = new StreamReader(fileStream))
                    using (JsonTextReader jsonReader = new JsonTextReader(reader))
                    {
                        JsonSerializer serializer = new JsonSerializer()
                        {
                            ContractResolver = new DefaultContractResolver()
                        };
                        result = serializer.Deserialize<T>(jsonReader);
                    }
                }
                return result;
            });
        }

        public async Task SaveCsvData<T>(string fileName, IEnumerable<T> data)
        {
            await Task.Run(() =>
            {
                if (File.Exists(fileName))
                    File.Delete(fileName);
                if (data != null)
                {
                    using (FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
                    using (StreamWriter writer = new StreamWriter(stream, Encoding.Unicode))
                    using (CsvWriter csvWriter = new CsvWriter(writer, CultureInfo.CurrentCulture))
                    {
                        csvWriter.WriteRecords(data);
                    }
                }
            });
        }

        public async Task SaveJsonData(string fileName, object data)
        {
            await Task.Run(() =>
            {
                using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                using (StreamWriter writer = new StreamWriter(fileStream))
                using (JsonTextWriter jsonWriter = new JsonTextWriter(writer))
                {
                    JsonSerializer serializer = new JsonSerializer()
                    {
                        ContractResolver = new DefaultContractResolver(),
                        Formatting = Formatting.Indented
                    };
                    serializer.Serialize(jsonWriter, data);
                }
            });
        }
    }
}
