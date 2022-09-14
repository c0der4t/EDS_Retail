using Microsoft.EntityFrameworkCore;
using SQLite_DBTest.Models;
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

namespace SQLite_DBTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly ProductContext _context =
            new ProductContext();

        private CollectionViewSource categoryViewSource;

        public MainWindow()
        {
            InitializeComponent();
            categoryViewSource =
                (CollectionViewSource)FindResource(nameof(categoryViewSource));
        }

        private void btnSave_Click_1(object sender, RoutedEventArgs e)
        {
            // all changes are automatically tracked, including
            // deletes!
            _context.SaveChanges();

            // this forces the grid to refresh to latest values
            categoryDataGrid.Items.Refresh();
            productsDataGrid.Items.Refresh();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // this is for demo purposes only, to make it easier
            // to get up and running
            _context.Database.EnsureCreated();

            // load the entities into EF Core
            _context.Categories.Load();

            // bind to the source
            categoryViewSource.Source =
                _context.Categories.Local.ToObservableCollection();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            // clean up database connections
            _context.Dispose();
        }
    }
}
