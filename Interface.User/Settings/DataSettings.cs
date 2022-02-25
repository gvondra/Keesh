using Keesh.Interface.User.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.Settings
{
    public class DataSettings : ISettings
    {
        public string CacheFolderName { get; set; }
    }
}
