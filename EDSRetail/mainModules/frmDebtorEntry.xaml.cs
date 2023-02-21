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
using System.Windows.Shapes;

namespace mainModules
{
    /// <summary>
    /// Interaction logic for frmDebtorEntry.xaml
    /// </summary>
    public partial class frmDebtorEntry : Window
    {
        public frmDebtorEntry()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            databaseAPI.Models.Debtor newDebtor = new databaseAPI.Models.Debtor();

            newDebtor.AccountNumber = edtAccountNum.Text;
            newDebtor.AccName = edtAccName.Text;
            newDebtor.Address = edtAddress.Text;
            newDebtor.VatNum = edtVatNum.Text;
            newDebtor.ContactPerson = edtContactPerson.Text;
            newDebtor.ContactNumber = edtContactPhone.Text;
            newDebtor.ContactEmail= edtContactEmail.Text;
            newDebtor.APEmail = edtAPEmail.Text;
            newDebtor.OurAccNum = edtOurAccNum.Text;
            newDebtor.AccountBalance = 0;

            using (DebtorContext _contextDebtor = new DebtorContext())
            {
                _contextDebtor.Debtors.Add(newDebtor);

                _contextDebtor.SaveChanges();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
