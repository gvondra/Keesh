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
        Task<CompanyOverview> Get(string symbol);
        Task Save(string symbol, CompanyOverview data);
    }
}
