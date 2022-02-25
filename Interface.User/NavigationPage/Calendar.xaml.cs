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
    /// Interaction logic for Calendar.xaml
    /// </summary>
    public partial class Calendar : Page
    {
        private readonly IContainer _container;

        public Calendar()
        {
            _container = DependencyInjection.ContainerFactory.Container;
            NavigationCommands.BrowseBack.InputGestures.Clear();
            NavigationCommands.BrowseForward.InputGestures.Clear();
            InitializeComponent();
        }

        public CalendarVM CalendarVM { get; set; }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            using (ILifetimeScope scope = _container.BeginLifetimeScope())
            {
                InitializeData(scope);
            }
            DataContext = CalendarVM;
        }

        private void InitializeData(ILifetimeScope scope)
        {
            ISettingsFactory settingsFactory = scope.Resolve<ISettingsFactory>();
            IApiKeyProcessor apiKeyProcessor = scope.Resolve<IApiKeyProcessor>();
            IMapper mapper = new Mapper(Mapping.Configuration.MapperConfiguration);
            Model.ApiKey apiKey = apiKeyProcessor.GetApiKey(settingsFactory.Create());
            CalendarVM = new CalendarVM()
            {
                ApiKey = mapper.Map<ApiKeyVM>(apiKey)
            };
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(CalendarVM.ApiKey?.Key))
                {
                    Task.Run(() => GetEarningsCalendar(CalendarVM.ApiKey.Key))
                        .ContinueWith(GetEarningsCalendarCallback, null, TaskScheduler.FromCurrentSynchronizationContext());
                    Task.Run(() => GetIpoCalendar(CalendarVM.ApiKey.Key))
                        .ContinueWith(GetIpoCalendarCallback, null, TaskScheduler.FromCurrentSynchronizationContext());
                    Task.Run(() => SaveApiKey(CalendarVM.ApiKey));
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

        private async Task GetEarningsCalendarCallback(Task<List<Model.EarningsCalendarItem>> task, object state)
        {
            try
            {                
                CalendarVM.EarningsCalendarItems.Clear();
                using(DispatcherProcessingDisabled dpd = Dispatcher.DisableProcessing())
                foreach (Model.EarningsCalendarItem item in await task)
                {
                    CalendarVM.EarningsCalendarItems.Add(item);
                }
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }

        private async Task<List<Model.EarningsCalendarItem>> GetEarningsCalendar(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            using (ILifetimeScope scope = _container.BeginLifetimeScope())
            {
                ICalendarFactory calendarFactory = scope.Resolve<ICalendarFactory>();
                return (await calendarFactory.GetEarnings(key)).ToList();
            }
        }

        private async Task GetIpoCalendarCallback(Task<List<Model.IPOCalendarItem>> task, object state)
        {
            try
            {
                CalendarVM.IpoCalendarItems.Clear();
                using (DispatcherProcessingDisabled dpd = Dispatcher.DisableProcessing())
                foreach (Model.IPOCalendarItem item in await task)
                {
                    CalendarVM.IpoCalendarItems.Add(item);
                }
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }

        private async Task<List<Model.IPOCalendarItem>> GetIpoCalendar(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            using (ILifetimeScope scope = _container.BeginLifetimeScope())
            {
                ICalendarFactory calendarFactory = scope.Resolve<ICalendarFactory>();
                return (await calendarFactory.GetIPO(key)).ToList();
            }
        }
    }
}
