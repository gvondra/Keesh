using Autofac;
using AutoMapper;
using Keesh.Interface.User.Framework;
using Keesh.Interface.User.Settings;
using Keesh.Interface.User.ViewModel;
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
    /// Interaction logic for CompanyOverview.xaml
    /// </summary>
    public partial class CompanyOverview : Page
    {
        private readonly IContainer _container;

        public CompanyOverview()
        {
            _container = DependencyInjection.ContainerFactory.Container;
            NavigationCommands.BrowseBack.InputGestures.Clear();
            NavigationCommands.BrowseForward.InputGestures.Clear();
            InitializeComponent();
        }

        public CompanyOverviewVM CompanyOverviewVM { get; set; }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            using (ILifetimeScope scope = _container.BeginLifetimeScope())
            {
                InitializeData(scope);
            }
            DataContext = CompanyOverviewVM;
        }

        private void InitializeData(ILifetimeScope scope)
        {
            ISettingsFactory settingsFactory = scope.Resolve<ISettingsFactory>();
            IApiKeyProcessor apiKeyProcessor = scope.Resolve<IApiKeyProcessor>();
            IMapper mapper = new Mapper(Mapping.Configuration.MapperConfiguration);
            Model.ApiKey apiKey = apiKeyProcessor.GetApiKey(settingsFactory.Create());
            CompanyOverviewVM = new CompanyOverviewVM()
            {
                ApiKey = mapper.Map<ApiKeyVM>(apiKey)
            };
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(CompanyOverviewVM.ApiKey?.Key) && !string.IsNullOrEmpty(CompanyOverviewVM.TickerSymbol))
                {
                    Task.Run<Model.CompanyOverview>(() => GetCompanyOverview(CompanyOverviewVM.ApiKey.Key, CompanyOverviewVM.TickerSymbol))
                        .ContinueWith(GetCompanyOverviewCallback, null, TaskScheduler.FromCurrentSynchronizationContext());
                    Task.Run(() => SaveApiKey(CompanyOverviewVM.ApiKey));
                }
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }

        private void SaveApiKey(ApiKeyVM apiKey)
        {
            using (ILifetimeScope scope = _container.BeginLifetimeScope())
            {
                ISettingsFactory settingsFactory = scope.Resolve<ISettingsFactory>();
                IApiKeyProcessor apiKeyProcessor = scope.Resolve<IApiKeyProcessor>();
                IMapper mapper = new Mapper(Mapping.Configuration.MapperConfiguration);
                apiKeyProcessor.SaveApiKey(settingsFactory.Create(), mapper.Map<Model.ApiKey>(apiKey));
            }
        }

        private async Task GetCompanyOverviewCallback(Task<Model.CompanyOverview> task, object state)
        {
            try
            {
                Model.CompanyOverview data = await task;
                IMapper mapper = new Mapper(Mapping.Configuration.MapperConfiguration);
                mapper.Map<Model.CompanyOverview, CompanyOverviewVM>(data, this.CompanyOverviewVM);
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }

        private async Task<Model.CompanyOverview> GetCompanyOverview(string key, string symbol)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(symbol))
                throw new ArgumentNullException(nameof(symbol));
            using (ILifetimeScope scope = _container.BeginLifetimeScope())
            {
                ISettingsFactory settingsFactory = scope.Resolve<ISettingsFactory>();
                ICompanyOverviewFactory companyOverviewFactory = scope.Resolve<ICompanyOverviewFactory>();
                return await companyOverviewFactory.Get(settingsFactory.Create(), symbol, key);
            }
        }
    }
}
