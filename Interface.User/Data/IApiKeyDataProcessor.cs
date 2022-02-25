using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.Data
{
    public interface IApiKeyDataProcessor
    {
        string GetKey(ISettings settings);
        void SetKey(ISettings settings, string key);
    }
}
