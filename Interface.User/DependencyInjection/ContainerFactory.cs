using Autofac;
namespace Keesh.Interface.User.DependencyInjection
{
    public class ContainerFactory
    {
        private static readonly IContainer _container;

        static ContainerFactory()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterModule(new InterfaceUserModule());
            _container = builder.Build();
        }

        public static IContainer Container => _container;
    }
}
