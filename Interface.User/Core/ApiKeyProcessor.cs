using Keesh.Interface.User.Data;
using Keesh.Interface.User.Framework;
using Keesh.Interface.User.Model;
using Keesh.Interface.User.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.Core
{
    public class ApiKeyProcessor : IApiKeyProcessor
    {
        private static string _keyValue; // caching the key value here. when the key value changes, then drop this cache.
        private readonly IApiKeyDataProcessor _dataProcessor;

        public ApiKeyProcessor(IApiKeyDataProcessor apiKeyDataProcessor)
        {
            _dataProcessor = apiKeyDataProcessor;
        }

        public ApiKey GetApiKey(Framework.ISettings settings)
        {
            if (_keyValue == null)
                _keyValue = _dataProcessor.GetKey() ?? string.Empty;
            return new ApiKey
            {
                Key = _keyValue
            };
        }

        public void SaveApiKey(Framework.ISettings settings, ApiKey apiKey)
        {
            _keyValue = null;
            string value = (apiKey.Key ?? string.Empty).Trim();
            if (value == string.Empty)
                value = null;
            _dataProcessor.SetKey(value);
        }
    }
}
