using Autofac;
using Keesh.Interface.AlphaVantage;

namespace Keesh.Interface.User.DependencyInjection
{
    public static class ContainerFactory
    {
        private static readonly IContainer _container;

        static ContainerFactory()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterModule(new InterfaceUserModule());
            builder.RegisterModule(new Data.DataModule());
            builder.RegisterModule(new InterfaceAlphaVantageModule());
            _container = builder.Build();
        }

        public static IContainer Container => _container;
    }
}
