using mainModules.Models;
using Microsoft.EntityFrameworkCore;
using securityAPI;
using System;
using System.IO;
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

        private readonly _dbContext _mainContext =
            new _dbContext();

        public MainWindow()
        {
            InitializeComponent();
            InitDB();
            //CheckUserAuth();

        }

        #region Custom Methods
        //Any method written by a dev. This is non standard / non boilerplate code

        private void CheckUserAuth()
        {
            int LoopCounter = 0;
            while (!authToken.IsUserAuthorized())
            {
                if (LoopCounter >= 3)
                {
                    MessageBox.Show("Login avoidance detected. Closing application");
                    Environment.Exit(0);
                }
                frmUserLogin loginscreen = new frmUserLogin();
                loginscreen.ShowDialog();
                LoopCounter += 1;
            }

            lblGreetings.Text = $"Welcome, {authToken.GetFirstName()}";

        }

        /// <summary>
        /// Ensures DB folder and DB tables exist. If not, creates them
        /// </summary>
        private void InitDB()
        {
            //Ref: https://learn.microsoft.com/en-us/ef/core/get-started/wpf#add-code-that-handles-data-interaction

            if (Directory.Exists(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "db")))
            {
                Directory.CreateDirectory(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "db"));
            }

            //This checks if all tables are created, else creates them
            _mainContext.Database.EnsureCreated();
        }

        #endregion

        #region UI Events
        //Any method that is triggered by a UI element action/event

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
            authToken.DeauthorizeCurrentUser();
            CheckUserAuth();
        }


        #endregion

    }
}
