using System.Windows;

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

        /// <summary>
        /// Overrides default show/initialize component action.
        /// Override occurs when Show is called with parameter.
        /// </summary>
        /// <param name="callingForm">The form initializing the .Show call. Usually 'this' will suffice</param>
        public frmUserLogin(MainWindow callingForm)
        {
            InitializeComponent();
            parentForm = callingForm;

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //ToDo : Check user auth against database
            if (edtLoginUsername.Text == edtLoginPassword.Password)
            {
                Close();
            }
        }
    }
}
