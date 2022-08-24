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

namespace FastReportTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CustomReports customReports = new CustomReports();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Run Report");
           string result =  customReports.GenerateReport(customReports.TestReport(),true,true,@"C:\temp");
            Debug.Write(result);
        }

        private void btnLoadFromFile_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Run Report from File");
            string result = customReports.GenerateReport(customReports.LoadReportFromFile(@"C:\temp\receipt1.frx"), true, true, @"C:\temp");
            Debug.Write(result);
        }
    }
}
