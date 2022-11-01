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
using System.Diagnostics;

namespace mainModules
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string appLocation { get; set; }
        public bool isUserAuthenticated { get; set; }


        public MainWindow()
        {
            InitializeComponent();
            CheckUserAuth();
        }

        private void CheckUserAuth()
        {
            frmUserLogin loginscreen = new frmUserLogin();
            loginscreen.ShowDialog();

        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            frmsettings_main _settings_screen = new frmsettings_main();
            _settings_screen.Show();
        }

        private void btnStock_Click(object sender, RoutedEventArgs e)
        {
            stock_mgmt _stockMgmt = new stock_mgmt();
            _stockMgmt.Show();
        }

        private void btnPOS_Click(object sender, RoutedEventArgs e)
        {
           point_of_sale _posWindow = new point_of_sale();
            _posWindow.Show();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
