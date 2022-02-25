using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.Data
{
    public interface IFilePurger
    {
        Task Purge(DateTime expirationTimestamp, params string[] fileNames);
        Task Purge(DateTime expirationTimestamp, IEnumerable<string> fileNames);
    }
}
