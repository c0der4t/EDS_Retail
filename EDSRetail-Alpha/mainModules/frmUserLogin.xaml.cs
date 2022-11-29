using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace mainModules
{
    /// <summary>
    /// Interaction logic for frmUserLogin.xaml
    /// </summary>
    public partial class frmUserLogin : Window
    {
        MainWindow parentForm;

        private UserContext _contextUser =
          new UserContext();

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
            _contextUser.Users.Load();

        }




        #region Custom Methods

 

        private void VerifyLogin()
        {
            using (var _localcontextUsers = new UserContext())
            {
                var userItem = _localcontextUsers.Users
                           .Single(x => x.Username == edtLoginUsername.Text);

                if (securityAPI.Decryption.VerifyStringAgainstHash(edtLoginPassword.Password, userItem.Password))
                {
                    Debug.WriteLine($"User logged in:{userItem.ID} - {userItem.Username}");
                    Close();
                }
                else
                {
                    MessageBox.Show("Login failed");
                }

            }


        }


        #endregion

        #region UI Events

        private void edtLoginUsername_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                edtLoginPassword.Focus();
            }
        }

        private void edtLoginPassword_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                VerifyLogin();
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            VerifyLogin();
        }

        #endregion



    }
}
