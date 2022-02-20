using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.Settings
{
    public class SettingsFactory : ISettingsFactory
    {
        public Keesh.Interface.User.Framework.ISettings Create()
        {
            return new Settings()
            { 
                ApiKeyVariableName = Properties.Settings.Default.ApiKeyVariable
            };
        }
    }
}
