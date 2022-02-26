using Autofac;
namespace Keesh.Interface.User.Data
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<ApiKeyDataProcessor>().As<IApiKeyDataProcessor>();
            builder.RegisterType<CalendarDataProcessor>().As<ICalendarDataProcessor>();
            builder.RegisterType<CompanyOverviewDataProcessor>().As<ICompanyOverviewDataProcessor>();
            builder.RegisterType<DataSerializer>().SingleInstance().As<IDataSerializer>();
            builder.RegisterType<FilePurger>().SingleInstance().As<IFilePurger>();
            builder.RegisterType<PortfolioDataProcessor>().As<IPortfolioDataProcessor>();
            builder.RegisterType<PriceDataProcessor>().As<IPriceDataProcessor>();
        }
    }
}
