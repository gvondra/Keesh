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
            builder.RegisterType<DataSerializer>().As<IDataSerializer>();
            builder.RegisterType<FilePurger>().As<IFilePurger>();
            builder.RegisterType<PriceDataProcessor>().As<IPriceDataProcessor>();
        }
    }
}
