using Avalonia.Controls;
using Avalonia.Interactivity;
using FastReportTest;
using System.Diagnostics;

namespace FastReportsTest_Avaloniaui
{
    public partial class MainWindow : Window
    {

        CustomReports customReports = new CustomReports();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string result = customReports.GenerateReport(customReports.TestReport(), true, true, @"C:\temp");
            Debug.Write(result);
        }

        private void btnLoadFromFile_Click(object sender, RoutedEventArgs e)
        {
            string result = customReports.GenerateReport(customReports.LoadReportFromFile(@"C:\temp\receipt1.frx"), true, true, @"C:\temp");
            Debug.Write(result);
        }
    }
}
