using Keesh.Interface.AlphaVantage.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.AlphaVantage
{
    public interface ICalendarService
    {
        Task<List<EarningsCalendarItem>> GetEarningsCalendar(ISettings settings, string apiKey);
    }
}
