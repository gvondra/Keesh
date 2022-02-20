using Autofac;
using AutoMapper;
using Keesh.Interface.User.Framework;
using Keesh.Interface.User.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Keesh.Interface.User.NavigationPage
{
    /// <summary>
    /// Interaction logic for ProtoType.xaml
    /// </summary>
    public partial class ProtoType : Page
    {
        private readonly IContainer _container;        

        public ProtoType()
        {
            InitializeComponent();
            _container = DependencyInjection.ContainerFactory.Container;
            using (ILifetimeScope scope = _container.BeginLifetimeScope())
            {
                InitializeData(scope);
            }
            this.DataContext = ApiKeyVM;
        }

        public ViewModel.ApiKeyVM ApiKeyVM { get; set; }

        private void InitializeData(ILifetimeScope scope)
        {
            ISettingsFactory settingsFactory = scope.Resolve<ISettingsFactory>();
            ISettings settings = settingsFactory.Create();
            IApiKeyProcessor apiKeyProcessor = scope.Resolve<IApiKeyProcessor>();
            Model.ApiKey apiKey = apiKeyProcessor.GetApiKey(settings);
            IMapper mapper = new Mapper(Mapping.Configuration.MapperConfiguration);
            ApiKeyVM = mapper.Map<ViewModel.ApiKeyVM>(apiKey);            
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (ILifetimeScope scope = _container.BeginLifetimeScope())
                {
                    IMapper mapper = new Mapper(Mapping.Configuration.MapperConfiguration);
                    Model.ApiKey apiKey = mapper.Map<Model.ApiKey>(ApiKeyVM);
                    ISettingsFactory settingsFactory = scope.Resolve<ISettingsFactory>();
                    ISettings settings = settingsFactory.Create();
                    IApiKeyProcessor apiKeyProcessor = scope.Resolve<IApiKeyProcessor>();
                    apiKeyProcessor.SaveApiKey(settings, apiKey);
                }
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }
    }
}
