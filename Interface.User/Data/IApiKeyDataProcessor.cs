using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.Data
{
    public interface IApiKeyDataProcessor
    {
        string GetKey();
        void SetKey(string key);
    }
}
