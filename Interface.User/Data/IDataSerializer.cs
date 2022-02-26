using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.Data
{
    public interface IDataSerializer
    {
        Task<T> LoadJsonData<T>(string fileName);
        Task SaveJsonData(string fileName, object data);
        Task<List<T>> LoadCsvData<T>(string fileName);
        Task SaveCsvData<T>(string fileName, IEnumerable<T> data);
    }
}
