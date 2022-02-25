using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.Data
{
    public interface IDataSerializer
    {
        Task<T> LoadData<T>(string fileName);
        Task SaveData(string fileName, object data);
    }
}
