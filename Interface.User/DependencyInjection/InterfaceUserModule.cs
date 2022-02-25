using Autofac;
using Keesh.Interface.User.Core;
using Keesh.Interface.User.Framework;
using Keesh.Interface.User.Settings;
namespace Keesh.Interface.User.DependencyInjection
{
    public class InterfaceUserModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<SettingsFactory>().As<ISettingsFactory>();
            builder.RegisterType<ApiKeyProcessor>().As<IApiKeyProcessor>();
            builder.RegisterType<CompanyOverviewFactory>().As<ICompanyOverviewFactory>();
            builder.RegisterType<EnvironmentVariableProcessor>().As<IEnvironmentVariableProcessor>();
        }
    }
}
