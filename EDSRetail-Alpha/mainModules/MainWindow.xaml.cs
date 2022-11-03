
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace mainModules
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string appLocation { get; set; }
        public bool isUserAuthenticated { get; set; }

        private readonly SalesContext _contextSales =
            new SalesContext();


        public MainWindow()
        {
            InitializeComponent();
            CheckUserAuth();
            InitDB();

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

        private void InitDB()
        {
            //TODO : Always ensure the DB folder exists
            _contextSales.Database.EnsureCreated();

            _contextSales.Sales.Load();
        }
    }
}
