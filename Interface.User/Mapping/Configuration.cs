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

        static Configuration()
        {
            _mapperConfiguration = new MapperConfiguration(configExp =>
            {
                configExp.CreateMap<Model.ApiKey, ViewModel.ApiKeyVM>();
                configExp.CreateMap<ViewModel.ApiKeyVM, Model.ApiKey>();
            });
        }

        public static MapperConfiguration MapperConfiguration => _mapperConfiguration;
    }
}
