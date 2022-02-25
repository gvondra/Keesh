using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.Data
{
    public class FilePurger : IFilePurger
    {        
        // expiration timestamp represents the time before which files are considered expired
        public async Task Purge(DateTime expirationTimestamp, IEnumerable<string> fileNames)
        {
            expirationTimestamp = expirationTimestamp.ToUniversalTime();
            await Task.Run(() =>
            {
                foreach (string fileName in fileNames.Where(f => File.Exists(f)))
                {
                    if (File.GetLastWriteTimeUtc(fileName) < expirationTimestamp)
                        File.Delete(fileName);
                }
            });
        }

        public Task Purge(DateTime expirationTimestamp, params string[] fileNames)
        {
            return Purge(expirationTimestamp, (IEnumerable<string>)fileNames);
        }
    }
}
