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
using System.Windows.Threading;

namespace Keesh.Interface.User.NavigationPage
{
    /// <summary>
    /// Interaction logic for PriceHistory.xaml
    /// </summary>
    public partial class PriceHistory : Page
    {
        private readonly IContainer _container;

        public PriceHistory()
        {
            _container = DependencyInjection.ContainerFactory.Container;
            NavigationCommands.BrowseBack.InputGestures.Clear();
            NavigationCommands.BrowseForward.InputGestures.Clear();
            InitializeComponent();
        }

        public PriceHistoryVM PriceHistoryVM { get; set; }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            using (ILifetimeScope scope = _container.BeginLifetimeScope())
            {
                InitializeData(scope);
            }
            DataContext = PriceHistoryVM;
        }

        private void InitializeData(ILifetimeScope scope)
        {
            ISettingsFactory settingsFactory = scope.Resolve<ISettingsFactory>();
            IApiKeyProcessor apiKeyProcessor = scope.Resolve<IApiKeyProcessor>();
            IMapper mapper = new Mapper(Mapping.Configuration.MapperConfiguration);
            Model.ApiKey apiKey = apiKeyProcessor.GetApiKey(settingsFactory.Create());
            PriceHistoryVM = new PriceHistoryVM
            {
                ApiKey = mapper.Map<ApiKeyVM>(apiKey)
            };
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(PriceHistoryVM.ApiKey?.Key) && !string.IsNullOrEmpty(PriceHistoryVM.TickerSymbol))
                {
                    Task.Run<List<Model.PriceItem>>(() => GetDailyPrices(PriceHistoryVM.ApiKey.Key, PriceHistoryVM.TickerSymbol))
                        .ContinueWith(GetDailyPricesCallback, null, TaskScheduler.FromCurrentSynchronizationContext());
                    Task.Run(() => SaveApiKey(PriceHistoryVM.ApiKey));
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

        private async Task GetDailyPricesCallback(Task<List<Model.PriceItem>> task, object state)
        {
            try
            {
                PriceHistoryVM.PriceItems.Clear();
                using (DispatcherProcessingDisabled dpd = Dispatcher.DisableProcessing())
                foreach (Model.PriceItem priceItem in await task)
                {
                    PriceHistoryVM.PriceItems.Add(priceItem);
                }
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }

        private async Task<List<Model.PriceItem>> GetDailyPrices(string key, string symbol)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(symbol))
                throw new ArgumentNullException(nameof(symbol));
            using (ILifetimeScope scope = _container.BeginLifetimeScope())
            {
                IPriceFactory priceFactory = scope.Resolve<IPriceFactory>();
                return (await priceFactory.GetDaily(symbol, key)).ToList();
            }
        }
    }
}
