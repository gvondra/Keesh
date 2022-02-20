using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Keesh.Interface.User
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void DataProviderHyperlink_Click(object sender, RoutedEventArgs e)
        {   
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                UseShellExecute = true,
                Verb = "Open",
                FileName = ((Hyperlink)sender).NavigateUri.ToString()
            };
            Process.Start(startInfo);
        }

        private void GoToPageCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NavigationService navigationService = navigationFrame.NavigationService;
            //NavigationService navigationService = NavigationService.GetNavigationService(navigationFrame);
            navigationService.Navigate(new Uri((string)e.Parameter, UriKind.Relative));
        }
    }
}
