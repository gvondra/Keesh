using Autofac;
using BrassLoon.RestClient;
namespace Keesh.Interface.AlphaVantage
{
    public class InterfaceAlphaVantageModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<Service>().As<IService>();
            builder.RegisterType<CalendarService>().As<ICalendarService>();
            builder.RegisterType<FundamentalDataService>().As<IFundamentalDataService>();
        }
    }
}
