using AlphaVantageModels = Keesh.Interface.AlphaVantage.Models;
using AutoMapper;
using Keesh.Interface.AlphaVantage;
using Keesh.Interface.User.Data;
using Keesh.Interface.User.Framework;
using Keesh.Interface.User.Model;
using Keesh.Interface.User.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.Core
{
    public class CalendarFactory : ICalendarFactory
    {
        private readonly ICalendarService _calendarService;
        private readonly ISettingsFactory _settingsFactory;
        private readonly ICalendarDataProcessor _dataProcessor;

        public CalendarFactory(ICalendarService calendarService,
            ISettingsFactory settingsFactory,
            ICalendarDataProcessor calendarDataProcessor)
        {
            _calendarService = calendarService;
            _settingsFactory = settingsFactory;
            _dataProcessor = calendarDataProcessor;
        }

        public async Task<IEnumerable<EarningsCalendarItem>> GetEarnings(string apiKey)
        {
            IEnumerable<EarningsCalendarItem> earningsCalendar = await _dataProcessor.GetEarningsData();
            if (earningsCalendar == null)
            {
                List<AlphaVantageModels.EarningsCalendarItem> calendarAlphaVantage = await _calendarService.GetEarningsCalendar(_settingsFactory.CreateAlphaVantage(), apiKey);
                if (calendarAlphaVantage != null)
                {
                    IMapper mapper = new Mapper(Mapping.Configuration.MapperConfigurationAlphaVantage);
                    earningsCalendar = calendarAlphaVantage.Select<AlphaVantageModels.EarningsCalendarItem, EarningsCalendarItem>(i => mapper.Map<EarningsCalendarItem>(i));
                    await _dataProcessor.SaveEarningsData(earningsCalendar);
                }
            }
            return earningsCalendar.OrderBy(i => i.ReportDate);
        }
    }
}
