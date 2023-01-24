using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace EDSRetail_SetupWizard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            lblDBTabDescr.Text = "\n\nChoose where to house the EDS Retail database files\nYou can house the database anywhere as long as the location has read/write permissions granted.\n\nIf you are setting up EDS Retail to work only on your machine, the default setting should be fine.\n\n(Note, you can always move the database to another location in the future)";
            LoadCurrSettingstoUI();
        }

        private void LoadCurrSettingstoUI()
        {
            //Load db.ini if exists
            if (File.Exists(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "db.ini")))
            {

                edtDBLocationPath.Text = File.ReadAllLines(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "db.ini"))[0];

            }
            else
            {
                edtDBLocationPath.Text = "%appdata%\\EDSRetail\\db";
            }
           

        }

        private void FinishWizard()
        {
            //Write DB location to INI File

            using (StreamWriter dbINI = new StreamWriter(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory,"db.ini")))
            {
                dbINI.WriteLine(edtDBLocationPath.Text);
            }

            //Notify user of changes
            MessageBox.Show("You can now launch EDS retail","Setup Completed",MessageBoxButton.OK,MessageBoxImage.Information);
            this.Close();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {

            if (btnNext.Content != "Finish")
            {
                tbctrlWizard.SelectedIndex += 1;
            }
            else
            {
                FinishWizard();
            }
            
        }

        private void tbctrlWizard_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (tbctrlWizard.SelectedIndex >= 2)
            {
                btnNext.Content = "Finish";
            }
            else
            {
                btnNext.Content = "Finish";
            }
        }

        private void BrowseForDB(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dbLocation = new FolderBrowserDialog();
            dbLocation.ShowDialog();

            if (dbLocation.SelectedPath != String.Empty)
            {
                edtDBLocationPath.Text = dbLocation.SelectedPath;
            }

        }
    }
}
