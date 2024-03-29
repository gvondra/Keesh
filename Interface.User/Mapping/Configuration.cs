﻿using AlphaVantageModels = Keesh.Interface.AlphaVantage.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keesh.Interface.User.Mapping
{
    public class Configuration
    {
        private static readonly MapperConfiguration _mapperConfiguration;
        private static readonly MapperConfiguration _mapperConfigurationAlphaVantage;

        static Configuration()
        {
            _mapperConfiguration = new MapperConfiguration(configExp =>
            {
                configExp.CreateMap<Model.ApiKey, ViewModel.ApiKeyVM>();
                configExp.CreateMap<ViewModel.ApiKeyVM, Model.ApiKey>();
                configExp.CreateMap<Model.CompanyOverview, ViewModel.CompanyOverviewVM>();
                configExp.CreateMap<Model.PortfolioItem, ViewModel.PortfolioItemVM>();
                configExp.CreateMap<ViewModel.PortfolioItemVM, Model.PortfolioItem>();
            });

            _mapperConfigurationAlphaVantage = new MapperConfiguration(configExp =>
            {
                configExp.CreateMap<AlphaVantageModels.CompanyOverview, Model.CompanyOverview>();
                configExp.CreateMap<AlphaVantageModels.EarningsCalendarItem, Model.EarningsCalendarItem>();
                configExp.CreateMap<AlphaVantageModels.IPOCalendarItem, Model.IPOCalendarItem>();
                configExp.CreateMap<AlphaVantageModels.PriceItem, Model.PriceItem>();
            });
        }

        public static MapperConfiguration MapperConfiguration => _mapperConfiguration;
        public static MapperConfiguration MapperConfigurationAlphaVantage => _mapperConfigurationAlphaVantage;
    }
}
