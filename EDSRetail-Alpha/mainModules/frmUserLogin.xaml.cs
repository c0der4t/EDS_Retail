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
    /// Interaction logic for frmUserLogin.xaml
    /// </summary>
    public partial class frmUserLogin : Window
    {
        MainWindow parentForm;
        public frmUserLogin()
        {
            InitializeComponent();
        }

        public frmUserLogin(MainWindow callingForm)
        {
            InitializeComponent();
            parentForm = callingForm;

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (edtLoginUsername.Text == edtLoginPassword.Password)
            {
                Close();
            }
        }
    }
}
