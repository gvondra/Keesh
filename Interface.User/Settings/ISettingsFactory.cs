using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.Settings
{
    public interface ISettingsFactory
    {
        Keesh.Interface.User.Framework.ISettings Create();
    }
}
