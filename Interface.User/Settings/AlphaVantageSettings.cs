using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.Settings
{
    public class AlphaVantageSettings : Keesh.Interface.AlphaVantage.ISettings
    {
        public string BaseAddress { get; set; }
    }
}
