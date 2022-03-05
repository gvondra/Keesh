using Autofac;
using AutoMapper;
using Keesh.Interface.User.Framework;
using Keesh.Interface.User.Settings;
using Keesh.Interface.User.ViewBehavior;
using Keesh.Interface.User.ViewModel;
using Microsoft.Win32;
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
    /// Interaction logic for Portfolio.xaml
    /// </summary>
    public partial class Portfolio : Page
    {
        private readonly IContainer _container;

        public Portfolio()
        {
            _container = DependencyInjection.ContainerFactory.Container;
            NavigationCommands.BrowseBack.InputGestures.Clear();
            NavigationCommands.BrowseForward.InputGestures.Clear();
            InitializeComponent();
        }

        public PortfolioVM PortfolioVM { get; set; }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            using (ILifetimeScope scope = _container.BeginLifetimeScope())
            {
                InitializeData(scope);
            }
            DataContext = PortfolioVM;
            this.PortfolioVM.Items.CollectionChanged += Items_CollectionChanged;
        }

        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {            
            if (e.NewItems != null)
            using(ILifetimeScope scope = _container.BeginLifetimeScope())
            foreach(object item in e.NewItems)
            {
                    ((PortfolioItemVM)item).AddBehavior(
                        new PortfolioItemPriceLookup((PortfolioItemVM)item, scope.Resolve<IPriceFactory>(), scope.Resolve<IApiKeyProcessor>(), scope.Resolve<ISettingsFactory>())
                        );
            }
        }

        private void InitializeData(ILifetimeScope scope)
        {
            ISettingsFactory settingsFactory = scope.Resolve<ISettingsFactory>();
            IApiKeyProcessor apiKeyProcessor = scope.Resolve<IApiKeyProcessor>();
            IMapper mapper = new Mapper(Mapping.Configuration.MapperConfiguration);
            Model.ApiKey apiKey = apiKeyProcessor.GetApiKey(settingsFactory.Create());
            PortfolioVM = new PortfolioVM
            {
                ApiKey = mapper.Map<ApiKeyVM>(apiKey)
            };
        }

        private void OpenCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog
                {
                    AddExtension = true,
                    CheckFileExists = true,
                    CheckPathExists = true,
                    DefaultExt = "csv",
                    Filter = "csv|*.csv|all|*",
                    FilterIndex = 0,
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    Title = "Open Keesh Profile",
                    ValidateNames = true
                };
                bool? dialogResult = dialog.ShowDialog();
                if (dialogResult.HasValue && dialogResult.Value)
                {
                    PortfolioVM.Items.Clear();
                    Task.Run(() => GetPortfolioItems(dialog.FileName))
                        .ContinueWith(OpenPortfolioItemsCallback, null, TaskScheduler.FromCurrentSynchronizationContext());
                    Task.Run(() => SaveApiKey(PortfolioVM.ApiKey));
                    PortfolioVM.FileName = System.IO.Path.GetFileName(dialog.FileName);
                    PortfolioVM.FilePath = System.IO.Path.GetFullPath(dialog.FileName);
                }
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }

        private async Task<IEnumerable<Model.PortfolioItem>> GetPortfolioItems(string fileName)
        {
            using(ILifetimeScope scope = _container.BeginLifetimeScope())
            {
                IPortfolioProcessor portfolioProcessor = scope.Resolve<IPortfolioProcessor>();
                return await portfolioProcessor.GetItems(fileName);
            }
        }

        private async Task OpenPortfolioItemsCallback(Task<IEnumerable<Model.PortfolioItem>> task, object state)
        {
            try
            {
                IEnumerable<Model.PortfolioItem> items = await task;
                PortfolioVM.Items.Clear();
                if (items != null)
                {
                    IMapper mapper = new Mapper(Mapping.Configuration.MapperConfiguration);
                    foreach (Model.PortfolioItem item in items)
                    {
                        PortfolioItemVM itemVM = new PortfolioItemVM();
                        PortfolioVM.Items.Add(itemVM);
                        mapper.Map<Model.PortfolioItem, PortfolioItemVM>(item, itemVM);
                    }
                }
            }
            catch(Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }

        private void SaveCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                SaveFileDialog dialog = new SaveFileDialog
                {
                    AddExtension = true,
                    CheckFileExists = false,
                    CheckPathExists = true,                    
                    DefaultExt = "csv",
                    Filter = "csv|*.csv|all|*",
                    FilterIndex = 0,
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    OverwritePrompt = false,
                    Title = "Open Keesh Profile",
                    ValidateNames = true
                };
                if (!string.IsNullOrEmpty(PortfolioVM.FilePath) && System.IO.Directory.Exists(PortfolioVM.FilePath))
                    dialog.InitialDirectory = PortfolioVM.FilePath;
                if (!string.IsNullOrEmpty(PortfolioVM.FileName))
                    dialog.FileName = PortfolioVM.FileName;
                bool? dialogResult = dialog.ShowDialog();
                if (dialogResult.HasValue && dialogResult.Value)
                {
                    Task.Run(() => SavePortfolioItems(dialog.FileName))
                        .ContinueWith(SavePortfolioItemsCallback, null, TaskScheduler.FromCurrentSynchronizationContext());
                    Task.Run(() => SaveApiKey(PortfolioVM.ApiKey));
                }
            }
            catch (Exception ex)
            {
                ErrorWindow.Open(ex, Window.GetWindow(this));
            }
        }

        private async Task SavePortfolioItems(string fileName)
        {
            using (ILifetimeScope scope = _container.BeginLifetimeScope())
            {
                IMapper mapper = new Mapper(Mapping.Configuration.MapperConfiguration);
                IPortfolioProcessor portfolioProcessor = scope.Resolve<IPortfolioProcessor>();
                await portfolioProcessor.SaveItems(
                    fileName, 
                    PortfolioVM.Items.Select<PortfolioItemVM, Model.PortfolioItem>(i => mapper.Map<Model.PortfolioItem>(i))
                    );
            }
            
        }

        private void SavePortfolioItemsCallback(Task task, object state)
        {
            MessageBox.Show("Save Complete", "Save", MessageBoxButton.OK, MessageBoxImage.Information);
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
    }
}
