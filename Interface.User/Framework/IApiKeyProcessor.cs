using Keesh.Interface.User.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.Framework
{
    public interface IApiKeyProcessor
    {
        ApiKey GetApiKey(ISettings settings);
        void SaveApiKey(ISettings settings, ApiKey apiKey);
    }
}
