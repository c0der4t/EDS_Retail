using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace GenerateMaintenanceTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            edtLibraryFilePath.Text = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "tasklibrary");
            MessageBox.Show("This is a tool for devs.\nThis tool has no input validation.\nBe careful!", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void btnAddtoLibrary_Click(object sender, RoutedEventArgs e)
        {
            string command = Convert.ToBase64String(Encoding.UTF8.GetBytes(edtCommand.Text));
            string taskName = edtTaskName.Text.Replace(',', ' ');
            string actionName = edtActionName.Text.Replace(',', ' ');
            string descr = edtDescr.Text.Replace(',', ' ');

            using (StreamWriter library = new StreamWriter(edtLibraryFilePath.Text, append:true))
            {
                library.WriteLine($"{taskName},{actionName},{descr},{command}");
            }
        }

        private void btnFindLibraryFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();

            if (dialog.FileName != string.Empty)
            {
                edtLibraryFilePath.Text = dialog.FileName;
            }
        }
    }
}
