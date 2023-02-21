using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace mainModules
{
    /// <summary>
    /// Interaction logic for frmAccountsMain.xaml
    /// </summary>
    public partial class frmAccountsMain : Window
    {
        private DebtorContext _contextDebtor =
          new DebtorContext();

        private CollectionViewSource debtorViewSource;

        public frmAccountsMain()
        {
            InitializeComponent();

           debtorViewSource =
               (CollectionViewSource)FindResource(nameof(debtorViewSource));

            InitDB();

        }

        private void btnAddDebtor_Click(object sender, RoutedEventArgs e)
        {
            frmDebtorEntry _screen_DebtorEntry = new frmDebtorEntry();
            _screen_DebtorEntry.ShowDialog();
            RefreshDebtors();
        }

        private void RefreshDebtors()
        {
            _contextDebtor.Dispose();
            _contextDebtor = new DebtorContext();
            InitDB();
        }


        private void InitDB()
        {
            _contextDebtor.Debtors.Load();

            // bind to the source
            debtorViewSource.Source =
                _contextDebtor.Debtors.Local.ToObservableCollection();
        }
    }
}
