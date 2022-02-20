using Keesh.Interface.User.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.Core
{
    public class EnvironmentVariableProcessor : IEnvironmentVariableProcessor
    {
#if DEBUG
        private readonly EnvironmentVariableTarget _environmentVariableTarget = EnvironmentVariableTarget.Process;
#else
        private readonly EnvironmentVariableTarget _environmentVariableTarget = EnvironmentVariableTarget.User;
#endif
        public string GetValue(string key) => Environment.GetEnvironmentVariable(key, _environmentVariableTarget);

        public void SetValue(string key, string value) => Environment.SetEnvironmentVariable(key, value, _environmentVariableTarget);
    }
}
