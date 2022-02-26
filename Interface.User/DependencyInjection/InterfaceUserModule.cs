using Autofac;
using Keesh.Interface.User.Core;
using Keesh.Interface.User.Framework;
using Keesh.Interface.User.Settings;
using Keesh.Interface.User.ViewBehavior;
namespace Keesh.Interface.User.DependencyInjection
{
    public class InterfaceUserModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<SettingsFactory>().SingleInstance().As<ISettingsFactory>();
            builder.RegisterType<ApiKeyProcessor>().As<IApiKeyProcessor>();
            builder.RegisterType<CalendarFactory>().As<ICalendarFactory>();
            builder.RegisterType<CompanyOverviewFactory>().As<ICompanyOverviewFactory>();
            builder.RegisterType<PortfolioProcessor>().As<IPortfolioProcessor>();
            builder.RegisterType<PriceFactory>().As<IPriceFactory>();
        }
    }
}
