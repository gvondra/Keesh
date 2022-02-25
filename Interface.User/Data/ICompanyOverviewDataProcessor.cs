using Keesh.Interface.User.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.Data
{
    public interface ICompanyOverviewDataProcessor
    {
        Task<CompanyOverview> Get(ISettings settings, string symbol);
        Task Save(ISettings settings, string symbol, CompanyOverview data);
    }
}
