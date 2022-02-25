using AlphaVantageModels = Keesh.Interface.AlphaVantage.Models;
using AutoMapper;
using Keesh.Interface.AlphaVantage;
using Keesh.Interface.User.Data;
using Keesh.Interface.User.Framework;
using Keesh.Interface.User.Mapping;
using Keesh.Interface.User.Model;
using Keesh.Interface.User.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.Core
{
    public class CompanyOverviewFactory : ICompanyOverviewFactory
    {
        private readonly ICompanyOverviewDataProcessor _dataProcessor;
        private readonly IFundamentalDataService _fundamentalDataService;
        private readonly ISettingsFactory _settingsFactory;

        public CompanyOverviewFactory(ISettingsFactory settingsFactory, 
            IFundamentalDataService fundamentalDataService,
            ICompanyOverviewDataProcessor companyOverviewDataProcessor)
        {
            _settingsFactory = settingsFactory;
            _fundamentalDataService = fundamentalDataService;
            _dataProcessor = companyOverviewDataProcessor;
        }

        public async Task<CompanyOverview> Get(string symbol, string apiKey)
        {
            CompanyOverview companyOverview = await _dataProcessor.Get(symbol);
            if (companyOverview == null)
            {
                AlphaVantageModels.CompanyOverview companyOverviewAlphaVantage = await _fundamentalDataService.GetCompanyOverview(_settingsFactory.CreateAlphaVantage(), symbol, apiKey);
                if (companyOverviewAlphaVantage != null)
                {
                    IMapper mapper = new Mapper(Configuration.MapperConfigurationAlphaVantage);
                    companyOverview = mapper.Map<CompanyOverview>(companyOverviewAlphaVantage);
                    await _dataProcessor.Save(symbol, companyOverview);
                }
            }
            return companyOverview;
        }
    }
}
