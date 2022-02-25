using Keesh.Interface.User.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.Data
{
    public interface ICalendarDataProcessor
    {
        Task<IEnumerable<EarningsCalendarItem>> GetEarningsData();
        Task SaveEarningsData(IEnumerable<EarningsCalendarItem> earningsCalendarItems);
        Task<IEnumerable<IPOCalendarItem>> GetIpoData();
        Task SaveIpoData(IEnumerable<IPOCalendarItem> earningsCalendarItems);
    }
}
