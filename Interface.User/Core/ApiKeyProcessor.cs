using Keesh.Interface.User.Framework;
using Keesh.Interface.User.Model;
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
        private readonly IEnvironmentVariableProcessor _environmentVariableProcessor;

        public ApiKeyProcessor(IEnvironmentVariableProcessor environmentVariableProcessor)
        {
            _environmentVariableProcessor = environmentVariableProcessor;
        }

        public ApiKey GetApiKey(ISettings settings)
        {
            if (_keyValue == null)
                _keyValue = _environmentVariableProcessor.GetValue(settings.ApiKeyVariableName) ?? string.Empty;
            return new ApiKey
            {
                Key = _keyValue
            };
        }

        public void SaveApiKey(ISettings settings, ApiKey apiKey)
        {
            _keyValue = null;
            string value = (apiKey.Key ?? string.Empty).Trim();
            if (value == string.Empty)
                value = null;
            _environmentVariableProcessor.SetValue(settings.ApiKeyVariableName, value);
        }
    }
}
